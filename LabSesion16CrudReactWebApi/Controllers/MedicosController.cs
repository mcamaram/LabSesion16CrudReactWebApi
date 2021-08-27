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
    public class MedicosController : ControllerBase
    {
        private readonly BDCLINICAContext _context;

        public MedicosController(BDCLINICAContext context)
        {
            _context = context;
        }

        // GET: api/Medicos
        [HttpGet("Listado")]
        public async Task<ActionResult<IEnumerable<ListadoMedicos>>> GetMedicos()
        {
            var query = from m in _context.Medicos
                        join e in _context.Especialidads on m.Codesp equals e.Codesp
                        join d in _context.Distritos on m.Coddis equals d.Coddis
                        select new ListadoMedicos
                        {
                            codmed = m.Codmed,
                            codesp = e.Codesp,
                            especialidad = e.Nomesp,
                            nommed = m.Nommed,
                            anioColegiatura = m.AnioColegio,
                            coddis = d.Coddis,
                            distrito = d.Nomdis,
                            estado = m.Estado
                        };
            return await query.ToListAsync();
        }

        // GET: api/Medicos/5
        [HttpGet("Busqueda/{id}")]
        public async Task<ActionResult<Medico>> GetMedico(string id)
        {
            var medico = await _context.Medicos.FindAsync(id);

            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        // PUT: api/Medicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> PutMedico(string id, Medico medico)
        {
            if (id != medico.Codmed)
            {
                return BadRequest();
            }

            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(id))
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

        // POST: api/Medicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Registrar")]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
            _context.Medicos.Add(medico);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MedicoExists(medico.Codmed))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMedico", new { id = medico.Codmed }, medico);
        }

        // DELETE: api/Medicos/5
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> DeleteMedico(string id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicoExists(string id)
        {
            return _context.Medicos.Any(e => e.Codmed == id);
        }
    }
}
