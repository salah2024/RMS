namespace RMS.Controllers.BaseInfo.Dto
{
    public class GetFosoulOutputDto
    {
        public long Id { get; set; }
        public string FaslName { get; set; }
        public string Code { get; set; }
        public long Order { get; set; }
        public decimal JameFasl { get; set; }
        public string Description { get; set; }
    }
}
