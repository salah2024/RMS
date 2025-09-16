namespace RMS.Controllers.Operation.Dto
{
    public class GetAllDescendantIdsDto
    {
        public long ID { get; set; }
        public long? ParentId { get; set; }
        public long Order { get; set; }
        public string OperationName { get; set; }
        public string FunctionCall { get; set; }
        public string Sharh { get; set; }
        public string ItemsFBShomareh { get; set; }
        public int Year { get; set; }
    }
}
