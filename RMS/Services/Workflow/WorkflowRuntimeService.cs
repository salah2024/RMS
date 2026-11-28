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
        else if (access.AccessType != ProjectAccessType.Owner)
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

        // اگر انتخاب گیرنده فعال باشد فقط کاربران انتخاب‌شده (از فهرست مجاز)
        // ارجاع می‌گیرند؛ در غیر این صورت فقط کاربران صریحاً ثبت‌شده در مرحله.
        var users = await ResolveStepReceiversAsync(nextStep, selectedUsers);

        if (users.Count == 0)
            throw new Exception("برای مرحله بعد هیچ گیرنده‌ای تعریف نشده است.");

        //---------------------------------------
        // Assignment
        //---------------------------------------

        foreach (var userId in users)
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


        }

        // ارسال‌کننده فقط مشاهده داشته باشد و تمام گیرندگان ویرایش داشته باشند.
        await SetUserAccess(
            instance.EntityId,
            senderUserId,
            ProjectAccessType.View);

        foreach (var userId in users)
        {
            await SetUserAccess(
                instance.EntityId,
                userId,
                ProjectAccessType.Edit);
        }
    }


    private async Task ExecuteActionAsync(
    Guid workflowInstanceId,
    string currentUserId,
    WorkflowAction action,
    string? comment,
    List<string>? selectedUsers = null)
    {
        var ownsTransaction = _context.Database.CurrentTransaction == null;
        await using var transaction = ownsTransaction
            ? await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable)
            : null;

        try
        {
            var instance = await GetInstance(workflowInstanceId);

            // فقط گردش متناظر با Projection فعال اجازه دریافت اقدام دارد.
            if (instance.EntityType == nameof(clsBaravordUser))
            {
                var currentProjection = await _context.BarAvordProjectionWorkFlows
                    .SingleOrDefaultAsync(x =>
                        x.BarAvordUserId == instance.EntityId &&
                        !x.IsComplete);

                if (currentProjection == null ||
                    currentProjection.proccesType != instance.Workflow.proccesType)
                {
                    throw new Exception("این گردش دیگر گردش جاری برآورد نیست.");
                }
            }

            var currentStepId = instance.CurrentStepId;
            var assignment = await GetCurrentAssignment(
                workflowInstanceId,
                currentStepId,
                currentUserId);

            assignment.IsCompleted = true;
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

            // در گردش پیشنهاد قیمت، تأیید اولین پیمانکار برای اقدام‌شده شدن
            // مرحله کافی است؛ سایر ارجاع‌های همان مرحله دیگر در انتظار پاسخ نمی‌مانند.
            if (action == WorkflowAction.Approve &&
                instance.Workflow.proccesType == ProccesType.PishnahadGhaimat)
            {
                var otherPendingAssignments = await _context.WorkflowAssignments
                    .Where(x =>
                        x.WorkflowInstanceId == instance.ID &&
                        x.WorkflowStepId == currentStepId &&
                        x.ID != assignment.ID &&
                        !x.IsCompleted)
                    .ToListAsync();

                var completedAt = DateTime.Now;
                foreach (var otherAssignment in otherPendingAssignments)
                {
                    otherAssignment.IsCompleted = true;
                    otherAssignment.Result = WorkflowAssignmentResult.Cancelled;
                    otherAssignment.CompletedDate = completedAt;
                    otherAssignment.Comment = "به دلیل تأیید اولین پیمانکار، مرحله خاتمه یافت.";
                }
            }

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

            if (ownsTransaction)
                await transaction!.CommitAsync();
        }
        catch
        {
            if (ownsTransaction && transaction != null)
                await transaction.RollbackAsync();

            throw;
        }
    }


    private async Task GoPreviousStepAsync(
        clsWorkflowInstance instance,
        string senderUserId)
    {
        var currentStep = await _context.WorkflowSteps
            .FirstAsync(x => x.ID == instance.CurrentStepId);

        if (currentStep.PreviousStepId == null)
            throw new Exception("مرحله قبلی وجود ندارد.");

        var previousStep = await _context.WorkflowSteps
            .Include(x => x.Users)
            .Include(x => x.Roles)
            .FirstAsync(x => x.ID == currentStep.PreviousStepId);

        instance.CurrentStepId = previousStep.ID;

        await RemoveCurrentAccesses(instance.EntityId);

        // در زمان عودت، ابتدا همان کاربرانی را برگردان که قبلاً همین مرحله
        // را دریافت کرده بودند؛ نقش‌ها نباید باعث ارجاع مجدد به تمام اعضا شوند.
        var users = await _context.WorkflowAssignments
            .Where(x =>
                x.WorkflowInstanceId == instance.ID &&
                x.WorkflowStepId == previousStep.ID)
            .Select(x => x.UserId)
            .Distinct()
            .ToListAsync();

        if (users.Count == 0)
            users = await GetStepUsers(previousStep);

        if (users.Count == 0)
            throw new Exception("برای مرحله قبلی گیرنده مشخصی ثبت نشده است.");

        foreach (var userId in users)
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
        }

        await SetUserAccess(
            instance.EntityId,
            senderUserId,
            ProjectAccessType.View);

        foreach (var userId in users)
        {
            await SetUserAccess(
                instance.EntityId,
                userId,
                ProjectAccessType.Edit);
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

        foreach (var access in accesses)
            access.AccessType = ProjectAccessType.View;
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
        clsWorkflowHistory? workflowHistory = await _context.WorkflowHistories
            .Where(x => x.WorkflowInstanceId == instance.ID)
            .OrderByDescending(x => x.ActionDate)
            .FirstOrDefaultAsync();
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
            .FirstOrDefaultAsync(x => x.ID == workflowId && x.IsActive);

        if (workflow == null)
            throw new Exception("گردش کار فعال یافت نشد.");

        var firstStep = workflow.WorkflowSteps
            .OrderBy(x => x.Order)
            .FirstOrDefault();

        if (firstStep == null)
            throw new Exception("برای این گردش هیچ مرحله‌ای تعریف نشده است.");

        var ownsTransaction = _context.Database.CurrentTransaction == null;
        await using var transaction = ownsTransaction
            ? await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable)
            : null;

        try
        {
            var now = DateTime.Now;

            // Projection تنها مرجع تشخیص گردش جاری برآورد است.
            if (entityType == nameof(clsBaravordUser))
            {
                var baravordExists = _context.BaravordUsers.Local
                    .Any(x => x.ID == entityId)
                    || await _context.BaravordUsers
                        .AnyAsync(x => x.ID == entityId);

                if (!baravordExists)
                    throw new Exception("برآورد موردنظر یافت نشد.");

                var activeProjections = await _context.BarAvordProjectionWorkFlows
                    .Where(x => x.BarAvordUserId == entityId && !x.IsComplete)
                    .ToListAsync();

                // شروع مجدد همان گردش در حالی که اجرای قبلی هنوز فعال است، مجاز نیست.
                if (activeProjections.Any(x => x.proccesType == workflow.proccesType))
                {
                    var sameProcessRunning = await _context.WorkflowInstances
                        .AnyAsync(x =>
                            x.EntityId == entityId &&
                            x.Status == WorkflowInstanceStatus.Running &&
                            x.Workflow.proccesType == workflow.proccesType);

                    if (sameProcessRunning)
                        throw new Exception("این نوع گردش هم‌اکنون برای برآورد در حال اجراست.");
                }

                // گردش‌های قبلی بسته می‌شوند و اجرای بازمانده آن‌ها نیز لغو می‌شود
                // تا گیرندگان قبلی نتوانند روی گردش قدیمی اقدام کنند.
                // تمام اجراهای باز قبلی این برآورد بسته می‌شوند؛ حتی اگر Projection قدیمی نداشته باشند.
                var previousInstances = await _context.WorkflowInstances
                    .Where(x =>
                        x.EntityId == entityId &&
                        x.Status != WorkflowInstanceStatus.Completed &&
                        x.Status != WorkflowInstanceStatus.Rejected &&
                        x.Status != WorkflowInstanceStatus.Cancelled)
                    .ToListAsync();

                var previousInstanceIds = previousInstances
                    .Select(x => x.ID)
                    .ToList();

                foreach (var previousInstance in previousInstances)
                    previousInstance.Status = WorkflowInstanceStatus.Cancelled;

                if (previousInstanceIds.Count > 0)
                {
                    var pendingAssignments = await _context.WorkflowAssignments
                        .Where(x =>
                            previousInstanceIds.Contains(x.WorkflowInstanceId) &&
                            !x.IsCompleted)
                        .ToListAsync();

                    foreach (var assignment in pendingAssignments)
                    {
                        assignment.IsCompleted = true;
                        assignment.Result = WorkflowAssignmentResult.Cancelled;
                        assignment.CompletedDate = now;
                        assignment.Comment = "به دلیل شروع گردش جدید بسته شد.";
                    }
                }

                foreach (var projection in activeProjections)
                    projection.IsComplete = true;

                _context.BarAvordProjectionWorkFlows.Add(
                    new clsBarAvordProjectionWorkFlow
                    {
                        ID = Guid.NewGuid(),
                        BarAvordUserId = entityId,
                        proccesType = workflow.proccesType,
                        IsComplete = false,
                        IsDeleted = false,
                        UserInserter = senderUserId,
                        InsertDateTime = now
                    });
            }

            var instance = new clsWorkflowInstance
            {
                ID = Guid.NewGuid(),
                WorkflowId = workflow.ID,
                EntityId = entityId,
                EntityType = entityType,
                CurrentStepId = firstStep.ID,
                Status = WorkflowInstanceStatus.Running,
                UserInserter = senderUserId,
                InsertDateTime = now
            };

            _context.WorkflowInstances.Add(instance);

            await AssignStepAsync(
                instance,
                firstStep,
                senderUserId.ToString(),
                selectedUsers);

            await UpdateProjectAccessAsync(
                entityId,
                firstStep,
                selectedUsers);

            await _context.SaveChangesAsync();

            if (ownsTransaction)
                await transaction!.CommitAsync();

            return instance.ID;
        }
        catch
        {
            if (ownsTransaction && transaction != null)
                await transaction.RollbackAsync();

            throw;
        }
    }



    private async Task AssignStepAsync(
        clsWorkflowInstance instance,
        clsWorkflowStep step,
        string senderUserId,
        List<string>? selectedUsers = null)
    {
        // نقش‌ها فقط فهرست کاربران مجاز برای انتخاب را مشخص می‌کنند؛
        // هیچ‌گاه به‌تنهایی باعث ارجاع خودکار به تمام اعضای نقش نمی‌شوند.
        var users = await ResolveStepReceiversAsync(step, selectedUsers);

        if (users.Count == 0)
            throw new Exception("برای مرحله اول هیچ گیرنده‌ای تعریف نشده است.");

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

    /// <summary>
    /// گیرندگان ثابت مرحله؛ فقط کاربرانی که مستقیماً در خود مرحله ثبت شده‌اند.
    /// وجود نقش به معنی ارجاع خودکار تمام اعضای آن نقش نیست.
    /// </summary>
    private Task<List<string>> GetStepUsers(clsWorkflowStep step)
    {
        var users = step.Users
            .Select(x => x.UserId)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToList();

        return Task.FromResult(users);
    }

    /// <summary>
    /// کاربران مجاز برای انتخاب دستی: کاربران صریح مرحله به‌علاوه اعضای نقش‌های
    /// تعریف‌شده به‌عنوان دامنه مجاز انتخاب.
    /// </summary>
    private async Task<List<string>> GetSelectableStepUsersAsync(clsWorkflowStep step)
    {
        var users = (await GetStepUsers(step)).ToHashSet();

        var roleIds = step.Roles
            .Select(x => x.RoleId)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToList();

        if (roleIds.Count > 0)
        {
            var roleUsers = await _context.UserRoles
                .Where(x => roleIds.Contains(x.RoleId))
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();

            users.UnionWith(roleUsers.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        return users.ToList();
    }

    /// <summary>
    /// گیرندگان واقعی مرحله را با توجه به تنظیم انتخاب دستی تعیین می‌کند.
    /// </summary>
    private async Task<List<string>> ResolveStepReceiversAsync(
        clsWorkflowStep step,
        List<string>? selectedUsers)
    {
        if (!step.NeedSelectReceiver)
            return await GetStepUsers(step);

        if (selectedUsers == null || selectedUsers.Count == 0)
            throw new Exception("حداقل یک گیرنده برای این مرحله انتخاب کنید.");

        var allowedUsers = await GetSelectableStepUsersAsync(step);
        var receivers = selectedUsers
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToList();

        if (receivers.Count == 0)
            throw new Exception("گیرنده معتبری انتخاب نشده است.");

        if (receivers.Any(x => !allowedUsers.Contains(x)))
            throw new Exception("یکی از گیرندگان انتخاب‌شده در فهرست مجاز این مرحله نیست.");

        return receivers;
    }


    private async Task UpdateProjectAccessAsync(
    Guid entityId,
    clsWorkflowStep step,
    List<string>? selectedUsers = null)
    {
        var accesses = await _context.BaravordUserAccesses
            .Where(x => x.BaravordUserId == entityId)
            .ToListAsync();

        // دسترسی‌های ثبت‌نشده در همین DbContext (مثلاً مالک برآورد تازه‌ساخته‌شده)
        // نیز باید در محاسبه لحاظ شوند تا رکورد تکراری ایجاد نشود.
        var localAccesses = _context.BaravordUserAccesses.Local
            .Where(x =>
                x.BaravordUserId == entityId &&
                accesses.All(dbAccess => dbAccess.ID != x.ID))
            .ToList();

        accesses.AddRange(localAccesses);

        foreach (var access in accesses)
        {
            if (access.AccessType != ProjectAccessType.Owner)
                access.AccessType = ProjectAccessType.View;
        }

        var users = await ResolveStepReceiversAsync(step, selectedUsers);

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
