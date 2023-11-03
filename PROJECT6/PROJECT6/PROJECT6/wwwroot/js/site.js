// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function empty(id) {
    $("#" + id).html("<option value=''>Select</option>")
}



function GetCountry() {
    $.ajax(
        {
            url: "/Employee/GetCountry",
            type: "GET",
            async: false,
            success: function (response) {
                console.log(response)
                var ddl = "<option value=''>Select</option>"
                response.forEach((item, index) => {
                    ddl += "<option value=" + item.id + ">" + item.name + "</option>"

                })
                $('#ddlCountry').html(ddl)
            }
        }
    )
}

function GetState(id) {
    $.ajax({
        url: "/Employee/GetState",
        async: false,
        data: { "id": id },
        success: function (response) {
            var ddl = "<option value=''>Select</option>"

            response.forEach((item, index) => {
                ddl += "<option value=" + item.id + ">" + item.name + "</option>"
            })
            $("#ddlState").html(ddl)
        }
    })
}


function GetCity(id) {
    $.ajax({
        url: "/Employee/GetCity",
        async: false,
        data: { "id": id },
        success: function (response) {
            var ddl = "<option value=''>Select</option>"
            response.forEach((item, index) => {
                ddl += "<option value=" + item.id + ">" + item.name + "</option>"
            })
            $("#ddlCity").html(ddl)
        }
    })
}



function GetEmployees() {
    $.get("/Employee/GetEmployees", function (response) {
        console.log(response)
        $("#tbl_data").DataTable({
            data: response,
            columns: [
                {
                    "data": "id"
                },
                {
                    "data": "profileImg", class: "text-center", "render": function (image) {
                        var im = "<img src='" + image + "' style='max-height:100px; max-width:100px ;border-radius:50%'/>"
                        return im;
                    }
                },
                { "data": "name" },
                { "data": "gender" },
                { "data": "dateOfBirth" },
                { "data": "email" },
                { "data": "mobile" },
                { "data": "address" },
                { "data": "country" },
                { "data": "state" },
                { "data": "city" },
                {
                    "data": "id", class: "text-center", render: function (id) {
                        var link = "<div class='mt-2'><a onclick='EditRecord(" + id + ")'><i class='fa fa-edit'></i></a></div>"
                        var link2 = "<div class='mt-2'><a onclick='DeleteRecord(" + id + ")'><i class='fa fa-trash'></i></a></div>"
                        return link+link2;
                    }
                }
            ]
        })

    })
}


function Add(fileName) {
    var selectedGender = $("input[name='gender']:checked").val();
    var employee = {
        "name": $("#txtName").val(),
        "profileimg": fileName,
        "email": $("#txtEmail").val(),
        "mobile": $("#txtMobile").val(),
        "dateofbirth": $("#DOB").val(),
        "gender": selectedGender,
        "address": $("#txtAddress").val(),
        "country_id": $("#ddlCountry").val(),
        "state_id": $("#ddlState").val(),
        "city_id": $("#ddlCity").val()
    }
    console.log(employee)
    if ($("#btnSubmit").val() == "Submit") {
        $.post("/Employee/Create", employee, function (response) {
            if (response.ok) {
                Swal.fire(
                    'Good job!',
                    response.message
                )
                setTimeout(function () {
                    window.location.reload()
                }, 2000)
            }
            else {
                Swal.fire(
                    'Oops!!',
                    response.message
                )
            }
        })
    }
    else {
        employee.Id = $("#txtId").val()
        debugger
        $.post("/Employee/Update", employee, function (response) {
            if (response.ok) {
                Swal.fire(
                    'Good job!',
                    response.message
                )
                $("#btnSubmit").val("Submit")
                setTimeout(function () {
                    window.location.reload()
                }, 3000)
            }
            else {
                Swal.fire(
                    'Oops!!',
                    response.message
                )
            }
        })
    }
}


function AddImage() {
    var file = $("#ChooseImg").get(0).files[0]
    console.log(file);
    debugger
    if (file != null) {

        var frmdata = new FormData()
        frmdata.append("file", file)

        $.ajax({
            url: "/Employee/UploadProfileImage",
            type: "POST",
            data: frmdata,
            cache: false,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.ok) {

                    Add(response.name);
                }
                else {
                    Swal.fire(
                        'Oops! Oops! Oops!',
                        response.message
                    )
                }
            }
        })
    }
    else {
        var imageElement = document.getElementById("PrevImg");
        image = imageElement.src;
        profileimg = image.substring(image.lastIndexOf('/') + 1).toLowerCase();
        console.log(profileimg)
        Add(profileimg)
    }
}

function EditRecord(id) {
    alert("OK")
    $("#exampleModal").modal("show")
    $("#btnSubmit").val("Update")
    $.get("/Employee/GetEmployee", { "id": id }, function (response) {
        console.log(response)
        $("#txtId").val(response.id);
        $("#txtName").val(response.name);
        $("input[name='gender'][value='" + response.gender + "']").prop("checked", true);
        $("#DOB").val(response.dateOfBirth);

        $("#txtEmail").val(response.email);
        $("#txtMobile").val(response.mobile);
        $("#txtAddress").val(response.address);
        $("#PrevImg").attr("src", "/Images/" + response.profileImg);

        $("#ddlCountry").val(response.country_Id)
        GetState(response.country_Id)
        $("#ddlState").val(response.state_Id)
        GetCity($("#ddlState").val())
        $("#ddlCity").val(response.city_Id)
    })
}

function DeleteRecord(id) {
    if (confirm("Are you sure to delete this record ?") == false) {
        return false
    }
    $.get("/Employee/DeleteRecord", { "id": id }, function (response) {
        if (response.ok) {
            Swal.fire(
                'Good job!',
                'Successfully deleted!!',
                'success'
            )
            setTimeout(function () {
                window.location.reload()
            }, 1000)
        }
    })
}