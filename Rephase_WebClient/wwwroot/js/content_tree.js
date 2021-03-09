function RebuildContentTree(item) {

    var contentTreeHtml = $.ajax("/Home/UpdateContentTree")
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

    $("js-content_tree").html(contentTreeHtml);
}

// Current ContentTree we are currently viewing items for.
var CurrentTreeItem;

// Attach click events.
$(document).ready(function () {
    $(".parentItems").click(function (event) {
        DisplayItems(this, event);
        CurrentTreeItem = this;
    });
    $(".childItems").click(function (event) {
        DisplayItems(this, event);
        CurrentTreeItem = this;
    });

    $("#ct-upload-btn").click(function (event) {
        UploadContentToCloudStorage(event);
    });

    $("#ct-download-btn").click(function (event) {
        DownloadContentFromCloudStorage(event);
    }); 

    $("#ct-add-btn").click(function (event) {
      
        $('#AddNewItemsModal').removeClass("hidden");
        PopulateInsertDropdown(source);
      
        event.stopPropagation();
    }); 

});

function IncreaseSelectedCount(menuItemId) {

    // Find the item we clicked on in the item display area, in the content tree.
    var treeItem = $(CurrentTreeItem).find(`[data-id='${menuItemId}']`);
    if (treeItem.length === 0) {
        treeItem = $(CurrentTreeItem);
    }

    // Increment item selected counter and make visible if not clicked on already.
    var count = treeItem[0].children[1].children[0].innerHTML;
    if (count === "0") {

        $(treeItem[0].children[1].children[0]).attr("style", "visibility: visible");
        $(treeItem[0].children[1].children[0]).css('visibility', 'visible');
    }
    treeItem[0].children[1].children[0].innerHTML++;
}

function DecreaseSelectedCount(menuItemId) {

    var treeItem = $(CurrentTreeItem).find(`[data-id='${menuItemId}']`);
    if (treeItem.length === 0) {
        treeItem = $(CurrentTreeItem);
    }

    var count = treeItem[0].children[1].children[0].innerHTML;
    if (count === "1") {

        $(treeItem[0].children[1].children[0]).attr("style", "visibility: hidden");
        $(treeItem[0].children[1].children[0]).css('visibility', 'hidden');
    }
    treeItem[0].children[1].children[0].innerHTML--;
}

function UploadContentToCloudStorage(event) {
    var data = $("#js-content-json").val();

    $.ajax({
        type: "POST",
        url: "/Home/UploadContentJson",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (response) {

            DisplaySuccessToast("Saved changes to server");
        },
        failure: function (response) {

            console.log(response.responseText);
        },
        error: function (response) {

            console.log(response.responseText);
        }
    });

    event.stopPropagation();
}

function DownloadContentFromCloudStorage(event) {

    $.ajax({
        type: "GET",
        url: "Home/DownloadContentJson",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#js-content-json").val = response;

            DisplaySuccessToast("Downloaded data from server");
        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });

    event.stopPropagation();
}

function RefreshContentTree() {

    $.ajax({
        type: "GET",
        url: "Home/DownloadContentJson",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#js-content-json").val = response;
        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });

    return false;
}

function UpdateContentTree(queuedItem) {
    var contentItem = $("#js-content_tree").find(`[data-id='${queuedItem.dataset.id}']`);

    // Update data attributes
    contentItem[0].dataset.title = queuedItem.lastElementChild.innerText;
    contentItem[0].dataset.caption = `${queuedItem.lastElementChild.innerText}<${contentItem[0].dataset.caption.split('<')[1]}`;
    contentItem[0].dataset.filepath = queuedItem.firstElementChild.dataset.filepath;
    contentItem[0].dataset.icon = `<img src='${queuedItem.firstElementChild.dataset.filepath}'>`;

    // Update ui elements
    contentItem.children("span.caption").text(queuedItem.lastElementChild.innerText);
    contentItem.children("span.icon")[0].childNodes[0].scr = contentItem[0].dataset.icon;
}