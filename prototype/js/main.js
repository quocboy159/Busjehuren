(function($){
  var breakpoint_sp = 767;

  // *********** WINDOW RESIZE EVENT *************
	$(window).resize(function() {
	   	fixedFooter();   

      if ($(window).width() <= breakpoint_sp ) {
        $('body').on('click', '.call-toggle-location', function(event) {
          $(this).parents('.toggle-location-zone').toggleClass('active');
        });
      };
	});

  // ************************
	$(document).ready(function() {

		$('[data-toggle="popover"]').popover();
		$('.input-number').inputNumber();
    
    $('#form').parsley();
		$('#formContact').parsley();
    $('.match-height').matchHeight();

		fixedFooter();		
		//initDateTimePicker();
		handleCheckboxChange();
    expendParsley();
    trackChangeOverviewCart();

		// Sections backgrounds

		var pageSection = $(".home-section, .page-section, .small-section, .split-section");
		pageSection.each(function(indx){
		    
		    if ($(this).attr("data-background")){
		        $(this).css("background-image", "url(" + $(this).data("background") + ")");
		    }
		});

    // *********** Init Google Map *************
    if ( $('#map').length ) {

      // we need to set the height of map the same with the left paragraph at list-location page
      if ( $('.same-height-with-map').length ) {
        $('#map').height( $('.same-height-with-map').height() ) ;
      }; 

      if ( $('.id-container-map-right').length ) {
        renderMapContainer($('#map'),$('.id-container-map-right'));
      }; 
      

      // Get all locations data
      var locations = [];
      $.each($('.location-link'), function() {
          var data_latitude  = $(this).data('latitude'),
              data_longitude = $(this).data('longitude'),
              data_title     = $(this).data('title'),
              data_open_time = $(this).data('open-time'),            
              data_address   = $(this).data('address');
                
        if (data_latitude != '' && data_longitude != '' ) {
          var location = [ data_title,data_latitude,data_longitude, data_address, data_open_time];
          locations.push(location);
        };      
      });
      
      google.maps.event.addDomListener(window, 'load', initializeGoogleMap(locations) );
    };
  

    // *********** EVENTS *************
    $('body').on('click', '.btn-r-open', function(event) {
      handleShowOverview('open', $(this) );
      $('.btn-r-close').show();
      $('.btn-r-open').hide();
    });
    $('body').on('click', '.btn-overview-open', function(event) {
      handleShowOverview('open', $(this));
    });   
    $('body').on('click', '.btn-overview-close', function(event) {
      handleShowOverview('close', $(this));
    }); 

    $('body').on('click', '.btn-r-close', function(event) {
      handleShowOverview('close', $(this));
      $('.btn-r-open').show();
      $('.btn-r-close').hide();
    });
   
    $('body').on('click', '.call-action-handler', function(event) {
      handleShowSearch($(this).data('action'));
    });

    $('body').on('click', '.call-filter-handler', function(event) {
      handleShowFilter();
    });

    // *********** EVENTS ON SP ONLY *************
    if ($(window).width() <= 767 ) {
      $('body').on('click', '.call-toggle-location', function(event) {
        $(this).parents('.toggle-location-zone').toggleClass('active');
      });
    };
    
    
	});

  /**
  * Expand/Collapse Filter on SP
  */
  function handleShowFilter(){
    $('.primary-sidebar').toggleClass('open');   
  }

  /**
  * Expand/Collapse Search form on sp
  */
  function handleShowSearch(action){
    
    switch (action) {
      case 'close':
        $('.search-header-zone').removeClass('open');
        break;
      case 'open':
        $('.search-header-zone').addClass('open');
        break;
    }
  }

  /**
  * Expend Parsley with successful state
  */
  function expendParsley(){
    window.Parsley.on('field:success', function() {
      
      
    });  
  }  

  /**
   * Render container for google map, caculate correct height/width according to window size
   * @param {jQuery object} container
   * @param {jQuery object} reference container 
   * @return null
   */
  function renderMapContainer( $el, $ref ) {
    var ref = {
      "left" : $ref.position().left,
      "height" : $ref.height() + 50,
      "width" : $ref.width() + 40 
    };
    if ($(window).width() <= breakpoint_sp ) {
      $el.width($('body').width()).height(400);
    } else {
      $el.width(ref.left).height(ref.height);
    }
    
  }

  /**
  * OVERVIEW CART :: change position from fixed -> relative
  */
  function trackChangeOverviewCart(){
    var offsetHeight = $(window).height() - $('.section-cart-footer').outerHeight();    

    // when footer hit the view + offset ( height of overview )
    var waypoints = $('#footer').waypoint(function(direction) {
    
      switch (direction) {
        case 'down':
          // Remove fixed position
          $('.section-cart-footer:not(.step4)').addClass('remove-fixed');
          break;
        case 'up':
          // add fixed position
          $('.section-cart-footer:not(.step4)').removeClass('remove-fixed');
          break;
      }
    }, {
      offset: offsetHeight
    });
  }

  /**
  * Toggle Overview Cart footer
  */
  function handleShowOverview(action, $el ){
    var $small_overview = $el.parents('.container').find('.row-item-in-card'),
        $big_overview = $el.parents('.container').find('.block-overview-application');

    switch (action) {
      case 'close':
        $big_overview.slideUp();
        $small_overview.slideDown();        
       break;
      case 'open':        
        $small_overview.slideUp();
        $big_overview.slideDown();
        break;
    }
  }



  /*
  * Draw Google Map
  */
  function initializeGoogleMap(locations) {     


    // find <div id="map" />, #maker have data-image as marker image
    
    var mapCanvas = document.getElementById('map'), 
        marker_icon = $('#marker').data('image');
      
    if (locations.length > 0 ) {
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
        
      var map = new google.maps.Map(mapCanvas,mapOptions),
          infowindow = new google.maps.InfoWindow({maxWidth: 350});

      

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

        google.maps.event.addListener(marker, 'click', (function(marker, i) {
          return function() {

            // Info Window
            var content = renderInfoWindow( locations[i][0],locations[i][3],locations[i][4] );
            infowindow.setContent(content);
            infowindow.open(map, marker);

            activeLocation(locations[i][1], locations[i][2]);
          }
        })(marker, i));

        

      } // End for

 
      // START INFOWINDOW CUSTOMIZE.
      google.maps.event.addListener(infowindow, 'domready', function() {

        // Reference to the DIV that wraps the bottom of infowindow
        var iwOuter = $('.gm-style-iw');
      
        var iwBackground = iwOuter.prev();

        // Removes background shadow DIV
        iwBackground.children(':nth-child(2)').css({'display' : 'none'});

        // Removes white background DIV
        iwBackground.children(':nth-child(4)').css({'display' : 'none'});

        // Moves the infowindow 115px to the right.
        iwOuter.parent().parent().css({left: '115px'});

        // Moves the shadow of the arrow 76px to the left margin.
        iwBackground.children(':nth-child(1)').attr('style', function(i,s){ return s + 'left: 76px !important;'});

        // Moves the arrow 76px to the left margin.
        iwBackground.children(':nth-child(3)').attr('style', function(i,s){ return s + 'left: 76px !important;'});

        // Changes the desired tail shadow color.
        iwBackground.children(':nth-child(3)').find('div').children().css({'box-shadow': 'none', 'z-index' : '1'});

        // Reference to the div that groups the close button elements.
        var iwCloseBtn = iwOuter.next();

        // Apply the desired effect to the close button
        iwCloseBtn.addClass('close-map-marker');

        // If the content of infowindow not exceed the set maximum height, then the gradient is removed.
        if($('.iw-content').height() < 140){
          $('.iw-bottom-gradient').css({display: 'none'});
        }
        
      });

      google.maps.event.addListener(infowindow,'closeclick',function(){
          $('.location-link').removeClass('active');
      });

      // Open/Close Info Window when click location item
      $('body').on('click', '.location-item', function(event) {
          // Deactive
          if ($(this).hasClass('active')) {
            infowindow.close();
          } else {
            // Active
            map.setZoom(14);
          
            var id = $('.location-item').index($(this));
                map.setCenter(markers[id].getPosition());

            // Info Window
            var content = renderInfoWindow( locations[id][0],locations[id][3],locations[id][4] );
            infowindow.setContent(content);
            infowindow.open(map, markers[id]);
            activeLocation(locations[id][1], locations[id][2]);

            window.setTimeout(function() {
              map.panTo(markers[id].getPosition());
            }, 3000);
          }
          
      });
   
      // When resize window
      $(window).resize(function() {   
        renderMapContainer($('#map'),$('.id-container-map-right'));
        google.maps.event.trigger(map, 'resize'); 
      });
    }      
  }

  /*
  * Add/Remove active class when clicked on location-link
  */
  function activeLocation(latitude, longitude) {
    $('.location-link').removeClass('active');
    $('.location-link').filter(function(index) {
      return ( $(this).data('latitude') == latitude && $(this).data('longitude') == longitude );
    }).addClass('active');
    
  }

  /**
  * Render an customize info windows for google map
  */
  function renderInfoWindow( title, address, open ){
    // InfoWindow content
      var contentString = '<div id="iw-container">' +
        '<div class="iw-title">' + title +'</div>' +
        '<div class="iw-content">' +
          '<div class="iw-row clearfix">' +
            '<div class="iw-desc">'+ address +'</div>' +
          '</div>' +
          '<div class="iw-row clearfix">' +
            '<div class="iw-desc">'+ open +'</div>' +
          '</div>' +          
        '</div>' +  
		'<a href="#" class="location-btn">bekijk beschikbare voertuigen</a>'+
      '</div>';

      return contentString;
  }
  
 	/**
	* Change height dynamic, fixed footer if need
	*/
	function fixedFooter() {
	    $('#master').css('min-height', 0);
	    var heightNeed = 0;
	    // Caculate height, do no included padding
	    heightNeed = $(window).height() - ( $('#header').height() + $('#footer').height() + $('#toolbar').height() );
	   
	    $('#master').css('min-height',heightNeed - 30);
	}

	/**
	* Handle event on checkbox use same location
	*/
	var handleCheckboxChange = function(){
		$('#checkbox_same_location').on('change', function(){
			// checked = true
			if ( $(this).is(":checked") ) {
				
				$('.block-return').removeClass('show');
			} else {
				// Checked = false
				$('.block-return').addClass('show');
			}
		});
	};
	

	/**
	* Init Date Time Picker
	*/
	function initDateTimePicker() {
		$('#depart_date').datetimepicker({
            format: 'ddd D MMM Y'
        });
        $('#depart_time').datetimepicker({
        	format: "HH:mm",
			disabledHours: [0, 1, 2, 3, 4, 5, 6, 7, 18, 19, 20, 21, 22, 23]			
        });     
        $('#depart_time').data("DateTimePicker").stepping(30);  
        $('#arrival_date').datetimepicker({
            format: 'ddd D MMM Y',
            useCurrent: false
        });
        $('#arrival_time').datetimepicker({
        	format: "HH:mm",
			disabledHours: [0, 1, 2, 3, 4, 5, 6, 7, 18, 19, 20, 21, 22, 23]		
        });
        $('#arrival_time').data("DateTimePicker").stepping(30);  
        // Linked 
        $("#depart_date").on("dp.change", function (e) {
            $('#arrival_date').data("DateTimePicker").minDate(e.date);
        });
        $("#arrival_date").on("dp.change", function (e) {
            $('#depart_date').data("DateTimePicker").maxDate(e.date);
        });
	}
	
})(window.jQuery);

;(function($) {
  "use strict";
  
  function InputNumber(element) {
    this.$el = $(element);
    this.$input = this.$el.find('[type=text]');
    this.$inc = this.$el.find('[data-increment]');
    this.$dec = this.$el.find('[data-decrement]');
    this.min = this.$el.attr('min') || false;
    this.max = this.$el.attr('max') || false;
    this.init();
  }
  
  InputNumber.prototype = {
    init: function() {
      this.$dec.on('click', $.proxy(this.decrement, this));
      this.$inc.on('click', $.proxy(this.increment, this));
    },
    
    increment: function(e) {
      var value = this.$input[0].value;
      value++;
      if (!this.max || value <= this.max) {this.$input[0].value = value++;
      }
    },
    
    decrement: function(e) {
      var value = this.$input[0].value;
      value--;
      if (!this.min || value >= this.min) {
        this.$input[0].value = value;
      }
    }
  };

  $.fn.inputNumber = function(option) {
    return this.each(function () {
      var $this = $(this),
          data = $this.data('inputNumber');

      if (!data) {
        $this.data('inputNumber', (data = new InputNumber(this)));
      }
    });
  };

  $.fn.inputNumber.Constructor = InputNumber;
  
  /*31-05-2017*/
  

    $(window).scroll(function () {
        if ($('#toolbar.fixed').length > 0) {
            var hT = 0,
                hH = $('#toolbar.fixed').outerHeight(),
                wS = $(this).scrollTop();
            if (wS > (hT + hH)) {
                $('#toolbar.fixed').show();
            } else {
                $('#toolbar.fixed').hide();
            }
        }
    });
	
})(jQuery);

$(document).ready(function(){
	$('.quantity-container .caret-up').click(function(){
		var elm = $(this).parents('.quantity-container').find('input')
		elm.val(parseInt(elm.val())+1);
	});
	
	$('.quantity-container .caret-down').click(function(){
		var elm = $(this).parents('.quantity-container').find('input')
		if(elm.val()!=1) elm.val(parseInt(elm.val())-1);
	});
	
	$('.location-inner').click(function(){
		$(this).toggleClass('open');
	});

  $('.question-container').click(function(){
    $(this).toggleClass('open');
  });
})
