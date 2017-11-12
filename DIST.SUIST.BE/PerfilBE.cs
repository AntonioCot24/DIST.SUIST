using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class PerfilBE
    {
        #region Variables

        public int IdPerfil { get; set; }
        public string Denominacion { get; set; }

        #endregion

        #region Objetos

        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
