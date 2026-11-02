
$(document).ready(function () {
    ViewBarAvordClick();


});


$(document).on("mouseenter", ".action-menu", function() {
    var $menu = $(this);
    var $dropdown = $menu.data("activeDropdown");

    if (!$dropdown || !$dropdown.length) {
        $dropdown = $menu.find(".action-dropdown");
        $dropdown.appendTo("body");
        $menu.data("activeDropdown", $dropdown);
    }

    var rect = $menu.find(".action-btn")[0].getBoundingClientRect();

    $dropdown.css({
        display: "block",
        position: "fixed",
        top: "0px",
        left: "0px"
    });

    var menuHeight = $dropdown.outerHeight();
    var menuWidth = $dropdown.outerWidth();

    var top = rect.bottom + menuHeight <= window.innerHeight
        ? rect.bottom
        : rect.top - menuHeight;

    var left = Math.max(
        5,
        Math.min(
            rect.right - menuWidth,
            window.innerWidth - menuWidth - 5
        )
    );

    $dropdown.css({
        top: top,
        left: left
    });
});

// فقط وقتی ماوس از دکمه و منو خارج شود، منو بسته شود
$(document).on("mouseleave", ".action-menu", function() {
    var $menu = $(this);
    var $dropdown = $menu.data("activeDropdown");

    setTimeout(function() {
        if (
            !$menu.is(":hover") &&
            (!$dropdown || !$dropdown.is(":hover"))
        ) {
            if ($dropdown) {
                $dropdown.hide().appendTo($menu);
                $menu.removeData("activeDropdown");
            }
        }
    }, 150);
});

$(document).on("mouseleave", ".action-dropdown", function() {
    var $dropdown = $(this);

    setTimeout(function() {
        if (!$dropdown.is(":hover")) {
            var $menu = $(".action-menu").filter(function() {
                return $(this).data("activeDropdown")?.[0] === $dropdown[0];
            });

            if (!$menu.is(":hover")) {
                $dropdown.hide().appendTo($menu);
                $menu.removeData("activeDropdown");
            }
        }
    }, 150);
});

$(document).on("click", ".action-dropdown a", function() {
    var $dropdown = $(this).closest(".action-dropdown");

    // پیدا کردن منوی اصلی که این کادر متعلق به آن است
    var $menu = $(".action-menu").filter(function() {
        return $(this).data("activeDropdown")?.[0] === $dropdown[0];
    });

    $dropdown.hide().appendTo($menu);
    $menu.removeData("activeDropdown");
});









// بعد از این‌که جدولت پُر شد (مثلاً بعد از success هم می‌تونی بذاری یا در اسکریپت جدا)
// فیلتر کردن روی تغییر کاراکتر در input
$(document).on('keyup', '#txtSearchTitle', function () {
    var value = $(this).val().toLowerCase();

    $("#tableBody tr").each(function () {
        // ستون عنوان: سومین td (اولی مخفی، دومی شماره، سومی عنوان)
        var titleText = $(this).find("td:nth-child(3)").text().toLowerCase();

        if (titleText.indexOf(value) > -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
});

//$(document).on("click", ".btnDelete", function (e) {
function Delete(btn1) {
    btn = $(btn1);
    //var btn = $(this);
    var id = btn.data("id");
    var row = btn.closest("tr");


    // جلوگیری از چند بار نمایش
    $(".confirm-box").remove();

    DeleteBaravord(id, row);

    // ایجاد پیام کوچک تأیید
    // var confirmBox = $(`
    // <div class='confirm-box'
    //     style='position:absolute;
    //                      background:#fff;
    //                      border:1px solid #ccc;
    //                      padding:5px 10px;
    //                      border-radius:5px;
    //                      font-size:12px;
    //                      z-index:999;
    //                      box-shadow:0 2px 6px rgba(0,0,0,0.2);'>

    //     حذف شود؟
    //     <button class='btnYes' style='margin-left:20px;font-size:12px;'>بله</button>
    //     <button class='btnNo' style='font-size:12px;'>خیر</button>
    // </div>
    // `);

    //$("body").append(confirmBox);

    // موقعیت کنار دکمه
    // var pos = btn.offset();
    // confirmBox.css({
    //     top: pos.top + 30,
    //     left: pos.left
    // });

    // // دکمه‌ها
    // confirmBox.find(".btnYes").on("click", function () {
    //     confirmBox.remove();
    //     DeleteBaravord(id, row);
    // });

    // confirmBox.find(".btnNo").on("click", function () {
    //     confirmBox.remove();
    // });
};

$(document).on("click", ".btnConvert", function (e) {

    var btn = $(this);
    var id = btn.data("id");
    var row = btn.closest("tr");

    ConvertToGharardad(id, row);

    // جلوگیری از چند بار نمایش
    // $(".confirm-box").remove();

    // // ایجاد پیام کوچک تأیید
    // var confirmBox = $(`
    // <div class='confirm-box'
    //     style='position:absolute;
    //                      background:#fff;
    //                      border:1px solid #ccc;
    //                      padding:5px 10px;
    //                      border-radius:5px;
    //                      font-size:12px;
    //                      z-index:999;
    //                      box-shadow:0 2px 6px rgba(0,0,0,0.2);'>

    //     به قرارداد تبدیل شود؟
    //     <button class='btnYes' style='margin-left:10px;font-size:12px;'>بله</button>
    //     <button class='btnNo' style='font-size:12px;'>خیر</button>
    // </div>
    // `);

    // $("body").append(confirmBox);

    // // موقعیت کنار دکمه
    // var pos = btn.offset();
    // confirmBox.css({
    //     top: pos.top + 30,
    //     left: pos.left
    // });

    // // دکمه‌ها
    // confirmBox.find(".btnYes").on("click", function () {
    //     confirmBox.remove();
    // });

    // confirmBox.find(".btnNo").on("click", function () {
    //     confirmBox.remove();
    // });

});

function DeleteBaravord(id, row) {
    var vardata = new Object();
    vardata.Id = id;
    $.ajax({
        type: "POST",
        url: "/BaravordUser/DeleteBarAvordUser",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('حذف بدرستی صورت گرفت', 'موفق');
                row.fadeOut(500, function () {
                    $(this).remove();
                });
            }
        },
        error: function (response) {
            toastr.error('مشکل در حذف برآورد', 'خطا');

        }
    });
}

function ConvertToGharardad(id, row) {
    var vardata = new Object();
    vardata.Id = id;
    $.ajax({
        type: "POST",
        url: "/BaravordUser/ConvertToGharardad",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK") {
                toastr.success('بدرستی تبدیل صورت گرفت', 'موفق');
                ViewBarAvordClick();
            }
        },
        error: function (response) {
            toastr.error('مشکل در تبدیل', 'خطا');

        }
    });
}


////////////////

function Save() {
    NoeFB = $('#drdNoeFB').find(":selected").val();
    Year = $('#drdYear').val();
    UserName = $('#HDFUserName').val();
    Title = $('#txtTitle').val();
    var vardata = new Object();
    vardata.Title = Title;
    vardata.NoeFB = parseInt(NoeFB);
    vardata.Year = parseInt(Year);
    vardata.Type = 1;
    vardata.UserName = "salah"; //UserName;

    $.ajax({
        type: "POST",
        url: "/BaravordUser/CreateNewBU",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {
                toastr.success('درج بدرستی صورت گرفت', 'موفق');
                $('#HDFID').val(info[1]);
                $('#HDFBAType').val(parseInt(info[2]));
                $('#HDFNoeFB').val(NoeFB);
                $('#HDFYear').val(Year);
                goToBarAvord(Title);
            }
            else if (info[0] == "NOK") {
                toastr.info('دسترسی نامعتبر', 'اطلاع');
            }
            else {
                toastr.error('مشکل در درج برآورد جدید', 'خطا');
            }
        },
        error: function (response) {
            toastr.error('مشکل در درج برآورد جدید', 'خطا');
        }
    });
}
////////

function OpenWorkflow(entityId) {
    window.location.href = "/WorkflowRuntime/Open?entityId=" + entityId;
}
////////////

function SaveInTable(btn) {
    $(btn).attr('disabled', true);
    // پیدا کردن ردیف والد
    var $row = $(btn).closest("tr");

    var selectedFB = [];
    $row.find("#new_noeFB input[type='checkbox']:checked").each(function () {
        selectedFB.push($(this).val());
    });

    var selectedFBString = selectedFB.join(',');


    // ساخت آبجکت ورودی ها از روی همان ردیف
    var inputs = {
        name: $row.find("input#new_name").val().trim(),
        noeFB: selectedFBString, //$row.find("select#new_noeFB").val(),
        //date: $row.find("input#new_date").val().trim(),
        year: $row.find("select#new_year").val(),
        type: 1// $row.find("select#new_Type").val()
    };

    // --- اعتبارسنجی ---
    if (inputs.name === "") {
        toastr.warning("عنوان وارد نشده است");
        return;
    }

    // if (!inputs.noeFB) {
    // 	toastr.warning("فهرست بها انتخاب نشده است");
    // 	return;
    // }

    if (selectedFB.length === 0) {
        toastr.warning("حداقل یک آیتم فهرست بها انتخاب شود");
        $(btn).removeAttr('disabled');
        return;
    }

    // if (inputs.date === "") {
    // 	toastr.warning("تاریخ وارد نشده است");
    // 	return;
    // }

    if (!inputs.year) {
        toastr.warning("سال انتخاب نشده است");
        return;
    }

    if (!inputs.type) {
        toastr.warning("نوع انتخاب نشده است");
        return;
    }

    // --- ساخت آبجکت نهایی برای ارسال به سرور ---
    var vardata = {
        Title: inputs.name,
        NoeFBs: inputs.noeFB,
        Year: parseInt(inputs.year),
        Type: parseInt(inputs.type),
        UserName: "salah"
    };

    // --- فراخوانی AJAX ---
    $.ajax({
        type: "POST",
        url: "/BaravordUser/CreateNewBU",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            let info = response.split("_");

            if (info[0] === "OK") {

                toastr.success("درج با موفقیت انجام شد");

                // مقادیر برگشتی
                $("#HDFID").val(info[1]);
                $("#HDFBAType").val(info[2]);
                $("#HDFNoeFB").val(inputs.noeFB);
                $("#HDFYear").val(inputs.year);

                // پاک کردن مقادیر ردیف
                $row.find("input").val("");

                goToBarAvord(inputs.name);
            }
            else if (info[0] === "NOK") {
                toastr.info("دسترسی نامعتبر");
            }
            else {
                toastr.error("خطا در درج");
            }
        },

        error: function () {
            toastr.error("خطا در ارتباط با سرور");
            btn.removeAttr('disabled', true);
        }
    });
}
////////////

function SaveInTableSooratVaziat(btn, buId) {


    $(btn).attr('disabled', true);
    // پیدا کردن ردیف والد
    var $row = $(btn).closest("tr");

    //var selectedFB = [];
    //$row.find("#new_noeFB input[type='checkbox']:checked").each(function () {
    //    selectedFB.push($(this).val());
    //});

    //var selectedFBString = selectedFB.join(',');

    //Title = $('#drdSooratVaziatTitle-' + buId).val();
    var Title = $('#drdSooratVaziatTitle-' + buId + ' option:selected').text();
    FromDate = $('#SooratVaziatFromDate-' + buId).val();
    ToDate = $('#SooratVaziatToDate-' + buId).val();

    FromDateEn = convertPersianToEnglish(FromDate);
    ToDateEn = convertPersianToEnglish(ToDate);

    // --- اعتبارسنجی ---
    if (Title === "") {
        toastr.warning("عنوان وارد نشده است");
        return;
    }

    NoeFB = $("#HDFNoeFB").val();
    Year = $("#HDFYear").val();

    // --- ساخت آبجکت نهایی برای ارسال به سرور ---
    var vardata = {
        buId: buId,
        Title: Title,
        Year: Year,
        Type: 3,
        UserName: "salah",
        FromDate: FromDateEn,
        ToDate: ToDateEn
    };

    // --- فراخوانی AJAX ---
    $.ajax({
        type: "POST",
        url: "/BaravordUser/CreateNewSooratVaziat",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (response) {
            let info = response.split("_");

            if (info[0] === "OK") {

                toastr.success("درج با موفقیت انجام شد");

                // مقادیر برگشتی
                $("#HDFID").val(info[1]);
                $("#HDFBAType").val(info[2]);

                // پاک کردن مقادیر ردیف
                $row.find("input").val("");

                goToBarAvord(Title);
            }
            else if (info[0] === "NOK") {
                toastr.info("دسترسی نامعتبر");
            }
            else {
                toastr.error("خطا در درج");
            }
        },

        error: function () {
            toastr.error("خطا در ارتباط با سرور");
            btn.removeAttr('disabled', true);
        }
    });
}
////////////
function CreateBarAvordClick() {
    //FillNoeFB();
    $('#aCreateBarAvord').click();
}
/////
function ViewBarAvordClick() {
    $("[id*=tableBody]").html('');
    $.ajax({
        type: "POST",
        url: "/BaravordUser/List",
        data: JSON.stringify(""),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            debugger;

            $.each(response, function () {

                let actions = "";

                debugger;
                // مشاهده و شماتیک
                if ($(this)[0].canOpen) {
                    actions += `<a href="javascript:void(0)" onclick="OpenWorkflow('${$(this)[0].buId}')">
                                <i class="fa fa-eye"></i> مشاهده
                    </a>`;
                }

                if ($(this)[0].hasWorkflow) {
                    actions += `
                    <a href="javascript:void(0)"
                        onclick="SchimaStepsForCurrentBaravordUser('${$(this)[0].buId}')">
                        <i class="fa fa-sitemap"></i> گردش کار
                    </a>`;
                }

                // ارسال اولیه
                if (!$(this)[0].hasWorkflow) {

                    actions += `
                        <a href="javascript:void(0)"
                        onclick="StartWorkflow('${$(this)[0].buId}')">
                        <i class="fa fa-check text-success"></i> ارسال
                    </a>`;
                }

                // تکمیل مرحله
                if ($(this)[0].canApprove) {

                    actions += `
                        <a href="javascript:void(0)"
                        onclick="SubmitWorkflow('${$(this)[0].buId}',1)">
                        <i class="fa fa-check text-success"></i> تایید و ارسال
                    </a>`;
                }

                // عدم تکمیل (برگشت)
                if ($(this)[0].canReturn) {

                    actions += `
                        <a href="javascript:void(0)"
                        onclick="ReturnWorkflow('${$(this)[0].buId}')">
                        <i class="fa fa-undo text-danger"></i> عودت
                        </a>`;
                }


                // ارسال به پیمانکار جهت پیشنهاد قیمت
                if ($(this)[0].buType==2) {

                    actions += `
                        <a href="javascript:void(0)"
                        onclick="CheckCurrentStepForNeedUsers('${$(this)[0].buId}',3)">
                        <i class="fa fa-users text-primary"></i>
                        ارسال به پیمانکار جهت پیشنهاد قیمت
                        </a>`;
                }

                // تبدیل به قرارداد
                if ($(this)[0].buType == 3) {

                    actions += `
                        <a href="javascript:void(0)"
                        class="btnConvert"
                        data-id="${$(this)[0].buId}">
                        <i class="fa fa-recycle"></i> تبدیل به قرارداد
                    </a>`;
                }

                //حذف
                actions += `
                <a href="javascript:void(0)"
                    onclick="Delete(this)"
                    data-id="${$(this)[0].buId}">
                    <i class="fa fa-trash text-danger"></i> حذف
                </a>`;

                let projectName = $(this)[0].buName;

                // if (!$(this)[0].isOwner) {
                //     projectName +=
                //         ` <span class="badge bg-warning text-dark">
                //            ارجاع شده   
                //          </span>`;
                // }


                var row =
                    $(`<tr onclick="MainTableClick(this,${$(this)[0].buType})" ondblclick="MainTabledblClick(${$(this)[0].buType},'${$(this)[0].buName}')">
                <td style='display:none'></td>
                <td width="5%"></td>
                <td width="35%" style="text-wrap:auto;"></td>
                <td width="30%"></td>
                <td width="8%"></td>
                <td width="8%"></td>
                <td width="8%"></td>
                <td width="15%"></td>
                <td class="action-cell">
                    <div class="action-menu">
                        <button class="action-btn">
                            <i class="fa fa-cogs"></i>
                            </button>
                     <div class="action-dropdown">
                        ${actions}
                        </div>
                    </div>
                 </td>
                `)
                row.find("td:nth-child(1)").html($(this)[0].buId);
                row.find("td:nth-child(2)").html(toPersianDigits($(this)[0].buNum));
                row.find("td:nth-child(3)").html(projectName);
                row.find("td:nth-child(4)").html($(this)[0].noeFBName);
                row.find("td:nth-child(5)").html(toPersianDigits($(this)[0].buInsertDateSolar));
                row.find("td:nth-child(6)").html($(this)[0].buType == 1 ? "برآورد" : "قرارداد");
                row.find("td:nth-child(7)").html(toPersianDigits($(this)[0].buYear));
                row.find("td:nth-child(8)").html($(this)[0].workflowStatusTitle);
                row.find("td:nth-child(9)").html($(this)[0].buNoeFB);
                $("[id*=tableBody]").append(row);

                var rowFree = $(`<tr>
                    <td colspan="8" style="background-color: #efd4f7;padding:0px !important;">
                    <div style="display:none;margin: 5px;" id="divSooratVaziat_${$(this)[0].buId}">
                    </div>
                    </td>
                    </tr>`);
                $("[id*=tableBody]").append(rowFree);

            });
        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری برآورد', 'خطا');
        }
    });
}
//////////


function CheckCurrentStepForNeedUsers(id,proccesType)
{
    debugger;
    $.ajax({
        url: "/WorkflowRuntime/CheckCurrentStepForNeedUsers",
        type: "POST",
        data: {
            proccesType: proccesType
        },
        success: function(res) {
            debugger;
            if (!res.success)
                return;

            if (!res.needSelectReceiver) {

                if (proccesType == 1) {
                    DoSubmit(id, null, proccesType);
                }
                else if (proccesType == 3) {
                    SendToContractors(entityId,null);
                }
                return;
            }

            ShowReceiverDialog(id, res.users, proccesType);
        },
        error: function() {
            toastr.error("خطا در انجام عملیات");
        }
    });
}

function SendToContractors(id, receiversUserId) {
    debugger;

    $.ajax({
        url: "/BaravordUser/PishnahadGhyamatWorkFlow",
        type: "POST",
        data: {
            entityId: id,
            selectedUsers: receiversUserId
        },
        success: function (res) {
            debugger;
            if (res.success) {
                //SubmitWorkflow(id, receiversUserId, proccesType)
            }
            if (!res.success)
                return;

        },
        error: function () {
            toastr.error("خطا در انجام عملیات");
        }
    });


    // $.get("/BaravordUser/PishnahadGhyamatWorkFlow",
    //     {
    //         entityId: id,
    //         receiversUserId: receiversUserId
    //     },
    //     function (res) {
    //         debugger;

    //         if (res.success) {
    //             //SubmitWorkflow(id, receiversUserId, proccesType)
    //         }
    //         if (!res.success)
    //             return;

    //     });
    // window.location.href = "/Tender/SelectContractors?entityId=" + id;

}

// function OpenWorkflow(id) {

//     window.location =
//         "/BaravordUser/Edit?id=" + id;

// }
/////////////////
function ApproveWorkflow(id) {

    if (!confirm("آیا مرحله تکمیل شده است؟"))
        return;

    $.ajax({

        url: "/WorkflowRuntime/Approve",

        type: "POST",

        data: {

            entityId: id,

            comment: ""

        },

        success: function (res) {

            toastr.success(res.message);

            ViewBarAvordClick();

        },

        error: function () {

            toastr.error("خطا در انجام عملیات");

        }

    });

}

////////////

function StartWorkflow(id)
{
    $.ajax({

        url: "/WorkflowRuntime/StartWorkFlow",

        type: "POST",

        data: {

            entityId: id,

        },

        success: function(res) {
            toastr.success("بدرستی در گردش کار ثبت گردید");

            ViewBarAvordClick();
        }
    });
}
///////////////////

function SubmitWorkflow(id, proccesType) {

    $.ajax({

        url: "/WorkflowRuntime/CheckNextStepForNeedUsers",

        type: "POST",

        data: {

            entityId: id,
            proccesType: proccesType

        },

        success: function (res) {
            if (!res.success)
                return;

            if (!res.needSelectReceiver) {

                //if (proccesType == 1) {
                DoSubmit(id, null, proccesType);
                // }
                // else if (proccesType == 2) {

                //     SendToContractors(entityId, null);
                // }
                return;

            }

            ShowReceiverDialog(id, res.users, proccesType);
        }
    });



    // $.get("/WorkflowRuntime/CheckNextStepForNeedUsers",
    //     {
    //         entityId: id,
    //         proccesType: proccesType
    //     },
    //     function (res) {
    //         debugger;
    //         if (!res.success)
    //             return;

    //         if (!res.needSelectReceiver) {

    //             //if (proccesType == 1) {
    //             DoSubmit(id, null, proccesType);
    //             // }
    //             // else if (proccesType == 2) {

    //             //     SendToContractors(entityId, null);
    //             // }
    //             return;

    //         }

    //         ShowReceiverDialog(id, res.users, proccesType);
    //     });
}

// function ShowReceiverDialog(entityId, users, proccesType) {

//     let html = "";

//     html += "<select id='cmbReceiver' class='form-control'>";

//     $.each(users, function () {

//         html +=
//             `<option value="${this.id}">
//                 ${this.fullName}
//              </option>`;

//     });

//     html += "</select>";

//     bootbox.dialog({
//         title: "انتخاب گیرنده",
//         message: html,
//         buttons: {
//             ok: {
//                 label: "ارسال",
//                 className: "btn-success",

//                 callback: function () {
//                     if (proccesType == 1) {
//                         DoSubmit(
//                         entityId,
//                         $("#cmbReceiver").val(),
//                         proccesType
//                         );
//                     }
//                      else if (proccesType == 2) {
//                          SendToContractors(entityId, $("#cmbReceiver").val())
//                      }
//                 }
//             },
//             cancel: {

//                 label: "انصراف"

//             }

//         }

//     });

// }


function ShowReceiverDialog(entityId, users, proccesType) {


    debugger;

    let html = "";

    html += "<div id='receiverList' style='max-height:300px; overflow-y:auto;'>";

    $.each(users, function (index, user) {

        html += `
            <div class="form-check mb-2" style="padding:8px; border-bottom:1px solid #eee;">
                <label style="display:flex; align-items:center; gap:8px; cursor:pointer; margin:0;">
                    <input type="checkbox"
                           class="receiver-checkbox"
                           value="${user.id}">
                    <span>${user.fullName}</span>
                </label>
            </div>
        `;

    });

    html += "</div>";

    bootbox.dialog({
        title: "انتخاب گیرندگان",
        message: html,
        buttons: {
            ok: {
                label: "ارسال",
                className: "btn-success",

                callback: function () {

                    let selectedUsers = [];

                    $(".receiver-checkbox:checked").each(function () {
                        selectedUsers.push($(this).val());
                    });

                    if (selectedUsers.length === 0) {
                        bootbox.alert("لطفاً حداقل یک گیرنده انتخاب کنید.");
                        return false;
                    }

                    debugger;

                    if (proccesType == 1) {
                        DoSubmit(
                            entityId,
                            selectedUsers,
                            proccesType
                        );
                     }
                    else if (proccesType == 3) {
                        SendToContractors(
                            entityId,
                            selectedUsers
                        );
                    }
                }
            },

            cancel: {
                label: "انصراف"
            }
        }
    });
}

function DoSubmit(entityId, receiversUserId, proccesType) {
    debugger;

    $.ajax({

        url: "/WorkflowRuntime/Submit",

        type: "POST",
        traditional: true,
        data: {

            entityId: entityId,

            receiversUserId: receiversUserId,

            comment: "",

            proccesType: proccesType

        },

        success: function (res) {
            debugger;

            if (res.success) {

                toastr.success(res.message);

                ViewBarAvordClick();
            }
            else {

                toastr.info(res.message);
            }

        },
        error: function (xhr) {
            debugger;

            toastr.error(xhr.responseJSON?.message || "خطا در ارسال اطلاعات");
        }

    });
}

// function SubmitWorkflow(id) {

//     if (!confirm("آیا مرحله تکمیل شده است؟"))
//         return;

//     $.ajax({

//         url: "/WorkflowRuntime/Submit",

//         type: "POST",

//         data: {

//             entityId: id,

//             comment: ""

//         },

//         success: function (res) {

//             toastr.success(res.message);

//             ViewBarAvordClick();

//         },

//         error: function () {

//             toastr.error("خطا در انجام عملیات");

//         }

//     });

// }


//////////////////////
///////شماتیک گردش کار
/////////////////////


function ActionInfo(action) {

    switch (action) {

        case 1:
            return {
                title: "انجام مرحله",
                icon: "✓",
                css: "submit"
            };


        case 2:
            return {
                title: "تایید",
                icon: "✔",
                css: "approve"
            };


        case 3:
            return {
                title: "اصلاح",
                icon: "✎",
                css: "revision"
            };


        case 4:
            return {
                title: "رد",
                icon: "✖",
                css: "reject"
            };


        case 5:
            return {
                title: "لغو",
                icon: "⛔",
                css: "cancel"
            };

        default:
            return {
                title: "در انتظار تایید",
                icon: "✓",
                css: "waiting"
            };

    }

}

function StatusTitle(status) {

    let data = {

        1: "ایجاد شده",
        2: "در حال انجام",
        3: "متوقف شده",
        4: "تکمیل شده",
        5: "رد شده",
        6: "لغو شده"

    };


    return data[status];

}

function PersianDate(value) {

    return new persianDate(value)
        .format("YYYY/MM/DD HH:mm");

}

// function RenderWorkflow(data) {

//     $('#workflowSchimaTitle').html(data[0].projectName);

//     let html = "";


//     let total = data.length;

//     let completed =
//         data.filter(x => x.action == 2).length;


//     let percent =
//         Math.round((completed / total) * 100);



//     html += `
//     <div class="workflow-flow">
//     `;


//     data.forEach((x, index) => {
//         debugger;

//         let a = ActionInfo(x.action);


//         let current =
//             index == data.length - 1 &&
//                 x.status == 2
//                 ? "current"
//                 : "";

//         let DateSection = "";
//         if (x.actionDate != null) {
//             DateSection = `
//             <div>
//                 <span style="color:#1b0080 !important"  class="date">
//                     زمان تایید
//                 </span>
//             </div>
//             <div class="date">
//                 ${x.actionDateShamsi}
//             </div>`
//         }

//         html += `

//         <div class="step-card ${current}">


//             <div class="round-badge">
//                 مرحله ${x.roundNo.toLocaleString('fa-IR')}
//             </div>


//             <div class="step-icon ${a.css}">
//                 ${a.icon}
//             </div>


//             <h4>
//                 ${a.title}
//             </h4>


//             <div class="user">
//                 ${x.fullName}
//             </div>


//             <div class="company">
//                 ${x.companyName ?? ""}
//             </div>



//             ${DateSection}

//             ${x.comment
//                 ?
//                 `
//                 <div class="comment">
//                     ${x.comment}
//                 </div>
//                 `
//                 :
//                 ""
//             }


//         </div>
//         `;



//         // فلش بین کارت ها
//         if (index < data.length - 1) {

//             html += `
//             <div class="arrow">
//                 ➜
//             </div>
//             `;

//         }


//     });


//     html += `
//     </div>
//     `;


//     $("#workflowViewer").html(html);


//     $('#aworkflowSchimaShow').click();

// }




function RenderWorkflow(data) {

    if (!data || !data.steps || data.steps.length === 0) {
        $('#workflowSchimaTitle').html('');
        $("#workflowViewer").html(
            '<div class="alert alert-info">گردش کاری برای این پروژه ثبت نشده است.</div>'
        );
        $('#aworkflowSchimaShow').click();
        return;
    }

    $('#workflowSchimaTitle').html(data.projectName ?? "");

    let html = "";

    let totalSteps = data.steps.length;

    let completedSteps = data.steps.filter(x =>
        x.assignments.length > 0 &&
        x.assignments.every(a => a.isCompleted)
    ).length;

    let percent = totalSteps > 0
        ? Math.round((completedSteps / totalSteps) * 100)
        : 0;

    html += `
        <div class="workflow-container">

            <div class="workflow-progress">
                <div class="progress-info">
                    <span>پیشرفت گردش کار</span>
                    <strong>${percent.toLocaleString('fa-IR')}٪</strong>
                </div>

                <div class="progress">
                    <div class="progress-bar"
                         role="progressbar"
                         style="width:${percent}%">
                    </div>
                </div>
            </div>

            <div class="workflow-flow">
    `;

    data.steps.forEach((step, index) => {

        let current = step.isCurrent ? "current" : "";

        let statusCss = "pending";
        let statusIcon = "○";

        if (step.assignments.length > 0 &&
            step.assignments.every(x => x.isCompleted)) {

            statusCss = "completed";
            statusIcon = "✓";

        }
        else if (step.isCurrent) {

            statusCss = "in-progress";
            statusIcon = "●";

        }

        let roundText = step.roundNo != null
            ? `دور ${step.roundNo.toLocaleString('fa-IR')}`
            : "بدون ارجاع";

        html += `
            <div class="workflow-step ${current}">

                <div class="step-card">

                    <div class="step-header">

                        <span class="round-badge">
                            مرحله ${step.order.toLocaleString('fa-IR')}
                        </span>

                        <span class="round-number">
                            ${roundText}
                        </span>

                    </div>

                    <div class="step-title">
                        <span class="step-status-icon ${statusCss}">
                            ${statusIcon}
                        </span>

                        <h4>${step.stepTitle}</h4>
                    </div>

                    <div class="step-status ${statusCss}">
                        ${step.statusTitle}
                    </div>
        `;

        // گیرندگان واقعی مرحله
        if (step.assignments && step.assignments.length > 0) {

            html += `
                <div class="assignment-list">

                    <div class="section-title">
                        <i class="fa fa-users"></i>
                        گیرندگان ارجاع
                    </div>
            `;

            step.assignments.forEach(user => {

                let userCss = "waiting";
                let userIcon = "fa-clock";
                let userStatus = "در انتظار اقدام";

                if (user.isCompleted) {

                    if (user.result != null) {

                        // مقادیر Enum را مطابق تعریف واقعی تنظیم کن
                        let result = user.result;

                        if (result == 1) {
                            userCss = "approved";
                            userIcon = "fa-check-circle";
                            userStatus = "تأیید شده";
                        }
                        else if (result == 2) {
                            userCss = "rejected";
                            userIcon = "fa-times-circle";
                            userStatus = "رد شده";
                        }
                        else {
                            userCss = "completed";
                            userIcon = "fa-check";
                            userStatus = user.resultTitle;
                        }

                    }
                    else {
                        userCss = "completed";
                        userIcon = "fa-check";
                        userStatus = user.resultTitle || "تکمیل شده";
                    }
                }

                html += `
                    <div class="assignment-item ${userCss}">

                        <div class="assignment-user">

                            <div class="user-icon">
                                <i class="fa ${userIcon}"></i>
                            </div>

                            <div class="user-info">

                                <div class="user-name">
                                    ${user.fullName}
                                </div>

                                <div class="company">
                                    ${user.companyName || ""}
                                </div>

                            </div>

                            <span class="user-status">
                                ${userStatus}
                            </span>

                        </div>
                `;

                if (user.assignDate) {
                    html += `
                        <div class="assignment-date">
                            تاریخ ارجاع:
                            ${user.assignDate}
                        </div>
                    `;
                }

                if (user.completedDate) {
                    html += `
                        <div class="assignment-date">
                            تاریخ اقدام:
                            ${user.completedDate}
                        </div>
                    `;
                }

                if (user.comment) {
                    html += `
                        <div class="assignment-comment">
                            ${user.comment}
                        </div>
                    `;
                }

                html += `</div>`;

            });

            html += `</div>`;

        }
        else if (!step.isCurrent) {

            html += `
                <div class="no-assignment">
                    هنوز ارجاعی برای این مرحله ثبت نشده است.
                </div>
            `;

        }

        // تاریخچه اقدامات
        if (step.histories && step.histories.length > 0) {

            html += `
                <div class="history-list">

                    <div class="section-title">
                        <i class="fa fa-history"></i>
                        تاریخچه اقدامات
                    </div>
            `;

            step.histories.forEach(history => {

                let actionInfo = ActionInfo(history.action);

                html += `
                    <div class="history-item">

                        <div class="history-action ${actionInfo.css}">
                            ${actionInfo.icon}
                        </div>

                        <div class="history-content">

                            <div class="history-user">
                                ${history.fullName}
                            </div>

                            <div class="history-action-title">
                                ${history.actionTitle}
                            </div>

                            <div class="history-date">
                                ${history.actionDateShamsi || ""}
                            </div>

                            ${history.comment
                        ? `<div class="history-comment">${history.comment}</div>`
                        : ""
                    }

                        </div>

                    </div>
                `;

            });

            html += `</div>`;

        }

        html += `
                </div>
            </div>
        `;

        // فلش بین مراحل
        if (index < data.steps.length - 1) {

            html += `
                <div class="arrow">
                    <i class="fa fa-arrow-left"></i>
                </div>
            `;

        }

    });

    html += `
            </div>
        </div>
    `;

    $("#workflowViewer").html(html);

    $('#aworkflowSchimaShow').click();
}



function SchimaStepsForCurrentBaravordUser(id) {

    $.ajax({

        url: "/WorkflowStep/GetSchimaStepsForCurrentBaravordUser",

        type: "POST",

        data: {
            entityId: id
        },

        success: function (res) {

            RenderWorkflow(res);

        },

        error: function (xhr) {

            toastr.error("خطا در دریافت اطلاعات گردش کار");

        }

    });

}

///////////////////
// function SchimaStepsForCurrentBaravordUser(id) {

//     $.ajax({

//         url: "/WorkflowStep/GetSchimaStepsForCurrentBaravordUser",

//         type: "POST",

//         data: {

//             entityId: id

//         },

//         success: function (res) {

//             RenderWorkflow(res);


//         },

//         error: function () {

//             toastr.error("خطا در انجام عملیات");

//         }

//     });

// }

////////////////////////////////
/////////////انتهای شماتیک گردش کار
/////////////////////////////
function ReturnWorkflow(id) {

    let comment =
        prompt("علت بازگشت را وارد نمایید");

    if (comment == null)
        return;

    $.ajax({

        url: "/WorkflowRuntime/Revision",

        type: "POST",

        data: {

            entityId: id,

            comment: comment

        },

        success: function (res) {

            toastr.success(res.message);

            ViewBarAvordClick();

        },

        error: function () {

            toastr.error("خطا در انجام عملیات");

        }

    });

}

/////////
function MainTabledblClick(Type, Title) {
    $('#HDFBAType').val(Type);
    if (Type == 1) {
        goToBarAvord(Title);
    }

}
/////////

////////////
function MainTableClick(Obj, Type) {
    debugger;
    $('.rowSelected').removeClass('rowSelected');
    $(Obj).addClass('rowSelected');
    $('#HDFID').val($(Obj).find("td:nth-child(1)").html());
    $('#HDFYear').val(convertPersianToEnglish($(Obj).find("td:nth-child(7)").html()));
    $('#HDFBAType').val($(Obj).find("td:nth-child(6)").html());
    //$('#HDFNoeFB').val($(Obj).find("td:nth-child(8)").html());

    $('#aCurrentProject').html($(Obj).find("td:nth-child(7)").html() + " - " + $(Obj).find("td:nth-child(3)").html());
    if (Type == 3) {
        GetSooratVaziat($(Obj).find("td:nth-child(1)").html());
    }
}


/////
function ViewBarAvordReportClick() {
    $.ajax({
        type: "POST",
        url: "/BaravordUser/List",
        data: JSON.stringify(""),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("[id*=table]").html('');
            var row = $("[id*=table] tr:last-child").clone(true);
            var row = $("<thead><tr><td style='display:none'></td><td >شماره</td ><td >عنوان</td><td >فهرست بها</td><td >تاریخ</td><td >نوع</td><td >سال</td><td style='display:none'></td></tr></thead>")
            $("[id*=table]").append(row);
            $("[id*=table] tr").not($("[id*=table] tr:first-child")).remove();
            $.each(response, function () {
                var row = $(`<tr><td style="display:none"></td><td width="10%"></td ><td width="30%"></td>
                                      <td width="20%"></td><td width="30%"></td><td width="10%"></td><td width="10%"></td>
                                      <td style="display:none"></td></tr>
                                      `)
                row.find("td:nth-child(1)").html($(this)[0].ID);
                row.find("td:nth-child(2)").html($(this)[0].Num);
                row.find("td:nth-child(3)").html($(this)[0].Name);
                row.find("td:nth-child(4)").html($(this)[0].NoeFBName);
                row.find("td:nth-child(5)").html($(this)[0].Date);
                row.find("td:nth-child(6)").html($(this)[0].Type);
                row.find("td:nth-child(7)").html($(this)[0].Year);
                row.find("td:nth-child(8)").html($(this)[0].NoeFB);
                $("[id*=table]").append(row);
            });
            $("[id*=table] tr").click(function () {
                $('.rowSelected').removeClass('rowSelected');
                $(this).addClass('rowSelected');
                $('#HDFID').val($(this).find("td:nth-child(1)").html());
                $('#HDFBAType').val($(this).find("td:nth-child(6)").html());
                $('#HDFYear').val(convertPersianToEnglish($(this).find("td:nth-child(7)").html()));
                $('#HDFNoeFB').val($(this).find("td:nth-child(8)").html());
            });

            $("[id*=table] tr").dblclick(function () {
                goToBarAvordReport();
            });

            $('#aViewBarAvordReport').click();
        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری برآورد', 'خطا');
        }
    });
}

///////
function GetJamBarAvordAll(buId) {
    BarAvordId = buId;
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    vardata.Year = Year;
    vardata.BarAvordId = BarAvordId;
    $.ajax({
        type: "post",
        url: "/BarAvordUser/CalculateBarAvordAll",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            jameFasl = response.jameFasl;
            jameFaslStar = response.jameFaslStar;
            jameFaslKol = response.jameFaslKol;
            jameFaslInZarib = response.jameFaslInZarib;
            jameFaslStarInZarib = response.jameFaslStarInZarib;
            jameFaslKolInZarib = response.jameFaslKolInZarib;
            $('#tdJamBarAvordKolWithZarib-' + buId).html(formatNumber(jameFaslKolInZarib.toFixed(0)));
            //$('#tdJamBarAvordKolWithZaribPGh-' + buId).html(formatNumber(jameFaslKolInZarib.toFixed(0)));
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}


function GetPishnahadGhaymat(buId) {

    CompanyId = $('#HDFCompanyID').val();

    BarAvordUserId = buId;// $('#HDFBarAvordUserID').val();
    //NoeFB = parseInt($('#HDFNoeFB').val());
    Year = parseInt($('#HDFYear').val());

    var vardata = new Object();
    //vardata.NoeFBId = NoeFB;
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

            SumPishnahadPaymanKarTajhiz = response.sumPishnahadPaymanKarTajhiz;

            $('#tdJamBarAvordKolWithZaribPGh-' + buId).html(formatNumber(SumPishnahadPaymanKarTajhiz.toFixed(0)));

        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}


////
function GetSooratVaziat(buId) {

    CompanyId = $('#HDFCompanyID').val();
    Year = parseInt($('#HDFYear').val());


    var vardata = new Object();
    vardata.BaravordUserId = buId;
    vardata.CompanyId = CompanyId;
    vardata.Year = Year;

    $.ajax({
        type: "POST",
        url: "/BaravordUser/ListSooratVaziat",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            barAvord = response.barAvord;
            PGhDate = response.pGhDate;// '1402/02/02';
            //PGhMablagh = response.pGhMablagh;// 45345345;

            var str = `
            <table>
                <tr style="background-color:#ccc;">
                    <td style="width:5%">شماره
                    </td>
                    <td style="width:25%">عنوان
                    </td>
                    <td style="width:10%">از تاریخ
                    </td>
                    <td style="width:10%">تا تاریخ
                    </td>
                    <td style="width:13%">مبلغ (ریال)
                    </td>
                    <td style="width:13%">تعدیل
                    </td>
                    <td style="width:13%">مابه التفاوت
                    </td>
                    <td style="width:11%">عملیات
                    </td>
                </tr>
                `;

            lastNum = 0;
            lastDate = '';

            str += `
                <tr ondblclick="goToBarAvordSV('${buId}',2,'برآورد اولیه')">
                    <td>
                    </td>
                    <td>برآورد اولیه
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <span id="tdJamBarAvordKolWithZarib-${buId}">
                        </span>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                 </tr>
            `
            str += `
                 <tr style="background-color: rgb(165 182 205);" ondblclick="goToPishnahadGhaymat('${buId}',2,'پیشنهاد قیمت')">
                    <td>
                    </td>
                    <td>پیشنهاد قیمت
                    </td>
                    <td>
                        <span id="tdDatePGh-${buId}">
                            ${toPersianDigits(PGhDate)}
                        </span>
                    </td>
                    <td>
                    </td>
                    <td>
                        <span id="tdJamBarAvordKolWithZaribPGh-${buId}">
                        </span>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                 </tr>
            `


            if (barAvord.length != 0) {

                $.each(barAvord, function () {
                    jameFaslKolInZarib = formatNumber($(this)[0].calculateBarAvord.jameFaslKolInZarib.toFixed(0));

                    if (parseInt(lastNum) < parseInt($(this)[0].buNum)) {
                        lastNum = parseInt($(this)[0].buNum);
                        lastDate = $(this)[0].toDate;
                    }

                    str += `
                    <tr ondblclick="goToBarAvordSV('${$(this)[0].buId}',${$(this)[0].buType},'${$(this)[0].buName}')">
                    <td>${$(this)[0].buNum}
                    </td>
                    <td> ${$(this)[0].buName}
                    </td>
                    <td>${toPersianDigits($(this)[0].fromDate)}
                    </td>
                    <td>${toPersianDigits($(this)[0].toDate)}
                    </td>
                    <td>
                        <span id="tdJamBarAvordKolWithZarib-${$(this)[0].buId}">
                            ${jameFaslKolInZarib}
                        </span>
                    </td>
                    <td>
                        <span class="spanAllTadilStyle" id="AllTadil-${$(this)[0].buId}" onclick="OnClickShowTadilIndex('${$(this)[0].buId}',${$(this)[0].buType},'${$(this)[0].buName}')"></span>
                    </td>
                    <td>
                    </td>
                    <td>
                        <button type="button" onclick="Delete(this)" class="ButtonRowsSaveStyle btnDelete" style="float:unset;" data-id="${$(this)[0].buId}">
                            <i id="iSave" class="fa fa-trash DelRMUStyle"></i>
                        </button>
                    </td>
                    </tr>
                `;
                });
            }


            lastNum++;

            str += `
                <tr style="background-color: #cfc3a9;">
                    <td style="width:5%">
                    <i class="fa fa-plus SaveRMUStyle"></i>
                    </td>
                    <td style="width:25%;">
                    <select id="drdSooratVaziatTitle-${buId}" style="height: 26px;" class="form-control input">
                        <option value="1">صورت وضعیت موقت شماره ${lastNum}</option>
                        <option value="2">صورت وضعیت ماقبل قطعی</option>
                        <option value="3">صورت وضعیت قطعی</option>
                    </select>
                    </td>
                    <td style="width:10%">
                        <input id="SooratVaziatFromDate-${buId}" type="text" class="form-control datepicker-input" placeholder="از تاریخ" />
                    </td>
                    <td style="width:10%">
                        <input id="SooratVaziatToDate-${buId}" type="text" class="form-control datepicker-input" placeholder="تا تاریخ"/>
                    </td>
                    <td style="width:13%">
                    </td>
                    <td style="width:13%">
                    </td>
                    <td style="width:13%">
                    </td>
                    <td style="width:11%"><button  type="button" onclick="SaveInTableSooratVaziat(this,'${buId}')" class="ButtonRowsSaveStyle"><i id="iSave" class="fa fa-save SaveRMUStyle"></i></button>
                    </td>
                </tr>
                </table>
                `;

            $('#divSooratVaziat_' + buId).html(str);

            // ================================
            // تا تاریخ
            // ================================

            $(`#SooratVaziatToDate-${buId}`).prop('disabled', true);

            $(`#SooratVaziatToDate-${buId}`).persianDatepicker({
                format: 'YYYY/MM/DD',
                autoClose: true,
                initialValue: false,
                observer: true,
                theme: 'light',
                maxDate: new persianDate().valueOf()
            });


            // ================================
            // از تاریخ
            // ================================

            // var minDateValue = null;

            // if (lastDate) {
            //     var enDate = faToEn(lastDate);
            //     var p = enDate.split('/');
            //     minDateValue = new persianDate([
            //         +p[0],
            //         +p[1],
            //         +p[2]
            //     ]).valueOf();
            // }


            var minDateValue = null;

            if (lastDate) {

                var enDate = faToEn(lastDate);
                var p = enDate.split('/');

                var oneDay = 24 * 60 * 60 * 1000;

                minDateValue = new persianDate([
                    +p[0],
                    +p[1],
                    +p[2]
                ]).valueOf() + oneDay;
            }

            $(`#SooratVaziatFromDate-${buId}`).persianDatepicker({

                format: 'YYYY/MM/DD',
                autoClose: true,
                initialValue: false,
                observer: true,
                theme: 'light',
                maxDate: new persianDate().valueOf(),
                minDate: minDateValue,

                // onSelect: function (unix) {

                //     // فعال شدن تاریخ دوم
                //     $(`#SooratVaziatToDate-${buId}`).prop('disabled', false);

                //     // پاک کردن مقدار قبلی
                //     $(`#SooratVaziatToDate-${buId}`).val('');

                //     // حذف دیت‌پیکر قبلی
                //     $(`#SooratVaziatToDate-${buId}`).data('datepicker').destroy();

                //     // ساخت مجدد با minDate
                //     $(`#SooratVaziatToDate-${buId}`).persianDatepicker({

                //         format: 'YYYY/MM/DD',
                //         autoClose: true,
                //         initialValue: false,
                //         observer: true,
                //         theme: 'light',

                //         minDate: unix,
                //         maxDate: new persianDate().valueOf()

                //     });
                // }


                onSelect: function (unix) {

                    $(`#SooratVaziatToDate-${buId}`).prop('disabled', false);

                    $(`#SooratVaziatToDate-${buId}`).val('');

                    $(`#SooratVaziatToDate-${buId}`).data('datepicker').destroy();

                    var oneDay = 24 * 60 * 60 * 1000;

                    $(`#SooratVaziatToDate-${buId}`).persianDatepicker({
                        format: 'YYYY/MM/DD',
                        autoClose: true,
                        initialValue: false,
                        observer: true,
                        theme: 'light',
                        minDate: unix + oneDay,
                        maxDate: new persianDate().valueOf()
                    });
                }
            });




            $('#divSooratVaziat_' + buId).toggle();


            GetJamBarAvordAll(buId);
            GetPishnahadGhaymat(buId);

            $.each(barAvord, function () {
                debugger;
                GetTadilValue($(this)[0].buId);
            });
        },
        error: function (response) {
            toastr.error('مشکل در بارگذاری برآورد', 'خطا');
        }
    });
}


function GetTadilValue(SVId) {
    debugger;
    CompanyId = $('#HDFCompanyID').val();
    Year = parseInt($('#HDFYear').val());

    strUrl = '/TadilIndex/SumTadilIndex';

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
            sumMablaghTadilAll = 0;
            response.lstresultTadilIndexDtos.forEach(function (noeFb) {
                sumMablaghTadilAll += noeFb.sumMablaghTadil;
            });
            $('#AllTadil-' + SVId).html(formatNumber(sumMablaghTadilAll));

        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function goToPishnahadGhaymat(buId, buType, Title) {

    // var BAId = $('#HDFID').val();
    // var BAType = $('#HDFBAType').val();


    //var NoeFB = $('#HDFNoeFB').val();
    var Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.BAId = buId;
    vardata.BAType = buType;
    vardata.Year = parseInt(Year);
    //vardata.NoeFB = parseInt(NoeFB);
    $.ajax({
        type: "POST",
        url: "/BaseInfo/SetViewBag",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {

                $('#spanProjectTitle').html(info[1] + " - " + Title);

                $('#content-body').html('<div class="text-center p-5">در حال بارگذاری...</div>'); // پیام موقت
                $('#content-body').load('/PishnahadGhaymat');
                $('.modal-backdrop.fade.show').remove();
            }
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });

}

function faToEn(str) {
    return str.replace(/[۰-۹]/g, function (d) {
        return d.charCodeAt(0) - 1776;
    });
}

function formatNumber(num) {
    if (num == null) return '';
    return Number(num).toLocaleString('fa-IR'); // یا 'en-US' برای کاما
}

function SooratVaziatClick(Obj) {
    $('.rowSelected').removeClass('rowSelected');
    $(Obj).addClass('rowSelected');
    $('#HDFID').val($(Obj).find("td:nth-child(1)").html());
    $('#HDFBAType').val($(Obj).find("td:nth-child(7)").html());
    $('#HDFYear').val(convertPersianToEnglish($(Obj).find("td:nth-child(6)").html()));
    $('#HDFNoeFB').val($(Obj).find("td:nth-child(8)").html());
    $('#aCurrentProject').html($(Obj).find("td:nth-child(7)").html() + " - " + $(this).find("td:nth-child(3)").html());
}

function goToBarAvordSV(buId, buType, Title) {
    $('#HDFID').val(buId);
    $('#HDFBAType').val(buType);
    goToBarAvord(Title);
}

// function SooratVaziatdblClick(Obj) {
//     goToBarAvord("");
// }

function OnClickShowTadilIndex(buId, buType, Title) {
    $('#HDFID').val(buId);
    $('#HDFBAType').val(buType);

    $('#HDFBarAvordUserID').val(buId);


    //var NoeFB = $('#HDFNoeFB').val();
    var Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.BAId = buId;
    vardata.BAType = buType;
    vardata.Year = parseInt(Year);
    //vardata.NoeFB = parseInt(NoeFB);
    $.ajax({
        type: "POST",
        url: "/BaseInfo/SetViewBag",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {

                $('#spanProjectTitle').html(info[1] + " - " + " تعدیل " + Title);

                $('#content-body').html('<div class="text-center p-5">در حال بارگذاری...</div>'); // پیام موقت
                $('#content-body').load('/TadilIndex/ShowTadilIndex');
                $('.modal-backdrop.fade.show').remove();
            }
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function goToBarAvord(Title) {
    var BAId = $('#HDFID').val();
    var BAType = $('#HDFBAType').val();


    //var NoeFB = $('#HDFNoeFB').val();
    var Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.BAId = BAId;
    vardata.BAType = BAType;
    vardata.Year = parseInt(Year);
    //vardata.NoeFB = parseInt(NoeFB);
    $.ajax({
        type: "POST",
        url: "/BaseInfo/SetViewBag",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            info = response.split('_');
            if (info[0] == "OK") {

                $('#spanProjectTitle').html(info[1] + " - " + Title);

                $('#content-body').html('<div class="text-center p-5">در حال بارگذاری...</div>'); // پیام موقت
                //$('#content-body').load('/Operation/ShowTree');
                $('#content-body').load('/ShowBarAvordUser');
                $('.modal-backdrop.fade.show').remove();
            }
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}

function goToBarAvordReport() {
    var BAId = $('#HDFID').val();
    var NoeFB = $('#HDFNoeFB').val();
    var Year = $('#HDFYear').val();
    var vardata = new Object();
    vardata.BAId = BAId;
    vardata.Year = Year;
    vardata.NoeFB = NoeFB;
    $.ajax({
        type: "POST",
        url: "/BaseInfo/SetViewBag",
        data: JSON.stringify(vardata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response == "OK")
                window.location.href = '/Operation/ShowTree';
        },
        error: function (response) {
            toastr.error('خطا', 'خطا');
        }
    });
}