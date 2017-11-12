using DIST.SUIST.BE;
using DIST.SUIST.DA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DIST.SUIST.BL
{
    public class PrecioBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public PrecioBE ObtenerPrecio(PrecioBE oPrecio)
        {
            PrecioDA oPrecioDA = new PrecioDA();

            try
            {
                return oPrecioDA.ObtenerPrecio(oPrecio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oPrecioDA = null;
            }
        }

        public bool GuardarPrecio(PrecioBE objPrecio, out string mensaje)
        {
            PrecioDA objPrecioDA = new PrecioDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objPrecioDA.GuardarPrecio(objPrecio, out mensaje))
                    {
                        transaccion.Complete();
                        return true;
                    }
                    else
                    {
                        transaccion.Dispose();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
