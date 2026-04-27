using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly AssetDbContext _context;

    public AssetsController(AssetDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssets()
    {
        var assets = await _context.Assets.Include(a => a.Employee).ToListAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssetById([FromRoute] int id)
    {
        var asset = await _context.Assets.Include(a => a.Employee).FirstOrDefaultAsync(a => a.Id == id);
        if (asset == null) return NotFound();

        return Ok(asset);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] Asset newAsset)
    {
        _context.Assets.Add(newAsset);
        await _context.SaveChangesAsync();
        return Ok(newAsset);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsset([FromRoute] int id, [FromBody] Asset updatedAsset)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return NotFound();

        asset.AssetName = updatedAsset.AssetName;
        asset.SerialNumber = updatedAsset.SerialNumber;
        asset.Type = updatedAsset.Type;
        asset.EmployeeId = updatedAsset.EmployeeId;

        await _context.SaveChangesAsync();
        return Ok(asset);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset([FromRoute] int id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return NotFound();

        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Asset deleted." });
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAssets([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            var allAssets = await _context.Assets.Include(a => a.Employee).ToListAsync();
            return Ok(allAssets);
        }

        var filteredAssets = await _context.Assets
            .Include(a => a.Employee)
            .Where(a => a.AssetName.Contains(keyword) || a.SerialNumber.Contains(keyword))
            .ToListAsync();

        return Ok(filteredAssets);
    }
}