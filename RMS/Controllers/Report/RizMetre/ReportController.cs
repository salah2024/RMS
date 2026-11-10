using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using RMS.Controllers.BarAvordUser.Common;
using RMS.Controllers.BaseInfo.Dto;
using RMS.Controllers.PaymanKar.Common;
using RMS.Controllers.PaymanKar.Dto;
using RMS.Controllers.Report.RizMetre.Dto.BaravordFullPdf;
using RMS.Controllers.Report.RizMetre.Dto.FosoulForBarAvord;
using RMS.Controllers.Report.RizMetre.Dto.RizMetre;
using RMS.Controllers.Report.RizMetre.Dto.FB;
using RMS.Controllers.Report.RizMetre.Dto.Financial;
using RMS.Models.Common;
using RMS.Models.Common.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Report.RizMetre
{
    public class ReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment env) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        private readonly ApplicationDbContext _context = context;
        private readonly IWebHostEnvironment _env = env;
        public IActionResult Index()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_viewReportForm");


            return View();
        }


        public IActionResult GetInfoItemsWithRizMetre()
        {
            string BAId = TempData["BAId"].ToString();

            Guid BarAvordId = Guid.Parse(BAId);
            // گرفتن اطلاعات از دیتابیس با EF
            var BaravordUser = _context.BaravordUsers
                .FirstOrDefault(x => x.ID == BarAvordId);

            List<RizMetreForReportDto> allItemsWithRizMetre = _context.RizMetreUserses.Include(x => x.FB).ThenInclude(x => x.BaravordUser)
                .Where(x => x.FB.BarAvordId == BarAvordId).Select(x => new RizMetreForReportDto
                {
                    Code = x.ShomarehNew,
                    Sharh = x.Sharh,
                    Tedad = x.Tedad,
                    Tool = x.Tool,
                    Arz = x.Arz,
                    Ertefa = x.Ertefa,
                    Vazn = x.Vazn,
                    JameJoz = x.MeghdarJoz,
                    NoeFB = (int)x.FB.NoeFBId,
                    NoeFBName = x.FB.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" : x.FB.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری"
                                              : x.FB.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" : x.FB.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                                              x.FB.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" : x.FB.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
                    Year = x.FB.BaravordUser.Year,
                    ItemShomareh = x.FB.Shomareh,
                    ItemSharh = x.FB.BahayeVahedSharh,
                    Tozihat = ""
                })
                .OrderBy(x => x.NoeFB)
                .ThenBy(x => x.ItemShomareh)
                .ToList();

            return new JsonResult(allItemsWithRizMetre);


            // ساخت گزارش
            //var report = new StiReport();

            //var reportPath = Path.Combine(_env.WebRootPath, "Reports", "ItemsWithRizMetre.mrt");

            //report.Load(reportPath);

            //// ساخت متن طرح
            ////string tarh = string.Join("\n",
            ////    shomarehTarh.Select(x => $"{x.ShomarehTarh} - {x.OnvanTarh}")
            ////);

            //// ست کردن متغیرها
            //report.Dictionary.Variables["Date"].Value = DateTime.Now.ToString("yyyy/MM/dd");
            //report.Dictionary.Variables["Onvan"].Value = BaravordUser?.Name?.Trim();
            //report.Dictionary.Variables["KarfarmaName"].Value = "";
            //report.Dictionary.Variables["MoshaverNazer"].Value = "";
            //report.Dictionary.Variables["ShomarayeProje"].Value = "";
            //report.Dictionary.Variables["Tarh"].Value = "";

            //// ثبت دیتا
            //report.RegBusinessObject("ItemsWithRizMetre","ItemsWithRizMetre", allItemsWithRizMetre);
            //report.Dictionary.Synchronize();

            //return StiNetCoreViewer.GetReportResult(this, report);
        }


        [HttpGet]
        public async Task<IActionResult> ItemsWithRizMetre(string BAId)
        {
            // فرض می‌کنیم BarAvordId را از جایی (مثلاً Session یا Query String) دریافت می‌کنیم
            //string BAId = TempData["BAId"].ToString();

            Guid BarAvordId = Guid.Parse(BAId);

            // گرفتن داده‌ها (مشابه کدی که خودتان ارائه دادید)
            var BaravordUser = _context.BaravordUsers.First(x => x.ID == BarAvordId);

            List<RizMetreForReportDto> allItemsWithRizMetre = _context.RizMetreUserses
                .Include(x => x.FB).ThenInclude(x => x.BaravordUser)
                .Where(x => x.FB.BarAvordId == BarAvordId)
                .Select(x => new RizMetreForReportDto
                {
                    Code = x.ShomarehNew,
                    Sharh = x.Sharh,
                    Tedad = x.Tedad,
                    Tool = x.Tool,
                    Arz = x.Arz,
                    Ertefa = x.Ertefa,
                    Vazn = x.Vazn,
                    JameJoz = x.MeghdarJoz,
                    NoeFB = (int)x.FB.NoeFBId,
                    NoeFBName = x.FB.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" : x.FB.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری"
                                              : x.FB.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" : x.FB.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                                              x.FB.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" : x.FB.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
                    Year = x.FB.BaravordUser.Year,
                    ItemShomareh = x.FB.Shomareh,
                    BahayeVahedNew = x.FB.BahayeVahedNew,
                    Tozihat = x.Des
                })
                .OrderBy(x => x.NoeFB)
                .ThenBy(x => x.ItemShomareh)
                .ToList();


            List<RizMetreForReportDto> lstItemFBStars = await _context.RizMetreStars.Include(x => x.ItemFBStar).ThenInclude(x => x.Vahed).Where(x => x.ItemFBStar.BaravordId == BarAvordId).Select(i => new RizMetreForReportDto
            {
                Code = i.ShomarehNew,
                Sharh = i.Sharh,
                Tedad = i.Tedad,
                Tool = i.Tool,
                Arz = i.Arz,
                Ertefa = i.Ertefa,
                Vazn = i.Vazn,
                JameJoz = i.MeghdarJoz,
                NoeFB = (int)i.ItemFBStar.NoeFBId,
                NoeFBName = i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری"
                                              : i.ItemFBStar.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                                              i.ItemFBStar.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
                Year = i.ItemFBStar.BaravordUser.Year,
                ItemShomareh = i.ItemFBStar.Shomareh,
                ShomarehMain = i.ItemFBStar.Shomareh,
                ItemSharh = i.ItemFBStar.Sharh,
                BahayeVahedNew = i.ItemFBStar.BahayeVahed,
                BahayeVahed = i.ItemFBStar.BahayeVahed,
                BahayeKol = (i.ItemFBStar.BahayeVahed == 0 ?
                                  i.ItemFBStar.BahayeVahed : i.ItemFBStar.BahayeVahed) * i.MeghdarJoz,
                Vahed = i.ItemFBStar.Vahed.Name,
                Tozihat = i.Des
            }).ToListAsync();


            //var fehrest = await _context.FehrestBahas.Where(x=>x.Sal== BaravordUser.Year).ToListAsync();

            var fehrest = await (from FehrestBaha in _context.FehrestBahas
                                 join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                                 on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
                                 from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
                                 where FehrestBaha.Sal == BaravordUser.Year
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

            List<RizMetreForReportDto> lstFehrestBahas =
                (from f in fehrest
                 join i in allItemsWithRizMetre
                 on f.Shomareh equals i.ItemShomareh.Substring(0,6)
                 where f.Sal == i.Year
                    && f.NoeFB == (NoeFehrestBaha)i.NoeFB
                 select new RizMetreForReportDto
                 {
                     Code = i.Code,
                     Sharh = i.Sharh,
                     Tedad = i.Tedad,
                     Tool = i.Tool,
                     Arz = i.Arz,
                     Ertefa = i.Ertefa,
                     Vazn = i.Vazn,
                     JameJoz = i.JameJoz,
                     NoeFB = i.NoeFB,
                     NoeFBName = i.NoeFBName,
                     Year = i.Year,
                     ItemShomareh = i.ItemShomareh,
                     ShomarehMain = i.ItemShomareh,
                     ItemSharh = f.Sharh,
                     Vahed = f.Vahed,
                     BahayeVahedNew = i.BahayeVahedNew,
                     BahayeVahed = (i.BahayeVahedNew == null || i.BahayeVahedNew == 0) ?
                                    decimal.Parse(f.BahayeVahed ?? "0") : i.BahayeVahedNew,
                     BahayeKol = ((i.BahayeVahedNew == null || i.BahayeVahedNew == 0) ?
                                  decimal.Parse(f.BahayeVahed ?? "0") : i.BahayeVahedNew) * i.JameJoz,
                     Tozihat = i.Tozihat
                 }).ToList();

            lstFehrestBahas.AddRange(lstItemFBStars);

            List<RizMetreForReportDto> lstFehrestBahas1 = new List<RizMetreForReportDto>();

            lstFehrestBahas1.AddRange(
            lstFehrestBahas.OrderBy(x => x.ShomarehMain).ThenBy(x => x.Code));

            var items = lstFehrestBahas1
                .GroupBy(x => new
                {
                    x.ShomarehMain,
                    x.ItemSharh,
                    x.BahayeVahed,
                    x.BahayeVahedNew,
                    x.Vahed,
                    //x.BahayeKol,
                })
                .Select(g => new RizMetreReportItemVm
                {
                    ItemShomareh = g.Key.ShomarehMain,
                    ItemSharh = g.Key.ItemSharh,
                    ItemBahayeVahed = g.Key.BahayeVahed,
                    ItemBahayeVahedNew = g.Key.BahayeVahedNew,
                    //ItemBahayeKol = g.Key.BahayeKol,
                    ItemVahed = g.Key.Vahed,
                    Details = g.ToList()
                }).ToList();

            //return View(items);




            List<RizMetreReportPageVm> pages = new();

            int currentRows = 0;

            var currentPage = new RizMetreReportPageVm
            {
                PageNo = 1,
                Items = new()
            };

            const int MaxRowsPerPage = 25;

            foreach (var item in items)
            {
                int itemRows = item.Details.Count + 5;

                if (currentRows + itemRows > MaxRowsPerPage)
                {
                    pages.Add(currentPage);

                    currentPage = new RizMetreReportPageVm
                    {
                        PageNo = pages.Count + 1,
                        Items = new()
                    };

                    currentRows = 0;
                }

                currentPage.Items.Add(item);

                currentRows += itemRows;
            }

            if (currentPage.Items.Any())
            {
                pages.Add(currentPage);
            }

            return View(pages);


        }




        [HttpPost]
        public async Task<IActionResult> Tadil(Guid BAId, Guid CompanyId, long Year)
        {
            GetTadilIndexDto request = new GetTadilIndexDto
            {
                SVId = BAId,
                CompanyId = CompanyId,
                Year = Year
            };

            TadilCommon tadilCommon = new TadilCommon();
            ResultGetTadilIndex Result = await tadilCommon.GetTadil(request, _context);


            const int RowsPerPage = 10;

            var pages = new List<TadilReportPageVm>();

            int pageNo = 1;

            foreach (var tadilIndex in Result.lstresultTadilIndexDtos)
            {
                var rows = tadilIndex.lstFaslTadilIndex
                    .SelectMany(f =>
                        (f.lstTadilIndex ?? new List<ResultItem>())
                        .Select(x => new TadilReportRowVm
                        {
                            Fasl = f,
                            Item = x
                        }))
                    .ToList();

                for (int i = 0; i < rows.Count; i += RowsPerPage)
                {
                    pages.Add(new TadilReportPageVm
                    {
                        PageNo = pageNo++,
                        TadilIndex = tadilIndex,
                        Rows = rows.Skip(i)
                                   .Take(RowsPerPage)
                                   .ToList()
                    });
                }
            }

            var vm = new TadilReportVm
            {
                Report = Result,
                Pages = pages
            };

            return View("~/Views/Report/TadilReport.cshtml", vm);


            //return View("~/Views/Report/TadilReport.cshtml", Result);
        }

        //public IActionResult ViewerEvent()
        //{
        //return StiNetCoreViewer.ViewerEventResult(this);
        //}

        [HttpPost]
        public async Task<IActionResult> ExportTadilToPdf(Guid BAId, Guid CompanyId, long Year)
        {
            GetTadilIndexDto request = new GetTadilIndexDto
            {
                SVId = BAId,
                CompanyId = CompanyId,
                Year = Year
            };

            TadilCommon tadilCommon = new TadilCommon();
            ResultGetTadilIndex Result = await tadilCommon.GetTadil(request, _context);

            var reportData =
            Result.lstresultTadilIndexDtos
            .Select(x =>
            new TadilPdfVm
            {
                AllDay = Result.AllDay,
                NoeFBName = x.NoeFBName,
                ProjectName = Result.ProjectName,
                Title = Result.Title,
                FromDate = Result.FromDate,
                ToDate = Result.ToDate,
                MabnaIndexTitle = x.MabnaIndexTitle,

                Rows = x.lstFaslTadilIndex
            .SelectMany(f =>
                f.lstTadilIndex.Select(i =>
                    new TadilPdfRowVm
                    {
                        Fasl = f.FaslCode,
                        Item = i,
                        MablaghCurrent = f.BahayeKolInZarib,
                        MablaghPrev = f.BahayeKolInZaribPrev,
                        Deff = f.Deff
                    }))
            .ToList()
            })
            .ToList();


            var document =
            new TadilPdfDocument(reportData);

            byte[] pdf =
                document.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                "TadilReport.pdf");
        }

        /// <summary>
        /// فقط فهرست بها ها
        /// </summary>
        /// <param name="BAId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ExportNoteBookPdf(string BAId)
        {
            ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

            Guid barAvordId = Guid.Parse(BAId);

            var baravordUser = _context.BaravordUsers
                .FirstOrDefault(x => x.ID == barAvordId);

            if (baravordUser == null)
                return NotFound();

            string projectName = baravordUser.Name;

            List<clsZaribPishnahadiPaymanKarFosoul> lstZPPaymanKar = new List<clsZaribPishnahadiPaymanKarFosoul>();


            Guid? CompanyId = Guid.Parse("7D9BD20C-6FD3-458C-0B05-08DE745B0EBA");
            if (baravordUser.Type != NoeBarAvord.BarAvord)
            {
                ///در صورتی که صورت وضعیت میباشد بایستی 
                /////پیمانکار گزارش بگیرد
                ///در نتیجه بایستی آیدی کومپانی موجود باشد
                ///تا بتوان ضریب پیشنهادی پبمانکار را پیدا کرد
                ///

                if (baravordUser.ParentId != null)
                {

                    var parent = await _context.BaravordUsers
                        .FirstAsync(x => x.ID == baravordUser.ParentId);

                    projectName = parent.Name;
                }
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    CompanyId = _context.UserDetails.First(x => x.UserId == user.Id).CompanyId;

                    lstZPPaymanKar = _context.ZaribPishnahadiPaymanKarFosouls.Include(x => x.ZaribPishnahadiPaymanKar)
                                         .Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == baravordUser.ParentId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId).ToList();

                }

                if (CompanyId == null)
                {
                    return null;
                }
            }
            else
                baravordUser.Name = "برآورد اولیه";


            List<ReportDataForFBSummaryDto> reportData = new();

            //decimal tajhizBase = 0;
            //decimal tajhizStar = 0;
            decimal tajhizTotal = 0;

            foreach (string strNoeFB in baravordUser.NoeFBs.Split(','))
            {
                NoeFehrestBaha noeFB = (NoeFehrestBaha)int.Parse(strNoeFB);

                GetFosoulForBAAndSVInputDto request = new()
                {
                    BarAvordUserId = barAvordId,
                    NoeFBId = noeFB,
                    Year = baravordUser.Year
                };

                var result = await showBarAvord.fnGetFosoulForBarAvord(request, _context);

                //---------------------------------------
                // حذف فصل های صفر
                //---------------------------------------
                result.Fosoul = result.Fosoul
                    .Where(x =>
                        x.JameFasl != 0 ||
                        x.JameFaslStar != 0 ||
                        x.JameFaslAll != 0 ||
                        x.JameFaslWithZarib != 0 ||
                        x.JameFaslStarWithZarib != 0 ||
                        x.JameFaslWithZaribAll != 0)
                    .ToList();

                //----------------------------------------------------
                // استخراج تجهیز کارگاه
                //----------------------------------------------------

                var tajhizFasl = result.Fosoul.FirstOrDefault(x => x.Code == "99");

                if (tajhizFasl != null)
                {
                    //tajhizBase += tajhizFasl.JameFaslWithZarib;
                    //tajhizStar += tajhizFasl.JameFaslStarWithZarib;
                    tajhizTotal += tajhizFasl.JameFaslWithZaribAll;

                    // حذف از گزارش فهرست بها
                    result.Fosoul.Remove(tajhizFasl);
                }
                else
                {
                    var tajhiz = _context.TajhizKargahDarsads.FirstOrDefault(x =>
                        x.BarAvordUserId == barAvordId &&
                        x.NoeFB == noeFB);

                    if (tajhiz != null && tajhiz.Meghdar != 0)
                    {
                        tajhizTotal += tajhiz.Meghdar;
                    }
                }
                //----------------------------------------------------
                // جمع فهرست بها (بدون تجهیز)
                //----------------------------------------------------

                decimal Base = result.Fosoul.Sum(x => x.JameFasl);
                decimal Star = result.Fosoul.Sum(x => x.JameFaslStar);
                decimal Total = result.Fosoul.Sum(x => x.JameFaslAll);

                decimal BaseZ = result.Fosoul.Sum(x => x.JameFaslWithZarib);
                decimal StarZ = result.Fosoul.Sum(x => x.JameFaslStarWithZarib);
                decimal TotalZ = result.Fosoul.Sum(x => x.JameFaslWithZaribAll);

                //decimal baseAmount =
                //    result.Fosoul.Sum(x => x.JameFaslWithZarib);

                //decimal starAmount =
                //    result.Fosoul.Sum(x => x.JameFaslStarWithZarib);

                //decimal totalAmount =
                //    result.Fosoul.Sum(x => x.JameFaslWithZaribAll);

                //---------------------------------------
                // نام فهرست بها
                //---------------------------------------

                string fbName = noeFB switch
                {
                    NoeFehrestBaha.RahDari => "راهداری",
                    NoeFehrestBaha.Abnie => "ابنیه",
                    NoeFehrestBaha.RahoBand => "راه، راه‌آهن و باند فرودگاه",
                    NoeFehrestBaha.Barghi => "تأسیسات برقی",
                    NoeFehrestBaha.Mekaniki => "تأسیسات مکانیکی",
                    NoeFehrestBaha.MareMat => "مرمت بناهای تاریخی",
                    _ => ""
                };

                reportData.Add(new ReportDataForFBSummaryDto
                {
                    NoeFBName = fbName,

                    //BaseAmount = baseAmount,

                    //StarAmount = starAmount,

                    //TotalAmount = totalAmount,

                    Base = Base,
                    Star = Star,
                    Total = Total,

                    BaseZ = BaseZ,
                    StarZ = StarZ,
                    TotalZ = TotalZ,

                    Title = projectName,

                    Year = baravordUser.Year
                });
            }

            decimal? sumFBBase = reportData.Sum(x => x.Base);

            decimal? sumFBStar = reportData.Sum(x => x.Star);

            decimal? sumFBTotal = reportData.Sum(x => x.Total);

            decimal? sumFBBaseZ = reportData.Sum(x => x.BaseZ);

            decimal? sumFBStarZ = reportData.Sum(x => x.StarZ);

            decimal? sumFBTotalZ = reportData.Sum(x => x.TotalZ);

            reportData.Add(new ReportDataForFBSummaryDto
            {
                NoeFBName = "جمع کل فهرست بهاها",

                Base = sumFBBase == 0 ? null : sumFBBase,

                Star = sumFBStar == 0 ? null : sumFBStar,

                Total = sumFBTotal == 0 ? null : sumFBTotal,

                BaseZ = sumFBBaseZ == 0 ? null : sumFBBaseZ,

                StarZ = sumFBStarZ == 0 ? null : sumFBStarZ,

                TotalZ = sumFBTotalZ == 0 ? null : sumFBTotalZ,

                Title = baravordUser.Name,

                Year = baravordUser.Year
            });

            reportData.Add(new ReportDataForFBSummaryDto
            {
                NoeFBName = "تجهیز و برچیدن کارگاه",

                //BaseAmount = tajhizBase,

                //StarAmount = tajhizStar,

                TotalZ = tajhizTotal,

                Title = baravordUser.Name,

                Year = baravordUser.Year
            });

            reportData.Add(new ReportDataForFBSummaryDto
            {
                NoeFBName = "جمع نهایی برآورد",

                //BaseAmount = sumFBBase,

                //StarAmount = sumFBStar,

                TotalZ = sumFBTotalZ + tajhizTotal,

                Title = baravordUser.Name,

                Year = baravordUser.Year
            });

            ReportDataForFBSummaryL1Dto reportDataForFB = new ReportDataForFBSummaryL1Dto
            {
                baravordUser = baravordUser,
                reportDataForFBSummary = reportData
            };

            var document = new FBSummaryPdfDocument(reportDataForFB);


            byte[] pdf = document.GeneratePdf();

            return File(pdf,
                "application/pdf",
                "FBSummary.pdf");
        }


        /// <summary>
        /// فصول تمامی فهرست بها ها
        /// </summary>
        /// <param name="BAId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportFosoulForBarAvordToPdf(string BAId)
        {

            ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

            Guid BarAvordId = Guid.Parse(BAId);
            clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == BarAvordId);

            List<ReportDataForFosoulForBarAvordDto> lstReportData = new List<ReportDataForFosoulForBarAvordDto>();
            if (baravordUser != null)
            {
                string[] strNoeFB = baravordUser.NoeFBs.Split(",");
                foreach (var NoeFB in strNoeFB)
                {
                    GetFosoulForBAAndSVInputDto request = new GetFosoulForBAAndSVInputDto
                    {
                        BarAvordUserId = BarAvordId,
                        NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB),
                        Year = baravordUser.Year,
                    };
                    ResultGetFosoulForBarAvordDto Result = await showBarAvord.fnGetFosoulForBarAvord(request, _context);


                    Result.Fosoul = Result.Fosoul
                        .Where(x =>
                            x.JameFasl != 0 ||
                            x.JameFaslStar != 0 ||
                            x.JameFaslAll != 0 ||
                            x.JameFaslWithZarib != 0 ||
                            x.JameFaslStarWithZarib != 0 ||
                            x.JameFaslWithZaribAll != 0)
                        .ToList();

                    string strNoeFBName = "";
                    switch ((NoeFehrestBaha)int.Parse(NoeFB))
                    {
                        case NoeFehrestBaha.RahDari:
                            strNoeFBName = "راهداری";
                            break;
                        case NoeFehrestBaha.Abnie:
                            strNoeFBName = "ابنیه";
                            break;
                        case NoeFehrestBaha.RahoBand:
                            strNoeFBName = "راه، راه‌آهن و باند فرودگاه";
                            break;
                        case NoeFehrestBaha.Barghi:
                            strNoeFBName = "تاسیسات برقی";
                            break;
                        case NoeFehrestBaha.Mekaniki:
                            strNoeFBName = "تاسیسات مکانیکی";
                            break;
                        case NoeFehrestBaha.MareMat:
                            strNoeFBName = "مرمت بناهای تاریخی";
                            break;
                        default:
                            break;
                    }

                    clsTajhizKargahDarsad? TajhizKargahDarsad = _context.TajhizKargahDarsads.FirstOrDefault(x => x.BarAvordUserId == request.BarAvordUserId && x.NoeFB == (NoeFehrestBaha)int.Parse(NoeFB));

                    decimal? MeghdarTajhiz = null;
                    if (TajhizKargahDarsad != null)
                    {
                        MeghdarTajhiz = TajhizKargahDarsad.Meghdar;
                    }

                    if (MeghdarTajhiz.HasValue && MeghdarTajhiz.Value != 0)
                    {
                        Result.Fosoul.Add(new GetFosoulOutputDto
                        {
                            Id = 0,
                            Order = 300,
                            FaslName = "پیوست 3 - دستورالعمل تجهیز و برچیدن کارگاه",

                            JameFasl = 0,
                            JameFaslStar = 0,
                            JameFaslAll = 0,

                            JameFaslWithZarib = 0,
                            JameFaslStarWithZarib = 0,
                            JameFaslWithZaribAll = MeghdarTajhiz.Value,

                            FaslAsli = true
                        });
                    }

                    Result.Fosoul = Result.Fosoul
                            .OrderBy(x => x.Order)
                            .ToList();

                    var reportDate = new ReportDataForFosoulForBarAvordDto
                    {
                        resultGetFosoulForBarAvord = Result,
                        NoeFBName = strNoeFBName,
                        Title = baravordUser.Name,
                        Year = baravordUser.Year
                    };

                    lstReportData.Add(reportDate);

                }
            }

            var document =
            new ReportDataForFosoulForBarAvordPdfDocument(lstReportData);

            byte[] pdf =
                document.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                "FosoulForBarAvordReport.pdf");
        }


        [HttpGet]
        public async Task<IActionResult> ExportRizMetrePdf(string BAId)
        {
            Guid BarAvordId = Guid.Parse(BAId);

            // گرفتن داده‌ها (مشابه کدی که خودتان ارائه دادید)
            var BaravordUser = _context.BaravordUsers.First(x => x.ID == BarAvordId);

            string strBATitle = BaravordUser.Name;


            if (BaravordUser.ParentId != null)
            {
                var BaravordUserParent = _context.BaravordUsers.First(x => x.ID == BaravordUser.ParentId);
                strBATitle = BaravordUserParent.Name;
            }
            else
                BaravordUser.Name = "برآورد اولیه";

            //string[] strNoeFB = BaravordUser.NoeFBs.Split(",");

            //foreach (var NoeFB in strNoeFB)
            //{
            //    _context.Fosouls.Where(x => x.Year == BaravordUser.Year && x.NoeFB == (NoeFehrestBaha)int.Parse(NoeFB));

            //}


            List<RizMetreForReportDto> allItemsWithRizMetre = _context.RizMetreUserses
                .Include(x => x.FB).ThenInclude(x => x.BaravordUser)
                .Where(x => x.FB.BarAvordId == BarAvordId)
                .Select(x => new RizMetreForReportDto
                {
                    Code = x.ShomarehNew,
                    Sharh = x.Sharh,
                    Tedad = x.Tedad,
                    Tool = x.Tool,
                    Arz = x.Arz,
                    Ertefa = x.Ertefa,
                    Vazn = x.Vazn,
                    JameJoz = x.MeghdarJoz,
                    NoeFB = (int)x.FB.NoeFBId,
                    NoeFBName = x.FB.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" : x.FB.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری"
                                              : x.FB.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" : x.FB.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                                              x.FB.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" : x.FB.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
                    Year = x.FB.BaravordUser.Year,
                    ItemShomareh = x.FB.Shomareh,
                    BahayeVahedNew = x.FB.BahayeVahedNew,
                    Tozihat = x.Des
                })
                .OrderBy(x => x.NoeFB)
                .ThenBy(x => x.ItemShomareh)
                .ToList();




            List<RizMetreForReportDto> lstItemFBStars = await _context.RizMetreStars.Include(x => x.ItemFBStar).ThenInclude(x => x.Vahed).Where(x => x.ItemFBStar.BaravordId == BarAvordId).Select(i => new RizMetreForReportDto
            {
                Code = i.ShomarehNew,
                Sharh = i.Sharh,
                Tedad = i.Tedad,
                Tool = i.Tool,
                Arz = i.Arz,
                Ertefa = i.Ertefa,
                Vazn = i.Vazn,
                JameJoz = i.MeghdarJoz,
                NoeFB = (int)i.ItemFBStar.NoeFBId,
                NoeFBName = i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری"
                                              : i.ItemFBStar.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                                              i.ItemFBStar.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
                Year = i.ItemFBStar.BaravordUser.Year,
                ItemShomareh = i.ItemFBStar.Shomareh,
                ShomarehMain = i.ItemFBStar.Shomareh,
                ItemSharh = i.ItemFBStar.Sharh,
                BahayeVahedNew = i.ItemFBStar.BahayeVahed,
                BahayeVahed = i.ItemFBStar.BahayeVahed,
                BahayeKol = (i.ItemFBStar.BahayeVahed == 0 ?
                                  i.ItemFBStar.BahayeVahed : i.ItemFBStar.BahayeVahed) * i.MeghdarJoz,
                Vahed = i.ItemFBStar.Vahed.Name,
                Tozihat = i.Des
            }).ToListAsync();


            //var fehrest = await _context.FehrestBahas.Where(x=>x.Sal== BaravordUser.Year).ToListAsync();

            var fehrest = await (from FehrestBaha in _context.FehrestBahas
                                 join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
                                 on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
                                 from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
                                 where FehrestBaha.Sal == BaravordUser.Year
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

            List<RizMetreForReportDto> lstFehrestBahas =
                (from f in fehrest
                 join i in allItemsWithRizMetre
                 on f.ShomarehMain equals i.ItemShomareh.Substring(0,6)
                 where f.Sal == i.Year
                    && f.NoeFB == (NoeFehrestBaha)i.NoeFB
                 select new RizMetreForReportDto
                 {
                     Code = i.Code,
                     Sharh = i.Sharh,
                     Tedad = i.Tedad,
                     Tool = i.Tool,
                     Arz = i.Arz,
                     Ertefa = i.Ertefa,
                     Vazn = i.Vazn,
                     JameJoz = i.JameJoz,
                     NoeFB = i.NoeFB,
                     NoeFBName = i.NoeFBName,
                     Year = i.Year,
                     ItemShomareh = i.ItemShomareh,
                     ShomarehMain = f.ShomarehMain!= i.ItemShomareh? i.ItemShomareh: f.ShomarehMain,
                     ItemSharh = f.Sharh,
                     Vahed = f.Vahed,
                     BahayeVahedNew = i.BahayeVahedNew,
                     BahayeVahed = (i.BahayeVahedNew == null || i.BahayeVahedNew == 0) ?
                                    decimal.Parse(f.BahayeVahed ?? "0") : i.BahayeVahedNew,
                     BahayeKol = ((i.BahayeVahedNew == null || i.BahayeVahedNew == 0) ?
                                  decimal.Parse(f.BahayeVahed ?? "0") : i.BahayeVahedNew) * i.JameJoz,
                     Tozihat = i.Tozihat
                 }).ToList();

            lstFehrestBahas.AddRange(lstItemFBStars);

            List<RizMetreForReportDto> lstFehrestBahas1 = new List<RizMetreForReportDto>();

            lstFehrestBahas1.AddRange(
            lstFehrestBahas.OrderBy(x => x.ShomarehMain).ThenBy(x => x.Code));



            var reportGroups = lstFehrestBahas1
             .GroupBy(x => new
             {
                 x.NoeFB,
                 x.NoeFBName,
                 x.Year
             })
             .Select(group => new RizMetreReportGroupVm
             {
                 NoeFB = group.Key.NoeFB,
                 NoeFBName = group.Key.NoeFBName,
                 Year = group.Key.Year,

                 Items = group
                     .GroupBy(x => new
                     {
                         x.ShomarehMain,
                         x.ItemSharh,
                         x.BahayeVahed,
                         x.BahayeVahedNew,
                         x.Vahed
                     })
                     .Select(g => new RizMetreReportItemVm
                     {
                         NoeFBName = group.Key.NoeFBName,

                         ItemShomareh = g.Key.ShomarehMain,

                         ItemSharh = g.Key.ItemSharh,

                         ItemBahayeVahed = g.Key.BahayeVahed,

                         ItemBahayeVahedNew = g.Key.BahayeVahedNew,

                         ItemVahed = g.Key.Vahed,

                         Details = g.ToList()
                     })
                     .ToList()
             })
             .OrderBy(x => x.NoeFB)
             .ToList();




            var document =
                new RizMetrePdfDocument(reportGroups, BaravordUser, strBATitle);

            byte[] pdf =
                document.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"ریزه متره پروژه_{strBATitle}.pdf");
        }



        [HttpGet]
        public async Task<IActionResult> ExportFinancialPdf(string BAId)
        {
            Guid barAvordId = Guid.Parse(BAId);

            var baravordUser = await _context.BaravordUsers
                .FirstAsync(x => x.ID == barAvordId);

            string projectName = baravordUser.Name;

            List<clsZaribPishnahadiPaymanKarFosoul> lstZPPaymanKar = new List<clsZaribPishnahadiPaymanKarFosoul>();

            Guid? CompanyId = Guid.Parse("7D9BD20C-6FD3-458C-0B05-08DE745B0EBA");
            if (baravordUser.Type != NoeBarAvord.BarAvord)
            {
                ///در صورتی که صورت وضعیت میباشد بایستی 
                /////پیمانکار گزارش بگیرد
                ///در نتیجه بایستی آیدی کومپانی موجود باشد
                ///تا بتوان ضریب پیشنهادی پبمانکار را پیدا کرد
                ///

                if (baravordUser.ParentId != null)
                {

                    var parent = await _context.BaravordUsers
                        .FirstAsync(x => x.ID == baravordUser.ParentId);

                    projectName = parent.Name;
                }
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    CompanyId = _context.UserDetails.First(x => x.UserId == user.Id).CompanyId;

                    lstZPPaymanKar = _context.ZaribPishnahadiPaymanKarFosouls.Include(x => x.ZaribPishnahadiPaymanKar)
                                         .Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == baravordUser.ParentId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId).ToList();

                }

                if (CompanyId == null)
                {
                    return null;
                }
            }
            else
                baravordUser.Name = "برآورد اولیه";




            #region ریز متره ها

            List<RizMetreForReportDto> allItemsWithRizMetre =
                await _context.RizMetreUserses
                .Include(x => x.FB)
                .ThenInclude(x => x.BaravordUser)
                .Where(x => x.FB.BarAvordId == barAvordId)
                .Select(x => new RizMetreForReportDto
                {
                    Code = x.ShomarehNew,
                    Sharh = x.Sharh,
                    Tedad = x.Tedad,
                    Tool = x.Tool,
                    Arz = x.Arz,
                    Ertefa = x.Ertefa,
                    Vazn = x.Vazn,
                    JameJoz = x.MeghdarJoz,
                    NoeFB = (int)x.FB.NoeFBId,
                    NoeFBName =
                        x.FB.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" :
                        x.FB.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری" :
                        x.FB.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" :
                        x.FB.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                        x.FB.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" :
                        x.FB.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
                    Year = x.FB.BaravordUser.Year,
                    ItemShomareh = x.FB.Shomareh,
                    BahayeVahedNew = x.FB.BahayeVahedNew
                })
                .OrderBy(x => x.NoeFB)
                .ThenBy(x => x.ItemShomareh)
                .ToListAsync();

            #endregion



            var allItemsWithRizMetre1 =
    (from itemRM in allItemsWithRizMetre
     join replace in _context.FehrestBahaItemsReplaces
     on new
     {
         FBShomarehMain = itemRM.ItemShomareh,
         NoeFB = (NoeFehrestBaha)itemRM.NoeFB,
         itemRM.Year
     }
     equals new
     {
         replace.FBShomarehMain,
         replace.NoeFB,
         replace.Year
     }
     into gj
     from replace in gj.DefaultIfEmpty()
     select new RizMetreForReportDto
     {
         Code = itemRM.Code,
         Sharh = itemRM.Sharh,
         Tedad = itemRM.Tedad,
         Tool = itemRM.Tool,
         Arz = itemRM.Arz,
         Ertefa = itemRM.Ertefa,
         Vazn = itemRM.Vazn,
         JameJoz = itemRM.JameJoz,
         NoeFB = itemRM.NoeFB,
         NoeFBName = itemRM.NoeFBName,
         Year = itemRM.Year,

         ShomarehMain = itemRM.ItemShomareh,
         BahayeVahedNew = itemRM.BahayeVahedNew,

         ItemShomareh = replace != null
             ? replace.FBShomarehNew
             : itemRM.ItemShomareh
     }).ToList();

            #region آیتم های ستاره دار

            List<RizMetreForReportDto> lstItemFBStars =
                await _context.RizMetreStars
                .Include(x => x.ItemFBStar)
                .ThenInclude(x => x.Vahed)
                .Where(x => x.ItemFBStar.BaravordId == barAvordId)
                .Select(i => new RizMetreForReportDto
                {
                    JameJoz = i.MeghdarJoz,
                    NoeFB = (int)i.ItemFBStar.NoeFBId,
                    NoeFBName =
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",

                    Year = i.ItemFBStar.BaravordUser.Year,

                    ItemShomareh = i.ItemFBStar.Shomareh,

                    ShomarehMain = i.ItemFBStar.Shomareh,

                    ItemSharh = i.ItemFBStar.Sharh,

                    BahayeVahed = i.ItemFBStar.BahayeVahed,

                    BahayeVahedNew = i.ItemFBStar.BahayeVahed,

                    Vahed = i.ItemFBStar.Vahed.Name,

                    BahayeKol = i.ItemFBStar.BahayeVahed * i.MeghdarJoz
                })
                .ToListAsync();

            #endregion

            #region فهرست بها

            var fehrest =
                await (from fb in _context.FehrestBahas
                       join rep in _context.FehrestBahaItemsReplaces
                       on fb.Shomareh equals rep.FBShomarehMain into grp
                       from rep in grp.DefaultIfEmpty()

                       where fb.Sal == baravordUser.Year

                       select new
                       {
                           fb.Shomareh,
                           fb.Sharh,
                           fb.Vahed,
                           fb.BahayeVahed,
                           fb.NoeFB,
                           fb.Sal,

                           ShomarehMain = fb.Shomareh,

                           NewCode = rep != null
                               ? rep.FBShomarehNew
                               : fb.Shomareh

                       }).ToListAsync();

            #endregion

            #region اتصال ریز متره به آیتم

            var items =
                (from f in fehrest
                 join r in allItemsWithRizMetre1
                 on f.ShomarehMain equals r.ShomarehMain.Substring(0,6)

                 where
                     f.NoeFB == (NoeFehrestBaha)r.NoeFB &&
                     f.Sal == r.Year

                 select new RizMetreForReportDto
                 {
                     NoeFB = r.NoeFB,
                     NoeFBName = r.NoeFBName,
                     ItemShomareh = r.ItemShomareh,
                     ShomarehMain = f.ShomarehMain!= r.ItemShomareh? r.ItemShomareh: f.ShomarehMain,

                     ItemSharh = f.Sharh,

                     Vahed = f.Vahed,

                     JameJoz = r.JameJoz,

                     BahayeVahed =
                        (r.BahayeVahedNew == null || r.BahayeVahedNew == 0)
                        ? decimal.Parse(f.BahayeVahed ?? "0")
                        : r.BahayeVahedNew,

                     BahayeKol =
                        ((r.BahayeVahedNew == null || r.BahayeVahedNew == 0)
                        ? decimal.Parse(f.BahayeVahed ?? "0")
                        : r.BahayeVahedNew) * r.JameJoz

                 }).ToList();

            items.AddRange(lstItemFBStars);

            #endregion



            #region فصل ها

            string[] noeFBs = baravordUser.NoeFBs.Split(',');

            List<FosoulForFinancialDto> fosoul = await
            (
                from fosoulItem in _context.Fosouls

                join zarib in _context.ZarayebFosouls
                    on fosoulItem.Id equals zarib.FaslId into zaribGroup

                from zarib in zaribGroup.DefaultIfEmpty()

                where fosoulItem.Year == baravordUser.Year
                      && noeFBs.Contains(((int)fosoulItem.NoeFB).ToString())

                orderby fosoulItem.NoeFB

                select new FosoulForFinancialDto
                {
                    Code = fosoulItem.Code,
                    Description = fosoulItem.Description,
                    Id = fosoulItem.Id,
                    LatinName = fosoulItem.LatinName,
                    Name = fosoulItem.Name,
                    NoeFB = fosoulItem.NoeFB,
                    order = fosoulItem.order,
                    Year = fosoulItem.Year,

                    ZaribBalasari = zarib != null ? zarib.ZaribBalaSari : null,
                    ZaribMantaghe = zarib != null ? zarib.ZaribManteghe : null
                }
            ).ToListAsync();



            //List <FosoulForFinancialDto> fosoul =
            //    await _context.Fosouls.Include(x=>x.zaraye)
            //    .Where(x =>
            //        x.Year == baravordUser.Year &&
            //        noeFBs.Contains(((int)x.NoeFB).ToString()))
            //    .OrderBy(x => x.NoeFB)
            //    .ThenBy(x => x.Code)
            //    .Select(x => new FosoulForFinancialDto
            //    {
            //        Code = x.Code,
            //        Description = x.Description,
            //        Id = x.Id,
            //        LatinName = x.LatinName,
            //        Name = x.Name,
            //        NoeFB = x.NoeFB,
            //        order = x.order,
            //        Year = x.Year,

            //    }).ToListAsync();

            foreach (var fasl in fosoul)
            {
                ////محاسبه ضرایب
                RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
                {
                    BarAvordId = barAvordId,
                    NoeFBId = fasl.NoeFB,
                    Year = baravordUser.Year
                };
                ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
                decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
                decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

                fasl.ZaribBalasari = fasl.ZaribBalasari == null ? zaribBalaSari : fasl.ZaribBalasari;
                fasl.ZaribMantaghe = fasl.ZaribMantaghe == null ? zaribManteghe : fasl.ZaribMantaghe;

                clsZaribPishnahadiPaymanKarFosoul? ZPPaymanKar = lstZPPaymanKar.FirstOrDefault(x => x.Code == fasl.Code);
                if (ZPPaymanKar != null)
                {
                    fasl.ZaribPishnahadiPK = ZPPaymanKar.ZaribJoz;
                }

            }




            #endregion

            #region مدل گزارش

            var report = fosoul
                .GroupBy(f => new
                {
                    f.NoeFB
                })
                .Select(gNoeFB => new FinancialNoeFBVm
                {
                    NoeFB = (int)gNoeFB.Key.NoeFB,

                    NoeFBName = gNoeFB.Key.NoeFB switch
                    {
                        NoeFehrestBaha.RahoBand => "راه، راه‌آهن و باند فرودگاه",

                        NoeFehrestBaha.RahDari => "راهداری",

                        NoeFehrestBaha.Abnie => "ابنیه",

                        NoeFehrestBaha.Barghi => "تأسیسات برقی",

                        NoeFehrestBaha.Mekaniki => "تأسیسات مکانیکی",

                        NoeFehrestBaha.MareMat => "مرمت بناهای تاریخی",

                        _ => ""
                    },

                    Chapters = gNoeFB
                        .OrderBy(x => x.Code)
                        .Select(f => new FinancialChapterVm
                        {
                            ChapterCode = f.Code,

                            ChapterName = f.Name,

                            NoeFB = (int)f.NoeFB,
                            ZaribBalasari = f.ZaribBalasari,
                            ZaribMantaghe = f.ZaribMantaghe,
                            ZaribPishnahadiPK = f.ZaribPishnahadiPK,

                            BaseItems = items
                                .Where(x =>
                                    x.NoeFB == (int)f.NoeFB &&
                                    x.ItemShomareh.StartsWith(f.Code))
                                .GroupBy(x => new
                                {
                                    x.ItemShomareh,
                                    //x.ShomarehMain,
                                    x.ItemSharh,
                                    x.Vahed,
                                    x.BahayeVahed,
                                    x.BahayeVahedNew
                                })
                                .Select(g => new FinancialItemVm
                                {
                                    ItemCode = g.Key.ItemShomareh,

                                    ItemSharh = g.Key.ItemSharh,

                                    Vahed = g.Key.Vahed,

                                    BahayeVahed = g.Key.BahayeVahed,

                                    IsStarItem =
                                        g.Key.BahayeVahedNew != null &&
                                        g.Key.BahayeVahedNew != 0,

                                    Meghdar = g.Sum(x => x.JameJoz ?? 0),

                                    BahayeKol = g.Sum(x => x.BahayeKol ?? 0)
                                })
                                .Where(x => !x.IsStarItem)
                                .OrderBy(x => x.ItemCode)
                                .ToList(),

                            StarItems = items
                                .Where(x =>
                                    x.NoeFB == (int)f.NoeFB &&
                                    x.ShomarehMain.StartsWith(f.Code))
                                .GroupBy(x => new
                                {
                                    x.ShomarehMain,
                                    x.ItemSharh,
                                    x.Vahed,
                                    x.BahayeVahed,
                                    x.BahayeVahedNew
                                })
                                .Select(g => new FinancialItemVm
                                {
                                    ItemCode = g.Key.ShomarehMain,

                                    ItemSharh = g.Key.ItemSharh,

                                    Vahed = g.Key.Vahed,

                                    BahayeVahed = g.Key.BahayeVahed,

                                    IsStarItem =
                                        g.Key.BahayeVahedNew != null &&
                                        g.Key.BahayeVahedNew != 0,

                                    Meghdar = g.Sum(x => x.JameJoz ?? 0),

                                    BahayeKol = g.Sum(x => x.BahayeKol ?? 0)
                                })
                                .Where(x => x.IsStarItem)
                                .OrderBy(x => x.ItemCode)
                                .ToList()

                        })
                        .ToList()

                })
                .OrderBy(x => x.NoeFB)
                .ToList();

            #endregion

            List<clsFehrestBahaItemsReplace> lstFBItemReplaces = _context.FehrestBahaItemsReplaces.Where(x => x.Year == baravordUser.Year).ToList();

            List<clsTajhizKargahDarsad> lstTajhizKargahDarsad =
                _context.TajhizKargahDarsads.Where(x => x.BarAvordUserId == barAvordId).ToList();

            foreach (var chapter in report)
            {
                foreach (var chap in chapter.Chapters)
                {
                    string strChapterCode = chap.ChapterCode;
                    foreach (var baseItem in chap.BaseItems)
                    {
                        clsFehrestBahaItemsReplace? fehrestBahaItemsReplace = lstFBItemReplaces.FirstOrDefault(x => x.FBShomarehNew == baseItem.ItemCode);
                        if (fehrestBahaItemsReplace != null)
                        {
                            baseItem.ItemCode = fehrestBahaItemsReplace.FBShomarehMain;
                            strChapterCode = fehrestBahaItemsReplace.FBShomarehMain.Substring(0, 2);
                        }

                    }
                    chap.ChapterCode = strChapterCode;
                }
                var tajhiz = lstTajhizKargahDarsad.FirstOrDefault(x => (int)x.NoeFB == chapter.NoeFB && x.Meghdar != 0);
                if (tajhiz != null)
                {
                    chapter.TajhizKargahAmount = tajhiz.Meghdar;
                    chapter.TajhizKargahPercent = tajhiz.Darsad;
                }

                //chapter.Chapters.Sort((x, y) =>
                //    string.Compare(x.ChapterCode, y.ChapterCode, StringComparison.Ordinal));

                chapter.Chapters = chapter.Chapters
                    .OrderBy(x => x.BaseItems.FirstOrDefault()?.ItemCode)
                    .ThenBy(x => x.ChapterCode)
                    .ToList();
            }



            var document =
                new FinancialPdfDocument(
                    report,
                    baravordUser,
                    projectName);

            byte[] pdf = document.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"گزارش مالی_{projectName}.pdf");
        }


        [HttpPost]
        public async Task<IActionResult> ExportBaravordFullPdf(Guid BAId)
        {

            ////////////////////
            //// دفترچه //////////
            ////////////////////

            ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

            Guid barAvordId = BAId;

            var baravordUser = _context.BaravordUsers
                .FirstOrDefault(x => x.ID == barAvordId);

            if (baravordUser == null)
                return NotFound();

            string projectName = baravordUser.Name;

            List<clsZaribPishnahadiPaymanKarFosoul> lstZPPaymanKar = new List<clsZaribPishnahadiPaymanKarFosoul>();


            Guid? CompanyId = Guid.Parse("7D9BD20C-6FD3-458C-0B05-08DE745B0EBA");
            if (baravordUser.Type != NoeBarAvord.BarAvord)
            {
                ///در صورتی که صورت وضعیت میباشد بایستی 
                /////پیمانکار گزارش بگیرد
                ///در نتیجه بایستی آیدی کومپانی موجود باشد
                ///تا بتوان ضریب پیشنهادی پبمانکار را پیدا کرد
                ///

                if (baravordUser.ParentId != null)
                {

                    var parent = await _context.BaravordUsers
                        .FirstAsync(x => x.ID == baravordUser.ParentId);

                    projectName = parent.Name;
                }
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    CompanyId = _context.UserDetails.First(x => x.UserId == user.Id).CompanyId;

                    lstZPPaymanKar = _context.ZaribPishnahadiPaymanKarFosouls.Include(x => x.ZaribPishnahadiPaymanKar)
                                         .Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == baravordUser.ParentId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId).ToList();

                }

                if (CompanyId == null)
                {
                    return null;
                }
            }
            else
                baravordUser.Name = "برآورد اولیه";


            List<ReportDataForFBSummaryDto> reportData = new();

            //decimal tajhizBase = 0;
            //decimal tajhizStar = 0;
            decimal tajhizTotal = 0;

            foreach (string strNoeFB in baravordUser.NoeFBs.Split(','))
            {
                NoeFehrestBaha noeFB = (NoeFehrestBaha)int.Parse(strNoeFB);

                GetFosoulForBAAndSVInputDto request = new()
                {
                    BarAvordUserId = barAvordId,
                    NoeFBId = noeFB,
                    Year = baravordUser.Year
                };

                var result = await showBarAvord.fnGetFosoulForBarAvord(request, _context);

                //---------------------------------------
                // حذف فصل های صفر
                //---------------------------------------
                result.Fosoul = result.Fosoul
                    .Where(x =>
                        x.JameFasl != 0 ||
                        x.JameFaslStar != 0 ||
                        x.JameFaslAll != 0 ||
                        x.JameFaslWithZarib != 0 ||
                        x.JameFaslStarWithZarib != 0 ||
                        x.JameFaslWithZaribAll != 0)
                    .ToList();

                //----------------------------------------------------
                // استخراج تجهیز کارگاه
                //----------------------------------------------------

                var tajhizFasl = result.Fosoul.FirstOrDefault(x => x.Code == "99");

                if (tajhizFasl != null)
                {
                    //tajhizBase += tajhizFasl.JameFaslWithZarib;
                    //tajhizStar += tajhizFasl.JameFaslStarWithZarib;
                    tajhizTotal += tajhizFasl.JameFaslWithZaribAll;

                    // حذف از گزارش فهرست بها
                    result.Fosoul.Remove(tajhizFasl);
                }
                else
                {
                    var tajhiz = _context.TajhizKargahDarsads.FirstOrDefault(x =>
                        x.BarAvordUserId == barAvordId &&
                        x.NoeFB == noeFB);

                    if (tajhiz != null && tajhiz.Meghdar != 0)
                    {
                        tajhizTotal += tajhiz.Meghdar;
                    }
                }
                //----------------------------------------------------
                // جمع فهرست بها (بدون تجهیز)
                //----------------------------------------------------

                decimal Base = result.Fosoul.Sum(x => x.JameFasl);
                decimal Star = result.Fosoul.Sum(x => x.JameFaslStar);
                decimal Total = result.Fosoul.Sum(x => x.JameFaslAll);

                decimal BaseZ = result.Fosoul.Sum(x => x.JameFaslWithZarib);
                decimal StarZ = result.Fosoul.Sum(x => x.JameFaslStarWithZarib);
                decimal TotalZ = result.Fosoul.Sum(x => x.JameFaslWithZaribAll);

                //decimal baseAmount =
                //    result.Fosoul.Sum(x => x.JameFaslWithZarib);

                //decimal starAmount =
                //    result.Fosoul.Sum(x => x.JameFaslStarWithZarib);

                //decimal totalAmount =
                //    result.Fosoul.Sum(x => x.JameFaslWithZaribAll);

                //---------------------------------------
                // نام فهرست بها
                //---------------------------------------

                string fbName = noeFB switch
                {
                    NoeFehrestBaha.RahDari => "راهداری",
                    NoeFehrestBaha.Abnie => "ابنیه",
                    NoeFehrestBaha.RahoBand => "راه، راه‌آهن و باند فرودگاه",
                    NoeFehrestBaha.Barghi => "تأسیسات برقی",
                    NoeFehrestBaha.Mekaniki => "تأسیسات مکانیکی",
                    NoeFehrestBaha.MareMat => "مرمت بناهای تاریخی",
                    _ => ""
                };

                reportData.Add(new ReportDataForFBSummaryDto
                {
                    NoeFBName = fbName,

                    //BaseAmount = baseAmount,

                    //StarAmount = starAmount,

                    //TotalAmount = totalAmount,

                    Base = Base,
                    Star = Star,
                    Total = Total,

                    BaseZ = BaseZ,
                    StarZ = StarZ,
                    TotalZ = TotalZ,

                    Title = projectName,

                    Year = baravordUser.Year
                });
            }

            decimal? sumFBBase = reportData.Sum(x => x.Base);

            decimal? sumFBStar = reportData.Sum(x => x.Star);

            decimal? sumFBTotal = reportData.Sum(x => x.Total);

            decimal? sumFBBaseZ = reportData.Sum(x => x.BaseZ);

            decimal? sumFBStarZ = reportData.Sum(x => x.StarZ);

            decimal? sumFBTotalZ = reportData.Sum(x => x.TotalZ);

            reportData.Add(new ReportDataForFBSummaryDto
            {
                NoeFBName = "جمع کل فهرست بهاها",

                Base = sumFBBase == 0 ? null : sumFBBase,

                Star = sumFBStar == 0 ? null : sumFBStar,

                Total = sumFBTotal == 0 ? null : sumFBTotal,

                BaseZ = sumFBBaseZ == 0 ? null : sumFBBaseZ,

                StarZ = sumFBStarZ == 0 ? null : sumFBStarZ,

                TotalZ = sumFBTotalZ == 0 ? null : sumFBTotalZ,

                Title = baravordUser.Name,

                Year = baravordUser.Year
            });

            reportData.Add(new ReportDataForFBSummaryDto
            {
                NoeFBName = "تجهیز و برچیدن کارگاه",

                //BaseAmount = tajhizBase,

                //StarAmount = tajhizStar,

                TotalZ = tajhizTotal,

                Title = baravordUser.Name,

                Year = baravordUser.Year
            });

            reportData.Add(new ReportDataForFBSummaryDto
            {
                NoeFBName = "جمع نهایی برآورد",

                //BaseAmount = sumFBBase,

                //StarAmount = sumFBStar,

                TotalZ = sumFBTotalZ + tajhizTotal,

                Title = baravordUser.Name,

                Year = baravordUser.Year
            });

            ReportDataForFBSummaryL1Dto reportDataForFB = new ReportDataForFBSummaryL1Dto
            {
                baravordUser = baravordUser,
                reportDataForFBSummary = reportData
            };

            //var document = new FBSummaryPdfDocument(reportDataForFB);


            ////////////////////
            //// فصول //////////
            ////////////////////
            //ShowBarAvordCommon showBarAvord = new ShowBarAvordCommon();

            Guid BarAvordId = BAId;
            //clsBaravordUser? baravordUser = _context.BaravordUsers.FirstOrDefault(x => x.ID == BarAvordId);
            //var baravordUser = await _context.BaravordUsers
            //        .FirstAsync(x => x.ID == BarAvordId);

            int Year = 0;
            string[] noeFBs = [];

            //string projectName = "";
            //Guid? CompanyId = null;

            //List<clsZaribPishnahadiPaymanKarFosoul> lstZPPaymanKar = new List<clsZaribPishnahadiPaymanKarFosoul>();

            //Guid? CompanyId = Guid.Parse("7D9BD20C-6FD3-458C-0B05-08DE745B0EBA");
            if (baravordUser != null)
            {
                Year = baravordUser.Year;
                noeFBs = baravordUser.NoeFBs.Split(',');
                if (baravordUser.Type != NoeBarAvord.BarAvord)
                {
                    ///در صورتی که صورت وضعیت میباشد بایستی 
                    /////پیمانکار گزارش بگیرد
                    ///در نتیجه بایستی آیدی کومپانی موجود باشد
                    ///تا بتوان ضریب پیشنهادی پبمانکار را پیدا کرد
                    ///

                    if (baravordUser.ParentId != null)
                    {

                        var parent = await _context.BaravordUsers
                            .FirstAsync(x => x.ID == baravordUser.ParentId);

                        projectName = parent.Name;
                    }
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        CompanyId = _context.UserDetails.First(x => x.UserId == user.Id).CompanyId;

                        lstZPPaymanKar = _context.ZaribPishnahadiPaymanKarFosouls.Include(x => x.ZaribPishnahadiPaymanKar)
                                             .Where(x => x.ZaribPishnahadiPaymanKar.BarAvordUserId == baravordUser.ParentId && x.ZaribPishnahadiPaymanKar.CompanyId == CompanyId).ToList();

                    }

                    if (CompanyId == null)
                    {
                        return null;
                    }
                }
                else
                    baravordUser.Name = "برآورد اولیه";
            }
            else
                return null;



            List<ReportDataForFosoulForBarAvordDto> lstReportData = new List<ReportDataForFosoulForBarAvordDto>();
            if (baravordUser != null)
            {
                string[] strNoeFB = baravordUser.NoeFBs.Split(",");
                foreach (var NoeFB in strNoeFB)
                {
                    GetFosoulForBAAndSVInputDto request = new GetFosoulForBAAndSVInputDto
                    {
                        BarAvordUserId = BarAvordId,
                        NoeFBId = (NoeFehrestBaha)int.Parse(NoeFB),
                        Year = baravordUser.Year,
                    };
                    ResultGetFosoulForBarAvordDto Result = await showBarAvord.fnGetFosoulForBarAvord(request, _context);


                    Result.Fosoul = Result.Fosoul
                        .Where(x =>
                            x.JameFasl != 0 ||
                            x.JameFaslStar != 0 ||
                            x.JameFaslAll != 0 ||
                            x.JameFaslWithZarib != 0 ||
                            x.JameFaslStarWithZarib != 0 ||
                            x.JameFaslWithZaribAll != 0)
                        .ToList();

                    string strNoeFBName = "";
                    switch ((NoeFehrestBaha)int.Parse(NoeFB))
                    {
                        case NoeFehrestBaha.RahDari:
                            strNoeFBName = "راهداری";
                            break;
                        case NoeFehrestBaha.Abnie:
                            strNoeFBName = "ابنیه";
                            break;
                        case NoeFehrestBaha.RahoBand:
                            strNoeFBName = "راه، راه‌آهن و باند فرودگاه";
                            break;
                        case NoeFehrestBaha.Barghi:
                            strNoeFBName = "تاسیسات برقی";
                            break;
                        case NoeFehrestBaha.Mekaniki:
                            strNoeFBName = "تاسیسات مکانیکی";
                            break;
                        case NoeFehrestBaha.MareMat:
                            strNoeFBName = "مرمت بناهای تاریخی";
                            break;
                        default:
                            break;
                    }

                    clsTajhizKargahDarsad? TajhizKargahDarsad = _context.TajhizKargahDarsads.FirstOrDefault(x => x.BarAvordUserId == request.BarAvordUserId && x.NoeFB == (NoeFehrestBaha)int.Parse(NoeFB));

                    decimal? MeghdarTajhiz = null;
                    if (TajhizKargahDarsad != null)
                    {
                        MeghdarTajhiz = TajhizKargahDarsad.Meghdar;
                    }

                    if (MeghdarTajhiz.HasValue && MeghdarTajhiz.Value != 0)
                    {
                        Result.Fosoul.Add(new GetFosoulOutputDto
                        {
                            Id = 0,
                            Order = 300,
                            FaslName = "پیوست 3 - دستورالعمل تجهیز و برچیدن کارگاه",

                            JameFasl = 0,
                            JameFaslStar = 0,
                            JameFaslAll = 0,

                            JameFaslWithZarib = 0,
                            JameFaslStarWithZarib = 0,
                            JameFaslWithZaribAll = MeghdarTajhiz.Value,

                            FaslAsli = true
                        });
                    }

                    Result.Fosoul = Result.Fosoul
                            .OrderBy(x => x.Order)
                            .ToList();

                    var reportDate = new ReportDataForFosoulForBarAvordDto
                    {
                        resultGetFosoulForBarAvord = Result,
                        NoeFBName = strNoeFBName,
                        Title = projectName,
                        Year=baravordUser.Year
                    };

                    lstReportData.Add(reportDate);

                }
            }

            //var summaryData = lstReportData;


            ////////////////////
            ////خلاصه مالی
            ///////////////////
            ///
            //Guid barAvordId = Guid.Parse(BAId);







            #region ریز متره ها

            List<RizMetreForReportDto> allItemsWithRizMetre =
                await _context.RizMetreUserses
                .Include(x => x.FB)
                .ThenInclude(x => x.BaravordUser)
                .Where(x => x.FB.BarAvordId == BarAvordId)
                .Select(x => new RizMetreForReportDto
                {
                    Code = x.ShomarehNew,
                    Sharh = x.Sharh,
                    Tedad = x.Tedad,
                    Tool = x.Tool,
                    Arz = x.Arz,
                    Ertefa = x.Ertefa,
                    Vazn = x.Vazn,
                    JameJoz = x.MeghdarJoz,
                    NoeFB = (int)x.FB.NoeFBId,
                    NoeFBName =
                        x.FB.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" :
                        x.FB.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری" :
                        x.FB.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" :
                        x.FB.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                        x.FB.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" :
                        x.FB.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
                    Year = x.FB.BaravordUser.Year,
                    ItemShomareh = x.FB.Shomareh,
                    BahayeVahedNew = x.FB.BahayeVahedNew
                })
                .OrderBy(x => x.NoeFB)
                .ThenBy(x => x.ItemShomareh)
                .ToListAsync();

            #endregion



            var allItemsWithRizMetre1 =
    (from itemRM in allItemsWithRizMetre
     join replace in _context.FehrestBahaItemsReplaces
     on new
     {
         FBShomarehMain = itemRM.ItemShomareh,
         NoeFB = (NoeFehrestBaha)itemRM.NoeFB,
         itemRM.Year
     }
     equals new
     {
         replace.FBShomarehMain,
         replace.NoeFB,
         replace.Year
     }
     into gj
     from replace in gj.DefaultIfEmpty()
     select new RizMetreForReportDto
     {
         Code = itemRM.Code,
         Sharh = itemRM.Sharh,
         Tedad = itemRM.Tedad,
         Tool = itemRM.Tool,
         Arz = itemRM.Arz,
         Ertefa = itemRM.Ertefa,
         Vazn = itemRM.Vazn,
         JameJoz = itemRM.JameJoz,
         NoeFB = itemRM.NoeFB,
         NoeFBName = itemRM.NoeFBName,
         Year = itemRM.Year,

         ShomarehMain = itemRM.ItemShomareh,
         BahayeVahedNew = itemRM.BahayeVahedNew,

         ItemShomareh = replace != null
             ? replace.FBShomarehNew
             : itemRM.ItemShomareh
     }).ToList();

            #region آیتم های ستاره دار

            List<RizMetreForReportDto> lstItemFBStars =
                await _context.RizMetreStars
                .Include(x => x.ItemFBStar)
                .ThenInclude(x => x.Vahed)
                .Where(x => x.ItemFBStar.BaravordId == BarAvordId)
                .Select(i => new RizMetreForReportDto
                {
                    JameJoz = i.MeghdarJoz,
                    NoeFB = (int)i.ItemFBStar.NoeFBId,
                    NoeFBName =
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه‌آهن و باند فرودگاه" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" :
                        i.ItemFBStar.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",

                    Year = i.ItemFBStar.BaravordUser.Year,

                    ItemShomareh = i.ItemFBStar.Shomareh,

                    ShomarehMain = i.ItemFBStar.Shomareh,

                    ItemSharh = i.ItemFBStar.Sharh,

                    BahayeVahed = i.ItemFBStar.BahayeVahed,

                    BahayeVahedNew = i.ItemFBStar.BahayeVahed,

                    Vahed = i.ItemFBStar.Vahed.Name,

                    BahayeKol = i.ItemFBStar.BahayeVahed * i.MeghdarJoz
                })
                .ToListAsync();

            #endregion

            #region فهرست بها

            var fehrest =
                await (from fb in _context.FehrestBahas
                       join rep in _context.FehrestBahaItemsReplaces
                       on fb.Shomareh equals rep.FBShomarehMain into grp
                       from rep in grp.DefaultIfEmpty()

                       where fb.Sal == Year

                       select new
                       {
                           fb.Shomareh,
                           fb.Sharh,
                           fb.Vahed,
                           fb.BahayeVahed,
                           fb.NoeFB,
                           fb.Sal,

                           ShomarehMain = fb.Shomareh,

                           NewCode = rep != null
                               ? rep.FBShomarehNew
                               : fb.Shomareh

                       }).ToListAsync();

            #endregion

            #region اتصال ریز متره به آیتم

            var items =
                (from f in fehrest
                 join r in allItemsWithRizMetre1
                 on f.ShomarehMain equals r.ShomarehMain.Substring(0, 6)

                 where
                     f.NoeFB == (NoeFehrestBaha)r.NoeFB &&
                     f.Sal == r.Year

                 select new RizMetreForReportDto
                 {
                     NoeFB = r.NoeFB,
                     NoeFBName = r.NoeFBName,
                     ItemShomareh = r.ItemShomareh,
                     ShomarehMain = f.ShomarehMain!= r.ItemShomareh? r.ItemShomareh: f.ShomarehMain,

                     ItemSharh = f.Sharh,

                     Vahed = f.Vahed,

                     JameJoz = r.JameJoz,

                     BahayeVahed =
                        (r.BahayeVahedNew == null || r.BahayeVahedNew == 0)
                        ? decimal.Parse(f.BahayeVahed ?? "0")
                        : r.BahayeVahedNew,

                     BahayeKol =
                        ((r.BahayeVahedNew == null || r.BahayeVahedNew == 0)
                        ? decimal.Parse(f.BahayeVahed ?? "0")
                        : r.BahayeVahedNew) * r.JameJoz

                 }).ToList();

            items.AddRange(lstItemFBStars);

            #endregion



            #region فصل ها


            List<FosoulForFinancialDto> fosoul = await
            (
                from fosoulItem in _context.Fosouls

                join zarib in _context.ZarayebFosouls
                    on fosoulItem.Id equals zarib.FaslId into zaribGroup

                from zarib in zaribGroup.DefaultIfEmpty()

                where fosoulItem.Year == Year
                      && noeFBs.Contains(((int)fosoulItem.NoeFB).ToString())

                orderby fosoulItem.NoeFB

                select new FosoulForFinancialDto
                {
                    Code = fosoulItem.Code,
                    Description = fosoulItem.Description,
                    Id = fosoulItem.Id,
                    LatinName = fosoulItem.LatinName,
                    Name = fosoulItem.Name,
                    NoeFB = fosoulItem.NoeFB,
                    order = fosoulItem.order,
                    Year = fosoulItem.Year,

                    ZaribBalasari = zarib != null ? zarib.ZaribBalaSari : null,
                    ZaribMantaghe = zarib != null ? zarib.ZaribManteghe : null
                }
            ).ToListAsync();

            foreach (var fasl in fosoul)
            {
                ////محاسبه ضرایب
                RequestGetZarayebDto requestGetZarayeb = new RequestGetZarayebDto
                {
                    BarAvordId = BarAvordId,
                    NoeFBId = fasl.NoeFB,
                    Year = Year
                };
                ResultGetZarayebDto result = RizMetreCommon.GetZarayeb(requestGetZarayeb, _context);
                decimal zaribBalaSari = result.zaribBalaSari != null ? result.zaribBalaSari.Value : 1;
                decimal zaribManteghe = result.zaribManteghe != null ? result.zaribManteghe.Value : 1;

                fasl.ZaribBalasari = fasl.ZaribBalasari == null ? zaribBalaSari : fasl.ZaribBalasari;
                fasl.ZaribMantaghe = fasl.ZaribMantaghe == null ? zaribManteghe : fasl.ZaribMantaghe;

                clsZaribPishnahadiPaymanKarFosoul? ZPPaymanKar = lstZPPaymanKar.FirstOrDefault(x => x.Code == fasl.Code);
                if (ZPPaymanKar != null)
                {
                    fasl.ZaribPishnahadiPK = ZPPaymanKar.ZaribJoz;
                }

            }




            #endregion

            #region مدل گزارش

            var report = fosoul
                .GroupBy(f => new
                {
                    f.NoeFB
                })
                .Select(gNoeFB => new FinancialNoeFBVm
                {
                    NoeFB = (int)gNoeFB.Key.NoeFB,

                    NoeFBName = gNoeFB.Key.NoeFB switch
                    {
                        NoeFehrestBaha.RahoBand => "راه، راه‌آهن و باند فرودگاه",

                        NoeFehrestBaha.RahDari => "راهداری",

                        NoeFehrestBaha.Abnie => "ابنیه",

                        NoeFehrestBaha.Barghi => "تأسیسات برقی",

                        NoeFehrestBaha.Mekaniki => "تأسیسات مکانیکی",

                        NoeFehrestBaha.MareMat => "مرمت بناهای تاریخی",

                        _ => ""
                    },

                    Chapters = gNoeFB
                        .OrderBy(x => x.Code)
                        .Select(f => new FinancialChapterVm
                        {
                            ChapterCode = f.Code,

                            ChapterName = f.Name,

                            NoeFB = (int)f.NoeFB,
                            ZaribBalasari = f.ZaribBalasari,
                            ZaribMantaghe = f.ZaribMantaghe,
                            ZaribPishnahadiPK = f.ZaribPishnahadiPK,

                            BaseItems = items
                                .Where(x =>
                                    x.NoeFB == (int)f.NoeFB &&
                                    x.ItemShomareh.StartsWith(f.Code))
                                .GroupBy(x => new
                                {
                                    x.ItemShomareh,
                                    //x.ShomarehMain,
                                    x.ItemSharh,
                                    x.Vahed,
                                    x.BahayeVahed,
                                    x.BahayeVahedNew
                                })
                                .Select(g => new FinancialItemVm
                                {
                                    ItemCode = g.Key.ItemShomareh,

                                    ItemSharh = g.Key.ItemSharh,

                                    Vahed = g.Key.Vahed,

                                    BahayeVahed = g.Key.BahayeVahed,

                                    IsStarItem =
                                        g.Key.BahayeVahedNew != null &&
                                        g.Key.BahayeVahedNew != 0,

                                    Meghdar = g.Sum(x => x.JameJoz ?? 0),

                                    BahayeKol = g.Sum(x => x.BahayeKol ?? 0)
                                })
                                .Where(x => !x.IsStarItem)
                                .OrderBy(x => x.ItemCode)
                                .ToList(),

                            StarItems = items
                                .Where(x =>
                                    x.NoeFB == (int)f.NoeFB &&
                                    x.ShomarehMain.StartsWith(f.Code))
                                .GroupBy(x => new
                                {
                                    x.ShomarehMain,
                                    x.ItemSharh,
                                    x.Vahed,
                                    x.BahayeVahed,
                                    x.BahayeVahedNew
                                })
                                .Select(g => new FinancialItemVm
                                {
                                    ItemCode = g.Key.ShomarehMain,

                                    ItemSharh = g.Key.ItemSharh,

                                    Vahed = g.Key.Vahed,

                                    BahayeVahed = g.Key.BahayeVahed,

                                    IsStarItem =
                                        g.Key.BahayeVahedNew != null &&
                                        g.Key.BahayeVahedNew != 0,

                                    Meghdar = g.Sum(x => x.JameJoz ?? 0),

                                    BahayeKol = g.Sum(x => x.BahayeKol ?? 0)
                                })
                                .Where(x => x.IsStarItem)
                                .OrderBy(x => x.ItemCode)
                                .ToList()

                        })
                        .ToList()

                })
                .OrderBy(x => x.NoeFB)
                .ToList();

            #endregion

            List<clsFehrestBahaItemsReplace> lstFBItemReplaces = _context.FehrestBahaItemsReplaces.Where(x => x.Year == Year).ToList();

            List<clsTajhizKargahDarsad> lstTajhizKargahDarsad =
                _context.TajhizKargahDarsads.Where(x => x.BarAvordUserId == BarAvordId).ToList();

            foreach (var chapter in report)
            {
                foreach (var chap in chapter.Chapters)
                {
                    string strChapterCode = chap.ChapterCode;
                    foreach (var baseItem in chap.BaseItems)
                    {
                        clsFehrestBahaItemsReplace? fehrestBahaItemsReplace = lstFBItemReplaces.FirstOrDefault(x => x.FBShomarehNew == baseItem.ItemCode);
                        if (fehrestBahaItemsReplace != null)
                        {
                            baseItem.ItemCode = fehrestBahaItemsReplace.FBShomarehMain;
                            strChapterCode = fehrestBahaItemsReplace.FBShomarehMain.Substring(0, 2);
                        }

                    }
                    chap.ChapterCode = strChapterCode;
                }
                var tajhiz = lstTajhizKargahDarsad.FirstOrDefault(x => (int)x.NoeFB == chapter.NoeFB && x.Meghdar != 0);
                if (tajhiz != null)
                {
                    chapter.TajhizKargahAmount = tajhiz.Meghdar;
                    chapter.TajhizKargahPercent = tajhiz.Darsad;
                }

                //chapter.Chapters.Sort((x, y) =>
                //    string.Compare(x.ChapterCode, y.ChapterCode, StringComparison.Ordinal));

                chapter.Chapters = chapter.Chapters
                    .OrderBy(x => x.BaseItems.FirstOrDefault()?.ItemCode)
                    .ThenBy(x => x.ChapterCode)
                    .ToList();
            }

            /////////////////////
            //////// ریزمتره
            /////////////////////

            //List<RizMetreForReportDto> allItemsWithRizMetre = _context.RizMetreUserses
            //    .Include(x => x.FB).ThenInclude(x => x.BaravordUser)
            //    .Where(x => x.FB.BarAvordId == BarAvordId)
            //    .Select(x => new RizMetreForReportDto
            //    {
            //        Code = x.ShomarehNew,
            //        Sharh = x.Sharh,
            //        Tedad = x.Tedad,
            //        Tool = x.Tool,
            //        Arz = x.Arz,
            //        Ertefa = x.Ertefa,
            //        Vazn = x.Vazn,
            //        JameJoz = x.MeghdarJoz,
            //        NoeFB = (int)x.FB.NoeFBId,
            //        NoeFBName = x.FB.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه آهن و باند فرودگاه" : x.FB.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری"
            //                                  : x.FB.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" : x.FB.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
            //                                  x.FB.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" : x.FB.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
            //        Year = x.FB.BaravordUser.Year,
            //        ItemShomareh = x.FB.Shomareh,
            //        BahayeVahedNew = x.FB.BahayeVahedNew,
            //        Tozihat = x.Des
            //    })
            //    .OrderBy(x => x.NoeFB)
            //    .ThenBy(x => x.ItemShomareh)
            //    .ToList();




            //List<RizMetreForReportDto> lstItemFBStars = await _context.RizMetreStars.Include(x => x.ItemFBStar).ThenInclude(x => x.Vahed).Where(x => x.ItemFBStar.BaravordId == BarAvordId).Select(i => new RizMetreForReportDto
            //{
            //    Code = i.ShomarehNew,
            //    Sharh = i.Sharh,
            //    Tedad = i.Tedad,
            //    Tool = i.Tool,
            //    Arz = i.Arz,
            //    Ertefa = i.Ertefa,
            //    Vazn = i.Vazn,
            //    JameJoz = i.MeghdarJoz,
            //    NoeFB = (int)i.ItemFBStar.NoeFBId,
            //    NoeFBName = i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahoBand ? "راه، راه آهن و باند فرودگاه" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.RahDari ? "راهداری"
            //                                  : i.ItemFBStar.NoeFBId == NoeFehrestBaha.Abnie ? "ابنیه" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.Barghi ? "تاسیسات برقی" :
            //                                  i.ItemFBStar.NoeFBId == NoeFehrestBaha.Mekaniki ? "تاسیسات مکانیکی" : i.ItemFBStar.NoeFBId == NoeFehrestBaha.MareMat ? "مرمت بناهای تاریخی" : "",
            //    Year = i.ItemFBStar.BaravordUser.Year,
            //    ItemShomareh = i.ItemFBStar.Shomareh,
            //    ShomarehMain = i.ItemFBStar.Shomareh,
            //    ItemSharh = i.ItemFBStar.Sharh,
            //    BahayeVahedNew = i.ItemFBStar.BahayeVahed,
            //    BahayeVahed = i.ItemFBStar.BahayeVahed,
            //    BahayeKol = (i.ItemFBStar.BahayeVahed == 0 ?
            //                      i.ItemFBStar.BahayeVahed : i.ItemFBStar.BahayeVahed) * i.MeghdarJoz,
            //    Vahed = i.ItemFBStar.Vahed.Name,
            //    Tozihat = i.Des
            //}).ToListAsync();


            //var fehrest = await _context.FehrestBahas.Where(x=>x.Sal== BaravordUser.Year).ToListAsync();

            //var fehrest1 = await (from FehrestBaha in _context.FehrestBahas
            //                     join FehrestBahaItemsReplace in _context.FehrestBahaItemsReplaces
            //                     on FehrestBaha.Shomareh equals FehrestBahaItemsReplace.FBShomarehMain into joinGroup
            //                     from FehrestBahaItemsReplace in joinGroup.DefaultIfEmpty()
            //                     where FehrestBaha.Sal == Year
            //                     select new
            //                     {
            //                         Id = FehrestBaha.Id,
            //                         BahayeVahed = FehrestBaha.BahayeVahed,
            //                         Vahed = FehrestBaha.Vahed,
            //                         Sharh = FehrestBaha.Sharh,
            //                         ShomarehMain = FehrestBaha.Shomareh,
            //                         Shomareh = FehrestBahaItemsReplace != null && FehrestBahaItemsReplace.FBShomarehNew != null
            //                                                   ? FehrestBahaItemsReplace.FBShomarehNew
            //                                                   : FehrestBaha.Shomareh,
            //                         Sal = FehrestBaha.Sal,
            //                         NoeFB = FehrestBaha.NoeFB,
            //                     }).ToListAsync();

            List<RizMetreForReportDto> lstFehrestBahas =
                (from f in fehrest
                 join i in allItemsWithRizMetre
                 on f.ShomarehMain equals i.ItemShomareh.Substring(0, 6)
                 where f.Sal == i.Year
                    && f.NoeFB == (NoeFehrestBaha)i.NoeFB
                 select new RizMetreForReportDto
                 {
                     Code = i.Code,
                     Sharh = i.Sharh,
                     Tedad = i.Tedad,
                     Tool = i.Tool,
                     Arz = i.Arz,
                     Ertefa = i.Ertefa,
                     Vazn = i.Vazn,
                     JameJoz = i.JameJoz,
                     NoeFB = i.NoeFB,
                     NoeFBName = i.NoeFBName,
                     Year = i.Year,
                     ItemShomareh = i.ItemShomareh,
                     ShomarehMain = f.ShomarehMain!= i.ItemShomareh? i.ItemShomareh: f.ShomarehMain,
                     ItemSharh = f.Sharh,
                     Vahed = f.Vahed,
                     BahayeVahedNew = i.BahayeVahedNew,
                     BahayeVahed = (i.BahayeVahedNew == null || i.BahayeVahedNew == 0) ?
                                    decimal.Parse(f.BahayeVahed ?? "0") : i.BahayeVahedNew,
                     BahayeKol = ((i.BahayeVahedNew == null || i.BahayeVahedNew == 0) ?
                                  decimal.Parse(f.BahayeVahed ?? "0") : i.BahayeVahedNew) * i.JameJoz,
                     Tozihat = i.Tozihat
                 }).ToList();

            lstFehrestBahas.AddRange(lstItemFBStars);

            List<RizMetreForReportDto> lstFehrestBahas1 = new List<RizMetreForReportDto>();

            lstFehrestBahas1.AddRange(
            lstFehrestBahas.OrderBy(x => x.ShomarehMain).ThenBy(x => x.Code));



            var reportGroups = lstFehrestBahas1
             .GroupBy(x => new
             {
                 x.NoeFB,
                 x.NoeFBName,
                 x.Year
             })
             .Select(group => new RizMetreReportGroupVm
             {
                 NoeFB = group.Key.NoeFB,
                 NoeFBName = group.Key.NoeFBName,
                 Year = group.Key.Year,

                 Items = group
                     .GroupBy(x => new
                     {
                         x.ShomarehMain,
                         x.ItemSharh,
                         x.BahayeVahed,
                         x.BahayeVahedNew,
                         x.Vahed
                     })
                     .Select(g => new RizMetreReportItemVm
                     {
                         NoeFBName = group.Key.NoeFBName,

                         ItemShomareh = g.Key.ShomarehMain,

                         ItemSharh = g.Key.ItemSharh,

                         ItemBahayeVahed = g.Key.BahayeVahed,

                         ItemBahayeVahedNew = g.Key.BahayeVahedNew,

                         ItemVahed = g.Key.Vahed,

                         Details = g.ToList()
                     })
                     .ToList()
             })
             .OrderBy(x => x.NoeFB)
             .ToList();

            ////////////////////

            var doc = new BaravordFullPdfDocument(
                reportGroups,
                report,
                lstReportData,
                reportDataForFB,
                baravordUser,
                projectName);

            var pdf = doc.GeneratePdf();

            return File(
                pdf,
                "application/pdf",
                $"گزارش کلی_{projectName}.pdf");
        }
        //public IActionResult ItemsWithRizMetre()
        // {
        //     return View();
        // }


    }
}