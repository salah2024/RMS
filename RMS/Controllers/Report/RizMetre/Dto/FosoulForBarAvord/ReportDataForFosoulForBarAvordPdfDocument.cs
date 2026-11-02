using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using System.Globalization;

namespace RMS.Controllers.Report.RizMetre.Dto.FosoulForBarAvord;

public class ReportDataForFosoulForBarAvordPdfDocument : IDocument
{
    private readonly List<ReportDataForFosoulForBarAvordDto> _reportData;
    private readonly string _title;
    private readonly int _year;

    public ReportDataForFosoulForBarAvordPdfDocument(
        List<ReportDataForFosoulForBarAvordDto> reportData)
    {
        _reportData = reportData;
        _title = reportData.FirstOrDefault()?.Title ?? "";
        _year = reportData.FirstOrDefault()?.Year ?? 0;

    }

    public DocumentMetadata GetMetadata()
        => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4.Landscape());

            page.Margin(15);

            page.DefaultTextStyle(x =>
                x.FontFamily("Tahoma")
                 .FontSize(9));

            page.Header()
                .Element(Header);

            page.Content()
                .ContentFromRightToLeft()
                .Element(Content);

            page.Footer()
                .Element(Footer);
        });
    }


    void Content(IContainer container)
    {
        container.Column(column =>
        {
            bool firstReport = true;

            foreach (var report in _reportData)
            {
                // هر فهرست بها از صفحه جدید
                if (!firstReport)
                    column.Item().PageBreak();

                firstReport = false;

                column.Item().Decoration(decoration =>
                {
                    decoration.Before()
                        .Repeat()
                        .Element(c => DrawSectionHeader(c, report));

                    decoration.Content()
                        .Element(c => DrawTable(c, report));
                });
            }
        });
    }



    void DrawSectionHeader(
    IContainer container,
    ReportDataForFosoulForBarAvordDto report)
    {
        container.Column(col =>
        {

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
                    .Text($"فهرست بهای پایه رشته {report.NoeFBName} - سال {ToPersianDigits(_year.ToString())}")
                    .Bold()
                    .FontSize(10);

                row.RelativeItem(1)
                   .AlignRight()
                   .Text("")
                   .Bold()
                   .FontSize(8);
            });

            //col.Item()
            //    .Text($"فهرست بهای {report.NoeFBName}")
            //    .FontSize(12)
            //    .Bold();

            col.Item().PaddingTop(5);

            col.Item()
            .PaddingTop(15)
            .Row(row =>
            {

                row.RelativeItem(2)
                           .AlignRight()
                           .Text(text =>
                           {
                               text.Span("عنوان پروژه : ").FontSize(10).Bold();
                               text.Span(_title).FontSize(10);
                           });

                row.RelativeItem(2)
                          .AlignRight()
                          .Text(text =>
                            {
                                text.Span("دستگاه اجرایی : ").FontSize(10).Bold();
                                text.Span("ساکار").FontSize(10);
                            });

                row.RelativeItem(1)
                            .AlignCenter()
                            .Text("")
                            .Bold()
                            .FontSize(10);

            });

            col.Item()
            .PaddingTop(4)
            .Row(row =>
            {
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
            });

            col.Item()
               .PaddingTop(5)
               .LineHorizontal(2);
            col.Item()
                .PaddingTop(2)
                .PaddingBottom(15)
                .LineHorizontal(1);



        });
    }



    void DrawTable(
    IContainer container,
    ReportDataForFosoulForBarAvordDto report)
    {
        var fosoul = report.resultGetFosoulForBarAvord.Fosoul;

        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(5);      // شرح فصل

                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);

                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
            });

            //-----------------------------------
            // HEADER
            //-----------------------------------

            table.Header(header =>
            {
                header.Cell()
                  .RowSpan(2)
                  .Element(HeaderStyle)
                  .Text("شرح فصل")
                  .Bold();

                header.Cell()
                    .ColumnSpan(3)
                    .BorderTop(1)
                    .Element(HeaderSubStyle)
                    .Text("جمع فصل بدون ضرایب (ريال)")
                    .Bold();

                header.Cell()
                    .ColumnSpan(3)
                    .BorderTop(1)
                    .BorderLeft(1)
                    .Element(HeaderSubStyle)
                    .Text("جمع فصل با ضرایب (ريال)")
                    .Bold();



                header.Cell().BorderBottom(1)
                    .Element(HeaderSubStyle)
                    .Text("پایه");

                header.Cell().BorderBottom(1)
                    .Element(HeaderSubStyle)
                    .Text("غیر پایه");

                header.Cell().BorderBottom(1)
                    .Element(HeaderSubStyle)
                    .Text("جمع");


                header.Cell().BorderBottom(1)
                    .Element(HeaderSubStyle)
                    .Text("پایه");

                header.Cell().BorderBottom(1)
                    .Element(HeaderSubStyle)
                    .Text("غیر پایه");

                header.Cell()
                    .BorderBottom(1)
                    .BorderLeft(1)
                    .Element(HeaderSubStyle)
                    .Text("جمع");

            });

            //-----------------------------------
            // BODY
            //-----------------------------------

            int row = 1;

            decimal sumBase = 0;
            decimal sumStar = 0;
            decimal sumAll = 0;

            decimal sumBaseZ = 0;
            decimal sumStarZ = 0;
            decimal sumAllZ = 0;

            foreach (var item in fosoul)
            {

                table.Cell()
                    .Element(c => GetRowStyle(c, row))
                    .AlignRight()
                    .Text(item.FaslName);

                table.Cell()
                   .Element(c => GetRowStyle(c, row))
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(item.JameFasl.ToString("N0")));

                table.Cell()
                    .Element(c => GetRowStyle(c, row))
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(item.JameFaslStar.ToString("N0")));

                table.Cell()
                    .Element(c => GetRowStyle(c, row))
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(item.JameFaslAll.ToString("N0")));



                table.Cell()
                    .Element(c => GetRowStyle(c, row))
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(item.JameFaslWithZarib.ToString("N0")));

                table.Cell()
                    .Element(c => GetRowStyle(c, row))
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(item.JameFaslStarWithZarib.ToString("N0")));

                table.Cell()
                    .Element(c => GetRowStyle(c, row))
                    .ContentFromLeftToRight()
                    .Text(ToPersianDigits(item.JameFaslWithZaribAll.ToString("N0")));



                sumBase += item.JameFasl;
                sumStar += item.JameFaslStar;
                sumAll += item.JameFaslAll;

                sumBaseZ += item.JameFaslWithZarib;
                sumStarZ += item.JameFaslStarWithZarib;
                sumAllZ += item.JameFaslWithZaribAll;

                row++;
            }

            //-----------------------------------
            // TOTAL
            //-----------------------------------

            table.Cell()
                .ColumnSpan(1)
                .BorderBottom(1)
                .BorderTop(1)
                .BorderRight(1)
                .Element(TotalStyle)
                .AlignRight()
                .Text("جمع");

            table.Cell()
                .BorderBottom(1)
                .BorderTop(1)
                .Element(TotalStyle)
                .ContentFromLeftToRight()
                .Text(ToPersianDigits(sumBase.ToString("N0")));

            table.Cell()
                .BorderBottom(1)
                .BorderTop(1)
                .Element(TotalStyle)
                .ContentFromLeftToRight()
                .Text(ToPersianDigits(sumStar.ToString("N0")));

            table.Cell()
                .BorderBottom(1)
                .BorderTop(1)
                .Element(TotalStyle)
                .ContentFromLeftToRight()
                .Text(ToPersianDigits(sumAll.ToString("N0")));

            table.Cell()
                .BorderBottom(1)
                .BorderTop(1)
                .Element(TotalStyle)
                .ContentFromLeftToRight()
                .Text(ToPersianDigits(sumBaseZ.ToString("N0")));

            table.Cell()
                .BorderBottom(1)
                .BorderTop(1)
                .Element(TotalStyle)
                .ContentFromLeftToRight()
                .Text(ToPersianDigits(sumStarZ.ToString("N0")));

            table.Cell()
                .BorderBottom(1)
                .BorderTop(1)
                .BorderLeft(1)
                .Element(TotalStyle)
                .ContentFromLeftToRight()
                .Text(ToPersianDigits(sumAllZ.ToString("N0")));

        });
    }

    private IContainer GetRowStyle(IContainer container, int row)
    {
        return row % 2 == 0
            ? AlternateRowStyle(container)
            : RowStyle(container);
    }

    void Header(IContainer container)
    {
        container.Column(column =>
        {

            // عنوان گزارش
            //column.Item()
            //    .AlignCenter()
            //    .Text(text =>
            //    {
            //        text.Span("خلاصه مالی فصول ").FontSize(14).Bold();
            //        text.Span(_title).FontSize(14).Bold();
            //    });


            column.Item().Row(row =>
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
                    .Text($"« خلاصه مالی فصول » ")
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


            //// اطلاعات بالای گزارش
            //column.Item().Row(row =>
            //{

            //    row.RelativeItem()
            //        .AlignLeft()
            //        .Text(text =>
            //        {
            //            text.Span("تاریخ چاپ : ").Bold();
            //            text.Span(GetPersianDate(DateTime.Now));
            //        });
            //});

            //column.Item().PaddingTop(5);
        });
    }


    void Footer(IContainer container)
    {
        container.Column(column =>
        {
            column.Item()
                .LineHorizontal(1);

            column.Item()
                .PaddingTop(5);

            column.Item().Row(row =>
            {
                row.RelativeItem()
                    .AlignCenter()
                    .Text("پیمانکار")
                    .Bold();

                row.RelativeItem()
                    .AlignCenter()
                    .Text("مشاور")
                    .Bold();

                row.RelativeItem()
                    .AlignCenter()
                    .Text("کارفرما")
                    .Bold();
            });

            // فضای امضاء
            column.Item()
                .Height(35);
        });
    }

    IContainer HeaderStyle(IContainer container)
    {
        return container
            .Background("#c9c9c9")
            .BorderTop(1)
            .BorderBottom(1)
            .BorderRight(1)
            .BorderColor("#000")
            .PaddingVertical(5)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle();
    }

    IContainer HeaderSubStyle(IContainer container)
    {
        return container
            .Background("#c9c9c9")
            .Border(0.5f)
            .BorderColor("#000")
            .PaddingVertical(4)
            .PaddingHorizontal(2)
            .AlignCenter()
            .AlignMiddle();
    }

    IContainer RowStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .BorderColor("#000")
            .PaddingVertical(5)
            .PaddingHorizontal(1)
            .AlignCenter()
            .AlignMiddle();
    }


    IContainer AlternateRowStyle(IContainer container)
    {
        return container
            .Background("#F3EDF9")
            .Border(0.5f)
            .BorderColor("#000")
            .PaddingVertical(5)
            .PaddingHorizontal(1)
            .AlignCenter()
            .AlignMiddle();
    }

    IContainer TotalStyle(IContainer container)
    {
        return container
            .Background("#d9d9d9")
            .Border(0.8f)
            .BorderColor("#666666")
            .PaddingVertical(5)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle();
    }


    public string ToPersianDigits(string input)
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

    private string GetPersianDate(DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();

        return $"{ToPersianDigits(pc.GetYear(date).ToString("0000"))}/" +
               $"{ToPersianDigits(pc.GetMonth(date).ToString("00"))}/" +
               $"{ToPersianDigits(pc.GetDayOfMonth(date).ToString("00"))}";
    }
}
