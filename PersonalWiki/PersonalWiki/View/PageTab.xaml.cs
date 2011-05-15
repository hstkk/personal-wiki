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
    /// <summary>
    /// Interaction logic for PageTab.xaml
    /// </summary>
    public partial class PageTab : UserControl
    {
        private int id;
        private bool _textChanged, _titleChanged, _gotFocus;
        DispatcherTimer timer;

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

        /// <summary>
        /// 
        /// </summary>
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

        private void ShowRevisionsDialog(object sender, RoutedEventArgs e)
        {

        }

        //binding aiheuttaa turhaan textchanged eventin sen ehkäisemiseksi
        /// <summary>
        /// 
        /// </summary>
        private void titleTextGotFocus(object sender, RoutedEventArgs e)
        {
            _gotFocus = true;
        }
    }
}
