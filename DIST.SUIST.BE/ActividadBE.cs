using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class ActividadBE
    {
        #region Variables

        public int IdActividad { get; set; }
        public string strUsuario { get; set; }
        public string strNombreCompleto { get; set; }
        public string Glosa { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int TotalHoras { get; set; }
        public int TotalMinutos { get; set; }
        public bool Facturable { get; set; }

        public int IdProyecto { get; set; }
        public string strFecha { get; set; }
        public string strFechaTitulo { get; set; }
        public string strFechaInicio { get; set; }
        public string strFechaFin { get; set; }
        public string strFechaAcumulada { get; set; }

        #endregion

        #region Objetos

        public UsuarioBE Usuario { get; set; }
        public ClienteBE Cliente { get; set; }
        public ProyectoBE Proyecto { get; set; }
        public TipoActividadBE TipoActividad { get; set; }
        public ContactoBE Contacto { get; set; }
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }

    public class ActividadExportarBE
    {
        #region Variables

        public string Titulo { get; set; }
        public string Cliente { get; set; }
        public string Fecha { get; set; }
        public string totalHorasMes { get; set; }
        public string totalMontoContrato { get; set; }
        public string totalMontoProyecto { get; set; }
        public string totalHorasRetainer { get; set; }
        public string totalHorasExceso { get; set; }
        public string totalMontoRetainer { get; set; }
        public string totalMontoHoraExceso { get; set; }
        public string totalMontoFlat { get; set; }
        public string totalPagar { get; set; }

        #endregion

        #region Objetos

        public List<ListaActividadesBE> ListaActividadesBE { get; set; }

        #endregion     
    }

    public class ListaActividadesBE
    {
        #region Variables

        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string Tema { get; set; }
        public string Tiempo { get; set; }
        public string TiempoAcumulado { get; set; }
       
        #endregion        
    }
}
