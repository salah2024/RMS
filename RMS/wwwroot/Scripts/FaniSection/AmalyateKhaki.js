ActivityLength = 0;

function KhakBardariMashinWithBarAvordIdClick(OpId, BarAvordUserId) {
    str = "";
    debugger;
    str += `

  <div class="col-12" id="divExistingKM">
  </div>


      <!-- متراژ های ثبت شده -->

    <div class="khakbardari-container" style="border: 1px solid #cb92ff;background-color: #f3ebff;">
    <div class="row khakbardari-box">

    <!-- از متراژ -->
    <div class="col-md-1">
        <i class="fa fa-plus" style="font-size: 20px;color: green;"></i>
    </div>
    <div class="col-md-1 label-col">
      <span>از متراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtFromKMForKhakbardari" value="0"/>
    </div>

    <!-- تا متراژ -->
    <div class="col-md-1 label-col">
      <span>تا متراژ:</span>
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

 <div class="col-md-1 action-col">
     <button type="button"
            onclick="SaveKhakBardariInfo('${BarAvordUserId}'` + ',' + `'Add')"
            class="ButtonRowsSaveStyle">
            <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
     </button>
    </div>
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
              background-color:#d3e4fc;">

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
       <!-- حمل به دپو -->
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

        <!-- واریزه/مسیر -->
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
      <span id="spanKhakBardariItems${i + 1}" class="spanStyleKhakBardariItems">
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

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled id="txtVarizi${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled id="txtDarsadVarizi${i + 1}" value=""  />
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


      </div>
    </div>
  </div>
</div>`;
            //////////////
            $('#ViewKhakBardariNew').html(strSSKB);
            //////////////////////////
            /////////////////

            // Enter در حجم خاکبرداری
            $('#ViewKhakBardariNew').on('keydown.khakFocus',
                'input[id^="txtKhDetail"]', function (e) {

                    if (e.key !== 'Enter') return;

                    let i = parseInt(this.id.replace('txtKhDetail', ''));
                    let value = $.trim($(this).val());

                    if (value === '' || parseFloat(value) === 0) {

                        e.preventDefault();
                        e.stopPropagation();
                        e.stopImmediatePropagation();

                        $('#txtDarsad' + i).focus();

                        return false;
                    }
                });


            // Enter در درصد اصلی خاکبرداری
            $('#ViewKhakBardariNew').on('keydown.khakFocus',
                'input[id^="txtDarsad"]', function (e) {

                    if (e.key !== 'Enter') return;

                    // فقط درصد اصلی، نه درصد حمل و واریزه
                    if (!/^txtDarsad\d+$/.test(this.id)) return;

                    let i = parseInt(this.id.replace('txtDarsad', ''));
                    let value = $.trim($(this).val());

                    if (value === '' || parseFloat(value) === 0) {

                        e.preventDefault();
                        e.stopPropagation();
                        e.stopImmediatePropagation();

                        let next = $('#txtKhDetail' + (i + 1));

                        if (next.length) {
                            next.focus();
                        }

                        return false;
                    }
                });


            ////////////////////////
            /////////////

            $('#ViewKhakBardariNew input[type="text"]').change(function () {

                debugger;

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

                //if (khDetail != null || khDetail != 0) {
                if (khDetail > 0) {
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
                    // dVarizi === 0 ? 0 : ((dVarizi / 100) * khDetail).toFixed(2);
                    // $("#txtVarizi" + i).val(dVarizi);
                    // $("#txtReUseHajm" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    // $("#txtHaml" + i).val(((dHaml / 100) * khDetail).toFixed(2));


                    // حجم هر بخش بر اساس درصد
                    let vVarizi = (dVarizi / 100) * khDetail;
                    let vReUse = (dReuse / 100) * khDetail;
                    let vHaml = (dHaml / 100) * khDetail;

                    $("#txtVarizi" + i).val(vVarizi.toFixed(2));
                    $("#txtReUseHajm" + i).val(vReUse.toFixed(2));
                    $("#txtHaml" + i).val(vHaml.toFixed(2));
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
                        $("#txtReUseDarsad" + i).val(dReuse.toFixed(0));
                        $("#txtReUseDarsad" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (khDetail === 0 ? 0 : (haml / khDetail)) * 100;
                        $("#txtDarsadHaml" + i).val(dHaml.toFixed(0));
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

               

                AutoFillThirdColumn(i, khDetail, changedId);

                SumKol = sumKolNew(ActivityLength,0);
                $('#spantotalKhDetailNew').html(toPersianDigits(SumKol[0].toFixed(2)));
                $('#spantotalDarsadKhDetailNew').html(toPersianDigits(SumKol[1].toFixed(0)) + " % ");

                $('#spantotalVariziNew').html(toPersianDigits(SumKol[2].toFixed(2)));

                DarsadVariziNew = (parseFloat(SumKol[2]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadVariziNew').html(toPersianDigits(DarsadVariziNew.toFixed(2)) + " % ");

                $('#spantotalReUseHajmNew').html(toPersianDigits(SumKol[3].toFixed(2)));
                DarsadReUseHajmNew = (parseFloat(SumKol[3]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadReUseHajmNew').html(toPersianDigits(DarsadReUseHajmNew.toFixed(2)) + " % ");

                $('#spantotalHamlNew').html(toPersianDigits(SumKol[4].toFixed(2)));
                DarsadHamlNew = (parseFloat(SumKol[4]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadHamlNew').html(toPersianDigits(DarsadHamlNew.toFixed(2)) + " % ");
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
                        toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
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
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
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

                debugger;
                $(this).closest('ul').find('.StyleMainViewKhakBardari').each(function () {
                    debugger;
                    $(this).slideUp(500);
                });

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
               


                var KMS = parseFloat(KM);//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForKhakbardari').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
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
               


                var KME = parseFloat(KM);//.replace('+', ''));
                var KMS = parseFloat($('#txtFromKMForKhakbardari').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
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
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;" ><span>از متراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>تا متراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>حجم خاکبرداری</span></div>
                        <div class="col-3" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>عملیات</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"></div>
                </div>

            `;

                lastKM = 0;
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

                    lastKM = KMNum;

                    strSEKB += `
    <div id="div${KMNum}" class="row col-12 ExistKhBStyle" style="border:1px solid #a99dbd;background-color:#f5daf5">
    <div id="divExistKMHeader${KMNum}" class="row col-12 ExistKMHeaderStyle">
    <div class="col-md-1 label-col" style="text-align:center">
      <span>${KMNum}</span>
    </div>
    <!-- از متراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForKhakbardari${KMNum}" value="${FromKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- تا متراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForKhakbardari${KMNum}" value="${ToKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- حجم خاکبرداری -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtHajmKhakBardari${KMNum}" value="${Value}" onclick="event.stopPropagation();"/>
    </div>

        <div class="col-md-3" style="text-align:center">

            <button class="ButtonRowsViewStyle" title="مشاهده" onclick="ViewKhakBardariInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}')">
                <i class="fa fa-eye ViewRMUStyle"></i>
            </button>
        
            <button type="button" title="ویرایش"
                onclick="UpdateKhakBardariInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum})" onclick="event.stopPropagation();"
                class="ButtonRowsSaveStyle">
                    <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
            </button>

            <button class="ButtonRowsViewStyle" title="حذف" onclick="DeleteKhakBardariInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}')">
                <i class="fa fa-trash DelRMUStyle"></i>
            </button>
        
        </div>
   
    </div>
  <!-- بخش نمایش -->

  <div class="row col-12 StyleMainViewKhakBardari" style="direction:ltr;display:none"  id="MainViewKhakBardari${KMNum}" class="khakbardari-view">
    <div id="ViewKhakBardari${KMNum}" class="khakbardari-view" style="direction: rtl;">
    </div>
    <div class="col-md-1 action-col">
        
    </div>
  <div id="ViewRizMetreKH${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  <div id="ViewKhakBardariEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  </div><!-- MainViewKhakBardari -->
  </div>
    `;

                    //    str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionKMAmalyateKhaki($(this),\'' + KMExistingId + '\',\'' + KMNum
                    //        + '\')\" ondblclick=\"ShowSelctionKhakBardari(0,' + KMExistingId + ',' + KMNum + ',' + BarAvordUserId + ',' + "'" + FromKM + "'" + ',' + "'" + ToKM + "'" + ',' + "'" + FromKMSplit + "'" + ',' + "'" + ToKMSplit + "'" + ',' + Value + ')\">' + count++
                    //        + ' - متراژ' + '<label>' + FromKMSplit + ' - ' + ToKMSplit + '</label>' + '</a></div>';
                });

                $('#divExistingKM').html(strSEKB);

                $('#divExistKMHeader' + lastKM).click();

                //$('#divExistingKM').find('#MainViewKhakBardari' + KMNum).hide();
            }
            else
                $('#divExistingKM').html('');

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

    const $v = $("#txtVarizi" + KMNum);
    const $dv = $("#txtDarsadVarizi" + KMNum);

    const $r = $("#txtReUseHajm" + KMNum);
    const $dr = $("#txtReUseDarsad" + KMNum);

    const $h = $("#txtHaml" + KMNum);
    const $dh = $("#txtDarsadHaml" + KMNum);

    // -----------------------------
    // Helper functions
    // -----------------------------

    const toNumber = function (value) {
        if (value === null || value === undefined || value === "") {
            return 0;
        }

        const number = parseFloat(value);

        return isNaN(number) ? 0 : number;
    };

    const hasValue = function ($input) {
        return $.trim($input.val()) !== "";
    };

    const clamp = function (value, min, max) {
        return Math.max(min, Math.min(max, value));
    };

    const setValue = function ($input, value) {
        $input.val(parseFloat(Number(value).toFixed(2)));
        $input.removeClass("blinking");
    };


    // -----------------------------
    // KhDetail
    // -----------------------------

    khDetail = toNumber(khDetail);

    if (khDetail <= 0) {
        return;
    }


    // =========================================================
    // ردیف 1 و 2
    // فقط:
    // Varizi + Haml = KhDetail
    // Varizi% + Haml% = 100
    //
    // ReUse کاملاً از محاسبات خارج است
    // =========================================================

    if (KMNum === "1" || KMNum === "2") {

        // ReUse در ردیف 1 و 2 اصلاً در محاسبات نیست
        setValue($r, 0);
        setValue($dr, 0);

        // تغییر درصد واریزی
        if (changedId && changedId.includes("DarsadVarizi")) {

            let variziPercent = clamp(
                toNumber($dv.val()),
                0,
                100
            );

            let hamlPercent = 100 - variziPercent;

            let variziVolume =
                (variziPercent / 100) * khDetail;

            let hamlVolume =
                (hamlPercent / 100) * khDetail;

            setValue($dv, variziPercent);
            setValue($dh, hamlPercent);

            setValue($v, variziVolume);
            setValue($h, hamlVolume);

            return;
        }

        // تغییر درصد حمل
        if (changedId && changedId.includes("DarsadHaml")) {

            let hamlPercent = clamp(
                toNumber($dh.val()),
                0,
                100
            );

            let variziPercent = 100 - hamlPercent;

            let hamlVolume =
                (hamlPercent / 100) * khDetail;

            let variziVolume =
                (variziPercent / 100) * khDetail;

            setValue($dh, hamlPercent);
            setValue($dv, variziPercent);

            setValue($h, hamlVolume);
            setValue($v, variziVolume);

            return;
        }

        // تغییر مقدار حجمی واریزی
        if (changedId &&
            changedId.includes("Varizi") &&
            !changedId.includes("Darsad")) {

            let variziVolume = clamp(
                toNumber($v.val()),
                0,
                khDetail
            );

            let hamlVolume =
                khDetail - variziVolume;

            let variziPercent =
                (variziVolume / khDetail) * 100;

            let hamlPercent =
                (hamlVolume / khDetail) * 100;

            setValue($v, variziVolume);
            setValue($h, hamlVolume);

            setValue($dv, variziPercent);
            setValue($dh, hamlPercent);

            return;
        }

        // تغییر مقدار حجمی حمل
        if (changedId &&
            changedId.includes("Haml") &&
            !changedId.includes("Darsad")) {

            let hamlVolume = clamp(
                toNumber($h.val()),
                0,
                khDetail
            );

            let variziVolume =
                khDetail - hamlVolume;

            let hamlPercent =
                (hamlVolume / khDetail) * 100;

            let variziPercent =
                (variziVolume / khDetail) * 100;

            setValue($h, hamlVolume);
            setValue($v, variziVolume);

            setValue($dh, hamlPercent);
            setValue($dv, variziPercent);

            return;
        }

        // حالت عادی
        let hamlVolume = clamp(
            toNumber($h.val()),
            0,
            khDetail
        );

        let variziVolume =
            khDetail - hamlVolume;

        let hamlPercent =
            (hamlVolume / khDetail) * 100;

        let variziPercent =
            (variziVolume / khDetail) * 100;

        setValue($v, variziVolume);
        setValue($h, hamlVolume);

        setValue($dv, variziPercent);
        setValue($dh, hamlPercent);

        return;
    }


    // =========================================================
    // سایر ردیف‌ها
    //
    // ReUse و Haml مستقل هستند
    //
    // Varizi = KhDetail - ReUse - Haml
    //
    // درصدها:
    //
    // DarsadVarizi = 100 - DarsadReUse - DarsadHaml
    // =========================================================


    // =========================================================
    // 1. تغییر حجم ReUse
    // =========================================================

    if (changedId && changedId.includes("ReUseHajm")) {

        let reuse = clamp(
            toNumber($r.val()),
            0,
            khDetail
        );

        let haml = clamp(
            toNumber($h.val()),
            0,
            khDetail - reuse
        );

        let varizi = khDetail - reuse - haml;

        if (varizi < 0) {
            varizi = 0;
        }


        // فقط Varizi محاسبه می‌شود
        // ReUse و Haml دست نمی‌خورند

        setValue($r, reuse);
        setValue($h, haml);
        setValue($v, varizi);


        // درصدها

        let reusePercent = (reuse / khDetail) * 100;
        let hamlPercent = (haml / khDetail) * 100;
        let variziPercent = (varizi / khDetail) * 100;

        setValue($dr, reusePercent);
        setValue($dh, hamlPercent);
        setValue($dv, variziPercent);

        return;
    }


    // =========================================================
    // 2. تغییر حجم Haml
    // =========================================================

    if (changedId && changedId.includes("Haml")) {

        let reuse = clamp(
            toNumber($r.val()),
            0,
            khDetail
        );

        // این قسمت مهم است:
        // Haml فقط نسبت به ReUse محدود می‌شود
        // نه نسبت به Varizi قبلی
        let haml = clamp(
            toNumber($h.val()),
            0,
            khDetail - reuse
        );

        let varizi = khDetail - reuse - haml;

        if (varizi < 0) {
            varizi = 0;
        }


        // ReUse اصلاً تغییر نمی‌کند
        setValue($r, reuse);

        // Haml همان مقداری که کاربر وارد کرده
        setValue($h, haml);

        // Varizi باقیمانده است
        setValue($v, varizi);


        // درصدها

        let reusePercent = (reuse / khDetail) * 100;
        let hamlPercent = (haml / khDetail) * 100;
        let variziPercent = (varizi / khDetail) * 100;

        setValue($dr, reusePercent);
        setValue($dh, hamlPercent);
        setValue($dv, variziPercent);

        return;
    }


    // =========================================================
    // 3. تغییر درصد ReUse
    // =========================================================

    if (changedId && changedId.includes("ReUseDarsad")) {

        let reusePercent = clamp(
            toNumber($dr.val()),
            0,
            100
        );

        let hamlPercent = clamp(
            toNumber($dh.val()),
            0,
            100 - reusePercent
        );

        let variziPercent =
            100 - reusePercent - hamlPercent;


        if (variziPercent < 0) {
            variziPercent = 0;
        }


        // تبدیل درصد به حجم

        let reuse = (reusePercent / 100) * khDetail;
        let haml = (hamlPercent / 100) * khDetail;
        let varizi = (variziPercent / 100) * khDetail;


        setValue($r, reuse);
        setValue($h, haml);
        setValue($v, varizi);


        setValue($dr, reusePercent);
        setValue($dh, hamlPercent);
        setValue($dv, variziPercent);

        return;
    }


    // =========================================================
    // 4. تغییر درصد Haml
    // =========================================================

    if (changedId && changedId.includes("DarsadHaml")) {

        let reusePercent = clamp(
            toNumber($dr.val()),
            0,
            100
        );

        // Haml مستقل است.
        // فقط نباید مجموع ReUse + Haml بیشتر از 100 شود.
        let hamlPercent = clamp(
            toNumber($dh.val()),
            0,
            100 - reusePercent
        );

        let variziPercent =
            100 - reusePercent - hamlPercent;


        if (variziPercent < 0) {
            variziPercent = 0;
        }


        // تبدیل درصد به حجم

        let reuse = (reusePercent / 100) * khDetail;
        let haml = (hamlPercent / 100) * khDetail;
        let varizi = (variziPercent / 100) * khDetail;


        // ReUse تغییر نمی‌کند
        setValue($dr, reusePercent);
        setValue($r, reuse);


        // Haml همان مقدار وارد شده
        setValue($dh, hamlPercent);
        setValue($h, haml);


        // فقط Varizi باقیمانده است
        setValue($dv, variziPercent);
        setValue($v, varizi);

        return;
    }


    // =========================================================
    // 5. اگر تابع بدون changedId اجرا شد
    // مقدار Varizi از ReUse و Haml محاسبه می‌شود
    // =========================================================

    let reuse = clamp(
        toNumber($r.val()),
        0,
        khDetail
    );

    let haml = clamp(
        toNumber($h.val()),
        0,
        khDetail - reuse
    );

    let varizi = khDetail - reuse - haml;

    if (varizi < 0) {
        varizi = 0;
    }


    // درصدها

    let reusePercent = (reuse / khDetail) * 100;
    let hamlPercent = (haml / khDetail) * 100;
    let variziPercent = (varizi / khDetail) * 100;


    // خروجی نهایی

    setValue($r, reuse);
    setValue($h, haml);
    setValue($v, varizi);

    setValue($dr, reusePercent);
    setValue($dh, hamlPercent);
    setValue($dv, variziPercent);
}


function AutoFillThirdColumnForEdit(KMNum, i, khDetail, changedId) {

    const $v = $("#txtVarizi" + KMNum + "_" + i);
    const $dv = $("#txtDarsadVarizi" + KMNum + "_" + i);

    const $r = $("#txtReUseHajm" + KMNum + "_" + i);
    const $dr = $("#txtReUseDarsad" + KMNum + "_" + i);

    const $h = $("#txtHaml" + KMNum + "_" + i);
    const $dh = $("#txtDarsadHaml" + KMNum + "_" + i);

    const hasVal = ($input) => {
        return $.trim($input.val()) !== "";
    };

    const clamp = (x, min, max) => {
        return Math.max(min, Math.min(max, x));
    };

    const n = (x) => {
        const value = parseFloat(x);
        return isNaN(value) ? 0 : value;
    };

    const setValue = ($input, value) => {
        $input.val(parseFloat(Number(value).toFixed(2)));
        $input.removeClass("blinking");
    };

    khDetail = n(khDetail);

    if (khDetail <= 0) {
        return;
    }


    // =========================================================
    // ردیف های 1 و 2
    // ReUse کاملاً از محاسبات حذف می شود
    //
    // Varizi + Haml = KhDetail
    // Varizi% + Haml% = 100
    // =========================================================

    const isNoReuseRow = (i === "1" || i === "2");

    if (isNoReuseRow) {

        // ReUse همیشه صفر
        setValue($r, 0);
        setValue($dr, 0);


        // -----------------------------------------------------
        // تغییر درصد واریزی
        // -----------------------------------------------------

        if (changedId &&
            changedId.includes("DarsadVarizi" + KMNum)) {

            let variziPercent = clamp(
                n($dv.val()),
                0,
                100
            );

            let hamlPercent = 100 - variziPercent;

            let variziVolume =
                (variziPercent / 100) * khDetail;

            let hamlVolume =
                (hamlPercent / 100) * khDetail;

            setValue($dv, variziPercent);
            setValue($dh, hamlPercent);

            setValue($v, variziVolume);
            setValue($h, hamlVolume);

            return;
        }


        // -----------------------------------------------------
        // تغییر درصد حمل
        // -----------------------------------------------------

        if (changedId &&
            changedId.includes("DarsadHaml" + KMNum)) {

            let hamlPercent = clamp(
                n($dh.val()),
                0,
                100
            );

            let variziPercent = 100 - hamlPercent;

            let hamlVolume =
                (hamlPercent / 100) * khDetail;

            let variziVolume =
                (variziPercent / 100) * khDetail;

            setValue($dh, hamlPercent);
            setValue($dv, variziPercent);

            setValue($h, hamlVolume);
            setValue($v, variziVolume);

            return;
        }


        // -----------------------------------------------------
        // تغییر حجم واریزی
        // -----------------------------------------------------

        if (changedId &&
            changedId.includes("Varizi" + KMNum) &&
            !changedId.includes("Darsad")) {

            let variziVolume = clamp(
                n($v.val()),
                0,
                khDetail
            );

            let hamlVolume =
                khDetail - variziVolume;

            let variziPercent =
                (variziVolume / khDetail) * 100;

            let hamlPercent =
                (hamlVolume / khDetail) * 100;

            setValue($v, variziVolume);
            setValue($h, hamlVolume);

            setValue($dv, variziPercent);
            setValue($dh, hamlPercent);

            return;
        }


        // -----------------------------------------------------
        // تغییر حجم حمل
        // -----------------------------------------------------

        if (changedId &&
            changedId.includes("Haml" + KMNum) &&
            !changedId.includes("Darsad")) {

            let hamlVolume = clamp(
                n($h.val()),
                0,
                khDetail
            );

            let variziVolume =
                khDetail - hamlVolume;

            let hamlPercent =
                (hamlVolume / khDetail) * 100;

            let variziPercent =
                (variziVolume / khDetail) * 100;

            setValue($h, hamlVolume);
            setValue($v, variziVolume);

            setValue($dh, hamlPercent);
            setValue($dv, variziPercent);

            return;
        }


        // -----------------------------------------------------
        // حالت اولیه / بدون تغییر مشخص
        // -----------------------------------------------------

        let variziVolume = clamp(
            n($v.val()),
            0,
            khDetail
        );

        let hamlVolume = khDetail - variziVolume;

        let variziPercent =
            (variziVolume / khDetail) * 100;

        let hamlPercent =
            (hamlVolume / khDetail) * 100;

        setValue($v, variziVolume);
        setValue($h, hamlVolume);

        setValue($dv, variziPercent);
        setValue($dh, hamlPercent);

        return;
    }


    // =========================================================
    // سایر ردیف ها
    // Varizi + ReUse + Haml = KhDetail
    // =========================================================


    // ---------------------------------------------------------
    // اگر درصد وارد شده، حجم متناظر را محاسبه کن
    // ---------------------------------------------------------

    if (changedId &&
        changedId.includes("DarsadVarizi" + KMNum)) {

        let dv = clamp(n($dv.val()), 0, 100);

        setValue(
            $v,
            (dv / 100) * khDetail
        );
    }

    if (changedId &&
        changedId.includes("ReUseDarsad" + KMNum)) {

        let dr = clamp(n($dr.val()), 0, 100);

        setValue(
            $r,
            (dr / 100) * khDetail
        );
    }

    if (changedId &&
        changedId.includes("DarsadHaml" + KMNum)) {

        let dh = clamp(n($dh.val()), 0, 100);

        setValue(
            $h,
            (dh / 100) * khDetail
        );
    }


    // =========================================================
    // سایر ردیف ها - ردیف 3 به بعد
    // =========================================================


    // ---------------------------------------------------------
    // تغییر حجم ReUse
    // Haml ثابت می ماند
    // Varizi = KhDetail - ReUse - Haml
    // ---------------------------------------------------------

    if (changedId &&
        changedId.includes("ReUseHajm" + KMNum) &&
        !changedId.includes("Darsad")) {

        let haml = clamp(
            n($h.val()),
            0,
            khDetail
        );

        let reuse = clamp(
            n($r.val()),
            0,
            khDetail - haml
        );

        let varizi = khDetail - reuse - haml;

        setValue($r, reuse);
        setValue($h, haml);
        setValue($v, varizi);

        setValue($dr, (reuse / khDetail) * 100);
        setValue($dh, (haml / khDetail) * 100);
        setValue($dv, (varizi / khDetail) * 100);

        return;
    }


    // ---------------------------------------------------------
    // تغییر حجم Haml
    // ReUse ثابت می ماند
    // Varizi = KhDetail - ReUse - Haml
    // ---------------------------------------------------------

    if (changedId &&
        changedId.includes("Haml" + KMNum) &&
        !changedId.includes("Darsad")) {

        let reuse = clamp(
            n($r.val()),
            0,
            khDetail
        );

        let haml = clamp(
            n($h.val()),
            0,
            khDetail - reuse
        );

        let varizi = khDetail - reuse - haml;

        setValue($r, reuse);
        setValue($h, haml);
        setValue($v, varizi);

        setValue($dr, (reuse / khDetail) * 100);
        setValue($dh, (haml / khDetail) * 100);
        setValue($dv, (varizi / khDetail) * 100);

        return;
    }


    // ---------------------------------------------------------
    // تغییر درصد ReUse
    // Haml ثابت می ماند
    // Varizi = باقی مانده
    // ---------------------------------------------------------

    if (changedId &&
        changedId.includes("ReUseDarsad" + KMNum)) {

        let haml = clamp(
            n($dh.val()),
            0,
            100
        );

        let reusePercent = clamp(
            n($dr.val()),
            0,
            100 - haml
        );

        let hamlVolume =
            (haml / 100) * khDetail;

        let reuseVolume =
            (reusePercent / 100) * khDetail;

        let variziVolume =
            khDetail - reuseVolume - hamlVolume;

        let variziPercent =
            (variziVolume / khDetail) * 100;

        setValue($dr, reusePercent);
        setValue($dh, haml);

        setValue($r, reuseVolume);
        setValue($h, hamlVolume);

        setValue($v, variziVolume);
        setValue($dv, variziPercent);

        return;
    }


    // ---------------------------------------------------------
    // تغییر درصد Haml
    // ReUse ثابت می ماند
    // Varizi = باقی مانده
    // ---------------------------------------------------------

    if (changedId &&
        changedId.includes("DarsadHaml" + KMNum)) {

        let reusePercent = clamp(
            n($dr.val()),
            0,
            100
        );

        let hamlPercent = clamp(
            n($dh.val()),
            0,
            100 - reusePercent
        );

        let reuseVolume =
            (reusePercent / 100) * khDetail;

        let hamlVolume =
            (hamlPercent / 100) * khDetail;

        let variziVolume =
            khDetail - reuseVolume - hamlVolume;

        let variziPercent =
            (variziVolume / khDetail) * 100;

        setValue($dr, reusePercent);
        setValue($dh, hamlPercent);

        setValue($r, reuseVolume);
        setValue($h, hamlVolume);

        setValue($v, variziVolume);
        setValue($dv, variziPercent);

        return;
    }

    // ---------------------------------------------------------
    // حالت پیش فرض
    // ---------------------------------------------------------

    let reuse = clamp(
        n($r.val()),
        0,
        khDetail
    );

    let haml = clamp(
        n($h.val()),
        0,
        khDetail - reuse
    );

    let varizi =
        khDetail - reuse - haml;

    setValue($r, reuse);
    setValue($h, haml);
    setValue($v, varizi);

    setValue(
        $dr,
        (reuse / khDetail) * 100
    );

    setValue(
        $dh,
        (haml / khDetail) * 100
    );

    setValue(
        $dv,
        (varizi / khDetail) * 100
    );
}

// function AutoFillThirdColumn(KMNum, khDetail, changedId) {
//     debugger;
//     // برای تشخیص "پر شده" بودن (خالی نبودن)
//     const hasVal = (selector) => $.trim($(selector).val()) !== "";

//     // selectors
//     const $v = $("#txtVarizi" + KMNum);
//     const $dv = $("#txtDarsadVarizi" + KMNum);

//     const $r = $("#txtReUseHajm" + KMNum);
//     const $dr = $("#txtReUseDarsad" + KMNum);

//     const $h = $("#txtHaml" + KMNum);
//     const $dh = $("#txtDarsadHaml" + KMNum);

//     // helpers
//     const clamp = (x, min, max) => Math.max(min, Math.min(max, x));
//     const n = (x) => (parseFloat(x) || 0);

//     // اگه khDetail معتبر نیست کاری نکن
//     khDetail = n(khDetail);
//     if (khDetail <= 0) return;

//     // ردیف های 1 و 2 => ReUse همیشه صفر، و Varizi/Haml مکمل هم تا 100%
//     const isNoReuseRow = (KMNum === "1" || KMNum === "2");
//     if (isNoReuseRow) {
//         $r.val("0.00");
//         $dr.val("0.00");

//         // اگر درصد یکی پر باشد، دیگری = 100 - آن
//         const variziPercentFilled = hasVal("#txtDarsadVarizi" + KMNum);
//         const hamlPercentFilled = hasVal("#txtDarsadHaml" + KMNum);

//         // اگر حجم یکی پر باشد، درصدش را بساز
//         const variziVolFilled = hasVal("#txtVarizi" + KMNum);
//         const hamlVolFilled = hasVal("#txtHaml" + KMNum);

//         // اول سعی کن درصدها را کامل کنی
//         if (variziPercentFilled && !hamlPercentFilled) {
//             let dv = clamp(n($dv.val()), 0, 100);
//             let dh = clamp(100 - dv, 0, 100);
//             $dh.val(dh.toFixed(2));
//             $dh.removeClass('blinking');

//             $v.val(dv === 0 ? '' : ((dv / 100) * khDetail).toFixed(2));
//             $v.removeClass('blinking');

//             $h.val(dh === 0 ? '' : ((dh / 100) * khDetail).toFixed(2));
//             $h.removeClass('blinking');

//             return;
//         }
//         if (hamlPercentFilled && !variziPercentFilled) {
//             let dh = clamp(n($dh.val()), 0, 100);
//             let dv = clamp(100 - dh, 0, 100);
//             $dv.val(dv.toFixed(2));
//             $dv.removeClass('blinking');

//             $h.val(dh === 0 ? '' : ((dh / 100) * khDetail).toFixed(2));
//             $h.removeClass('blinking');

//             $v.val(dv === 0 ? '' : ((dv / 100) * khDetail).toFixed(2));
//             $v.removeClass('blinking');

//             return;
//         }

//         // اگر درصدها پر نیستند، با حجم‌ها برو جلو: یکی پر شد -> دیگری = باقی‌مانده تا khDetail
//         if (variziVolFilled && !hamlVolFilled) {
//             let vv = clamp(n($v.val()), 0, khDetail);
//             let vh = clamp(khDetail - vv, 0, khDetail);
//             $h.val(vh.toFixed(2));
//             $h.removeClass('blinking');

//             $dv.val(dv === 0 ? '' : (khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
//             $dv.removeClass('blinking');

//             $dh.val(dh === 0 ? '' : (khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
//             $dh.removeClass('blinking');

//             return;
//         }
//         if (hamlVolFilled && !variziVolFilled) {
//             let vh = clamp(n($h.val()), 0, khDetail);
//             let vv = clamp(khDetail - vh, 0, khDetail);
//             $v.val(vv.toFixed(2));
//             $v.removeClass('blinking');

//             $dh.val(dh === 0 ? '' : (khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
//             $dh.removeClass('blinking');

//             $dv.val(dv === 0 ? '' : (khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
//             $dv.removeClass('blinking');

//             return;
//         }

//         // اگر هر دو پر هستند، فقط اصلاح clamp
//         let vv = clamp(n($v.val()), 0, khDetail);
//         let vh = clamp(n($h.val()), 0, khDetail);
//         // اگر جمع بیشتر شد، همان چیزی که تغییر کرده را نگه دار و دیگری را کم کن
//         if (vv + vh > khDetail) {
//             if (changedId.includes("Varizi")) vh = clamp(khDetail - vv, 0, khDetail);
//             else vv = clamp(khDetail - vh, 0, khDetail);
//             $v.val(vv.toFixed(2));
//             $v.removeClass('blinking');

//             $h.val(vh.toFixed(2));
//             $h.removeClass('blinking');

//         }
//         $dv.val(vv === 0 ? '' : (vv / khDetail * 100).toFixed(2));
//         $dv.removeClass('blinking');

//         $dh.val(vh === 0 ? '' : (vh / khDetail * 100).toFixed(2));
//         $dh.removeClass('blinking');

//         return;
//     }

//     // سایر ردیف ها: سه ستون فعال (Varizi / ReUse / Haml)

//     // اگر درصدی وارد شده، حجمش را بساز
//     // (فقط برای فیلدی که خودش درصدش پر است)
//     if (hasVal("#txtDarsadVarizi" + KMNum)) $v.val(((clamp(n($dv.val()), 0, 100) / 100) * khDetail).toFixed(2));
//     if (hasVal("#txtReUseDarsad" + KMNum)) $r.val(((clamp(n($dr.val()), 0, 100) / 100) * khDetail).toFixed(2));
//     if (hasVal("#txtDarsadHaml" + KMNum)) $h.val(((clamp(n($dh.val()), 0, 100) / 100) * khDetail).toFixed(2));

//     // تشخیص اینکه کدام درصدها "پر شده‌اند"
//     const filledP = {
//         v: hasVal("#txtDarsadVarizi" + KMNum),
//         r: hasVal("#txtReUseDarsad" + KMNum),
//         h: hasVal("#txtDarsadHaml" + KMNum),
//     };

//     // تشخیص اینکه کدام حجم‌ها "پر شده‌اند"
//     const filledV = {
//         v: hasVal("#txtVarizi" + KMNum),
//         r: hasVal("#txtReUseHajm" + KMNum),
//         h: hasVal("#txtHaml" + KMNum),
//     };

//     // اگر دو تا درصد پر بود => سومی = 100 - جمع
//     // const countP = (filledP.v ? 1 : 0) + (filledP.r ? 1 : 0) + (filledP.h ? 1 : 0);
//     // if (countP >= 2) {
//     //     let dv = clamp(n($dv.val()), 0, 100);
//     //     let dr = clamp(n($dr.val()), 0, 100);
//     //     let dh = clamp(n($dh.val()), 0, 100);

//     //     let vv = clamp(n($v.val()), 0, khDetail);
//     //     let vr = clamp(n($r.val()), 0, khDetail);
//     //     let vh = clamp(n($h.val()), 0, khDetail);

//     //     if (!filledP.v) dv = clamp(100 - (dr + dh), 0, 100), $dv.val(dv.toFixed(2));
//     //     if (!filledP.r) dr = clamp(100 - (dv + dh), 0, 100), $dr.val(dr.toFixed(2));
//     //     if (!filledP.h) dh = clamp(100 - (dv + dr), 0, 100), $dh.val(dh.toFixed(2));

//     //     if (vv === 0) {
//     //         //$v.val(dv === 0 ? '' : ((dv / 100) * khDetail).toFixed(2));
//     //         $v.val(dv === 0 ? '' : (khDetail - (vr + vh)).toFixed(2));
//     //         $v.removeClass('blinking');
//     //     }

//     //     if (vr === 0) {
//     //         //$r.val(dr === 0 ? '' : ((dr / 100) * khDetail).toFixed(2));
//     //         $r.val(dr === 0 ? '' : (khDetail - (vv + vh)).toFixed(2));
//     //         $r.removeClass('blinking');
//     //     }

//     //     if (vh === 0) {
//     //         //$h.val(dh === 0 ? '' : ((dh / 100) * khDetail).toFixed(2));
//     //         $h.val(dh === 0 ? '' : (khDetail - (vv + vr)).toFixed(2));
//     //         $h.removeClass('blinking');
//     //     }
//     //     return;
//     // }


//     const countP =
//         (filledP.v ? 1 : 0) +
//         (filledP.r ? 1 : 0) +
//         (filledP.h ? 1 : 0);

//     if (countP >= 2) {

//         let dv = clamp(n($dv.val()), 0, 100);
//         let dr = clamp(n($dr.val()), 0, 100);
//         let dh = clamp(n($dh.val()), 0, 100);

//         let vv = clamp(n($v.val()), 0, khDetail);
//         let vr = clamp(n($r.val()), 0, khDetail);
//         let vh = clamp(n($h.val()), 0, khDetail);


//         // =========================
//         // درصدها
//         // V همیشه از R و H محاسبه شود
//         // =========================

//         if (!filledP.v) {
//             dv = clamp(100 - dr - dh, 0, 100);
//             $dv.val(dv.toFixed(2));
//         }

//         if (!filledP.r) {
//             dr = clamp(100 - dv - dh, 0, 100);
//             $dr.val(dr.toFixed(2));
//         }

//         if (!filledP.h) {
//             dh = clamp(100 - dv - dr, 0, 100);
//             $dh.val(dh.toFixed(2));
//         }


//         // =========================
//         // مقادیر
//         // V همیشه از R و H محاسبه شود
//         // =========================

//         vv = clamp(khDetail - (vr + vh), 0, khDetail);

//         $v.val(vv.toFixed(2));
//         $v.removeClass('blinking');


//         // اگر R خالی/صفر باشد
//         if (vr === 0 && dr !== 0) {
//             vr = clamp(khDetail - (vv + vh), 0, khDetail);
//             $r.val(vr.toFixed(2));
//             $r.removeClass('blinking');
//         }


//         // اگر H خالی/صفر باشد
//         if (vh === 0 && dh !== 0) {
//             vh = clamp(khDetail - (vv + vr), 0, khDetail);
//             $h.val(vh.toFixed(2));
//             $h.removeClass('blinking');
//         }

//         return;
//     }

//     // اگر دو تا حجم پر بود => سومی = khDetail - جمع
//     const countV = (filledV.v ? 1 : 0) + (filledV.r ? 1 : 0) + (filledV.h ? 1 : 0);
//     if (countV >= 2) {
//         let vv = clamp(n($v.val()), 0, khDetail);
//         let vr = clamp(n($r.val()), 0, khDetail);
//         let vh = clamp(n($h.val()), 0, khDetail);

//         if (!filledV.v) vv = clamp(khDetail - (vr + vh), 0, khDetail), $v.val(vv.toFixed(2));
//         if (!filledV.r) vr = clamp(khDetail - (vv + vh), 0, khDetail), $r.val(vr.toFixed(2));
//         if (!filledV.h) vh = clamp(khDetail - (vv + vr), 0, khDetail), $h.val(vh.toFixed(2));

//         $dv.val(vv === 0 ? '' : (vv / khDetail * 100).toFixed(2));
//         $dv.removeClass('blinking');

//         $dr.val(vr === 0 ? '' : (vr / khDetail * 100).toFixed(2));
//         $dr.removeClass('blinking');

//         $dh.val(vh === 0 ? '' : (vh / khDetail * 100).toFixed(2));
//         $dh.removeClass('blinking');

//         return;
//     }

//     // حالت‌های تک‌ورودی: فعلاً کاری نکن (تا وقتی دومی پر بشه)
// }

// function AutoFillThirdColumnForEdit(KMNum, i, khDetail, changedId) {
//     debugger;
//     // برای تشخیص "پر شده" بودن (خالی نبودن)
//     const hasVal = (selector) => $.trim($(selector).val()) !== "";

//     // selectors
//     const $v = $("#txtVarizi" + KMNum + "_" + i);
//     const $dv = $("#txtDarsadVarizi" + KMNum + "_" + i);

//     const $r = $("#txtReUseHajm" + KMNum + "_" + i);
//     const $dr = $("#txtReUseDarsad" + KMNum + "_" + i);

//     const $h = $("#txtHaml" + KMNum + "_" + i);
//     const $dh = $("#txtDarsadHaml" + KMNum + "_" + i);

//     // helpers
//     const clamp = (x, min, max) => Math.max(min, Math.min(max, x));
//     const n = (x) => (parseFloat(x) || 0);

//     // اگه khDetail معتبر نیست کاری نکن
//     khDetail = n(khDetail);
//     if (khDetail <= 0) return;

//     // ردیف های 1 و 2 => ReUse همیشه صفر، و Varizi/Haml مکمل هم تا 100%
//     const isNoReuseRow = (i === "1" || i === "2");
//     if (isNoReuseRow) {
//         $r.val("0.00");
//         $dr.val("0.00");

//         // اگر درصد یکی پر باشد، دیگری = 100 - آن
//         const variziPercentFilled = hasVal("#txtDarsadVarizi" + KMNum + "_" + i);
//         const hamlPercentFilled = hasVal("#txtDarsadHaml" + KMNum + "_" + i);

//         // اگر حجم یکی پر باشد، درصدش را بساز
//         const variziVolFilled = hasVal("#txtVarizi" + KMNum + "_" + i);
//         const hamlVolFilled = hasVal("#txtHaml" + KMNum + "_" + i);

//         // اول سعی کن درصدها را کامل کنی
//         if (variziPercentFilled && !hamlPercentFilled) {
//             let dv = clamp(n($dv.val()), 0, 100);
//             let dh = clamp(100 - dv, 0, 100);
//             $dh.val(dh.toFixed(2));
//             $dh.removeClass('blinking');

//             $v.val(((dv / 100) * khDetail).toFixed(2));
//             $v.removeClass('blinking');

//             $h.val(((dh / 100) * khDetail).toFixed(2));
//             $h.removeClass('blinking');

//             return;
//         }
//         if (hamlPercentFilled && !variziPercentFilled) {
//             let dh = clamp(n($dh.val()), 0, 100);
//             let dv = clamp(100 - dh, 0, 100);
//             $dv.val(dv.toFixed(2));
//             $dv.removeClass('blinking');

//             $h.val(((dh / 100) * khDetail).toFixed(2));
//             $h.removeClass('blinking');

//             $v.val(((dv / 100) * khDetail).toFixed(2));
//             $v.removeClass('blinking');

//             return;
//         }

//         // اگر درصدها پر نیستند، با حجم‌ها برو جلو: یکی پر شد -> دیگری = باقی‌مانده تا khDetail
//         if (variziVolFilled && !hamlVolFilled) {
//             let vv = clamp(n($v.val()), 0, khDetail);
//             let vh = clamp(khDetail - vv, 0, khDetail);
//             $h.val(vh.toFixed(2));
//             $h.removeClass('blinking');

//             $dv.val((khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
//             $dv.removeClass('blinking');

//             $dh.val((khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
//             $dh.removeClass('blinking');

//             return;
//         }
//         if (hamlVolFilled && !variziVolFilled) {
//             let vh = clamp(n($h.val()), 0, khDetail);
//             let vv = clamp(khDetail - vh, 0, khDetail);
//             $v.val(vv.toFixed(2));
//             $v.removeClass('blinking');

//             $dh.val((khDetail === 0 ? 0 : (vh / khDetail) * 100).toFixed(2));
//             $dh.removeClass('blinking');

//             $dv.val((khDetail === 0 ? 0 : (vv / khDetail) * 100).toFixed(2));
//             $dv.removeClass('blinking');

//             return;
//         }

//         // اگر هر دو پر هستند، فقط اصلاح clamp
//         let vv = clamp(n($v.val()), 0, khDetail);
//         let vh = clamp(n($h.val()), 0, khDetail);
//         // اگر جمع بیشتر شد، همان چیزی که تغییر کرده را نگه دار و دیگری را کم کن
//         if (vv + vh > khDetail) {
//             if (changedId.includes("Varizi")) vh = clamp(khDetail - vv, 0, khDetail);
//             else vv = clamp(khDetail - vh, 0, khDetail);
//             $v.val(vv.toFixed(2));
//             $v.removeClass('blinking');

//             $h.val(vh.toFixed(2));
//             $h.removeClass('blinking');

//         }
//         $dv.val((vv / khDetail * 100).toFixed(2));
//         $dv.removeClass('blinking');

//         $dh.val((vh / khDetail * 100).toFixed(2));
//         $dh.removeClass('blinking');

//         return;
//     }

//     // سایر ردیف ها: سه ستون فعال (Varizi / ReUse / Haml)

//     // اگر درصدی وارد شده، حجمش را بساز
//     // (فقط برای فیلدی که خودش درصدش پر است)
//     if (hasVal("#txtDarsadVarizi" + KMNum + "_" + i)) $v.val(((clamp(n($dv.val()), 0, 100) / 100) * khDetail).toFixed(2));
//     if (hasVal("#txtReUseDarsad" + KMNum + "_" + i)) $r.val(((clamp(n($dr.val()), 0, 100) / 100) * khDetail).toFixed(2));
//     if (hasVal("#txtDarsadHaml" + KMNum + "_" + i)) $h.val(((clamp(n($dh.val()), 0, 100) / 100) * khDetail).toFixed(2));

//     // تشخیص اینکه کدام درصدها "پر شده‌اند"
//     const filledP = {
//         v: hasVal("#txtDarsadVarizi" + KMNum + "_" + i),
//         r: hasVal("#txtReUseDarsad" + KMNum + "_" + i),
//         h: hasVal("#txtDarsadHaml" + KMNum + "_" + i),
//     };

//     // تشخیص اینکه کدام حجم‌ها "پر شده‌اند"
//     const filledV = {
//         v: hasVal("#txtVarizi" + KMNum + "_" + i),
//         r: hasVal("#txtReUseHajm" + KMNum + "_" + i),
//         h: hasVal("#txtHaml" + KMNum + "_" + i),
//     };

//     // اگر دو تا درصد پر بود => سومی = 100 - جمع
//     const countP = (filledP.v ? 1 : 0) + (filledP.r ? 1 : 0) + (filledP.h ? 1 : 0);
//     if (countP >= 2) {
//         let dv = clamp(n($dv.val()), 0, 100);
//         let dr = clamp(n($dr.val()), 0, 100);
//         let dh = clamp(n($dh.val()), 0, 100);

//         if (!filledP.v) dv = clamp(100 - (dr + dh), 0, 100), $dv.val(dv.toFixed(2));
//         if (!filledP.r) dr = clamp(100 - (dv + dh), 0, 100), $dr.val(dr.toFixed(2));
//         if (!filledP.h) dh = clamp(100 - (dv + dr), 0, 100), $dh.val(dh.toFixed(2));


//         let vv = clamp(n($v.val()), 0, khDetail);
//         let vr = clamp(n($r.val()), 0, khDetail);
//         let vh = clamp(n($h.val()), 0, khDetail);


//         if (vv === 0) {
//             //$v.val(dv === 0 ? '' : ((dv / 100) * khDetail).toFixed(2));
//             $v.val(dv === 0 ? '' : (khDetail - (vr + vh)).toFixed(2));
//             $v.removeClass('blinking');
//         }

//         if (vr === 0) {
//             //$r.val(dr === 0 ? '' : ((dr / 100) * khDetail).toFixed(2));
//             $r.val(dr === 0 ? '' : (khDetail - (vv + vh)).toFixed(2));
//             $r.removeClass('blinking');
//         }

//         if (vh === 0) {
//             //$h.val(dh === 0 ? '' : ((dh / 100) * khDetail).toFixed(2));
//             $h.val(dh === 0 ? '' : (khDetail - (vv + vr)).toFixed(2));
//             $h.removeClass('blinking');
//         }

//         // $v.val(((dv / 100) * khDetail).toFixed(2));
//         // $v.removeClass('blinking');
//         // $r.val(((dr / 100) * khDetail).toFixed(2));
//         // $r.removeClass('blinking');
//         // $h.val(((dh / 100) * khDetail).toFixed(2));
//         // $h.removeClass('blinking');
//         return;
//     }

//     // اگر دو تا حجم پر بود => سومی = khDetail - جمع
//     const countV = (filledV.v ? 1 : 0) + (filledV.r ? 1 : 0) + (filledV.h ? 1 : 0);
//     if (countV >= 2) {
//         let vv = clamp(n($v.val()), 0, khDetail);
//         let vr = clamp(n($r.val()), 0, khDetail);
//         let vh = clamp(n($h.val()), 0, khDetail);

//         if (!filledV.v) vv = clamp(khDetail - (vr + vh), 0, khDetail), $v.val(vv.toFixed(2));
//         if (!filledV.r) vr = clamp(khDetail - (vv + vh), 0, khDetail), $r.val(vr.toFixed(2));
//         if (!filledV.h) vh = clamp(khDetail - (vv + vr), 0, khDetail), $h.val(vh.toFixed(2));

//         $dv.val((vv / khDetail * 100).toFixed(2));
//         $dv.removeClass('blinking');

//         $dr.val((vr / khDetail * 100).toFixed(2));
//         $dr.removeClass('blinking');

//         $dh.val((vh / khDetail * 100).toFixed(2));
//         $dh.removeClass('blinking');

//         return;
//     }

//     // حالت‌های تک‌ورودی: فعلاً کاری نکن (تا وقتی دومی پر بشه)
// }

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
              background-color:#d3e4fc;">

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
      <span id="spanKhakBardariItems${KMNum}_${i}" class="spanStyleKhakBardariItems">
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
        </div>`;


               

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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled  id="txtVarizi${KMNum}_${i}" value="${Varizi == 0 ? '' : Varizi}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled id="txtDarsadVarizi${KMNum}_${i}" value="${DarsadVarizi == 0 ? '' : DarsadVarizi}"  />
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

       <!-- حمل به دپو -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalHaml${KMNum}">${fmt(totalHaml)}</span>
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


         <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalVarizi${KMNum}">${fmt(totalVarizi)}</span>
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
                khDetail = parseFloat($("#txtKhDetail" + KMNum + "_" + i).val()) || 0;

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
                    $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(0));

                    let SumAll = ReturnSumAllDetailsKhakBardariForEdit(KMNum);
                    if (SumAll > HajmKhakBardari) {
                        let NewVal = HajmKhakBardari - (SumAll - khDetail);
                        $("#txtKhDetail" + KMNum + "_" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmKhakBardari === 0 ? 0 : (khDetail / HajmKhakBardari) * 100;
                        $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(0));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVarizi" + KMNum + "_" + i).val(dVarizi > 0 ? ((dVarizi / 100) * khDetail).toFixed(2) : "");
                    $("#txtReUseHajm" + KMNum + "_" + i).val(dReuse > 0 ? ((dReuse / 100) * khDetail).toFixed(2) : "");
                    $("#txtHaml" + KMNum + "_" + i).val(dHaml > 0 ? ((dHaml / 100) * khDetail).toFixed(2) : "");
                }

                if (changedId.includes("txtDarsad" + KMNum)) {
                    debugger;

                    var Zarb = parseFloat($(this).val()) || 0;
                    // مجموع درصد همه ردیف‌ها منهای همین ردیف
                    let SumOther = ReturnSumAllDardadForEdit(KMNum) - Zarb;

                    // اگه بیشتر از 100 بشه، اصلاح کن
                    if (SumOther + Zarb > 100) {
                        Zarb = 100 - SumOther;
                        if (Zarb < 0) Zarb = 0; // امنیتی
                        $(this).val(Zarb.toFixed(2));
                    }

                    // حالا حجم این ردیف رو بر اساس درصد اصلاح‌شده محاسبه کن
                    let newKhDetail = (Zarb / 100) * HajmKhakBardari;
                    $('#txtKhDetail' + KMNum + "_" + i).val(newKhDetail.toFixed(2));


                    // // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVarizi" + KMNum + "_" + i).val(dVarizi > 0 ? ((dVarizi / 100) * newKhDetail).toFixed(2) : "");
                    $("#txtReUseHajm" + KMNum + "_" + i).val(dReuse > 0 ? ((dReuse / 100) * newKhDetail).toFixed(2) : "");
                    $("#txtHaml" + KMNum + "_" + i).val(dHaml > 0 ? ((dHaml / 100) * newKhDetail).toFixed(2) : "");

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
                     //let newKhDetail = (dVal / 100) * HajmKhakBardari;
                     //$('#txtKhDetail' + KMNum + "_" + i).val(newKhDetail.toFixed(2));
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
                        $("#txtDarsadVarizi" + KMNum + "_" + i).val(dVarizi.toFixed(0));
                        $("#txtDarsadVarizi" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        dReuse = khDetail === 0 ? 0 : (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsad" + KMNum + "_" + i).val(dReuse.toFixed(0));
                        $("#txtReUseDarsad" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dHaml = khDetail === 0 ? 0 : (haml / khDetail) * 100;
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(0));
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



                // =========================================================
                // کنترل مجموع برای ردیف های 3 به بعد
                // ردیف های 1 و 2 توسط AutoFillThirdColumnForEdit مدیریت می شوند
                // =========================================================

                // if (i !== "1" && i !== "2") {

                //     // دوباره مقادیر را بخوان
                //     varizi = parseFloat(
                //         $("#txtVarizi" + KMNum + "_" + i).val()
                //     ) || 0;

                //     reuseHajm = parseFloat(
                //         $("#txtReUseHajm" + KMNum + "_" + i).val()
                //     ) || 0;

                //     haml = parseFloat(
                //         $("#txtHaml" + KMNum + "_" + i).val()
                //     ) || 0;

                //     dVarizi = parseFloat(
                //         $("#txtDarsadVarizi" + KMNum + "_" + i).val()
                //     ) || 0;

                //     dReuse = parseFloat(
                //         $("#txtReUseDarsad" + KMNum + "_" + i).val()
                //     ) || 0;

                //     dHaml = parseFloat(
                //         $("#txtDarsadHaml" + KMNum + "_" + i).val()
                //     ) || 0;


                //     // مجموع درصدها
                //     let dSum = dVarizi + dReuse + dHaml;

                //     if (dSum > 100) {

                //         let extra = dSum - 100;

                //         if (changedId.includes("DarsadVarizi" + KMNum)) {

                //             dVarizi -= extra;

                //             dVarizi = Math.max(0, dVarizi);

                //             $("#txtDarsadVarizi" + KMNum + "_" + i)
                //                 .val(dVarizi.toFixed(2));

                //             $("#txtVarizi" + KMNum + "_" + i)
                //                 .val(((dVarizi / 100) * khDetail).toFixed(2));

                //         }
                //         else if (changedId.includes("ReUseDarsad" + KMNum)) {

                //             dReuse -= extra;

                //             dReuse = Math.max(0, dReuse);

                //             $("#txtReUseDarsad" + KMNum + "_" + i)
                //                 .val(dReuse.toFixed(2));

                //             $("#txtReUseHajm" + KMNum + "_" + i)
                //                 .val(((dReuse / 100) * khDetail).toFixed(2));
                //         }
                //         else if (changedId.includes("DarsadHaml" + KMNum)) {

                //             dHaml -= extra;

                //             dHaml = Math.max(0, dHaml);

                //             $("#txtDarsadHaml" + KMNum + "_" + i)
                //                 .val(dHaml.toFixed(2));

                //             $("#txtHaml" + KMNum + "_" + i)
                //                 .val(((dHaml / 100) * khDetail).toFixed(2));
                //         }
                //     }


                //     // مجموع حجم ها
                //     varizi = parseFloat(
                //         $("#txtVarizi" + KMNum + "_" + i).val()
                //     ) || 0;

                //     reuseHajm = parseFloat(
                //         $("#txtReUseHajm" + KMNum + "_" + i).val()
                //     ) || 0;

                //     haml = parseFloat(
                //         $("#txtHaml" + KMNum + "_" + i).val()
                //     ) || 0;

                //     let vSum = varizi + reuseHajm + haml;

                //     if (vSum > khDetail) {

                //         let extra = vSum - khDetail;

                //         if (changedId.includes("Varizi" + KMNum) &&
                //             !changedId.includes("Darsad")) {

                //             varizi = Math.max(0, varizi - extra);

                //             $("#txtVarizi" + KMNum + "_" + i)
                //                 .val(varizi.toFixed(2));

                //             $("#txtDarsadVarizi" + KMNum + "_" + i)
                //                 .val(
                //                     ((varizi / khDetail) * 100).toFixed(2)
                //                 );
                //         }
                //         else if (changedId.includes("ReUseHajm" + KMNum)) {

                //             reuseHajm = Math.max(0, reuseHajm - extra);

                //             $("#txtReUseHajm" + KMNum + "_" + i)
                //                 .val(reuseHajm.toFixed(2));

                //             $("#txtReUseDarsad" + KMNum + "_" + i)
                //                 .val(
                //                     ((reuseHajm / khDetail) * 100).toFixed(2)
                //                 );
                //         }
                //         else if (changedId.includes("Haml" + KMNum) &&
                //             !changedId.includes("Darsad")) {

                //             haml = Math.max(0, haml - extra);

                //             $("#txtHaml" + KMNum + "_" + i)
                //                 .val(haml.toFixed(2));

                //             $("#txtDarsadHaml" + KMNum + "_" + i)
                //                 .val(
                //                     ((haml / khDetail) * 100).toFixed(2)
                //                 );
                //         }
                //     }
                // }


                khDetail = parseFloat($("#txtKhDetail" + KMNum + "_" + i).val()) || 0;


                AutoFillThirdColumnForEdit(KMNum, i, khDetail, changedId);

                SumKol = sumKolNew(9, KMNum);
                debugger;
                $('#spantotalKhDetail' + KMNum).html(toPersianDigits(SumKol[0].toFixed(2)));
                $('#spantotalDarsadKhDetail'+KMNum).html(toPersianDigits(SumKol[1].toFixed(0)) + " % ");

                $('#spantotalVarizi' + KMNum).html(toPersianDigits(SumKol[2].toFixed(2)));

                DarsadVariziNew = (parseFloat(SumKol[2]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadVarizi' + KMNum).html(toPersianDigits(DarsadVariziNew.toFixed(2)) + " % ");

                $('#spantotalReUseHajm' + KMNum).html(toPersianDigits(SumKol[3].toFixed(2)));
                DarsadReUseHajmNew = (parseFloat(SumKol[3]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadReUseHajm' + KMNum).html(toPersianDigits(DarsadReUseHajmNew.toFixed(2)) + " % ");

                $('#spantotalHaml' + KMNum).html(toPersianDigits(SumKol[4].toFixed(2)));
                DarsadHamlNew = (parseFloat(SumKol[4]) / parseFloat(HajmKhakBardari)) * 100;
                $('#spantotalDarsadHaml' + KMNum).html(toPersianDigits(DarsadHamlNew.toFixed(2)) + " % ");

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

            $('#txtDarsad' + Type).val(parseFloat(Value) == 0 ? 0 : (parseFloat(CurrentValue) / parseFloat(Value) * 100).toFixed(0));
            $('#txtReUseDarsad' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtDarsadVarizi' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtDarsadHaml' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtDarsadFaseleHaml' + Type).val(parseFloat(ValueOfVarize) == 0 ? 0 : (parseFloat(ValueOfFaseleHaml) / parseFloat(ValueOfVarize) * 100).toFixed(0));

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
            toastr.error('مشکل در بارگذاری متراژ انتخابی', 'خطا');
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

                str += "<div class=\"row col-12 styleHeaderTable\" style=\"text-align: center;\">";
                str += "<div class=\"col-md-1 spanStyleMitraSmall\" style=\"text-align:center\"><span id=\"spanFieldShomarehName\">ردیف</span></div>";
                str += "<div class=\"col-md-2 spanStyleMitraSmall\">شرح</div>";

                str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">تعداد</div>";
                str += "<div class=\"VahedStyle\">" + (ItemFields[0] != undefined ? ItemFields[0].vahed : "") + "</div>";
                str += "</div>";

                str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">طول</div>";
                str += "<div class=\"VahedStyle\">" + (ItemFields[1] != undefined ? ItemFields[1].vahed : "") + "</div>";
                str += "</div>";

                str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">عرض</div>";
                str += "<div class=\"VahedStyle\">" + (ItemFields[2] != undefined ? ItemFields[2].vahed : "") + "</div>";
                str += "</div>";

                str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">ارتفاع</div>";
                str += "<div class=\"VahedStyle\">" + (ItemFields[3] != undefined ? ItemFields[3].vahed : "") + "</div>";
                str += "</div>";

                str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #fff\">وزن یا...</div>";
                str += "<div class=\"VahedStyle\">" + (ItemFields[4] != undefined ? ItemFields[4].vahed : "") + "</div>";
                str += "</div>";

                str += "<div class=\"col-md-1 spanStyleMitraSmall\">";
                str += "<div style=\"padding-bottom:3px;border-bottom:1px solid #fff\"><span>مقدار جزء</span></div>";
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
                    str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span  class=\"RMRowStyle\">" + row.shomareh + "</span></div>";

                    str += "<div class='col-md-2'><input  type='text'" +
                        " class='form-control spanStyleMitraSmall' id='txtSharh" + id + "' value='" + row.sharh + "' /></div > ";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[0] != undefined ? ItemFields[0].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[0] != undefined ? ItemFields[0].essentialValue === true ? " essentialValue " : "" : "") + "' id='txtTedad" + id + "' value = '" + strTedad + "' /></div > ";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[1] != undefined ? ItemFields[1].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[1] != undefined ? ItemFields[1].essentialValue === true ? " essentialValue " : "" : "") + "' id='txtTool" + id + "' value='" + strTool + "'/></div>";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[2] != undefined ? ItemFields[2].isEnteringValue !== true ? " disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[2] != undefined ? ItemFields[2].essentialValue === true ? " essentialValue " : "" : "") + "' id='txtArz" + id + "' value='" + strArz + "'/></div>";

                    str += "<div class='col-md-1'><input  type='text'" + (ItemFields[3] != undefined ? ItemFields[3].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[3] != undefined ? ItemFields[3].essentialValue === true ? " essentialValue " : "" : "") + "' id='txtErtefa" + id + "' value='" + strErtefa + "'/></div>";

                    str += "<div class='col-md-1'><input type='text'" + (ItemFields[4] != undefined ? ItemFields[4].isEnteringValue !== true ? "disabled='disabled'" : "" : "") +
                        " class='form-control spanStyleMitraSmall " + (ItemFields[4] != undefined ? ItemFields[4].essentialValue === true ? " essentialValue " : "" : "") + "' id='txtVazn" + id + "' value='" + strVazn + "'/></div>";

                    str += "<div class='col-md-1'><label class='RMMJozStyle'>" + MeghdarJoz + "</label></div>";

                    str += "<div class='col-md-2'><input  type='text' title='" + row.des + "' class='form-control spanStyleMitraSmall input-sm' id='txtDes" + id + "' value='" + row.des + "'/></div>";

                    str += "<div class='col-md-1' style='text-align:center;'>";

                    if (HasEditButton)
                        //str += "<i  id='iUpdate" + id +"' class='fa fa-save SaveRMUStyle' onclick=\"UpdateRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i>";
                        str += "<button type='button' id='iUpdate" + id + "' onclick=\"UpdateRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>";

                    if (HasDelButton)
                        //str += "<i class='fa fa-trash DelRMUStyle' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i>";
                        str += "<button type='button' id='iDelete" + id + "' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\" class=\"ButtonRowsSaveStyle\"><i id=\"iDelete\" class=\"fa fa-trash DelRMUStyle\"></i></button>";



                    str += "</div>";
                    str += "</div>";
                });
                str += "</div>";

            }
        });

        $targetDivRizMetreKH = $('#ViewRizMetreKH' + KMNum);

        $targetDivRizMetreKH.html(str);
        debugger;
        $targetDivRizMetreKH.slideDown();



        $targetDivRizMetreKH.find("input[type='text'].essentialValue")
            .filter(function () {
                return $(this).val().trim() === "";
            })
            .addClass("blinking")
            .first()
            .focus();


        $targetDivRizMetreKH.on("change", "input[type='text'].essentialValue", function () {
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

    // حداکثر اختلاف مجاز
    const TOLERANCE = 0.01;

    // تبدیل مقدار input به عدد
    const getValue = (id) => {
        const value = $('#' + id).val();

        if (value === '' || value == null)
            return 0;

        const number = parseFloat(value);

        return isNaN(number) ? 0 : number;
    };

    // آیا دو عدد با در نظر گرفتن تلورانس برابر هستند؟
    const isEqual = (a, b) => {
        return Math.abs(a - b) <= TOLERANCE;
    };

    // پاک کردن خطاهای یک ردیف
    const clearRowErrors = (i) => {
        const currentRow = $('#txtDarsadHaml' + i)
            .closest('.AmalyatKhakiRowStyle');

        currentRow.removeClass('errorRow');

        $('#txtReUseHajm' + i).removeClass('blinking');
        $('#txtVarizi' + i).removeClass('blinking');
        $('#txtHaml' + i).removeClass('blinking');

        $('#txtReUseDarsad' + i).removeClass('blinking');
        $('#txtDarsadVarizi' + i).removeClass('blinking');
        $('#txtDarsadHaml' + i).removeClass('blinking');
    };

    // علامت زدن مقادیر حجم
    const blinkVolumes = (i) => {
        if (i != 1 && i != 2) {
            $('#txtReUseHajm' + i).addClass('blinking');
        }

        $('#txtVarizi' + i).addClass('blinking');
        $('#txtHaml' + i).addClass('blinking');
    };

    for (var i = 1; i <= ActivityLength; i++) {

        var KhDetail = getValue('txtKhDetail' + i);

        var ReUseHajm = getValue('txtReUseHajm' + i);
        var ReUseDarsad = getValue('txtReUseDarsad' + i);

        var Varizi = getValue('txtVarizi' + i);
        var DarsadVarizi = getValue('txtDarsadVarizi' + i);

        var Haml = getValue('txtHaml' + i);
        var DarsadHaml = getValue('txtDarsadHaml' + i);

        // جمع سه مقدار
        var sumAllThisRow = ReUseHajm + Varizi + Haml;

        // اختلاف
        var difference = Math.abs(sumAllThisRow - KhDetail);

        /*
         * اگر اختلاف حداکثر 0.01 باشد، قابل قبول است.
         *
         * مثال:
         * KhDetail = 0.10
         * Sum      = 0.09
         * اختلاف   = 0.01
         *
         * این حالت مجاز است.
         */
        if (difference <= TOLERANCE) {

            clearRowErrors(i);

        }
        else {

            // جمع کمتر یا بیشتر از مقدار خاکبرداری است
            const currentRow = $('#txtDarsadHaml' + i)
                .closest('.AmalyatKhakiRowStyle');

            currentRow.addClass('errorRow');

            blinkVolumes(i);

            checkValues = true;

            return true;
        }

        // بررسی مقادیر منفی یا بزرگ‌تر از مقدار کل
        if (ReUseHajm > KhDetail + TOLERANCE) {
            $('#txtReUseHajm' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtReUseHajm' + i).removeClass('blinking');
        }

        // درصد ReUse
        if (ReUseDarsad > 100) {
            $('#txtReUseDarsad' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtReUseDarsad' + i).removeClass('blinking');
        }

        // واریزی
        if (Varizi > KhDetail + TOLERANCE) {
            $('#txtVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtVarizi' + i).removeClass('blinking');
        }

        // حمل
        if (Haml > KhDetail + TOLERANCE) {
            $('#txtHaml' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtHaml' + i).removeClass('blinking');
        }

        // درصد واریزی
        if (DarsadVarizi > 100) {
            $('#txtDarsadVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadVarizi' + i).removeClass('blinking');
        }

        // درصد حمل
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

    // حداکثر اختلاف مجاز
    const TOLERANCE = 0.01;

    // مقایسه دو عدد اعشاری با تلورانس
    const isEqual = (a, b) => {
        return Math.abs(a - b) <= TOLERANCE;
    };

    // گرفتن مقدار عددی از input
    const getValue = (id) => {
        const value = parseFloat($('#' + id).val());
        return isNaN(value) ? 0 : value;
    };

    for (var i = 1; i <= ActivityLength; i++) {

        JamKol = 0;
        JamDarsad = 0;

        var prefix = KMNum + "_" + i;

        var KhDetail = parseFloat($('#txtKhDetail' + prefix).val());

        // اگر KhDetail عدد نیست، این ردیف بررسی نشود
        if (isNaN(KhDetail))
            continue;

        // ==========================================
        // ReUse
        // ==========================================

        var ReUseHajm = getValue('txtReUseHajm' + prefix);

        if (ReUseHajm > KhDetail + TOLERANCE) {
            $('#txtReUseHajm' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtReUseHajm' + prefix).removeClass('blinking');
        }

        JamKol += ReUseHajm;


        var ReUseDarsad = getValue('txtReUseDarsad' + prefix);

        if (ReUseDarsad > 100 + TOLERANCE) {
            $('#txtReUseDarsad' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtReUseDarsad' + prefix).removeClass('blinking');
        }

        JamDarsad += ReUseDarsad;


        // ==========================================
        // Varizi
        // ==========================================

        var Varizi = getValue('txtVarizi' + prefix);

        if (Varizi > KhDetail + TOLERANCE) {
            $('#txtVarizi' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtVarizi' + prefix).removeClass('blinking');
        }

        JamKol += Varizi;


        var DarsadVarizi = getValue('txtDarsadVarizi' + prefix);

        if (DarsadVarizi > 100 + TOLERANCE) {
            $('#txtDarsadVarizi' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadVarizi' + prefix).removeClass('blinking');
        }

        JamDarsad += DarsadVarizi;


        // ==========================================
        // Haml
        // ==========================================

        var Haml = getValue('txtHaml' + prefix);

        if (Haml > KhDetail + TOLERANCE) {
            $('#txtHaml' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtHaml' + prefix).removeClass('blinking');
        }

        JamKol += Haml;


        var DarsadHaml = getValue('txtDarsadHaml' + prefix);

        if (DarsadHaml > 100 + TOLERANCE) {
            $('#txtDarsadHaml' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadHaml' + prefix).removeClass('blinking');
        }

        JamDarsad += DarsadHaml;


        // ==========================================
        // بررسی نهایی ردیف
        // ==========================================

        if (KhDetail != 0) {

            // اختلاف حجم
            var volumeDifference = Math.abs(JamKol - KhDetail);

            // اختلاف درصد
            var percentDifference = Math.abs(JamDarsad - 100);

            /*
             * حجم:
             * تا 0.01 اختلاف مجاز است.
             *
             * درصد:
             * تا 0.01 اختلاف مجاز است.
             */
            if (volumeDifference > TOLERANCE ||
                percentDifference > TOLERANCE) {

                var currentRow = $('#txtDarsadHaml' + prefix)
                    .closest('.AmalyatKhakiRowStyle');

                currentRow.addClass('errorRow');

                checkValues = true;
            }
            else {

                // مقدارها قابل قبول هستند
                var currentRow = $('#txtDarsadHaml' + prefix)
                    .closest('.AmalyatKhakiRowStyle');

                currentRow.removeClass('errorRow');
            }
        }
        else {

            // اگر KhDetail صفر است و خطای قبلی داشته،
            // خطای ردیف پاک شود
            var currentRow = $('#txtDarsadHaml' + prefix)
                .closest('.AmalyatKhakiRowStyle');

            currentRow.removeClass('errorRow');
        }
    }

    return checkValues;
}

// function CheckValuesOfKhakBardariDetails() {
//     debugger;
//     checkValues = false;
//     for (var i = 1; i <= ActivityLength; i++) {
//         KhDetail = $('#txtKhDetail' + i).val() == '' ? 0 : parseFloat($('#txtKhDetail' + i).val());
//         ReUseHajm = $('#txtReUseHajm' + i).val() == '' ? 0 : parseFloat($('#txtReUseHajm' + i).val());
//         ReUseDarsad = $('#txtReUseDarsad' + i).val() == '' ? 0 : parseFloat($('#txtReUseDarsad' + i).val());
//         Varizi = $('#txtVarizi' + i).val() == '' ? 0 : parseFloat($('#txtVarizi' + i).val());
//         DarsadVarizi = $('#txtDarsadVarizi' + i).val() == '' ? 0 : parseFloat($('#txtDarsadVarizi' + i).val());
//         Haml = $('#txtHaml' + i).val() == '' ? 0 : parseFloat($('#txtHaml' + i).val());
//         DarsadHaml = $('#txtDarsadHaml' + i).val() == '' ? 0 : parseFloat($('#txtDarsadHaml' + i).val());

//         ReUseHajm = isNaN(ReUseHajm) ? 0 : ReUseHajm;
//         Varizi = isNaN(Varizi) ? 0 : Varizi;
//         Haml = isNaN(Haml) ? 0 : Haml;
//         sumAllThisRow = ReUseHajm + Varizi + Haml;
//         if (Math.abs(sumAllThisRow - KhDetail) > 0.01) {
//             currentRow = $('#txtDarsadHaml' + i).closest('.AmalyatKhakiRowStyle');
//             currentRow.addClass('errorRow');
//             checkValues = true;
//             return true;
//         }
//         if (sumAllThisRow < KhDetail) {
//             if (i != 1 && i != 2) {
//                 $('#txtReUseHajm' + i).addClass('blinking');
//             }
//             $('#txtVarizi' + i).addClass('blinking');
//             $('#txtHaml' + i).addClass('blinking');
//             return true;
//         }
//         if (sumAllThisRow > KhDetail) {
//             if (i != 1 && i != 2) {
//                 $('#txtReUseHajm' + i).addClass('blinking');
//             }
//             $('#txtVarizi' + i).addClass('blinking');
//             $('#txtHaml' + i).addClass('blinking');
//             return true;
//         }
//         else if (sumAllThisRow == KhDetail) {
//             currentRow = $('#txtDarsadHaml' + i).closest('.AmalyatKhakiRowStyle');
//             currentRow.removeClass('errorRow');

//             $('#txtReUseHajm' + i).removeClass('blinking');
//             $('#txtVarizi' + i).removeClass('blinking');
//             $('#txtHaml' + i).removeClass('blinking');

//             $('#txtReUseDarsad' + i).removeClass('blinking');
//             $('#txtDarsadVarizi' + i).removeClass('blinking');
//             $('#txtDarsadHaml' + i).removeClass('blinking');
//             return false;
//         }

//         if (KhDetail != 0 && KhDetail != '') {
//             if (ReUseHajm == 0 || isNaN(ReUseHajm)) {
//                 $('#txtReUseHajm' + i).addClass('blinking');
//                 return true;
//             }
//             else if (Varizi == 0 || isNaN(Varizi)) {
//                 $('#txtVarizi' + i).addClass('blinking');
//                 return true;
//             }
//             else if (Haml == 0 || isNaN(Haml)) {
//                 $('#txtHaml' + i).addClass('blinking');
//                 return true;
//             }
//             else return false;
//         }

//         if (ReUseHajm > KhDetail) {
//             $('#txtReUseHajm' + i).addClass('blinking');
//             checkValues = true;
//         }
//         else
//             $('#txtReUseHajm' + i).removeClass('blinking');

//         if (ReUseDarsad > 100) {
//             $('#txtReUseDarsad' + i).addClass('blinking');
//             checkValues = true;
//         }
//         else
//             $('#txtReUseDarsad' + i).removeClass('blinking');
//         /////////////
//         if (Varizi > KhDetail) {
//             $('#txtVarizi' + i).addClass('blinking');
//             checkValues = true;
//         }
//         else {
//             $('#txtVarizi' + i).removeClass('blinking');
//         }

//         if (Haml > KhDetail) {
//             $('#txtHaml' + i).addClass('blinking');
//             checkValues = true;
//         }
//         else {
//             $('#txtHaml' + i).removeClass('blinking');
//         }

//         if (DarsadVarizi > 100) {
//             $('#txtDarsadVarizi' + i).addClass('blinking');
//             checkValues = true;
//         }
//         else {
//             $('#txtDarsadVarizi' + i).removeClass('blinking');
//         }

//         if (DarsadHaml > 100) {
//             $('#txtDarsadHaml' + i).addClass('blinking');
//             checkValues = true;
//         }
//         else {
//             $('#txtDarsadHaml' + i).removeClass('blinking');
//         }
//     }
//     return checkValues;
// }

// function CheckValuesOfKhakBardariDetailsForEdit(KMNum) {
//     debugger;
//     checkValues = false;
//     for (var i = 1; i <= ActivityLength; i++) {
//         JamKol = 0;
//         JamDarsad = 0;
//         KhDetail = parseFloat($('#txtKhDetail' + KMNum + "_" + i).val());
//         if (!isNaN(KhDetail)) {

//             ReUseHajm = parseFloat($('#txtReUseHajm' + KMNum + "_" + i).val());
//             if (ReUseHajm > KhDetail) {
//                 $('#txtReUseHajm' + KMNum + "_" + i).addClass('blinking');
//                 checkValues = true;
//             }
//             else
//                 $('#txtReUseHajm' + KMNum + "_" + i).removeClass('blinking');
//             JamKol += isNaN(ReUseHajm) ? 0 : ReUseHajm;

//             ReUseDarsad = parseFloat($('#txtReUseDarsad' + KMNum + "_" + i).val());
//             if (ReUseDarsad > 100) {
//                 $('#txtReUseDarsad' + KMNum + "_" + i).addClass('blinking');
//                 checkValues = true;
//             }
//             else
//                 $('#txtReUseDarsad' + KMNum + "_" + i).removeClass('blinking');
//             JamDarsad += isNaN(ReUseDarsad) ? 0 : ReUseDarsad;

//             /////////////
//             Varizi = parseFloat($('#txtVarizi' + KMNum + "_" + i).val());
//             if (Varizi > KhDetail) {
//                 $('#txtVarizi' + KMNum + "_" + i).addClass('blinking');
//                 checkValues = true;
//             }
//             else {
//                 $('#txtVarizi' + KMNum + "_" + i).removeClass('blinking');
//             }
//             JamKol += isNaN(Varizi) ? 0 : Varizi;


//             Haml = parseFloat($('#txtHaml' + KMNum + "_" + i).val());
//             if (Haml > KhDetail) {
//                 $('#txtHaml' + KMNum + "_" + i).addClass('blinking');
//                 checkValues = true;
//             }
//             else {
//                 $('#txtHaml' + KMNum + "_" + i).removeClass('blinking');
//             }
//             JamKol += isNaN(Haml) ? 0 : Haml;

//             DarsadVarizi = parseFloat($('#txtDarsadVarizi' + KMNum + "_" + i).val());
//             if (DarsadVarizi > 100) {
//                 $('#txtDarsadVarizi' + KMNum + "_" + i).addClass('blinking');
//                 checkValues = true;
//             }
//             else {
//                 $('#txtDarsadVarizi' + KMNum + "_" + i).removeClass('blinking');
//             }
//             JamDarsad += isNaN(DarsadVarizi) ? 0 : DarsadVarizi;


//             DarsadHaml = parseFloat($('#txtDarsadHaml' + KMNum + "_" + i).val());
//             if (DarsadHaml > 100) {
//                 $('#txtDarsadHaml' + KMNum + "_" + i).addClass('blinking');
//                 checkValues = true;
//             }
//             else {
//                 $('#txtDarsadHaml' + KMNum + "_" + i).removeClass('blinking');
//             }
//             JamDarsad += isNaN(DarsadHaml) ? 0 : DarsadHaml;

//             if (KhDetail != 0) {
//                 if (JamDarsad != 100 || (Math.abs(JamKol - KhDetail) > 0.01)) { //JamKol != KhDetail) {
//                     currentRow = $('#txtDarsadHaml' + KMNum + "_" + i).closest('.AmalyatKhakiRowStyle');
//                     currentRow.addClass('errorRow');
//                     checkValues = true;
//                 }
//             }
//         }
//     }
//     return checkValues;
// }


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
        toastr.info('متراژ انتها بایستی بعد از متراژ شروع باشد', 'اطلاع');
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


        htmlShowWaiting = ShowWaiting();
        if (htmlShowWaiting != '')
            $('#MainViewKhakBardariNew').append(htmlShowWaiting);
        else
            $('#ContainerWaiting').show();


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
                        $('#txtKhDetail' + i).val('');
                        $('#txtDarsad' + i).val('');
                        $('#txtReUseHajm' + i).val('');
                        $('#txtReUseDarsad' + i).val('');
                        $('#txtVarizi' + i).val('');
                        $('#txtDarsadVarizi' + i).val('');
                        $('#txtHaml' + i).val('');
                        $('#txtDarsadHaml' + i).val('');
                        //$('#txtKhakBardariItemId' + i).val(0);
                    }

                    $('#spantotalKhDetailNew').html('');
                    $('#spantotalDarsadKhDetailNew').html('');
                    $('#spantotalVariziNew').html('');
                    $('#spantotalDarsadVariziNew').html('');
                    $('#spantotalReUseHajmNew').html('');
                    $('#spantotalDarsadReUseHajmNew').html('');
                    $('#spantotalHamlNew').html('');
                    $('#spantotalDarsadHamlNew').html('');

                    $('#MainViewKhakBardariNew').slideUp(500);

                    ShowExistingKMKhakBardari(BarAvordUserId);

                    $('#ContainerWaiting').hide();

                    toastr.success('اطلاعات متراژ بدرستی ثبت گردید', 'ثبت');
                }
                else {
                    toastr.error('مشکل در ثبت اطلاعات متراژ', 'خطا');
                    $('#ContainerWaiting').hide();
                }
            },
            error: function (response) {
                toastr.error('مشکل در ثبت اطلاعات متراژ', 'خطا');
                $('#ContainerWaiting').hide();
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
        toastr.info('متراژ انتها بایستی بعد از متراژ شروع باشد', 'اطلاع');
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
                    NoeKhakBardari:parseInt($('#txtKhakBardariItemId' + KMNum + "_" + i).val()),
                };
                dataList.push(obj);
            }
        }

        //KMKhakBardariId = $('#HDFKMAmalyateKhakiIdForEdit').val();
        //KMKhakBardariNum = $('#HDFKMAmalyateKhakiNum').val();
        Year = parseInt($('#HDFYear').val());
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


        htmlShowWaiting = ShowWaiting();
        if (htmlShowWaiting != '')
            $('#MainViewKhakBardari' + KMNum).append(htmlShowWaiting);
        else
            $('#ContainerWaiting').show();

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
                    toastr.success('اطلاعات متراژ بدرستی ویرایش گردید', 'ثبت');
                    $('#MainViewKhakBardari' + KMNum).slideUp(500);
                    $('#divExistKMHeader' + KMNum).click();
                    $('#ContainerWaiting').hide();
                    //$('#ViewKhakBardariEzafeBaha' + KMNum).html('');

                }
                else {
                    toastr.error('مشکل در ویرایش اطلاعات متراژ', 'خطا');
                    $('#ContainerWaiting').hide();
                }
            },
            error: function (response) {
                toastr.error('مشکل در ویرایش اطلاعات متراژ', 'خطا');
                $('#ContainerWaiting').hide();
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}

function DeleteKhakBardariInfo(KMKhakBardariId, KMNum, BarAvordUserId) {

    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.KMKhakBardariId = KMKhakBardariId;
    vardata.Year = Year;
    vardata.KMNum = KMNum;


    debugger;
    $.ajax({
        type: "POST",
        url: "/AmalyateKhakiInfoForBarAvords/DeleteKhakBardariInfoForBarAvord",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('اطلاعات متراژ بدرستی حذف گردید', 'ثبت');
                ShowExistingKMKhakBardari(BarAvordUserId);
            }
            else {
                toastr.error('مشکل در حذف اطلاعات', 'خطا');
                $('#ContainerWaiting').hide();
            }
        },
        error: function (response) {
            toastr.error('مشکل در حذف اطلاعات', 'خطا');
            $('#ContainerWaiting').hide();
        }
    });

}