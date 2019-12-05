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
    public class CandidatosController : Controller
    {
        private readonly DalContext _context;

        public CandidatosController(DalContext context)
        {
            _context = context;
        }

        // GET: Candidatos
        public async Task<IActionResult> Index()
        {
            var dalContext = _context.Candidato.Include(c => c.Partido).Include(c => c.TipoVoto);
            return View(await dalContext.ToListAsync());
        }

        // GET: Candidatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidato
                .Include(c => c.Partido)
                .Include(c => c.TipoVoto)
                .FirstOrDefaultAsync(m => m.CandidatoID == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // GET: Candidatos/Create
        public IActionResult Create()
        {
            ViewData["PartidoID"] = new SelectList(_context.Set<Partido>(), "PartidoID", "Nombre");
            ViewData["TipoVotoID"] = new SelectList(_context.Set<TipoVoto>(), "TipoVotoID", "Nombre");
            return View();
        }

        // POST: Candidatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidatoID,Nombres,Apellidos,TipoVotoID,PartidoID")] Candidato candidato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartidoID"] = new SelectList(_context.Set<Partido>(), "PartidoID", "Nombre", candidato.PartidoID);
            ViewData["TipoVotoID"] = new SelectList(_context.Set<TipoVoto>(), "TipoVotoID", "Nombre", candidato.TipoVotoID);
            return View(candidato);
        }

        // GET: Candidatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidato.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }
            ViewData["PartidoID"] = new SelectList(_context.Set<Partido>(), "PartidoID", "Nombre", candidato.PartidoID);
            ViewData["TipoVotoID"] = new SelectList(_context.Set<TipoVoto>(), "TipoVotoID", "Nombre", candidato.TipoVotoID);
            return View(candidato);
        }

        // POST: Candidatos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CandidatoID,Nombres,Apellidos,TipoVotoID,PartidoID")] Candidato candidato)
        {
            if (id != candidato.CandidatoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatoExists(candidato.CandidatoID))
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
            ViewData["PartidoID"] = new SelectList(_context.Set<Partido>(), "PartidoID", "Nombre", candidato.PartidoID);
            ViewData["TipoVotoID"] = new SelectList(_context.Set<TipoVoto>(), "TipoVotoID", "Nombre", candidato.TipoVotoID);
            return View(candidato);
        }

        // GET: Candidatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidato
                .Include(c => c.Partido)
                .Include(c => c.TipoVoto)
                .FirstOrDefaultAsync(m => m.CandidatoID == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidato = await _context.Candidato.FindAsync(id);
            _context.Candidato.Remove(candidato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatoExists(int id)
        {
            return _context.Candidato.Any(e => e.CandidatoID == id);
        }
    }
}
