using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class Actores
    {
        public Actores()
        {
            EtapaActor = new HashSet<EtapaActor>();
        }

        public string Actor { get; set; }
        public int? Lugar { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }

        public ICollection<EtapaActor> EtapaActor { get; set; }
    }
}
