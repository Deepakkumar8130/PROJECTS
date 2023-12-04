$(document).ready(function(){
    GetAllUsersWithRole();
    FillDropDownList("RoleManaged/GetActiveRoles", null, "ddlUserRoles")


    //$("#btnUserFormReset").click(function () {
    //    ResetUserForm();
    //})

    $("#btnUserSubmit").click(function () {
            SubmitData();
    })
})


function GetAllUsersWithRole() {

    $.ajax({
        url: base_url + "UserManaged/GetUsersWithRole",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": UserLoginInfo.id },
        "success": function (response) {
            if (response.ok) {
                var options = '<option value="">Select</option>'
                response.data.forEach(function (item, index) {
                    options += '<option data="'+item.role+'" value='+item.id+'>'+item.userName+'</option>'
                })
                $("#ddlUserNames").html(options)
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}

$("#ddlUserNames").change(function (){
    var role = $("#ddlUserNames option:selected").attr("data")
    $("#showRole").html("Existing Role : " + role)
})


//function SubmitData() {
//    isValid = requiredSelectFiled("UserName", "user name")
//    if (!isValid) { return false }

//    isValid = requiredSelectFiled("UserRolesUserRoles", "user role")
//    if (!isValid) { return false }


//    if (isValid) {
//        var data = new FormData();
//        var user = {
//            AdminId: UserLoginInfo.id,
//            UserName: $("#ddlUserName").val(),
//            UserName: $("#ddlUserRoles").val()
//        };
//        data.append("User", JSON.stringify(user));
//        //console.log(user);
//        $.ajax({
//            url: base_url + "UserManaged/RegisteredUser",
//            method: "POST",
//            cache: false,
//            contentType: false,
//            processData: false,
//            headers: {
//                "Authorization": "Bearer " + localStorage.getItem("token")
//            },
//            data: data,
//            "success": function (response) {
//                if (response.ok) {
//                    Swal.fire({
//                        title: "Good job!",
//                        text: response.message,
//                        icon: "success",
//                        timer: 1500
//                    });
//                }
//                else {
//                    Swal.fire({
//                        icon: "error",
//                        title: "Oops...",
//                        text: response.message,
//                        timer: 2000
//                    });
//                }
//                setTimeout(function () {
//                    location.reload()
//                }, 1500)
//            },
//            "error": function (err) {
//                console.log(err);
//            }
//        })

//    }
//}

//function ResetUserForm() {
//    var form = $("#UserForm")[0];
//    form.reset();
//}