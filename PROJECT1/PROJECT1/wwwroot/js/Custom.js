$(document).ready(function () {
    getCountry();



    $("#ddlCountry").change(function () {
        var cid = $(this).val();
        $("#ddlState").empty();
        $("#ddlCity").empty();
        $("#ddlState").append('<option>Select</option>')
        getState(cid);
    })

    $("#ddlState").change(function () {
        $("#ddlCity").empty();
        var sid = $("#ddlState").val();
        $("#ddlCity").append('<option>Select</option>')
        getCity(sid);
    })

    $("#btnSubmit").click(function () {

        isValid = requiredTextField("Name", "name")
        if (!isValid) { return false; }
        isValid = requiredTextField("Email", "email")
        if (!isValid) { return false; }
        isValid = requiredTextField("Mobile", "mobile number")
   
        if (!isValid) { return false; }

            Add();
    })

    $("#addbtn").click(function () {

        $("#btnSubmit").val("Submit");
        $("#txtId").val("");
        $("#txtName").val("");
        $("#txtEmail").val("");
        $("#txtMobile").val("");
        $("#ddlGender").val("");
        $("#ddlCountry").val("");
        emptyDropDown("ddlState");
        emptyDropDown("ddlCity");
    })

    $("#btnUpload").click(function () {
        AddImage();
    })

});



//function getCountry() {
//    $.ajax({
//        url: '/Employee/Country',
//        type: "GET",
//        async: false,
//        success: function (result) {
//            $.each(result, function (i, data) {
//                $("#ddlCountry").append('<option value=' + data.id + '>' + data.name + '</option>')
//            });
//        }
//    });
//}

//function getState(id) {
//    $.ajax({
//        url: '/Employee/State?id=' + id,
//        type: "GET",
//        async: false,
//        success: function (result) {
//            $.each(result, function (i, data) {
//                $("#ddlState").append('<option value=' + data.id + '>' + data.name + '</option>')
//            });
//        }
//    });
//}

//function getCity(id) {
//    $.ajax({
//        url: '/Employee/City?id=' + id,
//        type: "GET",
//        async: false,
//        success: function (result) {
//            $.each(result, function (i, data) {
//                $("#ddlCity").append('<option value=' + data.id + '>' + data.name + '</option>')
//            });
//        }
//    });
//}

//function Add() {
//    var employee = {
//        "name": $("#txtName").val(),
//        "email": $("#txtEmail").val(),
//        "mobile": $("#txtMobile").val(),
//        "gender": $("#ddlGender").val(),
//        "country_id": $("#ddlCountry").val(),
//        "state_id": $("#ddlState").val(),
//        "city_id": $("#ddlCity").val()
//    }
//    console.log(employee)
//    if ($("#btnSubmit").val() == "Submit") {
//        $.post("/Employee/Create", employee, function (response) {
//            if (response.ok) {
//                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
//                setTimeout(function () {
//                    window.location.reload()
//                }, 5000)
//            }
//            else {
//                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

//            }
//        })
//    }
//    else {
//        employee.Id = $("#txtId").val()
//        $.post("/Employee/Update", employee, function (response) {
//            if (response.ok) {
//                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
//                $("#btnSubmit").val("Submit")
//                setTimeout(function () {
//                    window.location.reload()
//                }, 5000)
//            }
//            else {
//                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")

//            }
//        })
//    }
//}


//function Delete(id) {
//    if (confirm("Are you sure to delete this record ?") == false) {
//        return false
//    }
//    $.get("/Employee/Delete", { "id": id }, function (response) {
//        if (response.ok) {
//            alert(response.message)
//            setTimeout(function () {
//                window.location.reload()
//            }, 1000)
//        }
//        else {
//            alert(response.message)
//        }
//    })
//}



//function Edit(id) {
//    //(id)
//    $("#exampleModal").modal("show")
//    $("#btnSubmit").val("Update")
//    $.get("/Employee/GetEmployee", { "id": id }, function (response) {

//        $("#txtId").val(response.id)
//        $("#txtName").val(response.name)
//        $("#txtEmail").val(response.email)
//        $("#txtMobile").val(response.mobile)
//        $("#ddlGender").val(response.gender)
//        $("#ddlCountry").val(response.country_id)
//        getState(response.country_id)
//        $("#ddlState").val(response.state_id)
//        getCity($("#ddlState").val())
//        $("#ddlCity").val(response.city_id)

//    })

//}


//function UploadProfile(id) {
//    $("#modalUploadProfile").modal("show")
//}


//$("#btnUpload").click(function () {

//    var id = $("#hdnId").val();
//    var file = $("#txtFile").get(0).files[0]

//    if (file != undefined) {

//        var ext = file.name.substr(file.name.indexOf(".") + 1)
//        if (ext == "jpg" || ext == "jpeg" || ext == "png") {
//            //valid case

//            var frmdata = new FormData()
//            frmdata.append("id", id)
//            frmdata.append("file", file)

//            $.ajax({
//                url: "/Employee/UpdateProfileImage",
//                type: "POST",
//                data: frmdata,
//                cache: false,
//                contentType: false,
//                processData: false,
//                success: function (response) {
//                    // console.log(response)
//                    if (response.ok) {
//                        $("#msgUpload").html(response.message).css("color", "green")
//                        setTimeout(function () {
//                            window.location.reload()
//                        }, 4000)
//                    }
//                    else {
//                        $("#msgUpload").html("Error in image upload").css("color", "red")

//                    }
//                }
//            })



//        }
//        else {
//            alert("Please choose image file only!")
//        }
//    }
//    else {
//        alert("Please choose file!")
//    }
//})
