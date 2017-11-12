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
    public class MonedaBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public MonedaBE ObtenerMoneda(int IdMoneda)
        {
            MonedaDA oMonedaDA = new MonedaDA();

            try
            {
                return oMonedaDA.ObtenerMoneda(IdMoneda); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oMonedaDA = null;
            }
        }

        public MonedaBE ObtenerMonedaPredeterminada()
        {
            MonedaDA oMonedaDA = new MonedaDA();

            try
            {
                return oMonedaDA.ObtenerMonedaPredeterminada(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oMonedaDA = null;
            }
        }

        public List<MonedaBE> ListarMonedas()
        {
            MonedaDA oMonedaDA = new MonedaDA();

            try
            {
                return oMonedaDA.ListarMonedas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oMonedaDA = null;
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
