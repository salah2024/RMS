using System.ComponentModel;
using System.Reflection;

namespace RMS.Controllers.KhakRizi.EnumKhakRizi;


public enum EnumRoadType
{
    [Description("آزاد راه، بزرگراه، راه اصلی، راه فرعی درجه یک")]
    Main = 1,
    [Description("راه فرعی درجه 2 و راههای روستایی")]
    Secondary = 2
}

public enum EnumNoeDaneBandi
{
    [Description("خاک درشت دانه شامل گروه های A1، A2، A3 مطابق T145 آشتو")]
    Dorosht = 1,
    [Description("خاک های ریزدانه شامل گروه های A4، A5، A6، A7 مطابق T145 آشتو")]
    Riz = 2
}

public enum EnumHajmKhakRizi
{
    [Description("حجم خاکریزی قشرهای بین 30 سانتیمتر تا بستر روسازی")]
    Top30 = 1,
    [Description("حجم خاکریزی قشرهای پایین تر از 30 سانتیمتر بستر روسازی")]
    Low30 = 2
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum Value)
    {
        var type = Value.GetType();
        var field = type.GetField(Value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? Value.ToString();
    }
    public static List<EnumItem> GetEnumList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
                   .Cast<Enum>()
                   .Select(e => new EnumItem
                   {
                       Id = Convert.ToInt64(e),
                       Description = e.GetDescription()
                   }).ToList();
    }
}


public class EnumItem
{
    public long Id { get; set; }
    public string Description { get; set; }
}


