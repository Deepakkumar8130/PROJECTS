$(document).ready(function(){
    GetAllUsersWithRole();
    FillDropDownList("RoleManaged/GetActiveRoles", null, "ddlUserRoles")


    //$("#btnUserFormReset").click(function () {
    //    ResetUserForm();
    //})

    $("#btnRoleAssignSubmit").click(function () {
            SubmitData();
    })
})


//function GetAllUsersWithRole() {

//    $.ajax({
//        url: base_url + "UserManaged/GetUsersWithRole",
//        method: "GET",
//        contentType: JSON,
//        headers: {
//            "Authorization": "Bearer " + localStorage.getItem("token")
//        },
//        data: { "UserId": UserLoginInfo.id },
//        "success": function (response) {
//            if (response.ok) {
//                var options = '<option value="">Select</option>'
//                response.data.forEach(function (item, index) {
//                    options += '<option data="'+item.role+'" value='+item.id+'>'+item.userName+'</option>'
//                })
//                $("#ddlUserNames").html(options)
//            }
//        },
//        "error": function (err) {
//            console.log(err)
//        }
//    })
//}

$("#ddlUserNames").change(function (){
    var role = $("#ddlUserNames option:selected").attr("data")
    $("#showRole").html("Existing Role : " + role)
})

/*--- VALIDATION ON CHANGE THE OPTION , OPTION IS VALID OR NOT ---*/
$("#ddlUserNames").change(function () {
    requiredSelectFiled("UserNames", "user name")
})

$("#ddlUserRoles").change(function () {
    requiredSelectFiled("UserRoles", "user role")
})


function SubmitData() {

    /*--- VALIDATION ON SUBMIT THE OPTION , OPTION IS VALID OR NOT ---*/
    isValid = requiredSelectFiled("UserNames", "user name")
    if (!isValid) { return false }

    isValid = requiredSelectFiled("UserRoles", "user role")
    if (!isValid) { return false }


    if (isValid) {
        var model = {
            AdminId: UserLoginInfo.id,
            UserId: $("#ddlUserNames").val(),
            RoleId: $("#ddlUserRoles").val(),
            Status: "1"
        };
        $.ajax({
            url: base_url + "RoleManaged/AssignedRole",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            data: JSON.stringify(model),
            "success": function (response) {
                if (response.ok) {
                    Swal.fire({
                        title: "Good job!",
                        text: response.message,
                        icon: "success",
                        timer: 1500
                    });
                }
                else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: response.message,
                        timer: 2000
                    });
                }
                setTimeout(function () {
                    location.reload()
                }, 1500)
            },
            "error": function (err) {
                console.log(err);
            }
        })

    }
}

//function ResetUserForm() {
//    var form = $("#UserForm")[0];
//    form.reset();
//}