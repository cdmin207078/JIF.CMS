; (function () {

    var _tr_background_color = 'rgb(255,255,255)',
        _tr_odd_background_color = 'rgb(251,251,251)',
        uploader;

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

    // 初始化上传组件
    var initUpload = function () {
        uploader = WebUploader.create({
            pick: '#picker',
            auto: true,
            swf: '~/Content/webuploader/Uploader.swf',
            server: '/article/uploadcategorycoverimg',
            // 不压缩image, 默认如果是jpeg，文件上传前会压缩一把再上传
            resize: false,
        });

        // 文件上传成功时触发
        uploader.on('uploadSuccess', function (file, response) {
            if (response.success) {
                console.log(response.message);
                $('#category-cover-img').attr('src', response.message).siblings('i').hide();
            }
        });
    }

    // 加载分类信息编辑页
    var loadCategoryInfo = function (id) {
        $.get({
            url: '/article/categoryinfo/' + id
        }).done(function (ret) {
            $('#category-info .box-header').nextAll().remove();
            $('#category-info .box-header').after(ret);

            initUpload();
        });
    }

    // 加载分类列表
    var loadCategories = function () {
        $.get('/article/_categories', function (ret) {
            $('#category-list').html(ret);
        });

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
                columnClass: 'medium',
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
                },
                onContentReady: function () {
                    // 上传组件初始化
                    initUpload();
                },
                buttons: {
                    save: {
                        text: '保存',
                        action: function () {
                            var id = this.$content.find('#hid-id').val();

                            var data = {
                                parentid: this.$content.find('#inp-category').val(),
                                name: this.$content.find('#inp-name').val(),
                                coverimg: this.$content.find('.category-cover-img').attr('src'),
                                description: this.$content.find('#inp-desc').val()
                            };

                            $.ajax({
                                url: '/article/savecategory/' + id,
                                data: data,
                                type: 'post',
                                success: function () {
                                    console.log('self ajax');

                                    $.alert({
                                        title: '消息',
                                        content: '保存成功',
                                        autoClose: 'done|3000',
                                        buttons: {
                                            done: {
                                                text: '确定 ',
                                                action: function () {

                                                }
                                            }
                                        }
                                    });
                                }
                            });
                        }
                    },
                    cancel: {
                        text: '取消',
                        action: function () { }
                    }
                }
            });
        });



        // 分类详情 - 新增
        $('#category-add').click(function () {
            loadCategoryInfo(0);
        });

        // 分类列表 - 编辑
        $(document).on('click', '#category-list .cate-modify', function () {
            loadCategoryInfo($(this).attr('data-id'));
        });

        // 分类详情 - 保存
        $(document).on('click', '#category-info .box-footer .btn-save', function () {
            var $this = $(this);
            var id = $this.attr('data-id');

            var data = {
                parentid: $('#inp-category').val(),
                name: $('#inp-name').val(),
                coverimg: $('#category-cover-img').attr('src'),
                description: $('#inp-desc').val()
            };

            $.ajax({
                url: '/article/savecategory/' + id,
                data: data,
                type: 'post',
                success: function (ret) {
                    $.alert({
                        type: ret.success ? 'green' : 'red',
                        title: ret.success ? '消息' : '错误',
                        content: ret.success ? '保存成功' : ret.message,
                        buttons: {
                            done: {
                                text: '确定 ',
                                action: function () {
                                    if (ret.success) {
                                        loadCategories();
                                        loadCategoryInfo(-1);
                                    }
                                }
                            }
                        }
                    });
                }
            });
        });

        // 分类详情 - 取消
        $(document).on('click', '#category-info .box-footer .btn-cancel', function () {
            loadCategoryInfo(-1);
        });

        // 分类详情 - 删除
        $(document).on('click', '#category-info .box-footer .btn-del', function () {
            var $this = $(this);
            var id = $this.attr('data-id');
            var name = $this.attr('data-name');

            $.confirm({
                type: 'orange',
                title: '信息',
                content: '确定要删除 分类 - [' + name + '], 删除后所属此分类下的文章, 将自动归档到 "未分类".',
                buttons: {
                    ok: {
                        text: '确定',
                        btnClass: 'btn-red',
                        action: function () {
                            $.post('/article/deleteCategory/' + id).done(function (ret) {
                                $.alert({
                                    type: ret.success ? 'green' : 'red',
                                    title: ret.success ? '消息' : '错误',
                                    content: ret.success ? '删除成功' : ret.message,
                                    buttons: {
                                        done: {
                                            text: '确定 ',
                                            action: function () {
                                                if (ret.success) {
                                                    loadCategories();
                                                    loadCategoryInfo(-1);
                                                }
                                            }
                                        }
                                    }
                                });
                            });
                        }
                    },
                    cancel: {
                        text: '取消',
                        action: function () { }
                    }
                }
            });
        });

        // 分类详情 - 选择封面图片
        $(document).on('click', '#btn-picker-cover-img', function () {
            $('#picker input').click();
        });


    }



    $(function () {
        init();
    });
})();