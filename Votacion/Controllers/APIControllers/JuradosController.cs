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
    public class JuradosController : ControllerBase
    {
        private readonly DalContext _context;

        public JuradosController(DalContext context)
        {
            _context = context;
        }

        // GET: api/Jurados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jurado>>> GetJurado()
        {
            return await _context.Jurado.ToListAsync();
        }

        // GET: api/Jurados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jurado>> GetJurado(int id)
        {
            var jurado = await _context.Jurado.FindAsync(id);

            if (jurado == null)
            {
                return NotFound();
            }

            return jurado;
        }

        // PUT: api/Jurados/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJurado(int id, Jurado jurado)
        {
            if (id != jurado.UsuarioID)
            {
                return BadRequest();
            }

            _context.Entry(jurado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuradoExists(id))
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

        // POST: api/Jurados
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Jurado>> PostJurado(Jurado jurado)
        {
            _context.Jurado.Add(jurado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJurado", new { id = jurado.UsuarioID }, jurado);
        }

        // DELETE: api/Jurados/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Jurado>> DeleteJurado(int id)
        {
            var jurado = await _context.Jurado.FindAsync(id);
            if (jurado == null)
            {
                return NotFound();
            }

            _context.Jurado.Remove(jurado);
            await _context.SaveChangesAsync();

            return jurado;
        }

        private bool JuradoExists(int id)
        {
            return _context.Jurado.Any(e => e.UsuarioID == id);
        }

        //api/jurados/login?usuario=&password=
        [HttpGet("login")]
        [Produces("application/json")]
        public async Task<ActionResult> Login(string usuario, string password)
        {
            var jurado = await _context.Jurado.FirstOrDefaultAsync(x => x.Usuario.Equals(usuario) && x.Password.Equals(password));
            jurado.Password = "";
            var mesa = await _context.Mesa.FirstOrDefaultAsync(x => x.MesaID == jurado.MesaID);
            jurado.Mesa = mesa;
            if(jurado == null)
            {
                return NotFound(new { respuesta = "false" });
            }
            return Ok(new { jurado });
        }
    }
}
