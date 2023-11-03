$(document).ready(function () { 

    $("#btnSubmit").click(function () {

        isValid = false;

        isValid = requiredTextField("Name", "name")
        if (!isValid) { return false; }
        isValid = requiredTextField("Email", "email")
        if (!isValid) { return false; }
        isValid = requiredTextField("Phone", "mobile number")
        if (!isValid) { return false; }
        isValid = requiredTextField("Salary","Valid Input")
        if (!isValid) { return false; }

        if (isValid) {
            alert("Form Data is valid");
            return true;
        }
    })
})