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
    public class ProgramaCircuitoController : Controller
    {
        private readonly empleoproyectosContext _context;

        public ProgramaCircuitoController(empleoproyectosContext context)
        {
            _context = context;
        }

        // GET: ProgramaCircuitoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProgramaCircuito.OrderBy(registro =>registro.Circuito ).ToListAsync());
        }

        // GET: ProgramaCircuitoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaCircuito = await _context.ProgramaCircuito
                .FirstOrDefaultAsync(m => m.Circuito == id);
            if (programaCircuito == null)
            {
                return NotFound();
            }

            return View(programaCircuito);
        }

        // GET: ProgramaCircuitoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgramaCircuitoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Circuito,Descripcion,PlanJefesHogar,Pne")] ProgramaCircuito programaCircuito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programaCircuito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(programaCircuito);
        }

        // GET: ProgramaCircuitoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaCircuito = await _context.ProgramaCircuito.FindAsync(id);
            if (programaCircuito == null)
            {
                return NotFound();
            }
            return View(programaCircuito);
        }

        // POST: ProgramaCircuitoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Circuito,Descripcion,PlanJefesHogar,Pne")] ProgramaCircuito programaCircuito)
        {
            if (id != programaCircuito.Circuito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programaCircuito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramaCircuitoExists(programaCircuito.Circuito))
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
            return View(programaCircuito);
        }

        // GET: ProgramaCircuitoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaCircuito = await _context.ProgramaCircuito
                .FirstOrDefaultAsync(m => m.Circuito == id);
            if (programaCircuito == null)
            {
                return NotFound();
            }

            return View(programaCircuito);
        }

        // POST: ProgramaCircuitoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programaCircuito = await _context.ProgramaCircuito.FindAsync(id);
            _context.ProgramaCircuito.Remove(programaCircuito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramaCircuitoExists(int id)
        {
            return _context.ProgramaCircuito.Any(e => e.Circuito == id);
        }
    }
}
