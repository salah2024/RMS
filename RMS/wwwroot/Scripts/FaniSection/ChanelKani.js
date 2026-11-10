ActivityLength = 0;
var SumAllDetailsChanelKani = 0;

function SumRizMetreChanelKani(Type) {
    OpId = $('#HDFOperationId').val();
    Year = $('#HDFYear').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    BarAvordUserId = $('#HDFBarAvordUserID').val();

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.OpId = OpId;
    vardata.Year = Year;
    vardata.NoeFBId = NoeFBId;

    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/SumRizMetreWithOperationId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                dSum = parseFloat(info[1]).toFixed(0);
                SumAllDetailsChanelKani = dSum;
                $('#txtKhDetailChK').val(dSum);
                $('#txtDarsadChK').val(100);

                DarsadVariziPK = parseFloat($('#txtDarsadVariziChK').val());
                VariziPK = isNaN(DarsadVariziPK) ? 0 : DarsadVariziPK * dSum / 100;
                $('#txtVariziChK').val(VariziPK.toFixed(2));

                ReUseDarsadPK = parseFloat($('#txtReUseDarsadChK').val());
                ReUseHajmPK = isNaN(ReUseDarsadPK) ? 0 : ReUseDarsadPK * dSum / 100;
                $('#txtReUseHajmChK').val(ReUseHajmPK.toFixed(2));

                DarsadHamlPK = parseFloat($('#txtDarsadHamlChK').val());
                ReUseHajmPK = isNaN(DarsadHamlPK) ? 0 : DarsadHamlPK * dSum / 100;
                $('#txtHamlChK').val(ReUseHajmPK.toFixed(2));

                SaveChanelKaniInfo1(BarAvordUserId, Type);
            }
            else
                toastr.error('مشکل در جمع ریزمتره', 'خطا');
        },
        error: function (response) {
            toastr.error('مشکل در جمع ریزمتره', 'خطا');
        }
    });
}

function formatNumberChanelKani(num) {
    if (num == null) return '';
    if (num == 0) return '';
    return Number(num).toLocaleString('fa-IR'); // یا 'en-US' برای کاما
}
function ChanelKaniClick111(OpId, Type) {

    $('#HDFOperationId').val(OpId);
    ForItem = $('#HDFItemsFBShomareh').val();

    BarAvordUserId = $('#HDFBarAvordUserID').val();

    NoeFBId = parseInt($('#HDFNoeFB').val());

    str = "";

    str += `


  <!-- بخش نمایش -->
  <div id="MainViewChanelKaniNew" class="khakbardari-view">
  <div id="ViewChanelKaniNew" class="khakbardari-view">
  </div>

  <!-- دکمه ذخیره -->
 <div class="row">
 <div class="col-12" style="direction: ltr;">
    <div class="col-md-1 action-col">
        <button type="button"
            onclick="SaveChanelKaniInfo1('${BarAvordUserId}',${Type})"
            class="ButtonRowsSaveStyle">
            <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
        </button>
    </div><!--col-md-2 action-col -->
    </div><!--col-12 -->
    </div>

    </div><!-- بخش نمایش -->
</div>

  <div id="ViewChanelKaniEzafeBaha" style="direction: rtl;" class="col-12 khakbardari-view">
  </div>


    `;

    $('#divItemsAddedAndRel' + NoeFBId + "_" + ForItem).html(str);
    $('#divAddedItems' + OpId).show();

    $('#divItemsAddedAndRel' + NoeFBId + "_" + ForItem).off('keydown.ChanelKani').on('keydown.ChanelKani', '.khakbardariTextStyle', function (e) {
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

    ViewChanelKaniInfo(BarAvordUserId, Type);
}

function ViewChanelKaniInfo(BarAvordId, Type) {
    debugger;
    $('#ViewChanelKaniEzafeBaha').html('');

    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.BarAvordId = BarAvordId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    vardata.Type = Type;

    $.ajax({
        type: "POST",
        url: "/PayKaniInfoForBarAvords/GetDetailsOfKMPayKaniInfoWithKMPayKaniId",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;

            KMChanelKaniBarAvordDetailsMore = response.kmPayKaniBarAvordDetailsMore;
            KMChanelKaniBarAvordDetails = response.kmPayKaniBarAvordDetails;
            lstChanelKaniInfoRizMetre = response.lstPayKaniInfoRizMetre;
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
      <span>شرح عملیات کانال کنی</span>
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
            debugger;


            var totalKhDetail = 0;
            var totalDarsadKhDetail = 0;
            var totalVarizi = 0;
            var totalDarsadVarizi = 0;
            var totalReUseHajm = 0;
            var totalDarsadReUseHajm = 0;
            var totalHaml = 0;
            var totalDarsadHaml = 0;

            if (KMChanelKaniBarAvordDetailsMore.length > 0) {
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
            }
            else
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Add');


            $.each(KMChanelKaniBarAvordDetails, function () {

                debugger;

                strKMAK += `
        <div class="container-fluid" style="width:100%;margin:0;padding:0;">
        <div class="row" style="padding:2px 0px;margin:2px 0px;border-bottom:1px solid #ccc;">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
    <input id="txtChanelKaniItemId" value="${this.noeKhakBardariId}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanChanelKaniItems" class="spanStyleKhakBardariItems" style="font-size:12px;">
        ${this.title}
      </span>
    </div>`;

                let result = KMChanelKaniBarAvordDetailsMore.filter(x => x.payKaniInfoForBarAvordDetailsId === this.id);

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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled id="txtKhDetailChK" value="${KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled id="txtDarsadChK" value="${DarsadKhDetail}" />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVariziChK" value="${Varizi}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVariziChK" value="${DarsadVarizi}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajmChK" value="${ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsadChK" value="${DarsadReUseHajm}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHamlChK" value="${Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHamlChK" value="${DarsadHaml}"  />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
            });


            SumRizMetreChanelKani(Type);
            ShowChanelKaniEzafeBaha(Type);

            // توابع کمکی برای رُند کردن
            const fmt = (n) => {
                const num = Number(n);
                return Number.isInteger(num) ? num.toString() : parseFloat(num.toFixed(2)).toString();
            };

            //$('div[id^="MainViewChanelKani"]').slideUp();

            $('#ViewChanelKaniNew').html(strKMAK);
            $('#MainViewChanelKaniNew').slideDown();


            $('#ViewChanelKaniNew input[type="text"]').change(function () {
                debugger;

                let changedId = $(this).attr("id");
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

                //if (HajmChanelKani == 0 || HajmChanelKani == '' || !$.isNumeric(HajmChanelKani)) {
                //    toastr.info('حجم خاکبرداری وارد شده نامعتبر میباشد', 'اطلاع');
                //    $('#txtHajmChanelKani').addClass('blinking');
                //    return;
                //} else {
                //    $('#txtHajmChanelKani').removeClass('blinking');
                //}

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetailChK").val()) || 0;

                let varizi = parseFloat($("#txtVariziChK").val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajmChK").val()) || 0;
                let haml = parseFloat($("#txtHamlChK").val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVariziChK").val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsadChK").val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHamlChK").val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail")) {
                    let Zarb = khDetail / HajmChanelKani * 100;
                    $("#txtDarsadChK").val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmChanelKani) {
                        let NewVal = HajmChanelKani - (SumAll - khDetail);
                        $("#txtKhDetailChK").val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = khDetail / HajmChanelKani * 100;
                        $("#txtDarsadChK").val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVariziChK").val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajmChK").val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHamlChK").val(((dHaml / 100) * khDetail).toFixed(2));
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
                    $('#txtKhDetailChK').val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi")) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVariziChK").val(varizi.toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajmChK").val(reuseHajm.toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHamlChK").val(haml.toFixed(2));
                    }

                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        dVarizi = (varizi / khDetail) * 100;
                        $("#txtDarsadVariziChK").val(dVarizi.toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsadChK").val(dReuse.toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (haml / khDetail) * 100;
                        $("#txtDarsadHamlChK").val(dHaml.toFixed(2));
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVariziChK").val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajmChK").val()) || 0;
                haml = parseFloat($("#txtHamlChK").val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVariziChK").val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsadChK").val()) || 0;
                dHaml = parseFloat($("#txtDarsadHamlChK").val()) || 0;

                // کنترل مجموع درصدها
                let dSum = dVarizi + dReuse + dHaml;
                if (dSum > 100) {
                    let extra = dSum - 100;
                    if (changedId.includes("DarsadVarizi")) {
                        dVarizi -= extra;
                        $("#txtDarsadVariziChK").val(dVarizi.toFixed(2));
                        $("#txtVariziChK").val(((dVarizi / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("ReUseDarsad")) {
                        dReuse -= extra;
                        $("#txtReUseDarsadChK").val(dReuse.toFixed(2));
                        $("#txtReUseHajmChK").val(((dReuse / 100) * khDetail).toFixed(2));
                    } else if (changedId.includes("DarsadHaml")) {
                        dHaml -= extra;
                        $("#txtDarsadHamlChK").val(dHaml.toFixed(2));
                        $("#txtHamlChK").val(((dHaml / 100) * khDetail).toFixed(2));
                    }
                }

                // کنترل مجموع حجم‌ها
                let vSum = varizi + reuseHajm + haml;
                if (vSum > khDetail) {
                    let extra = vSum - khDetail;
                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        varizi -= extra;
                        $("#txtVariziChK").val(varizi.toFixed(2));
                        $("#txtDarsadVariziChK").val(((varizi / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("ReUseHajm")) {
                        reuseHajm -= extra;
                        $("#txtReUseHajmChK").val(reuseHajm.toFixed(2));
                        $("#txtReUseDarsadChK").val(((reuseHajm / khDetail) * 100).toFixed(2));
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        haml -= extra;
                        $("#txtHamlChK").val(haml.toFixed(2));
                        $("#txtDarsadHamlChK").val(((haml / khDetail) * 100).toFixed(2));
                    }
                }
            });


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

    $('#ViewChanelKani').html();
}

function SumAllDetailsChanelKani() {
    sumAll = 0;
    sumAll += parseFloat($.trim($('#txtKhDetailChK').val()) == '' ? '0' : $.trim($('#txtKhDetailChK').val()));

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

function SaveChanelKaniInfo1(BarAvordUserId, Type) {
    debugger;
    AddOrEdit = $('#HDFStateAmalyateKhakiSaveOrEdit').val();
    if (AddOrEdit == 'Edit') {
        UpdateChanelKaniInfo(BarAvordUserId, Type);
    }
    else {

        check = false;

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

        //check = SumAllDetailsChanelKani();

        check1 = CheckValuesOfChanelKaniDetails();


        if (!check) {

            let dataList = [];

            let obj = {
                DetailValue: $('#txtKhDetailChK').val(),
                DarsadValue: $('#txtDarsadChK').val(),
                DetailValueOfReCycle: $('#txtReUseHajmChK').val(),
                DarsadValueOfReCycle: $('#txtReUseDarsadChK').val(),
                DetailValueOfVarize: $('#txtVariziChK').val(),
                DarsadValueOfVarize: $('#txtDarsadVariziChK').val(),
                DetailValueOfHaml: $('#txtHamlChK').val(),
                DarsadValueOfHaml: $('#txtDarsadHamlChK').val(),
                NoeKhakBardari: $('#txtChanelKaniItemId').val(),
            };

            dataList.push(obj);

            NoeFB = parseInt($('#HDFNoeFB').val());
            Year = $('#HDFYear').val();

            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId;
            vardata.NoeFBId = NoeFB;
            vardata.Type = Type;//پی کنی
            vardata.HKB = SumAllDetailsChanelKani;
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

                        ShowChanelKaniEzafeBaha();

                        toastr.success('اطلاعات بدرستی ثبت گردید', 'ثبت');
                    }
                    else
                        toastr.error('مشکل در ثبت اطلاعات', 'خطا');
                },
                error: function (response) {
                    toastr.error('مشکل در ثبت اطلاعات', 'خطا');
                }
            });
        }
        else
            toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');
    }
}

function UpdateChanelKaniInfo(BarAvordUserId, Type) {

    check = false;
    //////////
    HKB = SumAllDetailsChanelKani;// $('#txtHajmChanelKani' + KMNum).val();
    //if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
    //    $('#txtHajmChanelKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtHajmChanelKani' + KMNum).removeClass('blinking');
    //}
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

    ///////////////
    //$('#divChanelKaniInfoDetails input[type="text"]').each(function () {
    //    ////////////
    //    if (!$.isNumeric($(this).val())) {
    //        $(this).addClass('blinking');
    //        check = true;
    //    }
    //    else
    //        $(this).removeClass('blinking');
    //});

    //if (SumAllDetailsForEdit(KMNum)) check = true;

    //if (CheckValuesOfChanelKaniDetailsForEdit(KMNum)) {
    //    check = true;
    //}


    $('#divChanelKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    check1 = CheckValuesOfChanelKaniDetails();

    if (!check) {

        let dataList = [];

        var DarsadValue = $('#txtDarsadChK').val().trim();
        if (DarsadValue != "0") {
            let obj = {
                DetailValue: $('#txtKhDetailChK').val(),
                DarsadValue: DarsadValue,
                DetailValueOfReCycle: $('#txtReUseHajmChK').val(),
                DarsadValueOfReCycle: $('#txtReUseDarsadChK').val(),
                DetailValueOfVarize: $('#txtVariziChK').val(),
                DarsadValueOfVarize: $('#txtDarsadVariziChK').val(),
                DetailValueOfHaml: $('#txtHamlChK').val(),
                DarsadValueOfHaml: $('#txtDarsadHamlChK').val(),
                NoeKhakBardari: $('#txtChanelKaniItemId').val(),
            };
            dataList.push(obj);
        }

        Year = $('#HDFYear').val();
        NoeFB = parseInt($('#HDFNoeFB').val());

        var vardata = new Object();
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.HKB = HKB;
        vardata.lstItems = dataList;
        vardata.Year = Year;
        vardata.NoeFBId = NoeFB;
        vardata.Type = Type;

        debugger;

        $.ajax({
            type: "POST",
            url: "/PayKaniInfoForBarAvords/UpdatePayKaniInfoForBarAvord",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                info = response.split('_');
                if (info[0] == "OK") {
                    toastr.success('اطلاعات کانال کنی بدرستی ویرایش گردید', 'ثبت');

                    $('#ViewChanelKaniEzafeBaha').find("div[id^='ViewChanelKaniBEzafeBahaRizMetre']").slideUp(500);

                    //$('#divExistKMHeader').click();
                    //$('#ViewKhakBardariEzafeBaha' + KMNum).html('');

                }
                else
                    toastr.error('مشکل در ویرایش اطلاعات کانال کنی', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در ویرایش اطلاعات کانال کنی', 'خطا');
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');

}


function CheckValuesOfChanelKaniDetails() {
    checkValues = false;
    KhDetail = parseFloat($('#txtKhDetailChK').val());

    ReUseHajm = parseFloat($('#txtReUseHajmChK').val());
    if (ReUseHajm > KhDetail) {
        $('#txtReUseHajmChK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseHajmChK').removeClass('blinking');

    ReUseDarsad = parseFloat($('#txtReUseDarsadChK').val());
    if (ReUseDarsad > 100) {
        $('#txtReUseDarsadChK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseDarsadChK').removeClass('blinking');
    /////////////
    Varizi = parseFloat($('#txtVariziChK').val());
    Haml = parseFloat($('#txtHamlChK').val());
    if (Varizi > KhDetail) {
        $('#txtVariziChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtVariziChK').removeClass('blinking');
    }

    if (Haml > KhDetail) {
        $('#txtHamlChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtHamlChK').removeClass('blinking');
    }

    DarsadVarizi = parseFloat($('#txtDarsadVariziChK').val());
    DarsadHaml = parseFloat($('#txtDarsadHamlChK').val());
    if (DarsadVarizi > 100) {
        $('#txtDarsadVariziChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadVariziChK').removeClass('blinking');
    }

    if (DarsadHaml > 100) {
        $('#txtDarsadHamlChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadHamlChK').removeClass('blinking');
    }

    return checkValues;
}

function CheckValuesOfChanelKaniDetailsForEdit() {
    checkValues = false;
    KhDetail = parseFloat($('#txtKhDetailChK').val());

    ReUseHajm = parseFloat($('#txtReUseHajmChK').val());
    if (ReUseHajm > KhDetail) {
        $('#txtReUseHajmChK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseHajmChK').removeClass('blinking');

    ReUseDarsad = parseFloat($('#txtReUseDarsadChK').val());
    if (ReUseDarsad > 100) {
        $('#txtReUseDarsadChK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseDarsadChK').removeClass('blinking');
    /////////////
    Varizi = parseFloat($('#txtVariziChK').val());
    Haml = parseFloat($('#txtHamlChK').val());
    if (Varizi > KhDetail) {
        $('#txtVariziChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtVariziChK').removeClass('blinking');
    }

    if (Haml > KhDetail) {
        $('#txtHamlChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtHamlChK').removeClass('blinking');
    }

    DarsadVarizi = parseFloat($('#txtDarsadVariziChK').val());
    DarsadHaml = parseFloat($('#txtDarsadHamlChK').val());
    if (DarsadVarizi > 100) {
        $('#txtDarsadVariziChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadVariziChK').removeClass('blinking');
    }

    if (DarsadHaml > 100) {
        $('#txtDarsadHamlChK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadHamlChK').removeClass('blinking');
    }
    return checkValues;
}

function ShowRizMetreChanelKani(lstChanelKaniInfoRizMetre, lstItemFBShomarehForGet) {

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
                    str += "<div class='col-md-1' style=\"text-align:center;color:#000\"><span class=\"RMRowStyle\">" + row.shomareh + "</span></div>";

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

                    str += "<div class='col-md-1'><span class='RMMJozStyle'>" + MeghdarJoz + "</span></div>";

                    str += "<div class='col-md-2'><input  type='text' title='" + row.des + "' class='form-control spanStyleMitraSmall input-sm' id='txtDes" + id + "' value='" + row.des + "'/></div>";
                    str += "<div class='col-md-1' style='text-align:center;'>";

                    if (HasEditButton) 
                        //str += "<i id='iUpdate" + id +"' class='fa fa-save SaveRMUStyle' onclick =\"UpdateRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i>";
                        str += "<button type='button' id='iUpdate" + id + "' onclick=\"UpdateRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>";

                    if (HasDelButton) 
                        //str += "<i class='fa fa-trash DelRMUStyle' onclick =\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i>";
                        str += "<button type='button' id='iDelete" + id + "' onclick=\"DeleteRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\" class=\"ButtonRowsSaveStyle\"><i id=\"iDelete\" class=\"fa fa-trash DelRMUStyle\"></i></button>";


                    str += "</div>";
                    str += "</div>";
                });
                str += "</div>";

            }
        });

        $targetDivRizMetreChanelKani = $('#ViewRizMetreChanelKani');

        $targetDivRizMetreChanelKani.html(str);
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




