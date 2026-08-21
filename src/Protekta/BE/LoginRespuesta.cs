using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class LoginRespuesta
    {
        public Usuario UsuarioLogin { get; set; }
        public LoginEstado LoginEstado { get; set; }
    }
}
