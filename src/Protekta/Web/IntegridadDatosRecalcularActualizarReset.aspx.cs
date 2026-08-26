using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web
{
    public partial class IntegridadDatosRecalcularActualizarReset : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // validar password interna
            if(txtPassword.Text == "123")
            {
                // reset de los DVH y DVV
                new IntegridadDatosManager().RecalcularActualizarDV();
            }
            // se redirecciona a la página principal
            Response.Redirect("~/Default.aspx");
        }
    }
}
