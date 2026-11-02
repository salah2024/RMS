using RMS.Controllers.KhakRizi.EnumKhakRizi;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblMenuItem")]
    public class clsMenuItem
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = default!;

        [MaxLength(400)]
        public string? Url { get; set; }          

        [MaxLength(100)]
        public string? Icon { get; set; }          

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public int? ParentId { get; set; }
        public clsMenuItem? Parent { get; set; }
        public ICollection<clsMenuItem> Children { get; set; } = new List<clsMenuItem>();

        public ICollection<clsMenuPermission> MenuPermissions { get; set; } = new List<clsMenuPermission>();
    }
}
