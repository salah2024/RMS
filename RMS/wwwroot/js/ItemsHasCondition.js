$(function () {

    $("#cmbYear").val("1404");

    $("#cmbNoeFB").val("234");

    LoadData();

    $("#btnSave").click(function () {

        Save();

    });

    $("#txtSearch").keyup(function () {

        LoadData(1);

    });

    $("#cmbYear,#cmbNoeFB").change(function () {

        LoadData();

    });

});

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


function LoadData(page = 1) {
    $.ajax({

        url: "/ItemsHasCondition/GetAll",

        data: {

            year: $("#cmbYear").val(),

            noeFB: $("#cmbNoeFB").val(),

            search: $("#txtSearch").val(),

            page: page

        },

        success: function (result) {

            debugger;

            BuildTable(result.items);

            BuildPager(result.page, result.totalPages);

        }

    });

}

function Save() {

    var model = {

        id: parseInt($("#hfId").val()),

        itemFBShomareh: $("#txtItemNo").val(),

        year: parseInt($("#cmbYear").val()),

        noeFB: parseInt($("#cmbNoeFB").val())

    };

    debugger;

    var url = "/ItemsHasCondition/Save";

    if (model.id != 0)
        url = "/ItemsHasCondition/Update";

    $.ajax({

        url: url,

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function () {

            toastr.success("اطلاعات ذخیره شد");

            ClearForm();

            LoadData(1);

        },

        error: function (xhr) {

            toastr.error("خطا در انجام عملیات");

        }

    });

}

function Edit(id) {

    $.ajax({

        url: "/ItemsHasCondition/Get?id=" + id,

        type: "GET",

        success: function (item) {

            $("#hfId").val(item.id);

            $("#txtItemNo").val(item.itemFBShomareh);

            $("#txtYear").val(item.year);

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

 
    $.ajax({

        url: "/ItemsHasCondition/Delete?id=" + id,

        type: "POST",

        success: function () {

            toastr.success("حذف شد");

            LoadData(1);

        },

        error: function () {

            toastr.error("خطا");

        }

    });

}

    const fbNames = {

        232: "راهداری",

        233: "ابنیه",

        234: "راه و باند",

        235: "برقی",

        236: "مکانیکی",

        237: "مرمت"

    };

    function GetNoeFB(id) {

        return fbNames[id] || "-";

    }


function BuildTable(data) {

    let rows = "";

    $.each(data, function (i, item) {

        rows += `

        <tr>

            <td>${i + 1}</td>

            <td>${item.itemFBShomareh}</td>

            <td>${item.year}</td>

            <td>${GetNoeFB(item.noeFB)}</td>

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


function ClearForm() {

    $("#hfId").val(0);

    $("#txtItemNo").val("");

    $("#btnSave").html("<i class='fa fa-save'></i> ثبت");

}


// function Search() {

//     var value = $("#txtSearch").val().toLowerCase();

//     $("#tblBody tr").filter(function () {

//         $(this).toggle(

//             $(this).text().toLowerCase().indexOf(value) > -1

//         );

//     });

// }