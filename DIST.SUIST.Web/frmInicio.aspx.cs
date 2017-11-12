using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DIST.SUIST.Web
{
    public partial class frmInicio : PageValidation
    {
        #region Metodos Publicos

        public string NombreCompleto { get; set; }
        protected string urlCambiarContrasenia = UrlManager.getURLEncodeHTML("WEB_CAMBIAR_CONTRSENIA");
        protected string urlDescargarExcel = UrlManager.getURLEncodeHTML("WEB_DESCARGAR_EXCEL");

        #endregion

        #region Override

        public override void inicializar()
        {
            if (!Page.IsPostBack)
            {
                PerfilBE oPerfil = Session[Constantes.Sesion_Perfil] as PerfilBE;

                Response.CacheControl = "no-cache";
                Response.Expires = -1;

                if (!validateSession())
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect(UrlManager.GetSiteRoot() + "/frmLogeo.aspx");
                }

                NombreCompleto = Session[Constantes.Sesion_NombreUsuario].ToString();

                // Menu
                ltrMenuOpciones.Text = CargarMenu(oPerfil.IdPerfil);
            }
        }

        #endregion

        #region Metodos Privados

        public string CargarMenu(int IdPerfil)
        {
            StringBuilder menu = new StringBuilder();

            using (SeguridadBL objSeguridadBL = new SeguridadBL())
            {
                //Listar Padres
                List<OpcionBE> lstOpciones = objSeguridadBL.ListarOpcionesUsuarios(IdPerfil);
                menu.Append("<ul class='nav main-menu'>");

                List<OpcionBE> lstRaiz = (from r
                                          in lstOpciones
                                          where r.IdOpcionPadre.Equals(0)
                                          select r).ToList();

                foreach (OpcionBE OpcionRaiz in lstRaiz)
                {
                    List<OpcionBE> lstHijos = (from h
                                               in lstOpciones
                                               where h.IdOpcionPadre.Equals(OpcionRaiz.IdOpcion)
                                               select h).ToList();

                    //Listar Hijos
                    if (lstHijos.Count > 0)
                    {
                        menu.Append("<li class='dropdown'> <a href='#' class='dropdown-toggle'>" +
                                         "<i class='fa fa-angle-right'></i>&nbsp;" + HttpUtility.HtmlEncode(OpcionRaiz.Denominacion) + "</span>" +
                                         "</a>\n");
                        menu.Append("<ul class='dropdown-menu'>");

                        foreach (OpcionBE OpcionHijo in lstHijos)
                        {
                            menu.Append("<li><a href='#' urlOpcion='" + (string.IsNullOrEmpty(OpcionHijo.UrlOpcion) ? "" : UrlManager.getURLEncodeHTML(OpcionHijo.UrlOpcion)) + "' class='url_link ajax-link'>" +
                                        "&nbsp;&nbsp;<i class='fa fa-angle-right'></i>&nbsp;" + HttpUtility.HtmlEncode(OpcionHijo.Denominacion) +
                                        "</a> </li>\n");
                        }
                        menu.Append("</ul></li>\n");
                    }
                    else
                    {
                        menu.Append("<li> <a href='#' urlOpcion='" + (string.IsNullOrEmpty(OpcionRaiz.UrlOpcion) ? "" : UrlManager.getURLEncodeHTML(OpcionRaiz.UrlOpcion)) + "' class='url_link ajax-link'>" +
                                         "<i class='fa fa-angle-right'></i>&nbsp;" + HttpUtility.HtmlEncode(OpcionRaiz.Denominacion) + "</span>" +
                                         "</a></li>\n");
                    }                    
                }
                menu.Append("</ul></li>\n");
            }

            return menu.ToString();
        }

        private bool validateSession()
        {
            return (Session[Constantes.USER_SESSION] == null) ? false : true;
        }

        #endregion
    }
}