ActivityLength = 0;

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
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtFromKMForPayKani" value="0"/>
    </div>

    <!-- تا کیلومتراژ -->
    <div class="col-md-1 label-col">
      <span>تا کیلومتراژ:</span>
    </div>
    <div class="col-md-1">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForPayKani" value="0"/>
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
      <a class="btn buttonStyleBoard" style="color:#fff" onclick="SavePayKaniInfo1('${BarAvordUserId}')">
        ذخیره
      </a>
    </div><!--col-md-2 action-col -->
    </div><!--col-12 -->
    </div>

    </div><!-- بخش نمایش -->


</div>
    `;

    $('#ula' + OpId).html(str);

    setTimeout(() => { $('#txtFromKMForPayKani').focus().select(); }, 200);

    $('#ula' + OpId).off('keydown.paykani').on('keydown.paykani', '.khakbardariTextStyle', function (e) {
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

    ShowExistingKMPayKani(BarAvordUserId);

    ShowSelctionPayKani(1, 0, 0, BarAvordUserId, 0, 0, 0, 0, 0);
}

function ShowExistingKMPayKani(BarAvordUserId) {
    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.Type = 2;//پی کنی
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
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>از کیلومتراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>تا کیلومتراژ</span></div>
                        <div class="col-2" style="border-left: 1px solid #ccc;border-right: 1px solid #ccc;"><span>حجم پی کنی</span></div>
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
    <div id="divExistKMHeader${KMNum}" class="row col-12 ExistKMHeaderStyle" onclick="ViewPayKaniInfo('${KMExistingId}'` + ',' + `${KMNum}` + ',' + `'${BarAvordUserId}')">
    <div class="col-md-1 label-col" style="text-align:center">
      <span>${KMNum}</span>
    </div>
    <!-- از کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle  input-sm text-center" id="txtFromKMForPayKani${KMNum}" value="${FromKM}" onclick="event.stopPropagation();"/>
    </div>

    <!-- تا کیلومتراژ -->
    
    <div class="col-md-2">
      <input type="text" class="form-control_1 khakbardariTextStyle input-sm text-center" id="txtToKMForPayKani${KMNum}" value="${ToKM}" onclick="event.stopPropagation();"/>
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

  <div class="row col-12" style="direction:ltr;display:none" id="MainViewPayKani${KMNum}" class="khakbardari-view">
    <div id="ViewPayKani${KMNum}" class="khakbardari-view" style="direction: rtl;">
    </div>
    <div class="col-md-2 action-col">
      <a class="btn buttonStyleBoard" style="color:#fff" onclick="UpdatePayKaniInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum})" onclick="event.stopPropagation();">
        ذخیره
      </a>
    </div>
  <div id="ViewRizMetrePayKani${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  <div id="ViewPayKaniEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
  </div><!-- MainViewPayKani -->
  </div>
    `;

                    //    str += '<div class=\'col-md-12\' style=\'margin:1px 0px;\'><a class=\'ExsitingPolStyle\' onclick=\"SelctionKMAmalyateKhaki($(this),\'' + KMExistingId + '\',\'' + KMNum
                    //        + '\')\" ondblclick=\"ShowSelctionKhakBardari(0,' + KMExistingId + ',' + KMNum + ',' + BarAvordUserId + ',' + "'" + FromKM + "'" + ',' + "'" + ToKM + "'" + ',' + "'" + FromKMSplit + "'" + ',' + "'" + ToKMSplit + "'" + ',' + Value + ')\">' + count++
                    //        + ' - کیلومتراژ' + '<label>' + FromKMSplit + ' - ' + ToKMSplit + '</label>' + '</a></div>';
                });

                $('#divPayKaniExistingKM').html(strSEKB);
                $('#divPayKaniExistingKM').find('#MainViewPayKani' + KMNum).hide();
            }

            //$('#ula' + OpId).find('divPayKaniExistingKM').html(str);
            //$('#divViewExistingKMAmalyateKhaki').html(str);
            //$('#aViewExistingKMAmalyateKhaki').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
        }
    });
}

function ShowSelctionPayKani(IsNew, KMExistingId, KMNum, BarAvordId, FromKM, ToKM, FromKMSplit, ToKMSplit, Value) {

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
    //پی کنی
    vardata.Type = 2;

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
        <input id="txtPayKaniItemId${i + 1}" value="${ActivityTitleComplete[i].id}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanPayKaniItems${i + 1}" class="spanStyleKhakBardariItems" style="font-size:12px;">
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetailPK${i + 1}" value="0" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadPK${i + 1}" value="0" />
            </div>
          </div>
        </div>

        <!-- واریزه -->
        <div class="col-3" style="padding:0;">
          <div class="row">
            <div class="col-6" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVariziPK${i + 1}" value="0"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajmPK${i + 1}" value="0"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHamlPK${i + 1}" value="0"  />
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
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmPayKani').addClass('blinking');
                    return;
                } else {
                    $('#txtHajmPayKani').removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetailPK" + i).val()) || 0;

                let varizi = parseFloat($("#txtVariziPK" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajmPK" + i).val()) || 0;
                let haml = parseFloat($("#txtHamlPK" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVariziPK" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsadPK" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHamlPK" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail")) {
                    let Zarb = khDetail / HajmPayKani * 100;
                    $("#txtDarsadPK" + i).val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmPayKani) {
                        let NewVal = HajmPayKani - (SumAll - khDetail);
                        $("#txtKhDetailPK" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = khDetail / HajmPayKani * 100;
                        $("#txtDarsadPK" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVariziPK" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajmPK" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHamlPK" + i).val(((dHaml / 100) * khDetail).toFixed(2));
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
                    $('#txtKhDetailPK' + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi")) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVariziPK" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajmPK" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHamlPK" + i).val(haml.toFixed(2));
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
                varizi = parseFloat($("#txtVariziPK" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajmPK" + i).val()) || 0;
                haml = parseFloat($("#txtHamlPK" + i).val()) || 0;

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
                        $("#txtVariziPK" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        dReuse -= extra;
                        $("#txtReUseDarsadPK" + i).val(dReuse.toFixed(2));
                        $("#txtReUseHajmPK" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        dHaml -= extra;
                        $("#txtDarsadHamlPK" + i).val(dHaml.toFixed(2));
                        $("#txtHamlPK" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        varizi -= extra;
                        $("#txtVariziPK" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVariziPK" + i).val(((varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        reuseHajm -= extra;
                        $("#txtReUseHajmPK" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsadPK" + i).val(((reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        haml -= extra;
                        $("#txtHamlPK" + i).val(haml.toFixed(2));
                        $("#txtDarsadHamlPK" + i).val(((haml / khDetail) * 100).toFixed(2));
                    }
                }
            });

            $('#txtHajmPayKani').on('keydown', function (e) {
                if (e.key === 'Enter') {
                    e.preventDefault();

                    let HKB = parseFloat($(this).val());

                    var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
                    var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));

                    if (KMS == 0 || KME == 0) {
                        $('#txtFromKMForPayKani').addClass('blinking');
                        $('#txtToKMForPayKanii').addClass('blinking');
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
                        $('#txtFromKMFoPayKani').removeClass('blinking');
                    }

                    if (!$.isNumeric(HKB) || HKB <= 0) {
                        toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                        $(this).addClass('blinking');
                        return;
                    }

                    $('#MainViewPayKaniNew').slideDown(500);
                }
            });


            $('#txtHajmPayKani').change(function () {

                debugger;

                let HKB = parseFloat($(this).val());

                var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
                var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));

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
                    $('#txtDarsadPK' + i).val(100)
                    let Darsad = 100;

                    if (Darsad > 0) {
                        let KhDetail = (Darsad / 100 * HKB);

                        // حجم اصلی
                        $('#txtKhDetailPK' + i).val(KhDetail.toFixed(2));

                        // درصدهای مرتبط
                        let dVarizi = parseFloat($('#txtDarsadVariziPK' + i).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadPK' + i).val()) || 0;
                        $('#txtDarsadHamlPK' + i).val(100);
                        let dHaml = 100;// parseFloat($('#txtDarsadHamlPK' + i).val()) || 0;

                        // محاسبه حجم هر بخش
                        let vVarizi = (dVarizi / 100) * KhDetail;
                        let vReUse = (dReUse / 100) * KhDetail;
                        let vHaml = (dHaml / 100) * KhDetail;

                        // ست کردن نتایج
                        $('#txtVariziPK' + i).val(vVarizi.toFixed(2));
                        $('#txtReUseHajmPK' + i).val(vReUse.toFixed(2));
                        $('#txtHamlPK' + i).val(vHaml.toFixed(2));

                        totalAssigned += parseFloat(KhDetail.toFixed(2));
                        lastIndex = i;
                    }
                }

                // جبران خطای رندینگ روی آخرین ردیف
                if (lastIndex > -1) {
                    let diff = HKB - totalAssigned;
                    if (Math.abs(diff) >= 0.01) {
                        let lastVal = parseFloat($('#txtKhDetailPK' + lastIndex).val()) || 0;
                        let newVal = lastVal + diff;

                        $('#txtKhDetailPK' + lastIndex).val(newVal.toFixed(2));

                        // بروزرسانی دوباره بخش‌های وابسته به این ردیف
                        let dVarizi = parseFloat($('#txtDarsadVariziPK' + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadPK' + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlPK' + lastIndex).val()) || 0;

                        $('#txtVariziPK' + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtReUseHajmPK' + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtHamlPK' + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }
            });

            $('#txtFromKMForPayKani').change(function () {
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
                var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));
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
                //var KMSplit = KM.split('+');
                //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
                //    $(this).addClass('blinking');
                //    toastr.info('کیلومتراژ خاتمه وارد شده طبق فرمت نمی باشد', 'فرمت 000+000 می باشد');
                //}
                //else {
                //    $(this).removeClass('blinking');
                //}


                var KME = parseFloat(KM);//.replace('+', ''));
                var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
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
        },
        error: function (response) {
            toastr.error('مشکل در بارگزاری پی کنی', 'خطا');
        }
    });
}

function UpdatePayKaniInfo(KMPayKaniId, BarAvordUserId, KMNum) {

    debugger;
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
    var KM = $('#txtFromKMForPayKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForPayKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForPayKani' + KMNum).removeClass('blinking');
    //}
    ///////////////
    var KM = $('#txtToKMForPayKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForPayKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForPayKani' + KMNum).removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForPayKani' + KMNum).val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForPayKani' + KMNum).val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForPayKani' + KMNum).addClass('blinking');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForPayKani' + KMNum).removeClass('blinking');
    }

    ///////////////
    $('#divPayKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    if (SumAllDetailsForEdit(KMNum)) check = true;

    if (CheckValuesOfPayKaniDetailsForEdit(KMNum)) {
        check = true;
    }
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            var DarsadValue = $('#txtDarsadPK' + KMNum + "_" + i).val().trim();
            if (DarsadValue != "0") {
                let obj = {
                    DetailValue: $('#txtKhDetailPK' + KMNum + "_" + i).val(),
                    DarsadValue: DarsadValue,
                    DetailValueOfReCycle: $('#txtReUseHajmPK' + KMNum + "_" + i).val(),
                    DarsadValueOfReCycle: $('#txtReUseDarsadPK' + KMNum + "_" + i).val(),
                    DetailValueOfVarize: $('#txtVariziPK' + KMNum + "_" + i).val(),
                    DarsadValueOfVarize: $('#txtDarsadVariziPK' + KMNum + "_" + i).val(),
                    DetailValueOfHaml: $('#txtHamlPK' + KMNum + "_" + i).val(),
                    DarsadValueOfHaml: $('#txtDarsadHamlPK' + KMNum + "_" + i).val(),
                    NoeKhakBardari: $('#txtPayKaniItemId' + KMNum + "_" + i).val(),
                };
                dataList.push(obj);
            }
        }

        //KMPayKaniId = $('#HDFKMAmalyateKhakiIdForEdit').val();
        //KMKhakBardariNum = $('#HDFKMAmalyateKhakiNum').val();
        Year = $('#HDFYear').val();
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMPayKaniId = KMPayKaniId;
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
                    //$('#ViewPayKaniEzafeBaha' + KMNum).html('');

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

function ViewPayKaniInfo(KMExistingId, KMNum, BarAvordId) {
    debugger;
    //$('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
    if ($('#MainViewPayKani' + KMNum).is(':visible')) {
        $('#MainViewPayKani' + KMNum).slideUp(500);
        //$('#ViewRizMetreKH' + KMNum).slideUp(500);
        return
    }

    $('#ViewPayKaniEzafeBaha' + KMNum).html('');

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.PayKaniInfoForBarAvordId = KMExistingId;
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
            debugger;
            KMPayKaniBarAvordDetailsMore = response.kmPayKaniBarAvordDetailsMore;
            KMPayKaniBarAvordDetails = response.kmPayKaniBarAvordDetails;
            lstPayKaniInfoRizMetre = response.lstPayKaniInfoRizMetre;
            lstItemFBShomarehForGet = response.lstItemFBShomarehForGet;

            Value = 0;
            strKMAK = `

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
    <input id="txtPayKaniItemId${KMNum}_${i}" value="${this.noeKhakBardariId}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanPayKaniItems${KMNum}_${i}" class="spanStyleKhakBardariItems" style="font-size:12px;">
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtKhDetailPK${KMNum}_${i}" value="${KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadPK${KMNum}_${i}" value="${DarsadKhDetail}" />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVariziPK${KMNum}_${i}" value="${Varizi}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVariziPK${KMNum}_${i}" value="${DarsadVarizi}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajmPK${KMNum}_${i}" value="${ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsadPK${KMNum}_${i}" value="${DarsadReUseHajm}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHamlPK${KMNum}_${i}" value="${Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHamlPK${KMNum}_${i}" value="${DarsadHaml}"  />
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


            $('div[id^="MainViewPayKani"]').slideUp();
            //$('div[id^="ViewRizMetreKH"]').slideUp();

            $('#ViewPayKani' + KMNum).html(strKMAK);
            $('#MainViewPayKani' + KMNum).slideDown();

            HajmPayKani = $('#txtHajmPayKani' + KMNum).val();


            $('#ViewPayKani' + KMNum + ' input[type="text"]').off('change').change(function () {
                debugger;
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
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $('#txtHajmPayKani' + KMNum).addClass('blinking');
                    return;
                } else {
                    $('#txtHajmPayKani' + KMNum).removeClass('blinking');
                }

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetailPK" + KMNum + "_" + i).val()) || 0;

                let varizi = parseFloat($("#txtVariziPK" + KMNum + "_" + i).val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajmPK" + KMNum + "_" + i).val()) || 0;
                let haml = parseFloat($("#txtHamlPK" + KMNum + "_" + i).val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVariziPK" + KMNum + "_" + i).val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsadPK" + KMNum + "_" + i).val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHamlPK" + KMNum + "_" + i).val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail" + KMNum)) {
                    let Zarb = HajmPayKani===0?0:(khDetail / HajmPayKani) * 100;
                    $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetailsForEdit(KMNum);
                    if (SumAll > HajmPayKani) {
                        let NewVal = HajmPayKani - (SumAll - khDetail);
                        $("#txtKhDetail" + KMNum + "_" + i).val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = HajmPayKani===0?0:(khDetail / HajmPayKani) * 100;
                        $("#txtDarsad" + KMNum + "_" + i).val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVariziPK" + KMNum + "_" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajmPK" + KMNum + "_" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHamlPK" + KMNum + "_" + i).val(((dHaml / 100) * khDetail).toFixed(2));
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
                    let newKhDetail = (dVal / 100) * HajmPayKani;
                    $('#txtKhDetailPK' + KMNum + "_" + i).val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVariziPK" + KMNum + "_" + i).val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajmPK" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHamlPK" + KMNum + "_" + i).val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dVarizi = khDetail===0?0: (varizi / khDetail) * 100;
                        $("#txtDarsadVariziPK" + KMNum + "_" + i).val(dVarizi.toFixed(2));
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        dReuse = khDetail===0?0: (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsadPK" + KMNum + "_" + i).val(dReuse.toFixed(2));
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        dHaml = khDetail===0?0: (haml / khDetail) * 100;
                        $("#txtDarsadHamlPK" + KMNum + "_" + i).val(dHaml.toFixed(2));
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVariziPK" + KMNum + "_" + i).val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajmPK" + KMNum + "_" + i).val()) || 0;
                haml = parseFloat($("#txtHamlPK" + KMNum + "_" + i).val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVariziPK" + KMNum + "_" + i).val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsadPK" + KMNum + "_" + i).val()) || 0;
                dHaml = parseFloat($("#txtDarsadHamlPK" + KMNum + "_" + i).val()) || 0;

                // کنترل مجموع درصدها
                let dSum = dVarizi + dReuse + dHaml;
                if (dSum > 100) {
                    let extra = dSum - 100;
                    if (changedId.includes("DarsadVarizi" + KMNum)) {
                        dVarizi -= extra;
                        $("#txtDarsadVariziPK" + KMNum + "_" + i).val(dVarizi.toFixed(2));
                        $("#txtVariziPK" + KMNum + "_" + i).val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad" + KMNum)) {
                        dReuse -= extra;
                        $("#txtReUseDarsadPK" + KMNum + "_" + i).val(dReuse.toFixed(2));
                        $("#txtReUseHajmPK" + KMNum + "_" + i).val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml" + KMNum)) {
                        dHaml -= extra;
                        $("#txtDarsadHaml" + KMNum + "_" + i).val(dHaml.toFixed(2));
                        $("#txtHamlPK" + KMNum + "_" + i).val(((dHaml / 100) * khDetail).toFixed(2));
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        varizi -= extra;
                        $("#txtVariziPK" + KMNum + "_" + i).val(varizi.toFixed(2));
                        $("#txtDarsadVariziPK" + KMNum + "_" + i).val((khDetail===0?0:(varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm" + KMNum)) {
                        reuseHajm -= extra;
                        $("#txtReUseHajmPK" + KMNum + "_" + i).val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsadPK" + KMNum + "_" + i).val((khDetail===0?0:(reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml" + KMNum) && !changedId.includes("Darsad" + KMNum)) {
                        haml -= extra;
                        $("#txtHamlPK" + KMNum + "_" + i).val(haml.toFixed(2));
                        $("#txtDarsadHaml" + KMNum + "_" + i).val((khDetail===0?0:(haml / khDetail) * 100).toFixed(2));
                    }
                }
            });


            $('#txtHajmPayKani' + KMNum).off('change').change(function () {
                debugger;


                let HKB = parseFloat($(this).val());

                if (!$.isNumeric(HKB) || HKB <= 0) {
                    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                    $(this).addClass('blinking');
                    return;
                }

                //$('#MainViewPayKani'+KMNum).slideDown(500);
                $('#divPayKaniInfoDetails').show();
                $(this).removeClass('blinking');

                let totalAssigned = 0;
                let lastIndex = -1;

                for (let i = 1; i <= ActivityLength; i++) {
                    let Darsad = parseFloat($('#txtDarsadPK' + KMNum + "_" + i).val()) || 0;

                    if (Darsad > 0) {
                        let KhDetail = (Darsad / 100 * HKB);

                        // حجم اصلی
                        $('#txtKhDetailPK' + KMNum + "_" + i).val(KhDetail.toFixed(2));

                        // درصدهای مرتبط
                        let dVarizi = parseFloat($('#txtDarsadVariziPK' + KMNum + "_" + i).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadPK' + KMNum + "_" + i).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlPK' + KMNum + "_" + i).val()) || 0;
                        debugger;
                        // محاسبه حجم هر بخش
                        let vVarizi = (dVarizi / 100) * KhDetail;
                        let vReUse = (dReUse / 100) * KhDetail;
                        let vHaml = (dHaml / 100) * KhDetail;

                        // ست کردن نتایج
                        $('#txtVariziPK' + KMNum + "_" + i).val(vVarizi.toFixed(2));
                        $('#txtReUseHajmPK' + KMNum + "_" + i).val(vReUse.toFixed(2));
                        $('#txtHamlPK' + KMNum + "_" + i).val(vHaml.toFixed(2));

                        totalAssigned += parseFloat(KhDetail.toFixed(2));
                        lastIndex = i;
                    }
                }

                // جبران خطای رندینگ روی آخرین ردیف
                if (lastIndex > -1) {
                    let diff = HKB - totalAssigned;
                    if (Math.abs(diff) >= 0.01) {
                        let lastVal = parseFloat($('#txtKhDetailPK' + KMNum + "_" + lastIndex).val()) || 0;
                        let newVal = lastVal + diff;

                        $('#txtKhDetailPK' + KMNum + "_" + lastIndex).val(newVal.toFixed(2));

                        // بروزرسانی دوباره بخش‌های وابسته به این ردیف
                        let dVarizi = parseFloat($('#txtDarsadVariziPK' + KMNum + "_" + lastIndex).val()) || 0;
                        let dReUse = parseFloat($('#txtReUseDarsadPK' + KMNum + "_" + lastIndex).val()) || 0;
                        let dHaml = parseFloat($('#txtDarsadHamlPK' + KMNum + "_" + lastIndex).val()) || 0;

                        $('#txtVariziPK' + KMNum + "_" + lastIndex).val(((dVarizi / 100) * newVal).toFixed(2));
                        $('#txtReUseHajmPK' + KMNum + "_" + lastIndex).val(((dReUse / 100) * newVal).toFixed(2));
                        $('#txtHamlPK' + KMNum + "_" + lastIndex).val(((dHaml / 100) * newVal).toFixed(2));
                    }
                }

                UpdatePayKaniInfo(KMExistingId, BarAvordId, KMNum);

            });

            ShowRizMetrePayKani(KMExistingId, lstPayKaniInfoRizMetre, lstItemFBShomarehForGet, KMNum);

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

            CurrentValue = $('#txtKhDetailPK' + Type).val();
            ValueOfReCycle = $('#txtReUseHajmPK' + Type).val();
            ValueOfVarize = $('#txtVariziPK' + Type).val();
            ValueOfHaml = $('#txtHamlPK' + Type).val();
            ValueOfFaseleHaml = $('#txtFaseleHaml' + Type).val();

            $('#txtDarsadPK' + Type).val(parseFloat(Value) == 0 ? 0 : (parseFloat(CurrentValue) / parseFloat(Value) * 100).toFixed(2));
            $('#txtReUseDarsadPK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfReCycle) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadVariziPK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfVarize) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadHamlPK' + Type).val(parseFloat(CurrentValue) == 0 ? 0 : (parseFloat(ValueOfHaml) / parseFloat(CurrentValue) * 100).toFixed(2));
            $('#txtDarsadFaseleHamlPK' + Type).val(parseFloat(ValueOfVarize) == 0 ? 0 : (parseFloat(ValueOfFaseleHaml) / parseFloat(ValueOfVarize) * 100).toFixed(2));

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

    $('#ViewPayKani' + KMNum).html();
}

function SumAllDetailsPayKani() {
    sumAll = 0;
    for (var i = 1; i <= ActivityLength; i++) {
        sumAll += parseFloat($.trim($('#txtKhDetailPK' + i).val()) == '' ? '0' : $.trim($('#txtKhDetailPK' + i).val()));
    }

    HajmPayKani = parseFloat($('#txtHajmPayKani').val());
    if (sumAll > HajmPayKani) {
        $('#txtHajmPayKani').addClass('blinking');
        toastr.info('احجام وارد شده نبایستی از حجم خاکبرداری بیشتر باشد', 'اطلاع');
        return true;
    }
    else {
        $('#txtHajmPayKani').removeClass('blinking');
        return false;
    }
}

function SavePayKaniInfo1(BarAvordUserId) {
    debugger;
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
    ////////////////
    //var KM = $('#txtFromKMForPayKani').val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForPayKani').addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForPayKani').removeClass('blinking');
    //}
    /////////////////
    //var KM = $('#txtToKMForPayKani').val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForPayKani').addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForPayKani').removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForPayKani').val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForPayKani').val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForPayKani').addClass('blinking');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForPayKani').removeClass('blinking');
    }

    ///////////////
    $('#divPayKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    check =SumAllDetailsPayKani();

    check1 = CheckValuesOfPayKaniDetails();
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            debugger;

            let obj = {
                DetailValue: $('#txtKhDetailPK' + i).val(),
                DarsadValue: $('#txtDarsadPK' + i).val(),
                DetailValueOfReCycle: $('#txtReUseHajmPK' + i).val(),
                DarsadValueOfReCycle: $('#txtReUseDarsadPK' + i).val(),
                DetailValueOfVarize: $('#txtVariziPK' + i).val(),
                DarsadValueOfVarize: $('#txtDarsadVariziPK' + i).val(),
                DetailValueOfHaml: $('#txtHamlPK' + i).val(),
                DarsadValueOfHaml: $('#txtDarsadHamlPK' + i).val(),
                NoeKhakBardari: $('#txtPayKaniItemId' + i).val(),
            };

            dataList.push(obj);
        }


        debugger;

        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Type = 2;//پی کنی
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

                    $('#txtHajmPayKani').val(0);
                    $('#txtFromKMForPayKani').val('000+000')
                    $('#txtToKMForPayKani').val('000+000')

                    for (let i = 1; i <= ActivityLength; i++) {
                        $('#txtKhDetailPK' + i).val(0);
                        $('#txtDarsadPK' + i).val(0);
                        $('#txtReUseHajmPK' + i).val(0);
                        $('#txtReUseDarsadPK' + i).val(0);
                        $('#txtVariziPK' + i).val(0);
                        $('#txtDarsadVariziPK' + i).val(0);
                        $('#txtHamlPK' + i).val(0);
                        $('#txtDarsadHamlPK' + i).val(0);
                        $('#txtPayKaniItemId' + i).val(0);
                    }

                    $('#MainViewPayKaniNew').slideUp(500);

                    ShowExistingKMPayKani(BarAvordUserId);

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

function UpdatePayKaniInfo(KMPayKaniId, BarAvordUserId, KMNum) {

    debugger;
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
    //var KM = $('#txtFromKMForPayKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtFromKMForPayKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtFromKMForPayKani' + KMNum).removeClass('blinking');
    //}
    /////////////////
    //var KM = $('#txtToKMForPayKani' + KMNum).val();
    //var KMSplit = KM.split('+');
    //if (KMSplit.length != 2 || KMSplit[1].length != 3 || KMSplit[0].length > 3) {
    //    $('#txtToKMForPayKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtToKMForPayKani' + KMNum).removeClass('blinking');
    //}
    /////////////
    var KME = parseFloat($('#txtToKMForPayKani' + KMNum).val());//.replace('+', ''));
    var KMS = parseFloat($('#txtFromKMForPayKani' + KMNum).val());//.replace('+', ''));
    if (KMS >= KME) {
        $('#txtToKMForPayKani' + KMNum).addClass('blinking');
        toastr.info('کیلومتراژ انتها بایستی بعد از کیلومتراژ شروع باشد', 'اطلاع');
        check = true;
    }
    else {
        $('#txtToKMForPayKani' + KMNum).removeClass('blinking');
    }

    ///////////////
    $('#divPayKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    if (SumAllDetailsForEdit(KMNum)) check = true;

    if (CheckValuesOfPayKaniDetailsForEdit(KMNum)) {
        check = true;
    }
    debugger;

    if (!check) {

        let dataList = [];

        for (let i = 1; i <= ActivityLength; i++) {
            var DarsadValue = $('#txtDarsadPK' + KMNum + "_" + i).val().trim();
            if (DarsadValue != "0") {
                let obj = {
                    DetailValue: $('#txtKhDetailPK' + KMNum + "_" + i).val(),
                    DarsadValue: DarsadValue,
                    DetailValueOfReCycle: $('#txtReUseHajmPK' + KMNum + "_" + i).val(),
                    DarsadValueOfReCycle: $('#txtReUseDarsadPK' + KMNum + "_" + i).val(),
                    DetailValueOfVarize: $('#txtVariziPK' + KMNum + "_" + i).val(),
                    DarsadValueOfVarize: $('#txtDarsadVariziPK' + KMNum + "_" + i).val(),
                    DetailValueOfHaml: $('#txtHamlPK' + KMNum + "_" + i).val(),
                    DarsadValueOfHaml: $('#txtDarsadHamlPK' + KMNum + "_" + i).val(),
                    NoeKhakBardari: $('#txtPayKaniItemId' + KMNum + "_" + i).val(),
                };
                dataList.push(obj);
            }
        }

        //KMPayKaniId = $('#HDFKMAmalyateKhakiIdForEdit').val();
        //KMKhakBardariNum = $('#HDFKMAmalyateKhakiNum').val();
        Year = $('#HDFYear').val();
        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.KMPayKaniId = KMPayKaniId;
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


function CheckValuesOfPayKaniDetails() {
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        KhDetail = parseFloat($('#txtKhDetailPK' + i).val());

        ReUseHajm = parseFloat($('#txtReUseHajmPK' + i).val());
        if (ReUseHajm > KhDetail) {
            $('#txtReUseHajmPK' + i).addClass('blinking');
            checkValues = true;
        }
        else
            $('#txtReUseHajmPK' + i).removeClass('blinking');

        ReUseDarsad = parseFloat($('#txtReUseDarsadPK' + i).val());
        if (ReUseDarsad > 100) {
            $('#txtReUseDarsadPK' + i).addClass('blinking');
            checkValues = true;
        }
        else
            $('#txtReUseDarsadPK' + i).removeClass('blinking');
        /////////////
        Varizi = parseFloat($('#txtVariziPK' + i).val());
        Haml = parseFloat($('#txtHamlPK' + i).val());
        if (Varizi > KhDetail) {
            $('#txtVariziPK' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtVariziPK' + i).removeClass('blinking');
        }

        if (Haml > KhDetail) {
            $('#txtHamlPK' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtHamlPK' + i).removeClass('blinking');
        }

        DarsadVarizi = parseFloat($('#txtDarsadVariziPK' + i).val());
        DarsadHaml = parseFloat($('#txtDarsadHamlPK' + i).val());
        if (DarsadVarizi > 100) {
            $('#txtDarsadVariziPK' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadVariziPK' + i).removeClass('blinking');
        }

        if (DarsadHaml > 100) {
            $('#txtDarsadHamlPK' + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadHamlPK' + i).removeClass('blinking');
        }
    }
    return checkValues;
}

function CheckValuesOfPayKaniDetailsForEdit(KMNum) {
    checkValues = false;
    for (var i = 1; i <= ActivityLength; i++) {
        KhDetail = parseFloat($('#txtKhDetailPK' + KMNum + "_" + i).val());

        ReUseHajm = parseFloat($('#txtReUseHajmPK' + KMNum + "_" + i).val());
        if (ReUseHajm > KhDetail) {
            $('#txtReUseHajmPK' + KMNum + "_" + i).addClass('blinking');
            checkValues = true;
        }
        else
            $('#txtReUseHajmPK' + KMNum + "_" + i).removeClass('blinking');

        ReUseDarsad = parseFloat($('#txtReUseDarsadPK' + KMNum + "_" + i).val());
        if (ReUseDarsad > 100) {
            $('#txtReUseDarsadPK' + KMNum + "_" + i).addClass('blinking');
            checkValues = true;
        }
        else
            $('#txtReUseDarsadPK' + KMNum + "_" + i).removeClass('blinking');
        /////////////
        Varizi = parseFloat($('#txtVariziPK' + KMNum + "_" + i).val());
        Haml = parseFloat($('#txtHamlPK' + KMNum + "_" + i).val());
        if (Varizi > KhDetail) {
            $('#txtVariziPK' + KMNum + "_" + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtVariziPK' + KMNum + "_" + i).removeClass('blinking');
        }

        if (Haml > KhDetail) {
            $('#txtHamlPK' + KMNum + "_" + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtHamlPK' + KMNum + "_" + i).removeClass('blinking');
        }

        DarsadVarizi = parseFloat($('#txtDarsadVariziPK' + KMNum + "_" + i).val());
        DarsadHaml = parseFloat($('#txtDarsadHamlPK' + KMNum + "_" + i).val());
        if (DarsadVarizi > 100) {
            $('#txtDarsadVariziPK' + KMNum + "_" + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadVariziPK' + KMNum + "_" + i).removeClass('blinking');
        }

        if (DarsadHaml > 100) {
            $('#txtDarsadHamlPK' + KMNum + "_" + i).addClass('blinking');
            checkValues = true;
        }
        else {
            $('#txtDarsadHamlPK' + KMNum + "_" + i).removeClass('blinking');
        }
    }
    return checkValues;
}

function ShowRizMetrePayKani(KMExistingId, lstPayKaniInfoRizMetre, lstItemFBShomarehForGet, KMNum) {
    debugger;

    var str = '';

    // ساخت HTML بر اساس lstItemFBShomarehForGet

    // گروه‌بندی data.lst بر اساس itemFBShomareh
    let groupedData = {};
    lstPayKaniInfoRizMetre.forEach(function (row) {
        if (!groupedData[row.itemFBShomareh]) {
            groupedData[row.itemFBShomareh] = [];
        }
        groupedData[row.itemFBShomareh].push(row);
    });

    // ساخت HTML بر اساس lstItemFBShomarehForGet
    if (lstPayKaniInfoRizMetre.length != 0) {

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

        $targetDivRizMetrePayKani = $('#ViewRizMetrePayKani' + KMNum);

        $targetDivRizMetrePayKani.html(str);
        debugger;
        $targetDivRizMetrePayKani.slideDown();



        $targetDivRizMetrePayKani.find("input[type='text'].HasEnteringValue")
            .filter(function () {
                return $(this).val().trim() === "";
            })
            .addClass("blinking")
            .first()
            .focus();


        $targetDivRizMetrePayKani.on("change", "input[type='text'].HasEnteringValue", function () {
            if ($(this).val().trim() !== "") {
                $(this).removeClass("blinking");
            }
        });


        $targetDivRizMetrePayKani.on("keypress", "input[type='text'].HasEnteringValue", function (e) {
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




