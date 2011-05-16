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
        private int id;
        private bool _textChanged, _titleChanged, _gotFocus;
        private DispatcherTimer timer;

        //todo:suljettaessa tarkista onko tallennettu, 
        public PageTab(int id)
        {
            InitializeComponent();
            this.id = id;
            using (DataProvider dp = new DataProvider())
                this.DataContext = dp.GetPage(id);
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
            if (_titleChanged)
                using (DataProvider dp = new DataProvider())
                    dp.updateTitle(id, title.Text);
            if (_textChanged)
                using (DataProvider dp = new DataProvider())
                    dp.addRevision(id, text.Text);
            date.Text = DateTime.Now.ToString("HH:mm dd.MM.yyyy");
            _titleChanged = false;
            _textChanged = false;
        }

        //todo:validation
        private void titleChanged(object sender, TextChangedEventArgs e)
        {
            if (_gotFocus && !_titleChanged && !string.IsNullOrWhiteSpace(title.Text))
            {
                _titleChanged = true;
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

        private void onFound(object sender, EventArgs e)
        {
            View.FindReplaceDialog dlg = (View.FindReplaceDialog)sender;
            text.Select(dlg.Index, dlg.Lenght);
        }

        private void onReplaced(object sender, EventArgs e)
        {
            View.FindReplaceDialog dlg = (View.FindReplaceDialog)sender;
            text.Text = dlg.Text.Text;
        }
        #endregion

        #region commands
        private void ShowRevisionsDialog(object sender, RoutedEventArgs e)
        {
            View.RevisionDialog dlg = new View.RevisionDialog(id);
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
                using (DataProvider dp = new DataProvider())
                    this.DataContext = dp.GetPage(id);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowFindReplaceDialog(object sender, RoutedEventArgs e)
        {
            View.FindReplaceDialog dlg = new View.FindReplaceDialog(text);
            dlg.Found += new FoundEventHandler(onFound);
            dlg.Replaced += new FoundEventHandler(onReplaced);
            dlg.Show();
            if (dlg.DialogResult == true)
                text = dlg.Text;
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
                if (dp.GetRevisions(id).Count != 0)
                    e.CanExecute = true;
        }
        #endregion
    }
}
