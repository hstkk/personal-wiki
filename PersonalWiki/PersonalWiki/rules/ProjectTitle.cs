using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;
using System.Windows;

namespace PersonalWiki.rules
{
    class ProjectTitle : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            MessageBox.Show("RULE", "Warning!");
            string title = value.ToString();
            if(!string.IsNullOrWhiteSpace(title) && title.Length<=100){
                /*using (*/
                DataProvider dp = new DataProvider();//)
                if (!dp.ProjectExists(title))
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Please enter valid title! "+title+" might already exists.");
        }
    }
}
