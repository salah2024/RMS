function renderRadiosForEdit($container, groupName, items, KhakRizi) {

    debugger;

    $container.empty();

    if (!Array.isArray(items) || items.length === 0) {
        $container.append($('<div/>', { class: 'text-muted small', text: 'داده‌ای یافت نشد' }));
        return;
    }

    NoeDaneBandi = KhakRizi.noeDaneBandi;
    NoeRah = KhakRizi.noeRah;


    const uid = Math.random().toString(36).slice(2, 8);

    items.forEach((item, idx) => {
        // پشتیبانی از هر دو حالت PascalCase/camelCase
        const val = (item.id ?? item.Id ?? idx);
        const lbl = (item.description ?? item.Description ?? '').toString();
        const id = `${groupName}-${uid}-${val}`;

        if (groupName == 'roadType') {
            $input = $('<input/>', {
                class: 'form-check-input',
                type: 'radio',
                id,
                name: groupName,
                value: val,
                checked: (val == NoeRah)
            });
        }
        else {
            $input = $('<input/>', {
                class: 'form-check-input',
                type: 'radio',
                id,
                name: groupName,
                value: val,
                checked: (val == NoeDaneBandi)
            });
        }


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



function renderHajmInputsForEdit($container, items, KhakRizi, num) {
    $container.empty();
    if (!Array.isArray(items) || items.length === 0) {
        $container.append($('<div/>', { class: 'text-muted small', text: 'داده‌ای یافت نشد' }));
        return;
    }

    noeHajmKhakRizi_Value = KhakRizi.noeHajmKhakRizi_Value;

    noeHajmKhakRizi_ValueSplit = noeHajmKhakRizi_Value.split(',');

    items.forEach((item, idx) => {
        debugger;

        const idVal = item.id ?? item.Id ?? idx;
        const text = (item.description ?? item.Description ?? '').toString();
        const tid = `txtHajmKhRizi_${num}`;

        // col-10: متن توضیح آیتم
        const $colLabel = $('<div/>', { class: 'col-9 d-flex align-items-center' })
            .append($('<span/>', { text: text }));

        currentValue = 0;
        noeHajmKhakRizi_ValueSplit.forEach(item1 => {
            debugger;
            itemSplit = item1.split('_');
            currentid = itemSplit[0];
            currentHajm = itemSplit[1];
            if (idVal == currentid) {
                currentValue = currentHajm;
            }
        });


        // col-2: ورودی + لیبل «مترمکعب» (بدون کادر)
        const $txt = $('<input/>', {
            id: tid,
            type: 'text',
            class: 'form-control_1 form-control-sm hajm-input',
            'data-id': idVal,
            value: currentValue
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


function validateKmFieldsForEdit(num) {
    const $from = $('#txtFromKMForKhakRizi' + num);
    const $to = $('#txtToKMForKhakRizi' + num);

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

// --- بررسی انتخاب رادیوهای هر گروه
function validateRadioGroupsForEdit(num) {
    let ok = true;
    ['roadType' + num, 'noeDaneBandi' + num].forEach(g => {
        const selected = $(`input[name="${g}"]:checked`).length > 0;
        markGroupInvalid(g, !selected); // همان تابعی که radio-invalid می‌گذاشت
        if (!selected) ok = false;
    });
    return ok;
}

function validateHajmInputsOnEdit(num) {
    const $inputs = $('[id^="txtHajmKhRizi' + num + '"]');
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


function UpdateKhakRiziInfo(barAvordUserId, num) {
    const kmOk = validateKmFieldsForEdit(num);
    const radiosOk = validateRadioGroupsForEdit(num);  // برای roadType و noeDaneBandi
    const hajmOk = validateHajmInputsOnEdit(num);

    if (!(kmOk && radiosOk && hajmOk)) {
        toastr.error('لطفاً خطاها را برطرف کنید.', 'خطا');
        const $firstErr = $('.blinking, .radio-invalid').first();
        if ($firstErr.length) { $('html, body').animate({ scrollTop: $firstErr.offset().top - 120 }, 300); }
        return;
    }

    // گردآوری مقادیر حجم‌ها (فقط آن‌هایی که مقدار دارند)
    var hajmValues = '';
    $('.hajm-input').each(function () {
        const raw = ($(this).val() || '').trim();
        if (raw !== '') {
            const v = normalizeDecimal4(raw);
            const id = parseInt($(this).data('id'), 10);
            hajmValues = id + '_' + v + ',';
        }
    });



    var vardata = {
        FromKm: parseFloat($('#txtFromKMForKhakRizi' + num).val().replace('+', '')),
        ToKm: parseFloat($('#txtToKMForKhakRizi' + num).val().replace('+', '')),

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

            GetExistKhakRizi(barAvordUserId);
        },
        error: function () {
            toastr.error('مشکل در ثبت اطلاعات کیلومتراژ', 'خطا');
        }
    });
}

