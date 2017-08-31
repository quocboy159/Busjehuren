$.validator.addMethod("equalchecked", function (value, element) {
    return element.checked;
}, "The field is required");

$.validator.addMethod("existedcity", function (value, element) {
    var countries = JSON.parse($("#destinationsJsonString").val());
    var cities = [];
    countries.forEach(function (item) {
        item.Cities.forEach(function (city) {
            cities.push(city.DisplayName.toLowerCase().replace(" ",""));
        });
        
    });
    return $.inArray(value.toLowerCase().replace(" ", ""), cities) > -1;
}, "invalid address");