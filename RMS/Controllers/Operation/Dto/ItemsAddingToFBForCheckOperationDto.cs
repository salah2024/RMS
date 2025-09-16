namespace RMS.Controllers.Operation.Dto
{
    public class ItemsAddingToFBForCheckOperationDto
    {
        public long ItemsHasCondition_ConditionContextId { get; set; }
        public string AddedItems { get; set; }
        public string? Condition {  get; set; }
        public string? FinalWorking {  get; set; }
        public short ConditionType { get; set; }
        public string? DesOfAddingItems { get; set; }
        public string? UseItemForAdd {  get; set; }
        public string? FieldsAdding {  get; set; }
        public string? CharacterPlus {  get; set; }
        public string? ConditionContextRel {  get; set; }
        public long? ConditionContextId {  get; set; }
    }
}
