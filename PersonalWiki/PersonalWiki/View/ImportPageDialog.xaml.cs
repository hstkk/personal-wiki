using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace PersonalWiki.View
{
    /// <summary>
    /// Interaction logic for NewPageDialog.xaml
    /// </summary>
    public partial class ImportPageDialog : Window
    {
        #region initialize
        private string fileName,text;
        public ImportPageDialog()
        {
            InitializeComponent();
            using (DataProvider dp = new DataProvider())
                this.combobox.DataContext = dp.GetProjectsTree();
        }
        #endregion

        #region commands
        /// <summary>
        /// Imports new page to database
        /// </summary>
        private void importNewPage(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(combobox.SelectedValue.ToString(), out id)){
                try
                {
                    using (FileStream fstream = new FileStream(@fileName, FileMode.Open))
                    {
                        using (BufferedStream bstream = new BufferedStream(fstream))
                        {
                            if (bstream.CanRead)
                            {
                                using (StreamReader srdr = new StreamReader(bstream))
                                {
                                    text = srdr.ReadToEnd();
                                    if (!string.IsNullOrWhiteSpace(text))
                                    {
                                        using (DataProvider dp = new DataProvider())
                                            if (dp.importPage(title.Text,id,text))
                                            this.DialogResult = true;
                                    }
                                    else
                                        MessageBox.Show("Error, file is empty", "Error");
                                }
                            }
                            else
                                MessageBox.Show("Error, can't read file", "Error");
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error, can't read file", "Error");
                }
            }
        }

        /// <summary>
        /// Opens open dialog, if selected file exists sets it to fileName string, file textblock and prefills title textblock
        /// </summary>
        private void openExecuted(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "txt docs (.txt)|*.txt";
            dlg.ValidateNames = true;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            try
            {
                if (dlg.ShowDialog() == true)
                {
                    if (File.Exists(dlg.FileName))
                    {
                        fileName = dlg.FileName;
                        file.Text = dlg.FileName;
                        if (string.IsNullOrWhiteSpace(title.Text))
                            title.Text = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    }
                    else
                        MessageBox.Show("Error, file doesn't exist", "Error");
                }
            }
            catch (Exception err){ }
        }


        /// <summary>
        /// New page can be created if title != null and file is selected
        /// </summary>
        private void importNewPageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(title.Text) && !string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
                e.CanExecute = true;
        }

        /// <summary>
        /// If new page is cancelled set DialogResult = false in other words closes dialog
        /// </summary>
        private void CancelNewPage(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #endregion
    }
}
