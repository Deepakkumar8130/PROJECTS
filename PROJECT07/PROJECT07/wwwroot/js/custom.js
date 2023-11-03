$(document).ready(function () {

    getCountry();

    $("#ddlCountry").change(function () {
        var cid = $(this).val();
        $("#ddlState").empty();
        $("#ddlCity").empty();
        $("#ddlState").append('<option>Select</option>');
        $("#ddlCity").append('<option>Select</option>');
        getState(cid);
    });

    $("#ddlState").change(function () {
        $("#ddlCity").empty();
        var sid = $(this).val();
        $("#ddlCity").append('<option>Select</option>');
        getCity(sid);
    });
})