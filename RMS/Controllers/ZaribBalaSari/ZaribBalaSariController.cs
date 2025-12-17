using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.ZaribBalaSari.Dto;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.ZaribBalaSari;

public class ZaribBalaSariController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public JsonResult GetZaribBalaSari([FromBody] GetZaribBalaSariDto request)
    {
        Guid BaravordId = request.BaravordId;
        int Year = request.Year;

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

            clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == request.BaravordId);
            if (baravordZaribBalaSari != null)
            {
                _context.Entry(baravordZaribBalaSari)
                    .CurrentValues.SetValues(new
                    {
                        Tarh = request.planSelected,
                        Monaghese = request.tenderSelected,
                    });
            }
            else
            {
                clsBaravordZaribBalaSari baravordZaribBalaSariNew = new clsBaravordZaribBalaSari
                {
                    BaravordId = request.BaravordId,
                    Tarh = request.planSelected,
                    Monaghese = request.tenderSelected,
                };
                _context.BaravordZaribBalaSaris.Add(baravordZaribBalaSariNew);
            }
            _context.SaveChanges();

            List<clsZaribBalaSari> lstZaribBalaSari = _context.ZaribBalaSaris.Where(x => x.Noe1 == request.planSelected && x.Noe2 == request.tenderSelected
                                                                                            && x.Year == Year && lstNoeFBs.Contains(x.NoeFBId)).ToList();


            List<ResultGetZaribBalaSariDto> lstResultZaribBalaSari = new List<ResultGetZaribBalaSariDto>();
            if (lstZaribBalaSari.Count != 0)
            {
                foreach (var item in lstZaribBalaSari)
                {
                    ResultGetZaribBalaSariDto Zarayeb = new ResultGetZaribBalaSariDto();
                    string Zarib = item.Zarib != null ? item.Zarib.Value.ToString("n2") : "0";
                    NoeFehrestBaha NoeFBId = item.NoeFBId;
                    switch (NoeFBId)
                    {
                        case NoeFehrestBaha.RahDari:
                            {
                                Zarayeb.ZaribBalaSari = Zarib;
                                Zarayeb.FBName = "راهداری";
                                break;
                            }
                        case NoeFehrestBaha.Abnie:
                            {
                                Zarayeb.ZaribBalaSari = Zarib;
                                Zarayeb.FBName = "ابنیه";
                                break;
                            }
                        case NoeFehrestBaha.RahoBand:
                            {
                                Zarayeb.ZaribBalaSari = Zarib;
                                Zarayeb.FBName = "راه، باند و فرودگاه";
                                break;
                            }
                        case NoeFehrestBaha.Barghi:
                            {
                                Zarayeb.ZaribBalaSari = Zarib;
                                Zarayeb.FBName = "تاسیسات برقی";
                                break;
                            }
                        case NoeFehrestBaha.Mekaniki:
                            {
                                Zarayeb.ZaribBalaSari = Zarib;
                                Zarayeb.FBName = "تاسیسات مکانیکی";
                                break;
                            }
                        case NoeFehrestBaha.MareMat:
                            {
                                Zarayeb.ZaribBalaSari = Zarib;
                                Zarayeb.FBName = "مرمت بناهای تاریخی";
                                break;
                            }
                        default:
                            break;
                    }

                    lstResultZaribBalaSari.Add(Zarayeb);
                }

                return new JsonResult(lstResultZaribBalaSari);
            }
            else
                return new JsonResult("NOK");
        }
        else
            return new JsonResult("NOK");
    }

    [HttpPost]
    public JsonResult GetBaravordZaribBalasari([FromBody] GetBaravordZaribBalasariDto request)
    {
        Guid BaravordId = request.BaravordId;
        int Year = request.Year;

        clsBaravordZaribBalaSari? BaravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId == request.BaravordId);
        if (BaravordZaribBalaSari != null)
        {
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

                List<clsZaribBalaSari> lstZaribBalaSari = _context.ZaribBalaSaris.Where(x => x.Noe1 == BaravordZaribBalaSari.Tarh && x.Noe2 == BaravordZaribBalaSari.Monaghese
                                                                                                && x.Year == Year && lstNoeFBs.Contains(x.NoeFBId)).ToList();

                List<ResultGetZaribBalaSariDto> lstResultZaribBalaSari = new List<ResultGetZaribBalaSariDto>();
                if (lstZaribBalaSari.Count != 0)
                {
                    foreach (var item in lstZaribBalaSari)
                    {
                        ResultGetZaribBalaSariDto Zarayeb = new ResultGetZaribBalaSariDto();
                        string Zarib = item.Zarib != null ? item.Zarib.Value.ToString("n2") : "0";
                        NoeFehrestBaha NoeFBId = item.NoeFBId;
                        switch (NoeFBId)
                        {
                            case NoeFehrestBaha.RahDari:
                                {
                                    Zarayeb.ZaribBalaSari = Zarib;
                                    Zarayeb.FBName = "راهداری";
                                    break;
                                }
                            case NoeFehrestBaha.Abnie:
                                {
                                    Zarayeb.ZaribBalaSari = Zarib;
                                    Zarayeb.FBName = "ابنیه";
                                    break;
                                }
                            case NoeFehrestBaha.RahoBand:
                                {
                                    Zarayeb.ZaribBalaSari = Zarib;
                                    Zarayeb.FBName = "راه، باند و فرودگاه";
                                    break;
                                }
                            case NoeFehrestBaha.Barghi:
                                {
                                    Zarayeb.ZaribBalaSari = Zarib;
                                    Zarayeb.FBName = "تاسیسات برقی";
                                    break;
                                }
                            case NoeFehrestBaha.Mekaniki:
                                {
                                    Zarayeb.ZaribBalaSari = Zarib;
                                    Zarayeb.FBName = "تاسیسات مکانیکی";
                                    break;
                                }
                            case NoeFehrestBaha.MareMat:
                                {
                                    Zarayeb.ZaribBalaSari = Zarib;
                                    Zarayeb.FBName = "مرمت بناهای تاریخی";
                                    break;
                                }
                            default:
                                break;
                        }

                        lstResultZaribBalaSari.Add(Zarayeb);
                    }
                }

                var result = new
                {
                    BaravordZaribBalaSari,
                    lstResultZaribBalaSari
                };

                return new JsonResult(result);
            }
            else
                return new JsonResult(null);
        }
        else
            return new JsonResult(null);
    }
}
