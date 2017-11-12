using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class ClienteBE
    {
        #region Variables

        public int IdCliente { get; set; }
        public string Prefijo { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string SitioWeb { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaInicioContrato { get; set; }
        public DateTime? FechaFinContrato { get; set; }
        public string Color { get; set; }

        #endregion

        #region Objetos

        public TipoClienteBE TipoCliente { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
