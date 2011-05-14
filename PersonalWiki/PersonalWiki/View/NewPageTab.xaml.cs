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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalWiki.View
{
    /// <summary>
    /// Interaction logic for NewPageTab.xaml
    /// </summary>
    public partial class NewPageTab : UserControl
    {
        public NewPageTab()
        {
            InitializeComponent();
            using (DataProvider dp = new DataProvider())
            {
                this.combobox.DataContext = dp.GetProjectsTree();
            }
        }
    }
}
