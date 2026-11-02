using Microsoft.EntityFrameworkCore;
using RMS.Interfaces.Workflow;
using RMS.ViewModels.Workflow;

namespace RMS.Services.Workflow;

public class WorkflowQueryService : IWorkflowQueryService
{
    private readonly ApplicationDbContext _context;

    public WorkflowQueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkflowListVm>> GetAllAsync()
    {
        return await _context.Workflows

            .AsNoTracking()

            .OrderBy(x => x.Name)

            .Select(x => new WorkflowListVm
            {
                Id = x.ID,

                Name = x.Name,

                IsActive = x.IsActive,

                StepCount = x.WorkflowSteps.Count()
            })

            .ToListAsync();
    }

    public async Task<WorkflowVm?> GetAsync(Guid id)
    {
        return await _context.Workflows
            .Where(x => x.ID == id)
            .Select(x => new WorkflowVm
            {
                Id = x.ID,
                Name = x.Name,
                IsDefault = x.IsDefault,
                IsSystem = x.IsSystem,
                proccesType = x.proccesType,
                IsActive = x.IsActive,
                Auto = x.Auto
            })
            .FirstOrDefaultAsync();
    }
}
