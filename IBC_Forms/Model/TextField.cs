using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC_Forms.Model
{
    public class TextField : Field
    {
        public TextField(int Id, string Title) : base (Id, Title, FieldType.Text)
        {

        }
    }
}