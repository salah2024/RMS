using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.Controllers.BarAvordUser.Common;
using RMS.Controllers.BarAvordUser.Dto;
using RMS.Controllers.Operation.Dto;
using RMS.Interfaces.Workflow;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;
using static Stimulsoft.Report.StiRecentConnections;

namespace RMS.Controllers.BarAvordUser;

public class BaravordUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWorkflowRuntimeService workflowRuntimeService) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IWorkflowRuntimeService _workflowRuntimeService = workflowRuntimeService;

    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        return View();
    }

    // GET: BaravordUser
    [HttpPost]
    public async Task<JsonResult> List()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            Guid UserId = Guid.Parse(user?.Id);

            var barAvord = await (from BU in _context.BaravordUsers
                                  where ((BU.IsDeleted == false || BU.IsDeleted == null)
                                            && BU.Type != NoeBarAvord.SooratVaziat && BU.UserId == UserId
                                            ||
                                            _context.BaravordUserAccesses.Any(a =>
                                             a.BaravordUserId == BU.ID
                                                &&
                                             a.UserId == user.Id
                                                &&
                                             (a.IsDeleted == false || a.IsDeleted == null)))

                                  select new BaravordUserToShowDto
                                  {
                                      BUId = BU.ID,
                                      BUNum = BU.Num,
                                      BUName = BU.Name,
                                      BUInsertDate = BU.InsertDateTime,
                                      BUYear = BU.Year,
                                      BUNoeFBs = BU.NoeFBs,
                                      BUType = BU.Type,
                                      IsOwner = BU.UserId == UserId,
                                      //IsCompleted = BU.IsCompleted,
                                      CanOpen = true,
                                      hasWorkflow = _context.WorkflowInstances.Any(a => a.EntityId == BU.ID),
                                      CanApprove =
                                      _context.WorkflowAssignments.Any(a =>
                                            a.WorkflowInstance.EntityId == BU.ID
                                            &&
                                            a.UserId == user.Id
                                            &&
                                            !a.IsCompleted),

                                      CanReturn =
                                      _context.WorkflowAssignments.Any(a =>
                                            a.WorkflowInstance.EntityId == BU.ID
                                            &&
                                            a.UserId == user.Id
                                            &&
                                            !a.IsCompleted),

                                      WorkflowStatusTitle =
                                        _context.WorkflowInstances
                                        .Where(w => w.EntityId == BU.ID)
                                        .OrderByDescending(w => w.InsertDateTime)
                                        .Select(w =>
                                         w.Status == WorkflowInstanceStatus.Completed
                                            ? "تایید نهایی شده"
                                        : w.Status == WorkflowInstanceStatus.Rejected
                                            ? "رد شده"
                                        : w.Status == WorkflowInstanceStatus.Cancelled
                                            ? "لغو شده"
                                        : w.CurrentStep.Title).FirstOrDefault(),

                                  }).OrderByDescending(x => x.BUNum).OrderByDescending(x => x.BUInsertDate).ToListAsync();

            foreach (var item in barAvord)
            {
                var workflow = await _context.WorkflowInstances
                    .Include(x => x.CurrentStep)
                    .Where(x => x.EntityId == item.BUId)
                    .OrderByDescending(x => x.InsertDateTime)
                    .FirstOrDefaultAsync();

                if (workflow == null)
                {
                    item.WorkflowStatusTitle = "";
                }
                else if (workflow.Status == WorkflowInstanceStatus.Completed)
                {
                    item.WorkflowStatusTitle = "تایید نهایی شده";
                }
                else if (workflow.Status == WorkflowInstanceStatus.Rejected)
                {
                    item.WorkflowStatusTitle = "رد شده";
                }
                else if (workflow.Status == WorkflowInstanceStatus.Cancelled)
                {
                    item.WorkflowStatusTitle = "لغو شده";
                }
                else
                {
                    item.WorkflowStatusTitle = $"{workflow.CurrentStep.Title}";
                }


                string[] NoeFBs = item.BUNoeFBs.Split(",");

                string strNoeFB = "";
                foreach (string noeFB in NoeFBs)
                {
                    NoeFehrestBaha NoeFB = (NoeFehrestBaha)int.Parse(noeFB);
                    switch (NoeFB)
                    {
                        case NoeFehrestBaha.RahDari:
                            {
                                strNoeFB += "راهداری - ";
                                break;
                            }
                        case NoeFehrestBaha.Abnie:
                            {
                                strNoeFB += "ابنیه - ";
                                break;
                            }
                        case NoeFehrestBaha.RahoBand:
                            {
                                strNoeFB += "راه، راه‌آهن و باند، فرودگاه - ";
                                break;
                            }
                        case NoeFehrestBaha.Barghi:
                            {
                                strNoeFB += "تاسیسات برقی - ";
                                break;
                            }
                        case NoeFehrestBaha.Mekaniki:
                            {
                                strNoeFB += "تاسیسات مکانیکی - ";
                                break;
                            }
                        case NoeFehrestBaha.MareMat:
                            {
                                strNoeFB += "مرمت بناهای تاریخی - ";
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
        else
            return new JsonResult(null);
    }


    [HttpPost]
    public async Task<JsonResult> PishnahadGhyamatWorkFlow(Guid entityId, List<string>? selectedUsers = null)
    {
        //--------------------------------------------------
        // Workflow
        //--------------------------------------------------
        string userId = _userManager.GetUserId(User)!;

        ApplicationUser? currentUser =
            await _userManager.FindByIdAsync(userId);

        if (currentUser == null)
            return new JsonResult("NOK_");

        Guid currentUserId = Guid.Parse(currentUser.Id);



        clsWorkflow? workflow =
            await _context.Workflows
            .FirstOrDefaultAsync(x =>
                x.proccesType ==
                ProccesType.PishnahadGhaimat);

        if (workflow != null)
        {

            clsBarAvordProjectionWorkFlow barAvordProjectionWorkFlow = new clsBarAvordProjectionWorkFlow
            {
                BarAvordUserId = entityId,
                proccesType = ProccesType.PishnahadGhaimat
            };
            _context.BarAvordProjectionWorkFlows.Add(barAvordProjectionWorkFlow);


            await _workflowRuntimeService.StartWorkflowAsync(
                workflow.ID,
                entityId,
                nameof(clsBaravordUser),
                currentUserId,
                selectedUsers);
        }

        return Json(new
        {
            success = true
        });
    }

    [HttpPost]
    public async Task<JsonResult> ListSooratVaziat([FromBody] ListSooratVaziatDto request)
    {
        Guid CompanyId = request.CompanyId;
        Guid BaravordId = request.BaravordUserId;
        long Year = request.Year;

        var barAvord = await (from BU in _context.BaravordUsers
                              where ((BU.IsDeleted == false || BU.IsDeleted == null) && BU.ParentId == BaravordId && BU.Type == NoeBarAvord.SooratVaziat)
                              select new SVToShowDto
                              {
                                  BUId = BU.ID,
                                  BUNum = BU.Num,
                                  BUName = BU.Name,
                                  BUInsertDate = BU.InsertDateTime,
                                  BUYear = BU.Year,
                                  BUNoeFBs = BU.NoeFBs,
                                  BUType = BU.Type,
                                  FromDate = BU.FromDate,
                                  ToDate = BU.ToDate,
                              }).OrderBy(x => x.BUNum).ToListAsync();

        List<ResultCalculateBarAvordDto> lstCalBA = new List<ResultCalculateBarAvordDto>();

        foreach (var item in barAvord)
        {
            string[] NoeFBs = item.BUNoeFBs.Split(",");

            string strNoeFB = "";
            foreach (string noeFB in NoeFBs)
            {
                NoeFehrestBaha NoeFB = (NoeFehrestBaha)int.Parse(noeFB);
                switch (NoeFB)
                {
                    case NoeFehrestBaha.RahDari:
                        {
                            strNoeFB += "راهداری - ";
                            break;
                        }
                    case NoeFehrestBaha.Abnie:
                        {
                            strNoeFB += "ابنیه - ";
                            break;
                        }
                    case NoeFehrestBaha.RahoBand:
                        {
                            strNoeFB += "راه، راه‌آهن و باند فرودگاه - ";
                            break;
                        }
                    case NoeFehrestBaha.Barghi:
                        {
                            strNoeFB += "تاسیسات برقی - ";
                            break;
                        }
                    case NoeFehrestBaha.Mekaniki:
                        {
                            strNoeFB += "تاسیسات مکانیکی - ";
                            break;
                        }
                    case NoeFehrestBaha.MareMat:
                        {
                            strNoeFB += "مرمت بناهای تاریخی - ";
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

            CalculateBarAvordDto calculateBarAvordAll = new CalculateBarAvordDto
            {
                BarAvordId = item.BUId,
                Year = item.BUYear
            };
            ResultCalculateBarAvordDto culateBarAvordAll = await RizMetreCommon.fnCalculateBarAvord(calculateBarAvordAll, _context);

            item.CalculateBarAvord = culateBarAvordAll;
        }

        string PGhDate = "";
        clsZaribPishnahadiPaymanKar? zaribPishnahadiPaymanKar = _context.ZaribPishnahadiPaymanKars.FirstOrDefault(x => x.BarAvordUserId == BaravordId && x.CompanyId == CompanyId && x.Year == Year);
        if (zaribPishnahadiPaymanKar != null)
        {
            PGhDate = SolarDate.ConvertMiladiToPersion(zaribPishnahadiPaymanKar.InsertDateTime ?? new DateTime());
        }

        var result = new
        {
            PGhDate,
            barAvord
        };

        return new JsonResult(result);
    }

    [HttpPost]
    public async Task<ActionResult> CalculateBarAvordAll([FromBody] CalculateBarAvordDto request)
    {
        CalculateBarAvordDto calculateBarAvordAll = new CalculateBarAvordDto
        {
            BarAvordId = request.BarAvordId,
            Year = request.Year
        };
        ResultCalculateBarAvordDto Result = await RizMetreCommon.fnCalculateBarAvord(calculateBarAvordAll, _context);
        return new JsonResult(Result);
    }

    [HttpPost]
    public async Task<ActionResult> CalculateFosoulAll([FromBody] CalculateFosoulDto request)
    {
        CalculateFosoulDto calculateBarAvordAll = new CalculateFosoulDto
        {
            BarAvordId = request.BarAvordId,
            NoeFBId = request.NoeFBId,
            Year = request.Year
        };
        ResultCalculateBarAvordDto Result = await RizMetreCommon.fnCalculateFosoulAll(calculateBarAvordAll, _context);

        Guid userId = Guid.Parse(_userManager.GetUserId(User));
        if (Result.Check.Check)
        {
            clsUserNotification? currentUserNotification = await _context.UserNotifications.FirstOrDefaultAsync(x => x.UserId == userId && x.BarAvordId == request.BarAvordId && x.notifyType == NotifyType.ErrorStar);
            string strMsg = Result.Check.Des + " - " + "جمع مبلغ برآورد ردیف‌های ستاره‌دار، نسبت به جمع مبلغ برآورد ردیف‌های فهرست‌بها (پایه و غیرپایه) بدون اعمال هزینه تجهیز و برچیدن کارگاه، در این رشته، بیشتر از  حد اقلام ستاره دار (درصد) طبق جدول  الف  و بند 2-6 دستورالعمل کاربرد شده است لذا، لازم است دستگاه اجرایی قبل از انجام مناقصه، شرح و بهای واحد تمامی ردیف‌های ستاره‌دار در آن رشته را، پس از تصویب، همراه با تجزیه قیمت مربوط، به دبیرخانه شورای عالی فنی در سازمان برنامه و بودجه کشور ارسال دارد تا پس از رسیدگی و تصویب توسط شورای عالی فنی، (بر اساس دستورالعمل نحوه تهیه و تصویب ردیف‌های ستاره‌دار) ملاک عمل قرار گیرد.";
            string strShortMsg = Result.Check.Des;

            if (currentUserNotification != null)
            {
                _context.Entry(currentUserNotification).CurrentValues.SetValues(new
                {
                    ShortDes = strShortMsg,
                    Description = strMsg
                });
            }
            else
            {

                clsUserNotification userNotification = new clsUserNotification
                {
                    BarAvordId = request.BarAvordId,
                    UserId = userId,
                    Title = "مقدار غیرمجاز اقلام ستاره دار",
                    ShortDes = strShortMsg,
                    Description = strMsg,
                    notifyType = NotifyType.ErrorStar,
                    UserInserter = userId,
                    InsertDateTime = DateTime.Now,
                    IsRead = false,
                    RelateLink = ""
                };
                _context.UserNotifications.Add(userNotification);
            }
        }
        else
        {
            clsUserNotification? userNotification = await _context.UserNotifications.FirstOrDefaultAsync(x => x.UserId == userId && x.BarAvordId == request.BarAvordId && x.notifyType == NotifyType.ErrorStar);

            if (userNotification != null)
            {
                _context.UserNotifications.Remove(userNotification);
            }
        }
        await _context.SaveChangesAsync();
        return new JsonResult(Result);
    }

    [HttpPost]
    public async Task<ActionResult> RemoveStarValueInFB([FromBody] RemoveStarValueInFBInputDto request)
    {

        Guid FBId = new Guid();
        int Year = request.Year;
        Guid BarAvordId = request.BarAvordId;
        NoeFehrestBaha NoeFB = request.NoeFB;
        clsFB? currentFb = new clsFB();
        if (request.FBId == "")
        {
            currentFb = await _context.FBs.FirstOrDefaultAsync(x => x.NoeFBId == NoeFB && x.BarAvordId == BarAvordId);
            if (currentFb != null)
            {
                FBId = currentFb.ID;
            }

        }
        else
        {
            FBId = Guid.Parse(request.FBId);
            currentFb = await _context.FBs.FirstOrDefaultAsync(x => x.ID == FBId);
        }

        if (currentFb != null)
        {
            BarAvordId = currentFb.BarAvordId;
            NoeFehrestBaha NoeFBId = currentFb.NoeFBId;
            string FBShomareh = currentFb.Shomareh;
            string Code = FBShomareh.Substring(0, 2);

            currentFb.BahayeVahedNew = 0;
            await _context.SaveChangesAsync();
            string strBahayeVahed = "";
            string currentitemFbShomareh = request.itemFbShomareh.Substring(0, 6);

            //clsFehrestBaha? fehrestBaha = await _context.FehrestBahas.FirstOrDefaultAsync(x => x.Shomareh.Trim() == currentitemFbShomareh && x.Sal == Year && x.NoeFB==NoeFB);
            //if (fehrestBaha != null)
            //{
            //    if (currentFb.BahayeVahedZarib != 0)
            //    {
            //        strBahayeVahed = (currentFb.BahayeVahedZarib * decimal.Parse(fehrestBaha.BahayeVahed == null ? "0" : fehrestBaha.BahayeVahed)).ToString();
            //    }
            //    else
            //        strBahayeVahed = fehrestBaha.BahayeVahed;
            //}



            clsBahayeVahedItemsRelated? BVItemsRelated = _context.BahayeVahedItemsRelateds.FirstOrDefault(x => x.ItemShomarehRelated.ToLower().Trim() == FBShomareh && x.NoeFB == NoeFBId && x.Year == Year);

            //clsBahayeVahedItemsRelated? BVItemsRelated = lstBVItemRelated.FirstOrDefault(x => x.ItemShomarehRelated.Trim() == item.ItemFbShomareh.Trim());
            clsFehrestBaha? fehrestBaha = await _context.FehrestBahas.FirstOrDefaultAsync(x => x.Shomareh.Trim() == currentitemFbShomareh && x.Sal == Year && x.NoeFB == NoeFB);
            if (fehrestBaha != null)
            {
                if (BVItemsRelated != null)
                {
                    long BahayeVahed1 = fehrestBaha.BahayeVahed == null ? 0 : long.Parse(fehrestBaha.BahayeVahed);
                    strBahayeVahed = (BVItemsRelated.Value * BahayeVahed1).ToString("G0");
                }
                else
                    strBahayeVahed = fehrestBaha.BahayeVahed == null ? "0" : fehrestBaha.BahayeVahed;
            }


            CalculateFaslDto CalculateFasl = new CalculateFaslDto
            {

                BarAvordId = BarAvordId,
                Code = Code,
                FBId = FBId,
                NoeFBId = NoeFBId,
                Year = Year
            };
            ResultCalculateFaslDto result = await RizMetreCommon.fnCalculateFasl(CalculateFasl, _context);

            //await _context.SaveChangesAsync();
            return new JsonResult("OK_" + strBahayeVahed + "_" + result.SumMeghdarJoz + "_" + result.JameFasl + "_" + result.JameFaslInZarib);

            //return new JsonResult("OK_" + strBahayeVahed);
        }
        else
            return new JsonResult("NOK");
    }

    [HttpPost]
    public async Task<ActionResult> ConfirmBahayeVahedNew([FromBody] BahayeVahedNewInputDto request)
    {
        try
        {
            Guid BarAvordId = request.BarAvordUserId;
            int Year = request.Year;
            string Shomareh = request.itemFbShomareh.Trim();
            string Code = Shomareh.Substring(0, 2);
            Guid FBId = request.FBId;
            NoeFehrestBaha NoeFBId = request.NoeFBId;

            long BahayeVahedNew = long.Parse(request.BahayeVahedNew);
            Guid tempId = new Guid();

            if (FBId == new Guid())
            {
                List<clsFehrestBahaItemsReplace> lstfehrestBahaItemsReplace = await _context.FehrestBahaItemsReplaces.Where(x => x.FBShomarehNew == Shomareh && x.NoeFB == NoeFBId && x.Year == Year).ToListAsync();

                clsFehrestBahaItemsReplace? fehrestBahaItemsReplace = lstfehrestBahaItemsReplace.FirstOrDefault(x => x.FBShomarehNew == Shomareh && x.NoeFB == NoeFBId && x.Year == Year);
                if (fehrestBahaItemsReplace != null)
                {
                    Shomareh = fehrestBahaItemsReplace.FBShomarehMain;
                }

                clsFB? currentFB = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.Shomareh.Trim() == Shomareh.Trim());
                if (currentFB != null)
                {
                    FBId = currentFB.ID;
                    currentFB.BahayeVahedNew = BahayeVahedNew;
                }
                else
                {
                    FBId = Guid.NewGuid();
                    clsFB fb = new clsFB
                    {
                        BarAvordId = BarAvordId,
                        BahayeVahedNew = BahayeVahedNew,
                        ID = FBId,
                        NoeFBId = NoeFBId,
                        InsertDateTime = DateTime.Now,
                        Shomareh = Shomareh,
                    };
                    _context.FBs.Add(fb);
                }

            }
            else
            {
                if (FBId == tempId)
                {
                    FBId = Guid.NewGuid();
                    clsFB fb = new clsFB
                    {
                        BarAvordId = BarAvordId,
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
                    clsFB? currentFB = await _context.FBs.FirstOrDefaultAsync(x => x.ID == FBId);
                    if (currentFB != null)
                    {
                        currentFB.BahayeVahedNew = BahayeVahedNew;
                    }
                }
            }
            await _context.SaveChangesAsync();

            //////////
            ////////////
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = BarAvordId,
                NoeFBId = NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);

            decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

            ///
            ///
            //baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

            List<clsFehrestBahaItemsReplace> lstFBItemReplaces = _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToList();

            var lstFehrestBahas = await (from FehrestBaha in _context.FehrestBahas
                                         join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                                         on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
                                         from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
                                         where FehrestBaha.Sal == Year && FehrestBaha.NoeFB == NoeFBId
                                         select new
                                         {
                                             Id = FehrestBaha.Id,
                                             BahayeVahed = FehrestBaha.BahayeVahed,
                                             Vahed = FehrestBaha.Vahed,
                                             Sharh = FehrestBaha.Sharh,
                                             ShomarehMain = FehrestBaha.Shomareh,
                                             Shomareh = FehrestBahaItemsReplace != null && FehrestBahaItemsReplace.FBShomarehNew != null
                                                                       ? FehrestBahaItemsReplace.FBShomarehNew
                                                                       : FehrestBaha.Shomareh,
                                             Sal = FehrestBaha.Sal,
                                             NoeFB = FehrestBaha.NoeFB,
                                         }).ToListAsync();


            decimal JameFaslInZarib = 0;

            // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
            var fbItems = await _context.FBs
                .Where(f => f.BarAvordId == BarAvordId && f.NoeFBId == NoeFBId)
                .GroupBy(f => f.Shomareh)
                .Select(g => g.FirstOrDefault())
                .ToListAsync(); // انتقال به حافظه، جلوگیری از خطای EF

            foreach (var item in fbItems)
            {
                foreach (var fbItemReplace in lstFBItemReplaces)
                {
                    if (item.Shomareh == fbItemReplace.FBShomarehMain)
                    {
                        item.Shomareh = fbItemReplace.FBShomarehNew;
                    }
                }
            }

            // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
            var userBarAvordOutPut = lstFehrestBahas //_context.FehrestBahas
                .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId && fehrest.Shomareh.StartsWith(Code))
                .ToList() // انتقال به حافظه
                .GroupJoin(fbItems,
                    fehrest => fehrest.Shomareh,
                    fb => fb.Shomareh,
                    (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
                .OrderBy(x => x.fehrest.Shomareh)
                .Select(x => new ViewUserBarAvordOutPutDto
                {
                    ItemFbShomareh = x.fehrest.Shomareh,
                    Sharh = x.fehrest.Sharh,
                    BahayeVahed = x.fehrest.BahayeVahed,
                    Vahed = x.fehrest.Vahed,
                    FBId = x.fbItem != null ? x.fbItem.ID : null,
                    BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
                    RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                }).ToList();

            var RizMetreAll = await _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId && x.FB.NoeFBId == NoeFBId)
                .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
                {
                    Id = riz.ID,
                    Sharh = riz.Sharh,
                    Shomareh = riz.Shomareh,
                    Arz = riz.Arz,
                    Des = riz.Des,
                    Ertefa = riz.Ertefa,
                    Tedad = riz.Tedad,
                    Tool = riz.Tool,
                    Vazn = riz.Vazn,
                    FBId = riz.FBId,
                    MeghdarJoz = riz.MeghdarJoz
                }).OrderBy(x => x.Shomareh).ToListAsync();


            decimal JameFasl = 0;
            foreach (var item in userBarAvordOutPut)
            {
                decimal Meghdar = 0;
                foreach (var RM in RizMetreAll)
                {
                    if (item.FBId == RM.FBId)
                    {
                        item.RizMetre.Add(RM);
                        Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                    }
                }
                decimal dBVahed = item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                item.Meghdar = Meghdar;
                decimal dBahayeKol = Meghdar * dBVahed;
                item.BahayeKol = dBahayeKol;
                JameFasl += dBahayeKol;
                JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
            }

            return new JsonResult("OK_" + Code + "_" + JameFasl + "_" + JameFaslInZarib);

            //return new JsonResult("OK");

        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }


    [HttpPost]
    public async Task<ActionResult> ConfirmItemZarib([FromBody] ItemZaribInputDto request)
    {
        try
        {
            Guid BarAvordId = request.BarAvordUserId;
            int Year = request.Year;
            string Shomareh = request.itemFbShomareh.Trim();
            string Code = Shomareh.Substring(0, 2);
            Guid FBId = request.FBId;
            NoeFehrestBaha NoeFBId = request.NoeFBId;

            decimal ItemZarib = request.ItemZarib == null ? 1 : request.ItemZarib.Value;
            Guid tempId = new Guid();

            if (FBId == new Guid())
            {
                List<clsFehrestBahaItemsReplace> lstfehrestBahaItemsReplace = await _context.FehrestBahaItemsReplaces.Where(x => x.FBShomarehNew == Shomareh && x.NoeFB == NoeFBId && x.Year == Year).ToListAsync();

                clsFehrestBahaItemsReplace? fehrestBahaItemsReplace = lstfehrestBahaItemsReplace.FirstOrDefault(x => x.FBShomarehNew == Shomareh && x.NoeFB == NoeFBId && x.Year == Year);
                if (fehrestBahaItemsReplace != null)
                {
                    Shomareh = fehrestBahaItemsReplace.FBShomarehMain;
                }

                clsFB? currentFB = await _context.FBs.FirstOrDefaultAsync(x => x.BarAvordId == BarAvordId && x.NoeFBId == NoeFBId && x.Shomareh.Substring(0, 6).Trim() == Shomareh.Trim());
                if (currentFB != null)
                {
                    FBId = currentFB.ID;
                    currentFB.ItemZarib = ItemZarib;
                }
                else
                {
                    FBId = Guid.NewGuid();
                    clsFB fb = new clsFB
                    {
                        BarAvordId = BarAvordId,
                        ItemZarib = ItemZarib,
                        ID = FBId,
                        NoeFBId = NoeFBId,
                        InsertDateTime = DateTime.Now,
                        Shomareh = Shomareh,
                    };
                    _context.FBs.Add(fb);
                }

            }
            else
            {
                if (FBId == tempId)
                {
                    FBId = Guid.NewGuid();
                    clsFB fb = new clsFB
                    {
                        BarAvordId = BarAvordId,
                        ItemZarib = ItemZarib,
                        ID = FBId,
                        NoeFBId = NoeFBId,
                        InsertDateTime = DateTime.Now,
                        Shomareh = request.itemFbShomareh,
                    };
                    _context.FBs.Add(fb);
                }
                else
                {
                    clsFB? currentFB = await _context.FBs.FirstOrDefaultAsync(x => x.ID == FBId);
                    if (currentFB != null)
                    {
                        currentFB.ItemZarib = ItemZarib;
                    }
                }
            }
            await _context.SaveChangesAsync();

            //////////
            ////////////
            RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
            {
                BarAvordId = BarAvordId,
                NoeFBId = NoeFBId,
                Year = Year
            };
            ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);

            decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
            decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

            ///
            ///
            //baravordZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe != null ? baravordZaribManteghe.ZaribManteghe.Value : 1 : 1;
            //baravordZaribBalaSari != null ? baravordZaribBalaSari.ZaribBalasari != null ? baravordZaribBalaSari.ZaribBalasari.Value : 1 : 1;

            List<clsFehrestBahaItemsReplace> lstFBItemReplaces = _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year && x.NoeFB == NoeFBId).ToList();

            var lstFehrestBahas = await (from FehrestBaha in _context.FehrestBahas
                                         join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                                         on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
                                         from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
                                         where FehrestBaha.Sal == Year && FehrestBaha.NoeFB == NoeFBId
                                         select new
                                         {
                                             Id = FehrestBaha.Id,
                                             BahayeVahed = FehrestBaha.BahayeVahed,
                                             Vahed = FehrestBaha.Vahed,
                                             Sharh = FehrestBaha.Sharh,
                                             ShomarehMain = FehrestBaha.Shomareh,
                                             Shomareh = FehrestBahaItemsReplace != null && FehrestBahaItemsReplace.FBShomarehNew != null
                                                                       ? FehrestBahaItemsReplace.FBShomarehNew
                                                                       : FehrestBaha.Shomareh,
                                             Sal = FehrestBaha.Sal,
                                             NoeFB = FehrestBaha.NoeFB,
                                         }).ToListAsync();


            decimal JameFaslInZarib = 0;

            // اول FBs رو فیلتر و گروه‌بندی کن (در حافظه)
            var fbItems = await _context.FBs
                .Where(f => f.BarAvordId == BarAvordId && f.NoeFBId == NoeFBId)
                .GroupBy(f => f.Shomareh)
                .Select(g => g.FirstOrDefault())
                .ToListAsync(); // انتقال به حافظه، جلوگیری از خطای EF

            foreach (var item in fbItems)
            {
                foreach (var fbItemReplace in lstFBItemReplaces)
                {
                    if (item.Shomareh == fbItemReplace.FBShomarehMain)
                    {
                        item.Shomareh = fbItemReplace.FBShomarehNew;
                    }
                }
            }

            // سپس FehrestBahas رو از دیتابیس بگیر و join کن در حافظه
            var userBarAvordOutPut = lstFehrestBahas //_context.FehrestBahas
                .Where(fehrest => fehrest.Sal == Year && fehrest.NoeFB == NoeFBId && fehrest.Shomareh.StartsWith(Code))
                .ToList() // انتقال به حافظه
                .GroupJoin(fbItems,
                    fehrest => fehrest.Shomareh,
                    fb => fb.Shomareh,
                    (fehrest, fbGroup) => new { fehrest, fbItem = fbGroup.FirstOrDefault() })
                .OrderBy(x => x.fehrest.Shomareh)
                .Select(x => new ViewUserBarAvordOutPutDto
                {
                    ItemFbShomareh = x.fehrest.Shomareh,
                    Sharh = x.fehrest.Sharh,
                    BahayeVahed = x.fehrest.BahayeVahed,
                    Vahed = x.fehrest.Vahed,
                    FBId = x.fbItem != null ? x.fbItem.ID : null,
                    BahayeVahedNew = x.fbItem != null ? x.fbItem.BahayeVahedNew : 0,
                    RizMetre = new List<ViewUserBarAvordOutPutRizMetreDto>()
                }).ToList();

            var RizMetreAll = await _context.RizMetreUserses.Include(x => x.FB).Where(x => x.FB.BarAvordId == BarAvordId && x.FB.NoeFBId == NoeFBId)
                .Select(riz => new ViewUserBarAvordOutPutRizMetreDto
                {
                    Id = riz.ID,
                    Sharh = riz.Sharh,
                    Shomareh = riz.Shomareh,
                    Arz = riz.Arz,
                    Des = riz.Des,
                    Ertefa = riz.Ertefa,
                    Tedad = riz.Tedad,
                    Tool = riz.Tool,
                    Vazn = riz.Vazn,
                    FBId = riz.FBId,
                    MeghdarJoz = riz.MeghdarJoz
                }).OrderBy(x => x.Shomareh).ToListAsync();


            decimal JameFasl = 0;
            foreach (var item in userBarAvordOutPut)
            {
                decimal Meghdar = 0;
                foreach (var RM in RizMetreAll)
                {
                    if (item.FBId == RM.FBId)
                    {
                        item.RizMetre.Add(RM);
                        Meghdar += RM.MeghdarJoz != null ? RM.MeghdarJoz.Value : 0;
                    }
                }
                decimal dBVahed = item.BahayeVahedNew != 0 ? item.BahayeVahedNew : item.BahayeVahed == null || item.BahayeVahed == "" ? 0 : decimal.Parse(item.BahayeVahed);
                item.Meghdar = Meghdar;
                decimal dBahayeKol = Meghdar * dBVahed * ItemZarib;
                item.BahayeKol = dBahayeKol;
                JameFasl += dBahayeKol;
                JameFaslInZarib += dBahayeKol * zaribBalaSari * zaribManteghe;
            }

            return new JsonResult("OK_" + Code + "_" + JameFasl + "_" + JameFaslInZarib);

            //return new JsonResult("OK");

        }
        catch (Exception)
        {
            return new JsonResult("NOK");
        }
    }

    [HttpPost]
    public async Task<ActionResult> ConfirmItemZaribStar([FromBody] ItemZaribStarInputDto request)
    {
        try
        {

            Guid ItemStarId = request.ItemStarId;
            decimal ItemZarib = request.ItemZarib == null ? 1 : request.ItemZarib.Value;


            clsItemFBStar? itemFBStar = _context.ItemFBStars.FirstOrDefault(x => x.ID == ItemStarId);
            if (itemFBStar != null)
            {
                itemFBStar.ItemZaribStar = ItemZarib;
            }

            await _context.SaveChangesAsync();


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
    public async Task<JsonResult> CreateNewSooratVaziat([FromBody] CreateNewSooratVaziatDto request)
    {
        try
        {
            NoeBarAvord Type = request.Type;
            string Title = request.Title;
            int Year = request.Year;
            string UserName = request.UserName;
            Guid buId = request.buId;
            string? FromDate = request.FromDate;
            string? ToDate = request.ToDate;

            clsBaravordUser? baravordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == buId);
            if (baravordUser != null)
            {
                clsBaravordUser baravordUserNew = new clsBaravordUser();
                string userId = _userManager.GetUserId(User);

                var user = await _userManager.FindByIdAsync(userId);

                ApplicationUser? CurrentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
                if (User != null)
                {
                    Guid guidUserId = Guid.Parse(userId);

                    clsBaravordUser? baravordUserItem = await _context.BaravordUsers.Where(f => f.UserId == guidUserId
                        && f.Type == NoeBarAvord.SooratVaziat && f.Year == Year && f.ParentId == buId).OrderByDescending(f => f.Num).FirstOrDefaultAsync();

                    if (baravordUserItem != null)
                        baravordUserNew.Num = baravordUserItem.Num + 1;
                    else
                        baravordUserNew.Num = 1;

                    baravordUserNew.Name = Title;
                    baravordUserNew.Year = baravordUser.Year;
                    baravordUserNew.NoeFBs = baravordUser.NoeFBs;
                    baravordUserNew.UserId = guidUserId;
                    baravordUserNew.Type = Type;
                    baravordUserNew.InsertDateTime = DateTime.Now;
                    baravordUserNew.ParentId = buId;
                    baravordUserNew.FromDate = FromDate;
                    baravordUserNew.ToDate = ToDate;

                    _context.BaravordUsers.Add(baravordUserNew);
                    //ثبت آیتم هایی که بهای واحد آنها تغییر یافته است
                    //
                    List<clsFB> lstFBs = _context.FBs.Where(x => x.BarAvordId == buId && x.BahayeVahedNew != 0).ToList();

                    Guid BUSVId = baravordUserNew.ID;
                    foreach (var item in lstFBs)
                    {
                        clsFB FBSV = item;
                        FBSV.ID = Guid.NewGuid();
                        FBSV.BarAvordId = BUSVId;
                        FBSV.ItemZarib = null;
                        _context.FBs.Add(FBSV);

                    }

                    //ثبت آیتم هایی ستاره دار
                    //
                    List<clsItemFBStar> lstFBStars = _context.ItemFBStars.Where(x => x.BaravordId == buId).ToList();

                    foreach (var item in lstFBStars)
                    {
                        clsItemFBStar ItemFBStarSV = item;
                        ItemFBStarSV.ID = Guid.NewGuid();
                        ItemFBStarSV.BaravordId = BUSVId;
                        ItemFBStarSV.ItemZaribStar = null;
                        _context.ItemFBStars.Add(ItemFBStarSV);

                    }

                    ///ثبت ضرایب 
                    ///
                    clsBaravordZaribBalaSari? BA_ZBS = await _context.BaravordZaribBalaSaris.FirstOrDefaultAsync(x => x.BaravordId == buId);

                    if (BA_ZBS != null)
                    {
                        _context.BaravordZaribBalaSaris.Add(new clsBaravordZaribBalaSari
                        {
                            BaravordId = baravordUserNew.ID,
                            Monaghese = BA_ZBS.Monaghese,
                            Tarh = BA_ZBS.Tarh,
                        });
                    }

                    clsBaravordZaribManteghe? BA_ZM = await _context.BaravordZaribManteghes.FirstOrDefaultAsync(x => x.BaravordId == buId);
                    if (BA_ZM != null)
                    {
                        _context.BaravordZaribManteghes.Add(new clsBaravordZaribManteghe
                        {
                            BaravordId = baravordUserNew.ID,
                            OstanId = BA_ZM.OstanId,
                            ShahrId = BA_ZM.ShahrId,
                            BakhshId = BA_ZM.BakhshId
                        });
                    }

                    await _context.SaveChangesAsync();

                    return new JsonResult("OK_" + baravordUserNew.ID + "_" + (int)Type);
                }
                else
                    return new JsonResult("NOK_");
            }
            else
                return new JsonResult("NOK_");

        }
        catch (Exception e)
        {

            throw;
        }

    }


    [HttpPost]
    public async Task<JsonResult> CreateNewBU([FromBody] CreateNewBUInputDto request)
    {
        string userId = _userManager.GetUserId(User)!;

        ApplicationUser? currentUser =
            await _userManager.FindByIdAsync(userId);

        if (currentUser == null)
            return new JsonResult("NOK_");

        Guid currentUserId = Guid.Parse(currentUser.Id);

        using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            //--------------------------------------------------
            // شماره پروژه
            //--------------------------------------------------

            long lastNum =
                await _context.BaravordUsers
                .Where(x =>
                    x.UserId == currentUserId &&
                    x.Type == request.Type &&
                    x.Year == request.Year)
                .MaxAsync(x => (long?)x.Num) ?? 0;

            //--------------------------------------------------
            // ایجاد پروژه
            //--------------------------------------------------

            clsBaravordUser project =
                new clsBaravordUser
                {
                    ID = Guid.NewGuid(),

                    UserId = currentUserId,

                    Num = lastNum + 1,

                    Name = request.Title,

                    Type = request.Type,

                    Year = request.Year,

                    NoeFBs = request.NoeFBs,

                    InsertDateTime = DateTime.Now
                };

            _context.BaravordUsers.Add(project);

            //--------------------------------------------------
            // ضرایب
            //--------------------------------------------------

            _context.BaravordZaribBalaSaris.Add(
                new clsBaravordZaribBalaSari
                {
                    BaravordId = project.ID,
                    Monaghese = 3,
                    Tarh = 1
                });

            _context.BaravordZaribManteghes.Add(
                new clsBaravordZaribManteghe
                {
                    BaravordId = project.ID,
                    OstanId = 20,
                    ShahrId = 2001,
                    BakhshId = 1034
                });

            //--------------------------------------------------
            // مالک پروژه
            //--------------------------------------------------

            _context.BaravordUserAccesses.Add(
                new clsBaravordUserAccess
                {
                    ID = Guid.NewGuid(),

                    BaravordUserId = project.ID,

                    UserId = currentUser.Id,

                    AccessType = ProjectAccessType.Owner,

                    InsertDateTime = DateTime.Now
                });


            ApplicationUser? user = await _context.Users
            .Include(x => x.UserDetail)
            .ThenInclude(x => x.Company)
            .FirstOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));

            Guid? CompanyId = user?.UserDetail?.CompanyId;

            if (CompanyId != null)
            {
                CompanyType? companyType = user?.UserDetail?.Company?.CompanyType;
                if (companyType != null)
                {
                    switch (companyType)
                    {
                        case CompanyType.Moshaver:
                            {
                                clsBarAvordMoshaver barAvordMoshaver = new clsBarAvordMoshaver
                                {
                                    BarAvordUserId = project.ID,
                                    MoshaverId = CompanyId.Value,
                                };

                                _context.BarAvordMoshavers.Add(barAvordMoshaver);
                                break;
                            }

                        case CompanyType.PayManKar:
                            {
                                clsBarAvordPaymanKar barAvordPaymanKar = new clsBarAvordPaymanKar
                                {
                                    BarAvordUserId = project.ID,
                                    PayManKarId = CompanyId.Value,
                                };

                                _context.BarAvordPaymanKars.Add(barAvordPaymanKar);
                                break;
                            }

                        case CompanyType.KarFarma:
                            {
                                clsBarAvordKarfarma barAvordKarFarma = new clsBarAvordKarfarma
                                {
                                    BarAvordUserId = project.ID,
                                    KarFarmaId = CompanyId.Value,
                                };

                                _context.BarAvordKarfarmas.Add(barAvordKarFarma);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            //--------------------------------------------------
            // Workflow
            //--------------------------------------------------

            clsWorkflow? workflow =
                await _context.Workflows
                .FirstOrDefaultAsync(x =>
                    x.proccesType ==
                    ProccesType.BarAvordAvalieh);

            if (workflow != null)
            {
                bool blnAuto = workflow.Auto == null ? true : workflow.Auto.Value;
                ///در صورتی که گردش بصورت اتوماتیک باشد بعد از ایجاد برآورد به کاربر بعدی اتوماتیک ارسال میگردد
                if (blnAuto)
                {
                    await _workflowRuntimeService.StartWorkflowAsync(
                        workflow.ID,
                        project.ID,
                        nameof(clsBaravordUser),
                        currentUserId);
                }
                ///
                ///در صورتی که گردش بصورت دستی باشد بعد از ایجاد برآورد به کاربر بعدی بایستی از طریق دکمه ارسال گردد
                ///
            }


            //--------------------------------------------------
            // ذخیره نهایی
            //--------------------------------------------------

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new JsonResult(
                $"OK_{project.ID}_{(int)project.Type}");
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // GET: BaravordUser/Create
    //[HttpPost]
    //public async Task<JsonResult> CreateNewBU([FromBody] CreateNewBUInputDto request)
    //{
    //    NoeBarAvord Type = request.Type;
    //    string Title = request.Title;
    //    string NoeFBs = request.NoeFBs;
    //    int Year = request.Year;
    //    string UserName = request.UserName;

    //    string userId = _userManager.GetUserId(User);

    //    var user = await _userManager.FindByIdAsync(userId);

    //    clsBaravordUser baravordUser = new clsBaravordUser();
    //    ApplicationUser? CurrentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
    //    if (CurrentUser != null)
    //    {
    //        Guid guidUserId = Guid.Parse(userId);

    //        clsBaravordUser? baravordUserItem = await _context.BaravordUsers.Where(f => f.UserId == guidUserId
    //        && f.Type == NoeBarAvord.BarAvord && f.Year == Year).OrderByDescending(f => f.Num).FirstOrDefaultAsync();
    //        if (baravordUserItem != null)
    //            baravordUser.Num = baravordUserItem.Num + 1;
    //        else
    //            baravordUser.Num = 1;
    //        baravordUser.Name = Title;
    //        baravordUser.Year = Year;
    //        baravordUser.NoeFBs = NoeFBs;
    //        baravordUser.UserId = guidUserId;
    //        baravordUser.Type = Type;
    //        baravordUser.InsertDateTime = DateTime.Now;
    //        //baravordUser.Date = SolarDate.CurrentDate;
    //        //baravordUser.time = SolarDate.CurrentTime;
    //        _context.BaravordUsers.Add(baravordUser);


    //        clsBaravordZaribBalaSari baravordZaribBalaSari = new clsBaravordZaribBalaSari
    //        {
    //            BaravordId = baravordUser.ID,
    //            Monaghese = 3,
    //            Tarh = 1
    //        };
    //        _context.BaravordZaribBalaSaris.Add(baravordZaribBalaSari);

    //        //کردستان - سنندج
    //        clsBaravordZaribManteghe baravordZaribManteghe = new clsBaravordZaribManteghe
    //        {
    //            BaravordId = baravordUser.ID,
    //            OstanId = 20,
    //            ShahrId = 2001,
    //            BakhshId = 1034
    //        };
    //        _context.BaravordZaribManteghes.Add(baravordZaribManteghe);

    //        clsWorkflow? workflow = _context.Workflows.FirstOrDefault(x => x.proccesType == ProccesType.BarAvordAvalieh);

    //        if (workflow != null)
    //        {
    //            await _workflowRuntimeService.StartWorkflowAsync(
    //                    workflow.ID,
    //                    baravordUser.ID,
    //                    "Baravord",
    //                    guidUserId
    //                    );
    //        }

    //        await _context.SaveChangesAsync();
    //        return new JsonResult("OK_" + baravordUser.ID + "_" + (int)Type);
    //    }
    //    else
    //        return new JsonResult("NOK_");
    //}

    // POST: BaravordUser/Create
    [HttpPost]
    public async Task<ActionResult> Create(clsBaravordUser barAvordUser)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Guid UserId = Guid.Parse(user?.Id);

                // TODO: Add insert logic here
                barAvordUser.UserId = UserId;// Guid.Parse("b3500674-db49-44de-a006-45abad9fb1ed");
                //barAvordUser._Date = SolarDate.CurrentDate;
                //barAvordUser._time = SolarDate.CurrentTime;
                _context.BaravordUsers.Add(barAvordUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            else
                return new JsonResult(null);
        }
        catch (Exception e)
        {
            return new JsonResult(null);
        }
    }

    [HttpPost]
    public async Task<long> GetLastNum([FromBody] GetLastNumInputDto request)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            Guid guidUserId = Guid.Parse(user?.Id);
            //Guid guidUserId = Guid.Parse("b3500674-db49-44de-a006-45abad9fb1ed");
            clsBaravordUser? baravordUserItem = await _context.BaravordUsers.Where(f => f.UserId == guidUserId
                                                && f.Type == NoeBarAvord.Gharardad && f.Year == request.Year).OrderByDescending(f => f.Num).FirstOrDefaultAsync();
            long intNum = 0;
            if (baravordUserItem == null)
                intNum = 1;
            else
                intNum = baravordUserItem.Num + 1;
            return intNum;
        }
        else
            return 0;
    }

    // GET: BaravordUser/Edit/5
    public async Task<ActionResult> Edit(Guid id)
    {
        clsBaravordUser? barAvordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == id);
        return View(barAvordUser);
    }

    // POST: BaravordUser/Edit/5
    [HttpPost]
    public async Task<ActionResult> Edit(clsBaravordUser barAvordUser)
    {
        try
        {
            // TODO: Add update logic here
            clsBaravordUser? tblbarAvordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == barAvordUser.ID);
            if (tblbarAvordUser != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    Guid guidUserId = Guid.Parse(user?.Id);

                    barAvordUser.UserId = guidUserId;// Guid.Parse("b3500674-db49-44de-a006-45abad9fb1ed");
                                                     //barAvordUser._Date = tblbarAvordUser._Date;
                                                     //barAvordUser._time = tblbarAvordUser._time;
                    _context.Entry(tblbarAvordUser).CurrentValues.SetValues(barAvordUser);
                    _context.SaveChanges();
                    return RedirectToAction("List");
                }
                return View(barAvordUser);
            }
            else
                return View();
        }
        catch (Exception)
        {
            return View();
        }
    }

    [HttpPost]
    public async Task<JsonResult> DeleteBarAvordUser([FromBody] DeleteBarAvordUserDto request)
    {

        clsBaravordUser? barAvordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == request.Id);
        if (barAvordUser != null)
        {
            _context.BaravordUsers.Entry(barAvordUser).CurrentValues.SetValues(new
            {
                IsDeleted = true
            });

            //_context.BaravordUsers.Remove(barAvordUser);
            await _context.SaveChangesAsync();
        }
        return new JsonResult("OK");
        //}
        //catch (Exception)
        //{
        //    return new JsonResult("NOK");
        //}
    }

    [HttpPost]
    public async Task<JsonResult> ConvertToGharardad([FromBody] ConvertToGharardadDto request)
    {
        Guid BaravordId = request.Id;

        clsBaravordUser? barAvordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == BaravordId);
        if (barAvordUser != null)
        {
            string NoeFBs = barAvordUser.NoeFBs;

            _context.BaravordUsers.Entry(barAvordUser).CurrentValues.SetValues(new
            {
                Type = NoeBarAvord.Gharardad
            });

            PrePareToConvertDto PrePareToConvert = new PrePareToConvertDto
            {
                BarAvordUserId = BaravordId,
                NoeFBs = NoeFBs,
                Year = barAvordUser.Year
            };
            BarAvordUserCommon barAvordUserCommon = new BarAvordUserCommon();
            await barAvordUserCommon.PrePareToConvert(PrePareToConvert, _context);

            //_context.BaravordUsers.Remove(barAvordUser);
            await _context.SaveChangesAsync();
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
    public async Task<ActionResult> Delete(Guid id, clsBaravordUser baravordUser)
    {
        try
        {
            // TODO: Add delete logic here
            clsBaravordUser? barAvordUser = await _context.BaravordUsers.FirstOrDefaultAsync(x => x.ID == id);
            if (barAvordUser != null)
            {
                _context.BaravordUsers.Remove(barAvordUser);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
        catch
        {
            return View();
        }
    }

}
