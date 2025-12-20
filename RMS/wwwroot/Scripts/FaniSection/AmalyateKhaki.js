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
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForKhakbardari" value="0"/>
    </div>

    <!-- تا کیلومتراژ -->
    <div class="col-md-1 label-col">
      <span>تا کیلومتراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForKhakbardari" value="0"/>
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
      <input type="button" class="btn buttonStyleBoard" value="ذخیره" style="color:#fff" onclick="SaveKhakBardariInfo('${BarAvordUserId}'` + ',' + `'Add')"/>
        

    </div><!--col-md-2 action-col -->
    </div><!--col-12 -->
    </div>

    </div><!-- بخش نمایش -->


</div>
    `;


    $('#ula' + OpId).html(str);

    setTimeout(() => { $('#txtFromKMForKhakbardari').focus().select(); }, 200);

    $('#ula' + OpId)
        .off('keydown.KhakBardari')
        .on('keydown.KhakBardari', '.khakbardariTextStyle', function (e) {
            if (e.key === 'Enter') {
                e.preventDefault(); // جلوگیری از submit فرم

                const inputs = $('.khakbardariTextStyle');
                let idx = inputs.index(this);

                // تا وقتی input بعدی وجود دارد، بررسی کن disabled نباشد
                while (idx < inputs.length - 1) {
                    idx++;
                    if (!inputs.eq(idx).prop('disabled')) {
                        inputs.eq(idx).focus().select();
                        return; // خروج از تابع بعد از فوکوس موفق
                    }
                }

                // اگر هیچ input فعالی نبود → برو روی دکمه
                $('.buttonStyleBoard').focus();
            }
        });


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
    //خاکریزی
    vardata.Type = 1;

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
        <div class="row AmalyatKhakiRowStyle">

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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetail${i + 1}" value="" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsad${i + 1}" value="" />
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVarizi${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVarizi${i + 1}" value=""  />
            </div>
          </div>
        </div>

        <!-- قابل مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" ${((i + 1) === 1 || (i + 1) === 2) ? `disabled` : ``} id="txtReUseHajm${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" ${((i + 1) === 1 || (i + 1) === 2) ? `disabled` : ``} id="txtReUseDarsad${i + 1}" value=""  />
            </div>
          </div>
        </div>

        <!-- حمل به دپو/مسیر -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHaml${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHaml${i + 1}" value=""  />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
            }

            strSSKB += `
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
              <span id="spantotalKhDetailNew"></span>
            </div>
            <div class="col-4" style="text-align:center;padding:0 2px;">
              <span id="spantotalDarsadKhDetailNew"></span>
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalVariziNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalDarsadVariziNew"></span>
            </div>
          </div>
        </div>

        <!-- مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalReUseHajmNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalDarsadReUseHajmNew"></span>
            </div>
          </div>
        </div>
        <!-- حمل به دپو -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalHamlNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalDarsadHamlNew"></span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
            //////////////
            $('#ViewKhakBardariNew').html(strSSKB);

            $('#ViewKhakBardariNew input[type="text"]').change(function () {

                let changedId = $(this).attr("id");
                let i = changedId.match(/\d+/) ? changedId.match(/\d+/)[0] : ""; // شماره ردیف
                let HajmKhakBardari = parseFloat($.trim($('#txtHajmKhakBardari').val()));

                // 🚨 اعتبارسنجی
                if (!$.isNumeric($(this).val())) {
                    toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }
                else {
                    $(this).removeClass('blinking');
                }

                if (HajmKhakBardari == 0 || HajmKhakBardari == '' || !$.isNumeric(HajmKhakBardari)) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmKhakBardari').addClass('blinking');
                    return;
                } else {
                    $('#txtHajmKhakBardari').removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetail" + i).val()) || 0;

                if (khDetail != null || khDetail != 0) {

                    $("#txtVarizi" + i).prop('disabled') ? '' : $("#txtVarizi" + i).val() != "" ? $("#txtVarizi" + i).removeClass('blinking') : $("#txtVarizi" + i).addClass('blinking');
                    $("#txtReUseHajm" + i).prop('disabled') ? '' : $("#txtReUseHajm" + i).val() != "" ? $("#txtReUseHajm" + i).removeClass('blinking') : $("#txtReUseHajm" + i).addClass('blinking');
                    $("#txtHaml" + i).prop('disabled') ? '' : $("#txtHaml" + i).val() != "" ? $("#txtHaml" + i).removeClass('blinking') : $("#txtHaml" + i).addClass('blinking');

                    $("#txtDarsadVarizi" + i).prop('disabled') ? '' : $("#txtDarsadVarizi" + i).val() != "" ? $("#txtDarsadVarizi" + i).removeClass('blinking') : $("#txtDarsadVarizi" + i).addClass('blinking');
                    $("#txtReUseDarsad" + i).prop('disabled') ? '' : $("#txtReUseDarsad" + i).val() != "" ? $("#txtReUseDarsad" + i).removeClass('blinking') : $("#txtReUseDarsad" + i).addClass('blinking');
                    $("#txtDarsadHaml" + i).prop('disabled') ? '' : $("#txtDarsadHaml" + i).val() != "" ? $("#txtDarsadHaml" + i).removeClass('blinking') : $("#txtDarsadHaml" + i).addClass('blinking');
                }

                let varizi = parseFloat($("#txtVarizi" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajm" + i).val()) || 0;
                let haml = parseFloat($("#txtHaml" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVarizi" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsad" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHaml" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail")) {
                    let Zarb = HajmKhakBardari === 0 ? 0 : (khDetail / HajmKhakBardari) * 100;
                    $("#txtDarsad" + i).val(Zarb.toFixed(2));

                    $('#spantotalDarsadKhDetailNew').html();

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmKhakBardari) {
                        let NewVal = HajmKhakBardari - (SumAll - khDetail);
                        $("#txtKhDetail" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmKhakBardari === 0 ? 0 : (khDetail / HajmKhakBardari) * 100;
                        $("#txtDarsad" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    dVarizi === 0 ? 0 : ((dVarizi / 100) * khDetail).toFixed(2);
                    $("#txtVarizi" + i).val(dVarizi);
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
                        dVarizi = (khDetail === 0 ? 0 : (varizi / khDetail)) * 100;
                        $("#txtDarsadVarizi" + i).val(dVarizi.toFixed(2));
                        $("#txtDarsadVarizi" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (khDetail === 0 ? 0 : (reuseHajm / khDetail)) * 100;
                        $("#txtReUseDarsad" + i).val(dReuse.toFixed(2));
                        $("#txtReUseDarsad" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (khDetail === 0 ? 0 : (haml / khDetail)) * 100;
                        $("#txtDarsadHaml" + i).val(dHaml.toFixed(2));
                        $("#txtDarsadHaml" + i).removeClass('blinking');
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
                        $("#txtDarsadVarizi" + i).removeClass('blinking');

                        $("#txtVarizi" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                        $("#txtVarizi" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseDarsad")) {
                        dReuse -= extra;
                        $("#txtReUseDarsad" + i).val(dReuse.toFixed(2));
                        $("#txtReUseDarsad" + i).removeClass('blinking');
                        $("#txtReUseHajm" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                        $("#txtReUseHajm" + i).removeClass('blinking');
                    } else if (changedId.includes("DarsadHaml")) {
                        dHaml -= extra;
                        $("#txtDarsadHaml" + i).val(dHaml.toFixed(2));
                        $("#txtDarsadHaml" + i).removeClass('blinking');
                        $("#txtHaml" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                        $("#txtHaml" + i).removeClass('blinking');
                    }
                }

                //// کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        varizi -= extra;
                        $("#txtVarizi" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVarizi" + i).val((khDetail === 0 ? 0 : (varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        reuseHajm -= extra;
                        $("#txtReUseHajm" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsad" + i).val((khDetail === 0 ? 0 : (reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        haml -= extra;
                        $("#txtHaml" + i).val(haml.toFixed(2));
                        $("#txtDarsadHaml" + i).val((khDetail === 0 ? 0 : (haml / khDetail) * 100).toFixed(2));
                    }
                }


                AutoFillThirdColumn(i, khDetail, changedId);

                SumKol = sumKolNew(ActivityLength);
                $('#spantotalKhDetailNew').html(toPersianDigits(SumKol[0].toFixed(2)));
                $('#spantotalDarsadKhDetailNew').html(toPersianDigits(SumKol[1].toFixed(2)) + " % ");

                $('#spantotalVariziNew').html(toPersianDigits(SumKol[2].toFixed(2)));

                DarsadVariziNew = (parseFloat(SumKol[2]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadVariziNew').html(toPersianDigits(DarsadVariziNew.toFixed(2)) + " % ");

                $('#spantotalReUseHajmNew').html(toPersianDigits(SumKol[3].toFixed(2)));
                DarsadReUseHajmNew = (parseFloat(SumKol[3]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadReUseHajmNew').html(toPersianDigits(DarsadReUseHajmNew.toFixed(2)) + " % ");

                $('#spantotalHamlNew').html(toPersianDigits(SumKol[4].toFixed(2)));
                DarsadHamlNew = (parseFloat(SumKol[4]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadHamlNew').html(toPersianDigits(DarsadHamlNew.toFixed(2))+" % ");
            });

            $('#txtHajmKhakBardari').on('keydown', function (e) {
                if (e.key === 'Enter') {
                    e.preventDefault();

                    let HKB = parseFloat($(this).val());

                    var KMS = parseFloat($('#txtFromKMForKhakbardari').val());//.replace('+', ''));
                    var KME = parseFloat($('#txtToKMForKhakbardari').val());//.replace('+', ''));

                    if (KMS < 0 || KME == 0) {
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

                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                        $(this).addClass('blinking');
                        return;
                    }

                    $('#MainViewKhakBardariNew').slideDown(500);
                }
            });

            $('#txtHajmKhakBardari').change(function () {
                let HKB = parseFloat($(this).val());

                var KMS = parseFloat($('#txtFromKMForKhakbardari').val());//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForKhakbardari').val());//.replace('+', ''));

                if (KMS < 0 || KME == 0) {
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

                debugger;
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
                //var KMSplit = KM.split('+');
                //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
                //    $(this).addClass('blinking');
                //    toastr.info('کیلومتراژ شروع وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
                //}
                //else {
                //    $(this).removeClass('blinking');
                //}


                var KMS = parseFloat(KM);//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForKhakbardari').val());//.replace('+', ''));
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
                var KM = $(this).val();
                //var KMSplit = KM.split('+');
                //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
                //    $(this).addClass('blinking');
                //    toastr.info('کیلومتراژ خاتمه وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
                //}
                //else {
                //    $(this).removeClass('blinking');
                //}


                var KME = parseFloat(KM);//.replace('+', ''));
                var KMS = parseFloat($('#txtFromKMForKhakbardari').val());//.replace('+', ''));
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
                debugger;
                $.each(KMAmalyateKhakiBarAvord, function () {
                    debugger;

                    KMExistingId = this.id;
                    FromKM = this.fromKM;
                    ToKM = this.toKM;
                    //FromKMSplit = this.fromKMSplit;
                    //ToKMSplit = this.toKMSplit;
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
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForKhakbardari${KMNum}" value="${FromKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- تا کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForKhakbardari${KMNum}" value="${ToKM}" onclick="event.stopPropagation();"/>
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

  <div class="row col-12" style="direction:ltr;display:none" id="MainViewKhakBardari${KMNum}" class="khakbardari-view">
    <div id="ViewKhakBardari${KMNum}" class="khakbardari-view" style="direction: rtl;">
    </div>
    <div class="col-md-2 action-col">
      <input type="button" value="ذخیره" class="btn buttonStyleBoard" style="color:#fff" onclick="UpdateKhakBardariInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum})" onclick="event.stopPropagation();"/>
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
                //$('#divExistingKM').find('#MainViewKhakBardari' + KMNum).hide();
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

function AutoFillThirdColumn(KMNum, khDetail, changedId) {
    debugger;
    // برای تشخیص "پر شده" بودن (خالی نبودن)
    const hasVal = (selector) => $.trim($(selector).val()) !== "";

    // selectors
    const $v = $("#txtVarizi" + KMNum);
    const $dv = $("#txtDarsadVarizi" + KMNum);

    const $r = $("#txtReUseHajm" + KMNum);
    const $dr = $("#txtReUseDarsad" + KMNum);

    const $h = $("#txtHaml" + KMNum);
    const $dh = $("#txtDarsadHaml" + KMNum);

    // helpers
    const clamp = (x, min, max) => Math.max(min, Math.min(max, x));
    const n = (x) => (parseFloat(x) || 0);

    // اگه khDetail معتبر نیست کاری نکن
    khDetail = n(khDetail);
    if (khDetail <= 0) return;

    // ردیف های 1 و 2 => ReUse همیشه صفر، و Varizi/Haml مکمل هم تا 100%
    const isNoReuseRow = (KMNum === "1" || KMNum === "2");
    if (isNoReuseRow) {
        $r.val("0.00");
        $dr.val("0.00");

        // اگر درصد یکی پر باشد، دیگری = 100 - آن
        const variziPercentFilled = hasVal("#txtDarsadVarizi" + KMNum);
        const hamlPercentFilled = hasVal("#txtDarsadHaml" + KMNum);

        // اگر حجم یکی پر باشد، درصدش را بساز
        const variziVolFilled = hasVal("#txtVarizi" + KMNum);
        const hamlVolFilled = hasVal("#txtHaml" + KMNum);

        // اول سعی کن درصدها را کامل کنی
        if (variziPercentFilled && !hamlPercentFilled) {
            let dv = clamp(n($dv.val()), 0, 100);
            let dh = clamp(100 - dv, 0, 100);
            $dh.val(dh.toFixed(2));
            $dh.removeClass('blinking');

            $v.val(dv===0?'': ((dv / 100) * khDetail).toFixed(2));
            $v.removeClass('blinking');

            $h.val(dh===0?'':((dh / 100) * khDetail).toFixed(2));
            $h.removeClass('blinking');

            return;
        }
        if (hamlPercentFilled && !variziPercentFilled) {
            let dh = clamp(n($dh.val()), 0, 100);
            let dv = clamp(100 - dh, 0, 100);
            $dv.val(dv.toFixed(2));
            $dv.removeClass('blinking');

            $h.val(dh===0?'':((dh / 100) * khDetail).toFixed(2));
            $h.removeClass('blinking');

            $v.val(dv===0?'':((dv / 100) * khDetail).toFixed(2));
            $v.removeClass('blinking');

            return;
        }

        // اگر درصدها پر نیستند، با حجم‌ها برو جلو: یکی پر شد -> دیگری = باقی‌مانده تا khDetail
        if (variziVolFilled && !hamlVolFilled) {
            let vv = clamp(n($v.val()), 0, khDetail);
            let vh = clamp(khDetail - vv, 0, khDetail);
            $h.val(vh.toFixed(2));
            $h.removeClass('blinking');

            $dv.val(dv===0?'':(khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
            $dv.removeClass('blinking');

            $dh.val(dh===0?'':(khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
            $dh.removeClass('blinking');

            return;
        }
        if (hamlVolFilled && !variziVolFilled) {
            let vh = clamp(n($h.val()), 0, khDetail);
            let vv = clamp(khDetail - vh, 0, khDetail);
            $v.val(vv.toFixed(2));
            $v.removeClass('blinking');

            $dh.val(dh===0?'':(khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
            $dh.removeClass('blinking');

            $dv.val(dv===0?'':(khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
            $dv.removeClass('blinking');

            return;
        }

        // اگر هر دو پر هستند، فقط اصلاح clamp
        let vv = clamp(n($v.val()), 0, khDetail);
        let vh = clamp(n($h.val()), 0, khDetail);
        // اگر جمع بیشتر شد، همان چیزی که تغییر کرده را نگه دار و دیگری را کم کن
        if (vv + vh > khDetail) {
            if (changedId.includes("Varizi")) vh = clamp(khDetail - vv, 0, khDetail);
            else vv = clamp(khDetail - vh, 0, khDetail);
            $v.val(vv.toFixed(2));
            $v.removeClass('blinking');

            $h.val(vh.toFixed(2));
            $h.removeClass('blinking');

        }
        $dv.val(vv===0?'':(vv / khDetail * 100).toFixed(2));
        $dv.removeClass('blinking');

        $dh.val(vh===0?'':(vh / khDetail * 100).toFixed(2));
        $dh.removeClass('blinking');

        return;
    }

    // سایر ردیف ها: سه ستون فعال (Varizi / ReUse / Haml)

    // اگر درصدی وارد شده، حجمش را بساز
    // (فقط برای فیلدی که خودش درصدش پر است)
    if (hasVal("#txtDarsadVarizi" + KMNum)) $v.val(((clamp(n($dv.val()), 0, 100) / 100) * khDetail).toFixed(2));
    if (hasVal("#txtReUseDarsad" + KMNum)) $r.val(((clamp(n($dr.val()), 0, 100) / 100) * khDetail).toFixed(2));
    if (hasVal("#txtDarsadHaml" + KMNum)) $h.val(((clamp(n($dh.val()), 0, 100) / 100) * khDetail).toFixed(2));

    // تشخیص اینکه کدام درصدها "پر شده‌اند"
    const filledP = {
        v: hasVal("#txtDarsadVarizi" + KMNum),
        r: hasVal("#txtReUseDarsad" + KMNum),
        h: hasVal("#txtDarsadHaml" + KMNum),
    };

    // تشخیص اینکه کدام حجم‌ها "پر شده‌اند"
    const filledV = {
        v: hasVal("#txtVarizi" + KMNum),
        r: hasVal("#txtReUseHajm" + KMNum),
        h: hasVal("#txtHaml" + KMNum),
    };

    // اگر دو تا درصد پر بود => سومی = 100 - جمع
    const countP = (filledP.v ? 1 : 0) + (filledP.r ? 1 : 0) + (filledP.h ? 1 : 0);
    if (countP >= 2) {
        let dv = clamp(n($dv.val()), 0, 100);
        let dr = clamp(n($dr.val()), 0, 100);
        let dh = clamp(n($dh.val()), 0, 100);

        if (!filledP.v) dv = clamp(100 - (dr + dh), 0, 100), $dv.val(dv.toFixed(2));
        if (!filledP.r) dr = clamp(100 - (dv + dh), 0, 100), $dr.val(dr.toFixed(2));
        if (!filledP.h) dh = clamp(100 - (dv + dr), 0, 100), $dh.val(dh.toFixed(2));

        $v.val(dv===0?'':((dv / 100) * khDetail).toFixed(2));
        $v.removeClass('blinking');
        $r.val(dr===0?'':((dr / 100) * khDetail).toFixed(2));
        $r.removeClass('blinking');
        $h.val(dh===0?'':((dh / 100) * khDetail).toFixed(2));
        $h.removeClass('blinking');
        return;
    }

    // اگر دو تا حجم پر بود => سومی = khDetail - جمع
    const countV = (filledV.v ? 1 : 0) + (filledV.r ? 1 : 0) + (filledV.h ? 1 : 0);
    if (countV >= 2) {
        let vv = clamp(n($v.val()), 0, khDetail);
        let vr = clamp(n($r.val()), 0, khDetail);
        let vh = clamp(n($h.val()), 0, khDetail);

        if (!filledV.v) vv = clamp(khDetail - (vr + vh), 0, khDetail), $v.val(vv.toFixed(2));
        if (!filledV.r) vr = clamp(khDetail - (vv + vh), 0, khDetail), $r.val(vr.toFixed(2));
        if (!filledV.h) vh = clamp(khDetail - (vv + vr), 0, khDetail), $h.val(vh.toFixed(2));

        $dv.val(vv===0?'':(vv / khDetail * 100).toFixed(2));
        $dv.removeClass('blinking');

        $dr.val(vr===0?'':(vr / khDetail * 100).toFixed(2));
        $dr.removeClass('blinking');

        $dh.val(vh===0?'':(vh / khDetail * 100).toFixed(2));
        $dh.removeClass('blinking');

        return;
    }

    // حالت‌های تک‌ورودی: فعلاً کاری نکن (تا وقتی دومی پر بشه)
}

function AutoFillThirdColumnForEdit(KMNum, i, khDetail, changedId) {
    debugger;
    // برای تشخیص "پر شده" بودن (خالی نبودن)
    const hasVal = (selector) => $.trim($(selector).val()) !== "";

    // selectors
    const $v = $("#txtVarizi" + KMNum + "_" + i);
    const $dv = $("#txtDarsadVarizi" + KMNum + "_" + i);

    const $r = $("#txtReUseHajm" + KMNum + "_" + i);
    const $dr = $("#txtReUseDarsad" + KMNum + "_" + i);

    const $h = $("#txtHaml" + KMNum + "_" + i);
    const $dh = $("#txtDarsadHaml" + KMNum + "_" + i);

    // helpers
    const clamp = (x, min, max) => Math.max(min, Math.min(max, x));
    const n = (x) => (parseFloat(x) || 0);

    // اگه khDetail معتبر نیست کاری نکن
    khDetail = n(khDetail);
    if (khDetail <= 0) return;

    // ردیف های 1 و 2 => ReUse همیشه صفر، و Varizi/Haml مکمل هم تا 100%
    const isNoReuseRow = (i === "1" || i === "2");
    if (isNoReuseRow) {
        $r.val("0.00");
        $dr.val("0.00");

        // اگر درصد یکی پر باشد، دیگری = 100 - آن
        const variziPercentFilled = hasVal("#txtDarsadVarizi" + KMNum + "_" + i);
        const hamlPercentFilled = hasVal("#txtDarsadHaml" + KMNum + "_" + i);

        // اگر حجم یکی پر باشد، درصدش را بساز
        const variziVolFilled = hasVal("#txtVarizi" + KMNum + "_" + i);
        const hamlVolFilled = hasVal("#txtHaml" + KMNum + "_" + i);

        // اول سعی کن درصدها را کامل کنی
        if (variziPercentFilled && !hamlPercentFilled) {
            let dv = clamp(n($dv.val()), 0, 100);
            let dh = clamp(100 - dv, 0, 100);
            $dh.val(dh.toFixed(2));
            $dh.removeClass('blinking');

            $v.val(((dv / 100) * khDetail).toFixed(2));
            $v.removeClass('blinking');

            $h.val(((dh / 100) * khDetail).toFixed(2));
            $h.removeClass('blinking');

            return;
        }
        if (hamlPercentFilled && !variziPercentFilled) {
            let dh = clamp(n($dh.val()), 0, 100);
            let dv = clamp(100 - dh, 0, 100);
            $dv.val(dv.toFixed(2));
            $dv.removeClass('blinking');

            $h.val(((dh / 100) * khDetail).toFixed(2));
            $h.removeClass('blinking');

            $v.val(((dv / 100) * khDetail).toFixed(2));
            $v.removeClass('blinking');

            return;
        }

        // اگر درصدها پر نیستند، با حجم‌ها برو جلو: یکی پر شد -> دیگری = باقی‌مانده تا khDetail
        if (variziVolFilled && !hamlVolFilled) {
            let vv = clamp(n($v.val()), 0, khDetail);
            let vh = clamp(khDetail - vv, 0, khDetail);
            $h.val(vh.toFixed(2));
            $h.removeClass('blinking');

            $dv.val((khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
            $dv.removeClass('blinking');

            $dh.val((khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
            $dh.removeClass('blinking');

            return;
        }
        if (hamlVolFilled && !variziVolFilled) {
            let vh = clamp(n($h.val()), 0, khDetail);
            let vv = clamp(khDetail - vh, 0, khDetail);
            $v.val(vv.toFixed(2));
            $v.removeClass('blinking');

            $dh.val((khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
            $dh.removeClass('blinking');

            $dv.val((khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
            $dv.removeClass('blinking');

            return;
        }

        // اگر هر دو پر هستند، فقط اصلاح clamp
        let vv = clamp(n($v.val()), 0, khDetail);
        let vh = clamp(n($h.val()), 0, khDetail);
        // اگر جمع بیشتر شد، همان چیزی که تغییر کرده را نگه دار و دیگری را کم کن
        if (vv + vh > khDetail) {
            if (changedId.includes("Varizi")) vh = clamp(khDetail - vv, 0, khDetail);
            else vv = clamp(khDetail - vh, 0, khDetail);
            $v.val(vv.toFixed(2));
            $v.removeClass('blinking');

            $h.val(vh.toFixed(2));
            $h.removeClass('blinking');

        }
        $dv.val((vv / khDetail * 100).toFixed(2));
        $dv.removeClass('blinking');

        $dh.val((vh / khDetail * 100).toFixed(2));
        $dh.removeClass('blinking');

        return;
    }

    // سایر ردیف ها: سه ستون فعال (Varizi / ReUse / Haml)

    // اگر درصدی وارد شده، حجمش را بساز
    // (فقط برای فیلدی که خودش درصدش پر است)
    if (hasVal("#txtDarsadVarizi" + KMNum + "_" + i)) $v.val(((clamp(n($dv.val()), 0, 100) / 100) * khDetail).toFixed(2));
    if (hasVal("#txtReUseDarsad" + KMNum + "_" + i)) $r.val(((clamp(n($dr.val()), 0, 100) / 100) * khDetail).toFixed(2));
    if (hasVal("#txtDarsadHaml" + KMNum + "_" + i)) $h.val(((clamp(n($dh.val()), 0, 100) / 100) * khDetail).toFixed(2));

    // تشخیص اینکه کدام درصدها "پر شده‌اند"
    const filledP = {
        v: hasVal("#txtDarsadVarizi" + KMNum + "_" + i),
        r: hasVal("#txtReUseDarsad" + KMNum + "_" + i),
        h: hasVal("#txtDarsadHaml" + KMNum + "_" + i),
    };

    // تشخیص اینکه کدام حجم‌ها "پر شده‌اند"
    const filledV = {
        v: hasVal("#txtVarizi" + KMNum + "_" + i),
        r: hasVal("#txtReUseHajm" + KMNum + "_" + i),
        h: hasVal("#txtHaml" + KMNum + "_" + i),
    };

    // اگر دو تا درصد پر بود => سومی = 100 - جمع
    const countP = (filledP.v ? 1 : 0) + (filledP.r ? 1 : 0) + (filledP.h ? 1 : 0);
    if (countP >= 2) {
        let dv = clamp(n($dv.val()), 0, 100);
        let dr = clamp(n($dr.val()), 0, 100);
        let dh = clamp(n($dh.val()), 0, 100);

        if (!filledP.v) dv = clamp(100 - (dr + dh), 0, 100), $dv.val(dv.toFixed(2));
        if (!filledP.r) dr = clamp(100 - (dv + dh), 0, 100), $dr.val(dr.toFixed(2));
        if (!filledP.h) dh = clamp(100 - (dv + dr), 0, 100), $dh.val(dh.toFixed(2));

        $v.val(((dv / 100) * khDetail).toFixed(2));
        $v.removeClass('blinking');
        $r.val(((dr / 100) * khDetail).toFixed(2));
        $r.removeClass('blinking');
        $h.val(((dh / 100) * khDetail).toFixed(2));
        $h.removeClass('blinking');
        return;
    }

    // اگر دو تا حجم پر بود => سومی = khDetail - جمع
    const countV = (filledV.v ? 1 : 0) + (filledV.r ? 1 : 0) + (filledV.h ? 1 : 0);
    if (countV >= 2) {
        let vv = clamp(n($v.val()), 0, khDetail);
        let vr = clamp(n($r.val()), 0, khDetail);
        let vh = clamp(n($h.val()), 0, khDetail);

        if (!filledV.v) vv = clamp(khDetail - (vr + vh), 0, khDetail), $v.val(vv.toFixed(2));
        if (!filledV.r) vr = clamp(khDetail - (vv + vh), 0, khDetail), $r.val(vr.toFixed(2));
        if (!filledV.h) vh = clamp(khDetail - (vv + vr), 0, khDetail), $h.val(vh.toFixed(2));

        $dv.val((vv / khDetail * 100).toFixed(2));
        $dv.removeClass('blinking');

        $dr.val((vr / khDetail * 100).toFixed(2));
        $dr.removeClass('blinking');

        $dh.val((vh / khDetail * 100).toFixed(2));
        $dh.removeClass('blinking');

        return;
    }

    // حالت‌های تک‌ورودی: فعلاً کاری نکن (تا وقتی دومی پر بشه)
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
    vardata.Type = 1;

    debugger;

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
        <div class="row AmalyatKhakiRowStyle">

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
                KhDetail = KhDetail == null ? 0 : KhDetail;
                DarsadKhDetail = DarsadKhDetail == null ? 0 : DarsadKhDetail;

                // جمع کردن مقادیر
                totalKhDetail += parseFloat(KhDetail);
                totalDarsadKhDetail += parseFloat(DarsadKhDetail);


                strKMAK += ` <div class="col-8" style="padding:0;">
          <div class="row" style="margin:0;">

        <!-- حجم خاکبرداری -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetail${KMNum}_${i}" value="${KhDetail == 0 ? '' : KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsad${KMNum}_${i}" value="${DarsadKhDetail == 0 ? '' : DarsadKhDetail}" />
            </div>
          </div>
        </div>`;


                let Varizi = result.filter(x => x.name === "Varizi").length > 0 ? result.filter(x => x.name === "Varizi")[0].value : 0;
                let DarsadVarizi = result.filter(x => x.name === "DarsadVarizi").length > 0 ? result.filter(x => x.name === "DarsadVarizi")[0].value : 0;

                Varizi = Varizi == null ? 0 : Varizi;
                DarsadVarizi = DarsadVarizi == null ? 0 : DarsadVarizi;

                totalVarizi += parseFloat(Varizi);
                totalDarsadVarizi += parseFloat(DarsadVarizi);
                strKMAK += `
         <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle"  id="txtVarizi${KMNum}_${i}" value="${Varizi == 0 ? '' : Varizi}"
}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVarizi${KMNum}_${i}" value="${DarsadVarizi == 0 ? '' : DarsadVarizi}"  />
            </div>
          </div>
        </div>`

                let ReUseHajm = result.filter(x => x.name === "ReUseHajm").length > 0 ? result.filter(x => x.name === "ReUseHajm")[0].value : 0;
                let DarsadReUseHajm = result.filter(x => x.name === "DarsadReUseHajm").length > 0 ? result.filter(x => x.name === "DarsadReUseHajm")[0].value : 0;

                ReUseHajm = ReUseHajm == null ? 0 : ReUseHajm;
                DarsadReUseHajm = DarsadReUseHajm == null ? 0 : DarsadReUseHajm;

                totalReUseHajm += parseFloat(ReUseHajm);
                totalDarsadReUseHajm += parseFloat(DarsadReUseHajm);
                strKMAK += `
          <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" ${(i === 1 || i === 2) ? `disabled` : ``} id="txtReUseHajm${KMNum}_${i}" value="${ReUseHajm == 0 ? '' : ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" ${(i === 1 || i === 2) ? `disabled` : ``} id="txtReUseDarsad${KMNum}_${i}" value="${DarsadReUseHajm == 0 ? '' : DarsadReUseHajm}"  />
            </div>
          </div>
        </div>`
                let Haml = result.filter(x => x.name === "Haml").length > 0 ? result.filter(x => x.name === "Haml")[0].value : 0;
                let DarsadHaml = result.filter(x => x.name === "DarsadHaml").length > 0 ? result.filter(x => x.name === "DarsadHaml")[0].value : 0;

                Haml = Haml == null ? 0 : Haml;
                DarsadHaml = DarsadHaml == null ? 0 : DarsadHaml;

                totalHaml += parseFloat(Haml);
                totalDarsadHaml += parseFloat(DarsadHaml);
                strKMAK += `
        <!-- حمل به دپو/مسیر -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHaml${KMNum}_${i}" value="${Haml == 0 ? '' : Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHaml${KMNum}_${i}" value="${DarsadHaml == 0 ? '' : DarsadHaml}"  />
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
              <span id="spantotalKhDetail${KMNum}">${fmt(totalKhDetail)}</span>
            </div>
            <div class="col-4" style="text-align:center;padding:0 2px;">
              <span id="spantotalDarsadKhDetail${KMNum}">%${fmt(totalDarsadKhDetail)}</span>
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalVarizi${KMNum}">${fmt(totalVarizi)}</span>
            </div>
            
          </div>
        </div>

        <!-- مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalReUseHajm${KMNum}">${fmt(totalReUseHajm)}</span>
            </div>
            
          </div>
        </div>

        <!-- حمل به دپو -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantototalHaml${KMNum}">${fmt(totalHaml)}</span>
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
                    $(this).addClass('blinking');
                    return;
                } else {
                    $(this).removeClass('blinking');
                }

                if (HajmKhakBardari == 0 || HajmKhakBardari == '' || !$.isNumeric(HajmKhakBardari)) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmKhakBardari' + KMNum).addClass('blinking');
                    return;
                } else {
                    $('#txtHajmKhakBardari' + KMNum).removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetail" + KMNum + "_" + i).val()) || 0;

                if (khDetail != null || khDetail != 0) {

                    $("#txtVarizi" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtVarizi" + KMNum + "_" + i).val() != "" ? $("#txtVarizi" + KMNum + "_" + i).removeClass('blinking') : $("#txtVarizi" + KMNum + "_" + i).addClass('blinking');
                    $("#txtReUseHajm" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtReUseHajm" + KMNum + "_" + i).val() != "" ? $("#txtReUseHajm" + KMNum + "_" + i).removeClass('blinking') : $("#txtReUseHajm" + KMNum + "_" + i).addClass('blinking');
                    $("#txtHaml" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtHaml" + KMNum + "_" + i).val() != "" ? $("#txtHaml" + KMNum + "_" + i).removeClass('blinking') : $("#txtHaml" + KMNum + "_" + i).addClass('blinking');

                    $("#txtDarsadVarizi" + i).prop('disabled') ? '' : $("#txtDarsadVarizi" + KMNum + "_" + i).val() != "" ? $("#txtDarsadVarizi" + KMNum + "_" + i).removeClass('blinking') : $("#txtDarsadVarizi" + KMNum + "_" + i).addClass('blinking');
                    $("#txtReUseDarsad" + i).prop('disabled') ? '' : $("#txtReUseDarsad" + KMNum + "_" + i).val() != "" ? $("#txtReUseDarsad" + KMNum + "_" + i).removeClass('blinking') : $("#txtReUseDarsad" + KMNum + "_" + i).addClass('blinking');
                    $("#txtDarsadHaml" + i).prop('disabled') ? '' : $("#txtDarsadHaml" + KMNum + "_" + i).val() != "" ? $("#txtDarsadHaml" + KMNum + "_" + i).removeClass('blinking') : $("#txtDarsadHaml" + KMNum + "_" + i).addClass('blinking');
                }

                let varizi = parseFloat($("#txtVarizi" + KMNum + "_" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajm" + KMNum + "_" + i).val()) || 0;
                let haml = parseFloat($("#txtHaml" + KMNum + "_" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVarizi" + KMNum + "_" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsad" + KMNum + "_" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHaml" + KMNum + "_" + i).val()) || 0;


                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail" + KMNum)) {
                    let Zarb = HajmKhakBardari === 0 ? 0 : (khDetail / HajmKhakBardari) * 100;
                    $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetailsKhakBardariForEdit(KMNum);
                    if (SumAll > HajmKhakBardari) {
                        let NewVal = HajmKhakBardari - (SumAll - khDetail);
                        $("#txtKhDetail" + KMNum + "_" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmKhakBardari === 0 ? 0 : (khDetail / HajmKhakBardari) * 100;
                        $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVarizi" + KMNum + "_" + i).val(dVarizi > 0 ? ((dVarizi / 100) * khDetail).toFixed(2) : "");
                    $("#txtReUseHajm" + KMNum + "_" + i).val(dReuse > 0 ? ((dReuse / 100) * khDetail).toFixed(2) : "");
                    $("#txtHaml" + KMNum + "_" + i).val(dHaml > 0 ? ((dHaml / 100) * khDetail).toFixed(2) : "");
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
                        dVarizi = khDetail === 0 ? 0 : (varizi / khDetail) * 100;
                        $("#txtDarsadVarizi" + KMNum + "_" + i).val(dVarizi.toFixed(2));
                        $("#txtDarsadVarizi" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        dReuse = khDetail === 0 ? 0 : (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsad" + KMNum + "_" + i).val(dReuse.toFixed(2));
                        $("#txtReUseDarsad" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dHaml = khDetail === 0 ? 0 : (haml / khDetail) * 100;
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(2));
                        $("#txtDarsadHaml" + KMNum + "_" + i).removeClass('blinking');
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
                        $("#txtDarsadVarizi" + KMNum + "_" + i).removeClass('blinking');

                        $("#txtVarizi" + KMNum + "_" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                        $("#txtVarizi" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        dReuse -= extra;
                        $("#txtReUseDarsad" + KMNum + "_" + i).val(dReuse.toFixed(2));
                        $("#txtReUseDarsad" + KMNum + "_" + i).removeClass('blinking');
                        $("#txtReUseHajm" + KMNum + "_" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                        $("#txtReUseHajm" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        dHaml -= extra;
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(2));
                        $("#txtDarsadHaml" + KMNum + "_" + i).removeClass('blinking');
                        $("#txtHaml" + KMNum + "_" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                        $("#txtHaml" + KMNum + "_" + i).removeClass('blinking');
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        varizi -= extra;
                        $("#txtVarizi" + KMNum + "_" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVarizi" + KMNum + "_" + i).val((khDetail === 0 ? 0 : (varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        reuseHajm -= extra;
                        $("#txtReUseHajm" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsad" + KMNum + "_" + i).val((khDetail === 0 ? 0 : (reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        haml -= extra;
                        $("#txtHaml" + KMNum + "_" + i).val(haml.toFixed(2));
                        $("#txtDarsadHaml" + KMNum + "_" + i).val((khDetail === 0 ? 0 : (haml / khDetail) * 100).toFixed(2));
                    }
                }

                AutoFillThirdColumnForEdit(KMNum, i, khDetail, changedId);

            });


            $('#txtHajmKhakBardari' + KMNum).off('change').change(function () {
                debugger;


                let HKB = parseFloat($(this).val());

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
                var inputs = $(this).parent().parent().find("input[Type=text].HasEnteringValue,button,a");
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

function ReturnSumAllDetailsKhakBardariForEdit(KMNum) {
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

function SumAllDetailsKhakBardari() {
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
    else if (sumAll < HajmKhakBardari) {
        $('#txtHajmKhakBardari').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم خاکبرداری کمتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmKhakBardari').removeClass('blinking');
        return false;
    }
}


function SumAllDetailsKhakBardariForEdit(KMNum) {

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
    else if (sumAll < HajmKhakBardari) {
        $('#txtHajmKhakBardari' + KMNum).addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم خاکبرداری کمتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmKhakBardari' + KMNum).removeClass('blinking');
        return false;
    }
}

function CheckValuesOfKhakBardariDetails() {
    debugger;
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        KhDetail = $('#txtKhDetail' + i).val() == '' ? 0 : parseFloat($('#txtKhDetail' + i).val());
        ReUseHajm = $('#txtReUseHajm' + i).val() == '' ? 0 : parseFloat($('#txtReUseHajm' + i).val());
        ReUseDarsad = $('#txtReUseDarsad' + i).val() == '' ? 0 : parseFloat($('#txtReUseDarsad' + i).val());
        Varizi = $('#txtVarizi' + i).val() == '' ? 0 : parseFloat($('#txtVarizi' + i).val());
        DarsadVarizi = $('#txtDarsadVarizi' + i).val() == '' ? 0 : parseFloat($('#txtDarsadVarizi' + i).val());
        Haml = $('#txtHaml' + i).val() == '' ? 0 : parseFloat($('#txtHaml' + i).val());
        DarsadHaml = $('#txtDarsadHaml' + i).val() == '' ? 0 : parseFloat($('#txtDarsadHaml' + i).val());

        ReUseHajm = isNaN(ReUseHajm) ? 0 : ReUseHajm;
        Varizi = isNaN(Varizi) ? 0 : Varizi;
        Haml = isNaN(Haml) ? 0 : Haml;
        sumAllThisRow = ReUseHajm + Varizi + Haml;
        if (sumAllThisRow != KhDetail) {
            currentRow = $('#txtDarsadHaml' + i).closest('.AmalyatKhakiRowStyle');
            currentRow.addClass('errorRow');
            checkValues = true;
            return true;
        }
        if (sumAllThisRow < KhDetail) {
            if (i != 1 && i != 2) {
                $('#txtReUseHajm' + i).addClass('blinking');
            }
            $('#txtVarizi' + i).addClass('blinking');
            $('#txtHaml' + i).addClass('blinking');
            return true;
        }
        if (sumAllThisRow > KhDetail) {
            if (i != 1 && i != 2) {
                $('#txtReUseHajm' + i).addClass('blinking');
            }
            $('#txtVarizi' + i).addClass('blinking');
            $('#txtHaml' + i).addClass('blinking');
            return true;
        }
        else if (sumAllThisRow == KhDetail) {
            currentRow = $('#txtDarsadHaml' + i).closest('.AmalyatKhakiRowStyle');
            currentRow.removeClass('errorRow');

            $('#txtReUseHajm' + i).removeClass('blinking');
            $('#txtVarizi' + i).removeClass('blinking');
            $('#txtHaml' + i).removeClass('blinking');

            $('#txtReUseDarsad' + i).removeClass('blinking');
            $('#txtDarsadVarizi' + i).removeClass('blinking');
            $('#txtDarsadHaml' + i).removeClass('blinking');
            return false;
        }

        if (KhDetail != 0 && KhDetail != '') {
            if (ReUseHajm == 0 || isNaN(ReUseHajm)) {
                $('#txtReUseHajm' + i).addClass('blinking');
                return true;
            }
            else if (Varizi == 0 || isNaN(Varizi)) {
                $('#txtVarizi' + i).addClass('blinking');
                return true;
            }
            else if (Haml == 0 || isNaN(Haml)) {
                $('#txtHaml' + i).addClass('blinking');
                return true;
            }
            else return false;
        }

        if (ReUseHajm > KhDetail) {
            $('#txtReUseHajm' + i).addClass('blinking');
            checkValues = true;
        }
        else
            $('#txtReUseHajm' + i).removeClass('blinking');

        if (ReUseDarsad > 100) {
            $('#txtReUseDarsad' + i).addClass('blinking');
            checkValues = true;
        }
        else
            $('#txtReUseDarsad' + i).removeClass('blinking');
        /////////////
        if (Varizi > KhDetail) {
            $('#txtVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtVarizi' + i).removeClass('blinking');
        }

        if (Haml > KhDetail) {
            $('#txtHaml' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtHaml' + i).removeClass('blinking');
        }

        if (DarsadVarizi > 100) {
            $('#txtDarsadVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadVarizi' + i).removeClass('blinking');
        }

        if (DarsadHaml > 100) {
            $('#txtDarsadHaml' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadHaml' + i).removeClass('blinking');
        }
    }
    return checkValues;
}

function CheckValuesOfKhakBardariDetailsForEdit(KMNum) {
    debugger;
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        JamKol = 0;
        JamDarsad = 0;
        KhDetail = parseFloat($('#txtKhDetail' + KMNum + "_" + i).val());
        if (!isNaN(KhDetail)) {

            ReUseHajm = parseFloat($('#txtReUseHajm' + KMNum + "_" + i).val());
            if (ReUseHajm > KhDetail) {
                $('#txtReUseHajm' + KMNum + "_" + i).addClass('blinking');
                checkValues = true;
            }
            else
                $('#txtReUseHajm' + KMNum + "_" + i).removeClass('blinking');
            JamKol += isNaN(ReUseHajm) ? 0 : ReUseHajm;

            ReUseDarsad = parseFloat($('#txtReUseDarsad' + KMNum + "_" + i).val());
            if (ReUseDarsad > 100) {
                $('#txtReUseDarsad' + KMNum + "_" + i).addClass('blinking');
                checkValues = true;
            }
            else
                $('#txtReUseDarsad' + KMNum + "_" + i).removeClass('blinking');
            JamDarsad += isNaN(ReUseDarsad) ? 0 : ReUseDarsad;

            /////////////
            Varizi = parseFloat($('#txtVarizi' + KMNum + "_" + i).val());
            if (Varizi > KhDetail) {
                $('#txtVarizi' + KMNum + "_" + i).addClass('blinking');
                checkValues = true;
            }
            else {
                $('#txtVarizi' + KMNum + "_" + i).removeClass('blinking');
            }
            JamKol += isNaN(Varizi) ? 0 : Varizi;


            Haml = parseFloat($('#txtHaml' + KMNum + "_" + i).val());
            if (Haml > KhDetail) {
                $('#txtHaml' + KMNum + "_" + i).addClass('blinking');
                checkValues = true;
            }
            else {
                $('#txtHaml' + KMNum + "_" + i).removeClass('blinking');
            }
            JamKol += isNaN(Haml) ? 0 : Haml;

            DarsadVarizi = parseFloat($('#txtDarsadVarizi' + KMNum + "_" + i).val());
            if (DarsadVarizi > 100) {
                $('#txtDarsadVarizi' + KMNum + "_" + i).addClass('blinking');
                checkValues = true;
            }
            else {
                $('#txtDarsadVarizi' + KMNum + "_" + i).removeClass('blinking');
            }
            JamDarsad += isNaN(DarsadVarizi) ? 0 : DarsadVarizi;


            DarsadHaml = parseFloat($('#txtDarsadHaml' + KMNum + "_" + i).val());
            if (DarsadHaml > 100) {
                $('#txtDarsadHaml' + KMNum + "_" + i).addClass('blinking');
                checkValues = true;
            }
            else {
                $('#txtDarsadHaml' + KMNum + "_" + i).removeClass('blinking');
            }
            JamDarsad += isNaN(DarsadHaml) ? 0 : DarsadHaml;

            if (JamDarsad != 100 || JamKol != KhDetail) {
                currentRow = $('#txtDarsadHaml' + KMNum + "_" + i).closest('.AmalyatKhakiRowStyle');
                currentRow.addClass('errorRow');
                checkValues = true;
            }
        }
    }
    return checkValues;
}


function SaveKhakBardariInfo(BarAvordUserId, Type) {
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
    //var KM = $('#txtFromKMForKhakbardari').val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForKhakbardari').addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForKhakbardari').removeClass('blinking');
    //}
    /////////////////
    //var KM = $('#txtToKMForKhakbardari').val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForKhakbardari').addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForKhakbardari').removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForKhakbardari').val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForKhakbardari').val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForKhakbardari').addClass('blinking');
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
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    debugger;

    check = SumAllDetailsKhakBardari();

    //if (check) {
    //    toastr.info('مجموع احجام وارد شده بایستی با حجم کل برابر باشد', 'اطلاع');
    //    return 0;
    //}

    check1 = CheckValuesOfKhakBardariDetails();

    //if (check1) {
    //toastr.info('احجام وارد شده درست نمی باشند', 'اطلاع');
    //}
    debugger;

    if (!check1 && !check) {

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
        NoeFBId = parseInt($('#HDFNoeFB').val());

        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Type = 1;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.NoeFBId = NoeFBId;
        $.ajax({
            type: "POST",
            url: "/AmalyateKhakiInfoForBarAvords/SaveKhakBardariInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    //$('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
                    //$('#HDFKMAmalyateKhakiIdForEdit').val(info[1]);
                    //$('#HDFKMAmalyateKhakiNum').val(info[2]);

                    $('#txtHajmKhakBardari').val(0);
                    $('#txtFromKMForKhakbardari').val('0')
                    $('#txtToKMForKhakbardari').val('0')

                    for (let i = 1; i <= ActivityLength; i++) {
                        $('#txtKhDetail' + i).val(0);
                        $('#txtDarsad' + i).val(0);
                        $('#txtReUseHajm' + i).val(0);
                        $('#txtReUseDarsad' + i).val(0);
                        $('#txtVarizi' + i).val(0);
                        $('#txtDarsadVarizi' + i).val(0);
                        $('#txtHaml' + i).val(0);
                        $('#txtDarsadHaml' + i).val(0);
                        //$('#txtKhakBardariItemId' + i).val(0);
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
    //var KM = $('#txtFromKMForKhakbardari' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForKhakbardari' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForKhakbardari' + KMNum).removeClass('blinking');
    //}
    /////////////////
    //var KM = $('#txtToKMForKhakbardari' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForKhakbardari' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForKhakbardari' + KMNum).removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForKhakbardari' + KMNum).val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForKhakbardari' + KMNum).val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForKhakbardari' + KMNum).addClass('blinking');
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
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    check = SumAllDetailsKhakBardariForEdit(KMNum);
    //if (check) {
    //    toastr.info('حجم کل خاکبرداری صحیح نمیباشد', 'اطلاع');
    //}

    check1 = CheckValuesOfKhakBardariDetailsForEdit(KMNum);
    //if (check1) {
    //    toastr.info('مشکل در مقادیر وارده', 'اطلاع');
    //}
    debugger;

    if (!check1 && !check) {

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
        NoeFBId = parseInt($('#HDFNoeFB').val());
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMKhakBardariId = KMKhakBardariId;
        vardata.KMNum = KMNum;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.Year = Year;
        vardata.NoeFBId = NoeFBId;
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
