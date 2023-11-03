function requiredTextField(control, errorMessage) {
    var id = "#txt" + control;
    var err = "#err" + control;
    var txtval = $(id).val();

    if (txtval == "" || txtval == null || txtval == undefined || $(id).val()=="") {
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





function getCountry() {
    $.ajax({
        url: '/Employee/Country',
        type: "GET",
        async: false,
        success: function (result) {
            $.each(result, function (i, data) {
                $("#ddlCountry").append('<option value=' + data.id + '>' + data.name + '</option>')
            });
        }
    });
}

function getState(id) {
    $.ajax({
        url: '/Employee/State?id=' + id,
        type: "GET",
        async: false,
        success: function (result) {
            $.each(result, function (i, data) {
                $("#ddlState").append('<option value=' + data.id + '>' + data.name + '</option>')
            });
        }
    });
}

function getCity(id) {
    $.ajax({
        url: '/Employee/City?id=' + id,
        type: "GET",
        async: false,
        success: function (result) {
            $.each(result, function (i, data) {
                $("#ddlCity").append('<option value=' + data.id + '>' + data.name + '</option>')
            });
        }
    });
}

function Add() {
    var employee = {
        "name": $("#txtName").val(),
        "email": $("#txtEmail").val(),
        "mobile": $("#txtMobile").val(),
        "gender": $("#ddlGender").val(),
        "country_id": $("#ddlCountry").val(),
        "state_id": $("#ddlState").val(),
        "city_id": $("#ddlCity").val()
    }
    console.log(employee)
    if ($("#btnSubmit").val() == "Submit") {
        $.post("/Employee/Create", employee, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                setTimeout(function () {
                    window.location.reload()
                }, 5000)
            }
            else {
                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

            }
        })
    }
    else {
        employee.Id = $("#txtId").val()
        $.post("/Employee/Update", employee, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                $("#btnSubmit").val("Submit")
                setTimeout(function () {
                    window.location.reload()
                }, 5000)
            }
            else {
                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

            }
        })
    }
}


function Delete(id) {
    if (confirm("Are you sure to delete this record ?") == false) {
        return false
    }
    $.get("/Employee/Delete", { "id": id }, function (response) {
        if (response.ok) {
            alert(response.message)
            setTimeout(function () {
                window.location.reload()
            }, 1000)
        }
        else {
            alert(response.message)
        }
    })
}



function Edit(id) {
    //(id)
    $("#exampleModal").modal("show")
    $("#btnSubmit").val("Update")
    $.get("/Employee/GetEmployee", { "id": id }, function (response) {

        $("#txtId").val(response.id)
        $("#txtName").val(response.name)
        $("#txtEmail").val(response.email)
        $("#txtMobile").val(response.mobile)
        $("#ddlGender").val(response.gender)
        $("#ddlCountry").val(response.country_id)
        getState(response.country_id)
        $("#ddlState").val(response.state_id)
        getCity($("#ddlState").val())
        $("#ddlCity").val(response.city_id)

    })

}


function UploadProfile(id) {
    $("#hdnId").val(id);
    $("#modalUploadProfile").modal("show");
}


function AddImage() {

    var id = $("#hdnId").val();
    var file = $("#txtFile").get(0).files[0]

    if (file != undefined) {

        var ext = file.name.substr(file.name.indexOf(".") + 1)
        if (ext == "jpg" || ext == "jpeg" || ext == "png") {

            var frmdata = new FormData()
            frmdata.append("id", id)
            frmdata.append("file", file)

            $.ajax({
                url: "/Employee/UpdateProfileImage",
                type: "POST",
                data: frmdata,
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    // console.log(response)
                    if (response.ok) {
                        $("#msgUpload").html(response.message).css("color", "green")
                        setTimeout(function () {
                            window.location.reload()
                        }, 4000)
                    }
                    else {
                        $("#msgUpload").html("Error in image upload").css("color", "red")

                    }
                }
            })



        }
        else {
            alert("Please choose image file only!")
        }
    }
    else {
        alert("Please choose file!")
    }
}
