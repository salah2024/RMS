ActivityLength = 0;

function KhakBardariMashinWithBarAvordIdClick(OpId, BarAvordUserId) {

    str = "";
    debugger;
    str += `

  <div class="col-12" id="divExistingKM">
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
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForKhakbardari" value="000+000"/>
    </div>

    <!-- تا کیلومتراژ -->
    <div class="col-md-1 label-col">
      <span>تا کیلومتراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForKhakbardari" value="000+000"/>
    </div>

    <!-- حجم خاکبرداری -->
    <div class="col-md-2 label-col">
      <span>حجم خاکبرداری:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtHajmKhakBardari" value="0"/>
    </div>
    <div class="col-md-1 unit-col">
      <span>مترمکعب</span>
    </div>
  </div>

  <!-- بخش نمایش -->
  <div id="MainViewKhakBardariNew" class="khakbardari-view" style="display:none">
  <div id="ViewKhakBardariNew" class="khakbardari-view">
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
    ShowExistingKMKhakBardari(BarAvordUserId);

    //ShowExistingKMKhakBardari(BarAvordUserId);

    ShowSelctionKhakBardari(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0);

}

function ShowSelctionKhakBardari(IsNew, KMExistingId, KMNum, BarAvordId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value) {

    strSSKB = `

    <div id="divKhakBardariInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVarizi${i + 1}" value="0"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsad${i + 1}" value="0"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHaml${i + 1}" value="0"  />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
            }

            //////////////
            $('#ViewKhakBardariNew').html(strSSKB);

            $('#ViewKhakBardariNew input[type="text"]').change(function () {
                debugger;
                let changedId = $(this).attr("id");
                let i = changedId.match(/\d+/) ? changedId.match(/\d+/)[0] : ""; // شماره ردیف
                let HajmKhakBardari = parseFloat($.trim($('#txtHajmKhakBardari').val()));

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
                    $('#txtHajmKhakBardari').addClass('ErrorValueStyle');
                    return;
                } else {
                    $('#txtHajmKhakBardari').removeClass('ErrorValueStyle');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetail" + i).val()) || 0;

                let varizi = parseFloat($("#txtVarizi" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajm" + i).val()) || 0;
                let haml = parseFloat($("#txtHaml" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVarizi" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsad" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHaml" + i).val()) || 0;

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
                        $("#txtDarsadVarizi" + i).val(dVarizi.toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsad" + i).val(dReuse.toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (haml / khDetail) * 100;
                        $("#txtDarsadHaml" + i).val(dHaml.toFixed(2));
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVarizi" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajm" + i).val()) || 0;
                haml = parseFloat($("#txtHaml" + i).val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVarizi" + i).val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsad" + i).val()) || 0;
                dHaml = parseFloat($("#txtDarsadHaml" + i).val()) || 0;

                // کنترل مجموع درصدها
                let dSum = dVarizi + dReuse + dHaml;
                if (dSum > 100) {
                    let extra = dSum - 100;
                    if (changedId.includes("DarsadVarizi")) {
                        dVarizi -= extra;
                        $("#txtDarsadVarizi" + i).val(dVarizi.toFixed(2));
                        $("#txtVarizi" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        dReuse -= extra;
                        $("#txtReUseDarsad" + i).val(dReuse.toFixed(2));
                        $("#txtReUseHajm" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        dHaml -= extra;
                        $("#txtDarsadHaml" + i).val(dHaml.toFixed(2));
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
                        $("#txtDarsadVarizi" + i).val(((varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        reuseHajm -= extra;
                        $("#txtReUseHajm" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsad" + i).val(((reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        haml -= extra;
                        $("#txtHaml" + i).val(haml.toFixed(2));
                        $("#txtDarsadHaml" + i).val(((haml / khDetail) * 100).toFixed(2));
                    }
                }
            });

            $('#txtHajmKhakBardari').change(function () {

                debugger;

                let HKB = parseFloat($(this).val());

                var KMS = parseFloat($('#txtFromKMForKhakbardari').val().replace('+', ''));
                var KME = parseFloat($('#txtToKMForKhakbardari').val().replace('+', ''));

                if (KMS == 0 || KME == 0) {
                    $('#txtFromKMForKhakbardari').addClass('blinking');
                    $('#txtToKMForKhakbardari').addClass('blinking');
                    return;
                }

                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForKhakbardari').addClass('blinking');
                    $('#txtFromKMForKhakbardari').addClass('blinking');
                    return;
                }
                else {
                    $('#txtToKMForKhakbardari').removeClass('blinking');
                    $('#txtFromKMForKhakbardari').removeClass('blinking');
                }

                //OverLowKMCheck(KMS, KME);


                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                $('#MainViewKhakBardariNew').slideDown(500);
                $('#divKhakBardariInfoDetails').show();
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
                        let dVarizi = parseFloat($('#txtDarsadVarizi' + i).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsad' + i).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHaml' + i).val()) || 0;

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
                        let dVarizi = parseFloat($('#txtDarsadVarizi' + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsad' + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHaml' + lastIndex).val()) || 0;

                        $('#txtVarizi' + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtReUseHajm' + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtHaml' + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }
            });

            $('#txtFromKMForKhakbardari').change(function () {
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
                var KME = parseFloat($('#txtToKMForKhakbardari').val().replace('+', ''));
                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForKhakbardari').addClass('blinking');
                }
                else {
                    $('#txtToKMForKhakbardari').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmKhakBardari').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                        $('#txtHajmKhakBardari').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtHajmKhakBardari').removeClass('blinking');
                    }

                    $('#MainViewKhakBardariNew').slideDown(500);
                    $('#divKhakBardariInfoDetails').show();
                }
            });

            $('#txtToKMForKhakbardari').change(function () {
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
                var KMS = parseFloat($('#txtFromKMForKhakbardari').val().replace('+', ''));
                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForKhakbardari').addClass('blinking');
                }
                else {
                    $('#txtToKMForKhakbardari').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmKhakBardari').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                        $('#txtHajmKhakBardari').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtHajmKhakBardari').removeClass('blinking');
                    }

                    $('#MainViewKhakBardariNew').slideDown(500);
                    $('#divKhakBardariInfoDetails').show();
                }
            });



            if (IsNew == 0) {
                $('#txtFromKMForKhakbardari').val(FromKMSplit);
                $('#txtToKMForKhakbardari').val(ToKMSplit);
                $('#txtHajmKhakBardari').val(Value);
                $('#divKhakBardariInfoDetails').show();
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
                                $('#txtHajmKhakBardari').val(Value);
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
                            $('#txtReUseDarsad' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(2));
                            $('#txtDarsadVarizi' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(2));
                            $('#txtDarsadHaml' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(2));
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

function ShowExistingKMKhakBardari(BarAvordUserId) {
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
  <div id="ViewRizMetreKH${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  <div id="ViewKhakBardariEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  </div><!-- MainViewKhakBardari -->
  </div>
    `;

                    //    str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionKMAmalyateKhaki($(this),\'' + KMExistingId + '\',\'' + KMNum
                    //        + '\')\" ondblclick=\"ShowSelctionKhakBardari(0,' + KMExistingId + ',' + KMNum + ',' + BarAvordUserId + ',' + "'" + FromKM + "'" + ',' + "'" + ToKM + "'" + ',' + "'" + FromKMSplit + "'" + ',' + "'" + ToKMSplit + "'" + ',' + Value + ')\">' + count++
                    //        + ' - کیلومتراژ' + '<label>' + FromKMSplit + ' - ' + ToKMSplit + '</label>' + '</a></div>';
                });

                $('#divExistingKM').html(strSEKB);
                $('#divExistingKM').find('#MainViewKhakBardari' + KMNum).hide();
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

function ViewKhakBardariInfo(KMExistingId, KMNum, BarAvordId) {
    //$('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
    if ($('#MainViewKhakBardari' + KMNum).is(':visible')) {
        $('#MainViewKhakBardari' + KMNum).slideUp(500);
        //$('#ViewRizMetreKH' + KMNum).slideUp(500);
        return
    }

    $('#ViewKhakBardariEzafeBaha' + KMNum).html('');

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/GetDetailsOfKMKhakBardariInfoWithKMKhakBardariId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            KMAmalyateKhakiBarAvordDetailsMore = response.kmAmalyateKhakiBarAvordDetailsMore;
            KMAmalyateKhakiBarAvordDetails = response.kmAmalyateKhakiBarAvordDetails;
            lstAKhInfoRizMetre = response.lstAKhInfoRizMetre;
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;

            Value = 0;
            strKMAK = `

    <div id="divKhakBardariInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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


            var totalKhDetail = 0;
            var totalDarsadKhDetail = 0;
            var totalVarizi = 0;
            var totalDarsadVarizi = 0;
            var totalReUseHajm = 0;
            var totalDarsadReUseHajm = 0;
            var totalHaml = 0;
            var totalDarsadHaml = 0;

            i = 1;
            $.each(KMAmalyateKhakiBarAvordDetails, function () {
                strKMAK += `
        <div class="container-fluid" style="width:100%;margin:0;padding:0;">
        <div class="row" style="padding:2px 0px;margin:2px 0px;border-bottom:1px solid #ccc;">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
    <input id="txtKhakBardariItemId${KMNum}_${i}" value="${this.noeKhakBardariId}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanKhakBardariItems${KMNum}_${i}" class="spanStyleKhakBardariItems" style="font-size:12px;">
        ${this.title}
      </span>
    </div>`;

                debugger;

                let result = KMAmalyateKhakiBarAvordDetailsMore.filter(x => x.amalyateKhakiInfoForBarAvordDetailsId === this.id);

                let KhDetail = result.filter(x => x.name === "KhDetail").length > 0 ? result.filter(x => x.name === "KhDetail")[0].value : 0;
                let DarsadKhDetail = result.filter(x => x.name === "DarsadKhDetail").length > 0 ? result.filter(x => x.name === "DarsadKhDetail")[0].value : 0;


                // جمع کردن مقادیر
                totalKhDetail += parseFloat(KhDetail);
                totalDarsadKhDetail += parseFloat(DarsadKhDetail);


                strKMAK += ` <div class="col-8" style="padding:0;">
          <div class="row" style="margin:0;">

        <!-- حجم خاکبرداری -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetail${KMNum}_${i}" value="${KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsad${KMNum}_${i}" value="${DarsadKhDetail}" />
            </div>
          </div>
        </div>`;


                let Varizi = result.filter(x => x.name === "Varizi").length > 0 ? result.filter(x => x.name === "Varizi")[0].value : 0;
                let DarsadVarizi = result.filter(x => x.name === "DarsadVarizi").length > 0 ? result.filter(x => x.name === "DarsadVarizi")[0].value : 0;
                totalVarizi += parseFloat(Varizi);
                totalDarsadVarizi += parseFloat(DarsadVarizi);
                strKMAK += `
         <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajm${KMNum}_${i}" value="${Varizi}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsad${KMNum}_${i}" value="${DarsadVarizi}"  />
            </div>
          </div>
        </div>`

                let ReUseHajm = result.filter(x => x.name === "ReUseHajm").length > 0 ? result.filter(x => x.name === "ReUseHajm")[0].value : 0;
                let DarsadReUseHajm = result.filter(x => x.name === "DarsadReUseHajm").length > 0 ? result.filter(x => x.name === "DarsadReUseHajm")[0].value : 0;
                totalReUseHajm += parseFloat(ReUseHajm);
                totalDarsadReUseHajm += parseFloat(DarsadReUseHajm);
                strKMAK += `
          <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVarizi${KMNum}_${i}" value="${ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVarizi${KMNum}_${i}" value="${DarsadReUseHajm}"  />
            </div>
          </div>
        </div>`
                let Haml = result.filter(x => x.name === "Haml").length > 0 ? result.filter(x => x.name === "Haml")[0].value : 0;
                let DarsadHaml = result.filter(x => x.name === "DarsadHaml").length > 0 ? result.filter(x => x.name === "DarsadHaml")[0].value : 0;
                totalHaml += parseFloat(Haml);
                totalDarsadHaml += parseFloat(DarsadHaml);
                strKMAK += `
        <!-- حمل به دپو/مسیر -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHaml${KMNum}_${i}" value="${Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHaml${KMNum}_${i}" value="${DarsadHaml}"  />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
                i++;
            });



            // توابع کمکی برای رُند کردن
            const fmt = (n) => {
                const num = Number(n);
                return Number.isInteger(num) ? num.toString() : parseFloat(num.toFixed(2)).toString();
            };

            strKMAK += `
<div class="container-fluid" style="width:100%;margin:0;padding:0;">
  <div class="row" 
       style="padding:4px 0;margin:2px 0;
              border-top:2px solid #333;
              background:#f9f9f9;
              font-weight:bold;">

    <div class="col-4" style="text-align:right;">
      جمع کل
    </div>

    <div class="col-8" style="padding:0;">
      <div class="row" style="margin:0;">
        
        <!-- حجم خاکبرداری -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span>${fmt(totalKhDetail)}</span>
            </div>
            <div class="col-4" style="text-align:center;padding:0 2px;">
              <span>%${fmt(totalDarsadKhDetail)}</span>
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span>${fmt(totalVarizi)}</span>
            </div>
            
          </div>
        </div>

        <!-- مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span>${fmt(totalReUseHajm)}</span>
            </div>
            
          </div>
        </div>

        <!-- حمل به دپو -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span>${fmt(totalHaml)}</span>
            </div>
            
          </div>
        </div>

      </div>
    </div>

  </div>
</div>`;


            debugger;


            $('div[id^="MainViewKhakBardari"]').slideUp();
            //$('div[id^="ViewRizMetreKH"]').slideUp();

            $('#ViewKhakBardari' + KMNum).html(strKMAK);
            $('#MainViewKhakBardari' + KMNum).slideDown();

            HajmKhakBardari = $('#txtHajmKhakBardari' + KMNum).val();


            $('#ViewKhakBardari' + KMNum + ' input[type="text"]').off('change').change(function () {
                debugger;
                let changedId = $(this).attr("id");
                let i = changedId.split("_")[1];
                let HajmKhakBardari = parseFloat($.trim($('#txtHajmKhakBardari' + KMNum).val()));

                // 🚨 اعتبارسنجی
                if (!$.isNumeric($(this).val())) {
                    toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('ErrorValueStyle');
                    return;
                } else {
                    $(this).removeClass('ErrorValueStyle');
                }

                if (HajmKhakBardari == 0 || HajmKhakBardari == '' || !$.isNumeric(HajmKhakBardari)) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmKhakBardari' + KMNum).addClass('ErrorValueStyle');
                    return;
                } else {
                    $('#txtHajmKhakBardari' + KMNum).removeClass('ErrorValueStyle');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetail" + KMNum + "_" + i).val()) || 0;

                let varizi = parseFloat($("#txtVarizi" + KMNum + "_" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajm" + KMNum + "_" + i).val()) || 0;
                let haml = parseFloat($("#txtHaml" + KMNum + "_" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVarizi" + KMNum + "_" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsad" + KMNum + "_" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHaml" + KMNum + "_" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail" + KMNum)) {
                    let Zarb = khDetail / HajmKhakBardari * 100;
                    $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetailsForEdit(KMNum);
                    if (SumAll > HajmKhakBardari) {
                        let NewVal = HajmKhakBardari - (SumAll - khDetail);
                        $("#txtKhDetail" + KMNum + "_" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = khDetail / HajmKhakBardari * 100;
                        $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVarizi" + KMNum + "_" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajm" + KMNum + "_" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHaml" + KMNum + "_" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                }

                if (changedId.includes("Darsad" + KMNum)
                    && !changedId.includes("Varizi" + KMNum)
                    && !changedId.includes("ReUse" + KMNum)
                    && !changedId.includes("Haml" + KMNum)) {
                    debugger;

                    let dVal = parseFloat($(this).val()) || 0;

                    // مجموع درصد همه ردیف‌ها منهای همین ردیف
                    let SumOther = ReturnSumAllDardadForEdit(KMNum) - dVal;

                    // اگه بیشتر از 100 بشه، اصلاح کن
                    if (SumOther + dVal > 100) {
                        dVal = 100 - SumOther;
                        if (dVal < 0) dVal = 0; // امنیتی
                        $(this).val(dVal.toFixed(2));
                    }

                    // حالا حجم این ردیف رو بر اساس درصد اصلاح‌شده محاسبه کن
                    let newKhDetail = (dVal / 100) * HajmKhakBardari;
                    $('#txtKhDetail' + KMNum + "_" + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVarizi" + KMNum + "_" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajm" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHaml" + KMNum + "_" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dVarizi = (varizi / khDetail) * 100;
                        $("#txtDarsadVarizi" + KMNum + "_" + i).val(dVarizi.toFixed(2));
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        dReuse = (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsad" + KMNum + "_" + i).val(dReuse.toFixed(2));
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dHaml = (haml / khDetail) * 100;
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(2));
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVarizi" + KMNum + "_" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajm" + KMNum + "_" + i).val()) || 0;
                haml = parseFloat($("#txtHaml" + KMNum + "_" + i).val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVarizi" + KMNum + "_" + i).val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsad" + KMNum + "_" + i).val()) || 0;
                dHaml = parseFloat($("#txtDarsadHaml" + KMNum + "_" + i).val()) || 0;

                // کنترل مجموع درصدها
                let dSum = dVarizi + dReuse + dHaml;
                if (dSum > 100) {
                    let extra = dSum - 100;
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        dVarizi -= extra;
                        $("#txtDarsadVarizi" + KMNum + "_" + i).val(dVarizi.toFixed(2));
                        $("#txtVarizi" + KMNum + "_" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        dReuse -= extra;
                        $("#txtReUseDarsad" + KMNum + "_" + i).val(dReuse.toFixed(2));
                        $("#txtReUseHajm" + KMNum + "_" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        dHaml -= extra;
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(2));
                        $("#txtHaml" + KMNum + "_" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        varizi -= extra;
                        $("#txtVarizi" + KMNum + "_" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVarizi" + KMNum + "_" + i).val(((varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        reuseHajm -= extra;
                        $("#txtReUseHajm" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsad" + KMNum + "_" + i).val(((reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        haml -= extra;
                        $("#txtHaml" + KMNum + "_" + i).val(haml.toFixed(2));
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(((haml / khDetail) * 100).toFixed(2));
                    }
                }
            });


            $('#txtHajmKhakBardari' + KMNum).off('change').change(function () {
                debugger;


                let HKB = parseFloat($(this).val());

                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('ErrorValueStyle');
                    return;
                }

                $('#MainViewKhakBardariNew').slideDown(500);
                $('#divKhakBardariInfoDetails').show();
                $(this).removeClass('ErrorValueStyle');

                let totalAssigned = 0;
                let lastIndex = -1;

                for (let i = 1; i <= ActivityLength; i++) {
                    let Darsad = parseFloat($('#txtDarsad' + KMNum + "_" + i).val()) || 0;

                    if (Darsad > 0) {
                        let KhDetail = (Darsad / 100 * HKB);

                        // حجم اصلی
                        $('#txtKhDetail' + KMNum + "_" + i).val(KhDetail.toFixed(2));

                        // درصدهای مرتبط
                        let dVarizi = parseFloat($('#txtDarsadVarizi' + KMNum + "_" + i).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsad' + KMNum + "_" + i).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHaml' + KMNum + "_" + i).val()) || 0;

                        // محاسبه حجم هر بخش
                        let vVarizi = (dVarizi / 100) * KhDetail;
                        let vReUse = (dReUse / 100) * KhDetail;
                        let vHaml = (dHaml / 100) * KhDetail;

                        // ست کردن نتایج
                        $('#txtVarizi' + KMNum + "_" + i).val(vVarizi.toFixed(2));
                        $('#txtReUseHajm' + KMNum + "_" + i).val(vReUse.toFixed(2));
                        $('#txtHaml' + KMNum + "_" + i).val(vHaml.toFixed(2));

                        totalAssigned += parseFloat(KhDetail.toFixed(2));
                        lastIndex = i;
                    }
                }

                // جبران خطای رندینگ روی آخرین ردیف
                if (lastIndex > -1) {
                    let diff = HKB - totalAssigned;
                    if (Math.abs(diff) >= 0.01) {
                        let lastVal = parseFloat($('#txtKhDetail' + KMNum + "_" + lastIndex).val()) || 0;
                        let newVal = lastVal + diff;

                        $('#txtKhDetail' + KMNum + "_" + lastIndex).val(newVal.toFixed(2));

                        // بروزرسانی دوباره بخش‌های وابسته به این ردیف
                        let dVarizi = parseFloat($('#txtDarsadVarizi' + KMNum + "_" + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsad' + KMNum + "_" + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHaml' + KMNum + "_" + lastIndex).val()) || 0;

                        $('#txtVarizi' + KMNum + "_" + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtReUseHajm' + KMNum + "_" + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtHaml' + KMNum + "_" + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }

                UpdateKhakBardariInfo(KMExistingId, BarAvordId, KMNum);

            });

            ShowRizMetreKH(KMExistingId, lstAKhInfoRizMetre, lstItemFBShomarehForGet, KMNum);

            ShowA_KhEzafeBaha(KMExistingId, KMNum);

            KMAmalyateKhakiBarAvordDetails = response.kmAmalyateKhakiBarAvordDetails;
            $.each(KMAmalyateKhakiBarAvordDetails, function () {
                Id = this.id;
                AmalyateKhakiInfoForBarAvordId = this.amalyateKhakiInfoForBarAvordId;
                Type = this.type;
            });

            KMAmalyateRizeshBarAvordDetailsMore = response.kmAmalyateRizeshBarAvordDetailsMore;
            $.each(KMAmalyateRizeshBarAvordDetailsMore, function () {
                CurrentId = this.id;
                Name = $.trim(this.name);
                ValueMore = this.value;
                AmalyateKhakiInfoForBarAvordDetailsId = this.amalyateKhakiInfoForBarAvordDetailsId;
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
            $('#txtReUseDarsad' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadVarizi' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadHaml' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadFaseleHaml' + Type).val(parseFloat(ValueOfVarize) == 0 ? 0 : (parseFloat(ValueOfFaseleHaml) / parseFloat(ValueOfVarize) * 100).toFixed(2));

            KMAmalyateRizeshBarAvordDetailsEzafeBaha = response.kmAmalyateRizeshBarAvordDetailsEzafeBaha;
            $.each(KMAmalyateRizeshBarAvordDetailsEzafeBaha, function () {
                CurrentId = this.id;
                Name = $.trim(this.name);
                boolValue = this.value == 'true' ? true : false;
                AmalyateKhakiInfoForBarAvordDetailsId = this.amalyateKhakiInfoForBarAvordDetailsId;

                if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                    $('#ck' + Name + Type).attr('checked', boolValue);
                }
            });


        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری کیلومتراژ انتخابی', 'خطا');
        }
    });

    $('#ViewKhakBardari' + KMNum).html();
}

function ShowRizMetreKH(KMExistingId, lstAKhInfoRizMetre, lstItemFBShomarehForGet, KMNum) {
    debugger;

    var str = '';

    // ساخت HTML بر اساس lstItemFBShomarehForGet

    // گروه‌بندی data.lst بر اساس itemFBShomareh
    let groupedData = {};
    lstAKhInfoRizMetre.forEach(function (row) {
        if (!groupedData[row.itemFBShomareh]) {
            groupedData[row.itemFBShomareh] = [];
        }
        groupedData[row.itemFBShomareh].push(row);
    });

    // ساخت HTML بر اساس lstItemFBShomarehForGet
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
                    str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span>" + row.shomareh + "</span></div>";

                    str += "<div class='col-md-2'><input  type='text'" +
                        " class='form-control spanStyleMitraSmall' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTedad" + id + "' value = '" + strTedad + "' /></div > ";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtTool" + id + "' value='" + strTool + "'/></div>";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtArz" + id + "' value='" + strArz + "'/></div>";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtErtefa" + id + "' value='" + strErtefa + "'/></div>";

                    str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue === true ? " HasEnteringValue " : "" : "") + "' id='txtVazn" + id + "' value='" + strVazn + "'/></div>";

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

        $targetDivRizMetreKH = $('#ViewRizMetreKH' + KMNum);

        $targetDivRizMetreKH.html(str);
        debugger;
        $targetDivRizMetreKH.slideDown();



        $targetDivRizMetreKH.find("input[type='text'].HasEnteringValue")
            .filter(function () {
                return $(this).val().trim() === "";
            })
            .addClass("blinking")
            .first()
            .focus();


        $targetDivRizMetreKH.on("change", "input[type='text'].HasEnteringValue", function () {
            if ($(this).val().trim() !== "") {
                $(this).removeClass("blinking");
            }
        });


        $targetDivRizMetreKH.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
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

}

function ReturnSumAllDetails() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtKhDetail' + i).val()) == '' ? '0' : $.trim($('#txtKhDetail' + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllDetailsForEdit(KMNum) {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtKhDetail' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtKhDetail' + KMNum + "_" + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllRBDetails() {
    sumAll = 0;
    for (var i = 1; i < 3; i++) {
        sumAll += parseFloat($.trim($('#txtRBDetail' + i).val()) == '' ? '0' : $.trim($('#txtRBDetail' + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllDardad() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtDarsad' + i).val()) == '' ? '0' : $.trim($('#txtDarsad' + i).val()));
    }
    return parseFloat(sumAll);
}
function ReturnSumAllDardadForEdit(KMNum) {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtDarsad' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtDarsad' + KMNum + "_" + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllRBDardad() {
    sumAll = 0;
    for (var i = 1; i < 3; i++) {
        sumAll += parseFloat($.trim($('#txtRBDarsad' + i).val()) == '' ? '0' : $.trim($('#txtRBDarsad' + i).val()));
    }
    return parseFloat(sumAll);
}

function SumAllDetails() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtKhDetail' + i).val()) == '' ? '0' : $.trim($('#txtKhDetail' + i).val()));
    }

    HajmKhakBardari = parseFloat($('#txtHajmKhakBardari').val());
    if (sumAll > HajmKhakBardari) {
        $('#txtHajmKhakBardari').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم خاکبرداری بیشتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmKhakBardari').removeClass('blinking');
        return false;
    }
}


function SumAllDetailsForEdit(KMNum) {

    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtKhDetail' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtKhDetail' + KMNum + "_" + i).val()));
    }

    HajmKhakBardari = parseFloat($('#txtHajmKhakBardari' + KMNum).val());
    if (sumAll > HajmKhakBardari) {
        $('#txtHajmKhakBardari' + KMNum).addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم خاکبرداری بیشتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmKhakBardari' + KMNum).removeClass('blinking');
        return false;
    }
}

function CheckValuesOfKhakBardariDetails() {
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        KhDetail = parseFloat($('#txtKhDetail' + i).val());

        ReUseHajm = parseFloat($('#txtReUseHajm' + i).val());
        if (ReUseHajm > KhDetail) {
            $('#txtReUseHajm' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseHajm' + i).removeClass('ErrorValueStyle');

        ReUseDarsad = parseFloat($('#txtReUseDarsad' + i).val());
        if (ReUseDarsad > 100) {
            $('#txtReUseDarsad' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseDarsad' + i).removeClass('ErrorValueStyle');
        /////////////
        Varizi = parseFloat($('#txtVarizi' + i).val());
        Haml = parseFloat($('#txtHaml' + i).val());
        if (Varizi > KhDetail) {
            $('#txtVarizi' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtVarizi' + i).removeClass('ErrorValueStyle');
        }

        if (Haml > KhDetail) {
            $('#txtHaml' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtHaml' + i).removeClass('ErrorValueStyle');
        }

        DarsadVarizi = parseFloat($('#txtDarsadVarizi' + i).val());
        DarsadHaml = parseFloat($('#txtDarsadHaml' + i).val());
        if (DarsadVarizi > 100) {
            $('#txtDarsadVarizi' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadVarizi' + i).removeClass('ErrorValueStyle');
        }

        if (DarsadHaml > 100) {
            $('#txtDarsadHaml' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadHaml' + i).removeClass('ErrorValueStyle');
        }
    }
    return checkValues;
}

function CheckValuesOfKhakBardariDetailsForEdit(KMNum) {
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        KhDetail = parseFloat($('#txtKhDetail' + KMNum + "_" + i).val());

        ReUseHajm = parseFloat($('#txtReUseHajm' + KMNum + "_" + i).val());
        if (ReUseHajm > KhDetail) {
            $('#txtReUseHajm' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseHajm' + KMNum + "_" + i).removeClass('ErrorValueStyle');

        ReUseDarsad = parseFloat($('#txtReUseDarsad' + KMNum + "_" + i).val());
        if (ReUseDarsad > 100) {
            $('#txtReUseDarsad' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseDarsad' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        /////////////
        Varizi = parseFloat($('#txtVarizi' + KMNum + "_" + i).val());
        Haml = parseFloat($('#txtHaml' + KMNum + "_" + i).val());
        if (Varizi > KhDetail) {
            $('#txtVarizi' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtVarizi' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }

        if (Haml > KhDetail) {
            $('#txtHaml' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtHaml' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }

        DarsadVarizi = parseFloat($('#txtDarsadVarizi' + KMNum + "_" + i).val());
        DarsadHaml = parseFloat($('#txtDarsadHaml' + KMNum + "_" + i).val());
        if (DarsadVarizi > 100) {
            $('#txtDarsadVarizi' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadVarizi' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }

        if (DarsadHaml > 100) {
            $('#txtDarsadHaml' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadHaml' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }
    }
    return checkValues;
}


function SaveKhakBardariInfo(BarAvordUserId) {
    debugger;
    check = false;
    //////////
    HKB = $('#txtHajmKhakBardari').val();
    if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
        $('#txtHajmKhakBardari').addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmKhakBardari').removeClass('blinking');
    }
    ////////////////
    var KM = $('#txtFromKMForKhakbardari').val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtFromKMForKhakbardari').addClass('blinking');
        check = true;
    }
    else {
        $('#txtFromKMForKhakbardari').removeClass('blinking');
    }
    ///////////////
    var KM = $('#txtToKMForKhakbardari').val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtToKMForKhakbardari').addClass('blinking');
        check = true;
    }
    else {
        $('#txtToKMForKhakbardari').removeClass('blinking');
    }
    /////////////
    var KME = parseFloat($('#txtToKMForKhakbardari').val().replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForKhakbardari').val().replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForKhakbardari').addClass('ErrorValueStyle');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForKhakbardari').removeClass('blinking');
    }

    ///////////////
    $('#divKhakBardariInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('ErrorValueStyle');
            check = true;
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });

    if (SumAllDetails()) check = true;

    if (CheckValuesOfKhakBardariDetails()) {
        check = true;
    }
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            debugger;

            let obj = {
                DetailValue: $('#txtKhDetail' + i).val(),
                DarsadValue: $('#txtDarsad' + i).val(),
                DetailValueOfReCycle: $('#txtReUseHajm' + i).val(),
                DarsadValueOfReCycle: $('#txtReUseDarsad' + i).val(),
                DetailValueOfVarize: $('#txtVarizi' + i).val(),
                DarsadValueOfVarize: $('#txtDarsadVarizi' + i).val(),
                DetailValueOfHaml: $('#txtHaml' + i).val(),
                DarsadValueOfHaml: $('#txtDarsadHaml' + i).val(),
                NoeKhakBardari: $('#txtKhakBardariItemId' + i).val(),
            };

            dataList.push(obj);
        }


        debugger;

        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Type = 1;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        $.ajax({
            type: "POST",
            url: "/AmalyateKhakiInfoForBarAvords/SaveKhakBardariInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
                    $('#HDFKMAmalyateKhakiIdForEdit').val(info[1]);
                    $('#HDFKMAmalyateKhakiNum').val(info[2]);

                    $('#txtHajmKhakBardari').val(0);
                    $('#txtFromKMForKhakbardari').val('000+000')
                    $('#txtToKMForKhakbardari').val('000+000')

                    for (let i = 1; i <= ActivityLength; i++) {
                        $('#txtKhDetail' + i).val(0);
                        $('#txtDarsad' + i).val(0);
                        $('#txtReUseHajm' + i).val(0);
                        $('#txtReUseDarsad' + i).val(0);
                        $('#txtVarizi' + i).val(0);
                        $('#txtDarsadVarizi' + i).val(0);
                        $('#txtHaml' + i).val(0);
                        $('#txtDarsadHaml' + i).val(0);
                        $('#txtKhakBardariItemId' + i).val(0);
                    }

                    $('#MainViewKhakBardariNew').slideUp(500);

                    ShowExistingKMKhakBardari(BarAvordUserId);

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
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}

function UpdateKhakBardariInfo(KMKhakBardariId, BarAvordUserId, KMNum) {

    debugger;
    check = false;
    //////////
    HKB = $('#txtHajmKhakBardari' + KMNum).val();
    if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
        $('#txtHajmKhakBardari' + KMNum).addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmKhakBardari' + KMNum).removeClass('blinking');
    }
    ////////////////
    var KM = $('#txtFromKMForKhakbardari' + KMNum).val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtFromKMForKhakbardari' + KMNum).addClass('blinking');
        check = true;
    }
    else {
        $('#txtFromKMForKhakbardari' + KMNum).removeClass('blinking');
    }
    ///////////////
    var KM = $('#txtToKMForKhakbardari' + KMNum).val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtToKMForKhakbardari' + KMNum).addClass('blinking');
        check = true;
    }
    else {
        $('#txtToKMForKhakbardari' + KMNum).removeClass('blinking');
    }
    /////////////
    var KME = parseFloat($('#txtToKMForKhakbardari' + KMNum).val().replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForKhakbardari' + KMNum).val().replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForKhakbardari' + KMNum).addClass('ErrorValueStyle');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForKhakbardari' + KMNum).removeClass('blinking');
    }

    ///////////////
    $('#divKhakBardariInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('ErrorValueStyle');
            check = true;
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });

    if (SumAllDetailsForEdit(KMNum)) check = true;

    if (CheckValuesOfKhakBardariDetailsForEdit(KMNum)) {
        check = true;
    }
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            var DarsadValue = $('#txtDarsad' + KMNum + "_" + i).val().trim();
            if (DarsadValue != "0") {
                let obj = {
                    DetailValue: $('#txtKhDetail' + KMNum + "_" + i).val(),
                    DarsadValue: DarsadValue,
                    DetailValueOfReCycle: $('#txtReUseHajm' + KMNum + "_" + i).val(),
                    DarsadValueOfReCycle: $('#txtReUseDarsad' + KMNum + "_" + i).val(),
                    DetailValueOfVarize: $('#txtVarizi' + KMNum + "_" + i).val(),
                    DarsadValueOfVarize: $('#txtDarsadVarizi' + KMNum + "_" + i).val(),
                    DetailValueOfHaml: $('#txtHaml' + KMNum + "_" + i).val(),
                    DarsadValueOfHaml: $('#txtDarsadHaml' + KMNum + "_" + i).val(),
                    NoeKhakBardari: $('#txtKhakBardariItemId' + KMNum + "_" + i).val(),
                };
                dataList.push(obj);
            }
        }

        //KMKhakBardariId = $('#HDFKMAmalyateKhakiIdForEdit').val();
        //KMKhakBardariNum = $('#HDFKMAmalyateKhakiNum').val();
        Year = $('#HDFYear').val();
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMKhakBardariId = KMKhakBardariId;
        vardata.KMNum = KMNum;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.Year = Year;
        debugger;
        $.ajax({
            type: "POST",
            url: "/AmalyateKhakiInfoForBarAvords/UpdateKhakBardariInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                debugger;
                info = response.split('_');
                if (info[0] == "OK") {
                    toastr.success('اطلاعات کیلومتراژ بدرستی ویرایش گردید', 'ثبت');
                    $('#divExistKMHeader' + KMNum).click();
                    //$('#ViewKhakBardariEzafeBaha' + KMNum).html('');

                }
                else
                    toastr.error('مشکل در ویرایش اطلاعات کیلومتراژ', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در ویرایش اطلاعات کیلومتراژ', 'خطا');
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');

}



//////////////////ریزش برداری
////////////////////
///////////////////
function RizeshBardariWithBarAvordClick(OpId, BarAvordUserId) {
    str = '';
    str += '<div class=\'row\' style=\'margin-top:3px;\'><div class=\'col-md-7 row\'><div class=\'col-md-4\'><a class=\'NewPolStyle\' onclick=\"ShowSelctionRizeshBardari(1,0,0,' + "'" + BarAvordUserId + "'" + ',0,0,0,0,0)\">کیلومتراژ جدید</a></div><div class=\'col-md-5\'><a class=\'NewPolStyle\' onclick=\"ShowExistingKMRizeshBardari(' + "'" + BarAvordUserId + "'" + ')\">لیست ریزش برداری ها</a></div></div>';
    str += '<div class=\'row\' style=\'margin-top: 30px;\' id=\'ViewRizeshBardari\'></div>';

    $('#RizeshBardariShow').find('#divShowRizeshBardari').html(str);
    $('#aRizeshBardariShow').click();

    //$('#ula' + OpId).html(str);
}

function ShowSelctionRizeshBardari(IsNew, KMExistingId, KMNum, BarAvordUserId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value) {
    str = '<div class=\'col-md-12 row\'>';
    str += '<div class=\'col-md-12 row\' style=\'border: 1px solid #c0c4e2;border-radius: 5px !important;padding: 5px 0px;\'>';
    str += '<div class=\'col-md-2\' style=\'text-align: left;\'><span>از کیلومتراژ: </span></div>';
    str += '<div class=\'col-md-1\'><input style=\'text-align:center;padding:0px;font-size: 16px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtFromKMForRizeshBardari\' value=\'000+000\'/></div>';
    str += '<div class=\'col-md-2\' style=\'text-align:left;\'><span>تا کیلومتراژ: </span></div><div class=\'col-md-1\'><input style=\'text-align: center;padding: 0px;font-size: 16px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtToKMForRizeshBardari\' value=\'000+000\'/></div>';
    str += '<div class=\'col-md-2\' style=\'text-align:left;\'><span>حجم خاکبرداری: </span></div><div class=\'col-md-1\'><input style=\'text-align: center;padding-left: 0px;padding-right: 0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtHajmRizeshBardari\' value=\'0\'/></div>';
    str += '<div class=\'col-md-1\'><span>مترمکعب</span></div>';
    str += '<div class=\'col-md-1\' style=\'text-align:left;\'><a class=\'NewPolStyle\' onclick=\"SaveRizeshBardariInfo(' + "'" + BarAvordUserId + "'" + ')\">ذخیره</a></div>';
    str += '</div></div>';
    //////////////////////////////
    str += '<div class=\'col-md-12\' id=\'divRizeshBardariInfoDetails\' style=\'margin-top:10px;padding:0px;display:none\'>';
    str += '<div class=\'col-md-12\'>';
    str += '<div class=\'col-md-12 row\' style=\'padding:5px 0px 0px;border:1px solid #b1d3ec;border-radius:10px;background-color: #d3e4fc;font-size: 10px;\'>';
    str += '<div class=\'col-md-2 row\'><div class=\'col-md-12\'></div></div>';
    str += '<div class=\'col-md-10 row\'>';
    str += '<div class=\'col-md-2 row\'>';
    str += '<div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;\'><span>حجم خاکبرداری</span></div>';
    str += '<div class=\'col-md-6\' style=\'text-align: center;\'><span>متر مکعب</span></div>';
    str += '<div class=\'col-md-6\' style=\'text-align: center;\'><span>درصد</span></div></div>';
    str += '<div class=\'col-md-2 row\'><div style=\'border-bottom: 1px solid #98b3c3;padding: 0px;\' class=\'col-md-12\'><span>قابل مصرف در خاکریزی</span></div><div class=\'col-md-6\' style=\'text-align: center;\'><span>متر مکعب</span></div>';
    str += '<div class=\'col-md-6\' style=\'text-align: center;\'><span>درصد</span></div></div>';
    str += '<div class=\'col-md-2 row\'><div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;\'><span>واریزه</span></div><div class=\'col-md-6\' style=\'text-align: center;\'><span>متر مکعب</span></div>';
    str += '<div class=\'col-md-6\' style=\'text-align: center;\'><span>درصد</span></div></div>';
    str += '<div class=\'col-md-2 row\'><div class=\'col-md-12\' style=\'text-align: center;border-bottom: 1px solid #98b3c3;\'><span>حمل به دپو/مسیر</span></div><div class=\'col-md-6\' style=\'text-align: center;\'><span>متر مکعب</span></div>';
    str += '<div class=\'col-md-6\' style=\'text-align: center;\'><span>درصد</span></div></div>';
    str += '<div class=\'col-md-1\'><div style=\'padding: 0px;\' class=\'col-md-12\'><span>اضافه بها حمل</span></div><div class=\'col-md-12\' style=\'text-align: center;padding: 0px;\'><span>بین 20 تا 50 متر</span></div></div>';
    str += '<div class=\'col-md-1\'><div class=\'col-md-12\' style=\'text-align: center;\'><span>پخش مصالح خاکریزی</span></div><div class=\'col-md-12\' style=\'text-align: center;\'><span>شده در دپو</span></div></div>';
    //str += '<div class=\'col-md-2\' style=\'padding: 0px;\'><div class=\'col-md-12\' style=\'text-align: left;padding-left: 0px;padding-right: 0px;\'><span>استفاده از سیستم ناتل</span></div><div class=\'col-md-12\' style=\'text-align: left;padding-left: 0px;padding-right: 0px;\'><span>بجای چاشنی الکتریکی</span></div></div>';
    str += '</div></div></div>';

    ActivityTitle = ["ریزش برداری", "ریزش برداری سنگی"];

    ////////////////
    for (var i = 1; i < 3; i++) {
        str += '<div class=\'col-md-12\'>';
        str += '<div class=\'col-md-12 row\' style=\'padding:2px 0px;margin:2px 0px;border:1px solid #ccc;border-radius:10px\'>';
        str += '<div class=\'col-md-2\' style=\'padding: 0px;text-align: left;\'><span style=\'font-size: 10px;\'>' + ActivityTitle[i - 1] + '</span></div>';
        str += '<div class=\'col-md-10 row\'>';
        str += '<div class=\'col-md-2 row\' style=\'padding: 0px;\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBDetail' + i + '\' value=\'0\'/></div>';
        str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBDarsad' + i + '\' value=\'0\'/></div></div>';
        str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBReUseHajm' + i + '\' value=\'0\'/></div>';
        str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBReUseDarsad' + i + '\' value=\'0\'/></div></div>';
        str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBVarizi' + i + '\' value=\'0\'/></div>';
        str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBDarsadVarizi' + i + '\' value=\'0\'/></div></div>';
        str += '<div class=\'col-md-2 row\'><div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBHaml' + i + '\' value=\'0\'/></div>';
        str += '<div class=\'col-md-6\' style=\'padding: 0px 2px;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'text\' class=\'form-control input-sm\' id=\'txtRBDarsadHaml' + i + '\' value=\'0\'/></div></div>';
        str += '<div class=\'col-md-1\'><div class=\'col-md-12\' style=\'padding: 0px 2px;text-align: center;\'><input style=\'text-align:center;padding-left:0px;padding-right:0px;\' type=\'checkbox\' id=\'ckRBFaseleHaml' + i + '\' /></div></div>';
        str += '<div class=\'col-md-1\'><div class=\'col-md-12\' style=\'padding: 0px 2px;text-align: center;\'><input type=\'checkbox\' id=\'ckRBPakhsh' + i + '\'/></div></div>';
        str += '</div></div></div>';
    }
    //////////////
    $('#ViewRizeshBardari').html(str);

    $('#txtHajmRizeshBardari').change(function () {
        HRB = parseFloat($(this).val());
        if (!$.isNumeric(HRB)) {
            toastr.info('حجم ریزش وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('ErrorValueStyle');
        }
        else {
            $('#divRizeshBardariInfoDetails').show();
            $(this).removeClass('ErrorValueStyle');

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
            $('#txtRBDetail2').val((Darsad2 / 100 * HRB).toFixed(2));
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
            $(this).addClass('ErrorValueStyle');
            toastr.info('کیلومتراژ شروع وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });

    $('#txtToKMForRizeshbardari').change(function () {
        var KM = $(this).val();
        var KMSplit = KM.split('+');
        if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
            $(this).addClass('ErrorValueStyle');
            toastr.info('کیلومتراژ خاتمه وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
        }
        else
            $(this).removeClass('ErrorValueStyle');

        var KME = parseFloat(KM.replace('+', ''));
        var KMS = parseFloat($('#txtFromKMForKhakbardari').val().replace('+', ''));
        if (KMS > KME) {
            toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
            $('#txtToKMForKhakbardari').addClass('ErrorValueStyle');
        }
        else
            $('#txtToKMForKhakbardari').removeClass('ErrorValueStyle');
    });

    $('#divRizeshBardariInfoDetails input[type="checkbox"]').change(function () {
        id = $(this).attr('id');
        idShomareh = 0;
        idFix = id.substring(0, 14);
        if (idFix == 'ckRBFaseleHaml')
            idShomareh = id.substring(14, id.length);

        idFix = id.substring(0, 10);
        if (idFix == 'ckRBPakhsh')
            idShomareh = id.substring(10, id.length);

        if (parseFloat($.trim($('#txtRBDetail' + idShomareh).val()) == '' ? '0' : $.trim($('#txtRBDetail' + idShomareh).val())) == 0) {
            $(this).attr('checked', false);
        }
    });

    $('#divRizeshBardariInfoDetails input[type="text"]').change(function () {
        HajmRizeshBardari = parseFloat($.trim($('#txtHajmRizeshBardari').val()));
        ////////////
        if (!$.isNumeric($(this).val())) {
            toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
            $(this).addClass('ErrorValueStyle');
        }
        else {
            $(this).removeClass('ErrorValueStyle');
        }
        ///////////////
        if (HajmRizeshBardari == 0 || HajmRizeshBardari == '' || !$.isNumeric(HajmRizeshBardari)) {
            toastr.info('حجم ریزش برداری وارد شده نامعتبر میباشد', 'اطلاع');
            $('#txtHajmRizeshBardari').addClass('ErrorValueStyle');
        }
        else {
            $('#txtHajmRizeshBardari').removeClass('ErrorValueStyle');
            id = $(this).attr('id');
            idFix = id.substring(0, 11);
            idShomareh = id.substring(11, id.length);
            if (idFix == 'txtRBDetail') {
                Zarb = parseFloat($(this).val()) / parseFloat(HajmRizeshBardari) * 100;
                $('#txtRBDarsad' + idShomareh).val(Zarb.toFixed(2));

                SumAll = ReturnSumAllRBDetails();
                if (SumAll > HajmRizeshBardari) {
                    NewVal = HajmRizeshBardari - (SumAll - parseFloat($(this).val()));
                    $(this).val(NewVal.toFixed(2));
                    if (NewVal != 0) {
                        $('#txtRBReUseDarsad' + idShomareh).val(100);
                        $('#txtRBDarsadVarizi' + idShomareh).val(30);
                        $('#txtRBDarsadHaml' + idShomareh).val(70);
                    }
                }
                else {
                    if (idShomareh == "1") {
                        $('#txtRBDetail2').val(HajmRizeshBardari - parseFloat($(this).val()));
                        $('#txtRBDarsad2').val((parseFloat($('#txtRBDetail2').val()) / HajmRizeshBardari * 100).toFixed(2));
                        $('#txtRBReUseHajm2').val((parseFloat($('#txtRBReUseDarsad2').val()) * parseFloat($('#txtRBDetail2').val()) / 100).toFixed(2));
                        $('#txtRBVarizi2').val((parseFloat($('#txtRBDarsadVarizi2').val()) * parseFloat($('#txtRBDetail2').val()) / 100).toFixed(2));
                        $('#txtRBHaml2').val((parseFloat($('#txtRBDarsadHaml2').val()) * parseFloat($('#txtRBDetail2').val()) / 100).toFixed(2));
                    }

                    else if (idShomareh == "2") {
                        $('#txtRBDetail1').val(HajmRizeshBardari - parseFloat($(this).val()));
                        $('#txtRBDarsad1').val((parseFloat($('#txtRBDetail1').val()) / HajmRizeshBardari * 100).toFixed(2));
                        $('#txtRBReUseHajm1').val((parseFloat($('#txtRBReUseDarsad1').val()) * parseFloat($('#txtRBDetail1').val()) / 100).toFixed(2));
                        $('#txtRBVarizi1').val((parseFloat($('#txtRBDarsadVarizi1').val()) * parseFloat($('#txtRBDetail1').val()) / 100).toFixed(2));
                        $('#txtRBHaml1').val((parseFloat($('#txtRBDarsadHaml1').val()) * parseFloat($('#txtRBDetail1').val()) / 100).toFixed(2));
                    }

                    $('#txtRBReUseDarsad' + idShomareh).val(100);
                    $('#txtRBDarsadVarizi' + idShomareh).val(30);
                    $('#txtRBDarsadHaml' + idShomareh).val(70);
                }

                $('#txtRBDarsad' + idShomareh).val((parseFloat($(this).val()) / HajmRizeshBardari * 100).toFixed(2));

                $(this).removeClass('ErrorValueStyle');
                ReUseDarsad = parseFloat($('#txtRBReUseDarsad' + idShomareh).val());
                $('#txtRBReUseHajm' + idShomareh).val((ReUseDarsad / 100 * parseFloat($(this).val())).toFixed(2));

                DarsadVarizi = parseFloat($('#txtRBDarsadVarizi' + idShomareh).val());
                $('#txtRBVarizi' + idShomareh).val((DarsadVarizi / 100 * parseFloat($(this).val())).toFixed(2));

                DarsadHaml = parseFloat($('#txtRBDarsadHaml' + idShomareh).val());
                $('#txtRBHaml' + idShomareh).val((DarsadHaml / 100 * parseFloat($(this).val())).toFixed(2));
            }
            /////////
            idFix = id.substring(0, 11);
            idShomareh = id.substring(11, id.length);
            if (idFix == 'txtRBDarsad') {
                SumAllDardad = ReturnSumAllRBDardad();
                if (SumAllDardad > 100) {
                    NewVal = 100 - (SumAllDardad - parseFloat($(this).val()));
                    $(this).val(NewVal.toFixed(2));
                    if (NewVal != 0) {
                        //$('#txtRBReUseDarsad' + idShomareh).val(100);
                        //$('#txtRBDarsadVarizi' + idShomareh).val(30);
                        //$('#txtRBDarsadHaml' + idShomareh).val(70);
                    }
                }
                else {
                    if (idShomareh == "1") {
                        $('#txtRBDetail1').val(parseFloat($(this).val()) / 100 * HajmRizeshBardari)
                        $('#txtRBDetail2').val(HajmRizeshBardari - parseFloat($('#txtRBDetail1').val()));
                        $('#txtRBDarsad2').val((parseFloat($('#txtRBDetail2').val()) / HajmRizeshBardari * 100).toFixed(2));
                        $('#txtRBReUseHajm2').val((parseFloat($('#txtRBReUseDarsad2').val()) * parseFloat($('#txtRBDetail2').val()) / 100).toFixed(2));
                        $('#txtRBVarizi2').val((parseFloat($('#txtRBDarsadVarizi2').val()) * parseFloat($('#txtRBDetail2').val()) / 100).toFixed(2));
                        $('#txtRBHaml2').val((parseFloat($('#txtRBDarsadHaml2').val()) * parseFloat($('#txtRBDetail2').val()) / 100).toFixed(2));
                    }

                    else if (idShomareh == "2") {
                        $('#txtRBDetail2').val(parseFloat($(this).val()) / 100 * HajmRizeshBardari)
                        $('#txtRBDetail1').val(HajmRizeshBardari - parseFloat($('#txtRBDetail2').val()));
                        $('#txtRBDarsad1').val((parseFloat($('#txtRBDetail1').val()) / HajmRizeshBardari * 100).toFixed(2));
                        $('#txtRBReUseHajm1').val((parseFloat($('#txtRBReUseDarsad1').val()) * parseFloat($('#txtRBDetail1').val()) / 100).toFixed(2));
                        $('#txtRBVarizi1').val((parseFloat($('#txtRBDarsadVarizi1').val()) * parseFloat($('#txtRBDetail1').val()) / 100).toFixed(2));
                        $('#txtRBHaml1').val((parseFloat($('#txtRBDarsadHaml1').val()) * parseFloat($('#txtRBDetail1').val()) / 100).toFixed(2));
                    }

                    $('#txtRBReUseDarsad' + idShomareh).val(100);
                    $('#txtRBDarsadVarizi' + idShomareh).val(30);
                    $('#txtRBDarsadHaml' + idShomareh).val(70);
                }

                Zarb = parseFloat($(this).val()) / 100 * parseFloat(HajmRizeshBardari);
                $('#txtRBDetail' + idShomareh).val(Zarb.toFixed(2));

                ReUseDarsad = parseFloat($('#txtRBReUseDarsad' + idShomareh).val());
                $('#txtRBReUseHajm' + idShomareh).val((ReUseDarsad / 100 * parseFloat($('#txtRBDetail' + idShomareh).val())).toFixed(2));

                DarsadVarizi = parseFloat($('#txtRBDarsadVarizi' + idShomareh).val());
                $('#txtRBVarizi' + idShomareh).val((DarsadVarizi / 100 * parseFloat($('#txtRBDetail' + idShomareh).val())).toFixed(2));

                DarsadHaml = parseFloat($('#txtRBDarsadHaml' + idShomareh).val());
                $('#txtRBHaml' + idShomareh).val((DarsadHaml / 100 * parseFloat($('#txtRBDetail' + idShomareh).val())).toFixed(2));
            }
            ///////
            idFix = id.substring(0, 14);
            idShomareh = id.substring(14, id.length);
            if (idFix == 'txtRBReUseHajm') {
                RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                if (parseFloat($(this).val()) > RBDetail) {
                    $(this).val(RBDetail);
                    $(this).addClass('ErrorValueStyle');
                    toastr.info('مقدار وارد شده نبایستی از حجم آیتم بیشتر باشد', 'اطلاع');
                }
                else {
                    $(this).removeClass('ErrorValueStyle');
                    RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                    $('#txtRBReUseDarsad' + idShomareh).val((parseFloat($('#txtRBReUseHajm' + idShomareh).val()) / RBDetail * 100).toFixed(2));
                }
            }

            idFix = id.substring(0, 16);
            idShomareh = id.substring(16, id.length);
            if (idFix == 'txtRBReUseDarsad') {
                if (parseFloat($(this).val()) > 100) {
                    $(this).val(100);
                    $(this).addClass('ErrorValueStyle');
                    toastr.info('درصد وارد شده نبایستی از 100 بیشتر باشد', 'اطلاع');
                }
                else {
                    $(this).removeClass('ErrorValueStyle');
                    RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                    $('#txtRBReUseHajm' + idShomareh).val((parseFloat($('#txtRBReUseDarsad' + idShomareh).val()) / 100 * RBDetail).toFixed(2));
                }
            }
            ///////
            idFix = id.substring(0, 11);
            idShomareh = id.substring(11, id.length);
            if (idFix == 'txtRBVarizi') {
                RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                Haml = parseFloat($('#txtRBHaml' + idShomareh).val());
                if (parseFloat($(this).val()) > RBDetail) {
                    $(this).addClass('ErrorValueStyle');
                    $(this).val(RBDetail);
                    $('#txtRBHaml' + idShomareh).val(0);
                    $('#txtRBDarsadVarizi' + idShomareh).val(100);
                    $('#txtRBDarsadHaml' + idShomareh).val(0);
                    toastr.info('مقدار وارد شده نبایستی از حجم آیتم بیشتر باشد', 'اطلاع');
                }
                else {
                    $(this).removeClass('ErrorValueStyle');
                    RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                    $('#txtRBHaml' + idShomareh).val((RBDetail - parseFloat($(this).val())).toFixed(2));
                    $('#txtRBDarsadHaml' + idShomareh).val((parseFloat($('#txtRBHaml' + idShomareh).val()) / RBDetail * 100).toFixed(2));
                    $('#txtRBDarsadVarizi' + idShomareh).val((parseFloat($('#txtRBVarizi' + idShomareh).val()) / RBDetail * 100).toFixed(2));
                }
            }

            idFix = id.substring(0, 17);
            idShomareh = id.substring(17, id.length);
            DarsadHaml = parseFloat($('#txtRBDarsadHaml' + idShomareh).val());
            if (idFix == 'txtRBDarsadVarizi') {
                if (parseFloat($(this).val()) > 100) {
                    $(this).val(100);
                    $('#txtRBDarsadHaml' + idShomareh).val(0);
                    $('#txtRBVarizi' + idShomareh).val(HajmKhakBardari);
                    $('#txtRBHaml' + idShomareh).val(0);
                    $(this).addClass('ErrorValueStyle');
                    toastr.info('درصد وارد شده نبایستی از 100 بیشتر باشد', 'اطلاع');
                }
                else {
                    RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                    $('#txtRBDarsadHaml' + idShomareh).val((100 - parseFloat($(this).val())).toFixed(2));
                    $('#txtRBVarizi' + idShomareh).val((parseFloat($('#txtRBDarsadVarizi' + idShomareh).val()) / 100 * RBDetail).toFixed(2));
                    $('#txtRBHaml' + idShomareh).val((parseFloat($('#txtRBDarsadHaml' + idShomareh).val()) / 100 * RBDetail).toFixed(2));
                }
            }

            ///////
            idFix = id.substring(0, 9);
            idShomareh = id.substring(9, id.length);
            if (idFix == 'txtRBHaml') {
                RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                Varizi = parseFloat($('#txtRBVarizi' + idShomareh).val());
                if (parseFloat($(this).val()) > RBDetail) {
                    $(this).val(RBDetail);
                    $('#txtVarizi' + idShomareh).val(0);
                    $('#txtDarsadHaml' + idShomareh).val(100);
                    $('#txtDarsadVarizi' + idShomareh).val(0);
                    $(this).addClass('ErrorValueStyle');
                    toastr.info('مقدار وارد شده نبایستی از حجم آیتم بیشتر باشد', 'اطلاع');
                }
                else {
                    $(this).removeClass('ErrorValueStyle');
                    RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                    $('#txtRBVarizi' + idShomareh).val((RBDetail - $(this).val()).toFixed(2));
                    $('#txtRBDarsadVarizi' + idShomareh).val((parseFloat($('#txtRBVarizi' + idShomareh).val()) / RBDetail * 100).toFixed(2));
                    $('#txtRBDarsadHaml' + idShomareh).val((parseFloat($('#txtRBHaml' + idShomareh).val()) / RBDetail * 100).toFixed(2));
                }
            }

            idFix = id.substring(0, 15);
            idShomareh = id.substring(15, id.length);
            DarsadVarizi = parseFloat($('#txtRBDarsadVarizi' + idShomareh).val());
            if (idFix == 'txtRBDarsadHaml') {
                if (parseFloat($(this).val()) > 100) {
                    $(this).val(100);
                    $('#txtRBDarsadVarizi' + idShomareh).val(0);
                    $(this).addClass('ErrorValueStyle');
                    toastr.info('درصد وارد شده نبایستی از 100 بیشتر باشد', 'اطلاع');
                }
                else {
                    $(this).removeClass('ErrorValueStyle');
                    RBDetail = parseFloat($('#txtRBDetail' + idShomareh).val());
                    $('#txtRBDarsadVarizi' + idShomareh).val((100 - $(this).val()).toFixed(2));
                    $('#txtRBHaml' + idShomareh).val((parseFloat($('#txtRBDarsadHaml' + idShomareh).val()) / 100 * RBDetail).toFixed(2));
                    $('#txtRBVarizi' + idShomareh).val((parseFloat($('#txtRBDarsadVarizi' + idShomareh).val()) / 100 * RBDetail).toFixed(2));
                }
            }
            /////////
        }
    });



    if (IsNew == 0) {
        $('#txtFromKMForRizeshBardari').val(FromKMSplit);
        $('#txtToKMForRizeshBardari').val(ToKMSplit);
        $('#txtHajmRizeshBardari').val(Value);
        $('#divRizeshBardariInfoDetails').show();
        $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
        $('#btnCloseExistingKMAmalyateKhaki').click();

        NoeFB = parseInt($('#HDFNoeFB').val());
        Year = $('#HDFYear').val();

        var vardata = new Object();
        vardata.AmalyateKhakiInfoForBarAvordId = KMExistingId;
        vardata.Year = Year;
        vardata.NoeFB = NoeFB;

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
                var KMAmalyateRizeshBarAvordMore = xml.find("tblKMAmalyateKhakiBarAvordMore");
                var KMAmalyateRizeshBarAvordDetailsMore = xml.find("tblKMAmalyateKhakiBarAvordDetailsMore");
                var KMAmalyateRizeshBarAvordDetailsEzafeBaha = xml.find("tblKMAmalyateKhakiBarAvordDetailsEzafeBaha");

                Value = 0;
                $.each(KMAmalyateRizeshBarAvordMore, function () {
                    Name = $.trim($(this).find("_Name").text());
                    if (Name == 'HRB') {
                        Value = $.trim($(this).find("_Value").text());
                        $('#txtHajmRizeshBardari').val(Value);
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
                        AmalyateKhakiInfoForBarAvordDetailsId = $(this).find("_AmalyateKhakiInfoForBarAvordDetailsId").text();
                        if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                            $('#txtRB' + Name + Type).val(ValueMore);
                        }
                    });

                    CurrentValue = $('#txtRBDetail' + Type).val();
                    ValueOfReCycle = $('#txtRBReUseHajm' + Type).val();
                    ValueOfVarize = $('#txtRBVarizi' + Type).val();
                    ValueOfHaml = $('#txtRBHaml' + Type).val();

                    $('#txtRBDarsad' + Type).val(parseFloat(Value) == 0 ? 0 : (parseFloat(CurrentValue) / parseFloat(Value) * 100).toFixed(2));
                    $('#txtRBReUseDarsad' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(2));
                    $('#txtRBDarsadVarizi' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(2));
                    $('#txtRBDarsadHaml' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(2));

                    $.each(KMAmalyateRizeshBarAvordDetailsEzafeBaha, function () {
                        CurrentId = $(this).find("_ID").text();
                        Name = $.trim($(this).find("_Name").text());
                        boolValue = $(this).find("_Value").text() == 'true' ? true : false;
                        AmalyateKhakiInfoForBarAvordDetailsId = $(this).find("_AmalyateKhakiInfoForBarAvordDetailsId").text();

                        if (Id == AmalyateKhakiInfoForBarAvordDetailsId) {
                            $('#ckRB' + Name + Type).attr('checked', boolValue);
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
}

function ShowExistingKMRizeshBardari(BarAvordUserId) {
    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Type = 2;
    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/GetExistingKMAmalyateKhakiInfoWithBarAvordId",
        //data: '{BarAvordId:' + BarAvordId + ',Type:2}',
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var xmlDoc = $.parseXML(response);
            var xml = $(xmlDoc);
            var KMAmalyateKhakiBarAvord = xml.find("tblKMAmalyateKhakiBarAvord");
            count = 1;
            str = "";
            $.each(KMAmalyateKhakiBarAvord, function () {
                KMExistingId = $(this).find("_ID").text();
                FromKM = $(this).find("_FromKM").text();
                ToKM = $(this).find("_ToKM").text();
                FromKMSplit = $(this).find("FromKMSplit").text();
                ToKMSplit = $(this).find("ToKMSplit").text();
                Value = $(this).find("_Value").text();
                KMNum = $(this).find("_KMNum").text();
                Type = $(this).find("_Type").text();

                str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionKMAmalyateKhaki($(this),\'' + KMExistingId + '\',\'' + KMNum
                    + '\')\" ondblclick=\"ShowSelctionRizeshBardari(0,' + "'" + KMExistingId + "'" + ',' + KMNum + ',' + "'" + BarAvordUserId + "'" + ',' + "'" + FromKM + "'" + ',' + "'" + ToKM + "'" + ',' + "'" + FromKMSplit + "'" + ',' + "'" + ToKMSplit + "'" + ',' + Value + ')\">' + count++
                    + ' - کیلومتراژ' + '<label>' + FromKMSplit + ' - ' + ToKMSplit + '</label>' + '</a></div>';
            });
            $('#divViewExistingKMAmalyateKhaki').html(str);
            $('#aViewExistingKMAmalyateKhaki').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
        }
    });
}

function SaveRizeshBardariInfo(BarAvordUserId) {
    check = false;
    //////////
    HRB = $('#txtHajmRizeshBardari').val();
    if (!$.isNumeric(HRB) || HRB == 0 || HRB == '') {
        $('#txtHajmRizeshBardari').addClass('ErrorValueStyle');
        check = true;
    }
    else {
        $('#txtHajmRizeshBardari').removeClass('ErrorValueStyle');
    }
    ////////////////
    var KM = $('#txtFromKMForRizeshBardari').val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtFromKMForKhakbardari').addClass('ErrorValueStyle');
        check = true;
    }
    else {
        $('#txtFromKMForRizeshBardari').removeClass('ErrorValueStyle');
    }
    ///////////////
    var KM = $('#txtToKMForRizeshBardari').val();
    var KMSplit = KM.split('+');
    if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
        $('#txtToKMForRizeshBardari').addClass('ErrorValueStyle');
        check = true;
    }
    else {
        $('#txtToKMForRizeshBardari').removeClass('ErrorValueStyle');
    }
    /////////////
    var KME = parseFloat($('#txtToKMForRizeshBardari').val().replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForRizeshBardari').val().replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForRizeshBardari').addClass('ErrorValueStyle');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForRizeshBardari').removeClass('ErrorValueStyle');
    }

    ///////////////
    $('#divRizeshBardariInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('ErrorValueStyle');
            check = true;
        }
        else
            $(this).removeClass('ErrorValueStyle');

    });

    if (SumAllDetails()) check = true;

    if (CheckValuesOfKhakBardariDetails()) {
        check = true;
    }

    if (!check) {
        DetailValue1 = $('#txtRBDetail1').val();
        DetailValueOfReCycle1 = $('#txtRBReUseHajm1').val();
        DetailValueOfVarize1 = $('#txtRBVarizi1').val();
        DetailValueOfHaml1 = $('#txtRBHaml1').val();
        ckFaseleHaml1 = $('#ckRBFaseleHaml1').is(":checked");
        ckPakhsh1 = $('#ckRBPakhsh1').is(":checked");
        /////////
        DetailValue2 = $('#txtRBDetail2').val();
        DetailValueOfReCycle2 = $('#txtRBReUseHajm2').val();
        DetailValueOfVarize2 = $('#txtRBVarizi2').val();
        DetailValueOfHaml2 = $('#txtRBHaml2').val();
        ckFaseleHaml2 = $('#ckRBFaseleHaml2').is(":checked");
        ckPakhsh2 = $('#ckRBPakhsh2').is(":checked");
        /////////

        StateRizeshBardariSaveOrEdit = $('#HDFStateAmalyateKhakiSaveOrEdit').val();
        if (StateRizeshBardariSaveOrEdit == 'Add') {
            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId; vardata.Type = 2; vardata.FromKM = KMS; vardata.ToKM = KME; vardata.HRB = HRB;
            vardata.DetailValue1 = DetailValue1; vardata.DetailValueOfReCycle1 = DetailValueOfReCycle1; vardata.DetailValueOfVarize1 = DetailValueOfVarize1;
            vardata.DetailValueOfHaml1 = DetailValueOfHaml1; vardata.ckFaseleHaml1 = ckFaseleHaml1; vardata.ckPakhsh1 = ckPakhsh1;
            vardata.DetailValue2 = DetailValue2; vardata.DetailValueOfReCycle2 = DetailValueOfReCycle2; vardata.DetailValueOfVarize2 = DetailValueOfVarize2;
            vardata.DetailValueOfHaml2 = DetailValueOfHaml2; vardata.ckFaseleHaml2 = ckFaseleHaml2; vardata.ckPakhsh2 = ckPakhsh2;
            $.ajax({
                type: "POST",
                url: "/AmalyateKhakiInfoForBarAvords/SaveRizeshBardariInfoForBarAvord",
                //data: '{BarAvordId:' + BarAvordId + ',Type:2,FromKM:' + KMS + ',ToKM:' + KME + ',HRB:' + "'" + HRB + "'"
                //        + ',DetailValue1:' + "'" + DetailValue1 + "'" + ',DetailValueOfReCycle1:' + "'" + DetailValueOfReCycle1 + "'" + ',DetailValueOfVarize1:' + "'" + DetailValueOfVarize1 + "'" + ',DetailValueOfHaml1:' + "'" + DetailValueOfHaml1 + "'"
                //        + ',ckFaseleHaml1:' + ckFaseleHaml1 + ',ckPakhsh1:' + ckPakhsh1
                //        + ',DetailValue2:' + "'" + DetailValue2 + "'" + ',DetailValueOfReCycle2:' + "'" + DetailValueOfReCycle2 + "'" + ',DetailValueOfVarize2:' + "'" + DetailValueOfVarize2 + "'" + ',DetailValueOfHaml2:' + "'" + DetailValueOfHaml2 + "'"
                //        + ',ckFaseleHaml2:' + ckFaseleHaml2 + ',ckPakhsh2:' + ckPakhsh2
                //        + '}',
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
            KMRizeshBardariId = $('#HDFKMAmalyateKhakiIdForEdit').val();
            KMRizeshBardariNum = $('#HDFKMAmalyateKhakiNum').val();
            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId; vardata.KMRizeshBardariId = KMRizeshBardariId; vardata.KMNum = KMRizeshBardariNum;
            vardata.FromKM = KMS; vardata.ToKM = KME; vardata.HRB = HRB;
            vardata.DetailValue1 = DetailValue1; vardata.DetailValueOfReCycle1 = DetailValueOfReCycle1; vardata.DetailValueOfVarize1 = DetailValueOfVarize1;
            vardata.DetailValueOfHaml1 = DetailValueOfHaml1; vardata.ckFaseleHaml1 = ckFaseleHaml1; vardata.ckPakhsh1 = ckPakhsh1;
            vardata.DetailValue2 = DetailValue2; vardata.DetailValueOfReCycle2 = DetailValueOfReCycle2; vardata.DetailValueOfVarize2 = DetailValueOfVarize2;
            vardata.DetailValueOfHaml2 = DetailValueOfHaml2; vardata.ckFaseleHaml2 = ckFaseleHaml2; vardata.ckPakhsh2 = ckPakhsh2;
            $.ajax({
                type: "POST",
                url: "/AmalyateKhakiInfoForBarAvords/UpdateRizeshBardariInfoForBarAvord",
                //data: '{BarAvordId:' + BarAvordId + ',KMRizeshBardariId:' + KMRizeshBardariId + ',KMNum:' + KMRizeshBardariNum + ',FromKM:' + KMS + ',ToKM:' + KME + ',HRB:' + "'" + HRB + "'"
                //        + ',DetailValue1:' + "'" + DetailValue1 + "'" + ',DetailValueOfReCycle1:' + "'" + DetailValueOfReCycle1 + "'" + ',DetailValueOfVarize1:' + "'" + DetailValueOfVarize1 + "'" + ',DetailValueOfHaml1:' + "'" + DetailValueOfHaml1 + "'"
                //        + ',ckFaseleHaml1:' + ckFaseleHaml1 + ',ckPakhsh1:' + ckPakhsh1
                //        + ',DetailValue2:' + "'" + DetailValue2 + "'" + ',DetailValueOfReCycle2:' + "'" + DetailValueOfReCycle2 + "'" + ',DetailValueOfVarize2:' + "'" + DetailValueOfVarize2 + "'" + ',DetailValueOfHaml2:' + "'" + DetailValueOfHaml2 + "'"
                //        + ',ckFaseleHaml2:' + ckFaseleHaml2 + ',ckPakhsh2:' + ckPakhsh2
                //        + '}',
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

