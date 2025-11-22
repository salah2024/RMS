using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Area.Dto;
using RMS.Controllers.ZaribBalaSari.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.Area;

public class AreaController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public JsonResult GetBaravordZaribManteghe([FromBody] GetBaravordBakhshDto request)
    {
        Guid baravordId = request.BaravordId;

        var baravordZaribManteghe = _context.BaravordZaribManteghes
            .Include(x => x.BaravordUser)
            .FirstOrDefault(x => x.BaravordId == baravordId);

        // استان‌ها همیشه لود شوند
        List<clsOstan> lstOstan = _context.Ostans.ToList();

        List<clsShahr> lstShahr = new List<clsShahr>();
        List<clsBakhsh> lstBakhsh = new List<clsBakhsh>();

        if (baravordZaribManteghe != null)
        {
            lstShahr = _context.Shahrs
                .Where(x => x.OstanId == baravordZaribManteghe.OstanId)
                .ToList();

            lstBakhsh = _context.Bakhshs
                .Where(x => x.ShahrId == baravordZaribManteghe.ShahrId)
                .ToList();
        }

        var result = new
        {
            lstOstan,
            lstShahr,
            lstBakhsh,
            baravordZaribManteghe
        };

        return new JsonResult(result);
    }


    [HttpPost]
    public JsonResult GetOstan()
    {
        List<clsOstan> lstOstan = _context.Ostans.ToList();
        return new JsonResult(lstOstan);
    }

    [HttpPost]
    public JsonResult GetShahr([FromBody] GetShahrDto request)
    {
        List<clsShahr> lstShahr = _context.Shahrs.Where(x => x.OstanId == request.OstanId).ToList();
        return new JsonResult(lstShahr);
    }

    [HttpPost]
    public JsonResult GetBakhsh([FromBody] GetBakhshDto request)
    {
        List<clsBakhsh> lstBakhsh = _context.Bakhshs.Where(x => x.ShahrId == request.ShahrId).ToList();
        return new JsonResult(lstBakhsh);
    }

    [HttpPost]
    public JsonResult GetZaribManteghe([FromBody] GetZaribMantagheDto request)
    {
        long BakhshId = request.BakhshId;

        var Bakhsh = _context.Bakhshs.Include(x=>x.Shahr).Where(x => x.Id == BakhshId).Select(x=>new
        {
            ZaribRah=x.ZaribRah,
            OstanId=x.Shahr.OstanId,
            ShahrId=x.ShahrId,
        }).FirstOrDefault();
        ///در صورت پیدا شدن ضریب بخش، ضریب منطقه نیز همزمان در صورتی که قبلا درج نشده باشد درج  میشود
        ///و در صورتی که قبلا درج شده باشد، ویرایش میگردد
        if (Bakhsh != null)
        {
            Guid BaravordId = request.BaravordId;
            clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.Include(x => x.BaravordUser).FirstOrDefault(x => x.BaravordId == BaravordId);
            if (baravordZaribManteghe != null)
            {
                _context.Entry(baravordZaribManteghe).CurrentValues.SetValues(new
                {
                    OstanId = Bakhsh.OstanId,
                    ShahrId = Bakhsh.ShahrId,
                    BakhshId = BakhshId,
                    ZaribManteghe = Bakhsh.ZaribRah
                });
            }
            else
            {
                clsBaravordZaribManteghe baravordZaribMantegheNew = new clsBaravordZaribManteghe
                {
                    BaravordId = BaravordId,
                    OstanId = Bakhsh.OstanId,
                    ShahrId = Bakhsh.ShahrId,
                    BakhshId = BakhshId,
                    ZaribManteghe = Bakhsh.ZaribRah
                };
                _context.BaravordZaribManteghes.Add(baravordZaribMantegheNew);
            }

            _context.SaveChanges();
            return new JsonResult(Bakhsh);
        }
        else
            return new JsonResult(null);
    }
}
