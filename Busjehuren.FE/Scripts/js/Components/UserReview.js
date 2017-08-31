var UserReview = function () {
    var config = {
        businessID: '515db8d40000640005255e3d',
        apiPublicKey: 'HimOTBpjAa2hAyBtXU9eYoBql3BC6OP4',
        stars: 5,
        language: 'nl',
        perpage: 1,
        author: undefined,
        spanstar: undefined,
        reviewcontent: undefined,
        titlereview: undefined,
        reviewdate : undefined
    };

    var init = function (options) {
        $.extend(config, options);

        $.ajax({
            url: 'https://api.trustpilot.com/v1/business-units/' + config.businessID + '/reviews?apikey=' + config.apiPublicKey + '&stars=' + config.stars + '&language=' + config.language + '&perpage=' + config.perpage,
            dataType: 'json',
            success: function (data, status) {
                showRating(data);
            },
            error: function (XHR, txtStatus, errThrown) {
                console.log('Error: ' + txtStatus);
                console.log('Error: ' + errThrown);
            }
        });

    };

    function showRating(jsonData) {
        $("#" + config.author).text(jsonData.reviews[0].consumer.displayName);
        $("#" + config.spanstar).text(parseInt(jsonData.reviews[0].stars) * 2);
        $("#" + config.reviewcontent).text(jsonData.reviews[0].text);
        $("#" + config.titlereview).text(jsonData.reviews[0].title);
        $("#" + config.reviewdate).text(jsonData.reviews[0].createdAt);
    }

    return { init: init };
}