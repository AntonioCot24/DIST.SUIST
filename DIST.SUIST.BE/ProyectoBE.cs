using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class ProyectoBE
    {
        #region Variables

        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public double Precio { get; set; }

        #endregion

        #region Objetos

        public ClienteBE Cliente { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
