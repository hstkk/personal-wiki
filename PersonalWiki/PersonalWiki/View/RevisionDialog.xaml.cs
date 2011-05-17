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

namespace PersonalWiki.View
{
    /// <summary>
    /// Interaction logic for RevisionDialog.xaml
    /// </summary>
    public partial class RevisionDialog : Window
    {
        #region initialize
        private int id;
        public RevisionDialog(int id)
        {
            InitializeComponent();
            this.id = id;
            using (DataProvider dp = new DataProvider())
            {
                title.DataContext = dp.GetPageTabHeader(id);
                dataGrid.DataContext = dp.GetRevisions(id);
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
        /// Saves selected revision as current revision
        /// </summary>
        private void saveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Revision selectedRevision = (Model.Revision)dataGrid.SelectedItem;
            if (selectedRevision != null && selectedRevision.Date != null)
            {
                using (DataProvider dp = new DataProvider())
                {
                    if (dp.addRevision(id, selectedRevision.Text))
                        this.DialogResult = true;
                    else
                        this.DialogResult = false;
                }
            }
        }

        /// <summary>
        /// Revision save can be done if revision is selected on datagrid
        /// </summary>
        private void saveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex > -1)
                e.CanExecute = true;
        }
        #endregion
    }
}
