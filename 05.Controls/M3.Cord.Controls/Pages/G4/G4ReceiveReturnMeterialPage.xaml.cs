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
using static M3.Cord.M3CordApp;
using System.Runtime.Serialization;
using System.Security.Cryptography;

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
        private List<G4YarnReturn> returnItems = new List<G4YarnReturn>();
        private G4YarnReturnSummary sum = new G4YarnReturnSummary();

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

        private void cmdReceive_Click(object sender, RoutedEventArgs e)
        {
            Receive();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            returnItems = new List<G4YarnReturn>();
            this.InvokeAction(() => 
            {
                RefreshGrid();
                CalcTotals();
            });
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            var ctx = (null != btn) ? btn.DataContext : null;
            var item = (null != ctx) ? ctx as G4YarnReturn : null;
            Edit(item);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            var ctx = (null != btn) ? btn.DataContext : null;
            var item = (null != ctx) ? ctx as G4YarnReturn : null;
            Remove(item);
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
            SearchTraceNo();
        }

        private void txtLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                this.InvokeAction(() =>
                {
                    txtWeight.FocusControl(); // focus
                });
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtLotNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtWeight_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                this.InvokeAction(() =>
                {
                    txtCH.FocusControl(); // focus
                });
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtWeight.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtCH_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                // clear trace no.
                txtCH.Text = string.Empty;
                e.Handled = true;
            }
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

        private void UpdateCurrentInfo()
        {
            paCurrent.DataContext = null;

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
            else
            {
                cbItemYanrs.SelectedIndex = -1;
                cbGrades.SelectedIndex = -1;

                DisableControls();
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            if (null == returnItems) returnItems = new List<G4YarnReturn>();

            int rowId = 1;
            returnItems.ForEach(item =>
            {
                FormatItem(rowId, item);
                rowId++;
            });

            grid.ItemsSource = returnItems;
        }

        private void CalcTotals()
        {
            paTotals.DataContext = null;

            if (null == returnItems) returnItems = new List<G4YarnReturn>();
            if (null == sum) sum = new G4YarnReturnSummary();

            sum.TotalCH = decimal.Zero;
            sum.TotalWeight = decimal.Zero;
            sum.TotalPallet = decimal.Zero;

            returnItems.ForEach(item =>
            {
                sum.TotalWeight+= (item.WeightQty.HasValue) ? item.WeightQty.Value : decimal.Zero;
                sum.TotalCH += (item.ConeCH.HasValue) ? item.ConeCH.Value : decimal.Zero;
                sum.TotalPallet += 1;
            });

            paTotals.DataContext = sum;
        }

        private void SearchTraceNo()
        {
            string traceNo = txtTraceNo.Text;
            if (string.IsNullOrEmpty(traceNo)) 
            {
                return;
            }

            if (null != current && current.TraceNo == traceNo)
            {
                return; // same trace so ignore.
            }

            var items = G4YarnReturn.SearchYarnStocks(traceNo).Value();
            if (null == items || items.Count <= 0)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("Not found Trace No." + Environment.NewLine + "ไม่พบข้อมูล Trace No.");
                msgbox.ShowDialog();

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
                int rowId = returnItems.Count + 1;
                FormatItem(rowId, current);
                UpdateCurrentInfo();
            }
        }

        private void Receive()
        {
            if (null == returnItems) returnItems = new List<G4YarnReturn>();

            if (null == current)
                return;

            if (string.IsNullOrEmpty(current.LotNo))
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("Please Enter Lot No." + Environment.NewLine + "กรุณาบันทึก Lot No.");
                msgbox.ShowDialog();

                this.InvokeAction(() =>
                {
                    txtLotNo.SelectAll();
                    txtLotNo.FocusControl();
                });
                return;
            }

            if (!current.WeightQty.HasValue)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("Please Enter Weight." + Environment.NewLine + "กรุณาบันทึก Weight.");
                msgbox.ShowDialog();

                this.InvokeAction(() =>
                {
                    txtWeight.SelectAll();
                    txtWeight.FocusControl();
                });
                return;
            }

            if (!current.ConeCH.HasValue)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("Please Enter CH." + Environment.NewLine + "กรุณาบันทึก CH.");
                msgbox.ShowDialog();

                this.InvokeAction(() =>
                {
                    txtCH.SelectAll();
                    txtCH.FocusControl();
                });
                return;
            }

            returnItems.Add(current);

            // reset current
            current = null;
            txtTraceNo.Text = string.Empty;

            UpdateCurrentInfo();
            RefreshGrid();
            CalcTotals();
        }

        private void FormatItem(int rowId, G4YarnReturn item)
        {
            // set row no
            item.RowNo = rowId;

            // update operator
            item.Operator = (null != Current.User) ? Current.User.FullName : null;

            // set new trace no
            string newTraceNo = string.Empty;

            if (!string.IsNullOrEmpty(item.TraceNo))
            {
                string sTraceNo = item.TraceNo.Trim();
                if (sTraceNo.Length < 10)
                {
                    newTraceNo = "R" + sTraceNo;

                    if (newTraceNo.Length != 10)
                    {
                        var dt = DateTime.Now;
                        newTraceNo = "R" + dt.ToString("ddMMyymm", System.Globalization.DateTimeFormatInfo.InvariantInfo) + rowId.ToString();
                    }
                }
                else
                {
                    newTraceNo = "R" + sTraceNo.Substring(1, sTraceNo.Length - 1);

                    if (newTraceNo.Length != 10)
                    {
                        var dt = DateTime.Now;
                        newTraceNo = "R" + dt.ToString("ddMMyymm", System.Globalization.DateTimeFormatInfo.InvariantInfo) + rowId.ToString();
                    }
                }
            }

            item.NewTraceNo = newTraceNo;

            // check lot not exit auto generate
            if (string.IsNullOrEmpty(item.LotNo))
            {
                DateTime dt = DateTime.Now;
                current.LotNo = "L" + dt.ToString("ddMMyyyyHHmm",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
        }

        private void Edit(G4YarnReturn item)
        {
            if (null == item) 
                return;

            current = item;
            txtTraceNo.Text = item.TraceNo;
            UpdateCurrentInfo();
        }

        private void Remove(G4YarnReturn item)
        {
            if (null == item) 
                return;
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
    }
}
