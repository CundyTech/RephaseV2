var files = [];
var dropZone;

var content =
    "<div data-role=\"wizard\"" +
    "data-width=\"700px\"" +
    "data-button-mode=\"square\"" +
    "data-button-outline=\"false\"" +
    "data-start=\"1\"" +
    "data-finish=\"2\"" +
    "data-icon-help=\"<span>Help</span>\"" +
    "data-icon-prev=\"<span class='mif-arrow-left'></span>\"" +
    "data-icon-next=\"<span class='mif-arrow-right'></span>\" " +
    "data-icon-finish=\"images/checkmark.png\">" +
    "<section><div class=\"page-content\"><div>Page 1</div>" +
    "<form id=\"file-upload\" enctype=\"multipart/form-data\" method=\"post\">" +
    "<dl><dt><label asp-for=\"FileUpload.FormFile\"></label></dt><dd>" +
    "<input asp-for=\"FileUpload.FormFile\" type=\"file\" multiple>" +
    "<span asp-validation-for=\"FileUpload.FormFile\"></span>" +
    "</dd></dl>" +
    "</form></div></section>" +
    "<section><div class=\"page-content\"><div>Page 2</div>" +
    "<table id=\"item-preview\" class=\"tg\">" +
    "<thead><tr><th class=\"tg-ul38\">Preview</th><th class=\"tg-ul38\">File Name</th><th class=\"tg-ul38\">Display Name</th></tr></thead> " +
    "<tbody><tr>" +
    "<td class=\"tg-0lax\"></td>" +
    "<td class=\"tg-0lax\"></td>" +
    "<td class=\"tg-0lax\"></td>" +
    "</tr>" +
    "<tr>" +
    "<td class=\"tg-0lax\"></td>" +
    "<td class=\"tg-0lax\"></td>" +
    "<td class=\"tg-0lax\"></td>" +
    "</tr>" +
    "</tbody>" +
    "</table ></div></section> " +
    "<section><div class=\"page-content\"><div>Page 3</div> <input asp-page-handler=\"Upload\" class=\"btn\" type=\"submit\" value=\"Upload\"/></div></section></div>";

function ShowDialog() {
    Metro.dialog.create({
        title: "Add New Item.",
        content: content,
        actions: [
            {
                caption: "Cancel",
                cls: "js-dialog-close",
                onclick: function () {
                }
            }
        ],
        width: '600px'
    });

    $('#file-upload').change(function () {

        var form = $('#file-upload')[0];
        var files = form[0].files;

        for (var i = 0; i < files.length; i++) {
            $("#item-preview").append(`<tr><td><img id="${files[i].name}" alt="your image" width="50" height="50"/></td><td>${files[i].name}</td><td></td></tr>`);
            document.getElementById(`${files[i].name}`).src = window.URL.createObjectURL(files[i]);
        }
       
    });



}