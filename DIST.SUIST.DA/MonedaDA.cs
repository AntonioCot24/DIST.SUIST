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
    public class MonedaDA
    {
        public List<MonedaBE> ListarMonedas()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_MAE_SP_LISTARMONEDA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            MonedaBE objConsultaBE;
                            List<MonedaBE> lstConsultaBE = new List<MonedaBE>();
                            using (SqlDataReader objDRConsulta = cmd.ExecuteReader())
                            {
                                while (objDRConsulta.Read())
                                {
                                    objConsultaBE = new MonedaBE();
                                    AuditoriaBE objAuditoriaBE = new AuditoriaBE();

                                    if (objDRConsulta["MOD_IDMONEDA"] != DBNull.Value) objConsultaBE.IdMoneda = Convert.ToInt32(objDRConsulta["MOD_IDMONEDA"]);
                                    if (objDRConsulta["MOD_DESCRIPCION"] != DBNull.Value) objConsultaBE.Descripcion = Convert.ToString(objDRConsulta["MOD_DESCRIPCION"]);
                                    if (objDRConsulta["MOD_SIGNO"] != DBNull.Value) objConsultaBE.Signo = Convert.ToString(objDRConsulta["MOD_SIGNO"]);

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
                            return new List<MonedaBE>();
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
        
        public MonedaBE ObtenerMoneda(int IdMoneda)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_MAE_SP_OBTENERMONEDA";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(UtilDA.SetParameter("@MOD_IDMONEDA", SqlDbType.Int, ParameterDirection.Input, IdMoneda, !(IdMoneda > 0)));

                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            MonedaBE objMonedaBE = new MonedaBE();
                            ClienteBE objClienteBE = new ClienteBE();
                            ContactoBE objContactoBE = new ContactoBE();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_IDMONEDA"))) objMonedaBE.IdMoneda = (int)oRead.GetValue(oRead.GetOrdinal("MOD_IDMONEDA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_DESCRIPCION"))) objMonedaBE.Descripcion = (string)oRead.GetValue(oRead.GetOrdinal("MOD_DESCRIPCION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_SIGNO"))) objMonedaBE.Signo = (string)oRead.GetValue(oRead.GetOrdinal("MOD_SIGNO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_TIPOCAMBIO"))) objMonedaBE.TipoCambio = (bool)oRead.GetValue(oRead.GetOrdinal("MOD_TIPOCAMBIO"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            return objMonedaBE;
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

        public MonedaBE ObtenerMonedaPredeterminada()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SUIT_MAE_SP_OBTENERMONEDAPREDETERMINADA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (ConexionDA NewDA_CONEXION = new ConexionDA())
                    {
                        try
                        {
                            NewDA_CONEXION.retOpen();
                            cmd.Connection = NewDA_CONEXION.retConexion();
                            MonedaBE objMonedaBE = new MonedaBE();
                            ClienteBE objClienteBE = new ClienteBE();
                            ContactoBE objContactoBE = new ContactoBE();
                            using (SqlDataReader oRead = cmd.ExecuteReader())
                            {
                                while (oRead.Read())
                                {
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_IDMONEDA"))) objMonedaBE.IdMoneda = (int)oRead.GetValue(oRead.GetOrdinal("MOD_IDMONEDA"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_DESCRIPCION"))) objMonedaBE.Descripcion = (string)oRead.GetValue(oRead.GetOrdinal("MOD_DESCRIPCION"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_SIGNO"))) objMonedaBE.Signo = (string)oRead.GetValue(oRead.GetOrdinal("MOD_SIGNO"));
                                    if (DBNull.Value != oRead.GetValue(oRead.GetOrdinal("MOD_PREDETERMINADA"))) objMonedaBE.Predeteminado = (bool)oRead.GetValue(oRead.GetOrdinal("MOD_PREDETERMINADA"));
                                }
                                oRead.Close();
                            }
                            NewDA_CONEXION.retClose();

                            return objMonedaBE;
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
    }
}
