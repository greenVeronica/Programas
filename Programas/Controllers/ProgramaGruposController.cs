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
    public class ProgramaGruposController : Controller
    {
        private readonly empleoproyectosContext _context;

        public ProgramaGruposController(empleoproyectosContext context)
        {
            _context = context;
        }

        // GET: ProgramaGrupos
        public async Task<IActionResult> Index()
        {
            var empleoproyectosContext = _context.ProgramaGrupos.OrderBy(programagrupo => programagrupo.Programa);//.Include(p => p.GrupoProgramaNavigation);
            return View(await empleoproyectosContext.ToListAsync());
        }

        // GET: ProgramaGrupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaGrupos = await _context.ProgramaGrupos
                .Include(p => p.GrupoProgramaNavigation)
                .FirstOrDefaultAsync(m => m.GrupoPrograma == id);
            if (programaGrupos == null)
            {
                return NotFound();
            }

            return View(programaGrupos);
        }

        // GET: ProgramaGrupos/Create
        public IActionResult Create()
        {
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma");
            return View();
        }

        // POST: ProgramaGrupos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrupoPrograma,Programa")] ProgramaGrupos programaGrupos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programaGrupos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma", programaGrupos.GrupoPrograma);
            return View(programaGrupos);
        }

        // GET: ProgramaGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaGrupos = await _context.ProgramaGrupos.FindAsync(id);
            if (programaGrupos == null)
            {
                return NotFound();
            }
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma", programaGrupos.GrupoPrograma);
            return View(programaGrupos);
        }

        // POST: ProgramaGrupos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrupoPrograma,Programa")] ProgramaGrupos programaGrupos)
        {
            if (id != programaGrupos.GrupoPrograma)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programaGrupos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramaGruposExists(programaGrupos.GrupoPrograma))
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
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma", programaGrupos.GrupoPrograma);
            return View(programaGrupos);
        }

        // GET: ProgramaGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programaGrupos = await _context.ProgramaGrupos
                .Include(p => p.GrupoProgramaNavigation)
                .FirstOrDefaultAsync(m => m.GrupoPrograma == id);
            if (programaGrupos == null)
            {
                return NotFound();
            }

            return View(programaGrupos);
        }

        // POST: ProgramaGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programaGrupos = await _context.ProgramaGrupos.FindAsync(id);
            _context.ProgramaGrupos.Remove(programaGrupos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramaGruposExists(int id)
        {
            return _context.ProgramaGrupos.Any(e => e.GrupoPrograma == id);
        }
    }
}
