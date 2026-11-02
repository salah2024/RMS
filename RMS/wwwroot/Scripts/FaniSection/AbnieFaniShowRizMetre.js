
function showSubItemsRizMetre(item) {
    var $current = $('#' + item);

    if ($current.is(':visible')) {
        $current.slideUp(500);
    } else {
        $('.subItemRizMetre').not($current).hide();
        $current.slideDown(500);
    }
}

function AbnieFaniShowRizMetre(PolId) {

    NoeFBId = parseInt($('#HDFNoeFB').val());

    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.PolId = PolId;
    vardata.NoeFB = NoeFBId;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: "/RizMetreUserAbnieFani/GetRizMetreAbnieFaniWithPolId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;
            lst = response.lst;
            //const lstItemFBShomarehForGet = [...new Set(data.map(item => item.itemFBShomareh))];
            var str = '';

            // ساخت HTML بر اساس lstItemFBShomarehForGet

            // گروه‌بندی data.lst بر اساس itemFBShomareh
            let groupedData = {};
            lst.forEach(function (row) {
                if (!groupedData[row.fbShomareh]) {
                    groupedData[row.fbShomareh] = [];
                }
                groupedData[row.fbShomareh].push(row);
            });


            // ساخت HTML بر اساس lstItemFBShomarehForGet
            if (lst.length != 0) {
                lstItemFBShomarehForGet.forEach(function (itemGroup) {
                    let itemFBShomareh = itemGroup.itemFBShomareh.substring(0, 6);
                    let des = itemGroup.des;
                    let ItemFields = itemGroup.itemFields;

                    let rows = groupedData[itemFBShomareh] || [];
                    if (rows.length != 0) {

                        str += `
<div class='row col-12'>
    <div class='row col-md-12 showSubItemsRizMetreClass' onclick="showSubItemsRizMetre('subItem-${itemGroup.itemFBShomareh}')">
        <span class='spanDesForEzafeBahaStyle'>
            ${itemGroup.itemFBShomareh} - ${des}
        </span>
    </div>

    <div class="row col-12 subItemRizMetre" id="subItem-${itemGroup.itemFBShomareh}" style="display:none">
<div class="row col-12 styleHeaderTable" style="text-align: center;">
    <div class="col-md-1 spanStyleMitraSmall" style="text-align:center">
        <span id="spanFieldShomarehName">ردیف</span>
    </div>

    <div class="col-md-3 spanStyleMitraSmall">شرح</div>

    <div class="col-md-1 spanStyleMitraSmall">
        <div style="padding-bottom:3px;border-bottom:1px solid #fff">تعداد</div>
        <div class="VahedStyle">${ItemFields[0] ? ItemFields[0].vahed : ""}</div>
    </div>

    <div class="col-md-1 spanStyleMitraSmall">
        <div style="padding-bottom:3px;border-bottom:1px solid #fff">طول</div>
        <div class="VahedStyle">${ItemFields[1] ? ItemFields[1].vahed : ""}</div>
    </div>

    <div class="col-md-1 spanStyleMitraSmall">
        <div style="padding-bottom:3px;border-bottom:1px solid #fff">عرض</div>
        <div class="VahedStyle">${ItemFields[2] ? ItemFields[2].vahed : ""}</div>
    </div>

    <div class="col-md-1 spanStyleMitraSmall">
        <div style="padding-bottom:3px;border-bottom:1px solid #fff">ارتفاع</div>
        <div class="VahedStyle">${ItemFields[3] ? ItemFields[3].vahed : ""}</div>
    </div>

    <div class="col-md-1 spanStyleMitraSmall">
        <div style="padding-bottom:3px;border-bottom:1px solid #fff">وزن یا...</div>
        <div class="VahedStyle">${ItemFields[4] ? ItemFields[4].vahed : ""}</div>
    </div>

    <div class="col-md-1 spanStyleMitraSmall">
        <div style="padding-bottom:3px;border-bottom:1px solid #fff">مقدار جزء</div>
        <div class="VahedStyle">${ItemFields[5] ? ItemFields[5].vahed : ""}</div>
    </div>

    <div class="col-md-2 spanStyleMitraSmall">توضیحات</div>

</div>
`;

                        str += `<div class='col-12' style='overflow:auto;max-height:400px;padding: 0px;'>`;

                        // نمایش ردیف‌های مربوطه
                        rows.forEach(function (row) {

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

                             str += `
<div class='row styleRowTable' style="background-color:#fff">

    <div class='col-md-1' style="text-align:center;color:#000">
        <span>${row.shomareh}</span>
    </div>

    <div class='col-md-3'>
        <input disabled type='text'
               class='form-control spanStyleMitraSmall'
               id='txtSharh${id}'
               value='${row.sharh}' />
    </div>

    <div class='col-md-1'>
        <input disabled type='text'
               class='form-control spanStyleMitraSmall'
               id='txtTedad${rizmetreid}'
               value='${strTedad}' />
    </div>

    <div class='col-md-1'>
        <input disabled type='text'
               class='form-control spanStyleMitraSmall'
               id='txtTool${rizmetreid}'
               value='${strTool}' />
    </div>

    <div class='col-md-1'>
        <input disabled type='text'
               class='form-control spanStyleMitraSmall'
               id='txtArz${rizmetreid}'
               value='${strArz}' />
    </div>

    <div class='col-md-1'>
        <input disabled type='text'
               class='form-control spanStyleMitraSmall'
               id='txtErtefa${rizmetreid}'
               value='${strErtefa}' />
    </div>

    <div class='col-md-1'>
        <input disabled type='text'
               class='form-control spanStyleMitraSmall'
               id='txtVazn${rizmetreid}'
               value='${strVazn}' />
    </div>

    <div class='col-md-1'>
        <span class='RMMJozStyle' id='MeghdarJoz${rizmetreid}'>${MeghdarJoz}</span>
    </div>

    <div class='col-md-2'>
        <input disabled type='text'
               title='${row.des}'
               style='font-size:12px'
               class='form-control input-sm'
               id='txtDes${rizmetreid}'
               value='${row.des}' />
    </div>
</div>
`;

                            //str += "</div>";
                        });
                        str += `
                                </div>
                                </div>
                            </div>
                        </div>`;
                    }
                });

                //$('#divBoardRizMetre' + operationId).html(str);
            }
            else {
                ItemHasConditionIdSplit = ItemHasConditionId.split('_');
                var $targetDiv = $("#divShowRizMetre" + ItemHasConditionIdSplit[0]);
                $targetDiv.slideUp(500);
                $targetDiv.html('');

                $('#CK' + ItemHasConditionId).prop('checked', false);

            }


            var $targetDiv = $("#ViewRizMetrePol-" + PolId);


            debugger;

            $targetDiv.html(str);

            if ($targetDiv.hasClass('displayShow')) {
                $targetDiv.removeClass('displayShow');
                $targetDiv.hide();
            }
            else {
                $targetDiv.addClass('displayShow');
                $targetDiv.show();
            }


            //var itemsFields = response.itemsFields;
            //var rizMetreUsers = response.rizMetreUsers;

            //var lstItemsFields = "";
            //for (var i = 0; i < itemsFields.length; i++) {
            //    lstItemsFields = itemsFields.map(x => (x.isEnteringValue ? true : false)).join(",");
            //}

            //var str = "";

            //if (rizMetreUsers.length == 0) {
            //    var $targetDiv = $("#ViewRizMetrePol");
            //    $targetDiv.html(str);
            //}
            //else {
            //    rizMetreUsers.forEach(function (row) {

            //        debugger;

            //        let id = row.id;

            //        let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
            //        let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
            //        let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
            //        let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
            //        let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
            //        let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();

            //        str += "<div class='row styleRowTable' onclick=\"RizMetreSelectClick('" + id + "')\">";
            //        str += "<div class='col-md-1'><div class='col-md-12' style='padding-left:0px;'><span>" + row.shomarehNew + "</span></div></div>";

            //        str += "<div class='col-md-2'><input type='text' class='form-control spanStyleMitraSmall TextEdit row-input' id='txtSharh" + id + "' value='" + row.sharh + "'/></div>";

            //        str += "<div class='col-md-1'><input type='text'" + (itemsFields[0].isEnteringValue !== true ? " disabled='disabled'" : "") +
            //            " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[0].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtTedad" + id + "' value='" + strTedad + "'/></div>";

            //        str += "<div class='col-md-1'><input type='text'" + (itemsFields[1].isEnteringValue !== true ? " disabled='disabled'" : "") +
            //            " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[1].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtTool" + id + "' value='" + strTool + "'/></div>";

            //        str += "<div class='col-md-1'><input type='text'" + (itemsFields[2].isEnteringValue !== true ? " disabled='disabled'" : "") +
            //            " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[2].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtArz" + id + "' value='" + strArz + "'/></div>";

            //        str += "<div class='col-md-1'><input type='text'" + (itemsFields[3].isEnteringValue !== true ? " disabled='disabled'" : "") +
            //            " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[3].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtErtefa" + id + "' value='" + strErtefa + "'/></div>";

            //        str += "<div class='col-md-1'><input type='text'" + (itemsFields[4].isEnteringValue !== true ? " disabled='disabled'" : "") +
            //            " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[4].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtVazn" + id + "' value='" + strVazn + "'/></div>";

            //        str += "<div class='col-md-1 RMMJozStyle'>" + (MeghdarJoz === 0 ? "" : MeghdarJoz) + "</div>";

            //        str += "<div class='col-md-2'><input type='text' class='form-control input-sm TextEdit row-input' id='txtDes" + id + "' value='" + row.des + "'/></div>";

            //        str += "<div class='col-md-1'>" +
            //            "<i class='fa fa-edit EditRMUStyle displayNone' id='iEdit" + id + "'></i>" +
            //            "<button type=\"button\" id='iUpdate" + id + "' onclick=\"UpdateRMUClick('" + id + "')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>" +
            //            "<button type=\"button\" onclick=\"DeleteRMUClick('" + id + "')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-trash DelRMUStyle\"></i></button></div>";

            //        str += "</div>";
            //    });
            //    debugger;
            //    var $targetDiv = $("#ViewRizMetrePol");
            //    $targetDiv.html(str);

            //    $targetDiv.on('keydown', '.row-input', function (e) {
            //        if (e.key === 'Enter') {
            //            e.preventDefault();

            //            var $currentInput = $(this);
            //            var $row = $currentInput.closest('.row'); // فقط inputهای همین ردیف
            //            var $inputsInRow = $row.find('.row-input:visible:enabled');
            //            var currentIndex = $inputsInRow.index($currentInput);

            //            if (currentIndex !== -1 && currentIndex < $inputsInRow.length - 1) {
            //                $inputsInRow.eq(currentIndex + 1).focus();
            //            } else {
            //                // آخرین input هست، برو روی دکمه iUpdate
            //                var rowId = $row.attr('onclick').match(/'(.*?)'/)[1]; // استخراج ID از onClick
            //                $row.find("#iUpdate" + rowId).focus();
            //            }
            //        }
            //    });


            //    $targetDiv.animate({ scrollTop: $targetDiv[0].scrollHeight }, 500);
            //    $('#divAddedItems' + Operation).slideDown(1000);
            //}

            //$targetDiv.scrollTop($targetDiv[0].scrollHeight);

            //if (lst.length != 0) {
            //    if (!$("#divItemsAddedAndRel" + ItemsFBShomareh + " .styleHeaderTable1").length > 0) {
            //        debugger;
            //        PayKani = $('#HDFPayKani').val().split('_');
            //        if (PayKani[0] == 'true') {
            //            if (PayKani[1]==2) {
            //                PayKaniClick(Operation, PayKani[1]);
            //            }
            //            else if (PayKani[1]==3) {
            //                ChanelKaniClick(Operation, PayKani[1]);
            //            }
            //        }
            //        else
            //            GetItemsAddedAndRelForRizMetre(LevelNumber);
            //    }
            //}
        },
        error: function (response) {
            toastr.error('مشکل در دریافت اطلاعات', 'خطا');
        }
    });
}