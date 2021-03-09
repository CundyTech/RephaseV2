function InsertNewMenuItemsBefore(insertOnMenuItemId, newItems) {

    var existingItemsArray = JSON.parse($("#js-content-json").val);

    var index = existingItemsArray.findIndex(function (insertOnMenuItemId) {
        return existingItemsArray.Id === insertOnMenuItemId;
    });

    existingItemsArray.splice(index, 0, newItems);
}

function InsertNewMenuItemsAfter(insertedOnMenuItem, newItems) {

    var existingItemsArray = JSON.parse();


}