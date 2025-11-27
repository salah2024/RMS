function GetBaravordZaribManteghe(OperationId) {
    var baravordId = $('#HDFBarAvordUserID').val(); // همون Guid برآورد

    var vardata = {};
    vardata.BaravordId = baravordId; // 👈 اسم باید با DTO یکی باشد

    $.ajax({
        type: "POST",
        url: "/Area/GetBaravordZaribManteghe",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // اینجا باید سه تا دراپ‌داون و span را پر کنیم
            FillZaribMantegheControls(OperationId, data);
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری ضریب منطقه ای', 'خطا');
        }
    });
}

function FillZaribMantegheControls(OperationId, data) {

    debugger;
    var $ddlOstan = $('#ddlOstan_' + OperationId);
    var $ddlShahr = $('#ddlShahr_' + OperationId);
    var $ddlBakhsh = $('#ddlBakhsh_' + OperationId);
    var $spanZarib = $('#spanZaribRah'); // یا '#spanZaribRah' اگر عمومی است

    // ۱) استان‌ها همیشه باید لود شوند
    $ddlOstan.empty();
    $ddlOstan.append('<option value="">انتخاب استان</option>');

    if (data && data.lstOstan) {
        $.each(data.lstOstan, function (i, ostan) {
            // اینجا اسم پراپرتی‌ها را با مدل خودت تنظیم کن
            // مثال: ostan.OstanId , ostan.OstanName
            $ddlOstan.append(
                '<option value="' + ostan.id + '">' + ostan.name + '</option>'
            );
        });
    }

    // اگر اصلاً رکورد قبلی وجود نداشته
    if (!data || !data.baravordZaribManteghe) {
        // ۲) شهرستان و بخش در حالت "منتظر انتخاب" باشند
        $ddlShahr.empty().append('<option value="">ابتدا استان را انتخاب کنید</option>');
        $ddlBakhsh.empty().append('<option value="">ابتدا شهرستان را انتخاب کنید</option>');
        $spanZarib.text(''); // ضریب خالی
        return;
    }

    // اگر رکورد قبلی هست:
    var bz = data.baravordZaribManteghe;

    // ۳) شهرستان‌های استان مربوطه
    $ddlShahr.empty();
    $ddlShahr.append('<option value="">انتخاب شهرستان</option>');
    if (data.lstShahr) {
        $.each(data.lstShahr, function (i, shahr) {
            // مثال: shahr.ShahrId , shahr.ShahrName
            $ddlShahr.append(
                '<option value="' + shahr.id + '">' + shahr.name + '</option>'
            );
        });
    }

    // ۴) بخش‌های شهرستان مربوطه
    $ddlBakhsh.empty();
    $ddlBakhsh.append('<option value="">انتخاب بخش</option>');
    if (data.lstBakhsh) {
        $.each(data.lstBakhsh, function (i, bakhsh) {
            // مثال: bakhsh.BakhshId , bakhsh.BakhshName
            $ddlBakhsh.append(
                '<option value="' + bakhsh.id + '">' + bakhsh.name + '</option>'
            );
        });
    }

    // ۵) ست کردن انتخاب‌ها روی مقدار قبلی
    $ddlOstan.val(bz.ostanId);
    $ddlShahr.val(bz.shahrId);
    $ddlBakhsh.val(bz.bakhshId);

    // ۶) ست کردن ضریب
    $spanZarib.html(bz.zaribManteghe);
    $('#divZaribRah').show();
}


function ShowZaribMantaghe(OperationId) {
    let ostanId = 'ddlOstan_' + OperationId;
    let shahrId = 'ddlShahr_' + OperationId;
    let bakhshId = 'ddlBakhsh_' + OperationId;

    let str = `
<div class="row boardRowStyle" style="direction: rtl; text-align: right;">

    <div class="col-md-2">
        <label for="${ostanId}">استان</label>
        <select id="${ostanId}" class="form-control">
            <option value="">انتخاب استان</option>
        </select>
    </div>
    <div class="col-md-2" style="margin-right:20px">
        <label for="${shahrId}">شهرستان</label>
        <select id="${shahrId}" class="form-control">
            <option value="">ابتدا استان را انتخاب کنید</option>
        </select>
    </div>
    <div class="col-md-2" style="margin-right:20px">
        <label for="${bakhshId}">بخش</label>
        <select id="${bakhshId}" class="form-control">
            <option value="">ابتدا شهرستان را انتخاب کنید</option>
        </select>
    </div>
    <div class="col-md-2" id="divZaribRah" style="padding-top: 37px;display:none;margin-right:40px">
    <span style="font-weight:bold;">ضریب منطقه ای راه،باند و فرودگاه = </span>
    <span style="font-weight:bold;" id="spanZaribRah"></span>
    </div>

</div>`;

    // ست کردن HTML در div مربوط به OperationId
    $('#ula' + OperationId).html(str);

    // گرفتن ریفرنس‌های jQuery
    let $ddlOstan = $('#' + ostanId);
    let $ddlShahr = $('#' + shahrId);
    let $ddlBakhsh = $('#' + bakhshId);

    // ایونت‌ها
    $ddlOstan.off('change').on('change', function () {
        let ostanIdVal = $(this).val();
        debugger;
        if (ostanIdVal) {
            $('#divZaribRah').hide();
            GetShahr(ostanIdVal, $ddlShahr, $ddlBakhsh);
        } else {
            // اگر استان خالی شد، بقیه هم ریست
            $ddlShahr.empty().append('<option value="">ابتدا استان را انتخاب کنید</option>');
            $ddlBakhsh.empty().append('<option value="">ابتدا شهرستان را انتخاب کنید</option>');
        }
    });

    $ddlShahr.off('change').on('change', function () {
        let shahrIdVal = $(this).val();
        if (shahrIdVal) {
            $('#divZaribRah').hide();
            GetBakhsh(shahrIdVal, $ddlBakhsh);
        } else {
            $ddlBakhsh.empty().append('<option value="">ابتدا شهرستان را انتخاب کنید</option>');
        }
    });

    $ddlBakhsh.off('change').on('change', function () {
        let bakhshIdVal = $(this).val();
        if (bakhshIdVal) {
            GetZaribManteghe(bakhshIdVal);
        } 
    });

    // در بدو نمایش، استان‌ها لود شوند
    GetOstan($ddlOstan, $ddlShahr, $ddlBakhsh);

    GetBaravordZaribManteghe(OperationId);
}

function GetZaribManteghe(bakhshId) {
    var baravordId = $('#HDFBarAvordUserID').val(); // همون Guid برآورد
    debugger;
    var vardata = {};
    vardata.BaravordId = baravordId;
    vardata.BakhshId = bakhshId;

    $.ajax({
        type: "POST",
        url: "/Area/GetZaribManteghe",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            debugger;
            ZaribRah = data.zaribRah;
            $('#spanZaribRah').html(ZaribRah);
            $('#divZaribRah').show();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری ضریب منطقه ای', 'خطا');
        }
    });
}

// لود کردن استان‌ها
function GetOstan($ddlOstan, $ddlShahr, $ddlBakhsh) {
    debugger;
    $.ajax({
        type: "POST",
        url: "/Area/GetOstan",
        // نیازی به ارسال دیتا نیست
        data: JSON.stringify({}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $ddlOstan.empty();
            $ddlOstan.append('<option value="">انتخاب استان</option>');

            $.each(data, function (i, ostan) {
                $ddlOstan.append(
                    '<option value="' + ostan.id + '">' + ostan.name + '</option>'
                );
                // اگر پراپرتی‌ها اسم دیگری دارند اینجا اصلاح کن
                // مثال:
                // ostan.OstanId , ostan.OstanName
            });

            // ریست بقیه
            $ddlShahr.empty().append('<option value="">ابتدا استان را انتخاب کنید</option>');
            $ddlBakhsh.empty().append('<option value="">ابتدا شهرستان را انتخاب کنید</option>');
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری استان', 'خطا');
        }
    });
}

// لود کردن شهرستان‌ها
function GetShahr(OstanId, $ddlShahr, $ddlBakhsh) {
    var vardata = {};
    vardata.OstanId = OstanId;

    $.ajax({
        type: "POST",
        url: "/Area/GetShahr",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $ddlShahr.empty();
            $ddlShahr.append('<option value="">انتخاب شهرستان</option>');

            $.each(data, function (i, shahr) {
                $ddlShahr.append(
                    '<option value="' + shahr.id + '">' + shahr.name + '</option>'
                );
                // در صورت نیاز اسم پراپرتی‌ها را اصلاح کن
            });

            $ddlBakhsh.empty().append('<option value="">ابتدا شهرستان را انتخاب کنید</option>');
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری شهرستان', 'خطا');
        }
    });
}

// لود کردن بخش‌ها
function GetBakhsh(ShahrId, $ddlBakhsh) {
    debugger;
    var vardata = {};
    vardata.ShahrId = ShahrId;

    $.ajax({
        type: "POST",
        url: "/Area/GetBakhsh",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $ddlBakhsh.empty();
            $ddlBakhsh.append('<option value="">انتخاب بخش</option>');

            $.each(data, function (i, bakhsh) {
                $ddlBakhsh.append(
                    '<option value="' + bakhsh.id + '">' + bakhsh.name + '</option>'
                );
                // در صورت نیاز اسم پراپرتی‌ها را اصلاح کن
            });
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری بخش', 'خطا');
        }
    });
}

