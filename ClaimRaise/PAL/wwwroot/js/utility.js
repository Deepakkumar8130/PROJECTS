$(document).ready(function () {
    $("#themeMode").click(function () {
        var currentTheme = localStorage.getItem('theme') || 'light';
        var newTheme = (currentTheme === 'light') ? 'dark-mode' : "light"

        // Update the theme in localStorage
        localStorage.setItem('theme', newTheme);
        applyTheme(newTheme);
    })
})
function requiredTextFilled(control, ErrorMessage, validationtype = "all") {
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
        case "EmailAddress":
            regx = /^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$/
            break; case "Email":
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


function FillDropDownList(url, params, ddlId, async = true) {
    var ddl = "<option value='-1'>Select</option>";
    $.ajax({
        url: base_url + url,
        method: "GET",
        contentType: JSON,
        async: async,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": UserLoginInfo.id, "Role": params },
        "success": function (response) {
            //console.log(response.data)
            if (response.ok) {
                response.data.forEach(function (item, i) {
                    ddl += "<option value="+item.id+">"+item.name+"</option>"
                })
                $("#" + ddlId).html(ddl);
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}



function downloadURI(uri, name) {
    let link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.style.display = "none";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}




function requiredSelectFiled(control, ErrorMessage) {
    var id = "#ddl" + control
    var err = "#err" + control
    var formGroup = "#formGroup" + control
    var txtVal = $(id).val()
    if (txtVal == "" || txtVal == null || txtVal == "-1") {
        $(err).html("Please select " + ErrorMessage).addClass("text-danger")
        $(formGroup).addClass("text-danger")
        return false
    }
    else {

        $(err).html("").removeClass("text-danger")
        $(formGroup).removeClass("text-danger")
        return true
    }
}

function comparePassword(control1, control2) {
    var id = "#txt" + control1
    var id1 = "#txt" + control2
    var err = "#err" + control2
    var formGroup = "#formGroup" + control2
    var txtVal = $(id).val()
    var txtVal1 = $(id1).val()
    if (txtVal != txtVal1) {
        $(err).html("Confirm password not matched !").addClass("text-danger")
        $(formGroup).addClass("text-danger")
        return false
    }
    else {

        $(err).html("").removeClass("text-danger")
        $(formGroup).removeClass("text-danger")
        return true
    }
}



function CurrentAmount(control, ErrorMessage) {
    var id = "#txt" + control;
    var err = "#err" + control;
    var formGroup = "#formGroup" + control;
    var txtVal = $(id).val();

    if (txtVal < 0) {
        $(err).html("please enter " + ErrorMessage).addClass("text-danger");
        $(formGroup).addClass("text-danger");
        return false;
    }
    else {
        if (txtVal > 500) {
            $(err).html("").removeClass("text-danger");
            $(formGroup).removeClass("text-danger");
            return true;
        }
        else {
            $(err).html("Minimun Amount 500").addClass("text-danger");
            $(formGroup).addClass("text-danger");
            return false;
        }
    }
}

function compareDates(control) {
    var id = "#txt" + control
    var err = "#err" + control
    var formGroup = "#formGroup" + control
    var txtVal = $(id).val()
    var inputDate = new Date(txtVal);
    var currentDate = new Date();
    if (inputDate  > currentDate) {
        $(err).html("Please Choose Valid Date !").addClass("text-danger")
        $(formGroup).addClass("text-danger")
        return false
    }
    else {

        $(err).html("").removeClass("text-danger")
        $(formGroup).removeClass("text-danger")
        return true
    }
}


function GetAllUsersWithRole() {

    $.ajax({
        url: base_url + "UserManaged/GetUsersWithRole",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": UserLoginInfo.id },
        "success": function (response) {
            if (response.ok) {
                var options = '<option value="">Select</option>'
                response.data.forEach(function (item, index) {
                    options += '<option data="' + item.role + '" value=' + item.id + '>' + item.userName + '</option>'
                })
                $("#ddlUserNames").html(options)
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}