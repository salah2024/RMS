using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using RMS.Controllers.Report.Tajmie.Dto;
using RMS.Controllers.Report.Tajmie.Generator;
using RMS.Controllers.Tajmie;
using RMS.Controllers.Tajmie.Dto;
using RMS.Models.Entity;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Report.Tajmie;

public class TajmieReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private readonly ApplicationDbContext _context = context;
    public IActionResult Index()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("_viewReportForm");


        return View();
    }


    [HttpGet]
    public async Task<IActionResult> GetPdfReportTajmieRiz(
        long Year,
        Guid BarAvordId)
    {
        try
        {
            // ============================================================
            // 1. دریافت برآورد
            // ============================================================

            clsBaravordUser? baravordUser =
                await _context.BaravordUsers
                    .FirstOrDefaultAsync(x => x.ID == BarAvordId);

            if (baravordUser == null)
            {
                return NotFound(
                    "برآورد مورد نظر یافت نشد.");
            }


            // ============================================================
            // 2. بررسی فهرست بها
            // ============================================================

            if (string.IsNullOrWhiteSpace(baravordUser.NoeFBs))
            {
                return NotFound(
                    "فهرست بهایی برای این برآورد تعریف نشده است.");
            }


            // ============================================================
            // 3. تبدیل NoeFBs به لیست
            // ============================================================

            List<NoeFehrestBaha> noeFBs =
                baravordUser.NoeFBs
                    .Split(
                        ',',
                        StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => int.TryParse(x, out _))
                    .Select(x => (NoeFehrestBaha)int.Parse(x))
                    .Distinct()
                    .ToList();

            if (noeFBs.Count == 0)
            {
                return NotFound(
                    "فهرست بهای معتبر برای این برآورد یافت نشد.");
            }


            // ============================================================
            // 4. ساخت سرویس تجمیع
            // ============================================================

            TajmieCommon tajmieCommon =
                new TajmieCommon(_context);


            // ============================================================
            // 5. ساخت گزارش اصلی
            // ============================================================

            var fullReport =
                new TajmieFullReportDto
                {
                    Year = Year,

                    ProjectTitle =
                        baravordUser.Name ?? "",

                    ShomarehPeyman =
                        baravordUser.Num.ToString(),

                    TarikhPeyman =
                        baravordUser.FromDate ?? "",

                    Groups =
                        new List<TajmieReportGroupDto>()
                };


            // ============================================================
            // 6. دریافت اطلاعات هر فهرست بها
            // ============================================================

            foreach (NoeFehrestBaha noeFBId in noeFBs)
            {
                var result =
                    await tajmieCommon.TajmieFosoulAndSub(
                        Year,
                        BarAvordId,
                        noeFBId);


                // --------------------------------------------------------
                // اطلاعاتی وجود ندارد
                // --------------------------------------------------------

                if (result == null ||
                    result.Fosoul == null ||
                    result.Fosoul.Count == 0)
                {
                    continue;
                }


                // --------------------------------------------------------
                // فقط فصل‌هایی که ریز دارند
                // --------------------------------------------------------

                var validFosoul =
                    result.Fosoul
                        .Where(fasl =>
                            fasl.Items != null &&
                            fasl.Items.Any(item =>
                                item.Details != null &&
                                item.Details.Any()))
                        .ToList();


                if (validFosoul.Count == 0)
                {
                    continue;
                }


                // --------------------------------------------------------
                // اضافه کردن گروه
                // --------------------------------------------------------

                fullReport.Groups.Add(
                    new TajmieReportGroupDto
                    {
                        NoeFB =
                            noeFBId,

                        NoeFBName =
                            GetNoeFehrestBahaName(
                                noeFBId),

                        Year =
                            Year,

                        Fosoul =
                            validFosoul,

                        TotalBahayeKol =
                            validFosoul.Sum(
                                x => x.BahayeKol),

                        TotalWeightPercent =
                            validFosoul.Sum(
                                x => x.WeightPercent)
                    });
            }


            // ============================================================
            // 7. بررسی نهایی
            // ============================================================

            if (fullReport.Groups.Count == 0)
            {
                return NotFound(
                    "هیچ اطلاعات ریزمتره‌ای برای تهیه گزارش یافت نشد.");
            }

            // ============================================================
            // محاسبه صحیح درصدهای وزنی گزارش
            // ============================================================

            CalculateWeightPercentages(fullReport);

            // ============================================================
            // 8. ساخت PDF
            // ============================================================

            var document =
                new TajmiePdfDocument(
                    fullReport);


            byte[] pdfBytes =
                document.GeneratePdf();


            // ============================================================
            // 9. نام فایل
            // ============================================================

            string fileName =
                $"گزارش-تجمیعی-ریز-" +
                $"{DateTime.Now:yyyyMMdd-HHmmss}.pdf";


            // ============================================================
            // 10. خروجی
            // ============================================================

            return File(
                pdfBytes,
                "application/pdf",
                fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new
                {
                    success = false,

                    message =
                        "خطا در تهیه گزارش تجمیعی ریز",

                    error =
                        ex.Message,

                    innerError =
                        ex.InnerException?.Message
                });
        }
    }

    private void CalculateWeightPercentages(
    TajmieFullReportDto report)
    {
        // ============================================================
        // جمع کل بهای تمام آیتم‌ها
        // ============================================================

        decimal totalBahayeKol =
            report.Groups
                .SelectMany(g => g.Fosoul ?? new List<TajmieReportFaslDto>())
                .SelectMany(f => f.Items ?? new List<TajmieReportItemDto>())
                .Sum(i => i.BahayeKol);


        if (totalBahayeKol <= 0)
        {
            // اگر جمع کل صفر باشد، درصدها صفر می‌شوند
            foreach (var group in report.Groups)
            {
                group.TotalWeightPercent = 0;

                foreach (var fasl in group.Fosoul)
                {
                    fasl.WeightPercent = 0;

                    foreach (var item in fasl.Items)
                    {
                        item.WeightPercent = 0;

                        foreach (var detail in item.Details)
                        {
                            detail.WeightPercent = 0;
                        }
                    }
                }
            }

            return;
        }


        // ============================================================
        // محاسبه درصد وزنی
        // ============================================================

        foreach (var group in report.Groups)
        {
            // --------------------------------------------------------
            // جمع بهای گروه
            // --------------------------------------------------------

            group.TotalBahayeKol =
                group.Fosoul
                    .SelectMany(f =>
                        f.Items ??
                        new List<TajmieReportItemDto>())
                    .Sum(i => i.BahayeKol);


            // --------------------------------------------------------
            // درصد وزنی گروه
            // --------------------------------------------------------

            group.TotalWeightPercent =
                Math.Round(
                    group.TotalBahayeKol /
                    totalBahayeKol *
                    100m,
                    2);


            // --------------------------------------------------------
            // فصل‌ها
            // --------------------------------------------------------

            foreach (var fasl in group.Fosoul)
            {
                // ----------------------------------------------------
                // جمع واقعی فصل
                // ----------------------------------------------------

                fasl.BahayeKol =
                    fasl.Items
                        .Sum(i => i.BahayeKol);


                // ----------------------------------------------------
                // درصد وزنی فصل
                // ----------------------------------------------------

                fasl.WeightPercent =
                    Math.Round(
                        fasl.BahayeKol /
                        totalBahayeKol *
                        100m,
                        2);


                // ----------------------------------------------------
                // آیتم‌ها
                // ----------------------------------------------------

                foreach (var item in fasl.Items)
                {
                    item.WeightPercent =
                        Math.Round(
                            item.BahayeKol /
                            totalBahayeKol *
                            100m,
                            2);


                    // ------------------------------------------------
                    // ریزها
                    // ------------------------------------------------

                    if (item.Details == null ||
                        item.Details.Count == 0)
                    {
                        continue;
                    }


                    foreach (var detail in item.Details)
                    {
                        detail.WeightPercent =
                            Math.Round(
                                detail.BahayeKol /
                                totalBahayeKol *
                                100m,
                                2);
                    }
                }
            }
        }
    }

    private void CalculateItemWeightPercentages(
    TajmieFullReportDto report)
    {
        // ============================================================
        // جمع کل بهای آیتم‌ها
        // ============================================================

        decimal totalBahayeKol =
            report.Groups
                .SelectMany(x =>
                    x.Fosoul ??
                    new List<TajmieReportFaslDto>())
                .SelectMany(x =>
                    x.Items ??
                    new List<TajmieReportItemDto>())
                .Sum(x => x.BahayeKol);


        // ============================================================
        // اگر جمع صفر باشد
        // ============================================================

        if (totalBahayeKol <= 0)
        {
            foreach (var group in report.Groups)
            {
                foreach (var fasl in group.Fosoul)
                {
                    foreach (var item in fasl.Items)
                    {
                        item.WeightPercent = 0;
                    }
                }
            }

            return;
        }


        // ============================================================
        // درصد وزنی آیتم‌ها
        // ============================================================

        foreach (var group in report.Groups)
        {
            foreach (var fasl in group.Fosoul)
            {
                foreach (var item in fasl.Items)
                {
                    item.WeightPercent =
                        Math.Round(
                            item.BahayeKol /
                            totalBahayeKol *
                            100m,
                            2,
                            MidpointRounding.AwayFromZero);
                }
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPdfReportTajmieFasl(
        long Year,
        Guid BarAvordId)
    {
        try
        {
            // ============================================================
            // 1. دریافت برآورد
            // ============================================================

            clsBaravordUser? baravordUser =
                await _context.BaravordUsers
                    .FirstOrDefaultAsync(
                        x => x.ID == BarAvordId);

            if (baravordUser == null)
            {
                return NotFound(
                    "برآورد مورد نظر یافت نشد.");
            }


            // ============================================================
            // 2. بررسی فهرست بها
            // ============================================================

            if (string.IsNullOrWhiteSpace(
                baravordUser.NoeFBs))
            {
                return NotFound(
                    "فهرست بهایی برای این برآورد تعریف نشده است.");
            }


            // ============================================================
            // 3. تبدیل NoeFBs به لیست
            // ============================================================

            List<NoeFehrestBaha> noeFBs =
                baravordUser.NoeFBs
                    .Split(
                        ',',
                        StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x =>
                        int.TryParse(x, out _))
                    .Select(x =>
                        (NoeFehrestBaha)
                        int.Parse(x))
                    .Distinct()
                    .ToList();


            if (noeFBs.Count == 0)
            {
                return NotFound(
                    "فهرست بهای معتبر برای این برآورد یافت نشد.");
            }


            // ============================================================
            // 4. سرویس تجمیع
            // ============================================================

            TajmieCommon tajmieCommon =
                new TajmieCommon(_context);


            // ============================================================
            // 5. ساخت گزارش کامل
            // ============================================================

            var fullReport =
                new TajmieFullReportDto
                {
                    Year = Year,

                    ProjectTitle =
                        baravordUser.Name ?? "",

                    ShomarehPeyman =
                        baravordUser.Num.ToString(),

                    TarikhPeyman =
                        baravordUser.FromDate ?? "",

                    Groups =
                        new List<TajmieReportGroupDto>()
                };


            // ============================================================
            // 6. دریافت اطلاعات هر فهرست بها
            // ============================================================

            foreach (var noeFBId in noeFBs)
            {
                var result =
                    await tajmieCommon.TajmieFosoulAndSub(
                        Year,
                        BarAvordId,
                        noeFBId);


                if (result == null ||
                    result.Fosoul == null ||
                    result.Fosoul.Count == 0)
                {
                    continue;
                }


                // فقط فصل‌هایی که آیتم دارند
                var validFosoul =
                    result.Fosoul
                        .Where(x =>
                            x.Items != null &&
                            x.Items.Any())
                        .ToList();


                if (validFosoul.Count == 0)
                    continue;


                fullReport.Groups.Add(
                    new TajmieReportGroupDto
                    {
                        NoeFB =
                            noeFBId,

                        NoeFBName =
                            GetNoeFehrestBahaName(
                                noeFBId),

                        Year =
                            Year,

                        Fosoul =
                            validFosoul,

                        TotalBahayeKol =
                            validFosoul.Sum(
                                x => x.BahayeKol)
                    });
            }


            // ============================================================
            // 7. بررسی نهایی
            // ============================================================

            if (fullReport.Groups.Count == 0)
            {
                return NotFound(
                    "اطلاعاتی برای تهیه گزارش یافت نشد.");
            }


            // ============================================================
            // 8. محاسبه درصد وزنی سطح دوم
            // ============================================================

            CalculateItemWeightPercentages(
                fullReport);


            // ============================================================
            // 9. ساخت PDF
            // ============================================================

            var document =
                new TajmiePdfDocumentTajmieFasl(
                    fullReport);


            byte[] pdfBytes =
                document.GeneratePdf();


            // ============================================================
            // 10. نام فایل
            // ============================================================

            string fileName =
                $"گزارش-تجمیعی-فصل-" +
                $"{DateTime.Now:yyyyMMdd-HHmmss}.pdf";


            return File(
                pdfBytes,
                "application/pdf",
                fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new
                {
                    success = false,

                    message =
                        "خطا در تهیه گزارش PDF",

                    error =
                        ex.Message,

                    innerError =
                        ex.InnerException?.Message
                });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPdfReportTajmieAll(
        long Year,
        Guid BarAvordId)
    {
        try
        {
            // ============================================================
            // 1. دریافت برآورد
            // ============================================================

            clsBaravordUser? baravordUser =
                await _context.BaravordUsers
                    .FirstOrDefaultAsync(x => x.ID == BarAvordId);


            if (baravordUser == null)
            {
                return NotFound(
                    "برآورد مورد نظر یافت نشد.");
            }


            // ============================================================
            // 2. بررسی فهرست بها
            // ============================================================

            if (string.IsNullOrWhiteSpace(baravordUser.NoeFBs))
            {
                return NotFound(
                    "فهرست بهایی برای این برآورد تعریف نشده است.");
            }


            // ============================================================
            // 3. تبدیل NoeFBs به لیست فهرست بها
            // ============================================================

            List<NoeFehrestBaha> noeFBs =
                baravordUser.NoeFBs
                    .Split(
                        ',',
                        StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => int.TryParse(x, out _))
                    .Select(x =>
                        (NoeFehrestBaha)int.Parse(x))
                    .Distinct()
                    .ToList();


            if (noeFBs.Count == 0)
            {
                return NotFound(
                    "فهرست بهای معتبر برای این برآورد یافت نشد.");
            }


            // ============================================================
            // 4. ساخت سرویس مشترک
            // ============================================================

            TajmieCommon tajmieCommon =
                new TajmieCommon(_context);


            // ============================================================
            // 5. ساخت مدل اصلی گزارش
            // ============================================================

            var fullReport =
                new TajmieSingleLevelFullReportDto
                {
                    Year = Year,

                    ProjectName =
                        baravordUser.Name ?? "",

                    ShomarehPeyman =
                        baravordUser.Num.ToString(),

                    TarikhPeyman =
                        baravordUser.FromDate ?? "22/5/1404",

                    Groups =
                        new List<TajmieSingleLevelReportGroupDto>()
                };


            // ============================================================
            // 6. دریافت اطلاعات هر فهرست بها
            // ============================================================

            foreach (NoeFehrestBaha noeFBId in noeFBs)
            {
                var result =
                    await tajmieCommon.TajmieFosoulAndSub(
                        Year,
                        BarAvordId,
                        noeFBId);


                // --------------------------------------------------------
                // اگر این فهرست بها اطلاعاتی نداشت
                // از گزارش حذف شود و سراغ فهرست بعدی برویم
                // --------------------------------------------------------

                if (result == null ||
                    result.Fosoul == null ||
                    result.Fosoul.Count == 0)
                {
                    continue;
                }


                // --------------------------------------------------------
                // فقط فصل‌هایی که مقدار دارند
                // --------------------------------------------------------

                var validFosoul =
                    result.Fosoul
                        .Where(x =>
                            x.BahayeKol != 0)
                        .ToList();


                // --------------------------------------------------------
                // اگر این فهرست بها فصل دارای مقدار نداشت
                // اصلاً به گزارش اضافه نشود
                // --------------------------------------------------------

                if (validFosoul.Count == 0)
                {
                    continue;
                }


                // --------------------------------------------------------
                // اضافه کردن فهرست بها به گزارش
                // --------------------------------------------------------

                fullReport.Groups.Add(
                    new TajmieSingleLevelReportGroupDto
                    {
                        NoeFB = noeFBId,

                        NoeFBName =
                            GetNoeFehrestBahaName(noeFBId),

                        Year = Year,

                        Fosoul =
                            validFosoul,

                        TotalBahayeKol =
                            validFosoul.Sum(x =>
                                x.BahayeKol),

                        TotalWeightPercent =
                            validFosoul.Sum(x =>
                                x.WeightPercent)
                    });
            }


            // ============================================================
            // 7. بررسی نهایی
            // ============================================================

            if (fullReport.Groups.Count == 0)
            {
                return NotFound(
                    "هیچ اطلاعاتی برای تهیه گزارش یافت نشد.");
            }


            // ============================================================
            // 8. ساخت PDF
            // ============================================================

            var document =
                new TajmieSingleLevelPdfDocument(
                    fullReport);


            byte[] pdfBytes =
                document.GeneratePdf();


            // ============================================================
            // 9. نام فایل
            // ============================================================

            string fileName =
                $"گزارش-تجمیعی-فصل‌ها-" +
                $"{DateTime.Now:yyyyMMdd-HHmmss}.pdf";


            // ============================================================
            // 10. خروجی
            // ============================================================

            return File(
                pdfBytes,
                "application/pdf",
                fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new
                {
                    success = false,
                    message = "خطا در تهیه گزارش تجمیعی",
                    error = ex.Message
                });
        }
    }



    private string GetNoeFehrestBahaName(
    NoeFehrestBaha noeFB)
    {
        return noeFB switch
        {
            NoeFehrestBaha.RahDari =>
                "راهداری",

            NoeFehrestBaha.Abnie =>
                "ابنیه",

            NoeFehrestBaha.RahoBand =>
                "راه،راه آهن و باند فرودگاه",

            NoeFehrestBaha.Barghi =>
                "برقی",

            NoeFehrestBaha.Mekaniki =>
                "مکانیکی",

            NoeFehrestBaha.MareMat =>
                "مرمت",

            _ =>
                "نامشخص"
        };
    }
}

