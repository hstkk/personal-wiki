using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Controls;

namespace PersonalWiki.Controller
{
    class ExportX
    {
        private string title, text;

        public ExportX(string title, string text){
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

            if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
            {
                try
                {
                    using (StreamWriter swrtr = new StreamWriter(dlg.OpenFile()))
                    {
                        swrtr.Write(text);
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error, can't save file", "Error");
                }
            }
        }

        public void createHtml()
        {
            var dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "html files|*.html";
            dlg.DefaultExt = "html";
            dlg.FileName = title;
            dlg.CheckPathExists = true;
            dlg.OverwritePrompt = true;
            dlg.ValidateNames = true;

            if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
            {
                try
                {
                    using (StreamWriter swrtr = new StreamWriter(dlg.OpenFile()))
                    {
                        string html = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"/><title>"+title+"</title><link rel=\"stylesheet\" href=\"http://blueprintcss.org/blueprint/src/typography.css\"/></head><body><header><h1>"+title+"</h1></header><article><pre>"+text+"</pre></article></body></html>";
                        swrtr.Write(html);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error, can't save file", "Error");
                }
            }
        }
        /*
        public void createXps()
        {
            var dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "xps files|*.xps";
            dlg.DefaultExt = "xps";
            dlg.FileName = title;
            dlg.CheckPathExists = true;
            dlg.OverwritePrompt = true;
            dlg.ValidateNames = true;

            if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
            {
                try
                {
                    Paragraph ptitle = new Paragraph(new Run(title));
                    Paragraph ptext = new Paragraph(new Run(text));
                    FlowDocument export;
                    using(FileStream xaml = File.OpenRead(@"..\View\Export.xaml")){
                        export = XamlReader.Load(xaml) as FlowDocument;
                    }
                    export.Blocks.Add(ptitle);
                    export.Blocks.Add(ptext);
                    FlowDocumentReader myFlowDocumentReader = new FlowDocumentReader();
                    myFlowDocumentReader.Document = export;



                }
                catch (Exception e)
                {
                    MessageBox.Show(e+"", "Error");
                    MessageBox.Show("Error, can't save file", "Error");
                }
            }
        }*/
    }
}
