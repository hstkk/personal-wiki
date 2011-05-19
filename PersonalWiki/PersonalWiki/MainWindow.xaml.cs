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
using System.IO;

namespace PersonalWiki
{
    public delegate void FoundEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private bool canUseDatabase = false;
        public MainWindow()
        {
            InitializeComponent();

            font.DataContext = Fonts.SystemFontFamilies;
            font.SelectedValue = Properties.Settings.Default.Font;
            size.DataContext = new int[] { 8, 9, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            size.SelectedItem = Properties.Settings.Default.FontSize;
            /*language.DataContext = new string[] { "en-US", "fr-FR", "es-ES", "de-DE" };
            language.SelectedValue = Properties.Settings.Default.SpellCheckLanguage;*/
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.DatabasePath) && File.Exists(Properties.Settings.Default.DatabasePath))
                canUseDatabase = true;
            else
                MessageBox.Show("Select database", "Notice");
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Tabs))
                {
                    foreach (string sid in Properties.Settings.Default.Tabs.Trim().Split(' '))
                    {
                        int id;
                        if (int.TryParse(sid, out id))
                            addPageTab(id, false);
                    }
                    Properties.Settings.Default.Tabs = null;
                    Properties.Settings.Default.Save();
                }
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
            if (canUseDatabase && !TabIsOpen(id, showMessageBox))
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
            if(canUseDatabase)
                using (DataProvider dp = new DataProvider())
                   this.ProjectsTreeView.DataContext = dp.GetProjectsTree();
        }

        /// <summary>
        /// If projects does't exist can't create new page
        /// </summary>
        private void ShowNewPageDialogCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (canUseDatabase)
                using (DataProvider dp = new DataProvider())
                    e.CanExecute = dp.ProjectExists();
        }

        /// <summary>
        /// Quick n dirty autosave on closing
        /// </summary>
        private void MainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                StringBuilder ids = new StringBuilder();
                foreach (TabItem t in tabControl.Items)
                    if (t.Header.ToString() != "About")
                    {
                        View.PageTab p = ((View.PageTab)t.Content);
                        ids.Append(p.Id + " ");
                        p.Closing();
                    }
                Properties.Settings.Default.Tabs = ids.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception err) { }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeletePageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (canUseDatabase)
            {
                int id;
                if (MessageBox.Show("Do you want to remove this page?", "Remove", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes && int.TryParse(e.Parameter.ToString(), out id))
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
        }

        /// <summary>
        /// 
        /// </summary>
        private void DeleteProjectExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (canUseDatabase)
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
            int fsize;
            if (size.SelectedIndex != -1 && int.TryParse(size.SelectedItem.ToString(), out fsize))
            {
                Properties.Settings.Default.FontSize = fsize;
                Properties.Settings.Default.Save();
            }
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
            if (canUseDatabase)
            {
                using (DataProvider dp = new DataProvider())
                    if (dp.RevisionsExists())
                        e.CanExecute = true;
            }
        }

        private void wrapClicked(object sender, RoutedEventArgs e)
        {
            bool w = false;
            if (wrap.IsChecked == true)
                w = true;
            Properties.Settings.Default.TextWrap = w;
            Properties.Settings.Default.Save();
        }

        private void spellcheckClicked(object sender, RoutedEventArgs e)
        {
            bool s = false;
            if (spellcheck.IsChecked == true)
                s = true;
            Properties.Settings.Default.SpellCheck = s;
            Properties.Settings.Default.Save();
        }

        /*private void languageSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (language.SelectedIndex != -1)
            {
                Properties.Settings.Default.SpellCheckLanguage = language.SelectedItem.ToString();
                Properties.Settings.Default.Save();
            }
        }*/

        private void fontSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (font.SelectedIndex != -1)
            {
                Properties.Settings.Default.Font = font.SelectedItem.ToString();
                Properties.Settings.Default.Save();
            }
        }
    }
}
