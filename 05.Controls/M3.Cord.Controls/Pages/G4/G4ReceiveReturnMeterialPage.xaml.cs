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
    /// Interaction logic for G4ReceiveReturnMeterialPage.xaml
    /// </summary>
    public partial class G4ReceiveReturnMeterialPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G4ReceiveReturnMeterialPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordItemYarn> itemYarns = null;
        private List<YarnGrade> yarnGrades = null;
        private G4YarnReturn current = null;

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
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region TextBox Handlers

        private void txtTraceNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                this.InvokeAction(() => 
                {
                    txtLotNo.FocusControl(); // focus
                });
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtTraceNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtTraceNo_LostFocus(object sender, RoutedEventArgs e)
        {
            ReceivedTraceNo();
        }

        #endregion

        #region Private Methods

        private void LoadComboBox()
        {
            // Grade
            cbGrades.ItemsSource = null;
            yarnGrades = YarnGrade.Gets();
            cbGrades.ItemsSource = yarnGrades;

            this.InvokeAction(() =>
            {
                cbGrades.SelectedIndex = -1;
            });

            // Item Yarn
            cbItemYanrs.ItemsSource = null;
            itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;

            this.InvokeAction(() =>
            {
                cbItemYanrs.SelectedIndex = -1;
            });
        }

        private void UpdateCurrentInfo()
        {
            paCurrent.DataContext = null;

            if (string.IsNullOrWhiteSpace(txtTraceNo.Text))
            {
                DisableControls();
                return;
            }

            if (null != current)
            {
                paCurrent.DataContext = current;
                // Item Yarn
                int idx = -1;
                idx = itemYarns.FindIndex(x => x.ItemYarn == current.ItemYarn);
                if (idx > -1) 
                    cbItemYanrs.SelectedIndex = idx;
                else cbItemYanrs.SelectedIndex = -1;

                // Grade
                idx = yarnGrades.FindIndex(x => x.Grade == current.Grade);
                if (idx > -1)
                    cbGrades.SelectedIndex = idx;
                else cbGrades.SelectedIndex = -1;

                EnableControls();
            }
        }

        private void DisableControls()
        {
            cbItemYanrs.IsEnabled = false;
            cbGrades.IsEnabled = false;
            txtLotNo.IsEnabled = false;
            txtWeight.IsEnabled = false;
            txtCH.IsEnabled = false;
        }

        private void EnableControls()
        {
            cbItemYanrs.IsEnabled = true;
            cbGrades.IsEnabled = true;
            txtLotNo.IsEnabled = true;
            txtWeight.IsEnabled = true;
            txtCH.IsEnabled = true;
        }

        private void ReceivedTraceNo()
        {
            string traceNo = txtTraceNo.Text;

            var items = G4YarnReturn.SearchYarnStocks(traceNo).Value();
            if (null == items || items.Count <= 0)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("Not found Trace No." + Environment.NewLine + "ไม่พบข้อมูล Trace No.");

                this.InvokeAction(() => 
                {
                    txtTraceNo.SelectAll();
                    txtTraceNo.FocusControl();
                });

                return;
            }

            if (null != items && items.Count > 0)
            {
                current = items[0];
                UpdateCurrentInfo();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            LoadComboBox();
        }

        #endregion

        private void txtLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWeight_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCH_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
