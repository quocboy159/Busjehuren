var Checkout4 = function () {

    var config = {
        urlprocess: '',
        urlCheckout5: ''
    };

    var init = function (options) {

        $.extend(config, options);

        $("#btnMoveNextStep").click(function () {
            $("#formCheckout4").submit();
        });

    }

    return { init: init };
}