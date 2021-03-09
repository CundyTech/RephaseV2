var queuedItems = [];
var queuePos = 0;

$(document).ready(function () {
    $("#js-editor-save").off("click");
    $("#js-editor-save").on("click", function () {
        SaveEditorChanges();
    });

    $("#js-editor-last").off("click");
    $("#js-editor-last").on("click", function () {
        UpdateCurrentMenuItem(queuedItems[queuePos]);
        LoadLastItem();
    });

    $("#js-editor-first").off("click");
    $("#js-editor-first").on("click", function () {
        UpdateCurrentMenuItem(queuedItems[queuePos]);
        LoadFirstItem();
    });

    $("#js-editor-next").off("click");
    $("#js-editor-next").on("click", function () {
        UpdateCurrentMenuItem(queuedItems[queuePos]);
        LoadNextItem();
    });

    $("#js-editor-previous").off("click");
    $("#js-editor-previous").on("click", function () {
        UpdateCurrentMenuItem(queuedItems[queuePos]);
        LoadPreviousItem();
    });
});

function EnqueueInEditor(item) {

    queuedItems.unshift(item);
    LoadUI(item);
}

function DequeueInEditor(item) {

    queuedItems.shift(item);

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

var updates = Array();

function SaveEditorChanges() {

    // Update current queued item.
    UpdateCurrentMenuItem(queuedItems[queuePos]);

    // Get Menu Items Json.
    var json = _.unescape($("#js-content-json").val());
    var menuItems = JSON.parse(_.unescape(json));

    updates = [];

    //// Iterate over master menu items and look for matching items loaded in editor.
    for (let i = 0; i < queuedItems.length; i++) {

        FindElementById(queuedItems[i], menuItems);
    }

    // Update content tree UI element.
    for (let j = 0; j < queuedItems.length; j++) {
        UpdateContentTree(queuedItems[j]);
    }

    // Update master list held in hidden field on page.
    if (updates.length) {
        UpdateMasterMenuItems(updates);

        json = JSON.stringify(menuItems);
        $("#js-content-json").val(_.escape(json));
    }

    DisplaySuccessToast("Saved local changes");
}

function FindElementById(queuedItem, menuItems) {

    for (let menuItem of menuItems) {
        if (menuItem.Id === queuedItem.dataset.id) {

            const update = {
                menuItem: menuItem,
                queuedItem: queuedItem
            }

            updates.push(update);

        } else {
            if (menuItem.Child.length !== 0) {
                FindElementById(queuedItem, menuItem.Child);
                continue;
            } else {
                continue;
            }
        }
    }
}

function UpdateMasterMenuItems(updates) {

    for (var i = 0; i < updates.length; i++) {

        updates[i].menuItem.Title = updates[i].queuedItem.lastElementChild.innerText;
        updates[i].menuItem.ImageUrl = updates[i].queuedItem.firstElementChild.src;
        updates[i].menuItem.LocalImagePath = updates[i].queuedItem.firstElementChild.dataset.filepath;
    }
}

function UpdateCurrentMenuItem(queuedItem) {

    queuedItem.firstElementChild.src = $("#js-image").attr("src");
    queuedItem.firstElementChild.dataset.filepath = $("#js-image-filepath")[0].previousSibling.innerText;
    queuedItem.lastElementChild.innerText = $("#js-title").val();
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
