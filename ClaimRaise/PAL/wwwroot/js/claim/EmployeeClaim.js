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
            "UserId": UserLoginInfo.id
        }
        data.append("claim", JSON.stringify(claim));
        data.append("evidence", evidence);
        

        $.ajax({
            url: base_url + "Claim/ClaimRequest",
            method: "POST",
            cache: false,
            contentType: false,
            processData: false,
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            data: data,
            "success": function (response) {
                if (response.ok) {
                    Swal.fire({
                        title: "Good job!",
                        text: response.message,
                        icon: "success",
                        timer:1500
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
                console.log(err)
            }
        })
    }
}


$("#txtClaimTitle").keyup(function () {
    isValid = requiredTextFilled("ClaimTitle", "claim title");
    if (!isValid) { return false }
})
$("#txtClaimReason").change(function () {
    isValid = requiredTextFilled("ClaimReason", "claim reason");
    if (!isValid) { return false }
})
$("#txtClaimExpenseDt").change(function () {
    isValid = requiredTextFilled("ClaimExpenseDt", "claim expense date");
    isValid = compareDates("ClaimExpenseDt");
    if (!isValid) { return false }
})
$("#txtClaimAmount").keyup(function () {
    isValid = requiredTextFilled("ClaimAmount", "claim amount");
    isValid = CurrentAmount("ClaimAmount", "postive Number");
    if (!isValid) { return false }
})
$("#txtClaimEvidence").change(function () {
    isValid = requiredTextFilled("ClaimEvidence", "evidence");
    if (!isValid) { return false }
})
$("#txtClaimDescription").keyup(function () {
    isValid = requiredTextFilled("ClaimDescription", "description");
    if (!isValid) { return false }
})
