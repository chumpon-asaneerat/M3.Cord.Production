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

using NLib.Services;
using M3.Cord.Models;
using NLib.Models;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for FirstTwistPCCardPage.xaml
    /// </summary>
    public partial class FirstTwistPCCardPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistPCCardPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;
        private PCTwist1Operation _operation = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // First Twist
            var page = M3CordApp.Pages.FirstTwistMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSelectPCCard_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC)
                return;
            var win = M3CordApp.Windows.ChoosePCCardTwist1;
            win.Setup();
            if (win.ShowDialog() == false) return;
            if (null != win.SelectedPCCard)
            {
                AddNew(win.SelectedPCCard);
            }
        }

        private void cmdStartDoff_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC || null == pcCard)
                return;

            // Check operations
            var operations = lvPCCards.ItemsSource as List<PCTwist1Operation>;

            bool isNew = true;
            if (null != operations && operations.Count > 0)
            {
                isNew = false;
            }
            if (isNew)
            {
                // Check load yarn
                var records = Twist1LoadRecord.Gets(pcCard.PCTwist1Id.Value).Value();
                if (null == records || records.Count <= 0)
                {
                    // No yarn load so open load yarn dialog
                    RequestLoadYarn.Call(this, System.EventArgs.Empty);
                    return;
                }
                // First Doff - Show start doff
                var win = M3CordApp.Windows.StartTwist1Op;
                _operation = new PCTwist1Operation();
                _operation.PCTwist1Id = pcCard.PCTwist1Id;
                _operation.ProductionDate = DateTime.Now;
                _operation.MCCode = selectedMC.MCCode;
                _operation.DoffNo = 1; // doff
                win.Setup(_operation); // New
                if (win.ShowDialog() == false) return;
                RefreshGrids();
            }
            else
            {
                // Yarn load exists
                var records = Twist1LoadRecord.Gets(pcCard.PCTwist1Id.Value).Value();
                if (null != records && records.Count > 0)
                {
                    var record = records[records.Count - 1];

                    var msg = M3CordApp.Windows.MessageBoxOKCancel;
                    string sMsg = "Continue Next Doff" + Environment.NewLine + "with Existing Yarn Lot";
                    msg.Setup(sMsg);
                    if (msg.ShowDialog() == true)
                    {
                        // Show start doff
                        var win = M3CordApp.Windows.StartTwist1Op;
                        _operation = new PCTwist1Operation();
                        _operation.PCTwist1Id = pcCard.PCTwist1Id;
                        _operation.ProductionDate = DateTime.Now;
                        _operation.MCCode = selectedMC.MCCode;
                        _operation.DoffNo = record.DoffNo + 1; // doff
                        win.Setup(_operation); // New Doff
                        if (win.ShowDialog() == false) return;
                        RefreshGrids();

                        var inst = new Twist1LoadRecord();
                        inst.ProductionDate = DateTime.Now;
                        inst.PCTwist1Id = record.PCTwist1Id;
                        inst.ItemYarn = record.ItemYarn;

                        inst.ProductLotNo = record.ProductLotNo;
                        inst.TestFlag = record.TestFlag;
                        inst.DoffNo = record.DoffNo + 1;
                        inst.ShiftName = record.ShiftName;

                        inst = Twist1LoadRecord.Save(inst).Value();
                        var recordItems = Twist1LoadRecordItem.Gets(record.Twist1LoadId.Value,
                            null, null).Value();
                        if (null != recordItems)
                        {
                            foreach (var recordItem in recordItems)
                            {
                                // copy all items
                                var instItem = new Twist1LoadRecordItem();
                                instItem.Twist1LoadId = inst.Twist1LoadId.Value;
                                instItem.SPNo = recordItem.SPNo;
                                instItem.DeckNo = recordItem.DeckNo;
                                instItem.PalletNo = recordItem.PalletNo;
                                instItem.TraceNo = recordItem.TraceNo;
                                instItem.YarnBarcode = recordItem.YarnBarcode;
                                Twist1LoadRecordItem.Save(instItem);
                            }
                        }
                    }
                    else
                    {
                        // No = show yarn load record window by raise event
                        RequestLoadYarn.Call(this, System.EventArgs.Empty);
                    }
                }
                else
                {
                    // No yarn load so need to load first.
                    RequestLoadYarn.Call(this, System.EventArgs.Empty);
                }
            }
        }

        private void cmdEndDoff_Click(object sender, RoutedEventArgs e)
        {
            if (null != lvPCCards.SelectedItem)
            {
                var selected = lvPCCards.SelectedItem as PCTwist1Operation;
                if (null != selected)
                {
                    _operation = selected;
                }
                else
                {
                    _operation = null;
                }

                // already has end time.
                if (null != _operation && _operation.EndTime.HasValue)
                {
                    _operation = null;
                    return;
                }
            }

            if (null == selectedMC || null == pcCard || null == _operation)
                return;

            var win = M3CordApp.Windows.EndTwist1Op;
            _operation.PCTwist1Id = pcCard.PCTwist1Id;
            _operation.MCCode = selectedMC.MCCode;
            win.Setup(_operation); // New
            if (win.ShowDialog() == false) return;

            // calc actual qty
            var operations = lvPCCards.ItemsSource as List<PCTwist1Operation>;
            if (null != operations && operations.Count > 0)
            {
                decimal val = decimal.Zero;
                foreach (var opitem in operations)
                {
                    if (opitem.CalcProductWeight.HasValue)
                        val += opitem.CalcProductWeight.Value;
                    if (opitem.WasteWeight.HasValue)
                        val -= opitem.WasteWeight.Value;
                }
                // update to db
                pcCard.ActualQty = val;
                PCTwist1.Save(pcCard);
            }
            RefreshGrids();
        }

        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC || null == pcCard)
                return;
            // Check current operations
            bool bReachQuota = (pcCard.TargetQty.HasValue && pcCard.ActualQty.HasValue &&
                pcCard.TargetQty.Value < pcCard.ActualQty.Value);
            if (!bReachQuota)
            {
                var msg = M3CordApp.Windows.MessageBoxOKCancel;
                string txt = "Actual Qty < Target Qty." + Environment.NewLine;
                txt += "Finish ths PC Card?";
                msg.Setup(txt);
                if (msg.ShowDialog() == true)
                {
                    pcCard.FinishFlag = true;
                    PCTwist1.Save(pcCard);

                    pcCard = null; // reset pc card

                    RefreshGrids();
                }
            }
            else
            {
                pcCard.FinishFlag = true;
                PCTwist1.Save(pcCard);
                FinishPCCard.Call(this, EventArgs.Empty);
            }
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var ctx = (null != btn) ? btn.DataContext : null;
            var op = (null != ctx) ? ctx as PCTwist1Operation : null;
            if (null != op)
            {

                if (null == selectedMC || null == pcCard)
                    return;

                if (!op.EndTime.HasValue)
                {
                    var msg = M3CordApp.Windows.MessageBox;
                    string sMsg = "Cannot Edit Current Record." + Environment.NewLine + "due to doff is not end.";
                    msg.Setup(sMsg);
                    msg.ShowDialog();

                    return;
                }

                var win = M3CordApp.Windows.EditTwist1Op;
                op.PCTwist1Id = pcCard.PCTwist1Id;
                op.MCCode = selectedMC.MCCode;
                win.Setup(op); // Edit
                if (win.ShowDialog() == false) return;

                // calc actual qty
                var operations = lvPCCards.ItemsSource as List<PCTwist1Operation>;
                if (null != operations && operations.Count > 0)
                {
                    decimal val = decimal.Zero;
                    foreach (var opitem in operations)
                    {
                        if (opitem.CalcProductWeight.HasValue)
                            val += opitem.CalcProductWeight.Value;
                        if (opitem.WasteWeight.HasValue)
                            val -= opitem.WasteWeight.Value;
                    }
                    // update to db
                    pcCard.ActualQty = val;
                    PCTwist1.Save(pcCard);
                }

                RefreshGrids();
            }
        }

        #endregion

        #region ListView Handlers

        private void lvPCCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void AddNew(PCCard pccard)
        {
            if (null != selectedMC && null != pccard)
            {
                var ret = PCTwist1.AddNew(selectedMC, pccard);
                if (ret.Ok)
                {

                }
            }
            RefreshGrids();
        }

        private void UpdateMCStatus()
        {
            paPCCard.DataContext = null;
            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;
            // Binding
            paPCCard.DataContext = pcCard;
            cmdSelectPCCard.IsEnabled = (null == pcCard);

            // Check current operations
            bool bReachQuota = (null != pcCard && pcCard.TargetQty.HasValue && pcCard.ActualQty.HasValue &&
                pcCard.TargetQty.Value < pcCard.ActualQty.Value);

            var operations = lvPCCards.ItemsSource as List<PCTwist1Operation>;
            if (null != operations && operations.Count > 0)
            {
                int idx = operations.Count - 1;
                _operation = operations[idx];
                if (_operation.EndTime.HasValue)
                {
                    // already end.
                    _operation = null;
                }
            }

            // Check enable start/end doff
            cmdStartDoff.IsEnabled = (null != pcCard && null == _operation && !bReachQuota);
            cmdEndDoff.IsEnabled = (null != pcCard && null != _operation && !bReachQuota);
            cmdFinish.IsEnabled = (null != pcCard);
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
            RefreshGrids();
        }

        public void RefreshGrids()
        {
            lvPCCards.ItemsSource = null;
            if (null != pcCard)
            {
                var items = PCTwist1Operation.Gets(pcCard.PCTwist1Id.Value).Value();
                lvPCCards.ItemsSource = items;
            }

            UpdateMCStatus();
        }

        #endregion

        #region Public Events

        public event System.EventHandler RequestLoadYarn;
        public event System.EventHandler FinishPCCard;

        #endregion
    }
}
