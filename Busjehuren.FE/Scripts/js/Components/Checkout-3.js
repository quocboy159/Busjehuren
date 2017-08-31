var Checkout3 = function () {
    var config = {
        btnSubmit: '',
        urlprocess: '',
        urlCheckout4: '',
        dateFormat: 'd-m-yy',
        dateValue: '',
        checkCondition: $("#checkCondition"),
        form: $("#formCheckout3"),
        Hoofdboeker_Geslacht: $("#Hoofdboeker_Geslacht"),
        messageRequired: 'Dit veld is verplicht.',
        messageInvalidEmail: 'Geen geldig e-mailadres.',
    };

    var init = function (options) {

        $.extend(config, options);

        initDate();

        var isAfwijkendFactuuradres = $("#AfwijkendFactuuradres").prop("checked");

        $("#Afwijkend").prop("checked", isAfwijkendFactuuradres);

        if (isAfwijkendFactuuradres)
            $('.collapse').collapse();

        addValidate(isAfwijkendFactuuradres);


        $("#Afwijkend").click(function () {

            var ischecked = $(this).prop("checked");

            $("#AfwijkendFactuuradres").prop("checked", ischecked);

            addValidate(ischecked);
        });

        $("#btnShowTermCondition").click(function () {
            $("#termConditionModal").modal();
        });

        $("#btnMoveNextStep").click(function () {
            exceptionStyleValidate([config.Hoofdboeker_Geslacht, config.checkCondition]);

            config.form.submit();
        });

        config.Hoofdboeker_Geslacht.change(function () {
            exceptionStyleValidate(config.Hoofdboeker_Geslacht);
        });

        config.checkCondition.change(function () {
            exceptionStyleValidate(config.checkCondition);
        });
    }

    function initDate() {
        var now = new Date();
        now.setFullYear(now.getFullYear() - 18);
        $("#Hoofdboeker_BirthDate").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: config.dateFormat,
            defaultDate: now,
            yearRange: "-100:+0",
        }, $.datepicker.regional["nl"]);
    };

    //catch validate for these input was wrapper by design
    function exceptionStyleValidate(elements) {
        for (var i = 0 ; i < elements.length; i++)
            if (!$(elements[i]).valid()) {
                $(elements[i]).parent().addClass("has-error");
            }
            else {
                $(elements[i]).parent().removeClass("has-error");
            }
    };

    function checkElementValid(name)
    {
        return $("[data-valmsg-for=" + name + "]").children().length > 0;
    }

    function addValidate(ischecked) {

        config.checkCondition.rules("add", {
            equalchecked: true,
            messages: {
                equalchecked: jQuery.validator.format(config.messageRequired),
            }
        });

        if (ischecked) {
            $("#FactuurEmail").rules("add", {
                required: true,
                email: true,
                messages: {
                    required: jQuery.validator.format(config.messageRequired),
                    email: jQuery.validator.format(config.messageInvalidEmail)
                }
            });

            $("#FactuurBedrijfsnaam").rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format(config.messageRequired),
                }
            });

            $("#FactuurTelefoon").rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format(config.messageRequired),
                }
            });

            $("#FactuurStraat").rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format(config.messageRequired),
                }
            });

            $("#FactuurHuisnummer").rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format(config.messageRequired),
                }
            });

            $("#FactuurPostcode").rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format(config.messageRequired),
                }
            });

            $("#FactuurStad").rules("add", {
                required: true,
                messages: {
                    required: jQuery.validator.format(config.messageRequired),
                }
            });
        }
    };

    return { init: init };
}