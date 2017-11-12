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
    public class FacturacionBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<FacturacionBE> ListarFacturaciones(int IdCliente)
        {
            FacturacionDA oFacturacionDA = new FacturacionDA();

            try
            {
                return oFacturacionDA.ListarFacturaciones(IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oFacturacionDA = null;
            }
        }

        public FacturacionBE ObtenerFacturacion(int IdFacturacion, int IdCliente)
        {
            FacturacionDA oFacturacionDA = new FacturacionDA();
            MonedaDA oMonedaDA = new MonedaDA();

            try
            {
                FacturacionBE objFacturacion = oFacturacionDA.ObtenerFacturacion(IdFacturacion, IdCliente);
                MonedaBE objMoneda = oMonedaDA.ObtenerMonedaPredeterminada();

                List<MonedaFacturacionBE> LstMonedaFacturacion = oFacturacionDA.ListarMonedaFacturacion(objFacturacion.IdFacturacion);

                if (!LstMonedaFacturacion.Any(mf => mf.IdMoneda.Equals(objMoneda.IdMoneda)))
                {
                    LstMonedaFacturacion.Add(new MonedaFacturacionBE { Moneda = objMoneda });
                }

                objFacturacion.lstMonedaFacturacion = LstMonedaFacturacion.OrderByDescending(mf => mf.Moneda.Predeteminado).ToList();
                return objFacturacion;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oFacturacionDA = null;
            }
        }

        public List<MonedaFacturacionBE> ListarMonedaFacturacion(int IdFactura)
        {
            FacturacionDA oFacturacionDA = new FacturacionDA();

            try
            {
                return oFacturacionDA.ListarMonedaFacturacion(IdFactura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oFacturacionDA = null;
            }
        }

        public bool GuardarFacturacion(FacturacionBE objFacturacion, out string mensaje)
        {
            FacturacionDA objFacturacionDA = new FacturacionDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objFacturacionDA.GuardarFacturacion(objFacturacion, out mensaje))
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

        public bool GuardarMonedaFacturacion(MonedaFacturacionBE objMonedaFacturacion, out string mensaje)
        {
            FacturacionDA objFacturacionDA = new FacturacionDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objFacturacionDA.GuardarMonedaFacturacion(objMonedaFacturacion, out mensaje))
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

        public bool EliminarFacturacion(int IdFacturacion, out string mensaje)
        {
            FacturacionDA objFacturacionDA = new FacturacionDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objFacturacionDA.EliminarFacturacion(IdFacturacion, out mensaje))
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

        public bool EliminarMonedaFacturacion(int IdMonedaFacturacion, out string mensaje)
        {
            FacturacionDA objFacturacionDA = new FacturacionDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objFacturacionDA.EliminarMonedaFacturacion(IdMonedaFacturacion, out mensaje))
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
