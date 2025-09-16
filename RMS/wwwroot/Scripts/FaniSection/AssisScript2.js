function GetAndShowAddItems(ItemHasConditionId, ConditionGroupId) {

    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    OperationId = $('#HDFOperationId').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.strRBCode = ItemHasConditionId;
    vardata.OperationId = OperationId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.ItemHasConditionId = ItemHasConditionId;
    vardata.ConditionGroupId = ConditionGroupId;
    vardata.LevelNumber = 1;
    vardata.NoeFB = NoeFB;

    $.ajax({
        type: "POST",
        url: '/ItemsAddingToFB/GetAndShowAddItems',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            lstItemFBShomarehForGet = data.lstItemFBShomarehForGet;
            lst = data.lst;

            //در صورتی که مقدار ضریب غبر صفر باشد، یعنی 
            //آیتم دارای اضافه یا کسر بهای قیری بوده آیتم های 150801 یا 150802

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

                    debugger;
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
                        str += "<div class=\"col-md-1 spanStyleMitraSmall\"><span>ویرایش/حذف</span></div>";
                        str += "</div>";
                        str += "</div>";

                        str += "<div class='col-12' style='overflow:auto;max-height:400px;padding: 0px;'>";

                        // نمایش ردیف‌های مربوطه
                        rows.forEach(function (row) {
                            debugger;

                            let id = row.id;

                            let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
                            let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
                            let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
                            let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
                            let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
                            let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();


                            let HasDelButton = row.hasDelButton;
                            let HasEditButton = row.hasEditButton;

                            str += "<div class='row styleRowTable' style=\"background-color:#fff\" onclick=\"RizMetreSelectClick('" + id + "')\">";
                            str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span>" + row.shomarehNew + "</span></div>";

                            str += "<div class='col-md-2'><input  type='text'"+
                                " class='form-control spanStyleMitraSmall' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[0] != undefined ?ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "":"") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ?ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "":"") + "' id='txtTedad" + id + "' value = '" + strTedad + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[1] != undefined ?ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "":"") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ?ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "":"") + "' id='txtTool" + id + "' value='" + strTool + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[2] != undefined ?ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "":"") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ?ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "":"") + "' id='txtArz" + id + "' value='" + strArz + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[3]!=undefined? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "":"") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ?ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "":"") + "' id='txtErtefa" + id + "' value='" + strErtefa + "'/></div>";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ?ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "":"") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ?ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "":"") + "' id='txtVazn" + id + "' value='" + strVazn + "'/></div>";

                            str += "<div class='col-md-1 RMMJozStyle'>" + MeghdarJoz + "</div>";

                            str += "<div class='col-md-2'><input  type='text' title='" + row.des + "' style='font-size:12px' class='form-control input-sm' id='txtDes" + id + "' value='" + row.des + "'/></div>";
                            if (HasDelButton)
                                str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-trash DelRMUStyle' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i></div>";
                            if (HasEditButton)
                                str += "<button type='button' id='iUpdate" + id + "' onclick=\"UpdateRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>";

                            str += "</div>";
                        });
                        str += "</div>";
                    }
                });

                ItemHasConditionIdSplit = ItemHasConditionId.split('_');
                var $targetDiv = $("#divShowRizMetre" + ItemHasConditionIdSplit[0]);
                $targetDiv.html(str);

                $targetDiv.find("input[type='text'].HasEnteringValue")
                    .filter(function () {
                        return $(this).val().trim() === "";
                    })
                    .addClass("blinking")
                    .first()
                    .focus();


                $targetDiv.on("change", "input[type='text'].HasEnteringValue", function () {
                    if ($(this).val().trim() !== "") {
                        $(this).removeClass("blinking");
                    }
                });


                $targetDiv.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
                    /* ENTER PRESSED */
                    if (e.keyCode == 13) {
                        /* FOCUS ELEMENT */
                        var inputs = $(this).parent().parent().find("input[Type=text].HasEnteringValue,button");
                        var idx = inputs.index(this);
                        if (idx == inputs.length - 1) {
                            inputs[0].focus();
                            inputs[0].select();
                        } else {
                            while (inputs[idx + 1].disabled == true) {
                                idx++;
                            }
                            inputs[idx + 1].focus(); //  handles submit buttons
                            inputs[idx + 1].select();
                        }
                        return false;
                    }
                });
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


////////////
function DeleteRMUAddedItemsClick(Id, ItemHasConditionId, ConditionGroupId) {
    debugger;
    var vardata = new Object();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    OperationId = $('#HDFOperationId').val();

    vardata.Id = Id;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.OperationId = OperationId;

    $.ajax({
        url: "/RizMetreUser/DeleteRelRizMetre",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
                GetAndShowAddItems(ItemHasConditionId, ConditionGroupId);
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}
////////////
function UpdateRMUAddedItemsClick(Id, ItemHasConditionId, ConditionGroupId) {
    debugger;
    Obj = $('#iUpdate' + Id);
    Obj.prop('disabled', true);
    FBId = $('#HDFFBID').val();
    OperationId = $('#HDFOperationId').val();

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    Sharh = $('#txtSharh' + Id).val();
    Des = $('#txtDes' + Id).val();

    //if ($('#txtTedad' + Id).hasClass('HasEnteringValue')) {
    //    if ($.isNumeric(parseFloat($('#txtTedad' + Id).val()))) {
    //        Tedad = $('#txtTedad' + Id).val().replace(/\,/g, '');
    //    }
    //    else {
    //        $('#txtTedad' + Id).addClass('ErrorValueStyle');
    //        Check = false;
    //    }
    //}
    //else Tedad = 0;

    if ($('#txtTedad' + Id).val() != '') {
        if ($.isNumeric(parseFloat($('#txtTedad' + Id).val()))) {
            Tedad = $('#txtTedad' + Id).val().replace(/\,/g, '');
        }
        else {
            $('#txtTedad' + Id).addClass('ErrorValueStyle');
            Check = false;
        }
    }

    firstObjectHasFocus = null;
    if ($('#txtTool' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtTool' + Id).val()))) {
            Tool = $('#txtTool' + Id).val().replace(/\,/g, '');
            $('#txtTool' + Id).removeClass('ErrorValueStyle');
        }
        else {
            $('#txtTool' + Id).addClass('ErrorValueStyle');
            $('#txtTool' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtTool' + Id);
            }
            Check = false;
        }
    }
    else Tool = undefined;

    if ($('#txtArz' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtArz' + Id).val()))) {
            Arz = $('#txtArz' + Id).val().replace(/\,/g, '');
            $('#txtArz' + Id).removeClass('ErrorValueStyle');

        }
        else {
            $('#txtArz' + Id).addClass('ErrorValueStyle');
            $('#txtArz' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtArz' + Id);
            }
            Check = false;
        }
    }
    else Arz = undefined;

    if ($('#txtErtefa' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtErtefa' + Id).val()))) {
            Ertefa = $('#txtErtefa' + Id).val().replace(/\,/g, '');
            $('#txtErtefa' + Id).removeClass('ErrorValueStyle');

        }
        else {
            $('#txtErtefa' + Id).addClass('ErrorValueStyle');
            $('#txtErtefa' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtErtefa' + Id);
            }
            Check = false;
        }
    }
    else Ertefa = undefined;

    if ($('#txtVazn' + Id).hasClass('HasEnteringValue')) {
        if ($.isNumeric(parseFloat($('#txtVazn' + Id).val()))) {
            Vazn = $('#txtVazn' + Id).val().replace(/\,/g, '');
            $('#txtVazn' + Id).removeClass('ErrorValueStyle');

        }
        else {
            $('#txtVazn' + Id).addClass('ErrorValueStyle');
            $('#txtVazn' + Id).removeClass('TextEdit');
            if (firstObjectHasFocus == null) {
                firstObjectHasFocus = $('#txtVazn' + Id);
            }
            Check = false;
        }
    }
    else Vazn = undefined;

    debugger;
    if (firstObjectHasFocus != null)
    firstObjectHasFocus.focus();
    var vardata = new Object();
    vardata.Id = Id;
    vardata.Sharh = Sharh;
    vardata.Tedad = Tedad === undefined ? null : Tedad;
    vardata.Tool = Tool === undefined ? null : Tool;
    vardata.Arz = Arz === undefined ? null : Arz;
    vardata.Ertefa = Ertefa === undefined ? null : Ertefa;
    vardata.Vazn = Vazn === undefined ? null : Vazn;
    vardata.Des = Des;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.FBId = FBId;
    vardata.OperationId = OperationId;
    vardata.LevelNumber = 1;
    if (Check) {
        $.ajax({
            type: "POST",
            url: '/RizMetreUser/UpdateRizMetreUsersAddedItems',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                Obj.removeAttr('disabled');

                var info = data.split('_');
                if (info[0] == "OK") {
                  
                    GetAndShowAddItems(ItemHasConditionId, ConditionGroupId);
                    toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                }
                else
                    toastr.info('مشکل در ویرایش ریزه متره انتخابی', 'اطلاع');
            },
            error: function (msg) {
                toastr.error('مشکل در ویرایش ریزه متره انتخابی', 'خطا');
            }
        });
    }
    else {
        Obj.removeAttr('disabled');
        toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
    }
}
/////

function GetRizMetreWithFBId(LevelNumber) {
    NoeFB = parseInt($('#HDFNoeFB').val());
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();
    FBId = $('#HDFFBID').val();
    Operation = $('#HDFOperationId').val();
    ItemsFBShomareh = $('#HDFItemsFBShomareh').val();
    var vardata = new Object();
    vardata.Operation = Operation;
    vardata.BarAvordId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.LevelNumber = LevelNumber;
    vardata.FBId = FBId;

    $.ajax({
        type: "POST",
        url: "/Operation_ItemsFB/GetRizMetreWithFBId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var itemsFields = response.itemsFields;
            var rizMetreUsers = response.rizMetreUsers;

            var lstItemsFields = "";
            for (var i = 0; i < itemsFields.length; i++) {
                lstItemsFields = itemsFields.map(x => (x.isEnteringValue ? true : false)).join(",");
            }

            var str = "";

            if (rizMetreUsers.length == 0) {
                //var $targetDiv = $("#Grid" + ItemsFBShomareh).find(".RMCollectStyle");
                var $targetDiv = $("#uldiva" + Operation).find(".RMCollectStyle");
                $targetDiv.html(str);

                $('#divAddedItems' + Operation)
                    .find('input[type=checkbox]').each(function () {
                        $(this).prop('checked', false);
                        debugger;
                    });
                $('#divAddedItems' + Operation).slideUp(1000);
            }
            else {
                rizMetreUsers.forEach(function (row) {

                    let id = row.id;

                    let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
                    let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
                    let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
                    let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
                    let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
                    let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();

                    str += "<div class='row styleRowTable' onclick=\"RizMetreSelectClick('" + id + "')\">";
                    str += "<div class='col-md-1'><div class='col-md-12' style='padding-left:0px;'><span>" + row.shomareh + "</span></div></div>";

                    str += "<div class='col-md-2'><input type='text' class='form-control spanStyleMitraSmall TextEdit row-input' id='txtSharh" + id + "' value='" + row.sharh + "'/></div>";

                    str += "<div class='col-md-1'><input type='text'" + (itemsFields[0].isEnteringValue !== true ? " disabled='disabled'" : "") +
                        " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[0].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtTedad" + id + "' value='" + strTedad + "'/></div>";

                    str += "<div class='col-md-1'><input type='text'" + (itemsFields[1].isEnteringValue !== true ? " disabled='disabled'" : "") +
                        " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[1].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtTool" + id + "' value='" + strTool + "'/></div>";

                    str += "<div class='col-md-1'><input type='text'" + (itemsFields[2].isEnteringValue !== true ? " disabled='disabled'" : "") +
                        " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[2].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtArz" + id + "' value='" + strArz + "'/></div>";

                    str += "<div class='col-md-1'><input type='text'" + (itemsFields[3].isEnteringValue !== true ? " disabled='disabled'" : "") +
                        " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[3].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtErtefa" + id + "' value='" + strErtefa + "'/></div>";

                    str += "<div class='col-md-1'><input type='text'" + (itemsFields[4].isEnteringValue !== true ? " disabled='disabled'" : "") +
                        " class='form-control spanStyleMitraSmall TextEdit row-input" + (itemsFields[4].isEnteringValue === true ? " HasEnteringValue " : "") + "' id='txtVazn" + id + "' value='" + strVazn + "'/></div>";

                    str += "<div class='col-md-1 RMMJozStyle'>" + (MeghdarJoz === 0 ? "" : MeghdarJoz) + "</div>";

                    str += "<div class='col-md-2'><input type='text' class='form-control input-sm TextEdit row-input' id='txtDes" + id + "' value='" + row.des + "'/></div>";

                    str += "<div class='col-md-1'>" +
                        "<i class='fa fa-edit EditRMUStyle displayNone' id='iEdit" + id + "' onclick=\"EditRMUClick('" + id + "', '" + Operation + "', '" + row.fbId + "', '" + lstItemsFields + "')\"></i>" +
                        "<button type=\"button\" id='iUpdate" + id + "' onclick=\"UpdateRMUClick('" + id + "')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>" +
                        "<button type=\"button\" onclick=\"DeleteRMUClick('" + id + "')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-trash DelRMUStyle\"></i></button></div>";

                    str += "</div>";
                });
                debugger;
                //var $targetDiv = $("#Grid" + ItemsFBShomareh).find(".RMCollectStyle");
                var $targetDiv = $("#uldiva" + Operation).find(".RMCollectStyle");
                $targetDiv.html(str);

                $targetDiv.on('keydown', '.row-input', function (e) {
                    if (e.key === 'Enter') {
                        e.preventDefault();

                        var $currentInput = $(this);
                        var $row = $currentInput.closest('.row'); // فقط inputهای همین ردیف
                        var $inputsInRow = $row.find('.row-input:visible:enabled');
                        var currentIndex = $inputsInRow.index($currentInput);

                        if (currentIndex !== -1 && currentIndex < $inputsInRow.length - 1) {
                            $inputsInRow.eq(currentIndex + 1).focus();
                        } else {
                            // آخرین input هست، برو روی دکمه iUpdate
                            var rowId = $row.attr('onclick').match(/'(.*?)'/)[1]; // استخراج ID از onClick
                            $row.find("#iUpdate" + rowId).focus();
                        }
                    }
                });


                $targetDiv.animate({ scrollTop: $targetDiv[0].scrollHeight }, 500);
                $('#divAddedItems' + Operation).slideDown(1000);
            }

            //$targetDiv.scrollTop($targetDiv[0].scrollHeight);



            if (rizMetreUsers.length != 0) {
                if (!$("#divItemsAddedAndRel" + ItemsFBShomareh + " .styleHeaderTable1").length > 0) {
                    GetItemsAddedAndRelForRizMetre(LevelNumber);
                }
            }
        },
        error: function (response) {
            toastr.error('مشکل در دریافت اطلاعات', 'خطا');
        }
    });
}

function GetItemsAddedAndRelForRizMetre(LevelNumber) {


    BarAvordUserId = $('#HDFBarAvordUserID').val();
    IsFromAddedOperation = $('#HDFIsFromAddedOperation').val();
    if (IsFromAddedOperation == 'true')
        OperationId = $('#HDFOperationIdN').val();
    else
        OperationId = $('#HDFOperationId').val();
    FBId = $('#HDFFBID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    ForItem = '';
    //if (IsFromAddedOperation == 'true')
    ForItem = $('#HDFItemsFBShomareh').val();

    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.IsFromAddedOperation = IsFromAddedOperation;
    vardata.BarAvordId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.OperationId = OperationId;
    vardata.ForItem = ForItem;
    vardata.LevelNumber = LevelNumber;

    $.ajax({
        type: "POST",
        url: "/RizMetreUser/GetAddedAndRelItemsForRizMetreUsers",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            $('#divItemsAddedAndRel' + ForItem).html(response);
            $('#divAddedItems' + OperationId).show();
        },
        error: function (response) {
            toastr.error('مشکل در دریافت اطلاعات', 'خطا');
        }
    });
}


function CheckOperationHasExistActiveCondition(OperationId) {
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    FBId = $('#HDFFBID').val();

    var vardata = new Object();
    vardata.OperationId = OperationId;
    vardata.BarAvordId = BarAvordUserID;
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;
    vardata.FBId = FBId;

    $.ajax({
        type: "POST",
        url: "Operation/CheckOperationHasExistActiveCondition",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
        },
        error: function (response) {
            toastr.error('مشکل در دریافت اطلاعات', 'خطا');
        }
    });
}