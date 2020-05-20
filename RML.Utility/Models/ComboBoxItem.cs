using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RML.Utility.Models
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public bool Value { get; set; }
         
    }



    public class AuthTypeListItems : BindingList<ComboBoxItem>
    {
        public AuthTypeListItems()
        {
            base.Add(new ComboBoxItem { Text = "Windows Authentication", Value = true });
            base.Add(new ComboBoxItem { Text = "SQL Server Authentication", Value = false });
        }
    }
}
