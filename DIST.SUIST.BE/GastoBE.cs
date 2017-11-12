using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class GastoBE
    {
        #region Variables

        public int IdGasto { get; set; }
        public int IdCliente { get; set; }
        public int IdProyecto { get; set; }
        public int IdUsuario { get; set; }
        public string NombreAbogado { get; set; }
        public DateTime? Fecha { get; set; }
        public string Glosa { get; set; }
        public double Monto { get; set; }
        public string strEstado { get; set; }

        #endregion

        #region Objetos

        public ClienteBE Cliente { get; set; }
        public ProyectoBE Proyecto { get; set; }
        public UsuarioBE Usuario { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
