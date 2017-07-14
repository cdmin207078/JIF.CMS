
$(function () {
    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue',
    });

    //$('input[type="checkbox"].minimal, input[type="radio"].minimal').on('ifChecked', function (event) {
    //    alert($(this).is(':checked'));
    //});

    //$('input[type="checkbox"].minimal, input[type="radio"].minimal').on('ifUnchecked', function (event) {
    //    alert($(this).is(':checked'));
    //});


    // 检查页面是否需要弹出错误提醒
    var exp = $('#JIFExceptionMessage');
    if (exp.length) {
        $.alert({
            type: 'red',
            title: '失败',
            content: exp.text()
        });
    }
});


var _cms = (function () {

    var err = function (content) {
        $.alert({
            type: 'red',
            title: false,
            content: content
        });
    }

    var ok = function (content) {
        $.alert({
            type: 'green',
            title: '信息',
            content: content,
            //backgroundDismiss: true,
            //buttons:[]
        });
    }

    var info = function (content) {
        $.alert({
            type: 'blue',
            title: false,
            content: content
        });
    }


    /*1.用浏览器内部转换器实现html转码*/
    var htmlEncode = function (html) {
        //1.首先动态创建一个容器标签元素，如DIV
        var temp = document.createElement("div");
        //2.然后将要转换的字符串设置为这个元素的innerText(ie支持)或者textContent(火狐，google支持)
        (temp.textContent != undefined) ? (temp.textContent = html) : (temp.innerText = html);
        //3.最后返回这个元素的innerHTML，即得到经过HTML编码转换的字符串了
        var output = temp.innerHTML;
        temp = null;
        return output;
    }

    /*2.用浏览器内部转换器实现html解码*/
    var htmlDecode = function (text) {
        //1.首先动态创建一个容器标签元素，如DIV
        var temp = document.createElement("div");
        //2.然后将要转换的字符串设置为这个元素的innerHTML(ie，火狐，google都支持)
        temp.innerHTML = text;
        //3.最后返回这个元素的innerText(ie支持)或者textContent(火狐，google支持)，即得到经过HTML解码的字符串了。
        var output = temp.innerText || temp.textContent;
        temp = null;
        return output;
    }

    /*3.用正则表达式实现html转码*/
    var htmlEncodeByRegExp = function (str) {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&/g, "&amp;");
        s = s.replace(/</g, "&lt;");
        s = s.replace(/>/g, "&gt;");
        s = s.replace(/ /g, "&nbsp;");
        s = s.replace(/\'/g, "&#39;");
        s = s.replace(/\"/g, "&quot;");
        return s;
    }

    /*4.用正则表达式实现html解码*/
    var htmlDecodeByRegExp = function (str) {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&amp;/g, "&");
        s = s.replace(/&lt;/g, "<");
        s = s.replace(/&gt;/g, ">");
        s = s.replace(/&nbsp;/g, " ");
        s = s.replace(/&#39;/g, "\'");
        s = s.replace(/&quot;/g, "\"");
        return s;
    }

    return {
        err: err,
        ok: ok,
        info: info,
        htmlEncode: htmlEncode,
        htmlDecode: htmlDecode,
        htmlEncodeByRegExp: htmlEncodeByRegExp,
        htmlDecodeByRegExp: htmlDecodeByRegExp
    }
})();
