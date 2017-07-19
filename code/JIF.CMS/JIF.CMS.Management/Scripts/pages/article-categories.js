; (function () {

    var _tr_background_color = 'rgb(255,255,255)';
    var _tr_odd_background_color = 'rgb(251,251,251)';


    var init = function () {

        // 新增子类
        $(document).on('click', '.op-td a:has(> .fa-plus)', function () {
            alert('add child!');
        });

        // 上移分类
        $(document).on('click', '.op-td a:has(> .fa-caret-up)', function () {

            var $bottom = $(this).parents('tr').eq(0);
            var $top = $bottom.prev();

            if ($bottom.attr('data-lock-move') || $top.attr('data-lock-move')) {
                return;
            }

            $bottom.attr('data-lock-move', true);
            $top.attr('data-lock-move', true);

            if ($top.html()) {
                // 同级交换

                $bottom.insertBefore($top);

                var bottomColor = $bottom.css('backgroundColor');
                var topColor = $top.css('backgroundColor');

                $bottom.stop().animate({ 'backgroundColor': '#fdff6d' }, 500).animate({ 'backgroundColor': topColor }, 500, function () {
                    $bottom.removeAttr('data-lock-move');
                });

                $top.stop().animate({ 'backgroundColor': '#fdff6d' }, 500).animate({ 'backgroundColor': bottomColor }, 500, function () {
                    $top.removeAttr('data-lock-move');
                });
            } else {
                // 升级

                alert('none prev');
            }
        });

        // 下移分类
        $(document).on('click', '.op-td a:has(> .fa-cog)', function () {
            alert('option!');
        });

        // 下移分类
        $(document).on('click', '.op-td a:has(> .fa-caret-down)', function () {
            alert('down!');
        });

        // 删除分类
        $(document).on('click', '.op-td a:has(> .fa-trash)', function () {
            alert('remove!');
        });
    }

    $(function () {
        init();
    });
})();