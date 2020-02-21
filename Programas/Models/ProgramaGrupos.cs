using System;
using System.Collections.Generic;

namespace Programas.Models
{
    public partial class ProgramaGrupos
    {
        public int GrupoPrograma { get; set; }
        public int Programa { get; set; }

        public ProgramaGrupo GrupoProgramaNavigation { get; set; }
    }
}
