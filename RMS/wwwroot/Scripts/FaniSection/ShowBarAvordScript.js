function GetFosoul() {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.NoeFaslId = 3;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    $.ajax({
        type: "post",
        url: "/BaseInfo/GetFosoul",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            renderTableFosoul(response);
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function GetBarAvord(Code, faslName) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());
    var vardata = new Object();
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.ShomarehFasl = Code;
    $.ajax({
        type: "post",
        url: "/ShowBarAvordUser/GetUserBarAvord",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //$('#aView').click();
            //$('#ModalTitle').text('فصل  ' + Code +'  -  '+faslName);
            renderTable(response, Code);
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function renderTableFosoul(data) {
    const tbody = $('#fosoulTable tbody');
    tbody.empty();
    data.forEach((item, index) => {
        const rowClass = index % 2 === 0 ? '' : 'table-alt';
        const mainRow = $(`
		<tr class="${rowClass}">
		<td style="display:none">${item.code}</td>
		<td style=\"text-align:right\"><a href="#" onclick="event.stopPropagation(); DescriptionShow(${item.id})">${item.faslName}</a></td>
		<td style=\"text-align:center\">
            <span id="tdjameFasl-${item.code}">
			    ${formatNumber(item.jameFasl.toFixed(0))}
            </span>
		</td>
		<td style=\"text-align:center\">
            <span id="tdjameFaslBaZarib-${item.code}">
			    ${formatNumber(item.jameFasl.toFixed(0))}
            </span>
          </td>
		</tr>
		<tr id="${item.code}" style="display:none">
			<td colspan="3">
				<div id="barAvordContainer-${item.code}"></div>
			</td>
		</tr>
		`);
        convertNumbersInPage();

        mainRow.on('click', function (e) {
            if ($(e.target).closest('[id^="barAvordContainer-"]').length > 0) {
                return;
            }
            if (item.code == '_') {
                return 0;
            }
            e.stopPropagation();

            const rowId = item.code;
            const detailRow = $(`#${rowId}`);

            // اگر باز است → ببند
            if (detailRow.is(':visible')) {
                detailRow.hide();
            } else {
                // بستن همه ردیف‌های جزئیات
                $('#fosoulTable tr[id]').not(detailRow).hide();

                // باز کردن ردیف انتخاب‌شده
                detailRow.show();
                GetBarAvord(item.code, item.faslName);
            }
        });

        $('#chkNonZeroMeghdarFasl').off('change').on('change', function () {
            const showOnlyNonZero = $(this).is(':checked');

            // همه‌ی ردیف‌های اصلی (نه ردیف دوم مربوط به barAvordContainer)
            $('#fosoulTable tbody tr').each(function () {
                const $row = $(this);
                const span = $row.find('[id^="tdjameFasl-"]');

                if (span.length > 0) {
                    let meghdarText = span.text().replace(/\s+/g, '').replace(/[٬,]/g, '');
                    let EnNum = convertPersianToEnglish(meghdarText);
                    const meghdar = parseFloat(EnNum);

                    if (showOnlyNonZero) {
                        if (meghdar === 0) {
                            $row.hide(); // مخفی‌سازی ردیف اصلی
                            //$row.next('tr').hide(); // مخفی‌سازی ردیف barAvordContainer مربوطه
                        } else {
                            $row.show();
                            //$row.next('tr').show();
                        }
                    } else {
                        $row.show();
                        //$row.next('tr').show();
                    }
                }
            });
        });
        tbody.append(mainRow);
    });
}


function DescriptionShow(Id) {
    var vardata = new Object();
    vardata.Id = Id;
    $.ajax({
        type: "post",
        url: "/BaseInfo/GetDescriptionForFasl",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#ShowDes').html(response.description);
            $('#ModalTitle').html(response.title);
            $('#aViewDes').click();
            $('#PopupViewDes').on('shown.bs.modal', function () {
                var modalBody = $(this).find('.modal-body');
                modalBody.scrollTop(0);
            });
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}
function formatNumber(num) {
    if (num == null) return '';
    return Number(num).toLocaleString('fa-IR'); // یا 'en-US' برای کاما
}
function toPersianDigits(num) {
    if (num == null) return '';
    return num.toString().replace(/\d/g, d => '۰۱۲۳۴۵۶۷۸۹'[d]);
}

function convertNumbersInPage() {
    $('*').each(function () {
        if (this.children.length === 0 && this.nodeType === 1) {
            const originalText = $(this).text();
            const convertedText = toPersianDigits(originalText);
            if (originalText !== convertedText) {
                $(this).text(convertedText);
            }
        }
    });
}



function BahayeVahedNewMeghdarChange($input, FBId, itemFbShomareh) {
    debugger;
    var newValue = $input.val();
    if (!$.isNumeric(newValue)) {
        toastr.error('مقدار وارد شده نامعتبر میباشد', 'خطا');
        $input.addClass('blinking');
        return;
    }
    var floatVal = convertPersianToEnglish(newValue);
    var floatValEn = parseFloat(floatVal.replace(/٬/g, '').replace(/,/g, ''));

    // پیدا کردن ردیف والد
    const $row = $input.closest('tr');

    // گرفتن مقدار موجود در ستون مقدار (meghdarFasl)
    var meghdarText = $row.find('#meghdarFasl').text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.');
    var MeghdarEn = convertPersianToEnglish(meghdarText);
    var meghdar = parseFloat(MeghdarEn);

    // مدیریت کلاس blinking
    if (meghdar > 0 && (!floatValEn || floatValEn === 0)) {
        $input.addClass('blinking');
    } else {
        $input.removeClass('blinking');
    }

    // به‌روزرسانی بهای کل
    if (!isNaN(meghdar) && !isNaN(floatValEn)) {
        var bahayeKol = floatValEn * meghdar;
        $row.find('#bahayeKolFasl').text(formatNumber(bahayeKol.toFixed(0)));
    }

    strStar1 = `<div class="row"><span>*</span><input class="ReturnItemsStar" type="submit" value="🔄" onclick="StarReturn(this,'${FBId}','${itemFbShomareh}')"/></div>`

    $('#span' + itemFbShomareh).html(strStar1);
    BahayeVahedNewSave(floatValEn, FBId, itemFbShomareh);
}

function BahayeVahedNewSave(BahayeVahedNew, FBId, itemFbShomareh) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    debugger;
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.FBId = FBId == "null" ? '00000000-0000-0000-0000-000000000000' : FBId;
    vardata.BahayeVahedNew = BahayeVahedNew.toString();
    vardata.itemFbShomareh = itemFbShomareh;
    $.ajax({
        type: "POST",
        url: '/BaravordUser/ConfirmBahayeVahedNew',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data;
            if (info == "OK") {

                debugger;

                toastr.success('بهای واحد جدید بدرستی درج گردید', 'موفقیت');
            }
            else
                toastr.info('مشکل در درج اطلاعات', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در درج اطلاعات', 'خطا');
        }
    });
}


function StarReturn(element, FBId, itemFbShomareh) {
    Year = parseInt($('#HDFYear').val());
    debugger;
    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.itemFbShomareh = itemFbShomareh;
    vardata.Year = Year;
    $.ajax({
        type: "POST",
        url: '/BaravordUser/RemoveStarValueInFB',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            var info = data.split('_');
            if (info[0] == "OK") {

                const mainRow = element.closest('tr');
                if (!mainRow) return;

                txtmeghdarFasl = mainRow.querySelector('#meghdarFasl');

                meghdarFasl = parseFloat(convertPersianToEnglish(txtmeghdarFasl.innerText.replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                bahayeKol = parseFloat(info[1]);

               
                if (isNaN(bahayeKol)) {
                    bahayeKol = 0;
                }
                bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl');
                bahayeKolFasl.innerText = toPersianDigits(formatNumber(meghdarFasl * bahayeKol));
                // پیدا کردن input مربوط به مقدار واحد درون همین ردیف
                const targetInput = mainRow.querySelector('input[type="text"].form-control');
                if (targetInput) {
                    targetInput.value = formatNumber(info[1]);
                    if (bahayeKol == 0) {
                        targetInput.classList.add('blinking');
                    }
                    else
                        targetInput.classList.remove('blinking');
                }

                const row = element.closest('.row');
                if (row) {
                    row.remove();
                }

                toastr.success('بهای واحد بازنشانی گردید', 'موفقیت');
            }
            else
                toastr.info('مشکل در درج اطلاعات', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در درج اطلاعات', 'خطا');
        }
    });
}

function renderTable(data, Code) {
    debugger;
    const container = $(`#barAvordContainer-${Code}`);
    container.empty();

    const table = $(`
		<table class="table table-bordered fixed-table mb-2">
			<thead>
				<tr style="background-color: #aa87ff;">
					<th style="width: 5🔄%;text-align:center;color: #3a007a;font-size: 12px;">*</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">شماره فهرست بها</th>
					<th style="width: 55%;;color: #3a007a;font-size: 16px;">شرح فهرست بها</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">واحد</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">بهای واحد</th>
					<th style="width: 9%;text-align:center;color: #3a007a;font-size: 12px;">
                    مقدار
                    <label class="ShowNoneZeroClass" style="font-size: 9px;">
                    <input type="checkbox" id="chkNonZeroMeghdar-${Code}" style="margin-right: 5px;" />
                     مقادیر غیر صفر         
                    </label>
                    </th>
					<th style="width: 10%;text-align:center;color: #3a007a;font-size: 12px;">بهای کل</th>
				</tr>
			</thead>
			<tbody></tbody>
		</table>
	`);

    const tbody = table.find('tbody');

    data.forEach((item, index) => {
        const rizId = `riz-${Code}-${index}`;
        const clickableClass = 'clickable-row table-active';
        const BahayeKol = parseFloat(item.bahayeKol);
        const BahayeKolFix = BahayeKol.toFixed(0);
        BahayeVahedNew = item.bahayeVahedNew;
        bahayeVahedSetEn = item.bahayeVahed;
        strStar = '';
        if (BahayeVahedNew != 0) {
            bahayeVahedSetEn = BahayeVahedNew;
            strStar += `<div class="col-12" id="span${item.itemFbShomareh}">
                        <div class="row"><span>*</span><input class="ReturnItemsStar" type="submit" value="🔄" title="بازنشانی" onclick="StarReturn(this,'${item.fbId}','${item.itemFbShomareh}')"/></div>
                        </div>`;
        }
        else
            strStar += `<div class="col-12" id="span${item.itemFbShomareh}"></div>`;

        bahayeVahedSet = toPersianDigits(formatNumber(bahayeVahedSetEn));
        debugger;
        const isBlinking = item.meghdar > 0 && (!bahayeVahedSet || parseFloat(bahayeVahedSetEn) === 0);
        const bahayeVahedInput = `
            <input id="txtBahayeVahed-${rizId}" onchange="BahayeVahedNewMeghdarChange($(this),'${item.fbId}','${item.itemFbShomareh}')" type="text" class="form-control form-control-sm ${isBlinking ? 'blinking' : ''}" 
                value="${bahayeVahedSet ?? ''}" 
                style="text-align:center;" />
            `;
        const mainRow = $(`
        <tr id="rowFasl-${rizId}" class="main-row ${clickableClass}" data-riz-id="${rizId}" style="cursor: pointer;">
        <td style="text-align:center">${strStar}</td>
        <td style="text-align:center">${item.itemFbShomareh}</td>
        <td style="word-break: break-word; white-space: normal;text-align:right">${item.sharh}</td>
        <td style="text-align:center">${item.vahed}</td>
        <td style="text-align:center">${bahayeVahedInput}</td>
        <td style="text-align:center" id="meghdarFasl">${formatNumber(item.meghdar)}</td>
        <td style="text-align:center" id="bahayeKolFasl">${formatNumber(BahayeKolFix)}</td>
        <td style="text-align:center;display:none" id="bahayeVahedFasl">${item.bahayeVahed}</td>
        </tr>
        `);


        let rizRowsHtml = '';

        if (Array.isArray(item.rizMetre) && item.rizMetre.length > 0) {
            rizRowsHtml += item.rizMetre.map(riz =>
                `
		<tr class="riz-row-data" data-id="${riz.id}" onclick="EditRizMetreRow(this,'${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')" style="cursor:pointer;">
		<td style="text-align:center">${riz.shomareh == null ? '' : riz.shomareh}</td>
		<td><span>${riz.sharh}</span></td>
		<td style="text-align:center"><span>${riz.tedad == null ? '' : riz.tedad}</span></td>
		<td style="text-align:center"><span>${riz.tool == null ? '' : riz.tool}</span></td>
		<td style="text-align:center"><span>${riz.arz == null ? '' : riz.arz}</span></td>
		<td style="text-align:center"><span>${riz.ertefa == null ? '' : riz.ertefa}</span></td>
		<td style="text-align:center"><span>${riz.vazn == null ? '' : riz.vazn}</span></td>
		<td style="text-align:center"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : riz.meghdarJoz}</span></td>
		<td><span>${riz.des ?? ''}</span></td>
		<td style="text-align:center">
			<i class="fa fa-trash DelRMUStyle" onclick="DeleteRizMetre('${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')"></i>
		</td>
		</tr>
		`).join('');
        }

        // ردیف ورودی همیشه اضافه شود
        rizRowsHtml += `
			<tr class="riz-input-row">
				<td style="text-align:center"></td>
				<td><input id="txtSharh" class="form-control form-control-sm" type="text"/></td>
				<td style="text-align:center"><input id="txtTedad" class="form-control form-control-sm" type="text" /></td>
				<td style="text-align:center"><input id="txtTool" class="form-control form-control-sm" type="text" /></td>
				<td style="text-align:center"><input id="txtArz" class="form-control form-control-sm" type="text" /></td>
				<td style="text-align:center"><input id="txtErtefa" class="form-control form-control-sm" type="text" /></td>
				<td style="text-align:center"><input id="txtVazn" class="form-control form-control-sm" type="text" /></td>
				<td style="text-align:center"><input id="txtMeghdarJoz" disabled class="form-control form-control-sm" type="text" /></td>
				<td>
					<input id="txtDes" class="form-control form-control-sm" type="text"/>
				</td>
				<td style="text-align:center">
					<button type=\"button\" onclick=\"SaveRMUClick($(this),'${item.itemFbShomareh}','${rizId}','${Code}')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>
				</td>
			</tr>
		`;

        const detailRow = $(`
			<tr id="${rizId}" class="riz-row" style="display: none;">
				<td colspan="7">
					<table class="table table-sm table-bordered mb-0">
						<thead>
							<tr style="background-color: #c1b9e7;">
								<th style="width: 7%;text-align:center">ردیف</th>
								<th style="width: 25%;">شرح ریزه متره</th>
								<th style="width: 7%;text-align:center">تعداد</th>
								<th style="width: 7%;text-align:center">طول</th>
								<th style="width: 7%;text-align:center">عرض</th>
								<th style="width: 7%;text-align:center">ارتفاع</th>
								<th style="width: 7%;text-align:center">وزن</th>
								<th style="width: 7%;text-align:center">مقدار جزء</th>
								<th style="width: 20%;">توضیحات</th>
								<th style="width: 7%;text-align:center">عملیات</th>
							</tr>
						</thead>
						<tbody>
							${rizRowsHtml}
						</tbody>
					</table>
				</td>
			</tr>
		`);

        tbody.append(mainRow);




        tbody.append(detailRow);
    });

    container.append(table);
    $(`#${Code}`).slideDown(200);

    $('.clickable-row').off('click').on('click', function (e) {
        e.stopPropagation();

        if ($(e.target).is('input, button, textarea')) {
            return;
        }

        const rizId = $(this).data('riz-id');
        const rizRow = $(`#${rizId}`);

        // بسته کردن بقیه
        $('.riz-row').not(rizRow).slideUp(200);

        // باز یا بسته کردن این یکی
        rizRow.slideToggle(200);
    });
    ///
    $('tr[id^="riz-"]').off('click').on('click', function (e) {
        e.stopPropagation();
    });
    // افزودن ریزمتره
    $('.btn-add-riz').off('click').on('click', function (e) {
        e.stopPropagation();
        const inputs = $(this).closest('tr').find('input');
        const values = inputs.map((_, input) => $(input).val()).get();

        const newRow = $(`
			<tr class="riz-row-data">
				<td>${values[0]}</td>
				<td>${values[1]}</td>
				<td>${values[2]}</td>
				<td>${values[3]}</td>
				<td>${values[4]}</td>
				<td>${values[5]}</td>
				<td>${values[6]}</td>
				<td>${values[7]}</td>
				<td>${values[8] ?? ''} <button class="btn btn-sm btn-danger btn-delete-riz float-end">🗑️</button></td>
			</tr>
		`);

        $(this).closest('tbody').find('.riz-input-row').before(newRow);
        inputs.val('');
    });

    convertNumbersInPage();

    $('[id^="chkNonZeroMeghdar-"]').off('change').on('change', function () {
        const showOnlyNonZero = $(this).is(':checked');
        $(`#barAvordContainer-${Code} .main-row`).each(function () {
            debugger;
            const meghdarText = $(this).find('#meghdarFasl').text().replace(/,/g, '');
            const EnNum = convertPersianToEnglish(meghdarText);
            const meghdar = parseFloat(EnNum);
            if (showOnlyNonZero) {
                if (meghdar === 0) {
                    $(this).hide();
                    $(`#${$(this).data('riz-id')}`).hide(); // مخفی‌سازی ردیف ریز مربوطه
                } else {
                    $(this).show();
                }
            } else {
                $(this).show();
            }
        });
    });

}

function EditRizMetreRow(rowEl, rizMetreId, Shomareh, rizId, FBId, Code) {
    const $row = $(rowEl);

    if ($row.hasClass('editing')) return;

    // بستن سایر ردیف‌های در حال ویرایش
    $('.riz-row-data.editing').each(function () {
        UpdateRizMetreFromRow($(this), rizMetreId, Shomareh, rizId, FBId, Code);
        //cancelEditRow($(this), rizMetreId, Shomareh, rizId, FBId);
    });

    $row.addClass('editing');

    $row.find('td').each(function (index) {
        // ستون‌های غیرقابل ویرایش: 0 = شماره، 7 = مقدار جزء، 9 = عملیات
        if ([0, 7, 9].includes(index)) return;

        const text = $(this).text().trim();
        $(this).html(`<input class="form-control form-control-sm" type="text" value="${text}">`);
    });

    // جایگزینی آیکن عملیات با دکمه ذخیره
    const $actionCell = $row.find('td').eq(9);
    $actionCell.html(`
		<i class="fa fa-save text-success SaveRMUStyle" style="cursor:pointer" 
		   onclick="UpdateRizMetreFromRow(this,'${rizMetreId}','${Shomareh}','${rizId}','${FBId}','${Code}')"></i>
	`);
}


function cancelEditRow(row, rizMetreId, Shomareh, rizId1, FBId) {
    debugger;
    row.removeClass('editing');
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
		<i class="fa fa-trash DelRMUStyle ms-2" onclick="DeleteRizMetre('${rizMetreId}','${Shomareh}','${rizId1}','${FBId}')"></i>
	`);
    }
}

function convertPersianToEnglish(str) {
    return str.replace(/[۰-۹]/g, function (d) {
        return d.charCodeAt(0) - 1776;
    }).replace(/[٠-٩]/g, function (d) {
        return d.charCodeAt(0) - 1632;
    });
}


function UpdateRizMetreFromRow(el, rizMetreId, Shomareh, rizId1, FBId, Code) {
    const row = $(el).closest('tr');
    const rizId = row.data('id');

    const inputs = row.find('input');
    const MeghdarJoz = row.find('#spanMeghdarJoz');
    const values = inputs.map(function () {
        return $(this).val().trim();
    }).get();
    debugger;

    var meghdarFasl = $('#rowFasl-' + rizId1 + ' #meghdarFasl');
    var bahayeKolFasl = $('#rowFasl-' + rizId1 + ' #bahayeKolFasl');
    var bahayeVahedFasl = $('#rowFasl-' + rizId1 + ' #bahayeVahedFasl');

    const tdJameFasl = $('#td-' + Code + '-jameFasl');
    const tdJameFaslBaZarib = $('#td-' + Code + '-jameFaslBaZarib');

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.Id = rizId;
    vardata.Sharh = values[0];
    vardata.Tedad = convertPersianToEnglish(values[1]);
    vardata.Tool = convertPersianToEnglish(values[2]);
    vardata.Arz = convertPersianToEnglish(values[3]);
    vardata.Ertefa = convertPersianToEnglish(values[4]);
    vardata.Vazn = convertPersianToEnglish(values[5]);
    vardata.Des = values[6];
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Code = Code;
    debugger;
    vardata.LevelNumber = 1;
    $.ajax({
        type: "POST",
        url: '/RizMetreUser/UpdateRizMetreUsersFrmShowBarAvord',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data.split('_');
            if (info[0] == "OK") {
                debugger;
                let num = parseFloat(info[1]);
                let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text()));

                meghdarFasl.html(formatNumber(info[2]));
                bahayeKolFasl.html(formatNumber((bahayeVahedFasl1 * info[2]).toFixed(0)));
                MeghdarJoz.html(parseFloat(num.toFixed(2)));

                tdJameFasl.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                tdJameFaslBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                //GetRizMetreUsers();
                toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                cancelEditRow(row, rizMetreId, Shomareh, rizId1, FBId);
            }
            else
                toastr.info('مشکل در ویرایش ریزه متره انتخابی', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در ویرایش ریزه متره انتخابی', 'خطا');
        }
    });

    // ارسال به سرور با Ajax
    // $.post('/api/update-riz', updatedData, function (res) {
    // 	alert('ویرایش انجام شد');
    // });

    // برگشت به حالت نمایش
}



function SaveRMUClick(object, Shomareh, rizId, Code) {
    object.parent().parent().find('input[type=text]').each(function () {
        $(this).removeClass('ErrorValueStyle');
    });

    debugger;
    const row = $(object).closest('tr');
    const tdJameFasl = $('#td-' + Code + '-jameFasl');
    const tdJameFaslBaZarib = $('#td-' + Code + '-jameFaslBaZarib');

    const MeghdarJoz = row.find('#spanMeghdarJoz');

    var meghdarFasl = $('#rowFasl-' + rizId + ' #meghdarFasl');
    var bahayeKolFasl = $('#rowFasl-' + rizId + ' #bahayeKolFasl');
    var bahayeVahedFasl = $('#rowFasl-' + rizId + ' #bahayeVahedFasl');
    debugger;
    var txtBahayeVahed = $('#txtBahayeVahed-' + rizId);

    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    object.parent().parent().find('input[type=text]').each(function () {

        if ($(this).attr('id') == 'txtSharh')
            Sharh = $(this).val();
        else if ($(this).attr('id') == 'txtTedad') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Tedad = $(this).val().replace(/\,/g, '');
            }
            else {
                $(this).addClass('ErrorValueStyle');
                Check = false;
            }
        }
        else if ($(this).attr('id') == 'txtTool') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Tool = $(this).val().replace(/\,/g, '');
            }
            else {
                $(this).addClass('ErrorValueStyle');
                Check = false;
            }
        }
        else if ($(this).attr('id') == 'txtArz') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Arz = $(this).val().replace(/\,/g, '');
            }
            else {
                $(this).addClass('ErrorValueStyle');
                Check = false;
            }
        }
        else if ($(this).attr('id') == 'txtErtefa') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Ertefa = $(this).val().replace(/\,/g, '');
            }
            else {
                $(this).addClass('ErrorValueStyle');
                Check = false;
            }
        }
        else if ($(this).attr('id') == 'txtVazn') {
            if ($.isNumeric(parseFloat($(this).val()))) {
                Vazn = $(this).val().replace(/\,/g, '');
            }
            else {
                $(this).addClass('ErrorValueStyle');
                Check = false;
            }
        }
        else if ($(this).attr('id') == 'txtDes')
            Des = $(this).val();
    });

    if (Check) {
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        var vardata = new Object();
        vardata.Sharh = Sharh;
        vardata.Tedad = Tedad;
        vardata.Tool = Tool;
        vardata.Arz = Arz;
        vardata.Ertefa = Ertefa;
        vardata.Vazn = Vazn;
        vardata.Des = Des;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Shomareh = Shomareh;
        $.ajax({
            type: "POST",
            url: '/RizMetreUser/ConfirmRizMetreUsersFromShowBarAvord',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {

                    debugger;

                    let num = parseFloat(info[2]);
                    let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                    let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(txtBahayeVahed.val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                    meghdarFasl.html(formatNumber(info[3]));
                    bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * info[3]));
                    MeghdarJoz.html(num);
                    tdJameFasl.html(formatNumber(parseFloat(info[4]).toFixed(0)));
                    tdJameFaslBaZarib.html(formatNumber(parseFloat(info[4]).toFixed(0)));

                    inputBahayeVahed = $('#txtBahayeVahed-' + rizId);
                    const val = parseFloat(inputBahayeVahed.val().replace(/,/g, '')); // حذف کاما و تبدیل به عدد
                    if (val === 0) {
                        inputBahayeVahed.addClass('blinking');
                    } else {
                        inputBahayeVahed.removeClass('blinking'); // اگر نمی‌خواهید در غیر صفر بودن کلاس باقی بماند
                    }

                    GetCurrentRizMetreUsers(Shomareh, rizId, info[1], Code);

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
    else
        toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
}


function GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code) {
    var vardata = new Object();
    vardata.FBId = FBId;
    $.ajax({
        url: "/RizMetreUser/GetCurrentRizMetreUsersForShowBarAvord", // آدرس مناسب API
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            const rizRow = $(`#${rizId}`);
            const tbody = rizRow.find('tbody');
            tbody.empty(); // پاک‌کردن محتوای قدیمی

            let rizRowsHtml = '';

            if (Array.isArray(response) && response.length > 0) {
                rizRowsHtml += response.map(riz => `
                    <tr class="riz-row-data" data-id="${riz.id}" onclick="EditRizMetreRow(this,'${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')" style="cursor:pointer;">
					<td style="text-align:center">${riz.shomareh}</td>
					<td><span>${riz.sharh}</span></td>
					<td style="text-align:center"><span>${riz.tedad}</span></td>
					<td style="text-align:center"><span>${riz.tool}</span></td>
					<td style="text-align:center"><span>${riz.arz}</span></td>
					<td style="text-align:center"><span>${riz.ertefa}</span></td>
					<td style="text-align:center"><span>${riz.vazn}</span></td>
					<td style="text-align:center"><span>${riz.meghdarJoz}</span></td>
					<td><span>${riz.des ?? ''}</span></td>
                    <td style="text-align:center"><i class="fa fa-trash DelRMUStyle" onclick="DeleteRizMetre('${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')"></i></td>
                    </tr>
                `).join('');
            }

            // اضافه کردن ردیف ورودی جدید
            rizRowsHtml += `
                <tr class="riz-input-row">
                    <td></td>
                    <td><input id="txtSharh" class="form-control form-control-sm" type="text"/></td>
                    <td><input id="txtTedad" class="form-control form-control-sm" type="text" /></td>
                    <td><input id="txtTool" class="form-control form-control-sm" type="text" /></td>
                    <td><input id="txtArz" class="form-control form-control-sm" type="text" /></td>
                    <td><input id="txtErtefa" class="form-control form-control-sm" type="text" /></td>
                    <td><input id="txtVazn" class="form-control form-control-sm" type="text" /></td>
                    <td><input id="txtMeghdarJoz" disabled class="form-control form-control-sm" type="text" /></td>
                    <td><input id="txtDes" class="form-control form-control-sm" type="text"/></td>
                    <td>
                        <button type="button" onclick="SaveRMUClick($(this),'${Shomareh}','${rizId}','${Code}')" class="ButtonRowsSaveStyle">
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

function DeleteRizMetre(RizMetreId, Shomareh, rizId, FBId, Code) {
    debugger;
    var vardata = new Object();
    vardata.Id = RizMetreId;
    $.ajax({
        url: "/RizMetreUser/DeleteRizMetre",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
                GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code);
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}



