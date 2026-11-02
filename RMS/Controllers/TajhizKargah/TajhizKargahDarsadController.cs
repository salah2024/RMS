using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.TajhizKargah.Dto;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.TajhizKargah;

public class TajhizKargahDarsadController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public async Task<JsonResult> TajhizKargahDarsadSave([FromBody] TajhizKargahDarsadDto request)
    {
        Guid BarAvordId = request.BarAvordUserId;
        NoeFehrestBaha NoeFBId = request.NoeFB;
        int Year = request.Year;

        List<clsRizMetreUsers> lstRMTajhiz= await _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.NoeFBId == NoeFBId && x.FB.BarAvordId == BarAvordId && x.FB.Shomareh.Substring(0, 2) == "99").ToListAsync();
        _context.RizMetreUserses.RemoveRange(lstRMTajhiz);


        clsTajhizKargahDarsad? TajhizKargahDarsad = await _context.TajhizKargahDarsads.FirstOrDefaultAsync(x => x.BarAvordUserId == request.BarAvordUserId && x.NoeFB == request.NoeFB);
        if (TajhizKargahDarsad != null)
        {
            _context.TajhizKargahDarsads.Entry(TajhizKargahDarsad).CurrentValues.SetValues(new
            {
                Darsad = request.Darsad,
                Meghdar = request.Meghdar,
            });
        }
        else
        {
            clsTajhizKargahDarsad tajhizKargahDarsad = new clsTajhizKargahDarsad
            {
                BarAvordUserId = request.BarAvordUserId,
                Darsad = request.Darsad,
                Meghdar = request.Meghdar,
                NoeFB = request.NoeFB,
            };
            _context.TajhizKargahDarsads.Add(tajhizKargahDarsad);
        }
        await _context.SaveChangesAsync();


        ////محاسبه ضرایب
        RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
        {
            BarAvordId = BarAvordId,
            NoeFBId = NoeFBId,
            Year = Year
        };
        ResultGetZarayebDto result1 = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
        decimal zaribBalaSari = result1.zaribBalaSari != null ? result1.zaribBalaSari.Value : 1;
        decimal zaribManteghe = result1.zaribManteghe != null ? result1.zaribManteghe.Value : 1;

        var result = new
        {
            zaribBalaSari,
            zaribManteghe,
        };

        return new JsonResult(result);
    }

    [HttpPost]
    public async Task<JsonResult> TajhizKargahDarsadDelete([FromBody] TajhizKargahDarsadForDeleteDto request)
    {
        try
        {
            Guid BarAvordId = request.BarAvordUserId;
            NoeFehrestBaha NoeFBId = request.NoeFB;
            int Year = request.Year;

            clsTajhizKargahDarsad? TajhizKargahDarsad = await _context.TajhizKargahDarsads.FirstOrDefaultAsync(x => x.BarAvordUserId == request.BarAvordUserId && x.NoeFB == request.NoeFB);
            if (TajhizKargahDarsad != null)
            {
                _context.TajhizKargahDarsads.Remove(TajhizKargahDarsad);
            }

            await _context.SaveChangesAsync();

            return new JsonResult("OK");
        }
        catch (Exception)
        {
            return new JsonResult("NOK");

        }

    }


    [HttpPost]
    public JsonResult GetCurrentTajhizKargahDarsad([FromBody] RequestTajhizKargahDarsadDto request)
    {
        if (request != null)
        {
            Guid BarAvordId = request.BarAvordUserId;
            NoeFehrestBaha NoeFBId = request.NoeFB;
            int Year = request.Year;

            clsTajhizKargahDarsad? TajhizKargahDarsad = _context.TajhizKargahDarsads.FirstOrDefault(x => x.BarAvordUserId == request.BarAvordUserId && x.NoeFB == request.NoeFB);
            if (TajhizKargahDarsad != null)
            {

                ////محاسبه ضرایب
                RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
                {
                    BarAvordId = BarAvordId,
                    NoeFBId = NoeFBId,
                    Year = Year
                };
                ResultGetZarayebDto result1 = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
                decimal zaribBalaSari = result1.zaribBalaSari != null ? result1.zaribBalaSari.Value : 1;
                decimal zaribManteghe = result1.zaribManteghe != null ? result1.zaribManteghe.Value : 1;

                var Zarayeb = new
                {
                    zaribBalaSari,
                    zaribManteghe,
                };

                var result = new
                {
                    Zarayeb,
                    TajhizKargahDarsad
                };

                return new JsonResult(result);
            }
            else
                return new JsonResult("NOK");
        }
        else
            return new JsonResult("NOK");
    }
}
