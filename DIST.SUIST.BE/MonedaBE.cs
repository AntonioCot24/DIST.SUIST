using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class MonedaBE
    {
        #region Variables

        public int IdMoneda { get; set; }
        public string Descripcion { get; set; }
        public string Signo { get; set; }
        public bool TipoCambio { get; set; }
        public bool Predeteminado { get; set; }

        #endregion

        #region Objetos

        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
