using IBC_Forms.Model;
using IBC_Forms.Utils;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Word;
using Novacode;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace IBC_Forms.Controller
{
    public class ExportController : ApiController
    {
        Random rand = new Random();

        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetExport(int id, string fields, string type)
        {
            var form = getForm(id);
            string tmpPath = LocalPath.getTmpPath() + rand.Next(100000);
            string docxPath = tmpPath + ".docx";

            File.Copy(LocalPath.getTemplatePath() + form.DocXTemplatePath, docxPath);
            DocX document = DocX.Load(docxPath);
            document = replaceFieldsInDocument(fields, document);

            document.Save();

            if (type == "pdf")
            {
                string pdfPath = tmpPath + ".pdf";

                var word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = false;

                var wordDoc = word.Documents.Open(FileName: docxPath, ReadOnly: false);
                wordDoc.SaveAs2(FileName: pdfPath, FileFormat: WdSaveFormat.wdFormatPDF);
                wordDoc.Close();

                word.Quit();

                HttpResponseMessage response = getFileResponse(pdfPath, "form.pdf");

                File.Delete(docxPath);
                File.Delete(pdfPath);

                return response;
            }
            else if (type == "docx")
            {
                HttpResponseMessage response = getFileResponse(docxPath, "form.docx");

                File.Delete(docxPath);

                return response;
            }
            else if (type == "html")
            {
                string htmlPath = tmpPath + ".html";

                var word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = false;

                var wordDoc = word.Documents.Open(FileName: docxPath, ReadOnly: false);
                wordDoc.SaveAs2(FileName: htmlPath, FileFormat: WdSaveFormat.wdFormatFilteredHTML);
                wordDoc.Close();

                word.Quit();

                HttpResponseMessage response = getFileResponse(htmlPath, "form.html");

                File.Delete(docxPath);
                File.Delete(htmlPath);

                return response;
            }
            else if (type == "mail")
            {
                string htmlPath = tmpPath + ".html";

                var word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = false;

                var wordDoc = word.Documents.Open(FileName: docxPath, ReadOnly: false);
                wordDoc.SaveAs2(FileName: htmlPath, FileFormat: WdSaveFormat.wdFormatHTML);
                wordDoc.Close();

                word.Quit();

                string html = File.ReadAllText(htmlPath);

                //Send Mail
                //Send PDF?

                File.Delete(docxPath);
                File.Delete(htmlPath);

                return new HttpResponseMessage();
            }
            else
                return null;
        }

        private HttpResponseMessage getFileResponse(string path, string name)
        {
            byte[] bytes;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = name;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return response;
        }

        private Form getForm(int id)
        {
            return TestData.getForms().FirstOrDefault((p) => p.Id == id);
        }

        private DocX replaceFieldsInDocument(string fields, DocX document)
        {
            foreach (string field in fields.Split(';'))
            {
                if (field != null && field != "" && field != " ")
                {
                    string[] keyValue = field.Split('=');
                    document.ReplaceText("|" + keyValue[0] + "|", keyValue[1]);
                }
            }

            //Lösche ungenutzte Felder
            int i = 0;
            while (i < 200)
            {
                document.ReplaceText("|" + i + "|", "");
                i++;
            }

            return document;
        }
    }
}
