// Site JavaScript functions
$(document).ready(function () {
    // Side navigation toggle functionality
    $(".sideNav").append('<a href="javascript: void(0);" class="toggleMenuLink two"></a>');
    $("body").append('<div class="navOverlay"></div>');

    $(".toggleMenuLink").click(function () {
        $(".navOverlay").toggleClass("active");
        $(".toggleMenuLink").toggleClass("active");
        $(".sideNav").toggleClass("active");
        $("body").toggleClass("navOpen");
    });

    $(".navOverlay").click(function () {
        $(this).removeClass("active");
        $(".toggleMenuLink").removeClass("active");
        $(".sideNav").removeClass("active");
        $("body").removeClass("navOpen");
    });

    // Set active menu item based on current controller
    var currentPath = window.location.pathname;
    $(".sideNav ul li a").each(function () {
        var href = $(this).attr('href');
        if (href && currentPath.toLowerCase().indexOf(href.toLowerCase()) > -1 && href !== '/') {
            $(this).parent().addClass('active');
        }
    });
});
