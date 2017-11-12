using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    public class FacturacionBE
    {
        #region Variables

        public int IdFacturacion { get; set; }
        public int TipoFacturacion { get; set; }
        public EnumeradoresBE.enumTipoFacturacion enumTipoFactuacion { get; set; }
        public string strTipoFacturacion { get; set; }
        public double TarifaFija { get; set; }
        public double TarifaHoras { get; set; }
        public double TarifaHorasAdicionales { get; set; }
        public double MontoFlat { get; set; }
        public int FechaFactura { get; set; }
        public string Direccion { get; set; }
        public double PrecioProyecto { get; set; }

        #endregion

        #region Objetos

        public ClienteBE Cliente { get; set; }
        public ContactoBE Contacto { get; set; }
        public AuditoriaBE Auditoria { get; set; }
        public List<MonedaFacturacionBE> lstMonedaFacturacion { get; set; }

        #endregion

    }
}
