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
    public class UsuarioBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<UsuarioBE> ListarUsuarios()
        {
            UsuarioDA oUsuarioDA = new UsuarioDA();

            try
            {
                return oUsuarioDA.ListarUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oUsuarioDA = null;
            }
        }

        public UsuarioBE ObtenerUsuario(int IdUsuario)
        {
            UsuarioDA oUsuarioDA = new UsuarioDA();

            try
            {
                return oUsuarioDA.ObtenerUsuario(IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oUsuarioDA = null;
            }
        }

        public bool GuardarUsuario(UsuarioBE objUsuario, out string mensaje)
        {
            UsuarioDA objUsuarioDA = new UsuarioDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objUsuarioDA.GuardarUsuario(objUsuario, out mensaje))
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

        public bool EliminarUsuario(int IdUsuario, out string mensaje)
        {
            UsuarioDA objUsuarioDA = new UsuarioDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objUsuarioDA.EliminarUsuario(IdUsuario, out mensaje))
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
