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
    public class TipoVotosController : ControllerBase
    {
        private readonly DalContext _context;

        public TipoVotosController(DalContext context)
        {
            _context = context;
        }

        // GET: api/TipoVotos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoVoto>>> GetTipoVoto()
        {
            return await _context.TipoVoto.ToListAsync();
        }

        // GET: api/TipoVotos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoVoto>> GetTipoVoto(int id)
        {
            var tipoVoto = await _context.TipoVoto.FindAsync(id);

            if (tipoVoto == null)
            {
                return NotFound();
            }

            return tipoVoto;
        }

        // PUT: api/TipoVotos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoVoto(int id, TipoVoto tipoVoto)
        {
            if (id != tipoVoto.TipoVotoID)
            {
                return BadRequest();
            }

            _context.Entry(tipoVoto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoVotoExists(id))
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

        // POST: api/TipoVotos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TipoVoto>> PostTipoVoto(TipoVoto tipoVoto)
        {
            _context.TipoVoto.Add(tipoVoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoVoto", new { id = tipoVoto.TipoVotoID }, tipoVoto);
        }

        // DELETE: api/TipoVotos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoVoto>> DeleteTipoVoto(int id)
        {
            var tipoVoto = await _context.TipoVoto.FindAsync(id);
            if (tipoVoto == null)
            {
                return NotFound();
            }

            _context.TipoVoto.Remove(tipoVoto);
            await _context.SaveChangesAsync();

            return tipoVoto;
        }

        private bool TipoVotoExists(int id)
        {
            return _context.TipoVoto.Any(e => e.TipoVotoID == id);
        }
    }
}
