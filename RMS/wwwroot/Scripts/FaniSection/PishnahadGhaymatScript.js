function GetFosoulPishnahadGhaymat() {
    debugger;
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());
    CompanyId = $('#HDFCompanyID').val();


    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.CompanyId = CompanyId;

    $.ajax({
        type: "post",
        url: "/PishnahadGhaymat/GetFosoulPishnahadGhaymat",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            ZaribPishnahadiPaymanKars = response;
            //lstVaheds = response.lstVaheds;
            renderTableFosoul(ZaribPishnahadiPaymanKars);

        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}


function setMablaghPishnahadi(Obj, ItemId, Code) {
    debugger;
    txtValue = Obj.value;

    var vardata = new Object();
    vardata.ItemId = ItemId;
    vardata.txtValue = txtValue;
    $.ajax({
        type: "post",
        url: "/PishnahadGhaymat/SetMablaghPishnahadi",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            debugger;
            if (info[0] == "OK") {
                $(Obj).val(formatNumber(parseFloat(txtValue).toFixed(0)));
                Zarib = parseFloat(info[1]);
                $('#tdZaribPaymanKar-' + Code).html(formatNumber(Zarib.toFixed(4)));
                toastr.success('مبلغ پیشنهادی پیمانکار بدرستی ثبت گردید', 'موفقیت');
                SumEveryFBOnlyNumber();
            }
        },
        error: function (response) {
            toastr.error('خطا', 'مشکل در ثبت مبلغ پیشنهادی پیمانکار');
        }
    });
}

function setMablaghPishnahadiTajhiz(Obj) {
    debugger;
    CompanyId = $('#HDFCompanyID').val();
    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFBId = parseInt($('#HDFNoeFB').val());

    txtValue = Obj.value;
    var vardata = new Object();
    vardata.CompanyId = CompanyId;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.txtValue = txtValue;
    $.ajax({
        type: "post",
        url: "/PishnahadGhaymat/SetMablaghPishnahadiTajhiz",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            debugger;
            if (info[0] == "OK") {
                $(Obj).val(formatNumber(parseFloat(txtValue).toFixed(0)));
                Zarib = parseFloat(info[1]);
                $('#tdZaribTajhiz').html(formatNumber(Zarib.toFixed(4)));
                toastr.success('موفقیت', 'مبلغ پیشنهادی پیمانکار بدرستی ثبت گردید');
            }
        },
        error: function (response) {
            toastr.error('خطا', 'مشکل در ثبت مبلغ پیشنهادی پیمانکار');
        }
    });
}


function renderTableFosoul(data) {

    const tbody = $('#fosoulTable tbody');
    tbody.empty();
    data.forEach((item, index) => {

        debugger;
        const rowClass = index % 2 === 0 ? '' : 'table-alt';

        let style = '';
        let tagA = `<a href="#">${item.title}</a>`;

        Id = item.id;


        PishnahadPaymanKar = item.pishnahadPaymanKar == null ? 0 : item.pishnahadPaymanKar;
        ZaribJoz = item.zaribJoz == null ? 0 : item.zaribJoz;


        const mainRow = $(`
		<tr class="${rowClass}" style="${style}">
		<td style="display:none">${item.id}</td>
		
		<td style="text-align:center">
            <span id="tdcode-${item.code}">
			    ${item.radif}
            </span>
		</td>
        <td  colspan="2" style="text-align:right">
               ${tagA}
        </td>
		<td style="text-align:center">
            <span id="tdbaravordFasl-${item.code}">
			    ${formatNumber(item.baravordFasl.toFixed(0))}
            </span>
		</td>
		<td style="text-align:center">
            <span id="tdbaravordFaslBaZarib-${item.code}">
			    ${formatNumber(item.baravordFaslBaZarib.toFixed(0))}
            </span>
		</td>
        <td style="text-align:center;padding:0px">
            <input style="height:100%;border: unset;" onchange="setMablaghPishnahadi(this,'${item.id}','${item.code}')" value="${formatNumber(PishnahadPaymanKar.toFixed(0))}" id="txtMablaghPishnahadi-${item.code}" class="form-control form-control-sm" type="text"/>
		</td>
        <td style="text-align:center">
            <span id="tdZaribPaymanKar-${item.code}">
			    ${ZaribJoz.toFixed(4)}
            </span>
        </td>
        </tr>

		`);


       


        //mainRow.on('click', function (e) {
        //    e.stopPropagation();

        //    const rowId = item.id;
        //    const detailRow = $(`#${rowId}`);
        //    var faslAsli = item.faslAsli;

        //    // اگر باز است → ببند
        //    if (detailRow.is(':visible')) {
        //        detailRow.hide();
        //    } else {
        //        // بستن همه ردیف‌های جزئیات
        //        $('#fosoulTable tr[id]').not(detailRow).hide();

        //        // باز کردن ردیف انتخاب‌شده
        //        detailRow.show();
        //        //GetBarAvord(item.id, item.code, item.title);
        //    }
        //});

        tbody.append(mainRow);
    });

        //convertNumbersInPage();

    tbody.on('click', 'input', function (e) {
        this.select();
    });

    tbody.on('keydown', 'input', function (e) {

        if (e.key === 'Enter') {
            e.preventDefault();

            const $elements = tbody.find('input');
            let idx = $elements.index(this);

            let next = null;
            for (let i = idx + 1; i < $elements.length; i++) {
                if (!$elements.eq(i).is(':disabled')) {
                    next = $elements.eq(i);
                    break;
                }
            }

            if (next && next.length) {
                next.focus().select();
            }
            else
                $elements.eq(0).focus().select();
        }
    });


    SumEveryFB(tbody);

    SumAll();

    //    $('#chkNonZeroMeghdarFasl').off('change').on('change', function () {
    //        const showOnlyNonZero = $(this).is(':checked');
    //        // همه‌ی ردیف‌های اصلی (نه ردیف دوم مربوط به barAvordContainer)
    //        $('#fosoulTable tbody tr').each(function () {
    //            const $row = $(this);
    //            const tdjameFasl = $row.find('[id^="tdjameFasl-"]');
    //            const tdjameFaslStar = $row.find('[id^="tdjameFaslStar-"]');

    //            if (tdjameFasl.length > 0 || tdjameFaslStar.length > 0) {
    //                let meghdarTextjameFasl = tdjameFasl.text().replace(/\s+/g, '').replace(/[٬,]/g, '');
    //                let meghdarTextjameFaslStar = tdjameFaslStar.text().replace(/\s+/g, '').replace(/[٬,]/g, '');
    //                let EnNumjameFasl = convertPersianToEnglish(meghdarTextjameFasl);
    //                let EnNumjameFaslStar = convertPersianToEnglish(meghdarTextjameFaslStar);
    //                const meghdarjameFasl = EnNumjameFasl != "" ? parseFloat(EnNumjameFasl) : 0;
    //                const meghdarjameFaslStar = EnNumjameFaslStar != "" ? parseFloat(EnNumjameFaslStar) : 0;

    //                if (showOnlyNonZero) {
    //                    if (meghdarjameFasl === 0 && meghdarjameFaslStar === 0) {
    //                        $row.hide(); // مخفی‌سازی ردیف اصلی
    //                        //$row.next('tr').hide(); // مخفی‌سازی ردیف barAvordContainer مربوطه
    //                    } else {
    //                        $row.show();
    //                        //$row.next('tr').show();
    //                    }
    //                } else {
    //                    $row.show();
    //                    //$row.next('tr').show();
    //                }
    //            }
    //        });
    //});
}

function SumEveryFB(tbody) {

    CompanyId = $('#HDFCompanyID').val();

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.CompanyId = CompanyId;

    $.ajax({
        type: "post",
        url: "/PishnahadGhaymat/GetSumAllFBPishnahadGhaymat",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            SumAllFBPishnahadGhaymats = response;

            $.each(SumAllFBPishnahadGhaymats, function () {

                NoeFBId = this.noeFB;
                FBTitle = this.fbTitle;
                SumBaravordFaslWithoutTajhiz = this.sumBaravordFaslWithoutTajhiz;
                SumBaravordFaslBaZaribWithoutTajhiz = this.sumBaravordFaslBaZaribWithoutTajhiz;
                SumPishnahadPaymanKarWithoutTajhiz = this.sumPishnahadPaymanKarWithoutTajhiz;
                SumZaribJozWithoutTajhiz = this.sumZaribJozWithoutTajhiz;
                trJamWithoutTajhiz = `
                        <tr id="trJamWithoutTajhiz" style="background-color:#dfd9d9;text-align: center;">
                            <td id="tdNoeFB" style="display:none">${NoeFBId}</td>
                            <td></td>
                            <td  colspan="2" style="text-align:right; font-weight:bold;">
                                <span style="font-size: 15px;padding-right: 30px;color: #1e27b5;">
                                    مبلغ و ضرایب پیشنهادی جزء برای هر فصل فهرست بها - ${FBTitle} 
                                </span>
                            </td>
                            <td id="tdJamKolWithoutTajhiz-${NoeFBId}">${formatNumber(SumBaravordFaslWithoutTajhiz.toFixed(0))}</td>
                            <td id="tdJamKolWithZarayebWithoutTajhiz-${NoeFBId}">${formatNumber(SumBaravordFaslBaZaribWithoutTajhiz.toFixed(0))}</td>
                            <td id="tdJamMablaghPishnahadiWithoutTajhiz-${NoeFBId}">${formatNumber(SumPishnahadPaymanKarWithoutTajhiz.toFixed(0))}</td>
                            <td id="tdJamZaribWithoutTajhiz-${NoeFBId}"></td>
                        </tr>
            `
/*${formatNumber(SumZaribJozWithoutTajhiz.toFixed(4))}*/


                tbody.append(trJamWithoutTajhiz);



            });

            SumTajhiz(tbody);

            //sumBaravordFasl = response.sumBaravordFasl;
            //sumBaravordFaslBaZarib = response.sumBaravordFaslBaZarib;
            //sumPishnahadPaymanKar = response.sumPishnahadPaymanKar;
            //sumZaribJoz = response.sumZaribJoz;

            //$('#tdJamKol').html(formatNumber(sumBaravordFasl.toFixed(0)));
            //$('#tdJamKolWithZarayeb').html(formatNumber(sumBaravordFaslBaZarib.toFixed(0)));
            //$('#tdJamMablaghPishnahadi').html(formatNumber(sumPishnahadPaymanKar.toFixed(0)));
            //$('#tdJamZarib').html(formatNumber(sumZaribJoz.toFixed(4)));

            ///////////////////
            //SumBaravordFaslWithoutTajhiz = response.sumBaravordFaslWithoutTajhiz;
            //SumBaravordFaslBaZaribWithoutTajhiz = response.sumBaravordFaslBaZaribWithoutTajhiz;
            //SumPishnahadPaymanKarWithoutTajhiz = response.sumPishnahadPaymanKarWithoutTajhiz;
            //SumZaribJozWithoutTajhiz = response.sumZaribJozWithoutTajhiz;

            //$('#tdJamKolWithoutTajhiz').html(formatNumber(SumBaravordFaslWithoutTajhiz.toFixed(0)));
            //$('#tdJamKolWithZarayebWithoutTajhiz').html(formatNumber(SumBaravordFaslBaZaribWithoutTajhiz.toFixed(0)));
            //$('#tdJamMablaghPishnahadiWithoutTajhiz').html(formatNumber(SumPishnahadPaymanKarWithoutTajhiz.toFixed(0)));
            //$('#tdJamZaribWithoutTajhiz').html(formatNumber(SumZaribJozWithoutTajhiz.toFixed(4)));

        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });

    //tbody.append(mainRow);
}
function SumEveryFBOnlyNumber() {

    debugger;
    CompanyId = $('#HDFCompanyID').val();

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.CompanyId = CompanyId;

    $.ajax({
        type: "post",
        url: "/PishnahadGhaymat/GetSumAllFBPishnahadGhaymat",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;

            SumAllFBPishnahadGhaymats = response;

            $.each(SumAllFBPishnahadGhaymats, function () {
                debugger;

                NoeFBId = this.noeFB;
                //FBTitle = this.fbTitle;
                SumBaravordFaslWithoutTajhiz = this.sumBaravordFaslWithoutTajhiz;
                SumBaravordFaslBaZaribWithoutTajhiz = this.sumBaravordFaslBaZaribWithoutTajhiz;
                SumPishnahadPaymanKarWithoutTajhiz = this.sumPishnahadPaymanKarWithoutTajhiz;
                SumZaribJozWithoutTajhiz = this.sumZaribJozWithoutTajhiz;

                $(`#tdJamMablaghPishnahadiWithoutTajhiz-${NoeFBId}`).html(formatNumber(SumPishnahadPaymanKarWithoutTajhiz.toFixed(0)));
                SumAll();
                //$(`#tdJamZaribWithoutTajhiz-${NoeFBId}`).html(formatNumber(SumZaribJozWithoutTajhiz.toFixed(4)));
            });


        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });

    //tbody.append(mainRow);
}

function SumAll() {

    debugger;
    CompanyId = $('#HDFCompanyID').val();

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    //NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    //vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.CompanyId = CompanyId;

    $.ajax({
        type: "post",
        url: "/PishnahadGhaymat/GetSumAllWithTajhiz",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;
            SumBaravordFaslTajhiz = response.sumBaravordFaslTajhiz;
            SumBaravordFaslBaZaribTajhiz = response.sumBaravordFaslBaZaribTajhiz;
            SumPishnahadPaymanKarTajhiz = response.sumPishnahadPaymanKarTajhiz;
            SumZaribJozTajhiz = response.sumZaribJozTajhiz;

            $('#tdJamKol').html(formatNumber(SumBaravordFaslTajhiz.toFixed(0)));
            $('#tdJamKolWithZarayeb').html(formatNumber(SumBaravordFaslBaZaribTajhiz.toFixed(0)));
            $('#tdJamMablaghPishnahadi').html(formatNumber(SumPishnahadPaymanKarTajhiz.toFixed(0)));
            $('#tdJamZarib').html(formatNumber(SumZaribJozTajhiz.toFixed(4)));
           
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });

    //tbody.append(mainRow);
}

function SumTajhiz(tbody) {

    debugger;
    CompanyId = $('#HDFCompanyID').val();

    BarAvordUserId = $('#HDFBarAvordUserID').val();
    NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.NoeFBId = NoeFBId;
    vardata.Year = Year;
    vardata.BarAvordUserId = BarAvordUserId;
    vardata.CompanyId = CompanyId;

    $.ajax({
        type: "post",
        url: "/PishnahadGhaymat/GetSumTajhizKargah",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            FBTajhiz = response.fbTajhiz;
            PishnahadTajhiz = response.pishnahadTajhiz;
            debugger;
            SumBaravordFaslTajhiz = FBTajhiz.sumBaravordFaslTajhiz;
            SumBaravordFaslBaZaribTajhiz = FBTajhiz.sumBaravordFaslBaZaribTajhiz;

            SumPishnahadPaymanKarTajhiz = PishnahadTajhiz.pishnahadPaymanKarTajhiz == null ? 0 : PishnahadTajhiz.pishnahadPaymanKarTajhiz;
            SumZaribJozTajhiz = PishnahadTajhiz.zaribJozTajhiz == null ? 0 : PishnahadTajhiz.zaribJozTajhiz;

            trJamTajhiz = `
             <tr id="trJamWithoutTajhiz" style="background-color:#dfd9d9;text-align: center;">
                            <td></td>
                            <td  colspan="2" style="text-align:right; font-weight:bold;">
                                <span style="font-size: 15px;padding-right: 30px;color: #1e27b5;">
                                    مبلغ پیشنهادی جزء تجهیزوبرچیدن کارگاه
                                </span>
                            </td>
                            <td id="tdJamKolTajhiz">${formatNumber(SumBaravordFaslTajhiz.toFixed(0))}</td>
                            <td id="tdJamKolWithZarayebTajhiz">${formatNumber(SumBaravordFaslBaZaribTajhiz.toFixed(0))}</td>
                            <td id="tdMablaghPishnahadiTajhiz">
                                <input style="height:100%;border: unset;" onchange="setMablaghPishnahadiTajhiz(this)" value="${formatNumber(SumPishnahadPaymanKarTajhiz.toFixed(0))}" id="txtMablaghPishnahadiTajhiz-${BarAvordUserId}" class="form-control form-control-sm" type="text"/>
                            </td>
                            <td id="tdZaribTajhiz">${formatNumber(SumZaribJozTajhiz.toFixed(4))}</td>
                        </tr>`

            tbody.append(trJamTajhiz);

            //SumAll();

        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });

    //tbody.append(mainRow);
}

function GetBarAvord(ItemId, Code, faslName) {
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

function formatNumber(num) {
    if (num == null) return '';
    if (num == 0) return '';
    if (isNaN(num)) return '';
    return Number(num).toLocaleString('fa-IR'); // یا 'en-US' برای کاما
}
function toPersianDigits(num) {
    if (num == null) return '';
    return num.toString().replace(/\d/g, d => '۰۱۲۳۴۵۶۷۸۹'[d]);
}

function convertNumbersInPage() {
    $('*').each(function () {
        if (this.children.length === 0 && this.nodeType === 1) {
            const originalText = $(this).text();
            const convertedText = toPersianDigits(originalText);
            if (originalText !== convertedText) {
                $(this).text(convertedText);
            }
        }
    });
}
