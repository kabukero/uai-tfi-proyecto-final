using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;
using Resources;
using WebGrease.Activities;

namespace Web
{
    public partial class Login : BasePage
    {
        private UsuarioManager usuarioManager = new UsuarioManager();
        private IntegridadDatosManager integridadDatosManager = new IntegridadDatosManager();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogIn(object sender, EventArgs e)
        {
            // los datos ingresados no son validos
            if (!IsValid)
            {
                return;
            }

            try
            {
                // verificar integridad datos
                VerificarIntegridadRespuesta integridadRespuesta = integridadDatosManager.VerificarIntegridad();
                if(integridadRespuesta == null || integridadRespuesta.HayErrores)
                {
                    lblLoginError.Text = Labels.Login_ErrorMensajeIntegridadDatos;
                    pnlLoginError.Visible = true;
                    return;
                }

                // obtener usuario por e-mail
                LoginRespuesta respuesta = usuarioManager.Login(TxtEmail.Text, TxtPassword.Text);

                // validar usuario login
                if (respuesta.LoginEstado == LoginEstado.NoExisteUsuario)
                {
                    lblLoginError.Text = Labels.Login_ErrorMensajeUsuarioNoExiste;
                    pnlLoginError.Visible = true;
                    return;
                }
                else if (respuesta.LoginEstado == LoginEstado.UsuarioBloqueado)
                {
                    lblLoginError.Text = Labels.Login_ErrorMensajeUsuarioBloqueado;
                    pnlLoginError.Visible = true;
                    return;
                }
                else if (respuesta.LoginEstado == LoginEstado.PasswordIncorrecta)
                {
                    lblLoginError.Text = Labels.Login_ErrorMensajePasswordIncorrecta;
                    pnlLoginError.Visible = true;
                    return;
                }

                // guardo usuario logueado en property UsuarioLogueado
                // encapsula variable de sesion de usuario login
                UsuarioLogueado = respuesta.UsuarioLogin;

                // login process OK
                IrUrlHome();
            }
            catch (Exception ex)
            {
                lblLoginError.Text = ex.Message;
                pnlLoginError.Visible = true;
            }

            
        }

        private void IrUrlHome()
        {
            string urlRegreso = Request.QueryString["ReturnUrl"];
            if (string.IsNullOrEmpty(urlRegreso))
            {
                // No hay Url especificada, se redirecciona a la página principal
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Response.Redirect(urlRegreso);
            }
        }
    }
}
