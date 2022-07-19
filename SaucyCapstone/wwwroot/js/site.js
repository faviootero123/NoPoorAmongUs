﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = function () {

    let theme = window.localStorage.getItem("theme");
    let themeSwitch = document.getElementById("themeSwitch");

    if (theme == "dark") {
        document.getElementById("theme").href = "/css/themes/darkly.min.css";
        let icon = document.getElementById("themeIcon");
        icon.classList.remove("bi-sun-fill");
        icon.classList.add("bi-moon-fill");
        themeSwitch.checked = true;
    } else {
        document.getElementById("theme").href = "/css/themes/flatly.min.css";
        let icon = document.getElementById("themeIcon");
        icon.classList.remove("bi-moon-fill");
        icon.classList.add("bi-sun-fill");
    }

    themeSwitch.addEventListener('change', (e) => {
        if (themeSwitch.checked) {
            window.localStorage.setItem("theme", "dark");
            location.reload();
        } else {
            window.localStorage.setItem("theme", "light");
            location.reload();
        }
    });

};

function updateSlider(obj) {
    var slider = "slider " + obj.id;
    document.getElementById(slider).innerHTML = obj.value
}