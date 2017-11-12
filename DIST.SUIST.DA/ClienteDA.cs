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
    public class ClienteDA
    {
        public ClienteBE ObtenerCliente(int IdCliente)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_OBTENERCLIENTE";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, IdCliente, !(IdCliente > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ClienteBE objClienteBE = new ClienteBE();
                            TipoClienteBE objTipoClienteBE = new TipoClienteBE();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"))) objClienteBE.IdCliente = (int)oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("TIP_IDTIPOCLIENTE"))) objTipoClienteBE.IdTipoCliente = (int)oRead.GetValue(oRead.GetOrdinal("TIP_IDTIPOCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_DOCUMENTOIDENTIDAD"))) objClienteBE.DocumentoIdentidad = (string)oRead.GetValue(oRead.GetOrdinal("CLI_DOCUMENTOIDENTIDAD"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_PREFIJO"))) objClienteBE.Prefijo = (string)oRead.GetValue(oRead.GetOrdinal("CLI_PREFIJO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_NOMBRES"))) objClienteBE.NombreCompleto = (string)oRead.GetValue(oRead.GetOrdinal("CLI_NOMBRES"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_EMAIL"))) objClienteBE.Email = (string)oRead.GetValue(oRead.GetOrdinal("CLI_EMAIL"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_TELEFONO"))) objClienteBE.Telefono = (string)oRead.GetValue(oRead.GetOrdinal("CLI_TELEFONO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_SITIOWEB"))) objClienteBE.SitioWeb = (string)oRead.GetValue(oRead.GetOrdinal("CLI_SITIOWEB"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_DIRECCION"))) objClienteBE.Direccion = (string)oRead.GetValue(oRead.GetOrdinal("CLI_DIRECCION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_FECHAINICIOCONTRATO"))) objClienteBE.FechaInicioContrato = (DateTime)oRead.GetValue(oRead.GetOrdinal("CLI_FECHAINICIOCONTRATO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_FECHAFINCONTRATO"))) objClienteBE.FechaFinContrato = (DateTime)oRead.GetValue(oRead.GetOrdinal("CLI_FECHAFINCONTRATO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_COLOR"))) objClienteBE.Color = (string)oRead.GetValue(oRead.GetOrdinal("CLI_COLOR"));
                                    
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            objClienteBE.TipoCliente = objTipoClienteBE;
                            
                            return objClienteBE;
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

        public List<ClienteBE> ListarClientes()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARCLIENTES";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ClienteBE objConsultaBE;
                            List<ClienteBE> lstConsultaBE = new List<ClienteBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new ClienteBE();
                                    TipoClienteBE objTipoClienteBE = new TipoClienteBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["CLI_IDCLIENTE"] != DBNull.Value) objConsultaBE.IdCliente = Convert.ToInt32(objDRConsulta["CLI_IDCLIENTE"]);
                                    if (objDRConsulta["TIP_DESCRIPCION"] != DBNull.Value) objTipoClienteBE.Descripcion = Convert.ToString(objDRConsulta["TIP_DESCRIPCION"]);
                                    if (objDRConsulta["CLI_DOCUMENTOIDENTIDAD"] != DBNull.Value) objConsultaBE.DocumentoIdentidad = Convert.ToString(objDRConsulta["CLI_DOCUMENTOIDENTIDAD"]);
                                    if (objDRConsulta["CLI_NOMBRES"] != DBNull.Value) objConsultaBE.NombreCompleto = Convert.ToString(objDRConsulta["CLI_NOMBRES"]);
                                    if (objDRConsulta["CLI_EMAIL"] != DBNull.Value) objConsultaBE.Email = Convert.ToString(objDRConsulta["CLI_EMAIL"]);
                                    if (objDRConsulta["CLI_TELEFONO"] != DBNull.Value) objConsultaBE.Telefono = Convert.ToString(objDRConsulta["CLI_TELEFONO"]);
                                    if (objDRConsulta["CLI_SITIOWEB"] != DBNull.Value) objConsultaBE.SitioWeb = Convert.ToString(objDRConsulta["CLI_SITIOWEB"]);
                                    if (objDRConsulta["CLI_DIRECCION"] != DBNull.Value) objConsultaBE.Direccion = Convert.ToString(objDRConsulta["CLI_DIRECCION"]);
                                    if (objDRConsulta["CLI_FECHAINICIOCONTRATO"] != DBNull.Value) objConsultaBE.FechaInicioContrato = Convert.ToDateTime(objDRConsulta["CLI_FECHAINICIOCONTRATO"]);
                                    if (objDRConsulta["CLI_FECHAFINCONTRATO"] != DBNull.Value) objConsultaBE.FechaFinContrato = Convert.ToDateTime(objDRConsulta["CLI_FECHAFINCONTRATO"]); 
                                    if (objDRConsulta["CLI_COLOR"] != DBNull.Value) objConsultaBE.Color = Convert.ToString(objDRConsulta["CLI_COLOR"]);

                                    objConsultaBE.TipoCliente = objTipoClienteBE;

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
                            return new List<ClienteBE>();
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

        public List<TipoClienteBE> ListarTipoClientes()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARTIPOCLIENTES";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            TipoClienteBE objConsultaBE;
                            List<TipoClienteBE> lstConsultaBE = new List<TipoClienteBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new TipoClienteBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["TIP_IDTIPOCLIENTE"] != DBNull.Value) objConsultaBE.IdTipoCliente = Convert.ToInt32(objDRConsulta["TIP_IDTIPOCLIENTE"]);
                                    if (objDRConsulta["TIP_DESCRIPCION"] != DBNull.Value) objConsultaBE.Descripcion = Convert.ToString(objDRConsulta["TIP_DESCRIPCION"]);
                                    
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
                            return new List<TipoClienteBE>();
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

        public bool GuardarCliente(ClienteBE objCliente, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDARCLIENTE";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objCliente.IdCliente, !(objCliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@TIP_IDTIPOCLIENTE", SqlDbType.Int, ParameterDirection.Input, objCliente.TipoCliente.IdTipoCliente, !(objCliente.TipoCliente.IdTipoCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_DOCUMENTOIDENTIDAD", SqlDbType.VarChar, ParameterDirection.Input, objCliente.DocumentoIdentidad, string.IsNullOrEmpty(objCliente.DocumentoIdentidad)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_PREFIJO", SqlDbType.VarChar, ParameterDirection.Input, objCliente.Prefijo, string.IsNullOrEmpty(objCliente.Prefijo)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_NOMBRES", SqlDbType.VarChar, ParameterDirection.Input, objCliente.NombreCompleto, string.IsNullOrEmpty(objCliente.NombreCompleto)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_EMAIL", SqlDbType.VarChar, ParameterDirection.Input, objCliente.Email, string.IsNullOrEmpty(objCliente.Email)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_TELEFONO", SqlDbType.VarChar, ParameterDirection.Input, objCliente.Telefono, string.IsNullOrEmpty(objCliente.Telefono)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_SITIOWEB", SqlDbType.VarChar, ParameterDirection.Input, objCliente.SitioWeb, string.IsNullOrEmpty(objCliente.SitioWeb)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_DIRECCION", SqlDbType.VarChar, ParameterDirection.Input, objCliente.Direccion, string.IsNullOrEmpty(objCliente.SitioWeb)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_FECHAINICIOCONTRATO", SqlDbType.Date, ParameterDirection.Input, objCliente.FechaInicioContrato, (objCliente.FechaInicioContrato == null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_FECHAFINCONTRATO", SqlDbType.Date, ParameterDirection.Input, objCliente.FechaFinContrato, (objCliente.FechaFinContrato == null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_COLOR", SqlDbType.VarChar, ParameterDirection.Input, objCliente.Color, string.IsNullOrEmpty(objCliente.Color)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objCliente.Auditoria.Usuario, string.IsNullOrEmpty(objCliente.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objCliente.IdCliente = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vCLI_IDCLIENTE_OUT"]));
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

        public bool EliminarCliente(int IdCliente, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_ELIMINARCLIENTE";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, IdCliente, !(IdCliente > 0)));

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
