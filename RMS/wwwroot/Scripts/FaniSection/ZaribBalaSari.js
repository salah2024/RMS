function ShowZaribBalaSari(OperationId, event) {
    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }

    debugger;
    $('#HDFOperationId').val(OperationId);

    let str = `
<div class="row boardRowStyle" style="direction: rtl; text-align: right;">
  <!-- گروه طرح -->
  <div class="col-12">
  <div class="row">
  <div class="col-3">
      <fieldset style="margin-top:10px;">
      <legend>طرح</legend>
      <div class="form-check mb-2">
        <input class="form-check-input RadioStyle"
               type="radio" 
               name="planGroup" 
               id="plan1" 
               value="1"
               onclick="getZarib()"/>
        <label class="form-check-label RadioLabelStyle" for="plan1">
          طرح عمرانی
        </label>
      </div>
    <div class="form-check">
        <input class="form-check-input RadioStyle"
               type="radio" 
               name="planGroup" 
               id="plan2" 
               value="2"
               onclick="getZarib()" />
        <label class="form-check-label RadioLabelStyle" for="plan2">
          طرح غیر عمرانی
        </label>
      </div>
    </fieldset>
  </div>
  <div class="col-5">
      <fieldset>
      <legend>مناقصه</legend>
      <div class="form-check">
        <input class="form-check-input RadioStyle"
               type="radio"
               name="tenderGroup"
               id="tender3"
               value="3"
               onclick="getZarib()"/>
        <label class="form-check-label RadioLabelStyle" for="tender3">
          مناقصه عمومی
        </label>
      </div>
    <div class="form-check">
        <input class="form-check-input RadioStyle"
               type="radio"
               name="tenderGroup"
               id="tender4"
               value="4"
               onclick="getZarib()" />
        <label class="form-check-label RadioLabelStyle" for="tender4">
          عدم الزام به برگزاری مناقصه ناشی از انحصار فرآیند مناقصه (مناقصه محدود)
        </label>
    </div>
     <div class="form-check">
        <input class="form-check-input RadioStyle"
               type="radio"
               name="tenderGroup"
               id="tender5"
               value="5"
               onclick="getZarib()" />
        <label class="form-check-label RadioLabelStyle" for="tender5">
          ترک تشریفات
        </label>
      </div>
    </fieldset>
  </div>
  <!-- ضریب بالاسری -->
  <div class="col-3" style="text-align:right;display:none" id="divZaribBalaSari">
    <div style="margin-top:10px;">
        <span style="font-weight:bold; margin-left:4px;margin-left: 4px;background-color: lightblue;padding: 0px 10px;border-radius: 5px;">ضریب بالاسری</span>
        <span id="lblZaribBalasari" style="font-weight:bold;"></span>
    </div>
  </div>
  </div>
</div>
</div>
</div>`;
    $('#ula' + OperationId).html(str);

    GetZaribSaved();
}

function GetZaribSaved() {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = $('#HDFYear').val();

    var vardata = new Object();
    vardata.BaravordId = BarAvordUserId;
    vardata.Year = Year;
    $.ajax({
        type: "POST",
        url: "/ZaribBalaSari/GetBaravordZaribBalasari",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            // نمایش ضریب
            if (data.length > 0) {
                $('#divZaribBalaSari').show();
                $('#lblZaribBalasari').html(data[0].zaribBalasari);
            }

            BaravordZaribBalaSari = data.baravordZaribBalaSari;
            // انتخاب رادیوها
            //$.each(BaravordZaribBalaSari, function () {

                // اگر مقدار tarh داشت (1 یا 2)
            if (BaravordZaribBalaSari.tarh && BaravordZaribBalaSari.tarh !== 0) {
                $(`input[type=radio][name=planGroup][value=${BaravordZaribBalaSari.tarh}]`).prop("checked", true);
                }

                // اگر مقدار monaghese داشت (3,4,5)
            if (BaravordZaribBalaSari.monaghese && BaravordZaribBalaSari.monaghese !== 0) {
                    $(`input[type=radio][name=tenderGroup][value=${BaravordZaribBalaSari.monaghese}]`).prop("checked", true);
                }

            //});
            debugger;
            lstResultZaribBalaSari = data.lstResultZaribBalaSari;
            let str = '';
            $.each(lstResultZaribBalaSari, function () {
                debugger;
                FBName = this.fbName;
                ZaribBalaSari = this.zaribBalaSari;
                str += `
                        <div class="col-12" style="border:1px solid #fff;margin-top: 10px;">
                        <div class="row">
                            <div class="col-6" style="text-align:left">${FBName} =</div>
                            <div class="col-6">${ZaribBalaSari}</div>
                        </div>
                        </div>
                    `
            });

            debugger;
            $('#divZaribBalaSari').show();
            const lbl = $('#lblZaribBalasari');
            lbl.html(str);

        },
        error: function () {
            toastr.error('مشکل در بارگزاری اطلاعات ثبت شده', 'خطا');
        }
    });

}

function getZarib() {
    const planSelected = document.querySelector('input[name="planGroup"]:checked');
    const tenderSelected = document.querySelector('input[name="tenderGroup"]:checked');
    const lbl = $('#lblZaribBalasari');

    if (!lbl) return;

    if (planSelected && tenderSelected) {
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        Year = $('#HDFYear').val();
        var vardata = new Object();
        vardata.planSelected = planSelected.value;
        vardata.tenderSelected = tenderSelected.value;
        vardata.BaravordId = BarAvordUserId;
        vardata.Year = Year;
        $.ajax({
            type: "POST",
            url: "/ZaribBalaSari/GetZaribBalaSari",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                lstResultZaribBalaSari = data;
                let str = '';
                $.each(lstResultZaribBalaSari, function () {
                    FBName = this.fbName;
                    ZaribBalaSari = this.zaribBalaSari;
                    str += `
                        <div class="col-12" style="border:1px solid #fff;margin-top: 10px;">
                        <div class="row">
                            <div class="col-6" style="text-align:left">${FBName} =</div>
                            <div class="col-6">${ZaribBalaSari}</div>
                        </div>
                        </div>
                    `
                });

                $('#divZaribBalaSari').show();
                lbl.html(str);
            },
            error: function (response) {
                toastr.error('مشکل در بارگزاری ضریب بالاسری', 'خطا');
            }
        });
    } else {
        // در غیر این صورت لیبل خالی شود
        lbl.html('');
    }
}
