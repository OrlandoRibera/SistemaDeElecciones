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
    public class JuradosController : Controller
    {
        private readonly DalContext _context;

        public JuradosController(DalContext context)
        {
            _context = context;
        }

        // GET: Jurados
        public async Task<IActionResult> Index()
        {
            var dalContext = _context.Jurado.Include(j => j.Mesa);
            return View(await dalContext.ToListAsync());
        }

        // GET: Jurados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jurado = await _context.Jurado
                .Include(j => j.Mesa)
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (jurado == null)
            {
                return NotFound();
            }

            return View(jurado);
        }

        // GET: Jurados/Create
        public IActionResult Create()
        {
            ViewData["MesaID"] = new SelectList(_context.Set<Mesa>(), "MesaID", "CodigoQR");
            return View();
        }

        // POST: Jurados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioID,CI,Nombres,Apellidos,Usuario,Password,FechaNacimiento,MesaID")] Jurado jurado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jurado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesaID"] = new SelectList(_context.Set<Mesa>(), "MesaID", "CodigoQR", jurado.MesaID);
            return View(jurado);
        }

        // GET: Jurados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jurado = await _context.Jurado.FindAsync(id);
            if (jurado == null)
            {
                return NotFound();
            }
            ViewData["MesaID"] = new SelectList(_context.Set<Mesa>(), "MesaID", "CodigoQR", jurado.MesaID);
            return View(jurado);
        }

        // POST: Jurados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioID,CI,Nombres,Apellidos,Usuario,Password,FechaNacimiento,MesaID")] Jurado jurado)
        {
            if (id != jurado.UsuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jurado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JuradoExists(jurado.UsuarioID))
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
            ViewData["MesaID"] = new SelectList(_context.Set<Mesa>(), "MesaID", "CodigoQR", jurado.MesaID);
            return View(jurado);
        }

        // GET: Jurados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jurado = await _context.Jurado
                .Include(j => j.Mesa)
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (jurado == null)
            {
                return NotFound();
            }

            return View(jurado);
        }

        // POST: Jurados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jurado = await _context.Jurado.FindAsync(id);
            _context.Jurado.Remove(jurado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JuradoExists(int id)
        {
            return _context.Jurado.Any(e => e.UsuarioID == id);
        }
    }
}
