using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaElecciones.Models;
using Votacion.Context;

namespace Votacion.Controllers
{
    public class TipoVotosController : Controller
    {
        private readonly DalContext _context;

        public TipoVotosController(DalContext context)
        {
            _context = context;
        }

        // GET: TipoVotos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoVoto.ToListAsync());
        }

        // GET: TipoVotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVoto = await _context.TipoVoto
                .FirstOrDefaultAsync(m => m.TipoVotoID == id);
            if (tipoVoto == null)
            {
                return NotFound();
            }

            return View(tipoVoto);
        }

        // GET: TipoVotos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoVotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoVotoID,Nombre")] TipoVoto tipoVoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoVoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoVoto);
        }

        // GET: TipoVotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVoto = await _context.TipoVoto.FindAsync(id);
            if (tipoVoto == null)
            {
                return NotFound();
            }
            return View(tipoVoto);
        }

        // POST: TipoVotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoVotoID,Nombre")] TipoVoto tipoVoto)
        {
            if (id != tipoVoto.TipoVotoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoVoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVotoExists(tipoVoto.TipoVotoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoVoto);
        }

        // GET: TipoVotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVoto = await _context.TipoVoto
                .FirstOrDefaultAsync(m => m.TipoVotoID == id);
            if (tipoVoto == null)
            {
                return NotFound();
            }

            return View(tipoVoto);
        }

        // POST: TipoVotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoVoto = await _context.TipoVoto.FindAsync(id);
            _context.TipoVoto.Remove(tipoVoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoVotoExists(int id)
        {
            return _context.TipoVoto.Any(e => e.TipoVotoID == id);
        }
    }
}
