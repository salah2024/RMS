using Microsoft.EntityFrameworkCore;
using RMS.Interfaces.Workflow;
using RMS.Models.Entity;
using RMS.ViewModels.WorkflowStep;

namespace RMS.Services.Workflow;

public class WorkflowStepCommandService
    : IWorkflowStepCommandService
{

    private readonly ApplicationDbContext _context;


    public WorkflowStepCommandService(
        ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Guid> SaveAsync(WorkflowStepVm model)
    {
        if (model.WorkflowId == Guid.Empty)
            throw new Exception("گردش کار مشخص نشده است.");

        if (string.IsNullOrWhiteSpace(model.Title))
            throw new Exception("عنوان مرحله را وارد کنید.");

        if (!model.RoleIds.Any() && !model.UserIds.Any())
            throw new Exception("حداقل یک نقش یا یک کاربر انتخاب کنید.");

        var workflow = await _context.Workflows
            .FirstOrDefaultAsync(x => x.ID == model.WorkflowId);

        if (workflow == null)
            throw new Exception("گردش کار یافت نشد.");

        bool duplicateTitle = await _context.WorkflowSteps
            .AnyAsync(x =>
                x.WorkflowId == model.WorkflowId &&
                x.Title == model.Title &&
                x.ID != model.Id);

        if (duplicateTitle)
            throw new Exception("عنوان مرحله تکراری است.");

        if (model.IsFinal)
        {
            bool hasFinal = await _context.WorkflowSteps
                .AnyAsync(x =>
                    x.WorkflowId == model.WorkflowId &&
                    x.IsFinal &&
                    x.ID != model.Id);

            if (hasFinal)
                throw new Exception("این گردش قبلاً مرحله پایانی دارد.");
        }

        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            clsWorkflowStep? step;

            #region Insert

            if (model.Id == Guid.Empty)
            {
                int maxOrder = await _context.WorkflowSteps
                    .Where(x => x.WorkflowId == model.WorkflowId)
                    .Select(x => (int?)x.Order)
                    .MaxAsync() ?? 0;

                step = new clsWorkflowStep
                {
                    ID = Guid.NewGuid(),
                    WorkflowId = model.WorkflowId,
                    Title = model.Title.Trim(),
                    Order = maxOrder + 1,
                    AllowReturn = model.AllowReturn,
                    IsFinal = model.IsFinal,
                    NeedSelectReceiver = model.NeedSelectReceiver,
                    WorkflowActionId = (int)workflow.proccesType,
                    InsertDateTime = DateTime.Now,
                    IsDeleted = false
                };

                _context.WorkflowSteps.Add(step);

                await _context.SaveChangesAsync();
            }

            #endregion

            #region Update

            else
            {
                step = await _context.WorkflowSteps
                     .Include(x => x.Roles)
                     .Include(x => x.Users)
                     .FirstOrDefaultAsync(x => x.ID == model.Id);

                if (step == null)
                    throw new Exception("مرحله یافت نشد.");

                step.Title = model.Title.Trim();
                step.AllowReturn = model.AllowReturn;
                step.IsFinal = model.IsFinal;
                step.NeedSelectReceiver = model.NeedSelectReceiver;

                if (step.Roles.Any())
                    _context.WorkflowStepRoles.RemoveRange(step.Roles);

                if (step.Users.Any())
                    _context.WorkflowStepUsers.RemoveRange(step.Users);

                await _context.SaveChangesAsync();
            }

            #endregion

            await SaveReceivers(step.ID, model);

            await UpdateStepRelations(model.WorkflowId);

            await transaction.CommitAsync();

            return step.ID;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task UpdateStepRelations(Guid workflowId)
    {
        var steps = await _context.WorkflowSteps
            .Where(x => x.WorkflowId == workflowId &&
                        (x.IsDeleted == false || x.IsDeleted == null))
            .OrderBy(x => x.Order)
            .ToListAsync();

        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].PreviousStepId =
                i == 0
                    ? null
                    : steps[i - 1].ID;

            steps[i].NextStepId =
                i == steps.Count - 1
                    ? null
                    : steps[i + 1].ID;
        }


        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        bool blnCheck = true;
        var step = await _context.WorkflowSteps
            .Include(x => x.Roles)
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.ID == id);

        if (step == null)
        {    
            blnCheck = false;
            return false;
        }

        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        //try
        //{
        Guid workflowId = step.WorkflowId;

        // بررسی استفاده مرحله در نمونه‌های گردش کار
        bool hasInstances = await _context.WorkflowInstances
            .AnyAsync(x => x.CurrentStepId == id);

        if (hasInstances)
        {
            await transaction.RollbackAsync();
            blnCheck = false;
            return false;
        }

        // حذف تخصیص‌های وابسته به مرحله
        var assignments = await _context.WorkflowAssignments
            .Where(x => x.WorkflowStepId == id)
            .ToListAsync();

        if (assignments.Any())
        {
            _context.WorkflowAssignments.RemoveRange(assignments);
        }

        // حذف نقش‌های مرحله
        if (step.Roles.Any())
        {
            _context.WorkflowStepRoles.RemoveRange(step.Roles);
        }

        // حذف کاربران مرحله
        if (step.Users.Any())
        {
            _context.WorkflowStepUsers.RemoveRange(step.Users);
        }

        // حذف مرحله
        _context.WorkflowSteps.Remove(step);

        await _context.SaveChangesAsync();

        // اصلاح ترتیب مراحل باقی‌مانده
        var steps = await _context.WorkflowSteps
            .Where(x => x.WorkflowId == workflowId)
            .OrderBy(x => x.Order)
            .ToListAsync();

        int order = 1;

        foreach (var item in steps)
        {
            item.Order = order++;
        }

        await _context.SaveChangesAsync();

        // به‌روزرسانی روابط مراحل
        await UpdateStepRelations(workflowId);

        await transaction.CommitAsync();
        blnCheck = true;

        return blnCheck;
        //}
        //catch
        //{
        //    await transaction.RollbackAsync();
        //    throw;
        //}
    }

    //public async Task<bool> DeleteAsync(Guid id)
    //{
    //    var step = await _context.WorkflowSteps
    //        .Include(x => x.Roles)
    //        .Include(x => x.Users)
    //        .FirstOrDefaultAsync(x => x.ID == id);

    //    if (step == null)
    //        return false;

    //    await using var transaction =
    //        await _context.Database.BeginTransactionAsync();

    //    //try
    //    //{

    //    // حذف تخصیص‌های وابسته به مرحله
    //    var assignments = await _context.WorkflowAssignments
    //        .Where(x => x.WorkflowStepId == id)
    //        .ToListAsync();

    //    if (assignments.Any())
    //        _context.WorkflowAssignments.RemoveRange(assignments);

    //    if (step.Roles.Any())
    //            _context.WorkflowStepRoles.RemoveRange(step.Roles);

    //        if (step.Users.Any())
    //            _context.WorkflowStepUsers.RemoveRange(step.Users);

    //        Guid workflowId = step.WorkflowId;

    //        _context.WorkflowSteps.Remove(step);

    //        await _context.SaveChangesAsync();

    //        var steps = await _context.WorkflowSteps
    //            .Where(x => x.WorkflowId == workflowId)
    //            .OrderBy(x => x.Order)
    //            .ToListAsync();

    //        int order = 1;

    //        foreach (var item in steps)
    //        {
    //            item.Order = order++;
    //        }

    //        await _context.SaveChangesAsync();

    //        await UpdateStepRelations(step.WorkflowId);

    //        await transaction.CommitAsync();

    //        return true;
    //    //}
    //    //catch
    //    //{
    //        //await transaction.RollbackAsync();
    //        //throw;
    //    //}
    //}

    public async Task MoveUpAsync(Guid id)
    {
        var step = await _context.WorkflowSteps
            .FirstOrDefaultAsync(x => x.ID == id);

        if (step == null)
            return;

        var previous = await _context.WorkflowSteps
            .Where(x =>
                x.WorkflowId == step.WorkflowId &&
                x.Order < step.Order)
            .OrderByDescending(x => x.Order)
            .FirstOrDefaultAsync();

        if (previous == null)
            return;

        int temp = step.Order;
        step.Order = previous.Order;
        previous.Order = temp;

        await _context.SaveChangesAsync();
    }

    public async Task MoveDownAsync(Guid id)
    {
        var step = await _context.WorkflowSteps
            .FirstOrDefaultAsync(x => x.ID == id);

        if (step == null)
            return;

        var next = await _context.WorkflowSteps
            .Where(x =>
                x.WorkflowId == step.WorkflowId &&
                x.Order > step.Order)
            .OrderBy(x => x.Order)
            .FirstOrDefaultAsync();

        if (next == null)
            return;

        int temp = step.Order;
        step.Order = next.Order;
        next.Order = temp;

        await _context.SaveChangesAsync();
    }

    private async Task SaveReceivers(Guid workflowStepId, WorkflowStepVm model)
    {
        if (model.RoleIds.Any())
        {
            foreach (var roleId in model.RoleIds.Distinct())
            {
                _context.WorkflowStepRoles.Add(new clsWorkflowStepRole
                {
                    ID = Guid.NewGuid(),
                    WorkflowStepId = workflowStepId,
                    RoleId = roleId,
                    InsertDateTime = DateTime.Now,
                    IsDeleted = false
                });
            }
        }

        if (model.UserIds.Any())
        {
            foreach (var userId in model.UserIds.Distinct())
            {
                _context.WorkflowStepUsers.Add(new clsWorkflowStepUser
                {
                    ID = Guid.NewGuid(),
                    WorkflowStepId = workflowStepId,
                    UserId = userId,
                    InsertDateTime = DateTime.Now,
                    IsDeleted = false
                });
            }
        }

        await _context.SaveChangesAsync();
    }
}