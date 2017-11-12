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
    public class ProyectoDA
    {
        public ProyectoBE ObtenerProyecto(int IdProyecto)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_OBTENERPROYECTOS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_IDPROYECTO", SqlDbType.Int, ParameterDirection.Input, IdProyecto, !(IdProyecto > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ProyectoBE objProyectoBE = new ProyectoBE();
                            ClienteBE objClienteBE = new ClienteBE();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRY_IDPROYECTO"))) objProyectoBE.IdProyecto = (int)oRead.GetValue(oRead.GetOrdinal("PRY_IDPROYECTO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"))) objClienteBE.IdCliente = (int)oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRY_NOMBRE"))) objProyectoBE.NombreProyecto = (string)oRead.GetValue(oRead.GetOrdinal("PRY_NOMBRE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRY_PRECIO"))) objProyectoBE.Precio = (double)oRead.GetValue(oRead.GetOrdinal("PRY_PRECIO"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            objProyectoBE.Cliente = objClienteBE;

                            return objProyectoBE;
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

        public List<ProyectoBE> ListarProyectos()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARPROYECTOS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ProyectoBE objConsultaBE;
                            ClienteBE objClienteBE;
                            List<ProyectoBE> lstConsultaBE = new List<ProyectoBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new ProyectoBE();
                                    objClienteBE = new ClienteBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["PRY_IDPROYECTO"] != DBNull.Value) objConsultaBE.IdProyecto = Convert.ToInt32(objDRConsulta["PRY_IDPROYECTO"]);
                                    if (objDRConsulta["CLI_NOMBRES"] != DBNull.Value) objClienteBE.NombreCompleto = Convert.ToString(objDRConsulta["CLI_NOMBRES"]);
                                    if (objDRConsulta["PRY_NOMBRE"] != DBNull.Value) objConsultaBE.NombreProyecto = Convert.ToString(objDRConsulta["PRY_NOMBRE"]);
                                    if (objDRConsulta["PRY_PRECIO"] != DBNull.Value) objConsultaBE.Precio = Convert.ToDouble(objDRConsulta["PRY_PRECIO"]);

                                    objConsultaBE.Cliente = objClienteBE;

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
                            return new List<ProyectoBE>();
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

        public List<ProyectoBE> ListarProyectosCliente(int IdCliente)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARPROYECTOSCLIENTE";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, IdCliente, !(IdCliente > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ProyectoBE objConsultaBE;
                            List<ProyectoBE> lstConsultaBE = new List<ProyectoBE>();

                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new ProyectoBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["PRY_IDPROYECTO"] != DBNull.Value) objConsultaBE.IdProyecto = Convert.ToInt32(objDRConsulta["PRY_IDPROYECTO"]);
                                    if (objDRConsulta["PRY_NOMBRE"] != DBNull.Value) objConsultaBE.NombreProyecto = Convert.ToString(objDRConsulta["PRY_NOMBRE"]);
                                    if (objDRConsulta["PRY_PRECIO"] != DBNull.Value) objConsultaBE.Precio = Convert.ToDouble(objDRConsulta["PRY_PRECIO"]);
                                    
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
                            return new List<ProyectoBE>();
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

        public bool GuardarProyecto(ProyectoBE objProyecto, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDARPROYECTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_IDPROYECTO", SqlDbType.Int, ParameterDirection.Input, objProyecto.IdProyecto, !(objProyecto.IdProyecto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objProyecto.Cliente.IdCliente, !(objProyecto.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_NOMBRE", SqlDbType.VarChar, ParameterDirection.Input, objProyecto.NombreProyecto, string.IsNullOrEmpty(objProyecto.NombreProyecto)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_PRECIO", SqlDbType.Double, ParameterDirection.Input, objProyecto.Precio, !(objProyecto.Precio > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objProyecto.Auditoria.Usuario, string.IsNullOrEmpty(objProyecto.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_IDPROYECTO_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objProyecto.IdProyecto = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vPRY_IDPROYECTO_OUT"]));
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

        public bool EliminarProyecto(int IdProyecto, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_ELIMINARPROYECTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@PRY_IDPROYECTO", SqlDbType.Int, ParameterDirection.Input, IdProyecto, !(IdProyecto > 0)));
                    
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
