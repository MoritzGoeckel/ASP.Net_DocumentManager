using IBC_Forms.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace IBC_Forms.Utils
{
    public class Database
    {
        private static Database instance;
        public static Database getInstance()
        {
            if (instance == null)
                instance = new Database();

            return instance;
        }

        private SQLiteConnection connection;

        private Database()
        {
            connection = new SQLiteConnection("Data Source="+LocalPath.getDatabasePath() + "formsDB"+ ".s3db;Version=3;");
            connection.Open();

            if (getForms().Length == 0)
            {
                foreach (Form f in TestData.getForms())
                    insertForm(f);
            }
        }

        public Form[] getForms()
        {
            List<Form> forms = new List<Form>();

            string sql = "SELECT * FROM forms ORDER BY title";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string s = reader["fields"].ToString();

                forms.Add(new Form()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Title = reader["title"].ToString(),
                    Template_html = File.ReadAllText(LocalPath.getTemplatePath() + reader["template_html"].ToString()),
                    Template_docx = reader["template_docx"].ToString(),
                    Fields = JsonConvert.DeserializeObject<Field[]>(reader["fields"].ToString()),
                    Active = reader["visible"].ToString() == "1"
                });
            }

            return forms.ToArray();
        }

        public void setFormVisible(int id, bool visible)
        {
            string sql = "UPDATE forms SET visible = " + (visible ? "1" : "0") + " WHERE id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public void insertForm(Form f)
        {
            string sql = "INSERT INTO forms (title, template_docx, template_html, fields) VALUES ('" + f.Title + "', '"+ f.Template_docx +"', '" + f.Template_html + "', @fields)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.Add(new SQLiteParameter("@fields", JsonConvert.SerializeObject(f.Fields)));

            command.ExecuteNonQuery();
        }
    }
}