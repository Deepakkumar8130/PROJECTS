$(document).ready(function () {

    GetEmployees();
    GetCountry();
    $("#ddlCountry").change(function () {
        empty("ddlState");
        empty("ddlCity");
        var cid = $("#ddlCountry").val();
        GetState(cid);
    })
    $("#ddlState").change(function () {
        empty("ddlCity");
        var sid = $("#ddlState").val();
        GetCity(sid);
    })
    $('#ChooseImg').change(function (e) {
        var url = $('#ChooseImg').val();
        var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
        if (ChooseImg.files && ChooseImg.files[0] && (ext == "gif" || ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "bmp")) {
            var reader = new FileReader();
            reader.onload = function () {
                var output = document.getElementById('PrevImg');
                output.src = reader.result;
            }
            reader.readAsDataURL(e.target.files[0]);
        }
    });


    $("#addbtn").click(function () {
        // alert("OK")
        document.getElementById("PrevImg").src = "https://placehold.jp/150x150.png";
        document.getElementById("ChooseImg").val = "";

        $("#btnSubmit").val("Submit");
        $("#txtId").val("");
        $("#txtName").val("");
        $("#txtEmail").val("");
        $("#txtMobile").val("");
        $("#ddlGender").val("");
        $("#ddlCountry").val("");
        empty("ddlState");
        empty("ddlCity");
    })



    $("#btnSubmit").click(function () {
        AddImage();
    })

})
