using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Interfaces.Workflow;
using RMS.Models.Entity;
using RMS.ViewModels.Workflow;

namespace RMS.Controllers.WorkFlow;

public class WorkflowController(ApplicationDbContext context, IWorkflowQueryService query,
        IWorkflowCommandService command) : Controller
{
    private readonly ApplicationDbContext _context = context;
    private readonly IWorkflowQueryService _query=query;

    private readonly IWorkflowCommandService _command = command;
    public async Task<IActionResult> Index()
    {
        var model = await _context.Workflows
            .OrderBy(x => x.Name)
            .ToListAsync();

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ViewShowWorkFlowForm", model);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Save(
    [FromBody] WorkflowVm model)
    {

        try
        {

            var id = await _command.SaveAsync(model);


            return Json(new
            {
                success = true,
                id = id
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

    [HttpGet]
    public async Task<IActionResult> GetWorkflows()
    {
        var result = await _query.GetAllAsync();

        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _query.GetAsync(id);

        return Json(result);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {

        try
        {

            await _command.DeleteAsync(id);


            return Json(new
            {
                success = true
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


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(clsWorkflow model)
    {
        if (!ModelState.IsValid)
            return View(model);

        model.ID = Guid.NewGuid();

        _context.Workflows.Add(model);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = await _context.Workflows.FindAsync(id);

        if (model == null)
            return NotFound();

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(clsWorkflow model)
    {
        if (!ModelState.IsValid)
            return View(model);

        _context.Update(model);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }



}
