using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BE;

namespace Web
{
    public partial class IntegridadDatosRecalcularActualizarReset : BasePage
    {
        private BackupManager manager = new BackupManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // validar password interna
                if(txtPassword.Text == "#Akiroro1984#")
                {
                    BE.Backup ultimoBackup = manager.Obtener().OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                
                    if(ultimoBackup == null)
                        Response.Redirect("~/Default.aspx");

                    manager.HacerRestore(ultimoBackup.NombreArchivo);

                    // reset de los DVH y DVV
                    new IntegridadDatosManager().RecalcularActualizarDV();
                }
            }
            catch(Exception ex)
            {

            }
            // se redirecciona a la página principal
            Response.Redirect("~/Default.aspx");
        }
    }
}
