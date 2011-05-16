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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace PersonalWiki.View
{
    /// <summary>
    /// Interaction logic for FindReplaceDialog.xaml
    /// </summary>
    public partial class FindReplaceDialog : Window
    {
        #region initialize
        private MatchCollection matches;
        private int i;
        public int Index { get; set; }
        public int Lenght { get; set; }
        public TextBox Text { get; set; }
        public event FoundEventHandler Found;
        public event ReplacedEventHandler Replaced;

        public FindReplaceDialog(TextBox text)
        {
            InitializeComponent();
            Text = text;
            i = 0;
            Index = 0;
        }
        #endregion

        #region events
        /// <summary>
        /// Invoke Found event
        /// </summary>
        protected virtual void onFound()
        {
            if (Found != null)
                Found(this, EventArgs.Empty);
        }
        /// <summary>
        /// Invoke Replaced event
        /// </summary>
        protected virtual void onReplaced()
        {
            if (Replaced != null)
                Replaced(this, EventArgs.Empty);
        }
        #endregion

        #region commands
        private void SearchExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (matches.Equals(null))
            {
                if (searchCase.IsChecked.Equals(false))
                    matches = Regex.Matches(@Text.Text, @search.Text, RegexOptions.IgnoreCase);
                else
                    matches = Regex.Matches(@Text.Text, @search.Text);
            }

            if (Index.Equals(matches.Count))
                Index = 0;

            if (Found != null)
            {
                Index = matches[i].Index;
                Lenght = matches[i].Length;
                onFound();
            }
            i++;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SearchCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (search.Text.Length != 0)
                e.CanExecute = true;
        }

        private void ReplaceExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (replaceCase.IsChecked.Equals(false))
                    Text.Text = Regex.Replace(@Text.Text, @find.Text, @replace.Text, RegexOptions.IgnoreCase);
                else
                    Text.Text = Regex.Replace(@Text.Text, @find.Text, @replace.Text);
                onReplaced();
            }
            catch (Exception err) { }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReplaceCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (find.Text.Length != 0 && replace.Text.Length != 0)
                e.CanExecute = true;
        }

        #endregion
    }
}
