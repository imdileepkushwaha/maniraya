/*
 Project Name :Alteco Corporate & Business Template
 Author Name : Hash Theme
 Author Website : https://www.templatemonster.com/authors/hashtheme/
 Version    : 1.0
 */
 
/*=============================================
Table Of Contents
================================================
1.Preloader JS
1.Video Slider
2.Video sync two slider
3.Testimonial Slider
4.Full screen Height
5.Pretty Photo
6.Count Down
7.Progress Bar
8.Home slider
9.Branch slider
11.Increment Decrement
12. Single Product Zoom
13.Gallery Function
14.Menu Scroll Function On mobile
15. slide sub menu On mObile
16.Animation
17.Toggle Function Mobile
18.WOW js


 Table Of Contents end
 ================================================
 */
$(document).ready(function() {
    "use strict";

        /* PRELOADER JS */
        $(window).on('load', function () {
            function fadeOut(el) {
                el.style.opacity = 0.4;
                var last;
                var tick = function () {
                    el.style.opacity = +el.style.opacity - (new Date() - last) / 600;
                    last = +new Date();
                    if (+el.style.opacity > 0) {
                        (window.requestAnimationFrame && requestAnimationFrame(tick)) || setTimeout(tick, 100);
                    } else {
                        el.style.display = "none";
                    }
                };
                tick();
            }
            var pagePreloaderId = document.getElementById("page-preloader");
            setTimeout(function () {
                fadeOut(pagePreloaderId)
            }, 1000);
        });




    //================================================
    //Video Slider
    //=================================================
    var slider = new MasterSlider();
    slider.setup('video', {
        width: 670,
        height: 500,
        space: 0,
        loop: true,
        view: 'wave'
    });

    slider.control('arrows');
    //================================================
    //Testimonial Slider
    //=================================================
    var slider = new MasterSlider();
    slider.setup('testimonial-slider', {
        loop: true,
        width: 200,
        height: 200,
        speed: 20,
        view: 'stf',
        preload: 'all',
        space: 40,
        wheel: true
    });
    slider.control('arrows');
    slider.control('slideinfo', { insertTo: '#staff-info' });
    //======================================================
    //Video sync two slider
    //=====================================================

    var sync1 = $("#sync1");
    var sync2 = $("#sync2");

    sync1.owlCarousel({
        items: 1,
        itemsDesktop: [1199, 1],
        itemsDesktopSmall: [979, 1],
        itemsTablet: [768, 1],
        itemsMobile: [479, 1],
        itemsMobileSmall: [320, 1],
        slideSpeed: 1000,
        navigation: true,
        transitionStyle: "fadeUp",
        pagination: false,
        navigationText: ['<i class="fa fa-caret-left"></i>', '<i class="fa fa-caret-right"></i>'],
        responsive: true,
        afterAction: syncPosition,
        responsiveRefreshRate: 200,
        autoPlay: true

    });

    sync2.owlCarousel({
        items: 3,
        itemsDesktop: [1199, 3],
        itemsDesktopSmall: [979, 3],
        itemsTablet: [768, 3],
        itemsMobile: [479, 1],
        itemsMobileSmall: [320, 1],
        pagination: false,
        responsiveRefreshRate: 100,
        afterInit: function(el) {
            el.find(".owl-item").eq(0).addClass("synced");
        }
    });

    function syncPosition(el) {
        var current = this.currentItem;
        $("#sync2")
            .find(".owl-item")
            .removeClass("synced")
            .eq(current)
            .addClass("synced")
        if ($("#sync2").data("owlCarousel") !== undefined) {
            center(current)
        }
    }

    $("#sync2").on("click", ".owl-item", function(e) {
        e.preventDefault();
        var number = $(this).data("owlItem");
        sync1.trigger("owl.goTo", number);
    });

    function center(number) {
        var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
        var num = number;
        var found = false;
        for (var i in sync2visible) {
            if (num === sync2visible[i]) {
                var found = true;
            }
        }

        if (found === false) {
            if (num > sync2visible[sync2visible.length - 1]) {
                sync2.trigger("owl.goTo", num - sync2visible.length + 2)
            } else {
                if (num - 1 === -1) {
                    num = 0;
                }
                sync2.trigger("owl.goTo", num);
            }
        } else if (num === sync2visible[sync2visible.length - 1]) {
            sync2.trigger("owl.goTo", sync2visible[1])
        } else if (num === sync2visible[0]) {
            sync2.trigger("owl.goTo", num - 1)
        }

    }
    //============================================
    //404 height
    //=============================================
    $(function() {

        $("#error-page,#coming-soon").css({
            'height': window.innerHeight
        });

    });
    //================================================
    //Pretty Photo
    //=================================================
    $("a[data-gal^='prettyPhoto']").prettyPhoto({
        hook: 'data-gal',
        social_tools: false,
        animation_speed: 'fast',
        slideshow: 10000,
        hideflash: true,
        theme: "light_square"
    });
    //================================================
    // count dowm
    //================================================
    jQuery(function($) {
        $('#countdown').countdown('2021/06/29', function(event) {
            $(this).html(event.strftime('<div class="days count-down"><div class="inner"><span class="number">%-D</span><span class="string">%!D:Day,Days;</span></div></div> <div class="hours count-down"><div class="inner"><span class="number">%H</span><span class="string">%!H:Hour,Hours;</span></div> </div><div class="minutes count-down"><div class="inner"><span class="number">%M</span><span class="string">%!M:Minute,Minutes;</span></div> </div><div class="seconds count-down"><div class="inner"><span class="number">%S</span><span class="string">%!S:Second,Seconds;</span></div> </div>'));
        });
    });

    //================================================
    //Accordion
    //=================================================
    var selectIds = $('#panel1,#panel2,#panel3');
    $(function($) {
        selectIds.on('show.bs.collapse hidden.bs.collapse', function() {
            $(this).prev().find('.fa').toggleClass('fa-plus fa-minus');
        })
    });
    //========================================
    // count function
    //======================================
    $('.count').each(function() {
        jQuery(this).appear(function() {
            var $this = $(this);
            jQuery({ Counter: 0 }).animate({ Counter: $this.text() }, {
                duration: 1000,
                easing: 'swing',
                step: function() {
                    $this.text(Math.ceil(this.Counter));
                }
            });
        });
    });
    //================================================
    //Progress Bar
    //=================================================
    jQuery('.skill').each(function() {
        jQuery(this).appear(function() {
            jQuery(this).find('.skill-box').animate({
                width: jQuery(this).attr('data-percent')
            }, 1000);
        });
    });

        /* 4. SECTIONS BACKGROUNDS */

        var pageSection = $("section,div");
        pageSection.each(function(indx) {

            if ($(this).attr("data-background")) {
                $(this).css("background-image", "url(" + $(this).data("background") + ")");
            }
        });


    //================================================
    //Home slider
    //=================================================

        $('.home-slides').owlCarousel({
            items : 1,
            itemsDesktop : [1199,1],
            itemsDesktopSmall : [980,1],
            itemsTablet: [768,1],
            itemsTabletSmall: false,
            itemsMobile : [479,1],
            singleItem : false,
            slideSpeed : 200,
            paginationSpeed : 800,
            rewindSpeed : 1000,
            autoPlay : false,
            stopOnHover : false,
            navigation: true,
            transitionStyle : "fade",
            navigationText: ['<i class="fa fa-caret-left"></i>', '<i class="fa fa-caret-right"></i>'],
            rewindNav : true,
            scrollPerPage : false,
            responsive: true,
            responsiveRefreshRate : 200,
            responsiveBaseWidth: window,
        });
    
        var dot = $('.home-slides .owl-page');
        dot.each(function() {
            var index = $(this).index() + 1;
          if(index < 10){
            $(this).html('0').append(index);
          }else{
             $(this).html(index);
          }
        });

    //================================================
    //Branches slider
    //=================================================
    $('#branch').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        navigation: true,
        navigationText: ['<i class="fa fa-caret-left"></i>', '<i class="fa fa-caret-right"></i>'],
        pagination: false,
        autoPlay: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 3
            },
            1000: {
                items: 5
            }
        }
    });
    
    //================================================
    //Increment Decrement
    //=================================================
    $("input[name='demo_vertical']").TouchSpin({
        verticalbuttons: true
    });
    //================================================
    //Single Product Zoom
    //=================================================
    $('.zoom').elevateZoom({
        zoomType: "inner",
        cursor: "crosshair",
        zoomWindowFadeIn: 500,
        zoomWindowFadeOut: 750
    });
    //================================================
    //Gallery
    //=================================================
    $(function() {
        $('.categories a').click(function(e) {
            $('.categories ul li').removeClass('active');
            $(this).parent('li').addClass('active');
            var itemSelected = $(this).attr('data-filter');
            $('.portfolio-item').each(function() {
                if (itemSelected == '*') {
                    $(this).removeClass('filtered').removeClass('selected');
                    return;
                } else if ($(this).is(itemSelected)) {
                    $(this).removeClass('filtered').addClass('selected');
                } else {
                    $(this).removeClass('selected').addClass('filtered');
                }
            });
        });
    });
    //=========================================
    //Scroll function
    //=========================================
    $(function() {
        if (window.innerWidth < 767) {
            $("#menu_nav ul:first-child").css({
                'overflow-x': 'hidden',
                'overflow-y': 'scroll',
                'height': window.innerHeight
            });
        }
    });
    //============================================
    // slide sub menu
    //============================================
    //mobile menu

    if (window.innerWidth < 767) {

        $("#slide-nav #menu_nav ul li.dropdown").append('<div class="more"><i class="fa fa-plus"></i></div>');

        $("#slide-nav #menu_nav").on("click", ".more", function(e) {
            e.stopPropagation();

            $(this).parent().toggleClass("current")
                .children("#slide-nav #menu_nav ul li.dropdown > ul").toggleClass("open");

            $(this).html($(this).html() == '<i class="fa fa-plus"></i>' ? '<i class="fa fa-minus"></i>' : '<i class="fa fa-plus"></i>');
        });

        $("#slide-nav #menu_nav").on("click", "a", function(e) {
            if (($(this).attr('href') === "#") || ($(this).attr('href') === "")) {
                $(this).parent().children(".more").trigger("click");
            } else {
                offcanvas_close();
            }
        });

    }
    //=================================================
    //Animation
    //===============================================
    $(function() {
        var $elems = $('.animate-in');
        var winheight = $(window).height();
        var fullheight = $(document).height();

        $(window).scroll(function() {
            animate_elems();
        });

        function animate_elems() {
            var wintop = $(window).scrollTop(); // calculate distance from top of window
            // loop through each item to check when it animates
            $elems.each(function() {
                var $elm = $(this);

                var topcoords = $elm.offset().top; // element's distance from top of page in pixels

                if (wintop > (topcoords - (winheight * .99999))) {
                    // animate when top of the window is 3/4 above the element
                    $elm.addClass('animated');

                }

            });
        } // end animate_elems()
    });
    //================================================
    //Toggle Navigation function
    //=================================================
    //stick in the fixed 100% height behind the navbar but don't wrap it
    $('#slide-nav.navbar .container').append($('<div id="navbar-height-col"></div>'));
    // Enter your ids or classes
    var toggler = '.navbar-toggle';
    var pagewrapper = 'section:first-child';
    var navigationwrapper = '.navbar-header';
    var menuwidth = '100%'; // the menu inside the slide menu itself
    var slidewidth = '80%';
    var menuneg = '-100%';
    var slideneg = '-80%';


    $("#slide-nav").on("click", toggler, function(e) {

        var selected = $(this).hasClass('slide-active');

        $('#menu_nav').stop().animate({
            right: selected ? menuneg : '0px'
        });

        $('#navbar-height-col').stop().animate({
            right: selected ? slideneg : '0px'
        });

        $(pagewrapper).stop().animate({
            right: selected ? '0px' : slidewidth
        });

        $(navigationwrapper).stop().animate({
            right: selected ? '0px' : slidewidth
        });


        $(this).toggleClass('slide-active', !selected);
        $('#menu_nav').toggleClass('slide-active');


        $('#section:first-child, .navbar, body, .navbar-header').toggleClass('slide-active');


    });


    var selected = '#menu_nav, section:first-child, body, .navbar, .navbar-header';


    $(window).on("resize", function() {

        if ($(window).width() > 767 && $('.navbar-toggle').is(':hidden')) {
            $(selected).removeClass('slide-active');
        }

    });


    /*  WOW JS */
    new WOW().init()

    
});
