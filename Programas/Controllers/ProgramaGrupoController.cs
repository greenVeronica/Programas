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
    public class ProgramaGrupoController : Controller
    {
        private readonly empleoproyectosContext _context;

        public ProgramaGrupoController(empleoproyectosContext context)
        {
            _context = context;
        }

        // GET: ProgramaGrupoes
        public async Task<IActionResult> Index()
        {
            var empleoproyectosContext = _context.ProgramaGrupo.OrderBy(registro => registro.GrupoPrograma);//.Include(p => p.CircuitoNavigation);
            return View(await empleoproyectosContext.ToListAsync());
        }

        // GET: ProgramaGrupoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaGrupo = await _context.ProgramaGrupo
                .Include(p => p.CircuitoNavigation)
                .FirstOrDefaultAsync(m => m.GrupoPrograma == id);
            if (programaGrupo == null)
            {
                return NotFound();
            }

            return View(programaGrupo);
        }

        // GET: ProgramaGrupoes/Create
        public IActionResult Create()
        {
            ViewData["Circuito"] = new SelectList(_context.ProgramaCircuito, "Circuito", "Descripcion");
            return View();
        }

        // POST: ProgramaGrupoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrupoPrograma,Circuito,Descripcion,FormularioTipo,DatoEmpleadorId")] ProgramaGrupo programaGrupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programaGrupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Circuito"] = new SelectList(_context.ProgramaCircuito, "Circuito", "Descripcion", programaGrupo.Circuito);
            return View(programaGrupo);
        }

        // GET: ProgramaGrupoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaGrupo = await _context.ProgramaGrupo.FindAsync(id);
            if (programaGrupo == null)
            {
                return NotFound();
            }
            ViewData["Circuito"] = new SelectList(_context.ProgramaCircuito, "Circuito", "Descripcion", programaGrupo.Circuito);
            return View(programaGrupo);
        }

        // POST: ProgramaGrupoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrupoPrograma,Circuito,Descripcion,FormularioTipo,DatoEmpleadorId")] ProgramaGrupo programaGrupo)
        {
            if (id != programaGrupo.GrupoPrograma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programaGrupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramaGrupoExists(programaGrupo.GrupoPrograma))
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
            ViewData["Circuito"] = new SelectList(_context.ProgramaCircuito, "Circuito", "Descripcion", programaGrupo.Circuito);
            return View(programaGrupo);
        }

        // GET: ProgramaGrupoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaGrupo = await _context.ProgramaGrupo
                .Include(p => p.CircuitoNavigation)
                .FirstOrDefaultAsync(m => m.GrupoPrograma == id);
            if (programaGrupo == null)
            {
                return NotFound();
            }

            return View(programaGrupo);
        }

        // POST: ProgramaGrupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programaGrupo = await _context.ProgramaGrupo.FindAsync(id);
            _context.ProgramaGrupo.Remove(programaGrupo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramaGrupoExists(int id)
        {
            return _context.ProgramaGrupo.Any(e => e.GrupoPrograma == id);
        }
    }
}
