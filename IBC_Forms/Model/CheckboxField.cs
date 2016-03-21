using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC_Forms.Model
{
    public class CheckboxField : Field
    {
        public CheckboxField(int Id, string Title, bool DefaultChecked) : base (Id, Title, FieldType.Checkbox)
        {
            this.CheckBox_DefaultChecked = DefaultChecked;
        }
    }
}