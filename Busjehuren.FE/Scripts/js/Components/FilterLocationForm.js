/**
	* Init Date Time Picker
	*/
var holidays = ["04-17", "04-27", "05-25", "06-05", "12-25", "12-26"];
var invalidHours = [0, 1, 2, 3, 4, 5, 6, 7, 18, 19, 20, 21, 22, 23];
var validHours = [8, 9, 10, 11, 12, 13, 14, 15, 16, 17];
var validpickUpDate = [1, 2, 3, 4, 5, 6, 7];
var validdropOffDate = [1, 2, 3, 4, 5, 6, 7];

function gv(inId) {
    if (document.getElementById) {
        return document.getElementById(inId).value;
    } else {
        return '';
    }
}

function validatesearchform() {
    if (document.getElementById) {
        var i;
        var d_tmp = '';
        var i_chk = 0;
        var i_vld = '';


        if (gv('StartDate')) {
            var d_v = gv('StartDate');
        } else {
            i_chk++;
            i_vld = i_vld + ' - Ophaaldatum [leeg]\n';
        }

        if (gv('EndDate')) {
            var d_r = gv('EndDate');
        } else {
            i_chk++;
            i_vld = i_vld + ' - Inleverdatum [leeg]\n';
        }
        var today = new Date();

        if (d_v && d_r) {
            //zet vreemde seperators om in /
            d_vS = d_v.replace(/-|;|:|\\|\.|,/g, '/');
            d_rS = d_r.replace(/-|;|:|\\|\.|,/g, '/');
            //zet datumsegmenten in een array
            var d_vA = new Array();
            var d_rA = new Array();
            for (i = 0; i < d_vS.split("/").length; i++) {
                d_tmp = d_vS.split("/")[i];
                if (!d_tmp == '') d_vA[i] = d_tmp;
            }
            for (i = 0; i < d_rS.split("/").length; i++) {
                d_tmp = d_rS.split("/")[i];
                if (!d_tmp == '') d_rA[i] = d_tmp;
            }

            var chosendate = new Date(parseInt(d_vA[2], 10), parseInt(d_vA[1], 10) - 1, parseInt(d_vA[0], 10));
            var pickupTime = parseInt($('#PickUpTime').val().split(':')[0], 10);
            pickupTime += (parseInt($('#PickUpTime').val().split(':')[1], 10) / 60);

            var dropofftime = parseInt($('#DropOffTime').val().split(':')[0], 10);
            dropofftime += (parseInt($('#DropOffTime').val().split(':')[1], 10) / 60);


            //weekend is friday after 17:00, saturday or sunday
            var isWeekendCheck = ((today.getDay() == 5 && today.getHours() >= 17) || today.getDay() == 6 || today.getDay() == 0);
            if (chosendate.getDate() == today.getDate() && chosendate.getMonth() == today.getMonth() && chosendate.getYear() == today.getYear()) {
                showremark(1001);
                return false;
            }
            else if (!isWeekendCheck && (chosendate.getDate() == today.getDate() + 1 && chosendate.getMonth() == today.getMonth() && chosendate.getYear() == today.getYear())) {
                //tomorrow, check if  time is later than current time
                if (pickupTime <= today.getHours()) {
                    showremark(1001);
                    return false;
                }
            }
            //if it is in weekend, and chosen day is next monday before 12:00. Last check with miliseconds ensures it is the next monday
            else if (isWeekendCheck && chosendate.getDay() == 1 && pickupTime < 12 && (Math.abs(today - chosendate) / 3600000) < 72) {
                showremark(1002);
                return false;
            }

            if (d_v == d_r && pickupTime >= dropofftime) {
                //ophaal en terugbreng data zijn zelfde, en ophaaltijdstip ligt na terugbreng tijdstip
                showremark(1003);
                return false;
            }
        }

        if (i_chk == 0) {
            return true;
        } else {
            var Message = ") Het formulier is niet correct of niet volledig ingevuld! \n Veld(en) om te controleren: \n";
            console.log(Message);

            return false;
        }
    }

    return false;
}

function showremark(id) {
    $("#messageValidateModal_" + id).modal();
}

function GetValidDate(d, availableDates) {
    var date = new Date(d);
    if (availableDates == null) {
        availableDates = [1, 2, 3, 4, 5, 6, 7];
    }

    //skip sunday and holidays
    while (true) {
        var datestring = jQuery.datepicker.formatDate('mm-dd', date);
        if (holidays.indexOf(date) == -1 && availableDates.indexOf(date.getDay() + 1) != -1) {// && (date.getDay() != 0)) {
            break;
        }
        date = date.addDays(1);
    }

    return date;
}

var FilterLocationForm = function () {

    var config = {
        btnSearch: undefined,
        frmCamperSearch: undefined,
        destinationsJsonString: undefined,
        departField: undefined,
        displayDepartNameField: undefined,
        departValueField: undefined,
        departCountryValueField: undefined,
        arrivalField: undefined,
        displayArrivalNameField: undefined,
        arrivalValueField: undefined,
        arrivalCountryValueField: undefined,
        pickUpDateField: undefined,
        pickUpDateValueField: undefined,
        dropOffDateField: undefined,
        dropOffDateValueField: undefined,
        pickUpTimeField: undefined,
        pickUpTimeValueField: undefined,
        dropOffTimeField: undefined,
        dropOffTimeValueField: undefined,
        dateFormat: "D-M-YYYY",
        timeFormat: "HH:mm:ss",
        hoursOfArrival: [],
        hoursOfDepartCountry: [],
        urlGetLocationById: '',
        dateNormalFormat: 'DD/MM/YYYY'
    };

    var initDateTimePicker = function (options) {

        var initPickUpDate = GetValidDate(new Date()).addDays(2);
        var initDropOffDate = GetValidDate(new Date()).addDays(3);

        options.pickUpDateField.datepicker({
            dateFormat: 'D d M yy',
            numberOfMonths: isMobile() ? 1 : 2,
            minDate: new Date(),
            beforeShowDay: function (date) {
                var string = jQuery.datepicker.formatDate('mm-dd', date);
                return [holidays.indexOf(string) == -1 && validpickUpDate.indexOf(date.getDay() + 1) != -1];// && date.getDay() != 0];// 
            },
            onSelect: function (dateText, inst) {

                var selectedDate = options.pickUpDateField.datepicker('getDate');
                var now = new Date();
                var chosenDate = new Date(selectedDate);
                var diffDate = chosenDate.getDate() - now.getDate();

                config.pickUpDateValueField.val(moment(selectedDate).format(config.dateFormat));
                if (config.departValueField.val() != '') {
                    LimitTime(config.pickUpTimeField, config.departValueField.val(), config.pickUpDateValueField.val(), true, false);
                }

                var dropOffDate = new Date(config.dropOffDateField.datepicker('getDate'));
                // set max min for dropOffDate
                options.dropOffDateField.datepicker("option", "minDate", GetValidDate(chosenDate.addDays(1), validdropOffDate));
                options.dropOffDateField.datepicker("option", "maxDate", chosenDate.addDays(28));
                if (chosenDate >= dropOffDate) {
                    options.dropOffDateField.datepicker('setDate', GetValidDate(chosenDate.addDays(1), validdropOffDate));
                    options.dropOffDateValueField.val(moment(GetValidDate(chosenDate.addDays(1), validdropOffDate)).format(config.dateFormat));


                    if (config.arrivalValueField.val() != '') {
                        LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val(), false, false);
                    }
                }


                if (diffDate == 1) {
                    var validTime = now.getHours() + ":" + now.getMinutes();

                    if (invalidHours.indexOf(now.getHours()) != -1) {
                        validTime = validHours[validHours.length - 1] + ":00";
                    }
                    $(options.pickUpTimeField).val("09:00");
                    $(options.pickUpTimeField).val("09:00:00");
                }
                else {
                    $(options.pickUpTimeField).val("09:00");
                    $(options.pickUpTimeField).val("09:00:00");
                }
            }
        }, $.datepicker.regional["nl"]);

        if (options.pickUpDateField.val() == '') {
            options.pickUpDateField.datepicker('setDate', initPickUpDate);
            config.pickUpDateValueField.val(moment(initPickUpDate).format(config.dateFormat));
        }
        else {
            options.pickUpDateField.datepicker('setDate', new Date(moment(options.pickUpDateField.val(), config.dateNormalFormat)));
            var chosenDate = new Date(options.pickUpDateField.datepicker('getDate'));
            options.dropOffDateField.datepicker("option", "minDate", GetValidDate(chosenDate.addDays(1), validdropOffDate));
            options.dropOffDateField.datepicker("option", "maxDate", chosenDate.addDays(28));
        }

        //options.pickUpTimeField.datetimepicker({
        //    useCurrent: false,
        //    format: "HH:mm",
        //    disabledHours: invalidHours,
        //});

        //options.pickUpTimeField.data("DateTimePicker").stepping(30);

        //if (options.pickUpTimeValueField.val() == "") {
        //    options.pickUpTimeField.data("DateTimePicker").date("09:00");
        //    options.pickUpTimeValueField.val(options.pickUpTimeField.val() + ":00");
        //}

        options.dropOffDateField.datepicker({
            dateFormat: 'D d M yy',
            numberOfMonths: isMobile() ? 1 : 2,
            minDate: (new Date(moment(options.pickUpDateValueField.val(), config.dateNormalFormat))).addDays(1),
            beforeShowDay: function (date) {
                var string = jQuery.datepicker.formatDate('mm-dd', date);
                return [holidays.indexOf(string) == -1 && validdropOffDate.indexOf(date.getDay() + 1) != -1];//return [holidays.indexOf(string) == -1 && date.getDay() != 0];
            },
            onSelect: function (dateText, inst) {
                var selectedDate = options.dropOffDateField.datepicker('getDate');
                config.dropOffDateValueField.val(moment(selectedDate).format(config.dateFormat));
                if (config.arrivalValueField.val() != '') {
                    LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val(), false, false);
                }
            }
        }, $.datepicker.regional["nl"]);

        if (options.dropOffDateField.val() == '') {
            options.dropOffDateField.datepicker('setDate', initDropOffDate);
            options.dropOffDateValueField.val(moment(initDropOffDate).format(config.dateFormat));
        }
        else {
            options.dropOffDateField.datepicker('setDate', new Date(moment(options.dropOffDateField.val(), config.dateNormalFormat)));
        }

        //options.dropOffTimeField.datetimepicker({
        //    useCurrent: false,
        //    format: "HH:mm",
        //    disabledHours: invalidHours
        //});

        //options.dropOffTimeField.data("DateTimePicker").stepping(30);

        //if (options.dropOffTimeValueField.val() == "") {
        //    options.dropOffTimeField.data("DateTimePicker").date("09:00");
        //    options.dropOffTimeValueField.val(options.dropOffTimeField.val() + ":00");
        //}
    };

    function extractLast(term) {
        return split(term).pop();
    }

    function split(val) {
        return val.split(/,\s*/);
    }

    function GetHoursByLocation(locationId, date) {
        var result = null;

        $.ajax({
            url: config.urlGetLocationById,
            dataType: 'json',
            async: false,
            data: { Id: locationId, date: date },
            success: function (data, status) {
                if (data.length != 0) {
                    result = data;
                }
            },
            error: function (XHR, txtStatus, errThrown) {
            }
        });

        return result;
    }

    function LimitTime(element, locationId, date, isDepart, isInit) {
        if (locationId != '' && date != '') {
            var durationTime = GetHoursByLocation(locationId, date);

            if (durationTime != null) {
                if (isDepart) {
                    validpickUpDate = durationTime.availableDates;
                } else {
                    validdropOffDate = durationTime.availableDates;
                }
                if (!isInit) {
                    if (durationTime.minTime != null && durationTime.maxTime != null) {
                        $(element[0]).empty();
                        let startHour = durationTime.minTime.Hour,
                            endHour = durationTime.maxTime.Hour,
                            openingHours = [];

                        if (durationTime.minTime.Minute !== 0) {
                            let tempStringHour = startHour < 10 ? '0' + startHour : startHour;
                            openingHours.push('<option value="' + tempStringHour + ':30:00">' + tempStringHour + ':30</option>');
                            startHour++;
                        }

                        while (startHour < endHour) {
                            let stringHour = startHour < 10 ? '0' + startHour : startHour;
                            openingHours.push('<option value="' + stringHour + ':00:00">' + stringHour + ':00</option>');
                            openingHours.push('<option value="' + stringHour + ':30:00">' + stringHour + ':30</option>');
                            startHour++;
                        }

                        var tempStringHour = startHour < 10 ? '0' + startHour : startHour;
                        openingHours.push('<option value="' + tempStringHour + ':00:00">' + tempStringHour + ':00</option>');

                        if (durationTime.maxTime.Minute !== 0) {
                            openingHours.push('<option value="' + tempStringHour + ':30:00">' + tempStringHour + ':30</option>');
                        }

                        $(element[0]).html(openingHours);
                        //element.data("DateTimePicker").minDate(moment({ h: durationTime.minTime.Hour, m: durationTime.minTime.Minute }));
                        //element.data("DateTimePicker").maxDate(moment({ h: durationTime.maxTime.Hour, m: durationTime.maxTime.Minute }));
                    } else {
                        let openingHours = [];
                        for (let i = 0; i < validHours.length; i++) {
                            let stringHour = validHours[i] < 10 ? '0' + validHours[i] : validHours[i];
                            openingHours.push('<option value="' + stringHour + ':00:00">' + stringHour + ':00</option>');
                            openingHours.push('<option value="' + stringHour + ':30:00">' + stringHour + ':30</option>');
                        }
                        $(element[0]).html(openingHours);
                    }
                }
            } else {
                if (isDepart) {
                    validpickUpDate = [];
                } else {
                    validdropOffDate = [];
                }
                let openingHours = [];
                for (let i = 0; i < validHours.length; i++) {
                    let stringHour = validHours[i] < 10 ? '0' + validHours[i] : validHours[i];
                    openingHours.push('<option value="' + stringHour + ':00:00">' + stringHour + ':00</option>');
                    openingHours.push('<option value="' + stringHour + ':30:00">' + stringHour + ':30</option>');
                }
                $(element[0]).html(openingHours);
                //element.data("DateTimePicker").minDate(moment({ h: 8 }));
                //element.data("DateTimePicker").maxDate(moment({ h: 17, m: 30 }));
            }
        } else {
            $(element[0]).empty();
        }
    }

    var init = function (options) {

        $.extend(config, options);

        initDateTimePicker(config);

        $(".reserve").on('submit', function () {
            if ($("#frmCamperSearch").valid()) {
                if (validatesearchform())
                    return true;
                else
                    return false;
            }
            $("#messageValidateSearchForm").modal();
            return false;
        })

        $("#step2").on('click', function () {
            if ($("#frmCamperSearch").valid()) {
                if (validatesearchform())
                    return true;
                else
                    return false;
            }
            $("#messageValidateSearchForm").modal();
            return false;
        })

        config.btnSearch.click(function () {

            config.frmCamperSearch.validate();

            if (config.frmCamperSearch.valid() && validatesearchform()) {
                var selectedDestination = cities.filter(function (item, index) {
                    return (config.departValueField.val() == item.Id);
                })[0];

                config.frmCamperSearch.attr("action", "/" + selectedDestination.UrlNameParent + "/" + selectedDestination.UrlName);

                return true;
            }

            return false;
        });

        var countries = JSON.parse(config.destinationsJsonString.val());
        var cities = [];
        countries.forEach(function (item) {
            item.Cities.forEach(function (city) {
                city.label = city.DisplayName;
                city.value = city.Id;
                city.category = item.DisplayName;
                city.countryDisplayName = item.DisplayName;
                city.countryId = item.Id;
                city.UrlNameParent = item.UrlName;
            });
            cities = cities.concat(item.Cities);
        });

        //cities = cities.sort(sort(null, 'label'));

        //cities = cities.sort(function (a, b) {
        //    if (a.label < b.label)
        //        return -1;
        //    if (a.label > b.label)
        //        return 1;
        //    return 0;
        //});


        //config.departField.rules("add",
        //        {
        //            existedcity: true
        //        });

        //config.arrivalField.rules("add",
        //        {
        //            existedcity: true
        //        });

        //var enterDepartFieldValue = {};
        //config.departField.autocomplete({
        //    minLength: 0,
        //    source: function (request, response) {
        //        var term = $.ui.autocomplete.escapeRegex(extractLast(request.term)),
        //            startsWithMatcher = new RegExp("^" + term, "i"),
        //            startsWith = $.grep(cities,
        //                function (value) {
        //                    return startsWithMatcher.test(value.label || value.value || value);
        //                }),
        //            containsMatcher = new RegExp(term, "i"),
        //            contains = $.grep(cities,
        //                function (value) {
        //                    return $.inArray(value, startsWith) < 0 &&
        //                        containsMatcher.test(value.label || value.value || value);
        //                });

        //        // in order to show all results when input's text equals any cities's item lable
        //        var fixValue = cities.filter(function (item, index) {
        //            return (request.term == item.label);
        //        });

        //        // delegate back to autocomplete, but extract the last term
        //        response(fixValue.length == 1 ? cities : startsWith.concat(contains));
        //    },
        //    onChange: function (event, ui) {
        //        config.displayDepartNameField.val(ui.item.label);
        //        config.departValueField.val(ui.item.value);
        //        config.departCountryValueField.val(ui.item.countryId);
        //        if (config.arrivalValueField.val() == "") {
        //            config.displayArrivalNameField.val(ui.item.label);
        //            config.arrivalValueField.val(ui.item.value);
        //            config.arrivalCountryValueField.val(ui.item.countryId);

        //            LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val());
        //        }
        //        LimitTime(config.pickUpTimeField, config.departValueField.val(), config.pickUpDateValueField.val());

        //        config.arrivalField.blur();

        //        return false;
        //    },
        //    response: function (event, ui) {
        //        if (ui.content.length == 0) {
        //            config.departValueField.val("");
        //            config.departCountryValueField.val("");

        //            if (config.arrivalValueField.val() == "") {
        //                config.displayArrivalNameField.val("");
        //                config.arrivalValueField.val("");
        //                config.arrivalCountryValueField.val("");
        //            }
        //        } else {
        //            var item = ui.content[0];

        //            config.departValueField.val(item.value);
        //            config.departCountryValueField.val(item.countryId);

        //            if (ui.content.length == 1) {
        //                enterDepartFieldValue.label = item.label;
        //                enterDepartFieldValue.value = item.value;
        //                enterDepartFieldValue.countryId = item.countryId;
        //            }
        //        }
        //    }
        //}).focus(function () {
        //    $(this).autocomplete("search");
        //}).autocomplete("instance")._renderItem = function (ul, item) {
        //    return $("<li>")
        //      .append("<div>" + item.label + "</div>")
        //      .appendTo(ul);
        //};

        //config.departField.on("blur", function () {
        //    if (enterDepartFieldValue.value != null && config.arrivalValueField.val() == "") {
        //        config.displayArrivalNameField.val(enterDepartFieldValue.label);
        //        config.arrivalValueField.val(enterDepartFieldValue.value);
        //        config.arrivalCountryValueField.val(enterDepartFieldValue.countryId);
        //    }
        //});
        config.departField.on('change', function () {

            let fixValue = cities.filter(function (item, index) {
                return (config.departField.val() == item.value);
            });

            config.departValueField.val(config.departField.val());
            config.displayDepartNameField.val(fixValue.length != 0 ? fixValue[0].label : "");
            config.departCountryValueField.val(fixValue.length != 0 ? fixValue[0].countryId : 0);

            if (config.pickUpDateValueField.val() == '') {
                config.pickUpDateField.datepicker('setDate', GetValidDate(new Date()).addDays(2));
                config.pickUpDateValueField.val(moment(GetValidDate(new Date()).addDays(2)).format(config.dateFormat));

                config.dropOffDateField.datepicker("option", "minDate", GetValidDate(new Date(), validdropOffDate).addDays(3));
                config.dropOffDateField.datepicker("option", "maxDate", GetValidDate(new Date(), validdropOffDate).addDays(28));
            }

            LimitTime(config.pickUpTimeField, config.departValueField.val(), config.pickUpDateValueField.val(), true, false);

            var chosenDate = new Date(config.pickUpDateField.datepicker('getDate'));

            if (validpickUpDate.length == 0) {
                config.pickUpDateField.datepicker('setDate', '');
                config.pickUpDateValueField.val('');
            } else {
                if (validpickUpDate.indexOf(chosenDate.getDay() + 1) == -1) {
                    config.pickUpDateField.datepicker('setDate', GetValidDate(chosenDate.addDays(1), validpickUpDate));
                    config.pickUpDateValueField.val(moment(GetValidDate(chosenDate.addDays(1), validpickUpDate)).format(config.dateFormat));
                }
            }

            if (config.arrivalValueField.val() == "") {
                config.arrivalField.val(config.departValueField.val());
                config.arrivalValueField.val(config.departValueField.val());
                config.displayArrivalNameField.val(fixValue.length != 0 ? fixValue[0].label : "");
                config.arrivalCountryValueField.val(fixValue.length != 0 ? fixValue[0].countryId : 0);

                if (config.dropOffDateValueField.val() == '') {
                    config.dropOffDateField.datepicker('setDate', GetValidDate(new Date()).addDays(3));
                    config.dropOffDateValueField.val(moment(GetValidDate(new Date()).addDays(3)).format(config.dateFormat));
                }

                LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val(), false, false);
            }
            chosenDate = new Date(config.pickUpDateField.datepicker('getDate'));
            var dropOffDate = new Date(config.dropOffDateField.datepicker('getDate'));
            if (chosenDate >= dropOffDate) {
                if (validdropOffDate.length == 0) {
                    config.dropOffDateField.datepicker('setDate', '');
                    config.dropOffDateValueField.val('');
                } else {
                    //if (validdropOffDate.indexOf(chosenDate.getDay() + 1) == -1) {
                    //    config.dropOffDateField.datepicker('setDate', GetValidDate(dropOffDate.addDays(1), validdropOffDate));
                    //    config.dropOffDateValueField.val(moment(GetValidDate(dropOffDate.addDays(1), validdropOffDate)).format(config.dateFormat));
                    //}
                    config.dropOffDateField.datepicker('setDate', GetValidDate(dropOffDate.addDays(1), validdropOffDate));
                    config.dropOffDateValueField.val(moment(GetValidDate(dropOffDate.addDays(1), validdropOffDate)).format(config.dateFormat));
                    config.dropOffDateField.datepicker("option", "minDate", GetValidDate(new Date(config.pickUpDateField.datepicker('getDate')).addDays(1), validdropOffDate));
                    config.dropOffDateField.datepicker("option", "maxDate", dropOffDate.addDays(28));
                }

                LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val(), false, false);
            }

            config.arrivalField.blur();
            return false;
        });
        if (config.displayDepartNameField.val() != '') {
            LimitTime(config.pickUpTimeField, config.departValueField.val(), config.pickUpDateValueField.val(), true, true);
            var chosenDate = new Date(config.pickUpDateField.datepicker('getDate'));

            if (validpickUpDate.length == 0) {
                config.pickUpDateField.datepicker('setDate', '');
                config.pickUpDateValueField.val('');
            }
            //else {
            //    if (validpickUpDate.indexOf(chosenDate.getDay() + 1) == -1) {
            //        config.pickUpDateField.datepicker('setDate', GetValidDate(chosenDate.addDays(1), validpickUpDate));
            //        config.pickUpDateValueField.val(moment(GetValidDate(chosenDate.addDays(1))).format(config.dateFormat));
            //    }
            //}
        }
        //config.arrivalField.autocomplete({
        //    minLength: 0,
        //    source: function (request, response) {
        //        var term = $.ui.autocomplete.escapeRegex(extractLast(request.term))
        //            , startsWithMatcher = new RegExp("^" + term, "i")
        //            , startsWith = $.grep(cities, function (value) {
        //                return startsWithMatcher.test(value.label || value.value || value);
        //            })
        //            , containsMatcher = new RegExp(term, "i")
        //            , contains = $.grep(cities, function (value) {
        //                return $.inArray(value, startsWith) < 0 &&
        //                    containsMatcher.test(value.label || value.value || value);
        //            });

        //        // in order to show all results when input's text equals any cities's item lable
        //        var fixValue = cities.filter(function (item, index) {
        //            return (request.term == item.label);
        //        });

        //        // delegate back to autocomplete, but extract the last term
        //        response(fixValue.length == 1 ? cities : startsWith.concat(contains));
        //    },
        //    select: function (event, ui) {
        //        config.displayArrivalNameField.val(ui.item.label);
        //        config.arrivalValueField.val(ui.item.value);
        //        config.arrivalCountryValueField.val(ui.item.countryId);

        //        LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val());

        //        return false;
        //    },
        //    response: function (event, ui) {

        //        if (ui.content.length == 0) {
        //            config.arrivalValueField.val("");
        //            config.arrivalCountryValueField.val("");
        //        } else {
        //            var item = ui.content[0];
        //            config.arrivalValueField.val(item.value);
        //            config.arrivalCountryValueField.val(item.countryId);
        //        }
        //    }
        //}).focus(function () {
        //    $(this).autocomplete("search");
        //}).autocomplete("instance")._renderItem = function (ul, item) {
        //    return $("<li>")
        //      .append("<div>" + item.label + "</div>")
        //      .appendTo(ul);
        //};
        config.arrivalField.on('change', function () {
            let fixValue = cities.filter(function (item, index) {
                return (config.arrivalField.val() == item.value);
            });

            config.arrivalValueField.val(config.arrivalField.val());
            config.displayArrivalNameField.val(fixValue.length != 0 ? fixValue[0].label : "");
            config.arrivalCountryValueField.val(fixValue.length != 0 ? fixValue[0].countryId : 0);

            if (config.dropOffDateValueField.val() == '') {
                config.dropOffDateField.datepicker('setDate', GetValidDate(new Date()).addDays(3));
                config.dropOffDateValueField.val(moment(GetValidDate(new Date()).addDays(3)).format(config.dateFormat));
            }

            LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val(), false, false);

            if (validdropOffDate.length == 0) {
                config.dropOffDateField.datepicker('setDate', '');
                config.dropOffDateValueField.val('');
            } else {
                var chosenDate = new Date(config.dropOffDateField.datepicker('getDate'));
                if (validdropOffDate.indexOf(chosenDate.getDay() + 1) == -1) {
                    config.dropOffDateField.datepicker('setDate', GetValidDate(chosenDate.addDays(1), validdropOffDate));
                    config.dropOffDateValueField.val(moment(GetValidDate(chosenDate.addDays(1))).format(config.dateFormat));
                }

                config.dropOffDateField.datepicker("option", "minDate", GetValidDate(new Date(config.pickUpDateField.datepicker('getDate')).addDays(1), validdropOffDate));
                config.dropOffDateField.datepicker("option", "maxDate", chosenDate.addDays(28));
            }
            return false;
        });

        if (config.displayArrivalNameField.val() != '') {
            LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val(), false, true);
            if (validdropOffDate.length == 0) {
                config.dropOffDateField.datepicker('setDate', '');
                config.dropOffDateValueField.val('');
            } else {
                var chosenDate = new Date(config.dropOffDateField.datepicker('getDate'));
                //if (validdropOffDate.indexOf(chosenDate.getDay() + 1) == -1) {
                //    config.dropOffDateField.datepicker('setDate', GetValidDate(chosenDate.addDays(1), validdropOffDate));
                //    config.dropOffDateValueField.val(moment(GetValidDate(chosenDate.addDays(1))).format(config.dateFormat));
                //}

                config.dropOffDateField.datepicker("option", "minDate", GetValidDate(new Date(config.pickUpDateField.datepicker('getDate')).addDays(1), validdropOffDate));
                config.dropOffDateField.datepicker("option", "maxDate", chosenDate.addDays(28));
            }
        }

        config.pickUpTimeField.on("change", function (e) {
            if (e) {
                config.pickUpTimeValueField.val(e);
            }
            //else {
            //    config.pickUpTimeField.data("DateTimePicker").date(config.pickUpDateValueField.val() + " " + config.pickUpTimeValueField.val());
            //}
        });

        config.dropOffTimeField.on("change", function (e) {
            if (e) {
                config.dropOffTimeValueField.val(e);
            }
            //else {
            //    config.dropOffTimeField.data("DateTimePicker").date(config.dropOffDateValueField.val() + " " + config.dropOffTimeValueField.val());
            //}
        });
    }

    return { init: init };
}




/*-- old code --*/


//var holidays = ["2017-04-17", "2017-04-27", "2017-05-25", "2017-06-05", "2017-12-25", "2017-12-26"];
//var invalidHours = [0, 1, 2, 3, 4, 5, 6, 7, 18, 19, 20, 21, 22, 23];
//var validHours = [8, 9, 10, 11, 12, 13, 14, 15, 16, 17];

//function gv(inId) {
//    if (document.getElementById) {
//        return document.getElementById(inId).value;
//    } else {
//        return '';
//    }
//}

//function validatesearchform() {
//    if (document.getElementById) {
//        var i;
//        var d_tmp = '';
//        var i_chk = 0;
//        var i_vld = '';


//        if (gv('StartDate')) {
//            var d_v = gv('StartDate');
//        } else {
//            i_chk++;
//            i_vld = i_vld + ' - Ophaaldatum [leeg]\n';
//        }

//        if (gv('EndDate')) {
//            var d_r = gv('EndDate');
//        } else {
//            i_chk++;
//            i_vld = i_vld + ' - Inleverdatum [leeg]\n';
//        }
//        var today = new Date();

//        if (d_v && d_r) {
//            //zet vreemde seperators om in /
//            d_vS = d_v.replace(/-|;|:|\\|\.|,/g, '/');
//            d_rS = d_r.replace(/-|;|:|\\|\.|,/g, '/');
//            //zet datumsegmenten in een array
//            var d_vA = new Array();
//            var d_rA = new Array();
//            for (i = 0; i < d_vS.split("/").length; i++) {
//                d_tmp = d_vS.split("/")[i];
//                if (!d_tmp == '') d_vA[i] = d_tmp;
//            }
//            for (i = 0; i < d_rS.split("/").length; i++) {
//                d_tmp = d_rS.split("/")[i];
//                if (!d_tmp == '') d_rA[i] = d_tmp;
//            }

//            var chosendate = new Date(parseInt(d_vA[2], 10), parseInt(d_vA[1], 10) - 1, parseInt(d_vA[0], 10));
//            var pickupTime = parseInt($('#PickUpTime').val().split(':')[0], 10);
//            pickupTime += (parseInt($('#PickUpTime').val().split(':')[1], 10) / 60);

//            var dropofftime = parseInt($('#DropOffTime').val().split(':')[0], 10);
//            dropofftime += (parseInt($('#DropOffTime').val().split(':')[1], 10) / 60);


//            //weekend is friday after 17:00, saturday or sunday
//            var isWeekendCheck = ((today.getDay() == 5 && today.getHours() >= 17) || today.getDay() == 6 || today.getDay() == 0);
//            if (chosendate.getDate() == today.getDate() && chosendate.getMonth() == today.getMonth() && chosendate.getYear() == today.getYear()) {
//                showremark(1001);
//                return false;
//            }
//            else if (!isWeekendCheck && (chosendate.getDate() == today.getDate() + 1 && chosendate.getMonth() == today.getMonth() && chosendate.getYear() == today.getYear())) {
//                //tomorrow, check if  time is later than current time
//                if (pickupTime <= today.getHours()) {
//                    showremark(1001);
//                    return false;
//                }
//            }
//                //if it is in weekend, and chosen day is next monday before 12:00. Last check with miliseconds ensures it is the next monday
//            else if (isWeekendCheck && chosendate.getDay() == 1 && pickupTime < 12 && (Math.abs(today - chosendate) / 3600000) < 72) {
//                showremark(1002);
//                return false;
//            }

//            if (d_v == d_r && pickupTime >= dropofftime) {
//                //ophaal en terugbreng data zijn zelfde, en ophaaltijdstip ligt na terugbreng tijdstip
//                showremark(1003);
//                return false;
//            }
//        }

//        if (i_chk == 0) {
//            return true;
//        } else {
//            var Message = ") Het formulier is niet correct of niet volledig ingevuld! \n Veld(en) om te controleren: \n";
//            console.log(Message);

//            return false;
//        }
//    }

//    return false;
//}

//function showremark(id) {
//    $("#messageValidateModal_" + id).modal();
//}

//function GetValidDate(d) {
//    var date = new Date(d);

//    //skip sunday and holidays
//    while (true) {
//        var datestring = jQuery.datepicker.formatDate('yy-mm-dd', date);
//        if (holidays.indexOf(date) == -1 && (date.getDay() != 0)) {
//            break;
//        }
//        date = date.addDays(1);
//    }

//    return date;
//}

//var FilterLocationForm = function () {

//    var config = {
//        btnSearch: undefined,
//        frmCamperSearch: undefined,
//        destinationsJsonString: undefined,
//        departField: undefined,
//        displayDepartNameField: undefined,
//        departValueField: undefined,
//        departCountryValueField: undefined,
//        arrivalField: undefined,
//        displayArrivalNameField: undefined,
//        arrivalValueField: undefined,
//        arrivalCountryValueField: undefined,
//        pickUpDateField: undefined,
//        pickUpDateValueField: undefined,
//        dropOffDateField: undefined,
//        dropOffDateValueField: undefined,
//        pickUpTimeField: undefined,
//        pickUpTimeValueField: undefined,
//        dropOffTimeField: undefined,
//        dropOffTimeValueField: undefined,
//        dateFormat: "D-M-YYYY",
//        timeFormat: "HH:mm:ss",
//        hoursOfArrival: [],
//        hoursOfDepartCountry: [],
//        urlGetLocationById: '',
//    };

//    var changePickupDate = function (date, changeSelectedDropoffDate) {
//        //var dmy = date.split("/");

//        var today = new Date();
//        var chosendate = new Date(date);

//        if (chosendate.getMonth() + "/" + chosendate.getDate() == today.getMonth() + "/" + today.getDate()) {
//            showremark(1001);
//        }

//        if (changeSelectedDropoffDate) {
//            var nextDay = chosendate.addDays(1);
//            var nextMonth = chosendate.addDays(28);

//            config.dropOffDateField.data("DateTimePicker").minDate(nextDay);
//            config.dropOffDateField.data("DateTimePicker").maxDate(nextMonth);
//            config.dropOffDateField.data("DateTimePicker").date(GetValidDate(nextDay));
//        }
//    }

//    var initDateTimePicker = function (options) {
//        options.pickUpDateField.datetimepicker({
//            locale: 'nl',
//            format: 'ddd D MMM Y',
//            useCurrent: false,
//            disabledDates: holidays,
//            daysOfWeekDisabled: [0]
//        });

//        if (options.pickUpDateValueField.val() == '') {
//            let dateInit = GetValidDate(new Date()).addDays(2);

//            options.pickUpDateField.data('DateTimePicker').date(dateInit);
//            config.pickUpDateValueField.val(moment(dateInit).format(config.dateFormat));
//        }


//        options.pickUpDateField.data("DateTimePicker").date(moment(options.pickUpDateField.val(), "DD/MM/YYYY"));

//        options.pickUpDateField.data("DateTimePicker").minDate(new Date());

//        options.pickUpTimeField.datetimepicker({
//            useCurrent: false,
//            format: "HH:mm",
//            disabledHours: invalidHours,
//        });

//        options.pickUpTimeField.data("DateTimePicker").stepping(30);

//        if (options.pickUpTimeValueField.val() == "") {
//            options.pickUpTimeField.data("DateTimePicker").date("09:00");
//            options.pickUpTimeValueField.val(options.pickUpTimeField.val() + ":00");
//        }

//        options.dropOffDateField.datetimepicker({
//            locale: 'nl',
//            format: 'ddd D MMM Y',
//            useCurrent: false,
//            disabledDates: holidays,
//            daysOfWeekDisabled: [0]
//        });

//        if (options.dropOffDateValueField.val() == '') {
//            let dateInit = GetValidDate(new Date()).addDays(3);

//            options.dropOffDateField.data('DateTimePicker').date(dateInit);
//            options.dropOffDateValueField.val(moment(dateInit).format(config.dateFormat));
//        }

//        options.dropOffDateField.data("DateTimePicker").date(moment(options.dropOffDateField.val(), "DD/MM/YYYY"));

//        options.dropOffDateField.data("DateTimePicker").minDate(options.pickUpDateField.data("DateTimePicker").date());

//        options.dropOffTimeField.datetimepicker({
//            useCurrent: false,
//            format: "HH:mm",
//            disabledHours: invalidHours
//        });

//        options.dropOffTimeField.data("DateTimePicker").stepping(30);

//        if (options.dropOffTimeValueField.val() == "") {
//            options.dropOffTimeField.data("DateTimePicker").date("09:00");
//            options.dropOffTimeValueField.val(options.dropOffTimeField.val() + ":00");
//        }

//        // Linked 
//        options.pickUpDateField.on("dp.change", function (e) {
//            if (e.date != false) {
//                changePickupDate(e.date, true);
//                var now = new Date();
//                var chosenDate = new Date(e.date);
//                var diffDate = chosenDate.getDate() - now.getDate();
//                if (diffDate == 1) {
//                    var validTime = now.getHours() + ":" + now.getMinutes();
//                    if (invalidHours.indexOf(now.getHours()) != -1) {
//                        validTime = validHours[validHours.length - 1] + ":00";
//                    }
//                    options.pickUpTimeField.data("DateTimePicker").date(validTime);
//                    options.pickUpTimeValueField.val(options.pickUpTimeField.val() + ":00");
//                }
//                else {
//                    options.pickUpTimeField.data("DateTimePicker").date("09:00");
//                    options.pickUpTimeValueField.val(options.pickUpTimeField.val() + ":00");
//                }
//                options.dropOffDateField.blur();
//            }
//        });

//        options.dropOffDateField.on("dp.change", function (e) {
//            if (e.date != false)
//                changePickupDate(e.date, false);
//        });
//    };

//    function extractLast(term) {
//        return split(term).pop();
//    }

//    function split(val) {
//        return val.split(/,\s*/);
//    }

//    function GetHoursByLocation(locationId, date) {
//        var result = null;

//        $.ajax({
//            url: config.urlGetLocationById,
//            dataType: 'json',
//            async: false,
//            data: { Id: locationId, date: date },
//            success: function (data, status) {
//                if (data.length != 0) {
//                    result = data;
//                }
//            },
//            error: function (XHR, txtStatus, errThrown) {
//            }
//        });

//        return result;
//    }

//    function LimitTime(element, locationId, date) {
//        if (locationId != '' && date != '') {
//            var durationTime = GetHoursByLocation(locationId, date);

//            if (durationTime != null) {
//                element.data("DateTimePicker").minDate(moment({ h: durationTime.minTime.Hour, m: durationTime.minTime.Minute }));
//                element.data("DateTimePicker").maxDate(moment({ h: durationTime.maxTime.Hour, m: durationTime.maxTime.Minute }));
//            }
//            else {
//                element.data("DateTimePicker").minDate(moment({ h: 8 }));
//                element.data("DateTimePicker").maxDate(moment({ h: 17, m: 30 }));
//            }
//        }
//    }

//    var init = function (options) {

//        $.extend(config, options);

//        config.btnSearch.click(function () {

//            config.frmCamperSearch.validate();

//            if (config.frmCamperSearch.valid() && validatesearchform()) {
//                var selectedDestination = cities.filter(function (item, index) {
//                    return (config.departValueField.val() == item.Id);
//                })[0];

//                config.frmCamperSearch.attr("action", "/" + selectedDestination.UrlNameParent + "/" + selectedDestination.UrlName);

//                return true;
//            }

//            return false;
//        });

//        initDateTimePicker(config);

//        var countries = JSON.parse(config.destinationsJsonString.val());
//        var cities = [];
//        countries.forEach(function (item) {
//            item.Cities.forEach(function (city) {
//                city.label = city.DisplayName;
//                city.value = city.Id;
//                city.category = item.DisplayName;
//                city.countryDisplayName = item.DisplayName;
//                city.countryId = item.Id;
//                city.UrlNameParent = item.UrlName;
//            });
//            cities = cities.concat(item.Cities);
//        });

//        //cities = cities.sort(sort(null, 'label'));

//        cities = cities.sort(function (a, b) {
//            if (a.label < b.label)
//                return -1;
//            if (a.label > b.label)
//                return 1;
//            return 0;
//        });


//        config.departField.rules("add",
//                {
//                    existedcity: true
//                });

//        config.arrivalField.rules("add",
//                {
//                    existedcity: true
//                });

//        var enterDepartFieldValue = {};
//        config.departField.autocomplete({
//            minLength: 0,
//            source: function (request, response) {
//                var term = $.ui.autocomplete.escapeRegex(extractLast(request.term)),
//                    startsWithMatcher = new RegExp("^" + term, "i"),
//                    startsWith = $.grep(cities,
//                        function (value) {
//                            return startsWithMatcher.test(value.label || value.value || value);
//                        }),
//                    containsMatcher = new RegExp(term, "i"),
//                    contains = $.grep(cities,
//                        function (value) {
//                            return $.inArray(value, startsWith) < 0 &&
//                                containsMatcher.test(value.label || value.value || value);
//                        });

//                // in order to show all results when input's text equals any cities's item lable
//                var fixValue = cities.filter(function (item, index) {
//                    return (request.term == item.label);
//                });

//                // delegate back to autocomplete, but extract the last term
//                response(fixValue.length == 1 ? cities : startsWith.concat(contains));
//            },
//            select: function (event, ui) {
//                config.displayDepartNameField.val(ui.item.label);
//                config.departValueField.val(ui.item.value);
//                config.departCountryValueField.val(ui.item.countryId);

//                if (config.arrivalValueField.val() == "") {
//                    config.displayArrivalNameField.val(ui.item.label);
//                    config.arrivalValueField.val(ui.item.value);
//                    config.arrivalCountryValueField.val(ui.item.countryId);

//                    LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val());
//                }

//                LimitTime(config.pickUpTimeField, config.departValueField.val(), config.pickUpDateValueField.val());

//                config.arrivalField.blur();

//                return false;
//            },
//            response: function (event, ui) {
//                if (ui.content.length == 0) {
//                    config.departValueField.val("");
//                    config.departCountryValueField.val("");

//                    if (config.arrivalValueField.val() == "") {
//                        config.displayArrivalNameField.val("");
//                        config.arrivalValueField.val("");
//                        config.arrivalCountryValueField.val("");
//                    }
//                } else {
//                    var item = ui.content[0];

//                    config.departValueField.val(item.value);
//                    config.departCountryValueField.val(item.countryId);

//                    if (ui.content.length == 1) {
//                        enterDepartFieldValue.label = item.label;
//                        enterDepartFieldValue.value = item.value;
//                        enterDepartFieldValue.countryId = item.countryId;
//                    }
//                }
//            }
//        }).focus(function () {
//            $(this).autocomplete("search");
//        }).autocomplete("instance")._renderItem = function (ul, item) {
//            return $("<li>")
//              .append("<div>" + item.label + "</div>")
//              .appendTo(ul);
//        };

//        config.departField.on("blur", function () {
//            if (enterDepartFieldValue.value != null && config.arrivalValueField.val() == "") {
//                config.displayArrivalNameField.val(enterDepartFieldValue.label);
//                config.arrivalValueField.val(enterDepartFieldValue.value);
//                config.arrivalCountryValueField.val(enterDepartFieldValue.countryId);
//            }
//        });

//        config.arrivalField.autocomplete({
//            minLength: 0,
//            source: function (request, response) {
//                var term = $.ui.autocomplete.escapeRegex(extractLast(request.term))
//                    , startsWithMatcher = new RegExp("^" + term, "i")
//                    , startsWith = $.grep(cities, function (value) {
//                        return startsWithMatcher.test(value.label || value.value || value);
//                    })
//                    , containsMatcher = new RegExp(term, "i")
//                    , contains = $.grep(cities, function (value) {
//                        return $.inArray(value, startsWith) < 0 &&
//                            containsMatcher.test(value.label || value.value || value);
//                    });

//                // in order to show all results when input's text equals any cities's item lable
//                var fixValue = cities.filter(function (item, index) {
//                    return (request.term == item.label);
//                });

//                // delegate back to autocomplete, but extract the last term
//                response(fixValue.length == 1 ? cities : startsWith.concat(contains));
//            },
//            select: function (event, ui) {
//                config.displayArrivalNameField.val(ui.item.label);
//                config.arrivalValueField.val(ui.item.value);
//                config.arrivalCountryValueField.val(ui.item.countryId);

//                LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val());

//                return false;
//            },
//            response: function (event, ui) {

//                if (ui.content.length == 0) {
//                    config.arrivalValueField.val("");
//                    config.arrivalCountryValueField.val("");
//                } else {
//                    var item = ui.content[0];
//                    config.arrivalValueField.val(item.value);
//                    config.arrivalCountryValueField.val(item.countryId);
//                }
//            }
//        }).focus(function () {
//            $(this).autocomplete("search");
//        }).autocomplete("instance")._renderItem = function (ul, item) {
//            return $("<li>")
//              .append("<div>" + item.label + "</div>")
//              .appendTo(ul);
//        };

//        config.pickUpDateField.on("dp.change", function (e) {
//            if (e.date) {
//                config.pickUpDateValueField.val(e.date.format(config.dateFormat));
//            } else {
//                config.pickUpDateField.data("DateTimePicker").date(config.pickUpDateValueField.val());
//            }

//            LimitTime(config.pickUpTimeField, config.departValueField.val(), config.pickUpDateValueField.val());
//        });

//        config.dropOffDateField.on("dp.change", function (e) {
//            if (e.date) {
//                config.dropOffDateValueField.val(e.date.format(config.dateFormat));
//            } else {
//                config.dropOffDateField.data("DateTimePicker").date(config.dropOffDateValueField.val());
//            }

//            LimitTime(config.dropOffTimeField, config.arrivalValueField.val(), config.dropOffDateValueField.val());
//        });

//        config.pickUpTimeField.on("dp.change", function (e) {
//            if (e.date) {
//                config.pickUpTimeValueField.val(e.date.format(config.timeFormat));
//            } else {
//                config.pickUpTimeField.data("DateTimePicker").date(config.pickUpDateValueField.val() + " " + config.pickUpTimeValueField.val());
//            }
//        });

//        config.dropOffTimeField.on("dp.change", function (e) {
//            if (e.date) {
//                config.dropOffTimeValueField.val(e.date.format(config.timeFormat));
//            } else {
//                config.dropOffTimeField.data("DateTimePicker").date(config.dropOffDateValueField.val() + " " + config.dropOffTimeValueField.val());
//            }
//        });
//    }

//    return { init: init };
//} 