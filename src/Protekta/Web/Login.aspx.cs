using System;
using BE;
using BLL;
using Resources;
using static iTextSharp.text.pdf.AcroFields;

namespace Web
{
    public partial class Login : BasePage
    {
        private UsuarioManager usuarioManager = new UsuarioManager();
        private IntegridadDatosManager integridadDatosManager = new IntegridadDatosManager();
        private BitacoraManager bitacoraManager = new BitacoraManager();

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
                BE.Bitacora bitacora = null;
                VerificarIntegridadRespuesta integridadRespuesta = integridadDatosManager.VerificarIntegridad();
                if(integridadRespuesta == null || integridadRespuesta.HayErrores)
                {
                    // mostrar mensaje de error
                    string ErrorMensajes = string.Join(" ", integridadRespuesta.Mensajes);
                    lblLoginError.Text = ErrorMensajes;
                    pnlLoginError.Visible = true;

                    // registrar evento login en la bitacora
                    bitacora = new BE.Bitacora()
                    {
                        Descripcion = ErrorMensajes,
                        FechaEvento = DateTime.Now,
                        Usuario = new Usuario() { Id = 1 },
                        BitacoraTipoEvento = new BitacoraTipoEvento()
                        {
                            Id = (int)BitacoraTipoEventoEnum.Error
                        }
                    };
                    bitacora.Id = bitacoraManager.Alta(bitacora);

                    // recalcular usuario DV
                    integridadDatosManager.ActualizarDV(bitacora);
                    return;
                }

                // obtener usuario por e-mail
                LoginRespuesta respuesta = usuarioManager.Login(TxtEmail.Text, TxtPassword.Text);

                // validar usuario login
                if (respuesta.LoginEstado == LoginEstado.NoExisteUsuario)
                {
                    // mostrar mensaje de error
                    lblLoginError.Text = Labels.Login_ErrorMensajeUsuarioNoExiste;
                    pnlLoginError.Visible = true;
                    return;
                }
                else if (respuesta.LoginEstado == LoginEstado.UsuarioBloqueado)
                {
                    // mostrar mensaje de error
                    lblLoginError.Text = Labels.Login_ErrorMensajeUsuarioBloqueado;
                    pnlLoginError.Visible = true;
                    return;
                }
                else if (respuesta.LoginEstado == LoginEstado.PasswordIncorrecta)
                {
                    // mostrar mensaje de error
                    lblLoginError.Text = Labels.Login_ErrorMensajePasswordIncorrecta;
                    pnlLoginError.Visible = true;

                    // actualizar contador de intentos de login
                    IncrementarIntentosLogin();

                    // validar maximo intentos de login
                    if (int.Parse(Session["CantidadIntentosLogin"].ToString()) >= 3)
                    {
                        // mostrar mensaje de error
                        lblLoginError.Text = Labels.Login_ErrorMensajeUsuarioBloqueado;
                        pnlLoginError.Visible = true;

                        // resetear cantidad intentos de login zero
                        Session["CantidadIntentosLogin"] = 0;

                        // bloquear usuario por e-mail
                        usuarioManager.Bloquear(TxtEmail.Text);

                        // recalcular usuario DV
                        respuesta.UsuarioLogin.Activo = false;
                        integridadDatosManager.ActualizarDV(respuesta.UsuarioLogin);
                    }
                    return;
                }

                // guardo usuario logueado en property UsuarioLogueado
                // (encapsula variable de sesion de usuario login)
                UsuarioLogueado = respuesta.UsuarioLogin;

                // configurar idioma de preferencia del usuario logueado
                Session["UsuarioIdioma"] = UsuarioLogueado.Idioma.Codigo;

                // resetear cantidad intentos de login zero
                Session["CantidadIntentosLogin"] = 0;

                // recalcular usuario DV
                integridadDatosManager.ActualizarDV(UsuarioLogueado);

                // registrar evento login en la bitacora
                bitacora = new BE.Bitacora()
                {
                    Descripcion = Labels.Bitacora_MensajeLogin,
                    FechaEvento = DateTime.Now,
                    Usuario = UsuarioLogueado,
                    BitacoraTipoEvento = new BitacoraTipoEvento()
                    {
                        Id = (int)BitacoraTipoEventoEnum.Informacion
                    }
                };
                bitacora.Id = bitacoraManager.Alta(bitacora);

                // recalcular usuario DV
                integridadDatosManager.ActualizarDV(bitacora);

                // login process OK
                IrUrlHome();
            }
            catch (Exception ex)
            {
                lblLoginError.Text = ex.Message;
                pnlLoginError.Visible = true;
            }
        }

        private void IncrementarIntentosLogin()
        {
            if (Session["CantidadIntentosLogin"] == null)
            {
                Session["CantidadIntentosLogin"] = 1;
            }
            else
            {
                Session["CantidadIntentosLogin"] = int.Parse(Session["CantidadIntentosLogin"].ToString()) + 1;
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
