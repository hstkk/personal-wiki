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
using System.ComponentModel;
using System.Windows.Threading;
using System.Drawing.Printing;

namespace PersonalWiki.View
{
    public delegate void FoundEventHandler(object sender, EventArgs e);
    public delegate void ReplacedEventHandler(object sender, EventArgs e);

    /// <summary>
    /// Interaction logic for PageTab.xaml
    /// </summary>
    public partial class PageTab : UserControl
    {
        #region initialize
        public int Id { get; set; }
        public int ProjectId {
            get{
                using (DataProvider dp = new DataProvider())
                    return dp.GetProjectId(Id);
            } 
        }
        private bool _textChanged, _titleChanged, _gotFocus;
        private DispatcherTimer timer;

        public PageTab(int id)
        {
            InitializeComponent();
            this.Id = id;
            using (DataProvider dp = new DataProvider())
                this.DataContext = dp.GetPage(Id);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += new EventHandler(Save);
            _textChanged = false;
            _titleChanged = false;
            _gotFocus = false;
        }
        #endregion

        #region events
        /// <summary>
        /// 
        /// </summary>
        private void Save(object sender, EventArgs e)
        {
            timer.Stop();
            save();
            date.Text = DateTime.Now.ToString("HH:mm dd.MM.yyyy");
            unsaved.Visibility = Visibility.Hidden;
        }

        private void titleChanged(object sender, TextChangedEventArgs e)
        {
            if (_gotFocus && !_titleChanged && !string.IsNullOrWhiteSpace(title.Text))
            {
                _titleChanged = true;
                unsaved.Visibility = Visibility.Visible;
                timer.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void textChanged(object sender, TextChangedEventArgs e)
        {
            if (_gotFocus && !_textChanged)
            {
                _textChanged = true;
                unsaved.Visibility = Visibility.Visible;
                timer.Start();
            }
        }

        //binding aiheuttaa turhaan textchanged eventin sen ehkäisemiseksi
        /// <summary>
        /// 
        /// </summary>
        private void titleTextGotFocus(object sender, RoutedEventArgs e)
        {
            _gotFocus = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void onFound(object sender, EventArgs e)
        {
            View.FindReplaceDialog dlg = (View.FindReplaceDialog)sender;
            text.Select(dlg.Index, dlg.Lenght);
            text.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        private void onReplaced(object sender, EventArgs e)
        {
            View.FindReplaceDialog dlg = (View.FindReplaceDialog)sender;
            text.Text = dlg.Text.Text;
        }
        #endregion

        #region commands
        private void ShowRevisionsDialog(object sender, RoutedEventArgs e)
        {
            View.RevisionDialog dlg = new View.RevisionDialog(Id);
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
                using (DataProvider dp = new DataProvider())
                    this.DataContext = dp.GetPage(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowFindReplaceDialog(object sender, RoutedEventArgs e)
        {
            View.FindReplaceDialog dlg = new View.FindReplaceDialog(text);
            dlg.Found += new FoundEventHandler(onFound);
            dlg.Replaced += new ReplacedEventHandler(onReplaced);
            dlg.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        private void textCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (text.Text.Length > 0)
                e.CanExecute = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowRevisionsCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                if (dp.GetRevisions(Id).Count > 1)
                    e.CanExecute = true;
        }
        #endregion

        private void save()
        {
            if (_titleChanged && !string.IsNullOrWhiteSpace(title.Text))
                using (DataProvider dp = new DataProvider())
                    dp.updateTitle(Id, title.Text);
            if (_textChanged)
                using (DataProvider dp = new DataProvider())
                    dp.addRevision(Id, text.Text);
            _titleChanged = false;
            _textChanged = false;
        }

        public void Closing()
        {
            save();
        }

        private void printExecuted(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            dlg.PageRangeSelection = PageRangeSelection.AllPages;
            dlg.UserPageRangeEnabled = true;
            if(dlg.ShowDialog().Equals(true))
            {
                PrintDocument doc = new PrintDocument();
                dlg.PrintVisual(text, title.Text);
            }
        }

        private void exportTxtExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Controller.ExportX txt = new Controller.ExportX(title.Text, text.Text);
            txt.createTxt();
        }

        private void exportHtmlExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Controller.ExportX html = new Controller.ExportX(title.Text, text.Text);
            html.createHtml();
        }
    }
}
