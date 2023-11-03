$(document).ready(function () {
    getStudents();

    $("#btnSubmit").click(function () {
        isValid = requiredTextField("Name", "name")
        if (!isValid) { return false; }
        isValid = requiredTextField("Email", "email")
        if (!isValid) { return false; }
        isValid = requiredTextField("Phone", "mobile number")
        Add();
    })

    $("#addbtn").click(function () {

        $("#btnSubmit").val("Submit");
        $("#txtId").val("");
        $("#txtName").val("");
        $("#txtEmail").val("");
        $("#txtPhone").val("");
        $("#ddlGender").val("");
        $("#txtCourse").val("");
    })
})

