using static RMS.Models.Common.EnumForEntity;

namespace RMS.Controllers.Operation.Dto;

public class ConditionCaseCommonCase1Dto
{
    public string strFinalWorking { get; set; }
    public decimal Meghdar { get; set; }
    public decimal Meghdar2 { get; set; }
    public string strCondition { get; set; }
    public string strItemsFBShomareh { get; set; }
    public long lngItemsHasCondition_ConditionContextId { get; set; }
    public long ConditionGroupId { get; set; }
    public Guid BarAvordId { get; set; }
    public NoeFehrestBaha NoeFBId { get; set; }
    public string strCharacterPlus { get; set; }
    public string FBShomarehAddedItem { get; set; }
    public string DesOfAddingItems { get; set; }
    public Guid BaseFBId { get; set; }
    public int Year { get; set; }
    public string? FieldsAdding { get; set; }
    public long ShomarehNew { get; set; }
    public DateTime Now { get; set; }
    public string? ConditionContextRel { get; set; }
    public long? ConditionContextId { get; set; }
    public string blnCheckIsExistErrors { get; set; }
    public int LevelNumber { get; set; }
    public bool blnCheckIsExistWarning { get; set; }
    public string strWarning { get; set; }
    public bool blnCheckAgain { get; set; }
    public Guid? RMIdForEdit { get; set; }
    public bool Status { get; set; }

}
