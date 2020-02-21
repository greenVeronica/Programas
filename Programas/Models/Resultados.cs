using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class Resultados
    {
        public int Etapa { get; set; }
        public int Resultado { get; set; }
        public string Descripcion { get; set; }

        public Etapas EtapaNavigation { get; set; }
    }
}
