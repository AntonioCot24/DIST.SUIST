using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class PrecioBE
    {
        #region Variables

        public int IdPrecio { get; set; }
        public double Monto { get; set; }

        #endregion

        #region Objetos

        public ClienteBE Cliente { get; set; }
        public TipoActividadBE TipoActividad { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
