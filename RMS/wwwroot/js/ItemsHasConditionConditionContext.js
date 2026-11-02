var selectedGroupId = 0;
var selectedConditionId = 0;

var groupEditId = 0;
var conditionEditId = 0;


$(document).ready(function () {

    $("#cmbYear").val("1404");
    $("#cmbNoeFB").val("21");

    // رویدادها را اول ثبت کن
    $("#cmbNoeFB").change(function () {

        LoadItems(true);
        LoadGroups();

    });

    $("#cmbConditionType").change(function () {

        LoadItems(true);
        LoadGroups();

    });

    $("#cmbYear").change(function () {

        LoadItems(true);

    });

    $("#cmbItem").change(function () {

        LoadData(1);
        ClearForm();

    });

    $("#btnSave").click(function () {

        Save();

    });

    let timer;

    $("#txtSearch").keyup(function () {

        clearTimeout(timer);

        timer = setTimeout(function () {

            LoadData(1);

        }, 400);

    });


    // توابع اولیه
    LoadGroups();
    BindGroupSearch();

    // مهم: آخر از همه LoadItems
    LoadItems(true);

   
});

function LoadItems(firstLoad = false, callback = null, selectedItem = null) {

    $.ajax({
        url: "/ItemsHasConditionConditionContext/GetItems",
        type: "GET",
        data: {
            year: parseInt($("#cmbYear").val()),
            noeFB: parseInt($("#cmbNoeFB").val())
        },

        success: function (data) {

            const $cmb = $("#cmbItem");

            // حذف Select2 قبلی
            if ($cmb.hasClass("select2-hidden-accessible")) {
                $cmb.select2("destroy");
            }

            // خالی کردن
            $cmb.empty();

            // افزودن آیتم‌ها
            $.each(data, function (i, item) {

                $cmb.append(
                    $("<option>")
                        .val(item.id)
                        .text(item.text)
                );

            });

            $cmb.select2({
                width: "100%",
                dir: "rtl",
                placeholder: "انتخاب آیتم",
                allowClear: true,

                language: {
                    noResults: function () {
                        return "آیتمی پیدا نشد";
                    },
                    searching: function () {
                        return "در حال جستجو...";
                    }
                }
            });

           

            // ==================================================
            // انتخاب آیتم
            // ==================================================

            if (selectedItem !== null &&
                selectedItem !== undefined &&
                selectedItem !== "") {

                $cmb.val(String(selectedItem)).trigger("change");

            }
            else if (data.length > 0) {

                //$cmb.val(String(data[0].id)).trigger("change");

                $cmb.val(data[0].id);


            }


            // ==================================================
            // Callback
            // ==================================================

            if (typeof callback === "function") {
                callback();
            }


            // ==================================================
            // LoadData
            // ==================================================

            if (firstLoad) {
                LoadData(1);
            }

        },

        error: function (xhr, status, error) {

            console.error("GetItems Error:", error);

        }
    });
}

function LoadConditions(selectedId = null, callback = null) {

    $.ajax({

        url: "/ItemsHasConditionConditionContext/GetConditions",

        type: "GET",

        data: {

            groupId: selectedGroupId,

            year: parseInt($("#cmbYear").val()),

            noeFB: parseInt($("#cmbNoeFB").val())

        },

        success: function (data) {

            var html = "";

            $.each(data, function (i, item) {

                html += `

<tr class="conditionRow"

data-id="${item.id}"

onclick="SelectCondition(this,${item.id})">


<td style="text-wrap: auto;width:90%">

${item.text}

</td>

<td style="width:10%">

<button
class="btn btn-warning btn-sm"

onclick="event.stopPropagation();EditCondition(${item.id})">

<i class="fa fa-edit"></i>

</button>

<button
class="btn btn-danger btn-sm"

onclick="event.stopPropagation();DeleteCondition(${item.id})">

<i class="fa fa-trash"></i>

</button>

</td>

</tr>

`;

            });

            $("#tblConditionBody").html(html);

            if (data.length > 0) {

                if (selectedId != null) {

                    var row = $("#tblConditionBody tr[data-id='" + selectedId + "']");

                    if (row.length) {

                        SelectCondition(row[0], selectedId);
                    }

                }
                else {

                    SelectCondition($("#tblConditionBody tr:first")[0], data[0].id);

                }

            }

            if (callback)
                callback();

        }

    });

}

function SelectCondition(row, id) {

    $(".conditionRow").removeClass("selected-row");

    $(row).addClass("selected-row");

    selectedConditionId = id;

    // بروزرسانی جدول اصلی
    LoadData(1);

    ViewItemsUseCondition(id);
}


function ViewItemsUseCondition(id) {
    $.ajax({

        url: "/ItemsHasConditionConditionContext/GetItemsUseCondition",

        type: "GET",

        data: {

            ConditionContextId: id,

        },

        success: function (data) {
            debugger;

            var ItemShomareh = "";

            $.each(data.lstShomarehUse, function (i, item) {

                ItemShomareh += item.itemFBShomareh+"_";

            });

            $('#divItemsUseForCondition').html(" شماره آیتم: "+ ItemShomareh);

            $('#divConditionType').html("شرط شماره: " + data.conditionType)
        }

    });

}



function LoadData(page = 1) {

    $.ajax({

        url: "/ItemsHasConditionConditionContext/GetAll",

        type: "GET",

        data: {

            year: parseInt($("#cmbYear").val()),

            noeFB: parseInt($("#cmbNoeFB").val()),

            itemFBShomareh: $("#cmbItem option:selected").text().trim(),

            search: $("#txtSearch").val(),

            page: page

        },

        success: function (result) {

            BuildTable(result.items);

            BuildPager(
                result.page,
                result.totalPages
            );
        }
    });
}

function BuildTable(data) {

    let rows = "";

    $.each(data, function (index, item) {
        debugger;
        rows += `

        <tr class="main-row"
            data-id="${item.id}"
            onclick="ToggleDetail(this,${item.id})">

    <td style="white-space: normal;word-break: break-word;">${item.conditionContextText}</td>

    <td>${item.ihC_CC_Related ?? ""}</td>

    <td>${item.enteringCount ?? 0}</td>

    <td>${item.desForEnteringValue ?? ""}</td>

    <td>${item.defaultValue ?? ""}</td>

    <td>${item.minValue ?? ""}</td>

    <td>${item.maxValue ?? ""}</td>

    <td>${item.stepChange ?? ""}</td>

    <td>
        ${item.viewCheckAllRecords ? "✔" : ""}
    </td>
    <td>
        ${item.autoSelecting ? "✔" : ""}
    </td>

    <td>
        ${item.enableEditing ? "✔" : ""}
    </td>

    <td>
        ${item.enableDeleting ? "✔" : ""}
    </td>

    <td>

        <button class="btn btn-sm btn-warning"
                onclick="event.stopPropagation();Edit(${item.id})">

            <i class="fa fa-edit"></i>

        </button>

        <button class="btn btn-sm btn-danger"
                onclick="event.stopPropagation();Delete(${item.id})">

            <i class="fa fa-trash"></i>

        </button>

    </td>

</tr>

<tr class="detail-row" style="display:none;">

<td colspan="16">

<div id="detail_${item.id}">

</div>

</td>

</tr>
        `;
    });

    $("#tblBody").html(rows);

}

/////
///group section
//////////

function LoadGroups(selectedId = null, callback = null) {
    $.ajax({

        url: "/ItemsHasConditionConditionContext/GetGroups",

        type: "GET",

        data: {
            ConditionType: parseInt($("#cmbConditionType").val()),
            noeFB: parseInt($("#cmbNoeFB").val()),
            Year: parseInt($("#cmbYear").val()),

        },


        success: function (data) {

            var html = "";

            $.each(data, function (i, item) {

                html += `

                    <tr class="groupRow"
                            data-id="${item.id}"
                            onclick="SelectGroup(this,${item.id})">

                    <td style="width:80%">
                    <span class="wrap-text">
                        ${item.text}
                        </span>
                    </td>
                    <td style="width:10%">
                    ${item.order}
                    </td>
<td style="width:10%">

<button
class="btn btn-warning btn-sm"
onclick="event.stopPropagation();EditGroup(${item.id})">

<i class="fa fa-edit"></i>

</button>

<button
class="btn btn-danger btn-sm"
onclick="event.stopPropagation();DeleteGroup(${item.id})">

<i class="fa fa-trash"></i>

</button>

</td>

</tr>

`;

            });

            $("#tblConditionGroupBody").html(html);

            if (data.length > 0) {

                if (selectedId != null) {

                    var row = $("#tblConditionGroupBody tr[data-id='" + selectedId + "']");

                    if (row.length) {

                        SelectGroup(row[0], selectedId, callback);
                        return;
                    }
                }

                SelectGroup($("#tblConditionGroupBody tr:first")[0], data[0].id, callback);

            }

            if (callback)
                callback();

        }

    });

}


function SelectGroup(row, id, callback = null) {

    $(".groupRow").removeClass("selected-row");

    $(row).addClass("selected-row");

    selectedGroupId = id;

    // پاک کردن شرط انتخاب شده قبلی
    selectedConditionId = 0;

    // بارگذاری شرط های این گروه
    LoadConditions(null, callback);

}

function SaveGroup() {

    var order = $("#txtOrderGroup").val().trim();
    var title = $("#txtNewGroup").val().trim();

    if (title == "") {

        toastr.warning("عنوان گروه شرط را وارد کنید.");

        return;
    }

    var model = {

        id: groupEditId,

        order: order,

        conditionGroupName: title,

        noeFB: parseInt($("#cmbNoeFB").val()),

    };

    $.ajax({

        url: groupEditId == 0
            ? "/ConditionGroup/Save"
            : "/ConditionGroup/Update",

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            toastr.success(
                groupEditId == 0
                    ? "گروه شرط ثبت شد."
                    : "گروه شرط ویرایش شد.");

            groupEditId = 0;

            $("#txtNewGroup").val("");
            $("#txtOrderGroup").val("");

            $("#btnSaveGroup")
                .removeClass("btn-warning")
                .addClass("btn-success")
                .html("<i class='fa fa-plus'></i>");

            $("#btnCancelGroup1").hide();

            LoadGroups();

        },

        error: function (xhr) {

            if (xhr.status == 400 && xhr.responseJSON) {

                toastr.warning(xhr.responseJSON.message);

            }
            else {

                toastr.error("خطا در ثبت اطلاعات");

            }

        }

    });

}

function EditGroup(id) {

    $.get("/ConditionGroup/Get",
        { id: id },
        function (item) {

            groupEditId = item.id;

            $("#txtNewGroup").val(item.conditionGroupName);
            $("#txtOrderGroup").val(item.order);

            $("#btnSaveGroup")
                .removeClass("btn-success")
                .addClass("btn-warning")
                .html("<i class='fa fa-edit'></i>");

            $("#btnCancelGroup1").show();

        });

}

function DeleteGroup(id) {

    // if (!confirm("آیا از حذف این گروه شرط اطمینان دارید؟"))
    //     return;

    $.ajax({

        url: "/ConditionGroup/Delete",

        type: "POST",

        data: {
            id: id
        },

        success: function () {

            toastr.success("گروه شرط حذف شد.");

            LoadGroups();

        },

        error: function (xhr) {

            if (xhr.status == 400 && xhr.responseJSON) {

                toastr.warning(xhr.responseJSON.message);

            }
            else if (xhr.status == 404) {

                toastr.error("رکورد یافت نشد.");

            }
            else {

                toastr.error("خطا در حذف اطلاعات.");

            }

        }

    });

}


function CancelGroup() {

    groupEditId = 0;

    $("#txtNewGroup").val("");

    $("#btnSaveGroup")
        .removeClass("btn-warning")
        .addClass("btn-success")
        .html("<i class='fa fa-plus'></i>");

    $("#btnCancelGroup1").hide();

}
////end group section
//////////


/////
///Condition section
//////////
var conditionEditId = 0;

function SaveCondition() {

    var text = $("#txtNewCondition").val().trim();

    if (text == "") {

        toastr.warning("عنوان شرط را وارد کنید.");

        return;
    }

    if (selectedGroupId == 0) {

        toastr.warning("ابتدا یک گروه شرط را انتخاب کنید.");

        return;
    }

    var model = {

        id: conditionEditId,

        context: text,

        conditionGroupId: selectedGroupId,

        year: parseInt($("#cmbYear").val()),

        noeFB: parseInt($("#cmbNoeFB").val())

    };

    $.ajax({

        url: conditionEditId == 0
            ? "/ConditionContextBase/Save"
            : "/ConditionContextBase/Update",

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            toastr.success(
                conditionEditId == 0
                    ? "شرط ثبت شد."
                    : "شرط ویرایش شد.");

            CancelCondition();

            LoadConditions();

        },

        error: function (xhr) {

            if (xhr.status == 400 && xhr.responseJSON) {

                toastr.warning(xhr.responseJSON.message);

            }
            else {

                toastr.error("خطا در ثبت اطلاعات");

            }

        }

    });

}



function EditCondition(id) {

    $.get("/ConditionContextBase/Get",
        { id: id },
        function (item) {

            conditionEditId = item.id;

            // انتخاب سطر در جدول
            $(".conditionRow").removeClass("selected-row");

            var row = $("#tblConditionBody tr[data-id='" + item.id + "']");

            row.addClass("selected-row");

            selectedConditionId = item.id;

            // مقدار متن
            $("#txtNewCondition").val(item.context);

            // تغییر دکمه ثبت به ویرایش
            $("#btnSaveCondition")
                .removeClass("btn-success")
                .addClass("btn-warning")
                .html("<i class='fa fa-edit'></i> ویرایش");

            // نمایش دکمه انصراف
            $("#btnCancelCondition").show();

        });

}


function DeleteCondition(id) {

    // if (!confirm("آیا از حذف این شرط اطمینان دارید؟"))
    //     return;

    $.ajax({

        url: "/ConditionContextBase/Delete",

        type: "POST",

        data: {
            id: id
        },

        success: function () {

            toastr.success("شرط با موفقیت حذف شد.");

            // اگر رکورد انتخاب شده حذف شد
            if (selectedConditionId == id)
                selectedConditionId = 0;

            CancelCondition();

            LoadConditions();

        },

        error: function (xhr) {

            if (xhr.status == 400 && xhr.responseJSON) {

                toastr.warning(xhr.responseJSON.message);

            }
            else if (xhr.status == 404) {

                toastr.error("رکورد یافت نشد.");

            }
            else {

                toastr.error("خطا در حذف اطلاعات.");

            }

        }

    });

}


function CancelCondition() {

    conditionEditId = 0;

    $("#txtNewCondition").val("");

    $("#btnSaveCondition")
        .removeClass("btn-warning")
        .addClass("btn-success")
        .html("<i class='fa fa-plus'></i>");

    $("#btnCancelCondition").hide();

}














/////
///end Condition section
//////////


function Save() {

    var model = {

        id: $("#hfId").val(),

        itemsHasConditionId: $("#cmbItem").val(),

        conditionContextId:
            selectedConditionId,

        hasEnteringValue:
            parseInt($("#txtEnteringCount").val() || 0) > 0,

        des:
            $("#txtDes").val(),

        defaultValue:
            $("#txtDefaultValue").val(),

        minValue:
            $("#txtMinValue").val(),

        maxValue:
            $("#txtMaxValue").val(),


        stepChange:
            $("#txtStepChange").val(),

        isShow:true,

        moveToRel: false,


        year:
           parseInt($("#cmbYear").val()),
        noeFB:
           parseInt($("#cmbNoeFB").val()),

        enteringCount:
            $("#txtEnteringCount").val() == '' ? 0 : $("#txtEnteringCount").val(),
        desForEnteringValue:
            $("#txtDesForEnteringValue").val(),

        enableEditing:
            $("#chkEnableEditing").is(":checked"),

        enableDeleting:
            $("#chkEnableDeleting").is(":checked"),

        checkAll:
            $("#chkCheckAll").is(":checked"),

        autoSelecting:
            $("#chkAutoSelecting").is(":checked"),

        viewCheckAllRecords:
            $("#chkCheckAll").is(":checked"),

        ihc_CC_Related:
            $("#txtIHC_CC_Related").val()


    };

    let url = "/ItemsHasConditionConditionContext/Save";

    if (parseInt(model.id) > 0)

        url = "/ItemsHasConditionConditionContext/Update";


    $.ajax({

        url: url,

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            toastr.success("اطلاعات ثبت شد");

            ClearForm();

            LoadData();
        },


        error: function (xhr) {
            if (xhr.status === 400) {

                toastr.warning(xhr.responseText);

            }
            else {

                toastr.error("خطا در ثبت اطلاعات");

            }

        }

    });

}


function Edit(id) {


    $.ajax({

        url: "/ItemsHasConditionConditionContext/Get?id=" + id,

        type: "GET",

        success: function (item) {


            $("#hfId").val(item.id);


            $("#cmbYear").val(item.year);

            $("#cmbNoeFB").val(item.noeFB);



            // ابتدا آیتم ها

            LoadItems(false, function () {


                $("#cmbItem")
                    .val(item.itemsHasConditionId);



            });




            // گروه شرط

            LoadGroups(item.conditionGroupId, function () {

                LoadConditions(item.conditionContextId);

            });
            //////////////////
            /////////////////

            $("#chkHasEnteringValue")
                .prop("checked",
                    item.hasEnteringValue);



            $("#chkAutoSelecting")
                .prop("checked",
                    item.autoSelecting);

            $("#chkCheckAll")
                .prop("checked",
                    item.viewCheckAllRecords);



            $("#txtDefaultValue")
                .val(item.defaultValue);


            $("#txtMinValue")
                .val(item.minValue);


            $("#txtMaxValue")
                .val(item.maxValue);


            $("#txtStepChange")
                .val(item.stepChange);


            $("#txtEnteringCount")
                .val(item.enteringCount);


            $("#txtDesForEnteringValue")
                .val(item.desForEnteringValue);

            $("#txtDes")
                .val(item.des);

            $("#chkEnableEditing").prop(
                "checked",
                item.enableEditing === true
            );

            $("#chkEnableDeleting").prop(
                "checked",
                item.enableDeleting === true
            );

            $("#txtIHC_CC_Related")
                .val(item.ihC_CC_Related);

        }

    });

}


function BindGroupSearch() {

    $("#txtSearchGroup").keyup(function () {

        var value = $(this).val().toLowerCase();

        $("#tblConditionGroupBody tr").filter(function () {

            $(this).toggle(

                $(this).text()

                    .toLowerCase()

                    .indexOf(value) > -1

            );

        });

    });

}
function Delete(id) {


    // if (!confirm("آیا از حذف این رکورد اطمینان دارید؟"))

    //     return;



    $.ajax({


        url:
            "/ItemsHasConditionConditionContext/Delete?id=" + id,



        type: "POST",



        success: function () {


            toastr.success("حذف انجام شد");


            LoadData();


        },


        error: function () {


            toastr.error("خطا در حذف");


        }


    });



}

function ClearForm() {


    $("#hfId").val(0);



    $("#chkHasEnteringValue")
        .prop("checked", false);



    $("#chkIsShow")
        .prop("checked", true);



    $("#chkMoveToRel")
        .prop("checked", false);



    $("#chkAutoSelecting")
        .prop("checked", false);

    $("#chkCheckAll")
        .prop("checked", false);



    $("#txtDefaultValue").val("");

    $("#txtMinValue").val("");

    $("#txtMaxValue").val("");

    $("#txtStepChange").val("");

    $("#txtEnteringCount").val("");

    $("#txtDesForEnteringValue").val("");

    $("#txtDes").val("");

    $("#chkEnableEditing").prop("checked", false);

    $("#chkEnableDeleting").prop("checked", false);

    $("#txtIHC_CC_Related").val("");

    $("#btnSave")
        .html(
            "<i class='fa fa-save'></i> ثبت"
        );


}

function BuildPager(current, total) {
    let html = "";
    for (let i = 1; i <= total; i++) {

        html += `

        <li class="page-item 
        ${i == current ? 'active' : ''}">
            <a class="page-link"

               href="#"

               onclick="LoadData(${i});
               return false;">


                ${i}
            </a>
        </li>
        `;
    }
    $("#pagination").html(html);
}




///////////////
///////////////
///////////////
//tblItemsAddingToFB
////////////////////////////
////////////////////////////

var addingEditId = 0;


function ToggleDetail(row, id) {

    var detail = $(row).next();

    // اگر همان ردیف باز است
    if (detail.is(":visible")) {

        detail.hide();

        $(".main-row").removeClass("active-row");

        return;
    }

    // بستن همه
    $(".detail-row").hide();

    $(".main-row").removeClass("active-row");

    // فعال کردن ردیف جاری
    $(row).addClass("active-row");

    detail.show();

    LoadAddingItems(id);

}


function LoadAddingItems(parentId) {

    $.ajax({

        url: "/ItemsAddingToFB/GetAll",

        type: "GET",

        data:
        {
            itemsHasConditionConditionContextId: parentId
        },

        success: function (list) {

            BuildAddingTable(parentId, list);

        }

    });

}



function BuildAddingTable(parentId, list) {

    var html = "";

    html += `

        <table class="table table-bordered detail-table">

        <thead>

        <tr>

        <th width="120">آیتم اضافه</th>

        <th width="170">شرط</th>

        <th width="170">عملیات نهایی</th>

        <th width="90">نوع شرط</th>

        <th width="180">توضیح</th>

        <th width="130">آیتم مبنا</th>

        <th width="130">فیلدها</th>

        <th width="70">حرف اضافه شونده</th>

        <th width="120">عملیات</th>

        </tr>

        </thead>

        <tbody>

    `;

    $.each(list, function (i, x) {

        html += `

        <tr>

            <td>${x.addedItems ?? ""}</td>

            <td>${x.condition ?? ""}</td>

            <td>${x.finalWorking ?? ""}</td>

            <td>${x.conditionType}</td>

            <td>${x.desOfAddingItems ?? ""}</td>

            <td>${x.useItemForAdd ?? ""}</td>

            <td>${x.fieldsAdding ?? ""}</td>

            <td>${x.characterPlus ?? ""}</td>

            <td>

                <button class="btn btn-warning btn-sm" onclick="EditAdding(${x.id})">

                    <i class="fa fa-edit"></i>

                </button>

                <button class="btn btn-danger btn-sm"

                    onclick="DeleteAdding(${x.id},${parentId})">

                    <i class="fa fa-trash"></i>

                </button>

            </td>

        </tr>`;

    });

    html += `

<tr style="background:#fff8dc">

<td>

<input
id="txtAddedItems_${parentId}"
class="form-control">

</td>

<td>

<input
id="txtCondition_${parentId}"
class="form-control">

</td>

<td>

<input
id="txtFinalWorking_${parentId}"
class="form-control">

</td>

<td>

<input
id="txtConditionType_${parentId}"
class="form-control">

</td>

<td>

<input
id="txtDes_${parentId}"
class="form-control">

</td>

<td>

<input
id="txtUseItem_${parentId}"
class="form-control">

</td>

<td>

<input
id="txtFields_${parentId}"
class="form-control">

</td>

<td>

<input
id="txtCharacter_${parentId}"
class="form-control">

</td>

<td>

<button
id="btnSaveAdding_${parentId}"
class="btn btn-success"
onclick="SaveAdding(${parentId})">

<i class="fa fa-save"></i>

ثبت

</button>

<button
        id="btnCancelAdding_${parentId}"
        class="btn btn-secondary"
        style="display:none"
        onclick="CancelAdding(${parentId})">

        <i class="fa fa-times"></i>

        انصراف

    </button>

</td>

</tr>

`;

    html += "</tbody></table>";

    $("#detail_" + parentId).html(html);

}


function SaveAdding(parentId) {

    var model = {

        id: addingEditId,

        itemsHasCondition_ConditionContextId: parentId,

        addedItems: $("#txtAddedItems_" + parentId).val(),

        condition: $("#txtCondition_" + parentId).val(),

        finalWorking: $("#txtFinalWorking_" + parentId).val(),

        conditionType: $("#txtConditionType_" + parentId).val(),

        desOfAddingItems: $("#txtDes_" + parentId).val(),

        useItemForAdd: $("#txtUseItem_" + parentId).val(),

        fieldsAdding: $("#txtFields_" + parentId).val(),

        characterPlus: $("#txtCharacter_" + parentId).val(),

        year: parseInt($("#cmbYear").val()),

        noeFB: parseInt($("#cmbNoeFB").val())

    };


    var url =
        addingEditId == 0 ?
            "/ItemsAddingToFB/Save"
            :
            "/ItemsAddingToFB/Update";


    $.ajax({

        url: url,

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            addingEditId = 0;

            ClearAdding(parentId);
            LoadAddingItems(parentId);

        },

        error: function () {

            toastr.error("خطا در ثبت اطلاعات");

        }

    });

}


function EditAdding(id) {

    $.get(

        "/ItemsAddingToFB/Get",

        { id: id },

        function (item) {

            addingEditId = item.id;

            var parentId =
                item.itemsHasCondition_ConditionContextId;

            $("#txtAddedItems_" + parentId)
                .val(item.addedItems);

            $("#txtCondition_" + parentId)
                .val(item.condition);

            $("#txtFinalWorking_" + parentId)
                .val(item.finalWorking);

            $("#txtConditionType_" + parentId)
                .val(item.conditionType);

            $("#txtDes_" + parentId)
                .val(item.desOfAddingItems);

            $("#txtUseItem_" + parentId)
                .val(item.useItemForAdd);

            $("#txtFields_" + parentId)
                .val(item.fieldsAdding);

            $("#txtCharacter_" + parentId)
                .val(item.characterPlus);

            $("#btnSaveAdding_" + parentId)
                .removeClass("btn-success")
                .addClass("btn-warning")
                .html("<i class='fa fa-edit'></i> ویرایش");

            $("#btnCancelAdding_" + parentId).show();

        });

}



function CancelAdding(parentId) {
    addingEditId = 0;

    ClearAdding(parentId);

    $("#btnSaveAdding_" + parentId)
        .removeClass("btn-warning")
        .addClass("btn-success")
        .html("<i class='fa fa-save'></i> ثبت");

    $("#btnCancelAdding_" + parentId).hide();
}


function DeleteAdding(id, parentId) {

    // if (!confirm("رکورد حذف شود؟"))
    //     return;


    $.ajax({

        url: "/ItemsAddingToFB/Delete",

        type: "POST",

        data:
        {
            id: id
        },

        success: function () {

            LoadAddingItems(parentId);

        },

        error: function () {

            toastr.error("خطا در حذف");

        }

    });

}




function ClearAdding(parentId) {

    $("#txtAddedItems_" + parentId).val("");

    $("#txtCondition_" + parentId).val("");

    $("#txtFinalWorking_" + parentId).val("");

    $("#txtConditionType_" + parentId).val("");

    $("#txtDes_" + parentId).val("");

    $("#txtUseItem_" + parentId).val("");

    $("#txtFields_" + parentId).val("");

    $("#txtCharacter_" + parentId).val("");

    addingEditId = 0;

    $("#btnSaveAdding_" + parentId)
        .removeClass("btn-warning")
        .addClass("btn-success")
        .html("<i class='fa fa-save'></i> ثبت");

    $("#btnCancelAdding_" + parentId).hide();

    addingEditId = 0;

}

















