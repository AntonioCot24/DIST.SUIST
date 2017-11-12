using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class TipoActividadBE
    {
        #region Variables

        public int IdTipoActividad { get; set; }
        public string Nombre { get; set; }

        #endregion

        #region Objetos

        public PrecioBE Precio { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
