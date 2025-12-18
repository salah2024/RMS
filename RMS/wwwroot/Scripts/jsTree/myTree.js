var gParents = [];
function createTree(data, parentId) {
    debugger;
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
            var sharh = item.sharh;
            var HasEnteringValue = item.hasEnteringValue;
            var MinValue = item.minValue;
            var MaxValue = item.maxValue;
            var currentValue = item.operationDefaultValue;
            var Description = item.description == null ? "" : "(" + item.description + ")";
            var CheckNecessary = item.checkNecessary;

            if (fbShomareh === "") {
                if (funcCall !== "") {
                    debugger;
                    fn = funcCall.split('_');
                    if (fn[1] == "1") {
                        NoeFB = 0;
                        if (LatinName == 'RAH') {
                            NoeFB=234
                        }
                        else if (LatinName == 'RAHDARI') {
                            NoeFB=232
                        }
                        html += `
                        <li>
                        <a onclick="${fn[0]}('${id}',${NoeFB})" id="a${id}">
                            ${operationName}
                        <span id="span${id}"></span>
                        </a>
                        <span id="spanOpShomareh${id}"></span>
                        <ul style="margin-top:5px" id="ula${id}">`;
                        html += createTree(data, id);
                        html += `</ul></li>`;
                    }
                    else {
                        html += `
                        <li>
                        <a onclick="${funcCall}('${id}')" id="a${id}">
                            ${operationName}
                        <span id="span${id}"></span>
                        </a>
                        <span id="spanOpShomareh${id}"></span>
                        <ul style="margin-top:5px" id="ula${id}">`;
                        html += `</ul></li>`;
                    }
                } else {
                    var strCheckNecessary = CheckNecessary == true ? "checked" : "";
                    html += `<li>
                           <div style="display: flex; align-items: center; gap: 5px;">
                           <a id="a${id}">${operationName}
                           <span id="span${id}"></span></a>
                           ${HasEnteringValue ? `<input type="checkbox" ${strCheckNecessary} class="form-control_1"
                                    style="width:15px;margin-right:250px" onchange="onCheckBoxChange(this)" /><span>عدم الزام محدودیت فاصله حمل</span><span class="LegalOpStyle">براساس تبصره 2 بند 1 فصل 20</span>` : ''}
                                    <span id="spanOpShomareh${id}"></span>
                                    </div>
                      <ul id="ula${id}">`;
                         html += createTree(data, id); // Recursive call
                    html += `</ul></li>`;
                }
            } else {
                debugger;
                showAddedRecord = !HasEnteringValue;
                html += `
                <li style="display: flex; align-items: center; gap: 5px;">
                <a id="a${id}" onclick="OperationClick('${id}',${showAddedRecord})">


                    ${fbShomareh}-${sharh} <span id="span${id}"></span><span style="color:red">${Description}</span></a>
                    <span id="spanOpShomareh${fbShomareh}"></span>
                        ${HasEnteringValue ? `<input class="form-control_1" type="number" value="${currentValue}"
                        style="width:100px;margin-right:30px" onchange="onInputChange('${id}', this,${MaxValue},${currentValue})" />
                    <span> کیلومتر </span><span style="color:blue">${MinValue != 0 ? ' از کیلومتر ' + ` <span class="KMStyle">` + MinValue + `</span>` : ''}</span>
                <span style="color:blue">${(MaxValue != 0 && MaxValue != null) ? ' سقف فاصله ' + ` <span class="KMStyle">` + MaxValue + `</span>` : ''}</span>` : ''}
                </li>`;

                html += `<div id="ula${id}" class="row" style="display: none; margin:10px">
                        <div id="uldiva${id}" class="col-md-12" style="border:1px solid #79c7ea;padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;">
                        </div>
                        </div>`;
            }
        });
    }
    return html;
}






//var gParents = [];
//function createTree(data, parentId) {
//    var html = "";
//    var children = data.filter(function (item) {
//        return item.parentId == parentId;
//    });

//    if (children.length > 0) {
//        children.forEach(function (item) {
//            var id = item.id;
//            var parentId = item.parentId;
//            var operationName = item.operationName;
//            var funcCall = item.functionCall.trim();
//            var fbShomareh = item.itemsFBShomareh.trim();
//            var sharh = item.sharh;
//            var HasEnteringValue = item.hasEnteringValue;
//            var MinValue = item.minValue;
//            var MaxValue = item.maxValue;
//            var currentValue = item.operationDefaultValue;
//            var Description = item.description == null ? "" : "(" + item.description + ")";
//            var CheckNecessary = item.checkNecessary;

//            if (fbShomareh === "") {
//                if (funcCall !== "") {
//                    html += `
//                <li>
//                    <a onclick="${funcCall}('${id}')" id="a${id}">
//                     ${operationName}
//                    <span id="span${id}"></span>
//                    </a>
//                    <span id="spanOpShomareh${id}"></span>
//                    <ul style="margin-top:5px" id="ula${id}"></ul>
//                </li>`;
//                } else {
//                    var strCheckNecessary = CheckNecessary == true ? "checked" : "";
//                    html += `<li>
//                           <div style="display: flex; align-items: center; gap: 5px;">
//                           <a id="a${id}">${operationName}
//                           <span id="span${id}"></span></a>
//                           ${HasEnteringValue ? `<input type="checkbox" ${strCheckNecessary} class="form-control_1"
//                                    style="width:15px;margin-right:250px" onchange="onCheckBoxChange(this)" /><span>عدم الزام محدودیت فاصله حمل</span><span class="LegalOpStyle">براساس تبصره 2 بند 1 فصل 20</span>` : ''}
//                                    <span id="spanOpShomareh${id}"></span>
//                                    </div>
//                           <ul id="ula${id}">`;
//                    html += createTree(data, id); // Recursive call
//                    html += `</ul></li>`;
//                }
//            } else {
//                debugger;
//                showAddedRecord = !HasEnteringValue;
//                html += `
//                <li style="display: flex; align-items: center; gap: 5px;">
//                <a id="a${id}" onclick="OperationClick('${id}',${showAddedRecord})">
//                    ${fbShomareh}-${sharh} <span id="span${id}"></span><span style="color:red">${Description}</span></a>
//                    <span id="spanOpShomareh${fbShomareh}"></span>
//                        ${HasEnteringValue ? `<input class="form-control_1" type="number" value="${currentValue}" 
//                        style="width:100px;margin-right:30px" onchange="onInputChange('${id}', this,${MaxValue},${currentValue})" />
//                    <span> کیلومتر </span><span style="color:blue">${MinValue != 0 ? ' از کیلومتر ' + ` <span class="KMStyle">` + MinValue + `</span>` : ''}</span>
//                <span style="color:blue">${(MaxValue != 0 && MaxValue != null) ? ' سقف فاصله ' + ` <span class="KMStyle">` + MaxValue + `</span>` : ''}</span>` : ''}
//                </li>`;

//                html += `<div id="ula${id}" class="row" style="display: none; margin:10px">
//                        <div id="uldiva${id}" class="col-md-12" style="border:1px solid #79c7ea;padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;">
//                        </div>
//                        </div>`;

//                //html += "<li><a id=\"a" + id + "\" onclick=\"OperationClick('" + id + "')\">" +
//                //    fbShomareh + " - " + sharh + "<span id=\"span" + id + "\"></span></a>" +
//                //    "<span id=\"spanOpShomareh" + fbShomareh + "\"></span></li>";

//                //html += "<div id=\"ula" + id + "\" class=\"row\" style=\"display: none; margin:10px\">" +
//                //    "<div id=\"uldiva" + id + "\" class=\"col-md-12\" style=\"border:1px solid #79c7ea;" +
//                //    "padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;\">" +
//                //    "</div></div>";
//            }
//        });
//    }
//    return html;
//}





function onCheckBoxChange(obj) {
    debugger;
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
            if (data == "OK") {

                toastr.success('ثبت بدرستی انجام گرفت', 'موفقیت');
                GetTreeForOneOperation();
            }
            else
                toastr.error('مشکل در درج', 'خطا');
        },
        error: function (msg) {
            toastr.error('مشکل در ثبت اطلاعات', 'خطا');
        }
    });
}

function onInputChange(id, obj, maxValue, currentValue) {
    debugger;
    FBId = $('#HDFFBID').val();
    if (FBId == '') {
        toastr.info('هیچ یک از آیتم ها انتخاب نشده', 'اطلاع');
        obj.value = currentValue;
        return;
    }

    value = parseFloat(obj.value);
    if (maxValue != null) {
        if (value > maxValue) {
            toastr.info('حداکثر مقدار مجاز ثبت گردید', 'اطلاع');
            //$(obj).addClass('blinking');
            //return false;
        }
    }

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
            GetRizMetreWithFBId(1);
            toastr.success('ثبت بدرستی انجام گرفت', 'موفقیت');
        },
        error: function (msg) {
            toastr.error('مشکل در درج ریزه متره جدید', 'خطا');
        }
    });
}

