using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Controllers.ItemFBStar.Dto;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
namespace RMS.Controllers.ItemFBStar;

public class ItemFBStarController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public JsonResult SaveItemFBStar([FromBody] SaveItemFBStarDto request)
    {
        Guid BaravordId = request.BaravordId;
        string FBShomareh = request.FBShomareh;
        decimal BahayeVahed = request.BahayeVahed;
        int VahedId = request.VahedId;
        string Sharh = request.Sharh;

        clsItemFBStar itemFBStar = new clsItemFBStar
        {
            BaravordId = BaravordId,
            Shomareh = FBShomareh,
            BahayeVahed = BahayeVahed,
            VahedId = VahedId,
            Sharh = Sharh,
        };

        _context.ItemFBStars.Add(itemFBStar);
        _context.SaveChanges();

        return new JsonResult("OK");
    }

    [HttpPost]
    public JsonResult GetCurrentRizMetreStarForShowBarAvord([FromBody] GetCurrentRizMetreStarForShowBarAvordDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        string ItemFBShomareh = request.ItemFBShomareh.Trim();

        clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.BaravordId == BarAvordUserId && x.Shomareh == ItemFBShomareh);
        if (itemFBStar!=null)
        {

            List<clsRizMetreStar> lstRizMetreStars= _context.RizMetreStars.Where(x => x.ItemFBStarId == itemFBStar.ID).OrderBy(x=>x.Shomareh).ToList();
            return new JsonResult(lstRizMetreStars);
        }

        return new JsonResult("NOK");
    }

    [HttpPost]
    public JsonResult ConfirmRizMetreItemFBStar([FromBody] ConfirmRizMetreItemFBStarDto request)
    {
        DateTime Now = DateTime.Now;
        string Sharh = request.Sharh;
        decimal? Tedad = request.Tedad;
        decimal? Tool = request.Tedad;
        decimal? Arz = request.Arz;
        decimal? Ertefa = request.Ertefa;
        decimal? Vazn = request.Vazn;
        string Des = request.Des;
        string FBShomareh = request.Shomareh.Trim();
        int Year = request.Year;
        Guid BarAvordUserId = request.BarAvordUserId;

        clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.BaravordId == BarAvordUserId && x.Shomareh == FBShomareh);

        if (itemFBStar != null)
        {

            clsRizMetreStar? RizMetreStar = context.RizMetreStars.Where(x => x.ItemFBStarId == itemFBStar.ID).OrderByDescending(x => x.Shomareh).FirstOrDefault();
            long Shomareh = 0;
            if (RizMetreStar != null)
                Shomareh = RizMetreStar.Shomareh + 1;
            else
                Shomareh = 1;

            clsRizMetreStar RizMetreStarNew = new clsRizMetreStar();
            RizMetreStarNew.ID = Guid.NewGuid();
            RizMetreStarNew.Shomareh = Shomareh;
            RizMetreStarNew.ShomarehNew = Shomareh.ToString();
            RizMetreStarNew.Sharh = Sharh.Trim();
            RizMetreStarNew.Tedad = Tedad;
            RizMetreStarNew.Tool = Tool;
            RizMetreStarNew.Arz = Arz;
            RizMetreStarNew.Ertefa = Ertefa;
            RizMetreStarNew.Vazn = Vazn;
            RizMetreStarNew.Des = Des.Trim();
            RizMetreStarNew.ItemFBStarId = itemFBStar.ID;
            RizMetreStarNew.InsertDateTime = Now;

            _context.RizMetreStars.Add(RizMetreStarNew);
            _context.SaveChanges();
        }

        return new JsonResult("OK_");
    }

    [HttpPost]
    public JsonResult UpdateRizMetreItemStarFrmShowBarAvord([FromBody] UpdateRizMetreItemStarFrmShowBarAvordDto request)
    {
        Guid Id = request.Id;
        string Sharh = request.Sharh;
        decimal? Tedad = request.Tedad;
        decimal? Tool = request.Tool;
        decimal? Arz = request.Arz;
        decimal? Ertefa = request.Ertefa;
        decimal? Vazn = request.Vazn;
        string? Des = request.Des;
        string Code = request.Code;

        clsRizMetreStar? entity = context.RizMetreStars.FirstOrDefault(x => x.ID == Id);
        if (entity == null)
            return new JsonResult("NOK");

        entity.Sharh = Sharh;
        entity.Tedad = Tedad;
        entity.Tool = Tool;
        entity.Arz = Arz;
        entity.Ertefa = Ertefa;
        entity.Vazn = Vazn;
        entity.Des = Des;

        decimal dMeghdarJoz = 0;
        if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
            dMeghdarJoz = 0;
        else
            dMeghdarJoz += (Tedad == null ? 1 : Tedad.Value) * (Tool == null ? 1 : Tool.Value) *
            (Arz == null ? 1 : Arz.Value) * (Ertefa == null ? 1 : Ertefa.Value) * (Vazn == null ? 1 : Vazn.Value);
        entity.MeghdarJoz = dMeghdarJoz;

        _context.SaveChanges();

        return new JsonResult("OK_");
    }

    [HttpPost]
    public ActionResult DeleteRizMetreStar([FromBody] DeleteRizMetreInputStarFromShowBarAvordDto request)
    {
        try
        {
            clsRizMetreStar? entity = context.RizMetreStars.Find(request.Id);
            if (entity != null)
            {
                context.RizMetreStars.Remove(entity);
            }
            context.SaveChanges();
            return new JsonResult("OK");

        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }

}


