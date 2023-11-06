$(document).ready(function () {
    console.log(localStorage.getItem("UserId"))
    console.log(localStorage.getItem("UserRole"))
    GetAllPendingRequests();


    $("#btnReject").click(function () {
        isValid = requiredTextFilled("Remarks", "remarks");
        if (!isvalid) { return false; }

        if (confirm("Are you sure to reject this request ?")) {
            ApproveRejectRequest(0)
        }
    })

    $("#btnApprove").click(function () {
        isValid = requiredTextFilled("Remarks", "remarks");
        if (!isvalid) { return false; }

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
        //headers: {
        //    "Authorization": "Bearer " + localStorage.getItem("token")
        //},
        data: { "UserId": localStorage.getItem("UserId"), "Role": localStorage.getItem("UserRole") },
        "success": function (response) {
            console.log(response.data)
            if (response.ok) {
                $("#tblPendingRequest").DataTable({
                    data: response.data,
                    columns: [
                        { data: "claimTitle"},
                        { data: "employeeName"},
                        { data: "claimReason"},
                        { data: "claimDt"},
                        { data: "claimAmount"},
                        { data: "claimExpenseDt"},
                        { data: "claimDescription" },
                        {
                            data: "evidence", width: "70px", class: "text-center", render: function (data) {
                                var btn = '<a class="btn btn-sm btn-info">View</a>'
                                return btn
                            }
                        },
                        {
                            data: "claimId", class: "text-center", render: function (id) {
                                var btn = '<a class="btn btn-sm btn-info" onclick="ActionRequest(' + id + ')">Action</a>'
                                return btn
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
    
}