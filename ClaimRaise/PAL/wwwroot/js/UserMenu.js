var base_url = "https://localhost:7183/api/";
$(document).ready(function () {
    GetMenuProgram();
})

function GetMenuProgram() {
    let UserId = UserLoginInfo.id
    //console.log(typeof UserId)
    //console.log(UserId)
    //alert("check")
    $.ajax({
        "url": base_url + "Program/GetProgramsById",
        "method": "GET",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        "data": {UserId},
        "success": function (response) {
            if (response.ok) {
                //alert("OK")
                //console.log(response);
                var userMenu = ''
               
                response.data.forEach(function (item, index) {
                    userMenu += '<li class="nav-item menu-open">'
                    userMenu += '<a href="' +rootPath+item.path+'" class="nav-link">'
                    userMenu += '<p>'+item.title+'</p></a>'
                })
                $("#UserMenu").html(userMenu);
                $("#UserRole").text(UserLoginInfo.role);
                $("#UserName").html('<a href="#" class="d-block">' + UserLoginInfo .userName+'</a>');

                
            }
            else {
                $("#msg").html(response.message).css("color", "red");
            }
        },
        "error": function (err) {
            console.log(err);
        }
    })
}