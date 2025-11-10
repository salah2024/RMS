//function TreeView(Data, plugin) {
//  $('#jstree').jstree('destroy');
//    $('#jstree').jstree({
//        'plugins': plugin,
//        'core': {
//            //'check_callback': true,
//            'data': Data,
//            'animation': false,
//            //'expand_selected_onload': true,
//            'themes': {
//                'icons': false,
//            }
//        },
//        'search': {
//            'show_only_matches': true,
//            'show_only_matches_children': true
//        }
//    });

//    //$("#jstree").jstree(true).refresh();

//    $('#search').on("keyup change", function () {
//        $('#jstree').jstree(true).search($(this).val())
//    })

//    $('#clear').click(function (e) {
//        $('#search').val('').change().focus()
//    })

//    $('#jstree').one('ready.jstree', function (e, data) {
//        var inst = data.instance;

//        // بستن همه
//        inst.close_all();

//        // آیدی همون <a> که در تصویر انتخاب شده
//        var anchorEl = document.getElementById('a1404229');
//        if (!anchorEl) return;

//        debugger;
//        // از <a> به نود jsTree برس
//        var node = inst.get_node(anchorEl); // ← می‌تونه DOM بگیره
//        if (!node) return;

//        // اجداد را باز کن تا مسیر دیده شود
//        (node.parents || []).forEach(function (p) { inst.open_node(p); });

//        // خود شاخه را باز و انتخاب کن
//        inst.open_node(node);
//        inst.select_node(node);

//        // اگر می‌خواهی فقط همین باز بماند:
//        inst.close_all(node); // بستن بقیه
//    });

//    $('#jstree').on('changed.jstree', function (e, data) {
//        var objects = data.instance.get_selected(true)
//        var leaves = $.grep(objects, function (o) { return data.instance.is_leaf(o) })
//        var list = $('#output')
//        list.empty()
//        $.each(leaves, function (i, o) {
//            $('<li/>').text(o.text).appendTo(list)
//        })
//    });
//}


var gParents = [];
function createTree(data, parentId) {
    var html = "";
    var children = data.filter(function (item) {
        return item.parentId == parentId;
    });

    if (children.length > 0) {
        children.forEach(function (item) {
            var id = item.id;
            var parentId = item.parentId;
            var operationName = item.operationName;
            var funcCall = item.functionCall.trim();
            var fbShomareh = item.itemsFBShomareh.trim();
            var sharh = item.sharh;
            var HasEnteringValue = item.hasEnteringValue;
            var MinValue = item.minValue;
            var MaxValue = item.maxValue;
            var currentValue = item.operationDefaultValue;

            if (fbShomareh === "") {
                if (funcCall !== "") {
                    html += `
      <li>
          <a onclick="${funcCall}('${id}')" id="a${id}">
              ${operationName}
              <span id="span${id}"></span>
          </a>
          <span id="spanOpShomareh${id}"></span>
          <ul style="margin-top:5px" id="ula${id}"></ul>
      </li>`;
                } else {
                    html += `<li>
                           <div style="display: flex; align-items: center; gap: 5px;">
                           <a id="a${id}">${operationName}
                           <span id="span${id}"></span></a>
                           ${HasEnteringValue ? `<input type="checkbox" class="form-control_1" type="number" value="${currentValue}" 
                                  style="width:15px;margin-right:250px" onchange="onInputChange11('${id}', this)" /><span>عدم الزام محدودیت فاصله حمل</span><span class="LegalOpStyle">براساس تبصره 2 بند 1 فصل 20</span>` : ''}
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
             ${fbShomareh}-${sharh} <span id="span${id}"></span></a>
            <span id="spanOpShomareh${fbShomareh}"></span>
            ${HasEnteringValue ? `<input class="form-control_1" type="number" value="${currentValue}" 
            style="width:100px;margin-right:30px" onchange="onInputChange('${id}', this,${MaxValue})" />
            <span> کیلومتر </span><span style="color:blue">${MinValue != 0 ? ' از کیلومتر ' + ` <span class="KMStyle">` + MinValue + `</span>` : ''}</span>
            <span style="color:blue">${(MaxValue != 0 && MaxValue != null) ? ' سقف فاصله ' + ` <span class="KMStyle">` + MaxValue + `</span>` : ''}</span>` : ''}
            </li>`;

                html += `<div id="ula${id}" class="row" style="display: none; margin:10px">
             <div id="uldiva${id}" class="col-md-12" style="border:1px solid #79c7ea;padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;">
             </div>
          </div>`;

                //html += "<li><a id=\"a" + id + "\" onclick=\"OperationClick('" + id + "')\">" +
                //    fbShomareh + " - " + sharh + "<span id=\"span" + id + "\"></span></a>" +
                //    "<span id=\"spanOpShomareh" + fbShomareh + "\"></span></li>";

                //html += "<div id=\"ula" + id + "\" class=\"row\" style=\"display: none; margin:10px\">" +
                //    "<div id=\"uldiva" + id + "\" class=\"col-md-12\" style=\"border:1px solid #79c7ea;" +
                //    "padding-left:0px;padding-right:0px;text-align:center;border-radius:5px !important;\">" +
                //    "</div></div>";
            }



        });
    }

    return html;
}

function onInputChange(id, obj, maxValue) {
    debugger;
    maxValue = maxValue === null ? 0 : maxValue;
    value = parseFloat(obj.value);
    if (value < maxValue) {
        toastr.info('حداکثر مقدار مجاز ثبت گردید', 'اطلاع');
        //$(obj).addClass('blinking');
        //return false;
    }

    BarAvordUserId = $('#HDFBarAvordUserID').val();

    var vardata = new Object();
    vardata.OperationId = id;
    vardata.Value = value;
    vardata.BarAvordId = BarAvordUserId;

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

