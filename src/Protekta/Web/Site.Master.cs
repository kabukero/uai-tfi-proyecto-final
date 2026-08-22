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
        private IntegridadDatosManager integridadDatosManager = new IntegridadDatosManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarIdiomas();
            }

            // mostrar el nombre y apellido del usuario logueado
            MostrarUsuarioLogueado();

            // recalcular DV
            //integridadDatosManager.ActualizaDV();
        }

        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["UsuarioIdioma"] = ddlLanguage.SelectedValue;
            Response.Redirect(Request.RawUrl);
        }

        private void MostrarUsuarioLogueado()
        {
            HyperLink7.Text = Session["UsuarioLogueado"] == null ? "" : $"{((Usuario)Session["UsuarioLogueado"]).Nombre} {((Usuario)Session["UsuarioLogueado"]).Apellido}";
        }

        private void CargarIdiomas()
        {
            List<Idioma> items = manager.Obtener();
            ddlLanguage.DataSource = items;
            ddlLanguage.DataTextField = "Nombre";
            ddlLanguage.DataValueField = "Codigo";
            ddlLanguage.DataBind();
            ddlLanguage.SelectedValue = Session["UsuarioIdioma"] == null ? SistemaConfiguracion.IdiomaPredeterminado : Session["UsuarioIdioma"].ToString();
        }
    }
}
