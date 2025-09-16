namespace RMS.Controllers.Board.Dto
{
    public class BoardDataDto
    {
        public Guid BaravordId { get; set; }
        public Guid barAvordAddedBoardId { get; set; }
        public int Shape { get; set; }
        public long OperationId { get; set; }
        public long Tedad { get; set; }
        public string Sharh { get; set; }
        public decimal Arz { get; set; }
        public decimal Ertefa { get; set; }
        public bool UsePOP { get; set; }
        public decimal? PercentPrintPOP { get; set; }
        public List<SelectionItem> Items { get; set; }
    }
    public class BoardDataForSaveDto
    {
        public Guid BaravordId { get; set; }
        public int Shape { get; set; }
        public long OperationId { get; set; }
        public long Tedad { get; set; }
        public string Sharh { get; set; }
        public decimal Arz { get; set; }
        public decimal Ertefa { get; set; }
        public bool UsePOP { get; set; }
        public decimal? PercentPrintPOP { get; set; }
        public List<SelectionItem> Items { get; set; }
    }

    public class SelectionItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class BoardDataForDeleteDto
    {
        public Guid barAvordAddedBoardId { get; set; }
    }

}
