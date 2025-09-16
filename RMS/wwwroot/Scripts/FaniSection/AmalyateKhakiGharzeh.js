//////////////////قرضه
////////////////////
///////////////////
function GharzehWithBarAvordClick(OpId, BarAvordUserId) {
    str = '';
    str += '<div class=\'row\' id=\'ViewGharzeh\'></div>';
    $('#GharzehShow').find('#divShowGharzeh').html(str);
    $('#aGharzehShow').click();

    //$('#ula' + OpId).html(str);
    ShowSelctionGharzeh(1, 0, 0, BarAvordUserId, 0, 0, 0)
}

function ShowSelctionGharzeh(IsNew, XYExistingId, XYNum, BarAvordUserId, X, Y, Value) {
    str = '<div class=\'col-md-12 row\'>';
    str += '<div class=\'col-md-12 row\' style=\'border: 1px solid #c0c4e2;border-radius: 5px !important;padding: 5px 0px;\'>';
    str += '<div class=\'col-md-2\' style=\'text-align: left;\'><span>مختصات محل قرضه</span></div>';
    str += '<div class=\'col-md-1\' style=\'text-align: left;\'><span>X: </span></div>';
    str += '<div class=\'col-md-1\'><input style=\'text-align:center;padding:0px;font-size: 16px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtXForGharzeh\' value=\'0\'/></div>';
    str += '<div class=\'col-md-1\' style=\'text-align:left;\'><span>Y: </span></div><div class=\'col-md-1\'><input style=\'text-align: center;padding: 0px;font-size: 16px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtYForGharzeh\' value=\'0\'/></div>';
    str += '<div class=\'col-md-1\' style=\'text-align:left;\'><a class=\'NewPolStyle\' onclick=\"SaveGharzehInfo('+"'" + BarAvordUserId+"'" + ')\">ذخیره</a></div>';
    str += '</div></div>';

    //////////////////////////////
    str += '<div class=\'col-md-12 row\' id=\'divGharzehInfoDetails\' style=\'margin-top:10px;padding:0px;\'>';
    str += '<div class=\'col-md-12 row\'>';
    str += '<div class=\'col-md-12 row\' style=\'padding:5px 0px 0px;border:1px solid #b1d3ec;border-radius:10px;background-color: #d3e4fc;font-size: 10px;\'>';
    str += '<div class=\'col-md-4\'><div class=\'col-md-12\'></div></div>';
    str += '<div class=\'col-md-8 row\'>';
    str += '<div class=\'col-md-4 row\'>';
    str += '<div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;\'><span>حجم خاکبرداری</span></div>';
    str += '<div class=\'col-md-6\' style=\'text-align: center;\'><span>متر مکعب</span></div>';
    str += '<div class=\'col-md-6\' style=\'text-align: center;\'><span>درصد</span></div></div>';
    str += '<div class=\'col-md-3 row\' style=\'text-align:center\'><div style=\'padding: 0px;\' class=\'col-md-12\'><span id=\'spanKhakBardariItemsHeader1\' class=\'spanStyleKhakBardariItemsHeader\'>اضافه بها حمل</span></div><div class=\'col-md-12\' style=\'text-align: center;padding: 0px;\'><span>20 تا 50 متر</span></div></div>';
    str += '</div></div></div>';

    ///"خاکبرداری نباتی در زمین محل قرضه",
    ActivityTitle = [ "خاکبرداری در زمین نرم", "خاکبرداری در زمین دج", "خاکبرداری در زمین سنگی"];
    ////////////////
    for (var i = 1; i < 4; i++) {
        str += '<div class=\'col-md-12 row\'>';
        str += '<div class=\'col-md-12 row\' style=\'padding:2px 0px;margin:2px 0px;border:1px solid #ccc;border-radius:10px\'>';
        str += '<div class=\'col-md-4\' style=\'padding: 0px;text-align: left;z-index:555;text-align:center\'><span style=\'font-size: 10px;\'>' + ActivityTitle[i - 1] + '</span></div>';
        str += '<div class=\'col-md-8 row\'>';
        str += '<div class=\'col-md-4 row\' style=\'padding: 0px;\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtGhDetail' + i + '\' value=\'0\'/></div>';
        str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtGhDarsad' + i + '\' value=\'0\'/></div></div>';
        str += '<div class=\'col-md-3\'><div class=\'col-md-12\' style=\'padding: 0px 2px;text-align: center;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'checkbox\' id=\'ckGhFaseleHaml' + i + '\' /></div></div>';
        str += '</div></div></div>';
    }

    $('#ViewGharzeh').html(str);

    $('#txtGhDetail1').attr('disabled', true);
    $('#txtGhDetail2').attr('disabled', true);
    $('#txtGhDetail3').attr('disabled', true);
    $('#txtGhDarsad3').attr('disabled', true);
    //$('#txtGhDarsad1').attr('disabled', true);

    $('#txtXForGharzeh').change(function () {
        if (!$.isNumeric($(this).val())) {
            check = true;
            $(this).addClass('ErrorValueStyle');
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });

    $('#txtYForGharzeh').change(function () {
        if (!$.isNumeric($(this).val())) {
            check = true;
            $(this).addClass('ErrorValueStyle');
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });

    $('#txtGhDarsad1').change(function () {
        RemainPercent = 100 - parseFloat($('#txtGhDarsad2').val());
        CurrentDarsad = parseFloat($(this).val());

        sumAll = parseFloat($('#txtGhDetail1').val()) + parseFloat($('#txtGhDetail2').val())
                    + parseFloat($('#txtGhDetail3').val());

        if (CurrentDarsad>RemainPercent) {
            $(this).val(RemainPercent.toFixed(2));
            $('#txtGhDarsad3').val(0);

            $('#txtGhDetail2').val((parseFloat($('#txtGhDarsad2').val()) * sumAll / 100).toFixed(2));
            $('#txtGhDetail3').val(0);
        }
        else
        {
            $('#txtGhDetail1').val((parseFloat($('#txtGhDarsad1').val()) * sumAll / 100).toFixed(2));

            $('#txtGhDarsad2').val((RemainPercent - CurrentDarsad).toFixed(2));
            $('#txtGhDetail2').val(((RemainPercent - CurrentDarsad) * sumAll / 100).toFixed(2));
        }
    });

    $('#txtGhDarsad2').change(function () {
        RemainPercent = 100 - parseFloat($('#txtGhDarsad3').val());
        CurrentDarsad = parseFloat($(this).val());

        sumAll = parseFloat($('#txtGhDetail1').val()) + parseFloat($('#txtGhDetail2').val())
                    + parseFloat($('#txtGhDetail3').val());

        if (CurrentDarsad > RemainPercent) {
            $(this).val(RemainPercent.toFixed(2));
            $('#txtGhDarsad1').val(0);

            $('#txtGhDetail2').val((parseFloat($('#txtGhDarsad2').val()) * sumAll / 100).toFixed(2));
            $('#txtGhDetail1').val(0);
        }
        else {
            $('#txtGhDetail2').val((parseFloat($('#txtGhDarsad2').val()) * sumAll / 100).toFixed(2));

            $('#txtGhDarsad1').val((RemainPercent - CurrentDarsad).toFixed(2));
            $('#txtGhDetail1').val(((RemainPercent - CurrentDarsad) * sumAll / 100).toFixed(2));
        }
    });

    //$('#txtGhDetail1').change(function () {
    //    if (!$.isNumeric($(this).val())) {
    //        $(this).addClass('ErrorValueStyle');
    //        toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
    //    }
    //    else {
    //        $(this).removeClass('ErrorValueStyle');

    //        pGhDetail1 = parseFloat($('#txtGhDetail1').val() == '' ? 0 : $.trim($('#txtGhDetail1').val()));
    //        pGhDetail2 = parseFloat($('#txtGhDetail2').val() == '' ? 0 : $.trim($('#txtGhDetail2').val()));
    //        pGhDetail3 = parseFloat($('#txtGhDetail3').val() == '' ? 0 : $.trim($('#txtGhDetail3').val()));
    //        pGhDetail4 = parseFloat($('#txtGhDetail4').val() == '' ? 0 : $.trim($('#txtGhDetail4').val()));
    //        sumAll = pGhDetail1 + pGhDetail2 + pGhDetail3 + pGhDetail4;

    //        CurrentValue = parseFloat($.trim($(this).val()) == '' ? 0 : $.trim($(this).val()));
    //        $('#txtGhDarsad1').val((CurrentValue / sumAll * 100).toFixed(2));
    //        $('#txtGhDarsad2').val((parseFloat($('#txtGhDetail2').val()) / sumAll * 100).toFixed(2));
    //        $('#txtGhDarsad3').val((parseFloat($('#txtGhDetail3').val()) / sumAll * 100).toFixed(2));
    //        $('#txtGhDarsad4').val((parseFloat($('#txtGhDetail4').val()) / sumAll * 100).toFixed(2));
    //    }

    //});

    CalculateValuesForGharzeh(BarAvordUserId);
}

function LoadInfoGharzeh(BarAvordUserId) {
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/GetGharzehInfoForBarAvord",
        //data: '{BarAvordId:' + BarAvordId + '}',
        data:JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            var GharzehInfo = xml.find("tblGharzehInfo");

            var MGharzehBarAvordDetails = xml.find("tblMGharzehBarAvordDetails");
            var MGharzehBarAvordDetailsMore = xml.find("tblMGharzehBarAvordDetailsMore");
            var MGharzehBarAvordDetailsEzafeBaha = xml.find("tblMGharzehBarAvordDetailsEzafeBaha");
            if (GharzehInfo.length != 0) {
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
                $.each(GharzehInfo, function () {
                    $('#txtXForGharzeh').val($(this).find("_FromKM").text());
                    $('#txtYForGharzeh').val($(this).find("_ToKM").text());
                    $('#HDFKMAmalyateKhakiIdForEdit').val($(this).find("_ID").text());
                    $('#HDFKMAmalyateKhakiNum').val($(this).find("_KMNum").text());
                    $.each(MGharzehBarAvordDetails, function () {
                        Id = $(this).find("_Id").text();
                        $.each(MGharzehBarAvordDetailsMore, function () {
                            CurrentId = $(this).find("_Id").text();
                            Name = $.trim($(this).find("_Name").text());
                            ValueMore = $(this).find("_Value").text();
                            AmalyateKhakiInfoForBarAvordDetailsId = $(this).find("_AmalyateKhakiInfoForBarAvordDetailsId").text();
                            if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                                $('#txt' + Name).val(ValueMore);
                            }
                        });
                        $.each(MGharzehBarAvordDetailsEzafeBaha, function () {
                            CurrentId = $(this).find("_Id").text();
                            Name = $.trim($(this).find("_Name").text());
                            boolValue = $(this).find("_Value").text() == 'true' ? true : false;
                            AmalyateKhakiInfoForBarAvordDetailsId = $(this).find("_AmalyateKhakiInfoForBarAvordDetailsId").text();
                            if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                                $('#ck' + Name).attr('checked', boolValue);
                            }
                        });
                    });
                });

                ////////////////
                ///////
                //////////////
                sumAll = parseFloat($('#txtGhDetail1').val()) + parseFloat($('#txtGhDetail2').val())
                            + parseFloat($('#txtGhDetail3').val());

                $('#txtGhDetail2').val((parseFloat($('#txtGhDarsad2').val()) * sumAll / 100).toFixed(2));
                $('#txtGhDetail3').val((parseFloat($('#txtGhDarsad3').val()) * sumAll / 100).toFixed(2));
                $('#txtGhDarsad1').val((parseFloat($('#txtGhDetail1').val()) / sumAll * 100).toFixed(2));

                //////////////////////
                //////////
                /////////////////
            }
            else
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Add');
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اطلاعات قرضه', 'خطا');
        }
    });
}

function CalculateValuesForGharzeh(BarAvordUserId) {
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/CalculateValuesForGharzeh",
        //data: '{BarAvordId:' + BarAvordId + '}',
        data:JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            var ValueForGharzeh = xml.find("tblValueForGharzeh");
            $.each(ValueForGharzeh, function () {
                GharzehKhBSangi = $(this).find("GharzehKhBSangi").text();
                GharzehKhBNarmSakht = $(this).find("GharzehKhBNarmSakht").text();

                fGharzehKhBSangi = parseFloat(GharzehKhBSangi);
                fGharzehKhBNarmSakht = parseFloat(GharzehKhBNarmSakht);
                fSumAllGharzeh = fGharzehKhBSangi + fGharzehKhBNarmSakht;

                //if (fGharzehKhBSangi > 0 && fGharzehKhBNarmSakht > 0) {
                $('#txtGhDetail4').val(fGharzehKhBSangi.toFixed(2));
                $('#txtGhDetail2').val((fGharzehKhBNarmSakht / 2).toFixed(2));
                $('#txtGhDetail3').val((fGharzehKhBNarmSakht / 2).toFixed(2));

                $('#txtGhDarsad2').val(((fGharzehKhBNarmSakht / 2) / fSumAllGharzeh * 100).toFixed(2));
                $('#txtGhDarsad3').val(((fGharzehKhBNarmSakht / 2) / fSumAllGharzeh * 100).toFixed(2));
                $('#txtGhDarsad4').val((fGharzehKhBSangi / fSumAllGharzeh * 100).toFixed(2));
                //}

                LoadInfoGharzeh(BarAvordUserId);
            });
        },
        error: function (response) {
            toastr.error('مشکل در ثبت اطلاعات قرضه', 'خطا');
        }
    });
}

function SaveGharzehInfo(BarAvordUserId) {
    check = false;

    GhX = $('#txtXForGharzeh').val();
    GhY = $('#txtYForGharzeh').val();
    //////////
    if (!$.isNumeric(GhX)) {
        check = true;
        $(this).addClass('ErrorValueStyle');
    }
    else
        $(this).removeClass('ErrorValueStyle');

    if (!$.isNumeric(GhY)) {
        check = true;
        $(this).addClass('ErrorValueStyle');
    }
    else
        $(this).removeClass('ErrorValueStyle');

    $('#divKhakBardariInfoDetails input[type="text"]').each(function () {
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('ErrorValueStyle');
            check = true;
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });
    if (!check) {
        //DetailValue1 = $('#txtGhDetail1').val();
        //ckFaseleHaml1 = $('#ckGhFaseleHaml1').is(":checked");
        /////////
        Darsad1 = $('#txtGhDarsad1').val();
        ckFaseleHaml1 = $('#ckGhFaseleHaml1').is(":checked");
        /////////
        Darsad2 = $('#txtGhDarsad2').val();
        ckFaseleHaml2 = $('#ckGhFaseleHaml2').is(":checked");
        /////////
        ckFaseleHaml3 = $('#ckGhFaseleHaml3').is(":checked");
        /////////
        StateGharzehSaveOrEdit = $('#HDFStateAmalyateKhakiSaveOrEdit').val();
        if (StateGharzehSaveOrEdit == 'Add') {
            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId; vardata.Type = 4; vardata.X = GhX; vardata.Y = GhY;
            vardata.Darsad1 = Darsad1; vardata.ckFaseleHaml1 = ckFaseleHaml1;
            vardata.Darsad2 = Darsad2; vardata.ckFaseleHaml2 = ckFaseleHaml2;
            vardata.ckFaseleHaml3 = ckFaseleHaml3;
            $.ajax({
                type: "POST",
                url: "/AmalyateKhakiInfoForBarAvords/SaveGharzehInfoForBarAvord",
                data:JSON.stringify(vardata),
                //data: '{BarAvordId:' + BarAvordId + ',Type:4,X:' + GhX + ',Y:' + GhY
                //        + ',Darsad1:' + "'" + Darsad1 + "'" + ',ckFaseleHaml1:' + ckFaseleHaml1
                //        + ',Darsad2:' + "'" + Darsad2 + "'" + ',ckFaseleHaml2:' + ckFaseleHaml2
                //        + ',ckFaseleHaml3:' + ckFaseleHaml3
                //        + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    info = response.split('_');
                    if (info[0] == "OK") {
                        $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
                        $('#HDFKMAmalyateKhakiIdForEdit').val(info[1]);
                        $('#HDFKMAmalyateKhakiNum').val(info[2]);
                        toastr.success('اطلاعات قرضه بدرستی ثبت گردید', 'ثبت');
                    }
                    else
                        toastr.error('مشکل در ثبت اطلاعات قرضه', 'خطا');
                },
                error: function (response) {
                    toastr.error('مشکل در ثبت اطلاعات قرضه', 'خطا');
                }
            });
        }
        else {
            debugger;
            MGharzehId = $('#HDFKMAmalyateKhakiIdForEdit').val();
            MGharzehNum = $('#HDFKMAmalyateKhakiNum').val();
            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId; vardata.MGharzehId = MGharzehId; vardata.MGharzehNum = MGharzehNum;
            vardata.X = GhX; vardata.Y = GhY;
            vardata.Darsad1 = Darsad1; vardata.ckFaseleHaml1 = ckFaseleHaml1;
            vardata.Darsad2 = Darsad2; vardata.ckFaseleHaml2 = ckFaseleHaml2;
            vardata.ckFaseleHaml3 = ckFaseleHaml3;
            debugger;
            $.ajax({
                type: "POST",
                url: "/AmalyateKhakiInfoForBarAvords/UpdateGharzehInfoForBarAvord",
                //data: '{BarAvordId:' + BarAvordId + ',MGharzehId:' + MGharzehId + ',MGharzehNum:' + MGharzehNum + ',X:' + GhX + ',Y:' + GhY
                //        + ',Darsad1:' + "'" + Darsad1 + "'" + ',ckFaseleHaml1:' + ckFaseleHaml1
                //        + ',Darsad2:' + "'" + Darsad2 + "'" + ',ckFaseleHaml2:' + ckFaseleHaml2
                //        + ',ckFaseleHaml3:' + ckFaseleHaml3
                //        + '}',
                data:JSON.stringify(vardata),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    info = response.split('_');
                    if (info[0] == "OK") {
                        toastr.success('اطلاعات قرضه بدرستی ویرایش گردید', 'ثبت');
                    }
                    else
                        toastr.error('مشکل در ویرایش اطلاعات قرضه', 'خطا');
                },
                error: function (response) {
                    toastr.error('مشکل در ویرایش اطلاعات قرضه', 'خطا');
                }
            });
        }
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}
/////////////////پی کنی
////////////////////
///////////////////
function PayKaniWithBarAvordClick(OpId, BarAvordUserId) {
    str = '';
    str += '<div class=\'row\' style=\'margin-top:3px;\'><div class=\'col-md-3 row\'><a class=\'NewPolStyle\' onclick=\"ShowExistingPolForPayKani('+"'" + BarAvordUserId +"'"+ ')\">لیست پلهای موجود</a></div>';
    str += '</div>';
    str += '<div class=\'row\' style=\'margin-top: 30px; max-height: 450px; overflow: auto\' id=\'ViewPayKani\'></div>';
    $('#PayKaniShow').find('#divShowPayKani').html(str);
    $('#aPayKaniShow').click();

    //$('#ula' + OpId).html(str);
}
var DastakPolInfo = null;

function ShowSelctionPolForPaykani(PolExistingId, PolNum, GetTedadDahaneh, GetDahaneAbro, GetHadAksarErtefaKoole, GetHs
    , GetZavieBie, GetToolAbro, x, y, NoeBanaii, NahveEjraDal, BarAvordUserId) {
    $('#btnCloseExistingPolForPayKani').click();

    $('#HDFTedadDahaneh').val(GetTedadDahaneh);

    $('#HDFPolNum').val(PolNum);
    $('#HDFPolIdForEdit').val(PolExistingId);
    //////////////////////////////
    str = '<div class=\'col-md-12 row\'>';
    str += '<div class=\'col-md-12 row\' style=\'text-align:left;\'><a class=\'NewPolStyle\' onclick=\"SavePayKaniInfo('+"'" + BarAvordUserId+"'" + ')\">ذخیره</a></div>';
    str += '</div>';
    str += '<div class=\'col-md-12 row\'>';
    str += '<div class=\'col-md-12 row\' style=\'padding:5px 0px 0px;border:1px solid #b1d3ec;border-radius:10px;background-color: #d3e4fc;font-size: 10px;\'>';
    str += '<div class=\'col-md-2 row\' style=\'padding: 0px;text-align: left;z-index:555;text-align:center\'></div>';
    str += '<div class=\'col-md-1\' style=\'text-align: center;\'><span>تعداد</span></div>';
    str += '<div class=\'col-md-1\' style=\'text-align: center;\'><span>طول</span></div>';
    str += '<div class=\'col-md-1\' style=\'text-align: center;\'><span>عرض</span></div>';
    str += '<div class=\'col-md-1\' style=\'text-align: center;\'><span>ارتفاع</span></div>';
    str += '<div class=\'col-md-1\' style=\'text-align: center;\'><span>مقدار</span></div>';
    str += '<div class=\'col-md-2 row\' style=\'text-align: center;\'>';
    str += '<div class=\'col-md-12\' style=\'border-bottom: 1px solid #98b3c3;\'><span>عملیات خاکی با دست</span></div>';
    str += '<div class=\'col-md-6\'><span>حجم</span></div>';
    str += '<div class=\'col-md-6\'><span>درصد</span></div></div>';
    str += '<div class=\'col-md-2 row\' style=\'text-align: center;\'>';
    str += '<div class=\'col-md-12\' style=\'border-bottom: 1px solid #98b3c3;\'><span>عملیات خاکی با ماشین</span></div>';
    str += '<div class=\'col-md-6\'><span>حجم</span></div>';
    str += '<div class=\'col-md-6\'><span>درصد</span></div></div>';
    str += '<div class=\'col-md-1\' style=\'text-align: center;\'>';
    str += '<div class=\'col-md-12\'><span id=\'spanErtefaPayKani\'>ارتفاع پی کنی</span></div>';
    str += '<div class=\'col-md-12\'><span>زیر تراز آب (متر)</span></div>';
    str += '</div></div></div>';

    ActivityTitle = ["کوله", "پایه میانی", "دستک 1", "دستک 2", "دستک 3", "دستک 4"];

    ////////////////

    Toolw1 = 0;
    Toolw2 = 0;
    Toolw3 = 0;
    Toolw4 = 0;
    hMinW1 = 0;
    hMinW2 = 0;
    hMinW3 = 0;
    hMinW4 = 0;
    $.each(DastakPolInfo, function () {
        PolVaAbroId = $(this).find("_PolVaAbroId").text();
        if (PolVaAbroId == PolExistingId) {
            Shomareh = $(this).find("_Shomareh").text();
            ToolW = $(this).find("_ToolW").text();
            Zavie = $(this).find("_Zavie").text();
            hMin = $(this).find("_hMin").text();
            if (Shomareh == 1) {
                Toolw1 = ToolW;
                hMinW1 = hMin;
            }
            else if (Shomareh == 2) {
                Toolw2 = ToolW;
                hMinW2 = hMin;

            }
            else if (Shomareh == 3) {
                Toolw3 = ToolW;
                hMinW3 = hMin;

            }
            else if (Shomareh == 4) {
                Toolw4 = ToolW;
                hMinW4 = hMin;

            }
        }
    });
    /////////
    /////////
    /////////
    f = 0;
    m = 0;
    n = 0;
    k = 0;
    t = 0;
    var vardata = new Object();
    vardata.Id = GetHs;
    $.ajax({
        type: "POST",
        url: "/AbadeKoole/GetAbadKooleInfoWithId",
        //data: '{Id:' + GetHs + '}',
        data:JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            AbadeKoole = xml.find("AbadeKoole");
            AbadDivarBali = xml.find("AbadDivarBali");
            $.each(AbadeKoole, function () {
                Id = $(this).find("_ID").text();
                Hs = $(this).find("_Hs").text().toString().replace(/\>/g, '').replace(/\</g, '');
                a1 = $(this).find("_a1").text();
                a2 = $(this).find("_a2").text();
                b1 = $(this).find("_b1").text();
                b2 = $(this).find("_b2").text();
                c1 = $(this).find("_c1").text();
                c2 = $(this).find("_c2").text();
                f = $(this).find("_f").text();
                m = $(this).find("_m").text();
                p1 = $(this).find("_p1").text();
                p2 = $(this).find("_p2").text();
                e = $(this).find("_e").text();
                n = $(this).find("_n").text();
                k = $(this).find("_k").text();
                t = $(this).find("_t").text();
                j = $(this).find("_DerzEnbesat").text();
            });

            /////////
            xh = 0;
            ht = parseFloat(GetHadAksarErtefaKoole) + parseFloat(t);
            hRound = Math.round(ht);
            fMax = 0;
            mMax = 0;
            $.each(AbadDivarBali, function () {
                x = $(this).find("_x").text();
                h = $(this).find("_h").text();
                fAbadDivarBali = $(this).find("_f").text();
                m = $(this).find("_m").text();
                if (h == hRound) {
                    xh = x;
                    fMax = fAbadDivarBali;
                    mMax = m;
                }
            });

            Tool1 = (parseFloat(GetToolAbro) / Math.cos(toRadians(parseFloat(GetZavieBie)))).toFixed(2);
            Ertefa = 0;
            Arz = 0;

            ToolFree = 0;
            ArzFree = 0;

            str += '<div class=\'col-md-12\' id=\'divPayKaniInfo\'>';
            for (var i = 1; i < 7; i++) {
                Meghdar = 0;
                if (parseInt(GetTedadDahaneh) - 1 == 0 && i == 2) {
                }
                else {
                    str += '<div class=\'col-md-12 row\' style=\'padding:2px 0px;margin:2px 0px;border:1px solid #ccc;border-radius:10px\'>';
                    str += '<div class=\'col-md-2\' style=\'padding: 0px;text-align: left;z-index:555;text-align:center\'><span id=\'spanPayKaniItems' + i + '\' class=\'spanStyleKhakBardariItems\' style=\'font-size: 10px;\'>' + ActivityTitle[i - 1] + '</span></div>';

                    str += '<div class=\'col-md-1\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' disabled=\'disabled\' id=\'txtTedadPK' + i + '\' value=\'0\'/></div>';
                    str += '<div class=\'col-md-1\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' disabled=\'disabled\' id=\'txtToolPK' + i + '\' value=\'0\'/></div>';
                    str += '<div class=\'col-md-1\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' disabled=\'disabled\' id=\'txtArzPK' + i + '\' value=\'0\'/></div>';
                    str += '<div class=\'col-md-1\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' disabled=\'disabled\' id=\'txtErtefaPK' + i + '\' value=\'0\'/></div>';
                    str += '<div class=\'col-md-1\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtValuePK' + i + '\' value=\'0\'/></div>';
                    str += '<div class=\'col-md-2 row\' style=\'padding: 0px 2px;\'><div class=\'col-md-6 row\' style=\'padding-left:5px;padding-right:5px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtValueKDHPK' + i + '\' value=\'0\'/></div>';
                    str += '<div class=\'col-md-6 row\' style=\'padding-left:5px;padding-right:5px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtValueKDDPK' + i + '\' value=\'20\'/></div></div>';
                    str += '<div class=\'col-md-2 row\' style=\'padding: 0px 2px;\'><div class=\'col-md-6 row\' style=\'padding-left:5px;padding-right:5px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtValueKMHPK' + i + '\' value=\'0\'/></div>';
                    str += '<div class=\'col-md-6 row\' style=\'padding-left:5px;padding-right:5px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtValueKMDPK' + i + '\' value=\'80\'/></div></div>';
                    str += '<div class=\'col-md-1\' style=\'padding: 0px 2px;text-align: center;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtErtefaZirTarazPK' + i + '\' value=\'0\'/></div>';
                    str += '</div>';
                    str += '<div class=\'col-md-12 row\' id=\'divPkValue' + i + '\' style=\'padding-left: 0px;padding-right: 0px;border: 1px solid #bbc9e3;\'>'
                    str += '<div class=\'col-md-12 row clsValuePK\' id=\'divPkValueRizZir2_' + i + '\' style=\'padding-left: 0px;padding-right: 0px;margin: 1px 0px;display:none\'><div class=\'col-md-4 row\'></div><div class=\'col-md-2\' style=\'text-align: left;font-size: 10px;\'><span>حجم پی کنی کانال تا عمق 2 متر: </span></div><div class=\'col-md-1\'><input type=\'text\' id=\'txtPkValueRizZir2_' + i + '\' style=\'text-align:center\' class=\'form-control input-sm\'/></div></div>';
                    str += '<div class=\'col-md-12 row clsValuePK\' id=\'divPkValueRiz2ta3_' + i + '\' style=\'padding-left: 0px;padding-right: 0px;margin: 1px 0px;display:none\'><div class=\'col-md-4 row\'></div><div class=\'col-md-2\' style=\'text-align: left;font-size: 10px;\'><span>حجم پی کنی کانال، عمق 2 تا 3 متر: </span></div><div class=\'col-md-1\'><input type=\'text\' id=\'txtPkValueRiz2ta3_' + i + '\' style=\'text-align:center\' class=\'form-control input-sm\'/></div></div>';
                    str += '<div class=\'col-md-12 row clsValuePK\' id=\'divPkValueRiz3ta4_' + i + '\' style=\'padding-left: 0px;padding-right: 0px;margin: 1px 0px;display:none\'><div class=\'col-md-4 row\'></div><div class=\'col-md-2\' style=\'text-align: left;font-size: 10px;\'><span>حجم پی کنی کانال، عمق 3 تا 4 متر: </span></div><div class=\'col-md-1\'><input type=\'text\' id=\'txtPkValueRiz3ta4_' + i + '\' style=\'text-align:center\' class=\'form-control input-sm\'/></div></div>';
                    str += '<div class=\'col-md-12 row clsValuePK\' id=\'divPkValueRiz4ta5_' + i + '\' style=\'padding-left: 0px;padding-right: 0px;margin: 1px 0px;display:none\'><div class=\'col-md-4 row\'></div><div class=\'col-md-2\' style=\'text-align: left;font-size: 10px;\'><span>حجم پی کنی کانال، عمق 4 تا 5 متر: </span></div><div class=\'col-md-1\'><input type=\'text\' id=\'txtPkValueRiz4ta5_' + i + '\' style=\'text-align:center\' class=\'form-control input-sm\'/></div></div>';
                    str += '<div class=\'col-md-12 row clsValuePK\' id=\'divPkValueRiz5ta6_' + i + '\' style=\'padding-left: 0px;padding-right: 0px;margin: 1px 0px;display:none\'><div class=\'col-md-4 row\'></div><div class=\'col-md-2\' style=\'text-align: left;font-size: 10px;\'><span>حجم پی کنی کانال، عمق 5 تا 6 متر: </span></div><div class=\'col-md-1\'><input type=\'text\' id=\'txtPkValueRiz5ta6_' + i + '\' style=\'text-align:center\' class=\'form-control input-sm\'/></div></div>';
                    str += '<div class=\'col-md-12 row clsValuePK\' id=\'divPkValueRiz6ta7_' + i + '\' style=\'padding-left: 0px;padding-right: 0px;margin: 1px 0px;display:none\'><div class=\'col-md-4 row\'></div><div class=\'col-md-2\' style=\'text-align: left;font-size: 10px;\'><span>حجم پی کنی کانال، عمق 6 تا 7 متر: </span></div><div class=\'col-md-1\'><input type=\'text\' id=\'txtPkValueRiz6ta7_' + i + '\' style=\'text-align:center\' class=\'form-control input-sm\'/></div></div>';
                    str += '</div>';
                }
            }
            str += '</div>';
            //////////////
            $('#ViewPayKani').html(str);
            str = '';
            str += '<div class=\'col-md-12 row\' id=\'divPayKaniInfoDetails\' style=\'margin-top:10px;padding:0px;\'>';
            str += '<div class=\'col-md-12 row\'>';
            str += '<div class=\'col-md-12 row\' style=\'padding:5px 0px 0px;border:1px solid #b1d3ec;border-radius:10px;background-color: #d3e4fc;font-size: 10px;\'>';
            str += '<div class=\'col-md-2\'><div class=\'col-md-12\'></div></div>';
            str += '<div class=\'col-md-10 row\'>';
            str += '<div class=\'col-md-6 row\'>';
            str += '<div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;padding-left: 0px;padding-right: 0px;\'><span>پی کنی با هر وسیله مکانیکی</span></div>';
            str += '<div class=\'col-md-4 row\' style=\'padding-right: 0px;\'>';
            str += '<div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;padding-left: 0px;padding-right: 0px;\'><span>در زمین خاکی</span></div>';
            str += '<div class=\'col-md-6\' style=\'text-align: center;padding-left: 0px;padding-right: 0px;\'><span>متر مکعب</span></div>';
            str += '<div class=\'col-md-6\' style=\'text-align: center;padding-left: 0px;padding-right: 0px;\'><span>درصد</span></div></div>';
            str += '<div class=\'col-md-4 row\' style=\'padding-right: 0px;\'><div style=\'border-bottom: 1px solid #98b3c3;padding: 0px;text-align: center;padding-left: 0px;padding-right: 0px;\' class=\'col-md-12\'><span>در زمین لجنی</span></div><div class=\'col-md-6\' style=\'text-align: center;padding-left: 0px;padding-right: 0px;\'><span>متر مکعب</span></div>';
            str += '<div class=\'col-md-6\' style=\'text-align: center;padding-left: 0px;padding-right: 0px;\'><span>درصد</span></div></div>';
            str += '<div class=\'col-md-4 row\' style=\'padding-left: 0px;\'><div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;padding-left: 0px;padding-right: 0px;\'><span>در زمین سنگی پیکور</span></div><div class=\'col-md-6\' style=\'text-align: center;padding-left: 0px;padding-right: 0px;\'><span>متر مکعب</span></div>';
            str += '<div class=\'col-md-6\' style=\'text-align: center;padding-left: 0px;padding-right: 0px;\'><span>درصد</span></div></div>';
            str += '</div>';
            str += '<div class=\'col-md-6 row\'>';
            str += '<div class=\'col-md-12 row\' style=\'height: 22px;\'></div>';
            str += '<div class=\'col-md-4 row\' style=\'padding-right: 0px;padding-left: 0px;\'><div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;\'><span>واریزه</span></div><div class=\'col-md-6\' style=\'text-align: center;padding-right: 0px;padding-left: 0px;\'><span>متر مکعب</span></div>';
            str += '<div class=\'col-md-6\' style=\'text-align: center;padding-right: 0px;padding-left: 0px;\'><span>درصد</span></div></div>';
            str += '<div class=\'col-md-4 row\' style=\'padding-right: 0px;\'><div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;\'><span>حمل به دپو/مسیر</span></div><div class=\'col-md-6\' style=\'text-align: center;padding-right: 0px;padding-left: 0px;\'><span>متر مکعب</span></div>';
            str += '<div class=\'col-md-6\' style=\'text-align: center;padding-right: 0px;padding-left: 0px;\'><span>درصد</span></div></div>';
            str += '<div class=\'col-md-4 row\' style=\'padding-left: 0px;\'><div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;\'><span>ریختن خاک با مصالح سنگی به درون آنها</span></div><div class=\'col-md-6\' style=\'text-align: center;padding-right: 0px;padding-left: 0px;\'><span>متر مکعب</span></div>';
            str += '<div class=\'col-md-6\' style=\'text-align: center;padding-right: 0px;padding-left: 0px;\'><span>درصد</span></div></div>';
            str += '</div>';
            str += '</div></div></div>';

            ActivityTitle = ["کوله", "پایه میانی", "دستک 1", "دستک 2", "دستک 3", "دستک 4"];

            PolNum = $('#HDFPolNum').val();
            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId;
            vardata.PolNum = PolNum;
            vardata.Type = 5;
            $.ajax({
                type: "POST",
                url: "/AmalyateKhakiInfoForBarAvords/GetDetailsOfKMPayKaniInfoWithPolNum",
                //data: '{BarAvordId:' + BarAvordId + ',PolNum:' + PolNum + ',Type:5}',
                data:JSON.stringify(vardata),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var xmlDoc = $.parseXML(response);
                    var xml = $(xmlDoc);
                    var KMAmalyateKhakiBarAvordDetails = xml.find("tblKMAmalyateKhakiBarAvordDetails");
                    var KMAmalyateRizeshBarAvordDetailsMore = xml.find("tblKMAmalyateKhakiBarAvordDetailsMore");
                    ////////////////
                    for (var i = 1; i < 7; i++) {
                        if (parseInt(GetTedadDahaneh) - 1 == 0 && i == 2) {
                        }
                        else {
                            RikhtanValue = 0
                            RikhtanDarsad = 0
                            Khaki = 0;

                            Tedad = 0;
                            Tool = 0;
                            Arz = 0;
                            Ertefa = 0;
                            Meghdar = 0;
                            if (KMAmalyateKhakiBarAvordDetails.length == 0) {
                                if (i == 1) {
                                    Tedad = 2;
                                    ToolFree = parseFloat(Tool1);
                                    ArzFree = parseFloat(f)
                                    if (ht > 1.5) {
                                        Tool = (parseFloat(Tool1) + parseFloat('0.5')).toFixed(2);
                                        Arz = parseFloat(f) + parseFloat('1')
                                    }
                                    else {
                                        Tool = (parseFloat(Tool1) + parseFloat('0.3')).toFixed(2);
                                        Arz = parseFloat(f) + parseFloat('0.6')
                                    }
                                    Ertefa = ht;

                                    RikhtanValue = Tedad * parseFloat(ToolFree) * parseFloat(ArzFree) * parseFloat(Ertefa);
                                }
                                else if (i == 2) {
                                    Tedad = parseInt(GetTedadDahaneh) - 1;
                                    ToolFree = parseFloat(Tool1);
                                    ArzFree = parseFloat(k)
                                    if (ht > 1.5) {
                                        Tool = (parseFloat(Tool1) + parseFloat('0.5')).toFixed(2);
                                        Arz = parseFloat(k) + parseFloat('1')
                                    }
                                    else {
                                        Tool = (parseFloat(Tool1) + parseFloat('0.3')).toFixed(2);
                                        Arz = parseFloat(k) + parseFloat('0.6')
                                    }
                                    Ertefa = ht;

                                    RikhtanValue = Tedad * parseFloat(ToolFree) * parseFloat(ArzFree) * parseFloat(Ertefa);

                                }
                                else if (i == 3 || i == 4 || i == 5 || i == 6) {
                                    Tedad = 1;
                                    switch (i) {
                                        case 3: {
                                            if (ht > 1.5)
                                                Tool = (parseFloat(Toolw1) + parseFloat('0.5')).toFixed(2);
                                            else
                                                Tool = (parseFloat(Toolw1) + parseFloat('0.3')).toFixed(2);

                                            //xhMin = 0;
                                            fMin = 0;
                                            hMinRound = Math.round(hMinW1);
                                            $.each(AbadDivarBali, function () {
                                                f = $(this).find("_f").text();
                                                h = $(this).find("_h").text();
                                                if (h == hMinRound) {
                                                    //xhMin = x;
                                                    fMin = f;
                                                }
                                            });

                                            ToolFree = parseFloat(Toolw1);
                                            ArzFree = parseFloat(fMin)
                                            hMinW = hMinW1;
                                            //h1 = (parseFloat(ht) / 3) + parseFloat('0.35') + parseFloat(xh);
                                            //h2 = (parseFloat(hMinW1) / 3) + parseFloat('0.35') + parseFloat(xhMin);
                                            if ((parseFloat(ht) + parseFloat(hMinW1))/2 > 1.5)
                                                Arz = parseFloat(fMin) + parseFloat("1");// ((h1 + h2) / 2) + parseFloat("1");
                                            else
                                                Arz = parseFloat(fMin) + parseFloat("0.6");
                                            break;
                                        }
                                        case 4: {
                                            if (ht > 1.5)
                                                Tool = (parseFloat(Toolw2) + parseFloat('0.5')).toFixed(2);
                                            else
                                                Tool = (parseFloat(Toolw2) + parseFloat('0.3')).toFixed(2);

                                            fMin = 0;
                                            hMinRound = Math.round(hMinW2);
                                            $.each(AbadDivarBali, function () {
                                                f = $(this).find("_f").text();
                                                h = $(this).find("_h").text();
                                                if (h == hMinRound) {
                                                    fMin = f;
                                                }
                                            });

                                            ToolFree = parseFloat(Toolw2);
                                            ArzFree = parseFloat(fMin)

                                            hMinW = hMinW2;
                                            if ((parseFloat(ht) + parseFloat(hMinW2))/2 > 1.5)
                                                Arz = parseFloat(fMin) + parseFloat("1");
                                            else
                                                Arz = parseFloat(fMin) + parseFloat("0.6");

                                            break;
                                        }
                                        case 5: {
                                            if (ht > 1.5)
                                                Tool = (parseFloat(Toolw3) + parseFloat('0.5')).toFixed(2);
                                            else
                                                Tool = (parseFloat(Toolw3) + parseFloat('0.3')).toFixed(2);

                                            fMin = 0;
                                            hMinRound = Math.round(hMinW3);
                                            $.each(AbadDivarBali, function () {
                                                f = $(this).find("_f").text();
                                                h = $(this).find("_h").text();
                                                if (h == hMinRound) {
                                                    fMin = f;
                                                }
                                            });

                                            ToolFree = parseFloat(Toolw3);
                                            ArzFree = parseFloat(fMin);

                                            hMinW = hMinW3;
                                            if ((parseFloat(ht) + parseFloat(hMinW3))/2 > 1.5)
                                                Arz = parseFloat(fMin) + parseFloat("1");
                                            else
                                                Arz = parseFloat(fMin) + parseFloat("0.6");

                                            break;
                                        }
                                        case 6: {
                                            if (ht > 1.5)
                                                Tool = (parseFloat(Toolw4) + parseFloat('0.5')).toFixed(2);
                                            else
                                                Tool = (parseFloat(Toolw4) + parseFloat('0.3')).toFixed(2);

                                            fMin = 0;
                                            hMinRound = Math.round(hMinW4);
                                            $.each(AbadDivarBali, function () {
                                                f = $(this).find("_f").text();
                                                h = $(this).find("_h").text();
                                                if (h == hMinRound) {
                                                    fMin = f;
                                                }
                                            });

                                            ToolFree = parseFloat(Toolw4);
                                            ArzFree = parseFloat(fMin)

                                            hMinW = hMinW4;
                                            if ((parseFloat(ht) + parseFloat(hMinW4))/2 > 1.5)
                                                Arz = parseFloat(fMin) + parseFloat("1");
                                            else
                                                Arz = parseFloat(fMin) + parseFloat("0.6");
                                            break;
                                        }
                                        default:
                                    }
                                    Ertefa = (parseFloat(ht) + parseFloat(hMinW))/2;
                                    RikhtanValue = parseFloat(ToolFree) * parseFloat(ArzFree) * parseFloat(Ertefa);
                                }
                                Meghdar = parseFloat(Tool) * parseFloat(Arz) * parseFloat(Ertefa) * Tedad;
                                $('#txtTedadPK' + i).val(Tedad);
                                $('#txtToolPK' + i).val(Tool);
                                $('#txtArzPK' + i).val(parseFloat(Arz).toFixed(2));
                                $('#txtErtefaPK' + i).val(Ertefa);
                                $('#txtValuePK' + i).val(parseFloat(Meghdar).toFixed(2));
                                $('#txtValueKDHPK' + i).val((80 * parseFloat(Meghdar) / 100).toFixed(2));
                                $('#txtValueKDDPK' + i).val(80);
                                $('#txtValueKMHPK' + i).val((20 * parseFloat(Meghdar) / 100).toFixed(2));
                                $('#txtValueKMDPK' + i).val(20);
                            }
                            ValuePK = parseFloat($('#txtValuePK' + i).val());
                            RikhtanDarsad = (ValuePK - RikhtanValue) / parseFloat(ValuePK) * 100;
                            Varize = (ValuePK * 30 / 100).toFixed(2);
                            DarsadHaml = (100 - (parseFloat(RikhtanDarsad) + 30)).toFixed(2);
                            MeghdarHaml = (parseFloat(DarsadHaml) * ValuePK / 100).toFixed(2);
                            Khaki = 0;
                            KhakiDarsad = 0;
                            str += '<div class=\'col-md-12 row\'>';
                            str += '<div class=\'col-md-12 row\' style=\'padding:2px 0px;margin:2px 0px;border:1px solid #ccc;border-radius:10px\'>';
                            str += '<div class=\'col-md-2\' style=\'padding: 0px;text-align: left;z-index:555;text-align:center\'><span id=\'spanPayKaniItems' + i + '\' class=\'spanStyleKhakBardariItems\' style=\'font-size: 10px;\'>' + ActivityTitle[i - 1] + '</span></div>';
                            str += '<div class=\'col-md-10 row\'>';
                            str += '<div class=\'col-md-2 row\' style=\'padding: 0px;\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKKhaki' + i + '\' value=\'' + Khaki.toFixed(2) + '\'/></div>';
                            str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKDarsadKhaki' + i + '\' value=\'' + KhakiDarsad + '\'/></div></div>';
                            str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKLajani' + i + '\' value=\'0\'/></div>';
                            str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKDarsadLajani' + i + '\' value=\'0\'/></div></div>';
                            str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKSangi' + i + '\' value=\'0\'/></div>';
                            str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKDarsadSangi' + i + '\' value=\'0\'/></div></div>';
                            str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKVarize' + i + '\' value=\'' + Varize + '\'/></div>';
                            str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKDarsadVarize' + i + '\' value=\'30\'/></div></div>';
                            str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKHaml' + i + '\' value=\'' + MeghdarHaml + '\'/></div>';
                            str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtPKDarsadHaml' + i + '\' value=\'' + DarsadHaml + '\'/></div></div>';
                            str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' disabled=\'disabled\' id=\'txtPKRikhtan' + i + '\' value=\'' + (ValuePK - RikhtanValue).toFixed(2) + '\'/><input id=\'HajmBotonPK' + i
                                + '\' style=\'display:none\' type=\'text\' value=\'' + RikhtanValue + '\'/></div>';
                            str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' disabled=\'disabled\' id=\'txtPKDarsadRikhtan' + i + '\' value=\'' + RikhtanDarsad.toFixed(2) + '\'/></div></div>';
                            str += '</div></div></div>';
                        }
                    }

                    $('#ViewPayKani').append(str);
                    if (KMAmalyateKhakiBarAvordDetails.length != 0) {
                        for (var i = 1; i < 7; i++) {
                           if (i == 1) {
                                Tedad = 2;
                                ToolFree = parseFloat(Tool1);
                                ArzFree = parseFloat(f)
                                Ertefa = ht;
                                RikhtanValue = Tedad * parseFloat(ToolFree) * parseFloat(ArzFree) * parseFloat(Ertefa);
                            }
                            else if (i == 2) {
                                Tedad = parseInt(GetTedadDahaneh) - 1;
                                ToolFree = parseFloat(Tool1);
                                ArzFree = parseFloat(k)
                                Ertefa = ht;
                                RikhtanValue =Tedad* parseFloat(ToolFree) * parseFloat(ArzFree) * parseFloat(Ertefa);
                            }
                            else if (i == 3 || i == 4 || i == 5 || i == 6) {
                                Tedad = 1;
                                switch (i) {
                                    case 3: {
                                        fMin = 0;
                                        hMinw = hMinW4;
                                        hMinRound = Math.round(hMinW1);
                                        $.each(AbadDivarBali, function () {
                                            f = $(this).find("_f").text();
                                            h = $(this).find("_h").text();
                                            if (h == hMinRound) {
                                                fMin = f;
                                            }
                                        });
                                        ToolFree = parseFloat(Toolw1);
                                        ArzFree = parseFloat(fMin)
                                        break;
                                    }
                                    case 4: {
                                        fMin = 0;
                                        hMinw = hMinW4;
                                        hMinRound = Math.round(hMinW2);
                                        $.each(AbadDivarBali, function () {
                                            f = $(this).find("_f").text();
                                            h = $(this).find("_h").text();
                                            if (h == hMinRound) {
                                                fMin = f;
                                            }
                                        });
                                        ToolFree = parseFloat(Toolw2);
                                        ArzFree = parseFloat(fMin)
                                        break;
                                    }
                                    case 5: {
                                        fMin = 0;
                                        hMinw = hMinW4;
                                        hMinRound = Math.round(hMinW3);
                                        $.each(AbadDivarBali, function () {
                                            f = $(this).find("_f").text();
                                            h = $(this).find("_h").text();
                                            if (h == hMinRound) {
                                                fMin = f;
                                            }
                                        });
                                        ToolFree = parseFloat(Toolw3);
                                        ArzFree = parseFloat(fMin)
                                        break;
                                    }
                                    case 6: {
                                        fMin = 0;
                                        hMinw = hMinW4;
                                        hMinRound = Math.round(hMinW4);
                                        $.each(AbadDivarBali, function () {
                                            f = $(this).find("_f").text();
                                            h = $(this).find("_h").text();
                                            if (h == hMinRound) {
                                                fMin = f;
                                            }
                                        });
                                        ToolFree = parseFloat(Toolw4);
                                        ArzFree = parseFloat(fMin)
                                        break;
                                    }
                                    default:
                                }
                                Ertefa = parseFloat(ht) + parseFloat(hMinw);
                                RikhtanValue = parseFloat(ToolFree) * parseFloat(ArzFree) * parseFloat(Ertefa);
                            }
                           $('#txtTedadPK' + i).val(Tedad);
                           $('#HajmBotonPK' + i).val(RikhtanValue);
                       }
                        $.each(KMAmalyateKhakiBarAvordDetails, function () {
                            Id = $(this).find("_ID").text();
                            AmalyateKhakiInfoForBarAvordId = $(this).find("_AmalyateKhakiInfoForBarAvordId").text();
                            Type = $(this).find("_Type").text();
                            $.each(KMAmalyateRizeshBarAvordDetailsMore, function () {
                                CurrentId = $(this).find("_ID").text();
                                Name = $.trim($(this).find("_Name").text());
                                ValueMore = $(this).find("_Value").text();
                                AmalyateKhakiInfoForBarAvordDetailsId = $(this).find("_AmalyateKhakiInfoForBarAvordDetailsId").text();
                                if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                                    $('#txt' + Name + Type).val(ValueMore);
                                }
                            });

                            ValuePK = $('#txtValuePK' + Type).val();
                            ValueKDHPK = $('#txtValueKDHPK' + Type).val();
                            ValueKMHPK = $('#txtValueKMHPK' + Type).val();

                            $('#txtValueKDDPK' + Type).val(parseFloat(ValuePK) == 0 ? 0 : (parseFloat(ValueKDHPK) / parseFloat(ValuePK) * 100).toFixed(2));
                            $('#txtValueKMDPK' + Type).val(parseFloat(ValuePK) == 0 ? 0 : (parseFloat(ValueKMHPK) / parseFloat(ValuePK) * 100).toFixed(2));

                            ValueKMHPK = $('#txtValueKMHPK' + Type).val();
                            PKKhaki = $('#txtPKKhaki' + Type).val();
                            PKLajani = $('#txtPKLajani' + Type).val();
                            PKSangi = $('#txtPKSangi' + Type).val();
                            
                            $('#txtPKDarsadKhaki' + Type).val(parseFloat(ValueKMHPK) == 0 ? 0 : (parseFloat(PKKhaki) / parseFloat(ValueKMHPK) * 100).toFixed(2));
                            $('#txtPKDarsadLajani' + Type).val(parseFloat(ValueKMHPK) == 0 ? 0 : (parseFloat(PKLajani) / parseFloat(ValueKMHPK) * 100).toFixed(2));
                            $('#txtPKDarsadSangi' + Type).val(parseFloat(ValueKMHPK) == 0 ? 0 : (parseFloat(PKSangi) / parseFloat(ValueKMHPK) * 100).toFixed(2));

                            Varize = $('#txtPKVarize' + Type).val();
                            Haml = $('#txtPKHaml' + Type).val();
                            Rikhtan = $('#txtPKRikhtan' + Type).val();
                            $('#txtPKDarsadVarize' + Type).val(parseFloat(ValuePK) == 0 ? 0 : (parseFloat(Varize) / parseFloat(ValuePK) * 100).toFixed(2));
                            $('#txtPKDarsadHaml' + Type).val(parseFloat(ValuePK) == 0 ? 0 : (parseFloat(Haml) / parseFloat(ValuePK) * 100).toFixed(2));
                            $('#txtPKDarsadRikhtan' + Type).val(parseFloat(ValuePK) == 0 ? 0 : (parseFloat(Rikhtan) / parseFloat(ValuePK) * 100).toFixed(2));


                        });
                    }
                    //////////////
             
                        
                    $('#divPayKaniInfo input[type="text"]').focus(function () {
                        id = $(this).attr('id');
                        idFix = id.substring(0, 10);
                        idShomareh = id.substring(10, id.length);
                        if (idFix == 'txtValuePK') {
                            $('.clsValuePK').hide();
                            $('#divPkValueRizZir2_' + idShomareh).show();
                            if ($('#txtPkValueRiz2ta3_' + idShomareh).val() != '') $('#divPkValueRiz2ta3_' + idShomareh).show();
                            if ($('#txtPkValueRiz3ta4_' + idShomareh).val() != '') $('#divPkValueRiz3ta4_' + idShomareh).show();
                            if ($('#txtPkValueRiz4ta5_' + idShomareh).val() != '') $('#divPkValueRiz4ta5_' + idShomareh).show();
                            if ($('#txtPkValueRiz5ta6_' + idShomareh).val() != '') $('#divPkValueRiz5ta6_' + idShomareh).show();

                        }

                        idFix = id.substring(0, 18);
                        idShomareh = id.substring(18, id.length);
                        if (idFix == 'txtPkValueRizZir2_') {
                                $('#divPkValueRiz2ta3_' + idShomareh).show();
                        }

                        idFix = id.substring(0, 18);
                        idShomareh = id.substring(18, id.length);
                        if (idFix == 'txtPkValueRiz2ta3_') {
                                $('#divPkValueRiz3ta4_' + idShomareh).show();
                        }

                        idFix = id.substring(0, 18);
                        idShomareh = id.substring(18, id.length);
                        if (idFix == 'txtPkValueRiz3ta4_') {
                                $('#divPkValueRiz4ta5_' + idShomareh).show();
                        }
                        idFix = id.substring(0, 18);
                        idShomareh = id.substring(18, id.length);
                        if (idFix == 'txtPkValueRiz4ta5_') {
                                $('#divPkValueRiz5ta6_' + idShomareh).show();
                        }
                    });

                    $('#divPayKaniInfo input[type="text"]').change(function () {
                        idFix = id.substring(0, 18);
                        idShomareh = id.substring(18, id.length);
                        if (idFix == 'txtPkValueRizZir2_') {
                            if (!$.isNumeric($(this).val())) {
                                toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                                $(this).val('');
                                return;
                            }
                            if ($.trim($(this).val()) == '0' || $.trim($(this).val()) == '') {
                                $('#txtPkValueRizZir2_' + idShomareh).val('');
                                $('#txtPkValueRiz2ta3_' + idShomareh).val('');
                                $('#txtPkValueRiz3ta4_' + idShomareh).val('');
                                $('#txtPkValueRiz4ta5_' + idShomareh).val('');
                                $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            }

                            $('#txtTedadPK' + idShomareh).val('');
                            $('#txtToolPK' + idShomareh).val('');
                            $('#txtArzPK' + idShomareh).val('');
                            $('#txtErtefaPK' + idShomareh).val('');
                            $('#txtErtefaZirTarazPK' + idShomareh).val('0');

                            $('#spanErtefaPayKani').html('حجم پی کنی')

                            ValueRizZir2 = parseFloat($.trim($('#txtPkValueRizZir2_' + idShomareh).val()) == ''
                                ? '0' : $('#txtPkValueRizZir2_' + idShomareh).val());
                            ValueRiz2ta3 = parseFloat($.trim($('#txtPkValueRiz2ta3_' + idShomareh).val()) == ''
                                ? '0' : $('#txtPkValueRiz2ta3_' + idShomareh).val());
                            ValueRiz3ta4 = parseFloat($.trim($('#txtPkValueRiz3ta4_' + idShomareh).val()) == ''
                                ? '0' : $('#txtPkValueRiz3ta4_' + idShomareh).val());
                            ValueRiz4ta5 = parseFloat($.trim($('#txtPkValueRiz4ta5_' + idShomareh).val()) == ''
                                ? '0' : $('#txtPkValueRiz4ta5_' + idShomareh).val());
                            ValueRiz5ta6 = parseFloat($.trim($('#txtPkValueRiz5ta6_' + idShomareh).val()) == ''
                                ? '0' : $('#txtPkValueRiz5ta6_' + idShomareh).val());
                            $('#txtValuePK' + idShomareh).val(ValueRizZir2 + ValueRiz2ta3 + ValueRiz3ta4
                                + ValueRiz4ta5 + ValueRiz5ta6);


                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                        }

                        if (idFix == 'txtPkValueRiz2ta3_') {
                            if (!$.isNumeric($(this).val())) {
                                toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                                $(this).val('');
                                return;
                            }
                            if ($.trim($(this).val()) == '0' || $.trim($(this).val()) == '') {
                                $('#txtPkValueRiz2ta3_' + idShomareh).val('');
                                $('#txtPkValueRiz3ta4_' + idShomareh).val('');
                                $('#txtPkValueRiz4ta5_' + idShomareh).val('');
                                $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            }

                            PkValueRizZir2 = $('#txtPkValueRizZir2_' + idShomareh).val();
                            if (PkValueRizZir2 == '' || PkValueRizZir2=='0') {
                                toastr.info('حجم پی کنی کانال تا عمق 2 متر درج نگردیده است', 'اطلاع');
                                $(this).val('');
                            }
                            else {
                                $('#txtTedadPK' + idShomareh).val('');
                                $('#txtToolPK' + idShomareh).val('');
                                $('#txtArzPK' + idShomareh).val('');
                                $('#txtErtefaPK' + idShomareh).val('');

                                ValueRizZir2 = parseFloat($.trim($('#txtPkValueRizZir2_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRizZir2_' + idShomareh).val());
                                ValueRiz2ta3 = parseFloat($.trim($('#txtPkValueRiz2ta3_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz2ta3_' + idShomareh).val());
                                ValueRiz3ta4 = parseFloat($.trim($('#txtPkValueRiz3ta4_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz3ta4_' + idShomareh).val());
                                ValueRiz4ta5 = parseFloat($.trim($('#txtPkValueRiz4ta5_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz4ta5_' + idShomareh).val());
                                ValueRiz5ta6 = parseFloat($.trim($('#txtPkValueRiz5ta6_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz5ta6_' + idShomareh).val());
                                $('#txtValuePK' + idShomareh).val(ValueRizZir2 + ValueRiz2ta3 + ValueRiz3ta4
                                    + ValueRiz4ta5 + ValueRiz5ta6);
                            }

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));

                        }

                        if (idFix == 'txtPkValueRiz3ta4_') {
                            if (!$.isNumeric($(this).val())) {
                                toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                                $(this).val('');
                                return;
                            }
                            if ($.trim($(this).val()) == '0' || $.trim($(this).val()) == '') {
                                $('#txtPkValueRiz3ta4_' + idShomareh).val('');
                                $('#txtPkValueRiz4ta5_' + idShomareh).val('');
                                $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            }

                            PkValueRiz2ta3 = $('#txtPkValueRiz2ta3_' + idShomareh).val();
                            if (PkValueRiz2ta3 == '' || PkValueRiz2ta3 == '0') {
                                toastr.info('حجم پی کنی کانال، عمق 2 تا 3 متر درج نگردیده است', 'اطلاع');
                                $(this).val('');
                            }
                            else {
                                $('#txtTedadPK' + idShomareh).val('');
                                $('#txtToolPK' + idShomareh).val('');
                                $('#txtArzPK' + idShomareh).val('');
                                $('#txtErtefaPK' + idShomareh).val('');

                                ValueRizZir2 = parseFloat($.trim($('#txtPkValueRizZir2_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRizZir2_' + idShomareh).val());
                                ValueRiz2ta3 = parseFloat($.trim($('#txtPkValueRiz2ta3_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz2ta3_' + idShomareh).val());
                                ValueRiz3ta4 = parseFloat($.trim($('#txtPkValueRiz3ta4_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz3ta4_' + idShomareh).val());
                                ValueRiz4ta5 = parseFloat($.trim($('#txtPkValueRiz4ta5_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz4ta5_' + idShomareh).val());
                                ValueRiz5ta6 = parseFloat($.trim($('#txtPkValueRiz5ta6_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz5ta6_' + idShomareh).val());
                                $('#txtValuePK' + idShomareh).val(ValueRizZir2 + ValueRiz2ta3 + ValueRiz3ta4
                                    + ValueRiz4ta5 + ValueRiz5ta6);
                            }

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));

                        }

                        if (idFix == 'txtPkValueRiz4ta5_') {
                            if (!$.isNumeric($(this).val())) {
                                toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                                $(this).val('');
                                return;
                            }
                            if ($.trim($(this).val()) == '0' || $.trim($(this).val()) == '') {
                                $('#txtPkValueRiz4ta5_' + idShomareh).val('');
                                $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            }

                            PkValueRiz3ta4 = $('#txtPkValueRiz3ta4_' + idShomareh).val();
                            if (PkValueRiz3ta4 == '' || PkValueRiz3ta4 == '0') {
                                toastr.info('حجم پی کنی کانال، عمق 3 تا 4 متر درج نگردیده است', 'اطلاع');
                                $(this).val('');
                            }
                            else {
                                $('#txtTedadPK' + idShomareh).val('');
                                $('#txtToolPK' + idShomareh).val('');
                                $('#txtArzPK' + idShomareh).val('');
                                $('#txtErtefaPK' + idShomareh).val('');

                                ValueRizZir2 = parseFloat($.trim($('#txtPkValueRizZir2_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRizZir2_' + idShomareh).val());
                                ValueRiz2ta3 = parseFloat($.trim($('#txtPkValueRiz2ta3_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz2ta3_' + idShomareh).val());
                                ValueRiz3ta4 = parseFloat($.trim($('#txtPkValueRiz3ta4_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz3ta4_' + idShomareh).val());
                                ValueRiz4ta5 = parseFloat($.trim($('#txtPkValueRiz4ta5_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz4ta5_' + idShomareh).val());
                                ValueRiz5ta6 = parseFloat($.trim($('#txtPkValueRiz5ta6_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz5ta6_' + idShomareh).val());
                                $('#txtValuePK' + idShomareh).val(ValueRizZir2 + ValueRiz2ta3 + ValueRiz3ta4
                                    + ValueRiz4ta5 + ValueRiz5ta6);
                            }

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));

                        }

                        if (idFix == 'txtPkValueRiz5ta6_') {
                            if (!$.isNumeric($(this).val())) {
                                toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                                $(this).val('');
                                return;
                            }
                            if ($.trim($(this).val()) == '0' || $.trim($(this).val()) == '') {
                                $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            }

                            PkValueRiz4ta5 = $('#txtPkValueRiz4ta5_' + idShomareh).val();
                            if (PkValueRiz4ta5 == '' || PkValueRiz4ta5 == '0') {
                                toastr.info('حجم پی کنی کانال، عمق 4 تا 5 متر درج نگردیده است', 'اطلاع');
                                $(this).val('');
                            }
                            else {
                                $('#txtTedadPK' + idShomareh).val('');
                                $('#txtToolPK' + idShomareh).val('');
                                $('#txtArzPK' + idShomareh).val('');
                                $('#txtErtefaPK' + idShomareh).val('');

                                ValueRizZir2 = parseFloat($.trim($('#txtPkValueRizZir2_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRizZir2_' + idShomareh).val());
                                ValueRiz2ta3 = parseFloat($.trim($('#txtPkValueRiz2ta3_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz2ta3_' + idShomareh).val());
                                ValueRiz3ta4 = parseFloat($.trim($('#txtPkValueRiz3ta4_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz3ta4_' + idShomareh).val());
                                ValueRiz4ta5 = parseFloat($.trim($('#txtPkValueRiz4ta5_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz4ta5_' + idShomareh).val());
                                ValueRiz5ta6 = parseFloat($.trim($('#txtPkValueRiz5ta6_' + idShomareh).val()) == ''
                                    ? '0' : $('#txtPkValueRiz5ta6_' + idShomareh).val());
                                $('#txtValuePK' + idShomareh).val(ValueRizZir2 + ValueRiz2ta3 + ValueRiz3ta4
                                    + ValueRiz4ta5 + ValueRiz5ta6);
                            }

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));

                        }

                        id = $(this).attr('id');
                        idFix = id.substring(0, 19);
                        idShomareh = id.substring(19, id.length);
                        if (idFix == 'txtErtefaZirTarazPK') {
                            valCurrent = parseFloat($(this).val());
                            ErtefaPK = $.trim($('#txtErtefaPK' + idShomareh).val());
                            if (ErtefaPK == '0' || ErtefaPK =='') {
                                ValuePK = $('#txtValuePK' + idShomareh).val();
                                if (valCurrent > ValuePK) {
                                    toastr.info('حجم پی کنی زیر تراز آب نبایستی بیش از حجم کل باشد', 'اطلاع');
                                    $(this).val(ValuePK);
                                }
                            }
                            else {
                                if (valCurrent > ErtefaPK) {
                                    toastr.info('ارتفاع پی کنی زیر تراز آب نبایستی بیش از ارتفاع باشد', 'اطلاع');
                                    $(this).val(ErtefaPK);
                                }
                            }
                        }

                        idFix = id.substring(0, 10);
                        idShomareh = id.substring(10, id.length);
                        if (idFix == 'txtTedadPK') {
                            valCurrent = parseFloat($(this).val());
                            ToolPK = parseFloat($('#txtToolPK' + idShomareh).val());
                            ArzPK = parseFloat($('#txtArzPK' + idShomareh).val());
                            ErtefaPK = parseFloat($('#txtErtefaPK' + idShomareh).val());
                            $('#txtValuePK' + idShomareh).val((ToolPK * valCurrent * ArzPK * ErtefaPK).toFixed(2));

                            PKDarsadRikhtan = parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val());
                            $('#txtPKRikhtan' + idShomareh).val((PKDarsadRikhtan * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                        }

                        idFix = id.substring(0, 10);
                        idShomareh = id.substring(10, id.length);
                        if (idFix == 'txtValuePK') {
                            valCurrent = parseFloat($(this).val());
                            $('#txtToolPK' + idShomareh).val(0);
                            $('#txtArzPK' + idShomareh).val(0);
                            $('#txtErtefaPK' + idShomareh).val(0);

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                        }

                        idFix = id.substring(0, 9);
                        idShomareh = id.substring(9, id.length);
                        if (idFix == 'txtToolPK') {
                            valCurrent = parseFloat($(this).val());
                            TedadPK = parseFloat($('#txtTedadPK' + idShomareh).val() == '' ? '1' : $('#txtTedadPK' + idShomareh).val());
                            ArzPK = parseFloat($('#txtArzPK' + idShomareh).val() == '' ? '1' : $('#txtArzPK' + idShomareh).val());
                            ErtefaPK = parseFloat($('#txtErtefaPK' + idShomareh).val() == '' ? '1' : $('#txtErtefaPK' + idShomareh).val());
                            $('#txtValuePK' + idShomareh).val((TedadPK * valCurrent * ArzPK * ErtefaPK).toFixed(2));

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));

                            if (idShomareh == '1') {
                                $('#txtTedadPK' + idShomareh).val(2);
                            }
                            else if (idShomareh == '2') {
                                $('#txtTedadPK' + idShomareh).val(GetTedadDahaneh - 1);
                            }
                            else if (idShomareh == '3' || idShomareh == '4' || idShomareh == '5' || idShomareh == '6') {
                                $('#txtTedadPK' + idShomareh).val(1);
                            }

                            $('#txtPkValueRizZir2_' + idShomareh).val('');
                            $('#txtPkValueRiz2ta3_' + idShomareh).val('');
                            $('#txtPkValueRiz3ta4_' + idShomareh).val('');
                            $('#txtPkValueRiz4ta5_' + idShomareh).val('');
                            $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            $('#spanErtefaPayKani').html('ارتفاع پی کنی');
                            $('#txtErtefaZirTarazPK' + idShomareh).val('0');
                            $('.clsValuePK').hide();

                        }

                        idFix = id.substring(0, 8);
                        idShomareh = id.substring(8, id.length);
                        if (idFix == 'txtArzPK') {
                            valCurrent = parseFloat($(this).val());
                            TedadPK = parseFloat($('#txtTedadPK' + idShomareh).val() == '' ? '1' : $('#txtTedadPK' + idShomareh).val());
                            ToolPK = parseFloat($('#txtToolPK' + idShomareh).val() == '' ? '1' : $('#txtToolPK' + idShomareh).val());
                            ErtefaPK = parseFloat($('#txtErtefaPK' + idShomareh).val() == '' ? '1' : $('#txtErtefaPK' + idShomareh).val());
                            $('#txtValuePK' + idShomareh).val((TedadPK * valCurrent * ToolPK * ErtefaPK).toFixed(2));

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));

                            if (idShomareh == '1') {
                                $('#txtTedadPK' + idShomareh).val(2);
                            }
                            else if (idShomareh == '2') {
                                $('#txtTedadPK' + idShomareh).val(GetTedadDahaneh - 1);
                            }
                            else if (idShomareh == '3' || idShomareh == '4' || idShomareh == '5' || idShomareh == '6') {
                                $('#txtTedadPK' + idShomareh).val(1);
                            }

                            $('#txtPkValueRizZir2_' + idShomareh).val('');
                            $('#txtPkValueRiz2ta3_' + idShomareh).val('');
                            $('#txtPkValueRiz3ta4_' + idShomareh).val('');
                            $('#txtPkValueRiz4ta5_' + idShomareh).val('');
                            $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            $('#spanErtefaPayKani').html('ارتفاع پی کنی');
                            $('#txtErtefaZirTarazPK' + idShomareh).val('0');
                            $('.clsValuePK').hide();

                        }

                        idFix = id.substring(0, 11);
                        idShomareh = id.substring(11, id.length);
                        if (idFix == 'txtErtefaPK') {
                            valCurrent = parseFloat($(this).val());
                            TedadPK = parseFloat($('#txtTedadPK' + idShomareh).val() == '' ? '1' : $('#txtTedadPK' + idShomareh).val());
                            ToolPK = parseFloat($('#txtToolPK' + idShomareh).val() == '' ? '1' : $('#txtToolPK' + idShomareh).val());
                            ArzPK = parseFloat($('#txtArzPK' + idShomareh).val() == '' ? '1' : $('#txtArzPK' + idShomareh).val());
                            $('#txtValuePK' + idShomareh).val((TedadPK * valCurrent * ToolPK * ArzPK).toFixed(2));

                            $('#txtPKDarsadRikhtan' + idShomareh).val((parseFloat($('#txtPKRikhtan' + idShomareh).val()) / parseFloat($('#txtValuePK' + idShomareh).val()) * 100).toFixed(2));

                            $('#txtPKRikhtan' + idShomareh).val((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())).toFixed(2));
                            $('#txtPKDarsadRikhtan' + idShomareh).val(((parseFloat($('#txtValuePK' + idShomareh).val())
                                - parseFloat($('#HajmBotonPK' + idShomareh).val())) / parseFloat($('#txtValuePK' + idShomareh).val())
                                * 100).toFixed(2));
                            $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKDarsadHaml' + idShomareh).val((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val()))).toFixed(2));
                            $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) *
                                parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val())
                                * parseFloat($('#txtValuePK' + idShomareh).val()) / 100).toFixed(2));

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));

                            if (idShomareh=='1') {
                                $('#txtTedadPK' + idShomareh).val(2);
                            }
                            else if (idShomareh == '2') {
                                $('#txtTedadPK' + idShomareh).val(GetTedadDahaneh-1);
                            }
                            else if (idShomareh == '3' || idShomareh == '4' || idShomareh == '5' || idShomareh == '6') {
                                $('#txtTedadPK' + idShomareh).val(1);
                            }

                            $('#txtPkValueRizZir2_' + idShomareh).val('');
                            $('#txtPkValueRiz2ta3_' + idShomareh).val('');
                            $('#txtPkValueRiz3ta4_' + idShomareh).val('');
                            $('#txtPkValueRiz4ta5_' + idShomareh).val('');
                            $('#txtPkValueRiz5ta6_' + idShomareh).val('');
                            $('#spanErtefaPayKani').html('ارتفاع پی کنی');

                            $('#txtErtefaZirTarazPK' + idShomareh).val('0');

                            $('.clsValuePK').hide();
                        }

                        idFix = id.substring(0, 13);
                        idShomareh = id.substring(13, id.length);
                        if (idFix == 'txtValueKDHPK') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            ValueKMHPK = parseFloat($('#txtValueKMHPK' + idShomareh).val());
                            if (valCurrent + ValueKMHPK > ValuePK) {
                                $('#txtValueKDHPK' + idShomareh).val((ValuePK - ValueKMHPK).toFixed(2));
                                $('#txtValueKDDPK' + idShomareh).val((parseFloat($('#txtValueKDHPK' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                            else {
                                $('#txtValueKDDPK' + idShomareh).val((parseFloat($('#txtValueKDHPK' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                                $('#txtValueKMHPK' + idShomareh).val((ValuePK - parseFloat($('#txtValueKDHPK' + idShomareh).val())).toFixed(2));
                                $('#txtValueKMDPK' + idShomareh).val((parseFloat($('#txtValueKMHPK' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                        }

                        idFix = id.substring(0, 13);
                        idShomareh = id.substring(13, id.length);
                        if (idFix == 'txtValueKDDPK') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            ValueKMDPK = parseFloat($('#txtValueKMDPK' + idShomareh).val());
                            if (valCurrent + ValueKMDPK > 100) {
                                $('#txtValueKDDPK' + idShomareh).val((100 - ValueKMDPK).toFixed(2));
                                $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                            else {
                                $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                                $('#txtValueKMDPK' + idShomareh).val((100 - parseFloat($('#txtValueKDDPK' + idShomareh).val())).toFixed(2));
                                $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                        }

                        idFix = id.substring(0, 13);
                        idShomareh = id.substring(13, id.length);
                        if (idFix == 'txtValueKMHPK') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            ValueKDHPK = parseFloat($('#txtValueKDHPK' + idShomareh).val());
                            if (valCurrent + ValueKDHPK > ValuePK) {
                                $('#txtValueKMHPK' + idShomareh).val((ValuePK - ValueKDHPK).toFixed(2));
                                $('#txtValueKMDPK' + idShomareh).val((parseFloat($('#txtValueKMHPK' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                            else {
                                $('#txtValueKMDPK' + idShomareh).val((parseFloat($('#txtValueKMHPK' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                                $('#txtValueKDHPK' + idShomareh).val((ValuePK - parseFloat($('#txtValueKMHPK' + idShomareh).val())).toFixed(2));
                                $('#txtValueKDDPK' + idShomareh).val((parseFloat($('#txtValueKDHPK' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                        }

                        idFix = id.substring(0, 13);
                        idShomareh = id.substring(13, id.length);
                        if (idFix == 'txtValueKMDPK') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            ValueKDDPK = parseFloat($('#txtValueKDDPK' + idShomareh).val());
                            if (valCurrent + ValueKDDPK > 100) {
                                $('#txtValueKMDPK' + idShomareh).val((100 - ValueKDDPK).toFixed(2));
                                $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                            else {
                                $('#txtValueKMHPK' + idShomareh).val((parseFloat($('#txtValueKMDPK' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                                $('#txtValueKDDPK' + idShomareh).val((100 - parseFloat($('#txtValueKMDPK' + idShomareh).val())).toFixed(2));
                                $('#txtValueKDHPK' + idShomareh).val((parseFloat($('#txtValueKDDPK' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }

                            $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                            $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * parseFloat($('#txtValueKMHPK' + idShomareh).val()) / 100).toFixed(2));
                        }

                    });

                    $('#divPayKaniInfoDetails input[type="text"]').change(function () {
                        id = $(this).attr('id');

                        idFix = id.substring(0, 10);
                        idShomareh = id.substring(10, id.length);
                        if (idFix == 'txtPKKhaki') {
                            ValuePK = $('#txtValueKMHPK' + idShomareh).val();
                            valCurrent = parseFloat($(this).val());
                            valPKLajani = parseFloat($('#txtPKLajani' + idShomareh).val());
                            valPKSangi = parseFloat($('#txtPKSangi' + idShomareh).val());
                            if (valCurrent + valPKLajani + valPKSangi > ValuePK) {
                                $(this).val((ValuePK - (valPKLajani + valPKSangi)).toFixed(2));
                                $('#txtPKDarsadKhaki' + idShomareh).val((parseFloat($('#txtPKKhaki' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                            else {
                                $('#txtPKDarsadKhaki' + idShomareh).val((parseFloat($('#txtPKKhaki' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                        }

                        idFix = id.substring(0, 16);
                        idShomareh = id.substring(16, id.length);
                        if (idFix == 'txtPKDarsadKhaki') {
                            ValuePK = parseFloat($('#txtValueKMHPK' + idShomareh).val());
                            valCurrentDarsad = parseFloat($(this).val());
                            valPKLajaniDarsad = parseFloat($('#txtPKDarsadLajani' + idShomareh).val());
                            valPKSangiDarsad = parseFloat($('#txtPKDarsadSangi' + idShomareh).val());
                            if (valCurrentDarsad + valPKLajaniDarsad + valPKSangiDarsad > 100) {
                                $(this).val((100 - (valPKLajaniDarsad + valPKSangiDarsad)).toFixed(2));
                                $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                            else {
                                $('#txtPKKhaki' + idShomareh).val((parseFloat($('#txtPKDarsadKhaki' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                        }

                        idFix = id.substring(0, 11);
                        idShomareh = id.substring(11, id.length);
                        if (idFix == 'txtPKLajani') {
                            ValuePK = parseFloat($('#txtValueKMHPK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            valPKKhaki = parseFloat($('#txtPKKhaki' + idShomareh).val());
                            valPKSangi = parseFloat($('#txtPKSangi' + idShomareh).val());
                            if (valCurrent + valPKKhaki + valPKSangi > ValuePK) {
                                $(this).val((ValuePK - (valPKKhaki + valPKSangi)).toFixed(2));
                                $('#txtPKDarsadLajani' + idShomareh).val((parseFloat($('#txtPKLajani' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                            else {
                                $('#txtPKDarsadLajani' + idShomareh).val((parseFloat($('#txtPKLajani' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                        }

                        idFix = id.substring(0, 17);
                        idShomareh = id.substring(17, id.length);
                        if (idFix == 'txtPKDarsadLajani') {
                            ValuePK = parseFloat($('#txtValueKMHPK' + idShomareh).val());
                            valCurrentDarsad = parseFloat($(this).val());
                            valPKKhakiDarsad = parseFloat($('#txtPKDarsadKhaki' + idShomareh).val());
                            valPKSangiDarsad = parseFloat($('#txtPKDarsadSangi' + idShomareh).val());
                            if (valCurrentDarsad + valPKKhakiDarsad + valPKSangiDarsad > 100) {
                                $(this).val((100 - (valPKKhakiDarsad + valPKSangiDarsad)).toFixed(2));
                                $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                            else {
                                $('#txtPKLajani' + idShomareh).val((parseFloat($('#txtPKDarsadLajani' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                        }

                        idFix = id.substring(0, 10);
                        idShomareh = id.substring(10, id.length);
                        if (idFix == 'txtPKSangi') {
                            ValuePK = parseFloat($('#txtValueKMHPK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            valPKKhaki = parseFloat($('#txtPKKhaki' + idShomareh).val());
                            valPKLajani = parseFloat($('#txtPKLajani' + idShomareh).val());
                            if (valCurrent + valPKKhaki + valPKLajani > ValuePK) {
                                $(this).val((ValuePK - (valPKKhaki + valPKLajani)).toFixed(2));
                                $('#txtPKDarsadSangi' + idShomareh).val((parseFloat($('#txtPKSangi' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                            else {
                                $('#txtPKDarsadSangi' + idShomareh).val((parseFloat($('#txtPKSangi' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                        }
                        idFix = id.substring(0, 16);
                        idShomareh = id.substring(16, id.length);
                        if (idFix == 'txtPKDarsadSangi') {
                            ValuePK = $('#txtValueKMHPK' + idShomareh).val();
                            valCurrentDarsad = parseFloat($(this).val());
                            valPKKhakiDarsad = parseFloat($('#txtPKDarsadKhaki' + idShomareh).val());
                            valPKLajaniDarsad = parseFloat($('#txtPKDarsadLajani' + idShomareh).val());
                            if (valCurrentDarsad + valPKKhakiDarsad + valPKKhakiDarsad > 100) {
                                $(this).val((100 - (valPKLajaniDarsad + valPKKhakiDarsad)).toFixed(2));
                                $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                            else {
                                $('#txtPKSangi' + idShomareh).val((parseFloat($('#txtPKDarsadSangi' + idShomareh).val()) * ValuePK / 100).toFixed(2));
                            }
                        }
                        ////////////
                        ////////////
                        ////////////
                        idFix = id.substring(0, 11);
                        idShomareh = id.substring(11, id.length);
                        if (idFix == 'txtPKVarize') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            valPKHaml = parseFloat($('#txtPKHaml' + idShomareh).val());
                            valPKRikhtan = parseFloat($('#txtPKRikhtan' + idShomareh).val());
                            if (valCurrent + valPKHaml + valPKRikhtan > ValuePK) {
                                $(this).val((ValuePK - (valPKHaml + valPKRikhtan)).toFixed(2));
                                $('#txtPKDarsadVarize' + idShomareh).val((parseFloat($(this).val()) / ValuePK * 100).toFixed(2));
                            }
                            else {
                                $('#txtPKDarsadVarize' + idShomareh).val((parseFloat($(this).val()) / ValuePK * 100).toFixed(2));
                                $('#txtPKHaml' + idShomareh).val((ValuePK - (parseFloat($('#txtPKVarize' + idShomareh).val()) + parseFloat($('#txtPKRikhtan' + idShomareh).val()))).toFixed(2));
                                $('#txtPKDarsadHaml' + idShomareh).val((parseFloat($('#txtPKHaml' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                        }

                        idFix = id.substring(0, 17);
                        idShomareh = id.substring(17, id.length);
                        if (idFix == 'txtPKDarsadVarize') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            valPKDarsadHaml = parseFloat($('#txtPKDarsadHaml' + idShomareh).val());
                            valPKDarsadRikhtan = parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val());
                            if (valCurrent + valPKDarsadHaml + valPKDarsadRikhtan > 100) {
                                $(this).val((100 - (valPKDarsadHaml + valPKDarsadRikhtan)).toFixed(2));
                                $('#txtPKVarize' + idShomareh).val((parseFloat($(this).val()) * ValuePK / 100).toFixed(2));
                                $('#txtPKDarsadHaml' + idShomareh).val(((100 - (parseFloat($('#txtPKDarsadVarize' + idShomareh).val()) + valPKDarsadRikhtan))).toFixed(2));
                                $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val())
                                    * ValuePK / 100).toFixed(2));
                            }
                            else {
                                $('#txtPKVarize' + idShomareh).val((parseFloat($(this).val()) * ValuePK / 100).toFixed(2));
                                $('#txtPKDarsadHaml' + idShomareh).val(((100 - (valCurrent + valPKDarsadRikhtan))).toFixed(2));
                                $('#txtPKHaml' + idShomareh).val((parseFloat($('#txtPKDarsadHaml' + idShomareh).val())
                                    * ValuePK / 100).toFixed(2));
                            }
                        }

                        idFix = id.substring(0, 9);
                        idShomareh = id.substring(9, id.length);
                        if (idFix == 'txtPKHaml') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            valPKVarize = parseFloat($('#txtPKVarize' + idShomareh).val());
                            valPKRikhtan = parseFloat($('#txtPKRikhtan' + idShomareh).val());
                            if (valCurrent + valPKVarize + valPKRikhtan > ValuePK) {
                                $(this).val((ValuePK - (valPKVarize + valPKRikhtan)).toFixed(2));
                                $('#txtPKDarsadHaml' + idShomareh).val((parseFloat($(this).val()) / ValuePK * 100).toFixed(2));
                            }
                            else {
                                $('#txtPKDarsadHaml' + idShomareh).val((parseFloat($(this).val()) / ValuePK * 100).toFixed(2));
                                $('#txtPKVarize' + idShomareh).val((ValuePK - (parseFloat($('#txtPKHaml' + idShomareh).val()) + parseFloat($('#txtPKRikhtan' + idShomareh).val()))).toFixed(2));
                                $('#txtPKDarsadVarize' + idShomareh).val((parseFloat($('#txtPKVarize' + idShomareh).val()) / ValuePK * 100).toFixed(2));
                            }
                        }

                        idFix = id.substring(0, 15);
                        idShomareh = id.substring(15, id.length);
                        if (idFix == 'txtPKDarsadHaml') {
                            ValuePK = parseFloat($('#txtValuePK' + idShomareh).val());
                            valCurrent = parseFloat($(this).val());
                            valPKDarsadVarize = parseFloat($('#txtPKDarsadVarize' + idShomareh).val());
                            valPKDarsadRikhtan = parseFloat($('#txtPKDarsadRikhtan' + idShomareh).val());
                            if (valCurrent + valPKDarsadVarize + valPKDarsadRikhtan > 100) {
                                $(this).val((100 - (valPKDarsadVarize + valPKDarsadRikhtan)).toFixed(2));
                                $('#txtPKHaml' + idShomareh).val((parseFloat($(this).val()) * ValuePK / 100).toFixed(2));
                                $('#txtPKDarsadVarize' + idShomareh).val(((100 - (parseFloat($('#txtPKDarsadHaml' + idShomareh).val()) + valPKDarsadRikhtan))).toFixed(2));
                                $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val())
                                    * ValuePK / 100).toFixed(2));
                            }
                            else {
                                $('#txtPKHaml' + idShomareh).val((parseFloat($(this).val()) * ValuePK / 100).toFixed(2));
                                $('#txtPKDarsadVarize' + idShomareh).val(((100 - (valCurrent + valPKDarsadRikhtan))).toFixed(2));
                                $('#txtPKVarize' + idShomareh).val((parseFloat($('#txtPKDarsadVarize' + idShomareh).val())
                                    * ValuePK / 100).toFixed(2));
                            }
                        }
                    });
                },
                error: function (response) {
                    toastr.error('مشکل در بارگذاری کیلومتراژ انتخابی', 'خطا');
                }
            });
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری اطلاعات پی کنی', 'خطا');
        }
    });
};

function CheckTruePaykani() {
    blncheck = true;
    blncheckSum = true;
    blncheckSumKMHPK = true;

    $('#divPayKaniInfoDetails input').each(function () {
        val = parseFloat($(this).val());
        if (val<0) {
            $(this).addClass('ErrorValueStyle');
            blncheck= false;
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });

    for (var i = 1; i < 7; i++) {
        TedadDahaneh = parseInt($('#HDFTedadDahaneh').val());
        if (i==2) {
        if (TedadDahaneh-1==0) {
            i++;
        }
        }
        PKKhaki = parseFloat($('#txtPKKhaki' + i).val());
        PKLajani = parseFloat($('#txtPKLajani' + i).val());
        PKSangi = parseFloat($('#txtPKSangi' + i).val());
        ValueKMHPK = parseFloat($('#txtValueKMHPK' + i).val());
        SumKMHPK = PKKhaki + PKLajani + PKSangi;
        if (Math.floor(SumKMHPK) != Math.floor(ValueKMHPK)) {
            $('#txtValueKMHPK' + i).addClass('ErrorValueStyle');
            $('#txtPKKhaki' + i).addClass('ErrorValueStyle');
            $('#txtPKLajani' + i).addClass('ErrorValueStyle');
            $('#txtPKSangi' + i).addClass('ErrorValueStyle');
            blncheckSum = false;
            blncheck = false;
        }
        else
        {
            $('#txtValueKMHPK' + i).removeClass('ErrorValueStyle');
            $('#txtPKKhaki' + i).removeClass('ErrorValueStyle');
            $('#txtPKLajani' + i).removeClass('ErrorValueStyle');
            $('#txtPKSangi' + i).removeClass('ErrorValueStyle');
        }

        ValuePK = parseFloat($('#txtValuePK' + i).val());
        Varize = parseFloat($('#txtPKVarize' + i).val());
        PKHaml = parseFloat($('#txtPKHaml' + i).val());
        PKRikhtan = parseFloat($('#txtPKRikhtan' + i).val());
        SumPK = Varize + PKHaml + PKRikhtan;
        if (Math.floor(SumPK) != Math.floor(ValuePK)) {
            $('#txtValuePK' + i).addClass('ErrorValueStyle');
            $('#txtPKVarize' + i).addClass('ErrorValueStyle');
            $('#txtPKHaml' + i).addClass('ErrorValueStyle');
            $('#txtPKRikhtan' + i).addClass('ErrorValueStyle');
            blncheckSumKMHPK = false;
            blncheck = false;
        }
        else {
            $('#txtValuePK' + i).removeClass('ErrorValueStyle');
            $('#txtPKVarize' + i).removeClass('ErrorValueStyle');
            $('#txtPKHaml' + i).removeClass('ErrorValueStyle');
            $('#txtPKRikhtan' + i).removeClass('ErrorValueStyle');
        }
    }

    if (!blncheckSum) {
        toastr.info('مجموع پی کنی در زمین های خاکی، لجنی و سنگی بایستی برابر با حجم پی کنی عملیات خاکی با ماشین باشد', 'اطلاع');
    }

    if (!blncheckSumKMHPK) {
        toastr.info('مجموع، حمل به دپو و  ریختن خاک با مصالح سنگی به درون آنها بایستی برابر با حجم پی کنی کل باشد', 'اطلاع');
    }
    return blncheck;
}

function SavePayKaniInfo(BarAvordUserId) {
    check = CheckTruePaykani();
    if (check) {
        ToolPK1 = $('#txtToolPK1').val();
        ArzPK1 = $('#txtArzPK1').val();
        ErtefaPK1 = $('#txtErtefaPK1').val();
        ValuePK1 = $('#txtValuePK1').val();
        ValueKDHPK1 = $('#txtValueKDHPK1').val();
        ValueKMHPK1 = $('#txtValueKMHPK1').val();
        ErtefaZirTarazPK1 = $('#txtErtefaZirTarazPK1').val();

        ToolPK2 = ($('#txtToolPK2').val() == undefined || $('#txtToolPK2').val() =="") ? '0' : $('#txtToolPK2').val();
        ArzPK2 = ($('#txtArzPK2').val() == undefined || $('#txtArzPK2').val() == "") ? '0' : $('#txtArzPK2').val();
        ErtefaPK2 = ($('#txtErtefaPK2').val() == undefined || $('#txtErtefaPK2').val() == "") ? '0' : $('#txtErtefaPK2').val();
        ValuePK2 = ($('#txtValuePK2').val() == undefined || $('#txtValuePK2').val() == "") ? '0' : $('#txtValuePK2').val();
        ValueKDHPK2 = ($('#txtValueKDHPK2').val() == undefined || $('#txtValueKDHPK2').val() == "") ? '0' : $('#txtValueKDHPK2').val();
        ValueKMHPK2 = ($('#txtValueKMHPK2').val() == undefined || $('#txtValueKMHPK2').val() == "") ? '0' : $('#txtValueKMHPK2').val();
        ErtefaZirTarazPK2 = ($('#txtErtefaZirTarazPK2').val() == undefined || $('#txtErtefaZirTarazPK2').val() == "") ? '0' : $('#txtErtefaZirTarazPK2').val();

        ToolPK3 = $('#txtToolPK3').val();
        ArzPK3 = $('#txtArzPK3').val();
        ErtefaPK3 = $('#txtErtefaPK3').val();
        ValuePK3 = $('#txtValuePK3').val();
        ValueKDHPK3 = $('#txtValueKDHPK3').val();
        ValueKMHPK3 = $('#txtValueKMHPK3').val();
        ErtefaZirTarazPK3 = $('#txtErtefaZirTarazPK3').val();

        ToolPK4 = $('#txtToolPK4').val();
        ArzPK4 = $('#txtArzPK4').val();
        ErtefaPK4 = $('#txtErtefaPK4').val();
        ValuePK4 = $('#txtValuePK4').val();
        ValueKDHPK4 = $('#txtValueKDHPK4').val();
        ValueKMHPK4 = $('#txtValueKMHPK4').val();
        ErtefaZirTarazPK4 = $('#txtErtefaZirTarazPK4').val();

        ToolPK5 = $('#txtToolPK5').val();
        ArzPK5 = $('#txtArzPK5').val();
        ErtefaPK5 = $('#txtErtefaPK5').val();
        ValuePK5 = $('#txtValuePK5').val();
        ValueKDHPK5 = $('#txtValueKDHPK5').val();
        ValueKMHPK5 = $('#txtValueKMHPK5').val();
        ErtefaZirTarazPK5 = $('#txtErtefaZirTarazPK5').val();

        ToolPK6 = $('#txtToolPK6').val();
        ArzPK6 = $('#txtArzPK6').val();
        ErtefaPK6 = $('#txtErtefaPK6').val();
        ValuePK6 = $('#txtValuePK6').val();
        ValueKDHPK6 = $('#txtValueKDHPK6').val();
        ValueKMHPK6 = $('#txtValueKMHPK6').val();
        ErtefaZirTarazPK6 = $('#txtErtefaZirTarazPK6').val();

        PKKhaki1 = $('#txtPKKhaki1').val();
        PKLajani1 = $('#txtPKLajani1').val();
        PKSangi1 = $('#txtPKSangi1').val();
        PKVarize1 = $('#txtPKVarize1').val();
        PKHaml1 = $('#txtPKHaml1').val();
        PKRikhtan1 = $('#txtPKRikhtan1').val();

        PKKhaki2 = ($('#txtPKKhaki2').val() == undefined || $('#txtPKKhaki2').val() == "") ? '0' : $('#txtPKKhaki2').val();
        PKLajani2 = ($('#txtPKLajani2').val() == undefined || $('#txtPKLajani2').val() == "") ? '0' : $('#txtPKLajani2').val();
        PKSangi2 = ($('#txtPKSangi2').val() == undefined || $('#txtPKSangi2').val() == "") ? '0' : $('#txtPKSangi2').val();
        PKVarize2 = ($('#txtPKVarize2').val() == undefined ||$('#txtPKVarize2').val() == "") ? '0' : $('#txtPKVarize2').val();
        PKHaml2 = ($('#txtPKHaml2').val() == undefined || $('#txtPKHaml2').val() == "") ? '0' : $('#txtPKHaml2').val();
        PKRikhtan2 = ($('#txtPKRikhtan2').val() == undefined || $('#txtPKRikhtan2').val() == "") ? '0' : $('#txtPKRikhtan2').val();

        PKKhaki3 = $('#txtPKKhaki3').val();
        PKLajani3 = $('#txtPKLajani3').val();
        PKSangi3 = $('#txtPKSangi3').val();
        PKVarize3 = $('#txtPKVarize3').val();
        PKHaml3 = $('#txtPKHaml3').val();
        PKRikhtan3 = $('#txtPKRikhtan3').val();

        PKKhaki4 = $('#txtPKKhaki4').val();
        PKLajani4 = $('#txtPKLajani4').val();
        PKSangi4 = $('#txtPKSangi4').val();
        PKVarize4 = $('#txtPKVarize4').val();
        PKHaml4 = $('#txtPKHaml4').val();
        PKRikhtan4 = $('#txtPKRikhtan4').val();

        PKKhaki5 = $('#txtPKKhaki5').val();
        PKLajani5 = $('#txtPKLajani5').val();
        PKSangi5 = $('#txtPKSangi5').val();
        PKVarize5 = $('#txtPKVarize5').val();
        PKHaml5 = $('#txtPKHaml5').val();
        PKRikhtan5 = $('#txtPKRikhtan5').val();

        PKKhaki6 = $('#txtPKKhaki6').val();
        PKLajani6 = $('#txtPKLajani6').val();
        PKSangi6 = $('#txtPKSangi6').val();
        PKVarize6 = $('#txtPKVarize6').val();
        PKHaml6 = $('#txtPKHaml6').val();
        PKRikhtan6 = $('#txtPKRikhtan6').val();
        /////////////

        PkValueRizZir2_1 = ($('#txtPkValueRizZir2_1').val() == undefined || $('#txtPkValueRizZir2_1').val() == "") ? '0' : $('#txtPkValueRizZir2_1').val();
        PkValueRiz2ta3_1 = ($('#txtPkValueRiz2ta3_1').val() == undefined || $('#txtPkValueRiz2ta3_1').val() == "") ? '0' : $('#txtPkValueRiz2ta3_1').val();
        PkValueRiz3ta4_1 = ($('#txtPkValueRiz3ta4_1').val() == undefined || $('#txtPkValueRiz3ta4_1').val() == "") ? '0' : $('#txtPkValueRiz3ta4_1').val();
        PkValueRiz4ta5_1 = ($('#txtPkValueRiz4ta5_1').val() == undefined || $('#txtPkValueRiz4ta5_1').val() == "") ? '0' : $('#txtPkValueRiz4ta5_1').val();
        PkValueRiz5ta6_1 = ($('#txtPkValueRiz5ta6_1').val() == undefined || $('#txtPkValueRiz5ta6_1').val() == "") ? '0' : $('#txtPkValueRiz5ta6_1').val();

        PkValueRizZir2_2 = ($('#txtPkValueRizZir2_2').val() == undefined || $('#txtPkValueRizZir2_2').val() == "") ? '0' : $('#txtPkValueRizZir2_2').val();
        PkValueRiz2ta3_2 = ($('#txtPkValueRiz2ta3_2').val() == undefined || $('#txtPkValueRiz2ta3_2').val() == "") ? '0' : $('#txtPkValueRiz2ta3_2').val();
        PkValueRiz3ta4_2 = ($('#txtPkValueRiz3ta4_2').val() == undefined || $('#txtPkValueRiz3ta4_2').val() == "") ? '0' : $('#txtPkValueRiz3ta4_2').val();
        PkValueRiz4ta5_2 = ($('#txtPkValueRiz4ta5_2').val() == undefined || $('#txtPkValueRiz4ta5_2').val() == "") ? '0' : $('#txtPkValueRiz4ta5_2').val();
        PkValueRiz5ta6_2 = ($('#txtPkValueRiz5ta6_2').val() == undefined || $('#txtPkValueRiz5ta6_2').val() == "") ? '0' : $('#txtPkValueRiz5ta6_2').val();

        PkValueRizZir2_3 = ($('#txtPkValueRizZir2_3').val() == undefined || $('#txtPkValueRizZir2_3').val() == "") ? '0' : $('#txtPkValueRizZir2_3').val();
        PkValueRiz2ta3_3 = ($('#txtPkValueRiz2ta3_3').val() == undefined || $('#txtPkValueRiz2ta3_3').val() == "") ? '0' : $('#txtPkValueRiz2ta3_3').val();
        PkValueRiz3ta4_3 = ($('#txtPkValueRiz3ta4_3').val() == undefined || $('#txtPkValueRiz3ta4_3').val() == "") ? '0' : $('#txtPkValueRiz3ta4_3').val();
        PkValueRiz4ta5_3 = ($('#txtPkValueRiz4ta5_3').val() == undefined || $('#txtPkValueRiz4ta5_3').val() == "") ? '0' : $('#txtPkValueRiz4ta5_3').val();
        PkValueRiz5ta6_3 = ($('#txtPkValueRiz5ta6_3').val() == undefined || $('#txtPkValueRiz5ta6_3').val() == "") ? '0' : $('#txtPkValueRiz5ta6_3').val();

        PkValueRizZir2_4 = ($('#txtPkValueRizZir2_4').val() == undefined || $('#txtPkValueRizZir2_4').val() == "") ? '0' : $('#txtPkValueRizZir2_4').val();
        PkValueRiz2ta3_4 = ($('#txtPkValueRiz2ta3_4').val() == undefined || $('#txtPkValueRiz2ta3_4').val() == "") ? '0' : $('#txtPkValueRiz2ta3_4').val();
        PkValueRiz3ta4_4 = ($('#txtPkValueRiz3ta4_4').val() == undefined || $('#txtPkValueRiz3ta4_4').val() == "") ? '0' : $('#txtPkValueRiz3ta4_4').val();
        PkValueRiz4ta5_4 = ($('#txtPkValueRiz4ta5_4').val() == undefined || $('#txtPkValueRiz4ta5_4').val() == "") ? '0' : $('#txtPkValueRiz4ta5_4').val();
        PkValueRiz5ta6_4 = ($('#txtPkValueRiz5ta6_4').val() == undefined || $('#txtPkValueRiz5ta6_4').val() == "") ? '0' : $('#txtPkValueRiz5ta6_4').val();

        PkValueRizZir2_5 = ($('#txtPkValueRizZir2_5').val() == undefined || $('#txtPkValueRizZir2_5').val() == "") ? '0' : $('#txtPkValueRizZir2_5').val();
        PkValueRiz2ta3_5 = ($('#txtPkValueRiz2ta3_5').val() == undefined || $('#txtPkValueRiz2ta3_5').val() == "") ? '0' : $('#txtPkValueRiz2ta3_5').val();
        PkValueRiz3ta4_5 = ($('#txtPkValueRiz3ta4_5').val() == undefined || $('#txtPkValueRiz3ta4_5').val() == "") ? '0' : $('#txtPkValueRiz3ta4_5').val();
        PkValueRiz4ta5_5 = ($('#txtPkValueRiz4ta5_5').val() == undefined || $('#txtPkValueRiz4ta5_5').val() == "") ? '0' : $('#txtPkValueRiz4ta5_5').val();
        PkValueRiz5ta6_5 = ($('#txtPkValueRiz5ta6_5').val() == undefined || $('#txtPkValueRiz5ta6_5').val() == "") ? '0' : $('#txtPkValueRiz5ta6_5').val();

        PkValueRizZir2_6 = ($('#txtPkValueRizZir2_6').val() == undefined || $('#txtPkValueRizZir2_6').val() == "") ? '0' : $('#txtPkValueRizZir2_6').val();
        PkValueRiz2ta3_6 = ($('#txtPkValueRiz2ta3_6').val() == undefined || $('#txtPkValueRiz2ta3_6').val() == "") ? '0' : $('#txtPkValueRiz2ta3_6').val();
        PkValueRiz3ta4_6 = ($('#txtPkValueRiz3ta4_6').val() == undefined || $('#txtPkValueRiz3ta4_6').val() == "") ? '0' : $('#txtPkValueRiz3ta4_6').val();
        PkValueRiz4ta5_6 = ($('#txtPkValueRiz4ta5_6').val() == undefined || $('#txtPkValueRiz4ta5_6').val() == "") ? '0' : $('#txtPkValueRiz4ta5_6').val();
        PkValueRiz5ta6_6 = ($('#txtPkValueRiz5ta6_6').val() == undefined || $('#txtPkValueRiz5ta6_6').val() == "") ? '0' : $('#txtPkValueRiz5ta6_6').val();
        /////////////
        TedadDahaneh = parseInt($('#HDFTedadDahaneh').val());
        PolNum = $('#HDFPolNum').val();
        var vardata1 = new Object();
        vardata1.BarAvordUserId = BarAvordUserId; vardata1.Type = 5; vardata1.PolNum = PolNum; vardata1.TedadDahaneh = TedadDahaneh;
        vardata1.ToolPK1 = ToolPK1; vardata1.ArzPK1 = ArzPK1; vardata1.ErtefaPK1 = ErtefaPK1; vardata1.ValuePK1 = ValuePK1;
        vardata1.ValueKDHPK1 = ValueKDHPK1; vardata1.ValueKMHPK1 = ValueKMHPK1; vardata1.ErtefaZirTarazPK1 = ErtefaZirTarazPK1;
        vardata1.ToolPK2 = ToolPK2; vardata1.ArzPK2 = ArzPK2; vardata1.ErtefaPK2 = ErtefaPK2; vardata1.ValuePK2 = ValuePK2;
        vardata1.ValueKDHPK2 = ValueKDHPK2; vardata1.ValueKMHPK2 = ValueKMHPK2; vardata1.ErtefaZirTarazPK2 = ErtefaZirTarazPK2;
        vardata1.ToolPK3 = ToolPK3; vardata1.ArzPK3 = ArzPK3; vardata1.ErtefaPK3 = ErtefaPK3; vardata1.ValuePK3 = ValuePK3;
        vardata1.ValueKDHPK3 = ValueKDHPK3; vardata1.ValueKMHPK3 = ValueKMHPK3; vardata1.ErtefaZirTarazPK3 = ErtefaZirTarazPK3;
        vardata1.ToolPK4 = ToolPK4; vardata1.ArzPK4 = ArzPK4; vardata1.ErtefaPK4 = ErtefaPK4; vardata1.ValuePK4=ValuePK4;
        vardata1.ValueKDHPK4 = ValueKDHPK4; vardata1.ValueKMHPK4 = ValueKMHPK4; vardata1.ErtefaZirTarazPK4 = ErtefaZirTarazPK4;
        vardata1.ToolPK5 = ToolPK5; vardata1.ArzPK5 = ArzPK5; vardata1.ErtefaPK5 = ErtefaPK5; vardata1.ValuePK5 = ValuePK5;
        vardata1.ValueKDHPK5 = ValueKDHPK5; vardata1.ValueKMHPK5 = ValueKMHPK5; vardata1.ErtefaZirTarazPK5 = ErtefaZirTarazPK5;
        vardata1.ToolPK6 = ToolPK6; vardata1.ArzPK6 = ArzPK6; vardata1.ErtefaPK6 = ErtefaPK6; vardata1.ValuePK6 = ValuePK6;
        vardata1.ValueKDHPK6 = ValueKDHPK6; vardata1.ValueKMHPK6 = ValueKMHPK6; vardata1.ErtefaZirTarazPK6 = ErtefaZirTarazPK6;
        vardata1.PKKhaki1 = PKKhaki1; vardata1.PKLajani1 = PKLajani1; vardata1.PKSangi1 = PKSangi1;
        vardata1.PKVarize1 = PKVarize1; vardata1.PKHaml1 = PKHaml1; vardata1.PKRikhtan1 = PKRikhtan1;
        vardata1.PKKhaki2 = PKKhaki2; vardata1.PKLajani2 = PKLajani2; vardata1.PKSangi2 = PKSangi2;
        vardata1.PKVarize2 = PKVarize2; vardata1.PKHaml2 = PKHaml2; vardata1.PKRikhtan2 = PKRikhtan2;
        vardata1.PKKhaki3 = PKKhaki3; vardata1.PKLajani3 = PKLajani3; vardata1.PKSangi3 = PKSangi3;
        vardata1.PKVarize3 = PKVarize3; vardata1.PKHaml3 = PKHaml3; vardata1.PKRikhtan3 = PKRikhtan3;
        vardata1.PKKhaki4 = PKKhaki4; vardata1.PKLajani4 = PKLajani4; vardata1.PKSangi4 = PKSangi4;
        vardata1.PKVarize4 = PKVarize4; vardata1.PKHaml4 = PKHaml4; vardata1.PKRikhtan4 = PKRikhtan4;
        vardata1.PKKhaki5 = PKKhaki5; vardata1.PKLajani5 = PKLajani5; vardata1.PKSangi5 = PKSangi5;
        vardata1.PKVarize5 = PKVarize5; vardata1.PKHaml5 = PKHaml5; vardata1.PKRikhtan5 = PKRikhtan5;
        vardata1.PKKhaki6 = PKKhaki6; vardata1.PKLajani6 = PKLajani6; vardata1.PKSangi6 = PKSangi6;
        vardata1.PKVarize6 = PKVarize6; vardata1.PKHaml6 = PKHaml6; vardata1.PKRikhtan6 = PKRikhtan6;
        vardata1.PkValueRizZir2_1 = PkValueRizZir2_1; vardata1.PkValueRiz2ta3_1 = PkValueRiz2ta3_1; vardata1.PkValueRiz3ta4_1 = PkValueRiz3ta4_1;
        vardata1.PkValueRiz4ta5_1 = PkValueRiz4ta5_1; vardata1.PkValueRiz5ta6_1 = PkValueRiz5ta6_1;
        vardata1.PkValueRizZir2_2 = PkValueRizZir2_2; vardata1.PkValueRiz2ta3_2 = PkValueRiz2ta3_2; vardata1.PkValueRiz3ta4_2 = PkValueRiz3ta4_2;
        vardata1.PkValueRiz4ta5_2 = PkValueRiz4ta5_2; vardata1.PkValueRiz5ta6_2 = PkValueRiz5ta6_2;
        vardata1.PkValueRizZir2_3 = PkValueRizZir2_3; vardata1.PkValueRiz2ta3_3 = PkValueRiz2ta3_3; vardata1.PkValueRiz3ta4_3 = PkValueRiz3ta4_3;
        vardata1.PkValueRiz4ta5_3 = PkValueRiz4ta5_3; vardata1.PkValueRiz5ta6_3 = PkValueRiz5ta6_3;
        vardata1.PkValueRizZir2_4 = PkValueRizZir2_4; vardata1.PkValueRiz2ta3_4 = PkValueRiz2ta3_4; vardata1.PkValueRiz3ta4_4 = PkValueRiz3ta4_4;
        vardata1.PkValueRiz4ta5_4 = PkValueRiz4ta5_4; vardata1.PkValueRiz5ta6_4 = PkValueRiz5ta6_4;
        vardata1.PkValueRizZir2_5 = PkValueRizZir2_5; vardata1.PkValueRiz2ta3_5 = PkValueRiz2ta3_5; vardata1.PkValueRiz3ta4_5 = PkValueRiz3ta4_5;
        vardata1.PkValueRiz4ta5_5 = PkValueRiz4ta5_5; vardata1.PkValueRiz5ta6_5 = PkValueRiz5ta6_5;
        vardata1.PkValueRizZir2_6 = PkValueRizZir2_6; vardata1.PkValueRiz2ta3_6 = PkValueRiz2ta3_6; vardata1.PkValueRiz3ta4_6 = PkValueRiz3ta4_6;
        vardata1.PkValueRiz4ta5_6 = PkValueRiz4ta5_6; vardata1.PkValueRiz5ta6_6 = PkValueRiz5ta6_6;
        $.ajax({
            type: "POST",
            url: "/AmalyateKhakiInfoForBarAvords/SavePayKaniInfoForBarAvord",
            data:JSON.stringify(vardata1),
            //data: '{BarAvordId:' + BarAvordId + ',Type:5,PolNum:' + PolNum + ',TedadDahaneh:' + TedadDahaneh
            //        + ',ToolPK1:' + "'" + ToolPK1 + "'" + ',ArzPK1:' + "'" + ArzPK1 + "'" + ',ErtefaPK1:' + "'" + ErtefaPK1 + "'" + ',ValuePK1:' + "'" + ValuePK1 + "'"
            //        + ',ValueKDHPK1:' + "'" + ValueKDHPK1 + "'" + ',ValueKMHPK1:' + "'" + ValueKMHPK1 + "'" + ',ErtefaZirTarazPK1:' + "'" + ErtefaZirTarazPK1 + "'"
            //        + ',ToolPK2:' + "'" + ToolPK2 + "'" + ',ArzPK2:' + "'" + ArzPK2 + "'" + ',ErtefaPK2:' + "'" + ErtefaPK2 + "'" + ',ValuePK2:' + "'" + ValuePK2 + "'"
            //        + ',ValueKDHPK2:' + "'" + ValueKDHPK2 + "'" + ',ValueKMHPK2:' + "'" + ValueKMHPK2 + "'" + ',ErtefaZirTarazPK2:' + "'" + ErtefaZirTarazPK2 + "'"
            //        + ',ToolPK3:' + "'" + ToolPK3 + "'" + ',ArzPK3:' + "'" + ArzPK3 + "'" + ',ErtefaPK3:' + "'" + ErtefaPK3 + "'" + ',ValuePK3:' + "'" + ValuePK3 + "'"
            //        + ',ValueKDHPK3:' + "'" + ValueKDHPK3 + "'" + ',ValueKMHPK3:' + "'" + ValueKMHPK3 + "'" + ',ErtefaZirTarazPK3:' + "'" + ErtefaZirTarazPK3 + "'"
            //        + ',ToolPK4:' + "'" + ToolPK4 + "'" + ',ArzPK4:' + "'" + ArzPK4 + "'" + ',ErtefaPK4:' + "'" + ErtefaPK4 + "'" + ',ValuePK4:' + "'" + ValuePK4 + "'"
            //        + ',ValueKDHPK4:' + "'" + ValueKDHPK4 + "'" + ',ValueKMHPK4:' + "'" + ValueKMHPK4 + "'" + ',ErtefaZirTarazPK4:' + "'" + ErtefaZirTarazPK4 + "'"
            //        + ',ToolPK5:' + "'" + ToolPK5 + "'" + ',ArzPK5:' + "'" + ArzPK5 + "'" + ',ErtefaPK5:' + "'" + ErtefaPK5 + "'" + ',ValuePK5:' + "'" + ValuePK5 + "'"
            //        + ',ValueKDHPK5:' + "'" + ValueKDHPK5 + "'" + ',ValueKMHPK5:' + "'" + ValueKMHPK5 + "'" + ',ErtefaZirTarazPK5:' + "'" + ErtefaZirTarazPK5 + "'"
            //        + ',ToolPK6:' + "'" + ToolPK6 + "'" + ',ArzPK6:' + "'" + ArzPK6 + "'" + ',ErtefaPK6:' + "'" + ErtefaPK6 + "'" + ',ValuePK6:' + "'" + ValuePK6 + "'"
            //        + ',ValueKDHPK6:' + "'" + ValueKDHPK6 + "'" + ',ValueKMHPK6:' + "'" + ValueKMHPK6 + "'" + ',ErtefaZirTarazPK6:' + "'" + ErtefaZirTarazPK6 + "'"
            //        + ',PKKhaki1:' + "'" + PKKhaki1 + "'" + ',PKLajani1:' + "'" + PKLajani1 + "'" + ',PKSangi1:' + "'" + PKSangi1 + "'"
            //        + ',PKVarize1:' + "'" + PKVarize1 + "'" + ',PKHaml1:' + "'" + PKHaml1 + "'" + ',PKRikhtan1:' + "'" + PKRikhtan1 + "'"
            //        + ',PKKhaki2:' + "'" + PKKhaki2 + "'" + ',PKLajani2:' + "'" + PKLajani2 + "'" + ',PKSangi2:' + "'" + PKSangi2 + "'"
            //        + ',PKVarize2:' + "'" + PKVarize2 + "'" + ',PKHaml2:' + "'" + PKHaml2 + "'" + ',PKRikhtan2:' + "'" + PKRikhtan2 + "'"
            //        + ',PKKhaki3:' + "'" + PKKhaki3 + "'" + ',PKLajani3:' + "'" + PKLajani3 + "'" + ',PKSangi3:' + "'" + PKSangi3 + "'"
            //        + ',PKVarize3:' + "'" + PKVarize3 + "'" + ',PKHaml3:' + "'" + PKHaml3 + "'" + ',PKRikhtan3:' + "'" + PKRikhtan3 + "'"
            //        + ',PKKhaki4:' + "'" + PKKhaki4 + "'" + ',PKLajani4:' + "'" + PKLajani4 + "'" + ',PKSangi4:' + "'" + PKSangi4 + "'"
            //        + ',PKVarize4:' + "'" + PKVarize4 + "'" + ',PKHaml4:' + "'" + PKHaml4 + "'" + ',PKRikhtan4:' + "'" + PKRikhtan4 + "'"
            //        + ',PKKhaki5:' + "'" + PKKhaki5 + "'" + ',PKLajani5:' + "'" + PKLajani5 + "'" + ',PKSangi5:' + "'" + PKSangi5 + "'"
            //        + ',PKVarize5:' + "'" + PKVarize5 + "'" + ',PKHaml5:' + "'" + PKHaml5 + "'" + ',PKRikhtan5:' + "'" + PKRikhtan5 + "'"
            //        + ',PKKhaki6:' + "'" + PKKhaki6 + "'" + ',PKLajani6:' + "'" + PKLajani6 + "'" + ',PKSangi6:' + "'" + PKSangi6 + "'"
            //        + ',PKVarize6:' + "'" + PKVarize6 + "'" + ',PKHaml6:' + "'" + PKHaml6 + "'" + ',PKRikhtan6:' + "'" + PKRikhtan6 + "'"
            //        + ',PkValueRizZir2_1:' + "'" + PkValueRizZir2_1 + "'" + ',PkValueRiz2ta3_1:' + "'" + PkValueRiz2ta3_1 + "'" + ',PkValueRiz3ta4_1:' + "'" + PkValueRiz3ta4_1 + "'"
            //        + ',PkValueRiz4ta5_1:' + "'" + PkValueRiz4ta5_1 + "'" + ',PkValueRiz5ta6_1:' + "'" + PkValueRiz5ta6_1 + "'"
            //        + ',PkValueRizZir2_2:' + "'" + PkValueRizZir2_2 + "'" + ',PkValueRiz2ta3_2:' + "'" + PkValueRiz2ta3_2 + "'" + ',PkValueRiz3ta4_2:' + "'" + PkValueRiz3ta4_2 + "'"
            //        + ',PkValueRiz4ta5_2:' + "'" + PkValueRiz4ta5_2 + "'" + ',PkValueRiz5ta6_2:' + "'" + PkValueRiz5ta6_2 + "'"
            //        + ',PkValueRizZir2_3:' + "'" + PkValueRizZir2_3 + "'" + ',PkValueRiz2ta3_3:' + "'" + PkValueRiz2ta3_3 + "'" + ',PkValueRiz3ta4_3:' + "'" + PkValueRiz3ta4_3 + "'"
            //        + ',PkValueRiz4ta5_3:' + "'" + PkValueRiz4ta5_3 + "'" + ',PkValueRiz5ta6_3:' + "'" + PkValueRiz5ta6_3 + "'"
            //        + ',PkValueRizZir2_4:' + "'" + PkValueRizZir2_4 + "'" + ',PkValueRiz2ta3_4:' + "'" + PkValueRiz2ta3_4 + "'" + ',PkValueRiz3ta4_4:' + "'" + PkValueRiz3ta4_4 + "'"
            //        + ',PkValueRiz4ta5_4:' + "'" + PkValueRiz4ta5_4 + "'" + ',PkValueRiz5ta6_4:' + "'" + PkValueRiz5ta6_4 + "'"
            //        + ',PkValueRizZir2_5:' + "'" + PkValueRizZir2_5 + "'" + ',PkValueRiz2ta3_5:' + "'" + PkValueRiz2ta3_5 + "'" + ',PkValueRiz3ta4_5:' + "'" + PkValueRiz3ta4_5 + "'"
            //        + ',PkValueRiz4ta5_5:' + "'" + PkValueRiz4ta5_5 + "'" + ',PkValueRiz5ta6_5:' + "'" + PkValueRiz5ta6_5 + "'"
            //        + ',PkValueRizZir2_6:' + "'" + PkValueRizZir2_6 + "'" + ',PkValueRiz2ta3_6:' + "'" + PkValueRiz2ta3_6 + "'" + ',PkValueRiz3ta4_6:' + "'" + PkValueRiz3ta4_6 + "'"
            //        + ',PkValueRiz4ta5_6:' + "'" + PkValueRiz4ta5_6 + "'" + ',PkValueRiz5ta6_6:' + "'" + PkValueRiz5ta6_6 + "'"
            //        + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    toastr.success('اطلاعات پی کنی بدرستی ثبت گردید', 'ثبت');
                }
                else
                    toastr.error('مشکل در ثبت اطلاعات پی کنی', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در ثبت اطلاعات پی کنی', 'خطا');
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای ایراد میباشد', 'اطلاع');
}

function ShowExistingPolForPayKani(BarAvordUserId) {

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId
    $.ajax({
        type: "POST",
        url: "/PolVaAbroBarAvord/GetExistingPolInfoWithBarAvordId",
        data:JSON.stringify(vardata),
        //data: '{BarAvordUserId:' + BarAvordUserId + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            var PolVaAbroBarAvord = xml.find("tblPolVaAbroBarAvord");
            DastakPolInfo = xml.find("tblDastakPolInfo");
            count = 1;
            str = '';
            $.each(PolVaAbroBarAvord, function () {
                PolExistingId = $(this).find("_ID").text();
                PolNum = $(this).find("_PolNum").text();
                TedadDahaneh = $(this).find("_TedadDahaneh").text();
                DahaneAbro = $(this).find("_DahaneAbro").text();
                HadAksarErtefaKoole = $(this).find("_HadAksarErtefaKoole").text();
                Hs = $(this).find("_Hs").text();
                ZavieBie = $(this).find("_ZavieBie").text();
                ToolAbro = $(this).find("_ToolAbro").text();
                x = $(this).find("_X").text();
                y = $(this).find("_Y").text();
                NoeBanaii = $(this).find("_NoeBanaii").text();
                NahveEjraDal = $(this).find("_NahveEjraDal").text();

                TedadDarDahaneAbro = parseInt(TedadDahaneh) * parseInt(DahaneAbro);
                strNoeName = TedadDarDahaneAbro > 6 ? ' پل ' : ' آبرو ';

                str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' ondblclick=\"ShowSelctionPolForPaykani(' +"'"+
                    PolExistingId +"'"+ ',' + PolNum + ',' + TedadDahaneh + ',' + DahaneAbro + ',\'' + HadAksarErtefaKoole + '\',' + Hs + ',' +
                    ZavieBie + ',' + ToolAbro + ',' + x + ',' + y + ',' + NoeBanaii + ',' + NahveEjraDal + ','+"'" + BarAvordUserId+"'" + ')\">' + count++ + ' - ' + strNoeName + TedadDahaneh
                    + ' دهانه، ' + DahaneAbro + ' متری، موقعیت: X: ' + parseInt(x) + ' ، Y: ' + parseInt(y) + '</a></div>';
            });

            $('#divViewExistingPolForPayKani').html(str);
            $('#aViewExistingPolForPayKani').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری پل های موجود', 'خطا');
        }
    });
}



