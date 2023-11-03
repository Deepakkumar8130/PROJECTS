var base_url = "https://localhost:7183/api/";
$(document).ready(function () {

    $("#btnLogin").click(function () {
       
        var user = {
            "UserId": $("#txtUserId").val(),
            "Password": $("#txtPassword").val()
        };
        console.log(user);
      

        $.ajax({
            "url": base_url + "Account/Login",
            "method": "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            "data": JSON.stringify(user),
            "success": function (response) {
                if (response.ok) {
                    $("#msg").html(response.message).css("color", "green");
                    //debugger
                    var user = response.data;
                    localStorage.setItem("UserId", user.id)
                    //delete user.password;
                    localStorage.setItem("token", response.token);
                   
                    setTimeout(function () {
                        window.location.href = "/Home/Index"
                    }, 1000);
                }
                else {
                    $("#msg").html(response.message).css("color", "red");
                }
            },
            "error": function (err) {
                console.log(err);
            }
        })
    })
})