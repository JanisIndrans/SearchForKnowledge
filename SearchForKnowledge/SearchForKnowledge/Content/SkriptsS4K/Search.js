$(document).ready(function() {
    $("#searchForString a").click(function (e) {
        e.preventDefault();
        var $this = $(this);

        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href") + "?page=1" + "&searchString=" + $("input").val())
            .submit();
    });
    $("#searchForString input").keypress(function (e) {
        if(e.keyCode == 13) {
            e.preventDefault();
            var $this = $("#searchForString a");

            $("<form>")
                .attr("method", "post")
                .attr("action", $this.attr("href") + "?page=1" + "&searchString=" + $("input").val())
                .submit();
        }
        
    });
});