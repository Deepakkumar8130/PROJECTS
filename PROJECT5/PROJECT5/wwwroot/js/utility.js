
function requiredTextField(control, errorMessage) {
    var id = "#txt" + control;
    var err = "#err" + control;
    var txtval = $(id).val();

    if (txtval == "" || txtval == null || txtval == undefined || $(id).val() == "") {
        $(err).html("please enter a " + errorMessage).addClass("text-danger");
        return false;

    }
    else {
        $(err).html("");
        return true;

    }

}




function Validate(control, errorMessage) {
    var id = "#txt" + control;
    var err = "#err" + control;
    var txtval = $(id).val();

    if (txtval == "" || txtval == null || txtval == undefined) {
        $(err).html("please enter a " + errorMessage).addClass("text-danger");
        return false;

    }
    else {
        $(err).html("");
        if (GetRegex(control).test(txtval)) {

            return true;
        }
        else {
            $(err).html("please enter correct " + errorMessage).addClass("text-danger");
            return false;
        }
    }
}

function GetRegex(type) {
    var regx = /.+/;
    switch (type) {
        case "Email":
            regx = /^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$/
            break;
        case "Mobile":
        case "Phone":
            regx = /^(\+\d{1,3}[- ]?)?\d{10}$/
            break;
        case "Salary":
            regx = /[0-9][1-9.]*[0-9]+[1-9]*/
            break;
        case "Password":
            regx = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
        case "ConfirmPassword":
            regx = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
    }
    return regx;
}




function emptyDropDown(id) {
    $("#" + id).html("<option value=''>Select</option>")
}



function getStudents() {
    $.get("/Student/GetStudents", function (response) {
        console.log(response);
        $("#tbl_Student").DataTable({
            data: response,
            dom: 'Bfrtip',
            buttons: [
                'excel',
                'csv',
                'pdf'
            ],
            columns: [
                { "data": "id" },
                { "data": "name" },
                { "data": "email" },
                { "data": "phone" },
                { "data": "gender" },
                { "data": "course" },
                {
                    "data": "id", class: "text-center", render: function (id) {
                        var link1 = "<a onclick='EditRecord(" + id + ")'><i class='fa fa-edit'></i></a>"
                        var link2 = "<a onclick='DeleteRecord(" + id + ")'><i class='fa fa-trash'></i></a>"
                        return link1 + "&nbsp;" + link2
                    }
                }
            ]
        })

    })
}


function Add() {
    var student = {
        "name": $("#txtName").val(),
        "email": $("#txtEmail").val(),
        "phone": $("#txtPhone").val(),
        "gender": $("#ddlGender").val(),
        "course": $("#txtCourse").val(),
    }
    console.log(student)
    if ($("#btnSubmit").val() == "Submit") {
        $.post("/Student/Create", student, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                setTimeout(function () {
                    window.location.reload()
                }, 500)
            }
            else {
                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

            }
        })
    }
    else {
        student.Id = $("#txtId").val()
        $.post("/Student/Update", student, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                $("#btnSubmit").val("Submit")
                setTimeout(function () {
                    window.location.reload()
                }, 500)
            }
            else {
                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

            }
        })
    }
}

function EditRecord(id) {
    $("#exampleModal").modal("show");
    $("#btnSubmit").val("Update");
    $.get("Student/GetStudent", { "id": id }, function (response) {
        console.log(response);
        $("#txtId").val(response.id);
        $("#txtName").val(response.name);
        $("#txtEmail").val(response.email);
        $("#txtPhone").val(response.phone);
        $("#ddlGender").val(response.gender);
        $("#txtCourse").val(response.course);
    })
}

function DeleteRecord(id) {
    if (confirm("Are You Sure") == false) {
        return false;
    }
    $.post("/Student/Delete", { "id": id }, function (response) {
        if (response.ok) {
            $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
            setTimeout(function () {
                window.location.reload()
            }, 500)
        }
        else {
            $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
        }
    })
}
