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
                        new TextField(0, "Vorname"),
                        new TextField(1, "Nachname"),
                        new TextField(2, "PLZ"),
                        new CheckboxField(3, "Cool", false),
                        new DropdownField(4, "Position", new string[] { "Bäcker", "Maurer", "Angestellter" })
                    },
                    HTMLTemplate = File.ReadAllText(LocalPath.getTemplatePath() + "formEins.htm"),
                    DocXTemplatePath = "formEins.docx"
                },

                new Form {
                    Id = 1,
                    Title = "Zweite Form",
                    Fields = new Field[] {
                        new TextField(0, "Vorname"),
                        new TextField(1, "Nachname")
                    },
                    HTMLTemplate = File.ReadAllText(LocalPath.getTemplatePath() + "formZwei.htm"),
                    DocXTemplatePath = "formZwei.docx"
                }
            };

            return f;
        }
    }
}