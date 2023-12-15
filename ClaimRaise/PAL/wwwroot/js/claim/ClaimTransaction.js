$(document).ready(function () {
    GetClaimTransaction();
})

function GetClaimTransaction() {
    $.ajax({
        url: base_url + "Claim/GetClaimsTransaction",
        method: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { "UserId": UserLoginInfo.id },
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        success: function (response) {
            //console.log(response.data)
           
            if (response.ok) {
                response.data = response.data.map(function (item, index) {
                    item.serialNo = index + 1; // Serial numbers start from 1
                    return item;
                });
                $("#tblClaimTransactions").DataTable().destroy();
                $("#tblClaimTransactions").DataTable({

                    data: response.data,
                    columns: [
                        { data: "serialNo" },
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