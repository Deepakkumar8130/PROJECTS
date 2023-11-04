var base_url = "https://localhost:7183/api/";
$(document).ready(function () {
    GetMenuProgram();
})

function GetMenuProgram() {
    let UserId = window.localStorage.getItem("UserId")
    //console.log(typeof UserId)
    //console.log(UserId)
    //alert("check")
    $.ajax({
        "url": base_url + "Program/GetProgramsById",
        "method": "GET",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        "data": {UserId},
        "success": function (response) {
            if (response.ok) {
                //alert("OK")
                //console.log(response);
                var userMenu = ''
               
                response.data.forEach(function (item, index) {
                    userMenu += '<li class="nav-item menu-open">'
                    userMenu += '<a href="'+item.path+'" class="nav-link">'
                    userMenu += '<p>'+item.title+'</p></a>'
                })
                $("#UserMenu").html(userMenu);
                $("#UserName").html('<a href="#" class="d-block">'+window.localStorage.getItem("UserName")+'</a>');

                
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