using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LabSesion16CrudReactWebApi.Models;

namespace LabSesion16CrudReactWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadsController : ControllerBase
    {
        private readonly BDCLINICAContext _context;

        public EspecialidadsController(BDCLINICAContext context)
        {
            _context = context;
        }

        // GET: api/Especialidads
        [HttpGet("Listado")]
        public async Task<ActionResult<IEnumerable<Especialidad>>> GetEspecialidads()
        {
            return await _context.Especialidads.ToListAsync();
        }

        // GET: api/Especialidads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidad>> GetEspecialidad(string id)
        {
            var especialidad = await _context.Especialidads.FindAsync(id);

            if (especialidad == null)
            {
                return NotFound();
            }

            return especialidad;
        }

        // PUT: api/Especialidads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidad(string id, Especialidad especialidad)
        {
            if (id != especialidad.Codesp)
            {
                return BadRequest();
            }

            _context.Entry(especialidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadExists(id))
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

        // POST: api/Especialidads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Especialidad>> PostEspecialidad(Especialidad especialidad)
        {
            _context.Especialidads.Add(especialidad);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EspecialidadExists(especialidad.Codesp))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEspecialidad", new { id = especialidad.Codesp }, especialidad);
        }

        // DELETE: api/Especialidads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(string id)
        {
            var especialidad = await _context.Especialidads.FindAsync(id);
            if (especialidad == null)
            {
                return NotFound();
            }

            _context.Especialidads.Remove(especialidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecialidadExists(string id)
        {
            return _context.Especialidads.Any(e => e.Codesp == id);
        }
    }
}
