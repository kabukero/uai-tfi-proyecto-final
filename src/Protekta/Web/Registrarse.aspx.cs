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
    public partial class Registrarse : BasePage
    {
        private UsuarioManager usuarioManager = new UsuarioManager();
        private IntegridadDatosManager integridadDatosManager = new IntegridadDatosManager();
        private EncriptadorManager encriptadorManager = new EncriptadorManager();
        private BitacoraManager bitacoraManager = new BitacoraManager();
        private IdiomaManager idiomaManager = new IdiomaManager();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Registrar(object sender, EventArgs e)
        {
            string IdiomaCodigo = Session["UsuarioIdioma"] == null ? SistemaConfiguracion.IdiomaPredeterminado : Session["UsuarioIdioma"].ToString();

            // crear objeto usuario
            Usuario usuario = new Usuario()
            {
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Email = TxtEmail.Text,
                Password = encriptadorManager.Encriptar(TxtPassword.Text),
                Idioma = idiomaManager.Obtener(IdiomaCodigo),
            };

            // crear usuario cliente
            usuarioManager.AgregarCliente(usuario);

            // guardo usuario logueado en property UsuarioLogueado
            // (encapsula variable de sesion de usuario login)
            UsuarioLogueado = usuarioManager.Obtener(usuario.Id);

            // recalcular usuario DV
            integridadDatosManager.ActualizarDV(UsuarioLogueado);

            // registrar evento login en la bitacora
            BE.Bitacora bitacora = new BE.Bitacora()
            {
                Descripcion = Labels.Bitacora_MensajeCrearCliente,
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
            Response.Redirect("~/Default.aspx");
        }
    }
}
