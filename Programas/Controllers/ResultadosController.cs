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
    public class ResultadosController : Controller
    {
        private readonly empleoproyectosContext _context;

        public ResultadosController(empleoproyectosContext context)
        {
            _context = context;
        }

        // GET: Resultados
        public async Task<IActionResult> Index()
        {
            var empleoproyectosContext = _context.Resultados.OrderBy(registro => registro.Etapa);//.Include(r => r.EtapaNavigation);
            return View(await empleoproyectosContext.ToListAsync());
        }

        // GET: Resultados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultados = await _context.Resultados
                .Include(r => r.EtapaNavigation)
                .FirstOrDefaultAsync(m => m.Etapa == id);
            if (resultados == null)
            {
                return NotFound();
            }

            return View(resultados);
        }

        // GET: Resultados/Create
        public IActionResult Create()
        {
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Etapa", "Etapa");
            return View();
        }

        // POST: Resultados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Etapa,Resultado,Descripcion")] Resultados resultados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Etapa", "Etapa", resultados.Etapa);
            return View(resultados);
        }

        // GET: Resultados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultados = await _context.Resultados.FindAsync(id);
            if (resultados == null)
            {
                return NotFound();
            }
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Etapa", "Etapa", resultados.Etapa);
            return View(resultados);
        }

        // POST: Resultados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Etapa,Resultado,Descripcion")] Resultados resultados)
        {
            if (id != resultados.Etapa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultadosExists(resultados.Etapa))
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
            ViewData["Etapa"] = new SelectList(_context.Etapas, "Etapa", "Etapa", resultados.Etapa);
            return View(resultados);
        }

        // GET: Resultados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultados = await _context.Resultados
                .Include(r => r.EtapaNavigation)
                .FirstOrDefaultAsync(m => m.Etapa == id);
            if (resultados == null)
            {
                return NotFound();
            }

            return View(resultados);
        }

        // POST: Resultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultados = await _context.Resultados.FindAsync(id);
            _context.Resultados.Remove(resultados);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultadosExists(int id)
        {
            return _context.Resultados.Any(e => e.Etapa == id);
        }
    }
}
