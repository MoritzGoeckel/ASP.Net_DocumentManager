using IBC_Forms.Utils;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
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
                if (type == "pdf")
                {
                    MemoryStream workStream = new MemoryStream();
                    Document document = new Document();
                    PdfWriter.GetInstance(document, workStream).CloseStream = false;

                    document.Open();
                    HTMLWorker hw = new HTMLWorker(document);
                    hw.Parse(new StringReader(html));

                    //StyleSheet ss = new StyleSheet();
                    //hw.SetStyleSheet(ss);

                    document.Close();

                    byte[] byteInfo = workStream.ToArray();
                    workStream.Write(byteInfo, 0, byteInfo.Length);
                    workStream.Position = 0;

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(workStream.GetBuffer());
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = "form.pdf";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    return response;
                }
                else if (type == "docx")
                {
                    //Convert to docx ???
                    return null;
                }
                else if (type == "mail")
                {
                    //Do some mail stuff ???
                    return null;
                }
                else
                    return null;  
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
