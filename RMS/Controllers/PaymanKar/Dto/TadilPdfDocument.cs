using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static Stimulsoft.Report.StiRecentConnections;

namespace RMS.Controllers.PaymanKar.Dto;

public class TadilPdfDocument : IDocument
{
    private readonly List<TadilPdfVm> _data;

    public TadilPdfDocument(
        List<TadilPdfVm> data)
    {
        _data = data;
    }

    public DocumentMetadata GetMetadata()
    {
        return new DocumentMetadata
        {
            Title = "گزارش تعدیل"
        };
    }

    public void Compose(
        IDocumentContainer container)
    {
        foreach (var model in _data)
        {
            container.Page(page =>
            {
                page.DefaultTextStyle(x =>
                x.FontFamily("Tahoma")
                .FontSize(8));

                page.Size(PageSizes.A4.Landscape());

                page.Margin(20);

                page.DefaultTextStyle(x => x.FontSize(8));

                page.Header()
                    .Element(c => DrawHeader(c, model));

                page.Content()
                    .Element(c => DrawBody(c, model));

                page.Footer()
                    .PaddingTop(10)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Padding(20)
                            .AlignCenter()
                            .Text("پیمانکار");

                        row.RelativeItem()
                            .Padding(20)
                            .AlignCenter()
                            .Text("ناظر");

                        row.RelativeItem()
                            .Padding(20)
                            .AlignCenter()
                            .Text("مشاور");

                        row.RelativeItem()
                            .Padding(20)
                            .AlignCenter()
                            .Text("کارفرما");
                    });
            });
        }

        
    }


    private void DrawHeader(
    IContainer container,
    TadilPdfVm model)
    {
        container.Column(col =>
        {
            col.Item()
                .Row(row =>
                {
                    row.RelativeItem()
                        .AlignLeft()
                        .Text($"از تاریخ : {ToPersianDigits(model.FromDate)} - تا تاریخ : {ToPersianDigits(model.ToDate)}")
                        .FontSize(10);

                    row.RelativeItem()
                        .AlignCenter()
                        .Text($"تعدیل {model.Title}")
                        .FontSize(12)
                        .Bold();

                    row.RelativeItem();
                });

            col.Item()
                .AlignCenter()
                .Text($"فهرست بهای {ToPersianDigits(model.NoeFBName)}")
                .FontSize(11)
                .Bold();

            col.Item()
                .PaddingTop(5)
                .Row(row =>
                {
                    row.RelativeItem().Text(text =>
                    {
                        text.Span("صفحه ");
                        ToPersianDigits(text.CurrentPageNumber().ToString());
                        text.Span(" از ");
                        ToPersianDigits(text.TotalPages().ToString());
                    });

                    row.RelativeItem()
                        .AlignRight()
                        .Row(r =>
                        {
                            r.AutoItem()
                                .Text($"دوره مبنای پیمان : {ToPersianDigits(model.MabnaIndexTitle)}")
                                .FontSize(11);

                            r.ConstantItem(40);

                            r.AutoItem()
                                .Text($"نام پروژه : {ToPersianDigits(model.ProjectName)}")
                                .FontSize(11);
                        });
                });

            col.Item().PaddingBottom(10);
        });
    }


    private void DrawBody(
    IContainer container,
    TadilPdfVm model)
    {
        container.Table(table =>
        {
            

            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(); // مبلغ تعدیل
                columns.ConstantColumn(40); // ضریب
                columns.RelativeColumn(); // شاخص دوره
                columns.RelativeColumn(); // شاخص مبنا
                columns.RelativeColumn(); // مبلغ کارکرد
                columns.RelativeColumn(); // نسبت
                columns.RelativeColumn(); // مابه‌التفاوت
                columns.RelativeColumn(); // مبلغ قبلی
                columns.RelativeColumn(); // مبلغ فعلی
                columns.ConstantColumn(35); // فصل
                columns.ConstantColumn(40); // دوره

                columns.ConstantColumn(30); // سال (باریک)
            });

            table.Header(header =>
            {
                HeaderCell(header, "مبلغ تعدیل");
                HeaderCell(header, "ضریب تعدیل");
                HeaderCell(header, "شاخص دوره کارکرد");
                HeaderCell(header, "شاخص مبنا");
                HeaderCell(header, "مبلغ کارکرد دوره");
                HeaderCell(header, "نسبت کارکرد در دوره بمدت کارکرد");
                HeaderCell(header, "مابه‌التفاوت دو صورت وضعیت");
                HeaderCell(header, "مبلغ صورت وضعیت قبلی");
                HeaderCell(header, "مبلغ صورت وضعیت فعلی");
                HeaderCell(header, "فصل");
                HeaderCell(header, "دوره");
                HeaderCell(header, "سال");
            });

            decimal sumMablaghCurrent =
                model.Rows.Sum(x => x.MablaghCurrent);

            decimal sumMablaghPrev =
                model.Rows.Sum(x => x.MablaghPrev);

            decimal sumDeff =
                model.Rows.Sum(x => x.Deff);

            decimal sumKarkard =
                model.Rows.Sum(x => x.Item.MablaghKarkardDoreh);

            decimal sumTadil =
                model.Rows.Sum(x => x.Item.MablaghTadil);

            foreach (var r in model.Rows)
            {
                Cell(table, ToPersianDigits(r.Item.MablaghTadil.ToString("N0")));
                Cell(table, ToPersianDigits(r.Item.ZaribTadil.ToString("N3")));
                Cell(table, ToPersianDigits(r.Item.IndexValue.ToString("N1")));
                Cell(table, ToPersianDigits(r.Item.PGHIndexValue?.ToString("N1")));

                Cell(table, ToPersianDigits(r.Item.MablaghKarkardDoreh.ToString("N0")));

                //Cell(table,
                //$"{ToPersianDigits(r.Item.Days.ToString())}/{ToPersianDigits("715")}");
                //Cell(table, ToPersianDigits(r.Item.Nesbat.ToString("N3")));

                table.Cell()
                .Border(1)
    .Padding(3)
    .AlignCenter()
                    .Column(col =>
    {
        col.Item()
            .AlignCenter()
            .Text(ToPersianDigits(r.Item.Days.ToString()));

        col.Item()
            .LineHorizontal(1);

        col.Item()
            .AlignCenter()
            .Text(ToPersianDigits(model.AllDay.ToString()));
    });


                Cell(table, ToPersianDigits(r.Deff.ToString("N0")));
                Cell(table, ToPersianDigits(r.MablaghPrev.ToString("N0")));
                Cell(table, ToPersianDigits(r.MablaghCurrent.ToString("N0")));

                Cell(table, ToPersianDigits(r.Fasl));

                Cell(table,
                    GetCourseTitle(
                        r.Item.CourseType,
                        r.Item.CourseCode));

                Cell(table,
                    ToPersianDigits(
                        r.Item.Year.ToString()));
            }

            Cell(table, ToPersianDigits(sumTadil.ToString("N0")));

            table.Cell().Border(1).Text("");
            table.Cell().Border(1).Text("");
            table.Cell().Border(1).Text("");

            Cell(table, ToPersianDigits(sumKarkard.ToString("N0")));

            table.Cell().Border(1).Text("");

            Cell(table, ToPersianDigits(sumDeff.ToString("N0")));
            Cell(table, ToPersianDigits(sumMablaghPrev.ToString("N0")));
            Cell(table, ToPersianDigits(sumMablaghCurrent.ToString("N0")));

            table.Cell()
                .ColumnSpan(3)
                .Border(1)
                .Background(Colors.Grey.Lighten2)
                .AlignCenter()
                .Text("جمع کل")
                .Bold();
        });
    }

 


    private static string ToPersianDigits(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

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

    private static void HeaderCell(
        TableCellDescriptor cell,
        string text)
    {
        cell.Cell()
            .Background("#D9D9D9")
            .Border(1)
            .Padding(5)
            .AlignCenter()
            .Text(text)
            .Bold();
    }

    private static void Cell(
        TableDescriptor table,
        string text)
    {
        table.Cell()
            .Border(1)
            .Padding(3)
            .AlignCenter()
            .Text(text ?? "")
            .FontSize(11);
    }

    private string GetCourseTitle(int courseType, int courseCode)
    {
        if (courseType == 1) // فصلی
        {
            return courseCode switch
            {
                1 => "بهار",
                2 => "تابستان",
                3 => "پاییز",
                4 => "زمستان",
                _ => ""
            };
        }

        if (courseType == 2) // ماهانه
        {
            return courseCode switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",
                _ => ""
            };
        }

        return "";
    }
}
