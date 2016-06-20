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

        internal void DeleteData()
        {
            SQLiteCommand command = new SQLiteCommand("DELETE FROM forms", connection);
            command.ExecuteNonQuery();
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
                    Template = reader["template"].ToString(),
                    Fields = JsonConvert.DeserializeObject<Field[]>(reader["fields"].ToString()),
                    Active = reader["visible"].ToString() == "1",
                    Type = Convert.ToInt32(reader["type"])
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
            string sql = "INSERT INTO forms (title, template, template_html, fields, type, visible) VALUES ('" + f.Title + "', '"+ f.Template +"', '" + f.Template_html + "', @fields, "+ f.Type +", 1)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.Add(new SQLiteParameter("@fields", JsonConvert.SerializeObject(f.Fields)));

            command.ExecuteNonQuery();
        }
    }
}