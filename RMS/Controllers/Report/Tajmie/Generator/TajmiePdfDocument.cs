using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RMS.Controllers.Tajmie.Dto;
using System.Globalization;

namespace RMS.Controllers.Report.Tajmie.Generator;

public class TajmiePdfDocument : IDocument
{
    private readonly TajmieFullReportDto _report;


    public TajmiePdfDocument(
        TajmieFullReportDto report)
    {
        _report = report;
    }


    // ============================================================
    // Metadata
    // ============================================================

    public DocumentMetadata GetMetadata()
    {
        return new DocumentMetadata
        {
            Title = "گزارش تجمیعی ریز برآورد",
            Author = "سامانه ساکار",
            Subject = "گزارش تجمیعی ریز برآورد",
            Keywords = "تجمیع، فصل، آیتم، ریز",
            Creator = "سامانه ساکار"
        };
    }


    // ============================================================
    // Compose
    // ============================================================

    public void Compose(
        IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);

            page.MarginTop(15);
            page.MarginBottom(15);
            page.MarginLeft(15);
            page.MarginRight(15);


            page.DefaultTextStyle(x =>
                x.FontFamily("Tahoma")
                 .FontSize(8));


            // ====================================================
            // Header
            // ====================================================

            page.Header()
                .Element(Header);


            // ====================================================
            // Content
            // ====================================================

            page.Content()
                .ContentFromRightToLeft()
                .PaddingTop(8)
                .Element(Content);


            // ====================================================
            // Footer
            // ====================================================

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

            //column.Item()
            //    .AlignCenter()
            //    .Text("گزارش تجمیعی ریز برآورد")
            //    .Bold()
            //    .FontSize(14);


            // ----------------------------------------------------
            // فهرست بها
            // ----------------------------------------------------

            column.Item()
                .PaddingTop(5)
                .AlignCenter()
                .Text(text =>
                {
                    text.Span("ریزه متره فهرست بها تجمیع شده ")
                        .Bold()
                        .FontSize(14);

                    //text.Span(
                    //    _report.NoeFBNames)
                    //    .Bold()
                    //    .FontSize(12);

                    //text.Span(" - سال ")
                    //    .Bold()
                    //    .FontSize(12);

                    //text.Span(
                    //    ToPersianDigits(
                    //        _report.Year.ToString()))
                    //    .Bold()
                    //    .FontSize(12);
                });


            // ----------------------------------------------------
            // اطلاعات پروژه
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
                                _report.ProjectTitle ?? "");
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


    // ============================================================
    // Content
    // ============================================================

    private void Content(
        IContainer container)
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
                        "اطلاعاتی برای نمایش وجود ندارد.");

                return;
            }


            // ========================================================
            // هر فهرست بها
            // ========================================================

            foreach (var group in _report.Groups)
            {
                RenderGroup(
                    column,
                    group);
            }


            // ========================================================
            // جمع کل گزارش
            // ========================================================

            RenderTotal(column);
        });
    }

    // ============================================================
    // گروه فهرست بها
    // ============================================================

    private void RenderGroup(
        ColumnDescriptor column,
        TajmieReportGroupDto group)
    {
        // ============================================================
        // عنوان فهرست بها
        // ============================================================

        column.Item()
            .PaddingTop(10)
            .PaddingVertical(6)
            .PaddingHorizontal(5)
            .AlignCenter()
            .Text(text =>
            {
                text.Span("براساس فهرست بها ")
                    .Bold()
                    .FontSize(10)
                    .FontColor("#000");

                text.Span(
                    group.NoeFBName ?? "")
                    .Bold()
                    .FontSize(10)
                    .FontColor("#000");

                text.Span(" - سال ")
                    .Bold()
                    .FontSize(10)
                    .FontColor("#000");

                text.Span(
                    ToPersianDigits(
                        group.Year.ToString()))
                    .Bold()
                    .FontSize(10)
                    .FontColor("#000");
            });


        // ============================================================
        // فصل‌ها فقط برای دسترسی به آیتم‌ها
        // جدول فصل نمایش داده نمی‌شود
        // ============================================================

        if (group.Fosoul == null)
            return;


        foreach (var fasl in group.Fosoul)
        {
            if (fasl.Items == null)
                continue;


            foreach (var item in fasl.Items)
            {
                // فقط آیتم‌هایی که ریز دارند
                if (item.Details == null ||
                    item.Details.Count == 0)
                {
                    continue;
                }


                RenderItem(
                    column,
                    item);
            }
        }
    }


    // ============================================================
    // سطح دوم : آیتم
    // ============================================================

    private void RenderItem(
        ColumnDescriptor column,
        TajmieReportItemDto item)
    {
        column.Item()
            .PaddingTop(10)
            .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1.5f);
                    columns.RelativeColumn(4.2f);
                    columns.RelativeColumn(1.2f);
                    columns.RelativeColumn(1.4f);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2.2f);
                    columns.RelativeColumn(1.7f);
                });


                // =====================================================
                // Header
                // =====================================================


                table.Header(header =>
                {
                    header.Cell()
                        .Element(ItemHeaderStyle)
                        .Text("کد فعالیت")
                        .Bold();

                    header.Cell()
                        .Element(ItemHeaderStyle)
                        .Text("شرح")
                        .Bold();

                    header.Cell()
                        .Element(ItemHeaderStyle)
                        .Text("مقدار")
                        .Bold();

                    header.Cell()
                        .Element(ItemHeaderStyle)
                        .Text("واحد")
                        .Bold();

                    header.Cell()
                        .Element(ItemHeaderStyle)
                        .Text("بهای واحد")
                        .Bold();

                    header.Cell()
                        .Element(ItemHeaderStyle)
                        .Text("بهای کل")
                        .Bold();

                    header.Cell()
                        .Element(ItemHeaderStyle)
                        .Text("درصد وزنی")
                        .Bold();
                });


                // =====================================================
                // Data
                // =====================================================

                table.Cell()
                    .Element(ItemRowStyle)
                    .Text(
                        ToPersianDigits(
                            item.ItemCode));


                table.Cell()
                    .Element(ItemRowStyle)
                    .AlignRight()
                    .Text(
                        item.Sharh ?? "");


                table.Cell()
                    .Element(ItemRowStyle)
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            item.Meghdar
                                .ToString("G29")));


                table.Cell()
                    .Element(ItemRowStyle)
                    .Text(
                        item.Vahed ?? "");


                table.Cell()
                    .Element(ItemRowStyle)
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            item.BahayeVahed
                                .ToString("N0")));


                table.Cell()
                    .Element(ItemRowStyle)
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            item.BahayeKol
                                .ToString("N0")));


                table.Cell()
                    .Element(ItemRowStyle)
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            item.WeightPercent
                                .ToString("N2")));
            });


        // ============================================================
        // ریز
        // ============================================================

        if (item.Details != null &&
            item.Details.Count > 0)
        {
            RenderDetails(
                column,
                item.Details);
        }
    }

    // ============================================================
    // سطح سوم : ریز
    // ============================================================

    private void RenderDetails(
        ColumnDescriptor column,
        List<TajmieReportDetailDto> details)
    {
        column.Item()
            .PaddingTop(2)
            .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1.5f);
                    columns.RelativeColumn(5.4f);
                    columns.RelativeColumn(1.4f);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2.2f);
                    columns.RelativeColumn(1.7f);
                });


                // =================================================
                // Header
                // =================================================

                table.Header(header =>
                {
                    header.Cell()
                        .Element(DetailHeaderStyle)
                        .Text("شماره")
                        .Bold();


                    header.Cell()
                        .Element(DetailHeaderStyle)
                        .Text("شرح")
                        .Bold();


                    header.Cell()
                        .Element(DetailHeaderStyle)
                        .Text("مقدار")
                        .Bold();


                    header.Cell()
                        .Element(DetailHeaderStyle)
                        .Text("بهای واحد")
                        .Bold();


                    header.Cell()
                        .Element(DetailHeaderStyle)
                        .Text("بهای کل")
                        .Bold();


                    header.Cell()
                        .Element(DetailHeaderStyle)
                        .Text("درصد وزنی")
                        .Bold();
                });


                // =================================================
                // Rows
                // =================================================

                foreach (var detail in details)
                {
                    table.Cell()
                        .Element(DetailRowStyle)
                        .Text(
                            ToPersianDigits(
                                detail.FBShomareh));


                    table.Cell()
                        .Element(DetailRowStyle)
                        .AlignRight()
                        .Text(
                            detail.Sharh ?? "");


                    table.Cell()
                        .Element(DetailRowStyle)
                        .ContentFromLeftToRight()
                        .Text(
                            ToPersianDigits(
                                detail.Meghdar
                                    .ToString("G29")));


                    table.Cell()
                        .Element(DetailRowStyle)
                        .ContentFromLeftToRight()
                        .Text(
                            ToPersianDigits(
                                detail.BahayeVahed
                                    .ToString("N0")));


                    table.Cell()
                        .Element(DetailRowStyle)
                        .ContentFromLeftToRight()
                        .Text(
                            ToPersianDigits(
                                detail.BahayeKol
                                    .ToString("N0")));


                    table.Cell()
                        .Element(DetailRowStyle)
                        .ContentFromLeftToRight()
                        .Text(
                            ToPersianDigits(
                                detail.WeightPercent
                                    .ToString("N2")));
                }
            });
    }


    // ============================================================
    // جمع کل گزارش
    // ============================================================

    private void RenderTotal(
        ColumnDescriptor column)
    {
        column.Item()
            .PaddingTop(12)
            .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(7);
                    columns.RelativeColumn(2.5f);
                    columns.RelativeColumn(2);
                });


                table.Cell()
                    .Element(TotalStyle)
                    .Text("جمع کل")
                    .Bold();


                table.Cell()
                    .Element(TotalStyle)
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            _report.TotalBahayeKol
                                .ToString("N0")))
                    .Bold();


                table.Cell()
                    .Element(TotalStyle)
                    .ContentFromLeftToRight()
                    .Text(
                        ToPersianDigits(
                            _report.TotalWeightPercent
                                .ToString("N2")))
                    .Bold();
            });
    }


    // ============================================================
    // Footer
    // ============================================================

    private void ComposeFooter(
        IContainer container)
    {
        container
            .PaddingTop(5)
            .Column(column =>
            {
                column.Item()
                    .LineHorizontal(1)
                    .LineColor("#B8C7D9");


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

    private IContainer ItemHeaderStyle(
        IContainer container)
    {
        return container
            .Background("#9c9c9c")
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(5)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle()
            .DefaultTextStyle(
                x => x.FontColor("#000"));
    }


    private IContainer ItemRowStyle(
        IContainer container)
    {
        return container
            .Background("#d9d9d9")
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(5)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle();
    }


    private IContainer DetailHeaderStyle(
        IContainer container)
    {
        return container
            .Background("#c9c9c9")
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(5)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle()
            .DefaultTextStyle(
                x => x.FontColor("#000"));
    }


    private IContainer DetailRowStyle(
        IContainer container)
    {
        return container
            .Background("#fff")
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(4)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle();
    }


    private IContainer TotalStyle(
        IContainer container)
    {
        return container
            .Background("#c9c9c9")
            .Border(1)
            .BorderColor("#000")
            .PaddingVertical(7)
            .PaddingHorizontal(4)
            .AlignCenter()
            .AlignMiddle();
    }


    // ============================================================
    // Persian Digits
    // ============================================================

    private string ToPersianDigits(
        string input)
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