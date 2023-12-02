var base_url = "https://localhost:7183/api/";
$(document).ready(function () {
    $("#btnLogin").click(function () {
       
        var user = {
            "UserId": $("#txtUserId").val(),
            "Password": $("#txtPassword").val()
        };
        //console.log(user);
      

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
                    var user = JSON.stringify(response.data);
                    localStorage.setItem("User", user);
                    localStorage.setItem("token", response.token);
                    localStorage.setItem("theme", "light");
                    setTimeout(function () {
                        window.location.href = "/Home/Index"
                    }, 500);
                }
                else {
                    $("#msg").html(response.message).css("color", "red");
                }
            },
            "error": function (err) {
                console.log(err);
            }, beforeSend: function () {
                // Show the loader
                $("#loader-wrapper").show();
            },
            complete: function () {
                // Hide the loader
                $("#loader-wrapper").hide();
            }
        })
    })
})