function ShowBoardStand(OperationId) {

    $('#HDFOperationId').val(OperationId);
    BarAvordUserId = $('#HDFBarAvordUserID').val();

    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;

    $.ajax({
        type: "POST",
        url: "/BoardStand/ReturnBoardStandItems",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(vardata),
        success: function (data) {
            debugger;

            lst = data.boardStandList;
            lstBarAvordAddedBoardStand = data.lstBarAvordAddedBoardStand;

            str = '<div class="BoardStandEntryStyle row">';
            $.each(lst, function (index, item) {
                str += `
                <div class="row col-12" style="margin-bottom: 10px;">
                    <div class="col-3">
                    <input type="checkbox"  class="chkBoardStand" name="" value="${index + 1}">
                    <span id="spanBoardStand${index + 1}" class="spanBoardStandStyle">${item}</span>
                    </div>
                    <div class="col-1" style="text-align:left">
                    <span>تعداد: </span>
                    </div>
                    <div class="col-1">
                    <input  class="txtBoardStand form-control" style="height: 25px;text-align:center" type="number" name="" min="0" max="10000" step="1" placeholder="">
                    </div>
                </div>
                `;
            });
            str += '</div>';
            str += `<div id="divBoardStandRizMetre${OperationId}"></div>`

            $('#ula' + OperationId).html(str);
            debugger;

            $.each(lstBarAvordAddedBoardStand, function (i, item) {
                // پیدا کردن چک‌باکس با مقدار BoardStandType
                let $chk = $('#ula' + OperationId).find('.chkBoardStand[value="' + item.boardStandType + '"]');

                if ($chk.length > 0) {
                    // تیک زدن چک‌باکس
                    $chk.prop('checked', true);

                    // پیدا کردن تکست‌باکس کنار همون چک‌باکس
                    let $txt = $chk.closest('.row').find('.txtBoardStand');

                    // مقداردهی تکست‌باکس
                    $txt.val(item.tedad);
                }
            });

            $('#ula' + OperationId).off('click', '.spanBoardStandStyle')
                .on('click', '.spanBoardStandStyle', function () {

                    let $span = $(this);
                    let $row = $(this).closest(".row");
                    let $chk = $row.find(".chkBoardStand");
                    let $txt = $row.find(".txtBoardStand");

                    $('.spanBoardStandStyle').removeClass('spanBlinking');

                    $span.addClass("spanBlinking");

                    if ($chk.is(":checked")) {
                        // وقتی تیک خورده → ShowRizMetreBoardStand
                        let arr = [parseInt($chk.val())];
                        ShowRizMetreBoardStand(arr);
                    } else {
                        // وقتی تیک نخورده → SaveBoardStand
                        let tedad = $txt.val().trim();
                        if (tedad !== "" && tedad != 0) {
                            SaveBoardStand(tedad, $chk.val());
                        } else {
                            $txt.focus();
                            $txt.addClass('blinking');
                            $span.removeClass("spanBlinking");
                            toastr.warning("لطفا تعداد معتبر وارد کنید", "توجه");
                        }
                    }
                });


            $('#ula' + OperationId).off('change', ".chkBoardStand").on('change', ".chkBoardStand", function () {
                debugger;
                let $row = $(this).closest(".row");
                let $txt = $row.find(".txtBoardStand");

                if ($(this).is(":checked")) {
                    if ($txt.val().trim() === "") {
                        $txt.addClass("blinking");
                        $txt.focus();
                        $(this).prop('checked', false);
                    }
                } else {
                    $txt.val("");  // خالی کردن مقدار
                    $txt.removeClass("blinking");
                    DeleteBoardStand($(this).val());
                }
            });

            $('#ula' + OperationId).off('change', '.txtBoardStand').on('change', '.txtBoardStand', function () {
                debugger;
                let $txt = $(this);
                let $row = $(this).closest(".row");
                let $span = $(this).closest(".spanBoardStandStyle");
                let $chk = $row.find(".chkBoardStand");

                $txt.prop("disabled", true).addClass("loading");

                if ($(this).val().trim() !== "") {
                    if ($(this).val().trim() != 0) {
                        $(this).removeClass("blinking");
                        $chk.prop("checked", true);
                        SaveBoardStand($txt.val().trim(), $chk.val())
                            .always(function () {
                                // بعد از اتمام کار، دوباره فعال کن
                                $txt.prop("disabled", false).removeClass("loading");
                            });
                    }
                    else {
                        $chk.prop("checked", false);
                        DeleteBoardStand($chk.val())
                            .always(function () {
                            // بعد از اتمام کار، دوباره فعال کن
                            $txt.prop("disabled", false).removeClass("loading");
                        });
                    }
                } else {
                    // اگر خالی شد دوباره blinking بده
                    if ($chk.is(":checked")) {
                        $(this).addClass("blinking");
                    }
                }
            });


            //$('#ula' + OperationId).on('blur', ".txtBoardStand", function () {
            //    let val = $(this).val().trim();
            //        let $row = $(this).closest(".row");
            //        let $chk = $row.find(".chkBoardStand");
            //    if (val !== "") {
            //        SaveBoardStand(val, $chk.val());   // فراخوانی تابع ذخیره
            //    } else {

            //        $chk.prop("checked", false).trigger("change");
            //    }
            //});

            //$('#ula' + OperationId).on('keypress', ".txtBoardStand", function (e) {
            //    if (e.which === 13) { // کلید Enter

            //        let val = $(this).val().trim();
            //        let $row = $(this).closest(".row");
            //        let $chk = $row.find(".chkBoardStand");

            //        if (val !== "") {
            //            SaveBoardStand(val, $chk.val());   // فراخوانی تابع ذخیره
            //        } else {
            //            $chk.prop("checked", false).trigger("change");
            //        }
            //    }
            //});

            //$('#ula' + OperationId).on('change', ".txtBoardStand", function (e) {
            //    let val = $(this).val().trim();
            //    let $row = $(this).closest(".row");
            //    let $chk = $row.find(".chkBoardStand");

            //    if (val !== "") {
            //        if(val!=0)
            //            SaveBoardStand(val, $chk.val());   // فراخوانی تابع ذخیره
            //        else
            //            DeleteBoardStand([$chk.val()]); 
            //    } else {
            //        $chk.prop("checked", false).trigger("change");
            //    }
            //});

            lstBoardStandType = [1, 2, 3, 4];
            ShowRizMetreBoardStand(lstBoardStandType);
            //$('#ula' + OperationId).show();

        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اطلاعات پایه تابلو انتخابی', 'خطا');
        }
    });
}

function ShowRizMetreBoardStand(lstBoardStandType) {
    debugger;
    OperationId = $('#HDFOperationId').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.lstBoardStandType = lstBoardStandType;

    $.ajax({
        type: "POST",
        url: '/BoardStand/GetRizMetreForBoardStand',
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
                                " class='form-control spanStyleMitraSmall' style='direction:ltr;' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                            str += "<div class='col-md-1'  style='direction:ltr'><input type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "" : "")
                                + "' id='txtTedad" + rizmetreid + "' value = '" + strTedad + "'/></div> ";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTool" + rizmetreid + "' value='" + strTool + "'/></div>";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtArz" + rizmetreid + "' value='" + strArz + "'/></div>";

                            str += "<div class='col-md-1'><input disabled type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtErtefa" + rizmetreid + "' value='" + strErtefa + "'/></div>";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtVazn" + rizmetreid + "' value='" + strVazn + "'/></div>";

                            str += "<div class='col-md-1 RMMJozStyle' style='direction:ltr'><span id='MeghdarJoz" + rizmetreid + "'>" + MeghdarJoz + "</span></div>";

                            str += "<div class='col-md-2'><input disabled type='text' title='" + row.des + "' style='font-size:12px' class='form-control input-sm' id='txtDes" + rizmetreid + "' value='" + row.des + "'/></div>";
                            //str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-pen' onclick=\"ViewRMUDetailsClick('" + BarAvordAddedBoardId + "')\"></i></div>";
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


                $('#divBoardStandRizMetre' + OperationId).html(str);
                $('#divBoardStandRizMetre' + OperationId).slideDown(500);
            }
            else {

                $('#divBoardStandRizMetre' + OperationId).html('');
                $('#divBoardStandRizMetre' + OperationId).slideUp(500);
            }

            //$('#divShowRizMetre' + ItemHasConditionId).slideToggle(500);
        },
        error: function (msg) {
            toastr.error('خطا در درج اطلاعات', 'خطا');
        }
    });
}

function DeleteBoardStand(BoardStandType) {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.BoardStandType = BoardStandType;

   return $.ajax({
        type: "POST",
        url: "/BoardStand/DeleteBoardStand",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(vardata),
        success: function (data) {
            toastr.success('حذف بدرستی صورت گرفت', 'موفقیت');

            ShowRizMetreBoardStand([BoardStandType]);
        },
        error: function (response) {
            toastr.error('مشکل در حذف پایه تابلو انتخابی', 'خطا');
        }
    });
}
function SaveBoardStand(Tedad, ckVal) {
    var vardata = new Object();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();

    vardata.BaravordId = BarAvordUserId;
    vardata.Tedad = Tedad;
    vardata.BoardStandType = ckVal;
    vardata.Year = Year;

   return $.ajax({
        type: "POST",
        url: "/BoardStand/SaveBoardStand",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(vardata),
        success: function (data) {
    debugger;
            toastr.success('درج بدرستی صورت گرفت', 'موفقیت');

            let arr = [parseInt(ckVal)];
            ShowRizMetreBoardStand(arr);
        },
        error: function (response) {
            toastr.error('مشکل در ذخیره پایه تابلو انتخابی', 'خطا');
        }
    });
}
