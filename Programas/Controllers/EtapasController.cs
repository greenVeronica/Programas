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
    public class EtapasController : Controller
    {
        private readonly empleoproyectosContext _context;

        public EtapasController(empleoproyectosContext context)
        {
            _context = context;
        }
        
        // GET: Etapas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Etapas.OrderBy(registro => registro.Etapa).ToListAsync());
        }

       
        // GET: Etapas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapas = await _context.Etapas
                .FirstOrDefaultAsync(m => m.Etapa == id);
            if (etapas == null)
            {
                return NotFound();
            }

            return View(etapas);
        }

        // GET: Etapas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etapas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Etapa,Descripcion")] Etapas etapas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etapas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etapas);
        }

        // GET: Etapas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapas = await _context.Etapas.FindAsync(id);
            if (etapas == null)
            {
                return NotFound();
            }
            return View(etapas);
        }

        // POST: Etapas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Etapa,Descripcion")] Etapas etapas)
        {
            if (id != etapas.Etapa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etapas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapasExists(etapas.Etapa))
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
            return View(etapas);
        }

        // GET: Etapas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapas = await _context.Etapas
                .FirstOrDefaultAsync(m => m.Etapa == id);
            if (etapas == null)
            {
                return NotFound();
            }

            return View(etapas);
        }

        // POST: Etapas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etapas = await _context.Etapas.FindAsync(id);
            _context.Etapas.Remove(etapas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapasExists(int id)
        {
            return _context.Etapas.Any(e => e.Etapa == id);
        }
        
    }
}
