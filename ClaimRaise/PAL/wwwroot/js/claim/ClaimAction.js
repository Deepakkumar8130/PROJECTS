$(document).ready(function () {
    //console.log(localStorage.getItem("UserId"))
    //console.log(localStorage.getItem("UserRole"))
    GetAllPendingRequests();


    $("#btnReject").click(function () {
        isValid = requiredTextFilled("Remarks", "remarks");
        if (!isValid) { return false; }

        if (confirm("Are you sure to reject this request ?")) {
            ApproveRejectRequest(0)
        }
    })

    $("#btnApprove").click(function () {
        isValid = requiredTextFilled("Remarks", "remarks");
        if (!isValid) { return false; }

        if (confirm("Are you sure to reject this request ?")) {
            ApproveRejectRequest(1)
        }
    })
})

function GetAllPendingRequests() {
    $.ajax({
        url: base_url + "Claim/GetAllPendingRequests",
        method: "GET",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: { "UserId": localStorage.getItem("UserId"), "Role": localStorage.getItem("UserRole") },
        "success": function (response) {
            console.log(response.data)
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1; // Serial numbers start from 1
                return item;
            });
            if (response.ok) {
                $("#tblPendingRequest").DataTable().destroy()
                $("#tblPendingRequest").DataTable({
                    data: response.data,
                    columns: [
                        { data: "serialNo"},
                        { data: "claimTitle"},
                        { data: "employeeName"},
                        { data: "claimReason"},
                        { data: "claimDt"},
                        { data: "claimAmount"},
                        { data: "claimExpenseDt"},
                        { data: "claimDescription" },
                        {
                            data: "claimEvidence", width: "70px", class: "text-center", render: function (claimEvidence) {
                                var btn = `<a class="btn btn-sm btn-info" onclick="DownloadEvidence('${claimEvidence}')">View</a>`
                                return btn;
                            }
                        },
                        {
                            data: "claimId", width: "70px", class: "text-center", render: function (id) {
                                var btn = '<a class="btn btn-sm btn-info" onclick="ShowActionHistory(' + id + ')">View</a>'
                                return btn;
                            }
                        },
                        {
                            data: "claimId", class: "text-center", render: function (id) {
                                var btn = '<a class="btn btn-sm btn-info" onclick="ActionRequest(' + id + ')">Action</a>'
                                return btn;
                            },
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

function ActionRequest(id) {
    $("#modalAction").modal("show");
    $("#hdnClaimId").val(id);
}




function ApproveRejectRequest(action) {
    var claim = {
        ClaimId: $("#hdnClaimId").val(),
        Action: action,
        UserId: localStorage.getItem("UserId"),
        Role: localStorage.getItem("UserRole"),
        Remarks: $("#txtRemarks").val()
    }

    $.ajax({
        url: base_url + "Claim/ActionOnRequest",
        method: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: JSON.stringify(claim),
        "success": function (response) {
            if (response.ok) {
                Swal.fire({
                    title: "Good job!",
                    text: response.message,
                    icon: "success",
                    timer: 1500
                });
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: response.message,
                    timer: 2000
                });
            }
            setTimeout(function () {
                location.reload()
            }, 1500)
        },
        "error": function (err) {
            console.log(err);
        }
    })
}


function ShowActionHistory(id) {
    
    $.ajax({
        url: base_url + "Claim/GetClaimHistory",
        method: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Authorization":"Bearer "+localStorage.getItem("token")
        },
        data: { id },
        "success": function (response) {
            if (response.ok) {
                response.data = response.data.map(function (item, index) {
                    item.serialNo = index + 1; // Serial numbers start from 1
                    return item;
                });
                $("#modalActionHistory").modal("show")
                $("#tblActionHistory").DataTable().destroy();
                $("#tblActionHistory").DataTable({
                    data: response.data,
                    columns: [
                        { data: "serialNo" },
                        { data: "action" },
                        { data: "actionBy" },
                        { data: "remark" },
                        { data: "actionDt" },
                    ]
                })
            }
            else {
                console.log(response.message)
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}



function DownloadEvidence(path) {
    
    $.ajax({
        url: base_url + "Claim/GetClaimEvidence",
        method: "POST",
        contentType: "application/json",
        dataType: "json",
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        data: JSON.stringify(path),
        "success": function (response) {
            if (response.ok) {

                 // Convert the base64-encoded binary data to a byte array
                const binaryData = atob(response.data);
                const byteArray = new Uint8Array(binaryData.length);

                // Fill the byte array with binary data
                for (let i = 0; i < binaryData.length; i++) {
                    byteArray[i] = binaryData.charCodeAt(i);
                }
                
                // Create a Blob from the byte array and prepare for download
                const blob = new Blob([byteArray], { type: "application/octet/stream" });
                const url = window.URL.createObjectURL(blob);
                downloadURI(url, "evideence_file." + path.split(".")[1])
                window.URL.revokeObjectURL(url);
                
            }
            else {
                console.log(response.message)
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