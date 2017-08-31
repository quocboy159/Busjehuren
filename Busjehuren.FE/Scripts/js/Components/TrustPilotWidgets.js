var TrustPilotWidgets = function () {
    var config = {
        businessID: '515db8d40000640005255e3d',
        apiPublicKey: 'HimOTBpjAa2hAyBtXU9eYoBql3BC6OP4',
        pointId: undefined,
        starId: undefined
    };

    var init = function (options) {
        $.extend(config, options);

        $.ajax({
            url: 'https://api.trustpilot.com/v1/business-units/' + config.businessID + '/?apikey=' + config.apiPublicKey,
            dataType: 'json',
            success: function (data, status) {
                var stars = parseInt(data.stars);
                $("#" + config.starId).addClass("active-" + stars)
                $("#" + config.pointId).text(data.trustScore);
            }
        });
    };

    return { init: init };
}