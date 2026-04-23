using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly AppDbContext _context;

    public VehicleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("top-speed")]
    public async Task<IActionResult> GetTopSpeedVehicles()
    {
        var result = await _context.VehicleReadings
            .Include(v => v.Vehicle)
            .OrderByDescending(v => v.Speed)
            .Take(5)
            .Select(v => new
            {
                VehicleName = v.Vehicle.Name,
                v.Speed,
                v.Timestamp
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("engine-temperature")]
    public async Task<IActionResult> GetEngineTemperature()
    {
        var result = await _context.VehicleReadings
            .Include(v => v.Vehicle)
            .GroupBy(v => v.VehicleId)
            .Select(g => g.OrderByDescending(x => x.Timestamp).First())
            .Select(v => new
            {
                VehicleName = v.Vehicle.Name,
                v.EngineTemperature
            })
            .ToListAsync();

        return Ok(result);
    }
}