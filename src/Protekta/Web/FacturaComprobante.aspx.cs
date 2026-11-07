using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using BLL;
using BE;
using Image = iTextSharp.text.Image;


namespace Web
{
    public partial class FacturaComprobante : System.Web.UI.Page
    {
        CarritoManager carritoManager = new CarritoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] == null)
                return;

            int FacturaId = int.Parse(Request.QueryString["Id"].ToString());

            byte[] pdfBytes = GeneratePdf(FacturaId);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline; filename=Report.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }

        private byte[] GeneratePdf(int facturaId)
        {
            Factura factura = carritoManager.ObtenerFactura(facturaId);

            if (factura == null)
                throw new Exception("Invoice not found.");

            using (MemoryStream ms = new MemoryStream())
            {
                Document documento = new Document(
                    PageSize.A4, 40, 40, 40, 40);

                PdfWriter.GetInstance(documento, ms);

                documento.Open();

                Font fuenteTitulo = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD, 18);

                Font fuenteNegrita = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD, 10);

                Font fuenteNormal = FontFactory.GetFont(
                    FontFactory.HELVETICA, 10);

                Font fuenteBlanca = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                Font fuenteTotal = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD, 14);

                // Currency format
                string moneda(decimal precio)
                {
                    return precio.ToString(
                        "C2",
                        new System.Globalization.CultureInfo("es-AR"));
                }

                // Logo
                string rutaLogo = Server.MapPath(
                    "~/Content/Images/protekta-logo.jpeg");

                if (File.Exists(rutaLogo))
                {
                    Image logo = Image.GetInstance(rutaLogo);
                    logo.ScaleToFit(150f, 80f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    documento.Add(logo);
                }

                documento.Add(new Paragraph("\n"));

                // Title
                Paragraph titulo = new Paragraph(
                    "FACTURA", fuenteTitulo);

                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.SpacingAfter = 20f;

                documento.Add(titulo);

                // Invoice information
                PdfPTable tablaCabecera = new PdfPTable(2);

                tablaCabecera.WidthPercentage = 100;
                tablaCabecera.SetWidths(new float[] { 30f, 70f });

                void AgregarDato(string etiqueta, string valor)
                {
                    PdfPCell celda1 = new PdfPCell(
                        new Phrase(etiqueta, fuenteNegrita));

                    celda1.Border = Rectangle.NO_BORDER;
                    celda1.Padding = 5;

                    PdfPCell celda2 = new PdfPCell(
                        new Phrase(valor ?? "", fuenteNormal));

                    celda2.Border = Rectangle.NO_BORDER;
                    celda2.Padding = 5;

                    tablaCabecera.AddCell(celda1);
                    tablaCabecera.AddCell(celda2);
                }

                AgregarDato("Factura N°", factura.Id.ToString());

                AgregarDato(
                    "Fecha",
                    factura.Fecha.ToString("dd/MM/yyyy"));

                AgregarDato(
                    "Cliente",
                    $"{factura.Usuario.Nombre} {factura.Usuario.Apellido}");

                AgregarDato("Email", factura.Usuario.Email);

                AgregarDato(
                    "Medio de Pago",
                    factura.MedioPago.Nombre);

                tablaCabecera.SpacingAfter = 25f;

                documento.Add(tablaCabecera);

                // Invoice details
                PdfPTable tablaDetalle = new PdfPTable(4);

                tablaDetalle.WidthPercentage = 100;

                tablaDetalle.SetWidths(
                    new float[] { 25f, 40f, 15f, 20f });

                string[] encabezados =
                {
            "Suscripción",
            "Descripción",
            "Precio",
            "Subtotal"
        };

                foreach (string encabezado in encabezados)
                {
                    PdfPCell header = new PdfPCell(
                        new Phrase(encabezado, fuenteBlanca));

                    header.BackgroundColor = new BaseColor(0, 102, 204);
                    header.HorizontalAlignment = Element.ALIGN_CENTER;
                    header.VerticalAlignment = Element.ALIGN_MIDDLE;
                    header.Padding = 7;

                    tablaDetalle.AddCell(header);
                }

                decimal total = 0;

                foreach (FacturaDetalle detalle in factura.Detalles)
                {
                    decimal precio = detalle.Subscripcion.Precio;

                    // Assuming quantity = 1 per subscription
                    decimal subtotal = precio;

                    total += subtotal;

                    PdfPCell nombre = new PdfPCell(
                        new Phrase(detalle.Subscripcion.Titulo, fuenteNormal));

                    nombre.Padding = 6;

                    PdfPCell descripcion = new PdfPCell(
                        new Phrase(detalle.Subscripcion.Descripcion, fuenteNormal));

                    descripcion.Padding = 6;

                    PdfPCell precioCell = new PdfPCell(
                        new Phrase(moneda(precio), fuenteNormal));

                    precioCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    precioCell.Padding = 6;

                    PdfPCell subtotalCell = new PdfPCell(
                        new Phrase(moneda(subtotal), fuenteNormal));

                    subtotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    subtotalCell.Padding = 6;

                    tablaDetalle.AddCell(nombre);
                    tablaDetalle.AddCell(descripcion);
                    tablaDetalle.AddCell(precioCell);
                    tablaDetalle.AddCell(subtotalCell);
                }

                documento.Add(tablaDetalle);

                documento.Add(new Paragraph("\n"));

                // Invoice total
                PdfPTable tablaTotal = new PdfPTable(2);

                tablaTotal.WidthPercentage = 40;
                tablaTotal.HorizontalAlignment = Element.ALIGN_RIGHT;

                PdfPCell totalLabel = new PdfPCell(
                    new Phrase("TOTAL:", fuenteTotal));

                totalLabel.Border = Rectangle.NO_BORDER;
                totalLabel.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalLabel.Padding = 6;

                PdfPCell totalValue = new PdfPCell(
                    new Phrase(moneda(total), fuenteTotal));

                totalValue.Border = Rectangle.NO_BORDER;
                totalValue.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalValue.Padding = 6;

                tablaTotal.AddCell(totalLabel);
                tablaTotal.AddCell(totalValue);

                documento.Add(tablaTotal);

                documento.Add(new Paragraph("\n\n"));

                // Footer
                Paragraph pie = new Paragraph(
                    "Gracias por confiar en Protekta.",
                    fuenteNormal);

                pie.Alignment = Element.ALIGN_CENTER;

                documento.Add(pie);

                documento.Close();

                return ms.ToArray();
            }
        }

    }
}
