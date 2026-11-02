using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Interfaces.Workflow;
using RMS.ViewModels.WorkflowStep;

namespace RMS.Services.Workflow;

public class WorkflowStepQueryService
    : IWorkflowStepQueryService
{

    private readonly ApplicationDbContext _context;


    public WorkflowStepQueryService(
        ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<List<WorkflowStepListVm>> GetByWorkflowAsync(Guid workflowId)
    {
        return await _context.WorkflowSteps
            .AsNoTracking()
            .Where(x => x.WorkflowId == workflowId)
            .OrderBy(x => x.Order)
            .Select(x => new WorkflowStepListVm
            {
                Id = x.ID,

                Order = x.Order,

                Title = x.Title,

                AllowReturn = x.AllowReturn,

                IsFinal = x.IsFinal,

                NeedSelectReceiver = x.NeedSelectReceiver,

                Roles = string.Join(" ، ",
                    x.Roles
                     .Select(r => r.Role.Name)),

                Users = string.Join(" ، ",
                    x.Users
                     .Select(u => u.User.FullName))
            })
            .ToListAsync();
    }

    public async Task<List<WorkflowStepVm>> GetAsync(Guid id)
    {
        return await _context.WorkflowSteps

            .AsNoTracking()

            .Where(x => x.WorkflowId == id)

            .Select(x => new WorkflowStepVm
            {
                Id = x.ID,
                Order = x.Order,
                WorkflowId = x.WorkflowId,

                Title = x.Title,

                AllowReturn = x.AllowReturn,

                IsFinal = x.IsFinal,

                NeedSelectReceiver = x.NeedSelectReceiver,

                RoleNames = x.Roles
                    .Select(r => r.Role.Name)
                    .ToList(),

                UserNames = x.Users
                    .Select(u => u.User.FullName +
                        (u.User.UserDetail != null && u.User.UserDetail.Company != null
                            ? " - " + u.User.UserDetail.Company.CompanyName
                            : ""))
                    .ToList()

            }).OrderBy(x => x.Order).ToListAsync();
    }
    public async Task<WorkflowStepVm?> GetForEditAsync(Guid id)
    {
        return await _context.WorkflowSteps

            .AsNoTracking()

            .Where(x => x.ID == id)

            .Select(x => new WorkflowStepVm
            {
                Id = x.ID,

                WorkflowId = x.WorkflowId,

                Title = x.Title,

                AllowReturn = x.AllowReturn,

                IsFinal = x.IsFinal,

                NeedSelectReceiver = x.NeedSelectReceiver,

                RoleNames = x.Roles
                    .Select(r => r.Role.Name)
                    .ToList(),

                UserNames = x.Users
                    .Select(u => u.User.FullName)
                    .ToList(),

                RoleIds = x.Roles
                    .Select(r => r.Role.Id)
                    .ToList(),

                UserIds = x.Users
                    .Select(u => u.User.Id)
                    .ToList()

            })

            .FirstOrDefaultAsync();
    }
}