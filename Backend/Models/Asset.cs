namespace Backend.Models;

public enum AssetType
{
    InStock,
    Assigned,
    InRepair
}

public class Asset
{
    public int Id { get; set; }
    public string AssetName { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public AssetType Type { get; set; } = AssetType.InStock;

    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}