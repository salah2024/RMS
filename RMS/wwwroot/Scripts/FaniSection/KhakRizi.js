//////////////////خاکریزی
////////////////////
///////////////////
function KhakRiziWithBarAvordClick(OpId, BarAvordUserId) {
    ShowSelctionKhakRizi(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0, OpId);

    //str = '';
    //str += '<div class=\'\' style=\'margin-top:3px;\'><div class=\'col-md-6 row\'><div class=\'col-md-5\'><a class=\'NewPolStyle\' onclick=\"ShowSelctionKhakRizi(1,0,0,' + "'" + BarAvordUserId + "'" + ',0,0,0,0,0)\">کیلومتراژ جدید</a></div><div class=\'col-md-5\'><a class=\'NewPolStyle\' onclick=\"ShowExistingKMKhakRizi(' + BarAvordUserId + ')\">لیست خاکریزی ها</a></div></div>';
    //str += '<div class=\'\' style=\'margin-top: 30px;\' id=\'ViewKhakRizi\'></div>';
    ////$('#ula' + OpId).html(str);
    //$('#KhakRiziShow').find('#divShowKhakRizi').html(str);
    //$('#aKhakRiziShow').click();
}

function ShowSelctionKhakRizi(IsNew, KMExistingId, KMNum, BarAvordUserId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value, OpId) {

  str=  `
<div id="divViewKhakRizi" class="">
  <div class="row col-12" style="border:1px solid #c0c4e2;border-radius:5px!important;padding:5px 0;">
    <div class="col-md-1" style="text-align:left;"><span>از کیلومتراژ: </span></div>
    <div class="col-md-1">
      <input style="text-align:center;padding:0;font-size:16px;" type="text" class="form-control_1 input-sm" id="txtFromKMForKhakRizi" value="000+000"/>
    </div>
    <div class="col-md-1" style="text-align:left;"><span>تا کیلومتراژ: </span></div>
    <div class="col-md-1">
      <input style="text-align:center;padding:0;font-size:16px;" type="text" class="form-control_1 input-sm" id="txtToKMForKhakRizi" value="000+000"/>
    </div>
    <div class="col-md-1" style="text-align:left;">
      <a class="NewPolStyle" onclick="SaveKhakRiziInfo('${BarAvordUserId}')">ذخیره</a>
    </div>
  </div>
</div>

<div class="row col-12">
  <div class="row col-12" style="background-color:#ede7ff;padding:4px 0;margin:2px 0 0;border:1px solid #d5bfff;border-radius:5px!important;text-align:center;">
    <div class="row col-12">
      <div class="col-md-4"><span>نوع راه</span></div>
      <div class="col-md-3"><span>نوع دانه بندی نوع خاک مصرفی در خاکریزی</span></div>
      <div class="col-md-5"><span>حجم خاکریزی</span></div>
    </div>
  </div>
</div>

<div class="row col-12">
  <div class="row col-12" style="margin:2px 0;border:1px solid #d5bfff;background-color:#ede7ff;">
    <div class="col-md-7 row" style="margin-top:10px;">
      <div class="row" style="border:1px solid #d5dcef!important;border-radius:5px!important;">
        <div class="col-md-7">
          <div class="row" style="padding:5px;">
            <input id="radioNoeRahKhakRizi1" value="1" name="KhakRiziG" checked type="radio"/>
            <label for="radioNoeRahKhakRizi1" class="NoeRahStyle">آزاد راه - بزرگ راه - راه اصلی و راه فرعی درجه یک</label>
          </div>
          <div class="row" style="padding:5px;">
            <input id="radioNoeRahKhakRizi2" value="2" name="KhakRiziG" type="radio"/>
            <label for="radioNoeRahKhakRizi2" class="NoeRahStyle">راه فرعی درجه 2 و راههای روستایی</label>
          </div>
        </div>
        <div class="col-md-5 row">
          <div class="row" style="padding:5px;">
            <div class="col-md-4"><span>درشت دانه </span></div>
            <div class="col-md-4">
              <input id="txtDarsadKRDDaneh" value="100" style="text-align:center;padding:0;" class="form-control input-sm" type="text"/>
            </div>
            <div class="col-md-2"><span>درصد</span></div>
          </div>
          <div class="row" style="padding:5px;">
            <div class="col-md-4" style="text-align:left;"><span>ریز دانه </span></div>
            <div class="col-md-4">
              <input id="txtDarsadKRRDaneh" value="0" style="text-align:center;padding:0;" class="form-control input-sm" type="text"/>
            </div>
            <div class="col-md-2"><span>درصد</span></div>
          </div>
        </div>
      </div>

      <div class="row" style="border:1px solid #d5dcef!important;border-radius:5px!important;padding:5px;margin-top:2px;">
        <input id="ckKREzafeBahaKhakMosalah" type="checkbox"/>
        <label for="ckKREzafeBahaKhakMosalah" class="spanCheckBoxStyle">اضافه بها مسلح کردن خاک</label>
      </div>
    </div>

    <div class="col-md-5" style="margin-bottom:10px;margin-top:10px;">
      <div class="row col-12" style="border:1px solid #d5dcef!important;border-radius:5px!important;padding-bottom:3px;">
        <div class="row" style="padding:5px;">
          <div class="col-md-8" style="text-align:left;"><span>بین 30 سانتیمتر تا بستر روسازی </span></div>
          <div class="col-md-2"><input id="txtHajmBetween0To30" style="text-align:center;padding:0;" value="0" class="form-control input-sm" type="text"/></div>
          <div class="col-md-2"><span>متر مکعب</span></div>
        </div>
        <div class="row" style="padding:5px;">
          <div class="col-md-8" style="text-align:left;"><span>بین 100 تا 30 سانتیمتر مانده به بستر روسازی </span></div>
          <div class="col-md-2"><input id="txtHajmBetween30To100" style="text-align:center;padding:0;" value="0" class="form-control input-sm" type="text"/></div>
          <div class="col-md-2"><span>متر مکعب</span></div>
        </div>
        <div class="row" style="padding:5px;">
          <div class="col-md-8" style="text-align:left;"><span>زیر یک متر مانده به بستر روسازی </span></div>
          <div class="col-md-2"><input id="txtHajmBetweenTo100" value="0" style="text-align:center;padding:0;" class="form-control input-sm" type="text"/></div>
          <div class="col-md-2"><span>متر مکعب</span></div>
        </div>
      </div>
    </div>
  </div>
</div>

<div class="row col-12" id="divKhakRiziInfoDetails" style="margin-top:10px;padding:0;text-align:center;display:none;margin-right: 0px;margin-left: 0px;"></div>
`;

    //str = '<div class=\'\'>';
    //str += '<div class=\'row col-12\' style=\'border: 1px solid #c0c4e2;border-radius: 5px !important;padding: 5px 0px;\'>';
    //str += '<div class=\'col-md-1\' style=\'text-align: left;\'><span>از کیلومتراژ: </span></div>';
    //str += '<div class=\'col-md-1\'><input style=\'text-align:center;padding:0px;font-size: 16px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtFromKMForKhakRizi\' value=\'000+000\'/></div>';
    //str += '<div class=\'col-md-1\' style=\'text-align:left;\'><span>تا کیلومتراژ: </span></div><div class=\'col-md-1\'><input style=\'text-align: center;padding: 0px;font-size: 16px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtToKMForKhakRizi\' value=\'000+000\'/></div>';
    //str += '<div class=\'col-md-1\' style=\'text-align:left;\'><a class=\'NewPolStyle\' onclick=\"SaveKhakRiziInfo(' + "'" + BarAvordUserId + "'" + ')\">ذخیره</a></div>';
    //str += '</div></div>';

    //str += '<div class=\'row col-12\'><div class=\'row col-12\' style=\'background-color: #ede7ff;padding: 4px 0px;margin: 2px 0px 0px;border: 1px solid #d5bfff;border-radius: 5px !important;text-align:center\'>';
    //str += '<div class=\'row\'><div class=\'col-md-4\'><span>نوع راه</span></div>';
    //str += '<div class=\'col-md-3\'><span>نوع دانه بندی نوع خاک مصرفی در خاکریزی</span></div>';
    //str += '<div class=\'col-md-5\'><span>حجم خاکریزی</span></div>';
    //str += '</div></div></div>';

    //str += '<div class=\'row col-12\'><div class=\'row col-12\' style=\'margin:2px 0px;border: 1px solid #d5bfff;background-color: #ede7ff;\'><div class=\'col-md-7 row\' style=\'margin-top: 10px;\'><div class=\'row\' style=\'border: 1px solid #d5dcef !important;border-radius: 5px !important;\'><div class=\'col-md-7\'>';
    //str += '<div class=\'row\' style=\'padding: 5px;\'><input id=\'radioNoeRahKhakRizi1\' value=\'1\' name=\'KhakRiziG\' checked=\'true\' type=\'radio\' /><span onclick=\"$(\'#radioNoeRahKhakRizi1\').click()\" class=\'NoeRahStyle\'>آزاد راه - بزرگ راه - راه اصلی و راه فرعی درجه یک</span></div>';
    //str += '<div class=\'row\' style=\'padding: 5px;\'><input id=\'radioNoeRahKhakRizi2\' value=\'2\' name=\'KhakRiziG\' type=\'radio\' /><span onclick=\"$(\'#radioNoeRahKhakRizi2\').click()\" class=\'NoeRahStyle\'>راه فرعی درجه 2 و راههای روستایی</span></div></div>';
    //str += '<div class=\'col-md-5 row\'><div class=\'row\' style=\'padding: 5px;\'><div class=\'col-md-4\'><span>درشت دانه </span></div><div class=\'col-md-4\'><input id=\'txtDarsadKRDDaneh\' value=\'100\' style=\'text-align:center;padding:0px\' class=\'form-control input-sm\' type=\'text\' /></div><div class=\'col-md-2\'><span>درصد</span></div></div>';
    //str += '<div class=\'row\' style=\'padding: 5px;\'><div class=\'col-md-4\' style=\'text-align: left;\'><span>ریز دانه </span></div><div class=\'col-md-4\'><input id=\'txtDarsadKRRDaneh\' value=\'0\' style=\'text-align:center;padding:0px\' class=\'form-control input-sm\' type=\'text\' /></div><div class=\'col-md-2\'><span>درصد</span></div></div></div></div>';
    //str += '<div class=\'row\' style=\'border: 1px solid #d5dcef !important;border-radius: 5px !important;padding: 5px;margin-top: 2px;\'><input id=\'ckKREzafeBahaKhakMosalah\' type=\'checkbox\'/><span class=\'spanCheckBoxStyle\' onclick=\"$(\'#ckKREzafeBahaKhakMosalah\').click()\">اضافه بها مسلح کردن خاک</span></div>';
    //str += '</div>';
    //str += '<div class=\'col-md-5\' style=\'margin-bottom: 10px;margin-top: 10px;\'>';
    //str += '<div class=\'row col-12\' style=\'border: 1px solid #d5dcef !important;border-radius: 5px !important;padding-bottom: 3px;\' >';
    //str += '<div class=\'row\' style=\'padding: 5px;\'><div class=\'col-md-8\' style=\'text-align: left;\'><span>بین 30 سانتیمتر تا بستر روسازی </span></div><div class=\'col-md-2\'><input id=\'txtHajmBetween0To30\' style=\'text-align:center;padding:0px\' value=\'0\' class=\'form-control input-sm\' type=\'text\' /></div><div class=\'col-md-2\'><span>متر مکعب</span></div></div>';
    //str += '<div class=\'row\' style=\'padding: 5px;\'><div class=\'col-md-8\' style=\'text-align: left;\'><span>بین 100 تا 30 سانتیمتر مانده به بستر روسازی </span></div><div class=\'col-md-2\'><input id=\'txtHajmBetween30To100\' style=\'text-align:center;padding:0px\' value=\'0\' class=\'form-control input-sm\' type=\'text\' /></div><div class=\'col-md-2\'><span>متر مکعب</span></div></div>';
    //str += '<div class=\'row\' style=\'padding: 5px;\'><div class=\'col-md-8\' style=\'text-align: left;\'><span>زیر یک متر مانده به بستر روسازی </span></div><div class=\'col-md-2\'><input id=\'txtHajmBetweenTo100\' value=\'0\' style=\'text-align:center;padding:0px\' class=\'form-control input-sm\' type=\'text\' /></div><div class=\'col-md-2\'><span>متر مکعب</span></div></div>';
    //str += '</div></div>';
    //str += '</div></div></div>';
    ////////////////////////////////
    //str += '<div class=\'row col-12\' id=\'divKhakRiziInfoDetails\' style=\'margin-top:10px;padding:0px;text-align: center;display:none\'>';
    //str += '</div>';
    debugger;

    $('#ula' + OpId).html(str);

    //$('#ViewKhakRizi').html(str);
   $ViewKhakRizi= $('#divViewKhakRizi');
   $ViewKhakRizi.find('#txtDarsadKRDDaneh').change(function () {
        if (!$.isNumeric($(this).val())) {
            toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('blinking');
        }
        else
            $(this).removeClass('blinking');
    });

    $('#txtDarsadKRDDaneh').change(function () {
        if (!$.isNumeric($(this).val())) {
            toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('blinking');
        }
        else
            $(this).removeClass('blinking');
    });
    /////////////
    $('#txtHajmBetween0To30').change(function () {
        debugger;
        if (!$.isNumeric($(this).val())) {
            toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('blinking');
        }
        else {
            $(this).removeClass('blinking');
            ShowBestarKhakRizi();
        }
    });

    $('#txtHajmBetween30To100').change(function () {
        if (!$.isNumeric($(this).val())) {
            toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('blinking');
        }
        else {
            $(this).removeClass('blinking');
            ShowBestarKhakRizi();
        }
    });

    $('#txtHajmBetweenTo100').change(function () {
        if (!$.isNumeric($(this).val())) {
            toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('blinking');
        }
        else
            $(this).removeClass('blinking');
    });
    /////////////
    $('#txtDarsadKRDDaneh').change(function () {
        DarsadKRRDaneh = 100 - parseFloat($(this).val());
        $('#txtDarsadKRRDaneh').val(DarsadKRRDaneh.toFixed(2));
        ShowBestarKhakRizi()
    });

    $('#txtDarsadKRRDaneh').change(function () {
        DarsadKRDDaneh = 100 - parseFloat($(this).val());
        $('#txtDarsadKRDDaneh').val(DarsadKRDDaneh.toFixed(2));
        ShowBestarKhakRizi();
    });

    $('#txtHajmRizeshBardari').change(function () {
        HRB = parseFloat($(this).val());
        if (!$.isNumeric(HRB)) {
            toastr.info('حجم ریزش وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('blinking');
        }
        else {
            $('#divRizeshBardariInfoDetails').show();
            $(this).removeClass('blinking');

            Darsad1 = parseFloat($('#txtRBDarsad1').val());
            $('#txtRBDetail1').val((Darsad1 / 100 * HRB).toFixed(2));
            ReUseDarsad1 = parseFloat($('#txtRBReUseDarsad1').val());
            $('#txtRBReUseHajm1').val((ReUseDarsad1 / 100 * $('#txtRBDetail1').val()).toFixed(2));
            DarsadVarizi1 = parseFloat($('#txtRBDarsadVarizi1').val());
            $('#txtRBVarizi1').val((DarsadVarizi1 / 100 * $('#txtRBDetail1').val()).toFixed(2));
            DarsadHaml1 = parseFloat($('#txtRBDarsadHaml1').val());
            $('#txtRBHaml1').val((DarsadHaml1 / 100 * $('#txtRBDetail1').val()).toFixed(2));
            //////////
            Darsad2 = parseFloat($('#txtRBDarsad2').val());
            $('#txtRBDetail2').val((Darsad2 / 100 * HKB).toFixed(2));
            ReUseDarsad2 = parseFloat($('#txtRBReUseDarsad2').val());
            $('#txtRBReUseHajm2').val((ReUseDarsad2 / 100 * $('#txtRBDetail2').val()).toFixed(2));
            DarsadVarizi2 = parseFloat($('#txtRBDarsadVarizi2').val());
            $('#txtRBVarizi2').val((DarsadVarizi2 / 100 * $('#txtRBDetail2').val()).toFixed(2));
            DarsadHaml2 = parseFloat($('#txtRBDarsadHaml2').val());
            $('#txtRBHaml2').val((DarsadHaml2 / 100 * $('#txtRBDetail2').val()).toFixed(2));
            //////////
        }
    });

    $('#txtFromKMForRizeshbardari').change(function () {
        var KM = $(this).val();
        var KMSplit = KM.split('+');
        if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
            $(this).addClass('blinking');
            toastr.info('کیلومتراژ شروع وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
        }
        else
            $(this).removeClass('blinking');
    });

    $('#txtToKMForRizeshbardari').change(function () {
        var KM = $(this).val();
        var KMSplit = KM.split('+');
        if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
            $(this).addClass('blinking');
            toastr.info('کیلومتراژ خاتمه وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
        }
        else
            $(this).removeClass('blinking');

        var KME = parseFloat(KM.replace('+', ''));
        var KMS = parseFloat($('#txtFromKMForKhakbardari').val().replace('+', ''));
        if (KMS > KME) {
            toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
            $('#txtToKMForKhakbardari').addClass('blinking');
        }
        else
            $('#txtToKMForKhakbardari').removeClass('blinking');
    });

    $('#radioNoeRahKhakRizi1').change(function () {
        ShowBestarKhakRizi();
    });
    $('#radioNoeRahKhakRizi2').change(function () {
        ShowBestarKhakRizi();
    });
    if (IsNew == 0) {
        $('#txtFromKMForKhakRizi').val(FromKMSplit);
        $('#txtToKMForKhakRizi').val(ToKMSplit);
        $('#txtHajmKhakRizi').val(Value);

        $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
        $('#btnCloseExistingKMAmalyateKhaki').click();
        var vardata = new Object();
        vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
        $.ajax({
            type: "POST",
            url: "/KhakRizi/GetDetailsOfKMKhakBardariInfoWithKMKhakRiziId",
            //data: '{AmalyateKhakiInfoForBarAvordId:' + KMExistingId + '}',
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var KMAmalyateKhakiBarAvordDetails = response.tblKMAmalyateKhakiBarAvordDetails;
                var KMAmalyateKhakiBarAvordMore = response.tblKMAmalyateKhakiBarAvordMore;
                var KMAmalyateKhakiBarAvordDetailsMore = response.tblKMAmalyateKhakiBarAvordDetailsMore;
                var KMAmalyateKhakiBarAvordDetailsEzafeBaha = response.tblKMAmalyateKhakiBarAvordDetailsEzafeBaha;

                Value = 0;
                $.each(KMAmalyateKhakiBarAvordMore, function () {
                    Name = $.trim($(this).name);
                    if (Name == 'DarsadKRDDaneh') {
                        Value = $.trim($(this).value);
                        $('#txtDarsadKRDDaneh').val(Value);
                    }
                    if (Name == 'DarsadKRRDaneh') {
                        Value = $.trim($(this).value);
                        $('#txtDarsadKRRDaneh').val(Value);
                    }
                    if (Name == 'HajmBetween0To30') {
                        Value = $.trim($(this).value);
                        $('#txtHajmBetween0To30').val(Value);
                    }
                    if (Name == 'HajmBetween30To100') {
                        Value = $.trim($(this).value);
                        $('#txtHajmBetween30To100').val(Value);
                    }
                    if (Name == 'HajmBetweenTo100') {
                        Value = $.trim($(this).value);
                        $('#txtHajmBetweenTo100').val(Value);
                    }
                    if (Name == 'radioNoeRahKhakRizi') {
                        Value = parseInt($.trim($(this).value));
                        $('#radioNoeRahKhakRizi' + Value).attr('checked', true);
                    }
                });

                if (ShowBestarKhakRizi()) {
                    $.each(KMAmalyateKhakiBarAvordDetails, function () {
                        Id = $(this).id;
                        AmalyateKhakiInfoForBarAvordId = $(this).amalyateKhakiInfoForBarAvordId;
                        Type = $(this).find("_Type").text();

                        $.each(KMAmalyateKhakiBarAvordDetailsMore, function () {
                            CurrentId = $(this).id;
                            Name = $.trim($(this).name);
                            ValueMore = $(this).value;
                            AmalyateKhakiInfoForBarAvordDetailsId = $(this).amalyateKhakiInfoForBarAvordDetailsId;
                            if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                                $('#txt' + Name).val(ValueMore);
                            }
                        });

                        $.each(KMAmalyateKhakiBarAvordDetailsEzafeBaha, function () {
                            CurrentId = $(this).id;
                            Name = $.trim($(this).name);
                            boolValue = $(this).value == 'true' ? true : false;
                            AmalyateKhakiInfoForBarAvordDetailsId = $(this).amalyateKhakiInfoForBarAvordDetailsId;

                            if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                                $('#ck' + Name).attr('checked', boolValue);
                            }
                        });
                    });
                }
            },
            error: function (response) {
                toastr.error('مشکل در بارگذاری کیلومتراژ انتخابی', 'خطا');
            }
        });
    }
    else {
        $('#HDFStateAmalyateKhakiSaveOrEdit').val('Add');
    }
}

function ShowBestarKhakRizi() {
    HajmBetween0To30 = parseFloat($('#txtHajmBetween0To30').val());
    HajmBetween30To100 = parseFloat($('#txtHajmBetween30To100').val());
    HajmBetweenTo100 = parseFloat($('#txtHajmBetweenTo100').val());

    DarsadKRDDaneh = parseFloat($('#txtDarsadKRDDaneh').val());
    DarsadKRRDaneh = parseFloat($('#txtDarsadKRRDaneh').val());

    strParam = '';
    if ($('#radioNoeRahKhakRizi1').is(':checked')) {
        if (HajmBetween0To30 != 0 && HajmBetween0To30 != '') {
            if (DarsadKRDDaneh != 0) {
                strParam += '3,';
            }
            if (DarsadKRRDaneh != 0) {
                strParam += '2,';
            }
        }
        if (HajmBetween30To100 != 0 && HajmBetween30To100 != '') {
            if (DarsadKRDDaneh != 0) {
                strParam += '2,';
            }
            if (DarsadKRRDaneh != 0) {
                strParam += '1,';
            }
        }
    }
    else if ($('#radioNoeRahKhakRizi2').is(':checked')) {
        if (HajmBetween0To30 != 0 && HajmBetween0To30 != '') {
            if (DarsadKRDDaneh != 0) {
                strParam += '2,';
            }
            if (DarsadKRRDaneh != 0) {
                strParam += '1,';
            }
        }
        if (HajmBetween30To100 != 0 && HajmBetween30To100 != '') {
            if (DarsadKRDDaneh != 0) {
                strParam += '1,';
            }
            if (DarsadKRRDaneh != 0) {
                strParam += '0,';
            }
        }
    }

    strParamNew = '';
    strParamSplit = strParam.split(',');
    for (var i = 0; i < 4; i++) {
        for (var j = 0; j < strParamSplit.length - 1; j++) {
            if (i == strParamSplit[j]) {
                strParamNew += i + ',';
                break;
            }
        }
    }
    //////////////
    ActivityTitle = ["با تراکم 85 درصد، به روش آشتو اصلاحي تا عمق 15 سانتيمتر", "با تراکم 90 درصد، به روش آشتو اصلاحي تا عمق 15 سانتيمتر"
        , "با تراکم 95 درصد، به روش آشتو اصلاحي تا عمق 15 سانتيمتر", "با تراکم 100 درصد، به روش آشتو اصلاحي تا عمق 15 سانتيمتر"];
    //////////////////
    strParamNewSplit = strParamNew.split(',');
    ////////////////
    str = '';
    if ((strParamNewSplit.length - 1) != 0) {
        str += '<div class=\'row col-12\'><div class=\'row col-12\' style=\'background-color: #ffe8eb;border: 1px solid #ffa6c7;border-radius: 5px !important;\'>';
        str += '<div class=\'col-md-6\'>آب پاشي و کوبيدن بستر خاکريزها يا کـف ترانشه ها و مانند آنها</div>';
        str += '<div class=\'col-md-1\' style=\'padding: 0px;\'><div class=\'row\'><span style=\'border-bottom:1px solid #ccc\'>طول</span></div><div class=\'row\'><span>متر</span></div></div>';
        str += '<div class=\'col-md-1\' style=\'padding: 0px;\'><div class=\'row\'><span style=\'border-bottom:1px solid #ccc\'>عرض</span></div><div class=\'row\'><span>متر</span></div></div>';
        str += '<div class=\'col-md-2\'><div class=\'row\'><span>شخم زدن زمین غیر</span></div><div class=\'row\'>سنگی تا 15 سانتیمتر</div></div>';
        str += '<div class=\'col-md-2\'><div class=\'row\'><span>تسطیح بستر خاکریزی</span></div><div class=\'row\'>با گریدر</div></div>';
        str += '</div>';
        str += '<div class=\'row col-12\' style=\'border: 1px solid #ffa6c7;margin-top:2px\'>';
        for (var i = 0; i < strParamNewSplit.length - 1; i++) {
            str += '<div class=\'row col-12\' style=\'text-align: center;margin-top:5px\'>';
            str += '<div class=\'col-md-6\'>' + ActivityTitle[strParamNewSplit[i]] + '</div>';
            str += '<div class=\'col-md-1\' style=\'padding:0px 3px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtTool' + strParamNewSplit[i] + '\' value=\'0\'/></div>';
            str += '<div class=\'col-md-1\' style=\'padding:0px 3px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtArz' + strParamNewSplit[i] + '\' value=\'0\'/></div>';
            str += '<div class=\'col-md-2\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'checkbox\' id=\'ckKRShokhmZadan' + strParamNewSplit[i] + '\' /></div>';
            str += '<div class=\'col-md-2\'><input type=\'checkbox\' id=\'ckKRTastih' + strParamNewSplit[i] + '\'/></div>';
            str += '</div>';
        }
        str += '</div>';
    }

    $('#divKhakRiziInfoDetails').html(str);
    $('#divKhakRiziInfoDetails').show();

    $('#divKhakRiziInfoDetails input[type="checkbox"]').change(function () {
        id = $(this).attr('id');
        idFix = id.substring(0, 15);
        idShomareh = id.substring(15, id.length);

        if ($(this).is(':checked'))
            if (idFix == 'ckKRShokhmZadan') {
                $('#ckKRTastih' + idShomareh).prop("checked", true);
            }
    });

    $('#divKhakRiziInfoDetails input[type="text"]').change(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('ErrorValueStyle');
        }
        else {
            $(this).removeClass('ErrorValueStyle');
        }
        ///////////////
    });

    return true;
}

function ShowExistingKMKhakRizi(BarAvordUserId) {
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Type = 3;
    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/GetExistingKMAmalyateKhakiInfoWithBarAvordId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var KMAmalyateKhakiBarAvord = response;
            if (KMAmalyateKhakiBarAvord.length > 0) {
                strSEKB = `
                 <div class="row col-12 ExistKhBHeaderStyle">
                        <div class="col-1" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>ردیف</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;" ><span>از کیلومتراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>تا کیلومتراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>حجم خاکبرداری</span></div>
                        <div class="col-3" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>عملیات</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"></div>
                </div>

            `;
                $.each(KMAmalyateKhakiBarAvord, function () {
                    KMExistingId = this.id;
                    FromKM = this.fromKM;
                    ToKM = this.toKM;
                    FromKMSplit = this.fromKMSplit;
                    ToKMSplit = this.toKMSplit;
                    Value = this.value;
                    KMNum = this.kmNum;
                    Type = this.type;

                    strSEKB += `
    <div id="div${KMNum}" class="row col-12 ExistKhBStyle" style="border:1px solid #a99dbd;background-color:#ffe9ff">
    <div id="divExistKMHeader${KMNum}" class="row col-12 ExistKMHeaderStyle" onclick="ViewKhakBardariInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}')">
    <div class="col-md-1 label-col" style="text-align:center">
      <span>${KMNum}</span>
    </div>
    <!-- از کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForKhakbardari${KMNum}" value="${FromKMSplit}" onclick="event.stopPropagation();"/>
    </div>

    <!-- تا کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForKhakbardari${KMNum}" value="${ToKMSplit}" onclick="event.stopPropagation();"/>
    </div>

    <!-- حجم خاکبرداری -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtHajmKhakBardari${KMNum}" value="${Value}" onclick="event.stopPropagation();"/>
    </div>
        <div class="col-md-3" style="text-align:center">
        <span>جهت مشاهده جزییات کلیک نمایید</span>
</div>
   
    </div>
  <!-- بخش نمایش -->

  <div class="row col-12" style="direction:ltr" id="MainViewKhakBardari${KMNum}" class="khakbardari-view">
    <div id="ViewKhakBardari${KMNum}" class="khakbardari-view" style="direction: rtl;">
    </div>
    <div class="col-md-2 action-col">
      <a class="btn buttonStyleBoard" style="color:#fff" onclick="UpdateKhakBardariInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum})" onclick="event.stopPropagation();">
        ذخیره
      </a>
    </div>
  <div id="ViewRizMetreKHRizi${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  <div id="ViewKhakRiziEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  </div><!-- MainViewKhakBardari -->
  </div>
    `;
                });

                $('#divExistingKMKhakRizi').html(strSEKB);
                $('#divExistingKMKhakRizi').find('#MainViewKhakRizi' + KMNum).hide();
            }
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
        }
    });
}

function SaveKhakRiziInfo(BarAvordUserId) {
    debugger;
    check = false;
    //////////
    var KM = $('#txtFromKMForKhakRizi').val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtFromKMForKhakRizi').addClass('blinking');
        check = true;
    }
    else {
        $('#txtFromKMForKhakRizi').removeClass('blinking');
    }
    ///////////////
    var KM = $('#txtToKMForKhakRizi').val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtToKMForKhakRizi').addClass('blinking');
        check = true;
    }
    else {
        $('#txtToKMForKhakRizi').removeClass('blinking');
    }
    /////////////
    var KME = parseFloat($('#txtToKMForKhakRizi').val().replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForKhakRizi').val().replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForKhakRizi').addClass('blinking');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForKhakRizi').removeClass('blinking');
    }

    if ($('#radioNoeRahKhakRizi1').is(':checked')) {
        radioNoeRahKhakRizi = 1;
    }
    else if ($('#radioNoeRahKhakRizi2').is(':checked')) {
        radioNoeRahKhakRizi = 2;
    }
    DarsadKRDDaneh = $('#txtDarsadKRDDaneh').val();
    DarsadKRRDaneh = $('#txtDarsadKRRDaneh').val();

    HajmBetween0To30 = $('#txtHajmBetween0To30').val();
    HajmBetween30To100 = $('#txtHajmBetween30To100').val();
    HajmBetweenTo100 = $('#txtHajmBetweenTo100').val();

    ///////////
    if (!$.isNumeric(DarsadKRDDaneh)) {
        $('#txtDarsadKRDDaneh').addClass('blinking');
        check = true;
    }
    else {
        $('#txtDarsadKRDDaneh').removeClass('blinking');
    }
    ////////
    if (!$.isNumeric(DarsadKRRDaneh)) {
        $('#txtDarsadKRRDaneh').addClass('blinking');
        check = true;
    }
    else {
        $('#txtDarsadKRRDaneh').removeClass('blinking');
    }
    //////////
    if (!$.isNumeric(HajmBetween0To30)) {
        $('#txtHajmBetween0To30').addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmBetween0To30').removeClass('blinking');
    }
    //////////
    if (!$.isNumeric(HajmBetween30To100)) {
        $('#txtHajmBetween30To100').addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmBetween30To100').removeClass('blinking');
    }
    //////
    if (!$.isNumeric(HajmBetweenTo100)) {
        $('#txtHajmBetweenTo100').addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmBetweenTo100').removeClass('blinking');
    }
    ///////////////


    strKhakRiziInfoDetails = '';
    $('#divKhakRiziInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else {
            $(this).removeClass('blinking');
            strKhakRiziInfoDetails += $(this).attr('id').substring(3, $(this).attr('id').length) + '_' + $.trim($(this).val()) + '$';
        }
    });

    if (!check) {
        strKhakRiziInfoDetailsCheckBox = '';
        $('#divKhakRiziInfoDetails input[type="checkbox"]').each(function () {
            strKhakRiziInfoDetailsCheckBox += $(this).attr('id').substring(2, $(this).attr('id').length) + '_' + $(this).is(':checked') + '$';
        });

        EzafeBahaKRKhakMosalah = $('#ckKREzafeBahaKhakMosalah').is(':checked');

        StateKhakRiziSaveOrEdit = $('#HDFStateAmalyateKhakiSaveOrEdit').val();

        if (StateKhakRiziSaveOrEdit == 'Add') {
            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId;
            vardata.Type = 3;
            vardata.FromKM = KMS;
            vardata.ToKM = KME;
            vardata.radioNoeRahKhakRizi = radioNoeRahKhakRizi;
            vardata.DarsadKRDDaneh = DarsadKRDDaneh;
            vardata.DarsadKRRDaneh = DarsadKRRDaneh;
            vardata.HajmBetween0To30 = HajmBetween0To30;
            vardata.HajmBetween30To100 = HajmBetween30To100;
            vardata.HajmBetweenTo100 = HajmBetweenTo100;
            vardata.EzafeBahaKRKhakMosalah = EzafeBahaKRKhakMosalah;
            vardata.KhakRiziInfoDetails = strKhakRiziInfoDetails;
            vardata.KhakRiziInfoDetailsCheckBox = strKhakRiziInfoDetailsCheckBox;
            $.ajax({
                type: "POST",
                url: "/KhakRizi/SaveKhakRiziInfoForBarAvord",
                data: JSON.stringify(vardata),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    info = response.split('_');
                    if (info[0] == "OK") {
                        $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
                        $('#HDFKMAmalyateKhakiIdForEdit').val(info[1]);
                        $('#HDFKMAmalyateKhakiNum').val(info[2]);
                        toastr.success('اطلاعات کیلومتراژ بدرستی ثبت گردید', 'ثبت');
                    }
                    else
                        toastr.error('مشکل در ثبت اطلاعات کیلومتراژ', 'خطا');
                },
                error: function (response) {
                    toastr.error('مشکل در ثبت اطلاعات کیلومتراژ', 'خطا');
                }
            });
        }
        else {
            KMKhakRiziId = $('#HDFKMAmalyateKhakiIdForEdit').val();
            KMKhakRiziNum = $('#HDFKMAmalyateKhakiNum').val();
            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId;
            vardata.KMKhakRiziId = KMKhakRiziId;
            vardata.KMNum = KMKhakRiziNum;
            vardata.FromKM = KMS;
            vardata.ToKM = KME;
            vardata.radioNoeRahKhakRizi = radioNoeRahKhakRizi;
            vardata.DarsadKRDDaneh = DarsadKRDDaneh;
            vardata.DarsadKRRDaneh = DarsadKRRDaneh;
            vardata.HajmBetween0To30 = HajmBetween0To30;
            vardata.HajmBetween30To100 = HajmBetween30To100;
            vardata.HajmBetweenTo100 = HajmBetweenTo100;
            vardata.EzafeBahaKRKhakMosalah = EzafeBahaKRKhakMosalah;
            vardata.KhakRiziInfoDetails = strKhakRiziInfoDetails;
            vardata.KhakRiziInfoDetailsCheckBox = strKhakRiziInfoDetailsCheckBox;
            $.ajax({
                type: "POST",
                url: "/AmalyateKhakiInfoForBarAvords/UpdateKhakRiziInfoForBarAvord",
                data: JSON.stringify(vardata),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    info = response.split('_');
                    if (info[0] == "OK") {
                        toastr.success('اطلاعات کیلومتراژ بدرستی ویرایش گردید', 'ثبت');
                    }
                    else
                        toastr.error('مشکل در ویرایش اطلاعات کیلومتراژ', 'خطا');
                },
                error: function (response) {
                    toastr.error('مشکل در ویرایش اطلاعات کیلومتراژ', 'خطا');
                }
            });
        }
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}

