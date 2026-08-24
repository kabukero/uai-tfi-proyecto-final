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
            new IntegridadDatosManager().RecalcularActualizarDV();
        }
    }
}
