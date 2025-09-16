namespace RMS.Controllers.Operation.Dto
{
    public class ItemsHasConditionAddedToFBDto
    {
        public Guid ID { get; set; }
        public string FBShomareh { get; set; }
        public Guid BarAvordId { get; set; }
        public decimal Meghdar { get; set; }
        public decimal Meghdar2 { get; set; }
        public long ItemsHasCondition_ConditionContextId { get; set; }
        public long ConditionGroupId { get; set; }
        public string ItemFBShomareh { get; set; }
        public string? ConditionContextRel { get; set; }
        public long ConditionContextId { get; set; }

    }
}
