using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.WorkFlow.Dto;
using RMS.Interfaces.Workflow;
using RMS.Models.Common;
using RMS.Models.Entity;
using RMS.ViewModels.Workflow;
using RMS.ViewModels.WorkflowStep;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.WorkFlow;

public class WorkflowStepController(ApplicationDbContext context, IWorkflowStepQueryService stepQuery, IWorkflowStepCommandService WorkflowStepCommand) : Controller
{
    private readonly IWorkflowStepQueryService _stepQuery = stepQuery;
    private readonly IWorkflowStepCommandService _WorkflowStepCommand = WorkflowStepCommand;

    private readonly ApplicationDbContext _context = context;

    public async Task<IActionResult> Index(Guid workflowId)
    {
        var workflow = await _context.Workflows
            .Include(x => x.WorkflowSteps)
            .FirstAsync(x => x.ID == workflowId);

        return View(workflow);
    }

    //[HttpPost]
    //public async Task<IActionResult> GetSchimaStepsForCurrentBaravordUser(Guid entityId)
    //{



    //    ProccesType proccessType = ProccesType.BarAvordAvalieh;

    //    clsBarAvordProjectionWorkFlow? currentBarAvordProjectionWorkFlow = await _context.BarAvordProjectionWorkFlows.FirstOrDefaultAsync(x => x.BarAvordUserId == entityId);
    //    if (currentBarAvordProjectionWorkFlow != null)
    //    {
    //        proccessType = currentBarAvordProjectionWorkFlow.proccesType;
    //    }
    //    clsWorkflow workflow = await _context.Workflows.FirstAsync(x => x.proccesType == proccessType);

    //    clsWorkflowInstance? workflowInstance = await _context.WorkflowInstances
    //        .FirstOrDefaultAsync(x => x.EntityId == entityId && x.WorkflowId == workflow.ID);

    //    if (workflowInstance != null)
    //    {


    //        // تاریخچه انجام شده
    //        var completedSteps = await (
    //            from wi in _context.WorkflowInstances

    //            join bu in _context.BaravordUsers
    //                on wi.EntityId equals bu.ID

    //            join wh in _context.WorkflowHistories
    //                on wi.ID equals wh.WorkflowInstanceId

    //            join ws in _context.WorkflowSteps
    //                on wh.WorkflowStepId equals ws.ID

    //            join user in _context.Users
    //                on wh.UserId equals user.Id

    //            join ud in _context.UserDetails
    //                on user.Id equals ud.UserId into userDetails
    //            from ud in userDetails.DefaultIfEmpty()

    //            join company in _context.Companies
    //                on ud.CompanyId equals company.ID into companies
    //            from company in companies.DefaultIfEmpty()

    //            where wi.EntityId == entityId && wi.WorkflowId == workflow.ID

    //            select new WorkflowHistoryDto
    //            {
    //                WorkflowInstanceId = wi.ID,
    //                WorkflowId = wi.WorkflowId,
    //                EntityType = wi.EntityType,
    //                EntityId = wi.EntityId,
    //                CurrentStepId = wi.CurrentStepId,
    //                Status = wi.Status,

    //                ProjectName = bu.Name,

    //                WorkflowStepId = ws.ID,
    //                StepTitle = ws.Title,

    //                FullName = user.FullName,

    //                CompanyName = company != null
    //                    ? company.CompanyName
    //                    : "",

    //                Action = wh.Action,
    //                ActionDate = wh.ActionDate,
    //                Comment = wh.Comment,

    //                UserId = wh.UserId,

    //                RoundNo = wh.RoundNo,

    //                IsCompleted = true
    //            })
    //            .ToListAsync();


    //        completedSteps.ForEach(x =>
    //        {
    //            x.ActionDateShamsi = x.ActionDate.Value.ToPersianDateTime();
    //        });

    //        // گرفتن مراحل باقی مانده
    //        var completedIds = completedSteps
    //            .Select(x => x.WorkflowStepId)
    //            .ToList();


    //        var assignment = await _context.WorkflowAssignments
    //         .FirstOrDefaultAsync(x =>
    //             !x.IsCompleted &&
    //             x.WorkflowInstanceId == workflowInstance.ID);

    //        var pendingSteps = await
    //        (
    //            from ws in _context.WorkflowSteps

    //            join wsu in _context.WorkflowStepUsers
    //                on ws.ID equals wsu.WorkflowStepId into stepUsers
    //            from wsu in stepUsers.DefaultIfEmpty()

    //            join user in _context.Users
    //                on wsu.UserId equals user.Id into users
    //            from user in users.DefaultIfEmpty()

    //            join ud in _context.UserDetails
    //                on user.Id equals ud.UserId into userDetails
    //            from ud in userDetails.DefaultIfEmpty()

    //            join company in _context.Companies
    //                on ud.CompanyId equals company.ID into companies
    //            from company in companies.DefaultIfEmpty()

    //            where ws.WorkflowId == workflowInstance.WorkflowId
    //                  && !completedIds.Contains(ws.ID)
    //                  && (ws.IsDeleted == false || ws.IsDeleted == null)

    //            select new
    //            {
    //                Step = ws,
    //                User = user,
    //                Company = company
    //            }
    //        ).ToListAsync();

    //        var assignedUser = assignment == null
    //            ? null
    //            : await _context.Users
    //        .Include(x => x.UserDetail)
    //        .ThenInclude(x => x.Company)
    //        .FirstOrDefaultAsync(x => x.Id == assignment.UserId);

    //        var result1 = pendingSteps
    //            .GroupBy(x => x.Step.ID)
    //            .Select(g =>
    //            {
    //                var step = g.First().Step;

    //                var users = assignment != null && assignment.WorkflowStepId == step.ID
    //                    ? g.Where(x => x.User != null && x.User.Id == assignment.UserId)
    //                    : g.Where(x => x.User != null);

    //                return new WorkflowHistoryDto
    //                {
    //                    WorkflowInstanceId = workflowInstance.ID,
    //                    WorkflowId = workflowInstance.WorkflowId,
    //                    EntityType = workflowInstance.EntityType,
    //                    EntityId = workflowInstance.EntityId,
    //                    CurrentStepId = workflowInstance.CurrentStepId,
    //                    Status = workflowInstance.Status,

    //                    ProjectName = "",

    //                    WorkflowStepId = step.ID,
    //                    StepTitle = step.Title,

    //                    FullName = assignment != null &&
    //                        assignment.WorkflowStepId == step.ID
    //                        ? assignedUser?.FullName ?? ""
    //                        : string.Join(" ، ",
    //                        users.Select(x => x.User.FullName).Distinct()),

    //                    CompanyName = assignment != null &&
    //                        assignment.WorkflowStepId == step.ID
    //                        ? assignedUser?.UserDetail?.Company?.CompanyName ?? ""
    //                        : string.Join(" ، ",
    //                        users.Where(x => x.Company != null)
    //                        .Select(x => x.Company.CompanyName)
    //                        .Distinct()),

    //                    UserId = assignment != null &&
    //                             assignment.WorkflowStepId == step.ID
    //                             ? assignment.UserId
    //                             : null,

    //                    Action = null,
    //                    ActionDate = null,
    //                    Comment = null,

    //                    RoundNo = step.Order,
    //                    IsCompleted = false
    //                };
    //            })
    //            .ToList();


    //        // خروجی نهایی Timeline
    //        var result = completedSteps
    //            .Union(result1)
    //            .OrderBy(x => x.RoundNo)
    //            .ToList();

    //        return new JsonResult(result);
    //    }
    //    else
    //        return new JsonResult(null);

    //}



    [HttpPost]
    public async Task<IActionResult> GetSchimaStepsForCurrentBaravordUser(Guid entityId)
    {
        // 1. تعیین نوع فرآیند
        ProccesType proccessType = ProccesType.BarAvordAvalieh;

        var projection = await _context.BarAvordProjectionWorkFlows
            .FirstOrDefaultAsync(x => x.BarAvordUserId == entityId);

        if (projection != null)
            proccessType = projection.proccesType;


        // 2. دریافت گردش کار
        var workflow = await _context.Workflows
            .FirstOrDefaultAsync(x =>
                x.proccesType == proccessType &&
                x.IsActive &&
                (x.IsDeleted == false || x.IsDeleted == null));

        if (workflow == null)
            return new JsonResult(null);


        // 3. دریافت نمونه اجرایی گردش کار
        var instance = await _context.WorkflowInstances
            .FirstOrDefaultAsync(x =>
                x.EntityId == entityId &&
                x.WorkflowId == workflow.ID &&
                (x.IsDeleted == false || x.IsDeleted == null));

        if (instance == null)
            return new JsonResult(null);


        // 4. دریافت نام پروژه
        var projectName = await _context.BaravordUsers
            .Where(x => x.ID == entityId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync();


        // 5. محاسبه شماره دور جاری از اطلاعات موجود

        var historyRound = await _context.WorkflowHistories
            .Where(x =>
                x.WorkflowInstanceId == instance.ID &&
                (x.IsDeleted == false || x.IsDeleted == null))
            .Select(x => (int?)x.RoundNo)
            .MaxAsync() ?? 0;

        var assignmentRound = await _context.WorkflowAssignments
            .Where(x =>
                x.WorkflowInstanceId == instance.ID &&
                (x.IsDeleted == false || x.IsDeleted == null))
            .Select(x => (int?)x.RoundNo)
            .MaxAsync() ?? 0;

        int currentRoundNo = Math.Max(
            1,
            Math.Max(historyRound, assignmentRound)
        );


        // 6. دریافت تمام مراحل گردش کار
        var steps = await _context.WorkflowSteps
            .Where(x =>
                x.WorkflowId == instance.WorkflowId &&
                (x.IsDeleted == false || x.IsDeleted == null))
            .OrderBy(x => x.Order)
            .ToListAsync();


        // 7. دریافت تمام ارجاعات واقعی به کاربران
        var assignments = await _context.WorkflowAssignments
            .Include(x => x.User)
                .ThenInclude(x => x.UserDetail)
                    .ThenInclude(x => x.Company)
            .Where(x =>
                x.WorkflowInstanceId == instance.ID &&
                (x.IsDeleted == false || x.IsDeleted == null))
            .ToListAsync();


        // 8. دریافت تمام تاریخچه اقدامات
        var histories = await _context.WorkflowHistories
            .Include(x => x.User)
                .ThenInclude(x => x.UserDetail)
                    .ThenInclude(x => x.Company)
            .Where(x =>
                x.WorkflowInstanceId == instance.ID &&
                (x.IsDeleted == false || x.IsDeleted == null))
            .OrderBy(x => x.ActionDate)
            .ToListAsync();


        // 9. تشکیل لیست ترکیبی مراحل و دورهای اجرا

        var roundKeys = new HashSet<(Guid StepId, int RoundNo)>();

        // مراحل دارای ارجاع
        foreach (var item in assignments)
        {
            roundKeys.Add((
                item.WorkflowStepId,
                item.RoundNo
            ));
        }

        // مراحل دارای تاریخچه
        foreach (var item in histories)
        {
            roundKeys.Add((
                item.WorkflowStepId,
                item.RoundNo
            ));
        }

        // مرحله جاری
        roundKeys.Add((
            instance.CurrentStepId,
            currentRoundNo
        ));

        // مراحل آینده یا مراحل بدون سابقه
        foreach (var step in steps)
        {
            if (!roundKeys.Any(x => x.StepId == step.ID))
            {
                roundKeys.Add((
                    step.ID,
                    0
                ));
            }
        }


        // 10. ساخت خروجی اصلی
        var result = new WorkflowTimelineDto
        {
            WorkflowInstanceId = instance.ID,
            WorkflowId = instance.WorkflowId,
            EntityType = instance.EntityType,
            EntityId = instance.EntityId,
            CurrentStepId = instance.CurrentStepId,
            Status = instance.Status,
            ProjectName = projectName ?? "",
            Steps = new List<WorkflowStepTimelineDto>()
        };


        // 11. ساخت اطلاعات هر مرحله در هر دور اجرا

        foreach (var key in roundKeys
            .OrderBy(x =>
                steps.FirstOrDefault(s => s.ID == x.StepId)?.Order ?? 0)
            .ThenBy(x => x.RoundNo))
        {
            var step = steps.FirstOrDefault(x => x.ID == key.StepId);

            if (step == null)
                continue;


            // ارجاعات این مرحله در این دور
            var stepAssignments = assignments
                .Where(x =>
                    x.WorkflowStepId == step.ID &&
                    x.RoundNo == key.RoundNo)
                .ToList();


            // تاریخچه این مرحله در این دور
            var stepHistories = histories
                .Where(x =>
                    x.WorkflowStepId == step.ID &&
                    x.RoundNo == key.RoundNo)
                .ToList();


            // 12. تعیین وضعیت مرحله

            string statusTitle;

            if (stepAssignments.Count == 0 && stepHistories.Count == 0)
            {
                statusTitle = "شروع نشده";
            }
            else if (stepAssignments.Any(x => !x.IsCompleted))
            {
                statusTitle = "در انتظار اقدام";
            }
            else
            {
                statusTitle = "اقدامات تکمیل شده";
            }


            // 13. ساخت DTO مرحله
            var stepDto = new WorkflowStepTimelineDto
            {
                WorkflowStepId = step.ID,

                StepTitle = step.Title,

                Order = step.Order,

                RoundNo = key.RoundNo == 0
                    ? null
                    : key.RoundNo,

                IsCurrent =
                    step.ID == instance.CurrentStepId &&
                    key.RoundNo == currentRoundNo,

                IsFinal = step.IsFinal,

                StatusTitle = statusTitle,


                // 14. گیرندگان مرحله
                Assignments = stepAssignments
                    .Select(x => new WorkflowAssignmentTimelineDto
                    {
                        AssignmentId = x.ID,

                        UserId = x.UserId,

                        FullName = x.User?.FullName ?? "",

                        CompanyName =
                            x.User?.UserDetail?.Company?.CompanyName ?? "",

                        IsCompleted = x.IsCompleted,

                        Result = x.Result,

                        ResultTitle = x.IsCompleted
                            ? x.Result?.ToString() ?? "تکمیل شده"
                            : "در انتظار اقدام",

                        Comment = x.Comment ?? "",

                        AssignDate = x.AssignDate,

                        CompletedDate = x.CompletedDate
                    })
                    .ToList(),


                // 15. تاریخچه اقدامات کاربران
                Histories = stepHistories
                    .Select(x => new WorkflowHistoryTimelineDto
                    {
                        Id = x.ID,

                        UserId = x.UserId,

                        FullName = x.User?.FullName ?? "",

                        CompanyName =
                            x.User?.UserDetail?.Company?.CompanyName ?? "",

                        Action = x.Action,

                        ActionTitle = x.Action.ToString(),

                        Comment = x.Comment ?? "",

                        ActionDate = x.ActionDate,

                        ActionDateShamsi =
                            x.ActionDate.ToPersianDateTime()
                    })
                    .ToList()
            };


            result.Steps.Add(stepDto);
        }


        // 16. مرتب سازی نهایی
        result.Steps = result.Steps
            .OrderBy(x => x.Order)
            .ThenBy(x => x.RoundNo ?? 0)
            .ToList();


        return new JsonResult(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetStep(Guid Id)
    {
        var result = await _stepQuery.GetAsync(Id);

        if (result == null)
            return Json(new
            {
                success = false,
                message = "مرحله یافت نشد."
            });

        return Json(new
        {
            success = true,
            data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetStepForEdit(Guid Id)
    {
        var result = await _stepQuery.GetForEditAsync(Id);

        if (result == null)
            return Json(new
            {
                success = false,
                message = "مرحله یافت نشد."
            });

        return Json(new
        {
            success = true,
            data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _context.Roles
            .Select(x => new
            {
                id = x.Id,
                name = x.Name
            })
            .ToListAsync();


        return Json(roles);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {

        var users = await _context.Users
            .Select(
            x => new
            {
                id = x.Id,
                name = x.UserName,
                fullName = x.UserDetail.FirstName + " " + x.UserDetail.LastName,
                CompanyName = x.UserDetail == null
             ? ""
             : x.UserDetail.Company == null
                 ? ""
                 : x.UserDetail.Company.CompanyName
            })
            .ToListAsync();

        return Json(users);

    }


    [HttpPost]
    public async Task<IActionResult> SaveStep([FromBody] WorkflowStepVm model)
    {
        try
        {
            var id = await _WorkflowStepCommand.SaveAsync(model);

            return Json(new
            {
                success = true,
                id
            });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteStep(Guid id)
    {
        //try
        //{
        var currentWFS = await _context.WorkflowSteps.FirstAsync(x => x.ID == id);
        bool result = await _WorkflowStepCommand.DeleteAsync(id);

        if (result)
        {
            return Json(new
            {
                success = true,
                data = currentWFS.WorkflowId
            });
        }
        else
        {
            return Json(new
            {
                success = false,
                data = "مرحله دارای نمونه هستش"
            });
        }
        //}
        //catch (Exception ex)
        //{
        //    return Json(new
        //    {
        //        success = false,
        //        message = ex.Message
        //    });
        //}
    }

}
