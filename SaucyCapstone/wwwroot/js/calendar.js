function setTitle(student) {
    document.getElementsByClassName('rz-scheduler-nav-title')[0].setAttribute('style', 'display: block; text-align: center;')
    document.getElementsByClassName('rz-scheduler-nav-title')[0].innerHTML = student;
}

function reloadCss(print) {
    let link1 = document.getElementById("radzenTheme");
    let link2 = document.getElementById("theme");
    if (print) {
        link1.href = "/css/themes/calendar-print.css";
        link2.href = "/css/themes/flatly.min.css";
    } else {
        if (window.localStorage.getItem("theme") === "light") {
            link1.href = "/css/themes/default-base.css";
            link2.href = "/css/themes/flatly.min.css";
        }
        else if (window.localStorage.getItem("theme") === "dark") {
            link1.href = "/css/themes/dark-base.css";
            link2.href = "/css/themes/darkly.min.css";
        }
    }
}

function saveAsPdf(student) {
    //reloadCss(true);
    let element = document.getElementById('calendar');

    let opt = {
        margin: [.3, .1, .1, .1],
        filename: student + '_schedule.pdf',
        image: { type: 'jpeg', quality: 1 },
        html2canvas: { scale: 1 },
        jsPDF: { unit: 'in', format: 'A4', orientation: 'landscape' }
    };

    html2pdf(element, opt);
}
