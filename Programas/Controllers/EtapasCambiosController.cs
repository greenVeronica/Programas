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
    public class EtapasCambiosController : Controller
    {
        private readonly empleoproyectosContext _context;

        public EtapasCambiosController(empleoproyectosContext context)
        {
            _context = context;
        }

        // GET: EtapasCambios
        public async Task<IActionResult> Index()
        {
            return View(await _context.EtapasCambios.OrderBy(registro => registro.CambioEstado).ToListAsync());
        }

        // GET: EtapasCambios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapasCambios = await _context.EtapasCambios
                .FirstOrDefaultAsync(m => m.CambioEstado == id);
            if (etapasCambios == null)
            {
                return NotFound();
            }

            return View(etapasCambios);
        }

        // GET: EtapasCambios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EtapasCambios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CambioEstado,Descripcion,Etapa,Resultado,Lugar,ProxEtapa,ProxResultado,GenHistoria,TipoMovimientoBenef,ValidacionGrupo,AplicaReserva,PublicarRrhh,AccionOferta,GeneraProyecto,TareaAprueba")] EtapasCambios etapasCambios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etapasCambios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etapasCambios);
        }

        // GET: EtapasCambios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapasCambios = await _context.EtapasCambios.FindAsync(id);
            if (etapasCambios == null)
            {
                return NotFound();
            }
            return View(etapasCambios);
        }

        // POST: EtapasCambios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CambioEstado,Descripcion,Etapa,Resultado,Lugar,ProxEtapa,ProxResultado,GenHistoria,TipoMovimientoBenef,ValidacionGrupo,AplicaReserva,PublicarRrhh,AccionOferta,GeneraProyecto,TareaAprueba")] EtapasCambios etapasCambios)
        {
            if (id != etapasCambios.CambioEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etapasCambios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapasCambiosExists(etapasCambios.CambioEstado))
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
            return View(etapasCambios);
        }

        // GET: EtapasCambios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapasCambios = await _context.EtapasCambios
                .FirstOrDefaultAsync(m => m.CambioEstado == id);
            if (etapasCambios == null)
            {
                return NotFound();
            }

            return View(etapasCambios);
        }

        // POST: EtapasCambios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var etapasCambios = await _context.EtapasCambios.FindAsync(id);
            _context.EtapasCambios.Remove(etapasCambios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapasCambiosExists(string id)
        {
            return _context.EtapasCambios.Any(e => e.CambioEstado == id);
        }
    }
}
