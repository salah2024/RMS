let selectedWorkflowId = null;

$(function () {
    loadWorkflows();
    loadRoles();

    loadUsers();
});


function saveWorkflow() {
    debugger;
    let model =
    {
        id: $("#WorkflowId").val()
            || "00000000-0000-0000-0000-000000000000",

        name: $("#WorkflowName").val(),

        proccesType:parseInt($("#WorkflowProcessType").val()),

        isDefault: $("#WorkflowIsDefault").is(":checked"),

        isSystem: $("#WorkflowIsSystem").is(":checked"),

        auto: $("#WorkflowAuto").is(":checked"),

        isActive:
            $("#WorkflowIsActive").is(":checked")
    };

    debugger;

    $.ajax({

        url: "/Workflow/Save",

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),


        success: function (res) {

            if (res.success) {

                toastr.success(
                    "گردش با موفقیت ذخیره شد");


                clearWorkflowForm();


                loadWorkflows();

            }
            else {

                toastr.error(res.message);

            }

        }

    });

}

$(document).on(
    "click",
    ".workflow-item",
    function (e) {

        debugger;

        if ($(e.target).hasClass(
            "btn-delete-workflow"))
            return;


        let id = $(this)
            .data("id");


        loadSteps(id);


        $("#btnAddStep")
            .prop("disabled", false);


        $("#btnAddStep")
            .data("workflow-id", id);


    });


$(document).on("click", "#btnAddWorkflow", function () {
    newWorkflow();

});

function newWorkflow() {

    $("#WorkflowId").val("");

    $("#WorkflowName").val("");

    $("#WorkflowIsActive")
        .prop("checked", true);

    $("#WorkflowAuto")
        .prop("checked", false);

    $("#workflowMode")
        .text("گردش جدید");

    $("#workflowForm").slideDown();
}

$(document).on("click", "#btnCancelWorkflow", function () {

    clearWorkflowForm();

});

function clearWorkflowForm() {

    $("#WorkflowId").val("");

    $("#WorkflowName").val("");

    $("#WorkflowProcessType").val("1");

    $("#WorkflowIsDefault").prop("checked", false);

    $("#WorkflowIsSystem").prop("checked", false);

    $("#WorkflowAuto").prop("checked", false);

    $("#WorkflowIsActive").prop("checked", true);

    $("#WorkflowProcessType").prop("disabled", false);

    $("#workflowMode").text("");

    $("#workflowForm").slideUp();

}

$("#btnAddStep").click(function () {

    clearStepForm();


    $("#StepWorkflowId")
        .val(
            $(this).data("workflow-id")
        );


    $("#stepForm")
        .slideDown();


});


$("#btnSaveStep").click(function () {

    let model =
    {

        id:
            $("#WorkflowStepId").val()
            ||
            "00000000-0000-0000-0000-000000000000",


        workflowId:
            $("#StepWorkflowId").val(),


        title:
            $("#StepTitle").val(),


        allowReturn:
            $("#StepAllowReturn").is(":checked"),


        isFinal:
            $("#StepIsFinal").is(":checked"),


        needSelectReceiver:
            $("#StepNeedSelectReceiver").is(":checked"),


        roleIds:
            $("#StepRoles").val() || [],


        userIds:
            $("#StepUsers").val() || []

    };



    $.ajax({

        url: "/WorkflowStep/SaveStep",

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(model),

        success: function (res) {

            if (res.success) {

                toastr.success("مرحله با موفقیت ذخیره شد.");

                $("#stepForm").slideUp();

                clearStepForm();

                loadSteps(model.workflowId);

            }
            else {

                toastr.error(res.message);

            }

        },

        error: function (xhr) {

            toastr.error("خطا در ثبت مرحله");

            console.log(xhr.responseText);

        }

    });

});


function clearStepForm() {

    $("#WorkflowStepId").val("");

    $("#StepTitle").val("");

    $("#StepAllowReturn")
        .prop("checked", false);


    $("#StepIsFinal")
        .prop("checked", false);


    $("#StepNeedSelectReceiver")
        .prop("checked", false);


    $("#StepRoles")
        .val([]);


    $("#StepUsers")
        .val([]);


}

function loadWorkflows() {

    $("#workflowList").html(`
        <li class="list-group-item text-center">
            <i class="fa fa-spinner fa-spin"></i>
            در حال دریافت...
        </li>
    `);



    $.ajax({

        url: "/Workflow/GetWorkflows",

        type: "GET",


        success: function (data) {

            let html = "";


            if (data.length === 0) {

                html = `

                <li class="list-group-item text-center">

                    گردش کاری تعریف نشده است

                </li>

                `;

            }
            else {

                $.each(data, function (i, item) {
                    debugger;

                    html += `

                    <li class="list-group-item workflow-item"
                        data-id="${item.id}"
                        onclick="editWorkflow('${item.id}')">


                        <div class="d-flex justify-content-between">


                            <span>

                                <i class="fa fa-sitemap text-primary"></i>

                                ${item.name}

                            </span>


                            <span class="badge bg-secondary">

                                ${item.stepCount}

                            </span>

                                <button class="btn btn-danger btn-sm"
                                    onclick="deleteWorkflow('${item.id}')">

                                    <i class="fa fa-trash"></i>

                                </button>


                        </div>


                    </li>

                    `;


                });

            }


            $("#workflowList").html(html);

        },


        error: function () {

            toastr.error(
                "خطا در دریافت گردش‌ها");

        }

    });

}

function editWorkflow(id) {

    $.ajax({

        url: "/Workflow/Get?id=" + id,

        type: "GET",

        success: function (data) {

            if (!data) {
                toastr.error("گردش مورد نظر یافت نشد.");
                return;
            }

            $("#workflowMode").text("ویرایش گردش");

            $("#WorkflowId").val(data.id);

            $("#WorkflowName").val(data.name);

            $("#WorkflowProcessType").val(data.proccesType);

            $("#WorkflowIsDefault").prop("checked", data.isDefault);

            $("#WorkflowIsSystem").prop("checked", data.isSystem);

            $("#WorkflowIsActive").prop("checked", data.isActive);

            $("#WorkflowAuto").prop("checked", data.auto);

            // اگر فرآیند سیستمی است، اجازه تغییر نوع فرآیند را نده
            if (data.isSystem) {
                $("#WorkflowProcessType").prop("disabled", true);
            }
            else {
                $("#WorkflowProcessType").prop("disabled", false);
            }

            $("#workflowForm").slideDown();

            $("#WorkflowName").focus();
        },

        error: function () {
            toastr.error("خطا در دریافت اطلاعات گردش");
        }

    });

}
// function renderWorkflowList(data) {

//     let html = "";

//     if (data.length === 0) {

//         html = `
//             <li class="list-group-item text-center p-5">

//                 گردش کاری تعریف نشده است

//             </li>`;

//         $("#workflowList").html(html);

//         return;
//     }

//     $.each(data, function (index, item) {

//         html += `

// <li class="list-group-item workflow-item"

//     data-id="${item.id}">

// <div class="d-flex justify-content-between align-items-center">

//     <div>

//         <i class="fa fa-sitemap text-primary"></i>

//         ${item.name}

//     </div>

//     <span class="badge bg-secondary">

//         ${item.stepCount}

//     </span>

//     <button class="btn btn-danger btn-sm"
//                     onclick="deleteWorkflow('${item.id}')">

//                 <i class="fa fa-trash"></i>

//             </button>

// </div>

// </li>

// `;

//     });

//     $("#workflowList").html(html);

//     //-------------------------------------------------
//     // انتخاب اولین گردش
//     //-------------------------------------------------

//     selectedWorkflowId = data[0].id;

//     highlightSelectedWorkflow();

//     loadSteps(selectedWorkflowId);

//     $("#btnAddStep").prop("disabled", false);

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

    let completedSteps = data.steps.filter(step =>
        step.assignments &&
        step.assignments.length > 0 &&
        step.assignments.every(a => a.isCompleted)
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

        let assignments = step.assignments || [];
        let histories = step.histories || [];
        let potentialReceivers = step.potentialReceivers || [];

        let current = step.isCurrent ? "current" : "";

        // وضعیت ظاهری مرحله
        let statusCss = "pending";
        let statusIcon = "○";

        if (
            step.statusTitle === "اقدامات تکمیل شده" ||
            step.statusTitle === "تأیید شده"
        ) {
            statusCss = "completed";
            statusIcon = "✓";
        }
        else if (step.statusTitle === "رد شده") {
            statusCss = "rejected";
            statusIcon = "✕";
        }
        else if (step.statusTitle === "درخواست اصلاح") {
            statusCss = "revision";
            statusIcon = "↻";
        }
        else if (step.isCurrent) {
            statusCss = "in-progress";
            statusIcon = "●";
        }

        let roundText = step.roundNo != null
            ? `مرحله ${step.order.toLocaleString('fa-IR')} - دور ${step.roundNo.toLocaleString('fa-IR')}`
            : `مرحله ${step.order.toLocaleString('fa-IR')}`;

        html += `
            <div class="workflow-step ${current}">

                <div class="step-card">

                    <div class="step-header">

                        <span class="round-badge">
                            ${roundText}
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

        // =====================================
        // افراد تعیین شده برای ارجاع
        // فقط وقتی هنوز ارجاع واقعی وجود ندارد
        // =====================================

        if (assignments.length === 0 && potentialReceivers.length > 0) {

            html += `
                <div class="assignment-list">

                    <div class="section-title">
                        <i class="fa fa-users"></i>
                        فرد تعیین‌شده برای ارجاع
                    </div>
            `;

            potentialReceivers.forEach(user => {

                html += `
                    <div class="assignment-item waiting">

                        <div class="assignment-user">

                            <div class="user-icon">
                                <i class="fa fa-user"></i>
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
                                در انتظار ارجاع
                            </span>

                        </div>

                    </div>
                `;

            });

            html += `</div>`;
        }

        // =====================================
        // گیرندگان واقعی مرحله
        // =====================================

        if (assignments.length > 0) {

            html += `
                <div class="assignment-list">

                    <div class="section-title">
                        <i class="fa fa-users"></i>
                        گیرندگان ارجاع
                    </div>
            `;

            assignments.forEach(user => {

                let userCss = "waiting";
                let userIcon = "fa-clock";
                let userStatus = "در انتظار اقدام";

                // مقادیر واقعی WorkflowAssignmentResult
                switch (user.result) {

                    case 1: // Pending
                        userCss = "waiting";
                        userIcon = "fa-clock";
                        userStatus = "در انتظار اقدام";
                        break;

                    case 2: // Submitted
                        userCss = "completed";
                        userIcon = "fa-paper-plane";
                        userStatus = "ارسال شده";
                        break;

                    case 6: // Approved
                        userCss = "completed";
                        userIcon = "fa-check-circle";
                        userStatus = "تأیید شده";
                        break;

                    case 3: // RevisionRequested
                        userCss = "revision";
                        userIcon = "fa-refresh";
                        userStatus = "درخواست اصلاح";
                        break;

                    case 4: // Rejected
                        userCss = "rejected";
                        userIcon = "fa-times-circle";
                        userStatus = "رد شده";
                        break;

                    case 5: // Cancelled
                        userCss = "cancelled";
                        userIcon = "fa-ban";
                        userStatus = "لغو شده";
                        break;

                    default:
                        userStatus = user.resultTitle || "نامشخص";
                        break;
                }

                // فقط وضعیت‌های Pending و نامشخص
                // در صورت تکمیل‌نشدن، در انتظار اقدام هستند.
                // وضعیت‌های نهایی مثل Approved و Cancelled حفظ می‌شوند.
                if (
                    !user.isCompleted &&
                    (user.result == null || user.result === 1)
                ) {
                    userCss = "waiting";
                    userIcon = "fa-clock";
                    userStatus = "در انتظار اقدام";
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
        else if (potentialReceivers.length === 0 && !step.isCurrent) {

            html += `
                <div class="no-assignment">
                    هنوز ارجاعی برای این مرحله ثبت نشده است.
                </div>
            `;

        }

        // =====================================
        // تاریخچه اقدامات
        // =====================================

        if (histories.length > 0) {

            html += `
                <div class="history-list">

                    <div class="section-title">
                        <i class="fa fa-history"></i>
                        تاریخچه اقدامات
                    </div>
            `;

            histories.forEach(history => {

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

function highlightSelectedWorkflow() {

    $(".workflow-item").removeClass("active");

    $(".workflow-item[data-id='" + selectedWorkflowId + "']")
        .addClass("active");

}

// $(document).on("click", ".workflow-item", function () {

//     selectedWorkflowId = $(this).data("id");

//     highlightSelectedWorkflow();

//     loadSteps(selectedWorkflowId);

// });


$(document).on("click", ".btnDeleteStep", function () {

    let td = $(this).closest("td");

    td.find(".btnDeleteStep").hide();

    td.find(".delete-confirm").removeClass("d-none");

});


$(document).on("click", ".btnDeleteNo", function () {

    let td = $(this).closest("td");

    td.find(".delete-confirm").addClass("d-none");

    td.find(".btnDeleteStep").show();

});

function loadSteps(workflowId) {

    $("#stepList").html(`

    <tr>
        <td colspan="3"
            class="text-center">

            در حال دریافت...

        </td>
    </tr>

    `);


    $.ajax({

        url:
            "/WorkflowStep/GetStep?Id="
            + workflowId,


        type: "GET",


        success: function (data) {

            let html = "";
                debugger;


            $.each(data.data, function (i, item) {

                debugger;

                html += `

                <tr>

                    <td>

        ${item.order}

    </td>

    <td>

        ${item.title}

    </td>

    <td>

        ${item.roleNames}

    </td>

    <td>

    <span style="word-break: break-word;white-space: normal;">
        ${item.userNames}
        </span>
    </td>

    <td>

        ${item.allowReturn
                        ?
                        '<span class="badge bg-success">دارد</span>'
                        :
                        '<span class="badge bg-secondary">ندارد</span>'
                    }

    </td>

    <td>

        ${item.isFinal
                        ?
                        '<span class="badge bg-danger">پایانی</span>'
                        :
                        '-'
                    }

    </td>

    <td>

        ${item.needSelectReceiver
                        ?
                        '<span class="badge bg-warning text-dark">بله</span>'
                        :
                        '<span class="badge bg-info">خیر</span>'
                    }

    </td>

    <td>

        <button class="btn btn-warning btn-sm btnEditStep"
                data-id="${item.id}">
            <i class="fa fa-edit"></i>
        </button>

        <button class="btn btn-danger btn-sm btnDeleteStep"
                data-id="${item.id}">
            <i class="fa fa-trash"></i>
        </button>

        <span class="delete-confirm d-none">

        حذف شود؟

        <button class="btn btn-danger btn-sm btnDeleteYes"
                data-id="${item.id}">
            بله
        </button>

        <button class="btn btn-secondary btn-sm btnDeleteNo">
            خیر
        </button>

        </span>

        <button class="btn btn-light btn-sm btnMoveUp"
                data-id="${item.id}">
            <i class="fa fa-arrow-up"></i>
        </button>

        <button class="btn btn-light btn-sm btnMoveDown"
                data-id="${item.id}">
            <i class="fa fa-arrow-down"></i>
        </button>

    </td>

</tr>

`;

            });



            $("#stepList")
                .html(html);


        }
        ,
        error: function (xhr) {
        }

    });


}


$(document).on("click", ".btnDeleteYes", function () {

    let id = $(this).data("id");

    $.ajax({

        url: "/WorkflowStep/DeleteStep",

        type: "POST",

        data:
        {
            id: id
        },

        success: function (res) {
            debugger;

            if (res.success) {

                toastr.success("مرحله حذف شد.");

                loadSteps(res.data);

            }
            else {
                toastr.error("مرحله دارای نمونه استفاده شده میباشد");
                loadSteps(id);
            }

        },

        error: function () {

            toastr.error("خطا در حذف مرحله");

        }

    });

});


$(document).on("click",
    ".btnEditStep",
    function () {

        let id = $(this).data("id");

        editStep(id);

    });


function editStep(id) {

    $.ajax({

        url: "/WorkflowStep/GetStepForEdit",

        type: "GET",

        data:
        {
            id: id
        },

        success: function (res) {

            if (!res.success) {
                toastr.error(res.message);
                return;
            }
            debugger;

            let item = res.data;

            $("#WorkflowStepId").val(item.id);

            $("#StepWorkflowId").val(item.workflowId);

            $("#StepTitle").val(item.title);

            $("#StepAllowReturn")
                .prop("checked", item.allowReturn);

            $("#StepIsFinal")
                .prop("checked", item.isFinal);

            $("#StepNeedSelectReceiver")
                .prop("checked", item.needSelectReceiver);

            $("#StepRoles")
                .val(item.roleIds);

            $("#StepRoles").trigger("change");

            $("#StepUsers")
                .val(item.userIds);
            $("#StepUsers").trigger("change");

            $("#stepForm").slideDown();


        }

    });

}

function showWorkflowModal(id) {

}

function deleteWorkflow(id) {

    event.stopPropagation();


    if (!confirm("آیا از حذف این گردش مطمئن هستید؟"))
        return;



    $.ajax({

        url: "/Workflow/Delete",

        type: "POST",

        data: {
            id: id
        },


        success: function (res) {

            if (res.success) {

                toastr.success(
                    "گردش حذف شد");


                clearWorkflowForm();


                loadWorkflows();


                $("#stepList").empty();


                $("#btnAddStep")
                    .prop("disabled", true);

            }
            else {

                toastr.error(res.message);

            }

        },


        error: function () {
            toastr.error(
                "خطا در حذف گردش");
        }


    });


}


function loadRoles() {

    $.get("/WorkflowStep/GetRoles",
        function (data) {

            let html = "";


            $.each(data, function (i, item) {

                html += `

        <option value="${item.id}">
            ${item.name}
        </option>

        `;

            });


            $("#StepRoles")
                .html(html);


        });


}


function loadUsers() {

    $.get("/WorkflowStep/GetUsers",
        function (data) {

            let html = "";


            $.each(data, function (i, item) {

                html += `

        <option value="${item.id}">
            ${item.fullName} - (${item.companyName})
        </option>

        `;

            });


            $("#StepUsers")
                .html(html);


        });


}

function showStepModal(id) {

}

function saveStep() {

}

function deleteStep(id) {

}

function moveStep(id, direction) {

}

function showReceiverModal(id) {

}

function saveReceivers() {

}