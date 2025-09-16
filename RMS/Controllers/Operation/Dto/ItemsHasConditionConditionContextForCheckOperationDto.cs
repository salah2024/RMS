namespace RMS.Controllers.Operation.Dto
{
    public class ItemsHasConditionConditionContextForCheckOperationDto
    {
        public long Id { get; set; }
        public long ItemsHasConditionId { get; set; }
        public long ConditionContextId { get; set; }
        public bool HasEnteringValue { get; set; }  
        public string? Des { get; set; }
        public string? DefaultValue { get; set; }
        public bool IsShow { get;set; }
        public long? ParentId { get;set; }
        public bool MoveToRel { get; set; }
        public bool ViewCheckAllRecords { get; set; }
        public string? StepChange { get; set; }
        public decimal Meghdar { get; set; }
        public decimal Meghdar2 { get; set; }
        public string FBShomareh { get; set; }
        public long ConditionGroupId { get;set; }
        public long OperationId { get; set; }
        public Guid BarAvordId { get; set; }

    }
}
