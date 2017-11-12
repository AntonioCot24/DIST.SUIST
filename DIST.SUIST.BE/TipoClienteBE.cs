using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class TipoClienteBE
    {
        #region Variables

        public int IdTipoCliente { get; set; }
        public string Descripcion { get; set; }

        #endregion

        #region Objetos

        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
