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
    /// Interaction logic for PageTab.xaml
    /// </summary>
    public partial class PageTab : UserControl
    {
        private int id;
        private DateTime date;
        public PageTab(int id)
        {
            InitializeComponent();
            this.id = id;
/*            using (DataProvider dp = new DataProvider())
            {
                this.DataContext = dp.GetPage(id);
            }
*/        }

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            if (!changed.IsVisible)
                changed.Visibility = Visibility.Visible;
//            DataProvider dp = new DataProvider();
            
        }

        private void titleChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
