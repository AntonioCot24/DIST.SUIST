using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class ContactoBE
    {
        #region Variables

        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoCelular { get; set; }
        public string Correo { get; set; }
        public string Cargo { get; set; }
        public bool Principal { get; set; }

        #endregion

        #region Objetos

        public ClienteBE Cliente { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion

    }
}
