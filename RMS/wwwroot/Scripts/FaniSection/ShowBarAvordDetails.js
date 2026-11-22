// جلوگیری از ارسال چندباره عملیات و نمایش وضعیت انتظار دکمه
function beginBarAvordButtonWait(button) {
    var $button = button && button.jquery ? button.first() : $(button).first();
    if (!$button.length) return true;
    if ($button.data('barAvordBusy') === true) return false;
    $button.data('barAvordBusy', true);
    $button.data('barAvordOriginalHtml', $button.html());
    $button.prop('disabled', true).attr('aria-busy', 'true')
        .addClass('baravord-operation-waiting')
        .html('<i class="fa fa-spinner fa-spin" aria-hidden="true"></i>');
    return true;
}
function endBarAvordButtonWait(button) {
    var $button = button && button.jquery ? button.first() : $(button).first();
    if (!$button.length || $button.data('barAvordBusy') !== true) return;
    var originalHtml = $button.data('barAvordOriginalHtml');
    $button.html(originalHtml == null ? '<i class="fa fa-save"></i>' : originalHtml)
        .prop('disabled', false).removeAttr('aria-busy')
        .removeClass('baravord-operation-waiting')
        .removeData('barAvordBusy').removeData('barAvordOriginalHtml');
}
﻿function renderTable(data, Stars, Code, ItemId, faslAsli) {

    const renderPerfStartedAt = window.performance.now();
    const renderPerf = {};
    let renderPerfMark = renderPerfStartedAt;
    const markRenderPerf = function (stage) {
        const now = window.performance.now();
        renderPerf[stage] = Number((now - renderPerfMark).toFixed(1));
        renderPerfMark = now;
    };

    const container = $(`#barAvordContainer-${ItemId}`);
    container.empty();

    markRenderPerf('containerReset');

    const table = $(`
		<table class="table table-item table-bordered fixed-table mb-2">
			<thead>
				<tr style="background-color: #aa87ff;">
					<th style="width: 5🔄%;text-align:center;color: #3a007a;font-size: 12px;">*</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">شماره فهرست بها</th>
					<th style="width: 46%;color: #3a007a;font-size: 16px;">شرح فهرست بها</th>
					<th style="width: 8%;text-align:center;color: #3a007a;font-size: 12px;">واحد</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">بهای واحد</th>
					<th style="width: 10%;text-align:center;color: #3a007a;font-size: 12px;">
                    <label class="ShowNoneZeroClass" style="font-size: 9px;margin-bottom: 0px;">
                    <input type="checkbox" id="chkNonZeroMeghdar-${Code}" style="margin-right: 5px;"/>
                      <span style="font-size:12px">
                            مقدار < > 0 
                      </span>      
                    </label>
                    </th>
                    <th style="width: 4%;text-align:center;color: #3a007a;font-size: 12px;">ضریب</th>
					<th style="width: 10%;text-align:center;color: #3a007a;font-size: 12px;">بهای کل</th>
					<th style="width: 3%;text-align:center;color: #3a007a;font-size: 12px;">عملیات</th>
				</tr>
			</thead>
			<tbody class="tbodyFosoul"></tbody>
		</table>
	`);

    const tbody = table.find('tbody');
    markRenderPerf('tableShellBuild');

    var index = 0;
    data.forEach((item) => {

        const itemFields = Array.isArray(item.itemsFields) ? item.itemsFields : [];
        var Field0 = itemFields[0] == undefined ? "" : itemFields[0].vahed;
        var Field1 = itemFields[1] == undefined ? "" : itemFields[1].vahed;
        var Field2 = itemFields[2] == undefined ? "" : itemFields[2].vahed;
        var Field3 = itemFields[3] == undefined ? "" : itemFields[3].vahed;
        var Field4 = itemFields[4] == undefined ? "" : itemFields[4].vahed;
        var Field5 = itemFields[5] == undefined ? "" : itemFields[5].vahed;

        const rizId = `riz-${ItemId}-${index}`;
        const clickableClass = 'clickable-row table-active';
        const BahayeKol = parseFloat(item.bahayeKol);
        const BahayeKolFix = BahayeKol.toFixed(0);
        BahayeVahedNew = item.bahayeVahedNew;
        bahayeVahedSetEn = item.bahayeVahed;
        ItemZaribSetEn = item.itemZarib;
        strStar = '';
        if (BahayeVahedNew != 0) {
            bahayeVahedSetEn = BahayeVahedNew;
            strStar += `<div class="col-12" id="span${item.itemFbShomareh}">
                        <div class="row"><span style="font-size:25px">*</span><input  style="font-size:20px" class="ReturnItemsStar" type="submit" value="🔄" title="بازنشانی"
                        onclick="StarReturn(this,'${item.fbId}','${item.itemFbShomareh}')"/></div>
                        </div>`;
        }
        else
            strStar += `<div class="col-12" id="span${item.itemFbShomareh}"></div>`;

        bahayeVahedSet = toPersianDigits(formatNumber(bahayeVahedSetEn));
        ItemZaribSet = toPersianDigits(formatNumber(ItemZaribSetEn));

        const isBlinking = item.meghdar > 0 && (!bahayeVahedSet || parseFloat(bahayeVahedSetEn) === 0);
        const bahayeVahedInput = `
            <input id="txtBahayeVahed-${rizId}" onchange="BahayeVahedNewMeghdarChange(this,'${item.fbId}','${item.itemFbShomareh}')" type="text" class="form-control form-control-sm ${isBlinking ? 'blinking' : ''}" 
                value="${bahayeVahedSet ?? ''}" 
                style="text-align:center;border:unset" />
            `;

        const ItemZaribInput = `
            <input id="txtItemZarib-${rizId}" onchange="ItemZaribChange(this,'${item.fbId}','${item.itemFbShomareh}')" type="text" class="form-control form-control-sm"
                value="${ItemZaribSet ?? ''}" 
                style="text-align:center;border:unset" />
            `;

        var stritemFbShomareh = '';
        if (faslAsli) {
            stritemFbShomareh = `
            <span>
                ${toPersianDigits(item.itemFbShomareh)}
            </span>
            `;
        }
        else {
            stritemFbShomareh = `
            <span>
                ${toPersianDigits(item.itemFbShomarehMain)}
            </span>
            `;
        }

        const mainRow = $(`
        <tr id="rowFasl-${rizId}" class="main-row ${clickableClass}" data-riz-id="${rizId}" style="cursor: pointer;">
        <td style="text-align:center;width:5%">${strStar}</td>
        <td style="text-align:center;width:7%">  ${stritemFbShomareh}</td>
        <td style="word-break: break-word; white-space: normal;text-align:right;width:46%">${toPersianDigits(item.sharh)}</td>
        <td style="text-align:center;width:8%">${item.vahed}</td>
        <td style="text-align:center;width:7%;padding:0px">${bahayeVahedInput}</td>
        <td style="text-align:center;width:10%"><span id="meghdarFasl_${item.itemFbShomareh}">${formatNumber(item.meghdar)}</span></td>
        <td style="text-align:center;width:4%;padding:0px">${ItemZaribInput}</td>
        <td style="text-align:center;width:10%"><span id="bahayeKolFasl_${item.itemFbShomareh}">${formatNumber(BahayeKolFix)}</span></td>
        <td style="text-align:center;display:none"><span id="bahayeVahedFasl_${item.bahayeVahed}">${item.bahayeVahed}</span></td>
        <td></td>
        </tr>
        `);


        let rizRowsHtml = '';


        if (Array.isArray(item.rizMetre) && item.rizMetre.length > 0) {
            rizRowsHtml += item.rizMetre.map(riz =>
                `
		<tr class="riz-row-data" data-id="${riz.id}" data-itemsFields='${JSON.stringify(itemFields).replace(/"/g, '&quot;')}'
                onclick="EditRizMetreRow(this,'${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')" style="cursor:pointer;">
		<td style="text-align:center;width:4%">${riz.shomarehNew == null ? '' : toPersianDigits(riz.shomarehNew)}</td>
        <td style="text-align:center;width:6%"><span>${toPersianDigits(riz.tajmieItem)}</span></td>
		<td style="width:22%"><span>${toPersianDigits(riz.sharh)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tedad == null ? '' : formatNumber(riz.tedad)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tool == null ? '' : formatNumber(riz.tool)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.arz == null ? '' : formatNumber(riz.arz)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.ertefa == null ? '' : formatNumber(riz.ertefa)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.vazn == null ? '' : formatNumber(riz.vazn)}</span></td>
		<td style="text-align:center;width:7%"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : formatNumber(riz.meghdarJoz)}</span></td>
		<td style="width:20%"><span>${toPersianDigits(riz.des ?? '')}</span></td>
		<td style="text-align:center;width:7%">
			<i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetre(this,'${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')"></i>
		</td>
		</tr>
		`).join('');
        }


        const field = itemFields;
        const defaultField = { id: null, itemShomareh: item.itemShomareh || '', fieldType: null, vahed: '', isEnteringValue: false, essentialValue: false };

        const getField = (i) => (i >= 0 && i < field.length ? field[i] : defaultField);

        const f0 = getField(0);
        const f1 = getField(1);
        const f2 = getField(2);
        const f3 = getField(3);
        const f4 = getField(4);

        rizRowsHtml += `
<tr class="riz-input-row">
  <td style="text-align:center;width:4%"></td>
  <td style="text-align:center;width:6%">

  <div class="d-flex align-items-center gap-2">
            <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="ShowSelectTajmie('${item.itemFbShomareh}-New')"></i>
        
            <input id="txtTajmieItem-${item.itemFbShomareh}-New" class="form-control form-control-sm" type="text" onchange="TajmieItemChange(this)" />
       
    </div>
  </td>
  <td style="width:22%">
    <input id="txtSharh" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTedad" class="${getInputClass(f0)}" type="text" ${getDisabledAttr(f0)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTool" class="${getInputClass(f1)}" type="text" ${getDisabledAttr(f1)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtArz" class="${getInputClass(f2)}" type="text" ${getDisabledAttr(f2)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtErtefa" class="${getInputClass(f3)}" type="text" ${getDisabledAttr(f3)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtVazn" class="${getInputClass(f4)}" type="text" ${getDisabledAttr(f4)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td style="width:20%">
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
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


        const detailRow = $(`
			<tr id="${rizId}" class="riz-row" style="display: none;">
				<td colspan="8">
					<table class="table table-sm table-bordered mb-0">
						<thead style="width:100%">
							<tr style="background-color: #c1b9e7;">
								<th style="width: 4%;text-align: center;vertical-align: top;">ردیف</th>
								<th style="width: 6%;text-align: center;vertical-align: top;">فعالیت</th>
								<th style="width: 22%;text-align: center;vertical-align: top;">شرح ریزه متره</th>
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
                                    وزن یا...
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
    markRenderPerf('regularItemsBuild');


    Stars.forEach((item) => {

        const itemFields = Array.isArray(item.itemsFields) ? item.itemsFields : [];
        currentId = item.id;

        const rizId = `riz-${currentId}-${index}`;

        const ItemZaribStarSet = item.itemZaribStar;

        const ItemZaribStarInput = `
            <input id="txtItemZaribStar-${rizId}" onchange="ItemZaribStarChange(this,'${currentId}','${item.shomareh}')" type="text" class="form-control form-control-sm"
                value="${ItemZaribStarSet ?? ''}" 
                style="text-align:center;border:unset" />
            `;

        KharidTajhizat = item.blnKharidTajhizat == null ? false : item.blnKharidTajhizat;
        strKharidTajhizat = KharidTajhizat == true ? "ت" : "";
        checked = KharidTajhizat == true ? "checked" : "";
        const mainRowStar = $(`
        <tr id="rowFasl-${rizId}" class="main-row clickable-row" data-riz-id="${rizId}" style="cursor: pointer;">
        <td style="text-align:center;width:5%">
        <span>
            ${strKharidTajhizat}
        </span>
        <span style="font-size:20px">
            * 
        </span>
        </td>
        <td style="text-align:center;width:7%">${toPersianDigits(item.shomareh)}</td>
        <td style="word-break: break-word; white-space: normal;text-align:right;width:46%">
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
        <td style="text-align:center;width:8%">${item.vahedName}</td>
        <td style="text-align:center;width:7%" id="bahayeVahedFaslStar_${item.shomareh}">${toPersianDigits(item.bahayeVahed)}</td>
        <td style="text-align:center;width:10%" id="meghdarFaslStar_${item.shomareh}">${toPersianDigits(formatNumber(item.meghdar))}</td>
        <td style="width:4%">${ItemZaribStarInput}</td>
        <td style="text-align:center;width:10%" id="bahayeKolFaslStar_${item.shomareh}">${formatNumber(item.bahayeKol)}</td>
        <td>
            <i id="iDelete_${currentId}_${item.shomareh}" onclick="event.stopPropagation();DeleteItemFB(this,'${item.shomareh}','${Code}',${ItemId})" class="fa fa-trash DelRMUStyle" ></i>
        </td>
        </tr>
        `);

        let rizRowsHtmlStar = '';


        if (Array.isArray(item.rizMetre) && item.rizMetre.length > 0) {
            rizRowsHtmlStar += item.rizMetre.map(riz =>
                `
		<tr class="riz-row-data" data-id="${riz.id}" data-itemsFields='${JSON.stringify(itemFields).replace(/"/g, '&quot;')}'
        onclick="EditRizMetreRowStar(this,'${riz.id}','${item.shomareh}','${rizId}','${Code}')" style="cursor:pointer;">
		<td style="text-align:center;width:4%">${riz.shomarehNew == null ? '' : toPersianDigits(riz.shomarehNew)}</td>
		<td style="text-align:center;width:6%">
            <span>${riz.tajmieItem == null ? '' : toPersianDigits(riz.tajmieItem)}</span>
        </td>
		<td style="width:22%"><span>${toPersianDigits(riz.sharh)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tedad == null ? '' : formatNumber(riz.tedad)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tool == null ? '' : formatNumber(riz.tool)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.arz == null ? '' : formatNumber(riz.arz)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.ertefa == null ? '' : formatNumber(riz.ertefa)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.vazn == null ? '' : formatNumber(riz.vazn)}</span></td>
		<td style="text-align:center;width:7%"><span id="spanMeghdarJozStar">${riz.meghdarJoz == null ? '' : formatNumber(riz.meghdarJoz)}</span></td>
		<td style="width:20%"><span>${toPersianDigits(riz.des ?? '')}</span></td>
		<td style="text-align:center;width:7%">
			<i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetreStar($(this),'${riz.id}','${item.shomareh}','${rizId}','${Code}')"></i>
		</td>
		</tr>
		`).join('');
        }

        rizRowsHtmlStar += `
<tr class="riz-input-row">
  <td style="text-align:center;width:4%"></td>
  <td style="text-align:center;width:6%">

   <div class="d-flex align-items-center gap-2">
            <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="ShowSelectTajmie('Star-${Code}-New')"></i>
        
            <input id="txtTajmieItem-Star-${Code}-New" class="form-control form-control-sm" type="text" onchange="TajmieItemChange(this)" />
       
    </div>
  </td>
  <td style="width:22%">
    <input id="txtSharh-${Code}" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTedad-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTool-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtArz-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtErtefa-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtVazn-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtMeghdarJoz-${Code}" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td style="width:20%">
    <input id="txtDes-${Code}" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
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
								<th style="width: 4%;text-align:center">ردیف</th>
								<th style="width: 6%;text-align:center">فعالیت</th>
								<th style="width: 22%;">شرح ریزه متره</th>
								<th style="width: 7%;text-align:center">تعداد</th>
								<th style="width: 7%;text-align:center">طول</th>
								<th style="width: 7%;text-align:center">عرض</th>
								<th style="width: 7%;text-align:center">ارتفاع</th>
								<th style="width: 7%;text-align:center">وزن یا...</th>
								<th style="width: 7%;text-align:center">مقدار جزء</th>
								<th style="width: 20%;">توضیحات</th>
								<th style="width: 6%;text-align:center">عملیات</th>
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
    markRenderPerf('starItemsBuild');

    var RowNew = '';


    if (faslAsli) {

        RowNew = $(`
        <tr id="rowFaslNew-${ItemId}" class="main-row" data-riz-code="${Code}" style="cursor: pointer;background-color: #cbedcb;">
        <td style="text-align:center;width:5%"><div class=\"col-md-1\"><i class=\"fa fa-plus SaveRMUStyle\"></i></div></td>
        <td style="text-align:center;width:7%"><input id="inputFBShomareh-${ItemId}" type="text" class="form-control"/></td>
        <td style="text-align:center;width:46%">
           <div class="row">
            <div class="col-10"><input id="inputSharh-${ItemId}" type="text" class="form-control"/></div>
            <div class="col-2"><input id="ckKharidTajhizat-${ItemId}" type="checkbox" /><label>خرید تجهیزات</label></div>
           </div>
        </td>
        <td style="text-align:center;width:8%"><select style="width: 100%;" id="selectVahed-${ItemId}"></select></td>
        <td style="text-align:center;width:7%"><input id="inputBahayeVahed-${ItemId}" type="text" class="form-control"/></td>
        <td style="text-align:center;width:10%" id="meghdarFasl-${ItemId}"></td>
        <td style="width:4%">
            <input id="txtItemZaribStar-${ItemId}" type="text" class="form-control form-control-sm"
                value="" style="text-align:center;border:unset" />
        </td>
        <td style="width:10%"></td>
        <td><button type=\"button\" onclick=\"SaveNewRMUClick($(this))\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button></td>
        </tr>
        `);
        var strOption = '';
        $.each(lstVaheds, function () {
            strOption += `<option value="${this.id}">${this.name}</option>`;
        });

        RowNew.find('select').append(strOption)

        tbody.append(RowNew);
    }

    markRenderPerf('newItemRowBuild');
    container.append(table);
    markRenderPerf('tableAttach');

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


    markRenderPerf('eventBinding');
    const chapterAnimationStartedAt = window.performance.now();
    $(`#${ItemId}`).stop(true, true).slideDown(200, function () {
        const clickStartedAt = window.__showBarAvordClickStartedAt;
        console.log('[ShowBarAvord][PERF][END-TO-END] chapter detail animation completed:', {
            itemId: ItemId,
            animationMs: Number((window.performance.now() - chapterAnimationStartedAt).toFixed(1)),
            clickToVisibleMs: clickStartedAt
                ? Number((window.performance.now() - clickStartedAt).toFixed(1))
                : null
        });
    });

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

        txtSharh = rizRow.find('#txtSharh');
        txtSharh.focus();

        setTimeout(() => {
            const container = rizRow.closest('[id^="barAvordContainer-"]');

            const containerTop = container.scrollTop();
            const containerHeight = container.height();
            const containerBottom = containerTop + containerHeight;

            const rowTop = rizRow.position().top + containerTop;
            const rowBottom = rowTop + rizRow.outerHeight(true);

            // اگر پایین ردیف دیده نمی‌شود → اسکرول به اندازه لازم
            if (rowBottom > containerBottom) {
                container.stop().animate({
                    scrollTop: containerTop + (rowBottom - containerBottom) + 20
                }, 300);
            }

            // اگر بالای ردیف رفته بالا (حالت نادر)
            else if (rowTop < containerTop) {
                container.stop().animate({
                    scrollTop: rowTop - 20
                }, 300);
            }

        }, 0);


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

    //convertNumbersInPage();

    // وضعیت فیلتر برای هر برآورد و هر فصل، بین بارگذاری‌های مجدد حفظ شود.
    const nonZeroFilterStorageKey = 'showBarAvordNonZeroFilter:' + $('#HDFBarAvordUserID').val() + ':' + Code;

    $('[id^="chkNonZeroMeghdar-"]').off('change').on('change', function () {

        const showOnlyNonZero = $(this).is(':checked');
        try {
            localStorage.setItem(nonZeroFilterStorageKey, showOnlyNonZero ? '1' : '0');
        } catch (storageError) {
            console.warn('[ShowBarAvord] ذخیره وضعیت فیلتر ممکن نشد:', storageError);
        }
            isOneRowFilledRizMetre = false;
        $(`#barAvordContainer-${ItemId} .main-row`).each(function () {

            const meghdarText = $(this).find('[id^="meghdarFasl_"], [id^="meghdarFaslStar_"]').text().replace(/,/g, '');
            const EnNum = meghdarText != "" ? convertPersianToEnglish(meghdarText) : "0";
            const meghdar = parseFloat(EnNum);

            if (showOnlyNonZero) {
                if (meghdar === 0) {
                    $(this).hide();
                    $(`#${$(this).data('riz-id')}`).hide(); // مخفی‌سازی ردیف ریز مربوطه
                } else {
                    $(this).show();
                    isOneRowFilledRizMetre = true;
                }
            } else {
                $(this).show();
            }
        });

        if (!isOneRowFilledRizMetre) {
            $(`#barAvordContainer-${ItemId} .main-row`).each(function () {

                isOneRowFilledRizMetre = false;
                const meghdarText = $(this).find('[id^="meghdarFasl_"], [id^="meghdarFaslStar_"]').text().replace(/,/g, '');
                const EnNum = meghdarText != "" ? convertPersianToEnglish(meghdarText) : "0";
                const meghdar = parseFloat(EnNum);
                if (showOnlyNonZero) {
                   $(this).show();
                } else {
                    $(this).show();
                }
            });
        }

        // در حالت نمایش مقادیر غیرصفر، امکان درج آیتم ستاره‌دار غیرفعال باشد.
        $('#rowFaslNew-' + ItemId).toggle(!showOnlyNonZero);
    });
    //function KharidTajhizatChange(Id, blnKharidTajhizat, code)


    $('[id^="KharidTajhizat_"]').off('change').on('change', function () {
        thisId = this.id.split('_');
        Shomareh = thisId[2];
        Year = parseInt($('#HDFYear').val());

        var vardata = new Object();
        vardata.Id = thisId[1];
        vardata.KharidTajhizat = this.checked;
        vardata.Year = Year;

        $.ajax({
            type: "POST",
            url: '/ItemFBStar/UpdateItemFBStar',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                info = data.split('_');
                if (info[0] == "OK") {

                    //const row = $(this).closest('tr');
                    //const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                    //const tdJameFasl = $('#tdjameFasl-' + Code);
                    //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                    //const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                    //const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                    //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                    //const MeghdarJoz = row.find('#spanMeghdarJozStar');

                    //var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                    //var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
                    //var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                    //let num = parseFloat(info[1]);

                    ////let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                    ////let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                    //meghdarFasl.html(formatNumber(info[1]));
                    //bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * info[1]));
                    //MeghdarJoz.html(num);
                    //tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                    //tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));


                    //JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAll = parseFloat(info[2]) + isNaN(JameFasl) ? 0 : JameFasl;
                    //tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                    //JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAllBaZarib = parseFloat(info[3]) + isNaN(JameFaslBaZarib) ? 0 : JameFaslBaZarib;
                    //tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));


                    let num = parseFloat(info[1]);
                    bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);

                    var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                    meghdarFasl.html(formatNumber(parseFloat(info[1]).toFixed(0)));
                    bahayeKolFasl.html(formatNumber(parseFloat(info[2]).toFixed(0)));

                    Code = Shomareh.substring(0, 2);
                    refreshBaseItem(this, Shomareh, Code);

                    toastr.success('اطلاعات با موفقیت ویرایش شد.', 'موفق');

                    //GetBarAvord(thisId[1], Code, "", true);

                    GetJamBarAvordAll();
                    GetJamFosoulAll();
                }
            },
            error: function (msg) {
                toastr.error('مشکل در درج اطلاعات', 'خطا');
            }
        });
    });

    initRowEvents(RowNew);

    const initialFilterStartedAt = window.performance.now();
    const $nonZeroFilter = $(`#chkNonZeroMeghdar-${Code}`);
    let savedNonZeroFilterState = null;
    try {
        savedNonZeroFilterState = localStorage.getItem(nonZeroFilterStorageKey);
    } catch (storageError) {
        console.warn('[ShowBarAvord] خواندن وضعیت فیلتر ممکن نشد:', storageError);
    }
    // در اولین بارگذاری فیلتر روشن باشد؛ پس از آن آخرین وضعیت انتخاب‌شده کاربر بازیابی شود.
    const shouldShowOnlyNonZero = savedNonZeroFilterState === null
        ? true
        : savedNonZeroFilterState === '1';
    $nonZeroFilter.prop('checked', shouldShowOnlyNonZero).trigger('change');
    renderPerf.initialFilter = Number((window.performance.now() - initialFilterStartedAt).toFixed(1));
    renderPerf.total = Number((window.performance.now() - renderPerfStartedAt).toFixed(1));
    console.log('[ShowBarAvord][PERF][renderTable stages]', {
        itemCount: Array.isArray(data) ? data.length : 0,
        starCount: Array.isArray(Stars) ? Stars.length : 0,
        timingsMs: renderPerf
    });

}

function DeleteItemFB(obj, Shomareh, Code, ItemId) {

    Year = parseInt($('#HDFYear').val());

    thisId = obj.id.split('_');
    var vardata = new Object();
    vardata.Id = thisId[1];
    vardata.Year = Year;

    if (!beginBarAvordButtonWait($(obj))) return;
    $.ajax({
            complete: function () { endBarAvordButtonWait($(obj)); },
        type: "POST",
        url: '/ItemFBStar/DeleteItemFBStar',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            info = data.split('_');
            if (info[0] == "OK") {

                //GetBarAvord(ItemId, Code, "");

                $(obj).parent().parent().remove();
                closestTr = $(obj).closest('tr');
                dataId = closestTr.attr('data-riz-id');

                $('#' + dataId).slideUp(500);
                //Shomareh = thisId[2];

                //var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                //var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
                //var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                //const tdJameFasl = $('#tdjameFasl-' + Code);
                //const tdJameFaslBaZarib = $('#tdJameFaslBaZarib-' + Code);
                //const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                //const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * parseFloat(info[1])));

                //meghdarFasl.html(formatNumber(info[1]));
                //bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * parseFloat(info[1])));
                ////MeghdarJoz.html(num);
                //tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                //tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));

                //JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAll = parseFloat(info[2]) + JameFasl;
                //tdJameFaslAll.html(dJameFaslAll.toFixed(0));

                //JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAllBaZarib = parseFloat(info[3]) + JameFaslBaZarib;
                //tdJameFaslAllBaZarib.html(dJameFaslAllBaZarib.toFixed(0));


                //GetCurrentRizMetreItemFBStar(Shomareh, thisId[1], Code);

                //let num = parseFloat(info[1]);
                //bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);

                //var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                //meghdarFasl.html(parseFloat(info[1]).toFixed(0));
                //bahayeKolFasl.html(formatNumber(parseFloat(info[2]).toFixed(0)));

                //Code = Shomareh.substring(0, 2);
                refreshBaseItem(obj, Shomareh, Code);

                GetJamBarAvordAll();
                GetJamFosoulAll();
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

        if (index === 0 || index === 8) return;

        if (index === 1) {

            let inputHtml =
            `<div class="d-flex align-items-center gap-2">
                <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="event.stopPropagation();ShowSelectTajmie('${Shomareh}-${rizMetreId}')"></i>

                <input id="txtTajmieItem-${Shomareh}-${rizMetreId}" class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}">

            </div>`;
            $td.html(inputHtml);

        }

        else if (index === 3 || index === 4 || index === 5 || index === 6 || index === 7) {
            const field = itemsFields.find(f => f.fieldType === (index - 2));

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
    const $actionCell = $row.find('td').eq(10);
    $actionCell.html(`
    <button type="button"
      onclick="event.stopPropagation();UpdateRizMetreFromRow($(this),'${rizMetreId}','${Shomareh}','${rizId}','${FBId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
	`);

    $row.find('td').eq(2).find('input').focus().select();
}


function cancelEditRow(row, rizMetreId, Shomareh, rizId1, FBId) {
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
		<i class="fa fa-trash DelRMUStyle ms-2" onclick="event.stopPropagation();DeleteRizMetre(this,'${rizMetreId}','${Shomareh}','${rizId1}','${FBId}')"></i>
	`);
    }
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
        vardata.TajmieItem = values[0] != "" ? values[0] : null;
        vardata.Sharh = values[1];
        vardata.Tedad = values[2] != "" ? values[2] : null;
        vardata.Tool = values[3] != "" ? values[3] : null;
        vardata.Arz = values[4] != "" ? values[4] : null;
        vardata.Ertefa = values[5] != "" ? values[5] : null;
        vardata.Vazn = values[6] != "" ? values[6] : null;
        vardata.Des = values[7];
        vardata.NoeFBId = NoeFBId;
        vardata.Year = Year;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Code = Code;
        vardata.LevelNumber = 1;
        if (!beginBarAvordButtonWait($(el))) return;
    $.ajax({
            complete: function () { endBarAvordButtonWait($(el)); },
            type: "POST",
            url: '/RizMetreUserFromShowBarAvord/UpdateRizMetreUsersFrmShowBarAvord',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log('[RIZ-SUM-PERF] INSERT save endpoint ms:', Number((window.performance.now() - rizOperationStartedAt).toFixed(1)));
                var info = data.split('_');
                if (info[0] == "OK") {
                    row.removeClass('editing');
                    let num = parseFloat(info[1]);
                    //let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text()));


                    el.parent().parent().find('#spanMeghdarJoz').html(formatNumber(parseFloat(num).toFixed(2)));

                    var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                    meghdarFasl.html(formatNumber(parseFloat(info[2]).toFixed(3)));

                    dBahayeItem = info[3];
                    var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                    bahayeKolFasl.html(formatNumber(parseFloat(dBahayeItem).toFixed(0)));

                    //bahayeKolFasl.html(formatNumber((bahayeVahedFasl1 * info[2]).toFixed(0)));
                    //MeghdarJoz.html(parseFloat(num.toFixed(2)));

                    //tdJameFasl.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    //tdJameFaslBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    //GetRizMetreUsers();
                    toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                    cancelEditRow(row, rizMetreId, Shomareh, rizId1, FBId);
                    refreshBaseItem(el, Shomareh, Code);
                    GetJamBarAvordAll();
                    GetJamFosoulAll();
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

    itemId = $row.attr('id').split('-')[1];

    var $fbShomareh = $row.find('input[id^="inputFBShomareh-"]');
    var $sharh = $row.find('input[id^="inputSharh-"]');
    var $vahed = $row.find('select[id^="selectVahed-"]');
    var $bahayeVahed = $row.find('input[id^="inputBahayeVahed-"]');
    var $itemZaribStar = $row.find('input[id^="txtItemZaribStar-"]');
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


    ItemZaribStar = $itemZaribStar == undefined ? null : $itemZaribStar.val().trim() == "" ? null : $itemZaribStar.val();

    var vardata = new Object();

    vardata.BaravordId = BarAvordUserId;
    vardata.FBShomareh = fbVal;
    vardata.Sharh = sharhVal;
    vardata.VahedId = parseInt(vahedVal);
    vardata.BahayeVahed = parseFloat(bahayeVal);
    vardata.kharidTajhizat = kharidTajhizat;
    vardata.itemZaribStar = ItemZaribStar;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;


    if (!beginBarAvordButtonWait($button)) return;
    $.ajax({
            complete: function () { endBarAvordButtonWait($button); },
        type: "POST",
        url: '/ItemFBStar/SaveItemFBStar',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == "OK") {

                GetBarAvord(itemId, code, "", true);
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

    var rizOperationStartedAt = window.performance.now();
    console.log('[RIZ-SUM-PERF] INSERT started:', { item: Shomareh, code: Code }); 
object.parent().parent().find('input[type=text]').each(function () {
        $(this).removeClass('ErrorValueStyle');
    });



    var Sharh, TajmieItem, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    firstObj = null;
    findFirstObj = false;
    object.parent().parent().find('input[type=text]').each(function () {


        if ($(this).attr('id') == 'txtSharh')
            Sharh = $(this).val();
        else if ($(this).attr('id').startsWith('txtTajmieItem-'))
            TajmieItem = $(this).val();
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
            else {
                if ($(this).val().trim() != '') {
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
            else
            {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Tool = $(this).val().replace(/\,/g, '');
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
            else {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Arz = $(this).val().replace(/\,/g, '');
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
            else
            {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Ertefa = $(this).val().replace(/\,/g, '');
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
            else {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Vazn = $(this).val().replace(/\,/g, '');
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
        if (!beginBarAvordButtonWait($(object))) return;
    $.ajax({
            complete: function () { endBarAvordButtonWait($(object)); },
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


                    //dMeghdarJoz = data.dMeghdarJoz;
                    //SumMeghdarJoz = data.sumMeghdarJoz;
                    //JameFasl = data.jameFasl;
                    //JameFaslInZarib = data.jameFaslInZarib;
                    //JameFalsKol = data.jameFalsKol;
                    //JameFalsKolWithoutBahaVahedWithZarib = data.jameFalsKolWithoutBahaVahedWithZarib
                    //JameFaslWithoutBahaVahed = data.jameFaslWithoutBahaVahed;
                    //JameFaslWithoutBahaVahedWithZarib = data.jameFaslWithoutBahaVahedWithZarib;
                    SumMeghdarJoz = info[2];
                    dBahayeItem = info[3];
                    FBId = info[4];

                    var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                    meghdarFasl.html(formatNumber(parseFloat(SumMeghdarJoz).toFixed(3)));

                    var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                    bahayeKolFasl.html(formatNumber(parseFloat(dBahayeItem).toFixed(0)));
                    refreshBaseItem(object, Shomareh, Code);

                    GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code);
                    //////////////////
                    //////////////////
                    GetJamBarAvordAll();
                    GetJamFosoulAll();
                    toastr.success('ریزه متره جدید بدرستی درج گردید', 'موفقیت');


                    //inputBahayeVahed = $('#txtBahayeVahed-' + rizId);
                    //const val = parseFloat(inputBahayeVahed.val().replace(/,/g, '')); // حذف کاما و تبدیل به عدد
                    //if (val === 0) {
                    //    inputBahayeVahed.addClass('blinking');
                    //} else {
                    //    inputBahayeVahed.removeClass('blinking'); // اگر نمی‌خواهید در غیر صفر بودن کلاس باقی بماند
                    //}

                }
                else if (info[0] == "NOK") {
                    object.removeAttr('disabled');
                    toastr.info('فصل تجمیعی انتخابی وجود ندارد', 'اطلاع');
                }
                else if (info[0] == "NOOK") {
                    object.removeAttr('disabled');
                    toastr.warning('مقداری جهت درج وجود ندارد', 'اطلاع');
                }
                else {
                    object.removeAttr('disabled');
                    toastr.info('مشکل در درج ریزه متره جدید', 'اطلاع');
                }
            },
            error: function (msg) {
                object.removeAttr('disabled');

                toastr.error('مشکل در درج ریزه متره جدید', 'خطا');
            }
        });
    }
    else {
        firstObj.focus().select();
        object.removeAttr('disabled');
        toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
    }
}

function refreshBaseItem(object, Shomareh, Code) {

    var chapterSumStartedAt = window.performance.now();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = parseInt($('#HDFYear').val());
    NoeFBId = parseInt($('#HDFNoeFB').val());

    BAType = $('#HDFBAType').val();


    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Shomareh = Shomareh;
    vardata.Year = Year;
    vardata.NoeFBId = NoeFBId;
    vardata.Code = Code;

    if (BAType != 'BarAvord') {
        strUrl = '/BaseInfo/GetFosoulForSooratVaziat';

        $.ajax({
            type: "POST",
            url: strUrl,//'/RizMetreUserFromShowBarAvord/GetJameCurrentFasl',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log('[RIZ-SUM-PERF] GetJameCurrentFasl ms:', Number((window.performance.now() - chapterSumStartedAt).toFixed(1)));

                ///////////////
                ///////////////

                //dMeghdarJoz = data.dMeghdarJoz;
                //SumMeghdarJoz = data.sumMeghdarJoz;

                JameFasl = data.currentFasl.jameFasl;
                JameFaslAll = data.currentFasl.jameFaslAll;

                JameFaslWithZarib = data.currentFasl.jameFaslWithZarib;
                JameFaslWithZaribAll = data.currentFasl.jameFaslWithZaribAll;

                JameFaslAllWithZarib101 = data.sumFosoulForSooratVaziat.dJameFaslWithZaribAll;
                jameFaslBaZarib101 = data.sumFosoulForSooratVaziat.dJameFaslWithZarib;

                JameFasl101 = data.sumFosoulForSooratVaziat.dJameFasl;
                JameFaslAll101 = data.sumFosoulForSooratVaziat.dJameFaslAll;
                // JameFaslInZarib = data.jameFaslInZarib;
                // JameFalsKol = data.jameFalsKol;
                // JameFalsKolWithoutBahaVahedWithZarib = data.jameFalsKolWithoutBahaVahedWithZarib
                // JameFaslWithoutBahaVahed = data.jameFaslWithoutBahaVahed;
                // JameFaslWithoutBahaVahedWithZarib = data.jameFaslWithoutBahaVahedWithZarib;
                // FBId = data.fbId;


                //const row = $(object).closest('tr');

                const tdJameFasl = $('#tdjameFasl-' + Code);
                const tdjameFaslAll = $('#tdjameFaslAll-' + Code);

                const tdjameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                const tdjameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                tdJameFasl.html(formatNumber(JameFasl.toFixed(0)));
                tdjameFaslAll.html(formatNumber(JameFaslAll.toFixed(0)));

                tdjameFaslBaZarib.html(formatNumber(JameFaslWithZarib.toFixed(0)));
                tdjameFaslAllBaZarib.html(formatNumber(JameFaslWithZaribAll.toFixed(0)));

                const tdJameFasl101 = $('#tdjameFasl-101');
                const tdjameFaslAll101 = $('#tdjameFaslAll-101');

                const tdjameFaslBaZarib101 = $('#tdjameFaslBaZarib-101');
                const tdJameFaslAllBaZarib101 = $('#tdjameFaslAllBaZarib-101');


                tdJameFasl101.html(formatNumber(JameFasl101.toFixed(0)));
                tdjameFaslAll101.html(formatNumber(JameFaslAll101.toFixed(0)));

                tdjameFaslBaZarib101.html(formatNumber(jameFaslBaZarib101.toFixed(0)));
                tdJameFaslAllBaZarib101.html(formatNumber(JameFaslAllWithZarib101.toFixed(0)));

                GetSumAllFBForSooratVaziat();
                GetSumAllFosoulForSooratVaziat();
                //const tdjameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);
            },
            error: function (msg) {
                object.removeAttr('disabled');

                toastr.error('مشکل در بارگذاری جمع مقادیر', 'خطا');
            }
        });
    }
    else {
        strUrl = '/RizMetreUserFromShowBarAvord/GetJameCurrentFasl';

        $.ajax({
            type: "POST",
            url: strUrl,//'/RizMetreUserFromShowBarAvord/GetJameCurrentFasl',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log('[RIZ-SUM-PERF] GetJameCurrentFasl (Baravord) ms:', Number((window.performance.now() - chapterSumStartedAt).toFixed(1)));


                ///////////////
                ///////////////

                //dMeghdarJoz = data.dMeghdarJoz;
                //SumMeghdarJoz = data.sumMeghdarJoz;
                JameFasl = data.jameFasl;
                JameFaslInZarib = data.jameFaslInZarib;
                JameFalsKol = data.jameFalsKol;
                JameFalsKolWithoutBahaVahedWithZarib = data.jameFalsKolWithoutBahaVahedWithZarib
                JameFaslWithoutBahaVahed = data.jameFaslWithoutBahaVahed;
                JameFaslWithoutBahaVahedWithZarib = data.jameFaslWithoutBahaVahedWithZarib;
                FBId = data.fbId;


                const row = $(object).closest('tr');
                const tdJameFasl = $('#tdjameFasl-' + Code);
                const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                const tdjameFaslAll = $('#tdjameFaslAll-' + Code);
                const tdjameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);


                //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);

                //const MeghdarJoz = row.find('#spanMeghdarJoz');

                //var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                //var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                var bahayeVahedFasl = $('#bahayeVahedFasl_' + Shomareh);

                //var txtBahayeVahed = $('#txtBahayeVahed-' + rizId);

                //let num = parseFloat(dMeghdarJoz);
                let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(txtBahayeVahed.val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                //meghdarFasl.html(formatNumber(SumMeghdarJoz.toFixed(0)));
                //bahayeKolFasl.html(formatNumber(JameFasl.toFixed(0)));//(formatNumber(txtBahayeVahed1 * SumMeghdarJoz));
                //MeghdarJoz.html(num);
                tdJameFasl.html(formatNumber(parseFloat(JameFasl).toFixed(0)));
                tdJameFaslBaZarib.html(formatNumber(parseFloat(JameFaslInZarib).toFixed(0)));

                //JameFaslStar = parseFloat(convertPersianToEnglish(tdJameFaslStar.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAll = parseFloat(JameFasl) + (JameFaslStar === NaN ? JameFaslStar : 0);
                //tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                if (Code != '99') {
                    tdJameFaslStar.html(formatNumber(JameFaslWithoutBahaVahed.toFixed(0)));
                    tdJameFaslStarBaZarib.html(formatNumber(JameFaslWithoutBahaVahedWithZarib.toFixed(0)));

                    tdjameFaslAll.html(formatNumber(JameFalsKol.toFixed(0)));
                }
                tdjameFaslAllBaZarib.html(formatNumber(JameFalsKolWithoutBahaVahedWithZarib.toFixed(0)));

                //JameFaslStarBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslStarBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAllBaZarib = parseFloat(JameFaslInZarib) + (JameFaslStarBaZarib === NaN ? JameFaslStarBaZarib : 0);
                //tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));
            },
            error: function (msg) {
                object.removeAttr('disabled');

                toastr.error('مشکل در بارگذاری جمع مقادیر', 'خطا');
            }
        });
    }

}
function refreshBaseItemNewBV(object, JameFasl, JameFaslInZarib, Code, Shomareh, BahayeVahedNew) {

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

    let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

    //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(txtBahayeVahed.val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

    //meghdarFasl.html(formatNumber(SumMeghdarJoz));
    //bahayeKolFasl.html(formatNumber(BahayeVahedNew * SumMeghdarJoz));
    //MeghdarJoz.html(num);
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

    var rizListStartedAt = window.performance.now();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.ItemFBShomareh = Shomareh;
    vardata.NoeFB = NoeFBId;
    vardata.Year = Year;

    $.ajax({
        url: "/RizMetreUserFromShowBarAvord/GetCurrentRizMetreUsersForShowBarAvord",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log('[RIZ-SUM-PERF] GetCurrentRizMetreUsers ms:', Number((window.performance.now() - rizListStartedAt).toFixed(1)), 'rows:', (response.rizMetreUsers || []).length);

            rizMetreUsers = response.rizMetreUsers;
            lstItemsFields = response.lstItemsFields;
            const rizRow = $(`#${rizId}`);
            const tbody = rizRow.find('tbody');
            tbody.empty(); // پاک‌کردن محتوای قدیمی

            let rizRowsHtml = '';

            if (Array.isArray(rizMetreUsers) && rizMetreUsers.length > 0) {
                rizRowsHtml += rizMetreUsers.map(riz => `
                    <tr class="riz-row-data" data-id="${riz.id}" data-itemsFields='${JSON.stringify(lstItemsFields).replace(/"/g, '&quot;')}' onclick="EditRizMetreRow(this,'${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')" style="cursor:pointer;">
					<td style="text-align:center;width:4%">${toPersianDigits(riz.shomarehNew)}</td>
					<td style="width:6%"><span>${formatNumber(riz.tajmieItem)}</span></td>
					<td style="width:22%"><span>${formatNumber(riz.sharh)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.tedad == null ? '' : formatNumber(riz.tedad)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.tool == null ? '' : formatNumber(riz.tool)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.arz == null ? '' : formatNumber(riz.arz)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.ertefa == null ? '' : formatNumber(riz.ertefa)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.vazn == null ? '' : formatNumber(riz.vazn)}</span></td>
					<td style="text-align:center;width:7%"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : formatNumber(riz.meghdarJoz)}</span></td>
					<td style="width:20%"><span>${toPersianDigits(riz.des ?? '')}</span></td>
                    <td style="text-align:center;width:7%"><i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetre(this,'${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')"></i></td>
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
  <td style="text-align:center;width:4%"></td>
  <td style="text-align:center;width:6%">
    <div class="d-flex align-items-center gap-2">
            <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="ShowSelectTajmie('${Shomareh}-New')"></i>
        
            <input id="txtTajmieItem-${Shomareh}-New" class="form-control form-control-sm" type="text" onchange="TajmieItemChange(this)"/>
       
    </div>
  </td>
  <td style="width:22%">
    <input id="txtSharh" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTedad" class="${getInputClass(f0)}" type="text" ${getDisabledAttr(f0)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTool" class="${getInputClass(f1)}" type="text" ${getDisabledAttr(f1)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtArz" class="${getInputClass(f2)}" type="text" ${getDisabledAttr(f2)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtErtefa" class="${getInputClass(f3)}" type="text" ${getDisabledAttr(f3)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtVazn" class="${getInputClass(f4)}" type="text" ${getDisabledAttr(f4)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td style="width:20%">
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;;width:7%">
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

            tbody.find('.riz-input-row #txtSharh').focus()
            //aaa= tbody.find('.riz-input-row');
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

function TajmieItemChange(Obj) {


    TajmieItemVal = $(Obj).val();
    NoeFBId = parseInt($('#HDFNoeFB').val());

    $.ajax({

        url: "/TajmieFosoul/CheckItemInTajmie",

        type: "GET",

        data: {

            itemTajmieValue: TajmieItemVal,
            noeFB: NoeFBId,
        },

        success: function (data) {

        },
         error: function (xhr) {

            if (xhr.status == 400) {

                toastr.warning(xhr.responseJSON.message);

            }
            else {

                toastr.warning("این فصل تجمیعی تعریف نشده است");

            }

        }

    });
}

function DeleteRizMetre(object, RizMetreId, Shomareh, rizId, FBId, Code) {
    var rizOperationStartedAt = window.performance.now();
    console.log('[RIZ-SUM-PERF] DELETE started:', { item: Shomareh, code: Code, rizMetreId: RizMetreId });
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.Id = RizMetreId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;

    if (!beginBarAvordButtonWait($(object))) return;
    $.ajax({
            complete: function () { endBarAvordButtonWait($(object)); },
        url: "/RizMetreUserFromShowBarAvord/DeleteRizMetre",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log('[RIZ-SUM-PERF] DELETE endpoint ms:', Number((window.performance.now() - rizOperationStartedAt).toFixed(1)));
            info = response.split('_');
            if (info[0] == "OK") {
                SumMeghdarJoz = info[2];
                dBahayeItem = info[3];

                var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                meghdarFasl.html(formatNumber(parseFloat(SumMeghdarJoz).toFixed(3)));

                var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                bahayeKolFasl.html(formatNumber(parseFloat(dBahayeItem).toFixed(0)));
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
                GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code);
                GetJamBarAvordAll();
                GetJamFosoulAll();
                refreshBaseItem(object, Shomareh, Code);
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}



