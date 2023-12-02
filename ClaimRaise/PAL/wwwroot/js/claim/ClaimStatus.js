$(document).ready(function () {
    GetClaimStatus();
})

function GetClaimStatus() {
    $.ajax({
        url: base_url + "Claim/GetClaimStatus",
        method: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { "UserId": UserLoginInfo.id },
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        success: function (response) {
            //console.log(response.data)
            response.data = response.data.map(function (item, index) {
                item.serialNo = index + 1; // Serial numbers start from 1
                return item;
            });
            if (response.ok) {
                $("#tblClaimStatus").DataTable().destroy();
                $("#tblClaimStatus").DataTable({

                    data: response.data,
                    columns: [
                        { data: "serialNo" },
                        { data: "claimId" },
                        { data: "claimTitle" },
                        { data: "claimReason" },
                        { data: "claimAmount" },
                        { data: "claimDt" },
                        { data: "action" },
                        { data: "actionBy" },
                        { data: "currentStatus" }
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