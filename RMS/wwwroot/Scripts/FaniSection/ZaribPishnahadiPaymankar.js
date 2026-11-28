/* پیشنهاد قیمت پیمانکار در پنل عملیات درخت دستیار
 * منطق دریافت فصل‌ها، ثبت مبالغ و محاسبه ضرایب در PishnahadGhaymatScript.js است.
 * این فایل رابط همان قابلیت را داخل پنل عملیات ایجاد و راه‌اندازی می‌کند.
 */
function ShowZaribPP(OperationId, event) {
    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }

    var $panel = $('#ula' + OperationId);
    if (!$panel.length) {
        console.error('پنل عملیات پیشنهاد قیمت پیدا نشد:', OperationId);
        return;
    }

    // فقط یک پنل پیشنهاد قیمت در لحظه فعال باشد؛ توابع قدیمی از شناسه‌های ثابت استفاده می‌کنند.
    $('.zaribpp-panel').closest('div[id^="ula"]').not($panel).empty();

    // اطلاعات برآورد در صفحه درخت قبلاً توسط GetViewBag تنظیم شده‌اند.
    var barAvordId = $('#HDFBarAvordUserID').val();
    var year = $('#HDFYear').val();
    var companyId = $('#HDFCompanyID').val();

    if (!barAvordId || !year || !companyId) {
        $panel.html('<div class="alert alert-danger text-center">اطلاعات برآورد یا شرکت برای ثبت پیشنهاد قیمت در دسترس نیست.</div>');
        console.error('Missing proposal context', {
            barAvordId: barAvordId,
            year: year,
            companyId: companyId
        });
        return;
    }

    var currentNoeFB = String($('#HDFNoeFB').val() || '');
    var noeFBs = window.lstNoeFBs || [];
    if (!currentNoeFB && noeFBs.length) {
        currentNoeFB = String(noeFBs[0].id);
        $('#HDFNoeFB').val(currentNoeFB);
    }

    var selectedFB = noeFBs.find(function (item) {
        return String(item.id) === currentNoeFB;
    });
    var selectedFBName = selectedFB ? $('<div>').text(selectedFB.name || '').html() : 'رشته انتخاب نشده است';

    var html = `
        <style>
            #ula${OperationId} .zaribpp-panel { direction: rtl; text-align: right; padding: 10px; }
            #ula${OperationId} .zaribpp-toolbar { display:flex; flex-wrap:wrap; align-items:center; gap:12px; padding:10px 12px; margin-bottom:12px; background:#f3f5f7; border:1px solid #d9dee3; border-radius:5px; }
            #ula${OperationId} .zaribpp-toolbar-title { font-weight:bold; color:#273746; }
            #ula${OperationId} .zaribpp-fb-options { display:flex; flex-wrap:wrap; gap:12px; align-items:center; }
            #ula${OperationId} .zaribpp-fb-option { margin:0; white-space:nowrap; cursor:pointer; font-weight:normal; }
            #ula${OperationId} .zaribpp-table-wrap { width:100%; overflow-x:auto; }
            #ula${OperationId} #fosoulTable { min-width:950px; margin-bottom:8px; }
            #ula${OperationId} #fosoulTable th, #ula${OperationId} #fosoulTable td { vertical-align:middle; }
            #ula${OperationId} #fosoulTable input { min-width:130px; text-align:center; }
            #ula${OperationId} #fosoulTable tfoot td { font-weight:bold; }
            #ula${OperationId} .zaribpp-loading { padding:18px; text-align:center; color:#555; }
        </style>
        <div class="zaribpp-panel">
            <div class="zaribpp-toolbar">
                <span class="zaribpp-toolbar-title">رشته فهرست‌بها:</span>
                <div class="zaribpp-fb-options">${radioHtml}</div>
            </div>
            <div class="zaribpp-table-wrap">
                <table id="fosoulTable" class="table table-bordered table-striped fixed-table">
                    <thead>
                        <tr>
                            <th style="text-align:center;width:5%">ردیف</th>
                            <th colspan="2" style="text-align:right;width:40%">عنوان فصول فهرست‌بها</th>
                            <th style="text-align:center;width:15%">برآورد فصل (اقلام پایه و غیرپایه) (ریال)</th>
                            <th style="text-align:center;width:15%">برآورد فصل پس از اعمال تمامی ضرایب (ریال)</th>
                            <th style="text-align:center;width:15%">مبلغ پیشنهادی پیمانکار با احتساب تمامی ضرایب و هزینه‌های مورد نظر (ریال)</th>
                            <th style="text-align:center;width:10%">ضریب پیشنهادی جزء/کل</th>
                        </tr>
                    </thead>
                    <tbody id="FaslBody"></tbody>
                    <tfoot>
                        <tr style="background-color:#d4c9ff;text-align:center;">
                            <td></td>
                            <td colspan="2" style="text-align:right;">
                                <span style="font-size:15px;padding-right:20px;color:#1e27b5;">
                                    جمع مبلغ پیشنهاد برای تمام رشته‌ها، تجهیز و برچیدن کارگاه و ضریب پیشنهادی کل
                                </span>
                            </td>
                            <td id="tdJamKol"></td>
                            <td id="tdJamKolWithZarayeb"></td>
                            <td id="tdJamMablaghPishnahadi"></td>
                            <td id="tdJamZarib"></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <div id="zaribppStatus_${OperationId}" class="zaribpp-loading">در حال دریافت اطلاعات پیشنهاد قیمت...</div>
        </div>`;

    $panel.html(html);

    if (!currentNoeFB) {
        $('#zaribppStatus_' + OperationId).text('برای نمایش فصل‌ها، رشته فهرست‌بها مشخص نشده است.');
        return;
    }

    // توابع اصلی در PishnahadGhaymatScript.js تعریف شده‌اند.
    if (typeof GetFosoulPishnahadGhaymat !== 'function') {
        $('#zaribppStatus_' + OperationId).text('اسکریپت محاسبات پیشنهاد قیمت بارگذاری نشده است.');
        console.error('PishnahadGhaymatScript.js is not loaded.');
        return;
    }

    $('#zaribppStatus_' + OperationId).hide();
    GetFosoulPishnahadGhaymat();
}
