var lstVaheds;
function GetFosoul() {
    debugger;
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

            debugger;
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
    debugger;
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
    debugger;
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
    debugger;
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

            debugger;

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
            debugger;
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
    debugger;
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

                debugger;
                GetBarAvord(item.id, item.code, item.faslName, faslAsli);
            }
        });

        $('#chkNonZeroMeghdarFasl').off('change').on('change', function () {

            isOneRowFilled = false;
            debugger;
            const showOnlyNonZero = $(this).is(':checked');
            // همه‌ی ردیف‌های اصلی (نه ردیف دوم مربوط به barAvordContainer)
            $('#fosoulTable tbody tr').each(function () {

                debugger;

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

                    debugger;

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
    debugger;

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
    debugger;
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

    debugger;
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
    $.ajax({
        type: "post",
        url: "/ShowBarAvordUser/GetUserBarAvord",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            userBarAvordOutPut = response.userBarAvordOutPut;
            lstItemFBStars = response.lstItemFBStars;
            //$('#aView').click();
            //$('#ModalTitle').text('فصل  ' + Code +'  -  '+faslName);
            renderTable(userBarAvordOutPut, lstItemFBStars, Code, ItemId, faslAsli);
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
    debugger;

    $input.value = formatNumber(floatValEn.toFixed(0));

    strStar1 = `<div class="row"><span style="font-size:25px">*</span><input style="font-size:20px" class="ReturnItemsStar" type="submit" value="🔄" onclick="StarReturn(this,'${FBId}','${itemFbShomareh}')"/></div>`

    $('#span' + itemFbShomareh).html(strStar1);
    BahayeVahedNewSave($input, floatValEn, FBId, itemFbShomareh);
}

function ItemZaribChange($input, FBId, itemFbShomareh) {

    debugger;
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
    debugger;

    $input.value = formatNumber(floatValEn.toFixed(3));

    ItemZaribSave($input, floatValEn, FBId, itemFbShomareh);
}

function ItemZaribStarChange($input, ItemStarId, itemFbShomareh) {

    debugger;
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
    debugger;
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
    debugger;

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
                debugger;
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
                debugger;
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

function renderTable(data, Stars, Code, ItemId, faslAsli) {

    const container = $(`#barAvordContainer-${ItemId}`);
    container.empty();

    const table = $(`
		<table class="table table-item table-bordered fixed-table mb-2">
			<thead>
				<tr style="background-color: #aa87ff;">
					<th style="width: 5🔄%;text-align:center;color: #3a007a;font-size: 12px;">*</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">شماره فهرست بها</th>
					<th style="width: 46%;color: #3a007a;font-size: 16px;">شرح فهرست بها</th>
					<th style="width: 8%;text-align:center;color: #3a007a;font-size: 12px;">واحد</th>
					<th style="width: 7%;text-align:center;color: #3a007a;font-size: 12px;">بهای واحد</th>
					<th style="width: 10%;text-align:center;color: #3a007a;font-size: 12px;">
                    <label class="ShowNoneZeroClass" style="font-size: 9px;margin-bottom: 0px;">
                    <input type="checkbox" id="chkNonZeroMeghdar-${Code}" style="margin-right: 5px;"/>
                      <span style="font-size:12px">
                            مقدار < > 0 
                      </span>      
                    </label>
                    </th>
                    <th style="width: 4%;text-align:center;color: #3a007a;font-size: 12px;">ضریب</th>
					<th style="width: 10%;text-align:center;color: #3a007a;font-size: 12px;">بهای کل</th>
					<th style="width: 3%;text-align:center;color: #3a007a;font-size: 12px;">عملیات</th>
				</tr>
			</thead>
			<tbody class="tbodyFosoul"></tbody>
		</table>
	`);

    const tbody = table.find('tbody');

    var index = 0;
    data.forEach((item) => {

        debugger;
        var Field0 = item.itemsFields[0] == undefined ? "" : item.itemsFields[0].vahed;
        var Field1 = item.itemsFields[1] == undefined ? "" : item.itemsFields[1].vahed;
        var Field2 = item.itemsFields[2] == undefined ? "" : item.itemsFields[2].vahed;
        var Field3 = item.itemsFields[3] == undefined ? "" : item.itemsFields[3].vahed;
        var Field4 = item.itemsFields[4] == undefined ? "" : item.itemsFields[4].vahed;
        var Field5 = item.itemsFields[5] == undefined ? "" : item.itemsFields[5].vahed;

        const rizId = `riz-${ItemId}-${index}`;
        const clickableClass = 'clickable-row table-active';
        const BahayeKol = parseFloat(item.bahayeKol);
        const BahayeKolFix = BahayeKol.toFixed(0);
        BahayeVahedNew = item.bahayeVahedNew;
        bahayeVahedSetEn = item.bahayeVahed;
        ItemZaribSetEn = item.itemZarib;
        strStar = '';
        if (BahayeVahedNew != 0) {
            bahayeVahedSetEn = BahayeVahedNew;
            strStar += `<div class="col-12" id="span${item.itemFbShomareh}">
                        <div class="row"><span style="font-size:25px">*</span><input  style="font-size:20px" class="ReturnItemsStar" type="submit" value="🔄" title="بازنشانی"
                        onclick="StarReturn(this,'${item.fbId}','${item.itemFbShomareh}')"/></div>
                        </div>`;
        }
        else
            strStar += `<div class="col-12" id="span${item.itemFbShomareh}"></div>`;

        bahayeVahedSet = toPersianDigits(formatNumber(bahayeVahedSetEn));
        ItemZaribSet = toPersianDigits(formatNumber(ItemZaribSetEn));

        const isBlinking = item.meghdar > 0 && (!bahayeVahedSet || parseFloat(bahayeVahedSetEn) === 0);
        const bahayeVahedInput = `
            <input id="txtBahayeVahed-${rizId}" onchange="BahayeVahedNewMeghdarChange(this,'${item.fbId}','${item.itemFbShomareh}')" type="text" class="form-control form-control-sm ${isBlinking ? 'blinking' : ''}" 
                value="${bahayeVahedSet ?? ''}" 
                style="text-align:center;border:unset" />
            `;

        const ItemZaribInput = `
            <input id="txtItemZarib-${rizId}" onchange="ItemZaribChange(this,'${item.fbId}','${item.itemFbShomareh}')" type="text" class="form-control form-control-sm"
                value="${ItemZaribSet ?? ''}" 
                style="text-align:center;border:unset" />
            `;

        var stritemFbShomareh = '';
        if (faslAsli) {
            stritemFbShomareh = `
            <span>
                ${toPersianDigits(item.itemFbShomareh)}
            </span>
            `;
        }
        else {
            stritemFbShomareh = `
            <span>
                ${toPersianDigits(item.itemFbShomarehMain)}
            </span>
            `;
        }

        const mainRow = $(`
        <tr id="rowFasl-${rizId}" class="main-row ${clickableClass}" data-riz-id="${rizId}" style="cursor: pointer;">
        <td style="text-align:center;width:5%">${strStar}</td>
        <td style="text-align:center;width:7%">  ${stritemFbShomareh}</td>
        <td style="word-break: break-word; white-space: normal;text-align:right;width:46%">${toPersianDigits(item.sharh)}</td>
        <td style="text-align:center;width:8%">${item.vahed}</td>
        <td style="text-align:center;width:7%;padding:0px">${bahayeVahedInput}</td>
        <td style="text-align:center;width:10%"><span id="meghdarFasl_${item.itemFbShomareh}">${formatNumber(item.meghdar)}</span></td>
        <td style="text-align:center;width:4%;padding:0px">${ItemZaribInput}</td>
        <td style="text-align:center;width:10%"><span id="bahayeKolFasl_${item.itemFbShomareh}">${formatNumber(BahayeKolFix)}</span></td>
        <td style="text-align:center;display:none"><span id="bahayeVahedFasl_${item.bahayeVahed}">${item.bahayeVahed}</span></td>
        <td></td>
        </tr>
        `);


        let rizRowsHtml = '';


        if (Array.isArray(item.rizMetre) && item.rizMetre.length > 0) {
            rizRowsHtml += item.rizMetre.map(riz =>
                `
		<tr class="riz-row-data" data-id="${riz.id}" data-itemsFields='${JSON.stringify(item.itemsFields).replace(/"/g, '&quot;')}'
                onclick="EditRizMetreRow(this,'${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')" style="cursor:pointer;">
		<td style="text-align:center;width:4%">${riz.shomarehNew == null ? '' : toPersianDigits(riz.shomarehNew)}</td>
        <td style="text-align:center;width:6%"><span>${toPersianDigits(riz.tajmieItem)}</span></td>
		<td style="width:22%"><span>${toPersianDigits(riz.sharh)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tedad == null ? '' : toPersianDigits(riz.tedad)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tool == null ? '' : toPersianDigits(riz.tool)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.arz == null ? '' : toPersianDigits(riz.arz)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.ertefa == null ? '' : toPersianDigits(riz.ertefa)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.vazn == null ? '' : toPersianDigits(riz.vazn)}</span></td>
		<td style="text-align:center;width:7%"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : toPersianDigits(riz.meghdarJoz)}</span></td>
		<td style="width:20%"><span>${toPersianDigits(riz.des ?? '')}</span></td>
		<td style="text-align:center;width:7%">
			<i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetre(this,'${riz.id}','${item.itemFbShomareh}','${rizId}','${item.fbId}','${Code}')"></i>
		</td>
		</tr>
		`).join('');
        }

        debugger;


        field = item.itemsFields;
        const defaultField = { id: null, itemShomareh: item.itemShomareh || '', fieldType: null, vahed: '', isEnteringValue: false, essentialValue: false };

        const getField = (i) => (i >= 0 && i < field.length ? field[i] : defaultField);

        const f0 = getField(0);
        const f1 = getField(1);
        const f2 = getField(2);
        const f3 = getField(3);
        const f4 = getField(4);

        rizRowsHtml += `
<tr class="riz-input-row">
  <td style="text-align:center;width:4%"></td>
  <td style="text-align:center;width:6%">

  <div class="d-flex align-items-center gap-2">
            <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="ShowSelectTajmie('${item.itemFbShomareh}-New')"></i>
        
            <input id="txtTajmieItem-${item.itemFbShomareh}-New" class="form-control form-control-sm" type="text" onchange="TajmieItemChange(this)" />
       
    </div>
  </td>
  <td style="width:22%">
    <input id="txtSharh" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTedad" class="${getInputClass(f0)}" type="text" ${getDisabledAttr(f0)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTool" class="${getInputClass(f1)}" type="text" ${getDisabledAttr(f1)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtArz" class="${getInputClass(f2)}" type="text" ${getDisabledAttr(f2)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtErtefa" class="${getInputClass(f3)}" type="text" ${getDisabledAttr(f3)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtVazn" class="${getInputClass(f4)}" type="text" ${getDisabledAttr(f4)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td style="width:20%">
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <button type="button"
      onclick="SaveRMUClick($(this),'${item.itemFbShomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
  </td>
</tr>
`;

        // ردیف ورودی همیشه اضافه شود
        //      rizRowsHtml += `
        //	<tr class="riz-input-row">
        //		<td style="text-align:center"></td>
        //		<td><input id="txtSharh" class="form-control form-control-sm" type="text"/></td>
        //		<td style="text-align:center"><input id="txtTedad"/></td>
        //		<td style="text-align:center"><input id="txtTool" /></td>
        //		<td style="text-align:center"><input id="txtArz"  /></td>
        //		<td style="text-align:center"><input id="txtErtefa" /></td>
        //		<td style="text-align:center"><input id="txtVazn" class="form-control /></td>
        //		<td style="text-align:center"><input id="txtMeghdarJoz" disabled class="form-control form-control-sm" type="text" /></td>
        //		<td>
        //			<input id="txtDes" class="form-control form-control-sm" type="text"/>
        //		</td>
        //		<td style="text-align:center">
        //			<button type=\"button\" onclick=\"SaveRMUClick($(this),'${item.itemFbShomareh}','${rizId}','${Code}')\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button>
        //		</td>
        //	</tr>
        //`;


        const detailRow = $(`
			<tr id="${rizId}" class="riz-row" style="display: none;">
				<td colspan="8">
					<table class="table table-sm table-bordered mb-0">
						<thead style="width:100%">
							<tr style="background-color: #c1b9e7;">
								<th style="width: 4%;text-align: center;vertical-align: top;">ردیف</th>
								<th style="width: 6%;text-align: center;vertical-align: top;">فعالیت</th>
								<th style="width: 22%;text-align: center;vertical-align: top;">شرح ریزه متره</th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                   <span>تعداد</span>   
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field0}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                    <span>
                                    طول
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field1}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                    <span>
                                    عرض
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field2}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                     <span>
                                     ارتفاع
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field3}
                                    </span>
                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">                               
                                    <span>
                                    وزن یا...
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field4}
                                    </span>

                                </th>
								<th style="width: 7%;text-align: center;vertical-align: top;">
                                
                                <span>
                                مقدار جزء
                                    </span>
                                    <hr/>
                                    <span style="font-size:10px">
                                       ${Field5}
                                    </span>

                                </th>
								<th style="width: 20%;text-align: center;vertical-align: top;">توضیحات</th>
								<th style="width: 7%;text-align: center;vertical-align: top;">عملیات</th>
							</tr>
						</thead>
						<tbody>
							${rizRowsHtml}
						</tbody>
					</table>
				</td>
			</tr>
		`);

        tbody.append(mainRow);

        tbody.append(detailRow);

        index++;
    });


    Stars.forEach((item) => {
        debugger;

        currentId = item.id;

        const rizId = `riz-${currentId}-${index}`;

        const ItemZaribStarSet = item.itemZaribStar;

        const ItemZaribStarInput = `
            <input id="txtItemZaribStar-${rizId}" onchange="ItemZaribStarChange(this,'${currentId}','${item.shomareh}')" type="text" class="form-control form-control-sm"
                value="${ItemZaribStarSet ?? ''}" 
                style="text-align:center;border:unset" />
            `;

        KharidTajhizat = item.blnKharidTajhizat == null ? false : item.blnKharidTajhizat;
        strKharidTajhizat = KharidTajhizat == true ? "ت" : "";
        checked = KharidTajhizat == true ? "checked" : "";
        const mainRowStar = $(`
        <tr id="rowFasl-${rizId}" class="main-row clickable-row" data-riz-id="${rizId}" style="cursor: pointer;">
        <td style="text-align:center;width:5%">
        <span>
            ${strKharidTajhizat}
        </span>
        <span style="font-size:20px">
            * 
        </span>
        </td>
        <td style="text-align:center;width:7%">${toPersianDigits(item.shomareh)}</td>
        <td style="word-break: break-word; white-space: normal;text-align:right;width:46%">
            <div class="row">
             <div class="col-10">
                ${item.sharh}
             </div>
             <div class="col-2" style="border-right:1px solid #ccc;background-color:#f3e0ff;">
                <input id="KharidTajhizat_${currentId}_${item.shomareh}" type="checkbox" ${checked} />
                <lable>خرید تجهیزات</label>
             </div>
            </div>
        </td>
        <td style="text-align:center;width:8%">${item.vahedName}</td>
        <td style="text-align:center;width:7%" id="bahayeVahedFaslStar_${item.shomareh}">${toPersianDigits(item.bahayeVahed)}</td>
        <td style="text-align:center;width:10%" id="meghdarFaslStar_${item.shomareh}">${toPersianDigits(formatNumber(item.meghdar))}</td>
        <td style="width:4%">${ItemZaribStarInput}</td>
        <td style="text-align:center;width:10%" id="bahayeKolFaslStar_${item.shomareh}">${formatNumber(item.bahayeKol)}</td>
        <td>
            <i id="iDelete_${currentId}_${item.shomareh}" onclick="event.stopPropagation();DeleteItemFB(this,'${item.shomareh}','${Code}',${ItemId})" class="fa fa-trash DelRMUStyle" ></i>
        </td>
        </tr>
        `);

        let rizRowsHtmlStar = '';


        if (Array.isArray(item.rizMetre) && item.rizMetre.length > 0) {
            rizRowsHtmlStar += item.rizMetre.map(riz =>
                `
		<tr class="riz-row-data" data-id="${riz.id}" data-itemsFields='${JSON.stringify(item.itemsFields).replace(/"/g, '&quot;')}'
        onclick="EditRizMetreRowStar(this,'${riz.id}','${item.shomareh}','${rizId}','${Code}')" style="cursor:pointer;">
		<td style="text-align:center;width:4%">${riz.shomarehNew == null ? '' : toPersianDigits(riz.shomarehNew)}</td>
		<td style="text-align:center;width:6%">
            <span>${riz.tajmieItem == null ? '' : toPersianDigits(riz.tajmieItem)}</span>
        </td>
		<td style="width:22%"><span>${toPersianDigits(riz.sharh)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tedad == null ? '' : toPersianDigits(riz.tedad)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.tool == null ? '' : toPersianDigits(riz.tool)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.arz == null ? '' : toPersianDigits(riz.arz)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.ertefa == null ? '' : toPersianDigits(riz.ertefa)}</span></td>
		<td style="text-align:center;width:7%"><span>${riz.vazn == null ? '' : toPersianDigits(riz.vazn)}</span></td>
		<td style="text-align:center;width:7%"><span id="spanMeghdarJozStar">${riz.meghdarJoz == null ? '' : toPersianDigits(riz.meghdarJoz)}</span></td>
		<td style="width:20%"><span>${toPersianDigits(riz.des ?? '')}</span></td>
		<td style="text-align:center;width:7%">
			<i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetreStar($(this),'${riz.id}','${item.shomareh}','${rizId}','${Code}')"></i>
		</td>
		</tr>
		`).join('');
        }

        rizRowsHtmlStar += `
<tr class="riz-input-row">
  <td style="text-align:center;width:4%"></td>
  <td style="text-align:center;width:6%">

   <div class="d-flex align-items-center gap-2">
            <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="ShowSelectTajmie('Star-${Code}-New')"></i>
        
            <input id="txtTajmieItem-Star-${Code}-New" class="form-control form-control-sm" type="text" onchange="TajmieItemChange(this)" />
       
    </div>
  </td>
  <td style="width:22%">
    <input id="txtSharh-${Code}" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTedad-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTool-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtArz-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtErtefa-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtVazn-${Code}" class="form-control form-control-sm" type="text"/>
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtMeghdarJoz-${Code}" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td style="width:20%">
    <input id="txtDes-${Code}" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <button type="button"
      onclick="SaveRMUStarClick($(this),'${item.shomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
  </td>
</tr>
`;

        const detailRowStar = $(`
			<tr id="${rizId}" class="riz-row" style="display:none">
				<td colspan="8">
					<table class="table table-sm table-bordered mb-0">
						<thead>
							<tr style="background-color: #c1b9e7;">
								<th style="width: 4%;text-align:center">ردیف</th>
								<th style="width: 6%;text-align:center">فعالیت</th>
								<th style="width: 22%;">شرح ریزه متره</th>
								<th style="width: 7%;text-align:center">تعداد</th>
								<th style="width: 7%;text-align:center">طول</th>
								<th style="width: 7%;text-align:center">عرض</th>
								<th style="width: 7%;text-align:center">ارتفاع</th>
								<th style="width: 7%;text-align:center">وزن یا...</th>
								<th style="width: 7%;text-align:center">مقدار جزء</th>
								<th style="width: 20%;">توضیحات</th>
								<th style="width: 6%;text-align:center">عملیات</th>
							</tr>
						</thead>
						<tbody>
							${rizRowsHtmlStar}
						</tbody>
					</table>
				</td>
			</tr>
		`);

        tbody.append(mainRowStar);
        tbody.append(detailRowStar);

        index++;

    });

    var RowNew = '';

    debugger;
    if (faslAsli) {

        RowNew = $(`
        <tr id="rowFaslNew-${ItemId}" class="main-row" data-riz-code="${Code}" style="cursor: pointer;background-color: #cbedcb;">
        <td style="text-align:center;width:5%"><div class=\"col-md-1\"><i class=\"fa fa-plus SaveRMUStyle\"></i></div></td>
        <td style="text-align:center;width:7%"><input id="inputFBShomareh-${ItemId}" type="text" class="form-control"/></td>
        <td style="text-align:center;width:46%">
           <div class="row">
            <div class="col-10"><input id="inputSharh-${ItemId}" type="text" class="form-control"/></div>
            <div class="col-2"><input id="ckKharidTajhizat-${ItemId}" type="checkbox" /><label>خرید تجهیزات</label></div>
           </div>
        </td>
        <td style="text-align:center;width:8%"><select style="width: 100%;" id="selectVahed-${ItemId}"></select></td>
        <td style="text-align:center;width:7%"><input id="inputBahayeVahed-${ItemId}" type="text" class="form-control"/></td>
        <td style="text-align:center;width:10%" id="meghdarFasl-${ItemId}"></td>
        <td style="width:4%">
            <input id="txtItemZaribStar-${ItemId}" type="text" class="form-control form-control-sm"
                value="" style="text-align:center;border:unset" />
        </td>
        <td style="width:10%"></td>
        <td><button type=\"button\" onclick=\"SaveNewRMUClick($(this))\" class=\"ButtonRowsSaveStyle\"><i id=\"iSave\" class=\"fa fa-save SaveRMUStyle\"></i></button></td>
        </tr>
        `);
        var strOption = '';
        $.each(lstVaheds, function () {
            strOption += `<option value="${this.id}">${this.name}</option>`;
        });

        RowNew.find('select').append(strOption)

        tbody.append(RowNew);
    }

    container.append(table);

    container.on('keydown', '.riz-row-data input,.riz-input-row input', function (e) {

        if (e.key === 'Enter') {
            e.preventDefault();

            const $row = $(this).closest('tr');
            const $elements = $row.find('input, button'); // همه‌ی inputها و دکمه Save در همان ردیف
            let idx = $elements.index(this);

            // حرکت به المنت بعدی که disabled نیست
            let next = null;
            for (let i = idx + 1; i < $elements.length; i++) {
                if (!$elements.eq(i).is(':disabled')) {
                    next = $elements.eq(i);
                    break;
                }
            }

            if (next && next.length) {
                next.focus().select();
            } else {
                // اگر به آخر رسید، برو روی دکمه Save
                const $saveBtn = $row.find('button');
                if ($saveBtn.length) {
                    $saveBtn.focus().select();
                    // اگر می‌خوای آخرین Enter دکمه Save رو هم کلیک کنه:
                    // $saveBtn.click();
                }
            }
        }
    });


    $(`#${ItemId}`).slideDown(200);

    $('.clickable-row').off('click').on('click', function (e) {

        e.stopPropagation();

        if ($(e.target).is('input, button, textarea')) {
            return;
        }

        const rizId = $(this).data('riz-id');
        const rizRow = $(`#${rizId}`);



        // بسته کردن بقیه
        $('.riz-row').not(rizRow).slideUp(200);

        // باز یا بسته کردن این یکی
        rizRow.slideToggle(200);

        txtSharh = rizRow.find('#txtSharh');
        txtSharh.focus();

        setTimeout(() => {
            const container = rizRow.closest('[id^="barAvordContainer-"]');

            const containerTop = container.scrollTop();
            const containerHeight = container.height();
            const containerBottom = containerTop + containerHeight;

            const rowTop = rizRow.position().top + containerTop;
            const rowBottom = rowTop + rizRow.outerHeight(true);

            // اگر پایین ردیف دیده نمی‌شود → اسکرول به اندازه لازم
            if (rowBottom > containerBottom) {
                container.stop().animate({
                    scrollTop: containerTop + (rowBottom - containerBottom) + 20
                }, 300);
            }

            // اگر بالای ردیف رفته بالا (حالت نادر)
            else if (rowTop < containerTop) {
                container.stop().animate({
                    scrollTop: rowTop - 20
                }, 300);
            }

        }, 0);


    });
    ///
    $('tr[id^="riz-"]').off('click').on('click', function (e) {
        e.stopPropagation();
    });
    // افزودن ریزمتره
    $('.btn-add-riz').off('click').on('click', function (e) {
        e.stopPropagation();
        const inputs = $(this).closest('tr').find('input');
        const values = inputs.map((_, input) => $(input).val()).get();

        const newRow = $(`
			<tr class="riz-row-data">
				<td>${values[0]}</td>
				<td>${values[1]}</td>
				<td>${values[2]}</td>
				<td>${values[3]}</td>
				<td>${values[4]}</td>
				<td>${values[5]}</td>
				<td>${values[6]}</td>
				<td>${values[7]}</td>
				<td>${values[8] ?? ''} <button class="btn btn-sm btn-danger btn-delete-riz float-end">🗑️</button></td>
			</tr>
		`);

        $(this).closest('tbody').find('.riz-input-row').before(newRow);
        inputs.val('');
    });

    //convertNumbersInPage();

    $('[id^="chkNonZeroMeghdar-"]').off('change').on('change', function () {

        const showOnlyNonZero = $(this).is(':checked');
            isOneRowFilledRizMetre = false;
        $(`#barAvordContainer-${ItemId} .main-row`).each(function () {

            const meghdarText = $(this).find('[id^="meghdarFasl_"], [id^="meghdarFaslStar_"]').text().replace(/,/g, '');
            const EnNum = meghdarText != "" ? convertPersianToEnglish(meghdarText) : "0";
            const meghdar = parseFloat(EnNum);

            if (showOnlyNonZero) {
                if (meghdar === 0) {
                    $(this).hide();
                    $(`#${$(this).data('riz-id')}`).hide(); // مخفی‌سازی ردیف ریز مربوطه
                } else {
                    $(this).show();
                    isOneRowFilledRizMetre = true;
                }
            } else {
                $(this).show();
            }
        });

        if (!isOneRowFilledRizMetre) {
            $(`#barAvordContainer-${ItemId} .main-row`).each(function () {

                isOneRowFilledRizMetre = false;
                const meghdarText = $(this).find('[id^="meghdarFasl_"], [id^="meghdarFaslStar_"]').text().replace(/,/g, '');
                const EnNum = meghdarText != "" ? convertPersianToEnglish(meghdarText) : "0";
                const meghdar = parseFloat(EnNum);
                if (showOnlyNonZero) {
                   $(this).show();
                } else {
                    $(this).show();
                }
            });
        }

        $('#rowFaslNew-' + ItemId).show();
    });
    //function KharidTajhizatChange(Id, blnKharidTajhizat, code)


    $('[id^="KharidTajhizat_"]').off('change').on('change', function () {
        thisId = this.id.split('_');
        Shomareh = thisId[2];
        Year = parseInt($('#HDFYear').val());

        var vardata = new Object();
        vardata.Id = thisId[1];
        vardata.KharidTajhizat = this.checked;
        vardata.Year = Year;

        $.ajax({
            type: "POST",
            url: '/ItemFBStar/UpdateItemFBStar',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                info = data.split('_');
                if (info[0] == "OK") {

                    //const row = $(this).closest('tr');
                    //const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                    //const tdJameFasl = $('#tdjameFasl-' + Code);
                    //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                    //const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                    //const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                    //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                    //const MeghdarJoz = row.find('#spanMeghdarJozStar');

                    //var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                    //var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
                    //var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                    //let num = parseFloat(info[1]);

                    ////let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                    ////let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                    //meghdarFasl.html(formatNumber(info[1]));
                    //bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * info[1]));
                    //MeghdarJoz.html(num);
                    //tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                    //tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));


                    //JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAll = parseFloat(info[2]) + isNaN(JameFasl) ? 0 : JameFasl;
                    //tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                    //JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                    //dJameFaslAllBaZarib = parseFloat(info[3]) + isNaN(JameFaslBaZarib) ? 0 : JameFaslBaZarib;
                    //tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));


                    let num = parseFloat(info[1]);
                    bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);

                    var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                    meghdarFasl.html(formatNumber(parseFloat(info[1]).toFixed(0)));
                    bahayeKolFasl.html(formatNumber(parseFloat(info[2]).toFixed(0)));

                    Code = Shomareh.substring(0, 2);
                    refreshBaseItem(this, Shomareh, Code);

                    toastr.success('اطلاعات با موفقیت ویرایش شد.', 'موفق');

                    //GetBarAvord(thisId[1], Code, "", true);

                    GetJamBarAvordAll();
                    GetJamFosoulAll();
                }
            },
            error: function (msg) {
                toastr.error('مشکل در درج اطلاعات', 'خطا');
            }
        });
    });

    initRowEvents(RowNew);

    $(`#chkNonZeroMeghdar-${Code}`).click();

}

function DeleteItemFB(obj, Shomareh, Code, ItemId) {

    Year = parseInt($('#HDFYear').val());

    thisId = obj.id.split('_');
    var vardata = new Object();
    vardata.Id = thisId[1];
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: '/ItemFBStar/DeleteItemFBStar',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            info = data.split('_');
            if (info[0] == "OK") {

                //GetBarAvord(ItemId, Code, "");

                $(obj).parent().parent().remove();
                closestTr = $(obj).closest('tr');
                dataId = closestTr.attr('data-riz-id');

                $('#' + dataId).slideUp(500);
                //Shomareh = thisId[2];

                //var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                //var bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);
                //var bahayeVahedFasl = $('#bahayeVahedFaslStar_' + Shomareh);

                //const tdJameFasl = $('#tdjameFasl-' + Code);
                //const tdJameFaslBaZarib = $('#tdJameFaslBaZarib-' + Code);
                //const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                //const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);
                //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * parseFloat(info[1])));

                //meghdarFasl.html(formatNumber(info[1]));
                //bahayeKolFasl.html(formatNumber(txtBahayeVahed1 * parseFloat(info[1])));
                ////MeghdarJoz.html(num);
                //tdJameFaslStar.html(formatNumber(parseFloat(info[2]).toFixed(0)));
                //tdJameFaslStarBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));

                //JameFasl = parseFloat(convertPersianToEnglish(tdJameFasl.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAll = parseFloat(info[2]) + JameFasl;
                //tdJameFaslAll.html(dJameFaslAll.toFixed(0));

                //JameFaslBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAllBaZarib = parseFloat(info[3]) + JameFaslBaZarib;
                //tdJameFaslAllBaZarib.html(dJameFaslAllBaZarib.toFixed(0));


                //GetCurrentRizMetreItemFBStar(Shomareh, thisId[1], Code);

                //let num = parseFloat(info[1]);
                //bahayeKolFasl = $('#bahayeKolFaslStar_' + Shomareh);

                //var meghdarFasl = $('#meghdarFaslStar_' + Shomareh);
                //meghdarFasl.html(parseFloat(info[1]).toFixed(0));
                //bahayeKolFasl.html(formatNumber(parseFloat(info[2]).toFixed(0)));

                //Code = Shomareh.substring(0, 2);
                refreshBaseItem(obj, Shomareh, Code);

                GetJamBarAvordAll();
                GetJamFosoulAll();
                toastr.success('اطلاعات با موفقیت حذف شد.', 'موفق');

            }
        },
        error: function (msg) {
            toastr.error('مشکل در درج اطلاعات', 'خطا');
        }
    });
}

function initRowEvents($row) {
    // ترتیب فوکوس
    var $focusables = $row.find('input, select, button.ButtonRowsSaveStyle')
        .filter(':visible:enabled');

    $focusables.on('keydown', function (e) {

        // فقط روی Enter
        if (e.key !== 'Enter' && e.which !== 13) return;

        // اگر دکمه Save هست → اجازه بده Enter رفتار پیشفرض داشته باشد (عمل Save انجام شود)
        if ($(this).is('button.ButtonRowsSaveStyle')) {
            return; // هیچ preventDefault نکن → خودش submit می‌شود
        }

        // بقیه موارد: رفتار اینتر را override کنیم
        e.preventDefault();
        var idx = $focusables.index(this);

        // برو به بعدی
        if (idx > -1 && idx < $focusables.length - 1) {
            $focusables.eq(idx + 1).focus();
        } else {
            // آخرین ورودی → بایست روی دکمه save
            $focusables.eq($focusables.length - 1).focus();
        }
    });
}



const getInputClass = (f) =>
    `form-control form-control-sm ${f.isEnteringValue && f.essentialValue ? 'essentialValue' : ''}`;

const getDisabledAttr = (f) => f.isEnteringValue ? '' : 'disabled';


function EditRizMetreRow(rowEl, rizMetreId, Shomareh, rizId, FBId, Code) {

    debugger;
    const itemsFieldsStr = rowEl.getAttribute('data-itemsFields');
    const itemsFields = JSON.parse(itemsFieldsStr);

    const $row = $(rowEl);
    if ($row.hasClass('editing')) {
        UpdateRizMetreFromRow($row, rizMetreId, Shomareh, rizId, FBId, Code);
        return;
    }
    //if ($row.hasClass('editing')) return;

    // بستن سایر ردیف‌های در حال ویرایش
    $('.riz-row-data.editing').each(function () {
        UpdateRizMetreFromRow($(this), rizMetreId, Shomareh, rizId, FBId, Code);

        return;
        //cancelEditRow($(this), rizMetreId, Shomareh, rizId, FBId);
    });

    $row.addClass('editing');

    $row.find('td').each(function (index) {
        debugger;

        const $td = $(this);
        const text = $td.find('span').html();

        if (index === 0 || index === 8) return;

        if (index === 1) {

            let inputHtml =
            `<div class="d-flex align-items-center gap-2">
                <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="event.stopPropagation();ShowSelectTajmie('${Shomareh}-${rizMetreId}')"></i>

                <input id="txtTajmieItem-${Shomareh}-${rizMetreId}" class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}">

            </div>`;
            $td.html(inputHtml);

        }

        else if (index === 3 || index === 4 || index === 5 || index === 6 || index === 7) {
            const field = itemsFields.find(f => f.fieldType === (index - 2));

            // ساخت input با تنظیمات پیش‌فرض
            let inputHtml = `<input class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}"`;

            if (field) {
                // اگر isEnteringValue=false بود، input غیرفعال شود
                if (field.isEnteringValue === false) {
                    inputHtml += ' disabled';
                }
                else {
                    if (field.essentialValue) {
                        inputHtml = inputHtml.replace('class="', 'class="essentialValue ');
                    }
                }
            }

            inputHtml += '>'; // بستن تگ input
            $td.html(inputHtml);

        }
        else {
            let inputHtml = `<input class="form-control form-control-sm" type="text" onclick="event.stopPropagation()" value="${text}" />`;
            $td.html(inputHtml);
        }
    });

    // جایگزینی آیکن عملیات با دکمه ذخیره
    const $actionCell = $row.find('td').eq(10);
    $actionCell.html(`
    <button type="button"
      onclick="event.stopPropagation();UpdateRizMetreFromRow($(this),'${rizMetreId}','${Shomareh}','${rizId}','${FBId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
	`);

    $row.find('td').eq(2).find('input').focus().select();
}


function cancelEditRow(row, rizMetreId, Shomareh, rizId1, FBId) {
    //row.removeClass('editing');
    row.find('td').each(function (index) {

        debugger;
        if (index === 0 || index === row.children('td').length - 1) {
            return;
        }

        else if (index === 2 || index === 9) {
            const input = $(this).find('input');
            if (input.length > 0) {
                const value = input.val();
                input.replaceWith(`<span>${value}</span>`);
            }
        }
        else {

            const input = $(this).find('input');
            if (input.length > 0) {
                const value = input.val();
                input.replaceWith(`<span>${formatNumber(parseFloat(convertPersianToEnglish(value)))}</span>`);
            }

            const i = $(this).find('i');
            if (i.length > 0) {
                i.replaceWith(``);
            }
        }
    });
    //convertNumbersInPage();
    const actionCell1 = row.find('td:last-child');
    actionCell1.empty();
    if (actionCell1.find('.fa-trash').length === 0) {
        actionCell1.append(`
		<i class="fa fa-trash DelRMUStyle ms-2" onclick="event.stopPropagation();DeleteRizMetre(this,'${rizMetreId}','${Shomareh}','${rizId1}','${FBId}')"></i>
	`);
    }
}



function UpdateRizMetreFromRow(el, rizMetreId, Shomareh, rizId1, FBId, Code) {
    const row = $(el).closest('tr');

    const rizId = row.data('id');

    const inputs = row.find('input');
    const MeghdarJoz = row.find('#spanMeghdarJoz');

    Check = false;
    const values = inputs.map(function () {
        if ($(this).hasClass('essentialValue')) {
            currentValue = convertPersianToEnglish($(this).val().replace(/\,/g, ''));
            if ($.isNumeric(parseFloat(currentValue))) {
                $(this).removeClass('blinking');
                return currentValue;
            }
            else {
                $(this).addClass('blinking');
                Check = true;
            }
        }
        else return convertPersianToEnglish($(this).val().replace(/\,/g, ''));

        //return $(this).val().trim();
    }).get();

    if (Check) {
        toastr.info('اطلاعات وارد شده، صحیح نمی باشند', 'اطلاع');
    }
    else {

        BarAvordUserId = $('#HDFBarAvordUserID').val();
        NoeFBId = parseInt($('#HDFNoeFB').val());
        Year = parseInt($('#HDFYear').val());
        debugger;
        var vardata = new Object();
        vardata.Id = rizId;
        vardata.TajmieItem = values[0] != "" ? values[0] : null;
        vardata.Sharh = values[1];
        vardata.Tedad = values[2] != "" ? values[2] : null;
        vardata.Tool = values[3] != "" ? values[3] : null;
        vardata.Arz = values[4] != "" ? values[4] : null;
        vardata.Ertefa = values[5] != "" ? values[5] : null;
        vardata.Vazn = values[6] != "" ? values[6] : null;
        vardata.Des = values[7];
        vardata.NoeFBId = NoeFBId;
        vardata.Year = Year;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Code = Code;
        vardata.LevelNumber = 1;
        $.ajax({
            type: "POST",
            url: '/RizMetreUserFromShowBarAvord/UpdateRizMetreUsersFrmShowBarAvord',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {
                    row.removeClass('editing');
                    let num = parseFloat(info[1]);
                    //let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text()));

                    el.parent().parent().find('#spanMeghdarJoz').html(formatNumber(parseFloat(num).toFixed(2)));

                    var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                    meghdarFasl.html(formatNumber(parseFloat(info[2]).toFixed(3)));

                    dBahayeItem = info[3];
                    var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                    bahayeKolFasl.html(formatNumber(parseFloat(dBahayeItem).toFixed(0)));

                    //bahayeKolFasl.html(formatNumber((bahayeVahedFasl1 * info[2]).toFixed(0)));
                    //MeghdarJoz.html(parseFloat(num.toFixed(2)));

                    //tdJameFasl.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    //tdJameFaslBaZarib.html(formatNumber(parseFloat(info[3]).toFixed(0)));
                    //GetRizMetreUsers();
                    toastr.success('ریزه متره انتخابی بدرستی ویرایش گردید', 'موفقیت');
                    cancelEditRow(row, rizMetreId, Shomareh, rizId1, FBId);
                    refreshBaseItem(el, Shomareh, Code);
                    GetJamBarAvordAll();
                    GetJamFosoulAll();
                }
                else if (info[0] == "NOK") {
                    toastr.info('فصل تجمیعی انتخابی وجود ندارد', 'اطلاع');
                }
                else
                    toastr.info('مشکل در ویرایش ریزه متره انتخابی', 'اطلاع');
            },
            error: function (msg) {
                toastr.error('مشکل در ویرایش ریزه متره انتخابی', 'خطا');
            }
        });

    }
    // ارسال به سرور با Ajax
    // $.post('/api/update-riz', updatedData, function (res) {
    // 	alert('ویرایش انجام شد');
    // });

    // برگشت به حالت نمایش
}

function SaveNewRMUClick($btn) {

    var $button = $btn instanceof jQuery ? $btn : $($btn);
    var $row = $button.closest('tr');
    var code = ($row.data('riz-code') || '').toString(); // مثلا "12"

    itemId = $row.attr('id').split('-')[1];

    var $fbShomareh = $row.find('input[id^="inputFBShomareh-"]');
    var $sharh = $row.find('input[id^="inputSharh-"]');
    var $vahed = $row.find('select[id^="selectVahed-"]');
    var $bahayeVahed = $row.find('input[id^="inputBahayeVahed-"]');
    var $itemZaribStar = $row.find('input[id^="txtItemZaribStar-"]');
    var $kharidTajhizat = $row.find('input[id^="ckKharidTajhizat-"]');

    // پاک کردن خطاهای قبلی
    $row.find('.blinking').removeClass('blinking');

    var valid = true;
    var firstInvalid = null;
    var errorMsg = null;

    function markInvalid($el, msg) {
        $el.addClass('blinking');
        if (!firstInvalid) {
            firstInvalid = $el;
            errorMsg = msg;
        }
        valid = false;
    }

    var fbVal = ($fbShomareh.val() || '').trim();
    var sharhVal = ($sharh.val() || '').trim();
    var vahedVal = ($vahed.val() || '').trim();
    var bahayeVal = ($bahayeVahed.val() || '').trim();

    var kharidTajhizat = $kharidTajhizat.prop("checked");

    // 1) خالی نبودن فیلدها
    if (!fbVal) markInvalid($fbShomareh, 'شماره فهرست‌بها را وارد کنید.');
    if (!sharhVal) markInvalid($sharh, 'شرح را وارد کنید.');
    if (!vahedVal) markInvalid($vahed, 'واحد را انتخاب کنید.');
    if (!bahayeVal) markInvalid($bahayeVahed, 'بهای واحد را وارد کنید.');

    // 2) inputFBShomareh عددی باشد
    if (fbVal && !/^\d+$/.test(fbVal)) {
        markInvalid($fbShomareh, 'شماره فهرست‌بها باید فقط شامل ارقام باشد.');
    }

    // 3) inputFBShomareh با data-riz-code شروع شود و دقیقا 6 رقم باشد
    if (fbVal) {
        if (fbVal.length !== 6) {
            markInvalid($fbShomareh, 'شماره فهرست‌بها باید دقیقا ۶ رقم باشد.');
        } else if (code && !fbVal.startsWith(code)) {
            markInvalid(
                $fbShomareh,
                'شماره فهرست‌بها باید با کد ریز فصل (' + code + ') شروع شود.'
            );
        }
    }

    // 4) inputBahayeVahed دسیمال باشد
    if (bahayeVal) {
        var normalized = bahayeVal.replace(',', '.'); // اجازه استفاده از , نیز
        if (!/^\d+(\.\d+)?$/.test(normalized)) {
            markInvalid($bahayeVahed, 'بهای واحد باید عدد اعشاری معتبر باشد (مثال: 123 یا 123.45).');
        } else {
            bahayeVal = normalized; // آماده ارسال به سرور
        }
    }

    // اگر هر خطایی وجود داشت
    if (!valid) {
        toastr.info(errorMsg || 'اطلاعات وارد شده صحیح نمی‌باشند.', 'اطلاع');
        if (firstInvalid) {
            firstInvalid.focus(); // فوکوس روی اولین فیلد ایراددار
        }
        return;
    }

    BarAvordUserId = $('#HDFBarAvordUserID').val();

    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    debugger;
    ItemZaribStar = $itemZaribStar == undefined ? null : $itemZaribStar.val().trim() == "" ? null : $itemZaribStar.val();

    var vardata = new Object();

    vardata.BaravordId = BarAvordUserId;
    vardata.FBShomareh = fbVal;
    vardata.Sharh = sharhVal;
    vardata.VahedId = parseInt(vahedVal);
    vardata.BahayeVahed = parseFloat(bahayeVal);
    vardata.kharidTajhizat = kharidTajhizat;
    vardata.itemZaribStar = ItemZaribStar;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;


    $.ajax({
        type: "POST",
        url: '/ItemFBStar/SaveItemFBStar',
        dataType: "json",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == "OK") {

                GetBarAvord(itemId, code, "", true);
                toastr.success('اطلاعات با موفقیت ذخیره شد.', 'موفق');
            }
            else if (data == "repeat") {
                toastr.info('کد تکراری میباشد.', 'اطلاع');
            }
        },
        error: function () {
            toastr.error('خطا در ذخیره اطلاعات.', 'خطا');
        }
    });

}

function SaveRMUClick(object, Shomareh, rizId, Code) {

    object.attr('disabled', true);

    object.parent().parent().find('input[type=text]').each(function () {
        $(this).removeClass('ErrorValueStyle');
    });



    var Sharh, TajmieItem, Tedad, Tool, Arz, Ertefa, Vazn, Des, Check = true;
    firstObj = null;
    findFirstObj = false;
    object.parent().parent().find('input[type=text]').each(function () {

        debugger;

        if ($(this).attr('id') == 'txtSharh')
            Sharh = $(this).val();
        else if ($(this).attr('id').startsWith('txtTajmieItem-'))
            TajmieItem = $(this).val();
        else if ($(this).attr('id') == 'txtTedad') {
            if ($(this).hasClass('essentialValue')) {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Tedad = $(this).val().replace(/\,/g, '');
                    $(this).removeClass('blinking');
                }
                else {
                    $(this).addClass('blinking');
                    Check = false;
                    if (!findFirstObj) {
                        firstObj = $(this);
                        findFirstObj = true;
                    }
                }
            }
            else {
                if ($(this).val().trim() != '') {
                    if ($.isNumeric(parseFloat($(this).val()))) {
                        Tedad = $(this).val().replace(/\,/g, '');
                        $(this).removeClass('blinking');
                    }
                    else {
                        $(this).addClass('blinking');
                        Check = false;
                        if (!findFirstObj) {
                            firstObj = $(this);
                            findFirstObj = true;
                        }
                    }
                }
            }
        }
        else if ($(this).attr('id') == 'txtTool') {
            if ($(this).hasClass('essentialValue')) {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Tool = $(this).val().replace(/\,/g, '');
                    $(this).removeClass('blinking');

                }
                else {
                    $(this).addClass('blinking');
                    Check = false;
                    if (!findFirstObj) {
                        firstObj = $(this);
                        findFirstObj = true;
                    }
                }
            }
            else
            {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Tool = $(this).val().replace(/\,/g, '');
                }
            }
        }
        else if ($(this).attr('id') == 'txtArz') {
            if ($(this).hasClass('essentialValue')) {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Arz = $(this).val().replace(/\,/g, '');
                    $(this).removeClass('blinking');

                }
                else {
                    $(this).addClass('blinking');
                    Check = false;
                    if (!findFirstObj) {
                        firstObj = $(this);
                        findFirstObj = true;
                    }
                }
            }
            else {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Arz = $(this).val().replace(/\,/g, '');
                }
            }
        }
        else if ($(this).attr('id') == 'txtErtefa') {
            if ($(this).hasClass('essentialValue')) {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Ertefa = $(this).val().replace(/\,/g, '');
                    $(this).removeClass('blinking');

                }
                else {
                    $(this).addClass('blinking');
                    Check = false;
                    if (!findFirstObj) {
                        firstObj = $(this);
                        findFirstObj = true;
                    }
                }
            }
            else
            {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Ertefa = $(this).val().replace(/\,/g, '');
                }
            }
        }
        else if ($(this).attr('id') == 'txtVazn') {
            if ($(this).hasClass('essentialValue')) {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Vazn = $(this).val().replace(/\,/g, '');
                    $(this).removeClass('blinking');
                }
                else {
                    $(this).addClass('blinking');
                    Check = false;
                    if (!findFirstObj) {
                        firstObj = $(this);
                        findFirstObj = true;
                    }
                }
            }
            else {
                if ($.isNumeric(parseFloat($(this).val()))) {
                    Vazn = $(this).val().replace(/\,/g, '');
                }
            }
        }
        else if ($(this).attr('id') == 'txtDes')
            Des = $(this).val();
    });

    debugger;

    if (Check) {
        BarAvordUserId = $('#HDFBarAvordUserID').val();
        Year = parseInt($('#HDFYear').val());
        NoeFBId = parseInt($('#HDFNoeFB').val());

        var vardata = new Object();
        vardata.Sharh = Sharh;
        vardata.TajmieItem = TajmieItem === undefined ? null : TajmieItem;
        vardata.Tedad = Tedad === undefined ? null : Tedad;
        vardata.Tool = Tool === undefined ? null : Tool;
        vardata.Arz = Arz === undefined ? null : Arz;
        vardata.Ertefa = Ertefa === undefined ? null : Ertefa;
        vardata.Vazn = Vazn === undefined ? null : Vazn;
        vardata.Des = Des;
        vardata.BarAvordUserId = BarAvordUserId;
        vardata.Shomareh = Shomareh;
        vardata.Year = Year;
        vardata.NoeFBId = NoeFBId;
        $.ajax({
            type: "POST",
            url: '/RizMetreUserFromShowBarAvord/ConfirmRizMetreUsersFromShowBarAvord',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var info = data.split('_');
                if (info[0] == "OK") {


                    ///////////////
                    ///////////////

                    debugger;
                    //dMeghdarJoz = data.dMeghdarJoz;
                    //SumMeghdarJoz = data.sumMeghdarJoz;
                    //JameFasl = data.jameFasl;
                    //JameFaslInZarib = data.jameFaslInZarib;
                    //JameFalsKol = data.jameFalsKol;
                    //JameFalsKolWithoutBahaVahedWithZarib = data.jameFalsKolWithoutBahaVahedWithZarib
                    //JameFaslWithoutBahaVahed = data.jameFaslWithoutBahaVahed;
                    //JameFaslWithoutBahaVahedWithZarib = data.jameFaslWithoutBahaVahedWithZarib;
                    SumMeghdarJoz = info[2];
                    dBahayeItem = info[3];
                    FBId = info[4];

                    var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                    meghdarFasl.html(formatNumber(parseFloat(SumMeghdarJoz).toFixed(3)));

                    var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                    bahayeKolFasl.html(formatNumber(parseFloat(dBahayeItem).toFixed(0)));
                    refreshBaseItem(object, Shomareh, Code);

                    GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code);
                    //////////////////
                    //////////////////
                    GetJamBarAvordAll();
                    GetJamFosoulAll();
                    toastr.success('ریزه متره جدید بدرستی درج گردید', 'موفقیت');


                    //inputBahayeVahed = $('#txtBahayeVahed-' + rizId);
                    //const val = parseFloat(inputBahayeVahed.val().replace(/,/g, '')); // حذف کاما و تبدیل به عدد
                    //if (val === 0) {
                    //    inputBahayeVahed.addClass('blinking');
                    //} else {
                    //    inputBahayeVahed.removeClass('blinking'); // اگر نمی‌خواهید در غیر صفر بودن کلاس باقی بماند
                    //}
                    debugger;

                }
                else if (info[0] == "NOK") {
                    object.removeAttr('disabled');
                    toastr.info('فصل تجمیعی انتخابی وجود ندارد', 'اطلاع');
                }
                else if (info[0] == "NOOK") {
                    object.removeAttr('disabled');
                    toastr.warning('مقداری جهت درج وجود ندارد', 'اطلاع');
                }
                else {
                    object.removeAttr('disabled');
                    toastr.info('مشکل در درج ریزه متره جدید', 'اطلاع');
                }
            },
            error: function (msg) {
                object.removeAttr('disabled');

                toastr.error('مشکل در درج ریزه متره جدید', 'خطا');
            }
        });
    }
    else {
        firstObj.focus().select();
        object.removeAttr('disabled');
        toastr.warning('مقادیر مشخص شده را وارد نمایید', 'هشدار');
    }
}

function refreshBaseItem(object, Shomareh, Code) {

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    Year = parseInt($('#HDFYear').val());
    NoeFBId = parseInt($('#HDFNoeFB').val());

    BAType = $('#HDFBAType').val();


    var vardata = new Object();
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.Shomareh = Shomareh;
    vardata.Year = Year;
    vardata.NoeFBId = NoeFBId;
    vardata.Code = Code;

    if (BAType != 'BarAvord') {
        strUrl = '/BaseInfo/GetFosoulForSooratVaziat';

        $.ajax({
            type: "POST",
            url: strUrl,//'/RizMetreUserFromShowBarAvord/GetJameCurrentFasl',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                debugger;

                ///////////////
                ///////////////

                //dMeghdarJoz = data.dMeghdarJoz;
                //SumMeghdarJoz = data.sumMeghdarJoz;

                JameFasl = data.currentFasl.jameFasl;
                JameFaslAll = data.currentFasl.jameFaslAll;

                JameFaslWithZarib = data.currentFasl.jameFaslWithZarib;
                JameFaslWithZaribAll = data.currentFasl.jameFaslWithZaribAll;

                JameFaslAllWithZarib101 = data.sumFosoulForSooratVaziat.dJameFaslWithZaribAll;
                jameFaslBaZarib101 = data.sumFosoulForSooratVaziat.dJameFaslWithZarib;

                JameFasl101 = data.sumFosoulForSooratVaziat.dJameFasl;
                JameFaslAll101 = data.sumFosoulForSooratVaziat.dJameFaslAll;
                // JameFaslInZarib = data.jameFaslInZarib;
                // JameFalsKol = data.jameFalsKol;
                // JameFalsKolWithoutBahaVahedWithZarib = data.jameFalsKolWithoutBahaVahedWithZarib
                // JameFaslWithoutBahaVahed = data.jameFaslWithoutBahaVahed;
                // JameFaslWithoutBahaVahedWithZarib = data.jameFaslWithoutBahaVahedWithZarib;
                // FBId = data.fbId;


                //const row = $(object).closest('tr');

                const tdJameFasl = $('#tdjameFasl-' + Code);
                const tdjameFaslAll = $('#tdjameFaslAll-' + Code);

                const tdjameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                const tdjameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                tdJameFasl.html(formatNumber(JameFasl.toFixed(0)));
                tdjameFaslAll.html(formatNumber(JameFaslAll.toFixed(0)));

                tdjameFaslBaZarib.html(formatNumber(JameFaslWithZarib.toFixed(0)));
                tdjameFaslAllBaZarib.html(formatNumber(JameFaslWithZaribAll.toFixed(0)));

                const tdJameFasl101 = $('#tdjameFasl-101');
                const tdjameFaslAll101 = $('#tdjameFaslAll-101');

                const tdjameFaslBaZarib101 = $('#tdjameFaslBaZarib-101');
                const tdJameFaslAllBaZarib101 = $('#tdjameFaslAllBaZarib-101');


                tdJameFasl101.html(formatNumber(JameFasl101.toFixed(0)));
                tdjameFaslAll101.html(formatNumber(JameFaslAll101.toFixed(0)));

                tdjameFaslBaZarib101.html(formatNumber(jameFaslBaZarib101.toFixed(0)));
                tdJameFaslAllBaZarib101.html(formatNumber(JameFaslAllWithZarib101.toFixed(0)));

                GetSumAllFBForSooratVaziat();
                GetSumAllFosoulForSooratVaziat();
                //const tdjameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);
            },
            error: function (msg) {
                object.removeAttr('disabled');

                toastr.error('مشکل در بارگذاری جمع مقادیر', 'خطا');
            }
        });
    }
    else {
        strUrl = '/RizMetreUserFromShowBarAvord/GetJameCurrentFasl';

        $.ajax({
            type: "POST",
            url: strUrl,//'/RizMetreUserFromShowBarAvord/GetJameCurrentFasl',
            dataType: "json",
            data: JSON.stringify(vardata),
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                debugger;

                ///////////////
                ///////////////

                //dMeghdarJoz = data.dMeghdarJoz;
                //SumMeghdarJoz = data.sumMeghdarJoz;
                JameFasl = data.jameFasl;
                JameFaslInZarib = data.jameFaslInZarib;
                JameFalsKol = data.jameFalsKol;
                JameFalsKolWithoutBahaVahedWithZarib = data.jameFalsKolWithoutBahaVahedWithZarib
                JameFaslWithoutBahaVahed = data.jameFaslWithoutBahaVahed;
                JameFaslWithoutBahaVahedWithZarib = data.jameFaslWithoutBahaVahedWithZarib;
                FBId = data.fbId;


                const row = $(object).closest('tr');
                const tdJameFasl = $('#tdjameFasl-' + Code);
                const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);
                const tdjameFaslAll = $('#tdjameFaslAll-' + Code);
                const tdjameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);


                //const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
                //const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

                const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
                const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);

                //const MeghdarJoz = row.find('#spanMeghdarJoz');

                //var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                //var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                var bahayeVahedFasl = $('#bahayeVahedFasl_' + Shomareh);

                //var txtBahayeVahed = $('#txtBahayeVahed-' + rizId);

                //let num = parseFloat(dMeghdarJoz);
                let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

                //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(txtBahayeVahed.val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

                //meghdarFasl.html(formatNumber(SumMeghdarJoz.toFixed(0)));
                //bahayeKolFasl.html(formatNumber(JameFasl.toFixed(0)));//(formatNumber(txtBahayeVahed1 * SumMeghdarJoz));
                //MeghdarJoz.html(num);
                tdJameFasl.html(formatNumber(parseFloat(JameFasl).toFixed(0)));
                tdJameFaslBaZarib.html(formatNumber(parseFloat(JameFaslInZarib).toFixed(0)));

                //JameFaslStar = parseFloat(convertPersianToEnglish(tdJameFaslStar.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAll = parseFloat(JameFasl) + (JameFaslStar === NaN ? JameFaslStar : 0);
                //tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

                if (Code != '99') {
                    tdJameFaslStar.html(formatNumber(JameFaslWithoutBahaVahed.toFixed(0)));
                    tdJameFaslStarBaZarib.html(formatNumber(JameFaslWithoutBahaVahedWithZarib.toFixed(0)));

                    tdjameFaslAll.html(formatNumber(JameFalsKol.toFixed(0)));
                }
                tdjameFaslAllBaZarib.html(formatNumber(JameFalsKolWithoutBahaVahedWithZarib.toFixed(0)));

                //JameFaslStarBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslStarBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
                //dJameFaslAllBaZarib = parseFloat(JameFaslInZarib) + (JameFaslStarBaZarib === NaN ? JameFaslStarBaZarib : 0);
                //tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));
            },
            error: function (msg) {
                object.removeAttr('disabled');

                toastr.error('مشکل در بارگذاری جمع مقادیر', 'خطا');
            }
        });
    }

}
function refreshBaseItemNewBV(object, JameFasl, JameFaslInZarib, Code, Shomareh, BahayeVahedNew) {

    const row = $(object).closest('tr');
    const tdJameFasl = $('#tdjameFasl-' + Code);
    const tdJameFaslBaZarib = $('#tdjameFaslBaZarib-' + Code);

    const tdJameFaslAll = $('#tdjameFaslAll-' + Code);
    const tdJameFaslAllBaZarib = $('#tdjameFaslAllBaZarib-' + Code);

    const tdJameFaslStar = $('#tdjameFaslStar-' + Code);
    const tdJameFaslStarBaZarib = $('#tdjameFaslStarBaZarib-' + Code);

    const MeghdarJoz = row.find('#spanMeghdarJoz');

    var meghdarFasl = $('#meghdarFasl_' + Shomareh);
    var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
    var bahayeVahedFasl = $('#bahayeVahedFasl_' + Shomareh);

    let bahayeVahedFasl1 = parseFloat(convertPersianToEnglish(bahayeVahedFasl.text() == "" ? "0" : bahayeVahedFasl.text()));

    //let txtBahayeVahed1 = parseFloat(convertPersianToEnglish(txtBahayeVahed.val().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));

    //meghdarFasl.html(formatNumber(SumMeghdarJoz));
    //bahayeKolFasl.html(formatNumber(BahayeVahedNew * SumMeghdarJoz));
    //MeghdarJoz.html(num);
    tdJameFasl.html(formatNumber(parseFloat(JameFasl).toFixed(0)));
    tdJameFaslBaZarib.html(formatNumber(parseFloat(JameFaslInZarib).toFixed(0)));

    JameFaslStar = parseFloat(convertPersianToEnglish(tdJameFaslStar.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
    dJameFaslAll = parseFloat(JameFasl) + (JameFaslStar === NaN ? JameFaslStar : 0);
    tdJameFaslAll.html(formatNumber(dJameFaslAll.toFixed(0)));

    JameFaslStarBaZarib = parseFloat(convertPersianToEnglish(tdJameFaslStarBaZarib.html().replace(/٬/g, '').replace(/,/g, '').replace('٫', '.')));
    dJameFaslAllBaZarib = parseFloat(JameFaslInZarib) + (JameFaslStarBaZarib === NaN ? JameFaslStarBaZarib : 0);
    tdJameFaslAllBaZarib.html(formatNumber(dJameFaslAllBaZarib.toFixed(0)));
}


//ریزه متره های عادی
function GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code) {

    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.FBId = FBId;
    vardata.ItemFBShomareh = Shomareh;
    vardata.NoeFB = NoeFBId;
    vardata.Year = Year;

    $.ajax({
        url: "/RizMetreUserFromShowBarAvord/GetCurrentRizMetreUsersForShowBarAvord",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            rizMetreUsers = response.rizMetreUsers;
            lstItemsFields = response.lstItemsFields;
            const rizRow = $(`#${rizId}`);
            const tbody = rizRow.find('tbody');
            tbody.empty(); // پاک‌کردن محتوای قدیمی

            let rizRowsHtml = '';

            if (Array.isArray(rizMetreUsers) && rizMetreUsers.length > 0) {
                rizRowsHtml += rizMetreUsers.map(riz => `
                    <tr class="riz-row-data" data-id="${riz.id}" data-itemsFields='${JSON.stringify(lstItemsFields).replace(/"/g, '&quot;')}' onclick="EditRizMetreRow(this,'${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')" style="cursor:pointer;">
					<td style="text-align:center;width:4%">${toPersianDigits(riz.shomarehNew)}</td>
					<td style="width:6%"><span>${toPersianDigits(riz.tajmieItem)}</span></td>
					<td style="width:22%"><span>${toPersianDigits(riz.sharh)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.tedad == null ? '' : toPersianDigits(riz.tedad)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.tool == null ? '' : toPersianDigits(riz.tool)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.arz == null ? '' : toPersianDigits(riz.arz)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.ertefa == null ? '' : toPersianDigits(riz.ertefa)}</span></td>
					<td style="text-align:center;width:7%"><span>${riz.vazn == null ? '' : toPersianDigits(riz.vazn)}</span></td>
					<td style="text-align:center;width:7%"><span id="spanMeghdarJoz">${riz.meghdarJoz == null ? '' : toPersianDigits(riz.meghdarJoz)}</span></td>
					<td style="width:20%"><span>${toPersianDigits(riz.des ?? '')}</span></td>
                    <td style="text-align:center;width:7%"><i class="fa fa-trash DelRMUStyle" onclick="event.stopPropagation();DeleteRizMetre(this,'${riz.id}','${Shomareh}','${rizId}','${FBId}','${Code}')"></i></td>
                    </tr>
                `).join('');
            }




            field = lstItemsFields;
            const defaultField = { id: null, itemShomareh: Shomareh || '', fieldType: null, vahed: '', isEnteringValue: false, essentialValue: false };

            const getField = (i) => (i >= 0 && i < field.length ? field[i] : defaultField);

            const f0 = getField(0);
            const f1 = getField(1);
            const f2 = getField(2);
            const f3 = getField(3);
            const f4 = getField(4);

            rizRowsHtml += `
<tr class="riz-input-row">
  <td style="text-align:center;width:4%"></td>
  <td style="text-align:center;width:6%">
    <div class="d-flex align-items-center gap-2">
            <i class="fa fa-info SaveRMUStyle SelectTajmieItemClass" style="padding-left: 15px;" onclick="ShowSelectTajmie('${Shomareh}-New')"></i>
        
            <input id="txtTajmieItem-${Shomareh}-New" class="form-control form-control-sm" type="text" onchange="TajmieItemChange(this)"/>
       
    </div>
  </td>
  <td style="width:22%">
    <input id="txtSharh" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTedad" class="${getInputClass(f0)}" type="text" ${getDisabledAttr(f0)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtTool" class="${getInputClass(f1)}" type="text" ${getDisabledAttr(f1)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtArz" class="${getInputClass(f2)}" type="text" ${getDisabledAttr(f2)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtErtefa" class="${getInputClass(f3)}" type="text" ${getDisabledAttr(f3)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtVazn" class="${getInputClass(f4)}" type="text" ${getDisabledAttr(f4)} />
  </td>

  <td style="text-align:center;width:7%">
    <input id="txtMeghdarJoz" class="form-control form-control-sm" type="text" disabled />
  </td>

  <td style="width:20%">
    <input id="txtDes" class="form-control form-control-sm" type="text" />
  </td>

  <td style="text-align:center;;width:7%">
    <button type="button"
      onclick="SaveRMUClick($(this),'${Shomareh}','${rizId}','${Code}')"
      class="ButtonRowsSaveStyle">
      <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
    </button>
  </td>
</tr>
`;

            // اضافه کردن ردیف ورودی جدید
            //rizRowsHtml += `
            //    <tr class="riz-input-row">
            //        <td></td>
            //        <td><input id="txtSharh" class="form-control form-control-sm" type="text"/></td>
            //        <td><input id="txtTedad" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtTool" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtArz" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtErtefa" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtVazn" class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtMeghdarJoz" disabled class="form-control form-control-sm" type="text" /></td>
            //        <td><input id="txtDes" class="form-control form-control-sm" type="text"/></td>
            //        <td>
            //            <button type="button" onclick="SaveRMUClick($(this),'${Shomareh}','${rizId}','${Code}')" class="ButtonRowsSaveStyle">
            //                <i id="iSave" class="fa fa-save SaveRMUStyle"></i>
            //            </button>
            //        </td>
            //    </tr>
            //`;

            tbody.html(rizRowsHtml);

            tbody.find('.riz-input-row #txtSharh').focus()
            //aaa= tbody.find('.riz-input-row');
            // دوباره Bind کردن دکمه حذف
            $('.btn-delete-riz').off('click').on('click', function () {
                $(this).closest('tr').remove();
            });

            //convertNumbersInPage();
        },
        error: function () {
            toastr.error('خطا در دریافت اطلاعات ریزمتره!', 'خطا');
        }
    });
}

function TajmieItemChange(Obj) {

    debugger;

    TajmieItemVal = $(Obj).val();
    NoeFBId = parseInt($('#HDFNoeFB').val());

    $.ajax({

        url: "/TajmieFosoul/CheckItemInTajmie",

        type: "GET",

        data: {

            itemTajmieValue: TajmieItemVal,
            noeFB: NoeFBId,
        },

        success: function (data) {
            debugger;
           
        },
         error: function (xhr) {

            if (xhr.status == 400) {

                toastr.warning(xhr.responseJSON.message);

            }
            else {

                toastr.warning("این فصل تجمیعی تعریف نشده است");

            }

        }

    });
}

function DeleteRizMetre(object, RizMetreId, Shomareh, rizId, FBId, Code) {
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.Id = RizMetreId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;

    $.ajax({
        url: "/RizMetreUserFromShowBarAvord/DeleteRizMetre",
        method: "POST",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                SumMeghdarJoz = info[2];
                dBahayeItem = info[3];

                var meghdarFasl = $('#meghdarFasl_' + Shomareh);
                meghdarFasl.html(formatNumber(parseFloat(SumMeghdarJoz).toFixed(3)));

                var bahayeKolFasl = $('#bahayeKolFasl_' + Shomareh);
                bahayeKolFasl.html(formatNumber(parseFloat(dBahayeItem).toFixed(0)));
                toastr.success('ریز متره بدرستی حذف گردید', 'موفقیت');
                GetCurrentRizMetreUsers(Shomareh, rizId, FBId, Code);
                GetJamBarAvordAll();
                GetJamFosoulAll();
                refreshBaseItem(object, Shomareh, Code);
            }
        },
        error: function () {
            toastr.error('خطا در حذف ریزمتره!', 'خطا');
        }
    });
}



