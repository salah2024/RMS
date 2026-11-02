using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Stimulsoft.Report.Func;

namespace RMS.Controllers.Dashboard;

public class DashboardController : Controller
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _context = context;
    }

    // صفحه اصلی داشبورد
    // [Authorize]
    public async Task<IActionResult> Index()
    {
        //ApplicationUser? user = await _userManager.GetUserAsync(User);

        ApplicationUser? user = await _context.Users
        .Include(x => x.UserDetail)
        .ThenInclude(x => x.Company)
        .FirstOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));
       

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ViewDashboardForm");

        return View("Index");
    }
}
