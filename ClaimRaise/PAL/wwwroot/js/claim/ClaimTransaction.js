$(document).ready(function () {
    GetClaimTransaction();
})

function GetClaimTransaction() {
    $.ajax({
        url: base_url + "Claim/GetClaimsTransaction",
        method: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { "UserId": localStorage.getItem("UserId") },
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        success: function (response) {
            console.log(response.data)
            if (response.ok) {
                $("#tblClaimTransactions").DataTable().destroy();
                $("#tblClaimTransactions").DataTable({
                    data: response.data,
                    columns: [
                        { data: "transactionNo" },
                        { data: "claimTitle" },
                        { data: "claimReason" },
                        { data: "claimAmount" },
                        { data: "claimDescription" },
                        { data: "claimDt" },
                        { data: "transactionDt" },
                        { data: "approvedBy" }
                    ]
                })
            }
            else {
                console.log(response.message)
            }
            
        },
        error: function (err) {
            console.log(err)
        }
    })
}