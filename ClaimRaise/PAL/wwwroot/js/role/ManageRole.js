$(document).ready(function(){
    GetAllRoles();

    $("#btnAddRole").click(function () {
        $("#modalAddRole .modal-title").html("Add New Role")
        ResetRoleForm();
        $("#modalAddRole").modal("show")
        $("#ddlStatus").val("1").attr("disabled", true)
    })

    $("#btnRoleSubmit").click(function () {
        if ($("#hdnRoleId").val() == "0") {
            SubmitData();
        }
        else {
            SubmitDataUpdate();
        }
        
    })
})


function GetAllRoles() {

    $.ajax({
        url: base_url + "RoleManaged/GetAllRoles",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": UserLoginInfo.id },
        "success": function (response) {
            //console.log(response.data)
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1;
                return item;
            });
            if (response.ok) {
                $("#tblRoles").DataTable().destroy()
                $("#tblRoles").DataTable({
                    data: response.data,
                    columns: [
                        { data: "serialNo" },
                        { data: "roleName" },
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
    isValid = requiredTextFilled("RoleName", "role name")
    if (!isValid) { return false }

    isValid = requiredSelectFiled("Status", "status")
    if (!isValid) { return false }

    if (isValid) {
        var data = new FormData();
        var role = {
            AdminId: UserLoginInfo.id,
            RoleName: $("#txtRoleName").val(),
            Status: $("#ddlStatus").val()
        };
        data.append("Role", JSON.stringify(role));
        //console.log(role);
        $.ajax({
            url: base_url + "RoleManaged/RegisteredRole",
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
    $("#modalAddRole .modal-title").html("Update Role")
    $("#hdnRoleId").val(id)
    $("#modalAddRole").modal("show")
    $.ajax({
        url: base_url + "RoleManaged/GetRoleById",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "RoleId": id },
        "success": function (response) {
            //console.log(response.data)
            var user = response.data;
            if (response.ok) {
                $("#txtRoleName").val(user.roleName);
                $("#ddlStatus").val(user.status).removeAttr("disabled");
            }
            
        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function SubmitDataUpdate() {
    isValid = requiredTextFilled("RoleName", "role name")
    if (!isValid) { return false }

    isValid = requiredSelectFiled("Status", "status")
    if (!isValid) { return false }

    if (isValid) {
        var data = new FormData();
        var role = {
            AdminId: UserLoginInfo.id,
            Id: $("#hdnRoleId").val(),
            RoleName: $("#txtRoleName").val(),
            Status: $("#ddlStatus").val()
        };
        data.append("Role", JSON.stringify(role));
        //console.log(user);
        $.ajax({
            url: base_url + "RoleManaged/UpdateRole",
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


function ResetRoleForm() {
    var form = $("#RoleForm")[0];
    form.reset();
}