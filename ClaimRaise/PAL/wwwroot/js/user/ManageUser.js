$(document).ready(function(){
    GetAllUsers();
    FillDropDownList("UserManaged/GetUserByRole", "Manager", "ddlManager")

    $("#btnAdd").click(function () {
        $("#modalAddUser .modal-title").html("Add New user")
        $("#formGroupPassword").show()
        $("#formGroupConfirmPassword").show()
        ResetUserForm();
        $("#modalAddUser").modal("show")
        $("#ddlStatus").val("1").attr("disabled", true)
    })

    $("#btnUserFormReset").click(function () {
        ResetUserForm();
    })

    $("#btnUserSubmit").click(function () {
        if ($("#hdnUserId").val() == "0") {
            SubmitData();
        }
        else {
            SubmitDataUpdate();
        }
        
    })
})


function GetAllUsers() {

    $.ajax({
        url: base_url + "UserManaged/GetAllUsers",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": localStorage.getItem("UserId") },
        "success": function (response) {
            //console.log(response.data)
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1;
                return item;
            });
            if (response.ok) {
                $("#tblUsers").DataTable().destroy()
                $("#tblUsers").DataTable({
                    data: response.data,
                    columns: [
                        { data: "serialNo" },
                        { data: "userName" },
                        { data: "email" },
                        { data: "mobile" },
                        { data: "managerName" },
                        { data: "status" },
                        {
                            data: "id", width: "70px", class: "text-center", render: function (id) {
                                var btn = `<a class="btn btn-sm btn-info" onclick="Edit('${id}')">Edit</a>`
                                return btn;
                            }
                        }

                   ]
                 })
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function SubmitData() {
    isValid = requiredTextFilled("UserName", "user name")
    if (!isValid) { return false }

    isValid = requiredTextFilled("EmailAddress", "user email")
    if (!isValid) { return false }

    isValid = requiredTextFilled("Password", "password")
    if (!isValid) { return false }

    isValid = requiredTextFilled("ConfirmPassword", "Confirm Password")
    if (!isValid) { return false }

    isValid = comparePassword("Password", "ConfirmPassword")
    if (!isValid) { return false }

    isValid = requiredSelectFiled("Status", "status")
    if (!isValid) { return false }

    if (isValid) {
        var data = new FormData();
        var user = {
            AdminId: window.localStorage.getItem("UserId"),
            UserName: $("#txtUserName").val(),
            Email: $("#txtEmailAddress").val(),
            Mobile: $("#txtMobileNumber").val(),
            Password: $("#txtPassword").val(),
            ManagerId: $("#ddlManager").val(),
            Status: $("#ddlStatus").val()
        };
        data.append("User", JSON.stringify(user));
        //console.log(user);
        $.ajax({
            url: base_url + "UserManaged/RegisteredUser",
            method: "POST",
            cache: false,
            contentType: false,
            processData: false,
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            data: data,
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


function Edit(id) {
    $("#modalAddUser .modal-title").html("Update User")
    $("#formGroupPassword").hide()
    $("#formGroupConfirmPassword").hide()
    $("#hdnUserId").val(id)
    $("#modalAddUser").modal("show")
    FillDropDownList("UserManaged/GetUserByRole", "Manager", "ddlManager")
    $.ajax({
        url: base_url + "UserManaged/GetUserById",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": id },
        "success": function (response) {
            console.log(response.data)
            var user = response.data;
            if (response.ok) {
                $("#txtUserName").val(user.userName);
                $("#txtEmailAddress").val(user.email);
                $("#txtMobileNumber").val(user.mobile);
                $("#ddlStatus").val(user.status).removeAttr("disabled");
                if (user.managerId != "") {
                    $("#ddlManager").val(user.managerId);
                }
                else {
                    FillDropDownList("UserManaged/GetUserByRole", "Manager", "ddlManager");
                }
            }
            
        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function SubmitDataUpdate() {
    isValid = requiredTextFilled("UserName", "user name")
    if (!isValid) { return false }

    isValid = requiredTextFilled("EmailAddress", "user email")
    if (!isValid) { return false }

    isValid = requiredSelectFiled("Status", "status")
    if (!isValid) { return false }

    if (isValid) {
        var data = new FormData();
        var user = {
            AdminId: window.localStorage.getItem("UserId"),
            Id: $("#hdnUserId").val(),
            UserName: $("#txtUserName").val(),
            Email: $("#txtEmailAddress").val(),
            Mobile: $("#txtMobileNumber").val(),
            ManagerId: $("#ddlManager").val(),
            Status: $("#ddlStatus").val()
        };
        data.append("User", JSON.stringify(user));
        //console.log(user);
        $.ajax({
            url: base_url + "UserManaged/UpdateUser",
            method: "POST",
            cache: false,
            contentType: false,
            processData: false,
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            data: data,
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


function ResetUserForm() {
    var form = $("#UserForm")[0];
    form.reset();
}