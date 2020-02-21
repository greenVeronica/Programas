using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Programas.Models;
using Programas.DataAccess;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
//using System.Data.SqlClient;
//using System.Data;

namespace Programas.Controllers
{
    public class ProgramasPrestacionsController : Controller
    {
        private readonly empleoproyectosContext _context;
      //  private readonly Conexion _conexion;

        public ProgramasPrestacionsController(empleoproyectosContext context)//, Conexion conexion)
        {
            _context = context;
           // _conexion = conexion;
        }
        public JsonResult Burbujas(string programa)

        // public JsonResult Flechas()

        {
            var program = Int32.Parse(programa);
            var progGrup = new List<ProgramaGrupos>();
             progGrup = _context.ProgramaGrupos.Where(grupo => grupo.Programa == program).ToList();
     




            // inicio conexion
            using (SqlConnection conn = new SqlConnection("Server=S1-dIXX-SQL07;Initial Catalog=empleoproyectos;Integrated Security=SSPI"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("dbo.QryEstados2", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Grupo", progGrup [0].GrupoPrograma));

                // execute the command
                using (var rdr = cmd.ExecuteReader())
                {
                    List<cambioestado> Lista = new List<cambioestado>();


                    IList<cambioestado> result = new List<cambioestado>();
                    //3. Loop through rows
                    while (rdr.Read())
                    {

                        //                        id label   group

                        //Get each column
                        //  result.Add(new cambioestado() { id = (string)rdr.GetString(0), Id = rdr.GetString(1) });
                        result.Add(new cambioestado() {
                                    id = rdr.GetInt32(0),
                                    label = rdr.GetString(1),
                                    group = rdr.GetInt32(2) });
                    }

                    return Json(result, new Newtonsoft.Json.JsonSerializerSettings());

                }
                conn.Close();
                //fin conexion
            


               

            }
        }
        public JsonResult Flechas(string programa)
        {
            var program = Int32.Parse(programa);
            var progGrup = new List<ProgramaGrupos>();
            progGrup = _context.ProgramaGrupos.Where(grupo => grupo.Programa == program).ToList();
                                          
            // inicio conexion
            using (SqlConnection conn = new SqlConnection("Server=S1-dIXX-SQL07;Initial Catalog=empleoproyectos;Integrated Security=SSPI"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("dbo.QryEstados3", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@Grupo", progGrup[0].GrupoPrograma));

                // execute the command
                using (var rdr = cmd.ExecuteReader())
                {
                    List<cambioestado2> Lista = new List<cambioestado2>();


                    IList<cambioestado2> result = new List<cambioestado2>();
                    //3. Loop through rows
                    while (rdr.Read())
                    {

                        //    id	from	to	arrows	label
                                  //Get each column
                 
                        result.Add(new cambioestado2() { from = rdr.GetInt32(1),
                                                         to = rdr.GetInt32(2),
                                                         arrows = rdr.GetString(3),  
                                                         label = rdr.GetString(4)
                                                        
                        });
                    }

                    return Json(result, new Newtonsoft.Json.JsonSerializerSettings());

                }
                conn.Close();
                //fin conexion





            }
        }

        // GET: ProgramasPrestacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProgramasPrestacion.OrderBy(registro =>registro.Programa).ToListAsync());
        }

        // GET: ProgramasPrestacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programasPrestacion = await _context.ProgramasPrestacion
                .FirstOrDefaultAsync(m => m.Programa == id);
            if (programasPrestacion == null)
            {
                return NotFound();
            }

            return View(programasPrestacion);
        }

        // GET: ProgramasPrestacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgramasPrestacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Programa,TipoPrograma,Sexo,EstadoPrograma,Anio,Descripcion,DuracionMax,DuracionMin,EdadMax,EdadMin,FechaInicio,FechaFin,PrioridadEntreProgramas,AutoGenerarProyectos,AutoAsignarBeneficiarios,AutoIncrementarCupo,IndicaContraparte,PrioridadDePago,AnsesPrograma,ValidacionRegla,TipoOrganismo,AsociacionTipo,AsociacionObligatoria,CupoMinimo,CupoMaximo,TipoContratoLaboral,TipoModificacionAsignacion,TipoPago,PadronTerminalidad,GrupoOrganismo,TipoAcuerdoUnico,ProtocoloUsaGecal,CalculoPeriodoInicio,FormaLiquidacion,CorrespondeCierre,TipoLocalizacion,CalificacionTipo,LineaDeAccion,TipoAplicacionReserva")] ProgramasPrestacion programasPrestacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programasPrestacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(programasPrestacion);
        }

        // GET: ProgramasPrestacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programasPrestacion = await _context.ProgramasPrestacion.FindAsync(id);
            if (programasPrestacion == null)
            {
                return NotFound();
            }
            return View(programasPrestacion);
        }

        // POST: ProgramasPrestacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Programa,TipoPrograma,Sexo,EstadoPrograma,Anio,Descripcion,DuracionMax,DuracionMin,EdadMax,EdadMin,FechaInicio,FechaFin,PrioridadEntreProgramas,AutoGenerarProyectos,AutoAsignarBeneficiarios,AutoIncrementarCupo,IndicaContraparte,PrioridadDePago,AnsesPrograma,ValidacionRegla,TipoOrganismo,AsociacionTipo,AsociacionObligatoria,CupoMinimo,CupoMaximo,TipoContratoLaboral,TipoModificacionAsignacion,TipoPago,PadronTerminalidad,GrupoOrganismo,TipoAcuerdoUnico,ProtocoloUsaGecal,CalculoPeriodoInicio,FormaLiquidacion,CorrespondeCierre,TipoLocalizacion,CalificacionTipo,LineaDeAccion,TipoAplicacionReserva")] ProgramasPrestacion programasPrestacion)
        {
            if (id != programasPrestacion.Programa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programasPrestacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramasPrestacionExists(programasPrestacion.Programa))
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
            return View(programasPrestacion);
        }

        // GET: ProgramasPrestacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programasPrestacion = await _context.ProgramasPrestacion
                .FirstOrDefaultAsync(m => m.Programa == id);
            if (programasPrestacion == null)
            {
                return NotFound();
            }

            return View(programasPrestacion);
        }

        // POST: ProgramasPrestacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programasPrestacion = await _context.ProgramasPrestacion.FindAsync(id);
            _context.ProgramasPrestacion.Remove(programasPrestacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramasPrestacionExists(int id)
        {
            return _context.ProgramasPrestacion.Any(e => e.Programa == id);
        }


    }

    
}
