ActivityLength = 0;

function ChanelKaniClick(OpId) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    str = "";
    debugger;
    str += `

  <div class="col-12" id="divChanelKaniExistingKM">
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
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForChanelKani" value="0"/>
    </div>

    <!-- تا کیلومتراژ -->
    <div class="col-md-1 label-col">
      <span>تا کیلومتراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForChanelKani" value="0"/>
    </div>

    <!-- حجم پی کنی -->
    <div class="col-md-2 label-col">
      <span>حجم پی کنی:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtHajmChanelKani" value="0"/>
    </div>
    <div class="col-md-1 unit-col">
      <span>مترمکعب</span>
    </div>
  </div>

  <!-- بخش نمایش -->
  <div id="MainViewChanelKaniNew" class="khakbardari-view" style="display:none">
  <div id="ViewChanelKaniNew" class="khakbardari-view">
  </div>

 <!-- دکمه ذخیره -->
 <div class="row">
 <div class="col-12" style="direction: ltr;">
    <div class="col-md-2 action-col">
      <a class="btn buttonStyleBoard" style="color:#fff" onclick="SaveChanelKaniInfo1('${BarAvordUserId}')">
        ذخیره
      </a>
    </div><!--col-md-2 action-col -->
    </div><!--col-12 -->
    </div>

    </div><!-- بخش نمایش -->


</div>
    `;

    $('#ula' + OpId).html(str);

    setTimeout(() => { $('#txtFromKMForChanelKani').focus(); }, 200);

    $('#ula' + OpId).off('keydown.Chanelkani').on('keydown.Chanelkani', '.khakbardariTextStyle', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault(); // جلوگیری از submit فرم
            const inputs = $('.khakbardariTextStyle');
            const idx = inputs.index(this);
            if (idx >= 0 && idx < inputs.length - 1) {
                inputs.eq(idx + 1).focus().select();
            } else {
                // اگر آخرین input بود، می‌توانی فوکوس را به دکمه ذخیره بدهی
                $('.buttonStyleBoard').focus();
            }
        }
    });

    ShowExistingKMChanelKani(BarAvordUserId);

    ShowSelctionChanelKani(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0);
}

function ShowExistingKMChanelKani(BarAvordUserId) {
    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.Type = 3;//کانال کنی
    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/GetExistingKMPayKaniInfoWithBarAvordId",
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
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>حجم کانال کنی</span></div>
                        <div class="col-3" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>عملیات</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"></div>
                </div>

            `;
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
    <div id="divExistKMHeader${KMNum}" class="row col-12 ExistKMHeaderStyle" onclick="ViewChanelKaniInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}')">
    <div class="col-md-1 label-col" style="text-align:center">
      <span>${KMNum}</span>
    </div>
    <!-- از کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForChanelKani${KMNum}" value="${FromKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- تا کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForChanelKani${KMNum}" value="${ToKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- حجم خاکبرداری -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtHajmChanelKani${KMNum}" value="${Value}" onclick="event.stopPropagation();"/>
    </div>
        <div class="col-md-3" style="text-align:center">
        <span>جهت مشاهده جزییات کلیک نمایید</span>
</div>
   
    </div>
  <!-- بخش نمایش -->

  <div class="row col-12" style="direction:ltr;display:none" id="MainViewChanelKani${KMNum}" class="khakbardari-view">
    <div id="ViewChanelKani${KMNum}" class="khakbardari-view" style="direction: rtl;">
    </div>
    <div class="col-md-2 action-col">
      <a class="btn buttonStyleBoard" style="color:#fff" onclick="UpdateChanelKaniInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum})" onclick="event.stopPropagation();">
        ذخیره
      </a>
    </div>
  <div id="ViewRizMetreChanelKani${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  <div id="ViewChanelKaniEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  </div><!-- MainViewChanelKani -->
  </div>
    `;

                    //    str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionKMAmalyateKhaki($(this),\'' + KMExistingId + '\',\'' + KMNum
                    //        + '\')\" ondblclick=\"ShowSelctionKhakBardari(0,' + KMExistingId + ',' + KMNum + ',' + BarAvordUserId + ',' + "'" + FromKM + "'" + ',' + "'" + ToKM + "'" + ',' + "'" + FromKMSplit + "'" + ',' + "'" + ToKMSplit + "'" + ',' + Value + ')\">' + count++
                    //        + ' - کیلومتراژ' + '<label>' + FromKMSplit + ' - ' + ToKMSplit + '</label>' + '</a></div>';
                });

                $('#divChanelKaniExistingKM').html(strSEKB);
                $('#divChanelKaniExistingKM').find('#MainViewChanelKani' + KMNum).hide();
            }

            //$('#ula' + OpId).find('divChanelKaniExistingKM').html(str);
            //$('#divViewExistingKMAmalyateKhaki').html(str);
            //$('#aViewExistingKMAmalyateKhaki').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
        }
    });
}

function ShowSelctionChanelKani(IsNew, KMExistingId, KMNum, BarAvordId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value) {

    strSSKB = `

    <div id="divChanelKaniInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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
        <div class="row" style="padding:2px 0px;margin:2px 0px;border-bottom:1px solid #ccc;">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
        <input id="txtChanelKaniItemId${i + 1}" value="${ActivityTitleComplete[i].id}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanChanelKaniItems${i + 1}" class="spanStyleKhakBardariItems" style="font-size:12px;">
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetailChK${i + 1}" value="0" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadChK${i + 1}" value="0" />
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVariziChK${i + 1}" value="0"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVariziChK${i + 1}" value="0"  />
            </div>
          </div>
        </div>

        <!-- قابل مصرف در خاکریزی -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajmChK${i + 1}" value="0"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsadChK${i + 1}" value="0"  />
            </div>
          </div>
        </div>

        <!-- حمل به دپو/مسیر -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHamlChK${i + 1}" value="0"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHamlChK${i + 1}" value="0"  />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
            }

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
                    $(this).addClass('ErrorValueStyle');
                    return;
                }
                else {
                    $(this).removeClass('ErrorValueStyle');
                }

                if (HajmChanelKani == 0 || HajmChanelKani == '' || !$.isNumeric(HajmChanelKani)) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmChanelKani').addClass('ErrorValueStyle');
                    return;
                } else {
                    $('#txtHajmChanelKani').removeClass('ErrorValueStyle');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetailChK" + i).val()) || 0;

                let varizi = parseFloat($("#txtVariziChK" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajmChK" + i).val()) || 0;
                let haml = parseFloat($("#txtHamlChK" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVariziChK" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsadChK" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHamlChK" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail")) {
                    let Zarb = khDetail / HajmChanelKani * 100;
                    $("#txtDarsadChK" + i).val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmChanelKani) {
                        let NewVal = HajmChanelKani - (SumAll - khDetail);
                        $("#txtKhDetailChK" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = khDetail / HajmChanelKani * 100;
                        $("#txtDarsadChK" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVariziChK" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajmChK" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHamlChK" + i).val(((dHaml / 100) * khDetail).toFixed(2));
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
                    $('#txtKhDetailChK' + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi")) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVariziChK" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajmChK" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHamlChK" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        dVarizi = (varizi / khDetail) * 100;
                        $("#txtDarsadVariziChK" + i).val(dVarizi.toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsadChK" + i).val(dReuse.toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (haml / khDetail) * 100;
                        $("#txtDarsadHamlChK" + i).val(dHaml.toFixed(2));
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVariziChK" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajmChK" + i).val()) || 0;
                haml = parseFloat($("#txtHamlChK" + i).val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVariziChK" + i).val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsadChK" + i).val()) || 0;
                dHaml = parseFloat($("#txtDarsadHamlChK" + i).val()) || 0;

                // کنترل مجموع درصدها
                let dSum = dVarizi + dReuse + dHaml;
                if (dSum > 100) {
                    let extra = dSum - 100;
                    if (changedId.includes("DarsadVarizi")) {
                        dVarizi -= extra;
                        $("#txtDarsadVariziChK" + i).val(dVarizi.toFixed(2));
                        $("#txtVariziChK" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        dReuse -= extra;
                        $("#txtReUseDarsadChK" + i).val(dReuse.toFixed(2));
                        $("#txtReUseHajmChK" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        dHaml -= extra;
                        $("#txtDarsadHamlChK" + i).val(dHaml.toFixed(2));
                        $("#txtHamlChK" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        varizi -= extra;
                        $("#txtVariziChK" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVariziChK" + i).val(((varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        reuseHajm -= extra;
                        $("#txtReUseHajmChK" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsadChK" + i).val(((reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        haml -= extra;
                        $("#txtHamlChK" + i).val(haml.toFixed(2));
                        $("#txtDarsadHamlChK" + i).val(((haml / khDetail) * 100).toFixed(2));
                    }
                }
            });

            $('#txtHajmChanelKani').change(function () {

                debugger;

                let HKB = parseFloat($(this).val());

                var KMS = parseFloat($('#txtFromKMForChanelKani').val().replace('+', ''));
                var KME = parseFloat($('#txtToKMForChanelKani').val().replace('+', ''));

                if (KMS == 0 || KME == 0) {
                    $('#txtFromKMForChanelKani').addClass('blinking');
                    $('#txtToKMForChanelKani').addClass('blinking');
                    return;
                }

                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
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
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                $('#MainViewChanelKaniNew').slideDown(500);
                $('#divChanelKaniInfoDetails').show();
                $(this).removeClass('blinking');

                let totalAssigned = 0;
                let lastIndex = -1;

                for (let i = 1; i <= ActivityLength; i++) {
                    $('#txtDarsadChK' + i).val(100)
                    let Darsad = 100;

                    if (Darsad > 0) {
                        let KhDetail = (Darsad / 100 * HKB);

                        // حجم اصلی
                        $('#txtKhDetailChK' + i).val(KhDetail.toFixed(2));

                        // درصدهای مرتبط
                        let dVarizi = parseFloat($('#txtDarsadVariziChK' + i).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadChK' + i).val()) || 0;
                        $('#txtDarsadHamlChK' + i).val(100);
                        let dHaml = 100;// parseFloat($('#txtDarsadHamlChK' + i).val()) || 0;

                        // محاسبه حجم هر بخش
                        let vVarizi = (dVarizi / 100) * KhDetail;
                        let vReUse = (dReUse / 100) * KhDetail;
                        let vHaml = (dHaml / 100) * KhDetail;

                        // ست کردن نتایج
                        $('#txtVariziChK' + i).val(vVarizi.toFixed(2));
                        $('#txtReUseHajmChK' + i).val(vReUse.toFixed(2));
                        $('#txtHamlChK' + i).val(vHaml.toFixed(2));

                        totalAssigned += parseFloat(KhDetail.toFixed(2));
                        lastIndex = i;
                    }
                }

                // جبران خطای رندینگ روی آخرین ردیف
                if (lastIndex > -1) {
                    let diff = HKB - totalAssigned;
                    if (Math.abs(diff) >= 0.01) {
                        let lastVal = parseFloat($('#txtKhDetailChK' + lastIndex).val()) || 0;
                        let newVal = lastVal + diff;

                        $('#txtKhDetailChK' + lastIndex).val(newVal.toFixed(2));

                        // بروزرسانی دوباره بخش‌های وابسته به این ردیف
                        let dVarizi = parseFloat($('#txtDarsadVariziChK' + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadChK' + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlChK' + lastIndex).val()) || 0;

                        $('#txtVariziChK' + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtReUseHajmChK' + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtHamlChK' + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }
            });

            $('#txtFromKMForChanelKani').change(function () {
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
                var KME = parseFloat($('#txtToKMForChanelKani').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForChanelKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForChanelKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmChanelKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
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
                debugger;
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
                var KMS = parseFloat($('#txtFromKMForChanelKani').val());//.replace('+', ''));
                if (KMS > KME) {
                    toastr.info('کیلومتراژ خاتمه قبل از کیلومتراژ شروع میباشد', 'اطلاع');
                    $('#txtToKMForChanelKani').addClass('blinking');
                }
                else {
                    $('#txtToKMForChanelKani').removeClass('blinking');

                    let HKB = parseFloat($('#txtHajmChanelKani').val());
                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
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
            toastr.error('مشکل در بارگزاری کانال کنی', 'خطا');
        }
    });
}

function UpdateChanelKaniInfo(KMChanelKaniId, BarAvordUserId, KMNum) {

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
    //var KM = $('#txtFromKMForChanelKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForChanelKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForChanelKani' + KMNum).removeClass('blinking');
    //}
    ///////////////
    //var KM = $('#txtToKMForChanelKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForChanelKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForChanelKani' + KMNum).removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForChanelKani' + KMNum).val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForChanelKani' + KMNum).val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForChanelKani' + KMNum).addClass('blinking');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
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

    if (SumAllDetailsForEdit(KMNum)) check = true;

    if (CheckValuesOfChanelKaniDetailsForEdit(KMNum)) {
        check = true;
    }
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            var DarsadValue = $('#txtDarsadChK' + KMNum + "_" + i).val().trim();
            if (DarsadValue != "0") {
                let obj = {
                    DetailValue: $('#txtKhDetailChK' + KMNum + "_" + i).val(),
                    DarsadValue: DarsadValue,
                    DetailValueOfReCycle: $('#txtReUseHajmChK' + KMNum + "_" + i).val(),
                    DarsadValueOfReCycle: $('#txtReUseDarsadChK' + KMNum + "_" + i).val(),
                    DetailValueOfVarize: $('#txtVariziChK' + KMNum + "_" + i).val(),
                    DarsadValueOfVarize: $('#txtDarsadVariziChK' + KMNum + "_" + i).val(),
                    DetailValueOfHaml: $('#txtHamlChK' + KMNum + "_" + i).val(),
                    DarsadValueOfHaml: $('#txtDarsadHamlChK' + KMNum + "_" + i).val(),
                    NoeKhakBardari: $('#txtChanelKaniItemId' + KMNum + "_" + i).val(),
                };
                dataList.push(obj);
            }
        }

        //KMChanelKaniId = $('#HDFKMAmalyateKhakiIdForEdit').val();
        //KMKhakBardariNum = $('#HDFKMAmalyateKhakiNum').val();
        Year = $('#HDFYear').val();
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMChanelKaniId = KMChanelKaniId;
        vardata.KMNum = KMNum;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.Year = Year;
        debugger;
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
                    toastr.success('اطلاعات کیلومتراژ بدرستی ویرایش گردید', 'ثبت');
                    $('#divExistKMHeader' + KMNum).click();
                    //$('#ViewChanelKaniEzafeBaha' + KMNum).html('');

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

function ViewChanelKaniInfo(KMExistingId, KMNum, BarAvordId) {
    debugger;
    //$('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
    if ($('#MainViewChanelKani' + KMNum).is(':visible')) {
        $('#MainViewChanelKani' + KMNum).slideUp(500);
        //$('#ViewRizMetreKH' + KMNum).slideUp(500);
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

    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/GetDetailsOfKMPayKaniInfoWithKMPayKaniId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            KMPayKaniBarAvordDetailsMore = response.kmPayKaniBarAvordDetailsMore;
            KMPayKaniBarAvordDetails = response.kmPayKaniBarAvordDetails;
            lstPayKaniInfoRizMetre = response.lstPayKaniInfoRizMetre;
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;

            Value = 0;
            strKMAK = `

    <div id="divChanelKaniInfoDetails" class="container-fluid" style="margin-top:10px;padding:0;">
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
            $.each(KMPayKaniBarAvordDetails, function () {
                strKMAK += `
        <div class="container-fluid" style="width:100%;margin:0;padding:0;">
        <div class="row" style="padding:2px 0px;margin:2px 0px;border-bottom:1px solid #ccc;">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
    <input id="txtChanelKaniItemId${KMNum}_${i}" value="${this.noeKhakBardariId}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanChanelKaniItems${KMNum}_${i}" class="spanStyleKhakBardariItems" style="font-size:12px;">
        ${this.title}
      </span>
    </div>`;

                debugger;

                let result = KMPayKaniBarAvordDetailsMore.filter(x => x.payKaniInfoForBarAvordDetailsId === this.id);

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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetailChK${KMNum}_${i}" value="${KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadChK${KMNum}_${i}" value="${DarsadKhDetail}" />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajmChK${KMNum}_${i}" value="${Varizi}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsadChK${KMNum}_${i}" value="${DarsadVarizi}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVariziChK${KMNum}_${i}" value="${ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVariziChK${KMNum}_${i}" value="${DarsadReUseHajm}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHamlChK${KMNum}_${i}" value="${Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHamlChK${KMNum}_${i}" value="${DarsadHaml}"  />
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

            //            strKMAK += `
            //<div class="container-fluid" style="width:100%;margin:0;padding:0;">
            //  <div class="row" 
            //       style="padding:4px 0;margin:2px 0;
            //              border-top:2px solid #333;
            //              background:#f9f9f9;
            //              font-weight:bold;">

            //    <div class="col-4" style="text-align:right;">
            //      جمع کل
            //    </div>

            //    <div class="col-8" style="padding:0;">
            //      <div class="row" style="margin:0;">

            //        <!-- حجم خاکبرداری -->
            //        <div class="col-3" style="padding:0;">
            //          <div class="row">
            //            <div class="col-6" style="text-align:center;padding:0 2px;">
            //              <span>${fmt(totalKhDetail)}</span>
            //            </div>
            //            <div class="col-4" style="text-align:center;padding:0 2px;">
            //              <span>%${fmt(totalDarsadKhDetail)}</span>
            //            </div>
            //          </div>
            //        </div>

            //        <!-- واریزه -->
            //        <div class="col-3" style="padding:0;">
            //          <div class="row">
            //            <div class="col-6" style="text-align:center;padding:0 2px;">
            //              <span>${fmt(totalVarizi)}</span>
            //            </div>

            //          </div>
            //        </div>

            //        <!-- مصرف در خاکریزی -->
            //        <div class="col-3" style="padding:0;">
            //          <div class="row">
            //            <div class="col-6" style="text-align:center;padding:0 2px;">
            //              <span>${fmt(totalReUseHajm)}</span>
            //            </div>

            //          </div>
            //        </div>

            //        <!-- حمل به دپو -->
            //        <div class="col-3" style="padding:0;">
            //          <div class="row">
            //            <div class="col-6" style="text-align:center;padding:0 2px;">
            //              <span>${fmt(totalHaml)}</span>
            //            </div>

            //          </div>
            //        </div>

            //      </div>
            //    </div>

            //  </div>
            //</div>`;


            debugger;


            $('div[id^="MainViewChanelKani"]').slideUp();
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
                    $(this).addClass('ErrorValueStyle');
                    return;
                } else {
                    $(this).removeClass('ErrorValueStyle');
                }

                if (HajmChanelKani == 0 || HajmChanelKani == '' || !$.isNumeric(HajmChanelKani)) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmChanelKani' + KMNum).addClass('ErrorValueStyle');
                    return;
                } else {
                    $('#txtHajmChanelKani' + KMNum).removeClass('ErrorValueStyle');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetailChK" + KMNum + "_" + i).val()) || 0;

                let varizi = parseFloat($("#txtVariziChK" + KMNum + "_" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajmChK" + KMNum + "_" + i).val()) || 0;
                let haml = parseFloat($("#txtHamlChK" + KMNum + "_" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVariziChK" + KMNum + "_" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsadChK" + KMNum + "_" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHamlChK" + KMNum + "_" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail" + KMNum)) {
                    let Zarb = khDetail / HajmChanelKani * 100;
                    $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetailsForEdit(KMNum);
                    if (SumAll > HajmChanelKani) {
                        let NewVal = HajmChanelKani - (SumAll - khDetail);
                        $("#txtKhDetail" + KMNum + "_" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = khDetail / HajmChanelKani * 100;
                        $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVariziChK" + KMNum + "_" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajmChK" + KMNum + "_" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHamlChK" + KMNum + "_" + i).val(((dHaml / 100) * khDetail).toFixed(2));
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
                    let newKhDetail = (dVal / 100) * HajmChanelKani;
                    $('#txtKhDetailChK' + KMNum + "_" + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVariziChK" + KMNum + "_" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajmChK" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHamlChK" + KMNum + "_" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dVarizi = (varizi / khDetail) * 100;
                        $("#txtDarsadVariziChK" + KMNum + "_" + i).val(dVarizi.toFixed(2));
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        dReuse = (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsadChK" + KMNum + "_" + i).val(dReuse.toFixed(2));
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dHaml = (haml / khDetail) * 100;
                        $("#txtDarsadHamlChK" + KMNum + "_" + i).val(dHaml.toFixed(2));
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVariziChK" + KMNum + "_" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajmChK" + KMNum + "_" + i).val()) || 0;
                haml = parseFloat($("#txtHamlChK" + KMNum + "_" + i).val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVariziChK" + KMNum + "_" + i).val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsadChK" + KMNum + "_" + i).val()) || 0;
                dHaml = parseFloat($("#txtDarsadHamlChK" + KMNum + "_" + i).val()) || 0;

                // کنترل مجموع درصدها
                let dSum = dVarizi + dReuse + dHaml;
                if (dSum > 100) {
                    let extra = dSum - 100;
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        dVarizi -= extra;
                        $("#txtDarsadVariziChK" + KMNum + "_" + i).val(dVarizi.toFixed(2));
                        $("#txtVariziChK" + KMNum + "_" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        dReuse -= extra;
                        $("#txtReUseDarsadChK" + KMNum + "_" + i).val(dReuse.toFixed(2));
                        $("#txtReUseHajmChK" + KMNum + "_" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        dHaml -= extra;
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(2));
                        $("#txtHamlChK" + KMNum + "_" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        varizi -= extra;
                        $("#txtVariziChK" + KMNum + "_" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVariziChK" + KMNum + "_" + i).val(((varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        reuseHajm -= extra;
                        $("#txtReUseHajmChK" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsadChK" + KMNum + "_" + i).val(((reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        haml -= extra;
                        $("#txtHamlChK" + KMNum + "_" + i).val(haml.toFixed(2));
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(((haml / khDetail) * 100).toFixed(2));
                    }
                }
            });


            $('#txtHajmChanelKani' + KMNum).off('change').change(function () {
                debugger;


                let HKB = parseFloat($(this).val());

                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('ErrorValueStyle');
                    return;
                }

                //$('#MainViewChanelKani'+KMNum).slideDown(500);
                $('#divChanelKaniInfoDetails').show();
                $(this).removeClass('ErrorValueStyle');

                let totalAssigned = 0;
                let lastIndex = -1;

                for (let i = 1; i <= ActivityLength; i++) {
                    let Darsad = parseFloat($('#txtDarsadChK' + KMNum + "_" + i).val()) || 0;

                    if (Darsad > 0) {
                        let KhDetail = (Darsad / 100 * HKB);

                        // حجم اصلی
                        $('#txtKhDetailChK' + KMNum + "_" + i).val(KhDetail.toFixed(2));

                        // درصدهای مرتبط
                        let dVarizi = parseFloat($('#txtDarsadVariziChK' + KMNum + "_" + i).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadChK' + KMNum + "_" + i).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlChK' + KMNum + "_" + i).val()) || 0;
                        debugger;
                        // محاسبه حجم هر بخش
                        let vVarizi = (dVarizi / 100) * KhDetail;
                        let vReUse = (dReUse / 100) * KhDetail;
                        let vHaml = (dHaml / 100) * KhDetail;

                        // ست کردن نتایج
                        $('#txtVariziChK' + KMNum + "_" + i).val(vVarizi.toFixed(2));
                        $('#txtReUseHajmChK' + KMNum + "_" + i).val(vReUse.toFixed(2));
                        $('#txtHamlChK' + KMNum + "_" + i).val(vHaml.toFixed(2));

                        totalAssigned += parseFloat(KhDetail.toFixed(2));
                        lastIndex = i;
                    }
                }

                // جبران خطای رندینگ روی آخرین ردیف
                if (lastIndex > -1) {
                    let diff = HKB - totalAssigned;
                    if (Math.abs(diff) >= 0.01) {
                        let lastVal = parseFloat($('#txtKhDetailChK' + KMNum + "_" + lastIndex).val()) || 0;
                        let newVal = lastVal + diff;

                        $('#txtKhDetailChK' + KMNum + "_" + lastIndex).val(newVal.toFixed(2));

                        // بروزرسانی دوباره بخش‌های وابسته به این ردیف
                        let dVarizi = parseFloat($('#txtDarsadVariziChK' + KMNum + "_" + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadChK' + KMNum + "_" + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlChK' + KMNum + "_" + lastIndex).val()) || 0;

                        $('#txtVariziChK' + KMNum + "_" + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtReUseHajmChK' + KMNum + "_" + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtHamlChK' + KMNum + "_" + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }

                UpdateChanelKaniInfo(KMExistingId, BarAvordId, KMNum);

            });

            ShowRizMetreChanelKani(KMExistingId, lstPayKaniInfoRizMetre, lstItemFBShomarehForGet, KMNum);

            //ShowA_KhEzafeBaha(KMExistingId, KMNum);

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

            CurrentValue = $('#txtKhDetailChK' + Type).val();
            ValueOfReCycle = $('#txtReUseHajmChK' + Type).val();
            ValueOfVarize = $('#txtVariziChK' + Type).val();
            ValueOfHaml = $('#txtHamlChK' + Type).val();
            ValueOfFaseleHaml = $('#txtFaseleHaml' + Type).val();

            $('#txtDarsadChK' + Type).val(parseFloat(Value) == 0 ? 0 : (parseFloat(CurrentValue) / parseFloat(Value) * 100).toFixed(2));
            $('#txtReUseDarsadChK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadVariziChK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadHamlChK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadFaseleHamlChK' + Type).val(parseFloat(ValueOfVarize) == 0 ? 0 : (parseFloat(ValueOfFaseleHaml) / parseFloat(ValueOfVarize) * 100).toFixed(2));

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

    $('#ViewChanelKani' + KMNum).html();
}

function SumAllDetails() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtKhDetailChK' + i).val()) == '' ? '0' : $.trim($('#txtKhDetailChK' + i).val()));
    }

    HajmChanelKani = parseFloat($('#txtHajmChanelKani').val());
    if (sumAll > HajmChanelKani) {
        $('#txtHajmChanelKani').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم خاکبرداری بیشتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmChanelKani').removeClass('blinking');
        return false;
    }
}

function SaveChanelKaniInfo1(BarAvordUserId) {
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
    ////////////////
    //var KM = $('#txtFromKMForChanelKani').val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForChanelKani').addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForChanelKani').removeClass('blinking');
    //}
    /////////////////
    //var KM = $('#txtToKMForChanelKani').val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForChanelKani').addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForChanelKani').removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForChanelKani').val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForChanelKani').val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForChanelKani').addClass('blinking');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
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

    if (SumAllDetails()) check = true;

    if (CheckValuesOfChanelKaniDetails()) {
        check = true;
    }
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            debugger;

            let obj = {
                DetailValue: $('#txtKhDetailChK' + i).val(),
                DarsadValue: $('#txtDarsadChK' + i).val(),
                DetailValueOfReCycle: $('#txtReUseHajmChK' + i).val(),
                DarsadValueOfReCycle: $('#txtReUseDarsadChK' + i).val(),
                DetailValueOfVarize: $('#txtVariziChK' + i).val(),
                DarsadValueOfVarize: $('#txtDarsadVariziChK' + i).val(),
                DetailValueOfHaml: $('#txtHamlChK' + i).val(),
                DarsadValueOfHaml: $('#txtDarsadHamlChK' + i).val(),
                NoeKhakBardari: $('#txtChanelKaniItemId' + i).val(),
            };

            dataList.push(obj);
        }


        debugger;

        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Type = 3;//کانال کنی
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        $.ajax({
            type: "POST",
            url: "/PayKaniInfoForBarAvords/SavePayKaniInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                info = response.split('_');
                if (info[0] == "OK") {
                    $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
                    $('#HDFKMAmalyateKhakiIdForEdit').val(info[1]);
                    $('#HDFKMAmalyateKhakiNum').val(info[2]);

                    $('#txtHajmChanelKani').val(0);
                    $('#txtFromKMForChanelKani').val('0')
                    $('#txtToKMForChanelKani').val('0')

                    for (let i = 1; i <= ActivityLength; i++) {
                        $('#txtKhDetailChK' + i).val(0);
                        $('#txtDarsadChK' + i).val(0);
                        $('#txtReUseHajmChK' + i).val(0);
                        $('#txtReUseDarsadChK' + i).val(0);
                        $('#txtVariziChK' + i).val(0);
                        $('#txtDarsadVariziChK' + i).val(0);
                        $('#txtHamlChK' + i).val(0);
                        $('#txtDarsadHamlChK' + i).val(0);
                        $('#txtChanelKaniItemId' + i).val(0);
                    }

                    $('#MainViewChanelKaniNew').slideUp(500);

                    ShowExistingKMChanelKani(BarAvordUserId);

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

function UpdateChanelKaniInfo(KMChanelKaniId, BarAvordUserId, KMNum) {

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
    //var KM = $('#txtFromKMForChanelKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForChanelKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForChanelKani' + KMNum).removeClass('blinking');
    //}
    /////////////////
    //var KM = $('#txtToKMForChanelKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForChanelKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForChanelKani' + KMNum).removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForChanelKani' + KMNum).val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForChanelKani' + KMNum).val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForChanelKani' + KMNum).addClass('ErrorValueStyle');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForChanelKani' + KMNum).removeClass('blinking');
    }

    ///////////////
    $('#divChanelKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('ErrorValueStyle');
            check = true;
        }
        else
            $(this).removeClass('ErrorValueStyle');
    });

    if (SumAllDetailsForEdit(KMNum)) check = true;

    if (CheckValuesOfChanelKaniDetailsForEdit(KMNum)) {
        check = true;
    }
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            var DarsadValue = $('#txtDarsadChK' + KMNum + "_" + i).val().trim();
            if (DarsadValue != "0") {
                let obj = {
                    DetailValue: $('#txtKhDetailChK' + KMNum + "_" + i).val(),
                    DarsadValue: DarsadValue,
                    DetailValueOfReCycle: $('#txtReUseHajmChK' + KMNum + "_" + i).val(),
                    DarsadValueOfReCycle: $('#txtReUseDarsadChK' + KMNum + "_" + i).val(),
                    DetailValueOfVarize: $('#txtVariziChK' + KMNum + "_" + i).val(),
                    DarsadValueOfVarize: $('#txtDarsadVariziChK' + KMNum + "_" + i).val(),
                    DetailValueOfHaml: $('#txtHamlChK' + KMNum + "_" + i).val(),
                    DarsadValueOfHaml: $('#txtDarsadHamlChK' + KMNum + "_" + i).val(),
                    NoeKhakBardari: $('#txtChanelKaniItemId' + KMNum + "_" + i).val(),
                };
                dataList.push(obj);
            }
        }

        //KMChanelKaniId = $('#HDFKMAmalyateKhakiIdForEdit').val();
        //KMKhakBardariNum = $('#HDFKMAmalyateKhakiNum').val();
        Year = $('#HDFYear').val();
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMPayKaniId = KMChanelKaniId;
        vardata.FromKM = KMS;
        vardata.ToKM = KME;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.Year = Year;
        vardata.KMNum = KMNum;
        debugger;
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


function CheckValuesOfChanelKaniDetails() {
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        KhDetail = parseFloat($('#txtKhDetailChK' + i).val());

        ReUseHajm = parseFloat($('#txtReUseHajmChK' + i).val());
        if (ReUseHajm > KhDetail) {
            $('#txtReUseHajmChK' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseHajmChK' + i).removeClass('ErrorValueStyle');

        ReUseDarsad = parseFloat($('#txtReUseDarsadChK' + i).val());
        if (ReUseDarsad > 100) {
            $('#txtReUseDarsadChK' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseDarsadChK' + i).removeClass('ErrorValueStyle');
        /////////////
        Varizi = parseFloat($('#txtVariziChK' + i).val());
        Haml = parseFloat($('#txtHamlChK' + i).val());
        if (Varizi > KhDetail) {
            $('#txtVariziChK' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtVariziChK' + i).removeClass('ErrorValueStyle');
        }

        if (Haml > KhDetail) {
            $('#txtHamlChK' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtHamlChK' + i).removeClass('ErrorValueStyle');
        }

        DarsadVarizi = parseFloat($('#txtDarsadVariziChK' + i).val());
        DarsadHaml = parseFloat($('#txtDarsadHamlChK' + i).val());
        if (DarsadVarizi > 100) {
            $('#txtDarsadVariziChK' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadVariziChK' + i).removeClass('ErrorValueStyle');
        }

        if (DarsadHaml > 100) {
            $('#txtDarsadHamlChK' + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadHamlChK' + i).removeClass('ErrorValueStyle');
        }
    }
    return checkValues;
}

function CheckValuesOfChanelKaniDetailsForEdit(KMNum) {
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        KhDetail = parseFloat($('#txtKhDetailChK' + KMNum + "_" + i).val());

        ReUseHajm = parseFloat($('#txtReUseHajmChK' + KMNum + "_" + i).val());
        if (ReUseHajm > KhDetail) {
            $('#txtReUseHajmChK' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseHajmChK' + KMNum + "_" + i).removeClass('ErrorValueStyle');

        ReUseDarsad = parseFloat($('#txtReUseDarsadChK' + KMNum + "_" + i).val());
        if (ReUseDarsad > 100) {
            $('#txtReUseDarsadChK' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else
            $('#txtReUseDarsadChK' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        /////////////
        Varizi = parseFloat($('#txtVariziChK' + KMNum + "_" + i).val());
        Haml = parseFloat($('#txtHamlChK' + KMNum + "_" + i).val());
        if (Varizi > KhDetail) {
            $('#txtVariziChK' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtVariziChK' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }

        if (Haml > KhDetail) {
            $('#txtHamlChK' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtHamlChK' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }

        DarsadVarizi = parseFloat($('#txtDarsadVariziChK' + KMNum + "_" + i).val());
        DarsadHaml = parseFloat($('#txtDarsadHamlChK' + KMNum + "_" + i).val());
        if (DarsadVarizi > 100) {
            $('#txtDarsadVariziChK' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadVariziChK' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }

        if (DarsadHaml > 100) {
            $('#txtDarsadHamlChK' + KMNum + "_" + i).addClass('ErrorValueStyle');
            checkValues = true;
        }
        else {
            $('#txtDarsadHamlChK' + KMNum + "_" + i).removeClass('ErrorValueStyle');
        }
    }
    return checkValues;
}

function ShowRizMetreChanelKani(KMExistingId, lstChanelKaniInfoRizMetre, lstItemFBShomarehForGet, KMNum) {
    debugger;

    var str = '';

    // ساخت HTML بر اساس lstItemFBShomarehForGet

    // گروه‌بندی data.lst بر اساس itemFBShomareh
    let groupedData = {};
    lstChanelKaniInfoRizMetre.forEach(function (row) {
        if (!groupedData[row.itemFBShomareh]) {
            groupedData[row.itemFBShomareh] = [];
        }
        groupedData[row.itemFBShomareh].push(row);
    });

    // ساخت HTML بر اساس lstItemFBShomarehForGet
    if (lstChanelKaniInfoRizMetre.length != 0) {

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

        $targetDivRizMetreChanelKani = $('#ViewRizMetreChanelKani' + KMNum);

        $targetDivRizMetreChanelKani.html(str);
        debugger;
        $targetDivRizMetreChanelKani.slideDown();



        $targetDivRizMetreChanelKani.find("input[type='text'].HasEnteringValue")
            .filter(function () {
                return $(this).val().trim() === "";
            })
            .addClass("blinking")
            .first()
            .focus();


        $targetDivRizMetreChanelKani.on("change", "input[type='text'].HasEnteringValue", function () {
            if ($(this).val().trim() !== "") {
                $(this).removeClass("blinking");
            }
        });


        $targetDivRizMetreChanelKani.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
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




