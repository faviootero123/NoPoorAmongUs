$(document).ready(function () {
    $('#dataTable').dataTable();

    //this should switch a-roo the buttons but doesnt work for some reason lmao
    var ActiveCourse = localStorage.getItem("activecourse");
    var elements = document.getElementsByClassName("custom");

    for (var i = 0, len = elements.length; i < len; i++) {
        if (ActiveCourse == elements[i].innerHTML) {
            elements[i].classList.remove("btn-outline-primary");
            elements[i].classList.add("btn-primary");
        }
    }
});

//saves select course as activecourse cookie
function changeactive(activeCourse) {
    localStorage.setItem("activecourse", activeCourse);
};