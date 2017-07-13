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
            path: '../content/editor.md/lib/',
            placeholder: '文如流沙, 思若泉涌'
        });
    }

    $(function () {
        init();
    });
})();