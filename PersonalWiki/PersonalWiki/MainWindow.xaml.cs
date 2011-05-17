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
            DatabasePath.Path = @"C:\Users\Sami\Desktop\työnalla\Database – empty.sdf";
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
            if (int.TryParse(e.Parameter.ToString(), out id))
            {
                string header;
                using (DataProvider dp = new DataProvider())
                    header = dp.GetPageTabHeader(id);
                if (!string.IsNullOrWhiteSpace(header) && !TabIsOpen(header))
                    this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = header, Content = new View.PageTab(id) });
            }
        }

        /// <summary>
        /// Shows NewPageDialog, if dialog result is true calls refrefsProjectsTreeview method
        /// </summary>
        private void ShowNewPageDialog(object sender, RoutedEventArgs e)
        {
            View.NewPageDialog dlg = new View.NewPageDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
                refreshProjectsTreeview();
        }

        /// <summary>
        /// Shows NewProjectDialog, if dialog result is true calls refrefsProjectsTreeview method
        /// </summary>
        private void ShowNewProjectDialog(object sender, RoutedEventArgs e)
        {
            View.NewProjectDialog dlg = new View.NewProjectDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
                refreshProjectsTreeview();
        }

        private void CloseTab(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = e.Source as TabItem;
            if (tabItem != null)
            {
                if (tabItem.Header.ToString() != "About")
                    ((View.PageTab)tabItem.Content).Closing();
                //todo:error
                this.tabControl.Items.Remove(tabItem);
            }
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

        /// <summary>
        /// Updates ProjectsTreeview DataContext
        /// </summary>
        private void refreshProjectsTreeview()
        {
            using (DataProvider dp = new DataProvider())
            {
                this.ProjectsTreeView.DataContext = dp.GetProjectsTree();
            }
        }

        /// <summary>
        /// If projects does't exist can't create new page
        /// </summary>
        private void ShowNewPageDialogCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                e.CanExecute=dp.ProjectExists();
        }

        /// <summary>
        /// Quick n dirty autosave on closing
        /// </summary>
        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (TabItem t in tabControl.Items)
                if (t.Header.ToString() != "About")
                    ((View.PageTab)t.Content).Closing();
        }
    }
}
