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
    public class ProyectoBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<ProyectoBE> ListarProyectos()
        {
            ProyectoDA oProyectoDA = new ProyectoDA();

            try
            {
                return oProyectoDA.ListarProyectos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oProyectoDA = null;
            }
        }

        public List<ProyectoBE> ListarProyectosCliente(int IdCliente)
        {
            ProyectoDA oProyectoDA = new ProyectoDA();

            try
            {
                return oProyectoDA.ListarProyectosCliente(IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oProyectoDA = null;
            }
        }

        public ProyectoBE ObtenerProyecto(int IdProyecto)
        {
            ProyectoDA oProyectoDA = new ProyectoDA();

            try
            {
                return oProyectoDA.ObtenerProyecto(IdProyecto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oProyectoDA = null;
            }
        }

        public bool GuardarProyecto(ProyectoBE objProyecto, out string mensaje)
        {
            ProyectoDA objProyectoDA = new ProyectoDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objProyectoDA.GuardarProyecto(objProyecto, out mensaje))
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

        public bool EliminarProyecto(int IdProyecto, out string mensaje)
        {
            ProyectoDA objProyectoDA = new ProyectoDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objProyectoDA.EliminarProyecto(IdProyecto, out mensaje))
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
