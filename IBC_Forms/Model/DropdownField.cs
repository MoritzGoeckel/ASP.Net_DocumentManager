using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC_Forms.Model
{
    public class DropdownField : Field
    {
        public DropdownField(int Id, string Title, string[] Options) : base (Id, Title, FieldType.Dropdown)
        {
            this.Dropdown_Options = Options;
        }
    }
}