using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class ProgramaCircuito
    {
        public ProgramaCircuito()
        {
            ProgramaGrupo = new HashSet<ProgramaGrupo>();
        }

        public int Circuito { get; set; }
        public string Descripcion { get; set; }
        public int PlanJefesHogar { get; set; }
        public int Pne { get; set; }

        public ICollection<ProgramaGrupo> ProgramaGrupo { get; set; }
    }
}
