var attachmentUpload = (function ($) {

    var $wrap = $('#uploader'),

        // 上传按钮
        $upload = $wrap.find('.upload-btn'),

        // 添加的文件数量
        fileCount = 0,

        // 添加的文件总大小
        fileSize = 0,

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

    var init = function () {
        initWebUploader();
        initElemEvents();
    }

    var getChunkSize = function () {
        // 大于 5M, 则需要分割
        return 1024 * 1024 * 5;
    }

    var setState = function (val) {
        var stats;

        if (val === state) {
            return;
        }

        $upload.removeClass('state-' + state);
        $upload.addClass('state-' + val);
        state = val;

        switch (state) {
            case 'pedding':
                //$queue.hide();
                //$statusBar.addClass('element-invisible');
                uploader.refresh();
                break;

            case 'ready':
                $('#filePicker2').removeClass('element-invisible');
                //$queue.show();
                //$statusBar.removeClass('element-invisible');
                uploader.refresh();
                break;

            case 'uploading':
                $('#filePicker2').addClass('element-invisible');
                $progress.show();
                $upload.text('暂停上传');
                break;

            case 'paused':
                $progress.show();
                $upload.text('继续上传');
                break;

            case 'confirm':
                $progress.hide();
                $('#filePicker2').removeClass('element-invisible');
                $upload.text('开始上传');

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

    var initWebUploader = function () {

        // 实例化
        uploader = WebUploader.create({
            // 选择文件的按钮。可选。
            // 内部根据当前运行是创建，可能是input元素，也可能是flash.
            pick: '#picker',

            // swf文件路径
            swf: '~/Content/webuploader-0.1.5/Uploader.swf',

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
            threads: 1
        });

        // 文件被添加进队列的时候触发
        uploader.on('fileQueued', function (file) {
            fileCount++;
            fileSize += file.size;

            var innerText = doT.template($('#dt-upload-item').text());
            $('#uploader-list').append(innerText(file));

            setState('ready');
        });

        // 某个文件开始上传前触发，一个文件只会触发一次
        uploader.on('uploadStart', function (file) {
            console.log('[uploadStart]' + file.name);
        });

        // 当某个文件的分块在发送前触发，主要用来询问是否要添加附带参数，大文件在开起分片上传的前提下此事件可能会触发多次
        uploader.on('uploadBeforeSend', function (object, data, headers) {
            //console.info('-----------------  uploadBeforeSend - start  -----------------');

            //console.log(object);

            ////data.Say = 'hello world.';

            //console.log(data);

            //this.md5File(object.file, object.start, object.end).then(function (ret) {
            //    console.warn('uploadBeforeSend : md5 = ' + ret);
            //});

            ////console.log(headers);

            //console.info('-----------------  uploadBeforeSend - end  -----------------');
        });

        // 文件上传过程中创建进度条实时显示。
        uploader.on('uploadProgress', function (file, percentage) {
            var $li = $('#' + file.id);
            var $percent = $li.find('.progress .progress-bar');

            $percent.css('width', percentage * 100 + '%');
        });

        // 文件上传成功时触发
        uploader.on('uploadSuccess', function (file) {
            var $fi = $('#' + file.id);
            $fi.find('.progress').removeClass('active');
        });

        // 文件上传出错时触发
        uploader.on('uploadError', function (file) {
            $('#' + file.id).find('p.state').text('上传出错');
            //console.error('file : ' + file.name + " - uploadError")
        });

        // 不管成功或者失败，文件上传完成时触发
        uploader.on('uploadComplete', function (file) {
            //$('#' + file.id).find('.progress').fadeOut();
            //console.log('file : ' + file.name + " - uploadComplete")

            console.info('[uploadComplete] ' + file.name);

            // 暂停上传队列
            //uploader.stop();

        });

        // 当某个文件上传到服务端响应后，会派送此事件来询问服务端响应是否有效。
        uploader.on('uploadAccept', function (object, ret) {
            //console.log(object);
        });
    }

    var initElemEvents = function () {

        $upload.on('click', function () {

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


        //iCheck for checkbox and radio inputs
        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_minimal-blue'
        });

    }

    return {
        init: init,
        getChunkSize: getChunkSize
    };

})(jQuery);


WebUploader.Uploader.register({
    'before-send-file': 'before_send_file'
}, {
    before_send_file: function (file) {

        console.log(file);
        console.assert('before-send-file');

        var me = this,
            owner = this.owner,
            server = me.options.server,
            deferred = WebUploader.Deferred();

        var chunkSize = attachmentUpload.getChunkSize();

        if (file.size < chunkSize) {
            deferred.resolve();
        } else {
            var data = {
                fname: file.name,
                fsize: file.size,
                lastModifiedDate: file.lastModifiedDate
            };

            $.get('/attachment/bigfileprecheck', data, function (result) {

                console.log(result);

                //if (result.success) {
                //    deferred.resolve();
                //}
            });
        }



        return deferred.promise();

        //owner.md5File(file.source)
        //    .fail(function () {
        //        // 如果读取出错了，则通过reject告诉webuploader文件上传出错。

        //        deferred.reject();
        //    }).then(function (md5) {

        //        console.log(md5);

        //        //deferred.resolve();
        //        //// 与服务安验证
        //        //$.ajax(server, {
        //        //    dataType: 'json',
        //        //    data: {
        //        //        md5: ret
        //        //    },
        //        //    success: function (response) {

        //        //        // 如果验证已经上传过
        //        //        if (response.exist) {
        //        //            owner.skipFile(file);

        //        //            console.log('文件重复，已跳过');
        //        //        }

        //        //        // 介绍此promise, webuploader接着往下走。
        //        //        deferred.resolve();
        //        //    }
        //        //});
        //    });

        //return deferred.promise();
    }
});