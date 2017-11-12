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
    public class TipoActividadDA
    {
        public TipoActividadBE ObtenerTipoActividad(int IdTipoActividad)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_OBTENERTIPOACTIVIDAD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, IdTipoActividad, !(IdTipoActividad > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            TipoActividadBE objTipoActividadBE = new TipoActividadBE();

                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("TAC_IDTIPOACTIVIDAD"))) objTipoActividadBE.IdTipoActividad = (int)oRead.GetValue(oRead.GetOrdinal("TAC_IDTIPOACTIVIDAD"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("TAC_NOMBRE"))) objTipoActividadBE.Nombre = (string)oRead.GetValue(oRead.GetOrdinal("TAC_NOMBRE"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();
                            
                            return objTipoActividadBE;
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

        public List<TipoActividadBE> ListarTipoActividades()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARTIPOACTIVIDADES";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            TipoActividadBE objConsultaBE = new TipoActividadBE();
                            List<TipoActividadBE> lstConsultaBE = new List<TipoActividadBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new TipoActividadBE();
                                    PrecioBE objPrecioBE = new PrecioBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["TAC_IDTIPOACTIVIDAD"] != DBNull.Value) objConsultaBE.IdTipoActividad = Convert.ToInt32(objDRConsulta["TAC_IDTIPOACTIVIDAD"]);
                                    if (objDRConsulta["TAC_NOMBRE"] != DBNull.Value) objConsultaBE.Nombre = Convert.ToString(objDRConsulta["TAC_NOMBRE"]);

                                    objConsultaBE.Precio = objPrecioBE;

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
                            return new List<TipoActividadBE>();
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

        public bool GuardarTipoActividad(TipoActividadBE objTipoActividad, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDARTIPOACTIVIDAD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, objTipoActividad.IdTipoActividad, !(objTipoActividad.IdTipoActividad > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_NOMBRE", SqlDbType.VarChar, ParameterDirection.Input, objTipoActividad.Nombre, string.IsNullOrEmpty(objTipoActividad.Nombre)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objTipoActividad.Auditoria.Usuario, string.IsNullOrEmpty(objTipoActividad.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objTipoActividad.IdTipoActividad = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vTAC_IDTIPOACTIVIDAD_OUT"]));
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

        public bool EliminarTipoActividad(int IdTipoActividad, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_ELIMINARTIPOACTIVIDAD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, IdTipoActividad, !(IdTipoActividad > 0)));

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
