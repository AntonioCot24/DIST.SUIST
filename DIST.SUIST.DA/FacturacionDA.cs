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
    public class FacturacionDA
    {
        public FacturacionBE ObtenerFacturacion(int IdFacturacion, int IdCliente)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_OBTENERFACTURACION";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_IDFACTURACION", SqlDbType.Int, ParameterDirection.Input, IdFacturacion, !(IdFacturacion > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, IdCliente, !(IdCliente > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            FacturacionBE objFacturacionBE = new FacturacionBE();
                            ClienteBE objClienteBE = new ClienteBE();
                            ContactoBE objContactoBE = new ContactoBE();

                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_IDFACTURACION"))) objFacturacionBE.IdFacturacion = (int)oRead.GetValue(oRead.GetOrdinal("FAC_IDFACTURACION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"))) objClienteBE.IdCliente = (int)oRead.GetValue(oRead.GetOrdinal("CLI_IDCLIENTE"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("CON_IDCONTACTO"))) objContactoBE.IdContacto = (int)oRead.GetValue(oRead.GetOrdinal("CON_IDCONTACTO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_TIPOFACTURACION"))) objFacturacionBE.TipoFacturacion = (int)oRead.GetValue(oRead.GetOrdinal("FAC_TIPOFACTURACION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_TARIFAFIJA"))) objFacturacionBE.TarifaFija = (double)oRead.GetValue(oRead.GetOrdinal("FAC_TARIFAFIJA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_TARIFAHORAS"))) objFacturacionBE.TarifaHoras = (double)oRead.GetValue(oRead.GetOrdinal("FAC_TARIFAHORAS"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_TARIFAHORASADICIONAL"))) objFacturacionBE.TarifaHorasAdicionales = (double)oRead.GetValue(oRead.GetOrdinal("FAC_TARIFAHORASADICIONAL"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_MONTOFLAT"))) objFacturacionBE.MontoFlat = (double)oRead.GetValue(oRead.GetOrdinal("FAC_MONTOFLAT"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_FECHAFACTURA"))) objFacturacionBE.FechaFactura = (int)oRead.GetValue(oRead.GetOrdinal("FAC_FECHAFACTURA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("FAC_DIRECCION"))) objFacturacionBE.Direccion = (string)oRead.GetValue(oRead.GetOrdinal("FAC_DIRECCION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("PRY_PRECIO"))) objFacturacionBE.PrecioProyecto = (double)oRead.GetValue(oRead.GetOrdinal("PRY_PRECIO"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            objFacturacionBE.Cliente = objClienteBE;
                            objFacturacionBE.Contacto = objContactoBE;

                            return objFacturacionBE;
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

        public List<MonedaFacturacionBE> ListarMonedaFacturacion(int IdFactura)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARMONEDAFACTURACION";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_IDFACTURACION", SqlDbType.Int, ParameterDirection.Input, IdFactura, !(IdFactura > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            MonedaFacturacionBE objConsultaBE;
                            List<MonedaFacturacionBE> lstConsultaBE = new List<MonedaFacturacionBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new MonedaFacturacionBE();
                                    MonedaBE objMonedaBE = new MonedaBE();
                                    
                                    if (objDRConsulta["MOD_IDMONEDAFACTURA"] != DBNull.Value) objConsultaBE.IdMonedaFacturacion = Convert.ToInt32(objDRConsulta["MOD_IDMONEDAFACTURA"]);
                                    if (objDRConsulta["FAC_IDFACTURACION"] != DBNull.Value) objConsultaBE.IdFacturacion = Convert.ToInt32(objDRConsulta["FAC_IDFACTURACION"]);
                                    if (objDRConsulta["MOD_IDMONEDA"] != DBNull.Value) objConsultaBE.IdMoneda = Convert.ToInt32(objDRConsulta["MOD_IDMONEDA"]);
                                    if (objDRConsulta["MOD_DESCRIPCION"] != DBNull.Value) objMonedaBE.Descripcion = Convert.ToString(objDRConsulta["MOD_DESCRIPCION"]);
                                    if (objDRConsulta["MOD_SIGNO"] != DBNull.Value) objMonedaBE.Signo = Convert.ToString(objDRConsulta["MOD_SIGNO"]);
                                    if (objDRConsulta["MOD_TIPOCAMBIO"] != DBNull.Value) objMonedaBE.TipoCambio = Convert.ToBoolean(objDRConsulta["MOD_TIPOCAMBIO"]);
                                    if (objDRConsulta["MOD_PREDETERMINADA"] != DBNull.Value) objMonedaBE.Predeteminado = Convert.ToBoolean(objDRConsulta["MOD_PREDETERMINADA"]);

                                    objConsultaBE.Moneda = objMonedaBE;

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
                            return new List<MonedaFacturacionBE>();
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

        public List<FacturacionBE> ListarFacturaciones(int IdCliente)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_LISTARFACTURACIONES";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, IdCliente, !(IdCliente > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            FacturacionBE objConsultaBE;
                            List<FacturacionBE> lstConsultaBE = new List<FacturacionBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new FacturacionBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["FAC_IDFACTURACION"] != DBNull.Value) objConsultaBE.IdFacturacion = Convert.ToInt32(objDRConsulta["FAC_IDFACTURACION"]);
                                    if (objDRConsulta["FAC_TIPOFACTURACION"] != DBNull.Value) objConsultaBE.strTipoFacturacion = Convert.ToString(objDRConsulta["FAC_TIPOFACTURACION"]);
                                    if (objDRConsulta["FAC_DIRECCION"] != DBNull.Value) objConsultaBE.Direccion = Convert.ToString(objDRConsulta["FAC_DIRECCION"]);
                                    //if (objDRConsulta["FAC_CONTACTO"] != DBNull.Value) objConsultaBE.Contacto = Convert.ToString(objDRConsulta["FAC_CONTACTO"]);

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
                            return new List<FacturacionBE>();
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

        public bool GuardarFacturacion(FacturacionBE objFacturacion, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDARFACTURACION";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_IDFACTURACION", SqlDbType.Int, ParameterDirection.Input, objFacturacion.IdFacturacion, !(objFacturacion.IdFacturacion > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CLI_IDCLIENTE", SqlDbType.Int, ParameterDirection.Input, objFacturacion.Cliente.IdCliente, !(objFacturacion.Cliente.IdCliente > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@CON_IDCONTACTO", SqlDbType.Int, ParameterDirection.Input, objFacturacion.Contacto.IdContacto, !(objFacturacion.Contacto.IdContacto > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_TIPOFACTURACION", SqlDbType.Int, ParameterDirection.Input, objFacturacion.TipoFacturacion, !(objFacturacion.TipoFacturacion > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_TARIFAFIJA", SqlDbType.Float, ParameterDirection.Input, objFacturacion.TarifaFija, !(objFacturacion.TarifaFija > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_TARIFAHORAS", SqlDbType.Float, ParameterDirection.Input, objFacturacion.TarifaHoras, !(objFacturacion.TarifaHoras > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_TARIFAHORASADICIONAL", SqlDbType.Float, ParameterDirection.Input, objFacturacion.TarifaHorasAdicionales, !(objFacturacion.TarifaHorasAdicionales > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_MONTOFLAT", SqlDbType.Float, ParameterDirection.Input, objFacturacion.MontoFlat, !(objFacturacion.MontoFlat > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_FECHAFACTURA", SqlDbType.Int, ParameterDirection.Input, objFacturacion.FechaFactura, !(objFacturacion.FechaFactura > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_DIRECCION", SqlDbType.VarChar, ParameterDirection.Input, objFacturacion.Direccion, string.IsNullOrEmpty(objFacturacion.Direccion)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objFacturacion.Auditoria.Usuario, string.IsNullOrEmpty(objFacturacion.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_IDFACTURACION_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objFacturacion.IdFacturacion = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vFAC_IDFACTURACION_OUT"]));
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

        public bool GuardarMonedaFacturacion(MonedaFacturacionBE objMonedaFacturacion, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_GUARDARMONEDAFACTURACION";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@MOD_IDMONEDAFACTURA", SqlDbType.Int, ParameterDirection.Input, objMonedaFacturacion.IdMonedaFacturacion, !(objMonedaFacturacion.IdMonedaFacturacion > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_IDFACTURACION", SqlDbType.Int, ParameterDirection.Input, objMonedaFacturacion.IdFacturacion, !(objMonedaFacturacion.IdFacturacion > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@MOD_IDMONEDA", SqlDbType.Int, ParameterDirection.Input, objMonedaFacturacion.IdMoneda, !(objMonedaFacturacion.IdMoneda > 0)));
                    cmd.Parameters.Add(UtilDA.SetParameter("@USU_USUARIO", SqlDbType.VarChar, ParameterDirection.Input, objMonedaFacturacion.Auditoria.Usuario, string.IsNullOrEmpty(objMonedaFacturacion.Auditoria.Usuario)));

                    //Paremetros de salida
                    cmd.Parameters.Add(UtilDA.SetParameter("@MOD_IDMONEDAFACTURA_OUT", SqlDbType.Int, ParameterDirection.Output, null));
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
                                objMonedaFacturacion.IdMonedaFacturacion = Convert.ToInt32(UtilDA.ParseParameter(cmd.Parameters["vMOD_IDMONEDAFACTURA_OUT"]));
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

        public bool EliminarFacturacion(int IdFacturacion, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_ELIMINARFACTURACION";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@FAC_IDFACTURACION", SqlDbType.Int, ParameterDirection.Input, IdFacturacion, !(IdFacturacion > 0)));

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

        public bool EliminarMonedaFacturacion(int IdMonedaFacturacion, out string mensaje)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_ADM_SP_ELIMINARMONEDAFACTURACION";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Paramtros de Entrada
                    cmd.Parameters.Add(UtilDA.SetParameter("@MOD_IDMONEDAFACTURA", SqlDbType.Int, ParameterDirection.Input, IdMonedaFacturacion, !(IdMonedaFacturacion > 0)));
                    
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
