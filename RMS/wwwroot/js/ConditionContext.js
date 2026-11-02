$(document).ready(function () {

    $("#cmbYear").val("1404");
    $("#cmbNoeFB").val("21");

    LoadGroups();

    LoadData();

    $("#cmbNoeFB").change(function () {

        LoadGroups();

        LoadData(1);

    });

    $("#cmbYear").change(function () {

        LoadData(1);

    });

    $("#cmbGroup").change(function () {

        LoadData(1);

    });

    $("#btnSave").click(function () {

        Save();

    });

    let timer;

    $("#txtSearch").keyup(function () {

        clearTimeout(timer);

        timer = setTimeout(function () {

            LoadData(1);

        }, 300);

    });

});



function LoadGroups() {


    $.ajax({

        url: "/ConditionContextBase/GetGroups",

        type: "GET",

        data: {

            noeFB: parseInt($("#cmbNoeFB").val())

        },

        success: function (list) {

            let html = '<option value="">همه گروه‌ها</option>';

            $.each(list, function (i, item) {

                html += `<option value="${item.id}">
                ${item.text}
             </option>`;

            });


            $("#cmbGroup").html(html);

            if (selectedId)
                $("#cmbGroup").val(selectedId);

        }

    });

}



function LoadData(page = 1) {

    $.ajax({

        url: "/ConditionContextBase/GetAll",

        type: "GET",

        data: {

            year: $("#cmbYear").val(),

            noeFB: $("#cmbNoeFB").val(),

            conditionGroupId: $("#cmbGroup").val(),

            search: $("#txtSearch").val(),

            page: page

        },

        success: function (result) {

            BuildTable(result.items);

            BuildPager(result.page, result.totalPages);

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

            <td class="wrap-text" style="text-align:right">

                ${item.context}

            </td>

            <td class="wrap-text">
             ${item.titleDes ? "<b>" + item.titleDes + "</b><br>" : ""}
                 ${item.des ?? "-"}

            </td>

            <td>

                <button
                    class="btn btn-warning btn-sm action-btn"
                    onclick="Edit(${item.id})">

                    <i class="fa fa-edit"></i>

                </button>

                <button
                    class="btn btn-danger btn-sm action-btn"
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

    if ($("#txtContext").val().trim() == "") {

        toastr.warning("متن شرط را وارد کنید.");

        return;
    }

    var model = {

        id: parseInt($("#hfId").val()),

        context: $("#txtContext").val(),

        conditionGroupId: parseInt($("#cmbGroup").val()),

        year: parseInt($("#cmbYear").val()),

        titleDes: $("#txtTitleDes").val(),

        des: $("#txtDes").val(),

        conditionContextRel: $("#txtRel").val(),

        noeFB: parseInt($("#cmbNoeFB").val())

    };

    var url = "/ConditionContextBase/Save";

    if (parseInt(model.id) > 0)
        url = "/ConditionContextBase/Update";

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

            toastr.error(xhr.responseText);

        }

    });

}



function Edit(id) {

    $.ajax({

        url: "/ConditionContextBase/Get?id=" + id,

        type: "GET",

        success: function (item) {

            $("#hfId").val(item.id);

            $("#cmbYear").val(item.year);

            $("#cmbNoeFB").val(item.noeFB);

            LoadGroups(item.conditionGroupId);

            setTimeout(function () {

                $("#cmbGroup").val(item.conditionGroupId);

            }, 200);

            $("#txtContext").val(item.context);

            $("#chkHasDes").prop("checked", item.hasDes);

            $("#txtTitleDes").val(item.titleDes);

            $("#txtDes").val(item.des);

            $("#txtRel").val(item.conditionContextRel);


            $("#btnSave").html("<i class='fa fa-save'></i> ویرایش");

            $("html,body").animate({
                scrollTop: 0
            }, 300);

        }

    });

}


function Delete(id) {

    // if (!confirm("آیا از حذف این رکورد اطمینان دارید؟"))
    //     return;

    $.ajax({

        url: "/ConditionContextBase/Delete?id=" + id,

        type: "POST",

        success: function () {

            toastr.success("رکورد حذف شد.");

            LoadData();

        },

        error: function () {

            toastr.error("خطا در حذف اطلاعات");

        }

    });

}


function ClearForm() {

    $("#hfId").val(0);

    $("#txtContext").val("");

    $("#chkHasDes").prop("checked", false);

    $("#txtTitleDes").val("");

    $("#txtDes").val("");

    $("#txtRel").val("");

    $("#btnSave").html("<i class='fa fa-save'></i> ثبت");


}


function BuildPager(current, total) {

    let html = "";

    for (let i = 1; i <= total; i++) {

        html += `

        <li class="page-item ${i == current ? 'active' : ''}">

            <a class="page-link"

               href="#"

               onclick="LoadData(${i});return false;">

                ${i}

            </a>

        </li>

        `;

    }

    $("#pagination").html(html);

}

