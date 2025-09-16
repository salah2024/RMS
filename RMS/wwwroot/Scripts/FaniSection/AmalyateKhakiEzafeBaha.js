function ShowA_KhEzafeBaha(KMExistingId, KMNum) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    debugger;

    var vardata = new Object();
    vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
    vardata.BarAvordUserId = BarAvordUserId;

    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/GetEzafeBaha",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            lstNoeKhakBardariEzafeBaha = response.lstNoeKhakBardariEzafeBaha;
            lstAKhForBEB = response.lstAKhForBEB;

            debugger;

            $container = $('#ViewKhakBardariEzafeBaha' + KMNum);

            var preselectedIds = [];

            $.each(lstNoeKhakBardariEzafeBaha, function () {
                const item = this;
                const checkboxId = `chk-khak-${item.id}`;

                // div کانتینر اصلی
                const $row = $("<div/>", { class: "row", css: { margin: "6px 10px" } });

                // گروه چک‌باکس + متن
                const $group = $("<div/>", {
                    class: "row khak-item",
                    css: { display: "flex", gap: "8px", alignItems: "center", cursor: "pointer" }
                });

                // چک‌باکس
                var $checkbox = $("<input/>", {
                    type: "checkbox",
                    id: checkboxId,
                    name: "khakTypes[]",
                    value: item.id
                });

                // بررسی انتخاب‌شده بودن
                const isInLstAKhForBEB = lstAKhForBEB.some(x => x.noeKhakBardariEzafeBahaId === item.id);
                if (preselectedIds.includes(item.id) || isInLstAKhForBEB) {
                    $checkbox.prop("checked", true);
                }

                // متن لیبل
                const $text = $("<span/>").text(item.noeKhakBardariEzafeBaha);

                $group.append($checkbox, $text);

                // div مخفی زیر گروه
                const $hiddenDiv = $("<div/>", {
                    id: `ViewAKhBEzafeBahaRizMetre${item.id}`,
                    class: "hidden-div row",
                    css: { display: "none", marginTop: "4px" }
                });

                $row.append($group, $hiddenDiv);
                $container.append($row);

                // رویداد change روی چک‌باکس
                $checkbox.on("change", function () {
                    if ($(this).is(":checked")) {
                        SaveKhEzafeBaha(item.id, KMExistingId);
                    } else {
                        DeleteKhEzafeBaha(item.id, KMExistingId);
                    }
                });

                // رویداد کلیک روی گروه
                $group.on("click", function (e) {
                    if ($(e.target).is("span")) {
                        e.preventDefault();

                        if ($checkbox.is(":checked")) {
                            const $div = $(`#ViewAKhBEzafeBahaRizMetre${item.id}`);

                            if ($div.is(":visible")) {
                                $div.slideUp();
                            } else {
                                $("[id^='ViewAKhBEzafeBahaRizMetre']").slideUp();
                                ShowEzafeBahaRizMetre(item.id, KMExistingId);
                                $div.slideDown();
                            }
                        } else {
                            $checkbox.prop("checked", true).trigger("change");
                        }
                    }
                });
            });

            $container.slideDown(500);
        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری اضافه بها', 'خطا');
        }
    });
}

function DeleteKhEzafeBaha(EzafeBahaId, KMExistingId) {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.NoeKhakBardariEzafeBahaId = EzafeBahaId;
    vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/DeleteEzafeBahaAKh",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            toastr.success('حذف اضافه بها بدرستی صورت گرفت', 'موفقیت');

            //ShowEzafeBahaRizMetre(EzafeBahaId, KMExistingId);
            $('#ViewAKhBEzafeBahaRizMetre' + EzafeBahaId).slideUp();
        },
        error: function (response) {
            toastr.error('مشکل در درج اضافه بها', 'خطا');
        }
    });
}

function ShowEzafeBahaRizMetre(EzafeBahaId, KMExistingId) {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.NoeKhakBardariEzafeBahaId = EzafeBahaId;
    vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;

    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/GetAKh_EzafeBaha",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            lstAKhInfoRizMetre = response.lstAKhInfoRizMetre;
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;

            var str = '';
            let groupedData = {};
            lstAKhInfoRizMetre.forEach(function (row) {
                if (!groupedData[row.itemFBShomareh]) {
                    groupedData[row.itemFBShomareh] = [];
                }
                groupedData[row.itemFBShomareh].push(row);
            });
            if (lstAKhInfoRizMetre.length != 0) {

                lstItemFBShomarehForGet.forEach(function (itemGroup) {

                    let itemFBShomareh = itemGroup.itemFBShomareh.substring(0, 6);
                    let des = itemGroup.des;
                    let ItemFields = itemGroup.itemFields;

                    let rows = groupedData[itemFBShomareh] || [];
                    if (rows.length != 0) {
                        // نمایش توضیح گروه
                        str += "<div class='col-12'><span style='color:#000'>" + itemGroup.itemFBShomareh + " - " + des + "</span></div>";

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

                            let id = row.rmId;

                            let strTedad = parseFloat(row.tedad) === 0 ? "0" : isNaN(parseFloat(row.tedad)) ? "" : parseFloat(row.tedad).toString();
                            let strTool = parseFloat(row.tool) === 0 ? "0" : isNaN(parseFloat(row.tool)) ? "" : parseFloat(row.tool).toString();
                            let strArz = parseFloat(row.arz) === 0 ? "0" : isNaN(parseFloat(row.arz)) ? "" : parseFloat(row.arz).toString();
                            let strErtefa = parseFloat(row.ertefa) === 0 ? "0" : isNaN(parseFloat(row.ertefa)) ? "" : parseFloat(row.ertefa).toString();
                            let strVazn = parseFloat(row.vazn) === 0 ? "0" : isNaN(parseFloat(row.vazn)) ? "" : parseFloat(row.vazn).toString();
                            let MeghdarJoz = parseFloat(row.meghdarJoz) === 0 ? "0" : isNaN(parseFloat(row.meghdarJoz)) ? "" : parseFloat(row.meghdarJoz).toString();

                            debugger;

                            let HasDelButton = row.hasDelButton;
                            let HasEditButton = row.hasEditButton;

                            str += "<div class='row styleRowTable' style=\"background-color:#fff\" onclick=\"RizMetreSelectClick('" + id + "')\">";
                            str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span>" + row.shomareh + "</span></div>";

                            str += "<div class='col-md-2'><input  type='text'" +
                                " class='form-control spanStyleMitraSmall' id='txtSharh_" + id + "' value='" + row.sharh + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTedad_" + id + "' value = '" + strTedad + "' /></div > ";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTool_" + id + "' value='" + strTool + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtArz_" + id + "' value='" + strArz + "'/></div>";

                            str += "<div class='col-md-1'><input  type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtErtefa_" + id + "' value='" + strErtefa + "'/></div>";

                            str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                                " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtVazn_" + id + "' value='" + strVazn + "'/></div>";

                            str += "<div class='col-md-1 RMMJozStyle'><span id='spanMeghdarJoz" + id + "'>" + MeghdarJoz + "</div>";

                            str += "<div class='col-md-2'><input  type='text' title='" + row.des + "' style='font-size:12px' class='form-control input-sm' id='txtDes_" + id + "' value='" + row.des + "'/></div>";
                            if (HasDelButton)
                                str += "<div class='col-md-1' style='text-align:center;'><i class='fa fa-trash DelRMUStyle' onclick=\"DeleteRMUAddedItemsClick('" + id + "')\"></i></div>";
                            if (HasEditButton)
                                str += "<div class='col-md-1' style='text-align:center;'><button type='button' id='iUpdate" + id
                                    + "' onclick=\"UpdateAKhBRMEzafeBahaClick(this,'" + id + "')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button></div>";

                            str += "</div>";
                        });
                        str += "</div>";

                    }
                });

                debugger;

                $targetDivAKhBEzafeBahaRizMetreKH = $('#ViewAKhBEzafeBahaRizMetre' + EzafeBahaId);

                $targetDivAKhBEzafeBahaRizMetreKH.html(str);

                $targetDivAKhBEzafeBahaRizMetreKH.slideDown();

                $targetDivAKhBEzafeBahaRizMetreKH.find("input[type='text'].HasEnteringValue")
                    .filter(function () {
                        return $(this).val().trim() === "";
                    })
                    .addClass("blinking")
                    .first()
                    .focus();


                $targetDivAKhBEzafeBahaRizMetreKH.on("change", "input[type='text'].HasEnteringValue", function () {
                    if ($(this).val().trim() !== "") {
                        $(this).removeClass("blinking");
                    }
                });


                $targetDivAKhBEzafeBahaRizMetreKH.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
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
            //    else {
            //        ItemHasConditionIdSplit = ItemHasConditionId.split('_');
            //        var $targetDiv = $("#divShowRizMetre" + ItemHasConditionIdSplit[0]);
            //        $targetDiv.slideUp(500);
            //        $targetDiv.html('');
            //        $('#CK' + ItemHasConditionId).prop('checked', false);
            //    }
        },
        error: function (response) {
            toastr.error('مشکل در درج اضافه بها', 'خطا');
        }
    });

}

function UpdateAKhBRMEzafeBahaClick(obj, RMId) {

    debugger;
    var row1 = $(obj).closest(".row");
    var row = obj.closest(".row");

    MeghdarJoz = row1.find("[id^='spanMeghdarJoz']");
    // گرفتن همه‌ی input های داخل این row
    var inputs = row.querySelectorAll("input[type='text']");
    var vardata = new Object();
    vardata.Id = RMId;

    var Sharh, Tedad, Tool, Arz, Ertefa, Vazn, Des;

    inputs.forEach(function (input) {
        let id = input.id;
        let word = id.replace("txt", "").split("_")[0].toLowerCase();

        switch (word) {
            case "sharh": {
                vardata.Sharh = input.value;
                break;
            }
            case "tedad": {
                Tedad = input.value == "" ? null : input.value;
                break;
            }
            case "tool": {
                Tool = input.value == "" ? null : input.value;
                break;
            }
            case "arz": {
                Arz = input.value == "" ? null : input.value;
                break;
            }
            case "ertefa": {
                Ertefa = input.value == "" ? null : input.value;
                break;
            }
            case "vazn": {
                Vazn = input.value == "" ? null : input.value;
                break;
            }
            case "des": {
                vardata.Des = input.value;
                break;
            }
            default:
        }
    });

    vardata.Tedad = Tedad;

    vardata.Tool = Tool;

    vardata.Arz = Arz;

    vardata.Ertefa = Ertefa;

    vardata.Vazn = Vazn;

    debugger;

    $.ajax({
        type: "POST",
        url: '/AmalyateKhakiInfoForBarAvords/UpdateRizMetreAKhEzafeBaha',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger;
            $(obj).removeAttr('disabled');
            var info = data.split('_');
            if (info[0] == "OK") {
                toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                MeghdarJoz.html(Tool*Arz);
            }
            else
                toastr.info('مشکل در ویرایش ریزه متره انتخابی', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در ویرایش ریزه متره انتخابی', 'خطا');
        }
    });

}


function SaveKhEzafeBaha(EzafeBahaId, KMExistingId) {

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.NoeKhakBardariEzafeBahaId = EzafeBahaId;
    vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/SaveEzafeBahaAKh",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                ShowEzafeBahaRizMetre(EzafeBahaId, KMExistingId);
            }
        },
        error: function (response) {
            toastr.error('مشکل در درج اضافه بها', 'خطا');
        }
    });
}
