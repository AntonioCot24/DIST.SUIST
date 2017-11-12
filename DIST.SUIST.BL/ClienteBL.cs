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
    public class ClienteBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion
        
        public List<ClienteBE> ListarClientes()
        {
            ClienteDA oClienteDA = new ClienteDA();

            try
            {
                return oClienteDA.ListarClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oClienteDA = null;
            }
        }

        public List<TipoClienteBE> ListarTipoClientes()
        {
            ClienteDA oClienteDA = new ClienteDA();

            try
            {
                return oClienteDA.ListarTipoClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oClienteDA = null;
            }
        }
        
        public ClienteBE ObtenerCliente(int IdCliente)
        {
            ClienteDA oClienteDA = new ClienteDA();

            try
            {
                return oClienteDA.ObtenerCliente(IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oClienteDA = null;
            }
        }

        public bool GuardarCliente(ClienteBE objCliente, out string mensaje)
        {
            ClienteDA objClienteDA = new ClienteDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objClienteDA.GuardarCliente(objCliente, out mensaje))
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

        public bool EliminarCliente(int IdCliente, out string mensaje)
        {
            ClienteDA objClienteDA = new ClienteDA();

            try
            {
                using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (objClienteDA.EliminarCliente(IdCliente, out mensaje))
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
