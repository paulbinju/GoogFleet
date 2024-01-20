"use strict";
$(document).ready(function() {
    var $window = $(window);
    //add id to main menu for mobile menu start
    var getBody = $("body");
    var bodyClass = getBody[0].className;
    $(".main-menu").attr('id', bodyClass);
    //add id to main menu for mobile menu end

    // card js start
    $(".card-header-right .close-card").on('click', function() {
        var $this = $(this);
        $this.parents('.card').animate({
            'opacity': '0',
            '-webkit-transform': 'scale3d(.3, .3, .3)',
            'transform': 'scale3d(.3, .3, .3)'
        });

        setTimeout(function() {
            $this.parents('.card').remove();
        }, 800);
    });
    $(".card-header-right .reload-card").on('click', function() {
        var $this = $(this);
        $this.parents('.card').addClass("card-load");
        $this.parents('.card').append('<div class="card-loader"><i class="icofont icofont-refresh rotate-refresh"></div>');
        setTimeout(function() {
            $this.parents('.card').children(".card-loader").remove();
            $this.parents('.card').removeClass("card-load");
        }, 3000);
    });
    $(".card-header-right .card-option .icofont-simple-left").on('click', function() {
        var $this = $(this);
        if ($this.hasClass('icofont-simple-right')) {
            $this.parents('.card-option').animate({
                'width': '35px',
            });
        } else {
            $this.parents('.card-option').animate({
                'width': '180px',
            });
        }
        $(this).toggleClass("icofont-simple-right").fadeIn('slow');
    });

    $(".card-header-right .minimize-card").on('click', function() {
        var $this = $(this);
        var port = $($this.parents('.card'));
        var card = $(port).children('.card-block').slideToggle();
        $(this).toggleClass("icofont-plus").fadeIn('slow');
    });
    $(".card-header-right .full-card").on('click', function() {
        var $this = $(this);
        var port = $($this.parents('.card'));
        port.toggleClass("full-card");
        $(this).toggleClass("icofont-resize");
    });

    $(".card-header-right .icofont-spinner-alt-5").on('mouseenter mouseleave', function() {
        $(this).toggleClass("rotate-refresh").fadeIn('slow');
    });
    $("#more-details").on('click', function() {
        $(".more-details").slideToggle(500);
    });
    $(".mobile-options").on('click', function() {
        $(".navbar-container .nav-right").slideToggle('slow');
    });
    $(".main-search").on('click', function() {
        $("#morphsearch").addClass('open');
    });
    $(".morphsearch-close").on('click', function() {
        $("#morphsearch").removeClass('open');
    });
    // card js end
    $.mCustomScrollbar.defaults.axis = "yx";
    $("#styleSelector .style-cont").slimScroll({
        setTop: "10px",
        height:"calc(100vh - 515px)",
    });
    $(".main-menu").mCustomScrollbar({
        setTop: "10px",
        setHeight: "calc(100% - 80px)",
    });
    /*chatbar js start*/
    /*chat box scroll*/
    var a = $(window).height() - 80;
    $(".main-friend-list").slimScroll({
        height: a,
        allowPageScroll: false,
        wheelStep: 5,
        color: '#1b8bf9'
    });

    // search
    $("#search-friends").on("keyup", function() {
        var g = $(this).val().toLowerCase();
        $(".userlist-box .media-body .chat-header").each(function() {
            var s = $(this).text().toLowerCase();
            $(this).closest('.userlist-box')[s.indexOf(g) !== -1 ? 'show' : 'hide']();
        });
    });

    // open chat box
    $('.displayChatbox').on('click', function() {
        var my_val = $('.pcoded').attr('vertical-placement');
        if (my_val == 'right') {
            var options = {
                direction: 'left'
            };
        } else {
            var options = {
                direction: 'right'
            };
        }
        $('.showChat').toggle('slide', options, 500);
    });


    //open friend chat
    $('.userlist-box').on('click', function() {
        var my_val = $('.pcoded').attr('vertical-placement');
        if (my_val == 'right') {
            var options = {
                direction: 'left'
            };
        } else {
            var options = {
                direction: 'right'
            };
        }
        $('.showChat_inner').toggle('slide', options, 500);
    });
    //back to main chatbar
    $('.back_chatBox').on('click', function() {
        var my_val = $('.pcoded').attr('vertical-placement');
        if (my_val == 'right') {
            var options = {
                direction: 'left'
            };
        } else {
            var options = {
                direction: 'right'
            };
        }
        $('.showChat_inner').toggle('slide', options, 500);
        $('.showChat').css('display', 'block');
    });
    // /*chatbar js end*/

    //Language chage dropdown start
    i18next.use(window.i18nextXHRBackend).init({
                debug: !1,
                fallbackLng: !1,
                backend: {
                    loadPath: "assets/locales/{{lng}}/{{ns}}.json"
                },
                returnObjects: !0
            },
            function(err, t) {
                jqueryI18next.init(i18next, $)
            }),
        $(".lng-dropdown a").on("click", function() {

            var $this = $(this),
                selected_lng = $this.data("lng");
            i18next.changeLanguage(selected_lng, function(err, t) {
                    $(".main-menu").localize()
                }),
                $this.parent("li").siblings("li").children("a").removeClass("active"), $this.addClass("active"), $(".lng-dropdown a").removeClass("active");
            var drop_lng = $('.lng-dropdown a[data-lng="' + selected_lng + '"]').addClass("active");
            $(".lng-dropdown #dropdown-active-item").html(drop_lng.html())
        });

    //Language chage dropdown end
});
$(document).ready(function() {
    $(function() {
        $('[data-toggle="tooltip"]').tooltip()
    })
    $('.theme-loader').fadeOut('slow', function() {
        $(this).remove();
    });
});

// toggle full screen
function toggleFullScreen() {
    var a = $(window).height() - 10;

    if (!document.fullscreenElement && // alternative standard method
        !document.mozFullScreenElement && !document.webkitFullscreenElement) { // current working methods
        if (document.documentElement.requestFullscreen) {
            document.documentElement.requestFullscreen();
        } else if (document.documentElement.mozRequestFullScreen) {
            document.documentElement.mozRequestFullScreen();
        } else if (document.documentElement.webkitRequestFullscreen) {
            document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
        }
    } else {
        if (document.cancelFullScreen) {
            document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen();
        }
    }
}

//light box
$(document).on('click', '[data-toggle="lightbox"]', function(event) {
    event.preventDefault();
    $(this).ekkoLightbox();
});
/* --------------------------------------------------------
        Color picker - demo only
        --------------------------------------------------------   */
$('#styleSelector').append('' +
    '<div class="selector-toggle">' +
    '<a href="javascript:void(0)"></a>' +
    '</div>' +
    '<ul>' +
    '<div><br><h4>Add Vehicle</h4>' +
    '<div class="form-group"><div class="col-md-12"><b>Vehicle Group </b>' +
    '<span class="pull-right" style="font-size:9px;">( If not listed below <a style="color:red;font-size:12px;" data-toggle="modal" href="#VehicleGroupModal">Add Vehicle Group</a> )</span>' +
    '<br /><select class="form-control" name="VehicleGroupID" id="VehicleGroupID"></select></div>' +
    '<div class="form-group"><div class="col-md-12"><b>Vehicle Type</b>' +
    '<span class="pull-right" style="font-size:9px;">( If not listed below <a style="color:red;font-size:12px;"  data-toggle="modal" href="#VehicleTypeModal">Add Vehicle Type</a> )</span><br />' +
    '<select class="form-control" name="VehicleTypeID" id="VehicleTypeID"></select></div></div><div class="form-group"><div class="col-md-12">' +
    '<b>Auto Brand</b><span class="pull-right" style="font-size:9px;">( If not listed below <a style="color:red;font-size:12px;"  data-toggle="modal" href="#AutoBrandModal">Add Auto Brand</a> )</span><br />' +
    '<select id="AutoBrandID" class="form-control" name="AutoBrandID"></select></div></div><div class="form-group"><div class="col-md-12">' +
    '<b>Registration No</b><br /><input type="text" name="RegistrationNo" class="form-control" /></div></div><div class="form-group"><div class="col-md-12">' +
    '<b>Year of Manufacture </b><br /><input type="date" name="YearofManufacture" class="form-control" /></div></div><div class="col-md-12">' +
    '<b>GPS URL </b><br /><input type="text" name="GPSURL" class="form-control" /></div></div><div class="form-group">' +
    '<div class="col-md-12"><b>Note</b><br /><textarea name="Note" class="form-control"></textarea></div></div><div class="form-group"><div class="col-md-12">' +
    '<input  class="btn btn-danger"  type="submit" value="Save" /></div></div></div></div>' +
    '<li>' +
    '</li>' +
    '</ul>' +
    '');
