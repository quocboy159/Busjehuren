var breakpoint_sp = 767;

$(document).ready(function () {

    var locations = [];

    if ($('#map').length) {

        // Get all locations data
        $.each($('.location-link'), function () {
            var data_latitude = $(this).data('latitude'),
                data_longtitude = $(this).data('longtitude'),
                data_title = $(this).data('title'),
                data_open_time = $(this).data('open-time'),
                data_id = $(this).data('id'),
                data_address = $(this).data('address'),
                data_url = $(this).data('url');
                data_locationid = $(this).data('locationid');

            if (data_latitude != '' && data_longtitude != '') {
                var location = [data_title, data_latitude, data_longtitude, data_address, data_open_time, data_id, data_url, data_locationid];
                locations.push(location);
            };
        });

        google.maps.event.addDomListener(window, 'load', initializeGoogleMap(locations));

        $.each($('.spanCity'), function () {

            var self = $(this);

            var items = locations.filter(function (item) {
                return item[7] == self.data("locationid");
            });

            if (items.length != 0) {
                var content = renderPopover(items);
                $(this).attr("data-content", content);
            }
        });
       
    };

    $('body').on('click touchstart', '.close-location-marker', function (e) {
        $(this).parent("div").parent().siblings().popover('hide');
    });

    ShowActivedNavigate($("#nav-locaties"));
});

/* fix position for map */
function fixedPositionForMap() {
    $(window).scroll(function () {
        if (IsDesktop()) {

            var determinePosition = $(".section-rental-location").offset().top;
            var limitHeight = $("footer").last().offset().top - $("#map").height();
            var iCurScrollPos = $(this).scrollTop();

            if (iCurScrollPos > determinePosition && iCurScrollPos < limitHeight) {
                $("#map").css("margin-top", (iCurScrollPos - determinePosition) + "px");
            }
            else if (iCurScrollPos <= determinePosition) {
                $("#map").css("margin-top", 0 + "px");
            }
        }
        else
            $("#map").css("margin-top", 0 + "px");
    });
}

/**
   * Render container for google map, caculate correct height/width according to window size
   * @param {jQuery object} container
   * @param {jQuery object} reference container 
   * @return null
   */
function renderMapContainer($el, $ref, $customHeight) {
    var ref = {
        "left": $ref.position().left,
        "height": $ref.height() + 50,
        "width": $ref.width() + 40
    };

    if ($(window).width() <= breakpoint_sp) {
        $el.width($('body').width()).height(400);
    } else {
        $el.width(ref.left).height($customHeight);
    }
}

/* Draw Google Map */
function initializeGoogleMap(locations) {

    // find <div id="map" />, #maker have data-image as marker image
    var mapCanvas = document.getElementById('map'),
        marker_icon = $('#marker').data('image');

    const latitudeDefault = 51.654534;
    const longtitudeDefault = 5.943475;

    if (locations.length > 0) {
        // Create Map
        var mapOptions = {
            center: new google.maps.LatLng(latitudeDefault, longtitudeDefault),
            zoom: 8,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            mapTypeControl: false,
            panControl: false,
            ZoomControlStyle: {
                style: google.maps.ZoomControlStyle.SMALL
            }
        };

        var map = new google.maps.Map(mapCanvas, mapOptions),
            infowindow = new google.maps.InfoWindow({ maxWidth: 350 });

        // Draw marker
        var markers = [], i;

        for (i = 0; i < locations.length; i++) {
            var marker;
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                map: map,
                icon: marker_icon
            });

            markers.push(marker);

            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    // Info Window
                    var content = renderInfoWindow(locations[i][0], locations[i][3], locations[i][4], locations[i][6]);
                    infowindow.setContent(content);
                    infowindow.open(map, marker);

                    activeLocation(locations[i][5]);
                }
            })(marker, i));
        } // End for

        // START INFOWINDOW CUSTOMIZE.
        google.maps.event.addListener(infowindow, 'domready', function () {

            // Reference to the DIV that wraps the bottom of infowindow
            var iwOuter = $('.gm-style-iw');

            var iwBackground = iwOuter.prev();

            // Removes background shadow DIV
            iwBackground.children(':nth-child(2)').css({ 'display': 'none' });

            // Removes white background DIV
            iwBackground.children(':nth-child(4)').css({ 'display': 'none' });

            // Moves the infowindow 115px to the right.
            iwOuter.parent().parent().css({ left: '115px' });

            // Moves the shadow of the arrow 76px to the left margin.
            iwBackground.children(':nth-child(1)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

            // Moves the arrow 76px to the left margin.
            iwBackground.children(':nth-child(3)').attr('style', function (i, s) { return s + 'left: 76px !important;' });

            // Changes the desired tail shadow color.
            iwBackground.children(':nth-child(3)').find('div').children().css({ 'box-shadow': 'none', 'z-index': '1' });

            // Reference to the div that groups the close button elements.
            var iwCloseBtn = iwOuter.next();

            // Apply the desired effect to the close button
            iwCloseBtn.addClass('close-map-marker');

            // If the content of infowindow not exceed the set maximum height, then the gradient is removed.
            if ($('.iw-content').height() < 140) {
                $('.iw-bottom-gradient').css({ display: 'none' });
            }

        });

        google.maps.event.addListener(infowindow, 'closeclick', function () {
            $('.location-link').removeClass('active');
        });


        // close popup on map
        $('body').on('click', '.close-map-marker', function (event) {
            $(this).parent().addClass("hide");
        });


        // Open/Close Info Window when click location item
        $('body').on('click', '.location-item', function (event) {

            var id = $('.location-item').index($(this));

            // Deactive
            if ($(this).hasClass('active')) {
                infowindow.close();
                deActiveLocation(locations[id][5]);
            } else {
                // Active
                map.setZoom(14);
                map.setCenter(markers[id].getPosition());

                // Info Window
                var content = renderInfoWindow(locations[id][0], locations[id][3], locations[id][4], locations[id][6]);
                infowindow.setContent(content);
                infowindow.open(map, markers[id]);
                activeLocation(locations[id][5]);

                window.setTimeout(function () {
                    map.panTo(markers[id].getPosition());
                }, 3000);
            }

        });

        // When resize window
        $(window).resize(function () {
            //renderMapContainer($('#map'), $('.id-container-map-right'));
            google.maps.event.trigger(map, 'resize');
        });
    }
}

/* Add/Remove active class when clicked on location-link */
function activeLocation(id) {
    $('.location-link').each(function () {
        if (($(this).data('id') != id)) {
            $(this).removeClass('active');
        }
    });

    var selectedLocation = $('.location-link').filter(function (index) {
        return ($(this).data('id') == id);
    });

    selectedLocation.addClass('active');
}

function deActiveLocation(id) {
    var selectedLocation = $('.location-link').filter(function (index) {
        return ($(this).data('id') == id);
    });

    selectedLocation.removeClass('active');
}

/* Render an customize info windows for google map */
function renderInfoWindow(title, address, open, url) {
    var contentString = '<div id="iw-container">' +
      '<div class="iw-title">' + title + '</div>' +
      '<div class="iw-content">' +
        '<div class="iw-row clearfix">' +
          '<div class="iw-desc">' + address + '</div>' +
        '</div>' +
        '<div class="iw-row clearfix">' +
          '<div class="iw-desc">' + open + '</div>' +
        '</div>' +
      '</div>' +
      '<a href="' + url + '"class="location-btn">bekijk beschikbare voertuigen</a>' +
    '</div>';

    return contentString;
}

function renderPopover(items)
{
    var content = '<div class="close-location-marker"></div>' + '<div class="owl-carousel owl-theme" >';

    $.each(items, function () {
        var item = $(this);
        content += '<div class="item">';
        content += renderInfoWindow(item[0], item[3], item[4], item[6]);
        content += "</div>";
    });

    content += "</div>";

    content += "<script> $(document).ready(function () { $('.owl-carousel').owlCarousel({items: 1,loop: true,margin: 10, dots: true }); }) </script>";

    return content;
}

/* scroll page to position when user clicked on location on the map */
function scrolltoSelectedLocation(id) {
    if (IsDesktop()) {
        var elementLocation = $("div[data-id=" + id + "]");
        var heightOfLocation = $('.location-item.location-link').height();
        var top = elementLocation.offset().top;

        $('html, body').animate({
            scrollTop: top - heightOfLocation
        }, 800);
    }
}

function IsDesktop() {
    return $(window).width() >= 992;
}
