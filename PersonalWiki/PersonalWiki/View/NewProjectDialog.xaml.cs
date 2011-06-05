//2011 Sami Hostikka <sami@hostikka.com>
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

namespace PersonalWiki.View
{
    /// <summary>
    /// Interaction logic for NewPageDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : Window
    {
        public NewProjectDialog()
        {
            InitializeComponent();
        }

        #region commands
        /// <summary>
        /// Adds new project to database
        /// </summary>
        private void CreateNewPage(object sender, RoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                if (dp.addProject(title.Text))
                    this.DialogResult = true;
                else this.DialogResult = false;
        }

        /// <summary>
        /// If new page is cancelled set DialogResult = false in other words closes dialog
        /// </summary>
        private void CancelNewPage(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// CreateNewPage command can be executed if title != null
        /// </summary>
        private void createCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(title.Text))
                e.CanExecute = true;
        }
        #endregion
    }
}
