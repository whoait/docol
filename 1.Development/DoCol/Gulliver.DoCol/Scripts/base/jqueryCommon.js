// --------------------------------------------------------------------------------------------------------------------
// Version		: 001
// Designer		: NhanDCT
// Programmer	: NhanDCT
// Date			: 2015/01/01
// Comment		: Create new
// --------------------------------------------------------------------------------------------------------------------

var last_element_id;
var first_element_id;
function tabIndexPopup(last_element_id, first_element_id) {
	$(last_element_id).on('keydown', function (e) {
		if ((e.keyCode || e.which) == 9) {
			$(first_element_id).focus();
			e.preventDefault();
		}
	});
};

$(function () {
	// Function Calendar
	$('.datepick').each(function () {
		$(this).datepicker({
			showOn: "both",
			buttonImage: "/Content/themes/base/images/jquery_trigger_27.png",
			buttonImageOnly: true,
			buttonText: "Select date"
		});
	});
    // Function Curency
    //$("input[type='text']").each(function () {
    //    $(this).number(true, 2);
    //});

    // Changer Request MenuShop & MenuHQShop
    var url_localtion = $(location).attr('href');
    if (url_localtion) {
        // remove logo modal call from modal.js
        var index = url_localtion.lastIndexOf("/");
        var screenId = url_localtion.substr(index + 13, 4);
        if (screenId.toLowerCase() == "head") {
            $('#shop').css('display', 'none');
            $('.f_login').css('width', '33.333333%');
            $('.Cell').css('width','30%');
            // Remove icon
            $('.icon_shop').remove();
            //$('.title-page-section-popup').addClass('title_header');
        }
        else {
            $('#hqshop').css('display', 'none');
        }
    }

    //MENU
    // Remove Titlte
    //$('.title').remove();

    // Find user menu item length
    var usermenuItems = $('.user-menu').find('a');

    // Toggle open the user menu
    $('.sidebar-menu-toggle').click(function (e) {
        e.preventDefault();

        // Toggle Class to signal state change
        $('.user-menu').toggleClass('usermenu-open').slideToggle('fast');

        // If menu is closed apply animation
        if ($('.user-menu').hasClass('usermenu-open')) {
            usermenuItems.addClass('animated fadeIn');
        }
    });

    // 3. LEFT MENU LINKS TOGGLE
    $('.sidebar-menu li a.accordion-toggle').click(function (e) {
        // Any menu item with the accordion class is a dropdown submenu. Thus we prevent default actions
        e.preventDefault();

        // Any menu item with the accordion class is a dropdown submenu. Thus we prevent default actions
        if ($('body').hasClass('sb-l-m') && !$(this).parents('ul.sub-nav').length) {
            return;
        }

        // Any menu item with the accordion class is a dropdown submenu. Thus we prevent default actions
        if (!$(this).parents('ul.sub-nav').length) {
            $('a.accordion-toggle.menu-open').next('ul').slideUp('fast', 'swing', function () {
                $(this).attr('style', '').prev().removeClass('menu-open');
            });
        }
            // Any menu item with the accordion class is a dropdown submenu. Thus we prevent default actions
        else {
            var activeMenu = $(this).next('ul.sub-nav');
            var siblingMenu = $(this).parent().siblings('li').children('a.accordion-toggle.menu-open').next('ul.sub-nav')

            activeMenu.slideUp('fast', 'swing', function () {
                $(this).attr('style', '').prev().removeClass('menu-open');
            });
            siblingMenu.slideUp('fast', 'swing', function () {
                $(this).attr('style', '').prev().removeClass('menu-open');
            });
        }

        // Now we expand targeted menu item, add the ".open-menu" class
        // and remove any left over inline jQuery animation styles
        if (!$(this).hasClass('menu-open')) {
            $(this).next('ul').slideToggle('fast', 'swing', function () {
                $(this).attr('style', '').prev().toggleClass('menu-open');
            });
        }
    });
});