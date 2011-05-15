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
            DatabasePath.Path = @"C:\Users\Sami\Desktop\työnalla\Database.sdf";
            #endregion
            //todo:check if sql server ce is installed
            //todo:refresh treeView
            //todo:database and tabs from last time from ini file
            using (DataProvider dp = new DataProvider())
            {
                this.ProjectsTreeView.DataContext = dp.GetProjectsTree();
            }
        }

        /// <summary>
        /// Opens about tab
        /// </summary>
        private void ShowAboutTab(object sender, RoutedEventArgs e)
        {
            if (!TabIsOpen("About"))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = "About", Content = new View.AboutTab() });
        }

        private void ShowPageTab(object sender, ExecutedRoutedEventArgs e)
        {
            int id;
            int.TryParse(e.Parameter.ToString(), out id);
            e.Parameter.ToString();
            string header;
            using (DataProvider dp = new DataProvider())
            {
                header = dp.GetPageTabHeader(7);
            }
            if (!string.IsNullOrWhiteSpace(header) && !TabIsOpen(header))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = /*header*/id, Content = new View.PageTab(id)});
        }

        /// <summary>
        /// Opens new page tab
        /// </summary>
        private void ShowNewPageTab(object sender, RoutedEventArgs e)
        {
            if (!TabIsOpen("New page"))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = "New page", Content = new View.NewPageTab() });
        }

        /// <summary>
        /// Opens new project tab
        /// </summary>
        private void ShowNewProjectTab(object sender, RoutedEventArgs e)
        {
            if (!TabIsOpen("New project"))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = "New project", Content = new View.NewProjectTab() });
        }

        private void CloseTab(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = e.Source as TabItem;
            if (tabItem != null)
                //todo:error
                this.tabControl.Items.Remove(tabItem);
        }

        /// <summary>
        /// Checks if tab is already open
        /// </summary>
        /// <param name="name">Tab name</param>
        /// <returns>false if tab is already open, else true</returns>
        private bool TabIsOpen(string name)
        {
            bool open = true;
            var q = from TabItem t in tabControl.Items
                    where t.Header.Equals(name)
                    select t;
            if (q.Count().Equals(0))
                open = false;
            else
                MessageBox.Show(this,name+" is already open!","Warning!");
            return open;
        }
    }
}
