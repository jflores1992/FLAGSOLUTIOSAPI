using FLAGSOLUTIOSAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FLAGSOLUTIOSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {

        private readonly MANTENIMIENTODBContext _context;

        public CatalogosController(MANTENIMIENTODBContext context)
        {
            _context = context;
        }


        // GET: api/<CatalogosController>
        [HttpGet("Periodos")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Periodo>>> GetPeriodos()
        {
            return await _context.Periodos.ToListAsync();
        }


        // GET: api/catalogos/getPeriodo/{id}
        [HttpGet("Periodos/{id}")]
        [Authorize]
        public async Task<ActionResult<Periodo>> GetPeriodo(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);

            if (periodo == null)
            {
                return NotFound();
            }

            return periodo;
        }

        // POST: api/catalogos/postPeriodo
        [HttpPost("Periodos")]
        [Authorize]
        public async Task<ActionResult<Periodo>> PostPeriodo([FromBody] Periodo periodo)
        {
            _context.Periodos.Add(periodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeriodo), new { id = periodo.Id }, periodo);
        }

        // PUT: api/catalogos/putPeriodo/{id}
        [HttpPut("Periodos/{id}")]
        [Authorize]
        public async Task<IActionResult> PutPeriodo(int id, [FromBody] Periodo periodo)
        {
            if (id != periodo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo del objeto.");
            }

            _context.Entry(periodo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PeriodoExists(int id)
        {
            return _context.Periodos.Any(e => e.Id == id);
        }

        // DELETE: api/catalogos/deletePeriodo/{id}
        [HttpDelete("Periodo/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePeriodo(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
            {
                return NotFound();
            }

            _context.Periodos.Remove(periodo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
