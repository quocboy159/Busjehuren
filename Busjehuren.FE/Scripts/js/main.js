(function ($) {
    var breakpoint_sp = 767;

    // *********** WINDOW RESIZE EVENT *************
    $(window).resize(function () {
        fixedFooter();
    });

    // ************************
    $(document).ready(function () {

        $('[data-toggle="popover"]').popover();
        $('.input-number').inputNumber();

        $('#form').parsley();
        $('#formContact').parsley();
        $('.match-height').matchHeight();

        fixedFooter();

        handleCheckboxChange();
        expendParsley();
        trackChangeOverviewCart();

        // Sections backgrounds

        var pageSection = $(".home-section, .page-section, .small-section, .split-section");
        pageSection.each(function (indx) {

            if ($(this).attr("data-background")) {
                $(this).css("background-image", "url(" + $(this).data("background") + ")");
            }
        });

        // *********** Init Google Map *************



        // *********** EVENTS *************
        $('body').on('click', '.btn-r-open', function (event) {
            handleShowOverview('open', $(this));
            $('.btn-r-close').show();
            $('.btn-r-open').hide();
        });
        $('body').on('click', '.btn-overview-open', function (event) {
            handleShowOverview('open', $(this));
        });
        $('body').on('click', '.btn-overview-close', function (event) {
            handleShowOverview('close', $(this));
        });

        $('body').on('click', '.btn-r-close', function (event) {
            handleShowOverview('close', $(this));
            $('.btn-r-open').show();
            $('.btn-r-close').hide();
        });

        $('body').on('click', '.call-action-handler', function (event) {
            if ($("#frmCamperSearch").valid()) {
                handleShowSearch($(this).data('action'));
            }
        });

        $('body').on('click', '.call-filter-handler', function (event) {
            handleShowFilter();
        });

        // *********** EVENTS ON SP ONLY *************
        if ($(window).width() <= 767) {
            $('body').on('click', '.call-toggle-location', function (event) {
                $(this).parents('.toggle-location-zone').toggleClass('active');
            });
        };

        $('#header.fixed .navbar-header .navbar-toggle').on('click', function () {
            $(this).parents('#header').find('nav.navbar.main-nav-wrap.navbar-static-top').toggleClass('open-menu');
        });

    });

    /**
    * Expand/Collapse Filter on SP
    */
    function handleShowFilter() {
        $('.primary-sidebar').toggleClass('open');
    }

    /**
    * Expand/Collapse Search form on sp
    */
    function handleShowSearch(action) {

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
    function expendParsley() {
        window.Parsley.on('field:success', function () {


        });
    }



    /**
    * OVERVIEW CART :: change position from fixed -> relative
    */
    function trackChangeOverviewCart() {
        var offsetHeight = $(window).height() - $('.section-cart-footer').outerHeight();

        // when footer hit the view + offset ( height of overview )
        var waypoints = $('#footer').waypoint(function (direction) {

            switch (direction) {
                case 'down':
                    // Remove fixed position
                    $('.section-cart-footer').addClass('remove-fixed');
                    break;
                case 'up':
                    // add fixed position
                    $('.section-cart-footer').removeClass('remove-fixed');
                    break;
            }
        }, {
                offset: offsetHeight
            });
    }

    /**
    * Toggle Overview Cart footer
    */
    function handleShowOverview(action, $el) {
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




    /**
	* Change height dynamic, fixed footer if need
	*/
    function fixedFooter() {
        $('#master').css('min-height', 0);
        var heightNeed = 0;
        // Caculate height, do no included padding
        heightNeed = $(window).height() - ($('#header').height() + $('#footer').height() + $('#toolbar').height());

        $('#master').css('min-height', heightNeed - 30);
    }

    /**
	* Handle event on checkbox use same location
	*/
    var handleCheckboxChange = function () {
        $('#checkbox_same_location').on('change', function () {
            // checked = true
            if ($(this).is(":checked")) {

                $('.block-return').removeClass('show');
            } else {
                // Checked = false
                $('.block-return').addClass('show');
            }
        });
    };




})(window.jQuery);

; (function ($) {
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
        init: function () {
            this.$dec.on('click', $.proxy(this.decrement, this));
            this.$inc.on('click', $.proxy(this.increment, this));
        },

        increment: function (e) {
            var value = this.$input[0].value;
            value++;
            if (!this.max || value <= this.max) {
                this.$input[0].value = value++;
            }
        },

        decrement: function (e) {
            var value = this.$input[0].value;
            value--;
            if (!this.min || value >= this.min) {
                this.$input[0].value = value;
            }
        }
    };

    $.fn.inputNumber = function (option) {
        return this.each(function () {
            var $this = $(this),
                data = $this.data('inputNumber');

            if (!data) {
                $this.data('inputNumber', (data = new InputNumber(this)));
            }
        });
    };

    $.fn.inputNumber.Constructor = InputNumber;

    $(window).scroll(function () {

        if ($('#toolbar.fixed').length > 0) {
            var hT = 0,
                hH = $('#toolbar.fixed').outerHeight(),
                wS = $(this).scrollTop();
            if (wS > (hT + hH)) {
                $('.search-header-zone').addClass("searchFixed");
                $('#toolbar.fixed').show();
                $('#header.fixed').show();
            } else {
                $('.search-header-zone').removeClass("searchFixed");
                $('#toolbar.fixed').hide();
                $('#header.fixed').hide();
            }
        }



    });

})(jQuery);


function ShowActivedNavigate(activedNav) {
    activedNav.addClass("active");
}

