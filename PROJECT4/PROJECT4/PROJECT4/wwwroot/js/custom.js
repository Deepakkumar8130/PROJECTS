$(document).ready(function () {
    getCustomers();
    getCountry();

    $("#ddlCountry").change(function () {
        emptyDropDown("ddlState");
        emptyDropDown("ddlCity");
        getState($("#ddlCountry").val());

    })

    $("#ddlState").change(function () {
        emptyDropDown("ddlCity");
        getCity($("#ddlState").val());

    })

    $("#btnSubmit").click(function () {
        isValid = requiredTextField("Name", "name")
        if (!isValid) { return false; }
        isValid = requiredTextField("Email", "email")
        if (!isValid) { return false; }
        isValid = requiredTextField("Mobile", "mobile number")
        AddCustomer();
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
   
})

//function emptyDropDown(id) {
//    $("#"+id).html("<option value=''>Select</option>")
//}



//function getState(id) {
//    $.ajax({
//        url: "/Customer/GetState",
//        type: "GET",
//        async: false,
//        data: {"id":id},
//        success: function (response) {
//            var ddl = "<option value=''>Select</option>";
//            response.forEach((item, idx) => {
//                ddl += "<option value=" + item.id + ">" + item.name + "</option>";
//            })
//            $("#ddlState").html(ddl);
//        }
//    })
//}


//function getCity(id) {
//    $.ajax({
//        url: "/Customer/GetCity",
//        type: "GET",
//        async: false,
//        data: {"id":id},
//        success: function (response) {
//            var ddl = "<option value=''>Select</option>";
//            response.forEach((item, idx) => {
//                ddl += "<option value=" + item.id + ">" + item.name + "</option>";
//            })
//            $("#ddlCity").html(ddl);
//        }
//    })
//}


//function getCountry() {
//    $.ajax({
//        url: "/Customer/GetCountry",
//        type: "GET",
//        async: false,
//        success: function (response) {
//            var ddl = "<option value=''>Select</option>";
//            response.forEach((item, idx) => {
//                ddl += "<option value=" + item.id + ">" + item.name + "</option>";
//            })
//            $("#ddlCountry").html(ddl);
//        }
//    })
//}


//function getCustomers() {
//    $.get("/Customer/GetCustomers", function (response) {
//        console.log(response)
//        $("#tbl_Customer").DataTable({
//            data: response,
//            columns: [
//                {"data": "id"},
//                {"data": "name"},
//                { "data": "email"},
//                { "data": "mobile"},
//                { "data": "gender"},
//                { "data": "country"},
//                { "data": "state"},
//                { "data": "city" },
//                {
//                    "data": "id", class:"text-center", render: function (id) {
//                        var link = "<a onclick='EditRecord(" + id + ")'><i class='fa fa-edit'></i></a>"
//                        link += "<a onclick='DeleteRecord(" + id + ")'><i class='fa fa-trash'></i></a>"
//                        return link
//                    }
//                }
//            ]
//        })

//    })
//}

//function AddCustomer() {
//    var customer = {
//        "name": $("#txtName").val(),
//        "email": $("#txtEmail").val(),
//        "mobile": $("#txtMobile").val(),
//        "gender": $("#ddlGender").val(),
//        "country": $("#ddlCountry").val(),
//        "state": $("#ddlState").val(),
//        "city": $("#ddlCity").val()
//    }

//    if ($("#txtId").val() == "") {
//        $.post("/Customer/CreateRecord", customer, function (response) {
//            if (response.ok) {
//                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
//                setTimeout(function () {
//                    window.location.reload()
//                }, 2000)
//            }
//            else {
//                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
//            }
//        })
//    }
//    else {
//        customer.id = $("#txtId").val();
//        $.post("/Customer/UpdateRecord", customer, function (response) {
//            if (response.ok) {
//                $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
//                setTimeout(function () {
//                    window.location.reload()
//                }, 2000)
//            }
//            else {
//                $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
//            }
//        })
//    }
//}

//function EditRecord(id) {
//    $("#exampleModal").modal("show");
//    $("#btnSubmit").val("Update");
//    $.get("Customer/GetCustomer", {"id":id}, function (response) {
//        console.log(response);
//        $("#txtId").val(response.id);
//            $("#txtName").val(response.name);
//            $("#txtEmail").val(response.email);
//        $("#txtMobile").val(response.mobile);
//            $("#ddlGender").val(response.gender);
//            $("#ddlCountry").val(response.country);
//            getState(response.country);
//        $("#ddlState").val(response.state);
//            getCity(response.state);
//        $("#ddlCity").val(response.city);
//    })
//}

//function DeleteRecord(id) {
//    if (confirm("Are You Sure") == false) {
//        return false;
//    }
//    $.post("/Customer/DeleteRecord", { "id": id }, function (response) {
//        if (response.ok) {
//            $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
//            setTimeout(function () {
//                window.location.reload()
//            }, 2000)
//        }
//        else {
//            $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
//        }
//    })
//}