function OverLowKMCheck(KMS, KME) {
    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/OverLowKMCheck",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

        },
        error: function (response) {
            toastr.error('مشکل در دریافت اطلاعات', 'خطا');
        }
    });
}

function toPersianDigits(num) {
    if (num == null) return '';
    return num.toString().replace(/\d/g, d => '۰۱۲۳۴۵۶۷۸۹'[d]);
}