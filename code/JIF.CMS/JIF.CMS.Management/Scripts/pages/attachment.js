﻿; (function () {

    var $wrap = $('#uploader'),

        // 开始上传文件按钮
        $uploadBtn = $('#btn-file-upload'),

        // 开始上传文件按钮
        $uploadCancelBtn = $('#btn-cancel-choose'),

        // 上传文件 - 拖拽区域
        $uploadDnd = $wrap.find('.uploader-dnd'),
        // 上传文件 - 文件列表
        $uploadlist = $wrap.find('.uploader-list'),

        // 添加的文件队列
        files = [],

        // 添加的文件数量
        fileCount = 0,

        // 添加的文件总大小
        fileSize = 0,

        //// 验证文件总数量, 超出则不允许加入队列
        //fileNumLimit = 5,

        // 可能有pedding, ready, uploading, confirm, done.
        state = 'pedding',

        // 所有文件的进度信息，key为file id
        percentages = {},

        // 判断浏览器是否支持图片的base64
        isSupportBase64 = (function () {
            var data = new Image();
            var support = true;
            data.onload = data.onerror = function () {
                if (this.width != 1 || this.height != 1) {
                    support = false;
                }
            }
            data.src = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
            return support;
        })(),

        // 检测是否已经安装flash，检测flash的版本
        flashVersion = (function () {
            var version;

            try {
                version = navigator.plugins['Shockwave Flash'];
                version = version.description;
            } catch (ex) {
                try {
                    version = new ActiveXObject('ShockwaveFlash.ShockwaveFlash')
                        .GetVariable('$version');
                } catch (ex2) {
                    version = '0.0';
                }
            }
            version = version.match(/\d+/g);
            return parseFloat(version[0] + '.' + version[1], 10);
        })(),

        // WebUploader实例
        uploader;

    if (!WebUploader.Uploader.support('flash') && WebUploader.browser.ie) {

        // flash 安装了但是版本过低。
        if (flashVersion) {
            (function (container) {
                window['expressinstallcallback'] = function (state) {
                    switch (state) {
                        case 'Download.Cancelled':
                            alert('您取消了更新！')
                            break;

                        case 'Download.Failed':
                            alert('安装失败')
                            break;

                        default:
                            alert('安装已成功，请刷新！');
                            break;
                    }
                    delete window['expressinstallcallback'];
                };

                var swf = './expressInstall.swf';
                // insert flash object
                var html = '<object type="application/' +
                    'x-shockwave-flash" data="' + swf + '" ';

                if (WebUploader.browser.ie) {
                    html += 'classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" ';
                }

                html += 'width="100%" height="100%" style="outline:0">' +
                    '<param name="movie" value="' + swf + '" />' +
                    '<param name="wmode" value="transparent" />' +
                    '<param name="allowscriptaccess" value="always" />' +
                    '</object>';

                container.html(html);

            })($wrap);

            // 压根就没有安转。
        } else {
            $wrap.html('<a href="http://www.adobe.com/go/getflashplayer" target="_blank" border="0"><img alt="get flash player" src="http://www.adobe.com/macromedia/style_guide/images/160x41_Get_Flash_Player.jpg" /></a>');
        }

        return;
    } else if (!WebUploader.Uploader.support()) {
        alert('Web Uploader 不支持您的浏览器！');
        return;
    }

    // 获取chunk 文件分片大小
    var getChunkSize = function () {
        // 大于 5M, 则需要分割
        return 1024 * 1024 * 5;
    }

    // 获取文件上传列表html对象
    var getFileItemElement = function (fid) {
        return $uploadlist.find('.item[data-file-id=' + fid + ']');
    }

    // 获得文件分片数量
    var getFileUploadChunks = function (fsize) {

        var chunkSize = getChunkSize();

        // 文件大小小于分片大小, 无需分片
        if (fsize <= chunkSize)
            return 1;

        var chunks = fsize / chunkSize;

        if (chunks % 1 !== 0) {
            chunks = chunks + 1;
        }

        return parseInt(chunks);
    }

    var setState = function (val) {
        var stats;

        if (val === state) {
            return;
        }

        $uploadBtn.removeClass('state-' + state);
        $uploadBtn.addClass('state-' + val);
        state = val;

        switch (state) {
            case 'pedding':
                //$queue.hide();
                //$statusBar.addClass('element-invisible');
                uploader.refresh();
                break;

            case 'ready':
                uploader.refresh();
                break;

            case 'uploading':
                $uploadBtn.text('暂停上传');
                break;

            case 'paused':
                $uploadBtn.text('继续上传');
                break;

            case 'confirm':
                $uploadBtn.text('开始上传');

                stats = uploader.getStats();
                if (stats.successNum && !stats.uploadFailNum) {
                    setState('finish');
                    return;
                }
                break;
            case 'finish':
                stats = uploader.getStats();
                if (stats.successNum) {
                    alert('上传成功');
                } else {
                    // 没有成功的图片，重设
                    state = 'done';
                    location.reload();
                }
                break;
        }

        updateStatus();
    }

    var updateStatus = function () {

        //var text = '', stats;

        //if (state === 'ready') {
        //    text = '选中' + fileCount + '张图片，共' +
        //            WebUploader.formatSize(fileSize) + '。';
        //} else if (state === 'confirm') {
        //    stats = uploader.getStats();
        //    if (stats.uploadFailNum) {
        //        text = '已成功上传' + stats.successNum + '张照片至XX相册，' +
        //            stats.uploadFailNum + '张照片上传失败，<a class="retry" href="#">重新上传</a>失败图片或<a class="ignore" href="#">忽略</a>'
        //    }

        //} else {
        //    stats = uploader.getStats();
        //    text = '共' + fileCount + '张（' +
        //            WebUploader.formatSize(fileSize) +
        //            '），已上传' + stats.successNum + '张';

        //    if (stats.uploadFailNum) {
        //        text += '，失败' + stats.uploadFailNum + '张';
        //    }
        //}

        //$info.html(text);
    }

    var init = function () {

        // 全局hook 绑定, 必须在 实例化之前执行
        initHook();

        initWebUploader();

        initElemEvents();
    }

    var initHook = function () {

        WebUploader.Uploader.register({
            'before-send-file': 'before_send_file',
            'before-send': 'before_send',
        }, {
                before_send_file: function (file) {
                    //console.info('[hook - before_send_file]');

                    var me = this,
                        deferred = WebUploader.Deferred();
                    var chunkSize = getChunkSize();


                    if (file.size < chunkSize) {
                        deferred.resolve();
                    } else {
                        var data = {
                            fname: file.name,
                            fsize: file.size,
                            lastModifiedTimestamp: Date.parse(file.lastModifiedDate)
                        };

                        $.post('/attachment/bigfileprecheck', data, function (res) {
                            if (res.success) {
                                if (res.data.mode == 1) {
                                    me.options.existsChunks = res.data.chunks.split(',');
                                }

                                deferred.resolve();
                            }
                        });
                    }

                    return deferred.promise();

                }, before_send: function (file) {

                    var deferred = WebUploader.Deferred();

                    var me = this,
                        existsChunks = me.options.existsChunks;

                    if (existsChunks && existsChunks.indexOf(file.chunk.toString()) > -1) {
                        deferred.reject();
                    } else {
                        deferred.resolve();
                    }

                    return deferred.promise();
                }
            });
    }

    var initWebUploader = function () {

        // 实例化
        uploader = WebUploader.create({
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: '#picker',

            // 指定Drag And Drop拖拽的容器，如果不指定，则不启动. [默认值：undefined]
            dnd: '#uploader .uploader-dnd',

            // swf文件路径
            swf: '~/Content/webuploader/Uploader.swf',

            // 文件接收服务端。
            server: '/attachment/upload',

            // 开启文件分片
            chunked: true,

            // 如果要分片，分多大一片. 默认大小为5M.
            //chunkSize: 5242880,
            //chunkSize: 1048576,

            // 如果某个分片由于网络问题出错，允许自动重传多少次. 默认 2 次
            //chunkRetry: 2,

            // 图片不压缩
            compress: false,

            // 上传并发数。允许同时最大上传进程数。 [默认值：3]
            //threads: 1,
        });

        // 文件被添加进队列的时候触发
        uploader.on('fileQueued', function (file) {

            console.log(uploader.getStats());

            files.push({ id: file.id, file: file });
            fileCount++;
            fileSize += file.size;

            var innerText = doT.template($('#dt-upload-item').text());
            $uploadlist.append(innerText(file));

            $uploadDnd.hide();
            $uploadlist.show();

            setState('ready');
        });

        // 当文件被移除队列后触发
        uploader.on('fileDequeued', function (file) {

            var rf = _.remove(files, function (f) { return f.id == file.id; });

            if (rf) {

                fileCount--;
                fileSize -= file.size;

                getFileItemElement(file.id).fadeOut('fast');

                if (!files.length) {
                    $uploadDnd.show();
                    $uploadlist.hide();
                }
            }
        });

        // 某个文件开始上传前触发，一个文件只会触发一次
        uploader.on('uploadStart', function (file) {
            var $li = getFileItemElement(file.id);

            $li.find('.rm-file').show();
            $li.find('.pause-file').show();
        });

        // 当某个文件的分块在发送前触发，主要用来询问是否要添加附带参数，大文件在开起分片上传的前提下此事件可能会触发多次
        uploader.on('uploadBeforeSend', function (object, data, headers) {
            //console.info('-----------------  uploadBeforeSend - start  -----------------');
            //console.info('[uploadBeforeSend]');

            // 重设文件时间戳
            data.lastModifiedDate = Date.parse(data.lastModifiedDate);

            //console.info('-----------------  uploadBeforeSend - end  -----------------');
        });

        // 文件上传过程中创建进度条实时显示。
        uploader.on('uploadProgress', function (file, percentage) {
            var $li = getFileItemElement(file.id);
            var $percent = $li.find('.progress .progress-bar');

            $percent.css('width', percentage * 100 + '%');
        });


        // 文件上传开始, 若file有值, 则表示是继续上传, 否则则为首次上传
        uploader.on('startUpload', function (file) {
            if (file) {
                console.info('[startUpload] - ' + file.name);
            } else {
                console.info('[startUpload] - 首次上传');
            }

            $uploadBtn.attr('disabled', 'disabled');
            $uploadCancelBtn.attr('disabled', 'disabled');
        });

        // 当所有文件上传结束时触发
        uploader.on('uploadFinished', function () {
            console.info('[uploadFinished] - All trans over.');

            $uploadBtn.removeAttr('disabled');
            $uploadCancelBtn.removeAttr('disabled');
        });

        // 文件上传暂停
        uploader.on('stopUpload', function (file) {
            //console.log('[stopUpload]', file);
        });

        // 文件上传成功时触发
        uploader.on('uploadSuccess', function (file) {

            var $li = getFileItemElement(file.id);
            var chunks = getFileUploadChunks(file.size);

            if (chunks === 1) {
                $li.find('.upload-item-status').text('上传成功').addClass('text-green');

                $li.find('.btn-sm').remove();
                $li.find('.progress').remove();

            } else {

                var data = {
                    name: file.name,
                    size: file.size,
                    lastModifiedTimestamp: Date.parse(file.lastModifiedDate),
                    chunks: getFileUploadChunks(file.size)
                };

                $.post('/attachment/mergeFile', data, function (result) {

                    if (result.success) {
                        $li.find('.upload-item-status').text('上传成功').addClass('text-green');
                    } else {
                        $li.find('.upload-item-status').text('上传成功, 合并失败.  信息：' + result.message).addClass('text-red');
                    }

                    $li.find('.btn-sm').remove();
                    $li.find('.progress').remove();
                });
            }

        });

        // 文件上传出错时触发
        uploader.on('uploadError', function (file) {
            $('#' + file.id).find('p.state').text('上传出错');
        });

        // 不管成功或者失败，文件上传完成时触发
        uploader.on('uploadComplete', function (file) {
            console.info('[uploadComplete] ' + file.name);
        });

        // 当某个文件上传到服务端响应后，会派送此事件来询问服务端响应是否有效。
        uploader.on('uploadAccept', function (object, ret) {
            //console.log(object);
        });
    }

    var initElemEvents = function () {

        $uploadBtn.on('click', function () {

            if ($(this).hasClass('disabled')) {
                return false;
            }

            if (state === 'ready') {
                //console.log(uploader.getFiles());
                uploader.upload();
            } else if (state === 'paused') {
                uploader.upload();
            } else if (state === 'uploading') {
                uploader.stop();
            }
        });

        // 弹出选择文件对话框 - 隐藏这个id容器。同时，在自定义的按钮的click事件上手动触发input的click事件 https://github.com/fex-team/webuploader/issues/2341
        $('#picker-link, #btn-file-picker').on('click', function () {
            $('#picker input').click();
        });

        // 上传 - 按钮 - 打开上传界面区域
        $('#btn-open-choose').on('click', function () {
            $wrap.stop();
            $wrap.slideDown('fast');

            $('#btn-open-choose').hide();
            $uploadBtn.show();
            $('#btn-file-picker').show();
            $uploadCancelBtn.show();
        });

        // 上传 - 按钮 - 关闭上传区域, 清空上传队列
        $uploadCancelBtn.on('click', function () {

            if ($uploadCancelBtn.attr('disabled') === 'disabled') {
                return;
            }

            $wrap.stop();
            $wrap.slideUp('fast');

            if (files) {
                for (var i = 0; i < files.length; i++) {
                    uploader.removeFile(files[i].id);
                    i--;
                }
            }

            $('#btn-open-choose').show();
            $uploadBtn.hide();
            $('#btn-file-picker').hide();
            $uploadCancelBtn.hide();
        });

        // 文件上传 - 文件列表 - 删除文件
        $(document).on('click', '#uploader .uploader-list .item .rm-file', function () {
            var fid = $(this).attr('data-file-id');

            // 从队列中删除文件
            uploader.removeFile(fid);
        });

        // 文件上传 - 文件列表 - 暂停上传
        $(document).on('click', '#uploader .uploader-list .item .pause-file', function () {
            var fid = $(this).attr('data-file-id');

            var f = _.find(files, function (o) { return o.id == fid; });

            uploader.stop(f.file);

            $(this).hide();
            $(this).siblings('.upload-file').show();
        });

        // 文件上传 - 文件列表 - 暂停之后, 继续上传
        $(document).on('click', '#uploader .uploader-list .item .upload-file', function () {
            var fid = $(this).attr('data-file-id');

            uploader.upload(fid);

            $(this).hide();
            $(this).siblings('.pause-file').show();
        });
    }


    $(function () {
        init();
    });
})();