using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public enum AssetType
{
    InStock,
    Assigned,
    InRepair
}

public class Asset
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string AssetName { get; set; } = string.Empty;

    [MinLength(5)]
    [MaxLength(50)]
    public string SerialNumber { get; set; } = string.Empty;
    public AssetType Type { get; set; } = AssetType.InStock;

    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}