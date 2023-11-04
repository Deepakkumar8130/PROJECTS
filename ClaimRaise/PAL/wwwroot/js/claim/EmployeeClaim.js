$(document).ready(function () {
    $("#btnClaimSubmit").click(function () {
        addClaimRequest();
    })
    $("#btnFormReset").click(function () {
        var form = $("#ClaimForm")[0];
        form.reset();
    })
})

function addClaimRequest() {
    var isValid = false;

    isValid = requiredTextFilled("ClaimTitle", "claim title");
    if (!isValid) { return false; }

    isValid = requiredTextFilled("ClaimReason", "claim reason");
    if (!isValid) { return false; }

    isValid = requiredTextFilled("ClaimAmount", "claim amount");
    if (!isValid) { return false; }

    isValid = requiredTextFilled("ClaimExpenseDt", "expense date");
    if (!isValid) { return false; }

    isValid = requiredTextFilled("ClaimEvidence", "evidence");
    if (!isValid) { return false; }

    isValid = requiredTextFilled("ClaimDescription", "description");
    if (!isValid) { return false; }

    if (isValid) {
        var data = new FormData();
        var evidence = $("#txtClaimEvidence")[0].files[0];
        //console.log(evidence.name)
        var claim = {
            "ClaimTitle":$("#txtClaimTitle").val(),
            "ClaimReason": $("#txtClaimReason").val(),
            "ClaimDescription": $("#txtClaimDescription").val(),
            "ExpenseDt": $("#txtClaimExpenseDt").val(),
            "ClaimAmount": $("#txtClaimAmount").val(),
            "UserId": window.localStorage.getItem("UserId")
        }
        data.append("claim", JSON.stringify(claim));
        data.append("evidence", evidence);
        

        $.ajax({
            url: base_url + "Claim/ClaimRequest",
            method: "POST",
            contentType: false,
            processData: false,
            //headers: {
            //    "Authorization": "Bearer" + localStorage.getItem("token")
            //},
            data: data,
            "success": function (response) {
                console.log(response)
            },
            "error": function (err) {
                console.log(err)
            }
        })
    }
}


$("#txtClaimTitle").keyup(function () {
    isValid = requiredTextFilled("ClaimTitle", "claim title");
    if (!isValid) { return false }
})
$("#txtClaimReason").keyup(function () {
    isValid = requiredTextFilled("ClaimReason", "claim title");
    if (!isValid) { return false }
})
$("#txtClaimExpenseDt").keyup(function () {
    isValid = requiredTextFilled("txtClaimExpenseDt", "claim title");
    if (!isValid) { return false }
})
$("#txtClaimAmount").keyup(function () {
    isValid = requiredTextFilled("ClaimAmount", "claim amount");
    if (!isValid) { return false }
})
$("#txtClaimEvidence").keyup(function () {
    isValid = requiredTextFilled("ClaimEvidence", "evidence");
    if (!isValid) { return false }
})
$("#txtClaimDescription").keyup(function () {
    isValid = requiredTextFilled("ClaimDescription", "description");
    if (!isValid) { return false }
})
