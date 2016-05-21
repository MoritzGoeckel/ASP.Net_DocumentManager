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
                    Template_html = "formEins.htm",
                    Template_docx = "formEins.docx"
                },

                new Form {
                    Id = 1,
                    Title = "Zweite Form",
                    Fields = new Field[] {
                        new TextField(0, "Vorname"),
                        new TextField(1, "Nachname")
                    },
                    Template_html = "formZwei.htm",
                    Template_docx = "formZwei.docx"
                },

                new Form {
                    Id = 2,
                    Title = "ITE001-DE EDV-Zugangsdaten",
                    Fields = new Field[] {
                        new TextField(0, "Vorname"),
                        new TextField(1, "Nachname"),
                        new DropdownField(2, "Position", new string[] { "Mitarbeiter", "Teamleiter", "Gruppensprecher", "Schichtsprecher", "Stellvert. Schichtsprecher" }),
                        new TextField(3, "Bereich"),
                        new TextField(4, "Team"),
                        new TextField(5, "Gruppe"),
                        new TextField(6, "Bemerkung"),
                        new CheckboxField(7, "Festanschluss", false),
                        new CheckboxField(8, "Mobil intern", false),
                        new CheckboxField(9, "Handy", false),
                        new CheckboxField(10, "Terminal", false),
                        new CheckboxField(11, "PC", false),
                        new CheckboxField(12, "Laptop", false),
                        new CheckboxField(13, "Option 1", false),
                        new TextField(14, "Option 1 - Name"),

                        new CheckboxField(15, "Option 2", false),
                        new TextField(16, "Option 2 - Name"),

                        new CheckboxField(17, "PSIpenta", false),
                        new CheckboxField(18, "PSI BDEPZ", false),
                        new CheckboxField(19, "PSI Varial World – Finance", false),
                        new CheckboxField(20, "PSI Varial Guide", false),
                        new CheckboxField(21, "PSI Projektmanagement", false),
                        new CheckboxField(22, "PSI Leitstand", false),
                        new CheckboxField(23, "Perbit Views", false),
                        new CheckboxField(24, "QSYS", false),
                        new CheckboxField(25, "STB-System", false),

                        new CheckboxField(26, "Option 1", false),
                        new TextField(27, "Name"),
                        new TextField(28, "Name 2"),


                        new CheckboxField(29, "Option 2", false),
                        new TextField(30, "Name"),
                        new TextField(31, "Name 2"),

                        new CheckboxField(32, "Weitere Ordner: Option 1", false),
                        new TextField(33, "Name"),

                        new CheckboxField(34, "Option 2", false),
                        new TextField(35, "Name"),

                        new CheckboxField(36, "Datenbanken: TIM Technische Information", false),
                        new CheckboxField(37, "SAM", false),

                        new CheckboxField(38, "Option 1", false),
                        new TextField(39, "Name"),
                        new TextField(40, "Name 2"),

                        new CheckboxField(41, "Option 2", false),
                        new TextField(42, "Name"),
                        new TextField(43, "Name 2"),

                        new CheckboxField(44, "Option 3", false),
                        new TextField(45, "Name"),
                        new TextField(46, "Name 2"),

                        new CheckboxField(47, "Option 4", false),
                        new TextField(48, "Name"),
                        new TextField(49, "Name 2"),

                        new CheckboxField(50, "Option 5", false),
                        new TextField(51, "Name"),
                        new TextField(52, "Name 2"),

                        new CheckboxField(53, "Neuprojekte", false),
                        new CheckboxField(58, "Lesen", false),
                        new CheckboxField(59, "Lesen + Schreiben", false),
                        new CheckboxField(54, "Reiseplan", false),

                        new CheckboxField(55, "Option", false),
                        new TextField(56, "Name"),
                        new TextField(57, "Name 2"),

                        new CheckboxField(60, "Email intern", false),
                        new CheckboxField(61, "Email extern", false),
                        new CheckboxField(62, "Internetzugang", false),

                        new CheckboxField(64, "Download - Ja", false),
                        new CheckboxField(65, "Nein", false),


                        new CheckboxField(63, "Remote User", false),

                        new CheckboxField(66, "Option", false),
                        new TextField(67, "Name"),
                        new TextField(68, "Name 2")
                    },
                    Template_html = "EDV-Zugangsdaten.htm",
                    Template_docx = "EDV-Zugangsdaten.docx"
                }
            };

            return f;
        }
    }
}