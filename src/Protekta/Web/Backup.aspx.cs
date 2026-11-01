using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;
using Resources;

namespace Web
{
    public partial class Backup : BasePage
    {
        private UsuarioManager usuarioManager = new UsuarioManager();
        private BackupManager manager = new BackupManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            // validar si el usuario esta logueado
            ValidarExisteSesionLogin();

            // validar si el usuario tiene permisos
            if (!usuarioManager.TienePermiso((Usuario)Session["UsuarioLogueado"], SistemaConfiguracion.GESTION_BITACORA))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                pnlBackupError.Visible = false;
                CargarBackups();
            }
        }

        private void CargarBackups()
        {
            GridViewBackup.DataSource = manager.Obtener();
            GridViewBackup.DataBind();
        }

        protected void BtnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                manager.HacerBackup(new BE.Backup() { FechaCreacion = DateTime.Now, Usuario = (Usuario)Session["UsuarioLogueado"] });
                CargarBackups();
            }
            catch (Exception ex)
            {
                lblBackupError.Text = Labels.Backup_Error;
                pnlBackupError.Visible = true;
            }
        }
    }
}
