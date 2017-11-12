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
    public class SeguridadBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public UsuarioBE ValidarUsuario(UsuarioBE objUsuario)
        {
            SeguridadDA oSeguridadDA = new SeguridadDA();
            EmpresaDA oEmpresaDA = new EmpresaDA();

            try
            {
                UsuarioBE oUsuario =  oSeguridadDA.ValidarUsuario(objUsuario);
                //oUsuario.Empresa = oEmpresaDA.ObtenerEmpresa(oUsuario.IdEmpresa);
                return oUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSeguridadDA = null;
            }
        }

        public List<OpcionBE> ListarOpcionesUsuarios(int idPerfil)
        {
            SeguridadDA oSeguridadDA = new SeguridadDA();

            try
            {
                return oSeguridadDA.ListarOpcionesUsuarios(idPerfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSeguridadDA = null;
            }
        }

        public bool ActualizarContraseniaUsuario(UsuarioBE objUsuario, out string mensaje)
        {
            SeguridadDA oSeguridadDA = new SeguridadDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (oSeguridadDA.ActualizarContraseniaUsuario(objUsuario, out mensaje))
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
