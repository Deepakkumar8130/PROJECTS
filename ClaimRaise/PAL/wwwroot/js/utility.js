$(document).ready(function () {
    //$(".wrapper").addClass(localStorage.getItem("theme"));
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
        data: { "UserId": localStorage.getItem("UserId"), "Role": params },
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



function requiredSelectFiled(control, ErrorMessage) {
    var id = "#ddl" + control
    var err = "#err" + control
    var formGroup = "#formGroup" + control
    var txtVal = $(id).val()
    if (txtVal == "" || txtVal == null || txtVal == "-1") {
        $(err).html("Please select " + ErrorMessage).addClass("error-control")
        $(formGroup).addClass("error-control")
        return false
    }
    else {

        $(err).html("").removeClass("error-control")
        $(formGroup).removeClass("error-control")
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

//function changeTheme() {
//    alert("here")
//    //if (localStorage.getItem("theme") == "light") {
//    //    localStorage.setItem("theme", "dark-mode");
//    //    $("#themeMode").html("<i class='far fa-moon'></i>")
//    //    window.location.reload();
//    //}
//    //else {
//    //    localStorage.setItem("theme", "light");
//    //    window.location.reload();
//    //}

//    if (theme == "light") {
//        theme = "dark-mode";
//    }
//    else {
//        theme = "light"
//    }
//    var elements = document.querySelectorAll('.wrapper');
//    elements.forEach(function (element) {
//        element.classList.add(theme);
//    });
//}

