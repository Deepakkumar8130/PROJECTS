$(document).ready(function(){
    GetAllPrograms();
    //FillDropDownList("UserManaged/GetUserByRole", "Manager", "ddlManager")

    $("#btnAdd").click(function () {
        $("#modalAddPrograms .modal-title").html("Add New Program")
        ResetUserForm();
        $("#modalAddPrograms").modal("show")
        $("#ddlStatus").val("1").attr("disabled", true)
        $("#ddlSequence").val("0").attr("disabled", true)
    })

    $("#btnProgFormReset").click(function () {
        ResetUserForm();
    })

    $("#btnProgSubmit").click(function () {
        if ($("#hdnProgId").val() == "0") {
            SubmitData();
        }
        else {
            SubmitDataUpdate();
        }
        
    })
})


function GetAllPrograms() {

    $.ajax({
        url: base_url + "Program/GetAllPrograms",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        "success": function (response) {
            //console.log(response.data)
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1;
                return item;
            });
            if (response.ok) {
                $("#tblPrograms").DataTable().destroy()
                $("#tblPrograms").DataTable({
                    data: response.data,
                    columns: [
                        { data: "serialNo" },
                        { data: "title" },
                        { data: "path" },
                        { data: "description" },
                        { data: "disp_Sequence" },
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

            var disp_sequence = "<option value="+0+">New Program</option>";
            response.data.forEach(function (item, ind) {
                disp_sequence += "<option value=" + item.disp_Sequence + ">" + item.disp_Sequence + "</option>"
            });
            $("#ddlSequence").html(disp_sequence);

        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function SubmitData() {
    isValid = requiredTextFilled("ProgramTitle", "program name")
    if (!isValid) { return false }

    isValid = requiredTextFilled("Controller", "controller")
    if (!isValid) { return false }

    isValid = requiredTextFilled("Action", "Action")
    if (!isValid) { return false }

    isValid = requiredTextFilled("ProgramDescr", "program description")
    if (!isValid) { return false }

    if (isValid) {
        var data = new FormData();
        var program = {
            AdminId: UserLoginInfo.id,
            Title: $("#txtProgramTitle").val(),
            Path: $("#txtController").val() + "/" + $("#txtAction").val(),
            Description: $("#txtProgramDescr").val(),
            Status: $("#ddlStatus").val()
        };
        data.append("Program", JSON.stringify(program));
        //console.log(user);
        $.ajax({
            url: base_url + "Program/RegisteredProgram",
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
    $("#modalAddPrograms .modal-title").html("Update Program")
    $("#hdnProgId").val(id)
    $("#modalAddPrograms").modal("show")
    FillDropDownList("UserManaged/GetUserByRole", "Manager", "ddlManager")
    $.ajax({
        url: base_url + "Program/GetProgById",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "ProgId": id },
        "success": function (response) {
            //console.log(response.data)
            var prog = response.data;
            var path = prog.path.split("/")
            if (response.ok) {
                $("#txtProgramTitle").val(prog.title);
                $("#txtController").val(path[0]);
                $("#txtAction").val(path[1]);
                $("#txtProgramDescr").val(prog.description);
                $("#ddlStatus").val(prog.status).removeAttr("disabled");
                $("#ddlSequence").val(prog.disp_Sequence).removeAttr("disabled");
            }
            
        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function SubmitDataUpdate() {
    isValid = requiredTextFilled("ProgramTitle", "program name")
    if (!isValid) { return false }

    isValid = requiredTextFilled("Controller", "controller")
    if (!isValid) { return false }

    isValid = requiredTextFilled("Action", "Action")
    if (!isValid) { return false }

    isValid = requiredTextFilled("ProgramDescr", "program description")
    if (!isValid) { return false }

    isValid = requiredSelectFiled("Sequence", "Sequence")
    if (!isValid) { return false }

    isValid = requiredSelectFiled("Status", "status")
    if (!isValid) { return false }

    if (isValid) {
        var data = new FormData();

        var program = {
            AdminId: UserLoginInfo.id,
            Id: $("#hdnProgId").val(),
            Title: $("#txtProgramTitle").val(),
            Path: $("#txtController").val() + "/" + $("#txtAction").val(),
            Description: $("#txtProgramDescr").val(),
            Disp_Sequence: $("#ddlSequence").val(),
            Status: $("#ddlStatus").val()
        };
        data.append("Program", JSON.stringify(program));
        //console.log(user);
        $.ajax({
            url: base_url + "Program/UpdateProgram",
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
    var form = $("#ProgramForm")[0];
    form.reset();
}