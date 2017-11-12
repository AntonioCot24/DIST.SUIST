using System.Data.SqlClient;
using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.DA
{
    public interface IConexion
    {
        SqlConnection retConexion();
        string retStrConexion();
        void retOpen();
        void retClose();
    }

    public class ConexionDA : IConexion, IDisposable
    {
        #region "variables"

        public SqlConnection conexion;
        private Component component = new Component();
        private bool disposed = false;

        #endregion

        #region "Metodos"

        public string retStrConexion()
        {
            string strConexion = string.Empty;
            try
            {
                strConexion = ParametrosConfiguracionBE.CadenaConexion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strConexion;
        }

        public SqlConnection retConexion()
        {
            return conexion;
        }

        public void retOpen()
        {
            if (conexion == null)
            {
                conexion = new SqlConnection(retStrConexion());

                if (conexion.State !=  ConnectionState.Open)
                {
                    try
                    {
                        conexion.Open();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public void retClose()
        {
            conexion.Close();
            conexion.Dispose();
        }

        #endregion

        #region Devolver conjunto de registros en un DataTable

        public DataTable getTable(SqlCommand cmd)
        {
            DataTable dtt = new DataTable();
            try
            {
                retOpen();
                cmd.Connection = conexion;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dtt.Load(reader, LoadOption.OverwriteChanges);
                    reader.Close();
                }
                retClose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return dtt;
        }

        #endregion

        #region Ejecutar Command

        public int ejecutaSQL(SqlCommand cmd)
        {
            int res = 0;
            try
            {
                retOpen();
                cmd.Connection = conexion;
                res = cmd.ExecuteNonQuery();
                retClose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return res;
        }

        #endregion

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
