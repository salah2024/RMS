var timerAddPol;
function TabClick(currentTab) {
    if (currentTab == 'Plan') {
        $('#AbnieShow').find('#svgContainerPol').hide()
        $('#AbnieShow').find('#divPol').hide();
        $('#AbnieShow').find('#divPlan').show();
        $('#AbnieShow').find('#tabPlan').addClass('PlanTableActiveTab');
        $('#AbnieShow').find('#tabPol').removeClass('PlanTableActiveTab');
        $('#AbnieShow').find('#tabAddedItemsForAbnieFaniPol').removeClass('PlanTableActiveTab');
        $('#AbnieShow').find('#divAddedItemsForAbnieFaniPol').hide();

        //$('#alfaw1').val(135);
        //$('#alfaw2').val(135);
        //$('#alfaw3').val(135);
        //$('#alfaw4').val(135);

        //$('#hMinw1').val(1);
        //$('#hMinw2').val(1);
        //$('#hMinw3').val(1);
        //$('#hMinw4').val(1);


        $('#AbnieShow').find('#divPlan').find('input').change(function () {
            //////////////چشمک زدن دکمه ذخیره بعد از تغییر
            ChangeSaveButtonColor();
            h = parseFloat($('#drdHadeAksarErtefa').val());
            t = parseFloat($('#HDFPolt').val());
            ObjectId = $(this).attr('id');
            if (ObjectId.substring(0, 5) == 'hMinw') {
                if (parseFloat($(this).val()) > h + t) {
                    $(this).val(h + t);
                    toastr.info('ارتفاع دستک نبایستی از حداکثر ارتفاع کوله بعلاوه ضخامت دال انتخابی بیشتر باشد', 'اطلاع');
                }
            }

            if (ObjectId == 'alfaw1') {
                if (parseFloat($('#AbnieShow').find('#alfaw1').val()) > 180) {
                    toastr.info('زاویه دستک نبایستی بیش از 180 درجه باشد', 'اطلاع');
                    $('#alfaw1').val(180);
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hMinw1 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw1').val())) / (45)));
                    $('#AbnieShow').find('#hMinw1').val(hMinw1.toFixed(2));
                }
                else if (parseFloat($('#AbnieShow').find('#alfaw1').val()) >= 90 && parseFloat($('#AbnieShow').find('#alfaw1').val()) < 135) {
                    $('#AbnieShow').find('#hMinw1').val(1);
                }
                else if (parseFloat($('#AbnieShow').find('#AbnieShow').find('#alfaw1').val()) < 90) {
                    toastr.info('زاویه دستک نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfaw1').val(90);
                    $('#AbnieShow').find('#hMinw1').val(1);
                }
                else {
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hMinw1 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw1').val())) / (45)));
                    $('#AbnieShow').find('#hMinw1').val(hMinw1.toFixed(2));
                }
            }

            if (ObjectId == 'alfaw2') {
                if (parseFloat($('#AbnieShow').find('#alfaw2').val()) > 180) {
                    toastr.info('زاویه دستک نبایستی بیش از 180 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfaw2').val(180);
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hminw2 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw2').val())) / (45)));
                    $('#AbnieShow').find('#hMinw2').val(hminw2.toFixed(2));
                }
                else if (parseFloat($('#AbnieShow').find('#alfaw2').val()) >= 90 && parseFloat($('#AbnieShow').find('#alfaw2').val()) < 135) {
                    $('#AbnieShow').find('#hMinw2').val(1);
                }
                else if (parseFloat($('#AbnieShow').find('#alfaw2').val()) < 90) {
                    toastr.info('زاویه دستک نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfaw2').val(90);
                    $('#AbnieShow').find('#hMinw2').val(1);
                }
                else {
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hminw2 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw2').val())) / (45)));
                    $('#AbnieShow').find('#hMinw2').val(hminw2.toFixed(2));
                }
            }

            if (ObjectId == 'alfaw3') {
                if (parseFloat($('#alfaw3').val()) > 180) {
                    toastr.info('زاویه دستک نبایستی بیش از 180 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfaw3').val(180);
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hMinw3 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw3').val())) / (45)));
                    $('#AbnieShow').find('#hMinw3').val(hMinw3.toFixed(2));
                }
                else if (parseFloat($('#AbnieShow').find('#alfaw3').val()) >= 90 && parseFloat($('#AbnieShow').find('#alfaw3').val()) < 135) {
                    $('#AbnieShow').find('#hMinw3').val(1);
                }
                else if (parseFloat($('#alfaw3').val()) < 90) {
                    toastr.info('زاویه دستک نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfaw3').val(90);
                    $('#AbnieShow').find('#hMinw3').val(1);
                }
                else {
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hMinw3 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw3').val())) / (45)));
                    $('#AbnieShow').find('#hMinw3').val(hMinw3.toFixed(2));
                }
            }

            if (ObjectId == 'alfaw4') {
                if (parseFloat($('#AbnieShow').find('#alfaw4').val()) > 180) {
                    toastr.info('زاویه دستک نبایستی بیش از 180 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfaw4').val(180);
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hMinw4 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw4').val())) / (45)));
                    $('#AbnieShow').find('#hMinw4').val(hMinw4.toFixed(2));
                }
                else if (parseFloat($('#AbnieShow').find('#alfaw4').val()) >= 90 && parseFloat($('#AbnieShow').find('#alfaw4').val()) < 135) {
                    $('#AbnieShow').find('#hMinw4').val(1);
                }
                else if (parseFloat($('#AbnieShow').find('#alfaw4').val()) < 90) {
                    toastr.info('زاویه دستک نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfaw4').val(90);
                    $('#AbnieShow').find('#hMinw4').val(1);
                }
                else {
                    h = parseFloat($('#AbnieShow').find('#drdHadeAksarErtefa').val());
                    hMinw4 = h - (((h - 1) * (180 - parseFloat($('#AbnieShow').find('#alfaw4').val())) / (45)));
                    $('#AbnieShow').find('#hMinw4').val(hMinw4.toFixed(2));
                }
            }

            if (ObjectId == 'alfa') {
                if (parseFloat($('#AbnieShow').find('#alfa').val()) > 45) {
                    toastr.info('زاویه بیه نبایستی بیش از 45 درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfa').val(45);
                }
                else if (parseFloat($('#AbnieShow').find('#alfa').val()) < -45) {
                    toastr.info('زاویه بیه نبایستی کمتر از 45- درجه باشد', 'اطلاع');
                    $('#AbnieShow').find('#alfa').val(-45);
                }
            }

            if (ObjectId == 'LAbro' || ObjectId == 'alfa') {
                $('#AbnieShow').find('#LAbroWithBie').val(($('#AbnieShow').find('#LAbro').val() / Math.cos(toRadians($('#AbnieShow').find('#alfa').val()))).toFixed(2));
            }
            drawingPlan();
        });
        drawingPlan();
    }
    else {
        $('#AbnieShow').find('#tabAddedItemsForAbnieFaniPol').removeClass('PlanTableActiveTab');
        $('#AbnieShow').find('#divAddedItemsForAbnieFaniPol').hide();
        $('#AbnieShow').find('#svgContainerPol').show()
        $('#AbnieShow').find('#divPol').show();
        $('#AbnieShow').find('#divPlan').hide();
        $('#AbnieShow').find('#tabPlan').removeClass('PlanTableActiveTab');
        $('#AbnieShow').find('#tabPol').addClass('PlanTableActiveTab');
    }
}

function ChangeSaveButtonColor() {
    clearTimeout(timerAddPol);

    timerAddPol = setTimeout(ChangeBackColor1, 10);
    function ChangeBackColor1() {
        $('#AbnieShow').find('#btnAddEditPol').css('background-color', '#516b92');
        timerAddPol = setTimeout(ChangeBackColor2, 500);
    }
    function ChangeBackColor2() {
        $('#AbnieShow').find('#btnAddEditPol').css('background-color', 'green');
        timerAddPol = setTimeout(ChangeBackColor1, 500);
    }
}

var AbadeKoole = '';
var DastakPolInfo = null;
var HadAksarErtefaKoole = null;

function AbnieFaniWithBarAvordIdClick(OpId, BarAvordUserId) {
    NoeBanaii = $('#AbnieShow').find('#drdNoeBanaii').val();
    NahveEjraDal = $('#AbnieShow').find('#drdNahveEjraDal').val();

    str = '';
    str += '<div class=\'\' style=\'margin-top:3px;\'><div class=\'row\'><div class=\'col-md-3\'><a class=\'NewPolStyle\' onclick=\"ShowSelctionPol(1,0,0,0,0,0,0,0,0,0,0,' + NoeBanaii + ',' + NahveEjraDal + ',' + "'" + BarAvordUserId + "'" + ')\">ایجاد (آبرو/پل) جدید</a></div><div class=\'col-md-5\'><a class=\'NewPolStyle\' onclick=\"ShowExistingPol(' + "'" + BarAvordUserId + "'" + ')\">مشاهده و انتخاب (آبرو/پل) ثبت شده</a></div></div>';
    str += '<div class=\'\' style=\'margin-top: 30px;\' id=\'ViewPol\'></div>';
    //$('#ula' + OpId).html(str);
    $('#AbnieShow').find('#divShowAbnie').html(str);
    $('#aAbnieShow').click();
}

var ItemsFBDependQuestionForAbnieFani = '';
var QuesForAbnieFaniValues = '';
var RizMetreUsersEzafeBahaValue = '';
function ShowFormsAddedItemsAbnieFaniPol(BarAvordUserId) {
    $('#AbnieShow').find('#svgContainerPol').hide()
    $('#AbnieShow').find('#divPol').hide();
    $('#AbnieShow').find('#divPlan').hide();
    $('#AbnieShow').find('#tabPlan').removeClass('PlanTableActiveTab');
    $('#AbnieShow').find('#tabPol').removeClass('PlanTableActiveTab');

    $('#AbnieShow').find('#tabAddedItemsForAbnieFaniPol').addClass('PlanTableActiveTab');
    $('#AbnieShow').find('#divAddedItemsForAbnieFaniPol').show();
    PolVaAbroId = $('#HDFPolIdForEdit').val();
    PolNum = $('#HDFPolNum').val();
    debugger;

    if ($.trim(PolVaAbroId) != '') {
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.PolVaAbroId = PolVaAbroId;
        vardata.PolNum = PolNum;
        vardata.Year = $('#HDFYear').val();
        vardata.NoeFB = parseInt($('#HDFNoeFB').val());
        $.ajax({
            type: "POST",
            url: "/QuesForAbnieFani/GetQuesForAbnieFaniEzafeBaha",
            //data: '{BarAvordId:' + BarAvordId + ',PolVaAbroId:' + PolVaAbroId + ',PolNum:' + PolNum + ',year:' + 1397 + ',NoeFB:' + 234 + '}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                debugger;
                str = '';
                var xmlDoc = $.parseXML(response);
                var xml = $(xmlDoc);
                var AbnieFaniQueries = xml.find("tblAbnieFaniQueries");
                ItemsFBDependQuestionForAbnieFani = xml.find("tblItemsFBDependQuestionForAbnieFani");
                QuesForAbnieFaniValues = xml.find("tblQuesForAbnieFaniValues");
                RizMetreUsersEzafeBahaValue = xml.find("tblRizMetreUsersEzafeBahaValue");
                ShomarehFBForQuesForAbnieFani = xml.find("tblShomarehFBForQuesForAbnieFani");
                $.each(ShomarehFBForQuesForAbnieFani, function () {
                    QuestionForAbnieFaniId = $(this).find("Id").text();
                    Sharh = $(this).find("Sharh").text();
                    Shomareh = $(this).find("Shomareh").text();
                    str += '<div class=\'col-md-12\'><div class=\'col-md-4\'><span style=\'display:none\' id=\'spanShomareh' + QuestionForAbnieFaniId + '\'>' + Shomareh
                        + '</span><input onclick=\"EzafeBahaAbnieFaniPolClick(' + QuestionForAbnieFaniId + ')\" type=\"radio\" id=\'rdEBPol' + QuestionForAbnieFaniId
                        + '\' name=\"rdEBPol' + PolVaAbroId + '\" value=\"' + QuestionForAbnieFaniId + '\"><span class=\'spanCheckBoxEzafeBahaAbnieFaniPol\' id=\'spanCheckBoxEzafeBahaAbnieFaniPol' + QuestionForAbnieFaniId
                        + '\' onclick=\"spanCheckBoxEzafeBahaAbnieFaniPolClick(' + QuestionForAbnieFaniId + ')\">'
                        + Shomareh + " - " + Sharh + '</span></div></div></div>';
                    str += '<div style=\'display:none;text-align:center\' id=\'divSubEzafeBahaAbnieFaniPol' + QuestionForAbnieFaniId + '\' class=\'col-md-12\'></div>';
                });

                $('#AbnieShow').find('#divAddedItemsForAbnieFaniPol').html(str);
                $.each(AbnieFaniQueries, function () {
                    Id = $(this).find("Id").text();
                    $.each(QuesForAbnieFaniValues, function () {
                        objectId = $(this).find("Id").text();
                        QuesForAbnieFaniId = $(this).find("QuestionForAbnieFaniId").text();
                        if (Id == QuesForAbnieFaniId) {
                            $('#AbnieShow').find('#rdEBPol' + Id).prop('checked', true);
                            $('#AbnieShow').find('#spanCheckBoxEzafeBahaAbnieFaniPol' + Id).click();
                        }
                    });
                });
            },
            error: function (response) {
                toastr.error('مشکل در اضافه بها پل', 'خطا');
            }
        });
    }
    else {
        toastr.info('پل / آبرو ایجاد شده ذخیره نگردیده است', 'اطلاع');
    }
}

function spanCheckBoxEzafeBahaAbnieFaniPolClick(Id) {
    if ($('#AbnieShow').find('#rdEBPol' + Id).is(':checked'))
        ShowEzafeBahaAbnieFaniPol(Id)
    else {
        $('#AbnieShow').find('#rdEBPol' + Id).click();
        EzafeBahaAbnieFaniPolClick(Id);
    }
}

function ShowEzafeBahaAbnieFaniPol(Id) {
    str = '';
    if (!$('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).hasClass('ShowSubEzafeBahaAbnieFaniPol')) {
        str += '<div class=\'col-md-12\' style=\'padding: 2px;\'><div class=\'col-md-7\' style=\'background-color: #eaf3ff;border-radius: 5px !important;\'><div class=\'col-md-3\'><span>شماره آیتم</span></div><div class=\'col-md-3\'><span>حجم آیتم</span></div><div class=\'col-md-3\'><span>مقدار</span></div><div class=\'col-md-3\'><span>واحد</span></div></div></div>';
        strItemsForAdd = '';
        Shomareh = $('#AbnieShow').find('#spanShomareh' + Id).html();
        $.each(ItemsFBDependQuestionForAbnieFani, function () {
            objectId = $(this).find("Id").text();
            QuesForAbnieFaniId = $(this).find("QuesForAbnieFaniId").text();
            ItemShomareh = $(this).find("ItemShomareh").text();
            DefaultValue = $(this).find("DefaultValue").text();
            Vahed = $(this).find("Vahed").text();
            Hajm = $(this).find("Hajm").text();
            if (QuesForAbnieFaniId == Id && Hajm != 0) {
                Meghdar = 0;
                $.each(RizMetreUsersEzafeBahaValue, function () {
                    ForItem = $(this).find("ForItem").text();
                    Vazn = $(this).find("Vazn").text();
                    CurrentShomareh = $(this).find("ShomarehFB").text();
                    if ($.trim(ItemShomareh) == $.trim(ForItem) &&
                        $.trim(CurrentShomareh) == $.trim(Shomareh)) {
                        Meghdar = Vazn;
                    }
                });
                str += '<div class=\'col-md-12\' style=\'padding: 2px;\'><div class=\'col-md-7\'><div class=\'col-md-3\'><span id=\'spanItemShomareh' + objectId + '\'>' + ItemShomareh + '</span></div><div class=\'col-md-3\'><span id=\'spanHajm' + objectId + '\'>'
                    + Hajm + '</span></div><div class=\'col-md-3\'><input style=\'text-align: center;\' type=\'text\' class=\'form-control input-sm\' id=\'txtMeghdarAFPol' + objectId + '\' value=\''
                    + (Meghdar != 0 ? parseInt(Meghdar) : DefaultValue) + '\'/></div><div class=\'col-md-3\'><span>' + Vahed + '</span></div></div></div>';
                strItemsForAdd += ItemShomareh + '_' + Hajm + '_' + DefaultValue + ',';
            }
        });
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).html(str);


        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id + ' input[type="text"] ').change(function () {
            txtId = $(this).attr('id');
            ObjectId = txtId.substring(15, txtId.length);
            ItemShomareh = $('#spanItemShomareh' + ObjectId).html();
            Meghdar = $(this).val();
            Hajm = $('#AbnieShow').find('#spanHajm' + ObjectId).html();
            UpdateEzafeBahaAbnieFaniPol(Id, ItemShomareh, Hajm, Meghdar);
        });

        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).show();
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).addClass('ShowSubEzafeBahaAbnieFaniPol');
    }
    else {
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).hide();
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).removeClass('ShowSubEzafeBahaAbnieFaniPol');
    }
}

function EzafeBahaAbnieFaniPolClick(Id) {
    if (!$('#AbnieShow').find('#rdEBPol' + Id).is(':checked')) {
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).addClass('ShowSubEzafeBahaAbnieFaniPol');
        str = '';
        //if (!$('#divSubEzafeBahaAbnieFaniPol' + Id).hasClass('ShowSubEzafeBahaAbnieFaniPol')) {
        str += '<div class=\'col-md-12\' style=\'padding: 2px;\'><div class=\'col-md-7\' style=\'background-color: #eaf3ff;border-radius: 5px !important;\'><div class=\'col-md-3\'><span>شماره آیتم</span></div><div class=\'col-md-3\'><span>حجم آیتم</span></div><div class=\'col-md-3\'><span>مقدار</span></div><div class=\'col-md-3\'><span>واحد</span></div></div></div>';
        strItemsForAdd = '';
        $.each(ItemsFBDependQuestionForAbnieFani, function () {
            objectId = $(this).find("Id").text();
            QuesForAbnieFaniId = $(this).find("QuesForAbnieFaniId").text();
            ItemShomareh = $(this).find("ItemShomareh").text();
            DefaultValue = $(this).find("DefaultValue").text();
            Vahed = $(this).find("Vahed").text();
            Hajm = $(this).find("Hajm").text();
            if (QuesForAbnieFaniId == Id && Hajm != 0) {
                str += '<div class=\'col-md-12\' style=\'padding: 2px;\'><div class=\'col-md-7\'><div class=\'col-md-3\'><span id=\'spanItemShomareh' + objectId + '\'>' + ItemShomareh + '</span></div><div class=\'col-md-3\'><span id=\'spanHajm' + objectId + '\'>'
                    + Hajm + '</span></div><div class=\'col-md-3\'><input style=\'text-align: center;\' type=\'text\' class=\'form-control input-sm\' id=\'txtMeghdarAFPol' + objectId + '\' value=\''
                    + DefaultValue + '\'/></div><div class=\'col-md-3\'><span>' + Vahed + '</span></div></div></div>';
                strItemsForAdd += ItemShomareh + '_' + Hajm + '_' + DefaultValue + ',';
            }
        });
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).html(str);

        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id + ' input[type="text"] ').change(function () {
            txtId = $(this).attr('id');
            ObjectId = txtId.substring(15, txtId.length);
            ItemShomareh = $('#spanItemShomareh' + ObjectId).html();
            Meghdar = $(this).val();
            Hajm = $('#spanHajm' + ObjectId).html();
            UpdateEzafeBahaAbnieFaniPol(Id, ItemShomareh, Hajm, Meghdar);
        });

        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).show();

        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).addClass('ShowSubEzafeBahaAbnieFaniPol');

        //$('#divAddedItemsForAbnieFaniPol span').each(function () {
        //    if ($(this).hasClass('spanCheckBoxEzafeBahaAbnieFaniPol')) {
        //    debugger;
        //    code = $(this).attr('id').substring(33, $(this).attr('id').length);
        //    if (code != Id) {
        //        $('#spanCheckBoxEzafeBahaAbnieFaniPol' + code).click();
        //    }
        //    }
        //});

        SaveEzafeBahaForAbnieFaniPol(strItemsForAdd, Id);


        //}
        //else {
        //    $('#divSubEzafeBahaAbnieFaniPol' + Id).hide();
        //    $('#divSubEzafeBahaAbnieFaniPol' + Id).removeClass('ShowSubEzafeBahaAbnieFaniPol');
        //}
    }
    else {
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).hide();
        $('#AbnieShow').find('#divSubEzafeBahaAbnieFaniPol' + Id).removeClass('ShowSubEzafeBahaAbnieFaniPol');
        ItemFBForDel = $('#AbnieShow').find('#spanShomareh' + Id).html();
        DeleteEzafeBahaForAbnieFaniPolForItem(ItemFBForDel, Id);
    }
}

function ShowExistingPol(BarAvordUserId) {
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    $.ajax({
        type: "POST",
        url: "/PolVaAbroBarAvord/GetExistingPolInfoWithBarAvordId",
        //data: '{BarAvordId:' + BarAvordId + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            var PolVaAbroBarAvord = xml.find("tblPolVaAbroBarAvord");
            DastakPolInfo = xml.find("tblDastakPolInfo");
            count = 1;
            str = '';
            $.each(PolVaAbroBarAvord, function () {
                debugger;
                PolExistingId = $(this).find("ID").text();
                PolNum = $(this).find("PolNum").text();
                TedadDahaneh = $(this).find("TedadDahaneh").text();
                DahaneAbro = $(this).find("DahaneAbro").text();
                HadAksarErtefaKoole = $(this).find("HadAksarErtefaKoole").text();
                Hs = $(this).find("Hs").text() == "" ? 0 : $(this).find("Hs").text();
                ZavieBie = $(this).find("ZavieBie").text();
                ToolAbro = $(this).find("ToolAbro").text();
                x = $(this).find("X").text();
                y = $(this).find("Y").text();
                NoeBanaii = $(this).find("NoeBanaii").text();
                NahveEjraDal = $(this).find("NahveEjraDal").text();

                TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);
                strNoeName = TedadDarDahaneAbro > 6 ? ' پل ' : ' آبرو ';

                str += `
  <div class="col-md-12" style="margin:1px 0px;">
    <a class="ExsitingPolStyle"
       onclick="SelctionPol($(this), '${PolExistingId}', '${PolNum}')"
       ondblclick="ShowSelctionPol(
         0, '${PolExistingId}', '${PolNum}', ${TedadDahaneh}, ${DahaneAbro},
         '${HadAksarErtefaKoole}', ${Hs}, ${ZavieBie}, ${ToolAbro},
         ${x}, ${y}, ${NoeBanaii}, ${NahveEjraDal}, '${BarAvordUserId}'
       )">
       ${count++} - ${strNoeName}${TedadDahaneh} دهانه، ${DahaneAbro} متری، موقعیت: X: ${parseInt(x)} ، Y: ${parseInt(y)}
    </a>
  </div>
`;




                //str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionPol($(this),\'' + PolExistingId + "\'',\' + PolNum + '\')\" ondblclick=\"ShowSelctionPol(0,' +
                //    PolExistingId + ',' + PolNum + ',' + TedadDahaneh + ',' + DahaneAbro + ',\'' + HadAksarErtefaKoole + '\',' + Hs + ',' +
                //    ZavieBie + ',' + ToolAbro + ',' + x + ',' + y + ',' + NoeBanaii + ',' + NahveEjraDal + ',' + BarAvordId + ')\">' + count++ + ' - ' + strNoeName + TedadDahaneh
                //    + ' دهانه، ' + DahaneAbro + ' متری، موقعیت: X: ' + parseInt(x) + ' ، Y: ' + parseInt(y) + '</a></div>';
            });
            $('#ExistAbnieShow').find('#divViewExistingPol').html(str);
            $('#aExistAbnieShow').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری پل های موجود', 'خطا');
        }
    });
}

function SelctionPol() {
}

function fillTedadDahaneh(GetTedadDahaneh, GetDahaneAbro, GetHadAksarErtefaKoole, GetErtefaKhakriz) {
    TedadDahanehList = '';
    count = 0;
    TedadDahanehFirst = 0;
    str = '';
    $.each(HadAksarErtefaKoole, function () {
        blnCheck = false;
        TedadDahanehListSplit = TedadDahanehList.split(',');
        TedadDahaneh = $(this).find("TedadDahaneh").text();
        for (var i = 0; i < TedadDahanehListSplit.length; i++) {
            if (TedadDahanehListSplit[i] == TedadDahaneh)
                blnCheck = true;
        }
        if (!blnCheck) {
            count++;
            if (count == 1) {
                TedadDahanehFirst = TedadDahaneh;
            }
            TedadDahanehList += TedadDahaneh + ',';
            str += "<option value=\"" + TedadDahaneh + "\">" + TedadDahaneh + "</option>";

        }
    });

    $('#AbnieShow').find('#drdTedadDahaneh').html(str);
    $('#AbnieShow').find('#drdTedadDahaneh').each(function () {
        $('option', this).each(function () {
            if ($(this).text() == GetTedadDahaneh) {
                $('#AbnieShow').find('#drdTedadDahaneh').val(GetTedadDahaneh);
            };
        });
    });
    fillDahaneAbro(GetDahaneAbro, GetHadAksarErtefaKoole, GetErtefaKhakriz);
}

function fillDahaneAbro(GetDahaneAbro, GetHadAksarErtefaKoole, GetErtefaKhakriz) {
    count = 0;
    DahaneAbroList = '';
    str = '';
    TedadDahanehFirst = $('#AbnieShow').find('#drdTedadDahaneh').val();
    $.each(HadAksarErtefaKoole, function () {
        blnCheck = false;
        DahaneAbroListSplit = DahaneAbroList.split(',');
        DahaneAbro = $(this).find("DahaneAbro").text();
        TedadDahaneh = $(this).find("TedadDahaneh").text();
        for (var i = 0; i < DahaneAbroListSplit.length; i++) {
            if (DahaneAbroListSplit[i] == DahaneAbro)
                blnCheck = true;
        }
        if (!blnCheck && TedadDahaneh == TedadDahanehFirst) {
            count++;
            if (count == 1) {
                DahaneAbroFirst = DahaneAbro;
            }
            DahaneAbroList += DahaneAbro + ',';
            str += "<option value=\"" + DahaneAbro + "\">" + DahaneAbro + "</option>";
        }

    });
    $('#AbnieShow').find('#drdDahaneAbro').html(str);
    $('#AbnieShow').find('#drdDahaneAbro').each(function () {
        $('option', this).each(function () {
            if ($(this).text() == GetDahaneAbro) {
                $('#AbnieShow').find('#drdDahaneAbro').val(GetDahaneAbro);
            };
        });
    });

    TedadDahaneh = $('#AbnieShow').find('#drdTedadDahaneh').val();
    DahaneAbro = $('#AbnieShow').find('#drdDahaneAbro').val();
    TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);

    strNoeBanaii = '';
    if (TedadDarDahaneAbro > 6)
        strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option>';
    else {
        strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
    }
    $('#AbnieShow').find('#drdNoeBanaii').html(strNoeBanaii);

    strNahveEjraDal = '';
    if (DahaneAbro > 3)
        strNahveEjraDal = '<option value=\'1\'>درجا</option>';
    else {
        strNahveEjraDal = '<option value=\'1\'>درجا</option><option value=\'2\'>پیش ساخته</option>';
    }
    $('#AbnieShow').find('#drdNahveEjraDal').html(strNahveEjraDal);

    fillHadeAksarErtefa(GetHadAksarErtefaKoole, GetErtefaKhakriz)
}

function fillHadeAksarErtefa(GetHadAksarErtefaKoole, GetErtefaKhakriz) {
    HadeAksarErtefaList = '';
    DahaneAbroFirst = $('#AbnieShow').find('#drdDahaneAbro').val();
    str = '';
    $.each(HadAksarErtefaKoole, function () {
        blnCheck = false;
        HadeAksarErtefaListSplit = HadeAksarErtefaList.split(',');
        HadeAksarErtefa = $(this).find("HadAksarErtefaKoole").text();
        DahaneAbro = $(this).find("DahaneAbro").text();
        for (var i = 0; i < HadeAksarErtefaListSplit.length; i++) {
            if (HadeAksarErtefaListSplit[i] == HadeAksarErtefa)
                blnCheck = true;
        }
        if (!blnCheck && DahaneAbro == DahaneAbroFirst) {
            HadeAksarErtefaList += HadeAksarErtefa + ',';
            str += "<option value=\"" + HadeAksarErtefa + "\">" + HadeAksarErtefa + "</option>";
        }
    });
    $('#AbnieShow').find('#drdHadeAksarErtefa').html(str);
    $('#AbnieShow').find('#drdHadeAksarErtefa').each(function () {
        $('option', this).each(function () {
            if ($(this).text() == GetHadAksarErtefaKoole) {
                $('#AbnieShow').find('#drdHadeAksarErtefa').val(GetHadAksarErtefaKoole);
            };
        });
    });
    fnGetErtefaKhakriz(GetErtefaKhakriz);
}
function fnGetErtefaKhakriz(GetErtefaKhakriz) {
    TedadDahanehCurrent = $('#AbnieShow').find('#drdTedadDahaneh').val();
    DahaneAbroCurrent = $('#AbnieShow').find('#drdDahaneAbro').val();
    HadeAksarErtefa = $('#AbnieShow').find('#drdHadeAksarErtefa').val();
    Meyar = 1;
    str = '';
    var vardata = new Object();
    vardata.TedadDahaneh = TedadDahanehCurrent;
    vardata.DahaneAbro = DahaneAbroCurrent;
    vardata.HadAksarErtefaKoole = HadeAksarErtefa;
    $.ajax({
        type: "POST",
        url: "/HadAksarErtefaKoole/GetAbadKooleInfo",
        data: JSON.stringify(vardata),
        //data: '{TedadDahaneh:' + TedadDahanehCurrent + ',DahaneAbro:' + "'" + DahaneAbroCurrent + "'" + ',HadAksarErtefaKoole:' + "'" + HadeAksarErtefa + "'" + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            D = $('#AbnieShow').find('#drdDahaneAbro').val();
            H = $('#AbnieShow').find('#drdHadeAksarErtefa').val();
            Hs = ''; a1 = 0; a2 = 0; b1 = 0; b2 = 0;
            c1 = 0; c2 = 0; f = 0; m = 0; p1 = 0; p2 = 0; e = 0; n = 0; k = 0; t = 0; HadAghalZavieEstekak = 0; j = 0;
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            AbadeKoole = xml.find("AbadeKoole");
            Counter = 0;
            $.each(AbadeKoole, function () {
                Id = $(this).find("Id").text();
                Hs = $(this).find("Hs").text().toString().replace(/\>/g, '').replace(/\</g, '');
                str += '<option value=\'' + Id + '\'>' + Hs + '</option>';
                if (Counter == 0) {
                    a1 = $(this).find("a1").text();
                    a2 = $(this).find("a2").text();
                    b1 = $(this).find("b1").text();
                    b2 = $(this).find("b2").text();
                    c1 = $(this).find("c1").text();
                    c2 = $(this).find("c2").text();
                    f = $(this).find("f").text();
                    m = $(this).find("m").text();
                    p1 = $(this).find("p1").text();
                    p2 = $(this).find("p2").text();
                    e = $(this).find("e").text();
                    n = $(this).find("n").text();
                    k = $(this).find("k").text();
                    t = $(this).find("t").text();
                    j = $(this).find("DerzEnbesat").text();
                }
                Counter++;
            });
            $('#AbnieShow').find('#drdErtefaKhakriz').html(str);
            $('#AbnieShow').find('#drdErtefaKhakriz').each(function () {
                $('option', this).each(function () {
                    if ($(this).val() == GetErtefaKhakriz) {
                        $('#AbnieShow').find('#drdErtefaKhakriz').val(GetErtefaKhakriz);
                    };
                });
            });

            ///////////
            scale = 1;
            StartPointX = 0;
            StartPointY = 280;
            if (D >= 3) {
                scale = 1 / 2;
            }
            if (D >= 4 && D < 7) {
                StartPointX = 0;
                StartPointY = 320;
            }
            else if (D >= 7 && D < 9) {
                StartPointX = 0;
                StartPointY = 350;
            }
            else if (D >= 9) {
                StartPointX = 0;
                StartPointY = 370;
            }
            //else if (D >= 8)
            //    scale = 1 / 4;

            $('#AbnieShow').find('#svgLines').html('');
            if (TedadDahanehCurrent == 1) {
                Pol(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100)
                    , parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100)
                    , parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar);

                //t1 = 0.14 * 1.21;
                //t2 = 0.17;
                //t3 = t - 0.10;
                //t4 = 0.15;//<14 0.15 Or >14 0.17
                //t1t4 = t4 - t1;
                //Gap = 0.04;
                //ht = H + t;
                //DrawPos1(parseInt(D * 100), parseInt(t1 * 100), parseInt(t2 * 100), parseInt(t3 * 100), parseInt(c1 * 100), parseInt(j * 100), StartPointX + (c2 * scale * 100) + (j * scale * 100) + (Gap * scale * 100), StartPointY - (H * scale * 100) + (t * scale * 100) - (Gap * scale * 100), scale, Meyar);
                //DrawPos2(parseInt(D * 100), parseInt(t1 * 100), parseInt(t2 * 100), parseInt(t3 * 100), parseInt(c1 * 100), parseInt(j * 100), StartPointX + (c2 * scale * 100) + (j * scale * 100) + (Gap * scale * 100), StartPointY + (t * scale * 100) - (Gap * scale * 100) - ((t1 + t1t3) * scale * 100), scale, Meyar);
                //DrawPos4(parseInt(D * 100), parseInt(t4 * 100), parseInt(c1 * 100), parseInt(j * 100), StartPointX + (c2 * scale * 100) + (j * scale * 100) + (Gap * scale * 100), StartPointY + (t * scale * 100) - (Gap * scale * 100) + (t1t4 * scale * 100), scale, Meyar);
            }
            else if (TedadDahanehCurrent == 2) {
                Pol2Dahaneh(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100), parseInt(b2 * 100),
                    parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100), parseInt(p1 * 100), parseInt(p2 * 100)
                    , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar);
            }
            else if (TedadDahanehCurrent == 3) {
                if (D >= 2 && D < 4) {
                    scale = 1 / 2;
                }
                else if (D >= 4) {
                    scale = 1 / 4;
                }
                Pol3Dahaneh(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100), parseInt(b2 * 100),
                    parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100), parseInt(p1 * 100), parseInt(p2 * 100)
                    , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar);
            }
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری حداکثر ارتفاع خاکریز', 'خطا');
        }
    });
}
function ShowSelctionPol(IsNew, PolExistingId, PolNum, GetTedadDahaneh, GetDahaneAbro, GetHadAksarErtefaKoole, GetHs
    , GetZavieBie, GetToolAbro, x, y, NoeBanaii, NahveEjraDal, BarAvordUserId) {
    $('#btnCloseFormExistAbnie').click();
    objBarAvordUserId = JSON.stringify(BarAvordUserId);
    $.ajax({
        type: "POST",
        url: "/HadAksarErtefaKoole/GetPolInfo",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //var xmlDoc = $.parseXML(response);
            //var xml = $(xmlDoc);
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            HadAksarErtefaKoole = xml.find("HadAksarErtefaKoole");
            str = '<div class=\'\' style=\'margin-left:0px;margin-right:0px\'><div class=\'row\'><div class=\'col-md-2\'><input type=\'button\' style=\'border: 0px;color: #fff;border-radius:2px 2px 0px 0px !important;\' class=\'spanStyleMitraSmall spanFrameNameStyle PlanTableActiveTab\' id=\'tabPol\' onclick=\'TabClick(\"Pol\")\' value=\'مشخصات پل\'/></div><div class=\'col-md-2\'><input id=\'tabPlan\' onclick=\'TabClick(\"Plan\")\' type=\'button\' style=\'border: 0px;color: #fff;border-radius:2px 2px 0px 0px !important;\' class=\'spanStyleMitraSmall spanFrameNameStyle\' value=\'پلان\'/></div>' +
                '<div class=\'col-md-2\'><input id=\'tabAddedItemsForAbnieFaniPol\' onclick=\'ShowFormsAddedItemsAbnieFaniPol(' + objBarAvordUserId + ')\' type=\'button\' style=\'border: 0px;color: #fff;border-radius:2px 2px 0px 0px !important;\' class=\'spanStyleMitraSmall spanFrameNameStyle\' value=\'اضافه بها\'/></div>' + '</div></div>';
            str += '<div id=\'divPol\' style=\'padding-top: 10px;padding-bottom: 10px;border: 1px solid rgb(129, 173, 201);margin-left: 0px;margin-right: 0px;border-bottom: 0px;border-bottom: 1px solid rgb(129, 173, 201);\' class=\"row\">';
            str += '<div class=\"col-md-7 row\">';
            str += '<div class=\"col-md-2\" style=\"text-align:left\"><span class=\"spanStyleMitraSmall\">تعداد دهانه : </span></div><div class=\"col-md-2\"><select class=\"form-control spanStyleMitraSmall\" id=\'drdTedadDahaneh\'>';
            str += '</select></div>';

            str += '<div class=\"col-md-2\" style=\"text-align:left\"><span class=\"spanStyleMitraSmall\">دهانه آبرو : </span></div><div class=\"col-md-2\"><select class=\"form-control spanStyleMitraSmall\" id=\'drdDahaneAbro\'>';
            str += '</select></div>';

            str += '<div class=\"col-md-2\" style=\"text-align:left;padding-left: 0px;padding-right: 0px;\"><span class=\"spanStyleMitraSmall\">حداکثر ارتفاع کوله: </span></div><div class=\"col-md-2\"><select class=\"form-control spanStyleMitraSmall\" id=\'drdHadeAksarErtefa\'>';
            str += '</select></div>';
            str += '</div>';

            str += '<div class=\"col-md-5 row\">';
            str += '<div class=\"col-md-4\" style=\"text-align:left;padding-left: 0px;padding-right: 0px;\"><span class=\"spanStyleMitraSmall\">ارتفاع خاکریز روی آبرو : </span></div><div class=\"col-md-4\"><select class=\"form-control spanStyleMitraSmall\" id=\'drdErtefaKhakriz\'>';
            str += '</select></div></div>';

            str += '<div class=\"col-md-7 row\"><div class=\"col-md-2\" style=\"text-align:left\"><span class=\"spanStyleMitraSmall\">نوع بنایی : </span></div><div class=\"col-md-2\"><select style=\'padding: 0px 2px;\' class=\"form-control spanStyleMitraSmall\" id=\'drdNoeBanaii\'>';
            str += '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
            str += '</select></div>';
            str += '<div class=\"col-md-2\" style=\"text-align:left\"><span class=\"spanStyleMitraSmall\">نحوه اجرای دال : </span></div><div class=\"col-md-2\"><select class=\"form-control spanStyleMitraSmall\" id=\'drdNahveEjraDal\'>';
            str += '<option value=\'1\'>درجا</option><option value=\'2\'>پیش ساخته</option>';
            str += '</select></div>';
            str += '</div>';

            str += '</div>';

            str += '<div id=\'divPlan\' style=\'padding-top: 10px;border: 1px solid rgb(129, 173, 201);margin-left: 0px;margin-right: 0px;text-align:center;display:none\' class=\"row\">';
            str += '<div class=\'col-md-4\' style=\'padding-left: 0px;padding-right: 0px;\'>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 row PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>شماره</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-3 PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>1</span></div><div class=\'col-md-3 PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>2</span></div><div class=\'col-md-3 PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>3</span></div><div class=\'col-md-3 PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>4</span></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 row PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>طول دستک (w)</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input type=\'text\' id=\'w1\' value=\'4\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'w2\' value=\'4\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'w3\' value=\'4\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'w4\' value=\'4\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 row PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>زاویه دستک</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'alfaw1\' value=\'135\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'alfaw2\' value=\'135\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'alfaw3\' value=\'135\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'alfaw4\' value=\'135\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 row PlanTableStyle\' style=\'background-color:#f7f0f0;padding-left: 0px;padding-right: 0px;\'><span>حداقل ارتفاع دستک h(min)</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'hMinw1\' value=\'1\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'hMinw2\' value=\'1\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'hMinw3\' value=\'1\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'hMinw4\' value=\'1\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 row PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>طول آبرو</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'LAbro\' value=\'8\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 row PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>زاویه بیه پل</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'alfa\' value=\'0\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 row PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>طول آبرو با احتساب زاویه بیه</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-3 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'LAbroWithBie\' disabled=\'disabled\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;margin-top: 20px\'><div class=\'col-md-8 row PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>موقعیت آبرو براساس UTM</span></div><div class=\'col-md-6 row\' style=\'padding-left: 0px; padding-right: 0px;\'></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>X</span></div><div class=\'col-md-6\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-9 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'SAbroX\' value=\'0\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;\'><div class=\'col-md-6 PlanTableStyle\' style=\'background-color:#f7f0f0;\'><span>Y</span></div><div class=\'col-md-6\' style=\'padding-left: 0px; padding-right: 0px;\'><div class=\'col-md-9 PlanTableStyle\' style=\'padding-left: 0px;padding-right: 0px;\'><input id=\'SAbroY\' value=\'0\' type=\'text\' class=\"form-control spanStyleMitraSmall PlanTableTextBox\"/></div></div></div>';
            str += '<div class=\'row\' style=\'margin-left: 0px;margin-right: 0px;margin-top:20px\'><div class=\'col-md-2\'></div><div class=\'col-md-8\'><input id=\'btnAddEditPol\' type=\'button\' value=\'ثبت نهایی پل\' onclick=\'PolInfoInsert()\' class=\'btnSavePlanStyle\'/></div></div>';
            str += '</div>';
            //str += '<div class=\'col-md-8\'><canvas id=\'myCanvas\' height="500" width="600" style=\'background-color:#bde0d1;\'></canvas></div>';
            str += '<div class=\'col-md-8\' id=\"svgContainerPlan\" style=\'border:1px solid rgb(129, 173, 201);margin-right: 0px;max-height:350px;overflow:scroll\'>';
            str += '<svg id=\"svgLinesPlan\" style=\"background-color:#fff;\" height=\"600\" width=\"600\">';
            str += '</svg>';
            str += '</div>';
            str += '</div>';
            $('#AbnieShow').find('#ViewPol').html(str);
            $('#AbnieShow').find('#ViewEzafeBahaPol').css('display', 'block');
            $('#AbnieShow').find('#drdTedadDahaneh').change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();

                str = '';
                DahaneAbroList = '';
                TedadDahanehCurrent = $(this).val();
                $.each(HadAksarErtefaKoole, function () {
                    blnCheck = false;
                    DahaneAbroListSplit = DahaneAbroList.split(',');
                    DahaneAbro = $(this).find("DahaneAbro").text();
                    TedadDahaneh = $(this).find("TedadDahaneh").text();
                    for (var i = 0; i < DahaneAbroListSplit.length; i++) {
                        if (DahaneAbroListSplit[i] == DahaneAbro)
                            blnCheck = true;
                    }
                    if (!blnCheck && TedadDahaneh == TedadDahanehCurrent) {
                        DahaneAbroList += DahaneAbro + ',';
                        str += "<option value=\"" + DahaneAbro + "\">" + DahaneAbro + "</option>";
                    }
                });
                $('#AbnieShow').find('#drdDahaneAbro').html(str);
                $('#AbnieShow').find('#drdDahaneAbro').change();
                ///////////
                TedadDahaneh = $('#AbnieShow').find('#drdTedadDahaneh').val();
                DahaneAbro = $('#AbnieShow').find('#drdDahaneAbro').val();
                TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);
                strNoeBanaii = '';
                if (TedadDarDahaneAbro > 6)
                    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option>';
                else {
                    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
                }
                $('#AbnieShow').find('#drdNoeBanaii').html(strNoeBanaii);
                //////////////
                strNahveEjraDal = '';
                if (DahaneAbro > 3)
                    strNahveEjraDal = '<option value=\'1\'>درجا</option>';
                else {
                    strNahveEjraDal = '<option value=\'1\'>درجا</option><option value=\'2\'>پیش ساخته</option>';
                }
                $('#AbnieShow').find('#drdNahveEjraDal').html(strNahveEjraDal);
                ///////////
                //fnGetErtefaKhakriz();
            });

            $('#AbnieShow').find('#drdDahaneAbro').change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();

                str = '';
                HadeAksarErtefaList = '';
                DahaneAbroCurrent = $(this).val();
                TedadDahanehCurrent = $('#AbnieShow').find('#drdTedadDahaneh').val();
                $.each(HadAksarErtefaKoole, function () {
                    blnCheck = false;
                    HadeAksarErtefaListSplit = HadeAksarErtefaList.split(',');
                    HadeAksarErtefa = $(this).find("HadAksarErtefaKoole").text();
                    TedadDahaneh = $(this).find("TedadDahaneh").text();
                    DahaneAbro = $(this).find("DahaneAbro").text();
                    for (var i = 0; i < HadeAksarErtefaListSplit.length; i++) {
                        if (HadeAksarErtefaListSplit[i] == HadeAksarErtefa)
                            blnCheck = true;
                    }
                    if (!blnCheck && DahaneAbro == DahaneAbroCurrent && TedadDahaneh == TedadDahanehCurrent) {
                        HadeAksarErtefaList += HadeAksarErtefa + ',';
                        str += "<option value=\"" + HadeAksarErtefa + "\">" + HadeAksarErtefa + "</option>";
                    }
                });
                $('#AbnieShow').find('#drdHadeAksarErtefa').html(str);
                ///////////
                TedadDahaneh = $('#AbnieShow').find('#drdTedadDahaneh').val();
                DahaneAbro = $('#AbnieShow').find('#drdDahaneAbro').val();
                TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);
                strNoeBanaii = '';
                if (TedadDarDahaneAbro > 6)
                    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option>';
                else {
                    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
                }
                $('#AbnieShow').find('#drdNoeBanaii').html(strNoeBanaii);
                //////////////
                strNahveEjraDal = '';
                if (DahaneAbro > 3)
                    strNahveEjraDal = '<option value=\'1\'>درجا</option>';
                else {
                    strNahveEjraDal = '<option value=\'1\'>درجا</option><option value=\'2\'>پیش ساخته</option>';
                }
                $('#AbnieShow').find('#drdNahveEjraDal').html(strNahveEjraDal);
                //////////
                fnGetErtefaKhakriz();
            });

            $('#AbnieShow').find('#drdHadeAksarErtefa').change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();
                fnGetErtefaKhakriz();
            });

            $('#AbnieShow').find('#drdErtefaKhakriz').change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();

                ErtefaKhakrizChange();
            });

            $('#AbnieShow').find('#drdNoeBanaii').change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();
            });

            $('#AbnieShow').find('#drdNahveEjraDal').change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();

                if ($(this).val() == 2) {
                    $('#AbnieShow').find('#alfa').val(0);
                    $('#AbnieShow').find('#alfa').attr('disabled', true);
                }
                else
                    $('#AbnieShow').find('#alfa').attr('disabled', false);
            });

            strsvg = '';
            strsvg += "<div id=\"svgContainerPol\" style=\'border:1px solid rgb(129, 173, 201);border-top: 0px;margin-right: 0px;max-height:350px;overflow:auto\'>";
            strsvg += "<svg id=\"svgLines\" style=\"background-color:#fff;margin-top:20px;margin-right:20px;\" height=\"500\" width=\"900\">";
            strsvg += "</svg></div>";
            $('#AbnieShow').find('#ViewPol').append(strsvg);

            strEzafeBaha = '<div id=\'divAddedItemsForAbnieFaniPol\' class=\'col-md-12\' style=\'padding: 25px 5px 2px; margin-bottom: 5px; border: 1px solid rgb(196, 212, 219); border-radius: 4px !important;padding-bottom: 50px;margin-bottom: 20px; text-align: right; display: none;\'></div>';
            $('#AbnieShow').find('#ViewPol').append(strEzafeBaha);

            if (IsNew == 0) {
                fillTedadDahaneh(GetTedadDahaneh, GetDahaneAbro, GetHadAksarErtefaKoole, GetHs);
                $('#HDFPolIdForEdit').val(PolExistingId);
                $('#HDFPolNum').val(PolNum);
                $('#HDFStatePolSaveOrEdit').val('Edit');
                $('#AbnieShow').find('#alfa').val(GetZavieBie);
                $('#AbnieShow').find('#LAbro').val(GetToolAbro);
                $('#AbnieShow').find('#SAbroX').val(x);
                $('#AbnieShow').find('#SAbroY').val(y);
                $('#AbnieShow').find('#drdNoeBanaii').val(NoeBanaii);
                $('#AbnieShow').find('#drdNahveEjraDal').val(NahveEjraDal);
                $.each(DastakPolInfo, function () {
                    PolVaAbroId = $(this).find("PolVaAbroId").text();
                    if (PolVaAbroId == PolExistingId) {
                        Shomareh = $(this).find("Shomareh").text();
                        ToolW = $(this).find("ToolW").text();
                        Zavie = $(this).find("Zavie").text();
                        hMin = $(this).find("hMin").text();
                        if (Shomareh == 1) {
                            $('#AbnieShow').find('#w1').val(ToolW);
                            $('#AbnieShow').find('#alfaw1').val(Zavie);
                            $('#AbnieShow').find('#hMinw1').val(hMin);
                        }
                        else if (Shomareh == 2) {
                            $('#AbnieShow').find('#w2').val(ToolW);
                            $('#AbnieShow').find('#alfaw2').val(Zavie);
                            $('#AbnieShow').find('#hMinw2').val(hMin);
                        }
                        else if (Shomareh == 3) {
                            $('#AbnieShow').find('#w3').val(ToolW);
                            $('#AbnieShow').find('#alfaw3').val(Zavie);
                            $('#AbnieShow').find('#hMinw3').val(hMin);
                        }
                        else if (Shomareh == 4) {
                            $('#AbnieShow').find('#w4').val(ToolW);
                            $('#AbnieShow').find('#alfaw4').val(Zavie);
                            $('#AbnieShow').find('#hMinw4').val(hMin);
                        }
                    }
                });
                $('#AbnieShow').find('#btnCloseExistingPol').click();
            }
            else {
                $('#HDFPolIdForEdit').val('');
                $('#HDFPolNum').val('');
                $('#HDFStatePolSaveOrEdit').val('Add');
                fillTedadDahaneh(0, 0, 0, 0);
            }

            //$('#ula' + OpId).append(strsvg);
            //fnGetErtefaKhakriz();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری مشخصات کوله', 'خطا');
        }
    });
}
function ErtefaKhakrizChange() {
    ErtefaKhakrizCurrent = $('#AbnieShow').find('#drdErtefaKhakriz').val();
    TedadDahane = $('#AbnieShow').find('#drdTedadDahaneh').val();
    D = $('#AbnieShow').find('#drdDahaneAbro').val();
    H = $('#AbnieShow').find('#drdHadeAksarErtefa').val();
    Hs = ''; a1 = 0; a2 = 0; b1 = 0; b2 = 0;
    c1 = 0; c2 = 0; f = 0; m = 0; p1 = 0; p2 = 0; e = 0; n = 0; k = 0; t = 0; HadAghalZavieEstekak = 0; j = 0;
    $.each(AbadeKoole, function () {
        Id = $(this).find("Id").text();
        if (Id == ErtefaKhakrizCurrent) {
            Hs = $(this).find("Hs").text();
            a1 = $(this).find("a1").text();
            a2 = $(this).find("a2").text();
            b1 = $(this).find("b1").text();
            b2 = $(this).find("b2").text();
            c1 = $(this).find("c1").text();
            c2 = $(this).find("c2").text();
            f = $(this).find("f").text();
            m = $(this).find("m").text();
            p1 = $(this).find("p1").text();
            p2 = $(this).find("p2").text();
            e = $(this).find("e").text();
            n = $(this).find("n").text();
            k = $(this).find("k").text();
            t = $(this).find("t").text();
            //HadAghalZavieEstekak = $(this).find("HadAghalZavieEstekak").text();
            j = $(this).find("DerzEnbesat").text();
        }
    });
    Meyar = 1;
    scale = 1;
    StartPointX = 0;
    StartPointY = 280;
    if (D >= 3) {
        scale = 1 / 2;
    }
    if (D >= 4 && D < 7) {
        StartPointX = 0;
        StartPointY = 320;
    }
    else if (D >= 7 && D < 9) {
        StartPointX = 0;
        StartPointY = 350;
    }
    else if (D >= 9) {
        StartPointX = 0;
        StartPointY = 370;
    }
    $('#AbnieShow').find('#svgLines').html('');

    if (TedadDahane == 1) {
        Pol(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100)
            , parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100)
            , parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar);
    }
    else if (TedadDahane == 2) {
        Pol2Dahaneh(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100), parseInt(b2 * 100),
            parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100), parseInt(p1 * 100), parseInt(p2 * 100)
            , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar);
    }
    else if (TedadDahane == 3) {
        if (D >= 2 && D < 4) {
            scale = 1 / 2;
        }
        else if (D >= 4) {
            scale = 1 / 2;
        }
        Pol3Dahaneh(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100), parseInt(b2 * 100),
            parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100), parseInt(p1 * 100), parseInt(p2 * 100)
            , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar);
    }
}
function drawingPlan() {
    $('#AbnieShow').find('#svgLinesPlan').html('');
    ErtefaKhakrizCurrent = $('#drdErtefaKhakriz').val();
    TedadDahane = $('#drdTedadDahaneh').val();
    D = $('#AbnieShow').find('#drdDahaneAbro').val();
    H = $('#AbnieShow').find('#drdHadeAksarErtefa').val();
    Hs = ''; a1 = 0; a2 = 0; b1 = 0; b2 = 0;
    c1 = 0; c2 = 0; f = 0; m = 0; p1 = 0; p2 = 0; e = 0; n = 0; k = 0; t = 0; HadAghalZavieEstekak = 0; j = 0;
    $.each(AbadeKoole, function () {
        Id = $(this).find("Id").text();
        if (Id == ErtefaKhakrizCurrent) {
            Hs = $(this).find("Hs").text();
            a1 = $(this).find("a1").text();
            a2 = $(this).find("a2").text();
            b1 = $(this).find("b1").text();
            $('#HDFPolb1').val(b1);
            b2 = $(this).find("b2").text();
            $('#HDFPolb2').val(b2);
            c1 = $(this).find("c1").text();
            $('#HDFPolc1').val(c1);
            c2 = $(this).find("c2").text();
            $('#HDFPolc2').val(c2);
            f = $(this).find("f").text();
            $('#HDFPolf').val(f);
            m = $(this).find("m").text();
            $('#HDFPolm').val(m);
            p1 = $(this).find("p1").text();
            $('#HDFPolp1').val(p1);
            p2 = $(this).find("p2").text();
            $('#HDFPolp2').val(p2);
            e = $(this).find("e").text();
            n = $(this).find("n").text();
            $('#HDFPoln').val(n);
            k = $(this).find("k").text();
            $('#HDFPolk').val(k);
            t = $(this).find("t").text();
            $('#HDFPolt').val(t);
            j = $(this).find("DerzEnbesat").text();
            $('#HDFPolj').val(j);
        }
    });
    L = $('#AbnieShow').find('#LAbro').val();
    $('#AbnieShow').find('#LAbroWithBie').val(($('#AbnieShow').find('#LAbro').val() / Math.cos(toRadians($('#AbnieShow').find('#alfa').val()))).toFixed(2));
    LWithBie = $('#LAbroWithBie').val();
    //j = 0.02;
    w1 = $('#AbnieShow').find('#w1').val();
    w2 = $('#AbnieShow').find('#w2').val();
    w3 = $('#AbnieShow').find('#w3').val();
    w4 = $('#AbnieShow').find('#w4').val();
    alfa = $('#AbnieShow').find('#alfa').val();
    alfaw1 = $('#AbnieShow').find('#alfaw1').val();
    alfaw2 = $('#AbnieShow').find('#alfaw2').val();
    alfaw3 = $('#AbnieShow').find('#alfaw3').val();
    alfaw4 = $('#AbnieShow').find('#alfaw4').val();

    hminw1 = $('#AbnieShow').find('#hMinw1').val();
    hminw2 = $('#AbnieShow').find('#hMinw2').val();
    hminw3 = $('#AbnieShow').find('#hMinw3').val();
    hminw4 = $('#AbnieShow').find('#hMinw4').val();

    x = 0.15;
    h = H;

    Canvaswidth = $('#AbnieShow').find('#svgLinesPlan').attr('width');
    Canvasheight = $('#AbnieShow').find('#svgLinesPlan').attr('height');
    StartPointX = Canvaswidth / 2;
    StartPointY = Canvasheight / 2;

    if (h <= 2) Scale = 1 / 3;
    else if (h > 2 && h <= 4.5) Scale = 1 / 6
    else if (h > 4.5 && h <= 6) Scale = 1 / 8

    Meyar = 100;///m=1 or cm=100
    ///////////////////////////
    if (TedadDahane == 1)
        DrawingPlan1Dahaneh(alfa, D, LWithBie, w1, w2, w3, w4, b2, c1, c2, j,
            alfaw1, alfaw2, alfaw3, alfaw4, StartPointX, StartPointY, Scale, Meyar);
    else if (TedadDahane == 2)
        DrawingPlan2Dahaneh(alfa, D, LWithBie, w1, w2, w3, w4, b2, c1, c2, j,
            alfaw1, alfaw2, alfaw3, alfaw4, p2, StartPointX, StartPointY, Scale, Meyar);
    else if (TedadDahane == 3)
        DrawingPlan3Dahaneh(alfa, D, LWithBie, w1, w2, w3, w4, b2, c1, c2, j,
            alfaw1, alfaw2, alfaw3, alfaw4, p2, StartPointX, StartPointY, Scale, Meyar);
}

function DrawingPlan1Dahaneh(alfa, D, LWithBie, w1, w2, w3, w4, b2, c1, c2, j,
    alfaw1, alfaw2, alfaw3, alfaw4, StartPointX, StartPointY, Scale, Meya) {
    DrawPlan(alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100), parseInt(w2 * 100),
        parseInt(w3 * 100), parseInt(w4 * 100), parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100),
        parseInt(j * 100), alfaw1, alfaw2, alfaw3, alfaw4, StartPointX, StartPointY, Scale, Meyar);

    DrawDastakNew1(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw1),
        parseInt(hminw1 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);

    DrawDastakNew2(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w2 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw2),
        parseInt(hminw2 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);

    DrawDastakNew3(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w3 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw3),
        parseInt(hminw3 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);

    DrawDastakNew4(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w4 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw4),
        parseInt(hminw4 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);
}

function DrawingPlan2Dahaneh(alfa, D, LWithBie, w1, w2, w3, w4, b2, c1, c2, j,
    alfaw1, alfaw2, alfaw3, alfaw4, p2, StartPointX, StartPointY, Scale, Meyar) {

    DrawPlan2Dahaneh(alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100), parseInt(w2 * 100),
        parseInt(w3 * 100), parseInt(w4 * 100), parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100),
        parseInt(j * 100), alfaw1, alfaw2, alfaw3, alfaw4, parseInt(p2 * 100), StartPointX,
        StartPointY - (parseInt((D / 2) / Math.cos(toRadians(alfa)) * 100) * Scale) - (parseInt((p2 / 2) / Math.cos(toRadians(alfa)) * 100) * Scale), Scale, Meyar);

    DrawDastakNew1(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw1),
        parseInt(hminw1 * 100), parseInt(t * 100), StartPointX,
        StartPointY - (parseInt((D / 2) / Math.cos(toRadians(alfa)) * 100) * Scale) - (parseInt((p2 / 2) / Math.cos(toRadians(alfa)) * 100) * Scale), Scale, Meyar);

    DrawDastakNew2(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w2 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw2),
        parseInt(hminw2 * 100), parseInt(t * 100), StartPointX,
        StartPointY + (parseInt(D * 100) / Math.cos(toRadians(alfa)) * Scale) + (parseInt(p2 * 100) / Math.cos(toRadians(alfa)) * Scale) - (parseInt((D / 2) / Math.cos(toRadians(alfa)) * 100) * Scale) - (parseInt((p2 / 2) / Math.cos(toRadians(alfa)) * 100) * Scale), Scale, Meyar);

    DrawDastakNew3(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w3 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw3),
        parseInt(hminw3 * 100), parseInt(t * 100), StartPointX,
        StartPointY - (parseInt((D / 2) / Math.cos(toRadians(alfa)) * 100) * Scale) - (parseInt((p2 / 2) / Math.cos(toRadians(alfa)) * 100) * Scale), Scale, Meyar);

    DrawDastakNew4(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w4 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw4),
        parseInt(hminw4 * 100), parseInt(t * 100), StartPointX,
        StartPointY + (parseInt(D * 100) / Math.cos(toRadians(alfa)) * Scale) + (parseInt(p2 * 100) / Math.cos(toRadians(alfa)) * Scale) - (parseInt((D / 2) / Math.cos(toRadians(alfa)) * 100) * Scale) - (parseInt((p2 / 2) / Math.cos(toRadians(alfa)) * 100) * Scale), Scale, Meyar);
}

function DrawingPlan3Dahaneh(alfa, D, LWithBie, w1, w2, w3, w4, b2, c1, c2, j,
    alfaw1, alfaw2, alfaw3, alfaw4, p2, StartPointX, StartPointY, Scale, Meyar) {
    DrawPlan3Dahaneh(alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100), parseInt(w2 * 100),
        parseInt(w3 * 100), parseInt(w4 * 100), parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100),
        parseInt(j * 100), alfaw1, alfaw2, alfaw3, alfaw4, parseInt(p2 * 100), StartPointX,
        StartPointY - (parseInt(D * 100) / Math.cos(toRadians(alfa)) * Scale) - (parseInt(p2 * 100) / Math.cos(toRadians(alfa)) * Scale), Scale, Meyar);

    DrawDastakNew1(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw1),
        parseInt(hminw1 * 100), parseInt(t * 100), StartPointX,
        StartPointY - (parseInt(D * 100) / Math.cos(toRadians(alfa)) * Scale) - (parseInt(p2 * 100) / Math.cos(toRadians(alfa)) * Scale), Scale, Meyar);

    DrawDastakNew2(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w2 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw2),
        parseInt(hminw2 * 100), parseInt(t * 100), StartPointX,
        StartPointY + (parseInt((2 * D) / Math.cos(toRadians(alfa)) * 100) * Scale) + (parseInt((2 * p2) / Math.cos(toRadians(alfa)) * 100) * Scale) - (parseInt(D * 100) / Math.cos(toRadians(alfa)) * Scale) - (parseInt(p2 * 100) / Math.cos(toRadians(alfa)) * Scale), Scale, Meyar);

    DrawDastakNew3(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w3 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw3),
        parseInt(hminw3 * 100), parseInt(t * 100), StartPointX,
        StartPointY - (parseInt(D * 100) / Math.cos(toRadians(alfa)) * Scale) - (parseInt(p2 * 100) / Math.cos(toRadians(alfa)) * Scale), Scale, Meyar);

    DrawDastakNew4(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w4 * 100),
        parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw4),
        parseInt(hminw4 * 100), parseInt(t * 100), StartPointX,
        StartPointY + (parseInt((2 * D) / Math.cos(toRadians(alfa)) * 100) * Scale) + (parseInt((2 * p2) / Math.cos(toRadians(alfa)) * 100) * Scale) - (parseInt(D * 100) / Math.cos(toRadians(alfa)) * Scale) - (parseInt(p2 * 100) / Math.cos(toRadians(alfa)) * Scale), Scale, Meyar);
}

function SelectSelcectionPolOrAbroForPayKani() {
    $('#divViewExistingPolForPayKani div a').each(function () {
        if ($(this).hasClass('PolSelected')) {
            $(this).dblclick();
        }
    });
}

function SelectSelcectionKMKhakBardari() {
    $('#divViewExistingKMAmalyateKhaki div a').each(function () {
        if ($(this).hasClass('KMKhakBardariSelected')) {
            $(this).dblclick();
        }
    });
}

function SelctionKMAmalyateKhaki(object, KMId, KMNum) {
    $('#HDFKMAmalyateKhakiIdForEdit').val(KMId);
    $('#HDFKMAmalyateKhakiNum').val(KMNum);
    $('#divViewExistingKMAmalyateKhaki div a').each(function () {
        $(this).css('background-color', '#fff');
        $(this).css('color', '#28069f');
        $(this).removeClass('KMKhakBardariSelected');
    });
    object.css('background-color', 'rgb(82, 149, 192)');
    object.css('color', '#fff');
    object.addClass('KMKhakBardariSelected');
}

