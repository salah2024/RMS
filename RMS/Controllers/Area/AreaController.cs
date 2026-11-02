using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<JsonResult> GetBaravordZaribManteghe([FromBody] GetBaravordBakhshDto request)
    {
        Guid baravordId = request.BaravordId;

        clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == baravordId);

        if (baravordUser != null)
        {
            if (baravordUser.Type == NoeBarAvord.SooratVaziat)
            {
                baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == baravordUser.ParentId);
                baravordId = baravordUser != null ? baravordUser.ID : baravordId;
            }

            clsBaravordZaribManteghe? baravordZaribManteghe = await _context.BaravordZaribManteghes
            .Include(x => x.BaravordUser)
            .FirstOrDefaultAsync(x => x.BaravordId == baravordId);

            // استان‌ها همیشه لود شوند
            List<clsOstan> lstOstan = _context.Ostans.ToList();

            List<clsShahr> lstShahr = new List<clsShahr>();
            List<clsBakhsh> lstBakhsh = new List<clsBakhsh>();
            List<ResultGetZaribMantegheDto> lstZaribManteghe = new List<ResultGetZaribMantegheDto>();

            if (baravordZaribManteghe != null)
            {
                lstShahr = await _context.Shahrs
                    .Where(x => x.OstanId == baravordZaribManteghe.OstanId)
                    .ToListAsync();

                lstBakhsh = await _context.Bakhshs
                    .Where(x => x.ShahrId == baravordZaribManteghe.ShahrId)
                    .ToListAsync();

                clsBakhsh? Bakhsh = await _context.Bakhshs.FirstOrDefaultAsync(x => x.Id == baravordZaribManteghe.BakhshId);
                if (Bakhsh != null)
                {
                    //clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == baravordId);
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
                                        zaribManteghe.FBName = "راه، راه آهن و باند فرودگاه";
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
                                case NoeFehrestBaha.MareMat:
                                    {
                                        zaribManteghe.ZaribManteghe = Bakhsh.ZaribAbnie.ToString("n2");
                                        zaribManteghe.FBName = "مرمت بناهای تاریخی";
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
        else
            return new JsonResult(null);
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
        Guid BaravordId = request.BaravordId;
        clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == BaravordId);
        if (baravordUser != null)
        {
            if (baravordUser.Type == NoeBarAvord.SooratVaziat)
            {
                return new JsonResult(null);
            }
        }



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
                                zaribManteghe.FBName = "راه، راه‌آھن و باند فرودگاه";
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
                        case NoeFehrestBaha.MareMat:
                            {
                                zaribManteghe.ZaribManteghe = Bakhsh.ZaribMechanic.ToString("n2");
                                zaribManteghe.FBName = "مرمت بناهای تاریخی";
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
