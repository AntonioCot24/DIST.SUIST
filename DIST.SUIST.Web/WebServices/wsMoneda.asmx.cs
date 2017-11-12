using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DIST.SUIST.Web
{
    /// <summary>
    /// Descripción breve de wsMoneda
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsMoneda : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MonedaBE ObtenerMoneda(int IdMoneda)
        {
            MonedaBE objMonedaBE = new MonedaBE();

            try
            {
                using (MonedaBL objMonedaBL = new MonedaBL())
                {
                    objMonedaBE = objMonedaBL.ObtenerMoneda(IdMoneda);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new MonedaBE();
            }

            return objMonedaBE;
        }
    }
}
