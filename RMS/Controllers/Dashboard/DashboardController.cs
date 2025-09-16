using Microsoft.AspNetCore.Mvc;

namespace RMS.Controllers.Dashboard;

public class DashboardController : Controller
{
    // صفحه اصلی داشبورد
    public IActionResult Index()
    {
        return PartialView();
    }
}
