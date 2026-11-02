using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.StoredProceduresData;

[NotMapped]
public class OperationNameAnalizBahaWithOperationDto
{
    public string OperationExceptOpNodeLatinName { get; set; }
    public string CodeAmel { get; set; }
    public string NoeMashinAlatName { get; set; }
    public string MashinTypeName { get; set; }
    public string Title { get; set; }
    public decimal MeghdarMeghias { get; set;  }
    public int ZaribKarKard { get; set;  }
    public bool IsAmelMoaser { get; set;  }
}
