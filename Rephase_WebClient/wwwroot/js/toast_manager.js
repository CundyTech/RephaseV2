
/**
 * Display a success toast message on top of screen.
 * @param {any} message message to be displayed.
 */
function DisplaySuccessToast(message) {
    var options = {
        showTop: true,
        timeout: 3000
    }
    Metro.toast.create(message, null, 5000, "Success", options);
}