using System.ComponentModel.DataAnnotations;
using static RMS.Models.Common.EnumForEntity;

namespace RMS.ViewModels.Workflow;

public class WorkflowVm
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "نام گردش الزامی است")]
    [Display(Name = "نام گردش")]
    [MaxLength(200)]
    public string Name { get; set; }
    public ProccesType proccesType { get; set; }
    public bool IsDefault { get; set; }
    public bool IsSystem { get; set; }
    public bool IsActive { get; set; }
    public bool? Auto { get; set; }
}
