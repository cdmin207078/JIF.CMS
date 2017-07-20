; (function () {

    var _tr_background_color = 'rgb(255,255,255)';
    var _tr_odd_background_color = 'rgb(251,251,251)';

    // 交换行信息
    var exchangeRow = function ($top, $topChilds, $bottom, $bottomChilds) {

        $top.find('.op-td a').addClass('disabled');
        $bottom.find('.op-td a').addClass('disabled');

        if ($topChilds) {
            $topChilds.find('.op-td a').addClass('disabled');
        }
        if ($bottomChilds) {
            $bottomChilds.find('.op-td a').addClass('disabled');
        }

        $bottom.insertBefore($top);

        if ($bottomChilds)
            $bottomChilds.insertBefore($top);

        $bottom.add($bottomChilds).stop().animate({ 'backgroundColor': '#fdff6d' }, 250).animate({ 'backgroundColor': '#fff' }, 250, function () {
            $bottom.find('.op-td a').removeClass('disabled');

            if ($bottomChilds)
                $bottomChilds.find('.op-td a').removeClass('disabled');
        });

        $top.add($topChilds).stop().animate({ 'backgroundColor': '#fdff6d' }, 250).animate({ 'backgroundColor': '#fff' }, 250, function () {
            $top.find('.op-td a').removeClass('disabled');

            if ($topChilds)
                $topChilds.find('.op-td a').removeClass('disabled');
        });

    }

    // 升级行信息
    var upgradeRow = function () {

    }

    var init = function () {

        // tr hover
        $(document).on('mouseenter', '.table-hover > tbody > tr', function () {
            $(this).css('backgroundColor', 'rgba(130, 130, 130, 0.12)');
        });

        $(document).on('mouseleave', '.table-hover > tbody > tr', function () {
            $(this).css('backgroundColor', 'rgb(255,255,255)');
        });

        // 上移分类
        $(document).on('click', '.op-td a:has(> .fa-caret-up)', function () {

            var $top, $topChilds, $bottom, $bottomChilds;

            $bottom = $(this).parents('tr').eq(0);

            if ($bottom.attr('data-has-child') == 1)
                $bottomChilds = $bottom.next();

            if (!$bottom.prev().length)
                return;

            // 同级
            if ($bottom.prev().attr('data-has-child') == undefined) {
                $topChilds = $bottom.prev();
                $top = $topChilds.prev();
            } else {
                $top = $bottom.prev();
            }

            exchangeRow($top, $topChilds, $bottom, $bottomChilds);
        });

        // 下移分类
        $(document).on('click', '.op-td a:has(> .fa-caret-down)', function () {
            var $top, $topChilds, $bottom, $bottomChilds;

            $top = $(this).parents('tr').eq(0);

            if (!$top.next().length)
                return;

            if ($top.attr('data-has-child') == 1)
                $topChilds = $top.next();

            if ($topChilds && !$topChilds.next().length) {
                return;
            }

            if ($topChilds) {
                $bottom = $topChilds.next();
            } else {
                $bottom = $top.next();
            }

            if ($bottom.attr('data-has-child') == 1) {
                $bottomChilds = $bottom.next();
            }

            exchangeRow($top, $topChilds, $bottom, $bottomChilds);

        });

        // 编辑分类
        $(document).on('click', '.op-td a:has(> .fa-cog)', function () {

            var cate_id = $(this).parent().attr('data-cate-id');
            var cate_name = $(this).parent().attr('data-cate-name');

            $.confirm({
                content: function () {
                    var self = this;
                    return $.get({
                        url: '/article/categoryinfo/' + cate_id
                    }).done(function (response) {
                        self.setContent(response);
                        self.setTitle('编辑分类');
                    }).fail(function () {
                        self.setContent('Something went wrong.');
                    });
                }
            });
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