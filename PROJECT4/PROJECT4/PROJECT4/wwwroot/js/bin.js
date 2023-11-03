$(document).ready(function () {
    getBinCustomers();
})


function getBinCustomers() {
    $.get("/Customer/GetBinCustomers", function (response) {
        console.log(response)
        $("#tbl_Customer").DataTable({
            data: response,
            dom: 'Bfrtip',
            buttons: [
                'excel',
                'csv',
                'pdf'
            ],
            columns: [
                { "data": "id" },
                { "data": "name" },
                { "data": "email" },
                { "data": "mobile" },
                { "data": "gender" },
                { "data": "country" },
                { "data": "state" },
                { "data": "city" },
                {
                    "data": "id", class: "text-center", render: function (id) {
                        var link = "<a onclick='UndoRecord(" + id + ")'><i class='fa fa-undo'></i></a>"
                        link += "<a onclick='PermanentDeleteRecord(" + id + ")'><i class='fa fa-trash'></i></a>"
                        return link
                    }
                }
            ]
        })

    })
}


function UndoRecord(id) {
    if (confirm("Are You Sure To Recover This Record") == false) {
        return false;
    }
    $.post("/Customer/UndoRecord", { "id": id }, function (response) {
        if (response.ok) {
            $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
            setTimeout(function () {
                window.location.reload()
            }, 2000)
        }
        else {
            $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
        }
    })
}

function PermanentDeleteRecord(id) {
    if (confirm("Are You Sure To Permenent Delete") == false) {
        return false;
    }
    $.post("/Customer/PermanentDeleteRecord", { "id": id }, function (response) {
        if (response.ok) {
            $("#msg").html(response.message).removeClass("text-danger").addClass("text-success")
            setTimeout(function () {
                window.location.reload()
            }, 2000)
        }
        else {
            $("#msg").html(response.message).removeClass("text-success").addClass("text-danger")
        }
    })
}