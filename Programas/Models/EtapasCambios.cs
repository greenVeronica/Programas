using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class EtapasCambios
    {
        public EtapasCambios()
        {
            EtapaActor = new HashSet<EtapaActor>();
        }

        public string CambioEstado { get; set; }
        public string Descripcion { get; set; }
        public int Etapa { get; set; }
        public int Resultado { get; set; }
        public int? Lugar { get; set; }
        public int ProxEtapa { get; set; }
        public int ProxResultado { get; set; }
        public int? GenHistoria { get; set; }
        public int? TipoMovimientoBenef { get; set; }
        public int? ValidacionGrupo { get; set; }
        public int? AplicaReserva { get; set; }
        public int? PublicarRrhh { get; set; }
        public int? AccionOferta { get; set; }
        public int? GeneraProyecto { get; set; }
        public int? TareaAprueba { get; set; }

        public ICollection<EtapaActor> EtapaActor { get; set; }
    }
}
