using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class OpcionBE
    {
        #region Variables

        public int IdOpcion { get; set; }
        public int IdOpcionPadre { get; set; }
        public string Denominacion { get; set; }
        public string UrlOpcion { get; set; }
        public string Icono { get; set; }

        #endregion

        #region Objetos

        public PerfilBE Perfil { get; set; }

        #endregion
    }
}
