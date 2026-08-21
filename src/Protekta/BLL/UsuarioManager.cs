using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class UsuarioManager
    {
        private UsuarioMapper mapper = new UsuarioMapper();
        private EncriptadorManager encriptadorManager = new EncriptadorManager();

        public LoginRespuesta Login(string email, string password)
        {
            LoginRespuesta loginRespuesta = new LoginRespuesta();
            Usuario usuario = mapper.Obtener(email);
            loginRespuesta.UsuarioLogin = usuario;

            // validar si el usuario existe
            if (usuario == null)
            {
                loginRespuesta.LoginEstado = LoginEstado.NoExisteUsuario;
                return loginRespuesta;
            }

            // validar si el usuario bloqueado
            if (!usuario.Activo)
            {
                loginRespuesta.LoginEstado = LoginEstado.UsuarioBloqueado;
                return loginRespuesta;
            }

            // validar si la password es correcta
            if (usuario.Password != encriptadorManager.Encriptar(password))
            {
                loginRespuesta.LoginEstado = LoginEstado.PasswordIncorrecta;
                return loginRespuesta;
            }

            // el proceso login es ok
            loginRespuesta.LoginEstado = LoginEstado.LoginOK;
            return loginRespuesta;
        }
    }
}
