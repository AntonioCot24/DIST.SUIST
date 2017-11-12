using DIST.SUIST.BE;
using DIST.SUIST.DA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BL
{
    public class ReporteBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<ActividadBE> ReporteProductividad(ActividadBE objActividadBE)
        {
            ReporteDA oReporteDA = new ReporteDA();

            try
            {
                return oReporteDA.ReporteProductividad(objActividadBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oReporteDA = null;
            }
        }
        
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    component.Dispose();
                }
                disposed = true;

            }
        }

        #endregion
    }
}
