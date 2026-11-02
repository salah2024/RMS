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
function renderWorkflowList(data) {

    let html = "";

    if (data.length === 0) {

        html = `
            <li class="list-group-item text-center p-5">

                گردش کاری تعریف نشده است

            </li>`;

        $("#workflowList").html(html);

        return;
    }

    $.each(data, function (index, item) {

        html += `

<li class="list-group-item workflow-item"

    data-id="${item.id}">

<div class="d-flex justify-content-between align-items-center">

    <div>

        <i class="fa fa-sitemap text-primary"></i>

        ${item.name}

    </div>

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

    $("#workflowList").html(html);

    //-------------------------------------------------
    // انتخاب اولین گردش
    //-------------------------------------------------

    selectedWorkflowId = data[0].id;

    highlightSelectedWorkflow();

    loadSteps(selectedWorkflowId);

    $("#btnAddStep").prop("disabled", false);

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
            console.log(xhr.status);
            console.log(xhr.responseText);
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