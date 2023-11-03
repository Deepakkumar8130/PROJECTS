var base_url = "https://localhost:7080/api/"

$(document).ready(function () {

    var user = {
        "UserId": $("#txtEmail").val(),
        "Password": $("#txtPassword").val()
    }
    $.ajax({
        "url": base_url + "Account/Login",
        "method": "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        "data": JSON.stringify(user),
        "success": function (response) {
            if (response.ok) {
                $("#msg").html(response.message).css("color", "green")
                localStorage.setItem("token", response.token)
                var user = response.data;
                delete user.Password
                localStorage.setItem("user", JSON.stringify(user))
                setTimeout(function () {
                    window.location.href = "/Home/Index"
                }, 3000)
            }
            else {
                $("#msg").html(response.message).css("color", "red")
            }
        },
        "error": function (err) {
            console.log(err);
        }
    })
});