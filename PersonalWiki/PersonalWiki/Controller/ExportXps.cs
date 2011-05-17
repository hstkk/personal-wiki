using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Microsoft.Win32;

namespace PersonalWiki.Controller
{
    class ExportXps
    {
        private string title, text;

        public void ExportXps(string title, string text){
            this.title=title;
            this.text=text;
        }

        private void createXps()
        {
            var dlg = new SaveFileDialog
            {
                FileName = title,
                DefaultExt = "xps",
                Filter = "XPS docs|*.xps",
                AddExtension = true
            };

            if (dlg.ShowDialog() == true)
            {
                using (XpsDocument xps = new XpsDocument(dlg.FileName, System.IO.FileAccess.Write, CompressionOption.Fast))
                {
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xps);
                    writer.Write(text);
                }
            }
        }
    }
}
