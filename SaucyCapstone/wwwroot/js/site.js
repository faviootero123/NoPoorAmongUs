// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.onload = function () {

    let theme = window.localStorage.getItem("theme");
    let themeSwitch = document.getElementById("themeSwitch");

    if (theme == "dark") {
        let icon = document.getElementById("themeIcon");
        icon.classList.remove("bi-sun-fill");
        icon.classList.add("bi-moon-fill");
        themeSwitch.checked = true;
    } else {
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

function loadJs(sourceUrl) {
    if (sourceUrl.Length == 0) {
        console.error("Invalid source URL");
        return;
    }

    var tag = document.createElement('script');
    tag.src = sourceUrl;
    tag.type = "text/javascript";

    tag.onload = function () {
        console.log("Script loaded successfully");
    }

    tag.onerror = function () {
        console.error("Failed to load script");
    }

    document.body.appendChild(tag);
}
