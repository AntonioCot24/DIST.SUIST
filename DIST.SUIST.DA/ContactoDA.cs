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
    public class ContactoDA
    {
        public ContactoBE ObtenerContacto(int IdContacto)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_OBTENERCONTACTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDCONTACTO", SqlDbType.Int, ParameterDirection.Input, IdContacto, !(IdContacto > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ContactoBE objContactoBE = new ContactoBE();
                            ClienteBE objClienteBE = new ClienteBE();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_IDCONTACTO"))) objContactoBE.IdContacto = (int)oRead.GetValue(oRead.GetOrdinal("CON_IDCONTACTO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"))) objClienteBE.IdCliente = (int)oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_NOMBRE"))) objContactoBE.Nombre = (string)oRead.GetValue(oRead.GetOrdinal("CON_NOMBRE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_DIRECCION"))) objContactoBE.Direccion = (string)oRead.GetValue(oRead.GetOrdinal("CON_DIRECCION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_TELEFONOFIJO"))) objContactoBE.TelefonoFijo = (string)oRead.GetValue(oRead.GetOrdinal("CON_TELEFONOFIJO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_TELEFONOCELULAR"))) objContactoBE.TelefonoCelular = (string)oRead.GetValue(oRead.GetOrdinal("CON_TELEFONOCELULAR"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_CORREO"))) objContactoBE.Correo = (string)oRead.GetValue(oRead.GetOrdinal("CON_CORREO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_CARGO"))) objContactoBE.Cargo = (string)oRead.GetValue(oRead.GetOrdinal("CON_CARGO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_PRINCIPAL"))) objContactoBE.Principal = (bool)oRead.GetValue(oRead.GetOrdinal("CON_PRINCIPAL"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            objContactoBE.Cliente = objClienteBE;

                            return objContactoBE;
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

        public List<ContactoBE> ListarContactos(int IdCliente)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARCONTACTOS";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, IdCliente, !(IdCliente > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            ContactoBE objConsultaBE;
                            List<ContactoBE> lstConsultaBE = new List<ContactoBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new ContactoBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["CON_IDCONTACTO"] != DBNull.Value) objConsultaBE.IdContacto = Convert.ToInt32(objDRConsulta["CON_IDCONTACTO"]);
                                    if (objDRConsulta["CON_NOMBRE"] != DBNull.Value) objConsultaBE.Nombre = Convert.ToString(objDRConsulta["CON_NOMBRE"]);
                                    if (objDRConsulta["CON_DIRECCION"] != DBNull.Value) objConsultaBE.Direccion = Convert.ToString(objDRConsulta["CON_DIRECCION"]);
                                    if (objDRConsulta["CON_TELEFONOFIJO"] != DBNull.Value) objConsultaBE.TelefonoFijo = Convert.ToString(objDRConsulta["CON_TELEFONOFIJO"]);
                                    if (objDRConsulta["CON_TELEFONOCELULAR"] != DBNull.Value) objConsultaBE.TelefonoCelular = Convert.ToString(objDRConsulta["CON_TELEFONOCELULAR"]);
                                    if (objDRConsulta["CON_CORREO"] != DBNull.Value) objConsultaBE.Correo = Convert.ToString(objDRConsulta["CON_CORREO"]);
                                    if (objDRConsulta["CON_CARGO"] != DBNull.Value) objConsultaBE.Cargo = Convert.ToString(objDRConsulta["CON_CARGO"]);
                                    if (objDRConsulta["CON_PRINCIPAL"] != DBNull.Value) objConsultaBE.Principal = Convert.ToBoolean(objDRConsulta["CON_PRINCIPAL"]);

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
                            return new List<ContactoBE>();
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

        public bool GuardarContacto(ContactoBE objContacto, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDARCONTACTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDCONTACTO", SqlDbType.Int, ParameterDirection.Input, objContacto.IdContacto, !(objContacto.IdContacto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objContacto.Cliente.IdCliente, !(objContacto.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_NOMBRE", SqlDbType.VarChar, ParameterDirection.Input, objContacto.Nombre, string.IsNullOrEmpty(objContacto.Nombre)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_DIRECCION", SqlDbType.VarChar, ParameterDirection.Input, objContacto.Direccion, string.IsNullOrEmpty(objContacto.Direccion)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_TELEFONOFIJO", SqlDbType.VarChar, ParameterDirection.Input, objContacto.TelefonoFijo, string.IsNullOrEmpty(objContacto.TelefonoFijo)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_TELEFONOCELULAR", SqlDbType.VarChar, ParameterDirection.Input, objContacto.TelefonoCelular, string.IsNullOrEmpty(objContacto.TelefonoCelular)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_CORREO", SqlDbType.VarChar, ParameterDirection.Input, objContacto.Correo, string.IsNullOrEmpty(objContacto.Correo)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_CARGO", SqlDbType.VarChar, ParameterDirection.Input, objContacto.Cargo, string.IsNullOrEmpty(objContacto.Cargo)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_PRINCIPAL", SqlDbType.Bit, ParameterDirection.Input, objContacto.Principal, false));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objContacto.Auditoria.Usuario, string.IsNullOrEmpty(objContacto.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDCONTACTO_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objContacto.IdContacto = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vCON_IDCONTACTO_OUT"]));
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

        public bool EliminarContacto(int IdContacto, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_ELIMINARCONTACTO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDCONTACTO", SqlDbType.Int, ParameterDirection.Input, IdContacto, !(IdContacto > 0)));

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
