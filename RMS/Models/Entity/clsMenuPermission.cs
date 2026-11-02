using RMS.Controllers.KhakRizi.EnumKhakRizi;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace RMS.Models.Entity
{
    [Table("tblMenuPermission")]
    public class clsMenuPermission
    {
        public int MenuItemId { get; set; }
        public clsMenuItem MenuItem { get; set; } = default!;

        public int PermissionId { get; set; }
        public clsPermission Permission { get; set; } = default!;
    }
}
