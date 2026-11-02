using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RMS.Controllers.Report.Tajmie.Dto;
using RMS.Controllers.Tajmie.Dto;
using System.Globalization;

namespace RMS.Controllers.Report.Tajmie.Generator;

public class TajmieSingleLevelPdfDocument : IDocument
{

    private readonly TajmieSingleLevelFullReportDto _report;

    public TajmieSingleLevelPdfDocument(
        TajmieSingleLevelFullReportDto report)
    {
        _report = report;
    }


    // ================================================================
    // Metadata
    // ================================================================

    public DocumentMetadata GetMetadata()
    {
        return new DocumentMetadata
        {
            Title = "گزارش تجمیعی فصل‌ها",
            Author = "سامانه ساکار",
            Subject = "گزارش تجمیعی فصل‌ها",
            Keywords = "تجمیع، فصل، فهرست بها",
            Creator = "سامانه ساکار"
        };
    }


    // ================================================================
    // Compose
    // ================================================================

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            // ========================================================
            // صفحه
            // ========================================================

            page.Size(PageSizes.A4);

            page.MarginTop(15);
            page.MarginBottom(15);
            page.MarginLeft(20);
            page.MarginRight(20);


            // ========================================================
            // فونت پیش‌فرض
            // ========================================================

            page.DefaultTextStyle(
                x => x
                    .FontFamily("Tahoma")
                    .FontSize(8));


            // ========================================================
            // Header
            // ========================================================

            page.Header()
                .Element(Header);


            // ========================================================
            // Content
            // ========================================================

            page.Content()
                .ContentFromRightToLeft()
                .PaddingTop(8)
                .Element(ComposeContent);


            // ========================================================
            // Footer
            // ========================================================

            page.Footer()
                .Element(ComposeFooter);
        });
    }


    // ============================================================
    // Header
    // ============================================================

    private void Header(
        IContainer container)
    {
        container.Column(column =>
        {
            // ----------------------------------------------------
            // عنوان
            // ----------------------------------------------------

            column.Item()
                .AlignCenter()
                .Text(
                    "خلاصه مالی فصول فهرست بها تجمیع شده")
                .Bold()
                .FontSize(14);


            // ----------------------------------------------------
            // فهرست بها
            // ----------------------------------------------------

            //column.Item()
            //    .PaddingTop(5)
            //    .AlignCenter()
            //    .Text(text =>
            //    {
                    //text.Span("فهرست بها : ")
                    //    .Bold()
                    //    .FontSize(9);


                    //text.Span(
                    //    _report.NoeFBNames ?? "")
                    //    .Bold()
                    //    .FontSize(9);


                    //text.Span(" - سال ")
                    //    .Bold()
                    //    .FontSize(9);


                    //text.Span(
                    //    ToPersianDigits(
                    //        _report.Year.ToString()))
                    //    .Bold()
                    //    .FontSize(9);
               // });


            // ----------------------------------------------------
            // پروژه
            // ----------------------------------------------------

            column.Item()
                .PaddingTop(8)
                .Row(row =>
                {
                    row.RelativeItem(1)
                        .AlignLeft()
                        .Text(text =>
                        {
                            text.Span(
                                "تاریخ گزارش : ")
                                .Bold();


                            text.Span(
                                GetPersianDate(
                                    DateTime.Now));
                        });

                    row.RelativeItem(2)
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span(
                                "عنوان پروژه : ")
                                .Bold();


                            text.Span(
                                _report.ProjectName ?? "");
                        });
                });

            /////////////////////////////
            /////////////////////////////
            ///

            column.Item()
                .PaddingTop(8)
                .Row(row =>
                {

                    row.RelativeItem(2)
                     .AlignLeft()
                     .Text(text =>
                     {
                         text.Span(
                             "مشاور : ")
                             .Bold();


                         text.Span(
                             "پاد راه رضوان");
                     });
                    row.RelativeItem(2)
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span(
                                "پیمانکار : ")
                                .Bold();


                            text.Span("شرکت 1");
                        });

                    row.RelativeItem(2)
                           .AlignRight()
                           .Text(text =>
                           {
                               text.Span(
                                   "دستگاه اجرایی : ")
                                   .Bold();


                               text.Span("کارفرما");
                           });
                });


            // ----------------------------------------------------
            // شماره پیمان + تاریخ + صفحه
            // ----------------------------------------------------

            column.Item()
                .PaddingTop(5)
                .Row(row =>
                {


                    // صفحه
                    row.RelativeItem(2)
                        .AlignLeft()
                        .Text(text =>
                        {
                            text.Span("صفحه ")
                                .Bold();


                            text.CurrentPageNumber()
                                .Format(x =>
                                    ToPersianDigits(
                                        x.ToString()));


                            text.Span(" از ");


                            text.TotalPages()
                                .Format(x =>
                                    ToPersianDigits(
                                        x.ToString()));
                        });


                    // تاریخ پیمان
                    row.RelativeItem(2)
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span(
                                "تاریخ پیمان : ")
                                .Bold();


                            text.Span(
                                ToPersianDigits(
                                    _report.TarikhPeyman.Trim() == "" ? "1405/05/25" : _report.TarikhPeyman.Trim()));
                        });

                    // شماره پیمان
                    row.RelativeItem(2)
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span(
                                "شماره پیمان : ")
                                .Bold();


                            text.Span(
                                ToPersianDigits(
                                    _report.ShomarehPeyman ?? ""));
                        });
                });


            // ----------------------------------------------------
            // خطوط
            // ----------------------------------------------------

            column.Item()
                .PaddingTop(6)
                .LineHorizontal(1)
                .LineColor("#999");


            column.Item()
                .PaddingTop(2)
                .LineHorizontal(2)
                .LineColor("#777");
        });
    }


    // ================================================================
    // Content
    // ================================================================

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            if (_report.Groups == null ||
                !_report.Groups.Any())
            {
                column.Item()
                    .AlignCenter()
                    .PaddingTop(30)
                    .Text(
                        "اطلاعاتی برای نمایش وجود ندارد.")
                    .FontSize(10);

                return;
            }


            // ========================================================
            // هر فهرست بها یک بخش جدا
            // ========================================================

            bool firstGroup = true;


            foreach (var group in _report.Groups)
            {
                if (!firstGroup)
                {
                    column.Item()
                        .PaddingTop(15);
                }


                firstGroup = false;


                // ----------------------------------------------------
                // عنوان فهرست بها
                // ----------------------------------------------------

                column.Item()
                    .AlignCenter()
                    .PaddingBottom(6)
                    .Text(
                        $"براساس فهرست بهای {group.NoeFBName} سال {ToPersianDigits(group.Year.ToString())}")
                    .Bold()
                    .FontSize(10);


                // ----------------------------------------------------
                // جدول فصل‌ها
                // ----------------------------------------------------

                if (group.Fosoul != null &&
                    group.Fosoul.Any())
                {
                    ComposeFaslTable(
                        column,
                        group);
                }
            }
        });
    }


    // ================================================================
    // Footer
    // ================================================================

    private void ComposeFooter(IContainer container)
    {
        container
            .PaddingTop(5)
            .Column(column =>
            {
                // --------------------------------------------------------
                // خط بالای Footer
                // --------------------------------------------------------

                column.Item()
                    .LineHorizontal(1)
                    .LineColor("#B8C7D9");


                // --------------------------------------------------------
                // اطلاعات Footer
                // --------------------------------------------------------

                column.Item()
                     .PaddingTop(35)
                     .Row(row =>
                     {

                         row.RelativeItem()
                             .AlignLeft()
                             .PaddingLeft(30)
                             .Text("کارفرما")
                             .Bold()
                             .FontSize(7);

                         row.RelativeItem()
                             .AlignCenter()
                             .Text("پیمانکار")
                             .Bold()
                             .FontSize(7);

                         row.RelativeItem()
                              .AlignRight()
                              .PaddingRight(30)
                              .Text("مشاور")
                              .Bold()
                              .FontSize(7);

                     });
            });
    }


    // ================================================================
    // جدول فصل‌ها
    // ================================================================

    private void ComposeFaslTable(
        ColumnDescriptor column,
        TajmieSingleLevelReportGroupDto group)
    {
        // فقط فصل‌هایی که مبلغ دارند
        var validFosoul =
            group.Fosoul
                .Where(x =>
                    x.BahayeKol != 0)
                .ToList();


        if (!validFosoul.Any())
            return;


        column.Item()
            .Table(table =>
            {
                // ====================================================
                // Columns
                // ====================================================

                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1.4f);

                    columns.RelativeColumn(6);

                    columns.RelativeColumn(2.5f);

                    columns.RelativeColumn(1.8f);
                });


                // ====================================================
                // Header
                // ====================================================

                table.Header(header =>
                {
                    header.Cell()
                        .Element(FaslHeaderStyle)
                        .Text("فصل")
                        .Bold();


                    header.Cell()
                        .Element(FaslHeaderStyle)
                        .Text("شرح فصل")
                        .Bold();


                    header.Cell()
                        .Element(FaslHeaderStyle)
                        .Text("بهای کل (ریال)")
                        .Bold();


                    header.Cell()
                        .Element(FaslHeaderStyle)
                        .Text("درصد وزنی")
                        .Bold();
                });


                // ====================================================
                // Rows
                // ====================================================

                foreach (var fasl in validFosoul)
                {
                    // ------------------------------------------------
                    // کد فصل
                    // ------------------------------------------------

                    table.Cell()
                        .Element(FaslRowStyle)
                        .AlignCenter()
                        .Text(
                            ToPersianDigits(
                                fasl.FaslCode ?? ""));


                    // ------------------------------------------------
                    // شرح
                    // ------------------------------------------------

                    table.Cell()
                        .Element(FaslRowStyle)
                        .AlignRight()
                        .Text(
                            fasl.Title ?? "");


                    // ------------------------------------------------
                    // مبلغ
                    // ------------------------------------------------

                    table.Cell()
                        .Element(FaslRowStyle)
                        .AlignCenter()
                        .ContentFromLeftToRight()
                        .Text(
                            ToPersianDigits(
                                FormatNumber(
                                    fasl.BahayeKol)));


                    // ------------------------------------------------
                    // درصد
                    // ------------------------------------------------

                    table.Cell()
                        .Element(FaslRowStyle)
                        .AlignCenter()
                        .ContentFromLeftToRight()
                        .Text(
                            ToPersianDigits(
                                FormatPercent(
                                    fasl.WeightPercent)));
                }


                // ====================================================
                // جمع فهرست بها
                // ====================================================

                table.Cell()
                    .ColumnSpan(2)
                    .Element(TotalStyle)
                    .AlignRight()
                    .Text(
                        "جمع")
                    .Bold();
                //$"جمع فهرست بهای {group.NoeFBName}")

                table.Cell()
                    .Element(TotalStyle)
                    .AlignCenter()
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            FormatNumber(
                                group.TotalBahayeKol)))
                    .Bold();


                table.Cell()
                    .Element(TotalStyle)
                    .AlignCenter()
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            FormatPercent(
                                group.TotalWeightPercent)))
                    .Bold();
            });
    }


    // ================================================================
    // Header Style
    // ================================================================

    private IContainer FaslHeaderStyle(
        IContainer container)
    {
        return container
            .Background("#9c9c9c")
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(6)
            .PaddingHorizontal(4)
            .AlignCenter()
            .AlignMiddle()
            .DefaultTextStyle(x => x.FontColor("#000"));
    }


    // ================================================================
    // Row Style
    // ================================================================

    private IContainer FaslRowStyle(
        IContainer container)
    {
        return container
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(6)
            .PaddingHorizontal(4)
            .AlignMiddle();
    }


    // ================================================================
    // Total Style
    // ================================================================

    private IContainer TotalStyle(
        IContainer container)
    {
        return container
            .Background("#E9EEF5")
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(7)
            .PaddingHorizontal(4)
            .AlignMiddle();
    }


    // ================================================================
    // Number
    // ================================================================

    private string FormatNumber(decimal value)
    {
        return value.ToString(
            "#,##0",
            CultureInfo.InvariantCulture);
    }


    // ================================================================
    // Percent
    // ================================================================

    private string FormatPercent(decimal value)
    {
        return value.ToString(
            "0.00",
            CultureInfo.InvariantCulture);
    }


    // ================================================================
    // Persian Digits
    // ================================================================

    private string ToPersianDigits(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return input
            .Replace("0", "۰")
            .Replace("1", "۱")
            .Replace("2", "۲")
            .Replace("3", "۳")
            .Replace("4", "۴")
            .Replace("5", "۵")
            .Replace("6", "۶")
            .Replace("7", "۷")
            .Replace("8", "۸")
            .Replace("9", "۹");
    }



    // ============================================================
    // Persian Date
    // ============================================================

    private string GetPersianDate(
        DateTime date)
    {
        PersianCalendar pc =
            new PersianCalendar();

        return ToPersianDigits(
            $"{pc.GetYear(date)}/" +
            $"{pc.GetMonth(date):00}/" +
            $"{pc.GetDayOfMonth(date):00}");
    }
}
