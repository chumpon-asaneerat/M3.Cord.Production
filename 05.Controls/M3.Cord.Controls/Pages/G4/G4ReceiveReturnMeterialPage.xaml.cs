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
using System.Text.RegularExpressions;
using M3.Cord.Services.Excels;

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

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            var ctx = (null != btn) ? btn.DataContext : null;
            var item = (null != ctx) ? ctx as G4YarnReturn : null;
            Remove(item);
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        #endregion

        #region TextBox Handlers

        private void txtTraceNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                SearchTraceNo();
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
                    txtWeight.SelectAll();
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
                    txtCH.SelectAll();
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

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (null != grid.SelectedItem) ? grid.SelectedItem as G4YarnReturn : null;
            if (null == item) return;
            Edit(item);
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
            dtReceiveDate.IsEnabled = false;
            cbItemYanrs.IsEnabled = false;
            cbGrades.IsEnabled = false;
            txtWeight.IsEnabled = false;
            txtCH.IsEnabled = false;
        }

        private void EnableControls()
        {
            dtReceiveDate.IsEnabled = true;
            cbItemYanrs.IsEnabled = true;
            cbGrades.IsEnabled = true;
            txtWeight.IsEnabled = true;
            txtCH.IsEnabled = true;
        }

        private void UpdateCurrentInfo()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
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
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void RefreshGrid()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
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
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void CalcTotals()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                paTotals.DataContext = null;

                if (null == returnItems) returnItems = new List<G4YarnReturn>();
                if (null == sum) sum = new G4YarnReturnSummary();

                sum.TotalCH = decimal.Zero;
                sum.TotalWeight = decimal.Zero;
                sum.TotalPallet = decimal.Zero;

                returnItems.ForEach(item =>
                {
                    sum.TotalWeight += (item.WeightQty.HasValue) ? item.WeightQty.Value : decimal.Zero;
                    sum.TotalCH += (item.ConeCH.HasValue) ? item.ConeCH.Value : decimal.Zero;
                    sum.TotalPallet += 1;
                });

                paTotals.DataContext = sum;
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void SearchTraceNo()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                string traceNo = txtTraceNo.Text;
                if (string.IsNullOrEmpty(traceNo))
                {
                    return;
                }

                if (null != current && current.TraceNo == traceNo)
                {
                    // same trace so ignore.
                    this.InvokeAction(() =>
                    {
                        txtWeight.SelectAll();
                        txtWeight.FocusControl(); // focus
                    });

                    return;
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
                    if (null != current)
                    {
                        int rowId = returnItems.Count + 1;
                        FormatItem(rowId, current);

                        this.InvokeAction(() =>
                        {
                            txtWeight.SelectAll();
                            txtWeight.FocusControl(); // focus
                        });
                    }

                    UpdateCurrentInfo();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void Receive()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == returnItems) returnItems = new List<G4YarnReturn>();

            if (null == current)
                return;

            var itemYarn = cbItemYanrs.SelectedItem as CordItemYarn;
            current.ItemYarn = (null != itemYarn) ? itemYarn.ItemYarn : null;

            var grade = cbGrades.SelectedItem as YarnGrade;
            current.Grade = (null != grade) ? grade.Grade : null;

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

            if (string.IsNullOrEmpty(current.ItemYarn))
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("Please Enter Item Yarn." + Environment.NewLine + "กรุณาบันทึก Item Yarn.");
                msgbox.ShowDialog();

                this.InvokeAction(() =>
                {
                    cbItemYanrs.FocusControl();
                });
                return;
            }

            if (string.IsNullOrEmpty(current.Grade))
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("Please Enter Grade." + Environment.NewLine + "กรุณาบันทึก Grade.");
                msgbox.ShowDialog();

                this.InvokeAction(() =>
                {
                    cbGrades.FocusControl();
                });
                return;
            }

            try
            {
                int idx = returnItems.FindIndex(x => x.TraceNo == current.TraceNo);
                if (idx != -1)
                {
                    returnItems[idx] = current;
                }
                else
                {
                    returnItems.Add(current);
                }

                // reset current
                current = null;
                txtTraceNo.Text = string.Empty;

                UpdateCurrentInfo();
                RefreshGrid();
                CalcTotals();
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void FormatItem(int rowId, G4YarnReturn item)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                // set row no
                item.RowNo = rowId;
                // update operator
                item.Operator = (null != Current.User) ? Current.User.FullName : null;
            }
            catch (Exception ex) 
            {
                med.Err(ex);
            }
        }

        private void Edit(G4YarnReturn item)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == item) 
                return;

            try
            {
                current = item;
                txtTraceNo.Text = item.TraceNo;
                UpdateCurrentInfo();
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void Remove(G4YarnReturn item)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == item) 
                return;

            try
            {
                if (null == returnItems) returnItems = new List<G4YarnReturn>();
                int idx = returnItems.FindIndex(x => x.TraceNo == item.TraceNo);
                if (idx != -1)
                {
                    returnItems.RemoveAt(idx);

                    UpdateCurrentInfo();
                    RefreshGrid();
                    CalcTotals();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void Export()
        {
            G4ReturnYarnExport.Export(returnItems);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            LoadComboBox();

            UpdateCurrentInfo();
            RefreshGrid();
            CalcTotals();
        }

        #endregion
    }
}
