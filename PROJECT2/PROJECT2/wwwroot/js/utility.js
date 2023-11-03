function requiredTextField(control, errorMessage) {
    var id = "#txt" + control;
    var err = "#err" + control;
    var txtval = $(id).val();

    if (txtval == "" || txtval == null || txtval == undefined || $(id).val()=="") {
        $(err).html("please enter a " + errorMessage);
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
        $(err).html("please enter a " + errorMessage);
        return false;

    }
    else {
        $(err).html("");
        if (GetRegex(control).test(txtval)) {
          
            return true;
        }
        else {
            $(err).html("please enter correct " + errorMessage);
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