using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC_Forms.Model
{
    public class Field
    {
        public enum FieldType
        {
            Text, Checkbox, Dropdown
        };

        public Field(int Id, string Title, FieldType Type)
        {
            this.Id = Id;
            this.Title = Title;
            this.Type = Type;
        }

        //STD
        public int Id { get; set; }
        public string Title { get; set; }

        public FieldType Type;

        //Checkbox
        public bool CheckBox_DefaultChecked;

        //Dropdown
        public string[] Dropdown_Options;
    }
}