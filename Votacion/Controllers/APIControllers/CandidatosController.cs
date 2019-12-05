using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaElecciones.Models;
using Votacion.Context;

namespace Votacion.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private readonly DalContext _context;

        public CandidatosController(DalContext context)
        {
            _context = context;
        }

        // GET: api/Candidatos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidato>>> GetCandidato()
        {
            return await _context.Candidato.Include(x=> x.TipoVoto).Include(x => x.Partido).ToListAsync();
        }

        // GET: api/Candidatos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidato>> GetCandidato(int id)
        {
            var candidato = await _context.Candidato.FindAsync(id);

            if (candidato == null)
            {
                return NotFound();
            }

            return candidato;
        }

        // PUT: api/Candidatos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidato(int id, Candidato candidato)
        {
            if (id != candidato.CandidatoID)
            {
                return BadRequest();
            }

            _context.Entry(candidato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatoExists(id))
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

        // POST: api/Candidatos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Candidato>> PostCandidato(Candidato candidato)
        {
            _context.Candidato.Add(candidato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidato", new { id = candidato.CandidatoID }, candidato);
        }

        // DELETE: api/Candidatos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Candidato>> DeleteCandidato(int id)
        {
            var candidato = await _context.Candidato.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidato.Remove(candidato);
            await _context.SaveChangesAsync();

            return candidato;
        }

        private bool CandidatoExists(int id)
        {
            return _context.Candidato.Any(e => e.CandidatoID == id);
        }
    }
}
