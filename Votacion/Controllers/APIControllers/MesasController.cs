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
    public class MesasController : ControllerBase
    {
        private readonly DalContext _context;

        public MesasController(DalContext context)
        {
            _context = context;
        }

        // GET: api/Mesas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mesa>>> GetMesa()
        {
            return await _context.Mesa.ToListAsync();
        }

        // GET: api/Mesas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mesa>> GetMesa(int id)
        {
            var mesa = await _context.Mesa.FindAsync(id);

            if (mesa == null)
            {
                return NotFound();
            }

            return mesa;
        }

        // GET: api/Mesas/Verificar?qr=&mesaId=
        [HttpGet("Verificar")]
        public async Task<ActionResult<bool>> VerificarQRCode(string qr, int mesaId)
        {
            var mesa = await _context.Mesa.FindAsync(mesaId);

            if(mesa == null)
            {
                return NotFound();
            }
            return (mesa.CodigoQR.Equals(qr)) ? Ok(new { respuesta = "true" }) : Ok(new { respuesta = "false" });
        }

        // PUT: api/Mesas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMesa(int id, Mesa mesa)
        {
            if (id != mesa.MesaID)
            {
                return BadRequest();
            }

            _context.Entry(mesa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesaExists(id))
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

        // POST: api/Mesas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Mesa>> PostMesa(Mesa mesa)
        {
            _context.Mesa.Add(mesa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMesa", new { id = mesa.MesaID }, mesa);
        }

        // POST: api/Mesas/Cerrar?id=
        [HttpPost("Cerrar")]
        public async Task<ActionResult<Mesa>> CerrarMesa(int id) 
        {
            var mesa = await _context.Mesa.FindAsync(id);
            if(mesa == null)
            {
                return NotFound();
            }

            mesa.estadoMesa = true;
            _context.Mesa.Update(mesa);
            await _context.SaveChangesAsync();
            return Ok(mesa);
        }


        // DELETE: api/Mesas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Mesa>> DeleteMesa(int id)
        {
            var mesa = await _context.Mesa.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }

            _context.Mesa.Remove(mesa);
            await _context.SaveChangesAsync();

            return mesa;
        }


        private bool MesaExists(int id)
        {
            return _context.Mesa.Any(e => e.MesaID == id);
        }
    }
}
