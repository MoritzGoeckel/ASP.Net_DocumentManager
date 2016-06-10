using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC_Forms.Model
{
    public class User
    {
        private string name;
        private bool admin;

        public string getName()
        {
            return name;
        }

        public bool isAdmin()
        {
            return admin;
        }

        public User(string name, bool admin = false)
        {
            this.name = name;
            this.admin = admin;
        }
    }
}