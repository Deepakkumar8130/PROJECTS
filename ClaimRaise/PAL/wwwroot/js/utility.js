
function requiredTextFilled(control, ErrorMessage, validationtype="all") {
    var id = "#txt" + control;
    var err = "#err" + control;
    var formGroup = "#formGroup" + control;
    var txtVal = $(id).val();

    if (txtVal == "" || txtVal == null || txtVal == undefined) {
        $(err).html("please enter " + ErrorMessage).addClass("text-danger");
        $(formGroup).addClass("text-danger");
        return false;
    }
    else {
        if (GetRegx(validationtype).test(txtVal)) {
            $(err).html("").removeClass("text-danger");
            $(formGroup).removeClass("text-danger");
            return true;
        }
        else {
            $(err).html("Please enter valid " + ErrorMessage).addClass("text-danger");
            $(formGroup).addClass("text-danger");
            return false;
        }
    }
}


function GetRegx(type) {
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

