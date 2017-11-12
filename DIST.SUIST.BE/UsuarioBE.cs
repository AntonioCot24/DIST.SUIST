using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class UsuarioBE
    {
        #region Variables

        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string NombreCompleto { get; set; }
        public bool MasterAdmin { get; set; }
        public bool Estado { get; set; }
        public string strEstado { get; set; }

        #endregion

        #region Objetos

        public PerfilBE Perfil { get; set; }
        public EmpresaBE Empresa { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
