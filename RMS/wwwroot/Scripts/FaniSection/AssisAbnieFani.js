function AbnieFaniClick(OpId) {
    clearTimeout(timerAddPol);
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    AbnieFaniWithBarAvordIdClick(OpId, BarAvordUserID);
}


function PolInfoInsert() {
    PolExistingId = $('#HDFPolIdForEdit').val();
    StatePolSaveOrEdit = $('#HDFStatePolSaveOrEdit').val();
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    TedadDahaneh = $('#drdTedadDahaneh-' + PolExistingId).val();
    DahaneAbro = $('#drdDahaneAbro-' + PolExistingId).val();
    HadeAksarErtefa = $('#drdHadeAksarErtefa-' + PolExistingId).val();
    ErtefaKhakriz = $('#drdErtefaKhakriz-' + PolExistingId).val();
    //NoeBanaii = $('#drdNoeBanaii-' + PolExistingId).val();
    NoeBanaii = $(`input[name="drdNoeBanaii-${PolExistingId}"]:checked`).val();
    NahveEjraDal = $(`input[name="drdNahveEjraDal-${PolExistingId}"]:checked`).val();
    // $('#drdNahveEjraDal-' + PolExistingId).val();
    w1 = $('#w1-' + PolExistingId).val();
    w2 = $('#w2-' + PolExistingId).val();
    w3 = $('#w3-' + PolExistingId).val();
    w4 = $('#w4-' + PolExistingId).val();
    alfaw1 = $('#alfaw1-' + PolExistingId).val();
    alfaw2 = $('#alfaw2-' + PolExistingId).val();
    alfaw3 = $('#alfaw3-' + PolExistingId).val();
    alfaw4 = $('#alfaw4-' + PolExistingId).val();
    hMinw1 = $('#hMinw1-' + PolExistingId).val();
    hMinw2 = $('#hMinw2-' + PolExistingId).val();
    hMinw3 = $('#hMinw3-' + PolExistingId).val();
    hMinw4 = $('#hMinw4-' + PolExistingId).val();
    alfa = $('#alfa-' + PolExistingId).val();
    LAbro = $('#LAbro-' + PolExistingId).val();
    SAbroX = $('#SAbroX-' + PolExistingId).val();
    SAbroY = $('#SAbroY-' + PolExistingId).val();
    if (StatePolSaveOrEdit == 'Add') {
        var vardata = new Object();
        vardata.BarAvordId = BarAvordUserID;
        vardata.TedadDahaneh = TedadDahaneh;
        vardata.DahaneAbro = DahaneAbro;
        vardata.HadeAksarErtefa = HadeAksarErtefa;
        vardata.ErtefaKhakriz = ErtefaKhakriz;
        vardata.NoeBanaii = NoeBanaii;
        vardata.NahveEjraDal = NahveEjraDal;
        vardata.w1 = w1;
        vardata.w2 = w2;
        vardata.w3 = w3;
        vardata.w4 = w4;
        vardata.alfaw1 = alfaw1;
        vardata.alfaw2 = alfaw2;
        vardata.alfaw3 = alfaw3;
        vardata.alfaw4 = alfaw4;
        vardata.hMinw1 = hMinw1;
        vardata.hMinw2 = hMinw2;
        vardata.hMinw3 = hMinw3;
        vardata.hMinw4 = hMinw4;
        vardata.alfa = alfa;
        vardata.LAbro = LAbro;
        vardata.SAbroX = SAbroX;
        vardata.SAbroY = SAbroY;
        $.ajax({
            type: "POST",
            url: "/PolVaAbroBarAvord/SaveAbroInfo",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    debugger;
                    $('#HDFPolIdForEdit').val(info[1]);
                    $('#HDFPolNum').val(info[2]);
                    toastr.success('اطلاعات آبرو بدرستی ثبت گردید', 'موفقیت');

                    ConfirmAbnieFani292(info[1])

                    //ShowQuestionPol();
                }
            },
            error: function (response) {
                toastr.error('مشکل در ذخیره سازی اطلاعات آبرو', 'خطا');
            }
        });
    }
    else {
        PolIdForEdit = $('#HDFPolIdForEdit').val();
        var vardata = new Object();
        vardata.PolId = PolIdForEdit;
        vardata.TedadDahaneh = TedadDahaneh;
        vardata.DahaneAbro = DahaneAbro;
        vardata.HadeAksarErtefa = HadeAksarErtefa;
        vardata.ErtefaKhakriz = ErtefaKhakriz;
        vardata.NoeBanaii = NoeBanaii;
        vardata.NahveEjraDal = NahveEjraDal;
        vardata.w1 = w1;
        vardata.w2 = w2;
        vardata.w3 = w3;
        vardata.w4 = w4;
        vardata.alfaw1 = alfaw1;
        vardata.alfaw2 = alfaw2;
        vardata.alfaw3 = alfaw3;
        vardata.alfaw4 = alfaw4;
        vardata.hMinw1 = hMinw1;
        vardata.hMinw2 = hMinw2;
        vardata.hMinw3 = hMinw3;
        vardata.hMinw4 = hMinw4;
        vardata.alfa = alfa;
        vardata.LAbro = LAbro;
        vardata.SAbroX = SAbroX;
        vardata.SAbroY = SAbroY;
        $.ajax({
            type: "POST",
            url: "/PolVaAbroBarAvord/UpdateAbroInfo",
            //data: '{PolId:' + PolIdForEdit + ',TedadDahaneh:' + "'" + TedadDahaneh + "'"
            //    + ',DahaneAbro:' + "'" + DahaneAbro + "'" + ',HadeAksarErtefa:' + "'" + HadeAksarErtefa + "'"
            //    + ',ErtefaKhakriz:' + "'" + ErtefaKhakriz + "'" + ',NoeBanaii:' + "'" + NoeBanaii + "'"
            //    + ',NahveEjraDal:' + "'" + NahveEjraDal + "'" + ',w1:' + "'" + w1 + "'"
            //    + ',w2:' + "'" + w2 + "'" + ',w3:' + "'" + w3 + "'" + ',w4:' + "'" + w4 + "'"
            //    + ',alfaw1:' + "'" + alfaw1 + "'" + ',alfaw2:' + "'" + alfaw2 + "'" + ',alfaw3:' + "'" + alfaw3 + "'"
            //    + ',alfaw4:' + "'" + alfaw4 + "'" + ',hMinw1:' + "'" + hMinw1 + "'" + ',hMinw2:' + "'" + hMinw2 + "'"
            //    + ',hMinw3:' + "'" + hMinw3 + "'" + ',hMinw4:' + "'" + hMinw4 + "'" + ',alfa:' + "'" + alfa + "'"
            //    + ',LAbro:' + "'" + LAbro + "'" + ',SAbroX:' + "'" + SAbroX + "'" + ',SAbroY:' + "'" + SAbroY + "'"
            //    + '}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    //ConfirmAbnieFani292(info[1]);
                    //clearTimeout(timerAddPol);
                    $('#btnAddEditPol').css('background-color', '#516b92');
                    toastr.success('اطلاعات آبرو بدرستی ویرایش گردید', 'موفقیت');

                    ConfirmAbnieFani292(info[1])

                    //ShowQuestionPol();
                }
            },
            error: function (response) {
                toastr.error('مشکل در ویرایش اطلاعات آبرو', 'خطا');
            }
        });
    }
}



function ShowQuestionPol() {
    Year = $('#HDFYear').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    PolVaAbroId = $('#HDFPolIdForEdit').val();
    strQuestionPol = '';
    var vardata = new Object();
    vardata.PolVaAbroId = PolVaAbroId;
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;
    $.ajax({
        type: "POST",
        url: '/QuesForAbnieFani/GetQuestionForAbnieFani',
        dataType: "json",
        //data: '{PolVaAbroId:' + PolVaAbroId + ',year:' + Year + ',NoeFB:' + NoeFB + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var xmlDoc = $.parseXML(data);
            var xml = $(xmlDoc);
            tblAbnieFaniQueries = xml.find("tblAbnieFaniQueries");
            var tblShomarehFBForQuesForAbnieFani = xml.find("tblShomarehFBForQuesForAbnieFani");
            var tblQuesForAbnieFaniValues = xml.find("tblQuesForAbnieFaniValues");

            strQuestionPol = '<div class=\'col-md-12\'>';
            count = 1;
            $.each(tblAbnieFaniQueries, function () {

                Id = $(this).find("Id").text().trim();
                Question = $(this).find("Question").text().trim();
                HasGetValue = $(this).find("HasGetValue").text().trim();
                DefaultValue = $(this).find("DefaultValue").text().trim();
                ObjectType = $(this).find("ObjectType").text().trim();

                CountShomareh = 1;
                if (ObjectType == '1') {
                    strQuestionPol += '<div class=\'col-md-12\' style=\'padding: 10px;border: 1px solid #ffeeec;border-radius: 10px !important;\'>';
                    strQuestionPol += '<div class=\'col-md-5\'><span>' + count + ' - ' + Question + '</span></div>';
                    if (HasGetValue == 'true') {
                        strQuestionPol += '<div class=\'col-md-1\'><input style=\'text-align:center;padding: 1px;\' id=\'qu' + Id + '\' value=\'' + DefaultValue + '\' type=\'text\' class=\'form-control input-sm\'/></div>';
                    }
                    strQuestionPol += '<div class=\'col-md-12\' style=\'padding: 10px;\'>';
                    $.each(tblShomarehFBForQuesForAbnieFani, function () {

                        strQuestionPol += '<div class=\'col-md-12\'>';
                        QuesForAbnieFaniId = $(this).find("QuesForAbnieFaniId").text().trim();
                        ShomarehFBForQuesId = $(this).find("Id").text().trim();
                        Shomareh = $(this).find("Shomareh").text().trim();
                        Sharh = $(this).find("Sharh").text().trim();
                        if (QuesForAbnieFaniId == Id) {
                            if (CountShomareh == 1)
                                strQuestionPol += '<div class=\'col-md-1\'><input id=\'rd' + ShomarehFBForQuesId + '\' type=\'radio\' checked=\'checked\' name=\'group' + Id + '\'/></div>';
                            else
                                strQuestionPol += '<div class=\'col-md-1\'><input id=\'rd' + ShomarehFBForQuesId + '\' type=\'radio\' name=\'group' + Id + '\'/></div>';
                            strQuestionPol += '<div class=\'col-md-10\'><span onclick=\"$(\'#rd' + ShomarehFBForQuesId + '\').click()\">' + Shomareh + ' - ' + Sharh + '</span></div>';
                        }
                        strQuestionPol += '</div>';
                        CountShomareh++;
                    });
                    strQuestionPol += '</div>';
                    strQuestionPol += '</div>';
                }
                else {
                    strQuestionPol += '<div class=\'col-md-12\' style=\'padding: 10px;\'>';
                    $.each(tblShomarehFBForQuesForAbnieFani, function () {
                        QuesForAbnieFaniId = $(this).find("QuestionForAbnieFaniId").text().trim();
                        if (QuesForAbnieFaniId == Id) {
                            strQuestionPol += '<div class=\'col-md-12\' style=\'border: 1px solid #e7ebed;padding: 5px;border-radius: 5px !important;\'>';
                            ShomarehFBForQuesId = $(this).find("Id").text().trim();
                            Shomareh = $(this).find("Shomareh").text().trim();
                            Sharh = $(this).find("Sharh").text().trim();
                            strQuestionPol += '<div class=\'row\'><div class=\'col-md-11\'><span>' + count + ' - </span>';
                            strQuestionPol += '<input id=\'rd' + ShomarehFBForQuesId + '\' type=\'checkbox\' name=\'group' + Id + '\'/>';
                            strQuestionPol += '<span onclick=\"$(\'#rd' + ShomarehFBForQuesId + '\').click()\">' + Shomareh + ' - ' + Sharh + '</span></div></div>';
                            if (HasGetValue == 'true') {
                                strQuestionPol += '<div class=\'row\'><div class=\'col-md-4\'>' + Question + '</div>';
                                strQuestionPol += '<div class=\'col-md-1\'><input style=\'text-align:center\' id=\'qu' + Id + '\' value=\'' + DefaultValue + '\' type=\'text\' class=\'form-control input-sm\'/></div>';
                                str += '</div>';
                            }
                            strQuestionPol += '</div>';
                        }
                        CountShomareh++;
                    });
                }
                count++;
            });
            strQuestionPol += '</div>';
            $('#divFirstQuestionPol').html(strQuestionPol);
            ///////////////////
            value = 0;
            $.each(tblQuesForAbnieFaniValues, function () {
                QForAFId = $(this).find("QuestionForAbnieFaniId").text().trim();
                ShomarehFBSelectedId = $(this).find("ShomarehFBSelectedId").text().trim();
                value = $(this).find("Value").text().trim();
                if (value != 0) {
                    $('#rd' + ShomarehFBSelectedId).attr('checked', true);
                    $('#qu' + QForAFId).val(parseFloat(value).toFixed(2));
                }
            });
            //////////
            $('#aFirstQuestionPol').click();
        },
        error: function (msg) {
            toastr.error('مشکل در بارگزاری شرایط پل/آبرو', 'خطا');
        }
    });
}


function ConfirmFirstQuestionPol() {
    PolVaAbroId = $('#HDFPolIdForEdit').val();
    strQuesVal = '';
    $.each(tblAbnieFaniQueries, function () {

        Id = $(this).find("Id").text().trim();
        HasGetValue = $(this).find("HasGetValue").text().trim();
        Type = $(this).find("Type").text().trim();
        strQuesVal += "id" + Id;
        blnCheckQues = false;
        blnCheckRadio = false;
        blnCheckInvalidNum = false;
        if (Type == 1) {
            $('#divFirstQuestionPol input').each(function () {
                IdFS = $(this).attr('id').substring(0, 2);
                IdSS = $(this).attr('id').substring(2, $(this).attr('id').length);
                if (IdFS == 'qu' && IdSS == Id) {
                    if ($(this).val() != '' && $(this).val() != '0') {
                        if ($.isNumeric($(this).val())) {
                            strQuesVal += '_vl' + $(this).val();
                            blnCheckQues = true;
                        }
                        else
                            blnCheckInvalidNum = true;
                    }
                }

                if ($(this).attr('type') == 'checkbox' || $(this).attr('type') == 'radio') {
                    nameSS = $(this).attr('name').substring(5, $(this).attr('name').length);
                    if (IdFS == 'rd' && nameSS == Id) {
                        if ($(this).is(':checked')) {
                            strQuesVal += '_qu' + IdSS;
                            blnCheckRadio = true;
                        }
                    }
                }
            });
            strQuesVal += ',';
        }
        else {
            blnCheckRadio = true;
            if ($('#rd' + Id).is(':checked')) {
                $('#divFirstQuestionPol input').each(function () {
                    IdFS = $(this).attr('id').substring(0, 2);
                    IdSS = $(this).attr('id').substring(2, $(this).attr('id').length);
                    if (IdFS == 'qu' && IdSS == Id) {
                        if ($(this).val() != '' && $(this).val() != '0') {
                            if ($.isNumeric($(this).val())) {
                                strQuesVal += '_vl' + $(this).val();
                                blnCheckQues = true;
                            }
                            else
                                blnCheckInvalidNum = true;
                        }
                    }

                    if ($(this).attr('type') == 'checkbox' || $(this).attr('type') == 'radio') {
                        nameSS = $(this).attr('name').substring(5, $(this).attr('name').length);
                        if (IdFS == 'rd' && nameSS == Id) {
                            if ($(this).is(':checked')) {
                                strQuesVal += '_qu' + IdSS;
                                blnCheckRadio = true;
                            }
                        }
                    }
                });
                strQuesVal += ',';
            }
            else {
                blnCheckQues = true;
            }
        }

        blnFinalCheck = true;
        if (blnCheckInvalidNum) {
            toastr.info('مقادیر وارد شده نامعتبر میباشد', 'اطلاع');
            blnFinalCheck = false;
        }
        else {
            if (HasGetValue == 'true') {
                if (!blnCheckQues) {
                    toastr.info('هیچکدام از مقادیر مشخص شده نبایستی خالی باشد', 'اطلاع');
                    blnFinalCheck = false;
                }

            }
            if (!blnCheckRadio) {
                toastr.info('لطفا موارد انتخاب شونده را انتخاب نمایید', 'اطلاع');
                blnFinalCheck = false;
            }
        }
    });
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    if (blnFinalCheck) {
        var vardata = new Object();
        vardata.PolVaAbroId = PolVaAbroId;
        vardata.QuesVal = strQuesVal;
        $.ajax({
            type: "POST",
            url: '/QuesForAbnieFaniValues/ConfirmQuesForAbnieFaniValues',
            dataType: "json",
            //data: '{PolVaAbroId:' + PolVaAbroId + ',QuesVal:' + "'" + strQuesVal + "'" + '}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {
                    toastr.success('اطلاعات آبرو/پل بدرستی ثبت گردید', 'موفقیت');
                    $('#btnCloseFirstQuestionPol').click();
                    ConfirmAbnieFani292(PolVaAbroId)
                }
                else
                    toastr.info('مشکل در درج اطلاعات آبرو/پل جدید', 'اطلاع');
            },
            error: function (msg) {
                toastr.error('مشکل در درج اطلاعات آبرو/پل جدید', 'خطا');
            }
        });
    }
}


function ConfirmAbnieFani292(PolId) {
    AddOrEdit= $('#HDFStatePolSaveOrEdit').val();
    if (AddOrEdit == 'Add') {
        PolVaAbroId = 'New';
    }
    else
        PolVaAbroId = PolId;

    LAbro = $('#LAbro-' + PolVaAbroId).val();
    LAbroWithBie = $('#LAbroWithBie-' + PolVaAbroId).val();
    w1 = $('#w1-' + PolVaAbroId).val();
    w2 = $('#w2-' + PolVaAbroId).val();
    w3 = $('#w3-' + PolVaAbroId).val();
    w4 = $('#w4-' + PolVaAbroId).val();
    f = $('#HDFPolf').val();
    m = $('#HDFPolm').val();
    n = $('#HDFPoln').val();
    k = $('#HDFPolk').val();
    h = $('#drdHadeAksarErtefa-' + PolVaAbroId).val();
    t = $('#HDFPolt').val();
    b1 = $('#HDFPolb1').val();
    b2 = $('#HDFPolb2').val();
    c1 = $('#HDFPolc1').val();
    c2 = $('#HDFPolc2').val();
    j = $('#HDFPolj').val();
    p1 = $('#HDFPolp1').val();
    p2 = $('#HDFPolp2').val();
    D = $('#drdDahaneAbro-' + PolVaAbroId).val();
    //NoeBanaii = $('#drdNoeBanaii-' + PolVaAbroId).val();
    Hs = $('#drdErtefaKhakriz-' + PolVaAbroId).val();
    //NahveEjraDal = $('#drdNahveEjraDal-' + PolVaAbroId).val();
    NoeBanaii = $(`input[name="drdNoeBanaii-${PolExistingId}"]:checked`).val();
    NahveEjraDal = $(`input[name="drdNahveEjraDal-${PolExistingId}"]:checked`).val();
    TedadDahaneh = $('#drdTedadDahaneh-' + PolVaAbroId).val();
    LPayeMoarab = $('#HDFLPayeMoarab').val();
    LKooleMoarab = $('#HDFLKooleMoarab').val();

    LW1j = $('#HDFLW1j').val();
    LW1p = $('#HDFLW1p').val();
    LB1W1 = $('#HDFLB1W1').val();
    LB2W1 = $('#HDFLB2W1').val();
    LW2j = $('#HDFLW2j').val();
    LW2p = $('#HDFLW2p').val();
    LB1W2 = $('#HDFLB1W2').val();
    LB2W2 = $('#HDFLB2W2').val();
    LW3j = $('#HDFLW3j').val();
    LW3p = $('#HDFLW3p').val();
    LB1W3 = $('#HDFLB1W3').val();
    LB2W3 = $('#HDFLB2W3').val();
    LW4j = $('#HDFLW4j').val();
    LW4p = $('#HDFLW4p').val();
    LB1W4 = $('#HDFLB1W4').val();
    LB2W4 = $('#HDFLB2W4').val();

    hMinw1 = $('#hMinw1-' + PolVaAbroId).val();
    hMinw2 = $('#hMinw2-' + PolVaAbroId).val();
    hMinw3 = $('#hMinw3-' + PolVaAbroId).val();
    hMinw4 = $('#hMinw4-' + PolVaAbroId).val();

    PolNum = $('#HDFPolNum').val();
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.PolVaAbroId = PolId;
    vardata.PolNum = parseInt(PolNum);
    vardata.BarAvordId = BarAvordUserID;
    vardata.LAbro = LAbroWithBie;
    vardata.w1 = w1; vardata.w2 = w2; vardata.w3 = w3; vardata.w4 = w4;
    vardata.f = f; vardata.m = m; vardata.n = n; vardata.k = k;
    vardata.h = h; vardata.t = t;
    vardata.b1 = b1; vardata.b2 = b2;
    vardata.c1 = c1; vardata.c2 = c2;
    vardata.j = j;
    vardata.p1 = p1; vardata.p2 = p2;
    vardata.D = D;
    vardata.Hs = Hs;
    vardata.TedadDahaneh = TedadDahaneh;
    vardata.NahveEjraDal = NahveEjraDal;
    vardata.LPayeMoarab = LPayeMoarab;
    vardata.LKooleMoarab = LKooleMoarab;
    vardata.NoeBanaii = NoeBanaii;
    vardata.LW1j = LW1j; vardata.LW1p = LW1p;
    vardata.LB1W1 = LB1W1; vardata.LB2W1 = LB2W1;
    vardata.LW2j = LW2j; vardata.LW2p = LW2p;
    vardata.LB1W2 = LB1W2; vardata.LB2W2 = LB2W2;
    vardata.LW3j = LW3j; vardata.LW3p = LW3p;
    vardata.LB1W3 = LB1W3; vardata.LB2W3 = LB2W3;
    vardata.LW4j = LW4j; vardata.LW4p = LW4p;
    vardata.LB1W4 = LB1W4; vardata.LB2W4 = LB2W4;
    vardata.hMinw1 = hMinw1; vardata.hMinw2 = hMinw2;
    vardata.hMinw3 = hMinw3; vardata.hMinw4 = hMinw4;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: '/RizMetreUserAbnieFani/ConfirmRizMetreUsersForAbnieFani',
        dataType: "json",
        //data: '{PolVaAbroId:' + PolId + ',PolNum:' + PolNum + ',BarAvordID:' + BarAvordID + ',LAbro:' + "'" + LAbroWithBie + "'"
        //    + ',w1:' + "'" + w1 + "'" + ',w2:' + "'" + w2 + "'" + ',w3:' + "'" + w3 + "'" + ',w4:' + "'" + w4 + "'"
        //    + ',f:' + "'" + f + "'" + ',m:' + "'" + m + "'" + ',n:' + "'" + n + "'"
        //    + ',k: ' + "'" + k + "'" + ',h:' + "'" + h + "'" + ',t:' + "'" + t + "'" + ',b1:' + "'" + b1 + "'"
        //    + ',b2:' + "'" + b2 + "'" + ',c1:' + "'" + c1 + "'" + ',c2:' + "'" + c2 + "'" + ',j:' + "'" + j + "'"
        //    + ',p1:' + "'" + p1 + "'" + ',p2:' + "'" + p2 + "'" + ',D:' + "'" + D + "'" + ',Hs:' + "'" + Hs + "'"
        //    + ',TedadDahaneh:' + "'" + TedadDahaneh + "'" + ',NahveEjraDal:' + "'" + NahveEjraDal + "'"
        //    + ',LPayeMoarab:' + "'" + LPayeMoarab + "'" + ',LKooleMoarab:' + "'" + LKooleMoarab + "'"
        //    + ',NoeBanaii:' + "'" + NoeBanaii + "'"
        //    + ',LW1j:' + "'" + LW1j + "'" + ',LW1p:' + "'" + LW1p + "'" + ',LB1W1:' + "'" + LB1W1 + "'" + ',LB2W1:' + "'" + LB2W1 + "'"
        //    + ',LW2j:' + "'" + LW2j + "'" + ',LW2p:' + "'" + LW2p + "'" + ',LB1W2:' + "'" + LB1W2 + "'" + ',LB2W2:' + "'" + LB2W2 + "'"
        //    + ',LW3j:' + "'" + LW3j + "'" + ',LW3p:' + "'" + LW3p + "'" + ',LB1W3:' + "'" + LB1W3 + "'" + ',LB2W3:' + "'" + LB2W3 + "'"
        //    + ',LW4j:' + "'" + LW4j + "'" + ',LW4p:' + "'" + LW4p + "'" + ',LB1W4:' + "'" + LB1W4 + "'" + ',LB2W4:' + "'" + LB2W4 + "'"
        //    + ',hMinw1:' + "'" + hMinw1 + "'" + ',hMinw2:' + "'" + hMinw2 + "'" + ',hMinw3:' + "'" + hMinw3 + "'" + ',hMinw4:' + "'" + hMinw4 + "'"
        //    + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data.split('_');
            if (info[0] == "OK") {
                clearTimeout(timerAddPol);
                $('#btnAddEditPol').css('background-color', '#516b92');
                toastr.success('آیتم های فهرست بهایی آبرو/پل بدرستی ثبت گردید', 'موفقیت');
                //$('#HDFStatePolSaveOrEdit').val('Edit');

                $('#btnCloseFormCUB').click();
                OperationId = $('#HDFOperationId').val();
                ShowExistingPol(BarAvordUserID, OperationId)
                //AbnieFaniShowRizMetre(PolId);

            }
            else
                toastr.info('مشکل در درج آیتم های فهرست بهایی آبرو/پل جدید', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در درج آیتم های فهرست بهایی آبرو/پل جدید', 'خطا');
        }
    });
}


function SaveEzafeBahaForAbnieFaniPol(strItemsForAdd, QuestionForAbnieFaniId) {
    ItemFBForAdd = $('#spanShomareh' + QuestionForAbnieFaniId).html();
    PolVaAbroId = $('#HDFPolIdForEdit').val();
    PolNum = $('#HDFPolNum').val();
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.PolVaAbroId = PolVaAbroId
    vardata.PolNum = PolNum;
    vardata.BarAvordUserID = BarAvordUserID;
    vardata.QuestionForAbnieFaniId = QuestionForAbnieFaniId;
    vardata.ItemsForAdd = strItemsForAdd;
    vardata.ItemFBForAdd = ItemFBForAdd;
    vardata.LevelNumber = 1;
    $.ajax({
        type: "POST",
        url: "/RizMetreUserAbnieFani/SaveEzafeBahaForAbnieFaniPol",
        //data: '{PolVaAbroId:' + PolVaAbroId + ',PolNum:' + PolNum + ',BarAvordUserID:' + BarAvordUserID + ',QuestionForAbnieFaniId:' + QuestionForAbnieFaniId
        //    + ',ItemsForAdd:' + "'" + strItemsForAdd + "'" + ',ItemFBForAdd:' + "'" + ItemFBForAdd + "'" + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('اضافه بها با مقادیر پیش فرض ثبت گردید', 'موفقیت');
                ShowFormsAddedItemsAbnieFaniPol(BarAvordUserID);
            }
        },
        error: function (response) {
            toastr.error('مشکل در ثبت اضافه بها پل', 'خطا');
        }
    });
}

function UpdateEzafeBahaAbnieFaniPol(Id, ItemShomareh, Hajm, Meghdar) {
    ItemFBForUpdate = $('#spanShomareh' + Id).html();
    PolVaAbroId = $('#HDFPolIdForEdit').val();
    PolNum = $('#HDFPolNum').val();
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BarAvordUserID = BarAvordUserID;
    vardata.PolVaAbroId = PolVaAbroId;
    vardata.PolNum = PolNum;
    vardata.ItemFBForUpdate = ItemFBForUpdate;
    vardata.ItemShomareh = ItemShomareh;
    vardata.Hajm = Hajm;
    vardata.Meghdar = Meghdar;
    $.ajax({
        type: "POST",
        url: "/RizMetreUserAbnieFani/UpdateEzafeBahaForAbnieFaniPol",
        //data: '{BarAvordUserID:' + BarAvordUserID + ',PolVaAbroId:' + PolVaAbroId + ',PolNum:' + PolNum + ',ItemFBForUpdate:' + "'" + ItemFBForUpdate + "'"
        //     + ',ItemShomareh:' + "'" + ItemShomareh + "'" + ',Hajm:' + "'" + Hajm + "'"
        //     + ',Meghdar:' + "'" + Meghdar + "'" + '}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('اضافه بها انتخابی بدرستی ویرایش گردید', 'موفقیت');
            }
        },
        error: function (response) {
            toastr.error('مشکل در ویرایش اضافه بها انتخابی', 'خطا');
        }
    });
}

function DeleteEzafeBahaForAbnieFaniPolForItem(strItemsForAdd, QuestionId) {
    PolVaAbroId = $('#HDFPolIdForEdit').val();
    BarAvordUserID = $('#HDFBarAvordUserID').val();
    var vardata = new Object();
    vardata.BarAvordUserID = BarAvordUserID;
    vardata.PolVaAbroId = PolVaAbroId;
    vardata.QuestionId = QuestionId;
    vardata.ItemFBForDel = "120701";

    $.ajax({
        type: "POST",
        url: "/QuesForAbnieFaniValues/DeleteEzafeBahaForAbnieFaniPol",
        data: JSON.stringify(vardata),
        //data: '{BarAvordID:' + BarAvordID + ',PolVaAbroId:' + PolVaAbroId
        //    + ',QuestionId:' + QuestionId + ',ItemFBForDel:' + "'120701'" + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                $('#divSubEzafeBahaAbnieFaniPol' + Id).hide();
                toastr.success('اضافه بها با بدرستی حذف گردید', 'موفقیت');
            }
        },
        error: function (response) {
            toastr.error('مشکل در حذف اضافه بها پل', 'خطا');
        }
    });
}
