$(function () {

    $("#cmbNoeFB").val("21");

    LoadData();

    $("#btnSave").click(function () {
        Save();
    });

    $("#cmbNoeFB").change(function () {
        LoadData(1);
    });

    let searchTimer;

    $("#txtSearch").on("keyup", function () {

        clearTimeout(searchTimer);

        searchTimer = setTimeout(function () {

            LoadData(1);

        }, 400);

    });

});

function LoadData(page = 1) {

    $.ajax({

        url: "/ConditionGroup/GetAll",

        type: "GET",

        data: {

            noeFB: $("#cmbNoeFB").val(),

            search: $("#txtSearch").val(),

            page: page

        },

        success: function (result) {

            BuildTable(result.items);

            BuildPager(result.page, result.totalPages);

        },

        error: function () {

            toastr.error("خطا در دریافت اطلاعات");

        }

    });

}

function BuildTable(data) {

    let rows = "";

    $.each(data, function (i, item) {

        rows += `

        <tr>

            <td>${i + 1}</td>

            <td>${item.conditionGroupName}</td>

            <td>${item.order ?? ""}</td>

            <td>${GetNoeFB(item.noeFB)}</td>

            <td>

                <button class="btn btn-warning btn-sm action-btn"
                        onclick="Edit(${item.id})">

                    <i class="fa fa-edit"></i>

                </button>

                <button class="btn btn-danger btn-sm action-btn"
                        onclick="Delete(${item.id})">

                    <i class="fa fa-trash"></i>

                </button>

            </td>

        </tr>

        `;

    });

    $("#tblBody").html(rows);

}

function Save() {

    var model = {

        id:parseInt($("#hfId").val()),

        conditionGroupName: $("#txtConditionGroupName").val(),

        order: parseInt($("#txtOrder").val()),

        noeFB: parseInt($("#cmbNoeFB").val())

    };

    var url = "/ConditionGroup/Save";

    if (model.id != 0)
        url = "/ConditionGroup/Update";

    $.ajax({

        url: url,

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            toastr.success("اطلاعات با موفقیت ذخیره شد.");

            ClearForm();

            LoadData();

        },

        error: function (xhr) {

            toastr.error("خطا در انجام عملیات");

        }

    });

}

function Edit(id) {

    $.ajax({

        url: "/ConditionGroup/Get?id=" + id,

        type: "GET",

        success: function (item) {

            $("#hfId").val(item.id);

            $("#txtConditionGroupName").val(item.conditionGroupName);

            $("#txtOrder").val(item.order);

            $("#cmbNoeFB").val(item.noeFB);

            $("#btnSave").html("<i class='fa fa-save'></i> ویرایش");

            window.scrollTo({

                top: 0,

                behavior: "smooth"

            });

        }

    });

}

function Delete(id) {

    // if (!confirm("آیا از حذف این گروه اطمینان دارید؟"))
    //     return;

    $.ajax({

        url: "/ConditionGroup/Delete?id=" + id,

        type: "POST",

        success: function () {

            toastr.success("حذف شد.");

            LoadData();

        },

        error: function () {

            toastr.error("خطا در حذف اطلاعات");

        }

    });

}

function ClearForm() {

    $("#hfId").val(0);

    $("#txtConditionGroupName").val("");

    $("#txtOrder").val("");

    $("#btnSave").html("<i class='fa fa-save'></i> ثبت");

}

function BuildPager(current, total) {

    let html = "";

    for (let i = 1; i <= total; i++) {

        html += `

        <li class="page-item ${i == current ? 'active' : ''}">

            <a href="#"

               class="page-link"

               onclick="LoadData(${i});return false;">

                ${i}

            </a>

        </li>

        `;

    }

    $("#pagination").html(html);

}

function GetNoeFB(id) {

    switch (parseInt(id)) {

        case 22:
            return "راهداری";

        case 11:
            return "ابنیه";

        case 21:
            return "راه و باند";

        case 13:
            return "برقی";

        case 12:
            return "مکانیکی";

        case 16:
            return "مرمت";

        default:
            return "-";

    }

}