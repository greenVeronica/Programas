using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class Etapas
    {
        public Etapas()
        {
            Resultados = new HashSet<Resultados>();
        }

        public int Etapa { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Resultados> Resultados { get; set; }
    }
}
