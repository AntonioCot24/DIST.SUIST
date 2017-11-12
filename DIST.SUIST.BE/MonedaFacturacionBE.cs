using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class MonedaFacturacionBE
    {
        #region Variables

        public int IdMonedaFacturacion { get; set; }
        public int IdMoneda { get; set; }
        public int IdFacturacion { get; set; }

        #endregion

        #region Objetos

        public AuditoriaBE Auditoria { get; set; }
        public MonedaBE Moneda { get; set; }

        #endregion
    }
}
