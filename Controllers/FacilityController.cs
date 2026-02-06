using inspecto_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inspecto_API.Dtos.Facility;

namespace inspecto_API.Controllers;

[ApiController]
[Route("api/facilities")]
public class FacilityController : ControllerBase
{
    private readonly AppDbContext _db;

    public FacilityController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/facilities
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var facilities = await _db.facilities.ToListAsync();
        return Ok(facilities);
    }

    // GET /api/facilities/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var facility = await _db.facilities
        .AsNoTracking()
        .FirstOrDefaultAsync(f => f.id == id);

        if (facility == null) return NotFound();
        return Ok(facility);
    }

    // POST /api/facilities
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFacilityDto dto)
    {
        var facility = new Facility
        {
            name = dto.name,
            location = dto.location,
            type = dto.type,
            description = dto.description
        };

        _db.facilities.Add(facility);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = facility.id }, facility);
    }

    // PATCH /api/facilities/{id}
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFacilityDto dto)
    {
        var facility = await _db.facilities.FindAsync(id);
        if (facility == null) return NotFound();

        if (dto.name != null) facility.name = dto.name;
        if (dto.location != null) facility.location = dto.location;
        if (dto.type != null) facility.type = dto.type;
        if (dto.status != null) facility.status = dto.status;

        facility.updated_at = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return Ok(facility);
    }

    // DELETE /api/facilities/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var facility = await _db.facilities.FindAsync(id);
        if (facility == null) return NotFound();

        try
        {
            _db.facilities.Remove(facility);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Row already removed -> correct endresult
            return NoContent();
        }
    }
}
