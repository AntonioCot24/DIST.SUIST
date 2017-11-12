using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public abstract class PageValidation : Page
    {
        public string idopcion;
        public string objetoWeb;

        public abstract void inicializar();

        protected override void InitializeCulture()
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("es-PE");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            Response.Expires = -1;

            if (!validateSession())
            {
                Session.Clear();
                Session.Abandon();
                Page.ClientScript.RegisterStartupScript(typeof(PageValidation), "toDefault", " exitAppAjax();", true);
            }

            inicializar();
        }

        private bool validateSession()
        {
            return (Session[Constantes.USER_SESSION] == null) ? false : true;
        }
        
        public void CompletarCombos(Control Combo, object Lista, string DataValueField, string DataTextField, EnumeradoresBE.enumTipoCombo Tipo = EnumeradoresBE.enumTipoCombo.Seleccionar)
        {
            switch (Combo.GetType().ToString())
            {
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList ddlCombo = ((DropDownList)Combo);
                    ddlCombo.Items.Clear();
                    ddlCombo.DataSource = Lista;
                    ddlCombo.DataValueField = DataValueField;
                    ddlCombo.DataTextField = DataTextField;
                    ddlCombo.DataBind();
                    if (Tipo == EnumeradoresBE.enumTipoCombo.Seleccionar)
                        ddlCombo.Items.Insert(0, new ListItem(Constantes.TEXTO_SELECCION_COMBO, Constantes.VALOR_SELECCION_COMBO));
                    else if (Tipo == EnumeradoresBE.enumTipoCombo.Todos)
                        ddlCombo.Items.Insert(0, new ListItem(Constantes.TEXTO_TODOS_COMBO, Constantes.VALOR_SELECCION_COMBO));
                    break;
                case "System.Web.UI.HtmlControls.HtmlSelect":
                    HtmlSelect htlmCombo = ((HtmlSelect)Combo);
                    htlmCombo.Items.Clear();
                    htlmCombo.DataSource = Lista;
                    htlmCombo.DataValueField = DataValueField;
                    htlmCombo.DataTextField = DataTextField;
                    htlmCombo.DataBind();
                    if (Tipo == EnumeradoresBE.enumTipoCombo.Seleccionar)
                        htlmCombo.Items.Insert(0, new ListItem(Constantes.TEXTO_SELECCION_COMBO, Constantes.VALOR_SELECCION_COMBO));
                    else if (Tipo == EnumeradoresBE.enumTipoCombo.Todos)
                        htlmCombo.Items.Insert(0, new ListItem(Constantes.TEXTO_TODOS_COMBO, Constantes.VALOR_SELECCION_COMBO));
                    break;
                default:
                    return;
            }

        }

        public void CargarCombo(DropDownList Combo, object Lista, string DataValueField, string DataTextField)
        {
            Combo.DataSource = Lista;
            Combo.DataValueField = DataValueField;
            Combo.DataTextField = DataTextField;
            Combo.DataBind();
            Combo.Items.Insert(0, new ListItem(Constantes.TEXTO_SELECCION_COMBO, Constantes.VALOR_TODOS_COMBO));
        }
    }
}