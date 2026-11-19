var lstVaheds;

// بارگذاری تنبل فایل‌های اختصاصی این صفحه؛ درخواست‌های هم‌زمان از یک Promise مشترک استفاده می‌کنند.
var showBarAvordAssetPromises = window.showBarAvordAssetPromises || (window.showBarAvordAssetPromises = {});

function ensureShowBarAvordAsset(assetName) {
    var $table = $('#fosoulTable');
    var assetUrl = assetName === "star"
        ? $table.attr('data-star-script-url')
        : assetName === "detail"
            ? $table.attr('data-detail-script-url')
            : $table.attr('data-tree-script-url');

    if (!assetUrl) {
        return Promise.reject(new Error("URL for ShowBarAvord asset was not configured: " + assetName));
    }

    if (assetName === "tree" && $.fn.jstree) {
        return Promise.resolve();
    }

    if (assetName === "star" && typeof window.SaveRMUStarClick === "function") {
        return Promise.resolve();
    }

    if (assetName === "detail" && typeof window.renderTable === "function") {
        return Promise.resolve();
    }

    if (!showBarAvordAssetPromises[assetName]) {
        showBarAvordAssetPromises[assetName] = $.getScript(assetUrl).then(function () {
            if (assetName === "tree" && !$.fn.jstree) {
                throw new Error("jsTree script loaded but $.fn.jstree is unavailable.");
            }
            if (assetName === "star" && typeof window.SaveRMUStarClick !== "function") {
                throw new Error("Star script loaded but expected functions were not registered.");
            }
        }).catch(function (error) {
            delete showBarAvordAssetPromises[assetName];
            throw error;
        });
    }

    return showBarAvordAssetPromises[assetName];
}

function GetFosoul() {

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());
    BAType = $('#HDFBAType').val();
    strUrl = '/BaseInfo/GetFosoulForBarAvord';
    if (BAType != 'BarAvord')
        strUrl = '/BaseInfo/GetFosoulForSooratVaziat';


    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Code = "";
    $.ajax({
        type: "post",
        url: strUrl,
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Fosoul = response.fosoul;
            lstVaheds = response.lstVaheds;
            renderTableFosoul(response);


            if (BAType != 'BarAvord') {
            }
            else {
                GetJamBarAvordAll();
            }

        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}


function GetSumAllFBForSooratVaziat() {

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    strUrl = '/BaseInfo/GetSumAllFBForSooratVaziat';

    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    $.ajax({
        type: "post",
        url: strUrl,
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#tdJamBarAvordKolWithZarib').html(formatNumber(response.toFixed(0)));
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function GetSumAllFosoulForSooratVaziat() {

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    strUrl = '/BaseInfo/GetSumAllFosoulForSooratVaziat';

    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    $.ajax({
        type: "post",
        url: strUrl,
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#tdjameFaslAllBaZarib-102').html(formatNumber(response.toFixed(0)));
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function GetJamBarAvordAll() {

    BarAvordId = $('#HDFBarAvordUserID').val();
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.Year = Year;
    vardata.BarAvordId = BarAvordId;
    $.ajax({
        type: "post",
        url: "/BarAvordUser/CalculateBarAvordAll",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            jameFasl = response.jameFasl;
            jameFaslStar = response.jameFaslStar;
            jameFaslKol = response.jameFaslKol;
            jameFaslInZarib = response.jameFaslInZarib;
            jameFaslStarInZarib = response.jameFaslStarInZarib;
            jameFaslKolInZarib = response.jameFaslKolInZarib;
            //$('#tdJamBarAvordBase').html(formatNumber(jameFasl.toFixed(0)));
            //$('#tdJamBarAvordStar').html(formatNumber(jameFaslStar.toFixed(0)));
            //$('#tdJamBarAvordKol').html(formatNumber(jameFaslKol.toFixed(0)));
            //$('#tdJamBarAvordBaseWithZarib').html(formatNumber(jameFaslInZarib.toFixed(0)));
            //$('#tdJamBarAvordStarWithZarib').html(formatNumber(jameFaslStarInZarib.toFixed(0)));
            $('#tdJamBarAvordKolWithZarib').html(formatNumber(jameFaslKolInZarib.toFixed(0)));
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function GetJamFosoulAll() {
    BarAvordId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.Year = Year;
    vardata.NoeFBId = NoeFBId;
    vardata.BarAvordId = BarAvordId;
    $.ajax({
        type: "post",
        url: "/BarAvordUser/CalculateFosoulAll",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            jameFasl = response.jameFasl;
            jameFaslWithOutTajhiz = response.jameFaslWithOutTajhiz;
            jameFaslStar = response.jameFaslStar;
            jameFaslKol = response.jameFaslKol;
            jameFaslKolWithOutTajhiz = response.jameFaslKolWithOutTajhiz;
            jameFaslInZarib = response.jameFaslInZarib;
            jameFaslWithOutTajhizInZarib = response.jameFaslWithOutTajhizInZarib;
            jameFaslStarInZarib = response.jameFaslStarInZarib;
            jameFaslKolInZarib = response.jameFaslKolInZarib;
            jameFaslKolWithOutTajhizInZarib = response.jameFaslKolWithOutTajhizInZarib;
            check = response.check;

            $('#tdjameFasl-101').html(formatNumber(jameFaslWithOutTajhiz.toFixed(0)));
            //$('#tdjameFasl-102').html(formatNumber(jameFasl.toFixed(0)));
            $('#tdjameFaslStar-101').html(formatNumber(jameFaslStar.toFixed(0)));
            //$('#tdjameFaslStar-102').html(formatNumber(jameFaslStar.toFixed(0)));
            $('#tdjameFaslAll-101').html(formatNumber(jameFaslKolWithOutTajhiz.toFixed(0)));
            //$('#tdjameFaslAll-102').html(formatNumber(jameFaslKol.toFixed(0)));
            $('#tdjameFaslBaZarib-101').html(formatNumber(jameFaslWithOutTajhizInZarib.toFixed(0)));
            //$('#tdjameFaslBaZarib-102').html(formatNumber(jameFaslInZarib.toFixed(0)));
            $('#tdjameFaslStarBaZarib-101').html(formatNumber(jameFaslStarInZarib.toFixed(0)));
            //$('#tdjameFaslStarBaZarib-102').html(formatNumber(jameFaslStarInZarib.toFixed(0)));
            $('#tdjameFaslAllBaZarib-101').html(formatNumber(jameFaslKolWithOutTajhizInZarib.toFixed(0)));
            $('#tdjameFaslAllBaZarib-102').html(formatNumber(jameFaslKolInZarib.toFixed(0)));

            if (check.check) {
                let des = check.des;
                str = `
                <div>
                <p>توجه</p>
                <p>
                جمع مبلغ برآورد ردیف‌های ستاره‌دار، نسبت به جمع مبلغ برآورد ردیف‌های فهرست‌بها (پایه و غیرپایه) بدون اعمال هزینه تجهیز و برچیدن کارگاه، در این رشته، بیشتر از  حد اقلام ستاره دار (درصد) طبق جدول  الف  و بند 2-6 دستورالعمل کاربرد شده است لذا، لازم است دستگاه اجرایی قبل از انجام مناقصه، شرح و بهای واحد تمامی ردیف‌های ستاره‌دار در آن رشته را، پس از تصویب، همراه با تجزیه قیمت مربوط، به دبیرخانه شورای عالی فنی در سازمان برنامه و بودجه کشور ارسال دارد تا پس از رسیدگی و تصویب توسط شورای عالی فنی، (بر اساس دستورالعمل نحوه تهیه و تصویب ردیف‌های ستاره‌دار) ملاک عمل قرار گیرد.
                </p>
                <p>${des}</p>
                </div>
                `;

                toastr.info(str, 'هشدار');
            }


            LoadDarsadTajhizKargah();
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function chkDarsadTajhizClick(Obj) {

    IdSplit = Obj.id.split('-');
    Id = IdSplit[1];

    chkDarsadTajhiz = $('#chkDarsadTajhiz-' + Id).prop('checked');
    if (chkDarsadTajhiz == true) {
        $('#txtDarsadTajhiz-' + Id).focus();
        $('#txtDarsadTajhiz-' + Id).addClass('blinking');
        $('#' + Id).slideUp(500);
    }
    else {

        curentMeghdar = $('#tdjameFasl-' + Id);
        curentMeghdar.html('');
        $('#tdjameFaslStar-' + Id).html('');
        $('#tdjameFaslAll-' + Id).html('');
        $('#tdjameFaslBaZarib-' + Id).html('');
        $('#tdjameFaslStarBaZarib-' + Id).html('');
        $('#tdjameFaslAllBaZarib-' + Id).html('');
        DeleteTajhizKargahDarsad();


        $('#txtDarsadTajhiz-' + Id).removeClass('blinking');
        $('#txtDarsadTajhiz-' + Id).val('');
    }
}

function LoadDarsadTajhizKargah() {
    BarAvordId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.NoeFB = NoeFBId;
    vardata.BarAvordUserId = BarAvordId;
    vardata.Year = Year;

    $.ajax({
        type: "post",
        url: "/TajhizKargahDarsad/GetCurrentTajhizKargahDarsad",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response != "NOK") {
                TajhizKargahDarsad = response.tajhizKargahDarsad;
                Zarayeb = response.zarayeb;

                Darsad = TajhizKargahDarsad.darsad;
                Meghdar = TajhizKargahDarsad.meghdar;

                zaribBalaSari = Zarayeb.zaribBalaSari;
                zaribManteghe = Zarayeb.zaribManteghe;

                //MeghdarInZarib = Meghdar * zaribBalaSari * zaribManteghe;

                $('#txtDarsadTajhiz-99').val(Darsad);
                //$('#tdjameFaslStar-99').html(formatNumber(Meghdar.toFixed(0)));

                $('#chkDarsadTajhiz-99').prop('checked', true);

                //$('#tdjameFaslAll-99').html(formatNumber(Meghdar.toFixed(0)));

                //$('#tdjameFaslStarBaZarib-99').html(formatNumber(Meghdar.toFixed(0)));


                $('#tdjameFaslAllBaZarib-99').html(formatNumber(Meghdar.toFixed(0)));

            }
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}


function renderTableFosoul(data) {

    Fosoul = data.fosoul;

    const tbody = $('#fosoulTable tbody');
    tbody.empty();
    Fosoul.forEach((item, index) => {
        const rowClass = index % 2 === 0 ? '' : 'table-alt';

        FaslAsli = item.faslAsli;
        let style = '';
        let tagA = `<a href="#">${item.faslName}</a>`;
        if (item.code == '101' || item.code == '102') {
            style = 'background-color:#d4d0ff !important;';
            tagA = `<span style="font-size:15px;padding-right: 30px;">${item.faslName}</span>`;
        }

        Id = item.id;

        Label1 = item.code == '99' ? `
        <label>
          <input type="checkbox" id="chkDarsadTajhiz-${item.code}" onclick="event.stopPropagation();chkDarsadTajhizClick(this)" onchange="event.stopPropagation();" style="margin-right: 5px;" />
            درصدی از جمع فصول         
        </label>
           <input style="width:60px;text-align:center" type="number" id="txtDarsadTajhiz-${item.code}" calss="form-control form-control-sm" onclick="event.stopPropagation();" onchange="txtChangeTajhiz()" />
          <span> درصد </span>
                    `: '';


        const mainRow = $(`
		<tr class="${rowClass}" style="${style}">
        <td style="display:none" id="tdFaslAli-${item.id}">${FaslAsli}</td>
		<td style="display:none">${item.id}</td>
		<td style="text-align:right;width: 40%;">

        <a onclick="event.stopPropagation();DescriptionShow(${item.id})"><i class="fa fa-book" style="color:#f656ff"></i></a>

                ${tagA} ${Label1}
        </td>
		<td style="text-align:center">
            <span id="tdjameFasl-${item.code}">
			    ${formatNumber(item.jameFasl.toFixed(0))}
            </span>
		</td>
		<td style="text-align:center">
            <span id="tdjameFaslStar-${item.code}">
			    ${item.code != '99' ? formatNumber(item.jameFaslStar.toFixed(0)) : ''}
            </span>
		</td>
        <td style="text-align:center">
            <span id="tdjameFaslAll-${item.code}">
			    ${item.code != '99' ? formatNumber(item.jameFaslAll.toFixed(0)) : ''}
            </span>
		</td>
        <td style="text-align:center">
            <span id="tdjameFaslBaZarib-${item.code}">
			    ${formatNumber(item.jameFaslWithZarib.toFixed(0))}
            </span>
        </td>
		<td style="text-align:center">
            <span id="tdjameFaslStarBaZarib-${item.code}">
			    ${item.code != '99' ? formatNumber(item.jameFaslStarWithZarib.toFixed(0)) : ''}
            </span>
        </td>
        <td style="text-align:center">
            <span id="tdjameFaslAllBaZarib-${item.code}">
			    ${formatNumber(item.jameFaslWithZaribAll.toFixed(0))}
            </span>
        </td>
        </tr>
		<tr id="${item.id}" style="display:none">
			<td colspan="7">
				<div id="barAvordContainer-${item.id}" style="overflow:auto;">
                    
                </div>
			</td>
		</tr>
		`);
        //convertNumbersInPage();

        mainRow.on('click', function (e) {

            if ($(e.target).closest('[id^="barAvordContainer-"]').length > 0) {
                return;
            }
            if (item.code == '_' || item.code == '101' || item.code == '102') {
                return 0;
            }
            else if (item.code == '99') {
                chkDarsadTajhiz = $('#chkDarsadTajhiz-' + item.code).prop('checked');
                if (chkDarsadTajhiz == true) {
                    return 0;
                }
            }
            e.stopPropagation();

            const rowId = item.id;
            const detailRow = $(`#${rowId}`);
            var faslAsli = item.faslAsli;

            // اگر باز است → ببند
            if (detailRow.is(':visible')) {
                detailRow.hide();
            } else {
                // بستن همه ردیف‌های جزئیات
                $('#fosoulTable tr[id]').not(detailRow).hide();

                // باز کردن ردیف انتخاب‌شده
                detailRow.show();


                GetBarAvord(item.id, item.code, item.faslName, faslAsli);
            }
        });

        $('#chkNonZeroMeghdarFasl').off('change').on('change', function () {

            isOneRowFilled = false;

            const showOnlyNonZero = $(this).is(':checked');
            // همه‌ی ردیف‌های اصلی (نه ردیف دوم مربوط به barAvordContainer)
            $('#fosoulTable tbody tr').each(function () {


                const $row = $(this);

                const tdjameFaslAllBaZarib = $row.find('[id^="tdjameFaslAllBaZarib-"]');

                if (tdjameFaslAllBaZarib.length > 0) {

                    let jameFaslAllBaZarib = tdjameFaslAllBaZarib.text().replace(/\s+/g, '').replace(/[٬,]/g, '');

                    let EnNumjameFaslAllBaZarib = convertPersianToEnglish(jameFaslAllBaZarib);

                    const jameFaslAllBaZarib1 = EnNumjameFaslAllBaZarib != "" ? parseFloat(EnNumjameFaslAllBaZarib) : 0;

                    if (showOnlyNonZero) {

                        if (jameFaslAllBaZarib1 === 0) {
                            $row.hide(); // مخفی‌سازی ردیف اصلی

                        } else {
                            $row.show();
                            isOneRowFilled = true;
                        }
                    } else {
                        $row.show();

                    }
                }
            });

            if (!isOneRowFilled) {
                $('#fosoulTable tbody tr').each(function () {


                    const $row = $(this);

                    const tdjameFaslAllBaZarib = $row.find('[id^="tdjameFaslAllBaZarib-"]');

                    if (tdjameFaslAllBaZarib.length > 0) {

                        let jameFaslAllBaZarib = tdjameFaslAllBaZarib.text().replace(/\s+/g, '').replace(/[٬,]/g, '');

                        let EnNumjameFaslAllBaZarib = convertPersianToEnglish(jameFaslAllBaZarib);

                        const jameFaslAllBaZarib1 = EnNumjameFaslAllBaZarib != "" ? parseFloat(EnNumjameFaslAllBaZarib) : 0;

                        $row.show();
                        
                    }
                });
            }
        });

        tbody.append(mainRow);

    });

    BAType = $('#HDFBAType').val();

    if (BAType != 'BarAvord') {
        SumFosoulForSooratVaziat = data.sumFosoulForSooratVaziat;

        jameFasl = SumFosoulForSooratVaziat.dJameFasl;
        jameFaslAll = SumFosoulForSooratVaziat.dJameFaslAll;
        jameFaslStar = SumFosoulForSooratVaziat.dJameFaslStar;
        jameFaslStarInZarib = SumFosoulForSooratVaziat.dJameFaslStarWithZarib;
        jameFaslWithZarib = SumFosoulForSooratVaziat.dJameFaslWithZarib;
        jameFaslWithZaribAll = SumFosoulForSooratVaziat.dJameFaslWithZaribAll;

        jameFaslWithTajhizWithZarib = SumFosoulForSooratVaziat.dJameFaslWithTajhizWithZarib;
        //jameTajhiz = SumFosoulForSooratVaziat.dJameTajhiz;

        $('#tdjameFasl-101').html(formatNumber(jameFasl.toFixed(0)));
        $('#tdjameFaslStar-101').html(formatNumber(jameFaslStar.toFixed(0)));
        $('#tdjameFaslAll-101').html(formatNumber(jameFaslAll.toFixed(0)));//jameFaslKolWithOutTajhiz.toFixed(0)));
        $('#tdjameFaslBaZarib-101').html(formatNumber(jameFaslWithZarib.toFixed(0)));
        $('#tdjameFaslStarBaZarib-101').html(formatNumber(jameFaslStarInZarib.toFixed(0)));
        $('#tdjameFaslAllBaZarib-101').html(formatNumber(jameFaslWithZaribAll.toFixed(0)));

        $('#tdjameFaslAllBaZarib-102').html(formatNumber(jameFaslWithTajhizWithZarib.toFixed(0)));

        //$('#tdjameFaslAllBaZarib-99').html(formatNumber(jameTajhiz.toFixed(0)));

        GetSumAllFBForSooratVaziat();

        LoadDarsadTajhizKargah();
    }
    else
        GetJamFosoulAll();

    $('#chkNonZeroMeghdarFasl').click();


}

function txtChangeTajhiz() {

    Obj = $('#txtDarsadTajhiz-99')[0];

    IdSplit = Obj.id.split('-');
    Id = IdSplit[1];

    jameFaslBaZaribAll = $('#tdjameFaslAllBaZarib-101').html();
    JamFaslAdad = parseFloat(convertPersianToEnglish(jameFaslBaZaribAll.replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
    thisVal = parseFloat(Obj.value);
    MeghdarNew = thisVal * JamFaslAdad;
    curentMeghdar = $(Obj).parent().parent().find('[id^="tdjameFaslAllBaZarib-"]')
    MeghdarPercent = MeghdarNew / 100;
    dMeghdarPercent = formatNumber(MeghdarPercent.toFixed(0));

    if (isNaN(MeghdarPercent)) {
        $(Obj).val('');
        $('#chkDarsadTajhiz-' + Id).prop('checked', false);
        curentMeghdar.html('');
        toastr.warning('هشدار', 'مقدار برآورد خالی میباشد');
    }
    else {
        $('#chkDarsadTajhiz-' + Id).prop('checked', true);
        $('#' + Id).slideUp(500);
        SaveTajhizKargahDarsad(Obj, thisVal, MeghdarPercent.toFixed(0));
    }
}

function SaveTajhizKargahDarsad(Obj, Darsad, MeghdarPercent) {

    //curentMeghdar = $(Obj).parent().parent().find('[id^="tdjameFasl-"]')
    //curentMeghdar.html(dMeghdarPercent);


    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Darsad = Darsad;
    vardata.Meghdar = MeghdarPercent;
    vardata.Year = Year;
    $.ajax({
        type: "post",
        url: "/TajhizKargahDarsad/TajhizKargahDarsadSave",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            zaribBalaSari = response.zaribBalaSari;
            zaribManteghe = response.zaribManteghe;

            MeghdarInZarib = MeghdarPercent * zaribBalaSari * zaribManteghe;

            //curentMeghdarInZarib = $(Obj).parent().parent().find('[id^="tdjameFaslBaZarib-"]')
            //curentMeghdarInZarib.html(formatNumber(MeghdarInZarib.toFixed(0)));

            //curentjameFaslAll = $(Obj).parent().parent().find('[id^="tdjameFaslAll-"]')
            //curentjameFaslAll.html(dMeghdarPercent);

            //curentjameFaslAllBaZarib = $(Obj).parent().parent().find('[id^="tdjameFaslAllBaZarib-"]')
            //curentjameFaslAllBaZarib.html(formatNumber(MeghdarInZarib.toFixed(0)));

            BAType = $('#HDFBAType').val();

            if (BAType != 'BarAvord') {


                GetSumAllFBForSooratVaziat();
                GetSumAllFosoulForSooratVaziat();

                LoadDarsadTajhizKargah();
            }
            else {
                GetJamBarAvordAll();
                GetJamFosoulAll();
            }
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function DeleteTajhizKargahDarsad() {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFB = NoeFB;
    vardata.Year = Year;
    $.ajax({
        type: "post",
        url: "/TajhizKargahDarsad/TajhizKargahDarsadDelete",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('حذف درصد تجهیزکارگاه بدرستی صورت گرفت', 'موفقیت');

                BAType = $('#HDFBAType').val();

                if (BAType != 'BarAvord') {
                    GetSumAllFBForSooratVaziat();
                    GetSumAllFosoulForSooratVaziat();
                    //LoadDarsadTajhizKargah();
                }
                else {
                    GetJamBarAvordAll();
                    GetJamFosoulAll();
                }
            }
        },
        error: function (response) {
            toastr.error('مشکل در حذف درصد تجهیزکارگاه', 'خطا');
        }
    });
}


function GetBarAvord(ItemId, Code, faslName, faslAsli) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());
    var vardata = new Object();
    vardata.Id = ItemId;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.ShomarehFasl = Code;

    // دانلود فایل جزئیات را هم‌زمان با درخواست اطلاعات فصل آغاز می‌کنیم؛
    // قبلاً دانلود فایل بعد از پاسخ سرور شروع می‌شد و این دو زمان به هم اضافه می‌شدند.
    var detailAssetPromise = ensureShowBarAvordAsset("detail");

    $.ajax({
        type: "post",
        url: "/ShowBarAvordUser/GetUserBarAvord",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            userBarAvordOutPut = response.userBarAvordOutPut || [];
            lstItemFBStars = response.lstItemFBStars || [];

            // فایل جزئیات از ابتدای درخواست فصل در حال دانلود بوده یا از قبل آماده است.
            detailAssetPromise.then(function () {
                if (lstItemFBStars.length === 0) {
                    renderTable(userBarAvordOutPut, lstItemFBStars, Code, ItemId, faslAsli);
                    return;
                }

                // فایل عملیات ستاره‌دار فقط در فصل‌هایی دریافت می‌شود که ردیف ستاره‌دار دارند.
                return ensureShowBarAvordAsset("star").then(function () {
                    renderTable(userBarAvordOutPut, lstItemFBStars, Code, ItemId, faslAsli);
                });
            }).catch(function (error) {
                console.error(error);
                toastr.error('بارگذاری امکانات جزئیات فصل با خطا مواجه شد', 'خطا');
            });
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}


function DescriptionShow(Id) {
    var vardata = new Object();
    vardata.Id = Id;
    $.ajax({
        type: "post",
        url: "/BaseInfo/GetDescriptionForFasl",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#ShowDes').html(response.description);
            //$('#ModalTitle').html(response.title);
            $('#aViewDes').click();
            $('#PopupViewDes').on('shown.bs.modal', function () {
                var modalBody = $(this).find('.modal-body');
                modalBody.scrollTop(0);
            });
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}
function formatNumber(num) {
    if (num == null) return '';
    if (num == 0) return '';
    if (isNaN(num)) return '';
    return Number(num).toLocaleString('fa-IR'); // یا 'en-US' برای کاما
}






function BahayeVahedNewMeghdarChange($input, FBId, itemFbShomareh) {


    var newValue = $input.value;
    if (!$.isNumeric(newValue)) {
        toastr.error('مقدار وارد شده نامعتبر میباشد', 'خطا');
        $input.addClass('blinking');
        return;
    }
    var floatVal = convertPersianToEnglish(newValue);
    var floatValEn = parseFloat(floatVal.replace(/٬/g, '').replace(/,/g, ''));

    // پیدا کردن ردیف والد
    const $row = $($input).closest('tr');

    // گرفتن مقدار موجود در ستون مقدار (meghdarFasl)
    var meghdarText = $row.find('#meghdarFasl_' + itemFbShomareh).text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.');
    var MeghdarEn = convertPersianToEnglish(meghdarText);
    var meghdar = parseFloat(MeghdarEn);

    // مدیریت کلاس blinking
    if (meghdar > 0 && (!floatValEn || floatValEn === 0)) {
        $($input).addClass('blinking');
    } else {
        $($input).removeClass('blinking');
    }

    // به‌روزرسانی بهای کل
    if (!isNaN(meghdar) && !isNaN(floatValEn)) {
        var bahayeKol = floatValEn * meghdar;
        $row.find('#bahayeKolFasl').text(formatNumber(bahayeKol.toFixed(0)));
    }

    $input.value = formatNumber(floatValEn.toFixed(0));

    strStar1 = `<div class="row"><span style="font-size:25px">*</span><input style="font-size:20px" class="ReturnItemsStar" type="submit" value="🔄" onclick="StarReturn(this,'${FBId}','${itemFbShomareh}')"/></div>`

    $('#span' + itemFbShomareh).html(strStar1);
    BahayeVahedNewSave($input, floatValEn, FBId, itemFbShomareh);
}

function ItemZaribChange($input, FBId, itemFbShomareh) {


    var newValue = $input.value;
    if (!$.isNumeric(newValue)) {
        toastr.error('مقدار وارد شده نامعتبر میباشد', 'خطا');
        $input.addClass('blinking');
        return;
    }
    var floatVal = convertPersianToEnglish(newValue);
    if (parseFloat(newValue) > 1 || parseFloat(newValue) < 0) {
        toastr.info('مقدار نمیتواند بزرگتر از 1 یا کوچکتر از 0 باشد', 'هشدار');
        $input.value = 1;
        floatVal = "1";
    }

    var floatValEn = parseFloat(floatVal.replace(/٬/g, '').replace(/,/g, ''));

    // پیدا کردن ردیف والد
    const $row = $($input).closest('tr');

    // گرفتن مقدار موجود در ستون مقدار (meghdarFasl)
    var meghdarText = $row.find('#meghdarFasl_' + itemFbShomareh).text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.');
    var MeghdarEn = convertPersianToEnglish(meghdarText);
    var meghdar = parseFloat(MeghdarEn);

    // گرفتن مقدار موجود در ستون مقدار (BahayeVahed)
    var bahayeVahedText = $row.find('[id^="txtBahayeVahed-"]').val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.');
    var bahayeVahedEn = convertPersianToEnglish(bahayeVahedText);
    var bahayeVahed = parseFloat(bahayeVahedEn);

    // // مدیریت کلاس blinking
    // if (meghdar > 0 && (!floatValEn || floatValEn === 0)) {
    //     $($input).addClass('blinking');
    // } else {
    //     $($input).removeClass('blinking');
    // }

    // // به‌روزرسانی بهای کل
    if (!isNaN(meghdar) && !isNaN(floatValEn) && !isNaN(bahayeVahedEn)) {
        var bahayeKol = floatValEn * meghdar * bahayeVahedEn;
        $row.find('#bahayeKolFasl_' + itemFbShomareh).text(formatNumber(bahayeKol.toFixed(0)));
    }

    $input.value = formatNumber(floatValEn.toFixed(3));

    ItemZaribSave($input, floatValEn, FBId, itemFbShomareh);
}

function ItemZaribStarChange($input, ItemStarId, itemFbShomareh) {


    var newValue = $input.value;
    if (!$.isNumeric(newValue)) {
        toastr.error('مقدار وارد شده نامعتبر میباشد', 'خطا');
        $input.addClass('blinking');
        return;
    }
    var floatVal = convertPersianToEnglish(newValue);
    if (parseFloat(newValue) > 1 || parseFloat(newValue) < 0) {
        toastr.info('مقدار نمیتواند بزرگتر از 1 یا کوچکتر از 0 باشد', 'هشدار');
        $input.value = 1;
        floatVal = "1";
    }

    var floatValEn = parseFloat(floatVal.replace(/٬/g, '').replace(/,/g, ''));

    // پیدا کردن ردیف والد
    const $row = $($input).closest('tr');

    // گرفتن مقدار موجود در ستون مقدار (meghdarFasl)
    var meghdarText = $row.find('#meghdarFaslStar_' + itemFbShomareh).text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.');
    var MeghdarEn = convertPersianToEnglish(meghdarText);
    var meghdar = parseFloat(MeghdarEn);

    // گرفتن مقدار موجود در ستون مقدار (BahayeVahed)
    var bahayeVahedText = $row.find('#bahayeVahedFaslStar_' + itemFbShomareh).text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.');
    var bahayeVahedEn = convertPersianToEnglish(bahayeVahedText);
    var bahayeVahed = parseFloat(bahayeVahedEn);

    // // مدیریت کلاس blinking
    // if (meghdar > 0 && (!floatValEn || floatValEn === 0)) {
    //     $($input).addClass('blinking');
    // } else {
    //     $($input).removeClass('blinking');
    // }

    // // به‌روزرسانی بهای کل
    if (!isNaN(meghdar) && !isNaN(floatValEn) && !isNaN(bahayeVahedEn)) {
        var bahayeKol = floatValEn * meghdar * bahayeVahedEn;
        $row.find('#bahayeKolFaslStar_' + itemFbShomareh).text(formatNumber(bahayeKol.toFixed(0)));
    }

    $input.value = formatNumber(floatValEn.toFixed(3));

    ItemZaribStarSave($input, floatValEn, ItemStarId, itemFbShomareh);
}

function ItemZaribStarSave(element, ItemZarib, ItemStarId, itemFbShomareh) {

    var vardata = new Object();
    vardata.ItemStarId = ItemStarId;
    vardata.ItemZarib = ItemZarib;

    $.ajax({
        type: "POST",
        url: '/BaravordUser/ConfirmItemZaribStar',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data.split('_');
            if (info[0] == "OK") {

                ///////////////

                Code = itemFbShomareh.substring(0, 2);

                refreshBaseItem(element, itemFbShomareh, Code);
                GetJamBarAvordAll();
                GetJamFosoulAll();
                //////////////////

                toastr.success('ضریب آیتم بدرستی درج گردید', 'موفقیت');
            }
            else
                toastr.info('مشکل در درج ضریب آیتم', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در درج ضریب آیتم', 'خطا');
        }
    });
}


function ItemZaribSave(element, ItemZarib, FBId, itemFbShomareh) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.FBId = FBId == "null" ? '00000000-0000-0000-0000-000000000000' : FBId;
    vardata.ItemZarib = ItemZarib;
    vardata.itemFbShomareh = itemFbShomareh;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: '/BaravordUser/ConfirmItemZarib',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data.split('_');
            if (info[0] == "OK") {

                //const mainRow = element.closest('tr');
                //if (!mainRow) return;

                //txtmeghdarFasl = mainRow.querySelector('#meghdarFasl_' + itemFbShomareh);
                //bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);

                //meghdarFasl = parseFloat(convertPersianToEnglish(txtmeghdarFasl.innerHTML.replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //BahayeVahedNew1 = parseFloat(BahayeVahedNew);

                //bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);
                //bahayeKolFasl.innerHTML = toPersianDigits(formatNumber(isNaN(meghdarFasl) ? 0 : meghdarFasl * BahayeVahedNew1));

                ///////////////

                Code = itemFbShomareh.substring(0, 2);

                refreshBaseItem(element, itemFbShomareh, Code);
                GetJamBarAvordAll();
                GetJamFosoulAll();
                //////////////////

                toastr.success('ضریب آیتم بدرستی درج گردید', 'موفقیت');
            }
            else
                toastr.info('مشکل در درج ضریب آیتم', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در درج ضریب آیتم', 'خطا');
        }
    });
}


function BahayeVahedNewSave(element, BahayeVahedNew, FBId, itemFbShomareh) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.FBId = FBId == "null" ? '00000000-0000-0000-0000-000000000000' : FBId;
    vardata.BahayeVahedNew = BahayeVahedNew.toString();
    vardata.itemFbShomareh = itemFbShomareh;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    $.ajax({
        type: "POST",
        url: '/BaravordUser/ConfirmBahayeVahedNew',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data.split('_');
            if (info[0] == "OK") {

                const mainRow = element.closest('tr');
                if (!mainRow) return;

                txtmeghdarFasl = mainRow.querySelector('#meghdarFasl_' + itemFbShomareh);
                bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);

                meghdarFasl = parseFloat(convertPersianToEnglish(txtmeghdarFasl.innerHTML.replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                BahayeVahedNew1 = parseFloat(BahayeVahedNew);


                //if (isNaN(bahayeKol)) {
                //    bahayeKol = 0;
                //}
                bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);
                bahayeKolFasl.innerHTML = toPersianDigits(formatNumber(isNaN(meghdarFasl) ? 0 : meghdarFasl * BahayeVahedNew1));

                mainRow.click();

                ///////////////
                ///////////////

                //refreshBaseItemNewBV(element, info[2], info[3], info[1], itemFbShomareh, BahayeVahedNew);
                Code = itemFbShomareh.substring(0, 2);
                refreshBaseItem(element, itemFbShomareh, Code);
                GetJamBarAvordAll();
                GetJamFosoulAll();
                //////////////////
                //////////////////

                // پیدا کردن input مربوط به مقدار واحد درون همین ردیف
                //const targetInput = mainRow.querySelector('input[type="text"].form-control');
                //if (targetInput) {
                //    targetInput.value = formatNumber(info[1]);
                //    if (bahayeKol == 0) {
                //        targetInput.classList.add('blinking');
                //    }
                //    else
                //        targetInput.classList.remove('blinking');
                //}

                toastr.success('بهای واحد جدید بدرستی درج گردید', 'موفقیت');
            }
            else
                toastr.info('مشکل در درج اطلاعات', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در درج اطلاعات', 'خطا');
        }
    });
}


function StarReturn(element, FBId, itemFbShomareh) {

    FBId = FBId == 'null' ? '' : FBId;
    Year = parseInt($('#HDFYear').val());
    BarAvordId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());

    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.BarAvordId = BarAvordId;
    vardata.NoeFB = NoeFB;
    vardata.itemFbShomareh = itemFbShomareh;
    vardata.Year = Year;
    $.ajax({
        type: "POST",
        url: '/BaravordUser/RemoveStarValueInFB',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var info = data.split('_');
            if (info[0] == "OK") {

                const mainRow = element.closest('tr');
                if (!mainRow) return;

                txtmeghdarFasl = mainRow.querySelector('#meghdarFasl_' + itemFbShomareh);

                meghdarFasl = parseFloat(convertPersianToEnglish(txtmeghdarFasl.innerHTML.replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                bahayeKol = parseFloat(info[1]);

                if (isNaN(bahayeKol)) {
                    bahayeKol = 0;
                }
                bahayeKolFasl = mainRow.querySelector('#bahayeKolFasl_' + itemFbShomareh);
                bahayeKolFasl.innerHTML = toPersianDigits(formatNumber(meghdarFasl * bahayeKol));
                // پیدا کردن input مربوط به مقدار واحد درون همین ردیف
                const targetInput = mainRow.querySelector('input[type="text"].form-control');
                if (targetInput) {
                    targetInput.value = formatNumber(info[1]);
                    if (bahayeKol == 0) {
                        targetInput.classList.add('blinking');
                    }
                    else
                        targetInput.classList.remove('blinking');
                }

                const row = element.closest('.row');
                if (row) {
                    row.remove();
                }

                Code = itemFbShomareh.substring(0, 2);

                GetJamBarAvordAll();
                GetJamFosoulAll();
                refreshBaseItem(element, itemFbShomareh, Code);
                //refreshBaseItemNewBV(element, info[3], info[4], Code, itemFbShomareh, info[1]);

                toastr.success('بهای واحد بازنشانی گردید', 'موفقیت');
            }
            else
                toastr.info('مشکل در درج اطلاعات', 'اطلاع');
        },
        error: function (msg) {
            toastr.error('مشکل در درج اطلاعات', 'خطا');
        }
    });
}

function ShowSelectTajmie(ItemFBShomareh) {

    // افزونه درخت فقط هنگام درخواست انتخاب آیتم تجمیعی لازم است.
    ensureShowBarAvordAsset("tree").then(function () {
        openShowSelectTajmieTree(ItemFBShomareh);
    }).catch(function (error) {
        console.error(error);
        toastr.error('بارگذاری درخت انتخاب آیتم تجمیعی با خطا مواجه شد', 'خطا');
    });
}

function openShowSelectTajmieTree(ItemFBShomareh) {

    noeFB = parseInt($('#HDFNoeFB').val());

    $.ajax({
        url: '/TajmieFosoul/GetTajmieTree',
        type: 'GET',
        data: { noeFB: noeFB },
        success: function (result) {

            if ($('#treeItems').jstree(true)) {
                $('#treeItems').jstree("destroy");
            }

            $('#treeItems').jstree({
                core: {
                    data: result,
                    themes: {
                        responsive: true
                    }
                },
                plugins: ["wholerow"]
            });

            // حذف Event قبلی
            $('#treeItems').off('select_node.jstree');

            // ثبت Event جدید
            $('#treeItems').on('select_node.jstree', function (e, data) {

                if (data.node.parent == "#")
                    return;

                var itemCode = data.node.data.itemCode;

                $("#txtTajmieItem-" + ItemFBShomareh).val(itemCode);

                $("#PopupShowSelectTajmie").modal("hide");
            });

            $("#ModalTitle").text("انتخاب آیتم تجمیعی");
            $("#PopupShowSelectTajmie").modal("show");
        }
    });
}
