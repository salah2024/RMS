var timerAddPol;
function TabClick(currentTab) {
    PolExistingId = $('#HDFPolIdForEdit').val();
    if (currentTab == 'Plan-' + PolExistingId) {
        $('#svgContainerPol-' + PolExistingId).hide()
        $('#divPol-' + PolExistingId).hide();
        $('#divPlan-' + PolExistingId).show();
        $('#tabPlan-' + PolExistingId).addClass('PlanTableActiveTab');
        $('#tabPol-' + PolExistingId).removeClass('PlanTableActiveTab');
        $('#tabAddedItemsForAbnieFaniPol-' + PolExistingId).removeClass('PlanTableActiveTab');
        $('#divAddedItemsForAbnieFaniPol-' + PolExistingId).hide();

        
        //var currentDahaneAbro = $('#drdDahaneAbro-' + PolExistingId).val();

        //DahaneAbro = currentDahaneAbro * 2 > 4 ? 4 : currentDahaneAbro * 2;
        //$('#w1-' + PolExistingId).val(DahaneAbro);
        //DahaneAbro = currentDahaneAbro * 2 > 4 ? 4 : currentDahaneAbro * 2;
        //$('#w2-' + PolExistingId).val(DahaneAbro);
        //DahaneAbro = currentDahaneAbro * 2 > 4 ? 4 : currentDahaneAbro * 2;
        //$('#w3-' + PolExistingId).val(DahaneAbro);
        //DahaneAbro = currentDahaneAbro * 2 > 4 ? 4 : currentDahaneAbro * 2;
        //$('#w4-' + PolExistingId).val(DahaneAbro);



        //$('#alfaw1').val(135);
        //$('#alfaw2').val(135);
        //$('#alfaw3').val(135);
        //$('#alfaw4').val(135);

        //$('#hMinw1').val(1);
        //$('#hMinw2').val(1);
        //$('#hMinw3').val(1);
        //$('#hMinw4').val(1);


        $('#divPlan-' + PolExistingId).find('input').change(function () {
            
            //////////////چشمک زدن دکمه ذخیره بعد از تغییر
            ChangeSaveButtonColor();
            h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
            t = parseFloat($('#HDFPolt').val());

            ObjectId = $(this).attr('id');
            if (ObjectId.substring(0, 5) == 'hMinw') {


                checkErtefaKhakRiz($(this));

                //if (parseFloat($(this).val()) > (h + t + dErtefaKhakriz)) {
                //    numFix = (h + t + dErtefaKhakriz).toFixed(2);
                //    if (parseFloat($(this).val())>=7) {
                //        numFix = 7;
                //    }
                //    $(this).val(numFix);
                //    toastr.info('ارتفاع دیوار بالی نبایستی از حداکثر ارتفاع کوله بعلاوه ضخامت دال و خاکریز انتخابی بیشتر باشد', 'اطلاع');
                //}

                //if (parseFloat($(this).val()) >= 7) {
                //    $(this).val(7);
                //    toastr.info('ارتفاع دیوار بالی نبایستی از 7 متر بیشتر باشد', 'اطلاع');
                //}
            }

            if (ObjectId.substring(0, 1) == 'w') {

                var currentDahaneAbro = $('#drdDahaneAbro-' + PolExistingId).val();

                dw = parseFloat(this.value);
                DahaneAbro = currentDahaneAbro * 2 > dw ? dw : currentDahaneAbro * 2;
                $(this).val(DahaneAbro);
                //$('#w1-' + PolExistingId).val(DahaneAbro);
                //DahaneAbro = currentDahaneAbro * 2 > 4 ? 4 : currentDahaneAbro * 2;
                //$('#w2-' + PolExistingId).val(DahaneAbro);
                //DahaneAbro = currentDahaneAbro * 2 > 4 ? 4 : currentDahaneAbro * 2;
                //$('#w3-' + PolExistingId).val(DahaneAbro);
                //DahaneAbro = currentDahaneAbro * 2 > 4 ? 4 : currentDahaneAbro * 2;
                //$('#w4-' + PolExistingId).val(DahaneAbro);
            }

            if (ObjectId.substring(0, 6) == 'alfaw1') {
                //if (parseFloat($('#alfaw1-' + PolExistingId).val()) > 180) {
                //    toastr.info('زاویه دیوار بالی نبایستی بیش از 180 درجه باشد', 'اطلاع');
                //    $('#alfaw1-' + PolExistingId).val(180);
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hMinw1 = h - (((h - 1) * (180 - parseFloat($('#alfaw1-' + PolExistingId).val())) / (45)));
                //    $('#hMinw1-' + PolExistingId).val(hMinw1.toFixed(2));
                //}
                //else if (parseFloat($('#alfaw1-' + PolExistingId).val()) >= 90 && parseFloat($('#alfaw1-' + PolExistingId).val()) < 135) {
                //    $('#hMinw1' + PolExistingId).val(1);
                //}
                //else if (parseFloat($('#alfaw1-' + PolExistingId).val()) < 90) {
                //    toastr.info('زاویه دیوار بالی نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                //    $('#alfaw1-' + PolExistingId).val(90);
                //    $('#hMinw1-' + PolExistingId).val(1);
                //}
                //else {
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hMinw1 = h - (((h - 1) * (180 - parseFloat($('#alfaw1-' + PolExistingId).val())) / (45)));
                //    $('#hMinw1-' + PolExistingId).val(hMinw1.toFixed(2));
                //}

                alfaw1 = parseFloat($('#alfaw1-' + PolExistingId).val());

                hMax = h + t;
                hMin = hMax - ((2 - (alfaw1 / 90)) * (hMax - 1));

                $('#hMinw1-' + PolExistingId).val(hMin.toFixed(2));
            }

            if (ObjectId.substring(0, 6) == 'alfaw2') {
                //if (parseFloat($('#alfaw2-' + PolExistingId).val()) > 180) {
                //    toastr.info('زاویه دیوار بالی نبایستی بیش از 180 درجه باشد', 'اطلاع');
                //    $('#alfaw2-' + PolExistingId).val(180);
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hminw2 = h - (((h - 1) * (180 - parseFloat($('#alfaw2-' + PolExistingId).val())) / (45)));
                //    $('#hMinw2-' + PolExistingId).val(hminw2.toFixed(2));
                //}
                //else if (parseFloat($('#alfaw2-' + PolExistingId).val()) >= 90 && parseFloat($('#alfaw2-' + PolExistingId).val()) < 135) {
                //    $('#hMinw2-' + PolExistingId).val(1);
                //}
                //else if (parseFloat($('#alfaw2-' + PolExistingId).val()) < 90) {
                //    toastr.info('زاویه دیوار بالی نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                //    $('#alfaw2-' + PolExistingId).val(90);
                //    $('#hMinw2-' + PolExistingId).val(1);
                //}
                //else {
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hminw2 = h - (((h - 1) * (180 - parseFloat($('#alfaw2-' + PolExistingId).val())) / (45)));
                //    $('#hMinw2-' + PolExistingId).val(hminw2.toFixed(2));
                //}

                alfaw2 = parseFloat($('#alfaw2-' + PolExistingId).val());

                hMax = h + t;
                hMin = hMax - ((2 - (alfaw2 / 90)) * (hMax - 1));

                $('#hMinw2-' + PolExistingId).val(hMin.toFixed(2));

            }

            if (ObjectId.substring(0, 6) == 'alfaw3') {
                //if (parseFloat($('#alfaw3-' + PolExistingId).val()) > 180) {
                //    toastr.info('زاویه دیوار بالی نبایستی بیش از 180 درجه باشد', 'اطلاع');
                //    $('#alfaw3-' + PolExistingId).val(180);
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hMinw3 = h - (((h - 1) * (180 - parseFloat($('#alfaw3-' + PolExistingId).val())) / (45)));
                //    $('#hMinw3-' + PolExistingId).val(hMinw3.toFixed(2));
                //}
                //else if (parseFloat($('#alfaw3-' + PolExistingId).val()) >= 90 && parseFloat($('#alfaw3-' + PolExistingId).val()) < 135) {
                //    $('#hMinw3-' + PolExistingId).val(1);
                //}
                //else if (parseFloat($('#alfaw3-' + PolExistingId).val()) < 90) {
                //    toastr.info('زاویه دیوار بالی نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                //    $('#alfaw3-' + PolExistingId).val(90);
                //    $('#hMinw3-' + PolExistingId).val(1);
                //}
                //else {
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hMinw3 = h - (((h - 1) * (180 - parseFloat($('#alfaw3-' + PolExistingId).val())) / (45)));
                //    $('#hMinw3-' + PolExistingId).val(hMinw3.toFixed(2));
                //}

                alfaw3 = parseFloat($('#alfaw3-' + PolExistingId).val());

                hMax = h + t;
                hMin = hMax - ((2 - (alfaw3 / 90)) * (hMax - 1));

                $('#hMinw3-' + PolExistingId).val(hMin.toFixed(2));

            }

            if (ObjectId.substring(0, 6) == 'alfaw4') {
                //if (parseFloat($('#alfaw4-' + PolExistingId).val()) > 180) {
                //    toastr.info('زاویه دیوار بالی نبایستی بیش از 180 درجه باشد', 'اطلاع');
                //    $('#alfaw4').val(180);
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hMinw4 = h - (((h - 1) * (180 - parseFloat($('#alfaw4-' + PolExistingId).val())) / (45)));
                //    $('#hMinw4-' + PolExistingId).val(hMinw4.toFixed(2));
                //}
                //else if (parseFloat($('#alfaw4-' + PolExistingId).val()) >= 90 && parseFloat($('#alfaw4-' + PolExistingId).val()) < 135) {
                //    $('#hMinw4-' + PolExistingId).val(1);
                //}
                //else if (parseFloat($('#alfaw4-' + PolExistingId).val()) < 90) {
                //    toastr.info('زاویه دیوار بالی نبایستی کمتر از 90 درجه باشد', 'اطلاع');
                //    $('#alfaw4-' + PolExistingId).val(90);
                //    $('#hMinw4-' + PolExistingId).val(1);
                //}
                //else {
                //    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId).val());
                //    hMinw4 = h - (((h - 1) * (180 - parseFloat($('#alfaw4-' + PolExistingId).val())) / (45)));
                //    $('#hMinw4-' + PolExistingId).val(hMinw4.toFixed(2));
                //}

                alfaw4 = parseFloat($('#alfaw4-' + PolExistingId).val());

                hMax = h + t;
                hMin = hMax - ((2 - (alfaw4 / 90)) * (hMax - 1));

                $('#hMinw4-' + PolExistingId).val(hMin.toFixed(2));

            }

            if (ObjectId.substring(0, 4) == 'alfa') {
                fAlfa = parseFloat($('#alfa-' + PolExistingId).val());

                if (isNaN(fAlfa)) {
                    $('#alfa-' + PolExistingId).val(0);
                }
                if (fAlfa > 45) {
                    toastr.info('زاویه بیه نبایستی بیش از 45 درجه باشد', 'اطلاع');
                    $('#alfa-' + PolExistingId).val(45);
                }
                else if (fAlfa < -45) {
                    toastr.info('زاویه بیه نبایستی کمتر از 45- درجه باشد', 'اطلاع');
                    $('#alfa-' + PolExistingId).val(-45);
                }
            }

            if (ObjectId.substring(0, 5) == 'LAbro' || ObjectId.substring(0, 4) == 'alfa') {
                fLAbro = parseFloat($('#LAbro-' + PolExistingId).val());

                var NahveEjraDal = $(`input[name="drdNahveEjraDal-${PolExistingId}"]:checked`).val();
                currentLAbro = $('#LAbro-' + PolExistingId).val();
                //NahveEjraDal = $('#drdNahveEjraDal-' + PolExistingId).val();
                if (NahveEjraDal == "2") {
                    currentLAbro = Math.round(currentLAbro);
                    $('#LAbro-' + PolExistingId).val(currentLAbro);
                }
                if (fLAbro < 1 || isNaN(fLAbro)) {
                    $('#LAbro-' + PolExistingId).val(1);
                }
                $('#LAbroWithBie-' + PolExistingId).val(($('#LAbro-' + PolExistingId).val() / Math.cos(toRadians($('#alfa-' + PolExistingId).val()))).toFixed(2));
            }
            drawingPlan();
        });
        drawingPlan();
    }
    else {
        $('#tabAddedItemsForAbnieFaniPol-' + PolExistingId).removeClass('PlanTableActiveTab');
        $('#divAddedItemsForAbnieFaniPol-' + PolExistingId).hide();
        $('#svgContainerPol-' + PolExistingId).show()
        $('#divPol-' + PolExistingId).show();
        $('#divPlan-' + PolExistingId).hide();
        $('#tabPlan-' + PolExistingId).removeClass('PlanTableActiveTab');
        $('#tabPol-' + PolExistingId).addClass('PlanTableActiveTab');
    }
}

function checkErtefaKhakRiz(obj) {

    
    PolExistingId = $('#HDFPolIdForEdit').val();

    h = parseFloat($('#drdHadeAksarErtefa-' + PolExistingId + ' option:selected').val()); //parseFloat($('#txtHadeAksarErtefa-' + PolExistingId).val());

    t = parseFloat($('#HDFPolt').val());

    ErtefaKhakriz = $('#drdErtefaKhakriz-' + PolExistingId + ' option:selected').text();

    var match = ErtefaKhakriz.match(/[\d.]+$/);

    dErtefaKhakriz = parseFloat(match[0]);

    let maxHeight = h + t;// + dErtefaKhakriz;
    let inputVal = parseFloat(obj.val());

    if (inputVal > maxHeight || inputVal >= 7) {

        let numFix = Math.min(maxHeight, 7);

        obj.val(numFix.toFixed(2));

        if (numFix >= 7) {
            toastr.info('ارتفاع دیوار بالی نبایستی از 7 متر بیشتر باشد', 'اطلاع');
        } else {
            toastr.info('ارتفاع دیوار بالی نبایستی از حداکثر ارتفاع کوله بعلاوه ضخامت دال و خاکریز انتخابی بیشتر باشد', 'اطلاع');
        }
    }
    return 0;
}
function ChangeSaveButtonColor() {
    clearTimeout(timerAddPol);

    timerAddPol = setTimeout(ChangeBackColor1, 10);
    function ChangeBackColor1() {
        $('#btnAddEditPol').css('background-color', '#516b92');
        timerAddPol = setTimeout(ChangeBackColor2, 500);
    }
    function ChangeBackColor2() {
        $('#btnAddEditPol').css('background-color', 'green');
        timerAddPol = setTimeout(ChangeBackColor1, 500);
    }
}

var AbadeKoole = '';
var DastakPolInfo = null;
var HadAksarErtefaKoole = null;



function AbnieFaniWithBarAvordIdClick(OpId, BarAvordUserId) {

    ShowExistingPol(BarAvordUserId, OpId);

    //$('#AbnieShow').find('#divShowAbnie').html(str);
    //$('#aAbnieShow').click();
}

var ItemsFBDependQuestionForAbnieFani = '';
var QuesForAbnieFaniValues = '';
var RizMetreUsersEzafeBahaValue = '';
function ShowFormsAddedItemsAbnieFaniPol(BarAvordUserId) {
    PolExistingId = $('#HDFPolIdForEdit').val();
    $('#svgContainerPol-' + PolExistingId).hide()
    $('#divPol-' + PolExistingId).hide();
    $('#divPlan-' + PolExistingId).hide();
    $('#tabPlan-' + PolExistingId).removeClass('PlanTableActiveTab');
    $('#tabPol').removeClass('PlanTableActiveTab');

    $('#tabAddedItemsForAbnieFaniPol-' + PolExistingId).addClass('PlanTableActiveTab');
    $('#divAddedItemsForAbnieFaniPol-' + PolExistingId).show();
    PolVaAbroId = $('#HDFPolIdForEdit').val();
    PolNum = $('#HDFPolNum').val();
    

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

                $('#divAddedItemsForAbnieFaniPol-' + PolExistingId).html(str);
                $.each(AbnieFaniQueries, function () {
                    Id = $(this).find("Id").text();
                    $.each(QuesForAbnieFaniValues, function () {
                        objectId = $(this).find("Id").text();
                        QuesForAbnieFaniId = $(this).find("QuestionForAbnieFaniId").text();
                        if (Id == QuesForAbnieFaniId) {
                            $('#rdEBPol' + Id).prop('checked', true);
                            $('#spanCheckBoxEzafeBahaAbnieFaniPol' + Id).click();
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
    if ($('#rdEBPol' + Id).is(':checked'))
        ShowEzafeBahaAbnieFaniPol(Id)
    else {
        $('#rdEBPol' + Id).click();
        EzafeBahaAbnieFaniPolClick(Id);
    }
}

function ShowEzafeBahaAbnieFaniPol(Id) {
    str = '';
    if (!$('#divSubEzafeBahaAbnieFaniPol' + Id).hasClass('ShowSubEzafeBahaAbnieFaniPol')) {
        str += '<div class=\'col-md-12\' style=\'padding: 2px;\'><div class=\'col-md-7\' style=\'background-color: #eaf3ff;border-radius: 5px !important;\'><div class=\'col-md-3\'><span>شماره آیتم</span></div><div class=\'col-md-3\'><span>حجم آیتم</span></div><div class=\'col-md-3\'><span>مقدار</span></div><div class=\'col-md-3\'><span>واحد</span></div></div></div>';
        strItemsForAdd = '';
        Shomareh = $('#spanShomareh' + Id).html();
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
        $('#divSubEzafeBahaAbnieFaniPol' + Id).html(str);


        $('#divSubEzafeBahaAbnieFaniPol' + Id + ' input[type="text"] ').change(function () {
            txtId = $(this).attr('id');
            ObjectId = txtId.substring(15, txtId.length);
            ItemShomareh = $('#spanItemShomareh' + ObjectId).html();
            Meghdar = $(this).val();
            Hajm = $('#spanHajm' + ObjectId).html();
            UpdateEzafeBahaAbnieFaniPol(Id, ItemShomareh, Hajm, Meghdar);
        });

        $('#divSubEzafeBahaAbnieFaniPol' + Id).show();
        $('#divSubEzafeBahaAbnieFaniPol' + Id).addClass('ShowSubEzafeBahaAbnieFaniPol');
    }
    else {
        $('#divSubEzafeBahaAbnieFaniPol' + Id).hide();
        $('#divSubEzafeBahaAbnieFaniPol' + Id).removeClass('ShowSubEzafeBahaAbnieFaniPol');
    }
}

function EzafeBahaAbnieFaniPolClick(Id) {
    if (!$('#rdEBPol' + Id).is(':checked')) {
        $('#divSubEzafeBahaAbnieFaniPol' + Id).addClass('ShowSubEzafeBahaAbnieFaniPol');
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
        $('#divSubEzafeBahaAbnieFaniPol' + Id).html(str);

        $('#divSubEzafeBahaAbnieFaniPol' + Id + ' input[type="text"] ').change(function () {
            txtId = $(this).attr('id');
            ObjectId = txtId.substring(15, txtId.length);
            ItemShomareh = $('#spanItemShomareh' + ObjectId).html();
            Meghdar = $(this).val();
            Hajm = $('#spanHajm' + ObjectId).html();
            UpdateEzafeBahaAbnieFaniPol(Id, ItemShomareh, Hajm, Meghdar);
        });

        $('#divSubEzafeBahaAbnieFaniPol' + Id).show();

        $('#divSubEzafeBahaAbnieFaniPol' + Id).addClass('ShowSubEzafeBahaAbnieFaniPol');

        //$('#divAddedItemsForAbnieFaniPol span').each(function () {
        //    if ($(this).hasClass('spanCheckBoxEzafeBahaAbnieFaniPol')) {
        //    
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
        $('#divSubEzafeBahaAbnieFaniPol' + Id).hide();
        $('#divSubEzafeBahaAbnieFaniPol' + Id).removeClass('ShowSubEzafeBahaAbnieFaniPol');
        ItemFBForDel = $('#spanShomareh' + Id).html();
        DeleteEzafeBahaForAbnieFaniPolForItem(ItemFBForDel, Id);
    }
}

function ShowExistingPol(BarAvordUserId, OpId) {
    $('#HDFOperationId').val(OpId);
    
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
            

            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            var PolVaAbroBarAvord = xml.find("tblPolVaAbroBarAvord");
            DastakPolInfo = xml.find("tblDastakPolInfo");
            count = 1;
            str = `
            <div class="row col-12 PolExistHeadStyle">
                <div class="col-1">ردیف</div>
                <div class="col-1">نوع</div>
                <div class="col-1">تعداد دهانه</div>
                <div class="col-2">دهانه آبرو</div>
                <div class="col-2">حداکثر ارتفاع کوله</div>
                <div class="col-2">موقعیت</div>
                <div class="col-2">شرح</div>
                <div class="col-1">حذف</div>
            </div>
            `;
            $.each(PolVaAbroBarAvord, function () {
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
                Des = $(this).find("des").text();

                TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);
                strNoeName = TedadDarDahaneAbro > 6 ? ' پل ' : ' آبرو ';

                str += `
                    <div class="row col-12 PolExistBodyStyle"
                        onclick="ShowSelctionPol(
                        0, '${PolExistingId}', '${PolNum}', ${TedadDahaneh}, ${DahaneAbro},
                        '${HadAksarErtefaKoole}', ${Hs}, ${ZavieBie}, ${ToolAbro},
                        ${x}, ${y}, ${NoeBanaii}, ${NahveEjraDal}, '${BarAvordUserId}'
                        )">
                        <div class="col-1">
                            ${count++}
                        </div>
                        <div class="col-1">
                            ${strNoeName}
                        </div>
                        <div class="col-1">
                            ${TedadDahaneh}
                        </div>
                        <div class="col-2">
                            ${DahaneAbro}
                        </div>
                        <div class="col-2">
                            ${HadAksarErtefaKoole}
                        </div>
                        <div class="col-2">
                            X: ${parseInt(x)} ، Y: ${parseInt(y)}
                        </div>
                        <div class="col-2">
                            ${Des}
                        </div>
                        <div class="col-1">
                            <i class="fa fa-trash DelRMUStyle" onclick="DeletePolClick('${PolExistingId}')"></i>
                        </div>
                    </div>
                    <div id="PolShow-${PolExistingId}" style="width:100%"></div>
                    <div style="margin-top:10px" class="row col-12" id="ViewRizMetrePol-${PolExistingId}"></div>
                    `;



                //str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionPol($(this),\'' + PolExistingId + "\'',\' + PolNum + '\')\" ondblclick=\"ShowSelctionPol(0,' +
                //    PolExistingId + ',' + PolNum + ',' + TedadDahaneh + ',' + DahaneAbro + ',\'' + HadAksarErtefaKoole + '\',' + Hs + ',' +
                //    ZavieBie + ',' + ToolAbro + ',' + x + ',' + y + ',' + NoeBanaii + ',' + NahveEjraDal + ',' + BarAvordId + ')\">' + count++ + ' - ' + strNoeName + TedadDahaneh
                //    + ' دهانه، ' + DahaneAbro + ' متری، موقعیت: X: ' + parseInt(x) + ' ، Y: ' + parseInt(y) + '</a></div>';
            });

            str += `
                    <div class="row col-12 PolExistBodyStyle"
                        onclick="ShowSelctionPol(1, 'New', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '${BarAvordUserId}');">
                        <div class="col-1">
                            جدید
                        </div>
                        <div class="col-1">
                        </div>
                        <div class="col-1">
                        </div>
                        <div class="col-2">
                        </div>
                        <div class="col-2">
                        </div>
                        <div class="col-2">
                        </div>
                        <div class="col-2">
                        </div>
                    </div>
                    <div id="PolShow-New" style="width:100%"></div>
                    `;

            //<a class='NewPolStyle' onclick="ShowSelctionPol(1,0,0,0,0,0,0,0,0,0,0,'${NoeBanaii}','${NahveEjraDal}','${BarAvordUserId}')">
            //                ایجاد (آبرو/پل) جدید</a>

            str1 = `
            <div id="divViewExistingPol1" class="row">
                ${str}
            </div>`;
            //<div class="row col-12">
            //  <i onclick="PolNew()" class="fa fa-plus SaveRMUStyle"></i>
            //</div>`;

            //<div class="row col-12" id="PolShow-New">
            //    <div class="col-md-12 row" style="margin-top:40px">

            //        <div class="col-md-1" style="text-align:left">

            //            <span class="spanStyleMitraSmall">تعداد دهانه :</span>
            //        </div>
            //        <div class="col-md-1">
            //            <select class="form-control spanStyleMitraSmall" id="drdTedadDahaneh-New"></select>
            //        </div>

            //        <div class="col-md-1" style="text-align:left">
            //            <span class="spanStyleMitraSmall">دهانه آبرو :</span>
            //        </div>
            //        <div class="col-md-1">
            //            <select class="form-control spanStyleMitraSmall" id="drdDahaneAbro-New"></select>
            //        </div>

            //        <div class="col-md-1" style="text-align:left;padding-left:0;padding-right:0">
            //            <span class="spanStyleMitraSmall">حداکثر ارتفاع کوله:</span>
            //        </div>
            //        <div class="col-md-1">
            //            <select class="form-control spanStyleMitraSmall" id="drdHadeAksarErtefa-New"></select>
            //        </div>

            //        <div class="col-md-1" style="text-align:left;padding-left:0;padding-right:0">
            //            <span class="spanStyleMitraSmall">ارتفاع خاکریز روی آبرو :</span>
            //        </div>
            //        <div class="col-md-1">
            //            <select class="form-control spanStyleMitraSmall" id="drdErtefaKhakriz-New"></select>
            //        </div>

            //        <div class="col-md-1" style="text-align:left">
            //            <span class="spanStyleMitraSmall">نوع بنایی :</span>
            //        </div>
            //        <div class="col-md-1">
            //            <select class="form-control spanStyleMitraSmall" id="drdNoeBanaii-New}" style="padding:0 2px">
            //                <option value="1">بتن غیر مسلح</option>
            //                <option value="2">سنگی</option>
            //            </select>
            //        </div>

            //        <div class="col-md-1" style="text-align:left">
            //            <span class="spanStyleMitraSmall">نحوه اجرای دال :</span>
            //        </div>
            //        <div class="col-md-1">
            //            <select class="form-control spanStyleMitraSmall" id="drdNahveEjraDal-New">
            //                <option value="1">درجا</option>
            //                <option value="2">پیش ساخته</option>
            //            </select>
            //        </div>

            //    </div>

            //<div style="margin-top:10px" class="row col-12" id="ViewPol-${PolExistingId}"></div>
            //<div style="margin-top:10px" class="row col-12" id="ViewRizMetrePol-${PolExistingId}"></div>
            //</div>

            //`;


            $('#ula' + OpId).html(str1);

            //NoeBanaii = $('#drdNoeBanaii-New').val();
            //NahveEjraDal = $('#drdNahveEjraDal-New').val();

            //ShowSelctionPol(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "'" + NoeBanaii + "'", "'" + NahveEjraDal + "'", "'" + BarAvordUserId + "'");

            //fillTedadDahaneh(0, 0, 0, 0);

            


            //$('#ExistAbnieShow').find('#divViewExistingPol').html(str);
            //$('#aExistAbnieShow').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری پل های موجود', 'خطا');
        }
    });
}

function DeletePolClick(PolId) {
    var vardata = new Object();
    vardata.PolId = PolId;

    $.ajax({
        url: "/PolVaAbroBarAvord/DeletePolVaAbro",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('پل بدرستی حذف گردید', 'موفقیت');
                OperationId = $('#HDFOperationId').val();
                BarAvordUserID = $('#HDFBarAvordUserID').val();
                ShowExistingPol(BarAvordUserID, OperationId);
            }
        },
        error: function () {
            toastr.error('خطا در حذف پل!', 'خطا');
        }
    });
}

function fillTedadDahaneh(GetTedadDahaneh, GetDahaneAbro, GetHadAksarErtefaKoole, GetErtefaKhakriz, NoeBanaii, NahveEjraDal, IsNew) {

    
    PolExistingId = $('#HDFPolIdForEdit').val();
    TedadDahanehList = '';
    count = 0;
    TedadDahanehFirst = 0;
    HadAksarErtefaKooleFirst = 0;
    str = '';
    $.each(HadAksarErtefaKoole, function () {

        blnCheck = false;
        TedadDahanehListSplit = TedadDahanehList.split(',');
        TedadDahaneh = $(this).find("TedadDahaneh").text();
        HadAksarErtefaKooleItem = $(this).find("HadAksarErtefaKoole").text();
        for (var i = 0; i < TedadDahanehListSplit.length; i++) {
            if (TedadDahanehListSplit[i] == TedadDahaneh)
                blnCheck = true;
        }
        if (!blnCheck) {
            count++;
            if (count == 1) {
                TedadDahanehFirst = TedadDahaneh;
                HadAksarErtefaKooleFirst = HadAksarErtefaKooleItem;
            }
            TedadDahanehList += TedadDahaneh + ',';
            str += "<option value=\"" + TedadDahaneh + "\">" + TedadDahaneh + "</option>";

        }
    });

    if (IsNew == 1) {
        $('#drdHadeAksarErtefa-' + PolExistingId).val(HadAksarErtefaKooleFirst);
    }

    $('#drdTedadDahaneh-' + PolExistingId).html(str);
    $('#drdTedadDahaneh-' + PolExistingId).each(function () {
        $('option', this).each(function () {
            if ($(this).text() == GetTedadDahaneh) {
                $('#drdTedadDahaneh-' + PolExistingId).val(GetTedadDahaneh);
            };
        });
    });
    fillDahaneAbro(GetDahaneAbro, GetHadAksarErtefaKoole, GetErtefaKhakriz, NoeBanaii, NahveEjraDal);
}

function fillDahaneAbro(GetDahaneAbro, GetHadAksarErtefaKoole, GetErtefaKhakriz, NoeBanaii, NahveEjraDal) {
    PolExistingId = $('#HDFPolIdForEdit').val();
    count = 0;
    DahaneAbroList = '';
    str = '';
    TedadDahanehFirst = $('#drdTedadDahaneh-' + PolExistingId).val();
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
    $('#drdDahaneAbro-' + PolExistingId).html(str);
    $('#drdDahaneAbro-' + PolExistingId).each(function () {
        $('option', this).each(function () {
            if ($(this).text() == GetDahaneAbro) {
                $('#drdDahaneAbro-' + PolExistingId).val(GetDahaneAbro);
            };
        });
    });

    TedadDahaneh = $('#drdTedadDahaneh-' + PolExistingId).val();
    DahaneAbro = $('#drdDahaneAbro-' + PolExistingId).val();
    TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);

    strNoeBanaii = '';
    //if (TedadDarDahaneAbro > 6)
    //    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option>';
    //else {
    //    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
    //}

    if (TedadDarDahaneAbro > 4) {
        $('#drdNoeBanaiiSangi-' + PolExistingId).hide();
        $(`input[name="drdNoeBanaii-${PolExistingId}"][value="${NoeBanaii}"]`).prop("checked", true);
    }
    else {
        $('#drdNoeBanaiiBotton-' + PolExistingId).show();
        $(`input[name="drdNoeBanaii-${PolExistingId}"][value="${NoeBanaii}"]`).prop("checked", true);
        $('#drdNoeBanaiiSangi-' + PolExistingId).show();
    }
    $('#drdNoeBanaii-' + PolExistingId).html(strNoeBanaii);


    strNahveEjraDal = '';
    if (DahaneAbro > 3) {
        $('#drdNahveEjraDalPishSakhte-' + PolExistingId).hide();
        $(`input[name="drdNahveEjraDal-${PolExistingId}"][value="${NahveEjraDal}"]`).prop("checked", true);
    }
    else {
        $('#drdNahveEjraDalPishDarja-' + PolExistingId).show();
        $('#drdNahveEjraDalPishSakhte-' + PolExistingId).show();
        $(`input[name="drdNahveEjraDal-${PolExistingId}"][value="${NahveEjraDal}"]`).prop("checked", true);
    }

    //strNahveEjraDal = '';
    //if (DahaneAbro > 3)
    //    strNahveEjraDal = '<option value=\'1\'>درجا</option>';
    //else {
    //    strNahveEjraDal = '<option value=\'1\'>درجا</option><option value=\'2\'>پیش ساخته</option>';
    //}
    //$('#drdNahveEjraDal-' + PolExistingId).html(strNahveEjraDal);

    fillHadeAksarErtefa(GetHadAksarErtefaKoole, GetErtefaKhakriz)
}

function fillHadeAksarErtefa(GetHadAksarErtefaKoole, GetErtefaKhakriz) {

    debugger;
    PolExistingId = $('#HDFPolIdForEdit').val();
    HadeAksarErtefaList = '';
    DahaneAbroFirst = $('#drdDahaneAbro-' + PolExistingId).val();
    str = '';
    MaxHadAksarErtefaKoole = 0;

    $.each(HadAksarErtefaKoole, function () {
        debugger;

        blnCheck = false;
        HadeAksarErtefaListSplit = HadeAksarErtefaList.split(',');
        HadeAksarErtefa = $(this).find('HadAksarErtefaKoole').text();
        DahaneAbro = $(this).find('DahaneAbro').text();
        for (var i = 0; i < HadeAksarErtefaListSplit.length; i++) {
            if (HadeAksarErtefaListSplit[i] == HadeAksarErtefa)
                blnCheck = true;
        }
        if (!blnCheck && DahaneAbro == DahaneAbroFirst) {
            HadeAksarErtefaList += HadeAksarErtefa + ',';
            str += "<option value=\"" + HadeAksarErtefa + "\">" + HadeAksarErtefa + "</option>";

            //MaxHadAksarErtefaKoole = MaxHadAksarErtefaKoole > HadeAksarErtefa ? MaxHadAksarErtefaKoole : HadeAksarErtefa;
        }

    });

    

    //$('#txtHadeAksarErtefa-' + PolExistingId).val(MaxHadAksarErtefaKoole);
    $('#drdHadeAksarErtefa-' + PolExistingId).html(str);


    fnGetErtefaKhakriz(GetErtefaKhakriz);
    //$('#drdHadeAksarErtefa-' + PolExistingId).each(function () {
    //    $('option', this).each(function () {
    //        if ($(this).text() == GetHadAksarErtefaKoole) {
    //            $('#drdHadeAksarErtefa-' + PolExistingId).val(GetHadAksarErtefaKoole);
    //        };
    //    });
    //});
}
function fnGetErtefaKhakriz(GetErtefaKhakriz) {
    
    PolExistingId = $('#HDFPolIdForEdit').val();
    TedadDahanehCurrent = $('#drdTedadDahaneh-' + PolExistingId).val();
    DahaneAbroCurrent = $('#drdDahaneAbro-' + PolExistingId).val();
    HadeAksarErtefa =  $('#drdHadeAksarErtefa-' + PolExistingId).val();//$('#txtHadeAksarErtefa-' + PolExistingId).val();
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
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            debugger;

            D = $('#drdDahaneAbro-' + PolExistingId).val();
            H =  $('#drdHadeAksarErtefa-' + PolExistingId).val();//$('#txtHadeAksarErtefa-' + PolExistingId).val();
            Hs = ''; a1 = 0; a2 = 0; b1 = 0; b2 = 0;
            c1 = 0; c2 = 0; f = 0; m = 0; p1 = 0; p2 = 0; e = 0; n = 0; k = 0; t = 0; HadAghalZavieEstekak = 0; j = 0;
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            AbadeKoole = xml.find('AbadeKoole');
            if (AbadeKoole.length == 0) {
                $('#svgLines-' + PolExistingId).html('');
                $('#drdErtefaKhakriz-' + PolExistingId).val('');
                toastr.warning('متناسب با ارتفاع کوله هیچ خاکریزی تعریف نشده است', 'هشدار');
            }
            else {
                Counter = 0;

                debugger;

                $.each(AbadeKoole, function () {

                    Id = $(this).find("Id").text();
                    Hs = $(this).find("Hs").text().toString();
                    str += `<option value='${Id}'>${Hs}</option>`;
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


                $('#drdErtefaKhakriz-' + PolExistingId).html(str);
                $('#drdErtefaKhakriz-' + PolExistingId).each(function () {
                    $('option', this).each(function () {
                        if ($(this).val() == GetErtefaKhakriz) {
                            $('#drdErtefaKhakriz-' + PolExistingId).val(GetErtefaKhakriz);
                        };
                    });
                });



                Canvaswidth = $('#svgLines-' + PolExistingId).attr('width');

                ///////////

                //scale = 1;
                //StartPointX = Canvaswidth / 6;
                //StartPointY = 250;
                //if (D >= 3) {
                //    scale = 1 / 2;
                //    StartPointY = 350;
                //}
                //if (D >= 4 && D < 6) {
                //    //StartPointX = 0;
                //    StartPointY = 250;
                //}
                //else if (D >= 6 && D < 7) {
                //    scale = 1 / 2.5;
                //    StartPointY = 250;
                //}
                //else if (D >= 7 && D < 9) {
                //    scale = 1 / 2.5;
                //    StartPointY = 290;
                //}
                //else if (D >= 9) {
                //    scale = 1 / 3;
                //    //StartPointX = 0;
                //    StartPointY = 310;
                //}
                ////else if (D >= 8)
                ////    scale = 1 / 4;

                //$('#svgLines-' + PolExistingId).html('');

                debugger;


                Meyar = 1;
                scale = 1;
                StartPointX = 200;
                StartPointY = 280;
                if (D >= 3) {
                    scale = 1 / 2;
                }
                if (D >= 4 && D < 7) {
                    StartPointX = 200;
                    StartPointY = 250;
                    scale = 1 / 2.5;
                }
                else if (D >= 7 && D < 9) {
                    StartPointX = 200;
                    StartPointY = 250;
                    scale = 1 / 3.5;
                }
                else if (D >= 9) {
                    StartPointX = 200;
                    StartPointY = 250;
                    scale = 1 / 4.5;
                }
                $('#svgLines-' + PolExistingId).html('');


                NoeBanaii = $(`input[name="drdNoeBanaii-${PolExistingId}"]:checked`).val();

                if (TedadDahanehCurrent == 1) {

                    Pol(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100)
                        , parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100)
                        , parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar, NoeBanaii);

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
                    scale = 1 / 1.5;
                    StartPointY = 260;
                    Pol2Dahaneh(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100), parseInt(b2 * 100),
                        parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100), parseInt(p1 * 100), parseInt(p2 * 100)
                        , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar, NoeBanaii);
                }
                else if (TedadDahanehCurrent == 3) {
                    if (D >= 2 && D < 4) {
                        StartPointX = Canvaswidth / 10;
                        StartPointY = 260;
                        scale = 1 / 2;
                    }
                    else if (D >= 4 && D < 7) {
                        StartPointX = Canvaswidth / 10;
                        StartPointY = 260;
                        scale = 1 / 3;
                    }
                    else if (D >= 7) {
                        StartPointX = Canvaswidth / 10;
                        StartPointY = 260;
                        scale = 1 / 4;
                    }
                    Pol3Dahaneh(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100), parseInt(b2 * 100),
                        parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100), parseInt(p1 * 100), parseInt(p2 * 100)
                        , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar, NoeBanaii);
                }

                for (var i = 1; i < 5; i++) {
                    hMinwCurrent = $(`#hMinw${i}-` + PolExistingId);

                    hMinwCurrent.val($('#drdHadeAksarErtefa-' + PolExistingId).val());

                    checkErtefaKhakRiz(hMinwCurrent);
                }
            }
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری حداکثر ارتفاع خاکریز', 'خطا');
        }
    });
}


//function PolNew() {
//    $('[id^=PolShow-]').each(function () {
//        aa = $(this).html();
//        
//        if (aa != '') {
//            $(this).empty();
//            $(this).slideUp(500);

//            $('#PolShow-New').html(aa);
//            $('#ViewRizMetrePol').empty();
//            $('#PolShow-New').slideDown(500);
//        }
//    });
//}


function ShowSelctionPol(IsNew, PolExistingId, PolNum, GetTedadDahaneh, GetDahaneAbro, GetHadAksarErtefaKoole, GetHs
    , GetZavieBie, GetToolAbro, x, y, NoeBanaii, NahveEjraDal, BarAvordUserId) {
    //$('#btnCloseFormExistAbnie').click();

    

    $('#HDFPolIdForEdit').val(PolExistingId);
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
            


            //<div class='col-md-2'>
            //    <input id='tabAddedItemsForAbnieFaniPol-${PolExistingId}' onclick='ShowFormsAddedItemsAbnieFaniPol(${objBarAvordUserId})' type='button' style='border: 0px;color: #fff;border-radius:2px 2px 0px 0px !important;'
            //        class='spanStyleMitraSmall spanFrameNameStyle' value='اضافه بها'/>
            //</div>

            str = `
                <div class='row col-12' style='margin-left:0px;margin-right:0px'>
                    <div class='row col-12'><div class='col-md-2'>
                        <input type='button' style='border: 0px;color: #fff;border-radius:2px 2px 0px 0px !important;'
                                class='spanStyleMitraSmall spanFrameNameStyle PlanTableActiveTab'
                                id='tabPol-${PolExistingId}' onclick='TabClick("Pol-${PolExistingId}")' value='مقطع پل'/>
                    </div>
                    <div class='col-md-2'>
                        <input id='tabPlan-${PolExistingId}' onclick='TabClick("Plan-${PolExistingId}")' 
                                type='button' style='border: 0px;color: #fff;border-radius:2px 2px 0px 0px !important;' 
                                class='spanStyleMitraSmall spanFrameNameStyle' value='پلان'/>
                    </div>
                 </div>
                 <div id='divPol-${PolExistingId}' style='padding-bottom: 10px;margin-left: 0px;margin-right: 0px;border-bottom: 0px;border-bottom: 1px solid rgb(129, 173, 201);' class="row col-12">

                 <div class="col-md-3 row" style="margin-top: 15px;">
                        <div class="col-md-4" style="text-align:left">
                            <span class="spanStyleMitraSmall">تعداد دهانه : </span>
                        </div>
                        <div class="col-md-8">
                            <select class="form-control spanStyleMitraSmall" id='drdTedadDahaneh-${PolExistingId}'>
                            </select>
                        </div>
                        <div class="col-md-4" style="text-align:left">
                            <span class="spanStyleMitraSmall">دهانه آبرو : </span>
                        </div>
                        <div class="col-md-8">
                            <select class="form-control spanStyleMitraSmall" id='drdDahaneAbro-${PolExistingId}'>
                            </select>
                        </div>
                        <div class="col-md-4" style="text-align:left;padding-left:0;padding-right:0">
                            <span class="spanStyleMitraSmall">ارتفاع کوله:</span>
                        </div>
                        <div class="col-md-8">
                            <select class="form-control spanStyleMitraSmall" id='drdHadeAksarErtefa-${PolExistingId}'>
                            </select>

                        </div>
                        <div class="col-md-4" style="text-align:left;padding-left: 0px;padding-right: 0px;">
                            <span class="spanStyleMitraSmall">ارتفاع خاکریز روی آبرو : </span>
                        </div>
                        <div class="col-md-8">
                            <select class="form-control spanStyleMitraSmall" id='drdErtefaKhakriz-${PolExistingId}'>
                            </select>
                        </div>
                    
                        <div class="col-md-4" style="text-align:left">
                            <span class="spanStyleMitraSmall">نوع بنایی : </span>
                        </div>
                        <div class="col-md-8">
                        <div class="form-control spanStyleMitraSmall" style="border: unset !important;">
                            <label id="drdNoeBanaiiBotton-${PolExistingId}" style="margin-left:10px;">
                                 بتن غیر مسلح
                                 <input type="radio" checked name="drdNoeBanaii-${PolExistingId}" value="1">
                            </label>

                            <label id="drdNoeBanaiiSangi-${PolExistingId}">
                                 سنگی 
                                 <input type="radio" name="drdNoeBanaii-${PolExistingId}" value="2">
                            </label>
                        </div>
                        </div>
                        <div class="col-md-4" style="text-align:left">
                            <span class="spanStyleMitraSmall">نحوه اجرای دال : </span>
                        </div>
                        <div class="col-md-8">
                        <div class="form-control spanStyleMitraSmall" style="border: unset !important;">
                            <label id="drdNahveEjraDalDarja-${PolExistingId}" style="margin-left:10px;">
                                 درجا
                                 <input type="radio" checked name="drdNahveEjraDal-${PolExistingId}" value="1">
                            </label>

                            <label id="drdNahveEjraDalPishSakhte-${PolExistingId}">
                                پیش ساخته
                                 <input type="radio" name="drdNahveEjraDal-${PolExistingId}" value="2">
                            </label>
                        </div>
                        </div>
                    </div>

                 <div class="row col-9" id="svgContainerPol-${PolExistingId}" style='border:1px solid rgb(129, 173, 201);margin-right: 0px;overflow:auto'>
                            <svg id="svgLines-${PolExistingId}" style="background-color:#fff;margin-top:20px;margin-right:20px;width:100%" width="1200" height="400">
                            </svg>
                       </div>
                 </div>`;
         
            str += `
<div class="row col-12" id="divPlan-${PolExistingId}" style="margin-left:0;margin-right:0;text-align:center;display:none">
  
  <div class="col-md-4" style="padding-left:0;padding-right:0;margin-top:30px;">

    <div class="row" style="margin-left:0;margin-right:0">
      <div class="col-md-7 row PlanTableStyle" style="background-color:#f7f0f0">
        <span>شماره</span>
      </div>
      <div class="col-md-5 row" style="padding-left:0;padding-right:0">
        <div class="col-md-3 PlanTableStyle" style="background-color:#f7f0f0"><span>1</span></div>
        <div class="col-md-3 PlanTableStyle" style="background-color:#f7f0f0"><span>2</span></div>
        <div class="col-md-3 PlanTableStyle" style="background-color:#f7f0f0"><span>3</span></div>
        <div class="col-md-3 PlanTableStyle" style="background-color:#f7f0f0"><span>4</span></div>
      </div>
    </div>

    <div class="row" style="margin-left:0;margin-right:0">
      <div class="col-md-7 row PlanTableStyle" style="background-color:#f7f0f0">
        <span>طول دیوار بالی (w)</span>
      </div>
      <div class="col-md-5 row" style="padding-left:0;padding-right:0">
        <div class="col-md-3 PlanTableStyle"><input id="w1-${PolExistingId}" value="4" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="w2-${PolExistingId}" value="4" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="w3-${PolExistingId}" value="4" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="w4-${PolExistingId}" value="4" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
      </div>
    </div>

    <div class="row" style="margin-left:0;margin-right:0">
      <div class="col-md-7 row PlanTableStyle" style="background-color:#f7f0f0">
        <span>زاویه دیوار بالی</span>
      </div>
      <div class="col-md-5 row" style="padding-left:0;padding-right:0">
        <div class="col-md-3 PlanTableStyle"><input id="alfaw1-${PolExistingId}" value="135" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="alfaw2-${PolExistingId}" value="135" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="alfaw3-${PolExistingId}" value="135" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="alfaw4-${PolExistingId}" value="135" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
      </div>
    </div>

    <div class="row" style="margin-left:0;margin-right:0">
      <div class="col-md-7 row PlanTableStyle" style="background-color:#f7f0f0">
        <span>ارتفاع دیوار بالی در انتهای شیب خاکریز</span>
      </div>
      <div class="col-md-5 row" style="padding-left:0;padding-right:0">
        <div class="col-md-3 PlanTableStyle"><input id="hMinw1-${PolExistingId}" value="1" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="hMinw2-${PolExistingId}" value="1" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="hMinw3-${PolExistingId}" value="1" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
        <div class="col-md-3 PlanTableStyle"><input id="hMinw4-${PolExistingId}" value="1" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
      </div>
    </div>

    <div class="row" style="margin-left:0;margin-right:0">
      <div class="col-md-7 row PlanTableStyle" style="background-color:#f7f0f0;">
        <span>طول آبرو</span>
      </div>
      <div class="col-md-5 row" style="padding-left:0;padding-right:0;">
        <div class="col-md-3 PlanTableStyle"><input id="LAbro-${PolExistingId}" value="8" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
      </div>
    </div>

    <div class="row" style="margin-left:0;margin-right:0">
      <div class="col-md-7 row PlanTableStyle" style="background-color:#f7f0f0">
        <span>زاویه بیه پل</span>
      </div>
      <div class="col-md-5 row" style="padding-left:0;padding-right:0;">
        <div class="col-md-3 PlanTableStyle"><input id="alfa-${PolExistingId}" value="0" class="form-control spanStyleMitraSmall PlanTableTextBox"></div>
      </div>
    </div>

    <div class="row" style="margin-left:0;margin-right:0">
      <div class="col-md-7 row PlanTableStyle" style="background-color:#f7f0f0">
        <span>طول آبرو با احتساب زاویه بیه</span>
      </div>
      <div class="col-md-5 row" style="padding-left:0;padding-right:0;">
        <div class="col-md-3 PlanTableStyle">
          <input id="LAbroWithBie-${PolExistingId}" disabled class="form-control spanStyleMitraSmall PlanTableTextBox">
        </div>
      </div>
    </div>

    <div class="row" style="margin-top:20px;margin-right:0px;margin-left:0px;">
      <div class="col-md-8 row PlanTableStyle" style="background-color:#f7f0f0">
        <span>موقعیت آبرو براساس UTM</span>
      </div>
    </div>

    <div class="row">
      <div class="col-md-6 PlanTableStyle" style="background-color:#f7f0f0"><span>X</span></div>
      <div class="col-md-6">
        <input id="SAbroX-${PolExistingId}" value="0" class="form-control spanStyleMitraSmall PlanTableTextBox">
      </div>
    </div>

    <div class="row">
      <div class="col-md-6 PlanTableStyle" style="background-color:#f7f0f0"><span>Y</span></div>
      <div class="col-md-6">
        <input id="SAbroY-${PolExistingId}" value="0" class="form-control spanStyleMitraSmall PlanTableTextBox">
      </div>
    </div>

    <div class="row" style="margin-top:20px">
      <div class="col-md-8">
        <input id="btnAddEditPol-${PolExistingId}" type="button" value="ثبت نهایی پل" onclick="PolInfoInsert()" class="btnSavePlanStyle">
      </div>
    </div>

  </div>

  <div class="col-md-8" id="svgContainerPlan-${PolExistingId}" style="border:1px solid rgb(129,173,201);overflow:scroll">
    <svg id="svgLinesPlan-${PolExistingId}" style="background-color:#fff" width="600" height="600"></svg>
  </div>
</div>
`;


            //$('#AbnieShow').find('#ViewPol').html(str);
            $('#PolShow-' + PolExistingId).html(str);


            $('#ViewEzafeBahaPol-' + PolExistingId).css('display', 'block');
            $('#drdTedadDahaneh-' + PolExistingId).change(function () {
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
                $('#drdDahaneAbro-' + PolExistingId).html(str);
                $('#drdDahaneAbro-' + PolExistingId).change();
                ///////////
                TedadDahaneh = $('#drdTedadDahaneh-' + PolExistingId).val();
                DahaneAbro = $('#drdDahaneAbro-' + PolExistingId).val();
                TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);
                strNoeBanaii = '';
                if (TedadDarDahaneAbro > 4) {
                    $('#drdNoeBanaiiSangi-' + PolExistingId).hide();
                    $(`input[name="drdNoeBanaii-${PolExistingId}"][value="1"]`).prop("checked", true);
                    //strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option>';
                }
                else {
                    $('#drdNoeBanaiiBotton-' + PolExistingId).show();
                    $(`input[name="drdNoeBanaii-${PolExistingId}"][value="1"]`).prop("checked", true);
                    $('#drdNoeBanaiiSangi-' + PolExistingId).show();

                    //strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
                }
                $('#drdNoeBanaii-' + PolExistingId).html(strNoeBanaii);
                //////////////
                strNahveEjraDal = '';
                //if (DahaneAbro > 3)
                //    strNahveEjraDal = '<option value=\'1\'>درجا</option>';
                //else {
                //    strNahveEjraDal = '<option value=\'1\'>درجا</option><option value=\'2\'>پیش ساخته</option>';
                //}
                //$('#drdNahveEjraDal-' + PolExistingId).html(strNahveEjraDal);

                if (DahaneAbro > 3) {
                    $('#drdNahveEjraDalPishSakhte-' + PolExistingId).hide();
                    $(`input[name="drdNahveEjraDal-${PolExistingId}"][value="1"]`).prop("checked", true);
                }
                else {
                    $('#drdNahveEjraDalPishDarja-' + PolExistingId).show();
                    $('#drdNahveEjraDalPishSakhte-' + PolExistingId).show();
                    $(`input[name="drdNahveEjraDal-${PolExistingId}"][value="1"]`).prop("checked", true);
                }

                ///////////
                //fnGetErtefaKhakriz();
            });


            $('#drdDahaneAbro-' + PolExistingId).change(function () {
                MaxHadAksarErtefaKoole = 0;
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();
                PolExistingId = $('#HDFPolIdForEdit').val();
                str = '';
                HadeAksarErtefaList = '';
                DahaneAbroCurrent = $(this).val();
                TedadDahanehCurrent = $('#drdTedadDahaneh-' + PolExistingId).val();
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

                        //MaxHadAksarErtefaKoole = MaxHadAksarErtefaKoole > HadeAksarErtefa ? MaxHadAksarErtefaKoole : HadeAksarErtefa;

                    }
                });

                //$('#txtHadeAksarErtefa-' + PolExistingId).val(MaxHadAksarErtefaKoole);

                $('#drdHadeAksarErtefa-' + PolExistingId).html(str);
                ///////////
                TedadDahaneh = $('#drdTedadDahaneh-' + PolExistingId).val();
                DahaneAbro = $('#drdDahaneAbro-' + PolExistingId).val();
                TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);
                strNoeBanaii = '';

                if (TedadDarDahaneAbro > 4) {
                    $('#drdNoeBanaiiSangi-' + PolExistingId).hide();
                    $(`input[name="drdNoeBanaii-${PolExistingId}"][value="1"]`).prop("checked", true);

                    //strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option>';
                }
                else {
                    $('#drdNoeBanaiiBotton-' + PolExistingId).show();
                    $('#drdNoeBanaiiSangi-' + PolExistingId).show();
                    $(`input[name="drdNoeBanaii-${PolExistingId}"][value="1"]`).prop("checked", true);

                    //strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
                }


                //if (TedadDarDahaneAbro > 6)
                //    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option>';
                //else {
                //    strNoeBanaii = '<option value=\'1\'>بتن غیر مسلح</option><option value=\'2\'>سنگی</option>';
                //}
                $('#drdNoeBanaii-' + PolExistingId).html(strNoeBanaii);
                //////////////
                strNahveEjraDal = '';
                if (DahaneAbro > 3) {
                    $('#drdNahveEjraDalPishSakhte-' + PolExistingId).hide();
                    $(`input[name="drdNahveEjraDal-${PolExistingId}"][value="1"]`).prop("checked", true);

                    //strNahveEjraDal = '<option value=\'1\'>درجا</option>';
                }
                else {
                    $('#drdNahveEjraDalPishDarja-' + PolExistingId).show();
                    $('#drdNahveEjraDalPishSakhte-' + PolExistingId).show();
                    $(`input[name="drdNahveEjraDal-${PolExistingId}"][value="1"]`).prop("checked", true);

                    //strNahveEjraDal = '<option value=\'1\'>درجا</option><option value=\'2\'>پیش ساخته</option>';
                }
                $('#drdNahveEjraDal-' + PolExistingId).html(strNahveEjraDal);
                //////////
                fnGetErtefaKhakriz();
            });

            $('input[name="drdNoeBanaii-' + PolExistingId + '"]').change(function () {
                ErtefaKhakrizChange();
            });

            $('#drdHadeAksarErtefa-' + PolExistingId).change(function () {
               //////////////چشمک زدن دکمه ذخیره بعد از تغییر
               //clearTimeout(timerAddPol);
                //ChangeSaveButtonColor();
                debugger;
               fnGetErtefaKhakriz();
            });

            // $('#txtHadeAksarErtefa-' + PolExistingId).change(function () {

            //     HadeAksarErtefa = parseFloat($(this).val());

            //     if (HadeAksarErtefa < 1) {
            //         HadeAksarErtefa = 1;
            //     }

            //     if (HadeAksarErtefa > MaxHadAksarErtefaKoole) {
            //         HadeAksarErtefa = MaxHadAksarErtefaKoole;
            //     }

            //     $(this).val(HadeAksarErtefa);
            //     //////////////چشمک زدن دکمه ذخیره بعد از تغییر
            //     clearTimeout(timerAddPol);
            //     ChangeSaveButtonColor();
            //     fnGetErtefaKhakriz();
            // });

            $('#drdErtefaKhakriz-' + PolExistingId).change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                //clearTimeout(timerAddPol);
                //ChangeSaveButtonColor();

                debugger;

                ErtefaKhakrizChange();
            });

            $('#drdNoeBanaii').change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();
            });

            $('#drdNahveEjraDal-' + PolExistingId).change(function () {
                //////////////چشمک زدن دکمه ذخیره بعد از تغییر
                clearTimeout(timerAddPol);
                ChangeSaveButtonColor();

                if ($(this).val() == 2) {
                    $('#alfa').val(0);
                    $('#alfa').attr('disabled', true);
                }
                else
                    $('#alfa').attr('disabled', false);
            });


            strEzafeBaha = `<div id='divAddedItemsForAbnieFaniPol-${PolExistingId}' class='col-md-12' 
                                style='padding: 25px 5px 2px; margin-bottom: 5px; border: 1px solid rgb(196, 212, 219); 
                                border-radius: 4px !important;padding-bottom: 50px;margin-bottom: 20px; text-align: right; display: none;'>
                            </div>`;

            $('#PolShow-' + PolExistingId).append(strEzafeBaha);

            $("div[id^='PolShow-']").each(function () {
                if ($(this).attr('id') != 'PolShow-' + PolExistingId) {
                    $(this).slideUp(500);
                    $(this).removeClass('displayShow');
                }
            });

            $("div[id^='ViewRizMetrePol-']").each(function () {
                if ($(this).attr('id') != 'ViewRizMetrePol-' + PolExistingId) {
                    $(this).hide();
                    $(this).removeClass('displayShow');
                }
            });

            if ($('#PolShow-' + PolExistingId).hasClass('displayShow')) {
                $('#PolShow-' + PolExistingId).removeClass('displayShow');
                $('#PolShow-' + PolExistingId).slideUp(500);
            }
            else {
                $('#PolShow-' + PolExistingId).addClass('displayShow');
                $('#PolShow-' + PolExistingId).slideDown(500);
            }

            if (IsNew == 0) {

                $('#drdHadeAksarErtefa-' + PolExistingId).val(GetHadAksarErtefaKoole);

                fillTedadDahaneh(GetTedadDahaneh, GetDahaneAbro, GetHadAksarErtefaKoole, GetHs, NoeBanaii, NahveEjraDal, IsNew);
                AbnieFaniShowRizMetre(PolExistingId);

                $('#HDFPolIdForEdit').val(PolExistingId);
                $('#HDFPolNum').val(PolNum);
                $('#HDFStatePolSaveOrEdit').val('Edit');
                $('#alfa-' + PolExistingId).val(GetZavieBie);
                $('#LAbro-' + PolExistingId).val(GetToolAbro);
                $('#SAbroX-' + PolExistingId).val(x);
                $('#SAbroY-' + PolExistingId).val(y);
                $('#drdNoeBanaii-' + PolExistingId).val(NoeBanaii);
                $('#drdNahveEjraDal-' + PolExistingId).val(NahveEjraDal);
                $.each(DastakPolInfo, function () {
                    PolVaAbroId = $(this).find("PolVaAbroId").text();
                    if (PolVaAbroId == PolExistingId) {
                        Shomareh = $(this).find("Shomareh").text();
                        ToolW = $(this).find("ToolW").text();
                        Zavie = $(this).find("Zavie").text();
                        hMin = $(this).find("hMin").text();
                        if (Shomareh == 1) {
                            $('#w1-' + PolExistingId).val(ToolW);
                            $('#alfaw1-' + PolExistingId).val(Zavie);
                            $('#hMinw1-' + PolExistingId).val(hMin);
                        }
                        else if (Shomareh == 2) {
                            $('#w2-' + PolExistingId).val(ToolW);
                            $('#alfaw2-' + PolExistingId).val(Zavie);
                            $('#hMinw2-' + PolExistingId).val(hMin);
                        }
                        else if (Shomareh == 3) {
                            $('#w3-' + PolExistingId).val(ToolW);
                            $('#alfaw3-' + PolExistingId).val(Zavie);
                            $('#hMinw3-' + PolExistingId).val(hMin);
                        }
                        else if (Shomareh == 4) {
                            $('#w4-' + PolExistingId).val(ToolW);
                            $('#alfaw4-' + PolExistingId).val(Zavie);
                            $('#hMinw4-' + PolExistingId).val(hMin);
                        }
                    }
                });
                $('#btnCloseExistingPol').click();
            }
            else {
                //$('#HDFPolIdForEdit').val('');
                $('#HDFPolNum').val('');
                $('#HDFStatePolSaveOrEdit').val('Add');
                fillTedadDahaneh(0, 0, 0, 0, 1, 1, 1);
            }
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری مشخصات کوله', 'خطا');
        }
    });
}
function ErtefaKhakrizChange() {
    
    PolExistingId = $('#HDFPolIdForEdit').val();
    ErtefaKhakrizCurrent = $('#drdErtefaKhakriz-' + PolExistingId).val();
    TedadDahane = $('#drdTedadDahaneh-' + PolExistingId).val();
    D = $('#drdDahaneAbro-' + PolExistingId).val();
    H = $('#drdHadeAksarErtefa-' + PolExistingId).val();
    PolExistingId = $('#HDFPolIdForEdit').val();
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
    StartPointX = 200;
    StartPointY = 280;
    if (D >= 3) {
        scale = 1 / 2;
    }
    if (D >= 4 && D < 7) {
        StartPointX = 200;
        StartPointY = 250;
        scale = 1 / 2.5;
    }
    else if (D >= 7 && D < 9) {
        StartPointX = 200;
        StartPointY = 250;
        scale = 1 / 3.5;
    }
    else if (D >= 9) {
        StartPointX = 200;
        StartPointY = 250;
        scale = 1 / 4.5;
    }
    $('#svgLines-' + PolExistingId).html('');


    NoeBanaii = $(`input[name="drdNoeBanaii-${PolExistingId}"]:checked`).val();

    if (TedadDahane == 1) {
        Pol(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100)
            , parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100)
            , parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar,NoeBanaii);
    }
    else if (TedadDahane == 2) {
        Pol2Dahaneh(parseInt(D * 100), parseInt(H * 100), parseInt(a1 * 100), parseInt(a2 * 100), parseInt(b1 * 100), parseInt(b2 * 100),
            parseInt(c1 * 100), parseInt(c2 * 100), parseInt(f * 100), parseInt(m * 100), parseInt(p1 * 100), parseInt(p2 * 100)
            , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar,NoeBanaii);
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
            , parseInt(e * 100), parseInt(n * 100), parseInt(k * 100), parseInt(t * 100), parseInt(j * 100), StartPointX, StartPointY, scale, Meyar,NoeBanaii);
    }
}
function drawingPlan() {
    PolExistingId = $('#HDFPolIdForEdit').val();
    $('#svgLinesPlan-' + PolExistingId).html('');
    ErtefaKhakrizCurrent = $('#drdErtefaKhakriz-' + PolExistingId).val();
    TedadDahane = $('#drdTedadDahaneh-' + PolExistingId).val();
    D = $('#drdDahaneAbro-' + PolExistingId).val();
    H =  $('#drdHadeAksarErtefa-' + PolExistingId).val();//$('#txtHadeAksarErtefa-' + PolExistingId).val();
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
    L = $('#LAbro-' + PolExistingId).val();
    $('#LAbroWithBie-' + PolExistingId).val(($('#LAbro-' + PolExistingId).val() / Math.cos(toRadians($('#alfa-' + PolExistingId).val()))).toFixed(2));
    LWithBie = $('#LAbroWithBie-' + PolExistingId).val();
    //j = 0.02;

    dW1 = parseFloat($('#w1-' + PolExistingId).val()).toFixed(2);
    dW2 = parseFloat($('#w2-' + PolExistingId).val()).toFixed(2);
    dW3 = parseFloat($('#w3-' + PolExistingId).val()).toFixed(2);
    dW4 = parseFloat($('#w4-' + PolExistingId).val()).toFixed(2);

    $('#w1-' + PolExistingId).val(Check_w(dW1));
    $('#w2-' + PolExistingId).val(Check_w(dW2));
    $('#w3-' + PolExistingId).val(Check_w(dW3));
    $('#w4-' + PolExistingId).val(Check_w(dW4));

    w1 = $('#w1-' + PolExistingId).val();
    w2 = $('#w2-' + PolExistingId).val();
    w3 = $('#w3-' + PolExistingId).val();
    w4 = $('#w4-' + PolExistingId).val();
    alfa = $('#alfa-' + PolExistingId).val();

    $('#alfaw1-' + PolExistingId).val(Check_alfaw($('#alfaw1-' + PolExistingId).val()));
    $('#alfaw2-' + PolExistingId).val(Check_alfaw($('#alfaw2-' + PolExistingId).val()));
    $('#alfaw3-' + PolExistingId).val(Check_alfaw($('#alfaw3-' + PolExistingId).val()));
    $('#alfaw4-' + PolExistingId).val(Check_alfaw($('#alfaw4-' + PolExistingId).val()));

    alfaw1 = $('#alfaw1-' + PolExistingId).val();
    alfaw2 = $('#alfaw2-' + PolExistingId).val();
    alfaw3 = $('#alfaw3-' + PolExistingId).val();
    alfaw4 = $('#alfaw4-' + PolExistingId).val();


    $('#hMinw1-' + PolExistingId).val(Check_hminw($('#hMinw1-' + PolExistingId).val()));
    $('#hMinw2-' + PolExistingId).val(Check_hminw($('#hMinw2-' + PolExistingId).val()));
    $('#hMinw3-' + PolExistingId).val(Check_hminw($('#hMinw3-' + PolExistingId).val()));
    $('#hMinw4-' + PolExistingId).val(Check_hminw($('#hMinw4-' + PolExistingId).val()));

    hminw1 = $('#hMinw1-' + PolExistingId).val();
    hminw2 = $('#hMinw2-' + PolExistingId).val();
    hminw3 = $('#hMinw3-' + PolExistingId).val();
    hminw4 = $('#hMinw4-' + PolExistingId).val();

    x = 0.15;
    h = H;

    Canvaswidth = $('#svgLinesPlan-' + PolExistingId).attr('width');
    Canvasheight = $('#svgLinesPlan-' + PolExistingId).attr('height');
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

function Check_alfaw(alfaw) {
    Alfa = parseFloat(alfaw);
    if (isNaN(Alfa)) {
        return 145
    }
    if (Alfa > 180) {
        return 180
    }
    if (Alfa < 90) {
        return 90
    }

    return Alfa;

}

function Check_hminw(hminw) {

    HMin = parseFloat(hminw);
    if (isNaN(hminw)) {
        return 1
    }
    if (hminw < 1) {
        return 1
    }

    return HMin;

}
function Check_w(w) {
    dW = parseFloat(w);
    var currentDahaneAbro = $('#drdDahaneAbro-' + PolExistingId).val();
    dCurrentDahaneAbro = parseFloat(currentDahaneAbro);
    _2D = dW <= dCurrentDahaneAbro * 2 ? dW : dCurrentDahaneAbro * 2;

    if (isNaN(dW) || dW < 1) {
        DahaneAbro = 0;
    } else {
        DahaneAbro = dW > _2D ? _2D : dW;
    }
    return DahaneAbro;
}

function DrawingPlan1Dahaneh(alfa, D, LWithBie, w1, w2, w3, w4, b2, c1, c2, j,
    alfaw1, alfaw2, alfaw3, alfaw4, StartPointX, StartPointY, Scale, Meyar) {


    DrawPlan(alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100), parseInt(w2 * 100),
        parseInt(w3 * 100), parseInt(w4 * 100), parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100),
        parseInt(j * 100), alfaw1, alfaw2, alfaw3, alfaw4, StartPointX, StartPointY, Scale, Meyar);


    if (w1 >= 1) {
        DrawDastakNew1(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w1 * 100),
            parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw1),
            parseInt(hminw1 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);
    }

    if (w2 >= 1) {
        DrawDastakNew2(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w2 * 100),
            parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw2),
            parseInt(hminw2 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);
    }

    if (w3 >= 1) {
        DrawDastakNew3(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w3 * 100),
            parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw3),
            parseInt(hminw3 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);
    }

    if (w4 >= 1) {
        DrawDastakNew4(parseInt(h * 100), parseInt(x * 100), alfa, parseInt(D * 100), parseInt(LWithBie * 100), parseInt(w4 * 100),
            parseInt(b2 * 100), parseInt(c1 * 100), parseInt(c2 * 100), parseInt(j * 100), parseInt(alfaw4),
            parseInt(hminw4 * 100), parseInt(t * 100), StartPointX, StartPointY, Scale, Meyar);
    }
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

