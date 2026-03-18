using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineshopManagerStarterKit.Data;
using WineshopManagerStarterKit.Models;

namespace WineshopManagerStarterKit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypeVinController : ControllerBase
{
    private readonly AppDbContext _context;

    public TypeVinController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/typevin
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypeVin>>> GetAll()
    {
        return await _context.TypeVins.ToListAsync();
    }

    // GET: api/typevin/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TypeVin>> GetById(int id)

    {
        var typevin = await _context.TypeVins.FindAsync(id);

        if (typevin == null)
        {
            return NotFound();
        }

        return typevin;
    }

    // POST: api/typevin
    [HttpPost]
    public async Task<ActionResult<TypeVin>> Create(TypeVin typeVin)
    {
        _context.TypeVins.Add(typeVin);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = typeVin.Id }, typeVin);
    }

    // PUT: api/typevin/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TypeVin typeVin)
    {
        if (id != typeVin.Id)
        {
            return BadRequest();
        }

        _context.Entry(typeVin).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.TypeVins.AnyAsync(w => w.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/typevin/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var typeVin = await _context.TypeVins.FindAsync(id);

        if (typeVin == null)
        {
            return NotFound();
        }

        _context.TypeVins.Remove(typeVin);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
