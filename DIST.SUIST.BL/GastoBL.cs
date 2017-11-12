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
    public class GastoBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<GastoBE> ListarGastos()
        {
            GastoDA oGastoDA = new GastoDA();

            try
            {
                return oGastoDA.ListarGastos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGastoDA = null;
            }
        }

        public GastoBE ObtenerGasto(int IdGasto)
        {
            GastoDA oGastoDA = new GastoDA();

            try
            {
                return oGastoDA.ObtenerGasto(IdGasto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oGastoDA = null;
            }
        }

        public bool GuardarGasto(GastoBE objGasto, out string mensaje)
        {
            GastoDA objGastoDA = new GastoDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objGastoDA.GuardarGasto(objGasto, out mensaje))
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

        public bool EliminarGasto(int IdGasto, out string mensaje)
        {
            GastoDA objGastoDA = new GastoDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objGastoDA.EliminarGasto(IdGasto, out mensaje))
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
