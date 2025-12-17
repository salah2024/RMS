using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Controllers.ItemFBStar.Dto;
using RMS.Controllers.Operation.Common;
using RMS.Controllers.Operation.Dto;
using RMS.Models.Common.Dto;
using RMS.Models.Common;
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
        bool? blnKharidTajhizat = request.KharidTajhizat;
        int Year = request.Year;
        NoeFehrestBaha NoeFBId = request.NoeFBId;

        clsFehrestBaha? fehrestBaha = _context.FehrestBahas.FirstOrDefault(x => x.Sal == Year && x.NoeFB == NoeFBId && x.Shomareh.Trim() == FBShomareh.Trim());
        if (fehrestBaha != null)
        {
            return new JsonResult("repeat");
        }

        clsItemFBStar? itemFBStarCurrent = _context.ItemFBStars.FirstOrDefault(x => x.Shomareh.Trim() == FBShomareh.Trim() && x.BaravordId == BaravordId && x.NoeFBId == NoeFBId);
        if (itemFBStarCurrent != null)
        {
            return new JsonResult("repeat");
        }

        clsItemFBStar itemFBStar = new clsItemFBStar
        {
            BaravordId = BaravordId,
            Shomareh = FBShomareh,
            BahayeVahed = BahayeVahed,
            VahedId = VahedId,
            Sharh = Sharh,
            blnKharidTajhizat = blnKharidTajhizat,
            NoeFBId = NoeFBId
        };

        _context.ItemFBStars.Add(itemFBStar);
        _context.SaveChanges();


        return new JsonResult("OK");
    }

    [HttpPost]
    public JsonResult UpdateItemFBStar([FromBody] UpdateItemFBStarDto request)
    {
        int Year = request.Year;
        clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.ID == request.Id);
        if (itemFBStar != null)
        {
            _context.Entry(itemFBStar).CurrentValues.SetValues(new
            {
                blnKharidTajhizat = request.KharidTajhizat,
            });
            _context.SaveChanges();

            decimal SumMeghdarJoz = context.RizMetreStars.Include(x => x.ItemFBStar)
                .Where(x => x.ItemFBStar.Shomareh.Trim() == itemFBStar.Shomareh && x.ItemFBStar.BaravordId == itemFBStar.BaravordId)
                .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

            //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == itemFBStar.BaravordId);
            //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == itemFBStar.BaravordId);

            //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;


            ////محاسبه ضرایب
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = itemFBStar.BaravordId,
                NoeFBId = itemFBStar.NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
            ///
            ///

            decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

            /////////
            //////////
            decimal BahayeVahedCurrent = 0;
            decimal JameFaslInBahayeVahed = 0;

            decimal JameFaslBaZaribInBahayeVahed = 0;
            List<clsItemFBStar> lstItemFBStarCurrent = _context.ItemFBStars.Where(x => x.BaravordId == itemFBStar.BaravordId && x.Shomareh.Substring(0, 2) == itemFBStar.Shomareh.Substring(0, 2)).ToList();
            if (lstItemFBStarCurrent.Count != 0)
            {
                foreach (var itemFBStarCurrent in lstItemFBStarCurrent)
                {
                    decimal JameFasl = context.RizMetreStars
                        .Where(x => x.ItemFBStarId == itemFBStarCurrent.ID)
                        .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

                    BahayeVahedCurrent = itemFBStarCurrent.BahayeVahed;

                    bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;

                    decimal ZaribStar2 = zaribBalaSari;
                    if (blnKharidTajhizat)
                    {
                        ZaribStar2 = 1.14m;
                    }
                    JameFaslBaZaribInBahayeVahed += JameFasl * BahayeVahedCurrent * zaribManteghe * ZaribStar2;
                    JameFaslInBahayeVahed += JameFasl * BahayeVahedCurrent;
                }
            }

            return new JsonResult("OK_" + SumMeghdarJoz + "_" + JameFaslInBahayeVahed + "_" + JameFaslBaZaribInBahayeVahed);
        }
        else
            return new JsonResult("NOK");
    }


    [HttpPost]
    public JsonResult DeleteItemFBStar([FromBody] DeleteItemFBStarDto request)
    {
        int Year = request.Year;
        clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.ID == request.Id);
        if (itemFBStar != null)
        {
            _context.ItemFBStars.Remove(itemFBStar);
            _context.SaveChanges();

            decimal SumMeghdarJoz = context.RizMetreStars.Include(x => x.ItemFBStar)
                .Where(x => x.ItemFBStar.Shomareh.Trim() == itemFBStar.Shomareh && x.ItemFBStar.BaravordId == itemFBStar.BaravordId)
                .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

            //decimal JameFasl = context.RizMetreStars.Include(x => x.ItemFBStar)
            //.Where(x => x.ItemFBStar.BaravordId == itemFBStar.BaravordId)
            //.Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);


            //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == itemFBStar.BaravordId);
            //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == itemFBStar.BaravordId);

            //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;



            ////محاسبه ضرایب
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = itemFBStar.BaravordId,
                NoeFBId = itemFBStar.NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
            decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

            ///
            ///

            decimal BahayeVahedCurrent = 0;
            decimal JameFaslInBahayeVahed = 0;

            decimal JameFaslBaZaribInBahayeVahed = 0;
            List<clsItemFBStar> lstItemFBStarCurrent = _context.ItemFBStars.Where(x => x.BaravordId == itemFBStar.BaravordId && x.Shomareh.Substring(0, 2) == itemFBStar.Shomareh.Substring(0, 2)).ToList();
            if (lstItemFBStarCurrent.Count != 0)
            {
                foreach (var itemFBStarCurrent in lstItemFBStarCurrent)
                {
                    decimal JameFasl = context.RizMetreStars
                        .Where(x => x.ItemFBStarId == itemFBStarCurrent.ID)
                        .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

                    BahayeVahedCurrent = itemFBStarCurrent.BahayeVahed;

                    bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;

                    decimal ZaribStar2 = zaribBalaSari;
                    if (blnKharidTajhizat)
                    {
                        ZaribStar2 = 1.14m;
                    }
                    JameFaslBaZaribInBahayeVahed += JameFasl * BahayeVahedCurrent * zaribManteghe * ZaribStar2;
                    JameFaslInBahayeVahed += JameFasl * BahayeVahedCurrent;
                }
            }

            return new JsonResult("OK_" + SumMeghdarJoz + "_" + JameFaslInBahayeVahed + "_" + JameFaslBaZaribInBahayeVahed);


            //return new JsonResult("OK_" + SumMeghdarJoz);
        }
        else
            return new JsonResult("NOK");
    }

    [HttpPost]
    public JsonResult GetCurrentRizMetreStarForShowBarAvord([FromBody] GetCurrentRizMetreStarForShowBarAvordDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        string ItemFBShomareh = request.ItemFBShomareh.Trim();
        NoeFehrestBaha NoeFBId = request.NoeFBId;

        clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh == ItemFBShomareh);
        if (itemFBStar != null)
        {

            List<clsRizMetreStar> lstRizMetreStars = _context.RizMetreStars.Where(x => x.ItemFBStarId == itemFBStar.ID).OrderBy(x => x.Shomareh).ToList();
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
        NoeFehrestBaha NoeFBId = request.NoeFBId;

        clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh == FBShomareh);

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

            ///محاسبه مقدار جزء
            decimal? dMeghdarJoz = null;
            if (Tedad == null && Tool == null && Arz == null && Ertefa == null && Vazn == null)
                dMeghdarJoz = null;
            else
                dMeghdarJoz = (Tedad == null ? 1 : Tedad) * (Tool == null ? 1 : Tool) *
                (Arz == null ? 1 : Arz) * (Ertefa == null ? 1 : Ertefa) * (Vazn == null ? 1 : Vazn);
            RizMetreStarNew.MeghdarJoz = dMeghdarJoz;

            _context.RizMetreStars.Add(RizMetreStarNew);
            _context.SaveChanges();

            decimal SumMeghdarJoz = context.RizMetreStars.Include(x => x.ItemFBStar)
                .Where(x => x.ItemFBStar.Shomareh.Trim() == FBShomareh && x.ItemFBStar.BaravordId == BarAvordUserId)
                .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);



            //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == request.BarAvordUserId);
            //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == request.BarAvordUserId);

            //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

            ////محاسبه ضرایب
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = BarAvordUserId,
                NoeFBId = NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
            decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

            ///
            ///

            decimal BahayeVahedCurrent = 0;
            decimal JameFaslBaZaribInBahayeVahed = 0;
            decimal JameFaslInBahayeVahed = 0;
            List<clsItemFBStar> lstItemFBStarCurrent = _context.ItemFBStars.Where(x => x.BaravordId == BarAvordUserId && x.Shomareh.Substring(0, 2) == FBShomareh.Substring(0, 2)).ToList();
            if (lstItemFBStarCurrent.Count != 0)
            {
                foreach (var itemFBStarCurrent in lstItemFBStarCurrent)
                {
                    decimal JameFasl = context.RizMetreStars
                        .Where(x => x.ItemFBStarId == itemFBStarCurrent.ID)
                        .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

                    BahayeVahedCurrent = itemFBStarCurrent.BahayeVahed;

                    bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;

                    decimal ZaribStar2 = zaribBalaSari;
                    if (blnKharidTajhizat)
                    {
                        ZaribStar2 = 1.14m;
                    }
                    JameFaslBaZaribInBahayeVahed += JameFasl * BahayeVahedCurrent * zaribManteghe * ZaribStar2;
                    JameFaslInBahayeVahed += JameFasl * BahayeVahedCurrent;
                }
            }


            return new JsonResult("OK_" + dMeghdarJoz + "_" + SumMeghdarJoz + "_" + JameFaslInBahayeVahed + "_" + JameFaslBaZaribInBahayeVahed);
        }
        else
            return new JsonResult("NOK_");
    }

    [HttpPost]
    public JsonResult UpdateRizMetreItemStarFrmShowBarAvord([FromBody] UpdateRizMetreItemStarFrmShowBarAvordDto request)
    {
        int Year = request.Year;

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


        clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.ID == entity.ItemFBStarId);

        if (itemFBStar != null)
        {
            decimal SumMeghdarJoz = context.RizMetreStars.Include(x => x.ItemFBStar)
                .Where(x => x.ItemFBStar.Shomareh.Trim() == itemFBStar.Shomareh && x.ItemFBStar.BaravordId == itemFBStar.BaravordId)
                .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

            //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == itemFBStar.BaravordId);
            //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == itemFBStar.BaravordId);

            //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

            ////محاسبه ضرایب
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = itemFBStar.BaravordId,
                NoeFBId = itemFBStar.NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
            decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

            ///
            ///

            decimal BahayeVahedCurrent = 0;
            decimal JameFaslInBahayeVahed = 0;

            decimal JameFaslBaZaribInBahayeVahed = 0;
            List<clsItemFBStar> lstItemFBStarCurrent = _context.ItemFBStars.Where(x => x.BaravordId == itemFBStar.BaravordId && x.Shomareh.Substring(0, 2) == itemFBStar.Shomareh.Substring(0, 2)).ToList();
            if (lstItemFBStarCurrent.Count != 0)
            {
                foreach (var itemFBStarCurrent in lstItemFBStarCurrent)
                {
                    decimal JameFasl = context.RizMetreStars
                        .Where(x => x.ItemFBStarId == itemFBStarCurrent.ID)
                        .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

                    BahayeVahedCurrent = itemFBStarCurrent.BahayeVahed;

                    bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;

                    decimal ZaribStar2 = zaribBalaSari;
                    if (blnKharidTajhizat)
                    {
                        ZaribStar2 = 1.14m;
                    }
                    JameFaslBaZaribInBahayeVahed += JameFasl * BahayeVahedCurrent * zaribManteghe * ZaribStar2;
                    JameFaslInBahayeVahed += JameFasl * BahayeVahedCurrent;
                }
            }
            return new JsonResult("OK_" + SumMeghdarJoz + "_" + JameFaslInBahayeVahed + "_" + JameFaslBaZaribInBahayeVahed);
        }
        else
            return new JsonResult("NOK_");
    }

    [HttpPost]
    public ActionResult DeleteRizMetreStar([FromBody] DeleteRizMetreInputStarFromShowBarAvordDto request)
    {
        try
        {
            string FBShomareh = request.FBShomareh;
            int Year = request.Year;
            Guid BarAvordUserId = request.BarAvordId;
            NoeFehrestBaha NoeFBId = request.NoeFBId;

            clsRizMetreStar? entity = context.RizMetreStars.Find(request.Id);
            if (entity != null)
            {
                context.RizMetreStars.Remove(entity);
            }
            context.SaveChanges();

            decimal SumMeghdarJoz = context.RizMetreStars.Include(x => x.ItemFBStar)
                .Where(x => x.ItemFBStar.Shomareh.Trim() == FBShomareh && x.ItemFBStar.BaravordId == BarAvordUserId)
                .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

            //decimal JameFasl = context.RizMetreStars.Include(x => x.ItemFBStar)
            //    .Where(x => x.ItemFBStar.BaravordId == BarAvordUserId)
            //    .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);


            //clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == BarAvordUserId);
            //clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes.FirstOrDefault(x => x.BaravordId == BarAvordUserId);

            //decimal zaribManteghe = baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //decimal zaribBalaSari = baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

            ////محاسبه ضرایب
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = BarAvordUserId,
                NoeFBId = NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
            decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

            ///
            ///

            decimal BahayeVahedCurrent = 0;
            decimal JameFaslInBahayeVahed = 0;

            decimal JameFaslBaZaribInBahayeVahed = 0;
            List<clsItemFBStar> lstItemFBStarCurrent = _context.ItemFBStars.Where(x => x.BaravordId == BarAvordUserId && x.NoeFBId == NoeFBId && x.Shomareh.Substring(0, 2) == FBShomareh.Substring(0, 2)).ToList();
            if (lstItemFBStarCurrent.Count != 0)
            {
                foreach (var itemFBStarCurrent in lstItemFBStarCurrent)
                {
                    decimal JameFasl = context.RizMetreStars
                        .Where(x => x.ItemFBStarId == itemFBStarCurrent.ID)
                        .Sum(x => x.MeghdarJoz != null ? x.MeghdarJoz.Value : 0);

                    BahayeVahedCurrent = itemFBStarCurrent.BahayeVahed;

                    bool blnKharidTajhizat = itemFBStarCurrent.blnKharidTajhizat == null ? false : itemFBStarCurrent.blnKharidTajhizat.Value;

                    decimal ZaribStar2 = zaribBalaSari;
                    if (blnKharidTajhizat)
                    {
                        ZaribStar2 = 1.14m;
                    }
                    JameFaslBaZaribInBahayeVahed += JameFasl * BahayeVahedCurrent * zaribManteghe * ZaribStar2;
                    JameFaslInBahayeVahed += JameFasl * BahayeVahedCurrent;
                }
            }
            return new JsonResult("OK_" + SumMeghdarJoz + "_" + JameFaslInBahayeVahed + "_" + JameFaslBaZaribInBahayeVahed);

            //return new JsonResult("OK");

        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }

}


