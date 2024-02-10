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
using System.Runtime.InteropServices.ComTypes;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for DIPTimeTablePage.xaml
    /// </summary>
    public partial class DIPTimeTablePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPTimeTablePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPOperationMenu(mc);
        }

        private void cmdReset_Click(object sender, RoutedEventArgs e)
        {
            ResetStd();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void cmdDetails_Click(object sender, RoutedEventArgs e)
        {
            /*
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is DIPTimeTable) ? ctx as DIPTimeTable : null;
            Edit(item);
            */
        }

        private void cmdConfirmCondition_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion

        #region Selection Change

        private void dtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void ResetStd()
        {
            if (null == pcCard)
                return;
            var ret = DIPTimeTable.DeleteStd(pcCard.DIPPCId);
            if (null != ret && ret.Ok)
            {
                if (null != pcCard)
                {
                    CheckStd();
                }
                RefreshGrid();
            }
        }

        private void CheckStd()
        {
            if (null == pcCard)
                return;
            // Check is standard exits
            DateTime? startDate = new DateTime?();
            if (pcCard.StartTime.HasValue)
            {
                var dt = pcCard.StartTime.Value;
                startDate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            }

            if (!startDate.HasValue)
            {
                return;
            }

            var retV = DIPTimeTable.GetStdV(pcCard.DIPPCId).Value();
            if (null == retV)
            {
                DIPTimeTable.SaveStdV(pcCard.DIPPCId, pcCard.ProductCode, startDate.Value, pcCard.DIPLotNo);
            }
            var retB = DIPTimeTable.GetStdB(pcCard.DIPPCId).Value();
            if (null == retB)
            {
                DIPTimeTable.SaveStdB(pcCard.DIPPCId, pcCard.ProductCode, startDate.Value, pcCard.DIPLotNo);
            }
        }

        private void Add()
        {
            if (null == pcCard)
                return;
            if (!pcCard.StartTime.HasValue)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("M/C is not start" + Environment.NewLine + "ยังไม่ทำการเดินเครื่อง");
                msgbox.ShowDialog();
                return;
            }

            var dt = pcCard.StartTime.Value;
            var startDate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);

            var win = M3CordApp.Windows.DIPTimeTableEditor;
            var item = DIPTimeTable.Create(pcCard.ProductCode);
            item.DIPPCId = pcCard.DIPPCId;
            item.ProductCode = pcCard.ProductCode;
            item.RowType = 1;
            item.LotNo = pcCard.DIPLotNo;

            win.Setup(startDate, item);
            if (win.ShowDialog() == true)
            {
                RefreshGrid();
            }
        }

        private void Edit(DIPTimeTable item)
        {
            if (null == item) return;

            var dt = pcCard.StartTime.Value;
            var startDate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);

            var win = M3CordApp.Windows.DIPTimeTableEditor;
            win.Setup(startDate, item);
            if (win.ShowDialog() == true)
            {
                RefreshGrid();
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != pcCard)
            {
                if (!dtDate.SelectedDate.HasValue)
                {
                    this.InvokeAction(() =>
                    {
                        dtDate.Focus();
                    });
                    return;
                }
                grid.ItemsSource = DIPTimeTable.Gets(dtDate.SelectedDate.Value.Date).Value();

                // get lot list
                List<string> lots = DIPTimeTable.GetLots(dtDate.SelectedDate.Value.Date).Value();
                string sLot = string.Empty;
                if (null != lots && lots.Count > 0)
                {
                    foreach (var lot in lots)
                    {
                        if (string.IsNullOrEmpty(lot)) continue;
                        sLot += lot + ", ";
                    }

                    sLot = sLot.Trim();
                    if (sLot.EndsWith(","))
                    {
                        sLot = sLot.Remove(sLot.Length - 1, 1);
                    }
                }
                txtLotNos.Text = sLot;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            var today = DateTime.Now;
            if (today.Hour <= 7) today = today.AddDays(-1);
            dtDate.SelectedDate = today;

            if (null != selecteedMC)
            {
                string mcNo = (selecteedMC.MCCode.EndsWith("1")) ? "1" : "2";
                mc = DIPMC.Gets("S-8", "S-8-" + mcNo).Value().FirstOrDefault();

                mc = selecteedMC;
                pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                if (null != pcCard)
                {
                    CheckStd();
                }
            }
            RefreshGrid();

            paCondition.DataContext = pcCard;
        }

        #endregion
    }
}
