
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RMS.Models.Entity;
using System.Globalization;

namespace RMS.Controllers.Report.RizMetre.Dto.RizMetre;

public class RizMetrePdfDocument : IDocument
{
    private readonly List<RizMetreReportGroupVm> _groups;
    private readonly clsBaravordUser _baravordUser;
    string _strBATitle="";

    public RizMetrePdfDocument(List<RizMetreReportGroupVm> groups, clsBaravordUser baravordUser,string strBATitle)
    {
        _groups = groups;
        _baravordUser = baravordUser;
        _strBATitle = strBATitle;
    }

    public DocumentMetadata GetMetadata()
        => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.DefaultTextStyle(x =>
                x.FontFamily("Tahoma")
                .FontSize(9));

            page.Size(PageSizes.A4);

            page.Margin(15);


            page.Header().Element(Header);

            page.Content().ContentFromRightToLeft()
                            .Element(Content);


            //page.Footer().AlignCenter().Text(x =>
            //{
            //    x.Span("صفحه ");
            //    x.CurrentPageNumber();
            //    x.Span(" از ");
            //    x.TotalPages();
            //});


            page.Footer().Height(80).Column(col =>
            {
                col.Item().Row(row =>
                {

                    //.Text($"پیمانکار: {group.Peymankar}")
                    row.RelativeItem().AlignCenter().Text("پیمانکار").Bold();
                    row.RelativeItem().AlignCenter().Text("نظارت مقیم مشاور").Bold();
                    row.RelativeItem().AlignCenter().Text("مشاور").Bold();
                    row.RelativeItem().AlignCenter().Text("کارفرما").Bold();
                });

                col.Item().Height(45); // فضای امضا

                //col.Item().AlignCenter().Text(text =>
                //{
                //    text.Span("صفحه ");
                //    text.CurrentPageNumber();
                //    text.Span(" از ");
                //    text.TotalPages();
                //});
            });
        });
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


    void Header(IContainer container)
    {

    }

    IContainer HeaderStyle(IContainer container)
    {
        return container
            .Background("#c9c9c9")
            .Border(1)
            .PaddingVertical(3)
            .PaddingHorizontal(1)
            .AlignCenter()
            .AlignMiddle();
    }

    IContainer RowStyle(IContainer container)
    {
        return container
            .Background("#fff")
            .Border(1)
            .PaddingVertical(2)
            .PaddingHorizontal(2)
            .AlignCenter()
            .AlignMiddle();
    }
    IContainer HeaderStyleItems(IContainer container)
    {
        return container
            .Background("#9c9c9c")
            .Border(1)
            .PaddingVertical(8)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle();
    }

    IContainer RowStyleItems(IContainer container)
    {
        return container
            .Background("#d9d9d9")
            .Border(1)
            .PaddingVertical(5)
            .PaddingHorizontal(3)
            .AlignCenter()
            .AlignMiddle();
    }

    private string GetPersianDate(DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();

        return ToPersianDigits(
            $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}");
    }

    void Content(IContainer container)
    {
        container.Column(column =>
        {
            bool firstGroup = true;

            foreach (var group in _groups)
            {
                if (!firstGroup)
                    column.Item().PageBreak();

                firstGroup = false;

                column.Item().Decoration(decoration =>
                {
                    decoration.Before().Repeat().Column(col =>
                    {
                        col.Item().Row(row =>
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
                                .Text($"« ریزه متره {_baravordUser.Name} »")
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

                        //col.Item()
                        //    .AlignCenter()
                        //    .Text($"« ریزه متره {_baravordUser.Name} »")
                        //    .FontSize(14)
                        //    .Bold();

                        col.Item()
                            .AlignCenter()
                            .PaddingTop(10)
                            .Text($"فهرست بهای {group.NoeFBName} سال {ToPersianDigits(group.Year.ToString())}")
                            .FontSize(10)
                            .Bold();

                        col.Item()
                            .PaddingTop(15)
                            .Row(row =>
                            {
                                //row.RelativeItem()
                                //    .AlignRight()
                                //    .Text($"عنوان پروژه: {_strBATitle}").Bold()
                                //    .FontSize(10);

                                row.RelativeItem(2)
                                    .AlignRight()
                                    .Text(text =>
                                    {
                                        text.Span("عنوان پروژه : ").FontSize(10).Bold();
                                        text.Span(_strBATitle).FontSize(10);
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
                                        text.Span("پیمانکار : ").FontSize(10).Bold();
                                        text.Span("ساکار").FontSize(10);
                                    });

                                row.RelativeItem(2)
                                    .AlignRight()
                                    .Text(text =>
                                    {
                                        text.Span("مشاور : ").FontSize(10).Bold();
                                        text.Span("پاد راه").FontSize(10);
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
                            .LineHorizontal(1);
                    });

                    decoration.Content().Column(content =>
                    {
                        foreach (var item in group.Items)
                        {
                            decimal totalQty =
                                item.Details.Sum(x => x.JameJoz ?? 0);

                            decimal totalPrice =
                                item.Details.Sum(x => x.BahayeKol ?? 0);

                            content.Item()
                                .PaddingTop(10)
                                .Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(6);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn(3);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(HeaderStyleItems).Text("شماره").Bold();
                                        header.Cell().Element(HeaderStyleItems).Text("شرح").Bold();
                                        header.Cell().Element(HeaderStyleItems).Text("مقدار").Bold();
                                        header.Cell().Element(HeaderStyleItems).Text("واحد").Bold();
                                        header.Cell().Element(HeaderStyleItems).Text("بهای واحد(ريال)").Bold();
                                        header.Cell().Element(HeaderStyleItems).Text("بهای کل(ريال)").Bold();
                                    });

                                    string strStar = "";

                                    if (item.ItemBahayeVahedNew != null &&
                                        item.ItemBahayeVahedNew != 0)
                                    {
                                        strStar = "*";
                                    }

                                    table.Cell().Element(RowStyleItems)
                                        .Text(strStar + "   " + ToPersianDigits(item.ItemShomareh));

                                    table.Cell().Element(RowStyleItems).AlignRight()
                                        .Text(ToPersianDigits(item.ItemSharh));

                                    table.Cell().Element(RowStyleItems)
                                    .ContentFromLeftToRight()
                                        .Text(ToPersianDigits(totalQty.ToString("G29")));

                                    table.Cell().Element(RowStyleItems)
                                        .Text(item.ItemVahed);

                                    table.Cell().Element(RowStyleItems).ContentFromLeftToRight()
                                        .Text(ToPersianDigits(item.ItemBahayeVahed?.ToString("N0")));

                                    table.Cell().Element(RowStyleItems).ContentFromLeftToRight()
                                        .Text(ToPersianDigits(totalPrice.ToString("N0")));
                                });

                            content.Item()
                                .PaddingTop(3)
                                .Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(6);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(2);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(HeaderStyle).Text("ردیف").Bold();
                                        header.Cell().Element(HeaderStyle).Text("شرح").Bold();
                                        header.Cell().Element(HeaderStyle).Text("تعداد").Bold();
                                        header.Cell().Element(HeaderStyle).Text("طول").Bold();
                                        header.Cell().Element(HeaderStyle).Text("عرض").Bold();
                                        header.Cell().Element(HeaderStyle).Text("ارتفاع").Bold();
                                        header.Cell().Element(HeaderStyle).Text("وزن").Bold();
                                        header.Cell().Element(HeaderStyle).Text("مقدار جزء").Bold();
                                    });

                                    int row = 1;

                                    foreach (var d in item.Details)
                                    {
                                        string strDes = d.Sharh ?? "";

                                        if (!string.IsNullOrWhiteSpace(d.Tozihat))
                                            strDes += " - " + d.Tozihat;

                                        table.Cell().Element(RowStyle)
                                            .Text(ToPersianDigits(row.ToString()));

                                        table.Cell().Element(RowStyle)
                                            .Text(ToPersianDigits(strDes));

                                        table.Cell().Element(RowStyle)
                                        .ContentFromLeftToRight()
                                            .Text(ToPersianDigits(d.Tedad?.ToString("G29")));

                                        table.Cell().Element(RowStyle).ContentFromLeftToRight()
                                            .Text(ToPersianDigits(d.Tool?.ToString("G29")));

                                        table.Cell().Element(RowStyle).ContentFromLeftToRight()
                                            .Text(ToPersianDigits(d.Arz?.ToString("G29")));

                                        table.Cell().Element(RowStyle).ContentFromLeftToRight()
                                            .Text(ToPersianDigits(d.Ertefa?.ToString("G29")));

                                        table.Cell().Element(RowStyle).ContentFromLeftToRight()
                                            .Text(ToPersianDigits(d.Vazn?.ToString("G29")));

                                        table.Cell().Element(RowStyle).ContentFromLeftToRight()
                                            .Text(ToPersianDigits(d.JameJoz?.ToString("G29")));

                                        row++;
                                    }
                                });
                        }
                    });
                });
            }
        });
    }

}