using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;

    public List<Asset> Assets { get; set; } = new();
}