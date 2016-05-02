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
        $("#ZoomImageLarge").html("<img src='" + $(this).attr("src") + "' alt='" + $(this).attr("alt") + "' /><br/>" + $(this).attr("alt") + "")
            .center()
            .fadeIn();
    });

    $(document).keydown(function (e) {
        if (e.keyCode == 27) {
            $(".overlay").fadeOut("slow");
            $("#ZoomImageLarge").fadeOut("slow");
        }
    });
    $("body").on("scroll mousewheel touchmove", function (e) {
        if ($(".overlay").css("display") != "none") {
            e.preventDefault();
            e.stopPropagation();
            return false;
        }
        $(".overlay").click(function () {
            $(".overlay").fadeOut("slow");
            $("#ZoomImageLarge").fadeOut("slow");
        });

        $("#ZoomImageLarge").click(function () {
            $(".overlay").fadeOut("slow");
            $("#ZoomImageLarge").fadeOut("slow");
        });
    });
});