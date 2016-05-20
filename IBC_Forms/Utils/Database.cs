using IBC_Forms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        private Database()
        {

        }

        public Form[] getForms()
        {
            return TestData.getForms();
        }

        public void insertForms()
        {

        }
    }
}