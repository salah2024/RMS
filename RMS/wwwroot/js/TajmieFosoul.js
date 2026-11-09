var openedRow = null;

var itemEditId = 0;


$(document).ready(function () {
    
    $("#cmbNoeFB").val("21");
    $("#cmbNoeFBInsert").val("21");

    LoadData();


    $("#cmbNoeFB").change(function () {

        $("#cmbNoeFBInsert").val($(this).val());

        LoadData();

    });

});


function GetNoeFBTitle(noeFB) {

    switch (noeFB) {

        case "RahDari":
            return "راهداری";

        case "Abnie":
            return "ابنیه";

        case "RahoBand":
            return "راه، راه‌آهن و باند فرودگاه";

        case "Barghi":
            return "تأسیسات برقی";

        case "Mekaniki":
            return "تأسیسات مکانیکی";

        case "MareMat":
            return "مرمت آثار تاریخی";

        default:
            return "";
    }

}


function BuildRow(i, item) {

    return `<tr class="parentRow" data-id="${item.id}" onclick="ToggleItems(this,${item.id})" >
    <td style="text-align:center">
        ${toPersianDigits(i + 1)}
    </td>

    <td style="text-align:center">

    ${toPersianDigits(item.faslCode)}

    </td>
<td style="text-align:center">

${item.title}

</td>

<td style="text-align:center">

${GetNoeFBTitle(item.noeFB)}
</td>

<td>

<button class="btn btn-warning btn-sm"

onclick="event.stopPropagation();Edit(${item.id})">

<i class="fa fa-edit"></i>

</button>

<button class="btn btn-danger btn-sm"

onclick="event.stopPropagation();Delete(${item.id})">

<i class="fa fa-trash"></i>

</button>

</td>

</tr >

        <tr

            class="child-row"

            id="child_${item.id}"

            style="display:none;">

            <td colspan="5" style="padding: 0px !important;">

                <div class="child-container">

                    <table

                        class="tblTajmieFosoulItems table table-bordered table-sm mb-0">

                        <thead>

                            <tr>

                                <th style="width:5%">

                                    کد آیتم

                                </th>
                                <th style="width:35%">

                                    شرح

                                </th>
                                <th style="width:15%">

                                    واحد

                                </th>

                                <th style="width:15%">

                                    نوع محاسبه

                                </th>

                                <th style="width:15%">

                                    آیتم اصلی

                                </th>

                                <th style="width:15%">

                                    عملیات

                                </th>

                            </tr>

                        </thead>

                        <tbody id="items_${item.id}">

                        </tbody>

                        <tfoot>

                            <tr>

                                <td style="width:5%">

                                    <input
                                        type="text"
                                        class="form-control"
                                        id="txtItem_${item.id}" />

                                </td>
                                <td style="width:35%">

                                    <input
                                        type="text"
                                        class="form-control"
                                        id="txtSharh_${item.id}" />

                                </td>
                                <td style="width:15%">

                                    <input
                                        type="text"
                                        class="form-control"
                                        id="txtVahed_${item.id}" />

                                </td>

                                <td style="width:15%">

                                    <select
                                        class="form-control"
                                        id="cmbType_${item.id}">

                                        <option value="1">دستی</option>

                                        <option value="2">از آیتم</option>

                                        <option value="3">محاسباتی</option>

                                    </select>

                                </td>

                                <td style="width:15%">

                                    <input
                                        type="text"
                                        class="form-control"
                                        id="txtMainItem_${item.id}" />

                                </td>

                                   <td style="width:15%">

<div class="d-flex gap-1">

<button
id="btnSave_${item.id}"
class="btn btn-success btn-success-TajmieItem btn-sm"
onclick="SaveItem(${item.id})">

<i class="fa fa-plus"></i>

ذخیره آیتم تجمیعی

</button>

<button
id="btnCancel_${item.id}"
class="btn btn-secondary btn-sm"
style="display:none"
onclick="CancelItem(${item.id})">

<i class="fa fa-times"></i>

انصراف

</button>

</div>

</td>


                            </tr>

                        </tfoot>

                    </table>

                </div>

            </td>

        </tr>

`;


}


function ToggleItems(row, id) {

    $(".parentRow")
        .removeClass("parent-selected");

    $(row)
        .addClass("parent-selected");

    if (openedRow != null &&
        openedRow != id) {

        $("#child_" + openedRow).hide();

    }

    if ($("#child_" + id).is(":visible")) {

        $("#child_" + id).hide();

        openedRow = null;

        return;

    }

    $("#child_" + id).show();

    openedRow = id;

    itemEditId = 0;

    ClearItem(id);

    LoadItems(id);

}




function LoadItems(id) {

    $.ajax({

        url: "/TajmieFosoul/GetItems",

        type: "GET",

        data: {
            tajmieId: id
        },

        success: function (data) {

            var html = "";

            $.each(data, function (i, item) {

                html += `

<tr class="tajmieItemRow"

    data-id="${item.id}"

    onclick="ToggleFBItems(this,${item.id})">

<td style="width:5%;text-align:center">

${toPersianDigits(item.itemCode)}

</td>
<td style="width:35%;text-align:center">

${item.sharh}

</td>
<td style="width:15%;text-align:center">

${item.vahed}

</td>

<td style="width:15%;text-align:center">

${item.calculateValueTypeText}

</td>

<td style="width:15%;text-align:center">

${item.mainItem ?? ""}

</td>

<td style="width:15%;text-align:center">

<button
class="btn btn-warning btn-sm"
onclick="event.stopPropagation();EditItem(${item.id},${id})">

<i class="fa fa-edit"></i>

</button>

<button
class="btn btn-danger btn-sm"
onclick="event.stopPropagation();DeleteItem(${item.id},${id})">

<i class="fa fa-trash"></i>

</button>

</td>

</tr>



<tr id="fbChild_${item.id}"
    class="fbChildRow"
    style="display:none;">

    <td colspan="6">

        <div class="fb-container">

            <table class="tblTajmieFBItems table table-bordered table-sm">

                <thead>

                    <tr>

                        <th width="120">

                            سال

                        </th>

                        <th>

                            شماره آیتم فهرست بها

                        </th>

                        <th width="120">

                            عملیات

                        </th>

                    </tr>

                </thead>

                <tbody id="fbItems_${item.id}">

                </tbody>

                <tfoot>

                    <tr>

                        <td>

                            <input

                                id="txtYear_${item.id}"

                                class="form-control"/>

                        </td>

                        <td>

                            <input

                                id="txtFBItem_${item.id}"

                                class="form-control"/>

                        </td>

                        <td>

                            <button

id="btnSaveFB_${item.id}"

class="btn btn-success btn-success-Tajmie btn-sm"

onclick="SaveFBItem(${item.id})">

ذخیره آیتم فهرست بها

</button>

<button

id="btnCancelFB_${item.id}"

class="btn btn-secondary btn-sm"

style="display:none"

onclick="CancelFBItem(${item.id})">

انصراف

</button>

                        </td>

                    </tr>

                </tfoot>

            </table>

        </div>

    </td>

</tr>


`;
            });

            $("#items_" + id).html(html);

        }

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

    $.ajax({

        url: "/TajmieFosoul/GetAllTajmieFosoul",

        type: "GET",

        data: {

            noeFBId: $("#cmbNoeFB").val()

        },

        success: function (data) {

            var html = "";

            $.each(data, function (i, item) {

                html += BuildRow(i, item);

            });


            html += BuildInsertRow();

            $("#tblTajmieBody").html(html);

            $("#cmbNoeFBInsert").val($("#cmbNoeFB").val());

        }

    });

}



function BuildInsertRow() {

    return `

<tr class="table-warning">

    <td>


    </td>

    <td>

        <input id="txtFaslCode"
               class="form-control"
               placeholder="کد فصل" />

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

        faslCode: $("#txtFaslCode").val().trim(),

        title: $("#txtTitle").val().trim(),

        noeFBId: parseInt($("#cmbNoeFB").val())

    };

    if (model.title == "") {

        toastr.warning("عنوان فصل را وارد نمایید.");

        $("#txtFaslCode").focus();

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

    $("#txtFaslCode").val("");

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










////////////////////////
//////TajmieFBItems
/////////////////////////


var fbItemEditId = '00000000-0000-0000-0000-000000000000';
var openedFBRow = null;

function ToggleFBItems(row, id) {

    $(".tajmieItemRow")
        .removeClass("table-info");

    $(row)
        .addClass("table-info");

    if (openedFBRow != null &&
        openedFBRow != id) {

        $("#fbChild_" + openedFBRow).hide();

    }

    if ($("#fbChild_" + id).is(":visible")) {

        $("#fbChild_" + id).hide();

        openedFBRow = null;

        return;
    }

    $("#fbChild_" + id).show();

    openedFBRow = id;

    LoadFBItems(id);

}

function LoadFBItems(id) {

    $.get(

        "/TajmieFosoul/GetFBItems",

        { tajmieItemId: id },

        function (data) {

            var html = "";

            $.each(data, function (i, item) {

                html += `

<tr>

<td>

${item.year}

</td>

<td>

${item.itemFBShomareh}

</td>

<td>
<button

class="btn btn-warning btn-sm"

onclick="EditFBItem('${item.id}',${id})">

<i class="fa fa-edit"></i>

</button>

<button

class="btn btn-danger btn-sm"

onclick="DeleteFBItem('${item.id}',${id})">

حذف

</button>

</td>

</tr>

`;

            });

            $("#fbItems_" + id).html(html);

        });

}


function SaveFBItem(parentId) {

    debugger;
    var model = {

        ID: fbItemEditId,

        tajmieFosoulItemId: parseInt(parentId),

        year: parseInt($("#txtYear_" + parentId).val()),

        itemFBShomareh: $("#txtFBItem_" + parentId).val()

    };

    var url =

        fbItemEditId == '00000000-0000-0000-0000-000000000000'

            ? "/TajmieFosoul/SaveFBItem"

            : "/TajmieFosoul/UpdateFBItem";

    $.ajax({

        url: url,

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            fbItemEditId = '00000000-0000-0000-0000-000000000000';

            CancelFBItem(parentId);

            LoadFBItems(parentId);

        }

    });

}


function DeleteFBItem(id, parentId) {

    //if (!confirm("حذف شود؟"))
    //    return;

    $.post(

        "/TajmieFosoul/DeleteFBItem",

        { id: id },

        function () {

            LoadFBItems(parentId);

        });

}


function EditFBItem(id, parentId) {

    $.get("/TajmieFosoul/GetFBItem",
        { id: id },
        function (item) {

            fbItemEditId = item.id;

            $("#txtYear_" + parentId).val(item.year);

            $("#txtFBItem_" + parentId).val(item.itemFBShomareh);

            $("#btnSaveFB_" + parentId)

                .removeClass("btn-success")

                .addClass("btn-warning")

                .html("<i class='fa fa-edit'></i> ویرایش");

            $("#btnCancelFB_" + parentId).show();

        });

}



function CancelFBItem(parentId) {

    fbItemEditId = '00000000-0000-0000-0000-000000000000';

    $("#txtYear_" + parentId).val("");

    $("#txtFBItem_" + parentId).val("");

    $("#btnSaveFB_" + parentId)

        .removeClass("btn-warning")

        .addClass("btn-success")

        .html("<i class='fa fa-plus'></i> ذخیره آیتم فهرست بها");

    $("#btnCancelFB_" + parentId).hide();

}





////////////////////////
//////end TajmieFBItems
/////////////////////////

