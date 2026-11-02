using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.StoredProceduresData;

[NotMapped]
public class GetOperationNameHasValueDto
{
    public long OperationId { get; set; }
    public long ParentId { get; set; }
    public string OperationLatinName { get; set; }
    public string MaxMeghdarMeghias { get; set; }
    public string MaxCodeAmelMoaser { get; set; }
}
