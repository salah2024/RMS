function PayKaniClick(OpId) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    str = "";
    debugger;
    str += `

  <div class="col-12" id="divPayKaniExistingKM">
  </div>


      <!-- کیلومتراژ های ثبت شده -->

    <div class="khakbardari-container" style="border: 1px solid #cb92ff;background-color: #f3ebff;">
    <div class="row khakbardari-box">

    <!-- از کیلومتراژ -->
    <div class="col-md-1">
        <i class="fa fa-plus" style="font-size: 20px;color: green;"></i>
    </div>
    <div class="col-md-1 label-col">
      <span>از کیلومتراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForPayKani" value="000+000"/>
    </div>

    <!-- تا کیلومتراژ -->
    <div class="col-md-1 label-col">
      <span>تا کیلومتراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForPayKani" value="000+000"/>
    </div>

    <!-- حجم پی کنی -->
    <div class="col-md-2 label-col">
      <span>حجم پی کنی:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtHajmPayKani" value="0"/>
    </div>
    <div class="col-md-1 unit-col">
      <span>مترمکعب</span>
    </div>
  </div>

  <!-- بخش نمایش -->
  <div id="MainViewPayKaniNew" class="khakbardari-view" style="display:none">
  <div id="ViewPayKaniNew" class="khakbardari-view">
  </div>

 <!-- دکمه ذخیره -->
 <div class="row">
 <div class="col-12" style="direction: ltr;">
    <div class="col-md-2 action-col">
      <a class="btn buttonStyleBoard" style="color:#fff" onclick="SaveKhakBardariInfo('${BarAvordUserId}'` + ',' + `'Add')">
        ذخیره
      </a>
    </div><!--col-md-2 action-col -->
    </div><!--col-12 -->
    </div>

    </div><!-- بخش نمایش -->


</div>
    `;

    $('#ula' + OpId).html(str);
    ShowExistingKMPayKani(BarAvordUserId);

    ShowSelctionPayKani(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0);
}

function ShowExistingKMPayKani(BarAvordUserId) {
    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.Type = 1;
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
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForPayKani${KMNum}" value="${FromKMSplit}" onclick="event.stopPropagation();"/>
    </div>

    <!-- تا کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForPayKani${KMNum}" value="${ToKMSplit}" onclick="event.stopPropagation();"/>
    </div>

    <!-- حجم خاکبرداری -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtHajmPayKani${KMNum}" value="${Value}" onclick="event.stopPropagation();"/>
    </div>
        <div class="col-md-3" style="text-align:center">
        <span>جهت مشاهده جزییات کلیک نمایید</span>
</div>
   
    </div>
  <!-- بخش نمایش -->

  <div class="row col-12" style="direction:ltr" id="MainViewPayKani${KMNum}" class="khakbardari-view">
    <div id="ViewKhakBardari${KMNum}" class="khakbardari-view" style="direction: rtl;">
    </div>
    <div class="col-md-2 action-col">
      <a class="btn buttonStyleBoard" style="color:#fff" onclick="UpdateKhakBardariInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum})" onclick="event.stopPropagation();">
        ذخیره
      </a>
    </div>
  <div id="ViewRizMetreKH${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  <div id="ViewKhakBardariEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  </div><!-- MainViewPayKani -->
  </div>
    `;

                    //    str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionKMAmalyateKhaki($(this),\'' + KMExistingId + '\',\'' + KMNum
                    //        + '\')\" ondblclick=\"ShowSelctionKhakBardari(0,' + KMExistingId + ',' + KMNum + ',' + BarAvordUserId + ',' + "'" + FromKM + "'" + ',' + "'" + ToKM + "'" + ',' + "'" + FromKMSplit + "'" + ',' + "'" + ToKMSplit + "'" + ',' + Value + ')\">' + count++
                    //        + ' - کیلومتراژ' + '<label>' + FromKMSplit + ' - ' + ToKMSplit + '</label>' + '</a></div>';
                });

                $('#divExistingKM').html(strSEKB);
                $('#divExistingKM').find('#MainViewPayKani' + KMNum).hide();
            }

            //$('#ula' + OpId).find('divExistingKM').html(str);
            //$('#divViewExistingKMAmalyateKhaki').html(str);
            //$('#aViewExistingKMAmalyateKhaki').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
        }
    });
}

function ShowSelctionKhakBardari(IsNew, KMExistingId, KMNum, BarAvordId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value) {

    strSSKB = `

    <div id="divPayKaniInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
  <div class="row"
       style="padding:5px 0 0;
              border:1px solid #b1d3ec;
              border-radius:10px;
              background-color:#d3e4fc;
              font-size:12px;">

    <!-- ستون خالی -->
    <div class="col-1" style="display:none">
      <span>نوع</span>
    </div>
    <div class="col-4">
      <div class="col-12">
      <span>شرح عملیات خاکبرداری</span>
      </div>
    </div>

    <!-- ستون محتوا -->
    <div class="col-8" style="padding:0;">
      <div class="row" style="margin:0;">

        <!-- حجم خاکبرداری -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;">
            <span>حجم خاکبرداری</span>
          </div>
          <div class="row">
          <div class="col-6" style="text-align:center;padding:0;">
            <span>متر مکعب</span>
          </div>
          <div class="col-4" style="text-align:center;padding:0;">
            <span>درصد</span>
          </div>
          </div>
        </div>
       <!-- واریزه -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;">
            <span>واریزه</span>
          </div>
          <div class="row">
          <div class="col-6" style="text-align:center;padding:0;">
            <span>متر مکعب</span>
          </div>
          <div class="col-4" style="text-align:center;padding:0;">
            <span>درصد</span>
          </div>
          </div>
        </div>
        <!-- قابل مصرف در خاکریزی -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;padding:0;">
            <span>مصرف در خاکریزی</span>
          </div>
          <div class="row">
          <div class="col-6" style="text-align:center;padding:0;">
            <span>متر مکعب</span>
          </div>
          <div class="col-4" style="text-align:center;padding:0;">
            <span>درصد</span>
          </div>
          </div>
        </div>

        <!-- حمل به دپو/مسیر -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;">
            <span>حمل به دپو</span>
          </div>
          <div class="row">
          <div class="col-6" style="text-align:center;padding:0;">
            <span>متر مکعب</span>
          </div>
          <div class="col-4" style="text-align:center;padding:0;">
            <span>درصد</span>
          </div>
          </div>
        </div>


      </div>
    </div>
  </div>
</div>   `
    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/ReturnNoeKhakBardari",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {


            ActivityTitleComplete = data;

            ActivityLength = ActivityTitleComplete.length;
            for (let i = 0; i < ActivityTitleComplete.length; i++) {
                strSSKB += `
        <div class="container-fluid" style="width:100%;margin:0;padding:0;">
        <div class="row" style="padding:2px 0px;margin:2px 0px;border-bottom:1px solid #ccc;">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
        <input id="txtKhakBardariItemId${i + 1}" value="${ActivityTitleComplete[i].id}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanKhakBardariItems${i + 1}" class="spanStyleKhakBardariItems" style="font-size:12px;">
        ${ActivityTitleComplete[i].title}
      </span>
    </div>

    <!-- ستون اصلی محتوا -->
    <div class="col-8" style="padding:0;">
      <div class="row" style="margin:0;">

        <!-- حجم خاکبرداری -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetail${i + 1}" value="0" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsad${i + 1}" value="0" />
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVarizi${i + 1}" value="0"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVariziPK${i + 1}" value="0"  />
            </div>
          </div>
        </div>

        <!-- قابل مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajm${i + 1}" value="0"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsadPK${i + 1}" value="0"  />
            </div>
          </div>
        </div>

        <!-- حمل به دپو/مسیر -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHaml${i + 1}" value="0"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHamlPK${i + 1}" value="0"  />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
            }

            //////////////
            $('#ViewPayKaniNew').html(strSSKB);

            $('#ViewPayKaniNew input[type="text"]').change(function () {
                debugger;
                let changedId = $(this).attr("id");
                let i = changedId.match(/\d+/) ? changedId.match(/\d+/)[0] : ""; // شماره ردیف
                let HajmKhakBardari = parseFloat($.trim($('#txtHajmPayKani').val()));

                // 🚨 اعتبارسنجی
                if (!$.isNumeric($(this).val())) {
                    toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('ErrorValueStyle');
                    return;
                }
                else {
                    $(this).removeClass('ErrorValueStyle');
                }

                if (HajmKhakBardari == 0 || HajmKhakBardari == '' || !$.isNumeric(HajmKhakBardari)) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmPayKani').addClass('ErrorValueStyle');
                    return;
                } else {
                    $('#txtHajmPayKani').removeClass('ErrorValueStyle');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetail" + i).val()) || 0;

                let varizi = parseFloat($("#txtVarizi" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajm" + i).val()) || 0;
                let haml = parseFloat($("#txtHaml" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVariziPK" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsadPK" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHamlPK" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail")) {
                    let Zarb = khDetail / HajmKhakBardari * 100;
                    $("#txtDarsad" + i).val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmKhakBardari) {
                        let NewVal = HajmKhakBardari - (SumAll - khDetail);
                        $("#txtKhDetail" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = khDetail / HajmKhakBardari * 100;
                        $("#txtDarsad" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVarizi" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajm" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHaml" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                }

                if (changedId.includes("Darsad")
                    && !changedId.includes("Varizi")
                    && !changedId.includes("ReUse")
                    && !changedId.includes("Haml")) {

                    let dVal = parseFloat($(this).val()) || 0;

                    // مجموع درصد همه ردیف‌ها منهای همین ردیف
                    let SumOther = ReturnSumAllDardad() - dVal;

                    // اگه بیشتر از 100 بشه، اصلاح کن
                    if (SumOther + dVal > 100) {
                        dVal = 100 - SumOther;
                        if (dVal < 0) dVal = 0; // امنیتی
                        $(this).val(dVal.toFixed(2));
                    }

                    // حالا حجم این ردیف رو بر اساس درصد اصلاح‌شده محاسبه کن
                    let newKhDetail = (dVal / 100) * HajmKhakBardari;
                    $('#txtKhDetail' + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi")) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVarizi" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajm" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHaml" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        dVarizi = (varizi / khDetail) * 100;
                        $("#txtDarsadVariziPK" + i).val(dVarizi.toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsadPK" + i).val(dReuse.toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (haml / khDetail) * 100;
                        $("#txtDarsadHamlPK" + i).val(dHaml.toFixed(2));
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVarizi" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajm" + i).val()) || 0;
                haml = parseFloat($("#txtHaml" + i).val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVariziPK" + i).val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsadPK" + i).val()) || 0;
                dHaml = parseFloat($("#txtDarsadHamlPK" + i).val()) || 0;

                // کنترل مجموع درصدها
                let dSum = dVarizi + dReuse + dHaml;
                if (dSum > 100) {
                    let extra = dSum - 100;
                    if (changedId.includes("DarsadVarizi")) {
                        dVarizi -= extra;
                        $("#txtDarsadVariziPK" + i).val(dVarizi.toFixed(2));
                        $("#txtVarizi" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        dReuse -= extra;
                        $("#txtReUseDarsadPK" + i).val(dReuse.toFixed(2));
                        $("#txtReUseHajm" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        dHaml -= extra;
                        $("#txtDarsadHamlPK" + i).val(dHaml.toFixed(2));
                        $("#txtHaml" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        varizi -= extra;
                        $("#txtVarizi" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVariziPK" + i).val(((varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        reuseHajm -= extra;
                        $("#txtReUseHajm" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsadPK" + i).val(((reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        haml -= extra;
                        $("#txtHaml" + i).val(haml.toFixed(2));
                        $("#txtDarsadHamlPK" + i).val(((haml / khDetail) * 100).toFixed(2));
                    }
                }
            });

            $('#txtHajmPayKani').change(function () {

                debugger;

                let HKB = parseFloat($(this).val());

                var KMS = parseFloat($('#txtFromKMForPayKani').val().replace('+', ''));
                var KME = parseFloat($('#txtToKMForPayKani').val().replace('+', ''));

                if (KMS == 0 || KME == 0) {
                    $('#txtFromKMForPayKani').addClass('blinking');
                    $('#txtToKMForPayKani').addClass('blinking');
                    return;
                }

                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForPayKani').addClass('blinking');
                    $('#txtFromKMForPayKani').addClass('blinking');
                    return;
                }
                else {
                    $('#txtToKMForPayKani').removeClass('blinking');
                    $('#txtFromKMForPayKani').removeClass('blinking');
                }

                //OverLowKMCheck(KMS, KME);


                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                $('#MainViewPayKaniNew').slideDown(500);
                $('#divPayKaniInfoDetails').show();
                $(this).removeClass('blinking');

                let totalAssigned = 0;
                let lastIndex = -1;

                for (let i = 1; i <= ActivityLength; i++) {
                    let Darsad = parseFloat($('#txtDarsad' + i).val()) || 0;

                    if (Darsad > 0) {
                        let KhDetail = (Darsad / 100 * HKB);

                        // حجم اصلی
                        $('#txtKhDetail' + i).val(KhDetail.toFixed(2));

                        // درصدهای مرتبط
                        let dVarizi = parseFloat($('#txtDarsadVariziPK' + i).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadPK' + i).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlPK' + i).val()) || 0;

                        // محاسبه حجم هر بخش
                        let vVarizi = (dVarizi / 100) * KhDetail;
                        let vReUse = (dReUse / 100) * KhDetail;
                        let vHaml = (dHaml / 100) * KhDetail;

                        // ست کردن نتایج
                        $('#txtVarizi' + i).val(vVarizi.toFixed(2));
                        $('#txtReUseHajm' + i).val(vReUse.toFixed(2));
                        $('#txtHaml' + i).val(vHaml.toFixed(2));

                        totalAssigned += parseFloat(KhDetail.toFixed(2));
                        lastIndex = i;
                    }
                }

                // جبران خطای رندینگ روی آخرین ردیف
                if (lastIndex > -1) {
                    let diff = HKB - totalAssigned;
                    if (Math.abs(diff) >= 0.01) {
                        let lastVal = parseFloat($('#txtKhDetail' + lastIndex).val()) || 0;
                        let newVal = lastVal + diff;

                        $('#txtKhDetail' + lastIndex).val(newVal.toFixed(2));

                        // بروزرسانی دوباره بخش‌های وابسته به این ردیف
                        let dVarizi = parseFloat($('#txtDarsadVariziPK' + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadPK' + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlPK' + lastIndex).val()) || 0;

                        $('#txtVarizi' + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtReUseHajm' + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtHaml' + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }
            });

            $('#txtFromKMForPayKani').change(function () {
                var KM = $(this).val();
                var KMSplit = KM.split('+');
                if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
                    $(this).addClass('blinking');
                    toastr.info('کیلومتراژ شروع وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
                }
                else {
                    $(this).removeClass('blinking');
                }


                var KMS = parseFloat(KM.replace('+', ''));
                var KME = parseFloat($('#txtToKMForPayKani').val().replace('+', ''));
                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForPayKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForPayKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmPayKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                        $('#txtHajmPayKani').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtHajmPayKani').removeClass('blinking');
                    }

                    $('#MainViewPayKaniNew').slideDown(500);
                    $('#divPayKaniInfoDetails').show();
                }
            });

            $('#txtToKMForPayKani').change(function () {
                debugger;
                var KM = $(this).val();
                var KMSplit = KM.split('+');
                if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
                    $(this).addClass('blinking');
                    toastr.info('کیلومتراژ خاتمه وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
                }
                else {
                    $(this).removeClass('blinking');
                }


                var KME = parseFloat(KM.replace('+', ''));
                var KMS = parseFloat($('#txtFromKMForPayKani').val().replace('+', ''));
                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForPayKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForPayKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmPayKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                        $('#txtHajmPayKani').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtHajmPayKani').removeClass('blinking');
                    }

                    $('#MainViewPayKaniNew').slideDown(500);
                    $('#divPayKaniInfoDetails').show();
                }
            });



            if (IsNew == 0) {
                $('#txtFromKMForPayKani').val(FromKMSplit);
                $('#txtToKMForPayKani').val(ToKMSplit);
                $('#txtHajmPayKani').val(Value);
                $('#divPayKaniInfoDetails').show();
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
                $('#btnCloseExistingKMAmalyateKhaki').click();
                var vardata = new Object();
                vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
                $.ajax({
                    type: "POST",
                    url: "/AmalyateKhakiInfoForBarAvordDetails/GetDetailsOfKMKhakBardariInfoWithKMKhakBardariId",
                    data: JSON.stringify(vardata),
                    //data: '{AmalyateKhakiInfoForBarAvordId:' + KMExistingId + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var xmlDoc = $.parseXML(response);
                        var xml = $(xmlDoc);
                        var KMAmalyateKhakiBarAvordDetails = xml.find("tblKMAmalyateKhakiBarAvordDetails");
                        var KMAmalyateKhakiBarAvordMore = xml.find("tblKMAmalyateKhakiBarAvordMore");
                        var KMAmalyateRizeshBarAvordDetailsMore = xml.find("tblKMAmalyateKhakiBarAvordDetailsMore");
                        var KMAmalyateRizeshBarAvordDetailsEzafeBaha = xml.find("tblKMAmalyateKhakiBarAvordDetailsEzafeBaha");

                        Value = 0;
                        $.each(KMAmalyateKhakiBarAvordMore, function () {
                            Name = $.trim($(this).find("_Name").text());
                            if (Name == 'HKB') {
                                Value = $.trim($(this).find("_Value").text());
                                $('#txtHajmPayKani').val(Value);
                            }
                        });

                        $.each(KMAmalyateKhakiBarAvordDetails, function () {
                            Id = $(this).find("_ID").text();
                            AmalyateKhakiInfoForBarAvordId = $(this).find("_AmalyateKhakiInfoForBarAvordId").text();
                            Type = $(this).find("_Type").text();

                            $.each(KMAmalyateRizeshBarAvordDetailsMore, function () {
                                CurrentId = $(this).find("_ID").text();
                                Name = $.trim($(this).find("_Name").text());
                                ValueMore = $(this).find("_Value").text();
                                console.log(Name);
                                AmalyateKhakiInfoForBarAvordDetailsId = $(this).find("_AmalyateKhakiInfoForBarAvordDetailsId").text();
                                if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                                    $('#txt' + Name + Type).val(ValueMore);
                                }
                            });

                            CurrentValue = $('#txtKhDetail' + Type).val();
                            ValueOfReCycle = $('#txtReUseHajm' + Type).val();
                            ValueOfVarize = $('#txtVarizi' + Type).val();
                            ValueOfHaml = $('#txtHaml' + Type).val();
                            ValueOfFaseleHaml = $('#txtFaseleHaml' + Type).val();

                            $('#txtDarsad' + Type).val(parseFloat(Value) == 0 ? 0 : (parseFloat(CurrentValue) / parseFloat(Value) * 100).toFixed(2));
                            $('#txtReUseDarsadPK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(2));
                            $('#txtDarsadVariziPK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(2));
                            $('#txtDarsadHamlPK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(2));
                            $('#txtDarsadFaseleHaml' + Type).val(parseFloat(ValueOfVarize) == 0 ? 0 : (parseFloat(ValueOfFaseleHaml) / parseFloat(ValueOfVarize) * 100).toFixed(2));

                            $.each(KMAmalyateRizeshBarAvordDetailsEzafeBaha, function () {
                                CurrentId = $(this).find("_ID").text();
                                Name = $.trim($(this).find("_Name").text());
                                boolValue = $(this).find("_Value").text() == 'true' ? true : false;
                                AmalyateKhakiInfoForBarAvordDetailsId = $(this).find("_AmalyateKhakiInfoForBarAvordDetailsId").text();

                                if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                                    $('#ck' + Name + Type).attr('checked', boolValue);
                                }
                            });
                        });
                    },
                    error: function (response) {
                        toastr.error('مشکل در بارگذاری کیلومتراژ انتخابی', 'خطا');
                    }
                });
            }
            else {
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Add');
            }
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری خاکبرداری', 'خطا');
        }
    });
}
