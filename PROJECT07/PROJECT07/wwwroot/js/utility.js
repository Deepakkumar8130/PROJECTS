
function getCountry() {
    $.ajax({
        url: '/Employee/getCountry',
        type: 'GET',
        async: false,
        success: function (result) {
            $.each(result, function (i, data) {
                $("#ddlCountry").append('<option value='+data.id+'>'+data.name+'</option>')
            });
        }
    });
}


function getState(id) {
    $.ajax({
        url: '/Employee/getState?id='+id,
        type: 'GET',
        async: false,
        success: function (result) {
            $.each(result, function (i, data) {
                $("#ddlState").append('<option value='+data.id+'>'+data.name+'</option>')
            });
        }
    });
}

function getCity(id) {
    $.ajax({
        url: '/Employee/getCity?id='+id,
        type: 'GET',
        async: false,
        success: function (result) {
            $.each(result, function (i, data) {
                $("#ddlCity").append('<option value='+data.id+'>'+data.name+'</option>')
            });
        }
    });
}