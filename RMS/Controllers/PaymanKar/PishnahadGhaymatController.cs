using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Controllers.PaymanKar.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using static Stimulsoft.Report.Export.StiBidirectionalConvert;
using static Stimulsoft.Report.StiRecentConnections;

namespace RMS.Controllers.PaymanKar;

public class PishnahadGhaymatController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewPishnahadGhaymatForm");

        return PartialView();
    }

    [HttpPost]
    public async Task<JsonResult> SetMablaghPishnahadi([FromBody] RequestSetMablaghPishnahadiDto request)
    {
        Guid ItemId = request.ItemId;
        decimal txtValue = request.txtValue;

        if (txtValue != 0)
        {
            //ZaribPishnahadiPaymanKarDto? zaribPishnahadiPaymanKarFosoul =
            //    await _context.ZaribPishnahadiPaymanKarFosouls.Include(x => x.ZaribPishnahadiPaymanKar).Where(x => x.ZaribPishnahadiPaymanKar.ID == request.ItemId).Select(x => new ZaribPishnahadiPaymanKarDto
            //    {
            //        ID = x.ZaribPishnahadiPaymanKar.ID,
            //        Code = x.Code,
            //        Title = x.Title,
            //        BaravordFasl = x.BaravordFasl,
            //        BaravordFaslBaZarib = x.BaravordFaslBaZarib,
            //        PishnahadPaymanKar = x.PishnahadPaymanKar,
            //        ZaribJoz = x.ZaribJoz
            //    }).FirstOrDefaultAsync();

            decimal Zarib = 0;
            clsZaribPishnahadiPaymanKarFosoul? zaribPishnahadiPaymanKarFosoul = await _context.ZaribPishnahadiPaymanKarFosouls.FirstOrDefaultAsync(x => x.ID == request.ItemId);
            if (zaribPishnahadiPaymanKarFosoul != null)
            {
                Zarib = txtValue / zaribPishnahadiPaymanKarFosoul.BaravordFaslBaZarib;

                _context.Entry(zaribPishnahadiPaymanKarFosoul).CurrentValues.SetValues(new
                {
                    PishnahadPaymanKar = txtValue,
                    ZaribJoz = Zarib
                });

                await _context.SaveChangesAsync();


            }
            return new JsonResult("OK_" + Zarib);
        }
        else
            return new JsonResult("NOK");
    }

    [HttpPost]
    public async Task<JsonResult> SetMablaghPishnahadiTajhiz([FromBody] RequestSetMablaghPishnahadiTajhizDto request)
    {
        Guid CompanyId = request.CompanyId;
        Guid BarAvordUserId = request.BarAvordUserId;
        decimal txtValue = request.txtValue;

        if (txtValue != 0)
        {

            List<ZaribPishnahadiPaymanKarDto> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKarFosouls
                .Include(x => x.ZaribPishnahadiPaymanKar).Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == BarAvordUserId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId && x.Code == "99")
                .Select(x => new ZaribPishnahadiPaymanKarDto
                {
                    ID = x.ZaribPishnahadiPaymanKar.ID,
                    BaravordFasl = x.BaravordFasl,
                    BaravordFaslBaZarib = x.BaravordFaslBaZarib,
                    Code = x.Code,
                    PishnahadPaymanKar = x.PishnahadPaymanKar,
                    Title = x.Title,
                    ZaribJoz = x.ZaribJoz
                }).ToList();

            decimal SumBaravordFaslTajhiz = zaribPishnahadiPaymanKars.Sum(x => x.BaravordFasl);
            decimal SumBaravordFaslBaZaribTajhiz = zaribPishnahadiPaymanKars.Sum(x => x.BaravordFaslBaZarib);


            clsZaribPishnahadiPaymanKar? zaribPishnahadiPaymanKar =
                await _context.ZaribPishnahadiPaymanKars.Where(x => x.BarAvordUserId == request.BarAvordUserId && x.CompanyId == CompanyId).FirstOrDefaultAsync();

            decimal Zarib = 0;
            if (zaribPishnahadiPaymanKar != null)
            {
                if (SumBaravordFaslBaZaribTajhiz != 0)
                {
                    Zarib = txtValue / SumBaravordFaslBaZaribTajhiz;
                    _context.Entry(zaribPishnahadiPaymanKar).CurrentValues.SetValues(new
                    {
                        PishnahadPaymanKarTajhiz = txtValue,
                        ZaribJozTajhiz = Zarib
                    });

                    await _context.SaveChangesAsync();
                }
                else
                    return new JsonResult("NOK");
            }
            else
                return new JsonResult("NOK");

            return new JsonResult("OK_" + Zarib);
        }
        else
            return new JsonResult("NOK");

    }

    [HttpPost]
    public async Task<JsonResult> GetFosoulPishnahadGhaymat([FromBody] GetFosoulInputDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        Guid CompanyId = request.CompanyId;
        NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        List<ZaribPishnahadiPaymanKarDto> zaribPishnahadiPaymanKars =
        _context.ZaribPishnahadiPaymanKarFosouls
        .Include(x => x.ZaribPishnahadiPaymanKar)
        .Where(x =>
            x.ZaribPishnahadiPaymanKar.BarAvordUserId == BarAvordUserId &&
            x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId &&
            x.NoeFBId == NoeFBId &&
            x.Code != "99")
        .OrderBy(x => x.Code)
        .AsEnumerable()
        .Select((x, index) => new ZaribPishnahadiPaymanKarDto
        {
            Radif = index + 1,
            ID = x.ID,
            BaravordFasl = x.BaravordFasl,
            BaravordFaslBaZarib = x.BaravordFaslBaZarib,
            Code = x.Code,
            PishnahadPaymanKar = x.PishnahadPaymanKar,
            Title = x.Title,
            ZaribJoz = x.ZaribJoz
        }).ToList();

        return new JsonResult(zaribPishnahadiPaymanKars);
    }



    [HttpPost]
    public async Task<JsonResult> GetSumAllFBPishnahadGhaymat([FromBody] GetSumAllFBPishnahadGhaymatDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        Guid CompanyId = request.CompanyId;
        //NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        List<ResultSumAllFBPishnahadGhaymatDto> resultSumAllFBPishnahadGhaymats = new List<ResultSumAllFBPishnahadGhaymatDto>();
        clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (baravordUser != null)
        {
            string[] NoeFBs = baravordUser.NoeFBs.Split(',');
            foreach (var NoeFB in NoeFBs)
            {
                string FBTitle = "";

                NoeFehrestBaha NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB);
                switch (NoeFBId)
                {
                    case NoeFehrestBaha.RahDari:
                        {
                            FBTitle = "راهداری";
                            break;
                        }

                    case NoeFehrestBaha.RahoBand:
                        {
                            FBTitle = "راه،راه‌آهن و باند فرودگاه";
                            break;
                        }
                    default:
                        break;
                }


                //List<clsZaribPishnahadiPaymanKar> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKars.Where(x => x.BarAvordUserId == BarAvordUserId && x.NoeFBId == NoeFBId).ToList();

                List<ZaribPishnahadiPaymanKarDto> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKarFosouls
                    .Include(x => x.ZaribPishnahadiPaymanKar).Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == BarAvordUserId && x.NoeFBId == NoeFBId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId && x.Code != "99")
                    .Select(x => new ZaribPishnahadiPaymanKarDto
                    {
                        ID = x.ZaribPishnahadiPaymanKar.ID,
                        BaravordFasl = x.BaravordFasl,
                        BaravordFaslBaZarib = x.BaravordFaslBaZarib,
                        Code = x.Code,
                        PishnahadPaymanKar = x.PishnahadPaymanKar,
                        Title = x.Title,
                        ZaribJoz = x.ZaribJoz
                    }).ToList();


                decimal SumBaravordFaslWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.BaravordFasl);
                decimal SumBaravordFaslBaZaribWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.BaravordFaslBaZarib);
                decimal? SumPishnahadPaymanKarWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.PishnahadPaymanKar);
                decimal? SumZaribJozWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.ZaribJoz);

                if (SumBaravordFaslBaZaribWithoutTajhiz != 0)
                {
                    ResultSumAllFBPishnahadGhaymatDto SumAllFBPishnahadGhaymat = new ResultSumAllFBPishnahadGhaymatDto
                    {
                        NoeFB = NoeFBId,
                        FBTitle = FBTitle,
                        //SumBaravordFasl,
                        //SumBaravordFaslBaZarib,
                        //SumPishnahadPaymanKar,
                        //SumZaribJoz = SumPishnahadPaymanKar / SumBaravordFaslBaZarib,
                        SumBaravordFaslWithoutTajhiz = SumBaravordFaslWithoutTajhiz,
                        SumBaravordFaslBaZaribWithoutTajhiz = SumBaravordFaslBaZaribWithoutTajhiz,
                        SumPishnahadPaymanKarWithoutTajhiz = SumPishnahadPaymanKarWithoutTajhiz,
                        SumZaribJozWithoutTajhiz = SumPishnahadPaymanKarWithoutTajhiz / SumBaravordFaslBaZaribWithoutTajhiz
                    };

                    resultSumAllFBPishnahadGhaymats.Add(SumAllFBPishnahadGhaymat);
                }
            }
        }

        return new JsonResult(resultSumAllFBPishnahadGhaymats);

    }


    [HttpPost]
    public async Task<JsonResult> GetSumAllWithTajhiz([FromBody] GetSumAllFBPishnahadGhaymatDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        Guid CompanyId = request.CompanyId;
        //NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        ResultSumAllFBWithTajhizDto resultSumAllWithTajhiz = new ResultSumAllFBWithTajhizDto();
        clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (baravordUser != null)
        {
            List<ZaribPishnahadiPaymanKarDto> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKarFosouls
                .Include(x => x.ZaribPishnahadiPaymanKar).Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == BarAvordUserId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId && x.Code != "99")
                .Select(x => new ZaribPishnahadiPaymanKarDto
                {
                    ID = x.ZaribPishnahadiPaymanKar.ID,
                    BaravordFasl = x.BaravordFasl,
                    BaravordFaslBaZarib = x.BaravordFaslBaZarib,
                    Code = x.Code,
                    PishnahadPaymanKar = x.PishnahadPaymanKar,
                    Title = x.Title,
                    ZaribJoz = x.ZaribJoz
                }).ToList();



            decimal SumBaravordFaslWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.BaravordFasl);
            decimal SumBaravordFaslBaZaribWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.BaravordFaslBaZarib);
            decimal? SumPishnahadPaymanKarWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.PishnahadPaymanKar);
            //decimal? SumZaribJozWithoutTajhiz = zaribPishnahadiPaymanKars.Where(x => x.Code != "99").Sum(x => x.ZaribJoz);
            //decimal? SumZaribJozWithoutTajhiz = SumPishnahadPaymanKarWithoutTajhiz / SumBaravordFaslBaZaribWithoutTajhiz;

            //ResultSumAllFBPishnahadGhaymatDto SumAllFBPishnahadGhaymat = new ResultSumAllFBPishnahadGhaymatDto
            //{
            //    //NoeFB = NoeFBId,
            //    //SumBaravordFasl,
            //    //SumBaravordFaslBaZarib,
            //    //SumPishnahadPaymanKar,
            //    //SumZaribJoz = SumPishnahadPaymanKar / SumBaravordFaslBaZarib,
            //    SumBaravordFaslWithoutTajhiz = SumBaravordFaslWithoutTajhiz,
            //    SumBaravordFaslBaZaribWithoutTajhiz = SumBaravordFaslBaZaribWithoutTajhiz,
            //    SumPishnahadPaymanKarWithoutTajhiz = SumPishnahadPaymanKarWithoutTajhiz,
            //    SumZaribJozWithoutTajhiz = SumPishnahadPaymanKarWithoutTajhiz / SumBaravordFaslBaZaribWithoutTajhiz
            //};

            ResultSumTajhizKargahDto ResultSumTajhizKargah = await fnGetSumTajhizKargah(request);

            decimal SumBaravordFaslTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.BaravordFasl);
            decimal SumBaravordFaslBaZaribTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.BaravordFaslBaZarib);
            decimal? SumPishnahadPaymanKarTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.PishnahadPaymanKar);
            decimal? SumZaribJozTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.ZaribJoz);

            decimal dSumAll = SumBaravordFaslBaZaribTajhiz + SumBaravordFaslBaZaribWithoutTajhiz;
            resultSumAllWithTajhiz.SumBaravordFaslTajhiz = SumBaravordFaslTajhiz + SumBaravordFaslWithoutTajhiz;

            resultSumAllWithTajhiz.SumBaravordFaslBaZaribTajhiz = dSumAll; //SumBaravordFaslBaZaribTajhiz + SumBaravordFaslBaZaribWithoutTajhiz;

            //decimal SumAll= SumBaravordFaslWithoutTajhiz+ TajhizKargah.

            decimal? dPishnahadPaymanKarTajhiz = 0;
            if (ResultSumTajhizKargah.zaribPishnahadiPaymanKar != null)
            {
                dPishnahadPaymanKarTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKar.PishnahadPaymanKarTajhiz;
            }

            decimal? dSumAllPishnahadPaymanKar = SumPishnahadPaymanKarWithoutTajhiz + (dPishnahadPaymanKarTajhiz == null ? 0 : dPishnahadPaymanKarTajhiz.Value);

            if (dSumAllPishnahadPaymanKar != null)
            {
                resultSumAllWithTajhiz.SumPishnahadPaymanKarTajhiz = dSumAllPishnahadPaymanKar;
                resultSumAllWithTajhiz.SumZaribJozTajhiz = dSumAll == 0 ? 0 : dSumAllPishnahadPaymanKar / dSumAll;
            }


            //resultSumAllFBPishnahadGhaymats.Add(SumAllFBPishnahadGhaymat);
            return new JsonResult(resultSumAllWithTajhiz);
        }
        else
            return new JsonResult(null);
    }

    public async Task<ResultSumTajhizKargahDto> fnGetSumTajhizKargah(GetSumAllFBPishnahadGhaymatDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        Guid CompanyId = request.CompanyId;
        //NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        ResultSumTajhizKargahDto resultSumTajhizKargah = new ResultSumTajhizKargahDto();

        clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (baravordUser != null)
        {

            //List<clsZaribPishnahadiPaymanKar> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKars.Where(x => x.BarAvordUserId == BarAvordUserId).ToList();

            List<ZaribPishnahadiPaymanKarDto> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKarFosouls
                .Include(x => x.ZaribPishnahadiPaymanKar).Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == BarAvordUserId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId && x.Code == "99")
                .Select(x => new ZaribPishnahadiPaymanKarDto
                {
                    ID = x.ZaribPishnahadiPaymanKar.ID,
                    BaravordFasl = x.BaravordFasl,
                    BaravordFaslBaZarib = x.BaravordFaslBaZarib,
                    Code = x.Code,
                    PishnahadPaymanKar = x.PishnahadPaymanKar,
                    Title = x.Title,
                    ZaribJoz = x.ZaribJoz
                }).ToList();

            resultSumTajhizKargah.zaribPishnahadiPaymanKars = zaribPishnahadiPaymanKars;

            clsZaribPishnahadiPaymanKar? zaribPishnahadiPaymanKar = _context.ZaribPishnahadiPaymanKars.FirstOrDefault(x => x.BarAvordUserId == BarAvordUserId && x.CompanyId == CompanyId);

            resultSumTajhizKargah.zaribPishnahadiPaymanKar = zaribPishnahadiPaymanKar;
            return resultSumTajhizKargah;
        }
        else
        {
            return new ResultSumTajhizKargahDto();
        }
    }

    [HttpPost]
    public async Task<JsonResult> GetSumTajhizKargah([FromBody] GetSumAllFBPishnahadGhaymatDto request)
    {
        Guid BarAvordUserId = request.BarAvordUserId;
        Guid CompanyId = request.CompanyId;
        //NoeFehrestBaha NoeFBId = request.NoeFBId;
        int Year = request.Year;

        clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordUserId);
        if (baravordUser != null)
        {

            //List<clsZaribPishnahadiPaymanKar> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKars.Where(x => x.BarAvordUserId == BarAvordUserId).ToList();

            ResultSumTajhizKargahDto ResultSumTajhizKargah = await fnGetSumTajhizKargah(request);

            //List<ZaribPishnahadiPaymanKarDto> zaribPishnahadiPaymanKars = _context.ZaribPishnahadiPaymanKarFosouls
            //    .Include(x => x.ZaribPishnahadiPaymanKar).Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == BarAvordUserId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId && x.Code == "99")
            //    .Select(x => new ZaribPishnahadiPaymanKarDto
            //    {
            //        ID = x.ZaribPishnahadiPaymanKar.ID,
            //        BaravordFasl = x.BaravordFasl,
            //        BaravordFaslBaZarib = x.BaravordFaslBaZarib,
            //        Code = x.Code,
            //        PishnahadPaymanKar = x.PishnahadPaymanKar,
            //        Title = x.Title,
            //        ZaribJoz = x.ZaribJoz
            //    }).ToList();

            decimal SumBaravordFaslTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.BaravordFasl);
            decimal SumBaravordFaslBaZaribTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.BaravordFaslBaZarib);
            decimal? SumPishnahadPaymanKarTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.PishnahadPaymanKar);
            decimal? SumZaribJozTajhiz = ResultSumTajhizKargah.zaribPishnahadiPaymanKars.Sum(x => x.ZaribJoz);

            var FBTajhiz = new
            {
                SumBaravordFaslTajhiz,
                SumBaravordFaslBaZaribTajhiz,
                SumPishnahadPaymanKarTajhiz,
                SumZaribJozTajhiz
            };

            clsZaribPishnahadiPaymanKar? zaribPishnahadiPaymanKar = ResultSumTajhizKargah.zaribPishnahadiPaymanKar;

            if (zaribPishnahadiPaymanKar != null)
            {
                var PishnahadTajhiz = new
                {
                    zaribPishnahadiPaymanKar.ZaribJozTajhiz,
                    zaribPishnahadiPaymanKar.PishnahadPaymanKarTajhiz
                };

                var result = new
                {
                    FBTajhiz = FBTajhiz,
                    PishnahadTajhiz = PishnahadTajhiz
                };

                return new JsonResult(result);
            }
            else
                return new JsonResult("NOK");

        }
        else
            return new JsonResult("NOK");
    }

    [HttpPost]
    public async Task<JsonResult> GetZarayebForPaymanKar([FromBody] GetZarayebForPaymanKarDto request)
    {
        Guid BarAvordId = request.BarAvordId;
        //NoeFehrestBaha NoeFBId = request.NoeFBId;
        //int Year = request.Year;

        clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BarAvordId);
        if (baravordUser != null)
        {
            clsBaravordUser? baravordUserParent = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == baravordUser.ParentId);

            if (baravordUserParent != null)
            {

                string[] NoeFBIds = baravordUserParent.NoeFBs.Split(",");
                decimal zaribBalaSari = 0;
                decimal zaribManteghe = 0;

                List<ResultGetZarayebForPaymanKarDto> lstGetZarayebForPaymanKar = new List<ResultGetZarayebForPaymanKarDto>();
                foreach (var NoeFB in NoeFBIds)
                {
                    NoeFehrestBaha NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB);
                    RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
                    {
                        BarAvordId = baravordUserParent.ID,
                        NoeFBId = NoeFBId,
                        Year = baravordUserParent.Year
                    };

                    string strNoeFBTitle = "";

                    switch (NoeFBId)
                    {
                        case NoeFehrestBaha.RahDari:
                            {
                                strNoeFBTitle = "راهداری";
                                break;
                            }

                        case NoeFehrestBaha.RahoBand:
                            {
                                strNoeFBTitle = "راه، راه‌آهن، باند فرودگاه";
                                break;
                            }

                        default:
                            break;
                    }

                    ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);

                    zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
                    zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

                    lstGetZarayebForPaymanKar.Add(new ResultGetZarayebForPaymanKarDto
                    {
                        Title = strNoeFBTitle,
                        ZaribBalaSari = zaribBalaSari,
                        ZaribManteghe = zaribManteghe,
                    });
                }

                //clsBaravordZaribBalaSari? baravordZaribBalaSari = await _context.BaravordZaribBalaSaris.FirstOrDefaultAsync(x => x.BaravordId == baravordUser.ParentId);
                //if (baravordZaribBalaSari != null)
                //{
                //    int intTarh = baravordZaribBalaSari.Tarh;
                //    int intMonaghese = baravordZaribBalaSari.Monaghese;
                //}

                //clsBaravordZaribManteghe? baravordZaribManteghe = await _context.BaravordZaribManteghes.FirstOrDefaultAsync(x => x.BaravordId == baravordUser.ParentId);
                //if (baravordZaribManteghe!=null)
                //{
                //    long OstanId = baravordZaribManteghe.OstanId;
                //    long ShahrId = baravordZaribManteghe.ShahrId;
                //    long BakhshId = baravordZaribManteghe.BakhshId;
                //}


                return new JsonResult(lstGetZarayebForPaymanKar);
            }
            else
                return new JsonResult(null);
        }
        else
            return new JsonResult(null);
    }
}
