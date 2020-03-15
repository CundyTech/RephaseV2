function RebuildContentTree(item) {

    var contenttreeHtml = $.ajax("/Home/UpdateContentTree")
        .data = item
        .done(function () {
            alert("success");
        })
        .fail(function () {
            alert("error");
        })
        .always(function () {
            alert("complete");
        });

    $("js-content_tree").html(contenttreeHtml);

}

