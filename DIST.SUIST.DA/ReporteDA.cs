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
    public class ReporteDA
    {
        public List<ActividadBE> ReporteProductividad(ActividadBE objActividadBE)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_REP_SP_REPORTEPRODUCTIVIDAD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_IDUSUARIO", SqlDbType.Int, ParameterDirection.Input, objActividadBE.Usuario.IdUsuario, !(objActividadBE.Usuario.IdUsuario > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objActividadBE.Cliente.IdCliente, !(objActividadBE.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@TAC_IDTIPOACTIVIDAD", SqlDbType.Int, ParameterDirection.Input, objActividadBE.TipoActividad.IdTipoActividad, !(objActividadBE.TipoActividad.IdTipoActividad > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_FECHAINICO", SqlDbType.Date, ParameterDirection.Input, objActividadBE.FechaInicio, !(objActividadBE.FechaInicio != null)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@ACT_FECHAFIN", SqlDbType.Date, ParameterDirection.Input, objActividadBE.FechaFin, !(objActividadBE.FechaFin != null)));
                   
                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            
                            List<ActividadBE> lstActividadBE = new List<ActividadBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    ActividadBE objConsultaBE = new ActividadBE();
                                    UsuarioBE objUsuarioBE = new UsuarioBE();
                                    ClienteBE objClienteBE = new ClienteBE();
                                    ProyectoBE objProyectoBE = new ProyectoBE();
                                    TipoActividadBE objTipoActividadBE = new TipoActividadBE();

                                    if (objDRConsulta["USU_NOMBRECOMPLETO"] != DBNull.Value) objUsuarioBE.NombreCompleto = Convert.ToString(objDRConsulta["USU_NOMBRECOMPLETO"]);
                                    if (objDRConsulta["CLI_NOMBRES"] != DBNull.Value) objClienteBE.NombreCompleto = Convert.ToString(objDRConsulta["CLI_NOMBRES"]);
                                    if (objDRConsulta["PRY_NOMBRE"] != DBNull.Value) objProyectoBE.NombreProyecto = Convert.ToString(objDRConsulta["PRY_NOMBRE"]);
                                    if (objDRConsulta["TAC_NOMBRE"] != DBNull.Value) objTipoActividadBE.Nombre = Convert.ToString(objDRConsulta["TAC_NOMBRE"]);
                                    if (objDRConsulta["ACT_FECHAINCIO"] != DBNull.Value) objConsultaBE.FechaInicio = Convert.ToDateTime(objDRConsulta["ACT_FECHAINCIO"]);
                                    if (objDRConsulta["ACT_FECHAFIN"] != DBNull.Value) objConsultaBE.FechaFin = Convert.ToDateTime(objDRConsulta["ACT_FECHAFIN"]);

                                    objConsultaBE.Usuario = objUsuarioBE;
                                    objConsultaBE.Cliente = objClienteBE;
                                    objConsultaBE.Proyecto = objProyectoBE;
                                    objConsultaBE.TipoActividad = objTipoActividadBE;

                                    lstActividadBE.Add(objConsultaBE);
                                }
                                objDRConsulta.Close();
                            }
                            NewDA_CONEXION.retClose();
                            return lstActividadBE;
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
    }
}
