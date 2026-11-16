using RMS.Models.Common.Dto;

namespace RMS.Models.Common;

public class AbnieFaniCommon()
{
    public static readonly RangeItemForAbnieFaniDto[] arrItemsForInsert =
    {
            new() { MinValue = 0, MaxValue = 2, Code = "080201" },
            new() { MinValue = 2, MaxValue = 3, Code = "080202" },
            new() { MinValue = 3, MaxValue = 5, Code = "080203" },
            new() { MinValue = 5, MaxValue = 7, Code = "080204" },
            new() { MinValue = 7, MaxValue = 10, Code = "080205" }
    };


    public static async Task<List<RangeItemForAbnieFaniDto>> GetRangeItems()
    {
        // فعلاً دریافت از آرایه
        return AbnieFaniCommon.arrItemsForInsert.ToList();

        // در آینده فقط این قسمت را جایگزین کن:
        // return await _context.RangeItems.ToListAsync();
    }

    public static async Task<string> GetRangeCode(decimal value)
    {
        var items = await GetRangeItems();

        return items
            .Where(x => value >= x.MinValue && value < x.MaxValue)
            .Select(x => x.Code)
            .FirstOrDefault();
    }

}
