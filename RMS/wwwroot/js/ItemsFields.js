$(document).ready(function () {

    loadNoeFehrestBaha();

});


let searchTimer;

$('#txtSearchItem').on('input', function () {

    clearTimeout(searchTimer);

    searchTimer = setTimeout(function () {

        loadData(1);

    }, 300);

});

$('#cmbSearchNoeFB').on('change', function () {

    loadData(1);

});

// =========================================================
// Load Data
// =========================================================
let currentPage = 1;

function loadData(page = 1) {

    currentPage = page;

    $.ajax({

        url: "/ItemsFields/GetList",

        type: "GET",

        data: {

            itemShomareh: $('#txtSearchItem').val(),

            noeFB: $('#cmbSearchNoeFB').val(),

            page: page,

            pageSize: $('#cmbPageSize').val()

        },

        success: function (result) {

            let html = '';

            if (result.data.length === 0) {

                html = `
                    <tr>
                        <td colspan="11">
                            رکوردی یافت نشد
                        </td>
                    </tr>
                `;

            }
            else {

                $.each(result.data, function (index, item) {

                    let rowNumber =
                        ((result.page - 1) * result.pageSize)
                        + index + 1;

                    html += `

                        <tr>

                            <td>
                                ${rowNumber}
                            </td>

                            <td>
                                ${item.itemShomareh ?? ''}
                            </td>

                            <td>
                                ${item.noeFBName ?? ''}
                            </td>

                            <td>
                                ${item.fieldTypeName ?? ''}
                            </td>

                            <td>
                                ${item.vahed ?? ''}
                            </td>

                            <td>
                                ${item.isEnteringValue
                            ? '<span class="text-success">بله</span>'
                            : '<span class="text-danger">خیر</span>'
                        }
                            </td>

                            <td>
                                ${item.essentialValue
                            ? '<span class="text-success">بله</span>'
                            : '<span class="text-danger">خیر</span>'
                        }
                            </td>

                            <td>
                                ${item.defaultValue ?? ''}
                            </td>

                            <td>
                                ${item.minValue ?? ''}
                            </td>

                            <td>
                                ${item.maxValue ?? ''}
                            </td>

                            <td>

                                <button type="button"
                                        class="btn btn-sm btn-warning"
                                        onclick="editItem(${item.id})">

                                    <i class="fa fa-edit"></i>

                                </button>

                                <button type="button"
                                        class="btn btn-sm btn-danger"
                                        onclick="deleteItem(${item.id})">

                                    <i class="fa fa-trash"></i>

                                </button>

                            </td>

                        </tr>

                    `;
                });
            }

            $('#tblItemsFields').html(html);

            $('#paginationInfo').text(
                `تعداد کل رکوردها: ${result.totalCount}`
            );

            createPagination(
                result.page,
                result.totalPages
            );

        },

        error: function () {

            toastr.error('خطا در دریافت اطلاعات');

        }

    });
}


function createPagination(currentPage, totalPages) {

    let html = '';

    if (totalPages <= 1) {

        $('#pagination').html('');

        return;
    }


    // صفحه قبلی

    html += `
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">

            <a class="page-link"
               href="javascript:void(0)"
               onclick="loadData(${currentPage - 1})">

                قبلی

            </a>

        </li>
    `;


    // تعداد صفحات

    let startPage = Math.max(1, currentPage - 2);

    let endPage = Math.min(
        totalPages,
        currentPage + 2
    );


    // صفحه اول

    if (startPage > 1) {

        html += `
            <li class="page-item">

                <a class="page-link"
                   href="javascript:void(0)"
                   onclick="loadData(1)">

                    1

                </a>

            </li>
        `;


        if (startPage > 2) {

            html += `
                <li class="page-item disabled">

                    <span class="page-link">
                        ...
                    </span>

                </li>
            `;

        }

    }


    // صفحات

    for (let i = startPage; i <= endPage; i++) {

        html += `
            <li class="page-item ${i === currentPage ? 'active' : ''}">

                <a class="page-link"
                   href="javascript:void(0)"
                   onclick="loadData(${i})">

                    ${i}

                </a>

            </li>
        `;

    }


    // صفحه آخر

    if (endPage < totalPages) {

        if (endPage < totalPages - 1) {

            html += `
                <li class="page-item disabled">

                    <span class="page-link">
                        ...
                    </span>

                </li>
            `;

        }


        html += `
            <li class="page-item">

                <a class="page-link"
                   href="javascript:void(0)"
                   onclick="loadData(${totalPages})">

                    ${totalPages}

                </a>

            </li>
        `;

    }


    // صفحه بعد

    html += `
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">

            <a class="page-link"
               href="javascript:void(0)"
               onclick="loadData(${currentPage + 1})">

                بعدی

            </a>

        </li>
    `;


    $('#pagination').html(html);

}

// =========================================================
// New
// =========================================================

function newItem() {

    $('#Id').val(0);

    $('#ItemShomareh').val('');

    $('#NoeFB').val('');

    $('#FieldType').val(1);

    $('#Vahed').val('');

    $('#DefaultValue').val('');

    $('#MinValue').val('');

    $('#MaxValue').val('');

    $('#IsEnteringValue').prop('checked', false);

    $('#EssentialValue').prop('checked', false);

    $('#formTitle').text('ثبت فیلد جدید');

}


// =========================================================
// Save
// =========================================================

function saveItem() {

    let id = $('#Id').val();

    let url = id == 0
        ? "/ItemsFields/Create"
        : "/ItemsFields/Update";


    let data = {

        Id: $('#Id').val(),

        ItemShomareh: $('#ItemShomareh').val(),

        FieldType: $('#FieldType').val(),

        Vahed: $('#Vahed').val(),

        DefaultValue: $('#DefaultValue').val(),

        MinValue: $('#MinValue').val(),

        MaxValue: $('#MaxValue').val(),

        // خارج از فرم
        NoeFB: $('#cmbSearchNoeFB').val(),

        // Checkbox
        IsEnteringValue:
            $('#IsEnteringValue').prop('checked'),

        EssentialValue:
            $('#EssentialValue').prop('checked'),

        // Anti Forgery Token
        __RequestVerificationToken:
            $('#frmItemsFields input[name="__RequestVerificationToken"]').val()
    };


    console.log(data);


    $.ajax({

        url: url,

        type: 'POST',

        data: data,

        success: function (result) {

            if (result.success) {

                toastr.success(result.message);

                newItem();

                loadData();

            }
            else {

                toastr.error(result.message);

            }

        },

        error: function (xhr) {

            console.log("Status:", xhr.status);

            console.log("Response:", xhr.responseText);

            toastr.error(
                xhr.responseJSON?.message ||
                'خطا در ذخیره اطلاعات'
            );

        }

    });

}// =========================================================
// Edit
// =========================================================

function editItem(id) {


    $.ajax({

        url: '/ItemsFields/Get',

        type: 'GET',

        data: {
            id: id
        },

        success: function (item) {

            $('#Id').val(item.id);

            $('#ItemShomareh').val(item.itemShomareh);

            $('#FieldType').val(item.fieldType);

            $('#Vahed').val(item.vahed);

            $('#DefaultValue').val(item.defaultValue);

            $('#MinValue').val(item.minValue);

            $('#MaxValue').val(item.maxValue);

            $('#NoeFB').val(item.noeFB);

            $('#IsEnteringValue')
                .prop('checked', item.isEnteringValue);

            $('#EssentialValue')
                .prop('checked', item.essentialValue);

            $('#formTitle')
                .text('ویرایش فیلد');

            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });

        },

        error: function () {

            toastr.error('خطا در دریافت اطلاعات رکورد');

        }

    });

}


// =========================================================
// Delete
// =========================================================

function deleteItem(id) {

    // if (!confirm('آیا از حذف این رکورد اطمینان دارید؟')) {

    //     return;

    // }


    let token =
        $('#frmItemsFields input[name="__RequestVerificationToken"]')
            .val();


    $.ajax({

        url: '/ItemsFields/Delete',

        type: 'POST',

        data: {

            id: id,

            __RequestVerificationToken: token

        },

        success: function (result) {

            if (result.success) {

                toastr.success(result.message);

                loadData();

            }
            else {

                toastr.error(result.message);

            }

        },

        error: function () {

            toastr.error('خطا در حذف رکورد');

        }

    });

}


function loadNoeFehrestBaha() {

    $.ajax({

        url: "/ItemsFields/GetNoeFehrestBaha",


        type: 'GET',

        success: function (data) {

            let html = `
                <option value="">
                    همه
                </option>
            `;

            $.each(data, function (index, item) {

                html += `
                    <option value="${item.id}">
                        ${item.name}
                    </option>
                `;

            });

            $('#cmbSearchNoeFB').html(html);

            // پیش فرض 21
            $('#cmbSearchNoeFB').val('21');

            loadData();

        },

        error: function () {

            toastr.error('خطا در دریافت انواع فهرست‌بها');

        }

    });

}
