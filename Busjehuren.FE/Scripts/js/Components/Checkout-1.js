var Checkout1 = function () {
    var config = {
        url: '',
        urlCondition: '',
        urlSpecific: '',
        bg_load: $(".bg_load"),
        wrapper: $(".wrapper"),
        btnRestore: $("#btn-restore"),
        btnSubmit: $("#btn-submit"),
        frmCamperSearch: $("#frmCamperSearch"),
        radioElements: 'input[type=radio]',
        checkedRadioElements: 'input[type=radio]:checked',
        pageId: '',
        urlLoadMore: '',
        page: 1,
    };

    var init = function (options) {
        $.extend(config, options);
        var obj = {
            Name: '',
        };
        config.bg_load.fadeOut();
        config.wrapper.fadeOut();

        $("#propertyContainer").on('change', config.radioElements, function () {
            renderResult();
        });

        config.btnRestore.click(function () {
            ResetRadios();
            renderResult();
        });

        $('body').on('click', '#btnLoadMore', function () {
            $.ajax({
                url: config.urlLoadMore,
                type: "POST",
                dataType: "html",
                data: { page: config.page + 1, properties: getProperties() },
                beforeSend: function () {
                    //$("#btnLoadMore").html("Loading");
                    $("#loadMoreIcon").removeClass("hide");
                    $("#btnLoadMore").attr("disabled", true);
                },
                success: function (data) {
                    $("#productContainer").append(data);

                    config.page++;
                },
                complete: function () {
                    $("#loadMoreIcon").addClass("hide");
                    $("#btnLoadMore").attr("disabled", false);
                },
            });
        });

        $('a.condition').on('click', function () {
            var id = $(this).data("id");
            $.ajax({
                url: config.urlCondition,
                type: 'POST',
                dataType: 'JSON',
                data: { id: id },
                cache: false,
                beforeSend: function () {
                    config.bg_load.fadeIn('slow');
                    config.wrapper.fadeIn('slow');
                },
                success: function (data) {
                    if (data != null) {
                        renderCondtion(data);
                        $("#popUpModalCondition").modal();
                        $('#popUpModalCondition .modal-body').scrollTop(0);
                    }
                },
                complete: function () {
                    config.bg_load.fadeOut('slow');
                    config.wrapper.fadeOut('slow');
                },
                error: function (XHR, txtStatus, errThrown) {
                    console.log('Error: ' + txtStatus);
                    console.log('Error: ' + errThrown);
                },
            });
        });

        $('a.specific').on('click', function () {
            var id = $(this).data("id");
            var price = $('#price' + id).text();
            var image = $('#image' + id).attr("src");
            $.ajax({
                url: config.urlSpecific,
                type: 'POST',
                dataType: 'JSON',
                data: { id: id },
                cache: false,
                beforeSend: function () {
                    config.bg_load.fadeIn('slow');
                    config.wrapper.fadeIn('slow');
                },
                success: function (data) {
                    if (data != null) {
                        renderSpecific(data, price, image);
                        $("#popUpModalSpecification").modal();
                        $('#popUpModalSpecification .modal-body').scrollTop(0);
                    }
                },
                complete: function () {
                    config.bg_load.fadeOut('slow');
                    config.wrapper.fadeOut('slow');
                },
                error: function (XHR, txtStatus, errThrown) {
                    console.log('Error: ' + txtStatus);
                    console.log('Error: ' + errThrown);
                },
            });
        });
    };

    function renderCondtion(data) {
        $("#conditionTitle").html('');
        $('#conditionTitle').append('Leverancier: ' + data.Naam);

        let template = ' <br />';
        if (data.Prijsvoorwaarden.length > 90) {
            template += '<b>Prijs voorwaarden:</b ><br />' + data.Prijsvoorwaarden + '<br />';
        }
        if (data.Huurvoorwaarden.length > 90) {
            template += '<b>Huurvoorwaarden: </b ><br />' + data.Huurvoorwaarden + '<br />';
        }
        if (data.Openingstijden.length > 90) {
            template += '<b>Openingstijden: </b ><br />' + data.Openingstijden + '<br />';
        }
        if (data.OphaalInformatie.length > 90) {
            template += '<b>Ophaalinformatie: </b ><br />' + data.OphaalInformatie + '<br />';
        }
        if (data.InleverInformatie.length > 90) {
            template += '<b>Inleverinformatie: </b ><br />' + data.InleverInformatie + '<br />';
        }
        if (data.TransferInformatie.length > 90) {
            template += '<b>Transferinformatie: </b ><br />' + data.Transferinformatie + '<br />';
        }

        $("#condition").html('');
        $("#condition").append(template);
    }

    function renderSpecific(data, price, image) {
        $(".pd-name").html('');
        $('.pd-name').append('<a href="#">' + data.Naam + '</a ><span>of gelijkwaardig</span>');

        $('#specific-image').html('');
        $('#specific-image').append('<img src="' + image + '" alt="">');

        $('#price-popup').html('');
        $('#price-popup').append(price);
        let template = '<thead><tr><td>Laadruimte (m3)</td><td>' + data.LaadRuimte + 'm3</td></tr></thead >';

        template += '<tbody><tr><td>Aantal zitplaatsen</td><td>' + data.MaxAantalPersonen + '</td></tr>';
        data.EigenschapWaarden.sort(function (a, b) {
            if (a.EigenschapNaam < b.EigenschapNaam) return -1;
            if (a.EigenschapNaam > b.EigenschapNaam) return 1;
            return 0;
        })

        data.EigenschapWaarden.forEach(function (item) {
            template += '<tr><td>' + item.EigenschapNaam + '</td><td>' + item.Value + '</td></tr>';
        });

        template += 'tr><td>Laadruimte(l x b x h)</td><td>' +
            data.LaadLengte + ' x ' + data.LaadBreedte + ' x ' + data.LaadHoogte + ' cm</td></tr>';

        template += '<tr><td>laadvermogen</td><td>' + data.LaadVermogen + ' kg</td></tr></tbody>';

        $('.specify-table').html('');
        $('.specify-table').append(template);
    }

    function renderResult() {
        $.ajax({
            url: config.url,
            data: { properties: getProperties() },
            dataType: 'html',
            type: 'POST',
            beforeSend: function () {
                config.bg_load.fadeIn('slow');
                config.wrapper.fadeIn('slow');
            },
            success: function (data, status) {
                $("#resultcontainer").html('');
                $("#resultcontainer").append(data).slideDown("slow");
            },
            complete: function () {
                config.bg_load.fadeOut('slow');
                config.wrapper.fadeOut('slow');
            },
            error: function (XHR, txtStatus, errThrown) {
                console.log('Error: ' + txtStatus);
                console.log('Error: ' + errThrown);
            },
        });
    }

    function ResetRadios() {
        $(config.radioElements, '#propertyContainer').each(function () {
            $(this).prop("checked", false);
        })
    }

    function getProperties() {
        var list = [];

        $(config.checkedRadioElements, '#propertyContainer').each(function () {
            list.push($(this).val());
        })

        return list;
    }

    return { init: init };
}