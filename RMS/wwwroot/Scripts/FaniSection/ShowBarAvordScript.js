var lstVaheds;
function GetFosoul() {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    $.ajax({
        type: "post",
        url: "/BaseInfo/GetFosoul",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Fosoul = response.fosoul;
            lstVaheds = response.lstVaheds;
            renderTableFosoul(Fosoul);
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
		<td style=\"text-align:right\"><a href="#" onclick="event.stopPropagation();DescriptionShow(${item.id})">${item.faslName}</a></td>
		<td style=\"text-align:center\">
            <span id="tdjameFasl-${item.code}">
			    ${formatNumber(item.jameFasl.toFixed(0))}
            </span>
		</td>
		<td style=\"text-align:center\">
            <span id="tdjameFaslStar-${item.code}">
			    ${formatNumber(item.jameFaslStar.toFixed(0))}
            </span>
		</td>
        <td style=\"text-align:center\">
            <span id="tdjameFaslAll-${item.code}">
			    ${formatNumber(item.jameFaslAll.toFixed(0))}
            </span>
		</td>
        <td style=\"text-align:center\">
            <span id="tdjameFaslBaZarib-${item.code}">
			    ${formatNumber(item.jameFaslWithZarib.toFixed(0))}
            </span>
        </td>
		<td style=\"text-align:center\">
            <span id="tdjameFaslStarBaZarib-${item.code}">
			    ${formatNumber(item.jameFaslStarWithZarib.toFixed(0))}
            </span>
        </td>
        <td style=\"text-align:center\">
            <span id="tdjameFaslAllBaZarib-${item.code}">
			    ${formatNumber(item.jameFaslWithZaribAll.toFixed(0))}
            </span>
        </td>
        </tr>
		<tr id="${item.code}" style="display:none">
			<td colspan="7">
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
                debugger;
                const $row = $(this);
                const span = $row.find('[id^="tdjameFasl-"]');

                if (span.length > 0) {
                    let meghdarText = span.text().replace(/\s+/g, '').replace(/[٬,]/g, '');
                    let EnNum = convertPersianToEnglish(meghdarText);
                    const meghdar = EnNum != "" ? parseFloat(EnNum) : 0;

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


function GetBarAvord(Code, faslName) {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());
    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
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
            userBarAvordOutPut = response.userBarAvordOutPut;
            lstItemFBStars = response.lstItemFBStars;
            debugger;
            //$('#aView').click();
            //$('#ModalTitle').text('فصل  ' + Code +'  -  '+faslName);
            renderTable(userBarAvordOutPut, lstItemFBStars, Code);
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
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
    if (num == 0) return '';
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

    var newValue = $input.value;
    if (!$.isNumeric(newValue)) {
        toastr.error('مقدار وارد شده نامعتبر میباشد', 'خطا');
        $input.addClass('blinking');
        return;
    }
    var floatVal = convertPersianToEnglish(newValue);
    var floatValEn = parseFloat(floatVal.replace(/٬/g, '').replace(/,/g, ''));

    // پیدا کردن ردیف والد
    const $row = $($input).closest('tr');

    // گرفتن مقدار موجود در ستون مقدار (meghdarFasl)
    var meghdarText = $row.find('#meghdarFasl_'+itemFbShomareh).text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.');
    var MeghdarEn = convertPersianToEnglish(meghdarText);
    var meghdar = parseFloat(MeghdarEn);

    // مدیریت کلاس blinking
    if (meghdar > 0 && (!floatValEn || floatValEn === 0)) {
        $($input).addClass('blinking');
    } else {
        $($input).removeClass('blinking');
    }

    // به‌روزرسانی بهای کل
    if (!isNaN(meghdar) && !isNaN(floatValEn)) {
        var bahayeKol = floatValEn * meghdar;
        $row.find('#bahayeKolFasl').text(formatNumber(bahayeKol.toFixed(0)));
    }

    strStar1 = `<div class="row"><span style="font-size:25px">*</span><input style="font-size:20px" class="ReturnItemsStar" type="submit" value="🔄" onclick="StarReturn(this,'${FBId}','${itemFbShomareh}')"/></div>`

    $('#span' + itemFbShomareh).html(strStar1);
    BahayeVahedNewSave($input,floatValEn, FBId, itemFbShomareh);
}

function BahayeVahedNewSave(element,BahayeVahedNew, FBId, itemFbShomareh) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.FBId = FBId == "null" ? '00000000-0000-0000-0000-000000000000' : FBId;
    vardata.BahayeVahedNew = BahayeVahedNew.toString();
    vardata.itemFbShomareh = itemFbShomareh;
    vardata.NoeFBId = NoeFBId;
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

                const mainRow = element.closest('tr');
                if (!mainRow) return;

                txtmeghdarFasl = mainRow.querySelector('#meghdarFasl_' + itemFbShomareh);
                bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);

                meghdarFasl = parseFloat(convertPersianToEnglish(txtmeghdarFasl.innerHTML.replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                BahayeVahedNew1 = parseFloat(BahayeVahedNew);


                //if (isNaN(bahayeKol)) {
                //    bahayeKol = 0;
                //}
                bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);
                bahayeKolFasl.innerHTML = toPersianDigits(formatNumber(meghdarFasl * BahayeVahedNew1));
                // پیدا کردن input مربوط به مقدار واحد درون همین ردیف
                //const targetInput = mainRow.querySelector('input[type="text"].form-control');
                //if (targetInput) {
                //    targetInput.value = formatNumber(info[1]);
                //    if (bahayeKol == 0) {
                //        targetInput.classList.add('blinking');
                //    }
                //    else
                //        targetInput.classList.remove('blinking');
                //}

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
            var info = data.split('_');
            if (info[0] == "OK") {

                const mainRow = element.closest('tr');
                if (!mainRow) return;

                txtmeghdarFasl = mainRow.querySelector('#meghdarFasl_' + itemFbShomareh);

                meghdarFasl = parseFloat(convertPersianToEnglish(txtmeghdarFasl.innerHTML.replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                bahayeKol = parseFloat(info[1]);


                if (isNaN(bahayeKol)) {
                    bahayeKol = 0;
                }
                bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);
                bahayeKolFasl.innerHTML = toPersianDigits(formatNumber(meghdarFasl * bahayeKol));
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


function renderTable(data, Stars, Code) {
    const container = $(`#barAvordContainer-${Code}`);
    container.empty();

    const table = $(`
		<table class="table table-bordered fixed-table mb-2">
			<thead>
				<tr style="background-color: #aa87ff;">
					<th style="width: 5🔄%;text-align:center;color: #3a007a;font-size: 12px;">*</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">شماره فهرست بها</th>
					<th style="width: 52%;color: #3a007a;font-size: 16px;">شرح فهرست بها</th>
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
					<th style="width: 3%;text-align:center;color: #3a007a;font-size: 12px;">عملیات</th>
				</tr>
			</thead>
			<tbody></tbody>
		</table>
	`);

    const tbody = table.find('tbody');

    var index = 0;
    data.forEach((item) => {

        debugger;
        var Field0 = item.itemsFields[0] == undefined ? "" : item.itemsFields[0].vahed;
        var Field1 = item.itemsFields[1] == undefined ? "" : item.itemsFields[1].vahed;
        var Field2 = item.itemsFields[2] == undefined ? "" : item.itemsFields[2].vahed;
        var Field3 = item.itemsFields[3] == undefined ? "" : item.itemsFields[3].vahed;
        var Field4 = item.itemsFields[4] == undefined ? "" : item.itemsFields[4].vahed;
        var Field5 = item.itemsFields[5] == undefined ? "" : item.itemsFields[5].vahed;

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
                        <div class="row"><span style="font-size:25px">*</span><input  style="font-size:20px" class="ReturnItemsStar" type="submit" value="🔄" title="بازنشانی" onclick="StarReturn(this,'${item.fbId}','${item.itemFbShomareh}')"/></div>
                        </div>`;
        }
        else
            strStar += `<div class="col-12" id="span${item.itemFbShomareh}"></div>`;

        bahayeVahedSet = toPersianDigits(formatNumber(bahayeVahedSetEn));

        const isBlinking = item.meghdar > 0 && (!bahayeVahedSet || parseFloat(bahayeVahedSetEn) === 0);
        const bahayeVahedInput = `
            <input id="txtBahayeVahed-${rizId}" onchange="BahayeVahedNewMeghdarChange(this,'${item.fbId}','${item.itemFbShomareh}')" type="text" class="form-control form-control-sm ${isBlinking ? 'blinking' : ''}" 
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
        <td style="text-align:center"><span id="meghdarFasl_${item.itemFbShomareh}">${formatNumber(item.meghdar)}</span></td>
        <td style="text-align:center"><span id="bahayeKolFasl_${item.itemFbShomareh}">${formatNumber(BahayeKolFix)}</span></td>
        <td style="text-align:center;display:none"><span id="bahayeVahedFasl_${item.bahayeVahed}">${item.bahayeVahed}</span></td>
        <td></td>
        </tr>
        `);


        let rizRowsHtml = '';


        if (Array.isArray(item.rizMetre) && item.rizMetre.length > 0) {
            rizRowsHtml += item.rizMetre.map(riz =>
                `
		<tr class="riz-row-data" style="direction:ltr" data-id="${riz.id}" data-itemsFields='${JSON.stringify(item.itemsFields).replace(/"/g, '&quot;')}'
        onclick="EditRizMetreRow(this,'${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')" style="cursor:pointer;">
		<td style="text-align:center">${riz.shomarehNew == null ? '' : riz.shomarehNew}</td>
		<td><span>${riz.sharh}</span></td>
		<td style="text-align:center"><span>${riz.tedad == null ? '' : riz.tedad}</span></td>
		<td style="text-align:center"><span>${riz.tool == null ? '' : riz.tool}</span></td>
		<td style="text-align:center"><span>${riz.arz == null ? '' : riz.arz}</span></td>
		<td style="text-align:center"><span>${riz.ertefa == null ? '' : riz.ertefa}</span></td>
		<td style="text-align:center"><span>${riz.vazn == null ? '' : riz.vazn}</span></td>
		<td style="text-align:center"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : riz.meghdarJoz}</span></td>
		<td><span>${riz.des ?? ''}</span></td>
		<td style="text-align:center">
			<i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetre(this,'${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')"></i>
		</td>
		</tr>
		`).join('');
        }


        field = item.itemsFields;
        const defaultField = { id: null, itemShomareh: item.itemShomareh || '', fieldType: null, vahed: '', isEnteringValue: false, essentialValue: false };

        const getField = (i) => (i >= 0 && i < field.length ? field[i] : defaultField);

        const f0 = getField(0);
        const f1 = getField(1);
        const f2 = getField(2);
        const f3 = getField(3);
        const f4 = getField(4);

        rizRowsHtml += `
<tr class="riz-input-row">
  <td style="text-align:center"></td>
  <td><input id="txtSharh" class="form-control form-control-sm" type="text" /></td>

  <td style="text-align:center">
    <input id="txtTedad" class="${getInputClass(f0)}" type="text" ${getDisabledAttr(f0)} />
  </td>

  <td style="text-align:center">
    <input id="txtTool" class="${getInputClass(f1)}" type="text" ${getDisabledAttr(f1)} />
  </td>

  <td style="text-align:center">
    <input id="txtArz" class="${getInputClass(f2)}" type="text" ${getDisabledAttr(f2)} />
  </td>

  <td style="text-align:center">
    <input id="txtErtefa" class="${getInputClass(f3)}" type="text" ${getDisabledAttr(f3)} />
  </td>

  <td style="text-align:center">
    <input id="txtVazn" class="${getInputClass(f4)}" type="text" ${getDisabledAttr(f4)} />
  </td>

  <td style="text-align:center">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td>
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center">
    <button type="button"
      onclick="SaveRMUClick($(this),'${item.itemFbShomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
  </td>
</tr>
`;

        // ردیف ورودی همیشه اضافه شود
        //      rizRowsHtml += `
        //	<tr class="riz-input-row">
        //		<td style="text-align:center"></td>
        //		<td><input id="txtSharh" class="form-control form-control-sm" type="text"/></td>
        //		<td style="text-align:center"><input id="txtTedad"/></td>
        //		<td style="text-align:center"><input id="txtTool" /></td>
        //		<td style="text-align:center"><input id="txtArz"  /></td>
        //		<td style="text-align:center"><input id="txtErtefa" /></td>
        //		<td style="text-align:center"><input id="txtVazn" class="form-control /></td>
        //		<td style="text-align:center"><input id="txtMeghdarJoz" disabled class="form-control form-control-sm" type="text" /></td>
        //		<td>
        //			<input id="txtDes" class="form-control form-control-sm" type="text"/>
        //		</td>
        //		<td style="text-align:center">
        //			<button type=\"button\" onclick=\"SaveRMUClick($(this),'${item.itemFbShomareh}','${rizId}','${Code}')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>
        //		</td>
        //	</tr>
        //`;
        debugger;


        const detailRow = $(`
			<tr id="${rizId}" class="riz-row" style="display: none;">
				<td colspan="8">
					<table class="table table-sm table-bordered mb-0">
						<thead>
							<tr style="background-color: #c1b9e7;">
								<th style="width: 7%;text-align: center;vertical-align: top;">ردیف</th>
								<th style="width: 25%;text-align: center;vertical-align: top;">شرح ریزه متره</th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                   <span>تعداد</span>   
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field0}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                    <span>
                                    طول
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field1}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                    <span>
                                    عرض
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field2}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                     <span>
                                     ارتفاع
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field3}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">                               
                                    <span>
                                    وزن
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field4}
                                    </span>

                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                
                                <span>
                                مقدار جزء
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field5}
                                    </span>

                                </th>
								<th style="width: 20%;text-align: center;vertical-align: top;">توضیحات</th>
								<th style="width: 7%;text-align: center;vertical-align: top;">عملیات</th>
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

        index++;
    });


    Stars.forEach((item) => {
        const rizId = `riz-${Code}-${index}`;

        currentId = item.id;

        KharidTajhizat = item.blnKharidTajhizat == null ? false : item.blnKharidTajhizat;
        strKharidTajhizat = KharidTajhizat == true ? "ت" : "";
        checked = KharidTajhizat == true ? "checked" : "";
        const mainRowStar = $(`
        <tr id="rowFasl-${rizId}" class="main-row clickable-row" data-riz-id="${rizId}" style="cursor: pointer;">
        <td style="text-align:center;">
        <span>
            ${strKharidTajhizat}
        </span>
        <span style="font-size:20px">
            * 
        </span>
        </td>
        <td style="text-align:center">${item.shomareh}</td>
        <td style="word-break: break-word; white-space: normal;text-align:right">
            <div class="row">
             <div class="col-10">
                ${item.sharh}
             </div>
             <div class="col-2" style="border-right:1px solid #ccc;background-color:#f3e0ff;">
                <input id="KharidTajhizat_${currentId}_${item.shomareh}" type="checkbox" ${checked} />
                <lable>خرید تجهیزات</label>
             </div>
            </div>
        </td>
        <td style="text-align:center">${item.vahedName}</td>
        <td style="text-align:center" id="bahayeVahedFaslStar_${item.shomareh}">${item.bahayeVahed}</td>
        <td style="text-align:center" id="meghdarFaslStar_${item.shomareh}">${formatNumber(item.meghdar)}</td>
        <td style="text-align:center" id="bahayeKolFaslStar_${item.shomareh}">${formatNumber(item.bahayeKol)}</td>
        <td>
            <i id="iDelete_${currentId}_${item.shomareh}" onclick="DeleteItemFB(this,'${item.shomareh}','${Code}')" class="fa fa-trash DelRMUStyle" ></i>
        </td>
        </tr>
        `);

        let rizRowsHtmlStar = '';


        if (Array.isArray(item.rizMetre) && item.rizMetre.length > 0) {
            rizRowsHtmlStar += item.rizMetre.map(riz =>
                `
		<tr class="riz-row-data" style="direction:ltr" data-id="${riz.id}" data-itemsFields='${JSON.stringify(item.itemsFields).replace(/"/g, '&quot;')}'
        onclick="EditRizMetreRowStar(this,'${riz.id}','${item.shomareh}','${rizId}','${Code}')" style="cursor:pointer;">
		<td style="text-align:center">${riz.shomarehNew == null ? '' : riz.shomarehNew}</td>
		<td><span>${riz.sharh}</span></td>
		<td style="text-align:center"><span>${riz.tedad == null ? '' : riz.tedad}</span></td>
		<td style="text-align:center"><span>${riz.tool == null ? '' : riz.tool}</span></td>
		<td style="text-align:center"><span>${riz.arz == null ? '' : riz.arz}</span></td>
		<td style="text-align:center"><span>${riz.ertefa == null ? '' : riz.ertefa}</span></td>
		<td style="text-align:center"><span>${riz.vazn == null ? '' : riz.vazn}</span></td>
		<td style="text-align:center"><span id="spanMeghdarJozStar">${riz.meghdarJoz == null ? '' : riz.meghdarJoz}</span></td>
		<td><span>${riz.des ?? ''}</span></td>
		<td style="text-align:center">
			<i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetreStar($(this),'${riz.id}','${item.shomareh}','${rizId}','${Code}')"></i>
		</td>
		</tr>
		`).join('');
        }

        rizRowsHtmlStar += `
<tr class="riz-input-row">
  <td style="text-align:center"></td>
  <td><input id="txtSharh" class="form-control form-control-sm" type="text" /></td>

  <td style="text-align:center">
    <input id="txtTedad" class="" type="text"/>
  </td>

  <td style="text-align:center">
    <input id="txtTool" class="" type="text"/>
  </td>

  <td style="text-align:center">
    <input id="txtArz" class="" type="text"/>
  </td>

  <td style="text-align:center">
    <input id="txtErtefa" class="" type="text"/>
  </td>

  <td style="text-align:center">
    <input id="txtVazn" class="" type="text"/>
  </td>

  <td style="text-align:center">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td>
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center">
    <button type="button"
      onclick="SaveRMUStarClick($(this),'${item.shomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
  </td>
</tr>
`;

        const detailRowStar = $(`
			<tr id="${rizId}" class="riz-row" style="display:none">
				<td colspan="8">
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
							${rizRowsHtmlStar}
						</tbody>
					</table>
				</td>
			</tr>
		`);

        tbody.append(mainRowStar);
        tbody.append(detailRowStar);

        index++;

    });


    const RowNew = $(`
        <tr id="rowFaslNew-${Code}" class="main-row" data-riz-code="${Code}" style="cursor: pointer;background-color: #cbedcb;">
        <td style="text-align:center"><div class=\"col-md-1\"><i class=\"fa fa-plus SaveRMUStyle\"></i></div></td>
        <td style="text-align:center"><input id="inputFBShomareh-${Code}" type="text" class="form-control"/></td>
        <td style="text-align:center">
           <div class="row">
            <div class="col-10"><input id="inputSharh-${Code}" type="text" class="form-control"/></div>
            <div class="col-2"><input id="ckKharidTajhizat-${Code}" type="checkbox" /><label>خرید تجهیزات</label></div>
           </div>
        </td>
        <td style="text-align:center"><select style="width: 100%;" id="selectVahed-${Code}"></select></td>
        <td style="text-align:center"><input id="inputBahayeVahed-${Code}" type="text" class="form-control"/></td>
        <td style="text-align:center" id="meghdarFasl-${Code}"></td>
        <td></td>
        <td><button type=\"button\" onclick=\"SaveNewRMUClick($(this))\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button></td>
        </tr>
        `);


    var strOption = '';
    $.each(lstVaheds, function () {
        strOption += `<option value="${this.id}">${this.name}</option>`;
    });

    RowNew.find('select').append(strOption)

    tbody.append(RowNew);

    container.append(table);

    container.on('keydown', '.riz-row-data input,.riz-input-row input', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();

            const $row = $(this).closest('tr');
            const $elements = $row.find('input, button'); // همه‌ی inputها و دکمه Save در همان ردیف
            let idx = $elements.index(this);

            // حرکت به المنت بعدی که disabled نیست
            let next = null;
            for (let i = idx + 1; i < $elements.length; i++) {
                if (!$elements.eq(i).is(':disabled')) {
                    next = $elements.eq(i);
                    break;
                }
            }

            if (next && next.length) {
                next.focus().select();
            } else {
                // اگر به آخر رسید، برو روی دکمه Save
                const $saveBtn = $row.find('button');
                if ($saveBtn.length) {
                    $saveBtn.focus().select();
                    // اگر می‌خوای آخرین Enter دکمه Save رو هم کلیک کنه:
                    // $saveBtn.click();
                }
            }
        }
    });


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
        debugger;

        const showOnlyNonZero = $(this).is(':checked');
        $(`#barAvordContainer-${Code} .main-row`).each(function () {
            debugger;
            const meghdarText = $(this).find('[id^="meghdarFasl_"], [id^="meghdarFaslStar_"]').text().replace(/,/g, '');
            const EnNum = meghdarText != "" ? convertPersianToEnglish(meghdarText) : "0";
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

        $('#rowFaslNew-' + Code).show();
    });
    //function KharidTajhizatChange(Id, blnKharidTajhizat, code)


    $('[id^="KharidTajhizat_"]').off('change').on('change', function () {
        debugger;
        thisId = this.id.split('_');
        Shomareh = thisId[2];
        var vardata = new Object();
        vardata.Id = thisId[1];
        vardata.KharidTajhizat = this.checked;
        $.ajax({
            type: "POST",
            url: '/ItemFBStar/UpdateItemFBStar',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                info = data.split('_');
                if (info[0] == "OK") {
                    debugger;


                    const row = $(this).closest('tr');
                    const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                    const tdJameFasl = $('#tdjameFasl-' + Code);
                    const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                    const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                    const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                    const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                    const MeghdarJoz = row.find('#spanMeghdarJozStar');

                    var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                    var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
                    var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                    let num = parseFloat(info[1]);

                    let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                    let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                    meghdarFasl.html(formatNumber(info[1]));
                    bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * info[1]));
                    MeghdarJoz.html(num);
                    tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                    tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));


                    JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    dJameFaslAll = parseFloat(info[2]) + JameFasl;
                    tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                    JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    dJameFaslAllBaZarib = parseFloat(info[3]) + JameFaslBaZarib;
                    tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));

                    toastr.success('اطلاعات با موفقیت ویرایش شد.', 'موفق');
                    GetBarAvord(Code, "");
                }
            },
            error: function (msg) {
                toastr.error('مشکل در درج اطلاعات', 'خطا');
            }
        });
    });
    initRowEvents(RowNew);
}

function DeleteItemFB(obj, Shomareh, Code) {

    debugger;

    thisId = obj.id.split('_');
    var vardata = new Object();
    vardata.Id = thisId[1];

    $.ajax({
        type: "POST",
        url: '/ItemFBStar/DeleteItemFBStar',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            info = data.split('_');
            if (info[0] == "OK") {

                debugger;

                GetBarAvord(Code, "");
                Shomareh = thisId[2];

                var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
                var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * parseFloat(info[1])));

                meghdarFasl.html(formatNumber(info[1]));
                bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * parseFloat(info[1])));
                //MeghdarJoz.html(num);
                tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));

                JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                dJameFaslAll = parseFloat(info[2]) + JameFasl;
                tdJameFaslAll.html(dJameFaslAll.toFixed(0));

                JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                dJameFaslAllBaZarib = parseFloat(info[3]) + JameFaslBaZarib;
                tdJameFaslAllBaZarib.html(dJameFaslAllBaZarib.toFixed(0));


                toastr.success('اطلاعات با موفقیت حذف شد.', 'موفق');
            }
        },
        error: function (msg) {
            toastr.error('مشکل در درج اطلاعات', 'خطا');
        }
    });
}

function initRowEvents($row) {
    // ترتیب فوکوس
    var $focusables = $row.find('input, select, button.ButtonRowsSaveStyle')
        .filter(':visible:enabled');

    $focusables.on('keydown', function (e) {

        // فقط روی Enter
        if (e.key !== 'Enter' && e.which !== 13) return;

        // اگر دکمه Save هست → اجازه بده Enter رفتار پیشفرض داشته باشد (عمل Save انجام شود)
        if ($(this).is('button.ButtonRowsSaveStyle')) {
            return; // هیچ preventDefault نکن → خودش submit می‌شود
        }

        // بقیه موارد: رفتار اینتر را override کنیم
        e.preventDefault();
        var idx = $focusables.index(this);

        // برو به بعدی
        if (idx > -1 && idx < $focusables.length - 1) {
            $focusables.eq(idx + 1).focus();
        } else {
            // آخرین ورودی → بایست روی دکمه save
            $focusables.eq($focusables.length - 1).focus();
        }
    });
}



const getInputClass = (f) =>
    `form-control form-control-sm ${f.isEnteringValue && f.essentialValue ? 'essentialValue' : ''}`;

const getDisabledAttr = (f) => f.isEnteringValue ? '' : 'disabled';


function EditRizMetreRow(rowEl, rizMetreId, Shomareh, rizId, FBId, Code) {

    debugger;
    const itemsFieldsStr = rowEl.getAttribute('data-itemsFields');
    const itemsFields = JSON.parse(itemsFieldsStr);

    const $row = $(rowEl);
    if ($row.hasClass('editing')) {
        UpdateRizMetreFromRow($row, rizMetreId, Shomareh, rizId, FBId, Code);
        return;
    }
    //if ($row.hasClass('editing')) return;

    // بستن سایر ردیف‌های در حال ویرایش
    $('.riz-row-data.editing').each(function () {
        UpdateRizMetreFromRow($(this), rizMetreId, Shomareh, rizId, FBId, Code);

        return;
        //cancelEditRow($(this), rizMetreId, Shomareh, rizId, FBId);
    });

    $row.addClass('editing');

    $row.find('td').each(function (index) {

        const $td = $(this);
        const text = $td.find('span').html();

        if (index === 0 || index === 7) return;

        if (index === 2 || index === 3 || index === 4 || index === 5 || index === 6) {
            const field = itemsFields.find(f => f.fieldType === (index - 1));

            // ساخت input با تنظیمات پیش‌فرض
            let inputHtml = `<input class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}"`;

            if (field) {
                // اگر isEnteringValue=false بود، input غیرفعال شود
                if (field.isEnteringValue === false) {
                    inputHtml += ' disabled';
                }
                else {
                    if (field.essentialValue) {
                        inputHtml = inputHtml.replace('class="', 'class="essentialValue ');
                    }
                }
            }

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
      onclick="event.stopPropagation();UpdateRizMetreFromRow($(this),'${rizMetreId}','${Shomareh}','${rizId}','${FBId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
	`);

    $row.find('td').eq(1).find('input').focus().select();
}


function cancelEditRow(row, rizMetreId, Shomareh, rizId1, FBId) {
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
		<i class="fa fa-trash DelRMUStyle ms-2" onclick="event.stopPropagation();DeleteRizMetre(this,'${rizMetreId}','${Shomareh}','${rizId1}','${FBId}')"></i>
	`);
    }
}

function convertPersianToEnglish(str) {
    if (!str) return str;
    // تبدیل ارقام فارسی و عربی به انگلیسی
    const persianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    const arabicDigits = ['٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];
    let result = str;
    for (let i = 0; i < 10; i++) {
        result = result.replace(new RegExp(persianDigits[i], 'g'), i)
            .replace(new RegExp(arabicDigits[i], 'g'), i);
    }
    // تبدیل جداکننده اعشار فارسی یا عربی به "."
    result = result.replace(/[٫٬]/g, '.');
    return result;
}


function UpdateRizMetreFromRow(el, rizMetreId, Shomareh, rizId1, FBId, Code) {
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

        BarAvordUserId = $('#HDFBarAvordUserID').val();
        NoeFBId = parseInt($('#HDFNoeFB').val());
        Year = parseInt($('#HDFYear').val());

        var vardata = new Object();
        vardata.Id = rizId;
        vardata.Sharh = values[0];
        vardata.Tedad = values[1] != "" ? values[1] : null;
        vardata.Tool = values[2] != "" ? values[2] : null;
        vardata.Arz = values[3] != "" ? values[3] : null;
        vardata.Ertefa = values[4] != "" ? values[4] : null;
        vardata.Vazn = values[5] != "" ? values[5] : null;
        vardata.Des = values[6];
        vardata.NoeFBId = NoeFBId;
        vardata.Year = Year;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Code = Code;
        vardata.LevelNumber = 1;
        $.ajax({
            type: "POST",
            url: '/RizMetreUserFromShowBarAvord/UpdateRizMetreUsersFrmShowBarAvord',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {
                    row.removeClass('editing');
                    debugger;
                    //let num = parseFloat(info[1]);
                    //let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text()));

                    refreshBaseItem(el, info[1], info[2], info[3], info[4], Code, Shomareh, rizId1);

                    //meghdarFasl.html(formatNumber(info[2]));
                    //bahayeKolFasl.html(formatNumber((bahayeVahedFasl1 * info[2]).toFixed(0)));
                    //MeghdarJoz.html(parseFloat(num.toFixed(2)));

                    //tdJameFasl.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    //tdJameFaslBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));
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

    }
    // ارسال به سرور با Ajax
    // $.post('/api/update-riz', updatedData, function (res) {
    // 	alert('ویرایش انجام شد');
    // });

    // برگشت به حالت نمایش
}

function SaveNewRMUClick($btn) {
    var $button = $btn instanceof jQuery ? $btn : $($btn);
    var $row = $button.closest('tr');
    var code = ($row.data('riz-code') || '').toString(); // مثلا "12"

    var $fbShomareh = $row.find('input[id^="inputFBShomareh-"]');
    var $sharh = $row.find('input[id^="inputSharh-"]');
    var $vahed = $row.find('select[id^="selectVahed-"]');
    var $bahayeVahed = $row.find('input[id^="inputBahayeVahed-"]');
    var $kharidTajhizat = $row.find('input[id^="ckKharidTajhizat-"]');

    // پاک کردن خطاهای قبلی
    $row.find('.blinking').removeClass('blinking');

    var valid = true;
    var firstInvalid = null;
    var errorMsg = null;

    function markInvalid($el, msg) {
        $el.addClass('blinking');
        if (!firstInvalid) {
            firstInvalid = $el;
            errorMsg = msg;
        }
        valid = false;
    }

    var fbVal = ($fbShomareh.val() || '').trim();
    var sharhVal = ($sharh.val() || '').trim();
    var vahedVal = ($vahed.val() || '').trim();
    var bahayeVal = ($bahayeVahed.val() || '').trim();
    debugger;
    var kharidTajhizat = $kharidTajhizat.prop("checked");

    // 1) خالی نبودن فیلدها
    if (!fbVal) markInvalid($fbShomareh, 'شماره فهرست‌بها را وارد کنید.');
    if (!sharhVal) markInvalid($sharh, 'شرح را وارد کنید.');
    if (!vahedVal) markInvalid($vahed, 'واحد را انتخاب کنید.');
    if (!bahayeVal) markInvalid($bahayeVahed, 'بهای واحد را وارد کنید.');

    // 2) inputFBShomareh عددی باشد
    if (fbVal && !/^\d+$/.test(fbVal)) {
        markInvalid($fbShomareh, 'شماره فهرست‌بها باید فقط شامل ارقام باشد.');
    }

    // 3) inputFBShomareh با data-riz-code شروع شود و دقیقا 6 رقم باشد
    if (fbVal) {
        if (fbVal.length !== 6) {
            markInvalid($fbShomareh, 'شماره فهرست‌بها باید دقیقا ۶ رقم باشد.');
        } else if (code && !fbVal.startsWith(code)) {
            markInvalid(
                $fbShomareh,
                'شماره فهرست‌بها باید با کد ریز فصل (' + code + ') شروع شود.'
            );
        }
    }

    // 4) inputBahayeVahed دسیمال باشد
    if (bahayeVal) {
        var normalized = bahayeVal.replace(',', '.'); // اجازه استفاده از , نیز
        if (!/^\d+(\.\d+)?$/.test(normalized)) {
            markInvalid($bahayeVahed, 'بهای واحد باید عدد اعشاری معتبر باشد (مثال: 123 یا 123.45).');
        } else {
            bahayeVal = normalized; // آماده ارسال به سرور
        }
    }

    // اگر هر خطایی وجود داشت
    if (!valid) {
        toastr.info(errorMsg || 'اطلاعات وارد شده صحیح نمی‌باشند.', 'اطلاع');
        if (firstInvalid) {
            firstInvalid.focus(); // فوکوس روی اولین فیلد ایراددار
        }
        return;
    }

    BarAvordUserId = $('#HDFBarAvordUserID').val();

    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());


    var vardata = new Object();

    vardata.BaravordId = BarAvordUserId;
    vardata.FBShomareh = fbVal;
    vardata.Sharh = sharhVal;
    vardata.VahedId = parseInt(vahedVal);
    vardata.BahayeVahed = parseFloat(bahayeVal);
    vardata.kharidTajhizat = kharidTajhizat;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;

    debugger;

    $.ajax({
        type: "POST",
        url: '/ItemFBStar/SaveItemFBStar',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == "OK") {
                GetBarAvord(code, "")
                toastr.success('اطلاعات با موفقیت ذخیره شد.', 'موفق');
            }
            else if (data == "repeat") {
                toastr.info('کد تکراری میباشد.', 'اطلاع');
            }
        },
        error: function () {
            toastr.error('خطا در ذخیره اطلاعات.', 'خطا');
        }
    });

}

function SaveRMUClick(object, Shomareh, rizId, Code) {
    object.parent().parent().find('input[type=text]').each(function () {
        $(this).removeClass('ErrorValueStyle');
    });


    debugger;
    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    firstObj = null;
    findFirstObj = false;
    object.parent().parent().find('input[type=text]').each(function () {
        if ($(this).attr('id') == 'txtSharh')
            Sharh = $(this).val();
        else if ($(this).attr('id') == 'txtTedad') {
            if ($(this).hasClass('essentialValue')) {
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
        }
        else if ($(this).attr('id') == 'txtTool') {
            if ($(this).hasClass('essentialValue')) {
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
        }
        else if ($(this).attr('id') == 'txtArz') {
            if ($(this).hasClass('essentialValue')) {
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
        }
        else if ($(this).attr('id') == 'txtErtefa') {
            if ($(this).hasClass('essentialValue')) {
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
        }
        else if ($(this).attr('id') == 'txtVazn') {
            if ($(this).hasClass('essentialValue')) {
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
        }
        else if ($(this).attr('id') == 'txtDes')
            Des = $(this).val();
    });

    if (Check) {
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        Year = parseInt($('#HDFYear').val());
        NoeFBId = parseInt($('#HDFNoeFB').val());

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
        vardata.NoeFBId = NoeFBId;
        $.ajax({
            type: "POST",
            url: '/RizMetreUserFromShowBarAvord/ConfirmRizMetreUsersFromShowBarAvord',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {


                    ///////////////
                    ///////////////

                    refreshBaseItem(object, info[1], info[2], info[3], info[4], Code, Shomareh, rizId);

                    //////////////////
                    //////////////////

                    inputBahayeVahed = $('#txtBahayeVahed-' + rizId);
                    const val = parseFloat(inputBahayeVahed.val().replace(/,/g, '')); // حذف کاما و تبدیل به عدد
                    if (val === 0) {
                        inputBahayeVahed.addClass('blinking');
                    } else {
                        inputBahayeVahed.removeClass('blinking'); // اگر نمی‌خواهید در غیر صفر بودن کلاس باقی بماند
                    }

                    GetCurrentRizMetreUsers(Shomareh, rizId, info[5], Code);

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

function refreshBaseItem(object, dMeghdarJoz, SumMeghdarJoz, JameFasl, JameFaslInZarib, Code, Shomareh, rizId) {

    debugger;
    const row = $(object).closest('tr');
    const tdJameFasl = $('#tdjameFasl-' + Code);
    const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);

    const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
    const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

    const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
    const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);

    const MeghdarJoz = row.find('#spanMeghdarJoz');

    var meghdarFasl = $('#meghdarFasl_' + Shomareh);
    var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
    var bahayeVahedFasl = $('#bahayeVahedFasl_' + Shomareh);

    var txtBahayeVahed = $('#txtBahayeVahed-' + rizId);

    let num = parseFloat(dMeghdarJoz);
    let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

    let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(txtBahayeVahed.val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

    meghdarFasl.html(formatNumber(SumMeghdarJoz));
    bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * SumMeghdarJoz));
    MeghdarJoz.html(num);
    tdJameFasl.html(formatNumber(parseFloat(JameFasl).toFixed(0)));
    tdJameFaslBaZarib.html(formatNumber(parseFloat(JameFaslInZarib).toFixed(0)));

    JameFaslStar = parseFloat(convertPersianToEnglish(tdJameFaslStar.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
    dJameFaslAll = parseFloat(JameFasl) + (JameFaslStar === NaN ? JameFaslStar : 0);
    tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

    JameFaslStarBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslStarBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
    dJameFaslAllBaZarib = parseFloat(JameFaslInZarib) + (JameFaslStarBaZarib === NaN ? JameFaslStarBaZarib : 0);
    tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));
}



//ریزه متره های عادی
function GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code) {
    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.ItemFBShomareh = Shomareh;
    $.ajax({
        url: "/RizMetreUserFromShowBarAvord/GetCurrentRizMetreUsersForShowBarAvord",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            rizMetreUsers = response.rizMetreUsers;
            lstItemsFields = response.lstItemsFields;
            debugger;
            const rizRow = $(`#${rizId}`);
            const tbody = rizRow.find('tbody');
            tbody.empty(); // پاک‌کردن محتوای قدیمی

            let rizRowsHtml = '';

            if (Array.isArray(rizMetreUsers) && rizMetreUsers.length > 0) {
                rizRowsHtml += rizMetreUsers.map(riz => `
                    <tr class="riz-row-data" data-id="${riz.id}" data-itemsFields='${JSON.stringify(lstItemsFields).replace(/"/g, '&quot;')}' onclick="EditRizMetreRow(this,'${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')" style="cursor:pointer;">
					<td style="text-align:center">${riz.shomarehNew}</td>
					<td><span>${riz.sharh}</span></td>
					<td style="text-align:center"><span>${riz.tedad == null ? '' : riz.tedad}</span></td>
					<td style="text-align:center"><span>${riz.tool == null ? '' : riz.tool}</span></td>
					<td style="text-align:center"><span>${riz.arz == null ? '' : riz.arz}</span></td>
					<td style="text-align:center"><span>${riz.ertefa == null ? '' : riz.ertefa}</span></td>
					<td style="text-align:center"><span>${riz.vazn == null ? '' : riz.vazn}</span></td>
					<td style="text-align:center"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : riz.meghdarJoz}</span></td>
					<td><span>${riz.des ?? ''}</span></td>
                    <td style="text-align:center"><i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetre(this,'${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')"></i></td>
                    </tr>
                `).join('');
            }




            field = lstItemsFields;
            const defaultField = { id: null, itemShomareh: Shomareh || '', fieldType: null, vahed: '', isEnteringValue: false, essentialValue: false };

            const getField = (i) => (i >= 0 && i < field.length ? field[i] : defaultField);

            const f0 = getField(0);
            const f1 = getField(1);
            const f2 = getField(2);
            const f3 = getField(3);
            const f4 = getField(4);

            rizRowsHtml += `
<tr class="riz-input-row">
  <td style="text-align:center"></td>
  <td><input id="txtSharh" class="form-control form-control-sm" type="text" /></td>

  <td style="text-align:center">
    <input id="txtTedad" class="${getInputClass(f0)}" type="text" ${getDisabledAttr(f0)} />
  </td>

  <td style="text-align:center">
    <input id="txtTool" class="${getInputClass(f1)}" type="text" ${getDisabledAttr(f1)} />
  </td>

  <td style="text-align:center">
    <input id="txtArz" class="${getInputClass(f2)}" type="text" ${getDisabledAttr(f2)} />
  </td>

  <td style="text-align:center">
    <input id="txtErtefa" class="${getInputClass(f3)}" type="text" ${getDisabledAttr(f3)} />
  </td>

  <td style="text-align:center">
    <input id="txtVazn" class="${getInputClass(f4)}" type="text" ${getDisabledAttr(f4)} />
  </td>

  <td style="text-align:center">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td>
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center">
    <button type="button"
      onclick="SaveRMUClick($(this),'${Shomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
  </td>
</tr>
`;

            // اضافه کردن ردیف ورودی جدید
            //rizRowsHtml += `
            //    <tr class="riz-input-row">
            //        <td></td>
            //        <td><input id="txtSharh" class="form-control form-control-sm" type="text"/></td>
            //        <td><input id="txtTedad" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtTool" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtArz" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtErtefa" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtVazn" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtMeghdarJoz" disabled class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtDes" class="form-control form-control-sm" type="text"/></td>
            //        <td>
            //            <button type="button" onclick="SaveRMUClick($(this),'${Shomareh}','${rizId}','${Code}')" class="ButtonRowsSaveStyle">
            //                <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
            //            </button>
            //        </td>
            //    </tr>
            //`;

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

function DeleteRizMetre(object, RizMetreId, Shomareh, rizId, FBId, Code) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.Id = RizMetreId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFBId = NoeFBId;

    $.ajax({
        url: "/RizMetreUserFromShowBarAvord/DeleteRizMetre",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
                GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code);

                refreshBaseItem(object, info[1], info[2], info[3], info[4], Code, Shomareh, rizId);
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}



