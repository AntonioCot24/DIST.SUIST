using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmDescargarExcel : PageValidation
    {
        public override void inicializar()
        {
            if (!IsPostBack)
            {
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1500;
                Response.CacheControl = "no-cache";

                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();

                XLWorkbook wb = null;

                if (Session[Constantes.Sesion_ExpExcel] != null)
                {
                    wb = (XLWorkbook)Session[Constantes.Sesion_ExpExcel];
                }
                else
                {
                    wb = new XLWorkbook();

                    // Add DataTable as Worksheet
                    wb.Worksheets.Add(Datos, "Hoja 1");
                }
                   

                // Create Response
                HttpResponse response = Response;

                //Prepare the response
                response.Clear();
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", "attachment;filename=" + NombreArchivo + ".xlsx");

                //Flush the workbook to the Response.OutputStream
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(response.OutputStream);
                    MyMemoryStream.Close();
                }

                //Response.Flush();
                Response.End();
            }
        }

        protected DataTable Datos
        {
            get
            {
                DataTable output = null;
                if (Session[Constantes.Sesion_DtExcel] != null)
                    output = (DataTable)Session[Constantes.Sesion_DtExcel];
                return output;
            }
        }

        protected string NombreArchivo
        {
            get
            {
                string output = string.Empty;
                if (!string.IsNullOrEmpty(Request.QueryString["nomArchivoExcel"]))
                    output = Convert.ToString(Request.QueryString["nomArchivoExcel"]);
                return output;
            }
        }
    }
}