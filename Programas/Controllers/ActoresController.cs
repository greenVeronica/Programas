using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Programas.Models;

namespace Programas.Controllers
{
    public class ActoresController : Controller
    {
        private readonly empleoproyectosContext _context;

        public ActoresController(empleoproyectosContext context)
        {
            _context = context;
        }

        // GET: Actores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Actores.OrderBy(registro => registro.Actor).ToListAsync());
        }

        // GET: Actores/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actores = await _context.Actores
                .FirstOrDefaultAsync(m => m.Actor == id);
            if (actores == null)
            {
                return NotFound();
            }

            return View(actores);
        }

        // GET: Actores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Actor,Lugar,Descripcion,Nota")] Actores actores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actores);
        }

        // GET: Actores/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actores = await _context.Actores.FindAsync(id);
            if (actores == null)
            {
                return NotFound();
            }
            return View(actores);
        }

        // POST: Actores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Actor,Lugar,Descripcion,Nota")] Actores actores)
        {
            if (id != actores.Actor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActoresExists(actores.Actor))
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
            return View(actores);
        }

        // GET: Actores/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actores = await _context.Actores
                .FirstOrDefaultAsync(m => m.Actor == id);
            if (actores == null)
            {
                return NotFound();
            }

            return View(actores);
        }

        // POST: Actores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var actores = await _context.Actores.FindAsync(id);
            _context.Actores.Remove(actores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActoresExists(string id)
        {
            return _context.Actores.Any(e => e.Actor == id);
        }
    }
}
