using DIST.SUIST.BE;
using DIST.SUIST.DA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BL
{
    public class PerfilBL : IDisposable
    {
        #region Variables

        private Component component = new Component();
        private bool disposed = false;

        #endregion

        public List<PerfilBE> ListarPerfiles()
        {
            PerfilDA oPerfilDA = new PerfilDA();

            try
            {
                return oPerfilDA.ListarPerfiles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oPerfilDA = null;
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
