; (function () {

    var _editor;

    var init = function () {

        $('.datepicker').datepicker({
            format: 'yyyy-mm-dd',
            language: 'zh-CN'
        });

        _editor = editormd({
            id: 'txt-content',
            height: 650,
            path: '/content/editor.md/lib/',
            placeholder: '文如流沙, 思若泉涌',
            saveHTMLToTextarea: true,   // 保存 HTML 到 Textarea - 加上这个才能markdown转html
        });


        // 保存按钮 - 点击
        $('#btn-save').click(function () {

            var data = {
                id: $('#inp-id').val(),
                title: $('#inp-title').val(),
                markdownContent: _editor.getMarkdown(),
                content: _editor.getHTML(),
                categoryId: $('#inp-category').val(),
                allowComments: $('#inp-allow-comments').is(':checked'),
                isPublished: $('#inp-pub').is(':checked')
            };

            if (!data.title.trim()) {
                _cms.err('标题不能为空');
                return;
            }

            if (!data.content.trim()) {
                _cms.err('内容不能为空')
                return;
            }

            $.post('/article/save', data, function (result) {
                if (result.success) {
                    _cms.ok('保存成功');
                } else {
                    _cms.err(result.message);
                }
            });
        });
    }

    $(function () {
        init();
    });
})();