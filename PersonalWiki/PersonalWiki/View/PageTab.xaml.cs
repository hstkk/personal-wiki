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
        /// When auto save timer has stopped, date textblock is updated and save method is called
        /// </summary>
        private void Save(object sender, EventArgs e)
        {
            timer.Stop();
            save();
            date.Text = DateTime.Now.ToString("HH:mm dd.MM.yyyy");
            unsaved.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// If title is changed starts auto save timer
        /// </summary>
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
        /// If text is changed starts auto save timer
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

        /// <summary>
        /// It seems that binding triggers title/textchanged event, this event is only to fix that problem. When title or text textbox got focus _gotFocus bool is set to true.
        /// </summary>
        private void titleTextGotFocus(object sender, RoutedEventArgs e)
        {
            _gotFocus = true;
        }

        /// <summary>
        /// When keyword is found, keyword is selected
        /// </summary>
        private void onFound(object sender, EventArgs e)
        {
            View.FindReplaceDialog dlg = (View.FindReplaceDialog)sender;
            text.Select(dlg.Index, dlg.Lenght);
            text.Focus();
        }

        /// <summary>
        /// When replace is executed, text textbox content is replaced with new content
        /// </summary>
        private void onReplaced(object sender, EventArgs e)
        {
            View.FindReplaceDialog dlg = (View.FindReplaceDialog)sender;
            text.Text = dlg.Text.Text;
        }
        #endregion

        #region commands
        /// <summary>
        /// shows RevisionsDialog, if dialog result returns true updates datacontext
        /// </summary>
        private void ShowRevisionsDialog(object sender, RoutedEventArgs e)
        {
            View.RevisionDialog dlg = new View.RevisionDialog(Id);
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
                using (DataProvider dp = new DataProvider())
                    this.DataContext = dp.GetPage(Id);
        }

        /// <summary>
        /// shows FindReplaceDialog and sets events between FindReplaceDialog and this
        /// </summary>
        private void ShowFindReplaceDialog(object sender, RoutedEventArgs e)
        {
            View.FindReplaceDialog dlg = new View.FindReplaceDialog(text);
            dlg.Found += new FoundEventHandler(onFound);
            dlg.Replaced += new ReplacedEventHandler(onReplaced);
            dlg.Show();
        }

        /// <summary>
        /// if text textbox is not null commands can be executed
        /// </summary>
        private void textCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (text.Text.Length > 0)
                e.CanExecute = true;
        }

        /// <summary>
        /// Revisions link can be executes
        /// </summary>
        private void ShowRevisionsCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                if (dp.GetRevisions(Id).Count > 1)
                    e.CanExecute = true;
        }

        /// <summary>
        /// when tab is on closing state, save method is executed
        /// </summary>
        public void Closing()
        {
            save();
        }

        /// <summary>
        /// Executes ExportX class createPrint method
        /// </summary>
        private void printExecuted(object sender, RoutedEventArgs e)
        {
            Controller.ExportX print = new Controller.ExportX(title.Text, text.Text);
            print.createPrint();
        }

        /// <summary>
        /// Executes ExportX class createTxt method
        /// </summary>
        private void exportTxtExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Controller.ExportX txt = new Controller.ExportX(title.Text, text.Text);
            txt.createTxt();
        }

        /// <summary>
        /// Executes ExportX class createHtml method
        /// </summary>
        private void exportHtmlExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Controller.ExportX html = new Controller.ExportX(title.Text, text.Text);
            html.createHtml();
        }
        #endregion

        /// <summary>
        /// If title is changed, updates it to database. If text is changed, creates new revision to database.
        /// </summary>
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
    }
}
