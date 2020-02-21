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
    public class EtapaActorController : Controller
    {
        private readonly empleoproyectosContext _context;

        public EtapaActorController(empleoproyectosContext context)
        {
            _context = context;
        }

        // GET: EtapaActors
        public async Task<IActionResult> Index()
        {
            var empleoproyectosContext = _context.EtapaActor.OrderBy(registro => registro.GrupoPrograma);//.Include(e => e.ActorNavigation).Include(e => e.CambioEstadoNavigation).Include(e => e.GrupoProgramaNavigation);
            return View(await empleoproyectosContext.ToListAsync());
        }

        // GET: EtapaActors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapaActor = await _context.EtapaActor
                .Include(e => e.ActorNavigation)
                .Include(e => e.CambioEstadoNavigation)
                .Include(e => e.GrupoProgramaNavigation)
                .FirstOrDefaultAsync(m => m.Actor == id);
            if (etapaActor == null)
            {
                return NotFound();
            }

            return View(etapaActor);
        }

        // GET: EtapaActors/Create
        public IActionResult Create()
        {
            ViewData["Actor"] = new SelectList(_context.Actores, "Actor", "Actor");
            ViewData["CambioEstado"] = new SelectList(_context.EtapasCambios, "CambioEstado", "CambioEstado");
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma");
            return View();
        }

        // POST: EtapaActors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Actor,CambioEstado,GrupoPrograma,Nota")] EtapaActor etapaActor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etapaActor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Actor"] = new SelectList(_context.Actores, "Actor", "Actor", etapaActor.Actor);
            ViewData["CambioEstado"] = new SelectList(_context.EtapasCambios, "CambioEstado", "CambioEstado", etapaActor.CambioEstado);
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma", etapaActor.GrupoPrograma);
            return View(etapaActor);
        }

        // GET: EtapaActors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapaActor = await _context.EtapaActor.FindAsync(id);
            if (etapaActor == null)
            {
                return NotFound();
            }
            ViewData["Actor"] = new SelectList(_context.Actores, "Actor", "Actor", etapaActor.Actor);
            ViewData["CambioEstado"] = new SelectList(_context.EtapasCambios, "CambioEstado", "CambioEstado", etapaActor.CambioEstado);
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma", etapaActor.GrupoPrograma);
            return View(etapaActor);
        }

        // POST: EtapaActors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Actor,CambioEstado,GrupoPrograma,Nota")] EtapaActor etapaActor)
        {
            if (id != etapaActor.Actor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etapaActor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapaActorExists(etapaActor.Actor))
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
            ViewData["Actor"] = new SelectList(_context.Actores, "Actor", "Actor", etapaActor.Actor);
            ViewData["CambioEstado"] = new SelectList(_context.EtapasCambios, "CambioEstado", "CambioEstado", etapaActor.CambioEstado);
            ViewData["GrupoPrograma"] = new SelectList(_context.ProgramaGrupo, "GrupoPrograma", "GrupoPrograma", etapaActor.GrupoPrograma);
            return View(etapaActor);
        }

        // GET: EtapaActors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapaActor = await _context.EtapaActor
                .Include(e => e.ActorNavigation)
                .Include(e => e.CambioEstadoNavigation)
                .Include(e => e.GrupoProgramaNavigation)
                .FirstOrDefaultAsync(m => m.Actor == id);
            if (etapaActor == null)
            {
                return NotFound();
            }

            return View(etapaActor);
        }

        // POST: EtapaActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var etapaActor = await _context.EtapaActor.FindAsync(id);
            _context.EtapaActor.Remove(etapaActor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapaActorExists(string id)
        {
            return _context.EtapaActor.Any(e => e.Actor == id);
        }
    }
}
