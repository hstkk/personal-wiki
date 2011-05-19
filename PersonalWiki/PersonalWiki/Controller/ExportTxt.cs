using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Microsoft.Win32;
using System.Windows;

namespace PersonalWiki.Controller
{
    class ExportTxt
    {
        private string title, text;

        public ExportTxt(string title, string text){
            this.title=title;
            this.text=text;
        }

        public void createTxt()
        {
            var dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "txt files|*.txt";
            dlg.DefaultExt = "txt";
            dlg.FileName = title;
            dlg.CheckPathExists = true;
            dlg.OverwritePrompt = true;
            dlg.ValidateNames = true;

            if (dlg.ShowDialog() == true)
            {
                try
                {

                }
                catch(Exception e)
                {
                    MessageBox.Show("Error, can't save file", "Error");
                }
            }
        }
    }
}
