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
using System.Collections;

namespace PersonalWiki
{
    /// <summary>
    /// Interaction logic for RevisionDialog.xaml
    /// </summary>
    public partial class FindDialog : Window
    {
        #region initialize
        private int id;
        public List<int> pages { get; set; }
        public event FoundEventHandler Found;
        public FindDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region events
        /// <summary>
        /// Invoke Found event
        /// </summary>
        public virtual void onFound()
        {
            if (Found != null)
                Found(this, EventArgs.Empty);
        }

        /// <summary>
        ///  If text on search box changes set list to null
        /// </summary>
        private void searchTextChanged(object sender, TextChangedEventArgs e)
        {
            pages = null;
        }

        private void dataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (PageResult2 p in dataGrid.SelectedItems)
                pages.Add(p.Id);
            onFound();
        }
        #endregion
        #region commands
        /// <summary>
        /// Closes dialog (returns DialogResult=false)
        /// </summary>
        private void closeExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #endregion
    }
}
