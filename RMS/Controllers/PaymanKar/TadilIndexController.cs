using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Common;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Controllers.PaymanKar.Common;
using RMS.Controllers.PaymanKar.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using System.Globalization;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.PaymanKar;

public class TadilIndexController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public ActionResult ShowTadilIndex()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_ViewShowTadilIndexForm");

        return View();
    }
    [HttpPost]
    public async Task<JsonResult> GetTadilIndex([FromBody] GetTadilIndexDto request)
    {
        TadilCommon tadil = new TadilCommon();
        ResultGetTadilIndex Result = await tadil.GetTadil(request, _context);

        return new JsonResult(Result);
    }

    [HttpPost]
    public async Task<JsonResult> SumTadilIndex([FromBody] GetTadilIndexDto request)
    {
        TadilCommon tadil = new TadilCommon();
        ResultGetTadilIndex Result = await tadil.GetTadil(request, _context);

        return new JsonResult(Result);
    }

    /////////////
    
}