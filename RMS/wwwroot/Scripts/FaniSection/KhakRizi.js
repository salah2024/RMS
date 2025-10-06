//////////////////خاکریزی
////////////////////
///////////////////
function KhakRiziWithBarAvordClick(OpId, BarAvordUserId) {

    str = `
                <div id="ExistKhakRizi"></div>
            <div id="divViewKhakRizi" class="khak-section">
            </div>
    `
    $('#ula' + OpId).html(str);

    GetExistKhakRizi(BarAvordUserId,0);
    ShowSelctionKhakRizi(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0, OpId);

    //str = '';
    //str += '<div class=\'\' style=\'margin-top:3px;\'><div class=\'col-md-6 row\'><div class=\'col-md-5\'><a class=\'NewPolStyle\' onclick=\"ShowSelctionKhakRizi(1,0,0,' + "'" + BarAvordUserId + "'" + ',0,0,0,0,0)\">کیلومتراژ جدید</a></div><div class=\'col-md-5\'><a class=\'NewPolStyle\' onclick=\"ShowExistingKMKhakRizi(' + BarAvordUserId + ')\">لیست خاکریزی ها</a></div></div>';
    //str += '<div class=\'\' style=\'margin-top: 30px;\' id=\'ViewKhakRizi\'></div>';
    ////$('#ula' + OpId).html(str);
    //$('#KhakRiziShow').find('#divShowKhakRizi').html(str);
    //$('#aKhakRiziShow').click();
}




function renderRadios($container, groupName, items) {
    $container.empty();

    if (!Array.isArray(items) || items.length === 0) {
        $container.append($('<div/>', { class: 'text-muted small', text: 'داده‌ای یافت نشد' }));
        return;
    }

    const uid = Math.random().toString(36).slice(2, 8);

    items.forEach((item, idx) => {
        // پشتیبانی از هر دو حالت PascalCase/camelCase
        const val = (item.id ?? item.Id ?? idx);
        const lbl = (item.description ?? item.Description ?? '').toString();
        const id = `${groupName}-${uid}-${val}`;

        const $input = $('<input/>', {
            class: 'form-check-input',
            type: 'radio',
            id,
            name: groupName,
            value: val
        });

        const $label = $('<label/>', {
            class: 'form-check-label',
            for: id,
            text: lbl
        });

        const $wrap = $('<div/>', { class: 'form-check khak-radio' })
            .append($input, $label);

        $container.append($wrap);
    });
}

function renderHajmInputs($container, items) {
    $container.empty();
    if (!Array.isArray(items) || items.length === 0) {
        $container.append($('<div/>', { class: 'text-muted small', text: 'داده‌ای یافت نشد' }));
        return;
    }

    items.forEach((item, idx) => {
        const idVal = item.id ?? item.Id ?? idx;
        const text = (item.description ?? item.Description ?? '').toString();
        const tid = `txtHajmKhRiziNew`;

        // col-10: متن توضیح آیتم
        const $colLabel = $('<div/>', { class: 'col-9 d-flex align-items-center' })
            .append($('<span/>', { text: text }));

        // col-2: ورودی + لیبل «مترمکعب» (بدون کادر)
        const $txt = $('<input/>', {
            id: tid,
            type: 'text',
            class: 'form-control_1 form-control-sm hajm-input',
            'data-id': idVal        // ← برای ارسال به سرور
        });
        const $unit = $('<span/>', { class: 'hajm-unit', text: 'مترمکعب' });

        const $innerRow = $('<div/>', { class: 'row align-items-center g-1', style: 'direction:ltr;' })
            .append($('<div/>', { class: 'col-4 text-start' }).append($unit))
            .append($('<div/>', { class: 'col-8' }).append($txt));

        const $colValue = $('<div/>', { class: 'col-3 px-0' }, { style: 'padding-left:0px;padding-right:0px' }).append($innerRow);

        // ردیف هر آیتم
        const $row = $('<div/>', { class: 'row align-items-center g-2 mb-2' })
            .append($colLabel, $colValue);

        $container.append($row);
    });
}

function ShowSelctionKhakRizi(IsNew, KMExistingId, KMNum, BarAvordUserId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value, OpId) {
    $.ajax({
        type: "POST",
        url: "/KhakRizi/GetDataForKhakRizi",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            const str = `
  <!-- هدر کیلومتراژ -->
  <div id="headerNew" class="row mb-3 khak-header" onclick="toggleKhakRiziNew()">
      <div class="col-auto d-flex align-items-center">
        <i class="fa fa-plus" style="font-size: 20px;color: green;"></i>
    </div>
    <div class="col-auto d-flex align-items-center">
      <span>از کیلومتراژ:</span>
    </div>
    <div class="col-auto">
      <input type="text" class="form-control_1 input-sm khak-input"
             id="txtFromKMForKhakRizi" value="000+000"  onclick="event.stopPropagation();"/>
    </div>

    <div class="col-auto d-flex align-items-center">
      <span>تا کیلومتراژ:</span>
    </div>
    <div class="col-auto">
      <input type="text" class="form-control_1 input-sm khak-input"
             id="txtToKMForKhakRizi" value="000+000"  onclick="event.stopPropagation();"/>
    </div>
  </div>

  <!-- محتوای اصلی (رادیوها با jQuery ساخته می‌شوند) -->
  <div id="divulaSomeOpId" style="display:none">
  <div id="ulaSomeOpId"></div>

  <!-- دکمه ذخیره پایین چپ -->
  <div class="row khak-save-row">
  <div class="col-10">
  </div>
    <div class="col-2 force-left">
      <a class="NewPolStyle btn buttonStyleBoard"
         onclick="SaveKhakRiziInfo('${BarAvordUserId}')">ذخیره</a>
    </div>
  </div>
  </div>
`;

            $('#divViewKhakRizi').html(str);

            $('#ula' + OpId).on('input change', '#txtFromKMForKhakRizi, #txtToKMForKhakRizi', function () {
                // اختیاری: حذف فوریِ ظاهر blinking هنگام تایپ
                $(this).removeClass('blinking');
                // و سپس اعتبارسنجی کل جفت
                validateKmFields();
            });

            $('#ula' + OpId).on('input change', '.hajm-input', function () {
                const val = $(this).val();
                // اگر مقدار خالی است: در حالت تایپ، blinking نداشته باشد
                if (!val || String(val).trim() === '') { $(this).removeClass('blinking'); return; }
                // اگر مقدار دارد ولی معتبر نیست: blinking بماند
                const ok = isValidDecimal4(val);
                $(this).toggleClass('blinking', !ok);
            });


            $('#ula' + OpId).on('change', 'input[name="hajmKhakRizi"]', function () {
                $('.hajm-input').removeClass('blinking');          // همه را بردار
                const $txt = $(`#${this.id}-txt`);
                $txt.addClass('blinking').focus();
            });

            $('#ula' + OpId).on('input change', '.hajm-input', function () {
                const ok = isValidDecimal4($(this).val());
                $(this).toggleClass('blinking', !ok);
            });

            const $Container = $('#ulaSomeOpId');

            // ردیف عنوان‌ها
            const $headerRow =
                $('<div/>', { class: 'row' }).append(
                    $('<div/>', { class: 'col-12 khak-card' }).append(
                        $('<div/>', { class: 'row' })
                            .append($('<div/>', { class: 'col-md-3' }).append($('<span/>', { text: 'نوع راه' })))
                            .append($('<div/>', { class: 'col-md-4' }).append($('<span/>', { text: 'نوع دانه بندی نوع خاک مصرفی در خاکریزی' })))
                            .append($('<div/>', { class: 'col-md-5' }).append($('<span/>', { text: 'حجم خاکریزی' })))
                    )
                );

            // ردیف محتوای رادیوها
            const $contentRow =
                $('<div/>', { class: 'row', style: 'background-color:#ddd4ff;padding:5px;margin-bottom:20px;' })
                    .append($('<div/>', { class: 'col-md-3' }).append($('<div/>', { id: 'col-roadType', class: 'khak-radios' })))
                    .append($('<div/>', { class: 'col-md-4' }).append($('<div/>', { id: 'col-noeDaneBandi', class: 'khak-radios' })))
                    .append($('<div/>', { class: 'col-md-5' }).append($('<div/>', { id: 'col-hajmKhakRizi', class: 'khak-radios' })));

            $Container.append($headerRow, $contentRow);

            // لیست‌ها
            const ListRoadType = data.listRoadType || [];
            const ListNoeDaneBandi = data.listNoeDaneBandi || [];
            const ListHajmKhakRizi = data.listHajmKhakRizi || [];

            renderRadios($('#col-roadType'), 'roadType', ListRoadType);
            renderRadios($('#col-noeDaneBandi'), 'noeDaneBandi', ListNoeDaneBandi);
            renderHajmInputs($('#col-hajmKhakRizi'), ListHajmKhakRizi);
        },
        error: function () {
            toastr.error('مشکل در بارگزاری خاکبرداری', 'خطا');
        }
    });
}



function renderHajmRadios($container, groupName, items) {
    $container.empty();
    if (!Array.isArray(items) || items.length === 0) {
        $container.append($('<div/>', { class: 'text-muted small', text: 'داده‌ای یافت نشد' }));
        return;
    }
    const uid = Math.random().toString(36).slice(2, 8);

    items.forEach((item, idx) => {
        const val = item.id ?? item.Id ?? idx;
        const lbl = (item.description ?? item.Description ?? '').toString();
        const rid = `${groupName}-${uid}-${val}`;
        const tid = `${rid}-txt`;

        // ستون 9: خود رادیو + متن توضیح
        const $colRadio = $('<div/>', { class: 'col-9 d-flex align-items-center gap-2' })
            .append(
                $('<input/>', { class: 'form-check-input', type: 'radio', id: rid, name: groupName, value: val }),
                $('<label/>', { class: 'form-check-label', for: rid, text: lbl })
            );

        // ستون 3: ورودی + لیبل واحد (بدون کادر)
        // داخلش یک ردیف 12تایی می‌سازیم تا نسبت input/unit خوب دربیاد
        const $txt = $('<input/>', {
            id: tid, type: 'text',
            class: 'form-control_1 form-control-sm hajm-input',
            'data-group': groupName
        });
        const $unit = $('<span/>', { class: 'hajm-unit', text: 'مترمکعب' });

        const $innerRow = $('<div/>', { class: 'row align-items-center g-1', style: 'direction:ltr;' })
            .append($('<div/>', { class: 'col-4 text-start' }).append($unit))
            .append($('<div/>', { class: 'col-8' }).append($txt));

        const $colValue = $('<div/>', { class: 'col-3', style: 'padding-right:0px;padding-left:0px;' }).append($innerRow);

        // ردیف هر آیتم
        const $row = $('<div/>', { class: 'row align-items-center g-2 mb-2' })
            .append($colRadio, $colValue);

        $container.append($row);
    });
}


// --- پارسر کیلومتراژ به عدد (km*1000 + m)، فرمت 000+000
function kmToNumber(str) {
    if (!str) return 0;
    const s = toEnglishDigits(str).replace(/\s/g, '');
    const m = s.match(/^(\d+)(?:\+(\d{1,3}))?$/); // KM [+ m]
    if (!m) return 0;
    const km = parseInt(m[1] || '0', 10);
    const mtr = parseInt(m[2] || '0', 10);
    return (isNaN(km) || isNaN(mtr)) ? 0 : (km * 1000 + mtr);
}


// --- افزودن/حذف کلاس خطا برای گروه‌های رادیو
const groupContainerByName = {
    roadType: '#col-roadType',
    noeDaneBandi: '#col-noeDaneBandi',
    hajmKhakRizi: '#col-hajmKhakRizi'
};
function markGroupInvalid(groupName, invalid) {
    const $inputs = $(`input[name="${groupName}"]`);
    if (invalid) {
        $inputs.addClass('radio-invalid').attr('aria-invalid', 'true');
    } else {
        $inputs.removeClass('radio-invalid').removeAttr('aria-invalid');
    }
}

// --- بررسی و هایلایت ورودی‌های کیلومتراژ
function validateKmFields() {
    const $from = $('#txtFromKMForKhakRizi');
    const $to = $('#txtToKMForKhakRizi');

    const fromStr = $from.val()?.trim();
    const toStr = $to.val()?.trim();

    const fromNum = kmToNumber(fromStr);
    const toNum = kmToNumber(toStr);

    // ابتدا هر دو را پاک کن
    $from.removeClass('blinking');
    $to.removeClass('blinking');

    let ok = true;

    // خالی یا صفر
    if (!fromStr || fromNum === 0) { $from.addClass('blinking'); ok = false; }
    if (!toStr || toNum === 0) { $to.addClass('blinking'); ok = false; }

    // ترتیب نادرست
    if (fromNum > 0 && toNum > 0 && toNum < fromNum) {
        $from.addClass('blinking');
        $to.addClass('blinking');
        ok = false;
    }
    return ok;
}


function toEnglishDigits(s) {
    if (!s) return '';
    return String(s)
        .replace(/[\u06F0-\u06F9]/g, c => String(c.charCodeAt(0) - 0x06F0))
        .replace(/[\u0660-\u0669]/g, c => String(c.charCodeAt(0) - 0x0660));
}

// اعتبارسنجی اعشاری تا 4 رقم اعشار (مثبت)
function isValidDecimal4(v) {
    if (v == null) return false;
    const s = toEnglishDigits(String(v)).trim().replace(',', '.');
    if (s === '') return false;
    // عدد صحیح یا اعشاری با حداکثر 4 رقم بعد از ممیز
    return /^\d+(\.\d{1,4})?$/.test(s);
}

// --- بررسی انتخاب رادیوهای هر گروه
function validateRadioGroups() {
    let ok = true;
    ['roadType', 'noeDaneBandi'].forEach(g => {
        const selected = $(`input[name="${g}"]:checked`).length > 0;
        markGroupInvalid(g, !selected); // همان تابعی که radio-invalid می‌گذاشت
        if (!selected) ok = false;
    });
    return ok;
}

function validateHajmGroup() {
    const $sel = $('input[name="hajmKhakRizi"]:checked');
    if ($sel.length === 0) { markGroupInvalid('hajmKhakRizi', true); return false; }
    markGroupInvalid('hajmKhakRizi', false);
    const $txt = $(`#${$sel.attr('id')}-txt`);
    const ok = isValidDecimal4($txt.val());
    $txt.toggleClass('blinking', !ok);
    return ok;
}

// --- هندل‌کننده‌ی رویداد برای پاک کردن خطاها هنگام تغییر
function wireValidationEvents() {
    // کیلومتراژها
    $('#txtFromKMForKhakRizi, #txtToKMForKhakRizi').on('input change', function () {
        validateKmFields();
    });

    // وقتی یکی از رادیوهای هر گروه انتخاب شد، استایل خطا از همان گروه برداشته شود
    ['roadType', 'noeDaneBandi', 'hajmKhakRizi'].forEach(g => {
        $(document).on('change', `input[name="${g}"]`, function () {
            markGroupInvalid(g, false);
        });
    });
}


wireValidationEvents();


function validateHajmInputsOnSave() {
    const $inputs = $('[id^="txtHajmKhRiziNew"]');
    let anyValid = false;

    $inputs.each(function () {
        const raw = $(this).val();
        const trimmed = (raw ?? '').trim();
        if (trimmed === '') {
            // فعلاً بدون هایلایت؛ بعداً اگر هیچ معتبری نبود، همه را هایلایت می‌کنیم
            $(this).removeClass('blinking');
            return;
        }
        const ok = isValidDecimal4(trimmed);
        if (ok) anyValid = true;
        $(this).toggleClass('blinking', !ok); // غیرمعتبر → blinking
    });

    if (!anyValid) {
        // هیچ مقداری وارد نشده یا همه نامعتبرند → همه را هایلایت کن
        $inputs.addClass('blinking');
        return false;
    }
    return true;
}


function normalizeDecimal4(v) {
    const s = toEnglishDigits(String(v || '')).trim().replace(',', '.');
    if (s === '') return null;

    return Number(parseFloat(s).toFixed(4));
}

function SaveKhakRiziInfo(barAvordUserId) {
    debugger;
    const kmOk = validateKmFields();
    const radiosOk = validateRadioGroups();  // برای roadType و noeDaneBandi
    const hajmOk = validateHajmInputsOnSave();

    if (!(kmOk && radiosOk && hajmOk)) {
        toastr.error('لطفاً خطاها را برطرف کنید.', 'خطا');
        const $firstErr = $('.blinking, .radio-invalid').first();
        if ($firstErr.length) { $('html, body').animate({ scrollTop: $firstErr.offset().top - 120 }, 300); }
        return;
    }


    hajmValues = '';
    $('[id^="txtHajmKhRiziNew"]').each(function () {
        const raw = ($(this).val() || '').trim();
        if (raw !== '') {
            const v = normalizeDecimal4(raw);
            const id = parseInt($(this).data('id'), 10);
            hajmValues += id + '_' + v + ',';
        }
    });



    var vardata = {
        FromKm: parseFloat($('#txtFromKMForKhakRizi').val().replace('+', '')),
        ToKm: parseFloat($('#txtToKMForKhakRizi').val().replace('+', '')),

        RoadTypeId: parseInt($('input[name="roadType"]:checked').val(), 10),
        NoeDaneBandiId: parseInt($('input[name="noeDaneBandi"]:checked').val(), 10),

        // تغییر اصلی: به‌جای Id/Value تکی، مجموعه را می‌فرستیم
        HajmKhakRiziValues: hajmValues,  // ← لیست {Id, Value}

        BarAvordUserId: barAvordUserId,
        Year: Year = parseInt($('#HDFYear').val())
    };

    $.ajax({
        type: "POST",
        url: "/KhakRizi/SaveKhakRiziInfoForBarAvord",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('درج بدرستی صورت گرفت', 'موفقیت');
                GetExistKhakRizi(barAvordUserId, info[1]);
                $('#divExistKhakRiziD' + info[1]).slideDown();
                $('#divKhakRiziEzafeBaha' + info[1]).show();
                ClearContextNew();
            }

            //$('#divViewKhakRizi').slideUp();
        },
        error: function () {
            toastr.error('مشکل در ثبت اطلاعات کیلومتراژ', 'خطا');
        }
    });
}

function ClearContextNew() {
    $('#txtFromKMForKhakRizi').val('000+000');
    $('#txtToKMForKhakRizi').val('000+000');

    $('input[name="roadType"]').prop('checked', false);
    $('input[name="noeDaneBandi"]').prop('checked', false);

    $('.hajm-input').each(function () {
        this.value = '';
    });
    $('#headerNew').click();
}


function GetExistKhakRizi(BarAvordUserId, num) {
    const vardata = {
        BarAvordId: BarAvordUserId,
    };

    $.ajax({
        type: "POST",
        url: "/KhakRizi/GetExistKhakRizi",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            lstKhakRizi = data.lstKhakRizi;
            var str = '';


            $.each(lstKhakRizi, function () {
                str += `
<div id="divExistKhakRizi${this.kmNum}">
  <!-- هدر کیلومتراژ -->
  <div id="header${this.kmNum}" onclick="toggleKhakRiziDetails(${this.kmNum},'${BarAvordUserId}')">
  <div class="row mb-3 khak-header">
    <div class="col-auto d-flex align-items-center">
      <span>از کیلومتراژ:</span>
    </div>
    <div class="col-auto">
      <input type="text" class="form-control_1 input-sm khak-input"
             id="txtFromKMForKhakRizi${this.kmNum}" value="${this.fromKMSplit}"  onclick="event.stopPropagation();"/>
    </div>

    <div class="col-auto d-flex align-items-center">
      <span>تا کیلومتراژ:</span>
    </div>
    <div class="col-auto">
      <input type="text" class="form-control_1 input-sm khak-input"
             id="txtToKMForKhakRizi${this.kmNum}" value="${this.toKMSplit}"  onclick="event.stopPropagation();"/>
    </div>
  </div>
  </div>


  <div id="divExistKhakRiziD${this.kmNum}" style="display:none;border-bottom: 1px solid #d5bfff;margin-bottom: 10px;padding-bottom: 10px;">
  <div id="ulaSomeOpId${this.kmNum}">
    <div class="row">
        <div class="col-12 khak-card">
            <div class="row">
                <div class="col-md-4">
                    <span>
                    نوع راه
                    </span>
                </div>
                <div class="col-md-3">
                    <span>
                    نوع دانه بندی نوع خاک مصرفی در خاکریزی
                    </span>
                </div>
                 <div class="col-md-5">
                    <span>
                    حجم خاکریزی
                    </span>
                </div>
            </div>
        </div>
    </div>

            <div class="row" style="background-color: #ddd4ff;padding:5px;margin-bottom: 20px;">
                <div class="col-md-3">
                    <div id="col-roadType${this.kmNum}" class="khak-radios">
                    </div>
                </div>
                <div class="col-md-4">
                    <div id="col-noeDaneBandi${this.kmNum}" class="khak-radios">
                    </div>
                </div>
                 <div class="col-md-5">
                    <div id="col-hajmKhakRizi${this.kmNum}" class="khak-radios">
                    </div>
                </div>
            </div>
  </div>
  <!-- دکمه ذخیره پایین چپ -->
  
  <div id="divExistKhakRizi_RizMetre${this.kmNum}">
  </div>
<div class="row khak-save-row">
  <div class="col-10">
  </div>
    <div class="col-2 force-left">
      <a class="NewPolStyle btn buttonStyleBoard"
         onclick="UpdateKhakRiziInfo('${BarAvordUserId}',${this.kmNum})">ذخیره</a>
    </div>
  </div>
  <div id="divKhakRiziEzafeBaha${this.kmNum}">
  </div>
  </div>
  </div>
`;
                $('#ExistKhakRizi').html(str);



                $('#divExistKhakRizi' + this.kmNum).on('input change', '#txtFromKMForKhakRizi, #txtToKMForKhakRizi', function () {
                    // اختیاری: حذف فوریِ ظاهر blinking هنگام تایپ
                    $(this).removeClass('blinking');
                    // و سپس اعتبارسنجی کل جفت
                    validateKmFields();
                });

                $('#divExistKhakRizi' + this.kmNum).on('input change', '.hajm-input', function () {
                    const val = $(this).val();
                    // اگر مقدار خالی است: در حالت تایپ، blinking نداشته باشد
                    if (!val || String(val).trim() === '') { $(this).removeClass('blinking'); return; }
                    // اگر مقدار دارد ولی معتبر نیست: blinking بماند
                    const ok = isValidDecimal4(val);
                    $(this).toggleClass('blinking', !ok);
                });


                $('#divExistKhakRizi' + this.kmNum).on('change', 'input[name="hajmKhakRizi"]', function () {
                    $('.hajm-input').removeClass('blinking');          // همه را بردار
                    const $txt = $(`#${this.id}-txt`);
                    $txt.addClass('blinking').focus();
                });

                $('#divExistKhakRizi' + this.kmNum).on('input change', '.hajm-input', function () {
                    const ok = isValidDecimal4($(this).val());
                    $(this).toggleClass('blinking', !ok);
                });

                debugger;

                // لیست‌ها
                ShowRiziMetreKhakRizi(BarAvordUserId, this.kmNum);
            });

            if(num!=0)
                $('#header'+num).click();

            $.each(lstKhakRizi, function () {
                debugger;
                const ListRoadType = data.listRoadType || [];
                const ListNoeDaneBandi = data.listNoeDaneBandi || [];
                const ListHajmKhakRizi = data.listHajmKhakRizi || [];
                renderRadiosForEdit($('#col-roadType' + this.kmNum), 'roadType' + this.kmNum, ListRoadType, this, this.kmNum);
                renderRadiosForEdit($('#col-noeDaneBandi' + this.kmNum), 'noeDaneBandi' + this.kmNum, ListNoeDaneBandi, this, this.kmNum);
                renderHajmInputsForEdit($('#col-hajmKhakRizi' + this.kmNum), ListHajmKhakRizi, this, this.kmNum);
            });

        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری کیلومتراژ انتخابی', 'خطا');
        }
    });
}


function toggleKhakRiziDetails(num, BarAvordUserId) {
    debugger;

    var $div = $('#divExistKhakRiziD' + num);

    var $allDivs = $('[id^="divExistKhakRiziD"]');
    $allDivs.not($div).slideUp(500);

    if ($div.is(':visible')) {
        // اگه باز باشه فقط ببند
        $div.slideUp(500);
    } else {
        // اگه بسته باشه، تابع رو اجرا کن و بازش کن
        showKhakRiziDetails(num, BarAvordUserId);
    }
}
function toggleKhakRiziNew() {
    var $div = $('#divulaSomeOpId');

    if ($div.is(':visible')) {
        $div.slideUp(500);
    } else {
        $div.slideDown(500);
    }
}

function showKhakRiziDetails(num, BarAvordUserId) {
    $('#divExistKhakRiziD' + num).slideDown(500);

    Year = $('#HDFYear').val();

    const vardata = {
        BarAvordId: BarAvordUserId,
        Year: Year,
        Num: num
    };

    $.ajax({
        type: "POST",
        url: "/KhakRizi/GetEzafeBahaKhakRizi",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ezafeBahaKhakRizi = response.ezafeBahaKhakRizi;
            lstKhakRiziEzafeBahaBarAvord = response.lstKhakRiziEzafeBahaBarAvord;




            var $container = $('#divKhakRiziEzafeBaha' + num);
            $container.empty();

            var preselectedIds = new Set(
                (lstKhakRiziEzafeBahaBarAvord || []).map(x => Number(x.conditionContextId))
            );

            var pickedRadioByGroup = {};
            var lastSavedByGroup = {};

            var groups = {};
            ezafeBahaKhakRizi.forEach(function (it) {
                var key = String(it.conditionGroupId);
                if (!groups[key]) groups[key] = [];
                groups[key].push(it);
            });

            //دستور جاوا اسکریپت
            Object.keys(groups).forEach(function (key) {
                var group = groups[key];

                // اگر گروه فقط 1 آیتم دارد => چک‌باکس
                if (group.length === 1) {
                    var it = group[0];
                    var inputId = "chk_" + num + "_" + it.id;

                    var $wrap = $('<div class="single-checkbox" style="margin:6px 0;padding:10px;border:1px solid #ddd;border-radius:10px;background-color:#eee7ff;"></div>');
                    var $input = $('<input type="checkbox" class="eb-checkbox">')
                        .attr({ id: inputId, value: it.id })
                        .data("id", it.id);

                    // لیبل با data-type و data-id برای هندلرها
                    var $label = $('<label data-type="checkbox"></label>')
                        .attr("for", inputId).data("id", it.id).text(it.context);

                    // div مخفی زیر لیبل
                    var $panel = $('<div class="ebkhakrizi-panel" style="display:none; padding:8px; border:1px dashed #ccc; border-radius:8px; margin:6px 0 0 0;"></div>')
                        .attr("id", "divEBKhakRizi_" + num + "_" + it.id)
                        .text("");

                    $wrap.append($input).append(" ").append($label).append($panel);
                    $container.append($wrap);

                    if (preselectedIds.has(Number(it.id))) {
                        $input.prop('checked', true);
                        $panel.show(); // اگر باید با تیک باز شود
                        // اگر هندلر change دارید و می‌خواهید آن هم اجرا شود:
                        // $input.trigger('change');

                        LoadRizMetreEzafeBahaKhakRizi(it.id, BarAvordUserId, num);
                    }

                } else {
                    // چند آیتم با ConditionGroupId یکسان => گروه رادیویی
                    var groupName = "cg_" + key;
                    var groupLabel = group[0].groupContext || ("گروه " + key);

                    var $fs = $(`
                        <fieldset class="radio-group" style="margin:12px 0; padding:10px; border:1px solid #ddd; border-radius:10px;background-color:#eee7ff;">
                        <div class="eb-group-header" style="display:flex; align-items:center; gap:8px; margin-bottom:6px;">
                            <input type="checkbox" class="eb-group-checkbox" id="grpchk_${num}_${key}" data-group="${groupName}">
                            <span class="eb-group-title" data-group="${groupName}" style="font-weight:600; user-select:none; cursor:default;"></span>
                        </div>
                        </fieldset>
                        `);

                    $fs.find(".eb-group-title").text(groupLabel);


                    group.forEach(function (it) {
                        var inputId = "rd_" + num + "_" + it.id;
                        var $row = $('<div style="margin:6px 0;"></div>');

                        var $input = $('<input type="radio" class="eb-radio">')
                            .attr({ id: inputId, name: groupName, value: it.id })
                            .data("id", it.id)
                            .data("group", groupName);

                        var $label = $('<label data-type="radio"></label>')
                            .attr("for", inputId).data("id", it.id).text(it.context);

                        var $panel = $('<div class="ebkhakrizi-panel" style="display:none; padding:8px; border:1px dashed #ccc; border-radius:8px; margin:6px 0 0 0;"></div>')
                            .attr("id", "divEBKhakRizi_" + num + "_" + it.id)
                            .text("");

                        $row.append($input).append(" ").append($label).append($panel);
                        $fs.append($row);
                    });

                    $container.append($fs);

                    var prePick = group.find(it => preselectedIds.has(Number(it.id)));
                    if (prePick) {
                        var $inp = $("#rd_" + num + "_" + prePick.id);
                        var $panel = $("#divEBKhakRizi_" + num + "_" + prePick.id);

                        $inp.prop('checked', true);
                        $panel.show();
                        pickedRadioByGroup[groupName] = prePick.id;
                        lastSavedByGroup[groupName] = prePick.id;

                        // چک‌باکس هدر گروه را تیک بزن
                        $("#grpchk_" + num + "_" + key).prop("checked", true);

                        LoadRizMetreEzafeBahaKhakRizi(prePick.id, BarAvordUserId, num);
                    }
                }
            });

            $container.on("click", "label[data-type='checkbox']", function (e) {
                e.preventDefault();
                var id = $(this).data("id");
                var $chk = $("#chk_" + num + "_" + id);
                var $panel = $("#divEBKhakRizi_" + num + "_" + id);

                if (!$chk.prop("checked")) {
                    $chk.prop("checked", true);
                    SaveEBKhakRizi(id, BarAvordUserId, num);

                    $('[id^="divEBKhakRizi_' + num + '"]').slideUp(500);
                } else {
                    if ($panel.is(":visible")) {
                        $panel.slideUp(150);
                    } else {
                        LoadRizMetreEzafeBahaKhakRizi(id, BarAvordUserId, num);
                        $panel.slideDown(150);
                    }
                }
            });

            $container.on("click", "input.eb-checkbox", function (e) {
                debugger;
                var id = $(this).data("id");
                var $chk = $(this);

                // وضعیت فعلی قبل از تغییر (با توجه به اینکه مرورگر تغییر داده)
                var wasChecked = $chk.is(":checked");

                if (!wasChecked) {
                    DeleteEBKhakRizi(id, BarAvordUserId, num);
                }
                else {
                    SaveEBKhakRizi(id, BarAvordUserId, num);
                    LoadRizMetreEzafeBahaKhakRizi(id, BarAvordUserId, num);
                }
            });


            // --- چک‌باکس هدر گروه ---
            $container.on("click", "input.eb-group-checkbox", function (e) {
                debugger;
                var $chk = $(this);
                var groupName = $chk.data("group");

                // وضعیت فعلی قبل از تغییر (با توجه به اینکه مرورگر تغییر داده)
                var wasChecked = $chk.is(":checked");

                if (!wasChecked) {
                    // یعنی کاربر داشت تیک‌دار رو برمی‌داشت → پس باید Delete انجام بشه
                    //e.preventDefault();
                    //e.stopImmediatePropagation();

                    var lastId = pickedRadioByGroup[groupName] || lastSavedByGroup[groupName];

                    $("input.eb-radio[name='" + groupName + "']").prop("checked", false);
                    $('[id^="divEBKhakRizi_'+num+'"]').slideUp(300);
                    $chk.prop("checked", false);

                    if (lastId) {
                        DeleteEBKhakRizi(lastId, BarAvordUserId, num);
                    }

                    delete pickedRadioByGroup[groupName];
                    delete lastSavedByGroup[groupName];
                } else {
                    // اگر قبلاً خالی بود → نذار تیک بخوره
                    e.preventDefault();
                    e.stopImmediatePropagation();
                    $chk.prop("checked", false);
                }
            });


            // کلیک روی لیبل رادیو
            $container.on("click", "label[data-type='radio']", function () {
                var id = $(this).data("id");
                var $input = $("#rd_" + num + "_" + id);
                var $panel = $("#divEBKhakRizi_" + num + "_" + id);

                if ($input.prop("checked")) {
                    if ($panel.is(":visible")) {
                        // اگر باز است، ببند
                        $panel.stop(true, true).slideUp(150);
                    } else {
                        // اگر بسته است، باز کن
                        LoadRizMetreEzafeBahaKhakRizi(id, BarAvordUserId, num);
                        $panel.stop(true, true).slideDown(150);
                    }
                }
            });


            $container.on("change", "input.eb-radio", function () {
                var id = $(this).data("id");
                var groupName = $(this).data("group");

                // ذخیره انتخاب
                SaveEBKhakRizi(id, BarAvordUserId, num);
                pickedRadioByGroup[groupName] = id;
                lastSavedByGroup[groupName] = id;

                // بستن پنل‌های دیگر
                $('[id^="divEBKhakRizi_'+num+'"]').slideUp(500);

                // چک‌باکس هدر گروه را تیک بزن
                var key = groupName.replace("cg_"+num, ""); // استخراج key
                $("#grpchk_" + num + "_" + key).prop("checked", true);
            });



        }, error: function (response) {
            toastr.error('مشکل در بارگذاری اضافه بها خاک ریزی', 'خطا');
        }
    });
}

function LoadRizMetreEzafeBahaKhakRizi(ConditionContextId, BarAvordUserId, num) {

    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    debugger;
    const vardata = {
        BarAvordId: BarAvordUserId,
        NoeFB: NoeFB,
        Year: Year,
        ConditionContextId: ConditionContextId,
        Num: num
    };
    $.ajax({
        type: "POST",
        url: "/KhakRizi/GetRizMetreEzafeBahaForKhakRizi",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;
            lstkhakRiziEzafeBahaRizMetre = response.lstkhakRiziEzafeBahaRizMetre;

            var str = '';

            // ساخت HTML بر اساس lstItemFBShomarehForGet

            // گروه‌بندی data.lst بر اساس itemFBShomareh
            let groupedData = {};
            lstkhakRiziEzafeBahaRizMetre.forEach(function (row) {
                if (!groupedData[row.itemFBShomareh]) {
                    groupedData[row.itemFBShomareh] = [];
                }
                groupedData[row.itemFBShomareh].push(row);
            });

            // ساخت HTML بر اساس lstItemFBShomarehForGet
            if (lstkhakRiziEzafeBahaRizMetre.length != 0) {

                lstItemFBShomarehForGet.forEach(function (itemGroup) {

                    let itemFBShomareh = itemGroup.itemFBShomareh;
                    let des = itemGroup.des;
                    let ItemFields = itemGroup.itemFields;

                    let rows = groupedData[itemFBShomareh] || [];
                    if (rows.length != 0) {
                        // نمایش توضیح گروه
                        str += "<div class='col-12'><span style='color:#000'>" + itemGroup.itemFBShomareh + " - " + des + "</span></div>";

                        str += "<div class=\"row col-12 styleHeaderTable\" style=\"text-align: center;background-color: #c4bfe3;\">";
                        str += "<div class=\"col-md-1 spanStyleMitraSmall\" style=\"text-align:center\"><span id=\"spanFieldShomarehName\">ردیف</span></div>";
                        str += "<div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">تعداد</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[0] != undefined ? ItemFields[0].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">طول</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[1] != undefined ? ItemFields[1].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">عرض</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[2] != undefined ? ItemFields[2].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">ارتفاع</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[3] != undefined ? ItemFields[3].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">وزن</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[4] != undefined ? ItemFields[4].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\"><span>مقدار جزء</span></div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[5] != undefined ? ItemFields[5].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-2 spanStyleMitraSmall\">توضیحات</div>";
                        str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div>";
                        str += "</div>";
                        str += "</div>";

                        str += "<div class='col-12' style='overflow:auto;max-height:400px;padding: 0px;'>";

                        // نمایش ردیف‌های مربوطه
                        rows.forEach(function (row) {
                            let id = row.id;

                            let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
                            let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
                            let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
                            let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
                            let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
                            let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();


                            let HasDelButton = row.hasDelButton;
                            let HasEditButton = row.hasEditButton;

                            str += "<div class='row styleRowTable' style=\"background-color:#fff\" onclick=\"RizMetreSelectClick('" + id + "')\">";
                            str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span>" + row.shomareh + "</span></div>";

                            str += "<div class='col-md-2'><input  type='text'" +
                                " class='form-control spanStyleMitraSmall' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTedad" + id + "' value = '" + strTedad + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTool" + id + "' value='" + strTool + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtArz" + id + "' value='" + strArz + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtErtefa" + id + "' value='" + strErtefa + "'/></div>";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtVazn" + id + "' value='" + strVazn + "'/></div>";

                            str += "<div class='col-md-1 RMMJozStyle'><span id='MeghdarJoz" + id + "'>" + MeghdarJoz + "</span></div>";

                            str += "<div class='col-md-2'><input  type='text' title='" + row.des + "' style='font-size:12px' class='form-control input-sm' id='txtDes" + id + "' value='" + row.des + "'/></div>";
                            if (HasDelButton)
                                str += "<div class='col-md-1' style='text-align:center;'><button><i class='fa fa-trash DelRMUStyle' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i></button></div>";
                            if (HasEditButton)
                                str += "<div class='col-md-1'><button type='button' id='iUpdate" + id + "' onclick=\"UpdateRMKhakRiziAddedItemsClick('" + id + "')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button></div>";

                            str += "</div>";
                        });
                        str += "</div>";

                    }
                });


                $targetDivRizMetreKH = $('#divEBKhakRizi_' + num + "_" + ConditionContextId);

                $targetDivRizMetreKH.html(str);

                //$targetDivRizMetreKH.slideDown();



                $targetDivRizMetreKH.find("input[type='text'].HasEnteringValue")
                    .filter(function () {
                        return $(this).val().trim() === "";
                    })
                    .addClass("blinking")
                    .first()
                    .focus();


                $targetDivRizMetreKH.on("change", "input[type='text'].HasEnteringValue", function () {
                    if ($(this).val().trim() !== "") {
                        $(this).removeClass("blinking");
                    }
                });


                $targetDivRizMetreKH.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
                    /* ENTER PRESSED */
                    if (e.keyCode == 13) {
                        /* FOCUS ELEMENT */
                        var inputs = $(this).parent().parent().find("input[Type=text].HasEnteringValue,button");
                        var idx = inputs.index(this);
                        if (idx == inputs.length - 1) {
                            inputs[0].focus();
                            inputs[0].select();
                        } else {
                            while (inputs[idx + 1].disabled == true) {
                                idx++;
                            }
                            inputs[idx + 1].focus(); //  handles submit buttons
                            inputs[idx + 1].select();
                        }
                        return false;
                    }
                });
            }

        }, error: function (response) {
            toastr.error('مشکل در بارگذاری ریزمتره اضافه بها', 'خطا');
        }
    });
}

function ShowRiziMetreKhakRizi(BarAvordUserId, num) {
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();

    const vardata = {
        BarAvordId: BarAvordUserId,
        NoeFB: NoeFB,
        Year: Year,
        Num: num
    };

    $.ajax({
        type: "POST",
        url: "/KhakRizi/GetRizMetreForKhakRizi",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            lstAKhInfoRizMetre = response.rizMetreUsers;
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;

            var str = '';

            // ساخت HTML بر اساس lstItemFBShomarehForGet

            // گروه‌بندی data.lst بر اساس itemFBShomareh
            let groupedData = {};
            lstAKhInfoRizMetre.forEach(function (row) {
                if (!groupedData[row.itemFBShomareh]) {
                    groupedData[row.itemFBShomareh] = [];
                }
                groupedData[row.itemFBShomareh].push(row);
            });

            // ساخت HTML بر اساس lstItemFBShomarehForGet
            if (lstAKhInfoRizMetre.length != 0) {

                lstItemFBShomarehForGet.forEach(function (itemGroup) {

                    let itemFBShomareh = itemGroup.itemFBShomareh.substring(0, 6);
                    let des = itemGroup.des;
                    let ItemFields = itemGroup.itemFields;

                    let rows = groupedData[itemFBShomareh] || [];
                    if (rows.length != 0) {
                        // نمایش توضیح گروه
                        str += "<div class='col-12'><span style='color:#000'>" + itemGroup.itemFBShomareh + " - " + des + "</span></div>";

                        str += "<div class=\"row col-12 styleHeaderTable\" style=\"text-align: center;background-color: #c4bfe3;\">";
                        str += "<div class=\"col-md-1 spanStyleMitraSmall\" style=\"text-align:center\"><span id=\"spanFieldShomarehName\">ردیف</span></div>";
                        str += "<div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">تعداد</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[0] != undefined ? ItemFields[0].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">طول</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[1] != undefined ? ItemFields[1].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">عرض</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[2] != undefined ? ItemFields[2].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">ارتفاع</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[3] != undefined ? ItemFields[3].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\">وزن</div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[4] != undefined ? ItemFields[4].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                        str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #84d4e6\"><span>مقدار جزء</span></div>";
                        str += "<div class=\"VahedStyle\">" + (ItemFields[5] != undefined ? ItemFields[5].vahed : "") + "</div>";
                        str += "</div>";

                        str += "<div class=\"col-md-2 spanStyleMitraSmall\">توضیحات</div>";
                        str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div>";
                        str += "</div>";
                        str += "</div>";

                        str += "<div class='col-12' style='overflow:auto;max-height:400px;padding: 0px;'>";

                        // نمایش ردیف‌های مربوطه
                        rows.forEach(function (row) {

                            let id = row.id;

                            let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
                            let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
                            let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
                            let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
                            let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
                            let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();


                            let HasDelButton = row.hasDelButton;
                            let HasEditButton = row.hasEditButton;

                            str += "<div class='row styleRowTable' style=\"background-color:#fff\" onclick=\"RizMetreSelectClick('" + id + "')\">";
                            str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span>" + row.shomareh + "</span></div>";

                            str += "<div class='col-md-2'><input  type='text'" +
                                " class='form-control spanStyleMitraSmall' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTedad" + id + "' value = '" + strTedad + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTool" + id + "' value='" + strTool + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtArz" + id + "' value='" + strArz + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtErtefa" + id + "' value='" + strErtefa + "'/></div>";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtVazn" + id + "' value='" + strVazn + "'/></div>";

                            str += "<div class='col-md-1 RMMJozStyle'>" + MeghdarJoz + "</div>";

                            str += "<div class='col-md-2'><input  type='text' title='" + row.des + "' style='font-size:12px' class='form-control input-sm' id='txtDes" + id + "' value='" + row.des + "'/></div>";
                            if (HasDelButton)
                                str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-trash DelRMUStyle' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i></div>";
                            if (HasEditButton)
                                str += "<button type='button' id='iUpdate" + id + "' onclick=\"UpdateRMKhakRiziAddedItemsClick('" + id + "')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>";

                            str += "</div>";
                        });
                        str += "</div>";

                    }
                });

                $targetDivRizMetreKH = $('#divExistKhakRizi_RizMetre' + num);

                $targetDivRizMetreKH.html(str);

                $targetDivRizMetreKH.slideDown();



                $targetDivRizMetreKH.find("input[type='text'].HasEnteringValue")
                    .filter(function () {
                        return $(this).val().trim() === "";
                    })
                    .addClass("blinking")
                    .first()
                    .focus();


                $targetDivRizMetreKH.on("change", "input[type='text'].HasEnteringValue", function () {
                    if ($(this).val().trim() !== "") {
                        $(this).removeClass("blinking");
                    }
                });


                $targetDivRizMetreKH.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
                    /* ENTER PRESSED */
                    if (e.keyCode == 13) {
                        /* FOCUS ELEMENT */
                        var inputs = $(this).parent().parent().find("input[Type=text].HasEnteringValue,button");
                        var idx = inputs.index(this);
                        if (idx == inputs.length - 1) {
                            inputs[0].focus();
                            inputs[0].select();
                        } else {
                            while (inputs[idx + 1].disabled == true) {
                                idx++;
                            }
                            inputs[idx + 1].focus(); //  handles submit buttons
                            inputs[idx + 1].select();
                        }
                        return false;
                    }
                });
            }


        }, error: function (response) {
            toastr.error('مشکل در بارگذاری کیلومتراژ انتخابی', 'خطا');
        }
    });
}

function DeleteEBKhakRizi(EBId, BarAvordUserId, Num) {
    debugger;
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.ConditionContextId = EBId;
    vardata.Num = Num;
    $.ajax({
        type: "POST",
        url: "/KhakRizi/DeleteEBKhakRizi",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('حذف بدرستی صورت گرفت', 'موفقیت');
            }
        },
        error: function (response) {
            toastr.error('مشکل در حذف', 'خطا');
        }
    });

}

function SaveEBKhakRizi(EBId, BarAvordUserId, num) {
    Year = $('#HDFYear').val();
    debugger;
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.ConditionContextId = EBId;
    vardata.Year = Year;
    vardata.Num = num;

    $.ajax({
        type: "POST",
        url: "/KhakRizi/SaveEBKhakRizi",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            toastr.success('اضافه بها خاکریزی بدرستی درج شد', 'موفقیت');

            LoadRizMetreEzafeBahaKhakRizi(EBId, BarAvordUserId, num)

            $("#divEBKhakRizi_" + num + "_" + EBId).slideDown(500);
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اضافه بها خاکریزی', 'خطا');
        }
    });
}

