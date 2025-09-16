namespace RMS.Models.Dto.TreeDto
{
    public class OperationForTreeDto
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string OperationName { get; set; }
        public string? FunctionCall { get; set; }
        public string ItemsFBShomareh { get; set; }
    }
}
