
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



function getState(id) {
    $.ajax({
        url: "/Customer/GetState",
        type: "GET",
        async: false,
        data: { "id": id },
        success: function (response) {
            var ddl = "<option value=''>Select</option>";
            response.forEach((item, idx) => {
                ddl += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            $("#ddlState").html(ddl);
        }
    })
}


function getCity(id) {
    $.ajax({
        url: "/Customer/GetCity",
        type: "GET",
        async: false,
        data: { "id": id },
        success: function (response) {
            var ddl = "<option value=''>Select</option>";
            response.forEach((item, idx) => {
                ddl += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            $("#ddlCity").html(ddl);
        }
    })
}


function getCountry() {
    $.ajax({
        url: "/Customer/GetCountry",
        type: "GET",
        async: false,
        success: function (response) {
            var ddl = "<option value=''>Select</option>";
            response.forEach((item, idx) => {
                ddl += "<option value=" + item.id + ">" + item.name + "</option>";
            })
            $("#ddlCountry").html(ddl);
        }
    })
}


function getCustomers() {
    $.get("/Customer/GetCustomers", function (response) {
        console.log(response)
        $("#tbl_Customer").DataTable({
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
                { "data": "mobile" },
                { "data": "gender" },
                { "data": "country" },
                { "data": "state" },
                { "data": "city" },
                {
                    "data": "id", class: "text-center", render: function (id) {
                        var link1 = "<a onclick='EditRecord(" + id + ")'><i class='fa fa-edit'></i></a>"
                        var link2 = "<a onclick='DeleteRecord(" + id + ")'><i class='fa fa-trash'></i></a>"
                        return link1 + "&nbsp;" +link2
                    }
                },
                {
                    "data": "id", class: "text-center", render: function (id) {
                        var link = "<a onclick='UploadProfile(" + id + ")'><i class='fa fa-cloud'></i></a>"
                        return link
                    }
                }
            ]
        })

    })
}

function AddCustomer() {
    var customer = {
        "name": $("#txtName").val(),
        "email": $("#txtEmail").val(),
        "mobile": $("#txtMobile").val(),
        "gender": $("#ddlGender").val(),
        "country": $("#ddlCountry").val(),
        "state": $("#ddlState").val(),
        "city": $("#ddlCity").val()
    }

    if ($("#txtId").val() == "") {
        $.post("/Customer/CreateRecord", customer, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                setTimeout(function () {
                    window.location.reload()
                }, 2000)
            }
            else {
                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
            }
        })
    }
    else {
        customer.id = $("#txtId").val();
        $.post("/Customer/UpdateRecord", customer, function (response) {
            if (response.ok) {
                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
                setTimeout(function () {
                    window.location.reload()
                }, 2000)
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
    $.get("Customer/GetCustomer", { "id": id }, function (response) {
        console.log(response);
        $("#txtId").val(response.id);
        $("#txtName").val(response.name);
        $("#txtEmail").val(response.email);
        $("#txtMobile").val(response.mobile);
        $("#ddlGender").val(response.gender);
        $("#ddlCountry").val(response.country);
        getState(response.country);
        $("#ddlState").val(response.state);
        getCity(response.state);
        $("#ddlCity").val(response.city);
    })
}

function DeleteRecord(id) {
    if (confirm("Are You Sure") == false) {
        return false;
    }
    $.post("/Customer/DeleteRecord", { "id": id }, function (response) {
        if (response.ok) {
            $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
            setTimeout(function () {
                window.location.reload()
            }, 2000)
        }
        else {
            $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
        }
    })
}

function UploadProfile(id) {
    $("#hdnId").val(id);
    $("#modalUploadProfile").modal("show");
}


function AddImage() {
    var id = $("#hdnId").val();
    var file = $("#txtFile").get(0).files[0];

    if (file != undefined) {

        var ext = file.name.substr(file.name.indexOf(".") + 1);
        if (ext == "jpg" || ext == "jpeg" || ext == "png") {

            var fmdata = new FormData();
            fmdata.append("id", id);
            fmdata.append("file", file);

            $.ajax({
                url: "Customer/UpdateProfileImage",
                type: "POST",
                data: fmdata,
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.ok) {
                        $("#msgUpload").html(response.message).removeClass("text-danger").addClass("text-success");
                        setTimeout(function () {
                            window.location.reload()
                        }, 3000)
                    }
                    else {
                        $("#msgUpload").html("Error in Image Upload ").removeClass("text-success").addClass("text-danger")
                    }
                }
            })
        }
        else {
            $("#msgUpload").html("Please choose correct format for image upload like jpg , jpeg, png").removeClass("text-success").addClass("text-danger")
        }
    }
    else {
        alert("Please Choose File!")
    }
}