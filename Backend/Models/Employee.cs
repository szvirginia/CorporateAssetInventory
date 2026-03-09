namespace Backend.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;

    public List<Asset> Assets { get; set; } = new();
}