$(document).ready(function () {
    FillDropDownList("RoleManaged/GetActiveRoles", null, "ddlRoleNames")

    $("#btnGroupRightsSubmit").click(function () {
        SubmitData();
    })
})

$("#ddlRoleNames").change(function () {
    var id = $("#ddlRoleNames").val();
    GetProgramsRightsData(id);
})

function GetProgramsRightsData(id) {
    $.ajax({
        url: base_url + "ProgramRights/GetProgramsRightsByRoleId",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "RoleId": id },
        "success": function (response) {
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1; // Serial numbers start from 1
                return item;
            });
            if (response.ok) {
                $("#tblRoleProgramRights").DataTable().destroy()
                $("#tblRoleProgramRights").DataTable({
                    data: response.data,
                    columns: [
                        { data: "serialNo" },
                        { data: "title" },
                        { data: "descr" },
                        {
                            data: "id", render: function (id, data, row) {
                                var checkBox = "";
                                if (row.isChecked == 1) {
                                    checkBox = "<div class='checkbox-wrapper-26'>"
                                    checkBox += "<input type='checkbox' class='checkRights' checked value='" + id + "' id='_checkbox-" + id + "'>"
                                    checkBox += "<label for='_checkbox-" + id + "'>"
                                    checkBox += "<div class='tick_mark'></div>"
                                    checkBox += "</label></div>"
                                    return checkBox;
                                }
                                else {
                                    checkBox = "<div class='checkbox-wrapper-26'>"
                                    checkBox += "<input type='checkbox' class='checkRights' value='" + id + "' id='_checkbox-" + id + "'>"
                                    checkBox += "<label for='_checkbox-" + id + "'>"
                                    checkBox += "<div class='tick_mark'></div>"
                                    checkBox += "</label></div>"
                                    return checkBox;
                                }
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





/*--- VALIDATION ON CHANGE THE OPTION , OPTION IS VALID OR NOT ---*/
$("#ddlUserNames").change(function () {
    requiredSelectFiled("UserNames", "user name")
})

$("#ddlUserRoles").change(function () {
    requiredSelectFiled("UserRoles", "user role")
})


function SubmitData() {

    /*--- VALIDATION ON SUBMIT THE OPTION , OPTION IS VALID OR NOT ---*/
    isValid = requiredSelectFiled("RoleNames", "role name")
    if (!isValid) { return false }


    if (isValid) {

        var programs = [];
        $.each($("#tblRoleProgramRights .checkRights"), function (i, ele) {
            var id = $(ele).val()
            var isChecked = 0;
            if ($(ele).is(':checked')) {
                isChecked = 1;
            }
            var program = { Id: id, IsChecked: isChecked };
            programs.push(program);
        })
        var data = new FormData();
        data.append("RoleId", $("#ddlRoleNames").val());
        data.append("lstOfPrograms", JSON.stringify(programs));
        console.log(JSON.stringify(programs))


        $.ajax({
            url: base_url + "ProgramRights/AssignGroupProgramsRights",
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

//function ResetUserForm() {
//    var form = $("#UserForm")[0];
//    form.reset();
//}