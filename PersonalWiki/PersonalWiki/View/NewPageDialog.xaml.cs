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
        public NewPageDialog()
        {
            InitializeComponent();
            using (DataProvider dp = new DataProvider())
                this.combobox.DataContext = dp.GetProjectsTree();
        }

        private void CreateNewPage(object sender, RoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                dp.addPage("asd", 3);
            this.DialogResult = true;
        }

        /// <summary>
        /// If new page is cancelled set DialogResult = false in other words closes dialog
        /// </summary>
        private void CancelNewPage(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
