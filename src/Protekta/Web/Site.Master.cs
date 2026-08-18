using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

namespace Web
{
    public partial class SiteMaster : MasterPage
    {
        private IdiomaManager manager = new IdiomaManager();
        private string usuarioIdioma = "en-US";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIdiomas();
            }
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["UsuarioIdioma"] = ddlLanguage.SelectedValue;
            Response.Redirect(Request.RawUrl);
        }

        private void SetIdioma()
        {
            if (Session["UsuarioIdioma"] != null)
            {
                usuarioIdioma = Session["UsuarioIdioma"].ToString();
            }
        }

        private void CargarIdiomas()
        {
            List<Idioma> items = manager.Obtener();
            ddlLanguage.DataSource = items;
            ddlLanguage.DataTextField = "Nombre";
            ddlLanguage.DataValueField = "Codigo";
            ddlLanguage.DataBind();
            ddlLanguage.SelectedValue = usuarioIdioma;
        }
    }
}
