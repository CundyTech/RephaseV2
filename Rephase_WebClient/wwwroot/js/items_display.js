
var preventPropagation = false;


function DisplayItems(element, event) {

    ShowItems();

    // Check we action the event we originally clicked on and not a propagated event.
    if (event.target === event.currentTarget) {

        // Build Parent item first.
        $("#parent")[0].children[0].children[0].src = element.firstElementChild.firstElementChild.src;
        $("#parent-text")[0].innerText = element.dataset.title;
        $("#parent")[0].children[0].children[0].dataset.filepath = element.dataset.filepath;
        $("#parent")[0].children[0].dataset.id = element.dataset.id;

        // Add children items
        if (element.children.length > 2) {
            DisplayChildItems(element.children[2].children, event);
        } else {
            RemoveChildItems();
        }
    }

    $(".js-clickable").off("click");
    $(".js-clickable").on("click", function (event) {
        var item = $(this);
        item.toggleClass("selected");

        if (item[0].classList.contains("selected")) {
            IncreaseSelectedCount(item[0].dataset.id);
            EnqueueInEditor(this);
        } else {
            DecreaseSelectedCount(item[0].dataset.id);
            DequeueInEditor(this);
        }

        event.stopPropagation();
    });
}

function DisplayChildItems(element, event) {

    var liArray = [];

    for (var i = 0; i < element.length; i++) {

        var listItem = jQuery("<li />")
            .addClass(
                "js-clickable list-group-item list-group-item-action list-group-item-secondary flex-item img-container")
            .attr("data-id", element[i].dataset.id);
        var count = element[i].children[1].children[0].innerHTML;
        if (count === "1") {
            listItem.addClass("selected");
        }

        $('<img />',
            {
            }).appendTo(listItem)
            .attr("src", element[i].children[0].children[0].src)
            .attr("height", 40)
            .attr("width", 50)
            .addClass("marg-right-5")
            .attr("data-filepath", element[i].dataset.filepath);

        $('<p />',
            {
            }).appendTo(listItem)
            .attr("id", "child-text")
            .text(element[i].dataset.title)
            .addClass("wordwrap-break pad-0 marg-0 font-size-small");

        liArray.push(listItem);
    }

    RemoveChildItems();

    // Add children.
    var childrenPlaceHolder = document.getElementById("Children");
    for (var j = 0; j < liArray.length; j++) {

        var t = liArray[j][0];
        childrenPlaceHolder.appendChild(t);
    }
}

function RemoveChildItems() {
    var childrenPlaceHolder = document.getElementById("Children");

    // Clear existing children first.
    while (childrenPlaceHolder.firstChild) {

        childrenPlaceHolder.removeChild(childrenPlaceHolder.firstChild);
    }
}

function ShowItems() {
    $(".js-itemTitle").removeClass("d-none");
    $("#parent").removeClass("d-none");
}
