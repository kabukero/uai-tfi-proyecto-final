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
        private UsuarioMapper mapper = UsuarioMapper.Instance;
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

        public void Bloquear(string email)
        {
            mapper.Bloquear(email);
        }

        public bool TienePermiso(Usuario usuario, string permiso)
        {
            return ChequearPermiso(usuario.Permisos, permiso);
        }

        private bool ChequearPermiso(List<Permiso> permisos, string permisoAChequear)
        {
            foreach (Permiso permiso in permisos)
            {
                if (permiso.Nombre == permisoAChequear)
                {
                    return true;
                }
                else
                {
                    bool tienePermiso = ChequearPermiso(permiso.DevolverPerfil(), permisoAChequear);
                    if (tienePermiso)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
