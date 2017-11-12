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
    public class GastoDA
    {
        public GastoBE ObtenerGasto(int IdGasto)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_OBTENERGASTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@GAS_IDGASTO", SqlDbType.Int, ParameterDirection.Input, IdGasto, !(IdGasto > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            GastoBE objGastoBE = new GastoBE();

                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("GAS_IDGASTO"))) objGastoBE.IdGasto = (int)oRead.GetValue(oRead.GetOrdinal("GAS_IDGASTO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"))) objGastoBE.IdCliente = (int)oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRY_IDPROYECTO"))) objGastoBE.IdProyecto = (int)oRead.GetValue(oRead.GetOrdinal("PRY_IDPROYECTO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"))) objGastoBE.IdUsuario = (int)oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"))) objGastoBE.NombreAbogado = (string)oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("GAS_FECHA"))) objGastoBE.Fecha = (DateTime)oRead.GetValue(oRead.GetOrdinal("GAS_FECHA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("GAS_GLOSA"))) objGastoBE.Glosa = (string)oRead.GetValue(oRead.GetOrdinal("GAS_GLOSA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("GAS_MONTO"))) objGastoBE.Monto = (double)oRead.GetValue(oRead.GetOrdinal("GAS_MONTO"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();
                            
                            return objGastoBE;
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

        public List<GastoBE> ListarGastos()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_LISTARGASTOS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            List<GastoBE> lstConsultaBE = new List<GastoBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    GastoBE objConsultaBE = new GastoBE();
                                    ClienteBE objClienteBE = new ClienteBE();
                                    ProyectoBE objProyectoBE = new ProyectoBE();
                                    UsuarioBE objUsuarioBE = new UsuarioBE();

                                    if (objDRConsulta["GAS_IDGASTO"] != DBNull.Value) objConsultaBE.IdGasto = Convert.ToInt32(objDRConsulta["GAS_IDGASTO"]);
                                    if (objDRConsulta["CLI_IDCLIENTE"] != DBNull.Value) objConsultaBE.IdCliente = Convert.ToInt32(objDRConsulta["CLI_IDCLIENTE"]);
                                    if (objDRConsulta["CLI_NOMBRES"] != DBNull.Value) objClienteBE.NombreCompleto = Convert.ToString(objDRConsulta["CLI_NOMBRES"]);
                                    if (objDRConsulta["PRY_IDPROYECTO"] != DBNull.Value) objConsultaBE.IdProyecto = Convert.ToInt32(objDRConsulta["PRY_IDPROYECTO"]);
                                    if (objDRConsulta["PRY_NOMBRE"] != DBNull.Value) objProyectoBE.NombreProyecto = Convert.ToString(objDRConsulta["PRY_NOMBRE"]);
                                    if (objDRConsulta["USU_IDUSUARIO"] != DBNull.Value) objConsultaBE.IdUsuario = Convert.ToInt32(objDRConsulta["USU_IDUSUARIO"]);
                                    if (objDRConsulta["USU_NOMBRECOMPLETO"] != DBNull.Value) objUsuarioBE.NombreCompleto = Convert.ToString(objDRConsulta["USU_NOMBRECOMPLETO"]);
                                    if (objDRConsulta["GAS_FECHA"] != DBNull.Value) objConsultaBE.Fecha = Convert.ToDateTime(objDRConsulta["GAS_FECHA"]);
                                    if (objDRConsulta["GAS_MONTO"] != DBNull.Value) objConsultaBE.Monto = Convert.ToDouble(objDRConsulta["GAS_MONTO"]);
                                    if (objDRConsulta["USU_STRESTADO"] != DBNull.Value) objConsultaBE.strEstado = Convert.ToString(objDRConsulta["USU_STRESTADO"]);

                                    objConsultaBE.Cliente = objClienteBE;
                                    objConsultaBE.Proyecto = objProyectoBE;
                                    objConsultaBE.Usuario = objUsuarioBE;

                                    lstConsultaBE.Add(objConsultaBE);
                                }
                                objDRConsulta.Close();
                            }
                            NewDA_CONEXION.retClose();
                            return lstConsultaBE;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return new List<GastoBE>();
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

        public bool GuardarGasto(GastoBE objGasto, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_GUARDARGASTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@GAS_IDGASTO", SqlDbType.Int, ParameterDirection.Input, objGasto.IdGasto, !(objGasto.IdGasto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objGasto.IdCliente, !(objGasto.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_IDPROYECTO", SqlDbType.Int, ParameterDirection.Input, objGasto.IdProyecto, !(objGasto.IdProyecto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, objGasto.IdUsuario, !(objGasto.IdUsuario > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@GAS_FECHA", SqlDbType.DateTime, ParameterDirection.Input, objGasto.Fecha, (objGasto.Fecha == null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@GAS_GLOSA", SqlDbType.VarChar, ParameterDirection.Input, objGasto.Glosa, string.IsNullOrEmpty(objGasto.Glosa)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@GAS_MONTO", SqlDbType.Double, ParameterDirection.Input, objGasto.Monto, !(objGasto.Monto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objGasto.Auditoria.Usuario, string.IsNullOrEmpty(objGasto.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@GAS_IDGASTO_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objGasto.IdGasto = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vGAS_IDGASTO_OUT"]));
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

        public bool EliminarGasto(int IdGasto, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_ELIMINARGASTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@GAS_IDGASTO", SqlDbType.Int, ParameterDirection.Input, IdGasto, !(IdGasto > 0)));

                    //Paremetros de salida
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
