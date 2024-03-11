$(document).ready(function () {

    $("#LoginAdminBtn").click(LoginAdmin)
    $("#DataGetByAdmin").click(DataAdmin)

})


function LoginAdmin() {
    var data = {
        email: "user@example.com",
        password:"User@123"
    }
    $.ajax({
        url: "https://localhost:7178/api/Account/login",
        method: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (response) {
            localStorage.setItem("token", response.token);
            console.log(response);
        },
        error: function (err) {
            console.log(err)
        }

    })
}

function DataAdmin() {
    $.ajax({
        url: "https://localhost:7178/api/Data/GetDataByAdmin",
        method: "GET",
        headers: "Bearer " + localStorage.getItem("token"),
        contentType: "application/json; charset=utf-8",
        dataType:"json",
        success: function (response) {
            console.log(response);
        },
        error: function (err) {
            console.log(err)
        }

    })
}