ActivityLength = 0;

/* انتخاب خودکار مقدار ورودی‌ها با کلیک؛ شامل ورودی‌های داینامیک */
$(document)
    .off('click.chanelKaniSelectInput', '#ViewChanelKaniNew input[type="text"], [id^="ViewChanelKani"] input[type="text"], #divChanelKaniInfoDetails input[type="text"], input[id^="txtFromKMForChanelKani"], input[id^="txtToKMForChanelKani"], input[id^="txtHajmChanelKani"]')
    .on('click.chanelKaniSelectInput', '#ViewChanelKaniNew input[type="text"], [id^="ViewChanelKani"] input[type="text"], #divChanelKaniInfoDetails input[type="text"], input[id^="txtFromKMForChanelKani"], input[id^="txtToKMForChanelKani"], input[id^="txtHajmChanelKani"]', function () {
        if (!this.disabled && !this.readOnly) this.select();
    });

/* جلوگیری از ارسال درخواست تکراری و مدیریت وضعیت دکمه‌ها */
const chanelKaniPendingRequests = Object.create(null);
let chanelKaniActiveRequestCount = 0;

function beginChanelKaniRequest(requestKey, buttonElement) {
    if (chanelKaniPendingRequests[requestKey]) return false;
    const $button = buttonElement ? $(buttonElement) : $();
    if ($button.length && $button.prop('disabled')) return false;
    if ($button.length) {
        $button.data('chanelKaniOriginalHtml', $button.html());
        $button.data('chanelKaniOriginalCursor', $button.css('cursor'));
        $button.data('chanelKaniOriginalOpacity', $button.css('opacity'));
        $button.prop('disabled', true).attr('aria-busy', 'true')
            .addClass('ChanelKaniButtonLoading')
            .css({ cursor: 'wait', opacity: 0.7 })
            .html('<i class="fa fa-spinner fa-spin" aria-hidden="true"></i><span class="sr-only"> در حال انجام...</span>');
    }
    chanelKaniPendingRequests[requestKey] = $button;
    chanelKaniActiveRequestCount++;
    $('#ContainerWaiting').show();
    return true;
}

function endChanelKaniRequest(requestKey) {
    const $button = chanelKaniPendingRequests[requestKey];
    if ($button && $button.length) {
        const originalHtml = $button.data('chanelKaniOriginalHtml');
        const originalCursor = $button.data('chanelKaniOriginalCursor');
        const originalOpacity = $button.data('chanelKaniOriginalOpacity');
        if (originalHtml !== undefined) $button.html(originalHtml);
        $button.prop('disabled', false).removeAttr('aria-busy')
            .removeClass('ChanelKaniButtonLoading')
            .css({ cursor: originalCursor || '', opacity: originalOpacity || '' })
            .removeData('chanelKaniOriginalHtml')
            .removeData('chanelKaniOriginalCursor')
            .removeData('chanelKaniOriginalOpacity');
    }
    delete chanelKaniPendingRequests[requestKey];
    chanelKaniActiveRequestCount = Math.max(0, chanelKaniActiveRequestCount - 1);
    if (chanelKaniActiveRequestCount === 0) $('#ContainerWaiting').hide();
}

function ChanelKaniWithBarAvordIdClick(OpId,BarAvordUserId) {

    str = "";
    debugger;
    str += `

  <div class="col-12" id="divExistingKMChK">
  </div>


      <!-- متراژ های ثبت شده -->

    <div class="ChanelKani-container" style="border: 1px solid #cb92ff;background-color: #f3ebff;">
    <div class="row ChanelKani-box">

    <!-- از متراژ -->
    <div class="col-md-1 d-flex align-items-center justify-content-center" style="min-width:0;padding:4px 8px;box-sizing:border-box;overflow:hidden;">
        <i class="fa fa-plus" aria-hidden="true" style="display:inline-block;font-size:18px;line-height:1;color:#198754;margin:0;"></i>
    </div>
    <div class="col-md-1 label-col" style="padding-inline-start:8px;box-sizing:border-box;">
      <span>از متراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 ChanelKaniTextStyle input-sm text-center" id="txtFromKMForChanelKani" value="0" onclick="this.select();"/>
    </div>

    <!-- تا متراژ -->
    <div class="col-md-1 label-col">
      <span>تا متراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 ChanelKaniTextStyle input-sm text-center" id="txtToKMForChanelKani" value="0" onclick="this.select();"/>
    </div>

    <!-- حجم کانال کنی -->
    <div class="col-md-2 label-col">
      <span>حجم کانال کنی:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 ChanelKaniTextStyle input-sm text-center" id="txtHajmChanelKani" value="0" onclick="this.select();"/>
    </div>
    <div class="col-md-1 unit-col">
      <span>مترمکعب</span>
    </div>
  </div>

  <!-- بخش نمایش -->
  <div id="MainViewChanelKaniNew" class="ChanelKani-view" style="display:none">
  <div id="ViewChanelKaniNew" class="ChanelKani-view">
  </div>

 <!-- دکمه ذخیره -->
 <div class="row">
 <div class="col-12" style="direction: ltr;">

 <div class="col-md-1 action-col">
     <button type="button" id="btnSaveChanelKaniInfo"
            onclick="SaveChanelKaniInfo('${BarAvordUserId}'` + ',' + `'Add', this)"
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

    setTimeout(() => { $('#txtFromKMForChanelKani').focus().select(); }, 200);

    $('#ula' + OpId)
        .off('keydown.ChanelKani')
        .on('keydown.ChanelKani', '.ChanelKaniTextStyle', function (e) {
            if (e.key === 'Enter') {
                debugger;
                e.preventDefault(); // جلوگیری از submit فرم

                const inputs = $('.ChanelKaniTextStyle');
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
                        $('#btnSaveChanelKaniInfo').focus();
                    }
                }

                // اگر هیچ input فعالی نبود → برو روی دکمه
                $('.buttonStyleBoard').focus();
            }
        });


    ShowExistingKMChanelKani(BarAvordUserId);

    ShowSelctionChanelKani(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0);


}

function ShowSelctionChanelKani(IsNew, KMExistingId, KMNum, BarAvordId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value) {

    strSSKB = `

    <div id="divChanelKaniInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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

        <!-- حجم کانال کنی -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;">
            <span>حجم کانال کنی</span>
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
    //کانال کنی
    vardata.Type = 3;

    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/ReturnNoePayKani",
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
        <input id="txtChanelKaniItemId${i + 1}" value="${ActivityTitleComplete[i].id}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanChanelKaniItems${i + 1}" class="spanStyleChanelKaniItems">
        ${ActivityTitleComplete[i].title}
      </span>
    </div>

    <!-- ستون اصلی محتوا -->
    <div class="col-8" style="padding:0;">
      <div class="row" style="margin:0;">

        <!-- حجم کانال کنی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKDetail${i + 1}" value="" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKDarsad${i + 1}" value="" />
            </div>
          </div>
        </div>

      <!-- حمل به دپو/مسیر -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKHaml${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKDarsadHaml${i + 1}" value=""  />
            </div>
          </div>
        </div>
        
        <!-- قابل مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKReUseHajm${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKReUseDarsad${i + 1}" value=""  />
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" disabled id="txtChKVarizi${i + 1}" value=""  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" disabled id="txtChKDarsadVarizi${i + 1}" value=""  />
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

        <!-- حجم کانال کنی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKDetailNew"></span>
            </div>
            <div class="col-4" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKDarsadDetailNew"></span>
            </div>
          </div>
        </div>

    <!-- حمل به دپو -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKHamlNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKDarsadHamlNew"></span>
            </div>
          </div>
        </div>

        <!-- مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKReUseHajmNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKDarsadReUseHajmNew"></span>
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKVariziNew"></span>
            </div>
            <div class="col-6" style="text-align:center;padding:0 2px;">
              <span id="spantotalChKDarsadVariziNew"></span>
            </div>
          </div>
        </div>


      </div>
    </div>
  </div>
</div>`;
            //////////////
            $('#ViewChanelKaniNew').html(strSSKB);

            $('#ViewChanelKaniNew input[type="text"]').change(function () {

                debugger;

                let changedId = $(this).attr("id");
                let i = changedId.match(/\d+/) ? changedId.match(/\d+/)[0] : ""; // شماره ردیف
                let HajmChanelKani = parseFloat($.trim($('#txtHajmChanelKani').val()));

                // 🚨 اعتبارسنجی
                if (!$.isNumeric($(this).val())) {
                    toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }
                else {
                    $(this).removeClass('blinking');
                }

                if (HajmChanelKani == 0 || HajmChanelKani == '' || !$.isNumeric(HajmChanelKani)) {
                    toastr.info('حجم کانال کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmChanelKani').addClass('blinking');
                    return;
                } else {
                    $('#txtHajmChanelKani').removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                let ChKDetail = parseFloat($("#txtChKDetail" + i).val()) || 0;

                //if (ChKDetail != null || ChKDetail != 0) {
                if (ChKDetail > 0) {
                    $("#txtChKVarizi" + i).prop('disabled') ? '' : $("#txtChKVarizi" + i).val() != "" ? $("#txtChKVarizi" + i).removeClass('blinking') : $("#txtChKVarizi" + i).addClass('blinking');
                    $("#txtChKReUseHajm" + i).prop('disabled') ? '' : $("#txtChKReUseHajm" + i).val() != "" ? $("#txtChKReUseHajm" + i).removeClass('blinking') : $("#txtChKReUseHajm" + i).addClass('blinking');
                    $("#txtChKHaml" + i).prop('disabled') ? '' : $("#txtChKHaml" + i).val() != "" ? $("#txtChKHaml" + i).removeClass('blinking') : $("#txtChKHaml" + i).addClass('blinking');

                    $("#txtChKDarsadVarizi" + i).prop('disabled') ? '' : $("#txtChKDarsadVarizi" + i).val() != "" ? $("#txtChKDarsadVarizi" + i).removeClass('blinking') : $("#txtChKDarsadVarizi" + i).addClass('blinking');
                    $("#txtChKReUseDarsad" + i).prop('disabled') ? '' : $("#txtChKReUseDarsad" + i).val() != "" ? $("#txtChKReUseDarsad" + i).removeClass('blinking') : $("#txtChKReUseDarsad" + i).addClass('blinking');
                    $("#txtChKDarsadHaml" + i).prop('disabled') ? '' : $("#txtChKDarsadHaml" + i).val() != "" ? $("#txtChKDarsadHaml" + i).removeClass('blinking') : $("#txtChKDarsadHaml" + i).addClass('blinking');
                }

                let varizi = parseFloat($("#txtChKVarizi" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtChKReUseHajm" + i).val()) || 0;
                let haml = parseFloat($("#txtChKHaml" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtChKDarsadVarizi" + i).val()) || 0;
                let dReuse = parseFloat($("#txtChKReUseDarsad" + i).val()) || 0;
                let dHaml = parseFloat($("#txtChKDarsadHaml" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("Detail")) {
                    let Zarb = HajmChanelKani === 0 ? 0 : (ChKDetail / HajmChanelKani) * 100;
                    $("#txtChKDarsad" + i).val(Zarb.toFixed(2));

                    $('#spantotalChKDarsadDetailNew').html();

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmChanelKani) {
                        let NewVal = HajmChanelKani - (SumAll - ChKDetail);
                        $("#txtChKDetail" + i).val(NewVal.toFixed(2));
                        ChKDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmChanelKani === 0 ? 0 : (ChKDetail / HajmChanelKani) * 100;
                        $("#txtChKDarsad" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    // dVarizi === 0 ? 0 : ((dVarizi / 100) * ChKDetail).toFixed(2);
                    // $("#txtVarizi" + i).val(dVarizi);
                    // $("#txtReUseHajm" + i).val(((dReuse / 100) * ChKDetail).toFixed(2));
                    // $("#txtHaml" + i).val(((dHaml / 100) * ChKDetail).toFixed(2));


                    // حجم هر بخش بر اساس درصد
                    let vVarizi = (dVarizi / 100) * ChKDetail;
                    let vReUse = (dReuse / 100) * ChKDetail;
                    let vHaml = (dHaml / 100) * ChKDetail;

                    $("#txtChKVarizi" + i).val(vVarizi.toFixed(2));
                    $("#txtChKReUseHajm" + i).val(vReUse.toFixed(2));
                    $("#txtChKHaml" + i).val(vHaml.toFixed(2));
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
                    let newKhDetail = (dVal / 100) * HajmChanelKani;
                    $('#txtChKDetail' + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (ChKDetail > 0) {
                    if (changedId.includes("DarsadVarizi")) {
                        varizi = (dVarizi / 100) * ChKDetail;
                        $("#txtChKVarizi" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        reuseHajm = (dReuse / 100) * ChKDetail;
                        $("#txtChKReUseHajm" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        haml = (dHaml / 100) * ChKDetail;
                        $("#txtChKHaml" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        dVarizi = (ChKDetail === 0 ? 0 : (varizi / ChKDetail)) * 100;
                        $("#txtChKDarsadVarizi" + i).val(dVarizi.toFixed(2));
                        $("#txtChKDarsadVarizi" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (ChKDetail === 0 ? 0 : (reuseHajm / ChKDetail)) * 100;
                        $("#txtChKReUseDarsad" + i).val(dReuse.toFixed(0));
                        $("#txtChKReUseDarsad" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (ChKDetail === 0 ? 0 : (haml / ChKDetail)) * 100;
                        $("#txtChKDarsadHaml" + i).val(dHaml.toFixed(0));
                        $("#txtChKDarsadHaml" + i).removeClass('blinking');
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtChKVarizi" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtChKReUseHajm" + i).val()) || 0;
                haml = parseFloat($("#txtChKHaml" + i).val()) || 0;

                dVarizi = parseFloat($("#txtChKDarsadVarizi" + i).val()) || 0;
                dReuse = parseFloat($("#txtChKReUseDarsad" + i).val()) || 0;
                dHaml = parseFloat($("#txtChKDarsadHaml" + i).val()) || 0;

               

                AutoFillThirdColumnChK(i, ChKDetail, changedId);

                SumKol = sumKolNewChK(ActivityLength,0);
                $('#spantotalChKDetailNew').html(toPersianDigits(SumKol[0].toFixed(2)));
                $('#spantotalChKDarsadDetailNew').html(toPersianDigits(SumKol[1].toFixed(0)) + " % ");

                $('#spantotalChKVariziNew').html(toPersianDigits(SumKol[2].toFixed(2)));

                DarsadVariziNew = (parseFloat(SumKol[2]) / parseFloat(HajmChanelKani)) * 100;
                $('#spantotalChKDarsadVariziNew').html(toPersianDigits(DarsadVariziNew.toFixed(2)) + " % ");

                $('#spantotalChKReUseHajmNew').html(toPersianDigits(SumKol[3].toFixed(2)));
                DarsadReUseHajmNew = (parseFloat(SumKol[3]) / parseFloat(HajmChanelKani)) * 100;
                $('#spantotalChKDarsadReUseHajmNew').html(toPersianDigits(DarsadReUseHajmNew.toFixed(2)) + " % ");

                $('#spantotalChKHamlNew').html(toPersianDigits(SumKol[4].toFixed(2)));
                DarsadHamlNew = (parseFloat(SumKol[4]) / parseFloat(HajmChanelKani)) * 100;
                $('#spantotalChKDarsadHamlNew').html(toPersianDigits(DarsadHamlNew.toFixed(2)) + " % ");
            });

            $('#txtHajmChanelKani').on('keydown', function (e) {
                if (e.key === 'Enter') {
                    e.preventDefault();

                    let HKB = parseFloat($(this).val());

                    var KMS = parseFloat($('#txtFromKMForChanelKani').val());//.replace('+', ''));
                    var KME = parseFloat($('#txtToKMForChanelKani').val());//.replace('+', ''));

                    if (KMS < 0 || KME == 0) {
                        $('#txtFromKMForChanelKani').addClass('blinking');
                        $('#txtToKMForChanelKani').addClass('blinking');
                        return;
                    }

                    if (KMS > KME) {
                        toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
                        $('#txtToKMForChanelKani').addClass('blinking');
                        $('#txtFromKMForChanelKani').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtToKMForChanelKani').removeClass('blinking');
                        $('#txtFromKMForChanelKani').removeClass('blinking');
                    }

                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم کانال کنی وارد شده نامعتبر میباشد', 'اطلاع');
                        $(this).addClass('blinking');
                        return;
                    }

                    $('#MainViewChanelKaniNew').slideDown(500);
                }
            });

            $('#txtHajmChanelKani').change(function () {
                let HKB = parseFloat($(this).val());

                var KMS = parseFloat($('#txtFromKMForChanelKani').val());//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForChanelKani').val());//.replace('+', ''));

                if (KMS < 0 || KME == 0) {
                    $('#txtFromKMForChanelKani').addClass('blinking');
                    $('#txtToKMForChanelKani').addClass('blinking');
                    return;
                }

                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForChanelKani').addClass('blinking');
                    $('#txtFromKMForChanelKani').addClass('blinking');
                    return;
                }
                else {
                    $('#txtToKMForChanelKani').removeClass('blinking');
                    $('#txtFromKMForChanelKani').removeClass('blinking');
                }

                //OverLowKMCheck(KMS, KME);


                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم کانال کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                $('#MainViewChanelKaniNew').slideDown(500);

                debugger;
                $(this).closest('ul').find('.StyleMainViewChanelKani').each(function () {
                    debugger;
                    $(this).slideUp(500);
                });

                $('#divChanelKaniInfoDetails').show();
                $(this).removeClass('blinking');

                // ابتدا تمام ردیف‌ها صفر شوند
                for (let i = 1; i <= ActivityLength; i++) {

                    $('#txtChKDetail' + i).val('0.00');
                    $('#txtChKDarsad' + i).val('0');

                    $('#txtChKVarizi' + i).val('0.00');
                    $('#txtChKReUseHajm' + i).val('0.00');
                    $('#txtChKHaml' + i).val('0.00');
                }

                // کل حجم در ردیف اول
                $('#txtChKDetail1').val(HKB.toFixed(2));
                $('#txtChKDarsad1').val('100');

                // درصدهای قبلی جزئیات ردیف اول حفظ شوند
                let dVarizi = parseFloat($('#txtChKDarsadVarizi1').val()) || 0;
                let dReUse = parseFloat($('#txtChKReUseDarsad1').val()) || 0;
                let dHaml = parseFloat($('#txtChKDarsadHaml1').val()) || 0;

                // محاسبه مجدد حجم‌ها بر اساس درصدهای خودشان
                $('#txtChKVarizi1').val(
                    (HKB * dVarizi / 100).toFixed(2)
                );

                $('#txtChKReUseHajm1').val(
                    (HKB * dReUse / 100).toFixed(2)
                );

                $('#txtChKHaml1').val(
                    (HKB * dHaml / 100).toFixed(2)
                );

                // جبران خطای رندینگ روی آخرین ردیف
                if (lastIndex > -1) {
                    let diff = HKB - totalAssigned;
                    if (Math.abs(diff) >= 0.01) {
                        let lastVal = parseFloat($('#txtChKDetail' + lastIndex).val()) || 0;
                        let newVal = lastVal + diff;

                        $('#txtChKDetail' + lastIndex).val(newVal.toFixed(2));

                        // بروزرسانی دوباره بخش‌های وابسته به این ردیف
                        let dVarizi = parseFloat($('#txtChKDarsadVarizi' + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtChKReUseDarsad' + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtChKDarsadHaml' + lastIndex).val()) || 0;

                        $('#txtChKVarizi' + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtChKReUseHajm' + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtChKHaml' + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }
            });

            $('#txtFromKMForChanelKani').change(function () {
                var KM = $(this).val();

                var KMS = parseFloat(KM);//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForChanelKani').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForChanelKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForChanelKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmChanelKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم کانال کنی وارد شده نامعتبر میباشد', 'اطلاع');
                        $('#txtHajmChanelKani').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtHajmChanelKani').removeClass('blinking');
                    }

                    $('#MainViewChanelKaniNew').slideDown(500);
                    $('#divChanelKaniInfoDetails').show();
                }
            });

            $('#txtToKMForChanelKani').change(function () {
                var KM = $(this).val();
               


                var KME = parseFloat(KM);//.replace('+', ''));
                var KMS = parseFloat($('#txtFromKMForChanelKani').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('متراژ خاتمه قبل از متراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForChanelKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForChanelKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmChanelKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم کانال کنی وارد شده نامعتبر میباشد', 'اطلاع');
                        $('#txtHajmChanelKani').addClass('blinking');
                        return;
                    }
                    else {
                        $('#txtHajmChanelKani').removeClass('blinking');
                    }

                    $('#MainViewChanelKaniNew').slideDown(500);
                    $('#divChanelKaniInfoDetails').show();
                }
            });
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری پی کنی', 'خطا');
        }
    });
}

function ShowExistingKMChanelKani(BarAvordUserId) {
    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.Type = 3;
    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/GetExistingKMPayKaniInfoWithBarAvordId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var KMChanelKaniBarAvord = response;
            if (KMChanelKaniBarAvord.length > 0) {
                strSEKB = `
                 <div class="row col-12 ExistKhBHeaderStyle">
                        <div class="col-1" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>ردیف</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;" ><span>از متراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>تا متراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>حجم کانال کنی</span></div>
                        <div class="col-3" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>عملیات</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"></div>
                </div>`;

                lastKM = 0;
                debugger;

                $.each(KMChanelKaniBarAvord, function () {
                    debugger;

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
      <input type="text" class="form-control_1 ChanelKaniTextStyle  input-sm text-center" id="txtFromKMForChanelKani${KMNum}" value="${FromKM}" onclick="this.select(); event.stopPropagation();"/>
    </div>

    <!-- تا متراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 ChanelKaniTextStyle input-sm text-center" id="txtToKMForChanelKani${KMNum}" value="${ToKM}" onclick="this.select(); event.stopPropagation();"/>
    </div>

    <!-- حجم کانال کنی -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 ChanelKaniTextStyle input-sm text-center" id="txtHajmChanelKani${KMNum}" value="${Value}" onclick="this.select(); event.stopPropagation();"/>
    </div>

        <div class="col-md-3" style="text-align:center">

            <button class="ButtonRowsViewStyle" title="مشاهده" onclick="ViewChanelKaniInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}')">
                <i class="fa fa-eye ViewRMUStyle"></i>
            </button>
        
            <button type="button" title="ویرایش"
                onclick="event.stopPropagation(); UpdateChanelKaniInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum}`+ `,3, this)"
                class="ButtonRowsSaveStyle">
                    <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
            </button>

            <button class="ButtonRowsViewStyle" title="حذف" onclick="DeleteChanelKaniInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}', this)">
                <i class="fa fa-trash DelRMUStyle"></i>
            </button>
        
        </div>
   
    </div>
  <!-- بخش نمایش -->

  <div class="row col-12 StyleMainViewChanelKani" style="direction:ltr;display:none"  id="MainViewChanelKani${KMNum}" class="ChanelKani-view">
    <div id="ViewChanelKani${KMNum}" class="ChanelKani-view" style="direction: rtl;">
    </div>
    <div class="col-md-1 action-col">
        
    </div>
  <div id="ViewRizMetreChK${KMNum}" style="direction: rtl;" class="col-12 ChanelKani-view"></div>
  <div id="ViewChanelKaniEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 ChanelKani-view"></div>
  </div><!-- MainViewChanelKani -->
  </div>
    `;
                });

                $('#divExistingKMChK').html(strSEKB);

                $('#divExistKMHeader' + lastKM).click();
            }
            else
                $('#divExistingKMChK').html('');

        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
        }
    });
}

function AutoFillThirdColumnChK(KMNum, ChKDetail, changedId) {

    const $v = $("#txtChKVarizi" + KMNum);
    const $dv = $("#txtChKDarsadVarizi" + KMNum);

    const $r = $("#txtChKReUseHajm" + KMNum);
    const $dr = $("#txtChKReUseDarsad" + KMNum);

    const $h = $("#txtChKHaml" + KMNum);
    const $dh = $("#txtChKDarsadHaml" + KMNum);

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

    const setValueChK = function ($input, value) {
        $input.val(parseFloat(Number(value).toFixed(2)));
        $input.removeClass("blinking");
    };


    // -----------------------------
    // KhDetail
    // -----------------------------

    ChKDetail = toNumber(ChKDetail);

    if (ChKDetail <= 0) {
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
            ChKDetail
        );

        let haml = clamp(
            toNumber($h.val()),
            0,
            ChKDetail - reuse
        );

        let varizi = ChKDetail - reuse - haml;

        if (varizi < 0) {
            varizi = 0;
        }


        // فقط Varizi محاسبه می‌شود
        // ReUse و Haml دست نمی‌خورند

        setValueChK($r, reuse);
        setValueChK($h, haml);
        setValueChK($v, varizi);


        // درصدها

        let reusePercent = (reuse / ChKDetail) * 100;
        let hamlPercent = (haml / ChKDetail) * 100;
        let variziPercent = (varizi / ChKDetail) * 100;

        setValueChK($dr, reusePercent);
        setValueChK($dh, hamlPercent);
        setValueChK($dv, variziPercent);

        return;
    }


    // =========================================================
    // 2. تغییر حجم Haml
    // =========================================================

    if (changedId && changedId.includes("Haml")) {

        let reuse = clamp(
            toNumber($r.val()),
            0,
            ChKDetail
        );

        // این قسمت مهم است:
        // Haml فقط نسبت به ReUse محدود می‌شود
        // نه نسبت به Varizi قبلی
        let haml = clamp(
            toNumber($h.val()),
            0,
            ChKDetail - reuse
        );

        let varizi = ChKDetail - reuse - haml;

        if (varizi < 0) {
            varizi = 0;
        }


        // ReUse اصلاً تغییر نمی‌کند
        setValueChK($r, reuse);

        // Haml همان مقداری که کاربر وارد کرده
        setValueChK($h, haml);

        // Varizi باقیمانده است
        setValueChK($v, varizi);


        // درصدها

        let reusePercent = (reuse / ChKDetail) * 100;
        let hamlPercent = (haml / ChKDetail) * 100;
        let variziPercent = (varizi / ChKDetail) * 100;

        setValueChK($dr, reusePercent);
        setValueChK($dh, hamlPercent);
        setValueChK($dv, variziPercent);

        return;
    }


    // =========================================================
    // 3. تغییر درصد ReUse
    // =========================================================

    if (changedId && changedId.includes("ReUseDarsadChK")) {

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

        let reuse = (reusePercent / 100) * ChKDetail;
        let haml = (hamlPercent / 100) * ChKDetail;
        let varizi = (variziPercent / 100) * ChKDetail;


        setValueChK($r, reuse);
        setValueChK($h, haml);
        setValueChK($v, varizi);


        setValueChK($dr, reusePercent);
        setValueChK($dh, hamlPercent);
        setValueChK($dv, variziPercent);

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

        let reuse = (reusePercent / 100) * ChKDetail;
        let haml = (hamlPercent / 100) * ChKDetail;
        let varizi = (variziPercent / 100) * ChKDetail;


        // ReUse تغییر نمی‌کند
        setValueChK($dr, reusePercent);
        setValueChK($r, reuse);


        // Haml همان مقدار وارد شده
        setValueChK($dh, hamlPercent);
        setValueChK($h, haml);


        // فقط Varizi باقیمانده است
        setValueChK($dv, variziPercent);
        setValueChK($v, varizi);

        return;
    }


    // =========================================================
    // 5. اگر تابع بدون changedId اجرا شد
    // مقدار Varizi از ReUse و Haml محاسبه می‌شود
    // =========================================================

    let reuse = clamp(
        toNumber($r.val()),
        0,
        ChKDetail
    );

    let haml = clamp(
        toNumber($h.val()),
        0,
        ChKDetail - reuse
    );

    let varizi = ChKDetail - reuse - haml;

    if (varizi < 0) {
        varizi = 0;
    }


    // درصدها

    let reusePercent = (reuse / ChKDetail) * 100;
    let hamlPercent = (haml / ChKDetail) * 100;
    let variziPercent = (varizi / ChKDetail) * 100;


    // خروجی نهایی

    setValueChK($r, reuse);
    setValueChK($h, haml);
    setValueChK($v, varizi);

    setValueChK($dr, reusePercent);
    setValueChK($dh, hamlPercent);
    setValueChK($dv, variziPercent);
}


function AutoFillThirdColumnChKForEdit(KMNum, i, ChKDetail, changedId) {

    const $v = $("#txtChKVarizi" + KMNum + "_" + i);
    const $dv = $("#txtChKDarsadVarizi" + KMNum + "_" + i);

    const $r = $("#txtChKReUseHajm" + KMNum + "_" + i);
    const $dr = $("#txtChKReUseDarsad" + KMNum + "_" + i);

    const $h = $("#txtChKHaml" + KMNum + "_" + i);
    const $dh = $("#txtChKDarsadHaml" + KMNum + "_" + i);

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

    const setValueChK = ($input, value) => {
        $input.val(parseFloat(Number(value).toFixed(2)));
        $input.removeClass("blinking");
    };

    ChKDetail = n(ChKDetail);

    if (ChKDetail <= 0) {
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
    //     setValueChK($r, 0);
    //     setValueChK($dr, 0);


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
    //             (variziPercent / 100) * ChKDetail;

    //         let hamlVolume =
    //             (hamlPercent / 100) * ChKDetail;

    //         setValueChK($dv, variziPercent);
    //         setValueChK($dh, hamlPercent);

    //         setValueChK($v, variziVolume);
    //         setValueChK($h, hamlVolume);

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
    //             (hamlPercent / 100) * ChKDetail;

    //         let variziVolume =
    //             (variziPercent / 100) * ChKDetail;

    //         setValueChK($dh, hamlPercent);
    //         setValueChK($dv, variziPercent);

    //         setValueChK($h, hamlVolume);
    //         setValueChK($v, variziVolume);

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
    //             ChKDetail
    //         );

    //         let hamlVolume =
    //             ChKDetail - variziVolume;

    //         let variziPercent =
    //             (variziVolume / ChKDetail) * 100;

    //         let hamlPercent =
    //             (hamlVolume / ChKDetail) * 100;

    //         setValueChK($v, variziVolume);
    //         setValueChK($h, hamlVolume);

    //         setValueChK($dv, variziPercent);
    //         setValueChK($dh, hamlPercent);

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
    //             ChKDetail
    //         );

    //         let variziVolume =
    //             ChKDetail - hamlVolume;

    //         let hamlPercent =
    //             (hamlVolume / ChKDetail) * 100;

    //         let variziPercent =
    //             (variziVolume / ChKDetail) * 100;

    //         setValueChK($h, hamlVolume);
    //         setValueChK($v, variziVolume);

    //         setValueChK($dh, hamlPercent);
    //         setValueChK($dv, variziPercent);

    //         return;
    //     }


    //     // -----------------------------------------------------
    //     // حالت اولیه / بدون تغییر مشخص
    //     // -----------------------------------------------------

    //     let variziVolume = clamp(
    //         n($v.val()),
    //         0,
    //         ChKDetail
    //     );

    //     let hamlVolume = ChKDetail - variziVolume;

    //     let variziPercent =
    //         (variziVolume / ChKDetail) * 100;

    //     let hamlPercent =
    //         (hamlVolume / ChKDetail) * 100;

    //     setValueChK($v, variziVolume);
    //     setValueChK($h, hamlVolume);

    //     setValueChK($dv, variziPercent);
    //     setValueChK($dh, hamlPercent);

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

        setValueChK(
            $v,
            (dv / 100) * ChKDetail
        );
    }

    if (changedId &&
        changedId.includes("ReUseDarsad" + KMNum)) {

        let dr = clamp(n($dr.val()), 0, 100);

        setValueChK(
            $r,
            (dr / 100) * ChKDetail
        );
    }

    if (changedId &&
        changedId.includes("DarsadHaml" + KMNum)) {

        let dh = clamp(n($dh.val()), 0, 100);

        setValueChK(
            $h,
            (dh / 100) * ChKDetail
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
        changedId.includes("ChKReUseHajm" + KMNum) &&
        !changedId.includes("Darsad")) {

        let haml = clamp(
            n($h.val()),
            0,
            ChKDetail
        );

        let reuse = clamp(
            n($r.val()),
            0,
            ChKDetail - haml
        );

        let varizi = ChKDetail - reuse - haml;

        setValueChK($r, reuse);
        setValueChK($h, haml);
        setValueChK($v, varizi);

        setValueChK($dr, (reuse / ChKDetail) * 100);
        setValueChK($dh, (haml / ChKDetail) * 100);
        setValueChK($dv, (varizi / ChKDetail) * 100);

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
            ChKDetail
        );

        let haml = clamp(
            n($h.val()),
            0,
            ChKDetail - reuse
        );

        let varizi = ChKDetail - reuse - haml;

        setValueChK($r, reuse);
        setValueChK($h, haml);
        setValueChK($v, varizi);

        setValueChK($dr, (reuse / ChKDetail) * 100);
        setValueChK($dh, (haml / ChKDetail) * 100);
        setValueChK($dv, (varizi / ChKDetail) * 100);

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
            (haml / 100) * ChKDetail;

        let reuseVolume =
            (reusePercent / 100) * ChKDetail;

        let variziVolume =
            ChKDetail - reuseVolume - hamlVolume;

        let variziPercent =
            (variziVolume / ChKDetail) * 100;

        setValueChK($dr, reusePercent);
        setValueChK($dh, haml);

        setValueChK($r, reuseVolume);
        setValueChK($h, hamlVolume);

        setValueChK($v, variziVolume);
        setValueChK($dv, variziPercent);

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
            (reusePercent / 100) * ChKDetail;

        let hamlVolume =
            (hamlPercent / 100) * ChKDetail;

        let variziVolume =
            ChKDetail - reuseVolume - hamlVolume;

        let variziPercent =
            (variziVolume / ChKDetail) * 100;

        setValueChK($dr, reusePercent);
        setValueChK($dh, hamlPercent);

        setValueChK($r, reuseVolume);
        setValueChK($h, hamlVolume);

        setValueChK($v, variziVolume);
        setValueChK($dv, variziPercent);

        return;
    }

    // ---------------------------------------------------------
    // حالت پیش فرض
    // ---------------------------------------------------------

    let reuse = clamp(
        n($r.val()),
        0,
        ChKDetail
    );

    let haml = clamp(
        n($h.val()),
        0,
        ChKDetail - reuse
    );

    let varizi =
        ChKDetail - reuse - haml;

    setValueChK($r, reuse);
    setValueChK($h, haml);
    setValueChK($v, varizi);

    setValueChK(
        $dr,
        (reuse / ChKDetail) * 100
    );

    setValueChK(
        $dh,
        (haml / ChKDetail) * 100
    );

    setValueChK(
        $dv,
        (varizi / ChKDetail) * 100
    );
}



function ViewChanelKaniInfo(KMExistingId, KMNum, BarAvordId, forceReload) {

    if (!forceReload && $('#MainViewChanelKani' + KMNum).is(':visible')) {
        $('#MainViewChanelKani' + KMNum).slideUp(500);

        return
    }

    $('#ViewChanelKaniEzafeBaha' + KMNum).html('');

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.PayKaniInfoForBarAvordId = KMExistingId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.Type = 3;

    debugger;

    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/GetDetailsOfKMPayKaniInfoWithKMPayKaniId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            KMChanelKaniBarAvordDetailsMore = response.kmPayKaniBarAvordDetailsMore;
            KMChanelKaniBarAvordDetails = response.kmPayKaniBarAvordDetails;
            lstChKInfoRizMetre = response.lstPayKaniInfoRizMetre;
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;

            debugger;
            Value = 0;
            strKMAK = `

    <div id="divChanelKaniInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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

        <!-- حجم کانال کنی -->
        <div class="col-3">
          <div class="col-12" style="text-align:center;border-bottom:1px solid #98b3c3;">
            <span>حجم کانال کنی</span>
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
            $.each(KMChanelKaniBarAvordDetails, function () {
                strKMAK += `
        <div class="container-fluid" style="width:100%;margin:0;padding:0;">
        <div class="row AmalyatKhakiRowStyle">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
    <input id="txtChanelKaniItemId${KMNum}_${i}" value="${this.noeKhakBardariId}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanChanelKaniItems${KMNum}_${i}" class="spanStyleChanelKaniItems">
        ${this.title}
      </span>
    </div>`;

                debugger;

                let result = KMChanelKaniBarAvordDetailsMore.filter(x => x.payKaniInfoForBarAvordDetailsId === this.id);

                let KhDetail = result.filter(x => x.name === "KhDetail").length > 0 ? result.filter(x => x.name === "KhDetail")[0].value : 0;
                let DarsadKhDetail = result.filter(x => x.name === "DarsadKhDetail").length > 0 ? result.filter(x => x.name === "DarsadKhDetail")[0].value : 0;
                KhDetail = KhDetail == null ? 0 : KhDetail;
                DarsadKhDetail = DarsadKhDetail == null ? 0 : DarsadKhDetail;

                // جمع کردن مقادیر
                totalKhDetail += parseFloat(KhDetail);
                totalDarsadKhDetail += parseFloat(DarsadKhDetail);


                strKMAK += ` <div class="col-8" style="padding:0;">
          <div class="row" style="margin:0;">

        <!-- حجم کانال کنی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKDetail${KMNum}_${i}" value="${KhDetail == 0 ? '' : KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKDarsad${KMNum}_${i}" value="${DarsadKhDetail == 0 ? '' : DarsadKhDetail}" />
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
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKHaml${KMNum}_${i}" value="${Haml == 0 ? '' : Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKDarsadHaml${KMNum}_${i}" value="${DarsadHaml == 0 ? '' : DarsadHaml}"  />
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
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKReUseHajm${KMNum}_${i}" value="${ReUseHajm == 0 ? '' : ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" id="txtChKReUseDarsad${KMNum}_${i}" value="${DarsadReUseHajm == 0 ? '' : DarsadReUseHajm}"  />
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
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" disabled  id="txtChKVarizi${KMNum}_${i}" value="${Varizi == 0 ? '' : Varizi}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm ChanelKaniTextStyle" disabled id="txtChKDarsadVarizi${KMNum}_${i}" value="${DarsadVarizi == 0 ? '' : DarsadVarizi}"  />
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
        
        <!-- حجم کانال کنی -->
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


            if (!forceReload) $('div[id^="MainViewChanelKani"]').not('#MainViewChanelKani' + KMNum).slideUp();
            //$('div[id^="ViewRizMetreKH"]').slideUp();

            $('#ViewChanelKani' + KMNum).html(strKMAK);
            $('#MainViewChanelKani' + KMNum).slideDown();

            HajmChanelKani = $('#txtHajmChanelKani' + KMNum).val();


            $('#ViewChanelKani' + KMNum + ' input[type="text"]').off('change').change(function () {
                debugger;
                let changedId = $(this).attr("id");
                let i = changedId.split("_")[1];
                let HajmChanelKani = parseFloat($.trim($('#txtHajmChanelKani' + KMNum).val()));

                // 🚨 اعتبارسنجی
                if (!$.isNumeric($(this).val())) {
                    toastr.info('مقدار وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                } else {
                    $(this).removeClass('blinking');
                }

                if (HajmChanelKani == 0 || HajmChanelKani == '' || !$.isNumeric(HajmChanelKani)) {
                    toastr.info('حجم کانال کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmChanelKani' + KMNum).addClass('blinking');
                    return;
                } else {
                    $('#txtHajmChanelKani' + KMNum).removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                ChKDetail = parseFloat($("#txtChKDetail" + KMNum + "_" + i).val()) || 0;

                if (ChKDetail != null || ChKDetail != 0) {

                    $("#txtChKVarizi" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtChKVarizi" + KMNum + "_" + i).val() != "" ? $("#txtChKVarizi" + KMNum + "_" + i).removeClass('blinking') : $("#txtChKVarizi" + KMNum + "_" + i).addClass('blinking');
                    $("#txtChKReUseHajm" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtChKReUseHajm" + KMNum + "_" + i).val() != "" ? $("#txtChKReUseHajm" + KMNum + "_" + i).removeClass('blinking') : $("#txtChKReUseHajm" + KMNum + "_" + i).addClass('blinking');
                    $("#txtChKHaml" + KMNum + "_" + i).prop('disabled') ? '' : $("#txtChKHaml" + KMNum + "_" + i).val() != "" ? $("#txtChKHaml" + KMNum + "_" + i).removeClass('blinking') : $("#txtChKHaml" + KMNum + "_" + i).addClass('blinking');

                    $("#txtChKDarsadVarizi" + i).prop('disabled') ? '' : $("#txtChKDarsadVarizi" + KMNum + "_" + i).val() != "" ? $("#txtChKDarsadVarizi" + KMNum + "_" + i).removeClass('blinking') : $("#txtChKDarsadVarizi" + KMNum + "_" + i).addClass('blinking');
                    $("#txtChKReUseDarsad" + i).prop('disabled') ? '' : $("#txtChKReUseDarsad" + KMNum + "_" + i).val() != "" ? $("#txtChKReUseDarsad" + KMNum + "_" + i).removeClass('blinking') : $("#txtChKReUseDarsad" + KMNum + "_" + i).addClass('blinking');
                    $("#txtChKDarsadHaml" + i).prop('disabled') ? '' : $("#txtChKDarsadHaml" + KMNum + "_" + i).val() != "" ? $("#txtChKDarsadHaml" + KMNum + "_" + i).removeClass('blinking') : $("#txtChKDarsadHaml" + KMNum + "_" + i).addClass('blinking');
                }

                let varizi = parseFloat($("#txtChKVarizi" + KMNum + "_" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtChKReUseHajm" + KMNum + "_" + i).val()) || 0;
                let haml = parseFloat($("#txtChKHaml" + KMNum + "_" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtChKDarsadVarizi" + KMNum + "_" + i).val()) || 0;
                let dReuse = parseFloat($("#txtChKReUseDarsad" + KMNum + "_" + i).val()) || 0;
                let dHaml = parseFloat($("#txtChKDarsadHaml" + KMNum + "_" + i).val()) || 0;


                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("Detail" + KMNum)) {
                    let Zarb = HajmChanelKani === 0 ? 0 : (ChKDetail / HajmChanelKani) * 100;
                    $("#txtChKDarsad" + KMNum + "_" + i).val(Zarb.toFixed(0));

                    let SumAll = ReturnSumAllDetailsChanelKaniForEditChK(KMNum);
                    if (SumAll > HajmChanelKani) {
                        let NewVal = HajmChanelKani - (SumAll - ChKDetail);
                        $("#txtChKDetail" + KMNum + "_" + i).val(NewVal.toFixed(2));
                        ChKDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmChanelKani === 0 ? 0 : (ChKDetail / HajmChanelKani) * 100;
                        $("#txtChKDarsad" + KMNum + "_" + i).val(Zarb.toFixed(0));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtChKVarizi" + KMNum + "_" + i).val(dVarizi > 0 ? ((dVarizi / 100) * ChKDetail).toFixed(2) : "");
                    $("#txtChKReUseHajm" + KMNum + "_" + i).val(dReuse > 0 ? ((dReuse / 100) * ChKDetail).toFixed(2) : "");
                    $("#txtChKHaml" + KMNum + "_" + i).val(dHaml > 0 ? ((dHaml / 100) * ChKDetail).toFixed(2) : "");
                }

                if (changedId.includes("txtChKDarsad" + KMNum)) {
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
                    let newKhDetail = (Zarb / 100) * HajmChanelKani;
                    $('#txtChKDetail' + KMNum + "_" + i).val(newKhDetail.toFixed(2));


                    // // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtChKVarizi" + KMNum + "_" + i).val(dVarizi > 0 ? ((dVarizi / 100) * newKhDetail).toFixed(2) : "");
                    $("#txtChKReUseHajm" + KMNum + "_" + i).val(dReuse > 0 ? ((dReuse / 100) * newKhDetail).toFixed(2) : "");
                    $("#txtChKHaml" + KMNum + "_" + i).val(dHaml > 0 ? ((dHaml / 100) * newKhDetail).toFixed(2) : "");

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
                     //let newKhDetail = (dVal / 100) * HajmChanelKani;
                     //$('#txtKhDetail' + KMNum + "_" + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (ChKDetail > 0) {
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        varizi = (dVarizi / 100) * ChKDetail;
                        $("#txtChKVarizi" + KMNum + "_" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        reuseHajm = (dReuse / 100) * ChKDetail;
                        $("#txtChKReUseHajm" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        haml = (dHaml / 100) * ChKDetail;
                        $("#txtChKHaml" + KMNum + "_" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dVarizi = ChKDetail === 0 ? 0 : (varizi / ChKDetail) * 100;
                        $("#txtChKDarsadVarizi" + KMNum + "_" + i).val(dVarizi.toFixed(0));
                        $("#txtChKDarsadVarizi" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        dReuse = ChKDetail === 0 ? 0 : (reuseHajm / ChKDetail) * 100;
                        $("#txtChKReUseDarsad" + KMNum + "_" + i).val(dReuse.toFixed(0));
                        $("#txtChKReUseDarsad" + KMNum + "_" + i).removeClass('blinking');
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dHaml = ChKDetail === 0 ? 0 : (haml / ChKDetail) * 100;
                        $("#txtChKDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(0));
                        $("#txtChKDarsadHaml" + KMNum + "_" + i).removeClass('blinking');
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtChKVarizi" + KMNum + "_" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtChKReUseHajm" + KMNum + "_" + i).val()) || 0;
                haml = parseFloat($("#txtChKHaml" + KMNum + "_" + i).val()) || 0;

                dVarizi = parseFloat($("#txtChKDarsadVarizi" + KMNum + "_" + i).val()) || 0;
                dReuse = parseFloat($("#txtChKReUseDarsad" + KMNum + "_" + i).val()) || 0;
                dHaml = parseFloat($("#txtChKDarsadHaml" + KMNum + "_" + i).val()) || 0;


                ChKDetail = parseFloat($("#txtChKDetail" + KMNum + "_" + i).val()) || 0;


                AutoFillThirdColumnChKForEdit(KMNum, i, ChKDetail, changedId);

                SumKol = sumKolNewChK(9, KMNum);
                debugger;
                $('#spantotalChKDetail' + KMNum).html(toPersianDigits(SumKol[0].toFixed(2)));
                $('#spantotalDarsadChKDetail'+KMNum).html(toPersianDigits(SumKol[1].toFixed(0)) + " % ");

                $('#spantotalVarizi' + KMNum).html(toPersianDigits(SumKol[2].toFixed(2)));

                DarsadVariziNew = (parseFloat(SumKol[2]) / parseFloat(HajmChanelKani)) * 100;
                $('#spantotalDarsadVarizi' + KMNum).html(toPersianDigits(DarsadVariziNew.toFixed(2)) + " % ");

                $('#spantotalReUseHajm' + KMNum).html(toPersianDigits(SumKol[3].toFixed(2)));
                DarsadReUseHajmNew = (parseFloat(SumKol[3]) / parseFloat(HajmChanelKani)) * 100;
                $('#spantotalDarsadReUseHajm' + KMNum).html(toPersianDigits(DarsadReUseHajmNew.toFixed(2)) + " % ");

                $('#spantotalHaml' + KMNum).html(toPersianDigits(SumKol[4].toFixed(2)));
                DarsadHamlNew = (parseFloat(SumKol[4]) / parseFloat(HajmChanelKani)) * 100;
                $('#spantotalDarsadHaml' + KMNum).html(toPersianDigits(DarsadHamlNew.toFixed(2)) + " % ");

            });



            $('#txtHajmChanelKani' + KMNum).off('change').on('change', function () {

                let HKB = parseFloat($(this).val());

                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم کانال کنی وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                $(this).removeClass('blinking');

                let totalAssigned = 0;
                let lastIndex = -1;

                for (let i = 1; i <= ActivityLength; i++) {

                    let darsad = parseFloat(
                        $('#txtChKDarsad' + KMNum + '_' + i).val()
                    ) || 0;

                    if (darsad > 0) {

                        // حجم جدید فعالیت بر اساس درصد آن
                        let khDetail = (darsad / 100) * HKB;

                        $('#txtChKDetail' + KMNum + '_' + i)
                            .val(khDetail.toFixed(2));

                        // درصدهای قبلی جزئیات
                        let dVarizi = parseFloat(
                            $('#txtChKDarsadVarizi' + KMNum + '_' + i).val()
                        ) || 0;

                        let dReUse = parseFloat(
                            $('#txtChKReUseDarsad' + KMNum + '_' + i).val()
                        ) || 0;

                        let dHaml = parseFloat(
                            $('#txtChKDarsadHaml' + KMNum + '_' + i).val()
                        ) || 0;

                        // محاسبه حجم‌های جدید
                        let vVarizi = (dVarizi / 100) * khDetail;
                        let vReUse = (dReUse / 100) * khDetail;
                        let vHaml = (dHaml / 100) * khDetail;

                        // ثبت مقادیر
                        $('#txtChKVarizi' + KMNum + '_' + i)
                            .val(vVarizi.toFixed(2));

                        $('#txtChKReUseHajm' + KMNum + '_' + i)
                            .val(vReUse.toFixed(2));

                        $('#txtChKHaml' + KMNum + '_' + i)
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
                            $('#txtChKDetail' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        let newVal = lastVal + diff;

                        $('#txtChKDetail' + KMNum + '_' + lastIndex)
                            .val(newVal.toFixed(2));

                        let dVarizi = parseFloat(
                            $('#txtChKDarsadVarizi' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        let dReUse = parseFloat(
                            $('#txtChKReUseDarsad' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        let dHaml = parseFloat(
                            $('#txtChKDarsadHaml' + KMNum + '_' + lastIndex).val()
                        ) || 0;

                        $('#txtChKVarizi' + KMNum + '_' + lastIndex)
                            .val(((dVarizi / 100) * newVal).toFixed(2));

                        $('#txtChKReUseHajm' + KMNum + '_' + lastIndex)
                            .val(((dReUse / 100) * newVal).toFixed(2));

                        $('#txtChKHaml' + KMNum + '_' + lastIndex)
                            .val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }

                // به‌روزرسانی جمع کل‌ها
                let SumKol = sumKolNewChK(ActivityLength, KMNum);

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
                UpdateChanelKaniInfo(KMExistingId, BarAvordId, KMNum,3);

            });

            ///////////////////
            /////////////

            debugger;

            ShowRizMetreChK(KMExistingId, lstChKInfoRizMetre, lstItemFBShomarehForGet, KMNum);


            ShowA_ChKEzafeBaha(KMExistingId, KMNum);

            KMChanelKaniBarAvordDetails = response.kmPayKaniBarAvordDetails;
            $.each(KMChanelKaniBarAvordDetails, function () {
                Id = this.id;
                ChanelKaniInfoForBarAvordId = this.PayKaniInfoForBarAvordId;
                Type = this.type;
            });

            KMAmalyateRizeshBarAvordDetailsMore = response.kmAmalyateRizeshBarAvordDetailsMore;
            $.each(KMAmalyateRizeshBarAvordDetailsMore, function () {
                CurrentId = this.id;
                Name = $.trim(this.name);
                ValueMore = this.value;
                ChanelKaniInfoForBarAvordDetailsId = this.PayKaniInfoForBarAvordDetailsId;
                if (Id == ChanelKaniInfoForBarAvordDetailsId) {
                    $('#txt' + Name + Type).val(ValueMore);
                }
            });

            CurrentValue = $('#txtChKDetail' + Type).val();
            ValueOfReCycle = $('#txtChKReUseHajm' + Type).val();
            ValueOfVarize = $('#txtChKVarizi' + Type).val();
            ValueOfHaml = $('#txtChKHaml' + Type).val();
            ValueOfFaseleHaml = $('#txtChKFaseleHaml' + Type).val();

            $('#txtChKDarsad' + Type).val(parseFloat(Value) == 0 ? 0 : (parseFloat(CurrentValue) / parseFloat(Value) * 100).toFixed(0));
            $('#txtChKReUseDarsad' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtChKDarsadVarizi' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtChKDarsadHaml' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(0));
            $('#txtChKDarsadFaseleHaml' + Type).val(parseFloat(ValueOfVarize) == 0 ? 0 : (parseFloat(ValueOfFaseleHaml) / parseFloat(ValueOfVarize) * 100).toFixed(0));

            KMAmalyateRizeshBarAvordDetailsEzafeBaha = response.kmAmalyateRizeshBarAvordDetailsEzafeBaha;
            $.each(KMAmalyateRizeshBarAvordDetailsEzafeBaha, function () {
                CurrentId = this.id;
                Name = $.trim(this.name);
                boolValue = this.value == 'true' ? true : false;
                ChanelKaniInfoForBarAvordDetailsId = this.PayKaniInfoForBarAvordDetailsId;

                if (Id == ChanelKaniInfoForBarAvordDetailsId) {
                    $('#ck' + Name + Type).attr('checked', boolValue);
                }
            });


        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری متراژ انتخابی', 'خطا');
        }
    });

    $('#ViewChanelKani' + KMNum).html();
}

function ShowRizMetreChK(KMExistingId, lstChKInfoRizMetre, lstItemFBShomarehForGet, KMNum) {
    debugger;

    var str = '';

    // ساخت HTML بر اساس lstItemFBShomarehForGet

    // گروه‌بندی data.lst بر اساس itemFBShomareh
    let groupedData = {};
    lstChKInfoRizMetre.forEach(function (row) {
        if (!groupedData[row.itemFBShomareh]) {
            groupedData[row.itemFBShomareh] = [];
        }
        groupedData[row.itemFBShomareh].push(row);
    });

    // ساخت HTML بر اساس lstItemFBShomarehForGet
    if (lstChKInfoRizMetre.length != 0) {

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

        $targetDivRizMetreChK = $('#ViewRizMetreChK' + KMNum);

        $targetDivRizMetreChK.html(str);
        debugger;
        $targetDivRizMetreChK.slideDown();



        $targetDivRizMetreChK.find("input[type='text'].essentialValue")
            .filter(function () {
                return $(this).val().trim() === "";
            })
            .addClass("blinking")
            .first()
            .focus();


        $targetDivRizMetreChK.on("change", "input[type='text'].essentialValue", function () {
            if ($(this).val().trim() !== "") {
                $(this).removeClass("blinking");
            }
        });


        $targetDivRizMetreChK.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
            /* ENTER PRESSED */
            if (e.keyCode == 13) {
                /* FOCUS ELEMENT */
                    alert('sss');
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

function ReturnSumAllDetailsChK() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtChKDetail' + i).val()) == '' ? '0' : $.trim($('#txtChKDetail' + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllDetailsChanelKaniForEditChK(KMNum) {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtChKDetail' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtChKDetail' + KMNum + "_" + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllRBDetailsChK() {
    sumAll = 0;
    for (var i = 1; i < 3; i++) {
        sumAll += parseFloat($.trim($('#txtRBDetail' + i).val()) == '' ? '0' : $.trim($('#txtRBDetail' + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllDardadChK() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtDarsad' + i).val()) == '' ? '0' : $.trim($('#txtDarsad' + i).val()));
    }
    return parseFloat(sumAll);
}
function ReturnSumAllDardadChKForEdit(KMNum) {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtDarsad' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtDarsad' + KMNum + "_" + i).val()));
    }
    return parseFloat(sumAll);
}

function ReturnSumAllRBDardadChK() {
    sumAll = 0;
    for (var i = 1; i < 3; i++) {
        sumAll += parseFloat($.trim($('#txtRBDarsad' + i).val()) == '' ? '0' : $.trim($('#txtRBDarsad' + i).val()));
    }
    return parseFloat(sumAll);
}

function SumAllDetailsChanelKani() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtChKDetail' + i).val()) == '' ? '0' : $.trim($('#txtChKDetail' + i).val()));
    }

    HajmChanelKani = parseFloat($('#txtHajmChanelKani').val());
    if (sumAll > HajmChanelKani) {
        $('#txtHajmChanelKani').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم کانال کنی بیشتر باشد', 'اطلاع');
        return true;
    }
    else if (sumAll < HajmChanelKani) {
        $('#txtHajmChanelKani').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم کانال کنی کمتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmChanelKani').removeClass('blinking');
        return false;
    }
}


function SumAllDetailsChanelKaniForEditChK(KMNum) {

    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtChKDetail' + KMNum + "_" + i).val()) == '' ? '0' : $.trim($('#txtChKDetail' + KMNum + "_" + i).val()));
    }

    HajmChanelKani = parseFloat($('#txtHajmChanelKani' + KMNum).val());
    if (sumAll > HajmChanelKani) {
        $('#txtHajmChanelKani' + KMNum).addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم کانال کنی بیشتر باشد', 'اطلاع');
        return true;
    }
    else if (sumAll < HajmChanelKani) {
        $('#txtHajmChanelKani' + KMNum).addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم کانال کنی کمتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmChanelKani' + KMNum).removeClass('blinking');
        return false;
    }
}


function CheckValuesOfChanelKaniDetails() {
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

        $('#txtChKReUseHajm' + i).removeClass('blinking');
        $('#txtChKVarizi' + i).removeClass('blinking');
        $('#txtChKHaml' + i).removeClass('blinking');

        $('#txtChKReUseDarsad' + i).removeClass('blinking');
        $('#txtChKDarsadVarizi' + i).removeClass('blinking');
        $('#txtChKDarsadHaml' + i).removeClass('blinking');
    };

    // علامت زدن مقادیر حجم
    const blinkVolumes = (i) => {
        if (i != 1 && i != 2) {
            $('#txtChKReUseHajm' + i).addClass('blinking');
        }

        $('#txtChKVarizi' + i).addClass('blinking');
        $('#txtChKHaml' + i).addClass('blinking');
    };

    for (var i = 1; i <= ActivityLength; i++) {

        var KhDetail = getValue('txtChKDetail' + i);

        var ReUseHajm = getValue('txtChKReUseHajm' + i);
        var ReUseDarsad = getValue('txtChKReUseDarsad' + i);

        var Varizi = getValue('txtChKVarizi' + i);
        var DarsadVarizi = getValue('txtChKDarsadVarizi' + i);

        var Haml = getValue('txtChKHaml' + i);
        var DarsadHaml = getValue('txtChKDarsadHaml' + i);

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
            const currentRow = $('#txtChKDarsadHaml' + i)
                .closest('.AmalyatKhakiRowStyle');

            currentRow.addClass('errorRow');

            blinkVolumes(i);

            checkValues = true;

            return true;
        }

        // بررسی مقادیر منفی یا بزرگ‌تر از مقدار کل
        if (ReUseHajm > KhDetail + TOLERANCE) {
            $('#txtChKReUseHajm' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKReUseHajm' + i).removeClass('blinking');
        }

        // درصد ReUse
        if (ReUseDarsad > 100) {
            $('#txtChKReUseDarsad' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKReUseDarsad' + i).removeClass('blinking');
        }

        // واریزی
        if (Varizi > KhDetail + TOLERANCE) {
            $('#txtChKVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKVarizi' + i).removeClass('blinking');
        }

        // حمل
        if (Haml > KhDetail + TOLERANCE) {
            $('#txtChKHaml' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKHaml' + i).removeClass('blinking');
        }

        // درصد واریزی
        if (DarsadVarizi > 100) {
            $('#txtChKDarsadVarizi' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKDarsadVarizi' + i).removeClass('blinking');
        }

        // درصد حمل
        if (DarsadHaml > 100) {
            $('#txtChKDarsadHaml' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKDarsadHaml' + i).removeClass('blinking');
        }
    }

    return checkValues;
}


function CheckValuesOfChanelKaniDetailsForEditChK(KMNum) {
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

        var KhDetail = parseFloat($('#txtChKDetail' + prefix).val());

        // اگر KhDetail عدد نیست، این ردیف بررسی نشود
        if (isNaN(KhDetail))
            continue;

        // ==========================================
        // ReUse
        // ==========================================

        var ReUseHajm = getValue('txtChKReUseHajm' + prefix);

        if (ReUseHajm > KhDetail + TOLERANCE) {
            $('#txtChKReUseHajm' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKReUseHajm' + prefix).removeClass('blinking');
        }

        JamKol += ReUseHajm;


        var ReUseDarsad = getValue('txtChKReUseDarsad' + prefix);

        if (ReUseDarsad > 100 + TOLERANCE) {
            $('#txtChKReUseDarsad' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKReUseDarsad' + prefix).removeClass('blinking');
        }

        JamDarsad += ReUseDarsad;


        // ==========================================
        // Varizi
        // ==========================================

        var Varizi = getValue('txtChKVarizi' + prefix);

        if (Varizi > KhDetail + TOLERANCE) {
            $('#txtChKVarizi' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKVarizi' + prefix).removeClass('blinking');
        }

        JamKol += Varizi;


        var DarsadVarizi = getValue('txtChKDarsadVarizi' + prefix);

        if (DarsadVarizi > 100 + TOLERANCE) {
            $('#txtChKDarsadVarizi' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKDarsadVarizi' + prefix).removeClass('blinking');
        }

        JamDarsad += DarsadVarizi;


        // ==========================================
        // Haml
        // ==========================================

        var Haml = getValue('txtChKHaml' + prefix);

        if (Haml > KhDetail + TOLERANCE) {
            $('#txtChKHaml' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKHaml' + prefix).removeClass('blinking');
        }

        JamKol += Haml;


        var DarsadHaml = getValue('txtChKDarsadHaml' + prefix);

        if (DarsadHaml > 100 + TOLERANCE) {
            $('#txtChKDarsadHaml' + prefix).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtChKDarsadHaml' + prefix).removeClass('blinking');
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

                var currentRow = $('#txtChKDarsadHaml' + prefix)
                    .closest('.AmalyatKhakiRowStyle');

                currentRow.addClass('errorRow');

                checkValues = true;
            }
            else {

                // مقدارها قابل قبول هستند
                var currentRow = $('#txtChKDarsadHaml' + prefix)
                    .closest('.AmalyatKhakiRowStyle');

                currentRow.removeClass('errorRow');
            }
        }
        else {

            // اگر KhDetail صفر است و خطای قبلی داشته،
            // خطای ردیف پاک شود
            var currentRow = $('#txtChKDarsadHaml' + prefix)
                .closest('.AmalyatKhakiRowStyle');

            currentRow.removeClass('errorRow');
        }
    }

    return checkValues;
}

function SaveChanelKaniInfo(BarAvordUserId, Type, buttonElement) {

    debugger;
    check = false;
    //////////
    HKB = $('#txtHajmChanelKani').val();
    if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
        $('#txtHajmChanelKani').addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmChanelKani').removeClass('blinking');
    }
   
    /////////////
    var KME = parseFloat($('#txtToKMForChanelKani').val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForChanelKani').val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForChanelKani').addClass('blinking');
        toastr.info('متراژ انتها بایستی بعد از متراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForChanelKani').removeClass('blinking');
    }

    ///////////////
    $('#divChanelKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    debugger;

    check = SumAllDetailsChanelKani() || check;

    //if (check) {
    //    toastr.info('مجموع احجام وارد شده بایستی با حجم کل برابر باشد', 'اطلاع');
    //    return 0;
    //}

    check1 = CheckValuesOfChanelKaniDetails();

    //if (check1) {
    //toastr.info('احجام وارد شده درست نمی باشند', 'اطلاع');
    //}
    debugger;

    if (!check1 && !check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            debugger;

            let obj = {
                DetailValue: $('#txtChKDetail' + i).val(),
                DarsadValue: $('#txtChKDarsad' + i).val(),
                DetailValueOfReCycle: $('#txtChKReUseHajm' + i).val(),
                DarsadValueOfReCycle: $('#txtChKReUseDarsad' + i).val(),
                DetailValueOfVarize: $('#txtChKVarizi' + i).val(),
                DarsadValueOfVarize: $('#txtChKDarsadVarizi' + i).val(),
                DetailValueOfHaml: $('#txtChKHaml' + i).val(),
                DarsadValueOfHaml: $('#txtChKDarsadHaml' + i).val(),
                NoeKhakBardari: '10004', //$('#txtChanelKaniItemId' + i).val(),
            };

            dataList.push(obj);
        }


        debugger;
        NoeFBId = parseInt($('#HDFNoeFB').val());

        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Type = 3;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.NoeFBId = NoeFBId;


        const requestKey = 'save:' + BarAvordUserId;
        if (!beginChanelKaniRequest(requestKey, buttonElement)) return;

        $.ajax({
            type: "POST",
            url: "/PayKaniInfoForBarAvords/SavePayKaniInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    //$('#HDFStateChanelKaniSaveOrEdit').val('Edit');
                    //$('#HDFKMChanelKaniIdForEdit').val(info[1]);
                    //$('#HDFKMChanelKaniNum').val(info[2]);

                    $('#txtHajmChanelKani').val(0);
                    $('#txtFromKMForChanelKani').val('0')
                    $('#txtToKMForChanelKani').val('0')

                    for (let i = 1; i <= ActivityLength; i++) {
                        $('#txtChKDetail' + i).val('');
                        $('#txtChKDarsad' + i).val('');
                        $('#txtChKReUseHajm' + i).val('');
                        $('#txtChKReUseDarsad' + i).val('');
                        $('#txtChKVarizi' + i).val('');
                        $('#txtChKDarsadVarizi' + i).val('');
                        $('#txtChKHaml' + i).val('');
                        $('#txtChKDarsadHaml' + i).val('');
                        //$('#txtChanelKaniItemId' + i).val(0);
                    }

                    $('#spantotalChKDetailNew').html('');
                    $('#spantotalChKDarsadDetailNew').html('');
                    $('#spantotalChKVariziNew').html('');
                    $('#spantotalChKDarsadVariziNew').html('');
                    $('#spantotalChKReUseHajmNew').html('');
                    $('#spantotalChKDarsadReUseHajmNew').html('');
                    $('#spantotalChKHamlNew').html('');
                    $('#spantotalChKDarsadHamlNew').html('');

                    $('#MainViewChanelKaniNew').slideUp(500);

                    ShowExistingKMChanelKani(BarAvordUserId);

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
            },
            complete: function () {
                endChanelKaniRequest(requestKey);
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}

function UpdateChanelKaniInfo(KMChanelKaniId, BarAvordUserId, KMNum, Type, buttonElement) {


    debugger;
    check = false;
    //////////
    HKB = $('#txtHajmChanelKani' + KMNum).val();
    if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
        $('#txtHajmChanelKani' + KMNum).addClass('blinking');
        check = true;
    }
    else {
        $('#txtHajmChanelKani' + KMNum).removeClass('blinking');
    }
    ////////////////
    
    var KME = parseFloat($('#txtToKMForChanelKani' + KMNum).val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForChanelKani' + KMNum).val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForChanelKani' + KMNum).addClass('blinking');
        toastr.info('متراژ انتها بایستی بعد از متراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForChanelKani' + KMNum).removeClass('blinking');
    }

    ///////////////
    $('#divChanelKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    check = SumAllDetailsChanelKaniForEditChK(KMNum) || check;
    //if (check) {
    //    toastr.info('حجم کل پی کنی صحیح نمیباشد', 'اطلاع');
    //}

    check1 = CheckValuesOfChanelKaniDetailsForEditChK(KMNum);
    //if (check1) {
    //    toastr.info('مشکل در مقادیر وارده', 'اطلاع');
    //}
    debugger;

    if (!check1 && !check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            var DarsadValue = $('#txtChKDarsad' + KMNum + "_" + i).val().trim();
            if (DarsadValue != "0") {
                let obj = {
                    DetailValue: $('#txtChKDetail' + KMNum + "_" + i).val(),
                    DarsadValue: DarsadValue,
                    DetailValueOfReCycle: $('#txtChKReUseHajm' + KMNum + "_" + i).val(),
                    DarsadValueOfReCycle: $('#txtChKReUseDarsad' + KMNum + "_" + i).val(),
                    DetailValueOfVarize: $('#txtChKVarizi' + KMNum + "_" + i).val(),
                    DarsadValueOfVarize: $('#txtChKDarsadVarizi' + KMNum + "_" + i).val(),
                    DetailValueOfHaml: $('#txtChKHaml' + KMNum + "_" + i).val(),
                    DarsadValueOfHaml: $('#txtChKDarsadHaml' + KMNum + "_" + i).val(),
                    NoePayKani:10004,
                };
                dataList.push(obj);
            }
        }

        Year = parseInt($('#HDFYear').val());
        NoeFBId = parseInt($('#HDFNoeFB').val());
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMPayKaniId = KMChanelKaniId;
        vardata.KMNum = KMNum;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.Year = Year;
        vardata.NoeFBId = NoeFBId;
        vardata.Type = Type;


        const requestKey = 'record:' + KMChanelKaniId;
        if (!beginChanelKaniRequest(requestKey, buttonElement)) return;

        $.ajax({
            type: "POST",
            url: "/PayKaniInfoForBarAvords/UpdatePayKaniInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                debugger;
                info = response.split('_');
                if (info[0] == "OK") {
                    toastr.success('اطلاعات متراژ بدرستی ویرایش گردید', 'ویرایش');

                    // دریافت مجدد جزئیات ذخیره‌شده، بدون بستن پنل ویرایش
                    ViewChanelKaniInfo(KMChanelKaniId, KMNum, BarAvordUserId, true);

                }
                else {
                    toastr.error('مشکل در ویرایش اطلاعات متراژ', 'خطا');
                    $('#ContainerWaiting').hide();
                }
            },
            error: function (response) {
                toastr.error('مشکل در ویرایش اطلاعات متراژ', 'خطا');
            },
            complete: function () {
                endChanelKaniRequest(requestKey);
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
}

function DeleteChanelKaniInfo(KMChanelKaniId, KMNum, BarAvordUserId, buttonElement) {

    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.KMChanelKaniId = KMChanelKaniId;
    vardata.Year = Year;
    vardata.KMNum = KMNum;


    const requestKey = 'record:' + KMChanelKaniId;
    if (!beginChanelKaniRequest(requestKey, buttonElement)) return;

    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/DeletePayKaniInfoForBarAvord",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('اطلاعات متراژ بدرستی حذف گردید', 'ثبت');
                ShowExistingKMChanelKani(BarAvordUserId);
            }
            else {
                toastr.error('مشکل در حذف اطلاعات', 'خطا');
                $('#ContainerWaiting').hide();
            }
        },
        error: function (response) {
            toastr.error('مشکل در حذف اطلاعات', 'خطا');
        },
        complete: function () {
            endChanelKaniRequest(requestKey);
        }
    });

}


function sumKolNewChK(ActivityLength, KMNum) {
    debugger;
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
        currentKhDetail = $.trim($('#txtChKDetail' + strKMNum + (i + 1)).val());
        if (currentKhDetail != '') {
            KhDetail = parseFloat(currentKhDetail);
            KhDetail = isNaN(KhDetail) ? 0 : KhDetail;
            JamKolKhDetail += KhDetail;
        }
        currentDarsadKhDetail = $.trim($('#txtChKDarsad' + strKMNum + (i + 1)).val());
        if (currentDarsadKhDetail != '') {
            DarsadKhDetail = parseFloat(currentDarsadKhDetail);
            DarsadKhDetail = isNaN(DarsadKhDetail) ? 0 : DarsadKhDetail;
            JamKolDarsadKhDetail += DarsadKhDetail;
        }
        currentVarizi = $.trim($('#txtChKVarizi' + strKMNum + (i + 1)).val());
        if (currentVarizi != '') {
            Varizi = parseFloat(currentVarizi);
            Varizi = isNaN(Varizi) ? 0 : Varizi;
            JamKolVarizi += Varizi;
        }

        currentReUseHajm = $.trim($('#txtChKReUseHajm' + strKMNum + (i + 1)).val());
        if (currentReUseHajm != '') {
            ReUseHajm = parseFloat(currentReUseHajm);
            ReUseHajm = isNaN(ReUseHajm) ? 0 : ReUseHajm;
            JamKolReUseHajm += ReUseHajm;
        }

        currentHaml = $.trim($('#txtChKHaml' + strKMNum + (i + 1)).val());
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


