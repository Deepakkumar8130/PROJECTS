var base_url = "https://localhost:7183/api/";
checkUserLoggedIn();

function checkUserLoggedIn() {
    var auth_user = localStorage.getItem("token");
    if (auth_user == null) {
        window.location.href = "/Account/Login"
    }
}

$(document).ready(function (){
    $("#btnLogout").click(function () {
        localStorage.removeItem("user");
        localStorage.removeItem("token");
        window.location.href = "/Account/Login";
    })
})