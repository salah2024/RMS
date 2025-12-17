using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.Area.Dto;
using RMS.Controllers.ZaribBalaSari.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Area;

public class AreaController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public JsonResult GetBaravordZaribManteghe([FromBody] GetBaravordBakhshDto request)
    {
        Guid baravordId = request.BaravordId;

        clsBaravordZaribManteghe? baravordZaribManteghe = _context.BaravordZaribManteghes
            .Include(x => x.BaravordUser)
            .FirstOrDefault(x => x.BaravordId == baravordId);

        // استان‌ها همیشه لود شوند
        List<clsOstan> lstOstan = _context.Ostans.ToList();

        List<clsShahr> lstShahr = new List<clsShahr>();
        List<clsBakhsh> lstBakhsh = new List<clsBakhsh>();
        List<ResultGetZaribMantegheDto> lstZaribManteghe = new List<ResultGetZaribMantegheDto>();

        if (baravordZaribManteghe != null)
        {
            lstShahr = _context.Shahrs
                .Where(x => x.OstanId == baravordZaribManteghe.OstanId)
                .ToList();

            lstBakhsh = _context.Bakhshs
                .Where(x => x.ShahrId == baravordZaribManteghe.ShahrId)
                .ToList();

            clsBakhsh? Bakhsh = _context.Bakhshs.FirstOrDefault(x => x.Id == baravordZaribManteghe.BakhshId);
            if (Bakhsh != null)
            {
                clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == baravordId);
                if (baravordUser != null)
                {
                    string[] NoeFBIds = baravordUser.NoeFBs.Split(',');
                    List<NoeFehrestBaha> lstNoeFBs = new List<NoeFehrestBaha>();
                    foreach (var item in NoeFBIds)
                    {
                        if (item != "")
                        {
                            lstNoeFBs.Add((NoeFehrestBaha)int.Parse(item));
                        }
                    }
                    foreach (var item in lstNoeFBs)
                    {
                        ResultGetZaribMantegheDto zaribManteghe = new ResultGetZaribMantegheDto();
                        switch (item)
                        {
                            case NoeFehrestBaha.RahDari:
                                {
                                    zaribManteghe.ZaribManteghe = Bakhsh.ZaribRahDari.ToString("n2");
                                    zaribManteghe.FBName = "راهداری";
                                    break;
                                }
                            case NoeFehrestBaha.Abnie:
                                {
                                    zaribManteghe.ZaribManteghe = Bakhsh.ZaribAbnie.ToString("n2");
                                    zaribManteghe.FBName = "ابنیه";
                                    break;
                                }
                            case NoeFehrestBaha.RahoBand:
                                {
                                    zaribManteghe.ZaribManteghe = Bakhsh.ZaribRah.ToString("n2");
                                    zaribManteghe.FBName = "راه، باند و فرودگاه";
                                    break;
                                }
                            case NoeFehrestBaha.Barghi:
                                {
                                    zaribManteghe.ZaribManteghe = Bakhsh.ZaribBargh.ToString("n2");
                                    zaribManteghe.FBName = "تاسیسات برقی";
                                    break;
                                }
                            case NoeFehrestBaha.Mekaniki:
                                {
                                    zaribManteghe.ZaribManteghe = Bakhsh.ZaribMechanic.ToString("n2");
                                    zaribManteghe.FBName = "تاسیسات مکانیکی";
                                    break;
                                }
                            //case NoeFehrestBaha.MareMat:
                            //    {
                            //        zaribManteghe.ZaribManteghe = Bakhsh.Zari.ToString("n0");
                            //        zaribManteghe.FBName = "تاسیسات مکانیکی";
                            //        break;
                            //    }
                            default:
                                break;
                        }
                        lstZaribManteghe.Add(zaribManteghe);
                    }
                }
            }
        }

        var result = new
        {
            lstOstan,
            lstShahr,
            lstBakhsh,
            baravordZaribManteghe,
            lstZaribManteghe
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

        var Bakhsh = _context.Bakhshs.Include(x => x.Shahr).Where(x => x.Id == BakhshId).Select(x => new
        {
            x.ZaribRahDari,
            x.ZaribRah,
            x.ZaribChah,
            x.ZaribToziAb,
            x.ZaribAbnie,
            x.ZaribMechanic,
            x.ZaribSad,
            x.ZaribAbiariVaZeh,
            x.ZaribAbKhizDari,
            x.ZaribAbRostaii,
            x.ZaribBargh,
            x.ZaribEnteghalAb,
            x.ZaribEnteghalFazelAb,
            x.ZaribGhanat,
            x.ZaribTahteFeshar,
            OstanId = x.Shahr.OstanId,
            ShahrId = x.ShahrId,
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
                };
                _context.BaravordZaribManteghes.Add(baravordZaribMantegheNew);
            }

            _context.SaveChanges();

            //clsBakhsh? currentBakhsh = _context.Bakhshs.FirstOrDefault(x => x.Id == BakhshId);
            clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == BaravordId);
            if (baravordUser != null)
            {
                string[] NoeFBs = baravordUser.NoeFBs.Split(",");

                List<NoeFehrestBaha> lstNoeFBs = new List<NoeFehrestBaha>();
                foreach (var item in NoeFBs)
                {
                    if (item != "")
                    {
                        lstNoeFBs.Add((NoeFehrestBaha)int.Parse(item));
                    }
                }
                List<ResultGetZaribMantegheDto> lstZaribManteghe = new List<ResultGetZaribMantegheDto>();
                foreach (var item in lstNoeFBs)
                {
                    ResultGetZaribMantegheDto zaribManteghe = new ResultGetZaribMantegheDto();
                    switch (item)
                    {
                        case NoeFehrestBaha.RahDari:
                            {
                                zaribManteghe.ZaribManteghe = Bakhsh.ZaribRahDari.ToString("n2");
                                zaribManteghe.FBName = "راهداری";
                                break;
                            }
                        case NoeFehrestBaha.Abnie:
                            {
                                zaribManteghe.ZaribManteghe = Bakhsh.ZaribAbnie.ToString("n2");
                                zaribManteghe.FBName = "ابنیه";
                                break;
                            }
                        case NoeFehrestBaha.RahoBand:
                            {
                                zaribManteghe.ZaribManteghe = Bakhsh.ZaribRah.ToString("n2");
                                zaribManteghe.FBName = "راه، باند و فرودگاه";
                                break;
                            }
                        case NoeFehrestBaha.Barghi:
                            {
                                zaribManteghe.ZaribManteghe = Bakhsh.ZaribBargh.ToString("n2");
                                zaribManteghe.FBName = "تاسیسات برقی";
                                break;
                            }
                        case NoeFehrestBaha.Mekaniki:
                            {
                                zaribManteghe.ZaribManteghe = Bakhsh.ZaribMechanic.ToString("n2");
                                zaribManteghe.FBName = "تاسیسات مکانیکی";
                                break;
                            }
                        //case NoeFehrestBaha.MareMat:
                        //    {
                        //        zaribManteghe.ZaribManteghe = Bakhsh.Zari.ToString("n0");
                        //        zaribManteghe.FBName = "تاسیسات مکانیکی";
                        //        break;
                        //    }
                        default:
                            break;
                    }
                    lstZaribManteghe.Add(zaribManteghe);
                }


                return new JsonResult(lstZaribManteghe);
            }
            else
                return new JsonResult(null);
        }
        else
            return new JsonResult(null);
    }
}
