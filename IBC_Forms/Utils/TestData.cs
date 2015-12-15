using IBC_Forms.Controller;
using IBC_Forms.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace IBC_Forms.Utils
{
    public class TestData
    {
        public static Form[] getForms()
        {
            Form[] f = new Form[]
            {
                //Load Data From Database ???
                new Form {
                    Id = 0,
                    Title = "Erste Form",
                    Fields = new Field[] {
                        new Field { Id = 0, Title = "Vorname" },
                        new Field { Id = 1, Title = "Nachname" },
                        new Field { Id = 2, Title = "PLZ" }
                    },
                    HTMLTemplate = File.ReadAllText(LocalPath.getTemplatePath() + "formEins.htm"),
                    DocXTemplatePath = "formEins.docx"
                },

                new Form {
                    Id = 1,
                    Title = "Zweite Form",
                    Fields = new Field[] {
                        new Field { Id = 0, Title = "Vorname" },
                        new Field { Id = 1, Title = "Nachname" },
                        new Field { Id = 2, Title = "PLZ" }
                    },
                    HTMLTemplate = File.ReadAllText(LocalPath.getTemplatePath() + "formZwei.htm"),
                    DocXTemplatePath = "formZwei.docx"
                }
            };

            return f;
        }
    }
}