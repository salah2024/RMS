var gParents = [];
function createTree(data, parentId, lazyMode, searchMode) {

    var html = "";
    var children = data.filter(function (item) {
        return item.parentId == parentId;
    });

    if (children.length > 0) {

        children.forEach(function (item) {

            var id = item.id;
            var parentId = item.parentId;
            var operationName = item.operationName;
            var LatinName = item.latinName;
            var funcCall = item.functionCall.trim();
            var fbShomareh = item.itemsFBShomareh.trim();
            var fbShomarehMain = item.itemsFBShomarehMain != null ? item.itemsFBShomarehMain.trim() : "";
            var sharh = item.sharh;
            var HasEnteringValue = item.hasEnteringValue;
            var MinValue = item.minValue == 0 ? "" : item.minValue;
            var MaxValue = item.maxValue == 0 ? "" : item.maxValue;
            var currentValue = item.operationDefaultValue == 0 ? "" : item.operationDefaultValue;
            var Value = item.value == 0 ? "" : item.value;
            var Description = item.description == null ? "" : "(" + item.description + ")";
            var CheckNecessary = item.checkNecessary;
            var SumVazn = item.sumVazn;
            var Vahed = item.vahed;
            if (fbShomareh === "") {

                if (funcCall !== "") {

                    fn = funcCall.split('_');
                    if (fn[1] == "1") {
                        NoeFB = 0;
                        if (LatinName == 'RAH') {
                            NoeFB = 21
                        }
                        else if (LatinName == 'RAHDARI') {
                            NoeFB = 22
                        }


                        html += `
                        <li>
                        <a onclick="${fn[0]}('${id}',${NoeFB})" id="a${id}">
                            ${operationName}
                        <span id="span${id}"></span>
                        </a>
                        <span id="spanOpShomareh${id}"></span>`;
                        if (lazyMode) {
                            html += `<ul style="margin-top:5px" id="ula${id}" data-tree-lazy="${item.hasChildren ? 'true' : 'false'}" data-tree-loaded="false"></ul></li>`;
                        } else {
                            html += `<ul style="margin-top:5px" id="ula${id}">`;
                            html += createTree(data, id, false, searchMode);
                            html += `</ul></li>`;
                        }
                    }
                    else {
                        html += `
                        <li>
                        <a onclick="${funcCall}('${id}',e)" id="a${id}">
                            ${operationName}
                        <span id="span${id}"></span>
                        </a>
                        <span id="spanOpShomareh${id}"></span>
                        <ul style="margin-top:5px" id="ula${id}" data-tree-dynamic="true">`;
                        if (searchMode) {
                            html += createTree(data, id, false, true);
                        }
                        html += `</ul></li>`;
                    }
                }
                else {


                    if (LatinName == "SHARH") {
                        var strCheckNecessary = CheckNecessary ? "checked" : "";

                        html += `
                       <div class="row col-12" style="text-align: center;">
                    <div class="col-6 HamlTableStyle" style="background-color: #d5c4f1;">
                    <div class="row col-12">
                            <span>شرح</span>
                     </div>
                    <div class="row col-12">
                            <input type = "checkbox" id = "ckCheckNecessary-${id}" ${strCheckNecessary} class="form-control_1"
                        style = "width:15px;margin-right:250px" onchange = "onCheckBoxChange(this)" /><span style="margin-right:10px">عدم الزام محدودیت فاصله حمل</span><span class="LegalOpStyle">براساس تبصره 2 بند 1 فصل 20</span>

                     </div>
                    </div>
                    <div class="row col-6" >
                    <div class="col-2 HamlTableStyle"  style="background-color: #d5c4f1;">
                        <span>واحد</span>
                    </div>
                    <div class="col-2 HamlTableStyle"  style="background-color: #d5c4f1;">
                        <span>مقدار</span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="background-color: #d5c4f1;" >
                        <span>فاصله حمل (کیلومتر)</span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="background-color: #d5c4f1;" >
                        <span>فاصله حمل محاسباتی </span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="padding: 1px;background-color: #d5c4f1">
                        <span>حداقل فاصله حمل محاسباتی</span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="padding: 1px;background-color: #d5c4f1">
                        <span>حداکثر فاصله حمل قابل قبول</span>
                    </div>
                    </div>
                    </div> `
                    }
                    else {
                        var strCheckNecessary = CheckNecessary ? "checked" : "";
                        html += `<li>
                           <div style="display: flex; align-items: center; gap: 5px;">
                           <a id="a${id}">${operationName}
                           <span id="span${id}"></span></a>
                           </div>`;
                        if (lazyMode) {
                            html += `<ul id="ula${id}" data-tree-lazy="${item.hasChildren ? 'true' : 'false'}" data-tree-loaded="false"></ul></li>`;
                        } else {
                            html += `<ul id="ula${id}">`;
                            html += createTree(data, id, false);
                            html += `</ul></li>`;
                        }



                    }
                }
            }
            else {
                strDisabled = ``;
                BAType = $('#HDFBAType').val();
                if (BAType!='BarAvord') 
                    strDisabled = ` disabled `;


                if (fbShomarehMain != fbShomareh) {
                    fbShomareh = fbShomarehMain;
                }

                showAddedRecord = !HasEnteringValue;
                if (funcCall == "PayKaniFirstLevelClick" || funcCall == "ChanelKaniFirstLevelClick") {
                    TypeP = '';
                    if (funcCall == "PayKaniFirstLevelClick") {
                        TypeP = 2;
                    }
                    else if (funcCall == "ChanelKaniFirstLevelClick") {
                        TypeP = 3;
                    }

                    html += `
                    <li style="display: flex; align-items: center; gap: 5px;">
                    <a id="a${id}" onclick="OperationClick('${id}',${showAddedRecord},e,'true_${TypeP}')">
                    ${fbShomareh.substring(0, 6)}-${sharh} <span id="span${id}"></span><span style="color:red">${Description}</span></a>
                    <span id="spanOpShomareh${fbShomareh}"></span>
                        ${HasEnteringValue ? `<input class="form-control_1" ${strDisabled} type="number" value="${currentValue}"
                        style="width:100px;margin-right:30px" onchange="onInputChange('${id}', this,${MinValue == "" ? 0 : MinValue},${MaxValue},${currentValue == "" ? 0 : currentValue})" />
                            <span> کیلومتر </span><span style="color:blue">${MinValue != 0 ? ' از کیلومتر ' + ` <span class="KMStyle">` + MinValue + `</span>` : ''}</span>
                        <span style="color:blue">${(MaxValue != 0 && MaxValue != null) ? ' حداکثر فاصله حمل ' + ` <span class="KMStyle">` + MaxValue + `</span>` : ''}</span>` : ''}
                    </li>`;
                    html += `<div id="ula${id}" class="row" style="display: none; margin:10px">
                        <div id="uldiva${id}" class="col-md-12" style="border:1px solid #79c7ea;padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;">
                        </div>
                        </div>`;
                }
                else {
                    ///در صورتی که دارای مقدار ورودی باشد (بخش حمل) بصورت جدول نمایش داده میشود
                    if (HasEnteringValue) {
                        html += `
                            <li style="display: flex; align-items: center; gap: 5px;">
                                <div class="row col-12">
                    <div class="col-6 HamlTableStyle">
                        <a id="a${id}" onclick="OperationClick('${id}',${showAddedRecord},e,'false')">
                        ${fbShomareh.substring(0, 6)}-${sharh} <span id="span${id}"></span><span style="color:red">${Description}</span></a>
                        <span id="spanOpShomareh${fbShomareh}"></span>
                    </div>
                    <div class="row col-6">
                    <div class="col-2 HamlTableStyle" style="text-align: center;padding-top: 5px;">
                        <span id="spanVahed-${id}">${Vahed}</span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="text-align: center;padding-top: 5px;">
                         <span>${parseFloat(SumVazn).toFixed(3)}</span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="padding: 0px 0px;">
                        ${HasEnteringValue ? `<input class="form-control_1 KMEnterClass" onkeydown="KMEnter(this)" ${strDisabled} type="number" value="${currentValue}" id="txt-${id}"
                                               style="height: 100%;border: none;" onchange="onInputChange('${id}', this,${MinValue == "" ? 0 : MinValue},${MaxValue},${currentValue == "" ? 0 : currentValue})" />
                    </div>
                    <div class="col-2 HamlTableStyle" style="text-align: center;padding-top: 5px;">
                         <span id="span-${id}">${Value}</span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="text-align: center;padding-top: 5px;">
                        ${MinValue != 0 ? ` <span>` + MinValue + `</span>` : ''}</span>
                    </div>
                    <div class="col-2 HamlTableStyle" style="text-align: center;padding-top: 5px;">
                        <span style="color:blue">${(MaxValue != 0 && MaxValue != null) ? ` <span id="spanMax-${id}">` + MaxValue + `</span>` : ''}</span>` : ''}
                    </div>
                    </div>
                    </div>
                    </li>`;
                    }
                    else {
                        html += `
                            <li style="display: flex; align-items: center; gap: 5px;">
                        <a id="a${id}" onclick="OperationClick('${id}',${showAddedRecord},e,'false')">
                        ${fbShomareh}-${sharh} <span id="span${id}"></span><span style="color:red">${Description}</span></a>
                        <span id="spanOpShomareh${fbShomareh}"></span>
                        ${HasEnteringValue ? `<input class="form-control_1 KMEnterClass" onkeydown="KMEnter(this)" type="number" value="${currentValue}" id="txt-${id}"
                                               style="height: 100%;border: none;" onchange="onInputChange('${id}', this,${MinValue == "" ? 0 : MinValue},${MaxValue},${currentValue == "" ? 0 : currentValue})" />
       
                        ${MinValue != 0 ? ` <span>` + MinValue + `</span>` : ''}</span>
                        <span style="color:blue">${(MaxValue != 0 && MaxValue != null) ? ` <span id="spanMax-${id}">` + MaxValue + `</span>` : ''}</span>` : ''}
                             </li>`;
                    }

                    html += `<div id="ula${id}" class="row" style="display: none; margin:10px">
                        <div id="uldiva${id}" class="col-md-12" style="border:1px solid #79c7ea;padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;">
                        </div>
                        </div>`;
                }
            }
        });
    }
    return html;
}

function KMEnter(Obj) {
    $(Obj).on("keypress", function (e) {
        if (e.keyCode == 13) {
            ul = Obj.closest('ul');
            inputs = $(ul).find('input');
            var idx = inputs.index(Obj);
            if (idx == inputs.length - 1) {
                inputs[0].select()
            } else {
                inputs[idx + 1].focus();
                inputs[idx + 1].select();
            }
        }
    });
}

function onCheckBoxChange(obj) {
    objId = obj.id.split('-');
    ck = $('[id^="ckCheckNecessary-"]').prop('checked');
    if (!ck) {
        $('#ula' + objId[1]).find('input').each(function () {
            if ($(this).val().trim() != "") {
                txtId = this.id.split('-');
                MaxValue = $('#spanMax-' + txtId[1]).html();
                //if (MaxValue != "") {
                //    dMaxValue = parseFloat(MaxValue);
                //    currentValue = $(this).val();
                //    if (currentValue > dMaxValue) {
                //        $(this).val(dMaxValue);
                //    }
                //}
            }
        });
    }
    value = obj.checked;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.blnChecked = value;
    vardata.BarAvordId = BarAvordUserId;
    vardata.NoeFBId = NoeFBId;

    $.ajax({
        type: "POST",
        url: '/Operation/ChangeBarAvordHamlNecessaryLimit',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $.each(data, function () {

                debugger;
                afbShomarehHaml = this.fbShomarehHaml;
                aValueBase = this.valueBase == 0 ? null : this.valueBase;
                aValue = this.value == 0 ? null : this.value;

                $('#spanOpShomareh' + afbShomarehHaml).parent().parent().find('input').val(aValueBase);
                $('#spanOpShomareh' + afbShomarehHaml).parent().parent().find('span[id^="span-"]').html(aValue);
            });

            $("#ula" + objId[1] + " div[id^='ula']").each(function () {
                $(this).slideUp();
            });

            // پس از تغییر محدودیت فاصله حمل، Grid نیز مانند تغییر ورودی‌های حمل تازه‌سازی شود
            OperationId = $('#HDFOperationId').val();
            if (OperationId && OperationId.trim() !== '') {
                GetRizMetreWithFBId(1);
            }

            // این تابع شناسه عملیات را متناسب با حالت عادی یا عملیات اضافه انتخاب می‌کند
            GetItemsAddedAndRelForRizMetre(1);

            toastr.success('ثبت بدرستی انجام گرفت', 'موفقیت');

        },
        error: function (msg) {
            toastr.error('مشکل در ثبت اطلاعات', 'خطا');
        }
    });
}

function onInputChange(id, obj, minValue, maxValue, currentValue) {

    //FBId = $('#HDFFBID').val();
    //if (FBId == '') {
    //    toastr.info('هیچ یک از آیتم ها انتخاب نشده', 'اطلاع');
    //    obj.value = currentValue;
    //    return;
    //}
    debugger;

    ck = $('[id^="ckCheckNecessary-"]').prop('checked');

    //var ck = $('#ckCheckNecessary-' + id);

    value = parseFloat(obj.value);
    //if (maxValue != null) {
    //    if (!ck) {
    //        if (value >= maxValue) {
    //            toastr.info('حداکثر مقدار مجاز ثبت گردید', 'اطلاع');
    //            value = maxValue;
    //            obj.value = value;
    //        }
    //    }
    //    if (value < minValue) {
    //        toastr.info('حداقل مقدار مجاز ثبت گردید', 'اطلاع');
    //        value = minValue;
    //        obj.value = value;
    //    }
    //}

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.OperationId = id;
    vardata.Value = value;
    vardata.BarAvordId = BarAvordUserId;
    vardata.NoeFBId = NoeFBId;

    $.ajax({
        type: "POST",
        url: '/Operation/SaveHamlValue',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            info = data.split('_');
            if (info[0] == "OK") {
                $('#span-' + id).html(info[1]);

                OperationId = $('#HDFOperationId').val();;

                if (OperationId.trim() != '') {
                    GetRizMetreWithFBId(1);
                }

                toastr.success('ثبت بدرستی انجام گرفت', 'موفقیت');
            }
            else
                toastr.error('مشکل در ثبت', 'خطا');

        },
        error: function (msg) {
            toastr.error('مشکل در درج ریزه متره جدید', 'خطا');
        }
    });
}

