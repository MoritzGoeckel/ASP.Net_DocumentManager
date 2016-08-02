using IBC_Forms.Model;
using IBC_Forms.Utils;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
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
            string tmpTemplatePath = tmpPath + (form.Type == 0 ? ".docx" : ".xlsx");

            File.Copy(LocalPath.getTemplatePath() + form.Template, tmpTemplatePath);

            fields = fields.Replace("download=true", "");
            fields = fields.Replace("download=false", "");

            // =============================== DOCX
            if (form.Type == Utils.DocumentTypes.DOCX)
            {
                DocX document = DocX.Load(tmpTemplatePath);
                document = replaceFieldsInDocumentDocX(fields, document);

                document.Save();

                if (type == "pdf")
                {
                    string pdfPath = tmpPath + ".pdf";

                    var word = new Microsoft.Office.Interop.Word.Application();
                    word.Visible = false;

                    var wordDoc = word.Documents.Open(FileName: tmpTemplatePath, ReadOnly: false);
                    wordDoc.SaveAs2(FileName: pdfPath, FileFormat: WdSaveFormat.wdFormatPDF);
                    wordDoc.Close();

                    word.Quit();

                    HttpResponseMessage response = getFileResponse(pdfPath, "form.pdf");

                    File.Delete(tmpTemplatePath);
                    File.Delete(pdfPath);

                    return response;
                }
                else if (type == "docx")
                {
                    HttpResponseMessage response = getFileResponse(tmpTemplatePath, "form.docx");

                    File.Delete(tmpTemplatePath);

                    return response;
                }
                else if (type == "html")
                {
                    string htmlPath = tmpPath + ".html";

                    var word = new Microsoft.Office.Interop.Word.Application();
                    word.Visible = false;

                    var wordDoc = word.Documents.Open(FileName: tmpTemplatePath, ReadOnly: false);
                    wordDoc.SaveAs2(FileName: htmlPath, FileFormat: WdSaveFormat.wdFormatFilteredHTML);
                    wordDoc.Close();

                    word.Quit();

                    HttpResponseMessage response = getFileResponse(htmlPath, "form.html");

                    File.Delete(tmpTemplatePath);
                    File.Delete(htmlPath);

                    return response;
                }
                else if (type == "mail")
                {
                    string htmlPath = tmpPath + ".html";

                    var word = new Microsoft.Office.Interop.Word.Application();
                    word.Visible = false;

                    var wordDoc = word.Documents.Open(FileName: tmpTemplatePath, ReadOnly: false);
                    wordDoc.SaveAs2(FileName: htmlPath, FileFormat: WdSaveFormat.wdFormatHTML);
                    wordDoc.Close();

                    word.Quit();

                    string html = File.ReadAllText(htmlPath);

                    //Send Mail
                    //Send PDF?

                    File.Delete(tmpTemplatePath);
                    File.Delete(htmlPath);

                    return new HttpResponseMessage();
                }
                else
                    return null;
            }

            // =============================== XLS
            else if (form.Type == Utils.DocumentTypes.XLS)
            {
                object m = Type.Missing;
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = false;
                app.DisplayAlerts = false;

                Workbook wb = app.Workbooks.Open(
                tmpTemplatePath,
                m, false, m, m, m, m, m, m, m, m, m, m, m, m);

                wb = ReplaceTextInExcelFile(fields, wb);

                wb.Save();

                if (type == "pdf")
                {
                    string pdfPath = tmpPath + ".pdf";

                    //(wb.Sheets[1] as Worksheet).SaveAs(pdfPath, FileFormat: WdSaveFormat.wdFormatPDF);
                    wb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfPath);
                    wb.Close();
                    app.Quit();

                    HttpResponseMessage response = getFileResponse(pdfPath, "form.pdf");

                    File.Delete(tmpTemplatePath);
                    File.Delete(pdfPath);

                    return response;
                }
                else if (type == "docx")
                {
                    wb.Close();
                    app.Quit();

                    HttpResponseMessage response = getFileResponse(tmpTemplatePath, "form.xlsx");

                    File.Delete(tmpTemplatePath);

                    return response;
                }
                else if (type == "html")
                {
                    string htmlPath = tmpPath + ".htm";

                    wb.SaveAs(htmlPath, FileFormat: XlFileFormat.xlHtml); // Geht noch nicht

                    wb.Close();
                    app.Quit();

                    HttpResponseMessage response = getFileResponse(htmlPath, "form.htm");

                    File.Delete(tmpTemplatePath);
                    File.Delete(htmlPath);

                    return response;
                }
                else if (type == "mail")
                {
                    string htmlPath = tmpPath + ".html";

                    wb.SaveAs(htmlPath, FileFormat: WdSaveFormat.wdFormatFilteredHTML);
                    wb.Close();

                    app.Quit();

                    string html = File.ReadAllText(htmlPath);

                    //Send Mail ???
                    //Send PDF? ???

                    File.Delete(tmpTemplatePath);
                    File.Delete(htmlPath);

                    return new HttpResponseMessage();
                }
                else
                    return null;
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
            return Database.getInstance().getForms().FirstOrDefault((p) => p.Id == id);
        }

        private DocX replaceFieldsInDocumentDocX(string fields, DocX document)
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

        static Workbook ReplaceTextInExcelFile(string fields, Workbook wb)
        {
            Worksheet ws = wb.Worksheets[1] as Worksheet;
            object m = Type.Missing;

            Microsoft.Office.Interop.Excel.Range r = (Microsoft.Office.Interop.Excel.Range)ws.UsedRange;
            Protection p = ws.Protection;

            foreach (string field in fields.Split(';')) 
            {
                if (field != null && field != "" && field != " ")
                {
                    string[] keyValue = field.Split('=');
                    bool success = r.Replace(
                        "|" + keyValue[0] + "|",
                        keyValue[1],
                        XlLookAt.xlPart,
                        XlSearchOrder.xlByRows,
                        false, m, m, m);
                }
            }

            //Lösche ungenutzte Felder
            int i = 0;
            while (i < 200)
            {
                r.Replace(
                        "|" + i + "|",
                        "",
                        XlLookAt.xlWhole,
                        XlSearchOrder.xlByRows,
                        true, m, m, m);
                i++;
            }
            return wb;
        }
    }
}
