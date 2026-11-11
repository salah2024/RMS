ActivityLength = 0;

/* کنترل عملیات در حال اجرا و جلوگیری از ارسال درخواست تکراری */
const payKaniPendingRequests = Object.create(null);
let payKaniActiveRequestCount = 0;

function beginPayKaniRequest(requestKey, buttonElement) {
    if (payKaniPendingRequests[requestKey]) return false;

    const $button = buttonElement ? $(buttonElement) : $();
    if ($button.length && $button.prop('disabled')) return false;

    if ($button.length) {
        $button.data('payKaniOriginalHtml', $button.html());
        $button.data('payKaniOriginalCursor', $button.css('cursor'));
        $button.data('payKaniOriginalOpacity', $button.css('opacity'));
        $button
            .prop('disabled', true)
            .attr('aria-busy', 'true')
            .addClass('PayKaniButtonLoading')
            .css({ cursor: 'wait', opacity: 0.7 })
            .html('<i class="fa fa-spinner fa-spin" aria-hidden="true"></i><span class="sr-only"> در حال انجام...</span>');
    }

    payKaniPendingRequests[requestKey] = $button;
    payKaniActiveRequestCount++;
    $('#ContainerWaiting').show();
    return true;
}

function endPayKaniRequest(requestKey) {
    const $button = payKaniPendingRequests[requestKey];

    if ($button && $button.length) {
        const originalHtml = $button.data('payKaniOriginalHtml');
        const originalCursor = $button.data('payKaniOriginalCursor');
        const originalOpacity = $button.data('payKaniOriginalOpacity');

        if (originalHtml !== undefined) $button.html(originalHtml);

        $button
            .prop('disabled', false)
            .removeAttr('aria-busy')
            .removeClass('PayKaniButtonLoading')
            .css({
                cursor: originalCursor || '',
                opacity: originalOpacity || ''
            })
            .removeData('payKaniOriginalHtml')
            .removeData('payKaniOriginalCursor')
            .removeData('payKaniOriginalOpacity');
    }

    delete payKaniPendingRequests[requestKey];
    payKaniActiveRequestCount = Math.max(0, payKaniActiveRequestCount - 1);

    if (payKaniActiveRequestCount === 0) {
        $('#ContainerWaiting').hide();
    }
}


function PayKaniWithBarAvordIdClick(OpId,BarAvordUserId) {

    str = "";
    str += `

  <div class="col-12" id="divExistingKMPK">
  </div>


      <!-- متراژ های ثبت شده -->

    <div class="PayKani-container" style="border: 1px solid #cb92ff;background-color: #f3ebff;">
    <div class="row PayKani-box">

    <!-- از متراژ -->
    <div class="col-md-1">
        <i class="fa fa-plus" style="font-size: 20px;color: green;"></i>
    </div>
    <div class="col-md-1 label-col">
      <span>از متراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 PayKaniTextStyle input-sm text-center" id="txtFromKMForPayKani" value="0"/>
    </div>

    <!-- تا متراژ -->
    <div class="col-md-1 label-col">
      <span>تا متراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 PayKaniTextStyle input-sm text-center" id="txtToKMForPayKani" value="0"/>
    </div>

    <!-- حجم پی کنی -->
    <div class="col-md-2 label-col">
      <span>حجم پی کنی:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 PayKaniTextStyle input-sm text-center" id="txtHajmPayKani" value="0"/>
    </div>
    <div class="col-md-1 unit-col">
      <span>مترمکعب</span>
    </div>
  </div>

  <!-- بخش نمایش -->
  <div id="MainViewPayKaniNew" class="PayKani-view" style="display:none">
  <div id="ViewPayKaniNew" class="PayKani-view">
  </div>

 <!-- دکمه ذخیره -->
 <div class="row">
 <div class="col-12" style="direction: ltr;">

 <div class="col-md-1 action-col">
     <button type="button" id="btnSavePayKaniInfo"
            onclick="SavePayKaniInfo('${BarAvordUserId}'` + ',' + `'Add', this)"
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

    setTimeout(() => { $('#txtFromKMForPayKani').focus().select(); }, 200);

    $('#ula' + OpId)
        .off('keydown.PayKani')
        .on('keydown.PayKani', '.PayKaniTextStyle', function (e) {
            if (e.key === 'Enter') {
                e.preventDefault(); // جلوگیری از submit فرم

                const inputs = $('.PayKaniTextStyle');
                let idx = inputs.index(this);

                // تا وقتی input بعدی وجود دارد، بررسی کن disabled نباشد
                while (idx < inputs.length - 1) {
                    idx++;
                    if (!inputs.eq(idx).prop('disabled')) {
                        inputs.eq(idx).focus().select();
                        return; // خروج از تابع بعد از فوکوس موفق
                    }
                    else
                    {
                        $('#btnSavePayKaniInfo').focus();
                    }
                }

                // اگر هیچ input فعالی نبود → برو روی دکمه
                $('.buttonStyleBoard').focus();
            }
        });


    ShowExistingKMPayKani(BarAvordUserId);

    ShowSelctionPayKani(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0);


}

function ShowSelctionPayKani(IsNew, KMExistingId, KMNum, BarAvordId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value) {

    strSSKB = `

    <div id="divPayKaniInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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
      <span>شرح عملیات پی کنی</span>
      </div>
    </div>

    <!-- ستون محتوا -->
    <div class="col-8" style="padding:0;">
      <div class="row" style="margin:0;">

        <!-- حجم پی کنی -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;">
            <span>حجم پی کنی</span>
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
    //پی کنی
    vardata.Type = 2;

    $.ajax({
        type: "POST",
        url: "/payKaniInfoForBarAvords/ReturnNoePayKani",
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
        <input id="txtPayKaniItemId${i + 1}" value="${ActivityTitleComplete[i].id}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanPayKaniItems${i + 1}" class="spanStylePayKaniItems">
        ${ActivityTitleComplete[i].title}
      </span>
    </div>

    <!-- ستون اصلی محتوا -->
    <div class="col-8" style="padding:0;">
      <div class="row" style="margin:0;">

        <!-- حجم پی کنی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKDetail${i + 1}" value="" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKDarsad${i + 1}" value="" />
            </div>
          </div>
        </div>

      <!-- حمل به دپو/مسیر -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKHaml${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKDarsadHaml${i + 1}" value=""  />
            </div>
          </div>
        </div>
        
        <!-- قابل مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKReUseHajm${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKReUseDarsad${i + 1}" value=""  />
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" disabled id="txtPKVarizi${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" disabled id="txtPKDarsadVarizi${i + 1}" value=""  />
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

        <!-- حجم پی کنی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKDetailNew"></span>
            </div>
            <div class="col-4" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKDarsadDetailNew"></span>
            </div>
          </div>
        </div>

    <!-- حمل به دپو -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKHamlNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKDarsadHamlNew"></span>
            </div>
          </div>
        </div>

        <!-- مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKReUseHajmNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKDarsadReUseHajmNew"></span>
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKVariziNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalPKDarsadVariziNew"></span>
            </div>
          </div>
        </div>


      </div>
    </div>
  </div>
</div>`;
            //////////////
            $('#ViewPayKaniNew').html(strSSKB);

            $('#ViewPayKaniNew input[type="text"]').change(function () {

                let changedId = $(this).attr("id");
                let i = changedId.match(/\d+/) ? changedId.match(/\d+/)[0] : ""; // شماره ردیف
                let HajmPayKani = parseFloat($.trim($('#txtHajmPayKani').val()));

                // 🚨 اعتبارسنجی
                if (!$.isNumeric($(this).val())) {
                    toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }
                else {
                    $(this).removeClass('blinking');
                }

                if (HajmPayKani == 0 || HajmPayKani == '' || !$.isNumeric(HajmPayKani)) {
                    toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmPayKani').addClass('blinking');
                    return;
                } else {
                    $('#txtHajmPayKani').removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                let PKDetail = parseFloat($("#txtPKDetail" + i).val()) || 0;

                //if (PKDetail > 0) {
                if (PKDetail > 0) {
                    $("#txtPKVarizi" + i).prop('disabled') ? '' : $("#txtPKVarizi" + i).val() != "" ? $("#txtPKVarizi" + i).removeClass('blinking') : $("#txtPKVarizi" + i).addClass('blinking');
                    $("#txtPKReUseHajm" + i).prop('disabled') ? '' : $("#txtPKReUseHajm" + i).val() != "" ? $("#txtPKReUseHajm" + i).removeClass('blinking') : $("#txtPKReUseHajm" + i).addClass('blinking');
                    $("#txtPKHaml" + i).prop('disabled') ? '' : $("#txtPKHaml" + i).val() != "" ? $("#txtPKHaml" + i).removeClass('blinking') : $("#txtPKHaml" + i).addClass('blinking');

                    $("#txtPKDarsadVarizi" + i).prop('disabled') ? '' : $("#txtPKDarsadVarizi" + i).val() != "" ? $("#txtPKDarsadVarizi" + i).removeClass('blinking') : $("#txtPKDarsadVarizi" + i).addClass('blinking');
                    $("#txtPKReUseDarsad" + i).prop('disabled') ? '' : $("#txtPKReUseDarsad" + i).val() != "" ? $("#txtPKReUseDarsad" + i).removeClass('blinking') : $("#txtPKReUseDarsad" + i).addClass('blinking');
                    $("#txtPKDarsadHaml" + i).prop('disabled') ? '' : $("#txtPKDarsadHaml" + i).val() != "" ? $("#txtPKDarsadHaml" + i).removeClass('blinking') : $("#txtPKDarsadHaml" + i).addClass('blinking');
                }

                let varizi = parseFloat($("#txtPKVarizi" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtPKReUseHajm" + i).val()) || 0;
                let haml = parseFloat($("#txtPKHaml" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtPKDarsadVarizi" + i).val()) || 0;
                let dReuse = parseFloat($("#txtPKReUseDarsad" + i).val()) || 0;
                let dHaml = parseFloat($("#txtPKDarsadHaml" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("Detail")) {
                    let Zarb = HajmPayKani === 0 ? 0 : (PKDetail / HajmPayKani) * 100;
                    $("#txtPKDarsad" + i).val(Zarb.toFixed(2));

                    $('#spantotalPKDarsadDetailNew').html();

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmPayKani) {
                        let NewVal = HajmPayKani - (SumAll - PKDetail);
                        $("#txtPKDetail" + i).val(NewVal.toFixed(2));
                        PKDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmPayKani === 0 ? 0 : (PKDetail / HajmPayKani) * 100;
                        $("#txtPKDarsad" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    // dVarizi === 0 ? 0 : ((dVarizi / 100) * PKDetail).toFixed(2);
                    // $("#txtVarizi" + i).val(dVarizi);
                    // $("#txtReUseHajm" + i).val(((dReuse / 100) * PKDetail).toFixed(2));
                    // $("#txtHaml" + i).val(((dHaml / 100) * PKDetail).toFixed(2));


                    // حجم هر بخش بر اساس درصد
                    let vVarizi = (dVarizi / 100) * PKDetail;
                    let vReUse = (dReuse / 100) * PKDetail;
                    let vHaml = (dHaml / 100) * PKDetail;

                    $("#txtPKVarizi" + i).val(vVarizi.toFixed(2));
                    $("#txtPKReUseHajm" + i).val(vReUse.toFixed(2));
                    $("#txtPKHaml" + i).val(vHaml.toFixed(2));
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
                    let newKhDetail = (dVal / 100) * HajmPayKani;
                    $('#txtPKDetail' + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (PKDetail > 0) {
                    if (changedId.includes("DarsadVarizi")) {
                        varizi = (dVarizi / 100) * PKDetail;
                        $("#txtPKVarizi" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        reuseHajm = (dReuse / 100) * PKDetail;
                        $("#txtPKReUseHajm" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        haml = (dHaml / 100) * PKDetail;
                        $("#txtPKHaml" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        dVarizi = (PKDetail === 0 ? 0 : (varizi / PKDetail)) * 100;
                        $("#txtPKDarsadVarizi" + i).val(dVarizi.toFixed(2));
                        $("#txtPKDarsadVarizi" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (PKDetail === 0 ? 0 : (reuseHajm / PKDetail)) * 100;
                        $("#txtPKReUseDarsad" + i).val(dReuse.toFixed(0));
                        $("#txtPKReUseDarsad" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (PKDetail === 0 ? 0 : (haml / PKDetail)) * 100;
                        $("#txtPKDarsadHaml" + i).val(dHaml.toFixed(0));
                        $("#txtPKDarsadHaml" + i).removeClass('blinking');
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtPKVarizi" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtPKReUseHajm" + i).val()) || 0;
                haml = parseFloat($("#txtPKHaml" + i).val()) || 0;

                dVarizi = parseFloat($("#txtPKDarsadVarizi" + i).val()) || 0;
                dReuse = parseFloat($("#txtPKReUseDarsad" + i).val()) || 0;
                dHaml = parseFloat($("#txtPKDarsadHaml" + i).val()) || 0;

               

                AutoFillThirdColumnPK(i, PKDetail, changedId);

                SumKol = sumKolNewPK(ActivityLength,0);
                $('#spantotalPKDetailNew').html(toPersianDigits(SumKol[0].toFixed(2)));
                $('#spantotalPKDarsadDetailNew').html(toPersianDigits(SumKol[1].toFixed(0)) + " % ");

                $('#spantotalPKVariziNew').html(toPersianDigits(SumKol[2].toFixed(2)));

                DarsadVariziNew = (parseFloat(SumKol[2]) / parseFloat(HajmPayKani)) * 100;
                $('#spantotalPKDarsadVariziNew').html(toPersianDigits(DarsadVariziNew.toFixed(2)) + " % ");

                $('#spantotalPKReUseHajmNew').html(toPersianDigits(SumKol[3].toFixed(2)));
                DarsadReUseHajmNew = (parseFloat(SumKol[3]) / parseFloat(HajmPayKani)) * 100;
                $('#spantotalPKDarsadReUseHajmNew').html(toPersianDigits(DarsadReUseHajmNew.toFixed(2)) + " % ");

                $('#spantotalPKHamlNew').html(toPersianDigits(SumKol[4].toFixed(2)));
                DarsadHamlNew = (parseFloat(SumKol[4]) / parseFloat(HajmPayKani)) * 100;
                $('#spantotalPKDarsadHamlNew').html(toPersianDigits(DarsadHamlNew.toFixed(2)) + " % ");
            });

            $('#txtHajmPayKani').on('keydown', function (e) {
                if (e.key === 'Enter') {
                    e.preventDefault();

                    let HKB = parseFloat($(this).val());

                    var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
                    var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));

                    if (KMS < 0 || KME == 0) {
                        $('#txtFromKMForPayKani').addClass('blinking');
                        $('#txtToKMForPayKani').addClass('blinking');
                        return;
                    }

                    if (KMS > KME) {
                        toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
                        $('#txtToKMForPayKani').addClass('blinking');
                        $('#txtFromKMForPayKani').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtToKMForPayKani').removeClass('blinking');
                        $('#txtFromKMForPayKani').removeClass('blinking');
                    }

                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
                        $(this).addClass('blinking');
                        return;
                    }

                    $('#MainViewPayKaniNew').slideDown(500);
                }
            });

            $('#txtHajmPayKani').change(function () {
                let HKB = parseFloat($(this).val());

                var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));

                if (KMS < 0 || KME == 0) {
                    $('#txtFromKMForPayKani').addClass('blinking');
                    $('#txtToKMForPayKani').addClass('blinking');
                    return;
                }

                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
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
                    toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                $('#MainViewPayKaniNew').slideDown(500);

                $(this).closest('ul').find('.StyleMainViewPayKani').each(function () {
                    $(this).slideUp(500);
                });

                $('#divPayKaniInfoDetails').show();
                $(this).removeClass('blinking');

                // ابتدا تمام ردیف‌ها صفر شوند
                for (let i = 1; i <= ActivityLength; i++) {

                    $('#txtPKDetail' + i).val('0.00');
                    $('#txtPKDarsad' + i).val('0');

                    $('#txtPKVarizi' + i).val('0.00');
                    $('#txtPKReUseHajm' + i).val('0.00');
                    $('#txtPKHaml' + i).val('0.00');
                }

                // کل حجم در ردیف اول
                $('#txtPKDetail1').val(HKB.toFixed(2));
                $('#txtPKDarsad1').val('100');

                // درصدهای قبلی جزئیات ردیف اول حفظ شوند
                let dVarizi = parseFloat($('#txtPKDarsadVarizi1').val()) || 0;
                let dReUse = parseFloat($('#txtPKReUseDarsad1').val()) || 0;
                let dHaml = parseFloat($('#txtPKDarsadHaml1').val()) || 0;

                // محاسبه مجدد حجم‌ها بر اساس درصدهای خودشان
                $('#txtPKVarizi1').val(
                    (HKB * dVarizi / 100).toFixed(2)
                );

                $('#txtPKReUseHajm1').val(
                    (HKB * dReUse / 100).toFixed(2)
                );

                $('#txtPKHaml1').val(
                    (HKB * dHaml / 100).toFixed(2)
                );

            });

            $('#txtFromKMForPayKani').change(function () {
                var KM = $(this).val();

                var KMS = parseFloat(KM);//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForPayKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForPayKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmPayKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
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
                var KM = $(this).val();
               


                var KME = parseFloat(KM);//.replace('+', ''));
                var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForPayKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForPayKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmPayKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
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
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری پی کنی', 'خطا');
        }
    });
}

function ShowExistingKMPayKani(BarAvordUserId) {
    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.Type = 2;
    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/GetExistingKMPayKaniInfoWithBarAvordId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var KMPayKaniBarAvord = response;
            if (KMPayKaniBarAvord.length > 0) {
                strSEKB = `
                 <div class="row col-12 ExistKhBHeaderStyle">
                        <div class="col-1" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>ردیف</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;" ><span>از متراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>تا متراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>حجم پی کنی</span></div>
                        <div class="col-3" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>عملیات</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"></div>
                </div>`;

                lastKM = 0;
                $.each(KMPayKaniBarAvord, function () {
                    KMExistingId = this.id;
                    FromKM = this.fromKM;
                    ToKM = this.toKM;
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
      <input type="text" class="form-control_1 PayKaniTextStyle  input-sm text-center" id="txtFromKMForPayKani${KMNum}" value="${FromKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- تا متراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 PayKaniTextStyle input-sm text-center" id="txtToKMForPayKani${KMNum}" value="${ToKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- حجم پی کنی -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 PayKaniTextStyle input-sm text-center" id="txtHajmPayKani${KMNum}" value="${Value}" onclick="event.stopPropagation();"/>
    </div>

        <div class="col-md-3" style="text-align:center">

            <button type="button" class="ButtonRowsViewStyle" title="مشاهده" onclick="ViewPayKaniInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}')">
                <i class="fa fa-eye ViewRMUStyle"></i>
            </button>
        
            <button type="button" title="ویرایش"
                onclick="event.stopPropagation();UpdatePayKaniInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum}` + `,2, this)"
                class="ButtonRowsSaveStyle">
                    <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
            </button>

            <button type="button" class="ButtonRowsViewStyle" title="حذف" onclick="event.stopPropagation();DeletePayKaniInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}', this)">
                <i class="fa fa-trash DelRMUStyle"></i>
            </button>
        
        </div>
   
    </div>
  <!-- بخش نمایش -->

  <div class="row col-12 StyleMainViewPayKani" style="direction:ltr;display:none"  id="MainViewPayKani${KMNum}" class="PayKani-view">
    <div id="ViewPayKani${KMNum}" class="PayKani-view" style="direction: rtl;">
    </div>
    <div class="col-md-1 action-col">
        
    </div>
  <div id="ViewRizMetrePK${KMNum}" style="direction: rtl;" class="col-12 PayKani-view"></div>
  <div id="ViewPayKaniEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 PayKani-view"></div>
  </div><!-- MainViewPayKani -->
  </div>
    `;
                });

                $('#divExistingKMPK').html(strSEKB);

                $('#divExistKMHeader' + lastKM).click();
            }
            else
                $('#divExistingKMPK').html('');

        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
        }
    });
}

function AutoFillThirdColumnPK(KMNum, PKDetail, changedId) {

    const $v = $("#txtPKVarizi" + KMNum);
    const $dv = $("#txtPKDarsadVarizi" + KMNum);

    const $r = $("#txtPKReUseHajm" + KMNum);
    const $dr = $("#txtPKReUseDarsad" + KMNum);

    const $h = $("#txtPKHaml" + KMNum);
    const $dh = $("#txtPKDarsadHaml" + KMNum);

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

const clamp = function (value, min, max) {
        return Math.max(min, Math.min(max, value));
    };

    const setValuePK = function ($input, value) {
        $input.val(parseFloat(Number(value).toFixed(2)));
        $input.removeClass("blinking");
    };


    // -----------------------------
    // KhDetail
    // -----------------------------

    PKDetail = toNumber(PKDetail);

    if (PKDetail <= 0) {
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
            PKDetail
        );

        let haml = clamp(
            toNumber($h.val()),
            0,
            PKDetail - reuse
        );

        let varizi = PKDetail - reuse - haml;

        if (varizi < 0) {
            varizi = 0;
        }


        // فقط Varizi محاسبه می‌شود
        // ReUse و Haml دست نمی‌خورند

        setValuePK($r, reuse);
        setValuePK($h, haml);
        setValuePK($v, varizi);


        // درصدها

        let reusePercent = (reuse / PKDetail) * 100;
        let hamlPercent = (haml / PKDetail) * 100;
        let variziPercent = (varizi / PKDetail) * 100;

        setValuePK($dr, reusePercent);
        setValuePK($dh, hamlPercent);
        setValuePK($dv, variziPercent);

        return;
    }


    // =========================================================
    // 2. تغییر حجم Haml
    // =========================================================

    if (changedId && changedId.includes("Haml")) {

        let reuse = clamp(
            toNumber($r.val()),
            0,
            PKDetail
        );

        // این قسمت مهم است:
        // Haml فقط نسبت به ReUse محدود می‌شود
        // نه نسبت به Varizi قبلی
        let haml = clamp(
            toNumber($h.val()),
            0,
            PKDetail - reuse
        );

        let varizi = PKDetail - reuse - haml;

        if (varizi < 0) {
            varizi = 0;
        }


        // ReUse اصلاً تغییر نمی‌کند
        setValuePK($r, reuse);

        // Haml همان مقداری که کاربر وارد کرده
        setValuePK($h, haml);

        // Varizi باقیمانده است
        setValuePK($v, varizi);


        // درصدها

        let reusePercent = (reuse / PKDetail) * 100;
        let hamlPercent = (haml / PKDetail) * 100;
        let variziPercent = (varizi / PKDetail) * 100;

        setValuePK($dr, reusePercent);
        setValuePK($dh, hamlPercent);
        setValuePK($dv, variziPercent);

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

        let reuse = (reusePercent / 100) * PKDetail;
        let haml = (hamlPercent / 100) * PKDetail;
        let varizi = (variziPercent / 100) * PKDetail;


        setValuePK($r, reuse);
        setValuePK($h, haml);
        setValuePK($v, varizi);


        setValuePK($dr, reusePercent);
        setValuePK($dh, hamlPercent);
        setValuePK($dv, variziPercent);

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

        let reuse = (reusePercent / 100) * PKDetail;
        let haml = (hamlPercent / 100) * PKDetail;
        let varizi = (variziPercent / 100) * PKDetail;


        // ReUse تغییر نمی‌کند
        setValuePK($dr, reusePercent);
        setValuePK($r, reuse);


        // Haml همان مقدار وارد شده
        setValuePK($dh, hamlPercent);
        setValuePK($h, haml);


        // فقط Varizi باقیمانده است
        setValuePK($dv, variziPercent);
        setValuePK($v, varizi);

        return;
    }


    // =========================================================
    // 5. اگر تابع بدون changedId اجرا شد
    // مقدار Varizi از ReUse و Haml محاسبه می‌شود
    // =========================================================

    let reuse = clamp(
        toNumber($r.val()),
        0,
        PKDetail
    );

    let haml = clamp(
        toNumber($h.val()),
        0,
        PKDetail - reuse
    );

    let varizi = PKDetail - reuse - haml;

    if (varizi < 0) {
        varizi = 0;
    }


    // درصدها

    let reusePercent = (reuse / PKDetail) * 100;
    let hamlPercent = (haml / PKDetail) * 100;
    let variziPercent = (varizi / PKDetail) * 100;


    // خروجی نهایی

    setValuePK($r, reuse);
    setValuePK($h, haml);
    setValuePK($v, varizi);

    setValuePK($dr, reusePercent);
    setValuePK($dh, hamlPercent);
    setValuePK($dv, variziPercent);
}


function AutoFillThirdColumnPKForEdit(KMNum, i, PKDetail, changedId) {

    const $v = $("#txtPKVarizi" + KMNum + "_" + i);
    const $dv = $("#txtPKDarsadVarizi" + KMNum + "_" + i);

    const $r = $("#txtPKReUseHajm" + KMNum + "_" + i);
    const $dr = $("#txtPKReUseDarsad" + KMNum + "_" + i);

    const $h = $("#txtPKHaml" + KMNum + "_" + i);
    const $dh = $("#txtPKDarsadHaml" + KMNum + "_" + i);

const clamp = (x, min, max) => {
        return Math.max(min, Math.min(max, x));
    };

    const n = (x) => {
        const value = parseFloat(x);
        return isNaN(value) ? 0 : value;
    };

    const setValuePK = ($input, value) => {
        $input.val(parseFloat(Number(value).toFixed(2)));
        $input.removeClass("blinking");
    };

    PKDetail = n(PKDetail);

    if (PKDetail <= 0) {
        return;
    }


    // =========================================================
    // ردیف های 1 و 2
    // ReUse کاملاً از محاسبات حذف می شود
    //
    // Varizi + Haml = KhDetail
    // Varizi% + Haml% = 100
    // =========================================================

    // const isNoReuseRow = (i === "1" || i === "2");

    // if (isNoReuseRow) {

    //     // ReUse همیشه صفر
    //     setValuePK($r, 0);
    //     setValuePK($dr, 0);


    //     // -----------------------------------------------------
    //     // تغییر درصد واریزی
    //     // -----------------------------------------------------

    //     if (changedId &&
    //         changedId.includes("DarsadVarizi" + KMNum)) {

    //         let variziPercent = clamp(
    //             n($dv.val()),
    //             0,
    //             100
    //         );

    //         let hamlPercent = 100 - variziPercent;

    //         let variziVolume =
    //             (variziPercent / 100) * PKDetail;

    //         let hamlVolume =
    //             (hamlPercent / 100) * PKDetail;

    //         setValuePK($dv, variziPercent);
    //         setValuePK($dh, hamlPercent);

    //         setValuePK($v, variziVolume);
    //         setValuePK($h, hamlVolume);

    //         return;
    //     }


    //     // -----------------------------------------------------
    //     // تغییر درصد حمل
    //     // -----------------------------------------------------

    //     if (changedId &&
    //         changedId.includes("DarsadHaml" + KMNum)) {

    //         let hamlPercent = clamp(
    //             n($dh.val()),
    //             0,
    //             100
    //         );

    //         let variziPercent = 100 - hamlPercent;

    //         let hamlVolume =
    //             (hamlPercent / 100) * PKDetail;

    //         let variziVolume =
    //             (variziPercent / 100) * PKDetail;

    //         setValuePK($dh, hamlPercent);
    //         setValuePK($dv, variziPercent);

    //         setValuePK($h, hamlVolume);
    //         setValuePK($v, variziVolume);

    //         return;
    //     }


    //     // -----------------------------------------------------
    //     // تغییر حجم واریزی
    //     // -----------------------------------------------------

    //     if (changedId &&
    //         changedId.includes("Varizi" + KMNum) &&
    //         !changedId.includes("Darsad")) {

    //         let variziVolume = clamp(
    //             n($v.val()),
    //             0,
    //             PKDetail
    //         );

    //         let hamlVolume =
    //             PKDetail - variziVolume;

    //         let variziPercent =
    //             (variziVolume / PKDetail) * 100;

    //         let hamlPercent =
    //             (hamlVolume / PKDetail) * 100;

    //         setValuePK($v, variziVolume);
    //         setValuePK($h, hamlVolume);

    //         setValuePK($dv, variziPercent);
    //         setValuePK($dh, hamlPercent);

    //         return;
    //     }


    //     // -----------------------------------------------------
    //     // تغییر حجم حمل
    //     // -----------------------------------------------------

    //     if (changedId &&
    //         changedId.includes("Haml" + KMNum) &&
    //         !changedId.includes("Darsad")) {

    //         let hamlVolume = clamp(
    //             n($h.val()),
    //             0,
    //             PKDetail
    //         );

    //         let variziVolume =
    //             PKDetail - hamlVolume;

    //         let hamlPercent =
    //             (hamlVolume / PKDetail) * 100;

    //         let variziPercent =
    //             (variziVolume / PKDetail) * 100;

    //         setValuePK($h, hamlVolume);
    //         setValuePK($v, variziVolume);

    //         setValuePK($dh, hamlPercent);
    //         setValuePK($dv, variziPercent);

    //         return;
    //     }


    //     // -----------------------------------------------------
    //     // حالت اولیه / بدون تغییر مشخص
    //     // -----------------------------------------------------

    //     let variziVolume = clamp(
    //         n($v.val()),
    //         0,
    //         PKDetail
    //     );

    //     let hamlVolume = PKDetail - variziVolume;

    //     let variziPercent =
    //         (variziVolume / PKDetail) * 100;

    //     let hamlPercent =
    //         (hamlVolume / PKDetail) * 100;

    //     setValuePK($v, variziVolume);
    //     setValuePK($h, hamlVolume);

    //     setValuePK($dv, variziPercent);
    //     setValuePK($dh, hamlPercent);

    //     return;
    // }


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

        setValuePK(
            $v,
            (dv / 100) * PKDetail
        );
    }

    if (changedId &&
        changedId.includes("ReUseDarsad" + KMNum)) {

        let dr = clamp(n($dr.val()), 0, 100);

        setValuePK(
            $r,
            (dr / 100) * PKDetail
        );
    }

    if (changedId &&
        changedId.includes("DarsadHaml" + KMNum)) {

        let dh = clamp(n($dh.val()), 0, 100);

        setValuePK(
            $h,
            (dh / 100) * PKDetail
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
        changedId.includes("PKReUseHajm" + KMNum) &&
        !changedId.includes("Darsad")) {

        let haml = clamp(
            n($h.val()),
            0,
            PKDetail
        );

        let reuse = clamp(
            n($r.val()),
            0,
            PKDetail - haml
        );

        let varizi = PKDetail - reuse - haml;

        setValuePK($r, reuse);
        setValuePK($h, haml);
        setValuePK($v, varizi);

        setValuePK($dr, (reuse / PKDetail) * 100);
        setValuePK($dh, (haml / PKDetail) * 100);
        setValuePK($dv, (varizi / PKDetail) * 100);

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
            PKDetail
        );

        let haml = clamp(
            n($h.val()),
            0,
            PKDetail - reuse
        );

        let varizi = PKDetail - reuse - haml;

        setValuePK($r, reuse);
        setValuePK($h, haml);
        setValuePK($v, varizi);

        setValuePK($dr, (reuse / PKDetail) * 100);
        setValuePK($dh, (haml / PKDetail) * 100);
        setValuePK($dv, (varizi / PKDetail) * 100);

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
            (haml / 100) * PKDetail;

        let reuseVolume =
            (reusePercent / 100) * PKDetail;

        let variziVolume =
            PKDetail - reuseVolume - hamlVolume;

        let variziPercent =
            (variziVolume / PKDetail) * 100;

        setValuePK($dr, reusePercent);
        setValuePK($dh, haml);

        setValuePK($r, reuseVolume);
        setValuePK($h, hamlVolume);

        setValuePK($v, variziVolume);
        setValuePK($dv, variziPercent);

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
            (reusePercent / 100) * PKDetail;

        let hamlVolume =
            (hamlPercent / 100) * PKDetail;

        let variziVolume =
            PKDetail - reuseVolume - hamlVolume;

        let variziPercent =
            (variziVolume / PKDetail) * 100;

        setValuePK($dr, reusePercent);
        setValuePK($dh, hamlPercent);

        setValuePK($r, reuseVolume);
        setValuePK($h, hamlVolume);

        setValuePK($v, variziVolume);
        setValuePK($dv, variziPercent);

        return;
    }

    // ---------------------------------------------------------
    // حالت پیش فرض
    // ---------------------------------------------------------

    let reuse = clamp(
        n($r.val()),
        0,
        PKDetail
    );

    let haml = clamp(
        n($h.val()),
        0,
        PKDetail - reuse
    );

    let varizi =
        PKDetail - reuse - haml;

    setValuePK($r, reuse);
    setValuePK($h, haml);
    setValuePK($v, varizi);

    setValuePK(
        $dr,
        (reuse / PKDetail) * 100
    );

    setValuePK(
        $dh,
        (haml / PKDetail) * 100
    );

    setValuePK(
        $dv,
        (varizi / PKDetail) * 100
    );
}



function ViewPayKaniInfo(KMExistingId, KMNum, BarAvordId) {

    if ($('#MainViewPayKani' + KMNum).is(':visible')) {
        $('#MainViewPayKani' + KMNum).slideUp(500);

        return
    }

    $('#ViewPayKaniEzafeBaha' + KMNum).html('');

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.payKaniInfoForBarAvordId = KMExistingId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.Type = 2;

    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/GetDetailsOfKMPayKaniInfoWithKMPayKaniId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            KMPayKaniBarAvordDetailsMore = response.kmPayKaniBarAvordDetailsMore;
            KMPayKaniBarAvordDetails = response.kmPayKaniBarAvordDetails;
            lstPKInfoRizMetre = response.lstPayKaniInfoRizMetre;
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;

            Value = 0;
            strKMAK = `

    <div id="divPayKaniInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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
      <span>شرح عملیات پی کنی</span>
      </div>
    </div>

    <!-- ستون محتوا -->
    <div class="col-8" style="padding:0;">
      <div class="row" style="margin:0;">

        <!-- حجم پی کنی -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;">
            <span>حجم پی کنی</span>
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
            $.each(KMPayKaniBarAvordDetails, function () {
                strKMAK += `
        <div class="container-fluid" style="width:100%;margin:0;padding:0;">
        <div class="row AmalyatKhakiRowStyle">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
    <input id="txtPayKaniItemId${KMNum}_${i}" value="${this.noePayKaniId}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanPayKaniItems${KMNum}_${i}" class="spanStylePayKaniItems">
        ${this.title}
      </span>
    </div>`;

                let result = KMPayKaniBarAvordDetailsMore.filter(x => x.payKaniInfoForBarAvordDetailsId === this.id);

                let KhDetail = result.filter(x => x.name === "KhDetail").length > 0 ? result.filter(x => x.name === "KhDetail")[0].value : 0;
                let DarsadKhDetail = result.filter(x => x.name === "DarsadKhDetail").length > 0 ? result.filter(x => x.name === "DarsadKhDetail")[0].value : 0;
                KhDetail = KhDetail == null ? 0 : KhDetail;
                DarsadKhDetail = DarsadKhDetail == null ? 0 : DarsadKhDetail;

                // جمع کردن مقادیر
                totalKhDetail += parseFloat(KhDetail);
                totalDarsadKhDetail += parseFloat(DarsadKhDetail);


                strKMAK += ` <div class="col-8" style="padding:0;">
          <div class="row" style="margin:0;">

        <!-- حجم پی کنی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKDetail${KMNum}_${i}" value="${KhDetail == 0 ? '' : KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKDarsad${KMNum}_${i}" value="${DarsadKhDetail == 0 ? '' : DarsadKhDetail}" />
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
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKHaml${KMNum}_${i}" value="${Haml == 0 ? '' : Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKDarsadHaml${KMNum}_${i}" value="${DarsadHaml == 0 ? '' : DarsadHaml}"  />
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
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKReUseHajm${KMNum}_${i}" value="${ReUseHajm == 0 ? '' : ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" id="txtPKReUseDarsad${KMNum}_${i}" value="${DarsadReUseHajm == 0 ? '' : DarsadReUseHajm}"  />
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
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" disabled  id="txtPKVarizi${KMNum}_${i}" value="${Varizi == 0 ? '' : Varizi}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm PayKaniTextStyle" disabled id="txtPKDarsadVarizi${KMNum}_${i}" value="${DarsadVarizi == 0 ? '' : DarsadVarizi}"  />
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
        
        <!-- حجم پی کنی -->
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


            $('div[id^="MainViewPayKani"]').slideUp();
            //$('div[id^="ViewRizMetreKH"]').slideUp();

            $('#ViewPayKani' + KMNum).html(strKMAK);
            $('#MainViewPayKani' + KMNum).slideDown();

            HajmPayKani = $('#txtHajmPayKani' + KMNum).val();


            $('#ViewPayKani' + KMNum + ' input[type="text"]').off('change').change(function () {
                let changedId = $(this).attr("id");
                let i = changedId.split("_")[1];
                let HajmPayKani = parseFloat($.trim($('#txtHajmPayKani' + KMNum).val()));

                // 🚨 اعتبارسنجی
                if (!$.isNumeric($(this).val())) {
                    toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                } else {
                    $(this).removeClass('blinking');
                }

                if (HajmPayKani == 0 || HajmPayKani == '' || !$.isNumeric(HajmPayKani)) {
                    toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmPayKani' + KMNum).addClass('blinking');
                    return;
                } else {
                    $('#txtHajmPayKani' + KMNum).removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                PKDetail = parseFloat($("#txtPKDetail" + KMNum + "_" + i).val()) || 0;

                if (PKDetail > 0) {

                    $("#txtPKVarizi" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtPKVarizi" + KMNum + "_" + i).val() != "" ? $("#txtPKVarizi" + KMNum + "_" + i).removeClass('blinking') : $("#txtPKVarizi" + KMNum + "_" + i).addClass('blinking');
                    $("#txtPKReUseHajm" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtPKReUseHajm" + KMNum + "_" + i).val() != "" ? $("#txtPKReUseHajm" + KMNum + "_" + i).removeClass('blinking') : $("#txtPKReUseHajm" + KMNum + "_" + i).addClass('blinking');
                    $("#txtPKHaml" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtPKHaml" + KMNum + "_" + i).val() != "" ? $("#txtPKHaml" + KMNum + "_" + i).removeClass('blinking') : $("#txtPKHaml" + KMNum + "_" + i).addClass('blinking');

                    $("#txtPKDarsadVarizi" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtPKDarsadVarizi" + KMNum + "_" + i).val() != "" ? $("#txtPKDarsadVarizi" + KMNum + "_" + i).removeClass('blinking') : $("#txtPKDarsadVarizi" + KMNum + "_" + i).addClass('blinking');
                    $("#txtPKReUseDarsad" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtPKReUseDarsad" + KMNum + "_" + i).val() != "" ? $("#txtPKReUseDarsad" + KMNum + "_" + i).removeClass('blinking') : $("#txtPKReUseDarsad" + KMNum + "_" + i).addClass('blinking');
                    $("#txtPKDarsadHaml" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtPKDarsadHaml" + KMNum + "_" + i).val() != "" ? $("#txtPKDarsadHaml" + KMNum + "_" + i).removeClass('blinking') : $("#txtPKDarsadHaml" + KMNum + "_" + i).addClass('blinking');
                }

                let varizi = parseFloat($("#txtPKVarizi" + KMNum + "_" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtPKReUseHajm" + KMNum + "_" + i).val()) || 0;
                let haml = parseFloat($("#txtPKHaml" + KMNum + "_" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtPKDarsadVarizi" + KMNum + "_" + i).val()) || 0;
                let dReuse = parseFloat($("#txtPKReUseDarsad" + KMNum + "_" + i).val()) || 0;
                let dHaml = parseFloat($("#txtPKDarsadHaml" + KMNum + "_" + i).val()) || 0;


                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("Detail" + KMNum)) {
                    let Zarb = HajmPayKani === 0 ? 0 : (PKDetail / HajmPayKani) * 100;
                    $("#txtPKDarsad" + KMNum + "_" + i).val(Zarb.toFixed(0));

                    let SumAll = ReturnSumAllDetailsPayKaniForEditPK(KMNum);
                    if (SumAll > HajmPayKani) {
                        let NewVal = HajmPayKani - (SumAll - PKDetail);
                        $("#txtPKDetail" + KMNum + "_" + i).val(NewVal.toFixed(2));
                        PKDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmPayKani === 0 ? 0 : (PKDetail / HajmPayKani) * 100;
                        $("#txtPKDarsad" + KMNum + "_" + i).val(Zarb.toFixed(0));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtPKVarizi" + KMNum + "_" + i).val(dVarizi > 0 ? ((dVarizi / 100) * PKDetail).toFixed(2) : "");
                    $("#txtPKReUseHajm" + KMNum + "_" + i).val(dReuse > 0 ? ((dReuse / 100) * PKDetail).toFixed(2) : "");
                    $("#txtPKHaml" + KMNum + "_" + i).val(dHaml > 0 ? ((dHaml / 100) * PKDetail).toFixed(2) : "");
                }

                if (changedId.includes("txtPKDarsad" + KMNum)) {
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
                    let newKhDetail = (Zarb / 100) * HajmPayKani;
                    $('#txtPKDetail' + KMNum + "_" + i).val(newKhDetail.toFixed(2));


                    // // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtPKVarizi" + KMNum + "_" + i).val(dVarizi > 0 ? ((dVarizi / 100) * newKhDetail).toFixed(2) : "");
                    $("#txtPKReUseHajm" + KMNum + "_" + i).val(dReuse > 0 ? ((dReuse / 100) * newKhDetail).toFixed(2) : "");
                    $("#txtPKHaml" + KMNum + "_" + i).val(dHaml > 0 ? ((dHaml / 100) * newKhDetail).toFixed(2) : "");

                }

                if (changedId.includes("Darsad" + KMNum)
                    && !changedId.includes("Varizi" + KMNum)
                    && !changedId.includes("ReUse" + KMNum)
                    && !changedId.includes("Haml" + KMNum)) {
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
                     //let newKhDetail = (dVal / 100) * HajmPayKani;
                     //$('#txtKhDetail' + KMNum + "_" + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (PKDetail > 0) {
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        varizi = (dVarizi / 100) * PKDetail;
                        $("#txtPKVarizi" + KMNum + "_" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        reuseHajm = (dReuse / 100) * PKDetail;
                        $("#txtPKReUseHajm" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        haml = (dHaml / 100) * PKDetail;
                        $("#txtPKHaml" + KMNum + "_" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dVarizi = PKDetail === 0 ? 0 : (varizi / PKDetail) * 100;
                        $("#txtPKDarsadVarizi" + KMNum + "_" + i).val(dVarizi.toFixed(0));
                        $("#txtPKDarsadVarizi" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        dReuse = PKDetail === 0 ? 0 : (reuseHajm / PKDetail) * 100;
                        $("#txtPKReUseDarsad" + KMNum + "_" + i).val(dReuse.toFixed(0));
                        $("#txtPKReUseDarsad" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dHaml = PKDetail === 0 ? 0 : (haml / PKDetail) * 100;
                        $("#txtPKDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(0));
                        $("#txtPKDarsadHaml" + KMNum + "_" + i).removeClass('blinking');
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtPKVarizi" + KMNum + "_" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtPKReUseHajm" + KMNum + "_" + i).val()) || 0;
                haml = parseFloat($("#txtPKHaml" + KMNum + "_" + i).val()) || 0;

                dVarizi = parseFloat($("#txtPKDarsadVarizi" + KMNum + "_" + i).val()) || 0;
                dReuse = parseFloat($("#txtPKReUseDarsad" + KMNum + "_" + i).val()) || 0;
                dHaml = parseFloat($("#txtPKDarsadHaml" + KMNum + "_" + i).val()) || 0;


                PKDetail = parseFloat($("#txtPKDetail" + KMNum + "_" + i).val()) || 0;


                AutoFillThirdColumnPKForEdit(KMNum, i, PKDetail, changedId);

                SumKol = sumKolNewPK(ActivityLength, KMNum);
                $('#spantotalPKDetail' + KMNum).html(toPersianDigits(SumKol[0].toFixed(2)));
                $('#spantotalDarsadPKDetail'+KMNum).html(toPersianDigits(SumKol[1].toFixed(0)) + " % ");

                $('#spantotalVarizi' + KMNum).html(toPersianDigits(SumKol[2].toFixed(2)));

                DarsadVariziNew = (parseFloat(SumKol[2]) / parseFloat(HajmPayKani)) * 100;
                $('#spantotalDarsadVarizi' + KMNum).html(toPersianDigits(DarsadVariziNew.toFixed(2)) + " % ");

                $('#spantotalReUseHajm' + KMNum).html(toPersianDigits(SumKol[3].toFixed(2)));
                DarsadReUseHajmNew = (parseFloat(SumKol[3]) / parseFloat(HajmPayKani)) * 100;
                $('#spantotalDarsadReUseHajm' + KMNum).html(toPersianDigits(DarsadReUseHajmNew.toFixed(2)) + " % ");

                $('#spantotalHaml' + KMNum).html(toPersianDigits(SumKol[4].toFixed(2)));
                DarsadHamlNew = (parseFloat(SumKol[4]) / parseFloat(HajmPayKani)) * 100;
                $('#spantotalDarsadHaml' + KMNum).html(toPersianDigits(DarsadHamlNew.toFixed(2)) + " % ");

            });


            // $('#txtHajmPK' + KMNum).off('change').change(function () {
            

            //     let HKB = parseFloat($(this).val());

            //     if (!$.isNumeric(HKB) || HKB <= 0) {
            //         toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
            //         $(this).addClass('blinking');
            //         return;
            //     }

            //     $('#MainViewPayKaniNew').slideDown(500);
            //     $('#divPayKaniInfoDetails').show();
            //     $(this).removeClass('blinking');

            //     let totalAssigned = 0;
            //     let lastIndex = -1;

            //     for (let i = 1; i <= ActivityLength; i++) {
            //         let Darsad = parseFloat($('#txtPKDarsad' + KMNum + "_" + i).val()) || 0;

            //         if (Darsad > 0) {
            //             let KhDetail = (Darsad / 100 * HKB);

            //             // حجم اصلی
            //             $('#txtPKDetail' + KMNum + "_" + i).val(KhDetail.toFixed(2));

            //             // درصدهای مرتبط
            //             let dVarizi = parseFloat($('#txtPKDarsadVarizi' + KMNum + "_" + i).val()) || 0;
            //             let dReUse = parseFloat($('#txtPKReUseDarsad' + KMNum + "_" + i).val()) || 0;
            //             let dHaml = parseFloat($('#txtPKDarsadHaml' + KMNum + "_" + i).val()) || 0;

            //             // محاسبه حجم هر بخش
            //             let vVarizi = (dVarizi / 100) * KhDetail;
            //             let vReUse = (dReUse / 100) * KhDetail;
            //             let vHaml = (dHaml / 100) * KhDetail;

            //             // ست کردن نتایج
            //             $('#txtPKVarizi' + KMNum + "_" + i).val(vVarizi.toFixed(2));
            //             $('#txtPKReUseHajm' + KMNum + "_" + i).val(vReUse.toFixed(2));
            //             $('#txtPKHaml' + KMNum + "_" + i).val(vHaml.toFixed(2));

            //             totalAssigned += parseFloat(KhDetail.toFixed(2));
            //             lastIndex = i;
            //         }
            //     }

            //     // جبران خطای رندینگ روی آخرین ردیف
            //     if (lastIndex > -1) {
            //         let diff = HKB - totalAssigned;
            //         if (Math.abs(diff) >= 0.01) {
            //             let lastVal = parseFloat($('#txtPKDetail' + KMNum + "_" + lastIndex).val()) || 0;
            //             let newVal = lastVal + diff;

            //             $('#txtPKDetail' + KMNum + "_" + lastIndex).val(newVal.toFixed(2));

            //             // بروزرسانی دوباره بخش‌های وابسته به این ردیف
            //             let dVarizi = parseFloat($('#txtPKDarsadVarizi' + KMNum + "_" + lastIndex).val()) || 0;
            //             let dReUse = parseFloat($('#txtPKReUseDarsad' + KMNum + "_" + lastIndex).val()) || 0;
            //             let dHaml = parseFloat($('#txtPKDarsadHaml' + KMNum + "_" + lastIndex).val()) || 0;

            //             $('#txtPKVarizi' + KMNum + "_" + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
            //             $('#txtPKReUseHajm' + KMNum + "_" + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
            //             $('#txtPKHaml' + KMNum + "_" + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
            //         }
            //     }

            //     UpdatePayKaniInfo(KMExistingId, BarAvordId, KMNum);

            // });




            ////////////////
            //////////////



            $('#txtHajmPayKani' + KMNum).off('change').on('change', function () {

                let HKB = parseFloat($(this).val());

                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم پی کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                $(this).removeClass('blinking');

                let totalAssigned = 0;
                let lastIndex = -1;

                for (let i = 1; i <= ActivityLength; i++) {

                    let darsad = parseFloat(
                        $('#txtPKDarsad' + KMNum + '_' + i).val()
                    ) || 0;

                    if (darsad > 0) {

                        // حجم جدید فعالیت بر اساس درصد آن
                        let khDetail = (darsad / 100) * HKB;

                        $('#txtPKDetail' + KMNum + '_' + i)
                            .val(khDetail.toFixed(2));

                        // درصدهای قبلی جزئیات
                        let dVarizi = parseFloat(
                            $('#txtPKDarsadVarizi' + KMNum + '_' + i).val()
                        ) || 0;

                        let dReUse = parseFloat(
                            $('#txtPKReUseDarsad' + KMNum + '_' + i).val()
                        ) || 0;

                        let dHaml = parseFloat(
                            $('#txtPKDarsadHaml' + KMNum + '_' + i).val()
                        ) || 0;

                        // محاسبه حجم‌های جدید
                        let vVarizi = (dVarizi / 100) * khDetail;
                        let vReUse = (dReUse / 100) * khDetail;
                        let vHaml = (dHaml / 100) * khDetail;

                        // ثبت مقادیر
                        $('#txtPKVarizi' + KMNum + '_' + i)
                            .val(vVarizi.toFixed(2));

                        $('#txtPKReUseHajm' + KMNum + '_' + i)
                            .val(vReUse.toFixed(2));

                        $('#txtPKHaml' + KMNum + '_' + i)
                            .val(vHaml.toFixed(2));

                        totalAssigned += parseFloat(khDetail.toFixed(2));
                        lastIndex = i;
                    }
                }

                // اصلاح اختلاف ناشی از گرد کردن
                if (lastIndex > 0) {

                    let diff = HKB - totalAssigned;

                    if (Math.abs(diff) >= 0.01) {

                        let lastVal = parseFloat(
                            $('#txtPKDetail' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        let newVal = lastVal + diff;

                        $('#txtPKDetail' + KMNum + '_' + lastIndex)
                            .val(newVal.toFixed(2));

                        let dVarizi = parseFloat(
                            $('#txtPKDarsadVarizi' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        let dReUse = parseFloat(
                            $('#txtPKReUseDarsad' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        let dHaml = parseFloat(
                            $('#txtPKDarsadHaml' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        $('#txtPKVarizi' + KMNum + '_' + lastIndex)
                            .val(((dVarizi / 100) * newVal).toFixed(2));

                        $('#txtPKReUseHajm' + KMNum + '_' + lastIndex)
                            .val(((dReUse / 100) * newVal).toFixed(2));

                        $('#txtPKHaml' + KMNum + '_' + lastIndex)
                            .val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }

                // به‌روزرسانی جمع کل‌ها
                let SumKol = sumKolNewPK(ActivityLength, KMNum);

                $('#spantotalKhDetail' + KMNum)
                    .html(toPersianDigits(SumKol[0].toFixed(2)));

                $('#spantotalDarsadKhDetail' + KMNum)
                    .html(toPersianDigits(SumKol[1].toFixed(0)) + ' %');

                $('#spantotalVarizi' + KMNum)
                    .html(toPersianDigits(SumKol[2].toFixed(2)));

                $('#spantotalReUseHajm' + KMNum)
                    .html(toPersianDigits(SumKol[3].toFixed(2)));

                $('#spantotalHaml' + KMNum)
                    .html(toPersianDigits(SumKol[4].toFixed(2)));

                // ثبت اطلاعات
                UpdatePayKaniInfo(KMExistingId, BarAvordId, KMNum,2);

            });

            ///////////////////
            /////////////

            ShowRizMetrePK(KMExistingId, lstPKInfoRizMetre, lstItemFBShomarehForGet, KMNum);


            ShowA_PKEzafeBaha(KMExistingId, KMNum);

            KMpayKaniBarAvordDetails = response.kmpayKaniBarAvordDetails;
            $.each(KMpayKaniBarAvordDetails, function () {
                Id = this.id;
                payKaniInfoForBarAvordId = this.payKaniInfoForBarAvordId;
                Type = this.type;
            });

            KMAmalyateRizeshBarAvordDetailsMore = response.kmAmalyateRizeshBarAvordDetailsMore;
            $.each(KMAmalyateRizeshBarAvordDetailsMore, function () {
                CurrentId = this.id;
                Name = $.trim(this.name);
                ValueMore = this.value;
                payKaniInfoForBarAvordDetailsId = this.payKaniInfoForBarAvordDetailsId;
                if (Id == payKaniInfoForBarAvordDetailsId) {
                    $('#txt' + Name + Type).val(ValueMore);
                }
            });

            CurrentValue = $('#txtPKDetail' + Type).val();
            ValueOfReCycle = $('#txtPKReUseHajm' + Type).val();
            ValueOfVarize = $('#txtPKVarizi' + Type).val();
            ValueOfHaml = $('#txtPKHaml' + Type).val();
            ValueOfFaseleHaml = $('#txtPKFaseleHaml' + Type).val();

            $('#txtPKDarsad' + Type).val(parseFloat(Value) == 0 ? 0 : (parseFloat(CurrentValue) / parseFloat(Value) * 100).toFixed(0));
            $('#txtPKReUseDarsad' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtPKDarsadVarizi' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtPKDarsadHaml' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtPKDarsadFaseleHaml' + Type).val(parseFloat(ValueOfVarize) == 0 ? 0 : (parseFloat(ValueOfFaseleHaml) / parseFloat(ValueOfVarize) * 100).toFixed(0));

            KMAmalyateRizeshBarAvordDetailsEzafeBaha = response.kmAmalyateRizeshBarAvordDetailsEzafeBaha;
            $.each(KMAmalyateRizeshBarAvordDetailsEzafeBaha, function () {
                CurrentId = this.id;
                Name = $.trim(this.name);
                boolValue = this.value == 'true' ? true : false;
                payKaniInfoForBarAvordDetailsId = this.payKaniInfoForBarAvordDetailsId;

                if (Id == payKaniInfoForBarAvordDetailsId) {
                    $('#ck' + Name + Type).attr('checked', boolValue);
                }
            });


        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری متراژ انتخابی', 'خطا');
        }
    });

    $('#ViewPayKani' + KMNum).html();
}

function ShowRizMetrePK(KMExistingId, lstPKInfoRizMetre, lstItemFBShomarehForGet, KMNum) {
    var str = '';

    // ساخت HTML بر اساس lstItemFBShomarehForGet

    // گروه‌بندی data.lst بر اساس itemFBShomareh
    let groupedData = {};
    lstPKInfoRizMetre.forEach(function (row) {
        if (!groupedData[row.itemFBShomareh]) {
            groupedData[row.itemFBShomareh] = [];
        }
        groupedData[row.itemFBShomareh].push(row);
    });

    // ساخت HTML بر اساس lstItemFBShomarehForGet
    if (lstPKInfoRizMetre.length != 0) {

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

        $targetDivRizMetrePK = $('#ViewRizMetrePK' + KMNum);

        $targetDivRizMetrePK.html(str);
        $targetDivRizMetrePK.slideDown();



        $targetDivRizMetrePK.find("input[type='text'].essentialValue")
            .filter(function () {
                return $(this).val().trim() === "";
            })
            .addClass("blinking")
            .first()
            .focus();


        $targetDivRizMetrePK.on("change", "input[type='text'].essentialValue", function () {
            if ($(this).val().trim() !== "") {
                $(this).removeClass("blinking");
            }
        });


        $targetDivRizMetrePK.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
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

function ReturnSumAllDetailsPK() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtPKDetail' + i).val()) == '' ? '0' : $.trim($('#txtPKDetail' + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllDetailsPayKaniForEditPK(KMNum) {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtPKDetail' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtPKDetail' + KMNum + "_" + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllRBDetailsPK() {
    sumAll = 0;
    for (var i = 1; i < 3; i++) {
        sumAll += parseFloat($.trim($('#txtRBDetail' + i).val()) == '' ? '0' : $.trim($('#txtRBDetail' + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllDardadPK() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtDarsad' + i).val()) == '' ? '0' : $.trim($('#txtDarsad' + i).val()));
    }
    return parseFloat(sumAll);
}
function ReturnSumAllDardadPKForEdit(KMNum) {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtDarsad' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtDarsad' + KMNum + "_" + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllRBDardadPK() {
    sumAll = 0;
    for (var i = 1; i < 3; i++) {
        sumAll += parseFloat($.trim($('#txtRBDarsad' + i).val()) == '' ? '0' : $.trim($('#txtRBDarsad' + i).val()));
    }
    return parseFloat(sumAll);
}

function SumAllDetailsPayKani() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtPKDetail' + i).val()) == '' ? '0' : $.trim($('#txtPKDetail' + i).val()));
    }

    HajmPayKani = parseFloat($('#txtHajmPayKani').val());
    if (sumAll > HajmPayKani) {
        $('#txtHajmPayKani').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم پی کنی بیشتر باشد', 'اطلاع');
        return true;
    }
    else if (sumAll < HajmPayKani) {
        $('#txtHajmPayKani').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم پی کنی کمتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmPayKani').removeClass('blinking');
        return false;
    }
}


function SumAllDetailsPayKaniForEditPK(KMNum) {

    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtPKDetail' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtPKDetail' + KMNum + "_" + i).val()));
    }

    HajmPayKani = parseFloat($('#txtHajmPayKani' + KMNum).val());
    if (sumAll > HajmPayKani) {
        $('#txtHajmPayKani' + KMNum).addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم پی کنی بیشتر باشد', 'اطلاع');
        return true;
    }
    else if (sumAll < HajmPayKani) {
        $('#txtHajmPayKani' + KMNum).addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم پی کنی کمتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmPayKani' + KMNum).removeClass('blinking');
        return false;
    }
}


function CheckValuesOfPayKaniDetails() {
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
// پاک کردن خطاهای یک ردیف
    const clearRowErrors = (i) => {
        const currentRow = $('#txtPKDarsadHaml' + i)
            .closest('.AmalyatKhakiRowStyle');

        currentRow.removeClass('errorRow');

        $('#txtPKReUseHajm' + i).removeClass('blinking');
        $('#txtPKVarizi' + i).removeClass('blinking');
        $('#txtPKHaml' + i).removeClass('blinking');

        $('#txtPKReUseDarsad' + i).removeClass('blinking');
        $('#txtPKDarsadVarizi' + i).removeClass('blinking');
        $('#txtPKDarsadHaml' + i).removeClass('blinking');
    };

    // علامت زدن مقادیر حجم
    const blinkVolumes = (i) => {
        if (i != 1 && i != 2) {
            $('#txtPKReUseHajm' + i).addClass('blinking');
        }

        $('#txtPKVarizi' + i).addClass('blinking');
        $('#txtPKHaml' + i).addClass('blinking');
    };

    for (var i = 1; i <= ActivityLength; i++) {

        var KhDetail = getValue('txtPKDetail' + i);

        var ReUseHajm = getValue('txtPKReUseHajm' + i);
        var ReUseDarsad = getValue('txtPKReUseDarsad' + i);

        var Varizi = getValue('txtPKVarizi' + i);
        var DarsadVarizi = getValue('txtPKDarsadVarizi' + i);

        var Haml = getValue('txtPKHaml' + i);
        var DarsadHaml = getValue('txtPKDarsadHaml' + i);

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

            // جمع کمتر یا بیشتر از مقدار پی کنی است
            const currentRow = $('#txtPKDarsadHaml' + i)
                .closest('.AmalyatKhakiRowStyle');

            currentRow.addClass('errorRow');

            blinkVolumes(i);

            checkValues = true;

        }

        // بررسی مقادیر منفی یا بزرگ‌تر از مقدار کل
        if (ReUseHajm > KhDetail + TOLERANCE) {
            $('#txtPKReUseHajm' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKReUseHajm' + i).removeClass('blinking');
        }

        // درصد ReUse
        if (ReUseDarsad > 100) {
            $('#txtPKReUseDarsad' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKReUseDarsad' + i).removeClass('blinking');
        }

        // واریزی
        if (Varizi > KhDetail + TOLERANCE) {
            $('#txtPKVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKVarizi' + i).removeClass('blinking');
        }

        // حمل
        if (Haml > KhDetail + TOLERANCE) {
            $('#txtPKHaml' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKHaml' + i).removeClass('blinking');
        }

        // درصد واریزی
        if (DarsadVarizi > 100) {
            $('#txtPKDarsadVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKDarsadVarizi' + i).removeClass('blinking');
        }

        // درصد حمل
        if (DarsadHaml > 100) {
            $('#txtPKDarsadHaml' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKDarsadHaml' + i).removeClass('blinking');
        }
    }

    return checkValues;
}


function CheckValuesOfPayKaniDetailsForEditPK(KMNum) {
    checkValues = false;

    // حداکثر اختلاف مجاز
    const TOLERANCE = 0.01;

    // مقایسه دو عدد اعشاری با تلورانس
// گرفتن مقدار عددی از input
    const getValue = (id) => {
        const value = parseFloat($('#' + id).val());
        return isNaN(value) ? 0 : value;
    };

    for (var i = 1; i <= ActivityLength; i++) {

        JamKol = 0;
        JamDarsad = 0;

        var prefix = KMNum + "_" + i;

        var KhDetail = parseFloat($('#txtPKDetail' + prefix).val());

        // اگر KhDetail عدد نیست، این ردیف بررسی نشود
        if (isNaN(KhDetail))
            continue;

        // ==========================================
        // ReUse
        // ==========================================

        var ReUseHajm = getValue('txtPKReUseHajm' + prefix);

        if (ReUseHajm > KhDetail + TOLERANCE) {
            $('#txtPKReUseHajm' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKReUseHajm' + prefix).removeClass('blinking');
        }

        JamKol += ReUseHajm;


        var ReUseDarsad = getValue('txtPKReUseDarsad' + prefix);

        if (ReUseDarsad > 100 + TOLERANCE) {
            $('#txtPKReUseDarsad' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKReUseDarsad' + prefix).removeClass('blinking');
        }

        JamDarsad += ReUseDarsad;


        // ==========================================
        // Varizi
        // ==========================================

        var Varizi = getValue('txtPKVarizi' + prefix);

        if (Varizi > KhDetail + TOLERANCE) {
            $('#txtPKVarizi' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKVarizi' + prefix).removeClass('blinking');
        }

        JamKol += Varizi;


        var DarsadVarizi = getValue('txtPKDarsadVarizi' + prefix);

        if (DarsadVarizi > 100 + TOLERANCE) {
            $('#txtPKDarsadVarizi' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKDarsadVarizi' + prefix).removeClass('blinking');
        }

        JamDarsad += DarsadVarizi;


        // ==========================================
        // Haml
        // ==========================================

        var Haml = getValue('txtPKHaml' + prefix);

        if (Haml > KhDetail + TOLERANCE) {
            $('#txtPKHaml' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKHaml' + prefix).removeClass('blinking');
        }

        JamKol += Haml;


        var DarsadHaml = getValue('txtPKDarsadHaml' + prefix);

        if (DarsadHaml > 100 + TOLERANCE) {
            $('#txtPKDarsadHaml' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtPKDarsadHaml' + prefix).removeClass('blinking');
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

                var currentRow = $('#txtPKDarsadHaml' + prefix)
                    .closest('.AmalyatKhakiRowStyle');

                currentRow.addClass('errorRow');

                checkValues = true;
            }
            else {

                // مقدارها قابل قبول هستند
                var currentRow = $('#txtPKDarsadHaml' + prefix)
                    .closest('.AmalyatKhakiRowStyle');

                currentRow.removeClass('errorRow');
            }
        }
        else {

            // اگر KhDetail صفر است و خطای قبلی داشته،
            // خطای ردیف پاک شود
            var currentRow = $('#txtPKDarsadHaml' + prefix)
                .closest('.AmalyatKhakiRowStyle');

            currentRow.removeClass('errorRow');
        }
    }

    return checkValues;
}

function SavePayKaniInfo(BarAvordUserId, Type, buttonElement) {

    check = false;
    //////////
    HKB = $('#txtHajmPayKani').val();
    if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
        $('#txtHajmPayKani').addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmPayKani').removeClass('blinking');
    }
   
    /////////////
    var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForPayKani').addClass('blinking');
        toastr.info('متراژ انتها بایستی بعد از متراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForPayKani').removeClass('blinking');
    }

    ///////////////
// ورودی خالی معادل صفر است؛ اما متن غیرعددی نباید ذخیره شود.
    $('#divPayKaniInfoDetails input[type="text"]').each(function () {
        const value = $.trim($(this).val());
        const invalid = value !== '' && !$.isNumeric(value);
        $(this).toggleClass('blinking', invalid);
        check = check || invalid;
    });

    check = SumAllDetailsPayKani() || check;

    //if (check) {
    //    toastr.info('مجموع احجام وارد شده بایستی با حجم کل برابر باشد', 'اطلاع');
    //    return 0;
    //}

    check1 = CheckValuesOfPayKaniDetails();

    //if (check1) {
    //toastr.info('احجام وارد شده درست نمی باشند', 'اطلاع');
    //}
    if (!check1 && !check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            let obj = {
                DetailValue: $('#txtPKDetail' + i).val(),
                DarsadValue: $('#txtPKDarsad' + i).val(),
                DetailValueOfReCycle: $('#txtPKReUseHajm' + i).val(),
                DarsadValueOfReCycle: $('#txtPKReUseDarsad' + i).val(),
                DetailValueOfVarize: $('#txtPKVarizi' + i).val(),
                DarsadValueOfVarize: $('#txtPKDarsadVarizi' + i).val(),
                DetailValueOfHaml: $('#txtPKHaml' + i).val(),
                DarsadValueOfHaml: $('#txtPKDarsadHaml' + i).val(),
                NoeKhakBardari: '10003', //$('#txtPayKaniItemId' + i).val(),
            };

            dataList.push(obj);
        }


        NoeFBId = parseInt($('#HDFNoeFB').val());

        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Type = 2;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.NoeFBId = NoeFBId;


const requestKey = 'save:' + BarAvordUserId;
        if (!beginPayKaniRequest(requestKey, buttonElement)) return;
$.ajax({
            type: "POST",
            url: "/payKaniInfoForBarAvords/SavePayKaniInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    //$('#HDFStatepayKaniSaveOrEdit').val('Edit');
                    //$('#HDFKMpayKaniIdForEdit').val(info[1]);
                    //$('#HDFKMpayKaniNum').val(info[2]);

                    $('#txtHajmPayKani').val(0);
                    $('#txtFromKMForPayKani').val('0')
                    $('#txtToKMForPayKani').val('0')

                    for (let i = 1; i <= ActivityLength; i++) {
                        $('#txtPKDetail' + i).val('');
                        $('#txtPKDarsad' + i).val('');
                        $('#txtPKReUseHajm' + i).val('');
                        $('#txtPKReUseDarsad' + i).val('');
                        $('#txtPKVarizi' + i).val('');
                        $('#txtPKDarsadVarizi' + i).val('');
                        $('#txtPKHaml' + i).val('');
                        $('#txtPKDarsadHaml' + i).val('');
                        //$('#txtPayKaniItemId' + i).val(0);
                    }

                    $('#spantotalPKDetailNew').html('');
                    $('#spantotalPKDarsadDetailNew').html('');
                    $('#spantotalPKVariziNew').html('');
                    $('#spantotalPKDarsadVariziNew').html('');
                    $('#spantotalPKReUseHajmNew').html('');
                    $('#spantotalPKDarsadReUseHajmNew').html('');
                    $('#spantotalPKHamlNew').html('');
                    $('#spantotalPKDarsadHamlNew').html('');

                    $('#MainViewPayKaniNew').slideUp(500);

                    ShowExistingKMPayKani(BarAvordUserId);
toastr.success('اطلاعات متراژ بدرستی ثبت گردید', 'ثبت');
                }
                else {
                    toastr.error('مشکل در ثبت اطلاعات متراژ', 'خطا');
}
            },
            error: function (response) {
                toastr.error('مشکل در ثبت اطلاعات متراژ', 'خطا');
},
            complete: function () {
endPayKaniRequest(requestKey);
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}

function UpdatePayKaniInfo(KMPayKaniId, BarAvordUserId, KMNum, Type, buttonElement) {


    check = false;
    //////////
    HKB = $('#txtHajmPayKani' + KMNum).val();
    if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
        $('#txtHajmPayKani' + KMNum).addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmPayKani' + KMNum).removeClass('blinking');
    }
    ////////////////
    
    var KME = parseFloat($('#txtToKMForPayKani' + KMNum).val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForPayKani' + KMNum).val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForPayKani' + KMNum).addClass('blinking');
        toastr.info('متراژ انتها بایستی بعد از متراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForPayKani' + KMNum).removeClass('blinking');
    }

    ///////////////
// فقط ورودی‌های فرم کیلومتراژ جاری بررسی شوند.
    $('#ViewPayKani' + KMNum + ' input[type="text"]').each(function () {
        const value = $.trim($(this).val());
        const invalid = value !== '' && !$.isNumeric(value);
        $(this).toggleClass('blinking', invalid);
        check = check || invalid;
    });

    check = SumAllDetailsPayKaniForEditPK(KMNum) || check;
    //if (check) {
    //    toastr.info('حجم کل پی کنی صحیح نمیباشد', 'اطلاع');
    //}

    check1 = CheckValuesOfPayKaniDetailsForEditPK(KMNum);
    //if (check1) {
    //    toastr.info('مشکل در مقادیر وارده', 'اطلاع');
    //}
    if (!check1 && !check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            var DarsadValue = $('#txtPKDarsad' + KMNum + "_" + i).val().trim();
            if (DarsadValue != "0") {
                let obj = {
                    DetailValue: $('#txtPKDetail' + KMNum + "_" + i).val(),
                    DarsadValue: DarsadValue,
                    DetailValueOfReCycle: $('#txtPKReUseHajm' + KMNum + "_" + i).val(),
                    DarsadValueOfReCycle: $('#txtPKReUseDarsad' + KMNum + "_" + i).val(),
                    DetailValueOfVarize: $('#txtPKVarizi' + KMNum + "_" + i).val(),
                    DarsadValueOfVarize: $('#txtPKDarsadVarizi' + KMNum + "_" + i).val(),
                    DetailValueOfHaml: $('#txtPKHaml' + KMNum + "_" + i).val(),
                    DarsadValueOfHaml: $('#txtPKDarsadHaml' + KMNum + "_" + i).val(),
                    NoePayKani:10003,
                };
                dataList.push(obj);
            }
        }

        Year = parseInt($('#HDFYear').val());
        NoeFBId = parseInt($('#HDFNoeFB').val());
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMPayKaniId = KMPayKaniId;
        vardata.KMNum = KMNum;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.Year = Year;
        vardata.NoeFBId = NoeFBId;
        vardata.Type = Type;


const requestKey = 'record:' + KMPayKaniId;
        if (!beginPayKaniRequest(requestKey, buttonElement)) return;
$.ajax({
            type: "POST",
            url: "/payKaniInfoForBarAvords/UpdatePayKaniInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    toastr.success('اطلاعات متراژ بدرستی ویرایش گردید', 'ثبت');
                    $('#MainViewPayKani' + KMNum).slideUp(500);
                    $('#divExistKMHeader' + KMNum).click();
//$('#ViewPayKaniEzafeBaha' + KMNum).html('');

                }
                else {
                    toastr.error('مشکل در ویرایش اطلاعات متراژ', 'خطا');
}
            },
            error: function (response) {
                toastr.error('مشکل در ویرایش اطلاعات متراژ', 'خطا');
},
            complete: function () {
endPayKaniRequest(requestKey);
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}

function DeletePayKaniInfo(KMPayKaniId, KMNum, BarAvordUserId, buttonElement) {

    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.KMPayKaniId = KMPayKaniId;
    vardata.Year = Year;
    vardata.KMNum = KMNum;


    const requestKey = 'record:' + KMPayKaniId;
    if (!beginPayKaniRequest(requestKey, buttonElement)) return;
$.ajax({
        type: "POST",
        url: "/payKaniInfoForBarAvords/DeletePayKaniInfoForBarAvord",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('اطلاعات متراژ بدرستی حذف گردید', 'ثبت');
                ShowExistingKMPayKani(BarAvordUserId);
            }
            else {
                toastr.error('مشکل در حذف اطلاعات', 'خطا');
}
        },
        error: function (response) {
            toastr.error('مشکل در حذف اطلاعات', 'خطا');
},
        complete: function () {
endPayKaniRequest(requestKey);
        }
    });

}


function sumKolNewPK(ActivityLength, KMNum) {
    var result = [];
    JamKolKhDetail = 0;
    JamKolDarsadKhDetail = 0;

    JamKolVarizi = 0;
    JamKolDarsadVarizi = 0;

    JamKolReUseHajm = 0;
    JamKolReUseDarsad = 0;

    JamKolHaml = 0;
    JamKolDarsadHaml = 0;

    strKMNum = KMNum == 0 ? '' : KMNum + "_";

    for (let i = 0; i < ActivityLength; i++) {
        currentKhDetail = $.trim($('#txtPKDetail' + strKMNum + (i + 1)).val());
        if (currentKhDetail != '') {
            KhDetail = parseFloat(currentKhDetail);
            KhDetail = isNaN(KhDetail) ? 0 : KhDetail;
            JamKolKhDetail += KhDetail;
        }
        currentDarsadKhDetail = $.trim($('#txtPKDarsad' + strKMNum + (i + 1)).val());
        if (currentDarsadKhDetail != '') {
            DarsadKhDetail = parseFloat(currentDarsadKhDetail);
            DarsadKhDetail = isNaN(DarsadKhDetail) ? 0 : DarsadKhDetail;
            JamKolDarsadKhDetail += DarsadKhDetail;
        }
        currentVarizi = $.trim($('#txtPKVarizi' + strKMNum + (i + 1)).val());
        if (currentVarizi != '') {
            Varizi = parseFloat(currentVarizi);
            Varizi = isNaN(Varizi) ? 0 : Varizi;
            JamKolVarizi += Varizi;
        }

        currentReUseHajm = $.trim($('#txtPKReUseHajm' + strKMNum + (i + 1)).val());
        if (currentReUseHajm != '') {
            ReUseHajm = parseFloat(currentReUseHajm);
            ReUseHajm = isNaN(ReUseHajm) ? 0 : ReUseHajm;
            JamKolReUseHajm += ReUseHajm;
        }

        currentHaml = $.trim($('#txtPKHaml' + strKMNum + (i + 1)).val());
        if (currentHaml != '') {
            Haml = parseFloat(currentHaml);
            Haml = isNaN(Haml) ? 0 : Haml;
            JamKolHaml += Haml;
        }

    }
    result.push(JamKolKhDetail);
    result.push(JamKolDarsadKhDetail);
    result.push(JamKolVarizi);
    result.push(JamKolReUseHajm);
    result.push(JamKolHaml);

    return result;
}


