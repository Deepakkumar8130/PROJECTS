var base_url = "https://localhost:7183/api/";
var rootPath = "https://localhost:7289/";
checkUserLoggedIn();


function checkUserLoggedIn() {
    var auth_user = localStorage.getItem("token");
    if (auth_user == null) {
        window.location.href = "/Account/Login"
    }
}

$(document).ready(function (){
    $("#btnLogout").click(function () {
        localStorage.removeItem("UserId");
        localStorage.removeItem("UserName");
        localStorage.removeItem("UserRole");
        localStorage.removeItem("token");
        localStorage.removeItem("theme");
        window.location.href = "/Account/Login";
    })


    window.onload = function () {
        var currentTheme = localStorage.getItem('theme')
        applyTheme(currentTheme);
    };
})



function applyTheme(newTheme) {
    var icon = document.querySelector('#themeIcon');

    if (newTheme == "light") {
        icon.classList.remove('fa-moon');
        icon.classList.add("fa-sun");
    }
    else {
        icon.classList.remove('fa-sun');
        icon.classList.add("fa-moon");
    }
    // Get all elements with the class 'wrapper' and update their classes
    var elements = document.querySelectorAll('.wrapper');
    elements.forEach(function (element) {
        element.classList.remove('light', 'dark-mode');
        element.classList.add(newTheme);
    });
}