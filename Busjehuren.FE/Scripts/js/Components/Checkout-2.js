var Checkout2 = function () {

    var config = {
        tableId: undefined,
        urlprocess: '',
        urlCheckout3: '',
        objTooltip: undefined,
        culture: 'nl-NL',
        optionCurrency: { style: 'currency', currency: 'EUR' },
        form: $("#formCheckout2"),
        landBestemming: $("#LandBestemming"),
        caretup: $(".caret-up"),
        caretdown: $(".caret-down"),
        totalCamperPrice: $("#totalCamperPrice"),
        totalPriceOverView: $("#totalprice-overview"),
        nonlocalPackagePriceTotal: 'nonlocalPackagePriceTotal',
        localPackagePriceTotal: 'localPackagePriceTotal'
    };

    var init = function (options) {

        $.extend(config, options);

        $('body').on('hidden.bs.popover', function (e) {
            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false }
        });

        $('body').on('click', function (e) {
            if ($(e.target).data('toggle') !== 'popover' && $(e.target).parents('.popover.in').length === 0) {
                $('[data-toggle="popover"]').popover('hide');
            }
        });

        config.objTooltip.popover();

        config.objTooltip.on('click', function (e) {
            config.objTooltip.not(this).popover('hide');
        });

        $('#' + config.tableId).on("change", "tbody tr td input[type=checkbox]", function () {
            DisplayNumber($(this).data("packageid"), $(this).prop("checked"));

            showOverviewPackageRow();
            calculateTotal();

        });

        config.landBestemming.change(function () {
            exceptionStyleValidate();
        });

        config.caretup.click(function () {
            var refer = $("#" + $(this).data('refer'));
            var referVal = parseInt(refer.val());
            var referMaxVal = refer.data("max");

            if (referVal < referMaxVal)
                refer.val(referVal + 1);

            ChangePrice(refer);
            calculateTotal();
        })

        config.caretdown.click(function () {
            var refer = $("#" + $(this).data('refer'));
            var referVal = parseInt(refer.val());

            if (referVal > 1)
                refer.val(referVal - 1);

            ChangePrice(refer);
            calculateTotal();
        })

        $("#btnMoveNextStep").click(function () {
            exceptionStyleValidate()

            $("#SelectedPackages").val(JSON.stringify(getSelectedPackages()));
            config.form.submit();
        });
    }

    function ChangePrice(input) {
        var number = input.val();
        var packageId = input.data("packageid");
        var price = $("#price" + packageId);
        var spanprice = $("#package" + packageId);
        var spanNumer = $("#number" + packageId);

        var total = Math.round(number * parseFloat(price.data("value")) * 100) / 100;
        price.text(total.toLocaleString(config.culture, config.optionCurrency));
        spanprice.text(total.toLocaleString(config.culture, config.optionCurrency));
        spanprice.data("value", total);
        spanNumer.text(number);
    }

    //catch validate for these input was wrapper by design
    function exceptionStyleValidate() {
        if (!config.landBestemming.valid()) {
            config.landBestemming.parent().addClass("has-error");
        }
        else {
            config.landBestemming.parent().removeClass("has-error");
        }
    }

    function calculateTotal() {
        var total = parseFloat(config.totalCamperPrice.data("value"));

        $('#' + config.tableId + ' input:checkbox:checked').each(function () {
            var packageId = $(this).data("packageid");
            var selectedvalue = $("#select" + packageId).val();
            var price = selectedvalue * parseFloat($("#price" + packageId).data("value"));

            total += price;
        });

        config.totalCamperPrice.text(total.toLocaleString(config.culture, config.optionCurrency));
        config.totalPriceOverView.text(total.toLocaleString(config.culture, config.optionCurrency));

        GetPackagePriceTotal(config.nonlocalPackagePriceTotal);
        GetPackagePriceTotal(config.localPackagePriceTotal);
    }

    function GetPackagePriceTotal(type) {
        var total = 0;

        $("[data-belongto=" + type + "]:visible").each(function () {
                total += parseFloat($(this).data("value"));
        });

        $("#" + type).text(total.toLocaleString(config.culture, config.optionCurrency));
    }

    function showOverviewPackageRow() {
        $('#' + config.tableId + ' input:checkbox').each(function () {
            var packageId = $(this).data("packageid");
            var row = $("#row" + packageId);
            if ($(this).prop("checked"))
                row.show();
            else
                row.hide();
        });
    }

    function DisplayNumber(id, ischecked) {
        var containerNumber = $("#quantity-container" + id);
        if (ischecked)
            containerNumber.removeClass("hide");
        else
            containerNumber.addClass("hide");
    }

    function getSelectedPackages() {
        var list = [];

        $('#' + config.tableId + " tbody tr td input[type=checkbox]:checked").each(function () {
            var packageId = $(this).val();
            var selectedvalue = $("#select" + packageId).val();
            list.push(new PackageObject(packageId, selectedvalue));
        })

        return list;
    }

    function PackageObject(id, number) {
        var self = this;
        self.Id = id;
        self.Number = number;
    }

    return { init: init };
}

