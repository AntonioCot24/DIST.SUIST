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
    public class EmpresaBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<EmpresaBE> ListarEmpresas(int IdCliente)
        {
            EmpresaDA oEmpresaDA = new EmpresaDA();

            try
            {
                return oEmpresaDA.ListarEmpresas(IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oEmpresaDA = null;
            }
        }

        public EmpresaBE ObtenerEmpresa(int IdEmpresa)
        {
            EmpresaDA oEmpresaDA = new EmpresaDA();

            try
            {
                return oEmpresaDA.ObtenerEmpresa(IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oEmpresaDA = null;
            }
        }

        public bool GuardarEmpresa(EmpresaBE objEmpresa, out string mensaje)
        {
            EmpresaDA objEmpresaDA = new EmpresaDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objEmpresaDA.GuardarEmpresa(objEmpresa, out mensaje))
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

        public bool EliminarEmpresa(int IdEmpresa, out string mensaje)
        {
            EmpresaDA objEmpresaDA = new EmpresaDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objEmpresaDA.EliminarEmpresa(IdEmpresa, out mensaje))
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
