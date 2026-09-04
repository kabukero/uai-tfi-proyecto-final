using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

namespace Web
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMensajeBienvenida();
        }

        private void SetMensajeBienvenida()
        {
            bool mostrarBotonComprar = false;
            if(UsuarioLogueado == null)
            {
                // Mostrar mensaje de bienvenida
                PnlMensajeBienvenida.Visible = false;
                BtnPlanPrevencionTradicional.Visible = false;
            }
            else
            {
                // Mostrar mensaje de bienvenida
                PnlMensajeBienvenida.Visible = true;
                LblMensajeBienvenida.Text = string.Format(Labels.Default_TextoBienvenida, UsuarioLogueado.ToString(), UsuarioLogueado.Permisos == null || UsuarioLogueado.Permisos.Count == 0 ? "" : UsuarioLogueado.Permisos[0].Nombre);
                mostrarBotonComprar = true;
            }

            // Mostrar / Ocultar Botones Comprar si el usuario esta logueado o no
            BtnPlanPrevencionTradicional.Visible = mostrarBotonComprar;
            BtnPlanGestionDigital.Visible = mostrarBotonComprar;
            BtnPlanPrevencionIA.Visible = mostrarBotonComprar;
        }

        protected void BtnPlan_Command(object sender, CommandEventArgs e)
        {
            string plan = e.CommandArgument.ToString();

            switch (plan)
            {
                case "PlanTradicional":
                    // crear orden
                    break;
                case "PlanGestionDigital":
                    // crear orden
                    break;
                case "PlanPrevencionIA":
                    // crear orden
                    break;
            }
        }
    }
}
