using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class IntegridadDatosManager
    {
        private DigitoVerificadorMapper mapper = new DigitoVerificadorMapper();
        private UsuarioMapper usuarioMapper = new UsuarioMapper();

        public void ActualizaDV()
        {
            mapper.ActualizarDV(usuarioMapper.Obtener("pepe@gmail.com"));
        }

        public VerificarIntegridadRespuesta VerificarIntegridad()
        {
            return mapper.VerificarIntegridad();
        }
    }
}
