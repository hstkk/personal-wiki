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
        public int Id { get; set; }
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
            Id = -1;
        }

        /// <summary>
        /// if datagrid selection changes call onFound method
        /// </summary>
        private void dataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Id = ((PageResult2)dataGrid.SelectedItem).Id;
                if (Id > 0)
                    onFound();
            }
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

        /// <summary>
        /// If find is executed sets datagrid datacontext
        /// </summary>
        private void findExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            using (DataProvider dp = new DataProvider())
                dataGrid.DataContext = dp.FindPage(search.Text);
        }

        /// <summary>
        /// if keyword != null find can be executed
        /// </summary>
        private void findCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(search.Text))
                e.CanExecute = true;
        }
        #endregion
    }
}
