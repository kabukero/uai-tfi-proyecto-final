using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using BE;
using BLL;
using System.Xml;

namespace Web
{
    /// <summary>
    /// Summary description for SubscripcionWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SubscripcionWS : System.Web.Services.WebService
    {
        private SubscripcionManager manager = new SubscripcionManager();

        [WebMethod]
        public string ObtenerSubscripionesXml()
        {
            List<Subscripcion> lista = manager.ObtenerSubscripciones();

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter)
            {
                Formatting = Formatting.Indented
            };

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Subscripciones");

            foreach (Subscripcion subscripcion in lista)
            {
                xmlTextWriter.WriteStartElement("Subscripcion");
                xmlTextWriter.WriteStartElement("Id");
                xmlTextWriter.WriteString(subscripcion.Id.ToString());
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("Titulo");
                xmlTextWriter.WriteString(subscripcion.Titulo);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("Descripcion");
                xmlTextWriter.WriteString(subscripcion.Descripcion);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("Precio");
                xmlTextWriter.WriteString(subscripcion.Precio.ToString());
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
            }

            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Flush();
            xmlTextWriter.Close();

            return stringWriter.ToString();
        }
    }
}
