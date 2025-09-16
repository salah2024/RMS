namespace RMS.Controllers.Operation.Dto
{
    public class ItemsAddingToFBEzafeBahaAndLakeGiriDto
    {
        public long ID { get; set; }
        public int ConditionType { get; set; }
        public long ItemsHasCondition_ConditionContextId { get; set; }
        public string AddedItems { get; set; }
        public string? Condition { get; set; }
        public string? FinalWorking { get; set; }
        public string? CharacterPlus { get; set; }
        public string? ConditionContextRel { get; set; }
        public long ConditionContextId { get; set; }
        public string? FieldsAdding { get; set; }
        public string? UseItemForAdd { get; set; }
        public string? DesOfAddingItems { get; set; }

    }
}
