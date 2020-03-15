var queuedItems = [];
var queuePos = 0;

$(document).ready(function () {
    $("#js-editor-save").off("click");
    $("#js-editor-save").on("click", function () {
        SaveEditorChanges();
    });

    $("#js-editor-last").off("click");
    $("#js-editor-last").on("click", function () {
        LoadLastItem();
    });

    $("#js-editor-first").off("click");
    $("#js-editor-first").on("click", function () {
        LoadFirstItem();
    });

    $("#js-editor-next").off("click");
    $("#js-editor-next").on("click", function () {
        LoadNextItem();
    });

    $("#js-editor-previous").off("click");
    $("#js-editor-previous").on("click", function () {
        LoadPreviousItem();
    });
});

function EnqueueInEditor(item) {

    queuedItems.push(item);
    LoadUI(item);
}

function DequeueInEditor(item) {

    queuedItems.pop(item);

    if (queuePos === queuedItems.length) {
        // If removing last item, load previous item
        LoadPreviousItem();
        UnloadUI(queuedItems[queuedItems.length - 1]);

    } else if (queuePos === 0) {
        // If removing first item, load next item
        LoadNextItem();
        UnloadUI(queuedItems[0]);
    } else {
        LoadNextItem();
        UnloadUI(queuedItems[queuePos]);
    }
}

function LoadLastItem() {
    if (queuePos !== queuedItems.length - 1) {
        queuePos = queuedItems.length - 1;
        LoadUI(queuedItems[queuePos]);
    }
}

function LoadFirstItem() {
    if (queuePos !== 0) {
        queuePos = 0;
        LoadUI(queuedItems[queuePos]);
    }
}

function LoadNextItem() {
    if (queuePos !== queuedItems.length - 1) {
        queuePos++;
        LoadUI(queuedItems[queuePos]);
    }
}

function LoadPreviousItem() {
    if (queuePos !== 0) {
        queuePos--;
        LoadUI(queuedItems[queuePos]);
    }
}

function SaveEditorChanges() {

    // Get Menu Items Json.
    var json = $("#js-content-json").val();
    var menuItems = JSON.parse(_.unescape(json));

    // Iterate over items and look for matching items loaded in editor.
    for (var i = 0; i < queuedItems.length; i++) {
        var queuedItem = queuedItems[i];
    }
}

function LoadUI(item) {
    $("#js-id").val(item.dataset.id);
    $("#js-title").val(item.lastElementChild.innerText);
    $("#js-editor-count").text(`${queuePos + 1} of ${queuedItems.length}`);
    $("#js-image").attr("src", item.firstElementChild.src);
    $("#js-image-filepath")[0].previousSibling.innerText = item.firstElementChild.dataset.filepath;
}

function UnloadUI(item) {
    if (queuedItems.length === 0) {
        $("#js-image").attr("src", 'images/placeholder-image.png');
        $("#js-image-filepath")[0].previousSibling.innerText = "";
        $("#js-title").val("");
        $("#js-id").val("");
    } else {
        $("#js-image").attr("src", item.firstElementChild.src);
        $("#js-image-filepath")[0].previousSibling.innerText = item.firstElementChild.dataset.filepath;
        $("#js-title").val(item.lastElementChild.innerText);
        $("#js-id").val(item.dataset.id);
    }
    
    if (queuedItems.length === 0) {
        $("#js-editor-count").text(`${0} of ${queuedItems.length}`);
    }
}

