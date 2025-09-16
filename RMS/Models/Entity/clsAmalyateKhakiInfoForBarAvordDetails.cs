using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblAmalyateKhakiInfoForBarAvordDetails")]
public class clsAmalyateKhakiInfoForBarAvordDetails : clsBaseEntity
{
    [ForeignKey("AmalyateKhakiInfoForBarAvord")]
    public Guid AmalyateKhakiInfoForBarAvordId;
    public clsAmalyateKhakiInfoForBarAvord AmalyateKhakiInfoForBarAvord { get; set; }

    /// <summary>
    ///10- لجن برداری
    ///11- برداشت خاک نباتی
    ///12- خاکبرداری در زمین نوع 1
    ///13- خاکبرداری در زمین نوع 2
    ///14- خاکبرداری در زمین نوع 3
    ///15- خاکبرداری در زمین نوع 4
    ///16- خاکبرداری در زمین نوع 5
    ///17- خاکبرداری در زمین نوع 6
    ///18- خاکبرداری در زمین نوع 7
    /// </summary>

    [ForeignKey("NoeKhakBardari")]
    public long NoeKhakBardariId { get; set; }
    public clsNoeKhakBardari NoeKhakBardari { get; set; }
    public decimal Value { get; set; }
    public string Name { get; set; }
    public bool boolValue { get; set; }
    public ICollection<clsAmalyateKhakiInfoForBarAvordDetailsMore> lstAmalyateKhakiInfoForBarAvordDetailsMore { get; set; }
}