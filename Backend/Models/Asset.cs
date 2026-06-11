using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Ez kell a validációhoz

namespace Backend.Models;

public enum AssetType
{
    InStock,
    Assigned,
    InRepair
}

public class Asset : IValidatableObject
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Az eszköz nevének megadása kötelező!")]
    [StringLength(50)]
    public string AssetName { get; set; } = string.Empty;

    [Required(ErrorMessage = "A szériaszám megadása kötelező!")]
    [MinLength(5, ErrorMessage = "A szériaszám minimum 5 karakter!")]
    [MaxLength(50)]
    public string SerialNumber { get; set; } = string.Empty;

    public AssetType Type { get; set; } = AssetType.InStock;

    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if ((Type == AssetType.InStock || Type == AssetType.InRepair) && EmployeeId != null)
        {
            yield return new ValidationResult(
                "Asset in stock or in repair cannot be assigned to an employee!",
                new[] { nameof(EmployeeId), nameof(Type) });
        }

        if (Type == AssetType.Assigned && EmployeeId == null)
        {
            yield return new ValidationResult(
                "Assigned asset must be assigned to an employee!",
                new[] { nameof(EmployeeId), nameof(Type) });
        }
    }
}