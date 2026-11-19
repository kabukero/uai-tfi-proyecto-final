using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class CertificadoDigitalValidator
    {

        public bool ValidarCertificadoDigital(
            string xml,
            string numeroFactura,
            string usuarioId,
            string ultimosDigitosTarjeta,
            DateTime fecha,
            string servicioId,
            string suscripcionId = null)
        {
            try
            {
                XDocument documento = XDocument.Parse(xml);

                XElement root = documento.Element("CertificadoDigital");

                if (root == null)
                    return false;

                // Validar factura
                string factura = root.Element("Factura")
                                     ?.Element("Numero")?.Value;

                if (factura != numeroFactura)
                    return false;

                // Validar usuario
                string usuario = root.Element("Usuario")
                                     ?.Element("Id")?.Value;

                if (usuario != usuarioId)
                    return false;

                // Validar tarjeta
                string tarjeta = root.Element("Tarjeta")
                                     ?.Element("UltimosDigitos")?.Value;

                if (tarjeta != ultimosDigitosTarjeta)
                    return false;

                // Validar fecha de emisión
                string fechaEmision = root.Element("FechaEmision")?.Value;

                if (!DateTime.TryParse(fechaEmision, out DateTime fechaCertificado))
                    return false;

                if (fechaCertificado.Date != fecha.Date)
                    return false;

                // Validar servicio
                string servicio = root.Element("Servicio")
                                      ?.Element("Id")?.Value;

                if (servicio != servicioId)
                    return false;

                // Validar suscripción cuando corresponda
                XElement suscripcion = root.Element("Suscripcion");

                bool aplicaSuscripcion =
                    bool.TryParse(suscripcion?.Element("Aplica")?.Value,
                                  out bool aplica) && aplica;

                if (aplicaSuscripcion)
                {
                    string suscripcionXml =
                        suscripcion.Element("Id")?.Value;

                    if (string.IsNullOrWhiteSpace(suscripcionId) ||
                        suscripcionXml != suscripcionId)
                        return false;
                }
                else if (!string.IsNullOrWhiteSpace(suscripcionId))
                {
                    return false;
                }

                // Validar estado
                string estado = root.Element("Validacion")
                                    ?.Element("Estado")?.Value;

                if (estado != "VALIDADA")
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CertificadoDigitalValidator()
        {
            bool resultado = ValidarCertificadoDigital(
                xml: "certificado-digital.xml",
                numeroFactura: "FAC-0001-00001234",
                usuarioId: "USR-001",
                ultimosDigitosTarjeta: "1234",
                fecha: new DateTime(2026, 11, 18),
                servicioId: "SERV-001",
                suscripcionId: "SUB-001"
            );
        }
    }
}
