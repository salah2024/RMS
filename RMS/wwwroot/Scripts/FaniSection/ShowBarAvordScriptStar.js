function EditRizMetreRowStar(rowEl, rizMetreId, Shomareh, rizId, Code) {

    debugger;

    const $row = $(rowEl);
    if ($row.hasClass('editing')) {
        UpdateRizMetreFromRowStar($row, rizMetreId, Shomareh, rizId, Code);
        return;
    }
    //if ($row.hasClass('editing')) return;

    // بستن سایر ردیف‌های در حال ویرایش
    $('.riz-row-data.editing').each(function () {
        UpdateRizMetreFromRowStar($(this), rizMetreId, Shomareh, rizId, Code);

        return;
        //cancelEditRow($(this), rizMetreId, Shomareh, rizId, FBId);
    });

    $row.addClass('editing');

    $row.find('td').each(function (index) {

        const $td = $(this);
        const text = $td.find('span').html();

        if (index === 0 || index === 7) return;

        if (index === 2 || index === 3 || index === 4 || index === 5 || index === 6) {

            // ساخت input با تنظیمات پیش‌فرض
            let inputHtml = `<input class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}"`;

            inputHtml += '>'; // بستن تگ input
            $td.html(inputHtml);

        }
        else {
            let inputHtml = `<input class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}" />`;
            $td.html(inputHtml);
        }
    });

    // جایگزینی آیکن عملیات با دکمه ذخیره
    const $actionCell = $row.find('td').eq(9);
    $actionCell.html(`
    <button type="button"
      onclick="event.stopPropagation();UpdateRizMetreFromRowStar($(this),'${rizMetreId}','${Shomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
	`);

    $row.find('td').eq(1).find('input').focus().select();
}



function UpdateRizMetreFromRowStar(el, rizMetreId, Shomareh, rizId1, Code) {
    const row = $(el).closest('tr');

    const rizId = row.data('id');

    const inputs = row.find('input');
    const MeghdarJoz = row.find('#spanMeghdarJoz');

    Check = false;
    const values = inputs.map(function () {
        if ($(this).hasClass('essentialValue')) {
            currentValue = convertPersianToEnglish($(this).val().replace(/\,/g, ''));
            if ($.isNumeric(parseFloat(currentValue))) {
                $(this).removeClass('blinking');
                return currentValue;
            }
            else {
                $(this).addClass('blinking');
                Check = true;
            }
        }
        else return convertPersianToEnglish($(this).val().replace(/\,/g, ''));

        //return $(this).val().trim();
    }).get();

    if (Check) {
        toastr.info('اطلاعات وارد شده، صحیح نمی باشند', 'اطلاع');
    }
    else {

        var meghdarFasl = $('#rowFasl-' + rizId1 + ' #meghdarFasl');
        var bahayeKolFasl = $('#rowFasl-' + rizId1 + ' #bahayeKolFasl');
        var bahayeVahedFasl = $('#rowFasl-' + rizId1 + ' #bahayeVahedFasl');

        const tdJameFasl = $('#td-' + Code + '-jameFasl');
        const tdJameFaslBaZarib = $('#td-' + Code + '-jameFaslBaZarib');

        var vardata = new Object();
        vardata.Id = rizId;
        vardata.Sharh = values[0];
        vardata.Tedad = values[1] != "" ? values[1] : null;
        vardata.Tool = values[2] != "" ? values[2] : null;
        vardata.Arz = values[3] != "" ? values[3] : null;
        vardata.Ertefa = values[4] != "" ? values[4] : null;
        vardata.Vazn = values[5] != "" ? values[5] : null;
        vardata.Des = values[6];
        vardata.Code = Code;
        $.ajax({
            type: "POST",
            url: '/ItemFBStar/UpdateRizMetreItemStarFrmShowBarAvord',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {
                    row.removeClass('editing');

                    let num = parseFloat(info[1]);
                    let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text()));

                    meghdarFasl.html(formatNumber(info[2]));
                    bahayeKolFasl.html(formatNumber((bahayeVahedFasl1 * info[2]).toFixed(0)));
                    MeghdarJoz.html(parseFloat(num.toFixed(2)));

                    tdJameFasl.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    tdJameFaslBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    //GetRizMetreUsers();
                    toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                    cancelEditRowStar(row, rizMetreId, Shomareh, rizId1);
                }
                else
                    toastr.info('مشکل در ویرایش ریزه متره انتخابی', 'اطلاع');
            },
            error: function (msg) {
                toastr.error('مشکل در ویرایش ریزه متره انتخابی', 'خطا');
            }
        });

    }
}



function SaveRMUStarClick(object, Shomareh, rizId, Code) {
    object.parent().parent().find('input[type=text]').each(function () {
        $(this).removeClass('ErrorValueStyle');
    });

    const row = $(object).closest('tr');
    const tdJameFasl = $('#td-' + Code + '-jameFasl');
    const tdJameFaslBaZarib = $('#td-' + Code + '-jameFaslBaZarib');

    const MeghdarJoz = row.find('#spanMeghdarJoz');

    var meghdarFasl = $('#rowFasl-' + rizId + ' #meghdarFasl');
    var bahayeKolFasl = $('#rowFasl-' + rizId + ' #bahayeKolFasl');
    var bahayeVahedFasl = $('#rowFasl-' + rizId + ' #bahayeVahedFasl');

    var txtBahayeVahed = $('#txtBahayeVahed-' + rizId);

    debugger;
    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    firstObj = null;
    findFirstObj = false;
    object.parent().parent().find('input[type=text]').each(function () {
        if ($(this).attr('id') == 'txtSharh')
            Sharh = $(this).val();
        else if ($(this).attr('id') == 'txtTedad') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Tedad = $(this).val().replace(/\,/g, '');
                $(this).removeClass('blinking');

            }
            else {
                $(this).addClass('blinking');
                Check = false;
                if (!findFirstObj) {
                    firstObj = $(this);
                    findFirstObj = true;
                }
            }
        }
        else if ($(this).attr('id') == 'txtTool') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Tool = $(this).val().replace(/\,/g, '');
                $(this).removeClass('blinking');

            }
            else {
                $(this).addClass('blinking');
                Check = false;
                if (!findFirstObj) {
                    firstObj = $(this);
                    findFirstObj = true;
                }
            }
        }
        else if ($(this).attr('id') == 'txtArz') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Arz = $(this).val().replace(/\,/g, '');
                $(this).removeClass('blinking');

            }
            else {
                $(this).addClass('blinking');
                Check = false;
                if (!findFirstObj) {
                    firstObj = $(this);
                    findFirstObj = true;
                }
            }
        }
        else if ($(this).attr('id') == 'txtErtefa') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Ertefa = $(this).val().replace(/\,/g, '');
                $(this).removeClass('blinking');

            }
            else {
                $(this).addClass('blinking');
                Check = false;
                if (!findFirstObj) {
                    firstObj = $(this);
                    findFirstObj = true;
                }
            }
        }
        else if ($(this).attr('id') == 'txtVazn') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Vazn = $(this).val().replace(/\,/g, '');
                $(this).removeClass('blinking');
            }
            else {
                $(this).addClass('blinking');
                Check = false;
                if (!findFirstObj) {
                    firstObj = $(this);
                    findFirstObj = true;
                }
            }
        }
        else if ($(this).attr('id') == 'txtDes')
            Des = $(this).val();
    });

    if (Check) {
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        Year = parseInt($('#HDFYear').val());

        debugger;

        var vardata = new Object();
        vardata.Sharh = Sharh;
        vardata.Tedad = Tedad === undefined ? null : Tedad;
        vardata.Tool = Tool === undefined ? null : Tool;
        vardata.Arz = Arz === undefined ? null : Arz;
        vardata.Ertefa = Ertefa === undefined ? null : Ertefa;
        vardata.Vazn = Vazn === undefined ? null : Vazn;
        vardata.Des = Des;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Shomareh = Shomareh;
        vardata.Year = Year;
        $.ajax({
            type: "POST",
            url: '/ItemFBStar/ConfirmRizMetreItemFBStar',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {

                    //let num = parseFloat(info[2]);
                    //let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                    //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(txtBahayeVahed.val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                    //meghdarFasl.html(formatNumber(info[3]));
                    //bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * info[3]));
                    //MeghdarJoz.html(num);
                    //tdJameFasl.html(formatNumber(parseFloat(info[4]).toFixed(0)));
                    //tdJameFaslBaZarib.html(formatNumber(parseFloat(info[4]).toFixed(0)));

                    //inputBahayeVahed = $('#txtBahayeVahed-' + rizId);
                    //const val = parseFloat(inputBahayeVahed.val().replace(/,/g, '')); // حذف کاما و تبدیل به عدد
                    //if (val === 0) {
                    //    inputBahayeVahed.addClass('blinking');
                    //} else {
                    //    inputBahayeVahed.removeClass('blinking'); // اگر نمی‌خواهید در غیر صفر بودن کلاس باقی بماند
                    //}

                    GetCurrentRizMetreItemFBStar(Shomareh, rizId, Code);

                    toastr.success('ریزه متره جدید بدرستی درج گردید', 'موفقیت');
                }
                else
                    toastr.info('مشکل در درج ریزه متره جدید', 'اطلاع');
            },
            error: function (msg) {
                toastr.error('مشکل در درج ریزه متره جدید', 'خطا');
            }
        });
    }
    else {
        firstObj.focus().select();
        toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
    }
}



//ریزه متره های ستاره دار
function GetCurrentRizMetreItemFBStar(Shomareh, rizId, Code) {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.ItemFBShomareh = Shomareh;
    $.ajax({
        url: "/ItemFBStar/GetCurrentRizMetreStarForShowBarAvord",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            rizMetreUsers = response;
            debugger;
            const rizRow = $(`#${rizId}`);
            const tbody = rizRow.find('tbody');
            tbody.empty(); // پاک‌کردن محتوای قدیمی

            let rizRowsHtml = '';

            if (Array.isArray(rizMetreUsers) && rizMetreUsers.length > 0) {
                rizRowsHtml += rizMetreUsers.map(riz => `
                    <tr class="riz-row-data" data-id="${riz.id}" onclick="EditRizMetreRowStar(this,'${riz.id}','${Shomareh}','${rizId}','${Code}')" style="cursor:pointer;">
					<td style="text-align:center">${riz.shomarehNew}</td>
					<td><span>${riz.sharh}</span></td>
					<td style="text-align:center"><span>${riz.tedad == null ? '' : riz.tedad}</span></td>
					<td style="text-align:center"><span>${riz.tool == null ? '' : riz.tool}</span></td>
					<td style="text-align:center"><span>${riz.arz == null ? '' : riz.arz}</span></td>
					<td style="text-align:center"><span>${riz.ertefa == null ? '' : riz.ertefa}</span></td>
					<td style="text-align:center"><span>${riz.vazn == null ? '' : riz.vazn}</span></td>
					<td style="text-align:center"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : riz.meghdarJoz}</span></td>
					<td><span>${riz.des ?? ''}</span></td>
                    <td style="text-align:center"><i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetreStar('${riz.id}','${Shomareh}','${rizId}','${Code}')"></i></td>
                    </tr>
                `).join('');
            }




            rizRowsHtml += `
<tr class="riz-input-row">
  <td style="text-align:center"></td>
  <td><input id="txtSharh" class="form-control form-control-sm" type="text" /></td>

  <td style="text-align:center">
    <input id="txtTedad" class="" type="text" />
  </td>

  <td style="text-align:center">
    <input id="txtTool" class="" type="text"  />
  </td>

  <td style="text-align:center">
    <input id="txtArz" class="" type="text"  />
  </td>

  <td style="text-align:center">
    <input id="txtErtefa" class="" type="text"  />
  </td>

  <td style="text-align:center">
    <input id="txtVazn" class="" type="text"  />
  </td>

  <td style="text-align:center">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td>
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center">
    <button type="button"
      onclick="SaveRMUStarClick($(this),'${Shomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
  </td>
</tr>
`;

            tbody.html(rizRowsHtml);

            // دوباره Bind کردن دکمه حذف
            $('.btn-delete-riz').off('click').on('click', function () {
                $(this).closest('tr').remove();
            });

            convertNumbersInPage();
        },
        error: function () {
            toastr.error('خطا در دریافت اطلاعات ریزمتره!', 'خطا');
        }
    });
}


function cancelEditRowStar(row, rizMetreId, Shomareh, rizId1) {
    debugger;
    //row.removeClass('editing');
    row.find('td').each(function (index) {
        if (index === 0 || index === row.children('td').length - 1) return;

        const input = $(this).find('input');
        if (input.length > 0) {
            const value = input.val();
            input.replaceWith(`<span>${value}</span>`);
        }
    });
    convertNumbersInPage();
    const actionCell1 = row.find('td:last-child');
    actionCell1.empty();
    if (actionCell1.find('.fa-trash').length === 0) {
        actionCell1.append(`
		<i class="fa fa-trash DelRMUStyle ms-2" onclick="event.stopPropagation();DeleteRizMetreStar('${rizMetreId}','${Shomareh}','${rizId1}')"></i>
	`);
    }
}


function DeleteRizMetreStar(RizMetreId, Shomareh, rizId, Code) {
    var vardata = new Object();
    vardata.Id = RizMetreId;
    $.ajax({
        url: "/ItemFBStar/DeleteRizMetreStar",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                GetCurrentRizMetreItemFBStar(Shomareh, rizId, Code);
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}
