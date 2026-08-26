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

        public void ActualizarDV(Usuario usuario)
        {
            mapper.ActualizarDV(usuario);
        }

        public void ActualizarDV(Bitacora bitacora)
        {
            mapper.ActualizarDV(bitacora);
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
