$("a[data-post]").click(function (e) {
    e.preventDefault();
    var $this = $(this);
    var message = $this.data("post");
    swal({   
        title: "Are you sure?",   
        text: message,  //Your message here
        type: "warning",   
        showCancelButton: true,   
        confirmButtonColor: "#336699",
        confirmButtonText: "Yes",   
        closeOnConfirm: false 
    }, function(){
        //this gets executed if user hits `Yes`
        $("<form>")
         .attr("method", "post")
         .attr("action", $this.attr("href"))
         .appendTo(document.body)
         .submit();
    });
});
