(function () {
    $(function () {
        $("#navModule a").each(function () {
            var a = $(this);
            a.click(function () {
                $("li.active").removeClass("active");
                $(this).parent().addClass("active");
            });
        })
    })

})();