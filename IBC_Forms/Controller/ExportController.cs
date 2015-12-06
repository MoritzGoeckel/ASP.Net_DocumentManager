using IBC_Forms.Utils;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IBC_Forms.Controller
{
    public class ExportController : ApiController
    {
        private static string getTmpPath()
        {
            return System.Web.Hosting.HostingEnvironment.MapPath("~/tmp/");
        }

        Random rand = new Random();

        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage GetExport(int id, string fields, string type)
        {
            string html = getHTML(id, fields);
            if (html == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            else
            {
                WdSaveFormat format;

                if (type == "pdf")
                {

                }
                else if (type == "docx")
                {

                }
                else if (type == "mail")
                {
                    //Do some mail stuff
                    return null;
                }
                else
                    return null;

                //PDF oder DOCX
                var word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = true;

                int z = rand.Next(10000);

                if (Directory.Exists(getTmpPath()) == false)
                    Directory.CreateDirectory(getTmpPath());

                var fromPath = getTmpPath() + "converter_" + z + ".html";
                var toPath = getTmpPath() + "converter_" + z + ".converted";

                File.WriteAllText(fromPath, html);

                var wordDoc = word.Documents.Open(FileName: fromPath, ReadOnly: false);
                wordDoc.SaveAs2(FileName: toPath, FileFormat: WdSaveFormat.wdFormatPDF);

                HttpResponseMessage response = getFileResponse(toPath, "form." + type); // Naja...
                File.Delete(fromPath);
                File.Delete(toPath);

                return response;
            }
        }

        private HttpResponseMessage getFileResponse(string path, string name)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(path, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = name;

            return response;
        }

        private string getHTML(int id, string fields)
        {
            var form = TestData.forms.FirstOrDefault((p) => p.Id == id);
            if (form == null)
            {
                return null;
            }
            else
            {
                string html = form.Template;

                foreach (string field in fields.Split(';'))
                {
                    if (field != null && field != "" && field != " ")
                    {
                        string[] keyValue = field.Split('=');
                        html = html.Replace("|" + keyValue[0] + "|", keyValue[1]);
                    }
                }

                return html;
            }
        }
    }
}
