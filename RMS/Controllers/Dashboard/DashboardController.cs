using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers.Dashboard;

public class DashboardController : Controller
{
    // صفحه اصلی داشبورد
    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ViewDashboardForm");

        return View("Index");
    }
}
