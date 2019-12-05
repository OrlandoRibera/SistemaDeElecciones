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
    public class VotosController : Controller
    {
        private readonly DalContext _context;

        public VotosController(DalContext context)
        {
            _context = context;
        }

        // GET: Votos
        public async Task<IActionResult> Index()
        {
            var dalContext = _context.Voto.Include(v => v.Candidato).Include(v => v.Usuario);
            return View(await dalContext.ToListAsync());
        }

        // GET: Votos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voto = await _context.Voto
                .Include(v => v.Candidato)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.VotoID == id);
            if (voto == null)
            {
                return NotFound();
            }

            return View(voto);
        }

        // GET: Votos/Create
        public IActionResult Create()
        {
            ViewData["CandidatoID"] = new SelectList(_context.Candidato, "CandidatoID", "Apellidos");
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Apellidos");
            return View();
        }

        // POST: Votos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VotoID,CandidatoID,UsuarioID")] Voto voto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandidatoID"] = new SelectList(_context.Candidato, "CandidatoID", "Apellidos", voto.CandidatoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Apellidos", voto.UsuarioID);
            return View(voto);
        }

        // GET: Votos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voto = await _context.Voto.FindAsync(id);
            if (voto == null)
            {
                return NotFound();
            }
            ViewData["CandidatoID"] = new SelectList(_context.Candidato, "CandidatoID", "Apellidos", voto.CandidatoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Apellidos", voto.UsuarioID);
            return View(voto);
        }

        // POST: Votos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VotoID,CandidatoID,UsuarioID")] Voto voto)
        {
            if (id != voto.VotoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotoExists(voto.VotoID))
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
            ViewData["CandidatoID"] = new SelectList(_context.Candidato, "CandidatoID", "Apellidos", voto.CandidatoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Apellidos", voto.UsuarioID);
            return View(voto);
        }

        // GET: Votos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voto = await _context.Voto
                .Include(v => v.Candidato)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.VotoID == id);
            if (voto == null)
            {
                return NotFound();
            }

            return View(voto);
        }

        // POST: Votos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voto = await _context.Voto.FindAsync(id);
            _context.Voto.Remove(voto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotoExists(int id)
        {
            return _context.Voto.Any(e => e.VotoID == id);
        }
    }
}
