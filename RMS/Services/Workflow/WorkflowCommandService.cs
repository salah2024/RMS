using Microsoft.EntityFrameworkCore;
using RMS.Interfaces.Workflow;
using RMS.Models.Entity;
using RMS.ViewModels.Workflow;

namespace RMS.Services.Workflow;

public class WorkflowCommandService : IWorkflowCommandService
{
    private readonly ApplicationDbContext _context;

    public WorkflowCommandService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> SaveAsync(WorkflowVm model)
    {

        // بررسی نام تکراری

        bool exists = await _context.Workflows
            .AnyAsync(x =>
                x.Name == model.Name &&
                x.ID != model.Id);


        if (exists)
        {
            throw new Exception(
                "گردشی با این نام قبلاً ثبت شده است.");
        }



        // درج جدید

        if (model.Id == Guid.Empty)
        {

            clsWorkflow workflow = new clsWorkflow
            {
                ID = Guid.NewGuid(),

                Name = model.Name.Trim(),

                IsActive = model.IsActive,
                proccesType = model.proccesType
            };


            _context.Workflows.Add(workflow);


            await _context.SaveChangesAsync();


            return workflow.ID;

        }

        // ویرایش

        var item = await _context.Workflows
            .FirstOrDefaultAsync(x => x.ID == model.Id);


        if (item == null)
            throw new Exception("گردش پیدا نشد.");

        item.Name = model.Name.Trim();

        item.IsActive = model.IsActive;

        item.proccesType = model.proccesType;
        item.IsSystem = model.IsSystem;
        item.IsDefault = model.IsDefault;
        item.Auto = model.Auto;

        await _context.SaveChangesAsync();

        return item.ID;
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var workflow = await _context.Workflows
            .Include(x => x.WorkflowSteps)
            .FirstOrDefaultAsync(x => x.ID == id);

        if (workflow == null)
        {
            throw new Exception("گردش یافت نشد.");
        }



        if (workflow.WorkflowSteps.Any())
        {
            throw new Exception(
                "ابتدا مراحل این گردش را حذف کنید.");
        }



        _context.Workflows.Remove(workflow);


        await _context.SaveChangesAsync();


        return true;

    }
}