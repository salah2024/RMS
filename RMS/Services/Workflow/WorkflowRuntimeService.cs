using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMS.Interfaces.Workflow;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Services.Workflow;

public class WorkflowRuntimeService
    : IWorkflowRuntimeService
{
    private readonly ApplicationDbContext _context;

    private readonly UserManager<ApplicationUser> _userManager;

    public WorkflowRuntimeService(
        ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SubmitAsync(
    Guid workflowInstanceId,
    string currentUserId,
    string? comment = null,
    List<string>? receiversUserId = null)
    {
        var instance = await _context.WorkflowInstances
            .FirstOrDefaultAsync(x => x.ID == workflowInstanceId);

        if (instance == null)
            throw new Exception("گردش کار یافت نشد.");

        var currentStep = await _context.WorkflowSteps
            .FirstAsync(x => x.ID == instance.CurrentStepId);

        // مرحله پایانی
        if (currentStep.NextStepId == null)
        {
            await ExecuteActionAsync(
                workflowInstanceId,
                currentUserId,
                WorkflowAction.Approve,
                comment);

            return;
        }

        // مرحله بعد
        var nextStep = await _context.WorkflowSteps
            .FirstAsync(x => x.ID == currentStep.NextStepId);

        List<string>? selectedUsers = null;

        if (nextStep.NeedSelectReceiver)
        {
            if (receiversUserId == null || receiversUserId.Count == 0)
                throw new Exception("حداقل یک گیرنده انتخاب کنید.");

            selectedUsers = receiversUserId;
        }


        await ExecuteActionAsync(
        workflowInstanceId,
        currentUserId,
        WorkflowAction.Submit,
        comment,
        selectedUsers);
    }

    public async Task ApproveAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null)
    {
        await ExecuteActionAsync(
            workflowInstanceId,
            currentUserId,
            WorkflowAction.Approve,
            comment);
    }

    public async Task RevisionAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null)
    {
        await ExecuteActionAsync(
            workflowInstanceId,
            currentUserId,
            WorkflowAction.Revision,
            comment);
    }

    public async Task RejectAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null)
    {
        await ExecuteActionAsync(
            workflowInstanceId,
            currentUserId,
            WorkflowAction.Reject,
            comment);
    }

    public async Task CancelAsync(
        Guid workflowInstanceId,
        string currentUserId,
        string? comment = null)
    {
        await ExecuteActionAsync(
            workflowInstanceId,
            currentUserId,
            WorkflowAction.Cancel,
            comment);
    }

    private async Task SetUserAccess(
    Guid entityId,
    string userId,
    ProjectAccessType accessType)
    {
        var access = await _context.BaravordUserAccesses
            .FirstOrDefaultAsync(x =>
                x.BaravordUserId == entityId &&
                x.UserId == userId &&
                (x.IsDeleted == false || x.IsDeleted == null));

        if (access == null)
        {
            _context.BaravordUserAccesses.Add(new clsBaravordUserAccess
            {
                ID = Guid.NewGuid(),
                BaravordUserId = entityId,
                UserId = userId,
                AccessType = accessType,
                InsertDateTime = DateTime.Now
            });
        }
        else
        {
            access.AccessType = accessType;
        }
    }


    private async Task GoNextStepAsync(
    clsWorkflowInstance instance,
    string senderUserId,
    List<string>? selectedUsers = null)
    {
        //---------------------------------------
        // مرحله جاری
        //---------------------------------------

        var currentStep = await _context.WorkflowSteps
            .Include(x => x.Users)
            .Include(x => x.Roles)
            .FirstAsync(x => x.ID == instance.CurrentStepId);

        //---------------------------------------
        // مرحله بعد
        //---------------------------------------

        if (currentStep.NextStepId == null)
        {
            instance.Status = WorkflowInstanceStatus.Completed;

            var baravord = await _context.BaravordUsers
                .FirstOrDefaultAsync(x => x.ID == instance.EntityId);

            if (baravord != null)
            {
                //baravord.IsCompleted = true;
                baravord.Type++;
            }


            await ChangeProjectAccessToReadOnly(instance.EntityId);

            return;
        }

        var nextStep = await _context.WorkflowSteps
            .Include(x => x.Users)
            .Include(x => x.Roles)
            .FirstAsync(x => x.ID == currentStep.NextStepId);

        //---------------------------------------
        // مرحله جاری تغییر کند
        //---------------------------------------

        instance.CurrentStepId = nextStep.ID;

        //---------------------------------------
        // دسترسی های قبلی حذف شود
        //---------------------------------------

        await RemoveCurrentAccesses(instance.EntityId);

        //---------------------------------------
        // کاربران مرحله بعد
        //---------------------------------------

        var users = selectedUsers ?? await GetStepUsers(nextStep);

        //---------------------------------------
        // Assignment
        //---------------------------------------

        foreach (var userId in users.Distinct())
        {
            _context.WorkflowAssignments.Add(
                new clsWorkflowAssignment
                {
                    ID = Guid.NewGuid(),

                    WorkflowInstanceId = instance.ID,

                    WorkflowStepId = nextStep.ID,

                    UserId = userId,

                    AssignedByUserId = senderUserId,

                    AssignDate = DateTime.Now,

                    IsCompleted = false
                });

            //---------------------------------------
            // دسترسی پروژه
            //---------------------------------------

            //bool hasAccess = await _context.BaravordUserAccesses.AnyAsync(x =>

            //    x.BaravordUserId == instance.EntityId
            //    &&

            //    x.UserId == userId
            //    &&

            //    (x.IsDeleted == false || x.IsDeleted == null));

            //if (!hasAccess)
            //{
            //    _context.BaravordUserAccesses.Add(
            //        new clsBaravordUserAccess
            //        {
            //            ID = Guid.NewGuid(),

            //            BaravordUserId = instance.EntityId,

            //            UserId = userId,

            //            AccessType = ProjectAccessType.Edit,

            //            InsertDateTime = DateTime.Now
            //        });
            //}


            // ارسال کننده فقط مشاهده داشته باشد
            await SetUserAccess(
                instance.EntityId,
                senderUserId,
                ProjectAccessType.View);

            // گیرندگان مرحله بعد ویرایش داشته باشند
            foreach (var userId1 in users.Distinct())
            {
                await SetUserAccess(
                    instance.EntityId,
                    userId1,
                    ProjectAccessType.Edit);
            }
        }
    }


    private async Task ExecuteActionAsync(
    Guid workflowInstanceId,
    string currentUserId,
    WorkflowAction action,
    string? comment,
    List<string>? selectedUsers = null)
    {
        var instance = await GetInstance(workflowInstanceId);

        var assignment = await GetCurrentAssignment(
            workflowInstanceId,
            instance.CurrentStepId,
            currentUserId);

        assignment.IsCompleted = true;

        clsWorkflowInstance? workflowInstance = await _context.WorkflowInstances.FirstOrDefaultAsync(x => x.ID == workflowInstanceId);
        if (workflowInstance!=null)
        {
            clsBarAvordProjectionWorkFlow? barAvordProjectionWorkFlow= await _context.BarAvordProjectionWorkFlows.FirstOrDefaultAsync(x => x.BarAvordUserId == workflowInstance.EntityId);
            if (barAvordProjectionWorkFlow!=null)
            {
                barAvordProjectionWorkFlow.IsComplete = true;
            }
        }


        assignment.CompletedDate = DateTime.Now;

        assignment.Comment = comment;

        assignment.Result = action switch
        {
            WorkflowAction.Submit => WorkflowAssignmentResult.Submitted,

            WorkflowAction.Revision => WorkflowAssignmentResult.RevisionRequested,

            WorkflowAction.Reject => WorkflowAssignmentResult.Rejected,

            WorkflowAction.Cancel => WorkflowAssignmentResult.Cancelled,

            _ => WorkflowAssignmentResult.Submitted
        };

        await AddHistory(
            instance,
            currentUserId,
            action,
            comment);


        switch (action)
        {
            case WorkflowAction.Submit:

                await GoNextStepAsync(
                    instance,
                    currentUserId,
                    selectedUsers);

                break;

            case WorkflowAction.Revision:

                await GoPreviousStepAsync(
                    instance,
                    currentUserId);

                break;

            case WorkflowAction.Approve:

                //instance.Status = WorkflowInstanceStatus.Completed;


                //await ChangeProjectAccessToReadOnly(instance.EntityId);

                await GoNextStepAsync(
                    instance,
                    currentUserId,
                    selectedUsers);

                break;

            case WorkflowAction.Reject:

                instance.Status = WorkflowInstanceStatus.Rejected;

                break;

            case WorkflowAction.Cancel:

                instance.Status = WorkflowInstanceStatus.Cancelled;

                break;
        }


        await _context.SaveChangesAsync();
    }


    private async Task GoPreviousStepAsync(
    clsWorkflowInstance instance,
    string senderUserId)
    {
        //---------------------------------------
        // مرحله جاری
        //---------------------------------------

        var currentStep = await _context.WorkflowSteps
            .Include(x => x.Users)
            .Include(x => x.Roles)
            .FirstAsync(x => x.ID == instance.CurrentStepId);

        //---------------------------------------
        // مرحله قبل
        //---------------------------------------

        if (currentStep.PreviousStepId == null)
            throw new Exception("مرحله قبلی وجود ندارد.");

        var previousStep = await _context.WorkflowSteps
            .Include(x => x.Users)
            .Include(x => x.Roles)
            .FirstAsync(x => x.ID == currentStep.PreviousStepId);

        //---------------------------------------
        // تغییر مرحله جاری
        //---------------------------------------

        instance.CurrentStepId = previousStep.ID;

        //---------------------------------------
        // حذف دسترسی‌های فعلی
        //---------------------------------------

        await RemoveCurrentAccesses(instance.EntityId);

        //---------------------------------------
        // کاربران مرحله قبل
        //---------------------------------------

        var users = await GetStepUsers(previousStep);

        //---------------------------------------
        // ایجاد Assignment
        //---------------------------------------

        foreach (var userId in users.Distinct())
        {
            _context.WorkflowAssignments.Add(
                new clsWorkflowAssignment
                {
                    ID = Guid.NewGuid(),

                    WorkflowInstanceId = instance.ID,

                    WorkflowStepId = previousStep.ID,

                    UserId = userId,

                    AssignedByUserId = senderUserId,

                    AssignDate = DateTime.Now,

                    IsCompleted = false,

                    Result = WorkflowAssignmentResult.Pending
                });

            //---------------------------------------
            // دسترسی پروژه
            //---------------------------------------

            //bool hasAccess = await _context.BaravordUserAccesses.AnyAsync(x =>
            //    x.BaravordUserId == instance.EntityId &&
            //    x.UserId == userId &&
            //    (x.IsDeleted == false || x.IsDeleted == null));

            //if (!hasAccess)
            //{
            //    _context.BaravordUserAccesses.Add(
            //        new clsBaravordUserAccess
            //        {
            //            ID = Guid.NewGuid(),

            //            BaravordUserId = instance.EntityId,

            //            UserId = userId,

            //            AccessType = ProjectAccessType.Edit,

            //            InsertDateTime = DateTime.Now
            //        });
            //}

            await SetUserAccess(
                instance.EntityId,
                senderUserId,
                ProjectAccessType.View);

            foreach (var userId1 in users.Distinct())
            {
                await SetUserAccess(
                    instance.EntityId,
                    userId1,
                    ProjectAccessType.Edit);
            }
        }
    }

    private async Task RemoveCurrentAccesses(Guid entityId)
    {
        var accesses = await _context.BaravordUserAccesses
            .Where(x =>
                x.BaravordUserId == entityId &&
                x.AccessType == ProjectAccessType.Edit &&
                (x.IsDeleted == false || x.IsDeleted == null))
            .ToListAsync();

        //foreach (var item in accesses)
        //{
        //    item.IsDeleted = true;
        //    item.RemoveDateTime = DateTime.Now;
        //}
    }


    private async Task<clsWorkflowInstance> GetInstance(
    Guid workflowInstanceId)
    {
        var instance = await _context.WorkflowInstances

            .Include(x => x.Workflow)

            .Include(x => x.CurrentStep)
                .ThenInclude(x => x.Users)

            .Include(x => x.CurrentStep)
                .ThenInclude(x => x.Roles)

            .FirstOrDefaultAsync(x => x.ID == workflowInstanceId);

        if (instance == null)
            throw new Exception("گردش کار یافت نشد.");

        if (instance.Status != WorkflowInstanceStatus.Running)
            throw new Exception("گردش کار خاتمه یافته است.");

        return instance;
    }

    private async Task<clsWorkflowAssignment> GetCurrentAssignment(
    Guid workflowInstanceId,
    Guid stepId,
    string currentUserId)
    {
        var assignment = await _context.WorkflowAssignments

            .FirstOrDefaultAsync(x =>

                x.WorkflowInstanceId == workflowInstanceId
                &&

                x.WorkflowStepId == stepId
                &&

                x.UserId == currentUserId
                &&

                !x.IsCompleted);

        if (assignment == null)
            throw new Exception("این مرحله به شما ارجاع نشده است.");

        return assignment;
    }


    private async Task AddHistory(
    clsWorkflowInstance instance,
    string currentUserId,
    WorkflowAction action,
    string? comment)
    {
        clsWorkflowHistory? workflowHistory = _context.WorkflowHistories.Where(x => x.WorkflowInstanceId == instance.ID).OrderByDescending(x => x.ActionDate).FirstOrDefault();
        int roundNo = 1;
        if (workflowHistory != null)
        {
            roundNo = workflowHistory.RoundNo + 1;
        }

        _context.WorkflowHistories.Add(
            new clsWorkflowHistory
            {
                ID = Guid.NewGuid(),

                WorkflowInstanceId = instance.ID,

                WorkflowStepId = instance.CurrentStepId,

                UserId = currentUserId,

                Action = action,

                Comment = comment,

                ActionDate = DateTime.Now,

                RoundNo = roundNo
            });

        await Task.CompletedTask;
    }


    public async Task<Guid> StartWorkflowAsync(
        Guid workflowId,
        Guid entityId,
        string entityType,
        Guid senderUserId,
        List<string>? selectedUsers = null)
    {
        var workflow = await _context.Workflows
            .Include(x => x.WorkflowSteps).ThenInclude(x => x.Users)
            .Include(x => x.WorkflowSteps).ThenInclude(x => x.Roles)
            .FirstOrDefaultAsync(x => x.ID == workflowId);

        if (workflow == null)
            throw new Exception("گردش کار یافت نشد.");

        var firstStep = workflow.WorkflowSteps
            .OrderBy(x => x.Order)
            .First();

        var instance = new clsWorkflowInstance
        {
            ID = Guid.NewGuid(),

            WorkflowId = workflow.ID,

            EntityId = entityId,

            EntityType = entityType,

            CurrentStepId = firstStep.ID,

            Status = WorkflowInstanceStatus.Running
        };

        _context.WorkflowInstances.Add(instance);

        await AssignStepAsync(
            instance,
            firstStep,
            senderUserId.ToString(),
            selectedUsers);

        await UpdateProjectAccessAsync(
            entityId,
            firstStep);

        await _context.SaveChangesAsync();

        return instance.ID;
    }



    private async Task AssignStepAsync(
        clsWorkflowInstance instance,
        clsWorkflowStep step,
        string senderUserId,
        List<string>? selectedUsers = null)
    {
        List<string> users;

        if (step.NeedSelectReceiver)
        {
            if (selectedUsers == null || !selectedUsers.Any())
                throw new Exception("گیرنده مرحله انتخاب نشده است.");

            users = selectedUsers;
        }
        else
        {
            users = await GetStepUsers(step);
        }

        users = users.Distinct().ToList();

        foreach (var userId in users)
        {
            _context.WorkflowAssignments.Add(
                new clsWorkflowAssignment
                {
                    ID = Guid.NewGuid(),

                    WorkflowInstanceId = instance.ID,

                    WorkflowStepId = step.ID,

                    UserId = userId,

                    AssignedByUserId = senderUserId,

                    AssignDate = DateTime.Now,

                    IsCompleted = false,

                    Result = WorkflowAssignmentResult.Pending
                });
        }
    }

    private async Task<List<string>> GetStepUsers(
    clsWorkflowStep step)
    {
        List<string> users = new();

        if (step.Users.Count != 0)
        {
            users.AddRange(
                step.Users
                    .Select(x => x.UserId));
        }
        else
        {
            foreach (var role in step.Roles)
            {
                var roleUsers = await _context.UserRoles

                    .Where(x => x.RoleId == role.RoleId)

                    .Select(x => x.UserId)

                    .ToListAsync();

                users.AddRange(roleUsers);
            }
        }

        return users
            .Distinct()
            .ToList();
    }


    private async Task UpdateProjectAccessAsync(
    Guid entityId,
    clsWorkflowStep step)
    {
        var accesses = await _context.BaravordUserAccesses

            .Where(x => x.BaravordUserId == entityId)

            .ToListAsync();

        foreach (var access in accesses)
        {
            if (access.AccessType != ProjectAccessType.Owner)
                access.AccessType = ProjectAccessType.View;
        }

        var users = await GetStepUsers(step);

        foreach (var userId in users)
        {
            var access = accesses
                .FirstOrDefault(x => x.UserId == userId);

            if (access == null)
            {
                _context.BaravordUserAccesses.Add(
                    new clsBaravordUserAccess
                    {
                        ID = Guid.NewGuid(),

                        BaravordUserId = entityId,

                        UserId = userId,

                        AccessType = ProjectAccessType.Edit
                    });
            }
            else
            {
                if (access.AccessType != ProjectAccessType.Owner)
                    access.AccessType = ProjectAccessType.Edit;
            }
        }
    }



    private async Task ChangeProjectAccessToReadOnly(
    Guid entityId)
    {
        var accesses = await _context.BaravordUserAccesses

            .Where(x =>
                x.BaravordUserId == entityId)

            .ToListAsync();

        foreach (var access in accesses)
        {
            if (access.AccessType != ProjectAccessType.Owner)
                access.AccessType = ProjectAccessType.View;
        }
    }

}
