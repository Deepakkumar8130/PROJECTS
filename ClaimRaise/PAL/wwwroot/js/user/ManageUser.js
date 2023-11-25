$(document).ready(function(){
    GetAllUsers();

    $("#btnAdd").click(function () {
        $("#modalAddUser").modal("show")
    })

    $("#btnUserFormReset").click(function () {
        var form = $("#UserForm")[0];
        form.reset();
    })
})


function GetAllUsers() {

    $.ajax({
        url: base_url + "UserManaged/GetAllUsers",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": localStorage.getItem("UserId") },
        "success": function (response) {
            //console.log(response.data)
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1;
                return item;
            });
            if (response.ok) {
                $("#tblUsers").DataTable().destroy()
                $("#tblUsers").DataTable({
                    data: response.data,
                    columns: [
                        { data: "serialNo" },
                        { data: "userName" },
                        { data: "email" },
                        { data: "mobile" },
                        { data: "managerName" },
                        { data: "status" },
                        {
                            data: "id", width: "70px", class: "text-center", render: function (id) {
                                var btn = `<a class="btn btn-sm btn-info" onclick="Edit('${id}')">Edit</a>`
                                return btn;
                            }
                        }

                   ]
                 })
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}


function Edit(id) {
    $.ajax({
        url: base_url + "UserManaged/GetUserById",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": id },
        "success": function (response) {
            console.log(response.data)
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1;
                return item;
            });
            if (response.ok) {
                
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}