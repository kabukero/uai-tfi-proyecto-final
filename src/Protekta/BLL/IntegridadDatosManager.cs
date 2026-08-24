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
        private DigitoVerificadorMapper mapper = DigitoVerificadorMapper.Instance;
        private UsuarioMapper usuarioMapper = UsuarioMapper.Instance;

        public void ActualizaDV()
        {
            mapper.ActualizarDV(usuarioMapper.Obtener("pepe@gmail.com"));
        }

        public VerificarIntegridadRespuesta VerificarIntegridad()
        {
            return mapper.VerificarIntegridad();
        }

        public void RecalcularActualizarDV()
        {
            mapper.RecalcularActualizarDV();
        }
    }
}
