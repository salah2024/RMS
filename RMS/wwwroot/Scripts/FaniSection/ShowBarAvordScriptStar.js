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

        if (index === 0 || index === 8) return;

        if (index === 1) {

            let inputHtml =
            `<div class="d-flex align-items-center gap-2">
                <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="event.stopPropagation();ShowSelectTajmie('Star-${Shomareh}-${rizMetreId}')"></i>

                <input id="txtTajmieItem-Star-${Shomareh}-${rizMetreId}" class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}">

            </div>`;
            $td.html(inputHtml);

        }

       else if (index === 3 || index === 4 || index === 5 || index === 6 || index === 7) {

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
    const $actionCell = $row.find('td').eq(10);
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
    const MeghdarJoz = row.find('#spanMeghdarJozStar');

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
        NoeFBId = parseInt($('#HDFNoeFB').val());
        Year = parseInt($('#HDFYear').val());
        var vardata = new Object();
        vardata.Year = Year;
        vardata.NoeFB = NoeFBId;
        vardata.Id = rizId;
        vardata.TajmieItem = values[0];
        vardata.Sharh = values[1];
        vardata.Tedad = values[2] != "" ? values[2] : null;
        vardata.Tool = values[3] != "" ? values[3] : null;
        vardata.Arz = values[4] != "" ? values[4] : null;
        vardata.Ertefa = values[5] != "" ? values[5] : null;
        vardata.Vazn = values[6] != "" ? values[6] : null;
        vardata.Des = values[7];
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

                  debugger;
                  //var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                    let num = parseFloat(info[1]);
                    //let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text()));

                    //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                    //////////////////
                    ///////////////////
                    ///////////////////

                    //const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                    //const tdJameFasl = $('#tdjameFasl-' + Code);
                    //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                    //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);
                    //const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                    //const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);

                    const MeghdarJoz = row.find('#spanMeghdarJozStar');

                    var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                    var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
                    //var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                    meghdarFasl.html(formatNumber(info[2]));
                    bahayeKolFasl.html(formatNumber(info[3]));
                    MeghdarJoz.html(num);
                    //tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                    //tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));


                    //JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAll = parseFloat(info[2]) + isNaN(JameFasl) ? 0 : JameFasl;
                    //tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                    //JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAllBaZarib = parseFloat(info[3]) + isNaN(JameFaslBaZarib) ? 0 : JameFaslBaZarib;
                    //tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));
                    ///////////////////
                    ///////////////////
                    ///////////////////

                    refreshBaseItem(el, Shomareh, Code);
                    GetJamBarAvordAll();
                    GetJamFosoulAll();

                    //GetRizMetreUsers();
                    toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                    cancelEditRowStar(row, rizMetreId, Shomareh, rizId1);
                }
                else if (info[0] == "NOK") {
                    toastr.info('فصل تجمیعی انتخابی وجود ندارد', 'اطلاع');
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
    const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
    const tdJameFasl = $('#tdjameFasl-' + Code);
    const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
    const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);
    const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
    const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);

    const MeghdarJoz = row.find('#spanMeghdarJozStar');

    var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
    var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
    var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

    debugger;
    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    firstObj = null;
    findFirstObj = false;
    object.parent().parent().find('input[type=text]').each(function () {
        if ($(this).attr('id') == 'txtSharh-' + Code)
            Sharh = $(this).val();
        else if ($(this).attr('id').startsWith('txtTajmieItem-'))
            TajmieItem = $(this).val();
        else if ($(this).attr('id') == 'txtTedad-' + Code) {
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
        else if ($(this).attr('id') == 'txtTool-' + Code) {
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
        else if ($(this).attr('id') == 'txtArz-' + Code) {
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
        else if ($(this).attr('id') == 'txtErtefa-' + Code) {
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
        else if ($(this).attr('id') == 'txtVazn-' + Code) {
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
        else if ($(this).attr('id') == 'txtDes-' + Code)
            Des = $(this).val();
    });

    //if (Check) {
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        Year = parseInt($('#HDFYear').val());
        NoeFBId = parseInt($('#HDFNoeFB').val());

        debugger;

        var vardata = new Object();
        vardata.Sharh = Sharh;
        vardata.TajmieItem = TajmieItem === undefined ? null : TajmieItem;
        vardata.Tedad = Tedad === undefined ? null : Tedad;
        vardata.Tool = Tool === undefined ? null : Tool;
        vardata.Arz = Arz === undefined ? null : Arz;
        vardata.Ertefa = Ertefa === undefined ? null : Ertefa;
        vardata.Vazn = Vazn === undefined ? null : Vazn;
        vardata.Des = Des;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Shomareh = Shomareh;
        vardata.Year = Year;
        vardata.NoeFBId = NoeFBId;
        $.ajax({
            type: "POST",
            url: '/ItemFBStar/ConfirmRizMetreItemFBStar',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {

                    debugger;
                    let num = parseFloat(info[1]);
                    //let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                    //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                    bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);

                    var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                    meghdarFasl.html(formatNumber(parseFloat(info[1]).toFixed(0)));
                    bahayeKolFasl.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                    //MeghdarJoz.html(num);
                    //tdJameFaslStar.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    //tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[4]).toFixed(0)));


                    //JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAll = parseFloat(info[3]) + isNaN(JameFasl) ? 0 : JameFasl;
                    //tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                    //JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAllBaZarib = parseFloat(info[4]) + isNaN(JameFaslBaZarib) ? 0 : JameFaslBaZarib;
                    //tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));

                    //inputBahayeVahed = $('#txtBahayeVahed-' + rizId);
                    //const val = parseFloat(inputBahayeVahed.val().replace(/,/g, '')); // حذف کاما و تبدیل به عدد
                    //if (val === 0) {
                    //    inputBahayeVahed.addClass('blinking');
                    //} else {
                    //    inputBahayeVahed.removeClass('blinking'); // اگر نمی‌خواهید در غیر صفر بودن کلاس باقی بماند
                    //}

                    refreshBaseItem(object, Shomareh, Code);

                    GetJamBarAvordAll();
                    GetJamFosoulAll();

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
    //}
    //else {
    //    firstObj.focus().select();
    //    toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
    //}
}



//ریزه متره های ستاره دار
function GetCurrentRizMetreItemFBStar(Shomareh, rizId, Code) {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFBId = NoeFBId;
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
					<td style="text-align:center;width:4%">${toPersianDigits(riz.shomarehNew)}</td>
					<td style="text-align:center;width:6%"><span>${toPersianDigits(riz.tajmieItem)}</span></td>
					<td style="width:22%"><span>${toPersianDigits(riz.sharh)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.tedad == null ? '' : toPersianDigits(riz.tedad)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.tool == null ? '' : toPersianDigits(riz.tool)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.arz == null ? '' : toPersianDigits(riz.arz)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.ertefa == null ? '' : toPersianDigits(riz.ertefa)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.vazn == null ? '' : toPersianDigits(riz.vazn)}</span></td>
					<td style="text-align:center;width:7%"><span id="spanMeghdarJozStar">${riz.meghdarJoz == null ? '' : toPersianDigits(riz.meghdarJoz)}</span></td>
					<td style="width:20%"><span>${toPersianDigits(riz.des ?? '')}</span></td>
                    <td style="text-align:center;width:7%"><i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetreStar($(this),'${riz.id}','${Shomareh}','${rizId}','${Code}')"></i></td>
                    </tr>
                `).join('');
            }


            rizRowsHtml += `

<tr class="riz-input-row">
  <td style="text-align:center;width:4%"></td>
  <td style="text-align:center;width:6%">
    <div class="d-flex align-items-center gap-2">
            <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="ShowSelectTajmie('${Shomareh}-New')"></i>
            <input id="txtTajmieItem-${Shomareh}-New" class="form-control form-control-sm" type="text" onchange="TajmieItemChange(this)"/>
    </div>
  </td>
  <td style="width:22%">
    <input id="txtSharh-${Code}" class="form-control form-control-sm" type="text" />
  </td>
  <td style="text-align:center;width:7%">
    <input id="txtTedad-${Code}" class="form-control form-control-sm" type="text" />
  </td>
  <td style="text-align:center;width:7%">
    <input id="txtTool-${Code}" class="form-control form-control-sm" type="text"  />
  </td>
  <td style="text-align:center;width:7%">
    <input id="txtArz-${Code}" class="form-control form-control-sm" type="text"  />
  </td>
  <td style="text-align:center;width:7%">
    <input id="txtErtefa-${Code}" class="form-control form-control-sm" type="text"  />
  </td>
  <td style="text-align:center;width:7%">
    <input id="txtVazn-${Code}" class="form-control form-control-sm" type="text"  />
  </td>
  <td style="text-align:center;width:7%">
    <input id="txtMeghdarJoz-${Code}" class="form-control form-control-sm" type="text" disabled />
  </td>
  <td style="width:20%">
    <input id="txtDes-${Code}" class="form-control form-control-sm" type="text" />
  </td>
  <td style="text-align:center;width:7%">
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

            //convertNumbersInPage();
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
        if (index === 0 || index === row.children('td').length - 1) {
            return;
        }
        else if (index === 2 || index === 9) {
            const input = $(this).find('input');
            if (input.length > 0) {
                const value = input.val();
                input.replaceWith(`<span>${value}</span>`);
            }
        }
        else {
            const input = $(this).find('input');
            if (input.length > 0) {
                const value = input.val();
                input.replaceWith(`<span>${formatNumber(parseFloat(convertPersianToEnglish(value)))}</span>`);
            }

            const i = $(this).find('i');
            if (i.length > 0) {
                i.replaceWith(``);
            }
        }
    });
    //convertNumbersInPage();
    const actionCell1 = row.find('td:last-child');
    actionCell1.empty();
    if (actionCell1.find('.fa-trash').length === 0) {
        actionCell1.append(`
		<i class="fa fa-trash DelRMUStyle ms-2" onclick="event.stopPropagation();DeleteRizMetreStar($(this),'${rizMetreId}','${Shomareh}','${rizId1}')"></i>
	`);
    }
}


function DeleteRizMetreStar(object, RizMetreId, Shomareh, rizId, Code) {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.Id = RizMetreId;
    vardata.BarAvordId = BarAvordUserId;
    vardata.FBShomareh = Shomareh;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    $.ajax({
        url: "/ItemFBStar/DeleteRizMetreStar",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            info = response.split('_');
            if (info[0] == "OK") {

                //const row = $(object).closest('tr');
                const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                //const tdJameFasl = $('#tdjameFasl-' + Code);
                //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                //const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                //const MeghdarJoz = row.find('#spanMeghdarJozStar');

                var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                var bahayeKolFaslStar = $('#bahayeKolFaslStar_' + Shomareh);
                //var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                //let num = parseFloat(info[1]);
                //let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                meghdarFasl.html(formatNumber(parseFloat(info[1]).toFixed(0)));
                bahayeKolFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                //MeghdarJoz.html(num);
                //tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[4]).toFixed(0)));

                //JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAll = parseFloat(info[2]) + isNaN(JameFasl) ? 0 : JameFasl;
                //tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                //JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAllBaZarib = parseFloat(info[3]) + isNaN(JameFaslBaZarib) ? 0 : JameFaslBaZarib;
                //tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));

                refreshBaseItem(object, Shomareh, Code);

                GetCurrentRizMetreItemFBStar(Shomareh, rizId, Code);

                GetJamBarAvordAll();
                GetJamFosoulAll();
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}
