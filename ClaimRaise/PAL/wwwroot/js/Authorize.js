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
        window.location.href = "/Account/Login";
    })
})