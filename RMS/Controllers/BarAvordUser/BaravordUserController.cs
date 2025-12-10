using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Models.Common;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.BarAvordUser;

public class BaravordUserController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        return View();
    }

    // GET: BaravordUser
    [HttpPost]
    public JsonResult List()
    {

        var barAvord = (from BU in _context.BaravordUsers
                        where (BU.IsDeleted == false || BU.IsDeleted == null)
                        select new BaravordUserToShowDto
                        {
                            BUId = BU.ID,
                            BUNum = BU.Num,
                            BUName = BU.Name,
                            BUInsertDate = BU.InsertDateTime,
                            BUYear = BU.Year,
                            BUNoeFBs = BU.NoeFBs,
                            //NoeFBName = BU.NoeFB == NoeFehrestBaha.RahoBand ? "راه و باند"
                            //          : BU.NoeFB == NoeFehrestBaha.RahDari ? "راهداری"
                            //          : BU.NoeFB == NoeFehrestBaha.Abnie ? "ابنیه" : "",

                        }).OrderBy(x => x.BUNum).ToList();

        foreach (var item in barAvord)
        {
            string[] NoeFBs = item.BUNoeFBs.Split(",");

            string strNoeFB = "";
            foreach (string noeFB in NoeFBs)
            {
                switch (noeFB)
                {
                    case "232":
                        {
                            strNoeFB += "راهداری - ";
                            break;
                        }
                    case "233":
                        {
                            strNoeFB += "ابنیه - ";
                            break;
                        }
                    case "234":
                        {
                            strNoeFB += "راه، باند، فرودگاه - ";
                            break;
                        }
                    default:
                        break;
                }
            }

            item.NoeFBName = strNoeFB;
            if (item.BUInsertDate != null)
            {
                item.BUInsertDateSolar = SolarDate.ConvertMiladiToPersion(item.BUInsertDate.Value);
            }
        }

        return new JsonResult(barAvord);
    }

    [HttpPost]
    public ActionResult RemoveStarValueInFB([FromBody] RemoveStarValueInFBInputDto request)
    {

        clsFB? currentFb = _context.FBs.FirstOrDefault(x => x.ID == request.FBId);
        if (currentFb != null)
        {
            currentFb.BahayeVahedNew = 0;
            _context.SaveChanges();
            string strBahayeVahed = "";
            string currentitemFbShomareh = request.itemFbShomareh.Substring(0, 6);
            clsFehrestBaha? fehrestBaha = _context.FehrestBahas.FirstOrDefault(x => x.Shomareh.Trim() == currentitemFbShomareh && x.Sal == request.Year);
            if (fehrestBaha != null)
            {
                if (currentFb.BahayeVahedZarib != 0)
                {
                    strBahayeVahed = (currentFb.BahayeVahedZarib * decimal.Parse(fehrestBaha.BahayeVahed == null ? "0" : fehrestBaha.BahayeVahed)).ToString();
                }
                else
                    strBahayeVahed = fehrestBaha.BahayeVahed;
            }
            return new JsonResult("OK_" + strBahayeVahed);
        }
        else
            return new JsonResult("NOK");
    }

    [HttpPost]
    public ActionResult ConfirmBahayeVahedNew([FromBody] BahayeVahedNewInputDto request)
    {
        try
        {

            long BahayeVahedNew = long.Parse(request.BahayeVahedNew);
            Guid tempId = new Guid();
            Guid FBId = request.FBId;
            NoeFehrestBaha NoeFBId = request.NoeFBId;
            if (FBId == tempId)
            {
                FBId = Guid.NewGuid();
                clsFB fb = new clsFB
                {
                    BarAvordId = request.BarAvordUserId,
                    BahayeVahedNew = BahayeVahedNew,
                    ID = FBId,
                    NoeFBId = NoeFBId,
                    InsertDateTime = DateTime.Now,
                    Shomareh = request.itemFbShomareh,
                };
                _context.FBs.Add(fb);
            }
            else
            {
                clsFB? currentFB = _context.FBs.FirstOrDefault(x => x.ID == request.FBId);
                if (currentFB != null)
                {
                    currentFB.BahayeVahedNew = BahayeVahedNew;
                }
            }

            _context.SaveChanges();
            return new JsonResult("OK");

        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }

    // GET: BaravordUser/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }


    // GET: BaravordUser/Create
    [HttpPost]
    public JsonResult CreateNewBU([FromBody] CreateNewBUInputDto request)
    {
        NoeBarAvord Type = request.Type;
        string Title = request.Title;
        string NoeFBs = request.NoeFBs;
        int Year = request.Year;
        string UserName = request.UserName;

        clsBaravordUser baravordUser = new clsBaravordUser();
        ApplicationUser? User = _context.Users.Where(x => x.UserName == UserName).FirstOrDefault();
        if (User != null)
        {
            Guid guidUserId = Guid.Parse("b3500674-db49-44de-a006-45abad9fb1ed");
            clsBaravordUser? baravordUserItem = _context.BaravordUsers.Where(f => f.UserId == guidUserId
            && f.Type == NoeBarAvord.WithoutProject && f.Year == Year).OrderByDescending(f => f.Num).FirstOrDefault();
            if (baravordUserItem != null)
                baravordUser.Num = baravordUserItem.Num + 1;
            else
                baravordUser.Num = 1;
            baravordUser.Name = Title;
            baravordUser.Year = Year;
            baravordUser.NoeFBs = NoeFBs;
            baravordUser.UserId = guidUserId;
            baravordUser.Type = Type;
            baravordUser.InsertDateTime = DateTime.Now;
            //baravordUser.Date = SolarDate.CurrentDate;
            //baravordUser.time = SolarDate.CurrentTime;
            _context.BaravordUsers.Add(baravordUser);
            _context.SaveChanges();
            return new JsonResult("OK_" + baravordUser.ID);
        }
        else
            return new JsonResult("NOK_");
    }

    // POST: BaravordUser/Create
    [HttpPost]
    public ActionResult Create(clsBaravordUser barAvordUser)
    {
        try
        {
            // TODO: Add insert logic here
            barAvordUser.UserId = Guid.Parse("b3500674-db49-44de-a006-45abad9fb1ed");
            //barAvordUser._Date = SolarDate.CurrentDate;
            //barAvordUser._time = SolarDate.CurrentTime;
            _context.BaravordUsers.Add(barAvordUser);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    [HttpPost]
    public long GetLastNum([FromBody] GetLastNumInputDto request)
    {
        Guid guidUserId = Guid.Parse("b3500674-db49-44de-a006-45abad9fb1ed");
        clsBaravordUser? baravordUserItem = _context.BaravordUsers.Where(f => f.UserId == guidUserId
        && f.Type == NoeBarAvord.WithProject && f.Year == request.Year).OrderByDescending(f => f.Num).FirstOrDefault();
        long intNum = 0;
        if (baravordUserItem == null)
            intNum = 1;
        else
            intNum = baravordUserItem.Num + 1;
        return intNum;
    }

    // GET: BaravordUser/Edit/5
    public ActionResult Edit(Guid id)
    {
        clsBaravordUser? barAvordUser = _context.BaravordUsers.Where(x => x.ID == id).FirstOrDefault();
        return View(barAvordUser);
    }

    // POST: BaravordUser/Edit/5
    [HttpPost]
    public ActionResult Edit(clsBaravordUser barAvordUser)
    {
        try
        {
            // TODO: Add update logic here
            clsBaravordUser? tblbarAvordUser = _context.BaravordUsers.Where(x => x.ID == barAvordUser.ID).FirstOrDefault();
            if (tblbarAvordUser != null)
            {
                barAvordUser.UserId = Guid.Parse("b3500674-db49-44de-a006-45abad9fb1ed");
                //barAvordUser._Date = tblbarAvordUser._Date;
                //barAvordUser._time = tblbarAvordUser._time;
                _context.Entry(tblbarAvordUser).CurrentValues.SetValues(barAvordUser);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(barAvordUser);
        }
        catch (Exception)
        {
            return View();
        }
    }

    [HttpPost]
    public JsonResult DeleteBarAvordUser([FromBody] DeleteBarAvordUserDto request)
    {

        clsBaravordUser? barAvordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == request.Id);
        if (barAvordUser != null)
        {
            _context.BaravordUsers.Entry(barAvordUser).CurrentValues.SetValues(new
            {
                IsDeleted = true
            });

            //_context.BaravordUsers.Remove(barAvordUser);
            _context.SaveChanges();
        }
        return new JsonResult("OK");
        //}
        //catch (Exception)
        //{
        //    return new JsonResult("NOK");
        //}
    }

    // POST: BaravordUser/Delete/5
    [HttpPost]
    public ActionResult Delete(Guid id, clsBaravordUser baravordUser)
    {
        try
        {
            // TODO: Add delete logic here
            clsBaravordUser? barAvordUser = _context.BaravordUsers.Where(x => x.ID == id).FirstOrDefault();
            if (barAvordUser != null)
            {
                _context.BaravordUsers.Remove(barAvordUser);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
        catch
        {
            return View();
        }
    }

}
