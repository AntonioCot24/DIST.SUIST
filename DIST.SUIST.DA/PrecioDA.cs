using System.Data.SqlClient;
using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.DA
{
    public class PrecioDA
    {
        public PrecioBE ObtenerPrecio(PrecioBE oPrecio)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_OBTENERPRECIO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@PRE_IDPRECIO", SqlDbType.Int, ParameterDirection.Input, oPrecio.IdPrecio, !(oPrecio.IdPrecio > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, oPrecio.Cliente.IdCliente, !(oPrecio.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, oPrecio.TipoActividad.IdTipoActividad, !(oPrecio.TipoActividad.IdTipoActividad > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            PrecioBE objPrecioBE = new PrecioBE();
                            ClienteBE objClienteBE = new ClienteBE();
                            TipoActividadBE objTipoActividadBE = new TipoActividadBE();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRE_IDPRECIO"))) objPrecioBE.IdPrecio = (int)oRead.GetValue(oRead.GetOrdinal("PRE_IDPRECIO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"))) objClienteBE.IdCliente = (int)oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("TAC_IDTIPOACTIVIDAD"))) objTipoActividadBE.IdTipoActividad = (int)oRead.GetValue(oRead.GetOrdinal("TAC_IDTIPOACTIVIDAD"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRE_MONTO"))) objPrecioBE.Monto = (double)oRead.GetValue(oRead.GetOrdinal("PRE_MONTO"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            objPrecioBE.Cliente = objClienteBE;
                            objPrecioBE.TipoActividad = objTipoActividadBE;

                            return objPrecioBE;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (NewDA_CONEXION.conexion.State == ConnectionState.Open) NewDA_CONEXION.retClose();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarPrecio(PrecioBE objPrecio, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDARPRECIO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRE_IDPRECIO", SqlDbType.Int, ParameterDirection.Input, objPrecio.IdPrecio, !(objPrecio.IdPrecio > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objPrecio.Cliente.IdCliente, !(objPrecio.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, objPrecio.TipoActividad.IdTipoActividad, !(objPrecio.TipoActividad.IdTipoActividad > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRE_MONTO", SqlDbType.Double, ParameterDirection.Input, objPrecio.Monto, !(objPrecio.Monto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objPrecio.Auditoria.Usuario, string.IsNullOrEmpty(objPrecio.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRE_IDPRECIO_OUT", SqlDbType.Int, ParameterDirection.Output, null));
                    cmd.Parameters.Add(UtilDA.SetParameter("@RESULTADO", SqlDbType.VarChar, ParameterDirection.Output, null));
                    cmd.Parameters.Add(UtilDA.SetParameter("@MENSAJE", SqlDbType.VarChar, ParameterDirection.Output, null));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        int Retorno = 0;

                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                objPrecio.IdPrecio = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vPRE_IDPRECIO_OUT"]));
                                mensaje = UtilDA.ParseParameter(cmd.Parameters["vMENSAJE"]).ToString();
                                Retorno = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vRESULTADO"]));

                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (NewDA_CONEXION.conexion.State == ConnectionState.Open) NewDA_CONEXION.retClose();

                        }
                        if (Retorno == 1)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
