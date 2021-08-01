// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("hideDoneItemsBtn")
    .addEventListener("change", (event) => {
        var displayStyle = event.currentTarget.checked ? "none" : "block";
        var doneItems = document.getElementsByClassName("is-done");

        for (var i = 0; i < doneItems.length; i++) {
            doneItems[0].style.display = displayStyle;
        }
    });