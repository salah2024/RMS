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
        string userId = _userManager.GetUserId(User)!;

        ApplicationUser? currentUser =
            await _userManager.FindByIdAsync(userId);

        if (currentUser == null)
            return new JsonResult("NOK_");

        Guid currentUserId = Guid.Parse(currentUser.Id);

        clsWorkflow? workflow =
                await _context.Workflows
                .FirstOrDefaultAsync(x =>
                    x.proccesType ==
                    ProccesType.BarAvordAvalieh);

        clsBarAvordProjectionWorkFlow? barAvordProjectionWorkFlow1 = await _context.BarAvordProjectionWorkFlows.FirstOrDefaultAsync(x => x.BarAvordUserId == EntityId);
        if (barAvordProjectionWorkFlow1 != null)
        {
            barAvordProjectionWorkFlow1.proccesType = ProccesType.BarAvordAvalieh;
        }
        else
        {
            clsBarAvordProjectionWorkFlow barAvordProjectionWorkFlow = new clsBarAvordProjectionWorkFlow
            {
                BarAvordUserId = EntityId,
                proccesType = ProccesType.BarAvordAvalieh
            };
            _context.BarAvordProjectionWorkFlows.Add(barAvordProjectionWorkFlow);
        }


        await _workflowRuntimeService.StartWorkflowAsync(
            workflow.ID,
            EntityId,
            nameof(clsBaravordUser),
            currentUserId);

        return Json(new
        {
            success = false,
            message = "مرحله یافت نشد."
        });
    }

    [HttpGet]
    public async Task<IActionResult> Open(Guid entityId)
    {
        var instance = await _context.WorkflowInstances
            .Include(x => x.CurrentStep)
                .ThenInclude(x => x.WorkflowAction)
            .FirstOrDefaultAsync(x => x.EntityId == entityId);

        if (instance == null)
            return NotFound();

        if (instance.CurrentStep?.WorkflowAction == null)
            return BadRequest("برای این مرحله فرم عملیاتی تعریف نشده است.");

        TempData["BAId"] = entityId;
        clsBaravordUser? BA = _context.BaravordUsers.FirstOrDefault(x => x.ID == entityId);
        if (BA != null)
        {
            TempData["BAType"] = BA.Type;
            TempData["Year"] = BA.Year;

            return RedirectToAction(
                instance.CurrentStep.WorkflowAction.Action,
                instance.CurrentStep.WorkflowAction.Controller,
                new { entityId });
        }
        else
            return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Submit(
        Guid entityId,
        List<string>? receiversUserId,
        string? comment
        , int proccesType)
    {

        ProccesType currentProccesType = (ProccesType)proccesType;

        clsBarAvordProjectionWorkFlow? barAvordProjectionWorkFlow = await _context.BarAvordProjectionWorkFlows.FirstOrDefaultAsync(x => x.BarAvordUserId == entityId);
        if (barAvordProjectionWorkFlow != null)
        {
            currentProccesType = (ProccesType)barAvordProjectionWorkFlow.proccesType;
        }



        //var instance = await _context.WorkflowInstances
        //.FirstOrDefaultAsync(x => x.EntityId == entityId);

        var instance = await _context.WorkflowInstances.Include(x => x.Workflow)
            .Include(x => x.CurrentStep)
            .FirstOrDefaultAsync(x => x.EntityId == entityId && x.Workflow.proccesType == currentProccesType);


        if (instance == null)
            return Json(new
            {
                success = false,
                message = "گردش یافت نشد."
            });

        var user = await _userManager.GetUserAsync(User);

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

    [HttpPost]
    public async Task<IActionResult> CheckCurrentStepForNeedUsers(Guid entityId, int proccesType)
    {
        clsWorkflowStep? WorkFlowStep = await _context.WorkflowSteps
                        .Include(x => x.Workflow).FirstOrDefaultAsync(x => x.Workflow.proccesType == (ProccesType)proccesType);

        if (WorkFlowStep != null)
        {

            var users = await
            (from su in _context.WorkflowStepUsers
             join u in _context.Users
                on su.UserId equals u.Id
             where su.WorkflowStepId == WorkFlowStep.ID
             select new WorkflowStepUsersForCheckDto
             {
                 Id = u.Id,
                 FullName = u.FullName,
             }).ToListAsync();

            List<string> lstUserIds = users.Select(x => x.Id).ToList();

            List<clsUserDetail> lstUserDetails = await _context.UserDetails.Where(x => lstUserIds.Contains(x.UserId)).Include(x => x.Company).ToListAsync();

            foreach (var user in users)
            {
                clsUserDetail? currentUserDetails = lstUserDetails.Where(x => x.UserId == user.Id).FirstOrDefault();
                if (currentUserDetails != null)
                {
                    user.FullName += "(" + currentUserDetails.Company.CompanyName + ")";
                }
            }

            return Json(new
            {
                success = true,
                needSelectReceiver = true,
                users
            });
        }
        else
            return Json(new
            {
                success = false,
                needSelectReceiver = false
            });
    }


    [HttpPost]
    public async Task<IActionResult> CheckNextStepForNeedUsers(Guid entityId, int proccesType)
    {
        ProccesType currentProccesType = (ProccesType)proccesType;

        clsWorkflowInstance? instance = await _context.WorkflowInstances.Include(x => x.Workflow)
            .Include(x => x.CurrentStep)
            .FirstOrDefaultAsync(x => x.EntityId == entityId && x.Workflow.proccesType == currentProccesType);

        if (instance == null)

            return Json(new { success = false });


        var currentStep = await _context.WorkflowSteps
            .FirstAsync(x => x.ID == instance.CurrentStepId);

        if (currentStep.NextStepId == null)
            return Json(new
            {
                success = true,
                needSelectReceiver = false
            });

        var nextStep = await _context.WorkflowSteps
            .Include(x => x.Users)
            .FirstAsync(x => x.ID == currentStep.NextStepId);

        if (!nextStep.NeedSelectReceiver)
        {
            return Json(new
            {
                success = true,
                needSelectReceiver = false
            });
        }

        var users = await
            (from su in _context.WorkflowStepUsers
             join u in _context.Users
                 on su.UserId equals u.Id
             where su.WorkflowStepId == nextStep.ID
             select new
             {
                 id = u.Id,
                 fullName = u.FullName
             }).ToListAsync();

        return Json(new
        {
            success = true,
            needSelectReceiver = true,
            users
        });
    }


    [HttpPost]
    public async Task<IActionResult> Approve(
        Guid EntityId,
        string? comment)
    {
        clsWorkflowInstance? workflowInstance = _context.WorkflowInstances.FirstOrDefault(x => x.EntityId == EntityId);

        if (workflowInstance != null)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            await _runtime.ApproveAsync(
                workflowInstance.ID,
                user.Id,
                comment);

            return Json(new
            {
                success = true,
                message = "مرحله تایید شد."
            });
        }
        else
            return Json(new
            {
                success = false,
                message = "مرحله یافت نشد."
            });
    }

    [HttpPost]
    public async Task<IActionResult> Revision(
        Guid EntityId,
        string? comment)
    {
        clsWorkflowInstance? workflowInstance = _context.WorkflowInstances.FirstOrDefault(x => x.EntityId == EntityId);

        if (workflowInstance != null)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            await _runtime.RevisionAsync(
                workflowInstance.ID,
                user.Id,
                comment);

            return Json(new
            {
                success = true,
                message = "برای اصلاح بازگردانده شد."
            });
        }
        else
            return Json(new
            {
                success = false,
                message = "مرحله یافت نشد."
            });
    }

    [HttpPost]
    public async Task<IActionResult> Reject(
        Guid EntityId,
        string? comment)
    {
        clsWorkflowInstance? workflowInstance = _context.WorkflowInstances.FirstOrDefault(x => x.EntityId == EntityId);

        if (workflowInstance != null)
        {

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            await _runtime.RejectAsync(
                workflowInstance.ID,
                user.Id,
                comment);

            return Json(new
            {
                success = true,
                message = "درخواست رد شد."
            });
        }
        else
            return Json(new
            {
                success = false,
                message = "مرحله یافت نشد."
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