$(document).ready(function () {
    GetTadilIndex();
});


function GetTadilIndex() {
    debugger;
    SVId = $('#HDFBarAvordUserID').val();
    CompanyId = $('#HDFCompanyID').val();
    Year = parseInt($('#HDFYear').val());


    strUrl = '/TadilIndex/GetTadilIndex';

    var vardata = new Object();
    vardata.SVId = SVId;
    vardata.CompanyId = CompanyId;
    vardata.Year = Year;

    $.ajax({
        type: "post",
        url: strUrl,
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            debugger;

            title = response.title;
            ProjectName = response.projectName;
            fromDate = response.fromDate;
            toDate = response.toDate;
            Year = response.year;
            allDay = response.allDay;
            lstresultTadilIndexDtos = response.lstresultTadilIndexDtos;

            let html = '';

            let sumDeff = 0;
            let sumKarkard = 0;
            let sumTadil = 0;
            lstresultTadilIndexDtos.forEach(function (noeFb) {

                NoeFBName = noeFb.noeFBName;
                MabnaIndexTitle = noeFb.mabnaIndexTitle;

                html += `
            <div class="row">
                <div class="row col-12">
                <div class="col-1">
                </div>
                    <div class="col-4">
                    <span>
                        پروژه:
                    </span>
                    <span>
                    ${ProjectName}
                    </span>
                    </div>
                    <div class="col-2">
                    <span>
                        تعدیل 
                    </span>
                    <span>
                    ${title}
                    </span>
                    </div>
                    <div class="col-1">
                    <span>
                        از تاریخ:
                    </span>
                    <span>
                    ${fromDate}
                    </span>
                    </div>
                    <div class="col-1">
                    <span>
                        تا تاریخ:
                    </span>
                    <span>
                    ${toDate}
                    </span>
                    </div>

                </div>

                <div class="row col-12">
                <div class="col-1">
                </div>

                    <div class="col-4">
                    <span>
                        فهرست بهای رشته:
                    </span>
                    <span>
                        ${NoeFBName}
                    </span>
                    </div>
                    <div class="col-2">
                    <span>
                        دوره مبنای پیمان:
                    </span>
                    <span>
                        ${MabnaIndexTitle}
                    </span>
                    </div>
                </div>
            </div>
            `

                html += '<div class="row">';
                html += '<div class="table-wrapper">';
                html += '<table class="table table-bordered text-center table-fixed">';
                html += '<thead class="table-header">';
                html += '<tr>';
                html += `<th style="text-align:center;width:5%">سال</th>`;
                html += `<th style="text-align:center;width:5%">دوره</th>`;
                html += `<th style="text-align:center;width:5%">فصل</th>`;
                html += `<th style="text-align:center">
                         مبلغ صورت وضعیت
                         <br/>
                         فعلی (ريال)
                        </th>`;
                html += `<th style="text-align:center">
                        مبلغ صورت وضعیت
                        <br/>
                        قبلی (ريال)
                        </th>`;
                html += `
                    <th style="text-align:center">
                    مابه التفاوت دو
                    <br/>
                    صورت وضعیت(ريال)
                </th>`;
                html += `<th style="text-align:center">
                 مدت کارکرد در دوره
                <br/>
                بمدت کارکرد
                </th >`;
                html += `<th style="text-align:center">
                        مبلغ کارکرد
                        <br/>
                        دوره (ريال)
                        </th>`;
                html += `<th style="text-align:center;width:6%">شاخص مبنا</th>`;
                html += `<th style="text-align:center;width:6%">
                            شاخص دوره
                            <br/>
                            کارکرد
                         </th>`;
                html += `<th style="text-align:center;width:5%">
                        ضریب
                        <br/>
                        تعدیل       
                        </th>`;
                html += `<th style="text-align:center">مبلغ تعدیل (ريال)</th>`;
                html += `</tr>`;
                html += `</thead>`;

                html += '<tbody>';

                noeFb.lstFaslTadilIndex.forEach(function (fasl) {

                    fasl.lstTadilIndex.forEach(function (item) {

                        let courseTypeText = '';
                        // item.courseType == 1
                        //     ? 'سه ماهه'
                        //     : 'ماهانه';

                        if (item.courseType == 1) {

                            switch (item.courseCode) {
                                case 1: {
                                    courseTypeText = 'بهار';
                                    break;
                                }
                                case 2: {
                                    courseTypeText = 'تابستان';
                                    break;
                                }
                                case 3: {
                                    courseTypeText = 'پاییز';
                                    break;
                                }
                                case 4: {
                                    courseTypeText = 'زمستان';
                                    break;
                                }
                                default:
                            }
                        }
                        else {

                            switch (switch_on) {
                                case 1: {
                                    courseTypeText = 'فروردین';
                                    break;
                                }
                                case 2: {
                                    courseTypeText = 'اردیبهشت';
                                    break;
                                }
                                case 3: {
                                    courseTypeText = 'خرداد';
                                    break;
                                }
                                case 4: {
                                    courseTypeText = 'تیر';
                                    break;
                                }
                                case 5: {
                                    courseTypeText = 'مرداد';
                                    break;
                                }
                                case 6: {
                                    courseTypeText = 'شهریور';
                                    break;
                                }
                                case 7: {
                                    courseTypeText = 'مهر';
                                    break;
                                }
                                case 8: {
                                    courseTypeText = 'آبان';
                                    break;
                                }
                                case 9: {
                                    courseTypeText = 'آذر';
                                    break;
                                }
                                case 10: {
                                    courseTypeText = 'دی';
                                    break;
                                }
                                case 11: {
                                    courseTypeText = 'بهمن';
                                    break;
                                }
                                case 12: {
                                    courseTypeText = 'اسفند';
                                    break;
                                }
                                default:
                            }
                        }


                        html += '<tr>';

                        debugger;
                        var bahayeKolInZaribPrev = 0;
                        if (fasl.bahayeKolInZaribPrev != undefined) {
                            bahayeKolInZaribPrev = formatNumber(parseFloat(fasl.bahayeKolInZaribPrev).toFixed(0));
                        }

                        current = formatNumber(parseFloat(fasl.bahayeKolInZarib).toFixed(0));

                        //Deff = fasl.bahayeKolInZaribPrev - fasl.bahayeKolInZarib;
                        Deff = fasl.deff;


                        // nesbat = item.days / allDay;

                        // MablaghKarkardDoreh = nesbat * Deff;

                        IndexValue = item.indexValue;

                        // debugger;

                        PGHIndexValue = item.pghIndexValue;

                        // ZaribTadil = (parseFloat(IndexValue) / parseFloat(PGHIndexValue))* 0.95;

                        // MablaghTadil = parseFloat(MablaghKarkardDoreh).toFixed(0) * parseFloat(ZaribTadil).toFixed(3);



                        var nesbat = item.nesbat;
                        var mablaghKarkardDoreh = item.mablaghKarkardDoreh;
                        var zaribTadil = item.zaribTadil;
                        var mablaghTadil = item.mablaghTadil;



                        debugger;
                        html += `<td style="width:5%">${toPersianDigits(item.year)}</td>`;
                        html += `<td style="width:5%">${courseTypeText}</td>`;
                        html += `<td style="width:5%">${toPersianDigits(fasl.faslCode)}</td>`;
                        html += `<td>${current}</td>`;
                        html += `<td>${bahayeKolInZaribPrev}</td>`;
                        html += `<td>${formatNumber(parseFloat(Deff).toFixed(0))}</td>`;
                        html += `<td style="padding:0px !important">
                                \\[
                                \\frac{${toPersianDigits(item.days)}}{${toPersianDigits(allDay)}}
                                \\]
                                </td>`;
                        html += `<td>${formatNumber(parseFloat(mablaghKarkardDoreh).toFixed(0))}</td>`;
                        html += `<td style="width:6%">${formatNumber(parseFloat(PGHIndexValue).toFixed(3))}</td>`;
                        html += `<td style="width:6%">${formatNumber(parseFloat(IndexValue).toFixed(3))}</td>`;
                        html += `<td style="width:5%">${formatNumber(parseFloat(zaribTadil).toFixed(3))}</td>`;
                        html += `<td>${formatNumber(parseFloat(mablaghTadil).toFixed(0))}</td>`;


                        html += '</tr>';


                    });

                });

                html += '</tbody>';

                html += '</tbody>';

                html += '<tfoot class="table-footer">';
                html += '<tr style="font-weight:bold;background:#8498b3;height: 40px;">';
                html += '<td style="width:15%;font-size:14px">جمع مبلغ تعدیل (ریال)</td>';
                html += `<td style="font-size:14px">${formatNumber(noeFb.sumBahayeKolInZarib)}</td>`;
                html += `<td style="font-size:14px">${formatNumber(noeFb.sumBahayeKolInZaribPrev)}</td>`;
                html += `<td style="font-size:14px">${formatNumber(noeFb.sumDeff)}</td>`;
                html += `<td></td>`;
                html += `<td style="font-size:14px">${formatNumber(noeFb.sumMablaghKarkardDoreh.toFixed(0))}</td>`;
                html += `<td style="width:17%"></td>`;
                html += `<td style="font-size:14px">${formatNumber(noeFb.sumMablaghTadil.toFixed(0))}</td>`;
                html += '</tr>';
                html += '</tfoot>';

                html += '</table></div></div>';
            });



            $('#tblTadilIndexContainer').html(html);

            if (window.MathJax) {
                MathJax.typesetPromise()
                    .then(() => console.log("MathJax rendered"))
                    .catch(err => console.log(err));
            }
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}