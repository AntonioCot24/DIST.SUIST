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
    public class ContactoBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<ContactoBE> ListarContactos(int IdCliente)
        {
            ContactoDA oContactoDA = new ContactoDA();

            try
            {
                return oContactoDA.ListarContactos(IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oContactoDA = null;
            }
        }

        public ContactoBE ObtenerContacto(int IdContacto)
        {
            ContactoDA oContactoDA = new ContactoDA();

            try
            {
                return oContactoDA.ObtenerContacto(IdContacto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oContactoDA = null;
            }
        }

        public bool GuardarContacto(ContactoBE objContacto, out string mensaje)
        {
            ContactoDA objContactoDA = new ContactoDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objContactoDA.GuardarContacto(objContacto, out mensaje))
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

        public bool EliminarContacto(int IdContacto, out string mensaje)
        {
            ContactoDA objContactoDA = new ContactoDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objContactoDA.EliminarContacto(IdContacto, out mensaje))
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
