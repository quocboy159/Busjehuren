
Date.prototype.formatString = function (character) {
    var d = this,
       month = '' + (d.getMonth() + 1),
       day = '' + d.getDate(),
       year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join(character);
}

Date.prototype.addDays = function (days) {
    var ms = this.getTime() + (86400000 * days);
    var added = new Date(ms);
    return added;
};

jQuery.fn.exists = function () {
    return this.length > 0;
}

function sort(desc, key) {
    return function (a, b) {
        return desc ? ~~(key ? a[key] < b[key] : a < b)
                    : ~~(key ? a[key] > b[key] : a > b);
    };
}

function isMobile()
{
    return screen.width < 768;
}