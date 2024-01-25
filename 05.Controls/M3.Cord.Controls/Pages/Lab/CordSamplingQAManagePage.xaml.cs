#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NLib.Models;
using NLib.Services;
using M3.Cord.Models;
using System.Windows.Interop;
using NLib.Wpf.Controls;
using System.Diagnostics;
using System.Reflection;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for CordSamplingQAManagePage.xaml
    /// </summary>
    public partial class CordSamplingQAManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordSamplingQAManagePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordSamplingDetails> items = null;

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.QAMenu;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as CordSamplingDetails;
            EditItem(item);
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

        public void EditItem(CordSamplingDetails item)
        {
            var page = M3CordApp.Pages.CordSamplingQA;
            page.Setup(item);
            PageContentManager.Instance.Current = page;
        }

        private void RefreshGrid()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            grid.ItemsSource = null;

            var date = dtDate.SelectedDate;
            var lotNo = (string.IsNullOrEmpty(txtLotNo.Text)) ? null : txtLotNo.Text;

            try
            {
                items = CordSamplingDetails.GetQAs(date, lotNo).Value();
            }
            catch (Exception ex) 
            {
                med.Err(ex);
                items = new List<CordSamplingDetails>();
            }

            grid.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            dtDate.SelectedDate = DateTime.Today;
            txtLotNo.Text = string.Empty;
            RefreshGrid();
        }

        #endregion
    }
}
