ActivityLength = 0;
var SumAllDetailsPayKani = 0;

function SumRizMetrePK(Type) {
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
                SumAllDetailsPayKani = dSum;
                $('#txtKhDetailPK').val(dSum);
                $('#txtDarsadPK').val(100);

                DarsadVariziPK = parseFloat($('#txtDarsadVariziPK').val());
                VariziPK = isNaN(DarsadVariziPK) ? 0 : DarsadVariziPK * dSum / 100;
                $('#txtVariziPK').val(VariziPK.toFixed(2));

                ReUseDarsadPK = parseFloat($('#txtReUseDarsadPK').val());
                ReUseHajmPK = isNaN(ReUseDarsadPK) ? 0 : ReUseDarsadPK * dSum / 100;
                $('#txtReUseHajmPK').val(ReUseHajmPK.toFixed(2));

                DarsadHamlPK = parseFloat($('#txtDarsadHamlPK').val());
                ReUseHajmPK = isNaN(DarsadHamlPK) ? 0 : DarsadHamlPK * dSum / 100;
                $('#txtHamlPK').val(ReUseHajmPK.toFixed(2));

                SavePayKaniInfo1(BarAvordUserId, Type);
            }
            else
                toastr.error('مشکل در جمع ریزمتره', 'خطا');
        },
        error: function (response) {
            toastr.error('مشکل در جمع ریزمتره', 'خطا');
        }
    });
}

function formatNumber(num) {
    if (num == null) return '';
    if (num == 0) return '';
    return Number(num).toLocaleString('fa-IR'); // یا 'en-US' برای کاما
}

function PayKaniClick(OpId, Type) {

    debugger;
    $('#HDFOperationId').val(OpId);
    ForItem = $('#HDFItemsFBShomareh').val();

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    str = "";

    str += `


  <!-- بخش نمایش -->
  <div id="MainViewPayKaniNew" class="khakbardari-view">
  <div id="ViewPayKaniNew" class="khakbardari-view">
  </div>

  <!-- دکمه ذخیره -->
<div class="row">
 <div class="col-12" style="direction: ltr;">
    <div class="col-md-1 action-col">
     <button type="button" id="btnSavePayKani"
            onclick="SavePayKaniInfo1('${BarAvordUserId}',${Type})"
            class="ButtonRowsSaveStyle">
            <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
        </button>
    </div><!--col-md-2 action-col -->
 </div><!--col-12 -->
</div><!--row -->

    </div><!-- بخش نمایش -->
</div>

  <div id="ViewPayKaniEzafeBaha" style="direction: rtl;" class="col-12 khakbardari-view">
  </div>


    `;

    NoeFBId = parseInt($('#HDFNoeFB').val());


    $('#divItemsAddedAndRel' + NoeFBId + "_" + ForItem).html(str);
    $('#divAddedItems' + OpId).show();

    $('#divItemsAddedAndRel' + NoeFBId + "_" + ForItem).off('keydown.paykani').on('keydown.paykani', '.khakbardariTextStyle', function (e) {
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

    ViewPayKaniInfo(BarAvordUserId, Type);

    //ShowSelctionPayKani(1, 0, BarAvordUserId, 0, 0, 0, 0, 0);
}

function ViewPayKaniInfo(BarAvordId, Type) {


    $('#ViewPayKaniEzafeBaha').html('');

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

            if (KMPayKaniBarAvordDetailsMore.length > 0) {
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Edit');
            }
            else
                $('#HDFStateAmalyateKhakiSaveOrEdit').val('Add');


            $.each(KMPayKaniBarAvordDetails, function () {

                debugger;

                strKMAK += `
        <div class="container-fluid" style="width:100%;margin:0;padding:0;">
        <div class="row" style="padding:2px 0px;margin:2px 0px;border-bottom:1px solid #ccc;">

    <!-- عنوان فعالیت (سمت راست یا چپ) -->
    <div class="col-1" style="display:none">
    <input id="txtPayKaniItemId" value="${this.noeKhakBardariId}"/>
    </span>
    </div>
    <div class="col-4" style="padding:0;text-align:right;z-index:555;">
      <span id="spanPayKaniItems" class="spanStyleKhakBardariItems" style="font-size:12px;">
        ${this.title}
      </span>
    </div>`;

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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled id="txtKhDetailPK" value="${KhDetail}" />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" disabled id="txtDarsadPK" value="${DarsadKhDetail}" />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtVariziPK" value="${Varizi}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadVariziPK" value="${DarsadVarizi}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseHajmPK" value="${ReUseHajm}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtReUseDarsadPK" value="${DarsadReUseHajm}"  />
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
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtHamlPK" value="${Haml}"  />
            </div>
            <div class="col-4" style="padding:0 2px;">
              <input type="text" class="form-control_1 input-sm khakbardariTextStyle" id="txtDarsadHamlPK" value="${DarsadHaml}"  />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
            });


            SumRizMetrePK(Type);
            ShowPayKaniEzafeBaha(Type);

            // توابع کمکی برای رُند کردن
            const fmt = (n) => {
                const num = Number(n);
                return Number.isInteger(num) ? num.toString() : parseFloat(num.toFixed(2)).toString();
            };

            //$('div[id^="MainViewPayKani"]').slideUp();

            $('#ViewPayKaniNew').html(strKMAK);
            $('#MainViewPayKaniNew').slideDown();


            $('#ViewPayKaniNew input[type="text"]').change(function () {
                debugger;

                let changedId = $(this).attr("id");
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

                // مقادیر اصلی ردیف
                let khDetail = parseFloat($("#txtKhDetailPK").val()) || 0;

                let varizi = parseFloat($("#txtVariziPK").val()) || 0;
                let reuseHajm = parseFloat($("#txtReUseHajmPK").val()) || 0;
                let haml = parseFloat($("#txtHamlPK").val()) || 0;

                let dVarizi = parseFloat($("#txtDarsadVariziPK").val()) || 0;
                let dReuse = parseFloat($("#txtReUseDarsadPK").val()) || 0;
                let dHaml = parseFloat($("#txtDarsadHamlPK").val()) || 0;

                // 🟢 بخش ۱: کنترل حجم کل و درصد
                if (changedId.includes("KhDetail")) {
                    let Zarb = khDetail / HajmPayKani * 100;
                    $("#txtDarsadPK").val(Zarb.toFixed(2));

                    let SumAll = ReturnSumAllDetails();
                    if (SumAll > HajmPayKani) {
                        let NewVal = HajmPayKani - (SumAll - khDetail);
                        $("#txtKhDetailPK").val(NewVal.toFixed(2));
                        khDetail = NewVal; // ✅ مقدار جدید رو دوباره ست کن

                        let Zarb = khDetail / HajmPayKani * 100;
                        $("#txtDarsadPK").val(Zarb.toFixed(2));
                    }

                    // ✅ حالا با مقدار اصلاح‌شده محاسبه کن
                    $("#txtVariziPK").val(((dVarizi / 100) * khDetail).toFixed(2));
                    $("#txtReUseHajmPK").val(((dReuse / 100) * khDetail).toFixed(2));
                    $("#txtHamlPK").val(((dHaml / 100) * khDetail).toFixed(2));
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
                    $('#txtKhDetailPK').val(newKhDetail.toFixed(2));
                }



                // 🟢 بخش ۲: کنترل ریز جزئیات (وریزی، حمل، ReUse)
                if (khDetail > 0) {
                    if (changedId.includes("DarsadVarizi")) {
                        varizi = (dVarizi / 100) * khDetail;
                        $("#txtVariziPK").val(varizi.toFixed(2));
                        $("#txtVariziPK").removeClass('blinking');
                    } else if (changedId.includes("ReUseDarsad")) {
                        reuseHajm = (dReuse / 100) * khDetail;
                        $("#txtReUseHajmPK").val(reuseHajm.toFixed(2));
                        $("#txtReUseHajmPK").removeClass('blinking');
                    } else if (changedId.includes("DarsadHaml")) {
                        haml = (dHaml / 100) * khDetail;
                        $("#txtHamlPK").val(haml.toFixed(2));
                        $("#txtHamlPK").removeClass('blinking');
                    }

                    if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                        dVarizi = (varizi / khDetail) * 100;
                        $("#txtDarsadVariziPK").val(dVarizi.toFixed(2));
                        $("#txtDarsadVariziPK").removeClass('blinking');
                    } else if (changedId.includes("ReUseHajm")) {
                        dReuse = (reuseHajm / khDetail) * 100;
                        $("#txtReUseDarsadPK").val(dReuse.toFixed(2));
                        $("#txtReUseDarsadPK").removeClass('blinking');
                    } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                        dHaml = (haml / khDetail) * 100;
                        $("#txtDarsadHamlPK").val(dHaml.toFixed(2));
                        $("#txtDarsadHamlPK").removeClass('blinking');
                    }
                }

                // 🔁 دوباره گرفتن مقادیر بعد از تغییر
                varizi = parseFloat($("#txtVariziPK").val()) || 0;
                reuseHajm = parseFloat($("#txtReUseHajmPK").val()) || 0;
                haml = parseFloat($("#txtHamlPK").val()) || 0;

                dVarizi = parseFloat($("#txtDarsadVariziPK").val()) || 0;
                dReuse = parseFloat($("#txtReUseDarsadPK").val()) || 0;
                dHaml = parseFloat($("#txtDarsadHamlPK").val()) || 0;


                debugger;
                ////////////////////////
                // کنترل مجموع درصدها///
                ////////////////////////
                // let dSum = dVarizi + dReuse + dHaml;
                // if (dSum > 100) {
                //     let extra = dSum - 100;
                //     if (changedId.includes("DarsadVarizi")) {
                //         dVarizi -= extra;
                //         $("#txtDarsadVariziPK").val(dVarizi.toFixed(2));
                //         $("#txtVariziPK").val(((dVarizi / 100) * khDetail).toFixed(2));
                //         $("#txtVariziPK").removeClass('blinking');
                //     } else if (changedId.includes("ReUseDarsad")) {
                //         dReuse -= extra;
                //         $("#txtReUseDarsadPK").val(dReuse.toFixed(2));
                //         $("#txtReUseHajmPK").val(((dReuse / 100) * khDetail).toFixed(2));
                //         $("#txtReUseHajmPK").removeClass('blinking');
                //     } else if (changedId.includes("DarsadHaml")) {
                //         dHaml -= extra;
                //         $("#txtDarsadHamlPK").val(dHaml.toFixed(2));
                //         $("#txtHamlPK").val(((dHaml / 100) * khDetail).toFixed(2));
                //         $("#txtHamlPK").removeClass('blinking');
                //     }
                // }
                ///////////////////////
                // کنترل مجموع حجم‌ها///
                ///////////////////////
                // let vSum = varizi + reuseHajm + haml;
                // if (vSum > khDetail) {
                //     let extra = vSum - khDetail;
                //     if (changedId.includes("Varizi") && !changedId.includes("Darsad")) {
                //         varizi -= extra;
                //         $("#txtVariziPK").val(varizi.toFixed(2));
                //         $("#txtDarsadVariziPK").val(((varizi / khDetail) * 100).toFixed(2));
                //         $("#txtDarsadVariziPK").removeClass('blinking');
                //     } else if (changedId.includes("ReUseHajm")) {
                //         reuseHajm -= extra;
                //         $("#txtReUseHajmPK").val(reuseHajm.toFixed(2));
                //         $("#txtReUseDarsadPK").val(((reuseHajm / khDetail) * 100).toFixed(2));
                //         $("#txtReUseDarsadPK").removeClass('blinking');
                //     } else if (changedId.includes("Haml") && !changedId.includes("Darsad")) {
                //         haml -= extra;
                //         $("#txtHamlPK").val(haml.toFixed(2));
                //         $("#txtDarsadHamlPK").val(((haml / khDetail) * 100).toFixed(2));
                //         $("#txtDarsadHamlPK").removeClass('blinking');
                //     }
                // }
                ////////////////////////////////
                //محاسبه سومین فیلد خالی حجم///
                ///////////////////////////////
                // if (varizi != 0 && reuseHajm != 0 && haml == 0) {
                //     let haml = khDetail - (reuseHajm + varizi);
                //     $("#txtHamlPK").val(haml.toFixed(2));
                //     $("#txtHamlPK").removeClass('blinking');
                //     $("#txtDarsadHamlPK").val(((haml / khDetail) * 100).toFixed(2));
                //     $("#txtDarsadHamlPK").removeClass('blinking');
                // }
                // if (varizi == 0 && reuseHajm != 0 && haml != 0) {
                //     let varizi = khDetail - (haml + reuseHajm);
                //     $("#txtVariziPK").val(varizi.toFixed(2));
                //     $("#txtVariziPK").removeClass('blinking');
                //     $("#txtDarsadVariziPK").val(((varizi / khDetail) * 100).toFixed(2));
                //     $("#txtDarsadVariziPK").removeClass('blinking');
                // }
                // if (varizi != 0 && reuseHajm == 0 && haml != 0) {
                //     let reuseHajm = khDetail - (varizi + haml);
                //     $("#txtReUseHajmPK").val(reuseHajm.toFixed(2));
                //     $("#txtReUseHajmPK").removeClass('blinking');
                //     $("#txtReUseDarsadPK").val(((reuseHajm / khDetail) * 100).toFixed(2));
                //     $("#txtReUseDarsadPK").removeClass('blinking');
                // }
                //////////////////////////////////
                // محاسبه سومین فیلد خالی درصد///
                /////////////////////////////////
                // if (dVarizi != 0 && dReuseHajm != 0 && dHaml == 0) {
                //     let dHaml = 100 - (dReuseHajm + dVarizi);
                //     $("#txtDarsadHamlPK").val(dHaml.toFixed(2));
                //     $("#txtDarsadHamlPK").removeClass('blinking');
                //     $("#txtHamlPK").val(((dHaml / 100) * khDetail).toFixed(2));
                //     $("#txtHamlPK").removeClass('blinking');
                // }
                // if (dVarizi == 0 && dReuseHajm != 0 && dHaml != 0) {
                //     let dVarizi = 100 - (dHaml + dReuseHajm);
                //     $("#txtDarsadVariziPK").val(dVarizi.toFixed(2));
                //     $("#txtDarsadVariziPK").removeClass('blinking');
                //     $("#txtVariziPK").val(((dVarizi / 100) * khDetail).toFixed(2));
                //     $("#txtVariziPK").removeClass('blinking');
                // }
                // if (dVarizi != 0 && dReuseHajm == 0 && dHaml != 0) {
                //     let dReuseHajm = 100 - (dVarizi + dHaml);
                //     $("#txtReUseDarsadPK").val(dReuseHajm.toFixed(2));
                //     $("#txtReUseDarsadPK").removeClass('blinking');
                //     $("#txtReUseHajmPK").val(((dReuseHajm / 100) * khDetail).toFixed(2));
                //     $("#txtReUseHajmPK").removeClass('blinking');
                // }






                ///////////////////////////////
// کنترل و تنظیم سه فیلد اصلی
///////////////////////////////

// let khDetail = parseFloat($("#txtKhDetailPK").val()) || 0;

// let varizi = parseFloat($("#txtVariziPK").val()) || 0;
// let reuseHajm = parseFloat($("#txtReUseHajmPK").val()) || 0;
// let haml = parseFloat($("#txtHamlPK").val()) || 0;

// let dVarizi = parseFloat($("#txtDarsadVariziPK").val()) || 0;
// let dReuse = parseFloat($("#txtReUseDarsadPK").val()) || 0;
// let dHaml = parseFloat($("#txtDarsadHamlPK").val()) || 0;

if (khDetail > 0) {

    // اگر واریزه یا مصرف در خاکریزی تغییر کرده باشد
    // حمل به عنوان فیلد باقیمانده محاسبه می‌شود

    if (
        changedId.includes("Varizi") ||
        changedId.includes("ReUse")
    ) {

        if (changedId.includes("Darsad")) {

            // تغییر درصد واریزه
            if (changedId.includes("Varizi")) {
                dVarizi = Math.max(0, Math.min(100, dVarizi));
                varizi = khDetail * dVarizi / 100;
                $("#txtVariziPK").val(varizi.toFixed(2));
            }

            // تغییر درصد مصرف در خاکریزی
            else {
                dReuse = Math.max(0, Math.min(100, dReuse));
                reuseHajm = khDetail * dReuse / 100;
                $("#txtReUseHajmPK").val(reuseHajm.toFixed(2));
            }
        }
        else {

            // تغییر مستقیم حجم واریزه
            if (changedId.includes("Varizi")) {
                varizi = Math.max(0, Math.min(khDetail, varizi));
                dVarizi = varizi / khDetail * 100;
                $("#txtDarsadVariziPK").val(dVarizi.toFixed(2));
            }

            // تغییر مستقیم حجم مصرف در خاکریزی
            else {
                reuseHajm = Math.max(0, Math.min(khDetail, reuseHajm));
                dReuse = reuseHajm / khDetail * 100;
                $("#txtReUseDarsadPK").val(dReuse.toFixed(2));
            }
        }

        // جلوگیری از بیشتر شدن مجموع دو فیلد از حجم کل
        if (varizi + reuseHajm > khDetail) {

            if (changedId.includes("Varizi")) {
                varizi = khDetail - reuseHajm;
                $("#txtVariziPK").val(varizi.toFixed(2));
                dVarizi = varizi / khDetail * 100;
                $("#txtDarsadVariziPK").val(dVarizi.toFixed(2));
            }
            else {
                reuseHajm = khDetail - varizi;
                $("#txtReUseHajmPK").val(reuseHajm.toFixed(2));
                dReuse = reuseHajm / khDetail * 100;
                $("#txtReUseDarsadPK").val(dReuse.toFixed(2));
            }
        }

        // محاسبه حمل از مقدار باقیمانده
        haml = Math.max(0, khDetail - varizi - reuseHajm);

        dHaml = haml / khDetail * 100;

        $("#txtHamlPK").val(haml.toFixed(2));
        $("#txtDarsadHamlPK").val(dHaml.toFixed(2));
    }

    // اگر خود حمل تغییر کرده باشد
    else if (
        changedId.includes("Haml")
    ) {

        if (changedId.includes("Darsad")) {

            dHaml = Math.max(0, Math.min(100, dHaml));
            haml = khDetail * dHaml / 100;

        }
        else {

            haml = Math.max(0, Math.min(khDetail, haml));
            dHaml = haml / khDetail * 100;
        }

        // سهم باقیمانده بین واریزه و مصرف در خاکریزی
        let remaining = khDetail - haml;

        if (varizi + reuseHajm > remaining) {

            let oldSum = varizi + reuseHajm;

            varizi = oldSum > 0
                ? remaining * varizi / oldSum
                : 0;

            reuseHajm = remaining - varizi;

            dVarizi = varizi / khDetail * 100;
            dReuse = reuseHajm / khDetail * 100;

            $("#txtVariziPK").val(varizi.toFixed(2));
            $("#txtReUseHajmPK").val(reuseHajm.toFixed(2));

            $("#txtDarsadVariziPK").val(dVarizi.toFixed(2));
            $("#txtReUseDarsadPK").val(dReuse.toFixed(2));
        }

        $("#txtHamlPK").val(haml.toFixed(2));
        $("#txtDarsadHamlPK").val(dHaml.toFixed(2));
    }
}


    
            });

// انتخاب کل مقدار با کلیک روی فیلد
$('#ViewPayKaniNew input[type="text"]').on('focus click', function () {
    $(this).select();
});

            
$('#ViewPayKaniNew input[type="text"]').keydown(function (e) {

    if (e.key === "Enter") {
        e.preventDefault();

        let inputs = $('#ViewPayKaniNew input[type="text"]:visible:not(:disabled)');
        let currentIndex = inputs.index(this);

        if (currentIndex === inputs.length - 1) {
            $('#btnSavePayKani').focus();
        }
        else {
            inputs.eq(currentIndex + 1).focus().select();
        }
    }

});

$('#ViewPayKaniNew input[type="text"]').on('input change', function () {

    let value = $.trim($(this).val());

    if (value !== '' && $.isNumeric(value) && parseFloat(value) < 0) {
        $(this).val('0').trigger('change');
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

    $('#ViewPayKani').html();
}

function SumAllDetailsPayKani() {
    sumAll = 0;
    sumAll += parseFloat($.trim($('#txtKhDetailPK').val()) == '' ? '0' : $.trim($('#txtKhDetailPK').val()));

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

function SavePayKaniInfo1(BarAvordUserId, Type) {
    AddOrEdit = $('#HDFStateAmalyateKhakiSaveOrEdit').val();
    if (AddOrEdit == 'Edit') {
        UpdatePayKaniInfo(BarAvordUserId, Type);
    }
    else {

        check = false;

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

        //check = SumAllDetailsPayKani();

        check1 = CheckValuesOfPayKaniDetails();


        if (!check) {

            let dataList = [];

            let obj = {
                DetailValue: $('#txtKhDetailPK').val(),
                DarsadValue: $('#txtDarsadPK').val(),
                DetailValueOfReCycle: $('#txtReUseHajmPK').val(),
                DarsadValueOfReCycle: $('#txtReUseDarsadPK').val(),
                DetailValueOfVarize: $('#txtVariziPK').val(),
                DarsadValueOfVarize: $('#txtDarsadVariziPK').val(),
                DetailValueOfHaml: $('#txtHamlPK').val(),
                DarsadValueOfHaml: $('#txtDarsadHamlPK').val(),
                NoeKhakBardari: $('#txtPayKaniItemId').val(),
            };

            dataList.push(obj);

            NoeFB = parseInt($('#HDFNoeFB').val());
            Year = $('#HDFYear').val();

            var vardata = new Object();
            vardata.BarAvordUserId = BarAvordUserId;
            vardata.NoeFBId = NoeFB;
            vardata.Type = Type;//پی کنی
            vardata.HKB = SumAllDetailsPayKani;
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
                        //$('#HDFKMAmalyateKhakiNum').val(info[2]);

                        //$('#txtKhDetailPK').val(0);
                        //$('#txtDarsadPK').val(0);
                        //$('#txtReUseHajmPK').val(0);
                        //$('#txtReUseDarsadPK').val(0);
                        //$('#txtVariziPK').val(0);
                        //$('#txtDarsadVariziPK').val(0);
                        //$('#txtHamlPK').val(0);
                        //$('#txtDarsadHamlPK').val(0);
                        //$('#txtPayKaniItemId').val(0);

                        ShowPayKaniEzafeBaha(Type);
                        //ShowExistingKMPayKani(BarAvordUserId);

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

//function ShowExistingKMPayKani(BarAvordUserId) {
//    debugger;
//    var vardata = new Object();
//    vardata.BaravordId = BarAvordUserId;
//    vardata.Type = 2;//پی کنی
//    $.ajax({
//        type: "POST",
//        url: "/PayKaniInfoForBarAvords/GetExistingKMPayKaniInfoWithBarAvordId",
//        data: JSON.stringify(vardata),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            debugger;

//            var KMAmalyateKhakiBarAvord = response;
//            strSEKB = '';
//            $.each(KMAmalyateKhakiBarAvord, function () {
//                debugger;
//                KMExistingId = this.id;
//                FromKM = this.fromKM;
//                ToKM = this.toKM;
//                //FromKMSplit = this.fromKMSplit;
//                //ToKMSplit = this.toKMSplit;
//                Value = this.value;
//                KMNum = this.kmNum;
//                Type = this.type;

//                strSEKB += `
//  <div class="row col-12" style="direction:ltr;" id="MainViewPayKani${KMNum}" class="khakbardari-view">
//    <div id="ViewPayKani${KMNum}" class="khakbardari-view" style="direction: rtl;">
//    </div>
//    <div class="col-md-2 action-col">
//      <a class="btn buttonStyleBoard" style="color:#fff" onclick="UpdatePayKaniInfo('${KMExistingId}'` + ',' + `'${BarAvordUserId}'` + ',' + `${KMNum})" onclick="event.stopPropagation();">
//        ذخیره
//      </a>
//    </div>
//  <div id="ViewRizMetrePayKani${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
//  <div id="ViewPayKaniEzafeBaha${KMNum}" style="direction: rtl;" class="col-12 khakbardari-view"></div>
//  </div><!-- MainViewPayKani -->
//  </div>
//    `;
//            });
//            debugger;


//            $('#divPayKaniExistingKM').html(strSEKB);
//            //$('#divPayKaniExistingKM').find('#MainViewPayKani' + KMNum).hide();

//        },
//        error: function (response) {
//            toastr.error('مشکل در بارگزاری کیلومتراژهای موجود', 'خطا');
//        }
//    });
//}


function UpdatePayKaniInfo(BarAvordUserId, Type) {

    check = false;
    //////////
    HKB = SumAllDetailsPayKani;// $('#txtHajmPayKani' + KMNum).val();
    //if (!$.isNumeric(HKB) || HKB == 0 || HKB == '') {
    //    $('#txtHajmPayKani' + KMNum).addClass('blinking');
    //    check = true;
    //}
    //else {
    //    $('#txtHajmPayKani' + KMNum).removeClass('blinking');
    //}
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

    ///////////////
    //$('#divPayKaniInfoDetails input[type="text"]').each(function () {
    //    ////////////
    //    if (!$.isNumeric($(this).val())) {
    //        $(this).addClass('blinking');
    //        check = true;
    //    }
    //    else
    //        $(this).removeClass('blinking');
    //});

    //if (SumAllDetailsForEdit(KMNum)) check = true;

    //if (CheckValuesOfPayKaniDetailsForEdit(KMNum)) {
    //    check = true;
    //}


    $('#divPayKaniInfoDetails input[type="text"]').each(function () {
        ////////////
        if (!$.isNumeric($(this).val())) {
            $(this).addClass('blinking');
            check = true;
        }
        else
            $(this).removeClass('blinking');
    });

    check1 = CheckValuesOfPayKaniDetails();

    if (!check) {

        let dataList = [];

        var DarsadValue = $('#txtDarsadPK').val().trim();
        if (DarsadValue != "0") {
            let obj = {
                DetailValue: $('#txtKhDetailPK').val(),
                DarsadValue: DarsadValue,
                DetailValueOfReCycle: $('#txtReUseHajmPK').val(),
                DarsadValueOfReCycle: $('#txtReUseDarsadPK').val(),
                DetailValueOfVarize: $('#txtVariziPK').val(),
                DarsadValueOfVarize: $('#txtDarsadVariziPK').val(),
                DetailValueOfHaml: $('#txtHamlPK').val(),
                DarsadValueOfHaml: $('#txtDarsadHamlPK').val(),
                NoeKhakBardari: $('#txtPayKaniItemId').val(),
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
                    toastr.success('اطلاعات پی کنی بدرستی ویرایش گردید', 'ثبت');

                    $('#ViewPayKaniEzafeBaha').find("div[id^='ViewPayKaniBEzafeBahaRizMetre']").slideUp(500);

                    //ShowPayKaniEzafeBaha();
                    //$('#divExistKMHeader').click();
                    //$('#ViewKhakBardariEzafeBaha' + KMNum).html('');

                }
                else
                    toastr.error('مشکل در ویرایش اطلاعات پی کنی', 'خطا');
            },
            error: function (response) {
                toastr.error('مشکل در ویرایش اطلاعات پی کنی', 'خطا');
            }
        });
    }
    else
        toastr.info('موارد مشخص شده دارای مقادیر نامعتبر میباشند', 'اطلاع');

}


function CheckValuesOfPayKaniDetails() {
    checkValues = false;
    KhDetail = parseFloat($('#txtKhDetailPK').val());

    ReUseHajm = parseFloat($('#txtReUseHajmPK').val());
    if (ReUseHajm > KhDetail) {
        $('#txtReUseHajmPK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseHajmPK').removeClass('blinking');

    ReUseDarsad = parseFloat($('#txtReUseDarsadPK').val());
    if (ReUseDarsad > 100) {
        $('#txtReUseDarsadPK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseDarsadPK').removeClass('blinking');
    /////////////
    Varizi = parseFloat($('#txtVariziPK').val());
    Haml = parseFloat($('#txtHamlPK').val());
    if (Varizi > KhDetail) {
        $('#txtVariziPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtVariziPK').removeClass('blinking');
    }

    if (Haml > KhDetail) {
        $('#txtHamlPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtHamlPK').removeClass('blinking');
    }

    DarsadVarizi = parseFloat($('#txtDarsadVariziPK').val());
    DarsadHaml = parseFloat($('#txtDarsadHamlPK').val());
    if (DarsadVarizi > 100) {
        $('#txtDarsadVariziPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadVariziPK').removeClass('blinking');
    }

    if (DarsadHaml > 100) {
        $('#txtDarsadHamlPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadHamlPK').removeClass('blinking');
    }

    return checkValues;
}

function CheckValuesOfPayKaniDetailsForEdit() {
    checkValues = false;
    KhDetail = parseFloat($('#txtKhDetailPK').val());

    ReUseHajm = parseFloat($('#txtReUseHajmPK').val());
    if (ReUseHajm > KhDetail) {
        $('#txtReUseHajmPK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseHajmPK').removeClass('blinking');

    ReUseDarsad = parseFloat($('#txtReUseDarsadPK').val());
    if (ReUseDarsad > 100) {
        $('#txtReUseDarsadPK').addClass('blinking');
        checkValues = true;
    }
    else
        $('#txtReUseDarsadPK').removeClass('blinking');
    /////////////
    Varizi = parseFloat($('#txtVariziPK').val());
    Haml = parseFloat($('#txtHamlPK').val());
    if (Varizi > KhDetail) {
        $('#txtVariziPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtVariziPK').removeClass('blinking');
    }

    if (Haml > KhDetail) {
        $('#txtHamlPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtHamlPK').removeClass('blinking');
    }

    DarsadVarizi = parseFloat($('#txtDarsadVariziPK').val());
    DarsadHaml = parseFloat($('#txtDarsadHamlPK').val());
    if (DarsadVarizi > 100) {
        $('#txtDarsadVariziPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadVariziPK').removeClass('blinking');
    }

    if (DarsadHaml > 100) {
        $('#txtDarsadHamlPK').addClass('blinking');
        checkValues = true;
    }
    else {
        $('#txtDarsadHamlPK').removeClass('blinking');
    }
    return checkValues;
}

function ShowRizMetrePayKani(lstPayKaniInfoRizMetre, lstItemFBShomarehForGet) {

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
                        //str += "<i id='iUpdate" + id + "' class='fa fa-save SaveRMUStyle' onclick=\"UpdateRMUAddedItemsClick('" + id + "','" + ItemHasConditionId + "'," + ConditionGroupId + ")\"></i>";
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

        $targetDivRizMetrePayKani = $('#ViewRizMetrePayKani');

        $targetDivRizMetrePayKani.html(str);
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




