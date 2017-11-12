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
    public class ActividadBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<ActividadBE> ListarActividades(int IdUsuario)
        {
            ActividadDA oActividadDA = new ActividadDA();

            try
            {
                return oActividadDA.ListarActividades(IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oActividadDA = null;
            }
        }

        public List<ActividadBE> ListarActividadesFechas(ActividadBE oActividadBE)
        {
            ActividadDA oActividadDA = new ActividadDA();

            try
            {
                return oActividadDA.ListarActividadesFechas(oActividadBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oActividadDA = null;
            }
        }

        public ActividadBE ObtenerActividad(int IdActividad)
        {
            ActividadDA oActividadDA = new ActividadDA();

            try
            {
                return oActividadDA.ObtenerActividad(IdActividad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oActividadDA = null;
            }
        }

        public bool GuardarActividad(ActividadBE objActividad, out string mensaje)
        {
            ActividadDA objActividadDA = new ActividadDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objActividadDA.GuardarActividad(objActividad, out mensaje))
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

        public bool EliminarActividad(int IdActividad, out string mensaje)
        {
            ActividadDA objActividadDA = new ActividadDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objActividadDA.EliminarActividad(IdActividad, out mensaje))
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
