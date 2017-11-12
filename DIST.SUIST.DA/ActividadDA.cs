using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.DA
{
    public class ActividadDA
    {
        public ActividadBE ObtenerActividad(int IdActividad)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_OBTENERACTIVIDAD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_IDACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, IdActividad, !(IdActividad > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ActividadBE objActividadBE = new ActividadBE();

                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    UsuarioBE objUsuarioBE = new UsuarioBE();
                                    ClienteBE objClienteBE = new ClienteBE();
                                    ProyectoBE objProyectoBE = new ProyectoBE();
                                    TipoActividadBE objTipoActividadBE = new TipoActividadBE();
                                    ContactoBE objContactoBE = new ContactoBE();

                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("ACT_IDACTIVIDAD"))) objActividadBE.IdActividad = (int)oRead.GetValue(oRead.GetOrdinal("ACT_IDACTIVIDAD"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"))) objUsuarioBE.IdUsuario = (int)oRead.GetValue(oRead.GetOrdinal("USU_IDUSUARIO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"))) objUsuarioBE.NombreCompleto = (string)oRead.GetValue(oRead.GetOrdinal("USU_NOMBRECOMPLETO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"))) objClienteBE.IdCliente = (int)oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_COLOR"))) objClienteBE.Color = (string)oRead.GetValue(oRead.GetOrdinal("CLI_COLOR"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRY_IDPROYECTO"))) objProyectoBE.IdProyecto = (int)oRead.GetValue(oRead.GetOrdinal("PRY_IDPROYECTO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("TAC_IDTIPOACTIVIDAD"))) objTipoActividadBE.IdTipoActividad = (int)oRead.GetValue(oRead.GetOrdinal("TAC_IDTIPOACTIVIDAD"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_IDCONTACTO"))) objContactoBE.IdContacto = (int)oRead.GetValue(oRead.GetOrdinal("CON_IDCONTACTO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("ACT_GLOSA"))) objActividadBE.Glosa = (string)oRead.GetValue(oRead.GetOrdinal("ACT_GLOSA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("ACT_FECHAINCIO"))) objActividadBE.FechaInicio = (DateTime)oRead.GetValue(oRead.GetOrdinal("ACT_FECHAINCIO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("ACT_FECHAFIN"))) objActividadBE.FechaFin = (DateTime)oRead.GetValue(oRead.GetOrdinal("ACT_FECHAFIN"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("ACT_TOTALHORAS"))) objActividadBE.TotalHoras = (int)oRead.GetValue(oRead.GetOrdinal("ACT_TOTALHORAS"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("ACT_TOTALMINUTOS"))) objActividadBE.TotalMinutos = (int)oRead.GetValue(oRead.GetOrdinal("ACT_TOTALMINUTOS"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("ACT_FACTURABLE"))) objActividadBE.Facturable = (bool)oRead.GetValue(oRead.GetOrdinal("ACT_FACTURABLE"));

                                    objActividadBE.Usuario = objUsuarioBE;
                                    objActividadBE.Cliente = objClienteBE;
                                    objActividadBE.Proyecto = objProyectoBE;
                                    objActividadBE.TipoActividad = objTipoActividadBE;
                                    objActividadBE.Contacto = objContactoBE;
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            return objActividadBE;
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

        public List<ActividadBE> ListarActividades(int IdUsuario)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_LISTARACTIVIDADES";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, IdUsuario, !(IdUsuario > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ActividadBE objConsultaBE = new ActividadBE();
                            List<ActividadBE> lstConsultaBE = new List<ActividadBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new ActividadBE();
                                    ClienteBE objClienteBE = new ClienteBE();
                                    TipoActividadBE objTipoActividadBE = new TipoActividadBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["ACT_IDACTIVIDAD"] != DBNull.Value) objConsultaBE.IdActividad = Convert.ToInt32(objDRConsulta["ACT_IDACTIVIDAD"]);
                                    if (objDRConsulta["CLI_NOMBRES"] != DBNull.Value) objClienteBE.NombreCompleto = Convert.ToString(objDRConsulta["CLI_NOMBRES"]);
                                    if (objDRConsulta["CLI_COLOR"] != DBNull.Value) objClienteBE.Color = Convert.ToString(objDRConsulta["CLI_COLOR"]);
                                    if (objDRConsulta["TAC_NOMBRE"] != DBNull.Value) objTipoActividadBE.Nombre = Convert.ToString(objDRConsulta["TAC_NOMBRE"]);
                                    if (objDRConsulta["ACT_FECHAINCIO"] != DBNull.Value) objConsultaBE.FechaInicio = Convert.ToDateTime(objDRConsulta["ACT_FECHAINCIO"]);
                                    if (objDRConsulta["ACT_FECHAFIN"] != DBNull.Value) objConsultaBE.FechaFin = Convert.ToDateTime(objDRConsulta["ACT_FECHAFIN"]);
                                    if (objDRConsulta["ACT_GLOSA"] != DBNull.Value) objConsultaBE.Glosa = Convert.ToString(objDRConsulta["ACT_GLOSA"]);

                                    objConsultaBE.Cliente = objClienteBE;
                                    objConsultaBE.TipoActividad = objTipoActividadBE;

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
                            return new List<ActividadBE>();
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

        public List<ActividadBE> ListarActividadesFechas(ActividadBE oActividadBE)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_LISTARACTIVIDADESFECHAS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, oActividadBE.Cliente.IdCliente, !(oActividadBE.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_FECHAINCIO", SqlDbType.Date, ParameterDirection.Input, oActividadBE.FechaInicio, (oActividadBE.FechaInicio == null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_FECHAFIN", SqlDbType.Date, ParameterDirection.Input, oActividadBE.FechaFin, (oActividadBE.FechaFin == null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_IDPROYECTO", SqlDbType.Int, ParameterDirection.Input, oActividadBE.IdProyecto, !(oActividadBE.IdProyecto > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ActividadBE objConsultaBE = new ActividadBE();
                            List<ActividadBE> lstConsultaBE = new List<ActividadBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new ActividadBE();
                                    UsuarioBE objUsuarioBE = new UsuarioBE();
                                    ClienteBE objClienteBE = new ClienteBE();
                                    TipoActividadBE objTipoActividadBE = new TipoActividadBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["ACT_IDACTIVIDAD"] != DBNull.Value) objConsultaBE.IdActividad = Convert.ToInt32(objDRConsulta["ACT_IDACTIVIDAD"]);
                                    if (objDRConsulta["USU_NOMBRECOMPLETO"] != DBNull.Value) objUsuarioBE.NombreCompleto = Convert.ToString(objDRConsulta["USU_NOMBRECOMPLETO"]);
                                    if (objDRConsulta["CLI_NOMBRES"] != DBNull.Value) objClienteBE.NombreCompleto = Convert.ToString(objDRConsulta["CLI_NOMBRES"]);
                                    if (objDRConsulta["TAC_NOMBRE"] != DBNull.Value) objTipoActividadBE.Nombre = Convert.ToString(objDRConsulta["TAC_NOMBRE"]);
                                    if (objDRConsulta["ACT_FECHAINCIO"] != DBNull.Value) objConsultaBE.FechaInicio = Convert.ToDateTime(objDRConsulta["ACT_FECHAINCIO"]);
                                    if (objDRConsulta["ACT_FECHAFIN"] != DBNull.Value) objConsultaBE.FechaFin = Convert.ToDateTime(objDRConsulta["ACT_FECHAFIN"]);
                                    if (objDRConsulta["ACT_GLOSA"] != DBNull.Value) objConsultaBE.Glosa = Convert.ToString(objDRConsulta["ACT_GLOSA"]);
                                    if (objDRConsulta["ACT_TOTALHORAS"] != DBNull.Value) objConsultaBE.TotalHoras = Convert.ToInt32(objDRConsulta["ACT_TOTALHORAS"]);
                                    if (objDRConsulta["ACT_TOTALMINUTOS"] != DBNull.Value) objConsultaBE.TotalMinutos = Convert.ToInt32(objDRConsulta["ACT_TOTALMINUTOS"]);

                                    objConsultaBE.Usuario = objUsuarioBE;
                                    objConsultaBE.Cliente = objClienteBE;
                                    objConsultaBE.TipoActividad = objTipoActividadBE;

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
                            return new List<ActividadBE>();
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

        public bool GuardarActividad(ActividadBE objActividad, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_GUARDARACTIVIDAD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_IDACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, objActividad.IdActividad, !(objActividad.IdActividad > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, objActividad.Usuario.IdUsuario, !(objActividad.Usuario.IdUsuario > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objActividad.Cliente.IdCliente, !(objActividad.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_IDPROYECTO", SqlDbType.Int, ParameterDirection.Input, objActividad.Proyecto.IdProyecto, !(objActividad.Proyecto.IdProyecto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, objActividad.TipoActividad.IdTipoActividad, !(objActividad.TipoActividad.IdTipoActividad > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDCONTACTO", SqlDbType.Int, ParameterDirection.Input, objActividad.Contacto.IdContacto, !(objActividad.Contacto.IdContacto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_GLOSA", SqlDbType.VarChar, ParameterDirection.Input, objActividad.Glosa, string.IsNullOrEmpty(objActividad.Glosa)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_FECHAINCIO", SqlDbType.DateTime, ParameterDirection.Input, objActividad.FechaInicio, (objActividad.FechaInicio == null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_FECHAFIN", SqlDbType.DateTime, ParameterDirection.Input, objActividad.FechaFin, (objActividad.FechaFin == null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_TOTALHORAS", SqlDbType.Int, ParameterDirection.Input, objActividad.TotalHoras, !(objActividad.TotalHoras > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_TOTALMINUTOS", SqlDbType.Int, ParameterDirection.Input, objActividad.TotalMinutos, !(objActividad.TotalMinutos > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_FACTURABLE", SqlDbType.Bit, ParameterDirection.Input, objActividad.Facturable, false));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objActividad.Auditoria.Usuario, string.IsNullOrEmpty(objActividad.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_IDACTIVIDAD_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objActividad.IdActividad = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vACT_IDACTIVIDAD_OUT"]));
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

        public bool EliminarActividad(int IdActividad, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_COT_SP_ELIMINARACTIVIDAD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_IDACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, IdActividad, !(IdActividad > 0)));

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
