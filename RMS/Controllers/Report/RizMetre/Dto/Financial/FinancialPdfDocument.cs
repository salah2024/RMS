namespace RMS.Controllers.Report.RizMetre.Dto.Financial;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RMS.Controllers.Report.RizMetre.Common;
using RMS.Models.Entity;
using System.Globalization;

public class FinancialPdfDocument : IDocument
{
    private readonly List<FinancialNoeFBVm> _reports;
    private readonly clsBaravordUser _baravordUser;
    private readonly string _projectName;
    private FinancialNoeFBVm? _currentNoeFB;

    public FinancialPdfDocument(
        List<FinancialNoeFBVm> reports,
        clsBaravordUser baravordUser,
        string projectName)
    {
        _reports = reports;
        _baravordUser = baravordUser;
        _projectName = projectName;
    }

    private string GetPersianDate(DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();

        return ToPersianDigits(
            $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}");
    }

    public DocumentMetadata GetMetadata()
        => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {

        foreach (var report in _reports)
        {
            _currentNoeFB = report;

            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.Margin(15);

                page.DefaultTextStyle(x =>
                    x.FontFamily("Tahoma")
                     .FontSize(9));

                page.Header()
                    .Element(Header);

                page.Content()
                    .ContentFromRightToLeft()
                    .Element(c => Content(c, report));

                page.Footer()
                    .Element(Footer);
            });
        }
    }

    #region Header

    void Header(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Row(row =>
            {


                row.RelativeItem(2)
                    .AlignLeft()
                    .Text(text =>
                    {
                        text.Span("تاریخ پیمان : ").FontSize(8).Bold();
                        text.Span(GetPersianDate(DateTime.Now)).FontSize(8);
                    });

                // عنوان
                row.RelativeItem(4)
                    .AlignCenter()
                    .Text($"« برگه مالی {_baravordUser.Name} »")
                    .Bold()
                    .FontSize(12);


                row.RelativeItem(2)
                    .AlignRight()
                    .Text(text =>
                    {
                        text.Span("شماره پیمان : ").FontSize(8).Bold();
                        text.Span("524555").FontSize(8);
                    });
              
               
            });

            col.Item()
            .PaddingTop(10)
            .Row(row =>
            {
                row.RelativeItem(1)
                   .AlignRight()
                   .Text("")
                   .Bold()
                   .FontSize(8);

                row.RelativeItem(4)
                    .AlignCenter()
                    .Text($"فهرست بهای پایه رشته {_currentNoeFB.NoeFBName} - سال {ToPersianDigits(_baravordUser.Year.ToString())}")
                    .Bold()
                    .FontSize(10);

                row.RelativeItem(1)
                   .AlignRight()
                   .Text("")
                   .Bold()
                   .FontSize(8);
            });

            col.Item()
                .PaddingTop(15)
                .Row(row =>
                {
                    row.RelativeItem(1)
                        .AlignCenter()
                        .Text("")
                        .Bold()
                        .FontSize(10);


                    row.RelativeItem(2)
                    .AlignRight()
                    .Text(text =>
                    {
                        text.Span("دستگاه اجرایی : ").FontSize(10).Bold();
                        text.Span("ساکار").FontSize(10);
                    });

                    row.RelativeItem(2)
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span("عنوان پروژه : ").FontSize(10).Bold();
                            text.Span(_projectName).FontSize(10);
                        });
                });

            col.Item()
                .PaddingTop(4)
                .Row(row =>
                {
                    row.RelativeItem(1)
                    .AlignLeft()
                    .Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(8).Bold());

                        text.Span("صفحه ").FontSize(8);

                        text.CurrentPageNumber()
                            .Format(x => ToPersianDigits(x.ToString())).FontSize(8);

                        text.Span(" از ").FontSize(8);

                        text.TotalPages()
                            .Format(x => ToPersianDigits(x.ToString())).FontSize(8);
                    });

                    row.RelativeItem(2)
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span("مشاور : ").FontSize(10).Bold();
                            text.Span("پاد راه").FontSize(10);
                        });

                    row.RelativeItem(2)
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span("پیمانکار : ").FontSize(10).Bold();
                            text.Span("ساکار").FontSize(10);
                        });

                });

            col.Item()
                .PaddingTop(5)
                .LineHorizontal(2);
            col.Item()
                .PaddingTop(2)
                .LineHorizontal(1);
        });
    }

    #endregion

    #region Footer

    void Footer(IContainer container)
    {
        container.Column(col =>
        {
            col.Item()
                .LineHorizontal(1);

            col.Item()
                .PaddingTop(8)
                .Row(row =>
                {
                    // پیمانکار - سمت راست
                    row.RelativeItem()
                        .AlignRight()
                        .Text("پیمانکار")
                        .Bold();

                    // مشاور - وسط
                    row.RelativeItem()
                        .AlignCenter()
                        .Text("مشاور")
                        .Bold();

                    // کارفرما - سمت چپ
                    row.RelativeItem()
                        .AlignLeft()
                        .Text("کارفرما")
                        .Bold();
                });

            // فضای امضا
            col.Item().Height(50);
        });
    }
    #endregion


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

    void Content(IContainer container, FinancialNoeFBVm report)
    {
        container.Column(col =>
        {

            decimal totalWithoutZarib = report.Total;

            decimal totalWithZarib = 0;


            // فصل های این فهرست بها
            foreach (var chapter in report.Chapters)
            {
                if (chapter.BaseItems.Any())
                {
                    ChapterTable(
    col,
    chapter.ChapterName,
    chapter.ChapterCode,
    chapter.BaseItems,
    false,
    chapter.ZaribBalasari,
    chapter.ZaribMantaghe,
    chapter.ZaribPishnahadiPK);
                }

                if (chapter.StarItems.Any())
                {
                    ChapterTable(
    col,
    chapter.ChapterName + " (غیر پایه)",
    chapter.ChapterCode,
    chapter.StarItems,
    true,
    chapter.ZaribBalasari,
    chapter.ZaribMantaghe,
    chapter.ZaribPishnahadiPK);
                }


                decimal chapterTotal =
       chapter.BaseItems.Sum(x => x.BahayeKol) +
       chapter.StarItems.Sum(x => x.BahayeKol);

                if (chapter.ZaribBalasari.HasValue)
                    chapterTotal *= chapter.ZaribBalasari.Value;

                if (chapter.ZaribMantaghe.HasValue)
                    chapterTotal *= chapter.ZaribMantaghe.Value;

                if (chapter.ZaribPishnahadiPK.HasValue)
                    chapterTotal *= chapter.ZaribPishnahadiPK.Value;

                totalWithZarib += chapterTotal;

            }

            // تجهیز کارگاه (فقط یکبار برای این فهرست بها)
            if (report.TajhizKargahAmount.HasValue &&
                report.TajhizKargahAmount.Value != 0)
            {
                TajhizKargahTable(
                    col,
                    report.TajhizKargahPercent ?? 0,
                    report.TajhizKargahAmount.Value);
            }


            // تجهیز کارگاه نیز در صورت وجود به جمع اضافه شود
            if (report.TajhizKargahAmount.HasValue)
                totalWithZarib += report.TajhizKargahAmount.Value;


            //// جمع این فهرست بها
            //col.Item()
            //    .PaddingTop(10)
            //    .BorderTop(1)
            //    .PaddingTop(5)
            //    .AlignLeft()
            //    .Text(text =>
            //    {
            //        text.DefaultTextStyle(x => x.Bold().FontSize(10));

            //        text.Span("جمع فهرست بها : ");

            //        text.Span(ToPersianDigits(report.Total.ToString("N0")));
            //    });


            col.Item()
    .PaddingTop(10)
    .BorderTop(1)
    .PaddingTop(5)
    .Column(c =>
    {
        c.Item()
            .AlignLeft()
            .PaddingTop(7)
            .Text(text =>
            {
                text.DefaultTextStyle(x => x.Bold().FontSize(10));

                text.Span("جمع فهرست بها : ");

                text.Span(ToPersianDigits(totalWithoutZarib.ToString("N0")));
            });

        c.Item()
            .AlignLeft()
            .PaddingTop(7)
            .Text(text =>
            {
                text.DefaultTextStyle(x => x.Bold().FontSize(10));

                text.Span("جمع فهرست بها با اعمال ضرایب : ");

                text.Span(ToPersianDigits(totalWithZarib.ToString("N0")));
            });


        c.Item()
            .AlignLeft()
            .PaddingTop(7)
            .Text(text =>
            {
                text.DefaultTextStyle(x => x.Bold().FontSize(10));

                string result = NumberToPersian.ToWords(long.Parse(Math.Round(totalWithZarib).ToString()));

                text.Span(result);
            });

    });

        });
    }


    private void TajhizKargahTable(
    ColumnDescriptor column,
    decimal percent,
    decimal amount)
    {
        column.Item()
            .PaddingTop(8)
            .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1.5f);
                    columns.RelativeColumn(2.5f);
                    columns.RelativeColumn(7);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2.5f);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(3);
                });

                table.Cell().Element(RowStyle).Text("");
                table.Cell().Element(RowStyle).Text("");

                table.Cell()
                    .Element(RowStyle)
                    .AlignRight()
                    .Text("پیوست ۳ - دستورالعمل تجهیز و برچیدن کارگاه")
                    .Bold();

                table.Cell()
                    .Element(RowStyle)
                    .Text("%");

                table.Cell()
                    .Element(RowStyle)
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(percent.ToString("N2")));

                table.Cell()
                    .Element(RowStyle)
                    .Text("");

                table.Cell()
                    .Element(RowStyle)
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(amount.ToString("N0")))
                    .Bold();
            });
    }


    private void ChapterTable(
    ColumnDescriptor column,
    string title,
    string chapterCode,
    List<FinancialItemVm> items,
    bool isStar,
    decimal? zaribBalasari,
    decimal? zaribMantaghe,
    decimal? zaribPishnahadiPK)
    {
        if (items == null || !items.Any())
            return;

        //decimal chapterTotal = items.Sum(x => x.BahayeKol);

        decimal chapterTotal = items.Sum(x => x.BahayeKol);

        decimal totalAfterBalasari = chapterTotal;
        decimal finalTotal = chapterTotal;

        if (zaribBalasari.HasValue)
        {
            totalAfterBalasari *= zaribBalasari.Value;
            finalTotal = totalAfterBalasari;
        }

        if (zaribMantaghe.HasValue)
        {
            finalTotal *= zaribMantaghe.Value;
        }

        if (zaribPishnahadiPK.HasValue)
        {
            finalTotal *= zaribPishnahadiPK.Value;
        }

        // عنوان فصل
        column.Item()
            .PaddingTop(15)
            .Element(TitleStyle)
            .Text($"{ToPersianDigits(chapterCode)} - {title}")
            .Bold()
            .FontSize(10);

        column.Item()
            .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1.5f); // ردیف
                    columns.RelativeColumn(2.5f); // شماره آیتم
                    columns.RelativeColumn(7);    // شرح
                    columns.RelativeColumn(2);    // واحد
                    columns.RelativeColumn(2.5f); // مقدار
                    columns.RelativeColumn(3);    // بهای واحد
                    columns.RelativeColumn(3);    // بهای کل
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderStyle).Text("ردیف").FontSize(8).Bold();
                    header.Cell().Element(HeaderStyle).Text("شماره آیتم").FontSize(8).Bold();
                    header.Cell().Element(HeaderStyle).Text("شرح آیتم").FontSize(8).Bold();
                    header.Cell().Element(HeaderStyle).Text("واحد").FontSize(8).Bold();
                    header.Cell().Element(HeaderStyle).Text("مقدار").FontSize(8).Bold();
                    header.Cell().Element(HeaderStyle).Text("بهای واحد (ريال)").FontSize(8).Bold();
                    header.Cell().Element(HeaderStyle).Text("بهای کل (ريال)").FontSize(8).Bold();
                });

                int row = 1;

                foreach (var item in items.OrderBy(x => x.ItemCode))
                {
                    table.Cell().Element(RowStyle)
                        .Text(ToPersianDigits(row.ToString()))
                        .FontSize(9);

                    string itemCode = isStar
                        ? ToPersianDigits(item.ItemCode) + " *"
                        : ToPersianDigits(item.ItemCode);

                    table.Cell().Element(RowStyle)
                        .Text(itemCode)
                        .FontSize(9);

                    table.Cell().Element(RowStyle)
                        .AlignRight()
                        .Text(ToPersianDigits(item.ItemSharh))
                        .FontSize(9);

                    table.Cell().Element(RowStyle)
                        .Text(item.Vahed)
                        .FontSize(9);

                    table.Cell().Element(RowStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.Meghdar.ToString("G29")))
                        .FontSize(9);

                    table.Cell().Element(RowStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.BahayeVahed?.ToString("N0")))
                        .FontSize(9);

                    table.Cell().Element(RowStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.BahayeKol.ToString("N0")))
                        .FontSize(8);

                    row++;
                }
            });

        // جمع فصل
        //column.Item()
        //    .AlignLeft()
        //    .PaddingTop(4)
        //    .PaddingBottom(10)
        //    .Text(text =>
        //    {
        //        text.DefaultTextStyle(x => x.Bold().FontSize(9));

        //        text.Span(isStar
        //            ? "جمع آیتم های غیر پایه : "
        //            : "جمع آیتم های پایه : ");

        //        text.Span(ToPersianDigits(chapterTotal.ToString("N0")));
        //    });


        //    column.Item()
        //.PaddingTop(5)
        //.PaddingTop(5)
        //.Column(col =>
        //{
        //    col.Item().AlignLeft().PaddingTop(5).Text(text =>
        //    {
        //        text.DefaultTextStyle(x => x.FontSize(9));

        //        text.Span(isStar
        //            ? "جمع آیتم های غیر پایه : "
        //            : "جمع آیتم های پایه : ");

        //        text.Span(ToPersianDigits(chapterTotal.ToString("N0")));
        //    });

        //    if (zaribBalasari.HasValue)
        //    {
        //        col.Item().AlignLeft().PaddingTop(5).Text(text =>
        //        {
        //            text.DefaultTextStyle(x => x.FontSize(9));

        //            text.Span("ضریب بالاسری : ");

        //            text.Span(ToPersianDigits(zaribBalasari.Value.ToString("0.###")));
        //        });
        //    }

        //    if (zaribMantaghe.HasValue)
        //    {
        //        col.Item().AlignLeft().PaddingTop(5).Text(text =>
        //        {
        //            text.DefaultTextStyle(x => x.FontSize(9));

        //            text.Span("ضریب منطقه : ");

        //            text.Span(ToPersianDigits(zaribMantaghe.Value.ToString("0.###")));
        //        });
        //    }
        //    ;

        //    if (zaribBalasari.HasValue || zaribMantaghe.HasValue)
        //    {
        //        col.Item()
        //            .AlignLeft()
        //            .PaddingTop(7)
        //            .Text(text =>
        //            {
        //                text.DefaultTextStyle(x => x.Bold().FontSize(9));

        //                text.Span("جمع با اعمال ضرایب : ");

        //                text.Span(ToPersianDigits(finalTotal.ToString("N0")));
        //            });
        //    }
        //});


        column.Item()
    .PaddingTop(7)
    .AlignLeft()
    .Text(text =>
    {
        text.DefaultTextStyle(x => x.Bold().FontSize(9));

        // عنوان
        //text.Span(isStar
        //    ? "جمع آیتم های غیر پایه : "
        //    : "جمع آیتم های پایه : ");


        text.Span(" (ریال) ");
        // اگر حداقل یک ضریب وجود داشت، نتیجه را نمایش بده
        if (zaribBalasari.HasValue || zaribMantaghe.HasValue || zaribPishnahadiPK.HasValue)
        {

            text.Span(ToPersianDigits(finalTotal.ToString("N0")))
                .Bold()
                .FontSize(10);
            text.Span(" = ");
        }



        // ضریب پیشنهادی پیمانکار
        if (zaribPishnahadiPK.HasValue)
        {
            text.Span(ToPersianDigits(zaribPishnahadiPK.Value.ToString("0.###")));
            text.Span(" (ضریب پیشنهادی) ");
        }
        // ضریب منطقه
        if (zaribMantaghe.HasValue)
        {
            text.Span(" × ");
            text.Span(ToPersianDigits(zaribMantaghe.Value.ToString("0.###")));
            text.Span(" (ضریب منطقه‌ای) ");
        }

        // ضریب بالاسری
        if (zaribBalasari.HasValue)
        {
            text.Span(" × ");
            text.Span(ToPersianDigits(zaribBalasari.Value.ToString("0.###")));
            text.Span(" (ضریب بالاسری) ");
        }

        // جمع بدون ضریب
        text.Span(" × ");
        text.Span(ToPersianDigits(chapterTotal.ToString("N0")));


    });
    }

    private IContainer HeaderStyle(IContainer container)
    {
        return container
            .Background("#CFCFCF")
            .Border(1)
            .PaddingVertical(6)
            .PaddingHorizontal(4)
            .AlignCenter()
            .AlignMiddle();
    }

    private IContainer RowStyle(IContainer container)
    {
        return container
            .Border(1)
            .PaddingVertical(4)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle();
    }

    private IContainer TitleStyle(IContainer container)
    {
        return container
            .Background("#BFBFBF")
            .Border(1)
            .Padding(8)
            .AlignRight()
            .AlignMiddle();
    }



}