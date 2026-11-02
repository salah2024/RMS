using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity;

[Table("tblPermission")]
public class clsPermission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, MaxLength(200)]
    public string Key { get; set; } = default!;  

    [Required, MaxLength(200)]
    public string Title { get; set; } = default!; 

    [MaxLength(100)]
    public string? Group { get; set; } 

    public bool IsActive { get; set; } = true;

    public ICollection<clsRolePermission> RolePermissions { get; set; } = new List<clsRolePermission>();
    public ICollection<clsMenuPermission> MenuPermissions { get; set; } = new List<clsMenuPermission>();
}
