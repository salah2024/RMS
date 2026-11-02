using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RMS.Models.Entity;
using System.Globalization;
using static Stimulsoft.Report.StiOptions;

namespace RMS.Controllers.Report.RizMetre.Dto.FB;

public class FBSummaryPdfDocument : IDocument
{
    private readonly List<ReportDataForFBSummaryDto> _items;
    private readonly clsBaravordUser _baravordUser;
    private readonly string _title;
    private readonly int _year;

    public FBSummaryPdfDocument(ReportDataForFBSummaryL1Dto reportDataForFBSummaryL)
    {
        _items = reportDataForFBSummaryL.reportDataForFBSummary;
        _title = reportDataForFBSummaryL.reportDataForFBSummary.FirstOrDefault()?.Title ?? "";
        _year = reportDataForFBSummaryL.reportDataForFBSummary.FirstOrDefault()?.Year ?? 0;
        _baravordUser = reportDataForFBSummaryL.baravordUser;

    }

    public DocumentMetadata GetMetadata()
        => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4.Landscape());

            page.Margin(20);

            page.DefaultTextStyle(x =>
                x.FontFamily("Tahoma")
                 .FontSize(10));

            page.Content()
                .ContentFromRightToLeft()
                .Element(Content);
        });
    }

    private void Content(IContainer container)
    {
        container.Column(column =>
        {
            //column.Item()
            //    .AlignCenter()
            //    .Text("خلاصه مالی فهرست بها")
            //    .Bold()
            //    .FontSize(14);

            column.Item()
           .PaddingTop(10)
           .Row(row =>
           {
               row.RelativeItem(2)
                   .AlignRight()
                   .Text(text =>
                   {
                       text.Span("شماره پیمان : ").FontSize(8).Bold();
                       text.Span("524555").FontSize(8);
                   });


               // عنوان
               row.RelativeItem(4)
                   .AlignCenter()
                   .Text($"« مبالغ دفترچه ها {_baravordUser.Name}  » ")
                   .Bold()
                   .FontSize(12);


               row.RelativeItem(2)
                    .AlignLeft()
                    .Text(text =>
                    {
                        text.Span("تاریخ پیمان : ").FontSize(8).Bold();
                        text.Span(GetPersianDate(DateTime.Now)).FontSize(8);
                    });
             
           });

            column.Item().PaddingTop(5);


            column
            .Item()
            .PaddingTop(5)
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


            column.Item()
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


            column.Item()
           .PaddingTop(5)
           .LineHorizontal(2);


            column.Item()
                .PaddingTop(2)
                .PaddingBottom(15)
                .LineHorizontal(1);



            column.Item().Table(table =>
            {

                column.Item()
               .PaddingTop(15);

                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);      // شرح فصل
                    columns.RelativeColumn(5);      // شرح فصل

                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);

                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });


                //---------------- Header ----------------
                table.Header(header =>
                {
                    header.Cell()
                      .RowSpan(2)
                      .BorderRight(1)
                      .BorderTop(1)
                      .BorderBottom(1)
                      .Element(HeaderSubStyle)
                      .Text("ردیف")
                      .Bold();

                    header.Cell()
                      .RowSpan(2)
                      .BorderTop(1)
                      .BorderBottom(1)
                      .Element(HeaderSubStyle)
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

                int row = 1;

                decimal? grandBase = null;
                decimal? grandStar = null;
                decimal? grandTotal = null;

                decimal? grandBaseZ = null;
                decimal? grandStarZ = null;
                decimal? grandTotalZ = null;

                foreach (var item in _items)
                {
                    // ردیف
                    table.Cell()
                        .Element(CellStyle)
                        .Text(ToPersianDigits(row.ToString()));

                    // شرح
                    table.Cell()
                        .Element(CellStyle)
                        .AlignRight()
                        .Text(item.NoeFBName);

                    //-------------------------
                    // بدون ضرایب
                    //-------------------------

                    table.Cell()
                        .Element(CellStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.Base == null || item.Base == 0 ? "" : item.Base.Value.ToString("N0")));

                    table.Cell()
                        .Element(CellStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.Star == null || item.Star == 0 ? "" : item.Star.Value.ToString("N0")));

                    table.Cell()
                        .Element(CellStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.Total == null || item.Total == 0 ? "" : item.Total.Value.ToString("N0")));

                    //-------------------------
                    // با ضرایب
                    //-------------------------

                    table.Cell()
                        .Element(CellStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.BaseZ == null || item.BaseZ == 0 ? "" : item.BaseZ.Value.ToString("N0")));

                    table.Cell()
                        .Element(CellStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.StarZ == null || item.StarZ == 0 ? "" : item.StarZ.Value.ToString("N0")));

                    table.Cell()
                        .Element(CellStyle)
                        .ContentFromLeftToRight()
                        .Text(ToPersianDigits(item.TotalZ == null || item.TotalZ == 0 ? "" : item.TotalZ.Value.ToString("N0")));

                    //-------------------------
                    // جمع کل
                    //-------------------------

                    grandBase += item.Base;
                    grandStar += item.Star;
                    grandTotal += item.Total;

                    grandBaseZ += item.BaseZ;
                    grandStarZ += item.StarZ;
                    grandTotalZ += item.TotalZ;

                    row++;
                }

            });
        });
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

    IContainer HeaderStyle(IContainer container)
    {
        return container
            .Background("#D9D9D9")
            .Border(1)
            .Padding(5)
            .AlignCenter();
    }

    IContainer CellStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .Padding(5)
            .AlignCenter();
    }

    IContainer TotalStyle(IContainer container)
    {
        return container
            .Background("#EEEEEE")
            .Border(1)
            .Padding(5)
            .AlignCenter();
    }

    public string ToPersianDigits(string input)
    {
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