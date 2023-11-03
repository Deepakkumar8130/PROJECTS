$(document).ready(function () {

    $("#btnSubmit").click(function () {

        isValid = false;

        isValid = requiredTextField("Name", "name")
        if (!isValid) { return false; }
        isValid = requiredTextField("Email", "email")
        if (!isValid) { return false; }
        isValid = requiredTextField("Password", "password")
        if (!isValid) {return false; }
        isValid = requiredTextField("ConfirmPassword", "password")
        if (!isValid) { return false; }

        return isValid;
    })
})