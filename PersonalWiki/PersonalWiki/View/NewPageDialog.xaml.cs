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
    public partial class NewPageDialog : Window
    {
        #region initialize
        public NewPageDialog()
        {
            InitializeComponent();
            using (DataProvider dp = new DataProvider())
                this.combobox.DataContext = dp.GetProjectsTree();
        }
        #endregion

        #region commands
        /// <summary>
        /// Adds new page to database
        /// </summary>
        private void CreateNewPage(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(combobox.SelectedValue.ToString(), out id)){
                using (DataProvider dp = new DataProvider())
                    if (dp.addPage(title.Text, id))
                        this.DialogResult = true;
                    else
                        this.DialogResult = false;
            }
        }

        /// <summary>
        /// If new page is cancelled set DialogResult = false in other words closes dialog
        /// </summary>
        private void CancelNewPage(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// New page can be created if title != null
        /// </summary>
        private void createNewPageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(title.Text))
                e.CanExecute = true;
        }
        #endregion
    }
}
