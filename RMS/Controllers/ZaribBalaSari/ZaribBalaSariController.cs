using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.Board.Dto;
using RMS.Controllers.ZaribBalaSari.Dto;
using RMS.Models.Entity;

namespace RMS.Controllers.ZaribBalaSari;

public class ZaribBalaSariController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public JsonResult GetZaribBalaSari([FromBody] GetZaribBalaSariDto request)
    {
        clsZaribBalaSari? zaribBalaSari= _context.ZaribBalaSaris.FirstOrDefault(x => x.Noe1 == request.planSelected && x.Noe2 == request.tenderSelected);
        if (zaribBalaSari != null)
        {
            string Zarib = zaribBalaSari.Zarib != null ? zaribBalaSari.Zarib.Value.ToString("n2") : "0";
            return new JsonResult("OK_" + Zarib);
        }
        else
            return new JsonResult("NOK");
    }
}
