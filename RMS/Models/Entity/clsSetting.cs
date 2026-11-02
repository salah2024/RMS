using System.ComponentModel.DataAnnotations.Schema;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.Models.Entity;

[Table("tblSetting")]
public class clsSetting:clsBaseEntity
{
    public Guid UserId { get; set;  }
    /// <summary>
    /// نوع تنظیمات
    /// </summary>
    public SettingType SettingType { get; set;  }
    /// <summary>
    /// نوع فیلد استفاده شده
    /// مثلا اگر 1 باشد به معنی ترو یا فالس بودن مقدار بوده
    /// و فیلد
    /// BoolValue
    /// مقدار میگیرد
    /// و اگر برابر 
    /// Number
    /// بود 
    /// NumberValue
    ///مقدار میگیرد
    /// </summary>
    public VariableType VariableType { get; set; }
    public bool? BoolValue { get; set; }
    public decimal? NumberValue { get; set; }
    public string? StringValue { get; set; }

}
