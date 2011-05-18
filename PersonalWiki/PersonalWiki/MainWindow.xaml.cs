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
    public delegate void FoundEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //todo:check if sql server ce is installed
            //todo:database and tabs from last time from ini file
            font.DataContext = Fonts.SystemFontFamilies;
            refreshProjectsTreeview();
        }

        /// <summary>
        /// Opens about tab
        /// </summary>
        private void ShowAboutTab(object sender, RoutedEventArgs e)
        {
            if (!TabIsOpen(-1, true))
                this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = "About", Content = new View.AboutTab() });
        }

        private void ShowPageTab(object sender, ExecutedRoutedEventArgs e)
        {
            int id;
            if (int.TryParse(e.Parameter.ToString(), out id))
                addPageTab(id,true);
        }

        private void addPageTab(int id, bool showMessageBox)
        {
            if (!TabIsOpen(id, showMessageBox))
            {
                string header;
                using (DataProvider dp = new DataProvider())
                    header = dp.GetPageTabHeader(id);
                if (!string.IsNullOrWhiteSpace(header))
                    this.tabControl.SelectedIndex = this.tabControl.Items.Add(new TabItem { Header = header, Content = new View.PageTab(id) });
            }
        }

        /// <summary>
        /// Shows NewPageDialog, if dialog result is true calls refreshProjectsTreeview method
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
        /// Shows NewProjectDialog, if dialog result is true calls refreshProjectsTreeview method
        /// </summary>
        private void ShowNewProjectDialog(object sender, RoutedEventArgs e)
        {
            View.NewProjectDialog dlg = new View.NewProjectDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
                refreshProjectsTreeview();
        }

        /// <summary>
        /// Shows ImportPageDialog, if dialog result is true calls refreshProjectsTreeview method
        /// </summary>
        private void importExecuted(object sender, RoutedEventArgs e)
        {
            View.ImportPageDialog dlg = new View.ImportPageDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
                refreshProjectsTreeview();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseTab(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = e.Source as TabItem;
            if (tabItem != null)
            {
                try
                {
                    if (tabItem.Header.ToString() != "About")
                        ((View.PageTab)tabItem.Content).Closing();
                    tabControl.Items.Remove(tabItem);
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error can't close tab", "Error!");
                }
            }
        }

        /// <summary>
        /// Checks if tab is already open
        /// </summary>
        /// <param name="id">Pages id, about tabs id = -1</param>
        /// <returns>false if tab is already open, else true</returns>
        private bool TabIsOpen(int id, bool showMessageBox)
        {
            bool open = true;
            int count = -1;
            try
            {
                if (id == -1)
                {
                    var q = from TabItem t in tabControl.Items
                            where t.Header.Equals("About")
                            select t;
                    count = q.Count();
                }
                else
                {
                    var q = from TabItem t in tabControl.Items
                            where ((View.PageTab)t.Content).Id == id
                            select t;
                    count = q.Count();
                    
                }
                if (count < 1 )
                    open = false;
                else if (showMessageBox)
                    MessageBox.Show(this,"Tab is already open!", "Warning!");
            }
            catch (Exception e){ }
            return open;
        }

        /// <summary>
        /// Updates ProjectsTreeview DataContext
        /// </summary>
        private void refreshProjectsTreeview()
        {
            using (DataProvider dp = new DataProvider())
               this.ProjectsTreeView.DataContext = dp.GetProjectsTree();
        }

        /// <summary>
        /// If projects does't exist can't create new page
        /// </summary>
        private void ShowNewPageDialogCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                e.CanExecute = dp.ProjectExists();
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

        /// <summary>
        /// 
        /// </summary>
        private void DeletePageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            int id;
            if (MessageBox.Show("Do you want to remove this page?", "Remove", MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes && int.TryParse(e.Parameter.ToString(), out id))
            {
                ProjectsTreeView.IsEnabled = false;
                ProjectsTreeView.Visibility = System.Windows.Visibility.Hidden;
                try
                {
                    if (tabControl.Items.Count > 0)
                    {
                        var q = from TabItem t in tabControl.Items
                                where t.Header.ToString() != "About" &&
                                ((View.PageTab)t.Content).Id == id
                                select t;
                        tabControl.Items.Remove(q.Single());
                    }
                }
                catch (Exception err) { }
                using (DataProvider dp = new DataProvider())
                    if (dp.deletePage(id))
                        refreshProjectsTreeview();
                ProjectsTreeView.IsEnabled = true;
                ProjectsTreeView.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeleteProjectExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            int id;
            if (MessageBox.Show("Do you want to remove this project?", "Remove", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes && int.TryParse(e.Parameter.ToString(), out id))
            {
                ProjectsTreeView.IsEnabled = false;
                try
                {
                    if (tabControl.Items.Count > 0)
                    {
                        List<PageResult> pages;
                        using (DataProvider dp = new DataProvider())
                            pages = dp.GetPages(id).ToList<PageResult>();
                        foreach (PageResult page in pages)
                        {
                            var q = from TabItem t in tabControl.Items
                                    where t.Header.ToString() != "About" &&
                                    ((View.PageTab)t.Content).Id == page.Id
                                    select t;
                            tabControl.Items.Remove(q.Single());
                        }
                    }
                }
                catch (Exception err) { }
                using (DataProvider dp = new DataProvider())
                    if (dp.deleteProject(id))
                        refreshProjectsTreeview();
                ProjectsTreeView.IsEnabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void pageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            int id;
            if (int.TryParse(e.Parameter.ToString(), out id))
                e.CanExecute = true;
        }

        private void sizeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.FontSize = size.SelectedItem.ToString();
            Properties.Settings.Default.Save();
            MessageBox.Show(Properties.Settings.Default["FontSize"].ToString());
        }

        private void onFound(object sender, EventArgs e)
        {
            FindDialog dlg = (FindDialog)sender;
            addPageTab(dlg.Id,false);
        }

        private void findExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            FindDialog dlg = new FindDialog();
            dlg.Found += new FoundEventHandler(onFound);
            dlg.ShowDialog();
        }

        private void findCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                if (dp.RevisionsExists())
                    e.CanExecute = true;
        }
    }
}
