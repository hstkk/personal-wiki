//2011 Sami Hostikka <sami@hostikka.com>
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
using System.Drawing.Printing;
using System.Drawing;

namespace PersonalWiki.Controller
{
    class ExportX
    {
        private string title, text;

        /// <summary>
        /// Creates an object that allows to export data
        /// </summary>
        /// <param name="title">filename</param>
        /// <param name="text">file content</param>
        public ExportX(string title, string text){
            this.title=title;
            this.text=text;
        }

        /// <summary>
        /// Creates txt file from given data
        /// </summary>
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

        /// <summary>
        /// Creates html5 file from given data
        /// </summary>
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

        /// <summary>
        /// Prints given data
        /// </summary>
        public void createPrint()
        {
            try
            {
                PrintDialog dlg = new PrintDialog();
                dlg.PageRangeSelection = PageRangeSelection.AllPages;
                dlg.UserPageRangeEnabled = true;
                if (dlg.ShowDialog().Equals(true))
                {
                    FixedDocument fixedDocument = new FixedDocument();
                    fixedDocument.DocumentPaginator.PageSize = new System.Windows.Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);
                    var pageContent = new PageContent();
                    fixedDocument.Pages.Add(pageContent);
                    var page = new FixedPage();
                    page.Width = fixedDocument.DocumentPaginator.PageSize.Width;
                    page.Height = fixedDocument.DocumentPaginator.PageSize.Height;
                    pageContent.Child = page;
                    TextBlock block = new TextBlock();
                    block.TextWrapping = TextWrapping.Wrap;
                    block.FontSize = Properties.Settings.Default.FontSize;
                    block.Inlines.Add(new Bold(new Run(title)));
                    block.Inlines.Add(new LineBreak());
                    block.Inlines.Add(new Run(text));
                    page.Children.Add(block);
                    dlg.PrintDocument(fixedDocument.DocumentPaginator, title);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error, can't print", "Error");
            }
        }
    }
}
