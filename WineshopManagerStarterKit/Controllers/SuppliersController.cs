using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineshopManagerStarterKit.Data;
using WineshopManagerStarterKit.Models;

namespace WineshopManagerStarterKit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly AppDbContext _context;

    public SuppliersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/wines
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
    {
        return await _context.Suppliers.ToListAsync();
    }

    // GET: api/wines/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Supplier>> GetById(int id)
    {
        var suppliers = await _context.Suppliers.FindAsync(id);

        if (suppliers == null)
        {
            return NotFound();
        }

        return suppliers;
    }

    // POST: api/wines
    [HttpPost]
    public async Task<ActionResult<Supplier>> Create(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
    }

    // PUT: api/wines/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Supplier supplier)
    {
        if (id != supplier.Id)
        {
            return BadRequest();
        }

        _context.Entry(supplier).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Suppliers.AnyAsync(s => s.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/wines/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var suppliers = await _context.Suppliers.FindAsync(id);

        if (suppliers == null)
        {
            return NotFound();
        }

        _context.Suppliers.Remove(suppliers);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}