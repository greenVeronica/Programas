using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class ProgramasPrestacion
    {
        public int Programa { get; set; }
        public string TipoPrograma { get; set; }
        public string Sexo { get; set; }
        public string EstadoPrograma { get; set; }
        public int? Anio { get; set; }
        public string Descripcion { get; set; }
        public int? DuracionMax { get; set; }
        public int? DuracionMin { get; set; }
        public int? EdadMax { get; set; }
        public int? EdadMin { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? PrioridadEntreProgramas { get; set; }
        public int? AutoGenerarProyectos { get; set; }
        public int? AutoAsignarBeneficiarios { get; set; }
        public int? AutoIncrementarCupo { get; set; }
        public int? IndicaContraparte { get; set; }
        public int? PrioridadDePago { get; set; }
        public int? AnsesPrograma { get; set; }
        public int? ValidacionRegla { get; set; }
        public int? TipoOrganismo { get; set; }
        public int? AsociacionTipo { get; set; }
        public int? AsociacionObligatoria { get; set; }
        public int? CupoMinimo { get; set; }
        public int? CupoMaximo { get; set; }
        public int? TipoContratoLaboral { get; set; }
        public int? TipoModificacionAsignacion { get; set; }
        public int TipoPago { get; set; }
        public int? PadronTerminalidad { get; set; }
        public int? GrupoOrganismo { get; set; }
        public int? TipoAcuerdoUnico { get; set; }
        public int? ProtocoloUsaGecal { get; set; }
        public int? CalculoPeriodoInicio { get; set; }
        public int? FormaLiquidacion { get; set; }
        public int? CorrespondeCierre { get; set; }
        public int? TipoLocalizacion { get; set; }
        public int? CalificacionTipo { get; set; }
        public int? LineaDeAccion { get; set; }
        public int? TipoAplicacionReserva { get; set; }
    }
}
