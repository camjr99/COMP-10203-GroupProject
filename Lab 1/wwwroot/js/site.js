// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var button = document.getElementById('listButton'); // Assumes element with id='button'

button.onclick = function () {
    var box = document.getElementById("playersList");
    if (box.style.display === "none") {
        box.style.display = "block";
    } else {
        box.style.display = "none";
    }
};