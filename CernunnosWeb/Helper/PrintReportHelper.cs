using CernunnosWeb.Security.Helper;
using Microsoft.Reporting.WebForms;
using System;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Montreal.Protocolo.Web.Helper
{
    /// <summary>
    /// Classe que imprime o relatório.
    /// </summary>
    public sealed class PrintReportHelper
    {
        public enum RenderType
        {
            PDF,
            Excel,
            Image
        }

        /// <summary>
        /// Imprimi o relatório.
        /// </summary>
        /// <param name="report">Relatório a ser impresso.</param>
        /// <param name="frame">Frame onde será exibido a imporessão.</param>
        public static void Print(LocalReport report, HtmlIframe frame)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = report.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            CacheHelper.AddToUser<byte[]>("PDF_TO_PRINT", StreamPrintPdf(bytes), 1);

            frame.Attributes["src"] = VirtualPathUtility.ToAbsolute("~/") + "PDFViewer.aspx";
        }

        /// <summary>
        /// Imprimi o relatório.
        /// </summary>
        /// <param name="report">Relatório a ser impresso.</param>
        /// <param name="frame">Frame onde será exibido a imporessão.</param>
        public static void View(LocalReport report, HtmlIframe frame)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            /*
            ////The DeviceInfo settings should be changed based on the reportType 
            string deviceInfo = @"<DeviceInfo>
                                     <ColorDepth>32</ColorDepth>
                                     <DpiX>96</DpiX>
                                     <DpiY>96</DpiY>
                                     <OutputFormat>PDF</OutputFormat>
                                     <PageWidth>8.27in</PageWidth>
                                     <PageHeight>11.69in</PageHeight>
                                     <MarginTop>0in</MarginTop>
                                     <MarginLeft>0in</MarginLeft>
                                     <MarginRight>0in</MarginRight>
                                     <MarginBottom>0in</MarginBottom>
                                    </DeviceInfo>";
            */

            byte[] bytes = report.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            CacheHelper.AddToUser<byte[]>("PDF_TO_PRINT", bytes, 1);

            frame.Attributes["src"] = VirtualPathUtility.ToAbsolute("~/") + "PDFViewer.aspx";
        }

        /// <summary>
        /// Imprimi o relatório.
        /// </summary>
        /// <param name="report">Relatório a ser impresso.</param>
        /// <param name="frame">Frame onde será exibido a imporessão.</param>
        public static void Download(LocalReport report, HtmlIframe frame, RenderType renderType)
        {
            string extension = string.Empty;
            string mimeType = string.Empty;
            Warning[] warnings;
            string[] streamids;
            string encoding;

            switch (renderType)
            {
                case RenderType.PDF:
                    extension = "pdf";
                    mimeType = "application/pdf";
                    break;
                case RenderType.Excel:
                    extension = "xls";
                    mimeType = "application/vnd.excel";
                    break;
                case RenderType.Image:
                    extension = "emf";
                    mimeType = "application/image";
                    break;
                default:
                    throw new Exception("Unrecognized type: " + renderType + ".  Type must be PDF, Excel or Image.");
            }

            byte[] bytes = report.Render(renderType.ToString(), null, out mimeType, out encoding, out extension, out streamids, out warnings);

            CacheHelper.AddToUser<string>("PDF_TO_PRINT_EXTENSION", extension, 1);
            CacheHelper.AddToUser<string>("PDF_TO_PRINT_MIMETYPE", "application/octet-stream", 1);
            CacheHelper.AddToUser<byte[]>("PDF_TO_PRINT", bytes, 1);

            frame.Attributes["src"] = VirtualPathUtility.ToAbsolute("~/") + "PDFViewer.aspx";
        }

        /// <summary>
        /// Gera o array de bytes do pdf com o script para impressão.
        /// </summary>
        /// <param name="bytes">PDF em PDF.</param>
        /// <returns>Bytes do PDF com a impressão.</returns>
        private static byte[] StreamPrintPdf(byte[] bytes)
        {
            /*
            var outputStream = new MemoryStream();
            var pdfReader = new PdfReader(bytes);
            var pdfStamper = new PdfStamper(pdfReader, outputStream);

            ////Add the auto-print javascript
            var writer = pdfStamper.Writer;
            PdfAction action = PdfAction.JavaScript("this.print(true);\r", writer);
            writer.AddJavaScript(action);
            pdfStamper.Close();

            var content = outputStream.ToArray();
            outputStream.Close();
            outputStream.Dispose();

            return content;
            */

            return null;
        }
    }
}