
// تابع ساخت گروه رادیو باتن
function createRadioGroup(name, items) {
    let i = 1;
    return items.map((item, index) => {
        const isNumber = !isNaN(item) && !isNaN(parseFloat(item));
        const value = isNumber ? item : i++;

        // برای مورد خاص: آخرین آیتم از printTypeList
        if (name === 'print' && index === 2) {
            return `
        <label class="highlighted-checkbox" style="display: inline-flex; align-items: center; gap: 5px; margin-right: 15px;">
        <input id="ckPrintPOPBoard" type="checkbox" name="${name}_POP" value="${value}">
        ${item}
        <input id="txtPercentPOPBoard" style="height: 25px;text-align:center" type="number" name="${name}_percent" min="0" max="100" step="1" placeholder="">
        <span>درصد شبرنگ</span>
        </label>`;
        }
        // رادیو معمولی
        return `<label style="margin-right: 15px;">
        <input type="radio" name="${name}" value="${value}" ${index === 0 ? 'checked' : ''}> ${item}
        </label>`;
    }).join("\n");
}

function createRadioGroupInfoBoard(name, items) {
    let i = 1;
    return items.map((item, index) => {
     debugger;
       const isNumber = !isNaN(item) && !isNaN(parseFloat(item));
        const value = isNumber ? item : i++;

        // برای مورد خاص: آخرین آیتم از printTypeList
        if (name === 'printInfoBoard' && index === 3) {
            debugger;

       return `
        <label class="highlighted-checkbox" style="display: inline-flex; align-items: center; gap: 5px; margin-right: 15px;">
        <input id="ckPrintPOPBoardInfo" type="checkbox" name="${name}_POP" value="${value}">
        ${item}
        <input id="txtPercentPOPBoardInfo" style="height: 25px;text-align:center" type="number" name="${name}_percent" min="0" max="100" step="1" placeholder="">
        <span>درصد شبرنگ</span>
        </label>`;
       }
        // رادیو معمولی
        return `<label style="margin-right: 15px;">
            <input type="radio" name="${name}" value="${value}" ${index === 0 ? 'checked' : ''}> ${item}
        </label>`;
    }).join("\n");
}

function ShowBoard(OperationId) {
    debugger;

    $('#HDFOperationId').val(OperationId);

    var vardata = new Object();
    vardata.OperationId = OperationId;
    $.ajax({
        type: "POST",
        url: "/Board/ReturnBoardItems",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            operationId = data.operationId;

            debugger;

            let str = `
            <div class="row boardRowStyle" style="border-bottom: 1px solid #ffffff;padding-bottom: 10px;padding-top: 10px;">
            <div class="col-2">
            <span>انتخاب شکل: </span>
            </div>
            <div class="col-3">
            <select id="BoardType" aria-label="">
      <option value="">-- انتخاب --</option>
      <option value="1">8 ضلعی</option>
      <option value="2">دایره</option>
      <option value="3">مثلث</option>
      <option value="4">مربع مستطیل</option>
    </select>
    </div>
</div>
<div class="row boardRowStyle" style="padding-top: 20px;">
    <div class="col-5" id="Ghotr">
        <fieldset>
            <legend id="Size">${data.sizeName} (میلیمتر)</legend>
            <div id="circle-sizes">
                ${createRadioGroup("size", data.sizeList)}
            </div>
        </fieldset>
    </div>
    <div class="col-5" id="Moraba" style="display:none">
        <fieldset>
        <legend id="Moraba">مربع یا مستطیل</legend>

        <div class="row">
        <div class="col-3" style="text-align:left">
            <legend>عرض (میلیمتر)</legend>
        </div>
            <div class="col-3">
                <input class="textStyleBoard form-control_1" id="MorabaArz" name="MorabaArz" type="number" min="140" max="1200" step="1" placeholder="">
            </div>
           <div class="col-3" style="text-align:left">
            <legend>ارتفاع (میلیمتر)</legend>
            </div>
            <div class="col-3">
                <input class="textStyleBoard form-control_1" id="MorabaErtefa" name="MorabaErtefa" type="number" min="400" max="1200" step="1" placeholder="">
            </div>
            </div>
        </fieldset>
    </div>
    <div class="col-3">
        <fieldset>
            <legend id="Material">جنس ورق تابلو</legend>
            ${createRadioGroup("material", data.materialList)}
        </fieldset>
    </div>
   
 <div class="col-4">
        <fieldset>
            <legend id="Thikness">ضخامت ورق (میلیمتر)</legend>
            <div id="circle-sizes">
                ${createRadioGroup("thikness", data.thiknessList)}
            </div>
        </fieldset>
    </div>   
</div>
<div class="row boardRowStyle">
 <div class="col-5">
        <fieldset>
            <legend id="Tip">تیپ تابلو</legend>
            ${createRadioGroup("tip", data.boardTypeList)}
        </fieldset>
    </div>
    <div class="col-7">
        <fieldset>
            <legend id="Print">نوع شبرنگ</legend>
            ${createRadioGroup("print", data.printTypeList)}
        </fieldset>
    </div>
</div>

<div class="row boardRowStyle">
 <div class="col-2">
</div>
<div class="col-1" style="text-align:left">
    <span>شرح: </span>
</div>
<div class="col-3">
    <input class="form-control" id="BoardSharh" name="BoardSharh" style="height: 25px;text-align:center" type="text" placeholder="طرح تابلو">
</div>
<div class="col-1" style="text-align:left">
    <span>تعداد: </span>
</div>

<div class="col-1">
    <input class="textStyleBoard" id="BoardTedad" name="BoardTedad" type="number" min="1" max="10000" step="1" placeholder="">
</div>
<div class="col-2">
    <button id="buttonBoard" class="buttonStyleBoard" onclick="SaveBoard()">ثبت</button>
</div>
<div class="col-1">
    <button id="buttonBoardDel" style="display:none" class="buttonStyleDelBoard" onclick="DelBoard()">حذف</button>
</div>
<div class="col-1">
    <button id="buttonBoardCancel" style="display:none" class="buttonStyleCancelBoard" onclick="CancelBoard()">لغو</button>
</div>
</div>

<div id="divBoardRizMetre${operationId}"></div>
`;

            $('#ula' + operationId).html(str);

            $('#ula' + operationId).on('change', '#BoardType', function () {
                if ($(this).val() == "4") {
                    $('#Ghotr').hide();
                    $('#Moraba').show();
                } else {
                    $('#Ghotr').show();
                    $('#Moraba').hide();
                }
            });
            $('#BoardType').trigger('change');

            $('#ula' + operationId).on('change', '#ckPrintPOPBoard', function () {

                debugger;
                let $percentInput = $('#txtPercentPOPBoard');

                if ($(this).is(':checked')) {
                    // اگر ورودی خالی باشه، blinking رو اضافه کن
                    if ($percentInput.val().trim() === '') {
                        $percentInput.addClass('blinking');
                    }
                } else {
                    // وقتی چک‌باکس برداشته شد، مقدار رو پاک کن
                    $percentInput.val('').removeClass('blinking');
                }
            });
            $('#ckPrintPOPBoard').trigger('change');


            $('#ula' + operationId).on('focus', '#txtPercentPOPBoard', function () {
                $(this).removeClass('blinking');
            });

            $('#ula' + operationId).on('input', '#txtPercentPOPBoard', function () {
                let val = $(this).val().trim();
                if (val !== '' && !isNaN(val)) {
                    $('#ckPrintPOPBoard').prop('checked', true);
                }
            });

            $('#ula' + operationId).on('change', '#txtPercentPOPBoard', function () {
                let val = $(this).val().trim();
                if (val === '' || isNaN(val) || val === '0') {
                    $('#ckPrintPOPBoard').prop('checked', false);
                }
            });

            $('#ula' + operationId).on('input', 'input', function () {
                if ($(this).is(':radio') || $(this).is(':checkbox')) {
                    $(this).closest('fieldset').css('border', '');
                } else {
                    $(this).css('border', '');
                }
            });

            //if (data.length!=0) {
            //    $('#ula' + operationId).slideUp(500);
            //}
            //else
            //    toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');

            ShowRizMetreBoard();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
        }
    });

}

function ShowRizMetreBoard() {
    debugger;
    OperationId = $('#HDFOperationId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    lstBoardType = [1, 2, 3, 4];

    var vardata = new Object();

    vardata.BaravordId = BarAvordUserId;
    vardata.OperationId = OperationId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.lstBoardType = lstBoardType;

    $.ajax({
        type: "POST",
        url: '/Board/GetRizMetreForBoard',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            lstItemFBShomarehForGet = data.lstItemFBShomarehForGet;
            lst = data.lst;
            //const lstItemFBShomarehForGet = [...new Set(data.map(item => item.itemFBShomareh))];
            var str = '';

            // ساخت HTML بر اساس lstItemFBShomarehForGet

            // گروه‌بندی data.lst بر اساس itemFBShomareh
            let groupedData = {};
            lst.forEach(function (row) {
                if (!groupedData[row.itemFBShomareh]) {
                    groupedData[row.itemFBShomareh] = [];
                }
                groupedData[row.itemFBShomareh].push(row);
            });


            // ساخت HTML بر اساس lstItemFBShomarehForGet
            if (lst.length != 0) {
                lstItemFBShomarehForGet.forEach(function (itemGroup) {

                    let itemFBShomareh = itemGroup.itemFBShomareh.substring(0, 6);
                    let des = itemGroup.des;
                    let ItemFields = itemGroup.itemFields;

                    let rows = groupedData[itemFBShomareh] || [];
                    if (rows.length != 0) {
                        // نمایش توضیح گروه
                        str += "<div class='row'>";
                        str += "<div class='col-md-12'><span style='color:#000'>" + itemGroup.itemFBShomareh + " - " + des + "</span></div>";
                        str += "</div>";

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
                        //str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div>";
                        str += "</div>";
                        str += "</div>";

                        str += "<div class='col-12' style='overflow:auto;max-height:400px;padding: 0px;'>";

                        // نمایش ردیف‌های مربوطه
                        rows.forEach(function (row) {

                            debugger;
                            let id = row.id;
                            let rizmetreid = row.rizMetreId;
                            let BarAvordAddedBoardId = row.barAvordAddedBoardId;

                            let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
                            let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
                            let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
                            let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
                            let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
                            let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();


                            let HasDelButton = row.hasDelButton;
                            let HasEditButton = row.hasEditButton;

                            str += "<div class='row styleRowTable'  data-boardid='" + BarAvordAddedBoardId + "' style=\"background-color:#fff\" onclick=\"RizMetreSelectClick('" + rizmetreid + "')\">";
                            str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span>" + row.shomareh + "</span></div>";

                            str += "<div class='col-md-2'><input disabled type='text'" +
                                " class='form-control spanStyleMitraSmall' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "" : "")
                                + "' id='txtTedad" + rizmetreid + "' value = '" + strTedad + "'  onkeyup=\"if(event.key === 'Enter'){ UpdateRMUDetailsClick('" + rizmetreid + "'); }\""
                                + " onblur =\"UpdateRMUDetailsClick('" + rizmetreid + "')\"/></div> ";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTool" + rizmetreid + "' value='" + strTool + "'/></div>";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtArz" + rizmetreid + "' value='" + strArz + "'/></div>";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtErtefa" + rizmetreid + "' value='" + strErtefa + "'/></div>";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtVazn" + rizmetreid + "' value='" + strVazn + "'/></div>";

                            str += "<div class='col-md-1 RMMJozStyle'><span id='MeghdarJoz" + rizmetreid + "'>" + MeghdarJoz + "</span></div>";

                            str += "<div class='col-md-2'><input disabled type='text' title='" + row.des + "' style='font-size:12px' class='form-control input-sm' id='txtDes" + rizmetreid + "' value='" + row.des + "'/></div>";
                            str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-pen' onclick=\"ViewRMUDetailsClick('" + BarAvordAddedBoardId + "')\"></i></div>";
                            //str += "<i id =\"iSave\" class=\"fa fa-save SaveRMUStyle\" onclick=\"UpdateRMUDetailsClick('" + rizmetreid + "')\" class=\"ButtonRowsSaveStyle\"></i>";

                            //if (HasDelButton)
                            //    str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-trash DelRMUStyle' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i></div>";
                            //if (HasEditButton)

                            str += "</div>";
                        });
                        str += "</div>";
                    }
                });
                debugger;


                $('#divBoardRizMetre' + operationId).html(str);
            }
            else {
                ItemHasConditionIdSplit = ItemHasConditionId.split('_');
                var $targetDiv = $("#divShowRizMetre" + ItemHasConditionIdSplit[0]);
                $targetDiv.slideUp(500);
                $targetDiv.html('');

                $('#CK' + ItemHasConditionId).prop('checked', false);

            }

            //$('#divShowRizMetre' + ItemHasConditionId).slideToggle(500);
        },
        error: function (msg) {
            toastr.error('خطا در درج اطلاعات', 'خطا');
        }
    });
}

function ShowRizMetreInfoBoard() {
    debugger;
    OperationId = $('#HDFOperationId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    lstBoardType = [5];

    var vardata = new Object();

    vardata.BaravordId = BarAvordUserId;
    vardata.OperationId = OperationId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.lstBoardType = lstBoardType;

    $.ajax({
        type: "POST",
        url: '/Board/GetRizMetreForBoard',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            lstItemFBShomarehForGet = data.lstItemFBShomarehForGet;
            lst = data.lst;
            //const lstItemFBShomarehForGet = [...new Set(data.map(item => item.itemFBShomareh))];
            var str = '';

            // ساخت HTML بر اساس lstItemFBShomarehForGet

            // گروه‌بندی data.lst بر اساس itemFBShomareh
            let groupedData = {};
            lst.forEach(function (row) {
                if (!groupedData[row.itemFBShomareh]) {
                    groupedData[row.itemFBShomareh] = [];
                }
                groupedData[row.itemFBShomareh].push(row);
            });


            // ساخت HTML بر اساس lstItemFBShomarehForGet
            if (lst.length != 0) {
                lstItemFBShomarehForGet.forEach(function (itemGroup) {

                    let itemFBShomareh = itemGroup.itemFBShomareh.substring(0, 6);
                    let des = itemGroup.des;
                    let ItemFields = itemGroup.itemFields;

                    let rows = groupedData[itemFBShomareh] || [];
                    if (rows.length != 0) {
                        // نمایش توضیح گروه
                        str += "<div class='row'>";
                        str += "<div class='col-md-12'><span style='color:#000'>" + itemGroup.itemFBShomareh + " - " + des + "</span></div>";
                        str += "</div>";

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
                        //str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div>";
                        str += "</div>";
                        str += "</div>";

                        str += "<div class='col-12' style='overflow:auto;max-height:400px;padding: 0px;'>";

                        // نمایش ردیف‌های مربوطه
                        rows.forEach(function (row) {

                            debugger;
                            let id = row.id;
                            let rizmetreid = row.rizMetreId;
                            let BarAvordAddedBoardId = row.barAvordAddedBoardId;

                            let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
                            let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
                            let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
                            let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
                            let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
                            let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();


                            let HasDelButton = row.hasDelButton;
                            let HasEditButton = row.hasEditButton;

                            str += "<div class='row styleRowTable' data-boardid='" + BarAvordAddedBoardId + "' style=\"background-color:#fff\" onclick=\"RizMetreSelectClick('" + rizmetreid + "')\">";
                            str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span>" + row.shomareh + "</span></div>";

                            str += "<div class='col-md-2'><input disabled type='text'" +
                                " class='form-control spanStyleMitraSmall' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "" : "")
                                + "' id='txtTedad" + rizmetreid + "' value = '" + strTedad + "'  onkeyup=\"if(event.key === 'Enter'){ UpdateRMUDetailsClick('" + rizmetreid + "'); }\""
                                + " onblur =\"UpdateRMUDetailsClick('" + rizmetreid + "')\"/></div> ";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTool" + rizmetreid + "' value='" + strTool + "'/></div>";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtArz" + rizmetreid + "' value='" + strArz + "'/></div>";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtErtefa" + rizmetreid + "' value='" + strErtefa + "'/></div>";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtVazn" + rizmetreid + "' value='" + strVazn + "'/></div>";

                            str += "<div class='col-md-1 RMMJozStyle'><span id='MeghdarJoz" + rizmetreid + "'>" + MeghdarJoz + "</span></div>";

                            str += "<div class='col-md-2'><input disabled type='text' title='" + row.des + "' style='font-size:12px' class='form-control input-sm' id='txtDes" + rizmetreid + "' value='" + row.des + "'/></div>";
                            str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-pen' onclick=\"ViewRMUDetailsInfoBoardClick('" + BarAvordAddedBoardId + "')\"></i></div>";
                            //str += "<i id =\"iSave\" class=\"fa fa-save SaveRMUStyle\" onclick=\"UpdateRMUDetailsClick('" + rizmetreid + "')\" class=\"ButtonRowsSaveStyle\"></i>";

                            //if (HasDelButton)
                            //    str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-trash DelRMUStyle' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i></div>";
                            //if (HasEditButton)

                            str += "</div>";
                        });
                        str += "</div>";
                    }
                });
                debugger;


                $('#divBoardRizMetre' + operationId).html(str);
            }
            else {
                ItemHasConditionIdSplit = ItemHasConditionId.split('_');
                var $targetDiv = $("#divShowRizMetre" + ItemHasConditionIdSplit[0]);
                $targetDiv.slideUp(500);
                $targetDiv.html('');

                $('#CK' + ItemHasConditionId).prop('checked', false);

            }

            //$('#divShowRizMetre' + ItemHasConditionId).slideToggle(500);
        },
        error: function (msg) {
            toastr.error('خطا در درج اطلاعات', 'خطا');
        }
    });
}

function UpdateRMUDetailsClick(rizmetreid) {
    tedad = $('#txtTedad' + rizmetreid).val();
    var vardata = new Object();

    vardata.tedad = tedad;
    vardata.rizmetreid = rizmetreid;

    $.ajax({
        type: "POST",
        url: '/Board/UpdateRizMetre',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != 'NOK') {
                $('#MeghdarJoz' + rizmetreid).html(data);
                toastr.success('ریز متره بدرستی ویرایش گردید', 'موفقیت');
            }
            else {

                toastr.error('مشکل در ویرایش ریز متره', 'خطا');
            }
        },
        error: function (msg) {
            toastr.error('مشکل در ویرایش ریز متره', 'خطا');
        }
    });
}

function SaveBoard() {

    let hasError = false;

    // ریست کردن استایل خط قرمز قبلی
    $('input, select, fieldset').css('border', '');

    // 1- بررسی BoardType
    let boardType = $('#BoardType').val();
    if (!boardType) {
        $('#BoardType').css('border', '1px solid red');
        hasError = true;
    }

    // 2- اگر BoardType = 4 => MorabaArz و MorabaErtefa باید مقدار داشته باشند
    if (boardType == "4") {
        let arz = $('input[name="MorabaArz"]').val();
        let ertefa = $('input[name="MorabaErtefa"]').val();

        $('input[name="Moraba"]').closest('fieldset').css('border', '1px solid red');

        if (!arz) {
            $('input[name="MorabaArz"]').css('border', '1px solid red');
            hasError = true;
        }
        if (!ertefa) {
            $('input[name="MorabaErtefa"]').css('border', '1px solid red');
            hasError = true;
        }
    }

    // 3- اگر BoardType != 4 => یکی از رادیوهای Ghotr انتخاب شود
    if (boardType != "4") {
        if ($('input[name="size"]:checked').length == 0) {
            $('input[name="size"]').closest('fieldset').css('border', '1px solid red');
            hasError = true;
        } else {
            $('input[name="size"]').closest('fieldset').css('border', '');
        }
    }

    // 4- یکی از رادیوهای Material
    if ($('input[name="material"]:checked').length == 0) {
        $('input[name="material"]').closest('fieldset').css('border', '1px solid red');
        hasError = true;
    } else {
        $('input[name="material"]').closest('fieldset').css('border', '');
    }

    // 5- یکی از رادیوهای Thikness
    if ($('input[name="thikness"]:checked').length == 0) {
        $('input[name="thikness"]').closest('fieldset').css('border', '1px solid red');
        hasError = true;
    } else {
        $('input[name="thikness"]').closest('fieldset').css('border', '');
    }

    // 6- یکی از رادیوهای Tip
    if ($('input[name="tip"]:checked').length == 0) {
        $('input[name="tip"]').closest('fieldset').css('border', '1px solid red');
        hasError = true;
    } else {
        $('input[name="tip"]').closest('fieldset').css('border', '');
    }

    // 7- یکی از Print
    if ($('input[name="print"]:checked').length == 0) {
        $('input[name="print"]').closest('fieldset').css('border', '1px solid red');
        hasError = true;
    } else {
        $('input[name="print"]').closest('fieldset').css('border', '');
    }

    // 8- اگر ckPrintPOPBoard تیک داشته باشد => txtPercentPOPBoard باید > 0 باشد
    if ($('#ckPrintPOPBoard').is(':checked')) {
        let percentVal = $('#txtPercentPOPBoard').val();
        if (!percentVal || Number(percentVal) <= 0) {
            $('#txtPercentPOPBoard').css('border', '1px solid red');
            hasError = true;
        }
    }

    // 9- BoardSharh
    if (!$('#BoardSharh').val()) {
        $('#BoardSharh').css('border', '1px solid red');
        hasError = true;
    }

    // 10- BoardTedad
    if (!$('#BoardTedad').val()) {
        $('#BoardTedad').css('border', '1px solid red');
        hasError = true;
    }

    // اگر اروری هست، ذخیره انجام نشود
    if (hasError) {
        return;
    }


    BarAvordAddedBoardId = $('#HDFBarAvordAddedBoardId').val().trim();

    let selections = [];

    // گرفتن تمام گروه‌های یکتا
    let groups = new Set();
    $('input[type="radio"]').each(function () {
        groups.add($(this).attr('name'));
    });

    // بررسی و جمع‌آوری انتخاب‌ها
    groups.forEach(function (group) {
        let selected = $(`input[name="${group}"]:checked`).val();
        selections.push({
            name: group,
            value: selected || null
        });
    });

    usePOP = false;
    OperationId = $('#HDFOperationId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    shape = $('#BoardType').val();
    BoardTedad = $('#BoardTedad').val();
    BoardSharh = $('#BoardSharh').val();
    MorabaArz = $('#MorabaArz').val();
    MorabaErtefa = $('#MorabaErtefa').val();
    usePOP = $('#ckPrintPOPBoard').prop('checked');
    PercentPOPBoard = $('#txtPercentPOPBoard').val() == "" ? null : $('#txtPercentPOPBoard').val();
    debugger;

    var vardata = new Object();
    vardata.Items = selections;
    vardata.Shape = shape;
    vardata.BaravordId = BarAvordUserId;
    vardata.OperationId = OperationId;
    vardata.Tedad = BoardTedad;
    vardata.Sharh = BoardSharh;
    vardata.Arz = MorabaArz == "" ? 0 : MorabaArz;
    vardata.Ertefa = MorabaErtefa == "" ? 0 : MorabaErtefa;
    vardata.UsePOP = usePOP;
    vardata.PercentPrintPOP = PercentPOPBoard;

    if (BarAvordAddedBoardId == '') {
        $.ajax({
            type: "POST",
            url: "/Board/SaveBoard",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "OK") {
                    toastr.success('تابلو بدرستی ذخیره شد', 'موفقیت');
                    ClearBoard();
                    ShowRizMetreBoard();

                }
                else
                    toastr.error('مشکل در درج تابلو', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
            }
        });
    }
    else {
        vardata.barAvordAddedBoardId = BarAvordAddedBoardId;

        $.ajax({
            type: "POST",
            url: "/Board/UpdateBoard",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "OK") {
                    $('#HDFBarAvordAddedBoardId').val('');
                    toastr.success('تابلو بدرستی ذخیره شد', 'موفقیت');
                    ClearBoard();
                    ShowRizMetreBoard();

                }
                else
                    toastr.error('مشکل در درج تابلو', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
            }
        });
    }
}

function DelBoard() {
    BarAvordAddedBoardId = $('#HDFBarAvordAddedBoardId').val().trim();
    var vardata = new Object();
    vardata.barAvordAddedBoardId = BarAvordAddedBoardId;

    $.ajax({
        type: "POST",
        url: "/Board/DeleteBoard",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == "OK") {
                toastr.success('تابلو بدرستی حذف شد', 'موفقیت');
                ClearBoard();
                ShowRizMetreBoard();

            }
            else
                toastr.error('مشکل در حذف تابلو', 'خطا');
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
        }
    });
}
function ClearBoard() {
    $('#circle-sizes input[type="radio"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="material"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="thikness"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="tip"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="print"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="print_POP"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="BoardSharh"]').val('');
    $('#ula' + operationId + ' input[name="BoardTedad"]').val('');
    $('#ula' + operationId + ' input[name="MorabaArz"]').val('');
    $('#ula' + operationId + ' input[name="MorabaErtefa"]').val('');
    $('#ula' + operationId + ' input[name="print_percent"]').val('');
    $('#ula' + operationId + ' #buttonBoard').text('ثبت');
    $('#ula' + operationId + ' #buttonBoardDel').hide();
    $('#ula' + operationId + ' #buttonBoardCancel').hide();

    $('#BoardType').val('');
}
function ViewRMUDetailsClick(BarAvordAddedBoardId) {
    debugger;

    $(".styleRowTable").css("background-color", "#fff");

    // فقط ردیف‌هایی که مقدار BarAvordAddedBoardId مشابه دارند رو رنگی کن
    $(".styleRowTable[data-boardid='" + BarAvordAddedBoardId + "']").css("background-color", "#f9e79f");

    var vardata = new Object();
    vardata.BarAvordAddedBoardId = BarAvordAddedBoardId;

    $('#HDFBarAvordAddedBoardId').val(BarAvordAddedBoardId);

    $.ajax({
        type: "POST",
        url: '/Board/GetBarAvordAddedBoardWithId',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            material = data.material;
            tip = data.boardTip;
            thickness = data.thickness;
            size = data.size;
            shape = data.boardType;
            printType = data.printType;
            BoardTedad = data.tedad;
            BoardSharh = data.sharh;
            Arz = (data.arz != 0 && data.arz != '') ? data.arz * 1000 : 0;
            Ertefa = (data.ertefa != 0 && data.ertefa != '') ? data.ertefa * 1000 : 0;
            PercentPrintPOP = (data.percentPrintPOP != 0 && data.percentPrintPOP != '') ? data.percentPrintPOP * 100 : 0;
            UsePOP = data.usePOP;
            OperationId = $('#HDFOperationId').val();

            $('#circle-sizes input[type="radio"][value="' + size + '"]').prop('checked', true);
            $('#ula' + operationId + ' input[name="material"][value="' + material + '"]').prop('checked', true);
            $('#ula' + operationId + ' input[name="thikness"][value="' + thickness + '"]').prop('checked', true);
            $('#ula' + operationId + ' input[name="tip"][value="' + tip + '"]').prop('checked', true);
            $('#ula' + operationId + ' input[name="print"][value="' + printType + '"]').prop('checked', true);
            $('#ula' + operationId + ' input[name="BoardSharh"]').val(BoardSharh);
            $('#ula' + operationId + ' input[name="BoardTedad"]').val(BoardTedad);
            $('#ula' + operationId + ' input[name="MorabaArz"]').val(Arz);
            $('#ula' + operationId + ' input[name="MorabaErtefa"]').val(Ertefa);

            $('#ula' + operationId + ' input[id="ckPrintPOPBoard"]').prop('checked', UsePOP);
            $('#ula' + operationId + ' input[name="print_percent"]').val(PercentPrintPOP);

            $('#ula' + operationId + ' #buttonBoard').text('ویرایش تالبو');
            $('#ula' + operationId + ' #buttonBoardDel').show();
            $('#ula' + operationId + ' #buttonBoardCancel').show();
            $('#BoardType').val(shape);

            if (shape == 4) {
                $('#Ghotr').hide();
                $('#Moraba').show();
            } else {
                $('#Ghotr').show();
                $('#Moraba').hide();
            }

            toastr.success('اطلاعات تابلو بدرستی بارگزاری شد', 'موفقیت');

        },
        error: function (msg) {
            toastr.error('خطا در درج اطلاعات', 'خطا');
        }
    });
}
function ShowBoardInfo(OperationId) {
    debugger;
    $('#HDFOperationId').val(OperationId);

    var vardata = new Object();
    vardata.OperationId = OperationId;
    $.ajax({
        type: "POST",
        url: "/Board/ReturnBoardInfoItems",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            debugger;
            operationId = data.operationId;

            let str = `
<div class="row boardRowStyle">
    <div class="col-4" id="MorabaBoardInfo">
        <fieldset>
        <legend id="MorabaInfoBoard">ابعاد تابلو</legend>
        <div class="row">
        <div class="col-3" style="text-align:left;padding-left: 0px;">
            <span>عرض (میلیمتر)</span>
        </div>
            <div class="col-3">
                <input class="textStyleBoard" id="MorabaArzInfoBoard" name="MorabaArzInfoBoard" type="number" min="800" max="3000" step="1" placeholder="">
            </div>
           <div class="col-3" style="text-align:left;padding-left: 0px;">
                <span>ارتفاع (میلیمتر)</span>
            </div>
            <div class="col-3">
                <input class="textStyleBoard" id="MorabaErtefaInfoBoard" name="MorabaErtefaInfoBoard" type="number" min="400" max="3000" step="1" placeholder="">
            </div>
            </div>
        </fieldset>
    </div>
    <div class="col-5">
        <fieldset>
            <legend>جنس ورق تابلو</legend>
            <div>
                ${createRadioGroupInfoBoard("materialInfoBoard", data.materialInfoList)}
            </div>
        </fieldset>
    </div>
    <div class="col-3">
        <fieldset>
            <legend>ضخامت ورق (میلی متر)</legend>
            ${createRadioGroupInfoBoard("zakhamatInfoBoard", data.zakhamatVaraqInfoList)}
        </fieldset>
    </div>
  </div>
  <div class="row boardRowStyle">
    <div class="col-12">
        <fieldset>
            <legend>نوع شبرنگ مصرفی</legend>
            ${createRadioGroupInfoBoard("printInfoBoard", data.printTypeInfoList)}
        </fieldset>
    </div>
  </div>

<div class="row boardRowStyle">
 <div class="col-1">
</div>
<div class="col-1" style="text-align:left">
    <span>شرح: </span>
</div>
<div class="col-3">
    <input class="form-control" id="InfoBoardSharh" name="InfoBoardSharh" style="height: 25px;text-align:center" type="text" placeholder="طرح تابلو">
</div>
<div class="col-1" style="text-align:left">
    <span>تعداد: </span>
</div>
<div class="col-2">
    <input class="textStyleBoard" id="InfoBoardTedad" name="InfoBoardTedad" type="number" min="1" max="10000" step="1" placeholder="">
</div>
<div class="col-2">
    <button id="buttonInfoBoard" class="buttonStyleBoard" onclick="SaveInfoBoard()">ثبت</button>
</div>
<div class="col-1">
    <button id="buttonInfoBoardDel" style="display:none" class="buttonStyleDelBoard" onclick="DelInfoBoard()">حذف</button>
</div>
<div class="col-1">
    <button id="buttonInfoBoardCancel" style="display:none" class="buttonStyleCancelBoard" onclick="CancelInfoBoard()">لغو</button>
</div>
</div>

<div id="divBoardRizMetre${operationId}"></div>
`;
            debugger;
            $('#ula' + operationId).html(str);

            let $options = $("input[name='zakhamatInfoBoard']").closest("label"); // همه ضخامت‌ها

            $options.hide(); // اول همه رو مخفی می‌کنیم
            //اولین انتخاب را نشان بده
            $options.find("input[value='1.25'], input[value='1.5']").closest("label").show();
            $options.first().prop("checked", true);

            $('#ula' + operationId).on('change', '#ckPrintPOPBoardInfo', function () {

                debugger;
                let $percentInput = $('#txtPercentPOPBoardInfo');

                if ($(this).is(':checked')) {
                    // اگر ورودی خالی باشه، blinking رو اضافه کن
                    if ($percentInput.val().trim() === '') {
                        $percentInput.addClass('blinking');
                    }
                } else {
                    // وقتی چک‌باکس برداشته شد، مقدار رو پاک کن
                    $percentInput.val('').removeClass('blinking');
                }
            });
            $('#ckPrintPOPBoardInfo').trigger('change');

            $('#ula' + operationId).on('focus', '#txtPercentPOPBoardInfo', function () {
                $(this).removeClass('blinking');
            });

            $('#ula' + operationId).on('input', '#txtPercentPOPBoardInfo', function () {
                let val = $(this).val().trim();
                if (val !== '' && !isNaN(val)) {
                    $('#ckPrintPOPBoardInfo').prop('checked', true);
                }
            });

            $('#ula' + operationId).on('change', '#txtPercentPOPBoardInfo', function () {
                let val = $(this).val().trim();
                if (val === '' || isNaN(val) || val === '0') {
                    $('#ckPrintPOPBoardInfo').prop('checked', false);
                }
            });

            $('#ula' + operationId).on("change", "input[name='materialInfoBoard']", function () {
                let val = $(this).val();  // مقدار انتخاب شده
                let $options = $("input[name='zakhamatInfoBoard']").closest("label"); // همه ضخامت‌ها

                $options.hide(); // اول همه رو مخفی می‌کنیم

                let $visibleOptions;
                if (val == "1") {
                    // ورق گالوانیزه → فقط 1.25 و 1.5
                    $visibleOptions = $options.find("input[value='1.25'], input[value='1.5']").closest("label").show();
                }
                else if (val == "2") {
                    // ریل گالوانیزه → 1.25 و 1.5 
                    $visibleOptions = $options.find("input[value='1.25'], input[value='1.5']").closest("label").show();
                }
                else if (val == "3") {
                    $visibleOptions = $options.find("input[value='1.25'], input[value='1.5'], input[value='2']").closest("label").show();
                }
                else if (val == "4") {
                    // ورق روغنی → فقط 3
                    $visibleOptions = $options.find("input[value='3']").closest("label").show();
                }
                else {
                    // پیش‌فرض همه مخفی بمونه (یا می‌تونی همه رو نمایش بدی)
                }

                // پاک کردن انتخاب قبلی
                $("input[name='zakhamatInfoBoard']").prop("checked", false);

                $visibleOptions.find("input[name='zakhamatInfoBoard']").first().prop("checked", true);

            });

            $('#ula' + operationId).on('input', 'input', function () {
                if ($(this).is(':radio') || $(this).is(':checkbox')) {
                    $(this).closest('fieldset').css('border', '');
                } else {
                    $(this).css('border', '');
                }
            });


            ShowRizMetreInfoBoard();


        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
        }
    });

}

function SaveInfoBoard() {

    let hasError = false;
    debugger;
    // ریست کردن استایل خط قرمز قبلی
    $('input, select, fieldset').css('border', '');


    // 2- اگر BoardType = 4 => MorabaArz و MorabaErtefa باید مقدار داشته باشند
    let arz = $('input[name="MorabaArzInfoBoard"]').val();
    let ertefa = $('input[name="MorabaErtefaInfoBoard"]').val();

    $('input[name="MorabaInfoBoard"]').closest('fieldset').css('border', '1px solid red');

    if (!arz) {
        $('input[name="MorabaArzInfoBoard"]').css('border', '1px solid red');
        hasError = true;
    }
    if (!ertefa) {
        $('input[name="MorabaErtefaInfoBoard"]').css('border', '1px solid red');
        hasError = true;
    }


    // 4- یکی از رادیوهای materialInfoBoard
    if ($('input[name="materialInfoBoard"]:checked').length == 0) {
        $('input[name="materialInfoBoard"]').closest('fieldset').css('border', '1px solid red');
        hasError = true;
    } else {
        $('input[name="materialInfoBoard"]').closest('fieldset').css('border', '');
    }

    // 7- یکی از Print
    if ($('input[name="printInfoBoard"]:checked').length == 0) {
        $('input[name="printInfoBoard"]').closest('fieldset').css('border', '1px solid red');
        hasError = true;
    } else {
        $('input[name="printInfoBoard"]').closest('fieldset').css('border', '');
    }

    if ($('input[name="zakhamatInfoBoard"]:checked').length == 0) {
        $('input[name="zakhamatInfoBoard"]').closest('fieldset').css('border', '1px solid red');
        hasError = true;
    } else {
        $('input[name="zakhamatInfoBoard"]').closest('fieldset').css('border', '');
    }

    // 8- اگر ckPrintPOPBoard تیک داشته باشد => txtPercentPOPBoard باید > 0 باشد
    if ($('#ckPrintPOPInfoBoard').is(':checked')) {
        let percentVal = $('#txtPercentPOPInfoBoard').val();
        if (!percentVal || Number(percentVal) <= 0) {
            $('#txtPercentPOPInfoBoard').css('border', '1px solid red');
            hasError = true;
        }
    }

    // 9- BoardSharh
    if (!$('#InfoBoardSharh').val()) {
        $('#InfoBoardSharh').css('border', '1px solid red');
        hasError = true;
    }

    // 10- BoardTedad
    if (!$('#InfoBoardTedad').val()) {
        $('#InfoBoardTedad').css('border', '1px solid red');
        hasError = true;
    }

    // اگر اروری هست، ذخیره انجام نشود
    if (hasError) {
        return;
    }

    debugger;

    BarAvordAddedBoardId = $('#HDFBarAvordAddedBoardId').val().trim();

    let selections = [];

    // گرفتن تمام گروه‌های یکتا
    let groups = new Set();
    $('input[type="radio"]').each(function () {
        groups.add($(this).attr('name'));
    });

    // بررسی و جمع‌آوری انتخاب‌ها
    groups.forEach(function (group) {
        let selected = $(`input[name="${group}"]:checked`).val();
        selections.push({
            name: group,
            value: selected || null
        });
    });

    usePOP = false;
    OperationId = $('#HDFOperationId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    shape = 5;
    BoardTedad = $('#InfoBoardTedad').val();
    BoardSharh = $('#InfoBoardSharh').val();
    MorabaArz = $('#MorabaArzInfoBoard').val();
    MorabaErtefa = $('#MorabaErtefaInfoBoard').val();
    usePOP = $('#ckPrintPOPBoardInfo').prop('checked');
    PercentPOPBoard = $('#txtPercentPOPBoardInfo').val() == "" ? null : $('#txtPercentPOPBoardInfo').val();
    debugger;

    var vardata = new Object();
    vardata.items = selections;
    vardata.Shape = shape;
    vardata.BaravordId = BarAvordUserId;
    vardata.OperationId = OperationId;
    vardata.Tedad = BoardTedad;
    vardata.Sharh = BoardSharh;
    vardata.Arz = MorabaArz;
    vardata.Ertefa = MorabaErtefa;
    vardata.usePOP = usePOP;
    vardata.PercentPrintPOP = PercentPOPBoard;

    if (BarAvordAddedBoardId == '') {
        $.ajax({
            type: "POST",
            url: "/Board/SaveInfoBoard",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "OK") {
                    toastr.success('تابلو بدرستی ذخیره شد', 'موفقیت');
                    ClearInfoBoard();
                    ShowRizMetreInfoBoard();
                }
                else if (data == 'NoItem') {
                    toastr.info('آیتم فهرست بهایی جهت درج یافت نشد', 'اطلاع');
                }
                else
                    toastr.error('مشکل در درج تابلو', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
            }
        });
    }
    else {
        vardata.barAvordAddedBoardId = BarAvordAddedBoardId;

        $.ajax({
            type: "POST",
            url: "/Board/UpdateInfoBoard",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == "OK") {
                    $('#HDFBarAvordAddedBoardId').val('');
                    toastr.success('تابلو بدرستی ویرایش شد', 'موفقیت');
                    ClearInfoBoard();
                    ShowRizMetreInfoBoard();
                }
                else
                    toastr.error('مشکل در ویرایش تابلو', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
            }
        });
    }
}

function CancelInfoBoard() {
    $('#HDFBarAvordAddedBoardId').val('');
    ClearInfoBoard();
}
function CancelBoard() {
    $('#HDFBarAvordAddedBoardId').val('');
    ClearBoard();
}

function DelInfoBoard() {
    BarAvordAddedBoardId = $('#HDFBarAvordAddedBoardId').val().trim();
    var vardata = new Object();
    vardata.barAvordAddedBoardId = BarAvordAddedBoardId;

    $.ajax({
        type: "POST",
        url: "/Board/DeleteBoard",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data == "OK") {
                toastr.success('تابلو بدرستی حذف شد', 'موفقیت');
                ClearBoard();
                ShowRizMetreBoard();

            }
            else
                toastr.error('مشکل در حذف تابلو', 'خطا');
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اطلاعات تابلو انتخابی', 'خطا');
        }
    });
}
function ClearInfoBoard() {

    $(".styleRowTable").css("background-color", "#fff");

    let $options = $("input[name='zakhamatInfoBoard']").closest("label"); // همه ضخامت‌ها

    $options.hide(); // اول همه رو مخفی می‌کنیم

    $('#ula' + operationId + ' input[name="materialInfoBoard"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="zakhamatInfoBoard"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="printInfoBoard"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="printInfoBoard_POP"]').prop('checked', false);
    $('#ula' + operationId + ' input[name="InfoBoardSharh"]').val('');
    $('#ula' + operationId + ' input[name="InfoBoardTedad"]').val('');
    $('#ula' + operationId + ' input[name="MorabaArzInfoBoard"]').val('');
    $('#ula' + operationId + ' input[name="MorabaErtefaInfoBoard"]').val('');
    $('#ula' + operationId + ' input[name="printInfoBoard_percent"]').val('');
    $('#ula' + operationId + ' #buttonInfoBoard').text('ثبت');
    $('#ula' + operationId + ' #buttonInfoBoardDel').hide();
    $('#ula' + operationId + ' #buttonInfoBoardCancel').hide();
}

function ViewRMUDetailsInfoBoardClick(BarAvordAddedBoardId) {


    $(".styleRowTable").css("background-color", "#fff");

    // فقط ردیف‌هایی که مقدار BarAvordAddedBoardId مشابه دارند رو رنگی کن
    $(".styleRowTable[data-boardid='" + BarAvordAddedBoardId + "']").css("background-color", "#f9e79f");

    var vardata = new Object();
    vardata.BarAvordAddedBoardId = BarAvordAddedBoardId;

    $('#HDFBarAvordAddedBoardId').val(BarAvordAddedBoardId);

    $.ajax({
        type: "POST",
        url: '/Board/GetBarAvordAddedBoardWithId',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            material = data.material;
            thickness = data.thickness;
            printType = data.printType;
            BoardTedad = data.tedad;
            BoardSharh = data.sharh;
            Arz = (data.arz != 0 && data.arz != '') ? data.arz * 1000 : 0;
            Ertefa = (data.ertefa != 0 && data.ertefa != '') ? data.ertefa * 1000 : 0;
            PercentPrintPOP = (data.percentPrintPOP != 0 && data.percentPrintPOP != '') ? data.percentPrintPOP * 100 : 0;
            UsePOP = data.usePOP;
            OperationId = $('#HDFOperationId').val();

            $('#ula' + operationId + ' input[name="materialInfoBoard"][value="' + material + '"]').prop('checked', true);
            $('#ula' + operationId + ' input[name="printInfoBoard"][value="' + printType + '"]').prop('checked', true);
            $('#ula' + operationId + ' input[name="InfoBoardSharh"]').val(BoardSharh);
            $('#ula' + operationId + ' input[name="InfoBoardTedad"]').val(BoardTedad);
            $('#ula' + operationId + ' input[name="MorabaArzInfoBoard"]').val(Arz);
            $('#ula' + operationId + ' input[name="MorabaErtefaInfoBoard"]').val(Ertefa);

            $('#ula' + operationId + ' input[id="ckPrintPOPBoardInfo"]').prop('checked', UsePOP);
            $('#ula' + operationId + ' input[name="printInfoBoard_percent"]').val(PercentPrintPOP);

            $('#ula' + operationId + ' #buttonInfoBoard').text('ویرایش تالبو');
            $('#ula' + operationId + ' #buttonInfoBoardDel').show();
            $('#ula' + operationId + ' #buttonInfoBoardCancel').show();

            /////
            ///////
            ////////
            let val = $('#ula' + operationId + ' input[name="materialInfoBoard"][value="' + material + '"]').val();  // مقدار انتخاب شده
            let $options = $("input[name='zakhamatInfoBoard']").closest("label"); // همه ضخامت‌ها

            $options.hide(); // اول همه رو مخفی می‌کنیم

            if (val == "1") {
                // ورق گالوانیزه → فقط 1.25 و 1.5
                $options.find("input[value='1.25'], input[value='1.5']").closest("label").show();
            }
            else if (val == "2") {
                // ریل گالوانیزه → 1.25 و 1.5 
                $options.find("input[value='1.25'], input[value='1.5']").closest("label").show();
            }
            else if (val == "3") {
                $options.find("input[value='1.25'], input[value='1.5'], input[value='2']").closest("label").show();
            }
            else if (val == "4") {
                // ورق روغنی → فقط 3
                $options.find("input[value='3']").closest("label").show();
            }
            else {
                // پیش‌فرض همه مخفی بمونه (یا می‌تونی همه رو نمایش بدی)
            }
            // پاک کردن انتخاب قبلی
            $("input[name='zakhamatInfoBoard']").prop("checked", false);
            ////////
            //////////
            $('#ula' + operationId + ' input[name="zakhamatInfoBoard"][value="' + thickness + '"]').prop('checked', true);


            toastr.success('اطلاعات تابلو بدرستی بارگزاری شد', 'موفقیت');

        },
        error: function (msg) {
            toastr.error('خطا در درج اطلاعات', 'خطا');
        }
    });
}