using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.DA
{
    public class UtilDA
    {
        public static Object ParseParameter(SqlParameter param)
        {
            if (param.Value != DBNull.Value)
                return param.Value;

            switch (param.SqlDbType)
            {
                case SqlDbType.VarChar:
                    return string.Empty;
                case SqlDbType.DateTime:
                    return DateTime.MinValue;
                case SqlDbType.Int:
                    return long.MinValue;
                default:
                    return null;
            }
        }

        public static SqlParameter SetParameter(string nombre, SqlDbType tipodato, ParameterDirection direccion, object valor, bool esnulo = false)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = nombre;
            param.Direction = direccion;
            param.SqlDbType = tipodato;
            
            if (direccion == ParameterDirection.Input)
            {
                if (!esnulo)
                    param.Value = valor;
                else
                    param.Value = DBNull.Value;
                param.IsNullable = true;
            }
            else
            {
                if ((nombre == "po_Error") && (tipodato == SqlDbType.VarChar))
                {
                    param.Size = 1000;
                }
                else
                {
                    param.Size = 200;
                }
            }

            return param;
        }

        public static bool ValidateExistsColumn(IDataReader dataReader, string columnName)
        {
            bool isExistsColumn = false;

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (dataReader.GetName(i).Equals(columnName))
                {
                    isExistsColumn = true;
                    break;
                }
            }

            return isExistsColumn;
        }
    }
}
