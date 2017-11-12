using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class EmpresaBE
    {
        #region Variables

        public int IdEmpresa { get; set; }
        public string Ruc { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Telefeno { get; set; }
        public string Email { get; set; }
        public DateTime? ContratoInicio { get; set; }
        public DateTime? ContratoFin { get; set; }
        public string Logo { get; set; }
        public string LogoDocumentos { get; set; }

        #endregion

        #region Objetos

        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
