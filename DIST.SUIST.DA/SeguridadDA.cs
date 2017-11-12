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
    public class SeguridadDA
    {
        public UsuarioBE ValidarUsuario(UsuarioBE objUsuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {                    
                    cmd.CommandText = "SUIT_SEG_SP_VALIDARUSUARIO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_EMAIL", SqlDbType.VarChar, ParameterDirection.Input, objUsuario.Usuario, string.IsNullOrEmpty(objUsuario.Usuario)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_CONTRASENIA", SqlDbType.VarChar, ParameterDirection.Input, objUsuario.Contrasenia, string.IsNullOrEmpty(objUsuario.Contrasenia)));
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
                                if (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"))) objUsuarioBE.IdUsuario = (int)oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_EMAIL"))) objUsuarioBE.Usuario = (string)oRead.GetValue(oRead.GetOrdinal("USU_EMAIL"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_CONTRASENIA"))) objUsuarioBE.Contrasenia = (string)oRead.GetValue(oRead.GetOrdinal("USU_CONTRASENIA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"))) objUsuarioBE.NombreCompleto = (string)oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_MASTERADMIN"))) objUsuarioBE.MasterAdmin = (bool)oRead.GetValue(oRead.GetOrdinal("USU_MASTERADMIN"));                                    
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PER_IDPERFIL"))) objPerfilBE.IdPerfil = (int)oRead.GetValue(oRead.GetOrdinal("PER_IDPERFIL"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PER_DENOMINACION"))) objPerfilBE.Denominacion = (string)oRead.GetValue(oRead.GetOrdinal("PER_DENOMINACION"));
                                    //if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("EMP_IDEMPRESA"))) objUsuarioBE.IdEmpresa = (int)oRead.GetValue(oRead.GetOrdinal("EMP_IDEMPRESA"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();
                            if (objPerfilBE != null)
                                objUsuarioBE.Perfil = objPerfilBE;

                            return objUsuarioBE;
                        }
                        catch (Exception ex)
                        {
                            return new UsuarioBE();
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
                return new UsuarioBE();
                throw ex;
            }
        }

        public List<OpcionBE> ListarOpcionesUsuarios(int idPerfil)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_SEG_SP_LISTAROPCIONUSUARIO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(UtilDA.SetParameter("@PER_IDPERFIL", SqlDbType.Int, ParameterDirection.Input, idPerfil, !(idPerfil > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            OpcionBE objConsultaBE = new OpcionBE();
                            List<OpcionBE> lstConsultaBE = new List<OpcionBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new OpcionBE();
                                    if (objDRConsulta["OPC_IDOPCION"] != DBNull.Value) objConsultaBE.IdOpcion = Convert.ToInt32(objDRConsulta["OPC_IDOPCION"]);
                                    if (objDRConsulta["OPC_IDOPCIONPADRE"] != DBNull.Value) objConsultaBE.IdOpcionPadre = Convert.ToInt32(objDRConsulta["OPC_IDOPCIONPADRE"]);
                                    if (objDRConsulta["OPC_DENOMINACION"] != DBNull.Value) objConsultaBE.Denominacion = Convert.ToString(objDRConsulta["OPC_DENOMINACION"]).Trim();
                                    if (objDRConsulta["OPC_URLOPCION"] != DBNull.Value) objConsultaBE.UrlOpcion = Convert.ToString(objDRConsulta["OPC_URLOPCION"]).Trim().ToUpper();
                                    if (objDRConsulta["OPC_ICONO"] != DBNull.Value) objConsultaBE.Icono = Convert.ToString(objDRConsulta["OPC_ICONO"]).Trim();

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
                            return new List<OpcionBE>();
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

        public bool ActualizarContraseniaUsuario(UsuarioBE objUsuarioBE, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_SEG_SP_ACTUALIZARCONTRASENIA";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, objUsuarioBE.IdUsuario, !(objUsuarioBE.IdUsuario > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_CONTRASENIA", SqlDbType.VarChar, ParameterDirection.Input, objUsuarioBE.Contrasenia, string.IsNullOrEmpty(objUsuarioBE.Contrasenia)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objUsuarioBE.Auditoria.Usuario, string.IsNullOrEmpty(objUsuarioBE.Auditoria.Usuario)));

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
