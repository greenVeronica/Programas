using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class EtapaActor
    {
        public string Actor { get; set; }
        public string CambioEstado { get; set; }
        public int GrupoPrograma { get; set; }
        public string Nota { get; set; }

        public Actores ActorNavigation { get; set; }
        public EtapasCambios CambioEstadoNavigation { get; set; }
        public ProgramaGrupo GrupoProgramaNavigation { get; set; }
    }
}
