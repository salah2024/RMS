var openedRow = null;

var itemEditId = 0;


$(document).ready(function () {
    GetViewBag();
});

function toEnglishDigits(value) {

    if (value == null)
        return "";

    return value.toString()
        .replace(/[۰-۹]/g, function (d) {
            return "۰۱۲۳۴۵۶۷۸۹".indexOf(d);
        })
        .replace(/[٠-٩]/g, function (d) {
            return "٠١٢٣٤٥٦٧٨٩".indexOf(d);
        });
}

function GetViewBag() {
    $.ajax({
        type: "Get",
        url: "/BaseInfo/GetViewBag",
        data: JSON.stringify(''),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            debugger;
            viewBag = response.viewBag;
            lstNoeFBs = response.lstNoeFBs;

            $('#HDFBarAvordUserID').val(viewBag.baId);

            $('#spanProjectTitle').html(response.title);

            let str = "";
            let selectedId = null;


            $('#HDFNoeFB').val('21');
            $('#HDFYear').val(viewBag.year);
            // $('#divFB').html(str);

            LoadData();
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

// $(document).ready(function () {
    
//     LoadData();

// });

function BuildRow(i, item) {

    debugger;

    return `<tr class="parentRow" data-id="${item.faslCode}" onclick="ToggleItems(this,'${item.faslCode}')" >
    
    <td style="text-align:center">

        ${toPersianDigits(i + 1)}

   </td>

    <td style="text-align:center">

        ${toPersianDigits(item.faslCode)}

    </td>
    <td>

        ${item.title}

    </td>

<td style="text-align:center;">
    ${
        item.totalBahayehKol != null
            ? toPersianDigits(
                formatNumber(
                    Math.round(item.totalBahayehKol)
                )
            )
            : '0'
    }
</td>

<td style="text-align:center;">

    ${formatNumber(item.weightPercent)}

</td>

</tr >

        <tr

            class="child-row"

            id="child_${item.faslCode}"

            style="display:none;">

            <td colspan="5" style="padding: 0px !important;">

                <div class="child-container">

                    <table

                        class="tblTajmieFosoulItems table table-bordered table-sm mb-0" style="text-align:center;background-color: #c9c9ff;">

                        <thead>

                            <tr>

                                <th style="width:5%">

                                    کد آیتم

                                </th>
                                <th style="width:28%">

                                    شرح

                                </th>
                                <th style="width:12%">

                                    واحد

                                </th>

                                

                                <th style="width:11%">

                                    مقدار

                                </th>
                                <th style="width:15%">

                                    بهای واحد (ريال)

                                </th>
                                <th style="width:15%">

                                    بهای کل (ريال)

                                </th>
                                <th style="width:11%">

                                   درصد وزنی از پروژه

                                </th>

                            </tr>

                        </thead>

                        <tbody id="items_${item.faslCode}">

                        </tbody>

                        

                    </table>

                </div>

            </td>

        </tr>

`;


}


async function ToggleItems(row, FaslCode) {

    var childRow = $("#child_" + FaslCode);

    // اگر باز است، ببند
    if (childRow.is(":visible")) {

        childRow.hide();

        return;
    }

    try {

        // اطلاعات را بگیر
        var hasData = await LoadItems(FaslCode);

        // اگر اطلاعاتی وجود ندارد
        if (!hasData) {

            toastr.info('رکوردی جهت نمایش وجود ندارد', 'اطلاع');

            return;
        }

        // بستن سایر فصل‌ها
        $(".child-row").not(childRow).hide();

        // باز کردن فصل انتخاب شده
        childRow.show();

    }
    catch (error) {

        toastr.error('خطا در دریافت اطلاعات', 'خطا');
    }
}



function InsertUpdateBarAvordUserTajmie(input) {

    var tr = input.closest("tr");

    // -----------------------------
    // BarAvordUserId
    // -----------------------------
    var barAvordUserId = $('#HDFBarAvordUserID').val();

    // -----------------------------
    // TajmieFosoulItemId
    // -----------------------------
    var tajmieFosoulItemId = tr.attr("data-tajmie-item-id");

    // -----------------------------
    // مقدار
    // -----------------------------
    var meghdarText = input.val();

    var meghdar = parseFloat(
        toEnglishDigits(meghdarText)
            .replace(/,/g, '')
            .trim()
    );

    // -----------------------------
    // بهای کل
    // -----------------------------
    var bahayeKolText = tr.find(".hdn-bahaye-kol").val();

    var bahayeKol = parseFloat(
        toEnglishDigits(bahayeKolText)
            .replace(/,/g, '')
            .trim()
    );

    // -----------------------------
    // اعتبارسنجی
    // -----------------------------
    if (!tajmieFosoulItemId) {

        console.error("TajmieFosoulItemId یافت نشد.");

        return;
    }

    if (isNaN(meghdar) || meghdar <= 0) {

        toastr.info("مقدار وارد شده صحیح نیست.");

        input.focus();

        return;
    }

    if (isNaN(bahayeKol)) {

        toastr.info("بهای کل صحیح نیست.");

        return;
    }

    // -----------------------------
    // محاسبه بهای واحد
    // -----------------------------
    var bahayeVahed = Math.round(bahayeKol / meghdar);

    // -----------------------------
    // نمایش بهای واحد
    // -----------------------------
    tr.find(".txt-bahaye-vahed")
        .text(
            toPersianDigits(
                formatNumber(bahayeVahed)
            )
        );

    // -----------------------------
    // ارسال به سرور
    // -----------------------------
    var model = {

        BarAvordUserId: barAvordUserId,

        TajmieFosoulItemId: tajmieFosoulItemId,

        BahayeVahed: bahayeVahed,

        Meghdar: meghdar,

        BahayeKol: bahayeKol

    };

    $.ajax({

        url: "/BarAvordTajmie/InsertUpdateBarAvordUserTajmie",

        type: "POST",

        data: JSON.stringify(model),

        contentType: "application/json; charset=utf-8",

        success: function (result) {

            if (result.success) {

                console.log("اطلاعات با موفقیت ذخیره شد.");

            }
            else {

                alert(result.message || "خطا در ذخیره اطلاعات.");

            }

        },

        error: function (xhr) {

            console.error(xhr);

            alert("خطا در ارتباط با سرور.");

        }

    });
}


function CalculateAndSave(input) {
    var valueText = input.val();

    var meghdar = parseFloat(
        toEnglishDigits(valueText)
            .replace(/,/g, '')
            .trim()
    );

    if (isNaN(meghdar) || meghdar <= 0) {

        input.val("");

        input.closest("tr")
            .find(".txt-bahaye-vahed")
            .text("");

        return;
    }

    var tr = input.closest("tr");

    var bahayeKolText = tr
        .find(".hdn-bahaye-kol")
        .val();

    var bahayeKol = parseFloat(
        toEnglishDigits(bahayeKolText)
            .replace(/,/g, '')
            .trim()
    );

    if (isNaN(bahayeKol))
        return;

    // محاسبه بهای واحد بدون اعشار
    var bahayeVahed = Math.round(
        bahayeKol / meghdar
    );

    // نمایش مقدار
    input.val(
        toPersianDigits(
            formatNumber(meghdar)
        )
    );

    // نمایش بهای واحد
    tr.find(".txt-bahaye-vahed")
        .text(
            toPersianDigits(
                formatNumber(bahayeVahed)
            )
        );

    // ذخیره در دیتابیس
    InsertUpdateBarAvordUserTajmie(input);
}



function LoadItems(FaslCode) {

    return new Promise(function (resolve, reject) {

        var BarAvordId = $('#HDFBarAvordUserID').val();
        var NoeFBId = parseInt($('#HDFNoeFB').val());
        var Year = parseInt($('#HDFYear').val());

        var vardata = {
            BarAvordId: BarAvordId,
            NoeFB: NoeFBId,
            FaslCode: FaslCode,
            Year: Year
        };

        $.ajax({

            url: "/BarAvordTajmie/GetBaravordTajmie",

            type: "POST",

            data: JSON.stringify(vardata),

            contentType: "application/json; charset=utf-8",

            success: function (data) {

                // اگر رکوردی وجود ندارد
                if (!data || data.length === 0) {

                    resolve(false);

                    return;
                }

                var html = "";

                $.each(data, function (i, item) {

                    html += `
    <tr class="tajmie-item"
        data-itemcode="${item.itemCode}"
        data-tajmie-item-id="${item.tajmieFosoulId}"
        style="cursor:pointer;">

        <td style="text-align:center;">
            ${toPersianDigits(formatNumber(item.itemCode))}
        </td>

        <td style="text-align:right;">
            ${item.sharh}
        </td>

        <td style="text-align:center;">
            ${item.vahed}
        </td>

        <!-- مقدار -->
        <td style="text-align:center;">
            <input type="text"
                   class="form-control form-control-sm txt-tajmie-value"
                   style="text-align:center;"
                   autocomplete="off"
                   value="${item.meghdar ? toPersianDigits(formatNumber(item.meghdar)) : ''}"/>

        </td>
        <!-- بهای واحد -->
        <td style="text-align:center;">
            <span class="txt-bahaye-vahed">
                ${
                        item.bahayeVahed != null
                            ? toPersianDigits(
                                formatNumber(
                                    Math.round(item.bahayeVahed)
                                )
                            )
                            : ''
                }
            </span>
        </td>

        <!-- بهای کل -->
        <td style="text-align:center;">
            ${toPersianDigits(formatNumber(item.bahayehKol))}
            <!-- مقدار انگلیسی بهای کل -->
            <input type="hidden"
                   class="hdn-bahaye-kol"
                   value="${toEnglishDigits(item.bahayehKol)}" />

        </td>
        <td style="text-align:center;">
    ${
                        item.weightPercent != null
                            ? toPersianDigits(
                                formatNumber(item.weightPercent, 2)
                            )
                            : '0'
    }
</td>

    </tr>
`;
                });

                $("#items_" + FaslCode).html(html);

                $("#items_" + FaslCode)
                    .off("click", ".txt-tajmie-value")
                    .on("click", ".txt-tajmie-value", function (e) {

                        e.stopPropagation();

                    });

                $("#items_" + FaslCode)
                    .off("blur", ".txt-tajmie-value")
                    .on("blur", ".txt-tajmie-value", function () {

                        CalculateAndSave($(this));

                    });


                $("#items_" + FaslCode)
                    .off("keydown", ".txt-tajmie-value")
                    .on("keydown", ".txt-tajmie-value", function (e) {

                        if (e.key === "Enter" || e.keyCode === 13) {

                            e.preventDefault();

                            CalculateAndSave($(this));

                            // خارج شدن از input
                            //$(this).blur();
                        }

                    });

                // کلیک روی آیتم تجمیعی
                $("#items_" + FaslCode)
                    .off("click", ".tajmie-item")
                    .on("click", ".tajmie-item", function () {

                        var tr = $(this);

                        var currentDetail = tr.next(".tajmie-detail");

                        // بستن همه جزئیات
                        $("#items_" + FaslCode + " .tajmie-detail").remove();

                        // اگر همین آیتم باز بود، فقط ببند
                        if (currentDetail.length > 0) {
                            return;
                        }

                        var itemCode = tr.attr("data-itemcode");

                        var item = data.find(function (x) {
                            return x.itemCode == itemCode;
                        });

                        if (!item)
                            return;

                        var htmlFB = "";

                        $.each(item.items || [], function (j, fb) {

                            htmlFB += `
        <tr>

            <!-- شماره فهرست بها -->
            <td style="text-align:center;width:10%">
                ${fb.fbShomareh
                                    ? toPersianDigits(
                                        fb.fbShomareh
                                    )
                                    : ''
                                }
            </td>


            <!-- شرح -->
            <td style="text-align:right;width:40%">
                ${fb.fbSharh || ''}
            </td>


            <!-- بهای واحد -->
            <td style="text-align:center;width:10%">
                ${fb.bahayeVahed != null
                                    ? toPersianDigits(
                                        formatNumber(
                                            fb.bahayeVahed
                                        )
                                    )
                                    : ''
                                }
            </td>


            <!-- مقدار -->
            <td style="text-align:center;width:13%">
                ${fb.value != null
                                    ? toPersianDigits(
                                        formatNumber(
                                            fb.value
                                        )
                                    )
                                    : ''
                                }
            </td>


            <!-- بهای کل -->
            <td style="text-align:center;width:14%">

                <span>
                    ${fb.bahayeKolInMeghdar != null
                                    ? toPersianDigits(
                                        formatNumber(
                                            fb.bahayeKolInMeghdar
                                        )
                                    )
                                    : ''
                                }
                </span>

            </td>


            <!-- درصد وزنی -->
            <td style="text-align:center;width:12%">

                ${fb.weightPercent != null
                                    ? toPersianDigits(
                                        formatNumber(
                                            fb.weightPercent,
                                            2
                                        )
                                    )
                                    : '0'
                                }

            </td>

        </tr>
    `;
                        });

                        if (htmlFB !== "") {

                            var detailHtml = `
                                <tr class="tajmie-detail">

                                    <td colspan="7" style="padding:0;">

                                        <table class="tblTajmieFBItems table table-bordered"
                                               style="margin:0;background-color: #acd2dd;">

                                            <thead>

                                                <tr>

                                                    <th style="text-align:center;width:10%">
                                                        شماره فهرست بها
                                                    </th>

                                                    <th style="text-align:center;width:40%">
                                                        شرح
                                                    </th>

                                                    <th style="text-align:center;width:10%">
                                                        بهای واحد با ضرایب (ريال)
                                                    </th>

                                                    <th style="text-align:center;width:13%">
                                                        مقدار
                                                    </th>
                                                    <th style="text-align:center;width:14%">
                                                        بهای کل (ريال)
                                                    </th>
                                                    <th style="text-align:center;width:12%">
                                                        درصد وزنی از پروژه
                                                    </th>

                                                </tr>

                                            </thead>

                                            <tbody>
                                                ${htmlFB}
                                            </tbody>

                                        </table>

                                    </td>

                                </tr>
                            `;

                            tr.after(detailHtml);
                        }

                    });

                resolve(true);
            },

            error: function () {

                reject();
            }

        });

    });
}

function GetInsertRow() {

    return `
<tr class="table-warning">

    <td>

        <span>
            <i class="fa fa-plus">
        </span>

    </td>

    <td>

        <input id="txtTitleInsert"
               class="form-control"
               placeholder="عنوان فصل" />

    </td>

    <td>

        <select id="cmbNoeFBInsert"
                class="form-control">

            <option value="22">راهداری</option>
            <option value="11">ابنیه</option>
            <option value="21">راه، راه آهن و باند فرودگاه</option>
            <option value="13">تأسیسات برقی</option>
            <option value="12">تأسیسات مکانیکی</option>
            <option value="16">مرمت آثار تاریخی</option>

        </select>

    </td>

    <td>

        <button class="btn btn-success"
                onclick="Save()">

            <i class="fa fa-plus"></i>

            ثبت

        </button>

        <button class="btn btn-secondary"
                onclick="ClearNew()">

            انصراف

        </button>

    </td>

</tr>

`;

}


function LoadData() {

    var BarAvordId = $('#HDFBarAvordUserID').val();
    var NoeFB = parseInt($('#HDFNoeFB').val());
    var Year = parseInt($('#HDFYear').val());


    $.ajax({

        url: "/TajmieFosoul/GetAll",

        type: "GET",

        data: {
            year: Year,

            barAvordId: BarAvordId,

            noeFBId: NoeFB

        },

        success: function (data) {

            var html = "";

            $.each(data, function (i, item) {

                html += BuildRow(i, item);

            });

            //html += BuildInsertRow();

            $("#tblTajmieBody").html(html);

        }

    });

}



function BuildInsertRow() {

    return `

<tr class="table-warning">

    <td>


    </td>

    <td>

        <input id="txtTitle"
               class="form-control"
               placeholder="عنوان فصل" />

    </td>

    <td>

        <select id="cmbNoeFBInsert"
                class="form-control">

            <option value="22">راهداری</option>
            <option value="11">ابنیه</option>
            <option value="21">راه، راه‌آهن و باند فرودگاه</option>
            <option value="13">تأسیسات برقی</option>
            <option value="12">تأسیسات مکانیکی</option>
            <option value="16">مرمت آثار تاریخی</option>

        </select>

    </td>

    <td>

        <button id="btnSave"
                class="btn btn-success btn-sm"
                onclick="Save()">

            <i class="fa fa-plus"></i>

            ذخیره فصل

        </button>

        <button id="btnCancel"
                class="btn btn-secondary btn-sm"
                style="display:none"
                onclick="Cancel()">

            <i class="fa fa-times"></i>

            انصراف

        </button>

    </td>

</tr>

`;

}

function Save() {

    debugger;

    var model = {

        id: $("#hfId").val(),

        title: $("#txtTitle").val().trim(),

        noeFBId: parseInt($("#cmbNoeFB").val())

    };

    if (model.title == "") {

        toastr.warning("عنوان فصل را وارد نمایید.");

        $("#txtTitle").focus();

        return;

    }

    if ($("#cmbNoeFB").val() == "") {

        toastr.warning("نوع فهرست بها را انتخاب نمایید.");

        $("#cmbNoeFB").focus();

        return;

    }

    var url =
        model.id == 0
            ? "/TajmieFosoul/Save"
            : "/TajmieFosoul/Update";

    $.ajax({

        url: url,

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            toastr.success("اطلاعات با موفقیت ثبت شد.");

            Clear();

            LoadData();

        },

        error: function (xhr) {

            if (xhr.status == 400) {

                toastr.warning(xhr.responseJSON.message);

            }
            else {

                toastr.error("خطایی در ثبت اطلاعات رخ داد.");

            }

        }

    });

}



function Edit(id) {

    $.get(

        "/TajmieFosoul/Get",

        {
            id: id
        },

        function (item) {

            $("#hfId").val(item.id);

            $("#txtTitle").val(item.title);

            $("#cmbNoeFB").val(item.noeFBId);

            $("#cmbNoeFBInsert").val(item.noeFBId);

            $("#btnSave")

                .removeClass("btn-success")

                .addClass("btn-warning")

                .html("<i class='fa fa-edit'></i> ویرایش");

                $("#btnCancel")

                .show();

        });

}



function Cancel() {

    $("#hfId").val(0);
    $("#txtTitle").val("");

    $("#cmbNoeFB")
        .val($("#cmbNoeFB").val());

    $("#btnSave")

        .removeClass("btn-warning")

        .addClass("btn-success")

        .html("<i class='fa fa-plus'></i> ذخیره فصل");

    $("#btnCancel").hide();

}



function Delete(id) {

    // if (!confirm("حذف شود؟"))
    //     return;

    $.post(

        "/TajmieFosoul/Delete",

        {
            id: id
        },

        function () {

            toastr.success("حذف شد");

            LoadData();

        })

        .fail(function (xhr) {

            toastr.warning(xhr.responseJSON.message);

        });

}


function Clear() {

    $("#hfId").val(0);

    $("#txtTitle").val("");

    //$("#cmbNoeFB").prop("selectedIndex", 0);

    $("#btnSave")
        .removeClass("btn-warning")
        .addClass("btn-success")
        .html("<i class='fa fa-plus'></i> ثبت");

    $("#btnCancel").hide();

}



////////////
/////// TajmieFosoulItems
////////////

function SaveItem(parentId) {

    debugger;

    var model = {

        id: itemEditId,

        tajmieFosoulId: parentId,

        itemCode: $("#txtItem_" + parentId).val(),
        sharh: $("#txtSharh_" + parentId).val(),
        vahed: $("#txtVahed_" + parentId).val(),

        calculateValueType:

           parseInt($("#cmbType_" + parentId).val()),

        mainItem:

            $("#txtMainItem_" + parentId).val()

    };

    var url =

        itemEditId == 0 ?

            "/TajmieFosoul/SaveItem"

            :

            "/TajmieFosoul/UpdateItem";

    $.ajax({

        url: url,

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            toastr.success("ثبت شد");

            itemEditId = 0;

            LoadItems(parentId);

            ClearItem(parentId);

        },

        error: function (xhr) {

            toastr.warning(xhr.responseJSON.message);

        }

    });

}


function EditItem(id, parentId) {

    $.get(

        "/TajmieFosoul/GetItem",

        { id: id },

        function (item) {

            itemEditId = item.id;

            $("#txtItem_" + parentId)

                .val(item.itemCode);

            $("#cmbType_" + parentId)

                .val(item.calculateValueType);

            $("#txtMainItem_" + parentId)

                .val(item.mainItem);

            $("#btnSave_" + parentId)

                .removeClass("btn-success")

                .addClass("btn-warning")

                .html("<i class='fa fa-edit'></i> ویرایش");

            $("#btnCancel_" + parentId)

                .show();

        });

}



function CancelItem(parentId) {

    itemEditId = 0;

    ClearItem(parentId);

}


function ClearItem(parentId) {

    $("#txtItem_" + parentId).val("");
    $("#txtSharh_" + parentId).val("");

    $("#txtMainItem_" + parentId).val("");

    $("#cmbType_" + parentId).val("1");

    $("#btnSave_" + parentId)

        .removeClass("btn-warning")

        .addClass("btn-success")

        .text("ذخیره آیتم تجمیعی");

    $("#btnCancel_" + parentId).hide();

}


function DeleteItem(id, parentId) {

    //if (!confirm("حذف شود؟"))

     //   return;

    $.post(

        "/TajmieFosoul/DeleteItem",

        { id: id },

        function () {

            toastr.success("حذف شد");

            LoadItems(parentId);

        });

}

