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
    public class EmpresaDA
    {
        public EmpresaBE ObtenerEmpresa(int IdEmpresa)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_OBTENEREMPRESA";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@EMP_IDEMPRESA", SqlDbType.Int, ParameterDirection.Input, IdEmpresa, !(IdEmpresa > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try 
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            EmpresaBE objEmpresaBE = new EmpresaBE();

                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_IDEMPRESA"))) objEmpresaBE.IdEmpresa = (int)oRead.GetValue(oRead.GetOrdinal("EMP_IDEMPRESA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_RUC"))) objEmpresaBE.Ruc = (string)oRead.GetValue(oRead.GetOrdinal("EMP_RUC"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_DIRECCION"))) objEmpresaBE.Direccion = (string)oRead.GetValue(oRead.GetOrdinal("EMP_DIRECCION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTONOMBRE"))) objEmpresaBE.Nombre = (string)oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTONOMBRE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTOCARGO"))) objEmpresaBE.Cargo = (string)oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTOCARGO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTOTELEFONO"))) objEmpresaBE.Telefeno = (string)oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTOTELEFONO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTOEMAL"))) objEmpresaBE.Email = (string)oRead.GetValue(oRead.GetOrdinal("EMP_CONTACTOEMAL"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_CONTRATOINICIO"))) objEmpresaBE.ContratoInicio = (DateTime)oRead.GetValue(oRead.GetOrdinal("EMP_CONTRATOINICIO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_CONTRATOFIN"))) objEmpresaBE.ContratoFin = (DateTime)oRead.GetValue(oRead.GetOrdinal("EMP_CONTRATOFIN"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_LOGO"))) objEmpresaBE.Logo = (string)oRead.GetValue(oRead.GetOrdinal("EMP_LOGO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_LOGODOCUMENTOS"))) objEmpresaBE.LogoDocumentos = (string)oRead.GetValue(oRead.GetOrdinal("EMP_LOGODOCUMENTOS"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();
                            

                            return objEmpresaBE;
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

        public List<EmpresaBE> ListarEmpresas(int IdCliente)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTAREmpresaS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, IdCliente, !(IdCliente > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            EmpresaBE objConsultaBE;
                            List<EmpresaBE> lstConsultaBE = new List<EmpresaBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new EmpresaBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["CON_IDEmpresa"] != DBNull.Value) objConsultaBE.IdEmpresa = Convert.ToInt32(objDRConsulta["CON_IDEmpresa"]);

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
                            return new List<EmpresaBE>();
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

        public bool GuardarEmpresa(EmpresaBE objEmpresa, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDAREmpresa";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDEmpresa", SqlDbType.Int, ParameterDirection.Input, objEmpresa.IdEmpresa, !(objEmpresa.IdEmpresa > 0)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objEmpresa.Cliente.IdCliente, !(objEmpresa.Cliente.IdCliente > 0)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CON_NOMBRE", SqlDbType.VarChar, ParameterDirection.Input, objEmpresa.Nombre, string.IsNullOrEmpty(objEmpresa.Nombre)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CON_DIRECCION", SqlDbType.VarChar, ParameterDirection.Input, objEmpresa.Direccion, string.IsNullOrEmpty(objEmpresa.Direccion)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CON_TELEFONOFIJO", SqlDbType.VarChar, ParameterDirection.Input, objEmpresa.TelefonoFijo, string.IsNullOrEmpty(objEmpresa.TelefonoFijo)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CON_TELEFONOCELULAR", SqlDbType.VarChar, ParameterDirection.Input, objEmpresa.TelefonoCelular, string.IsNullOrEmpty(objEmpresa.TelefonoCelular)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CON_CORREO", SqlDbType.VarChar, ParameterDirection.Input, objEmpresa.Correo, string.IsNullOrEmpty(objEmpresa.Correo)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CON_CARGO", SqlDbType.VarChar, ParameterDirection.Input, objEmpresa.Cargo, string.IsNullOrEmpty(objEmpresa.Cargo)));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@CON_PRINCIPAL", SqlDbType.Bit, ParameterDirection.Input, objEmpresa.Principal, false));
                    //cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objEmpresa.Auditoria.Usuario, string.IsNullOrEmpty(objEmpresa.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDEmpresa_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objEmpresa.IdEmpresa = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vCON_IDEmpresa_OUT"]));
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

        public bool EliminarEmpresa(int IdEmpresa, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_ELIMINAREmpresa";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDEmpresa", SqlDbType.Int, ParameterDirection.Input, IdEmpresa, !(IdEmpresa > 0)));

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
