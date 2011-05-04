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
using Microsoft.Windows.Controls.Ribbon;

namespace PersonalWiki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            #region testPath
            DatabasePath.Path = @"C:\Users\Sami\Desktop\Database.sdf.test";
            #endregion
            //todo:check if sql server ce is installed
            //todo:refresh treeView
            //todo:database and tabs from last time from ini file
            using (DataProvider dp = new DataProvider())
            {
                this.ProjectsTreeView.DataContext = dp.GetProjects();
            }
        }

        //todo: else aktivoi välilehti
        private void ShowAboutTab(object sender, RoutedEventArgs e)
        {
            if (!TabIsOpen("About"))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = "About", Content = new View.AboutTab() });
        }

        private void ShowPageTab(object sender, RoutedEventArgs e)
        {
            string header;
            using (DataProvider dp = new DataProvider())
            {
                header = dp.GetPageTabHeader(7);
            }
            if (!string.IsNullOrWhiteSpace(header) && !TabIsOpen(header))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = header, Content = new View.PageTab(7) });
//            e.RoutedEvent.
            //            e.Parameter.ToString();
    //        this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = hl.CommandParameter.ToString()});
        }

/*        private void ShowPageTab(int id)
        {
            this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = id });
        }*/

        //todo: else aktivoi välilehti
        private void ShowNewPageTab(object sender, RoutedEventArgs e)
        {
            if (!TabIsOpen("New page"))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = "New page", Content = new View.NewPageTab() });
        }

        //todo: else aktivoi välilehti
        private void ShowNewProjectTab(object sender, RoutedEventArgs e)
        {
            if (!TabIsOpen("New project"))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = "New project", Content = new View.NewProjectTab() });
        }

        private bool TabIsOpen(string name)
        {
            bool open = true;
            var q = from TabItem t in tabControl.Items
                    where t.Header.Equals(name)
                    select t;
            if (q.Count().Equals(0))
                open = false;
            return open;
        }
    }
}
