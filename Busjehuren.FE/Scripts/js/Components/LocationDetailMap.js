var breakpoint_sp = 767;
$(document).ready(function () {
        if ($('#map').length) {

            // we need to set the height of map the same with the left paragraph at list-location page
            if ($('.same-height-with-map').length) {
                $('#map').height($('.same-height-with-map').height());
            };

            if ($('.id-container-map-right').length) {
                renderMapContainer($('#map'), $('.id-container-map-right'));
            };


            // Get all locations data
            var locations = [];
            $.each($('.location-link'), function () {
                var data_latitude = $(this).data('latitude'),
                    data_longtitude = $(this).data('longtitude'),
                    data_title = $(this).data('title'),
                    data_open_time = $(this).data('open-time'),
                    data_address = $(this).data('address');

                if (data_latitude != '' && data_longtitude != '') {
                    var location = [data_title, data_latitude, data_longtitude, data_address, data_open_time];
                    locations.push(location);
                };
            });

            google.maps.event.addDomListener(window, 'load', initializeGoogleMap(locations));
        };
    });
/**
   * Render container for google map, caculate correct height/width according to window size
   * @param {jQuery object} container
   * @param {jQuery object} reference container 
   * @return null
   */
function renderMapContainer($el, $ref) {
    
    if ($(window).width() <= breakpoint_sp) {
        $el.width($('body').width()).height(400);
    } 
}

/*
  * Draw Google Map
  */
function initializeGoogleMap(locations) {


    // find <div id="map" />, #maker have data-image as marker image

    var mapCanvas = document.getElementById('map'),
        marker_icon = $('#marker').data('image');

    if (locations.length > 0) {
        // Create Map
        var mapOptions = {
            center: new google.maps.LatLng(locations[0][1], locations[0][2]),
            zoom: 12,
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
                    var content = renderInfoWindow(locations[i][0], locations[i][3], locations[i][4]);
                    infowindow.setContent(content);
                    infowindow.open(map, marker);
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

        // Open/Close Info Window when click location item
        $('body').on('click', '.location-item', function (event) {
            // Deactive
            if ($(this).hasClass('active')) {
                infowindow.close();
            } else {
                // Active
                map.setZoom(14);

                var id = $('.location-item').index($(this));
                map.setCenter(markers[id].getPosition());

                // Info Window
                var content = renderInfoWindow(locations[id][0], locations[id][3], locations[id][4]);
                infowindow.setContent(content);
                infowindow.open(map, markers[id]);

               // window.setTimeout(function () {
                    map.panTo(markers[id].getPosition());
               // }, 3000);
            }

        });

        // When resize window
        $(window).resize(function () {
            renderMapContainer($('#map'), $('.id-container-map-right'));
            google.maps.event.trigger(map, 'resize');
        });
    }
}

/*
* Add/Remove active class when clicked on location-link
*/
function activeLocation(id) {
    $('.location-link').removeClass('active');
    $('.location-link').filter(function (index) {
        return ($(this).data('id') == id);
    }).addClass('active');

}

/**
* Render an customize info windows for google map
*/
function renderInfoWindow(title, address, open) {
    // InfoWindow content
    var contentString = '<div id="iw-container">' +
      '<div class="iw-title">' + title + '</div>' +
      '<div class="iw-content">' +
        '<div class="iw-row clearfix">' +
          '<div class="iw-subTitle">Adres</div>' +
          '<div class="iw-desc">' + address + '</div>' +
        '</div>' +
        '<div class="iw-row clearfix">' +
          '<div class="iw-subTitle">Openingstijden</div>' +
          '<div class="iw-desc">' + open + '</div>' +
        '</div>' +
      '</div>' +
    '</div>';

    return contentString;
}
