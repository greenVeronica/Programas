using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class ProgramaGrupo
    {
        public ProgramaGrupo()
        {
            EtapaActor = new HashSet<EtapaActor>();
            ProgramaGrupos = new HashSet<ProgramaGrupos>();
        }

        public int GrupoPrograma { get; set; }
        public int? Circuito { get; set; }
        public string Descripcion { get; set; }
        public int? FormularioTipo { get; set; }
        public int? DatoEmpleadorId { get; set; }

        public ProgramaCircuito CircuitoNavigation { get; set; }
        public ICollection<EtapaActor> EtapaActor { get; set; }
        public ICollection<ProgramaGrupos> ProgramaGrupos { get; set; }
    }
}
