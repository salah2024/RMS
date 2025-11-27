using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.ZaribBalaSari.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.ZaribBalaSari;

public class ZaribBalaSariController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public JsonResult GetZaribBalaSari([FromBody] GetZaribBalaSariDto request)
    {
        clsZaribBalaSari? zaribBalaSari = _context.ZaribBalaSaris.FirstOrDefault(x => x.Noe1 == request.planSelected && x.Noe2 == request.tenderSelected);
        if (zaribBalaSari != null)
        {
            string Zarib = zaribBalaSari.Zarib != null ? zaribBalaSari.Zarib.Value.ToString("n2") : "0";

                clsBaravordZaribBalaSari? baravordZaribBalaSari = _context.BaravordZaribBalaSaris.FirstOrDefault(x => x.BaravordId==request.BaravordId);
                if (baravordZaribBalaSari != null)
                {
                    _context.Entry(baravordZaribBalaSari).CurrentValues.SetValues(new
                    {
                        Tarh = request.planSelected,
                        Monaghese = request.tenderSelected,
                        ZaribBalasari = zaribBalaSari.Zarib
                    });
                }
                else
                {
                    clsBaravordZaribBalaSari baravordZaribBalaSariNew = new clsBaravordZaribBalaSari
                    {
                        BaravordId = request.BaravordId,
                        Tarh = request.planSelected,
                        Monaghese = request.tenderSelected,
                        ZaribBalasari = zaribBalaSari.Zarib
                    };
                    _context.BaravordZaribBalaSaris.Add(baravordZaribBalaSariNew);
                }
            
            _context.SaveChanges();

            return new JsonResult("OK_" + Zarib);
        }
        else
            return new JsonResult("NOK");
    }

    [HttpPost]
    public JsonResult GetBaravordZaribBalasari([FromBody] GetBaravordZaribBalasariDto request)
    {
        List<clsBaravordZaribBalaSari> lstBaravordZaribBalaSari = _context.BaravordZaribBalaSaris.Where(x => x.BaravordId == request.BaravordId).ToList();
        if (lstBaravordZaribBalaSari != null)
        {
            return new JsonResult(lstBaravordZaribBalaSari);
        }
        else
            return new JsonResult(null);
    }
}
