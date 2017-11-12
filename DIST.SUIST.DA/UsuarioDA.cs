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
    public class UsuarioDA
    {
        public UsuarioBE ObtenerUsuario(int IdUsuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_MAE_SP_OBTENERUSUARIO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, IdUsuario, !(IdUsuario > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            UsuarioBE objUsuarioBE = new UsuarioBE();
                            PerfilBE objPerfilBE = new PerfilBE();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"))) objUsuarioBE.IdUsuario = (int)oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PER_IDPERFIL"))) objPerfilBE.IdPerfil = (int)oRead.GetValue(oRead.GetOrdinal("PER_IDPERFIL"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_EMAIL"))) objUsuarioBE.Usuario = (string)oRead.GetValue(oRead.GetOrdinal("USU_EMAIL"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_CONTRASENIA"))) objUsuarioBE.Contrasenia = (string)oRead.GetValue(oRead.GetOrdinal("USU_CONTRASENIA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"))) objUsuarioBE.NombreCompleto = (string)oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_ESTADO"))) objUsuarioBE.Estado = (bool)oRead.GetValue(oRead.GetOrdinal("USU_ESTADO"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            objUsuarioBE.Perfil = objPerfilBE;

                            return objUsuarioBE;
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

        public List<UsuarioBE> ListarUsuarios()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_MAE_SP_LISTARUSUARIOS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            List<UsuarioBE> lstConsultaBE = new List<UsuarioBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    UsuarioBE objConsultaBE = new UsuarioBE();
                                    PerfilBE objPerfilBE = new PerfilBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["USU_IDUSUARIO"] != DBNull.Value) objConsultaBE.IdUsuario = Convert.ToInt32(objDRConsulta["USU_IDUSUARIO"]);
                                    if (objDRConsulta["USU_ESTADO"] != DBNull.Value) objConsultaBE.Estado = Convert.ToBoolean(objDRConsulta["USU_ESTADO"]);
                                    if (objDRConsulta["USU_NOMBRECOMPLETO"] != DBNull.Value) objConsultaBE.NombreCompleto = Convert.ToString(objDRConsulta["USU_NOMBRECOMPLETO"]);
                                    if (objDRConsulta["PER_IDPERFIL"] != DBNull.Value) objPerfilBE.IdPerfil = Convert.ToInt32(objDRConsulta["PER_IDPERFIL"]);                                    
                                    if (objDRConsulta["PER_DENOMINACION"] != DBNull.Value) objPerfilBE.Denominacion = Convert.ToString(objDRConsulta["PER_DENOMINACION"]);
                                    if (objDRConsulta["USU_STRESTADO"] != DBNull.Value) objConsultaBE.strEstado = Convert.ToString(objDRConsulta["USU_STRESTADO"]);

                                    objConsultaBE.Perfil = objPerfilBE;

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
                            return new List<UsuarioBE>();
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

        public bool GuardarUsuario(UsuarioBE objUsuario, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_MAE_SP_GUARDARUSUARIO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, objUsuario.IdUsuario, !(objUsuario.IdUsuario > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@PER_IDPERFIL", SqlDbType.Int, ParameterDirection.Input, objUsuario.Perfil.IdPerfil, !(objUsuario.Perfil.IdPerfil > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_EMAIL", SqlDbType.VarChar, ParameterDirection.Input, objUsuario.Usuario, string.IsNullOrEmpty(objUsuario.Usuario)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_CONTRASENIA", SqlDbType.VarChar, ParameterDirection.Input, objUsuario.Contrasenia, string.IsNullOrEmpty(objUsuario.Contrasenia)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_NOMBRECOMPLETO", SqlDbType.VarChar, ParameterDirection.Input, objUsuario.NombreCompleto, string.IsNullOrEmpty(objUsuario.NombreCompleto)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objUsuario.Auditoria.Usuario, string.IsNullOrEmpty(objUsuario.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objUsuario.IdUsuario = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vUSU_IDUSUARIO_OUT"]));
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

        public bool EliminarUsuario(int IdUsuario, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_MAE_SP_ELIMINARUSUARIO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, IdUsuario, !(IdUsuario > 0)));

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
