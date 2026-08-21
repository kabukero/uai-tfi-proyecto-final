using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum LoginEstado
    {
        LoginOK = 0,
        NoExisteUsuario = 1,
        UsuarioBloqueado = 2,
        SuperoCantidadIntentos = 3,
        PasswordIncorrecta = 4
    }
}
