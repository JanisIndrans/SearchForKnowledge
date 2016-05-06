jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px");
    this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px");
    return this;
}

$(document).ready(function () {
    $("#Zoom img").click(function (e) {
        e.preventDefault();
            $(".overlay").css({"opacity": "0.7"})
            .fadeIn("slow");
        $("#ZoomImageLarge").html("<a href='#'> <img src='content/images/X.png' style='width:20px; height:20px;' class='pull-right'/> </a> <img src='" + $(this).attr("src") + "' />")
            .center()
            .fadeIn();
        $("#ZoomImageLarge a").click(function () {
            $(".overlay").fadeOut("slow");
            $("#ZoomImageLarge").fadeOut("slow");
        });
    });

    $(document).keydown(function (e) {
        if (e.keyCode == 27) {
            $(".overlay").fadeOut("slow");
            $("#ZoomImageLarge").fadeOut("slow");
        }
    });

    $(".overlay").click(function () {
        $(".overlay").fadeOut("slow");
        $("#ZoomImageLarge").fadeOut("slow");
    });



    $("body").on("scroll mousewheel touchmove", function (e) {
        if ($(".overlay").css("display") != "none") {
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
    });
});