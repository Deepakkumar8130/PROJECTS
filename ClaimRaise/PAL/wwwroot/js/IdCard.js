$(document).ready(function () {
    $("#btnViewIdCard").click(function () {
        $("#modalIdCard").modal("show");
        $("#CardName").text(UserLoginInfo.userName);
        $("#CardDesignation").text(UserLoginInfo.role);
        $("#CardEmployeeId").text(UserLoginInfo.id);
        $("#CardEmail").text(UserLoginInfo.email);
        $("#CardPhone").text(UserLoginInfo.mobile);
    })

    $("#downloadIdBtn").click(function () {
        var element = document.getElementById('idCard');
        var options = {
            margin: 10,
            filename: UserLoginInfo.userName,
            image: { type: 'png', quality: 1 },
            html2canvas: { scale: 2 },
            jsPDF: { unit: 'mm', format: [180, 145], orientation: 'portrait', compressPDF: true }
        };

        var worker = html2pdf(element, options).from(element).outputPdf().then(function (pdf) {
            saveAs(pdf, options.filename);
        });
    })
})