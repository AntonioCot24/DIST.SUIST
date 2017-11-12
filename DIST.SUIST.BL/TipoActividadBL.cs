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
    public class TipoActividadBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<TipoActividadBE> ListarTipoActividades()
        {
            TipoActividadDA oTipoActividadDA = new TipoActividadDA();

            try
            {
                return oTipoActividadDA.ListarTipoActividades();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oTipoActividadDA = null;
            }
        }

        public TipoActividadBE ObtenerTipoActividad(int IdTipoActividad)
        {
            TipoActividadDA oTipoActividadDA = new TipoActividadDA();

            try
            {
                return oTipoActividadDA.ObtenerTipoActividad(IdTipoActividad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oTipoActividadDA = null;
            }
        }

        public bool GuardarTipoActividad(TipoActividadBE objTipoActividad, out string mensaje)
        {
            TipoActividadDA objTipoActividadDA = new TipoActividadDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objTipoActividadDA.GuardarTipoActividad(objTipoActividad, out mensaje))
                    {
                        /*using (PrecioBL objPrecioBL = new PrecioBL())
                        {
                            if (objTipoActividad.Precio.Monto!= 0)
                            {
                                PrecioBE objPrecioBE = objTipoActividad.Precio;
                                objPrecioBE.TipoActividad = objTipoActividad;
                                objPrecioBE.Cliente = new ClienteBE();
                                objPrecioBE.Auditoria = objTipoActividad.Auditoria;

                                string msjPrecio;
                                if (!objPrecioBL.GuardarPrecio(objPrecioBE, out msjPrecio))
                                {
                                    transaccion.Dispose();
                                    return false;
                                }
                            }
                        }*/
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

        public bool EliminarTipoActividad(int IdTipoActividad, out string mensaje)
        {
            TipoActividadDA objTipoActividadDA = new TipoActividadDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objTipoActividadDA.EliminarTipoActividad(IdTipoActividad, out mensaje))
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
