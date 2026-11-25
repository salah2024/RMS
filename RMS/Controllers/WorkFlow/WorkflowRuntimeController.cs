using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.WorkFlow.Dto;
using RMS.Interfaces.Workflow;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.WorkFlow;

public class WorkflowRuntimeController : Controller
{
    private readonly IWorkflowRuntimeService _runtime;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IWorkflowRuntimeService _workflowRuntimeService;

    public WorkflowRuntimeController(
        IWorkflowRuntimeService runtime,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        IWorkflowRuntimeService workflowRuntimeService)
    {
        _runtime = runtime;
        _userManager = userManager;
        _context = context;
        _workflowRuntimeService = workflowRuntimeService;
    }

    [HttpPost]
    public async Task<IActionResult> StartWorkFlow(Guid EntityId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var workflow = await _context.Workflows
            .Where(x =>
                x.proccesType == ProccesType.BarAvordAvalieh &&
                x.IsActive)
            .OrderByDescending(x => x.IsDefault)
            .ThenByDescending(x => x.InsertDateTime)
            .FirstOrDefaultAsync();

        if (workflow == null)
            return Json(new { success = false, message = "گردش فعال برآورد اولیه تعریف نشده است." });

        try
        {
            var instanceId = await _runtime.StartWorkflowAsync(
                workflow.ID,
                EntityId,
                nameof(clsBaravordUser),
                Guid.Parse(user.Id));

            return Json(new
            {
                success = true,
                workflowInstanceId = instanceId,
                message = "گردش کار آغاز شد."
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    private async Task<clsBarAvordProjectionWorkFlow?> GetCurrentProjectionAsync(Guid entityId)
    {
        var projections = await _context.BarAvordProjectionWorkFlows
            .Where(x => x.BarAvordUserId == entityId && !x.IsComplete)
            .OrderByDescending(x => x.InsertDateTime)
            .ToListAsync();

        if (projections.Count > 1)
            throw new InvalidOperationException("برای این برآورد بیش از یک گردش فعال ثبت شده است.");

        return projections.SingleOrDefault();
    }

    private Task<clsWorkflowInstance?> GetCurrentInstanceAsync(
        clsBarAvordProjectionWorkFlow projection)
    {
        return _context.WorkflowInstances
            .Include(x => x.Workflow)
            .Include(x => x.CurrentStep)
                .ThenInclude(x => x.WorkflowAction)
            .Where(x =>
                x.EntityId == projection.BarAvordUserId &&
                x.Workflow.proccesType == projection.proccesType &&
                x.Status == WorkflowInstanceStatus.Running)
            .OrderByDescending(x => x.InsertDateTime)
            .FirstOrDefaultAsync();
    }

    [HttpGet]
    public async Task<IActionResult> Open(Guid entityId)
    {
        var projection = await GetCurrentProjectionAsync(entityId);
        if (projection == null)
            return NotFound("گردش جاری برای این برآورد ثبت نشده است.");

        var instance = await GetCurrentInstanceAsync(projection);
        if (instance == null)
            return NotFound("اجرای فعالی برای گردش جاری پیدا نشد.");

        if (instance.CurrentStep?.WorkflowAction == null)
            return BadRequest("برای این مرحله فرم عملیاتی تعریف نشده است.");

        var baravord = await _context.BaravordUsers
            .FirstOrDefaultAsync(x => x.ID == entityId);

        if (baravord == null)
            return NotFound();

        TempData["BAId"] = entityId;
        TempData["BAType"] = baravord.Type;
        TempData["Year"] = baravord.Year;

        return RedirectToAction(
            instance.CurrentStep.WorkflowAction.Action,
            instance.CurrentStep.WorkflowAction.Controller,
            new { entityId });
    }

    [HttpPost]
    public async Task<IActionResult> Submit(
        Guid entityId,
        List<string>? receiversUserId,
        string? comment,
        int proccesType)
    {
        var projection = await GetCurrentProjectionAsync(entityId);
        if (projection == null)
            return Json(new { success = false, message = "گردش جاری برآورد یافت نشد." });

        if (projection.proccesType != (ProccesType)proccesType)
            return Json(new { success = false, message = "نوع گردش ارسالی با گردش جاری برآورد مطابقت ندارد." });

        var instance = await GetCurrentInstanceAsync(projection);
        if (instance == null)
            return Json(new { success = false, message = "اجرای فعالی برای گردش جاری یافت نشد." });

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        await _runtime.SubmitAsync(
            instance.ID,
            user.Id,
            comment,
            receiversUserId);

        return Json(new
        {
            success = true,
            message = "ارسال شد."
        });
    }

    private async Task<List<WorkflowStepUsersForCheckDto>> GetStepReceiverUsersAsync(Guid stepId)
    {
        var step = await _context.WorkflowSteps
            .Include(x => x.Users)
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.ID == stepId);

        if (step == null)
            return new List<WorkflowStepUsersForCheckDto>();

        // گیرندگان مستقیم و اعضای نقش‌های مرحله، هر دو مجاز هستند.
        var userIds = step.Users
            .Select(x => x.UserId)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        var roleIds = step.Roles
            .Select(x => x.RoleId)
            .Distinct()
            .ToList();

        if (roleIds.Count > 0)
        {
            var roleUserIds = await _context.UserRoles
                .Where(x => roleIds.Contains(x.RoleId))
                .Select(x => x.UserId)
                .ToListAsync();

            userIds.AddRange(roleUserIds);
        }

        userIds = userIds
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToList();

        if (userIds.Count == 0)
            return new List<WorkflowStepUsersForCheckDto>();

        var users = await _context.Users
            .Where(x => userIds.Contains(x.Id))
            .Select(x => new WorkflowStepUsersForCheckDto
            {
                Id = x.Id,
                FullName = x.FullName
            })
            .ToListAsync();

        var details = await _context.UserDetails
            .Where(x => userIds.Contains(x.UserId))
            .Include(x => x.Company)
            .ToListAsync();

        foreach (var user in users)
        {
            var detail = details.FirstOrDefault(x => x.UserId == user.Id);
            if (detail?.Company != null)
                user.FullName += "(" + detail.Company.CompanyName + ")";
        }

        return users;
    }

    [HttpPost]
    public async Task<IActionResult> CheckCurrentStepForNeedUsers(Guid entityId, int proccesType)
    {
        if (proccesType == (int)ProccesType.PishnahadGhaimat)
        {
            var hasActiveInitialProjection = await _context.BarAvordProjectionWorkFlows
                .AnyAsync(p =>
                    p.BarAvordUserId == entityId &&
                    !p.IsComplete &&
                    p.proccesType == ProccesType.BarAvordAvalieh);

            var lastInitialWorkflowStatus = await _context.WorkflowInstances
                .Where(w =>
                    w.EntityId == entityId &&
                    w.Workflow.proccesType == ProccesType.BarAvordAvalieh)
                .OrderByDescending(w => w.InsertDateTime)
                .Select(w => w.Status)
                .FirstOrDefaultAsync();

            if (!hasActiveInitialProjection ||
                lastInitialWorkflowStatus != WorkflowInstanceStatus.Completed)
            {
                return Json(new
                {
                    success = false,
                    needSelectReceiver = false,
                    message = "ابتدا باید گردش برآورد اولیه به‌طور کامل تایید و خاتمه یابد."
                });
            }
        }

        var workflow = await _context.Workflows
            .Where(x =>
                x.proccesType == (ProccesType)proccesType &&
                x.IsActive)
            .OrderByDescending(x => x.IsDefault)
            .ThenByDescending(x => x.InsertDateTime)
            .FirstOrDefaultAsync();

        if (workflow == null)
            return Json(new
            {
                success = false,
                needSelectReceiver = false,
                message = "گردش کار فعالی برای این عملیات تعریف نشده است."
            });

        var firstStep = await _context.WorkflowSteps
            .Where(x => x.WorkflowId == workflow.ID)
            .OrderBy(x => x.Order)
            .FirstOrDefaultAsync();

        if (firstStep == null)
            return Json(new
            {
                success = false,
                needSelectReceiver = false,
                message = "مرحله اول گردش کار تعریف نشده است."
            });

        if (!firstStep.NeedSelectReceiver)
            return Json(new { success = true, needSelectReceiver = false });

        var users = await GetStepReceiverUsersAsync(firstStep.ID);

        if (users.Count == 0)
            return Json(new
            {
                success = false,
                needSelectReceiver = false,
                message = "برای مرحله اول گردش پیشنهاد قیمت، پیمانکار قابل انتخابی تعریف نشده است."
            });

        return Json(new
        {
            success = true,
            needSelectReceiver = true,
            users
        });
    }

    [HttpPost]
    public async Task<IActionResult> CheckNextStepForNeedUsers(Guid entityId, int proccesType)
    {
        var projection = await GetCurrentProjectionAsync(entityId);
        if (projection == null || projection.proccesType != (ProccesType)proccesType)
            return Json(new { success = false, needSelectReceiver = false });

        var instance = await GetCurrentInstanceAsync(projection);
        if (instance == null)
            return Json(new { success = false, needSelectReceiver = false });

        var currentStep = instance.CurrentStep;
        if (currentStep == null || currentStep.NextStepId == null)
            return Json(new { success = true, needSelectReceiver = false });

        var nextStep = await _context.WorkflowSteps
            .FirstOrDefaultAsync(x => x.ID == currentStep.NextStepId);

        if (nextStep == null || !nextStep.NeedSelectReceiver)
            return Json(new { success = true, needSelectReceiver = false });

        var users = await GetStepReceiverUsersAsync(nextStep.ID);

        return Json(new
        {
            success = true,
            needSelectReceiver = true,
            users = users.Select(x => new { id = x.Id, fullName = x.FullName }).ToList()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Approve(Guid EntityId, string? comment)
    {
        var projection = await GetCurrentProjectionAsync(EntityId);
        if (projection == null)
            return Json(new { success = false, message = "گردش جاری یافت نشد." });

        var instance = await GetCurrentInstanceAsync(projection);
        if (instance == null)
            return Json(new { success = false, message = "اجرای فعالی برای گردش جاری یافت نشد." });

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        await _runtime.ApproveAsync(instance.ID, user.Id, comment);

        return Json(new
        {
            success = true,
            message = "مرحله تایید شد."
        });
    }



    [HttpPost]
    public async Task<IActionResult> Revision(Guid EntityId, string? comment)
    {
        var projection = await GetCurrentProjectionAsync(EntityId);
        if (projection == null)
            return Json(new { success = false, message = "گردش جاری یافت نشد." });

        var instance = await GetCurrentInstanceAsync(projection);
        if (instance == null)
            return Json(new { success = false, message = "اجرای فعالی برای گردش جاری یافت نشد." });

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        await _runtime.RevisionAsync(instance.ID, user.Id, comment);

        return Json(new
        {
            success = true,
            message = "برای اصلاح بازگردانده شد."
        });
    }



    [HttpPost]
    public async Task<IActionResult> Reject(Guid EntityId, string? comment)
    {
        var projection = await GetCurrentProjectionAsync(EntityId);
        if (projection == null)
            return Json(new { success = false, message = "گردش جاری یافت نشد." });

        var instance = await GetCurrentInstanceAsync(projection);
        if (instance == null)
            return Json(new { success = false, message = "اجرای فعالی برای گردش جاری یافت نشد." });

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        await _runtime.RejectAsync(instance.ID, user.Id, comment);

        return Json(new
        {
            success = true,
            message = "درخواست رد شد."
        });
    }



    [HttpPost]
    public async Task<IActionResult> Cancel(
        Guid workflowInstanceId,
        string? comment)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        await _runtime.CancelAsync(
            workflowInstanceId,
            user.Id,
            comment);

        return Json(new
        {
            success = true,
            message = "فرآیند لغو شد."
        });
    }
}