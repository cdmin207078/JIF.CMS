(function ($) {
    $('.article-item').hover(function () {

        var $h3 = $(this).find('h3');
        var $small = $(this).find('small');

        $h3.stop().animate({
            marginTop: '100px',
        }, 200, function () {
            $small.fadeIn();
        });
    }, function () {

        var $h3 = $(this).find('h3');
        var $small = $(this).find('small');

        $small.hide();

        $h3.stop().animate({
            marginTop: '200px',
        }, 200, function () {
        });
    });
})(jQuery);