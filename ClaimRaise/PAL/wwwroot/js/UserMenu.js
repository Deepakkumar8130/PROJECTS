var base_url = "https://localhost:7183/api/";
$(document).ready(function () {
    GetMenuProgram();
})

function GetMenuProgram() {
    let UserId = window.localStorage.getItem("UserId")
    console.log(typeof UserId)
    console.log(UserId)
    alert("check")
    $.ajax({
        "url": base_url + "Program/GetProgramsById",
        "method": "POST",
        "contentType": "application/json; charset=utf-8",
        dataType: "json",
        "data": parseInt(UserId),
        "success": function (response) {
            if (response.ok) {
                alert("OK")
                console.log(response);
            }
            else {
                $("#msg").html(response.message).css("color", "red");
            }
        },
        "error": function (err) {
            console.log(err);
        }
    })
}