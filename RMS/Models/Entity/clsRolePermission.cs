using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models.Entity
{
    [Table("tblRolePermission")]
    public class clsRolePermission
    {
        public string RoleId { get; set; } = default!;
        public IdentityRole Role { get; set; } = default!;

        public int PermissionId { get; set; }
        public clsPermission Permission { get; set; } = default!;
    }
}
