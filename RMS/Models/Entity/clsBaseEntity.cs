namespace RMS.Models.Entity;

public class clsBaseEntity
{
    public Guid ID { get; set; }
    public DateTime? InsertDateTime { get; set; }
    public DateTime? RemoveDateTime { get; set; }
    public Guid? UserInserter { get; set; }
    public Guid? UserRemover { get; set; }
    public bool? IsDeleted { get; set; }
}
