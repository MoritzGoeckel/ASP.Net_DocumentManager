using IBC_Forms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC_Forms.Utils
{
    public class TestData
    {
        public static Form[] forms = new Form[]
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
                HTMLTemplate = "<h1>Erste Form</h1>Vorname = |0|<br />Nachname = |1|<br />PLZ = |2|",
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
                HTMLTemplate = "<h1>Zweite Form</h1>Vorname = |0|<br />Nachname = |1|<br />PLZ = |2|",
                DocXTemplatePath = "formZwei.docx"
            }
        };
    }
}