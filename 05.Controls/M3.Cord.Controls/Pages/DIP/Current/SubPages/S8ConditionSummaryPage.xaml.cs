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
    /// Interaction logic for S8ConditionSummaryPage.xaml
    /// </summary>
    public partial class S8ConditionSummaryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S8ConditionSummaryPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private S8ProductionCondition sheet = null;
        private List<S8ProductionConditionItem> items = null;

        private S8WetPickUp pickup = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPOperationMenu(mc);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
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
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8ProductionConditionItem) ? ctx as S8ProductionConditionItem : null;
            Edit(item);
        }

        private void cmdConfirmCondition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdSave2_Click(object sender, RoutedEventArgs e)
        {
            Save2();
        }

        private void cmdSave3_Click(object sender, RoutedEventArgs e)
        {
            Save2();
        }

        private void cmdVerify_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8ProductionConditionItem) ? ctx as S8ProductionConditionItem : null;
            Verify(item);
        }

        #endregion

        #region Private Methods

        private void ResetStd()
        {
            if (null == pcCard)
                return;
            var ret = S8ProductionConditionItem.DeleteStd(pcCard.DIPPCId);
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

            var retV = S8ProductionConditionItem.GetStdV(pcCard.DIPPCId).Value();
            if (null == retV)
            {
                S8ProductionConditionItem.SaveStdV(pcCard.DIPPCId, pcCard.ProductCode, startDate.Value, pcCard.DIPLotNo, pcCard.DoffNo);
            }
            var retB = S8ProductionConditionItem.GetStdB(pcCard.DIPPCId).Value();
            if (null == retB)
            {
                S8ProductionConditionItem.SaveStdB(pcCard.DIPPCId, pcCard.ProductCode, startDate.Value, pcCard.DIPLotNo, pcCard.DoffNo);
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

            var win = M3CordApp.Windows.S8ProductionConditionItemEditor;
            var item = S8ProductionConditionItem.Create(pcCard.ProductCode);
            item.DIPPCId = pcCard.DIPPCId;
            item.ProductCode = pcCard.ProductCode;
            item.RowType = 1;
            item.LotNo = pcCard.DIPLotNo;
            item.DoffingDate = DateTime.Today;
            item.DoffingNo = pcCard.DoffNo;

            win.Setup(startDate, item);
            if (win.ShowDialog() == true)
            {
                
            }
            RefreshGrid();
        }

        private void Edit(S8ProductionConditionItem item)
        {
            if (null == item) return;

            var dt = pcCard.StartTime.Value;
            var startDate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);

            var win = M3CordApp.Windows.S8ProductionConditionItemEditor;
            win.Setup(startDate, item, true);
            if (win.ShowDialog() == true)
            {
                
            }
            RefreshGrid();
        }

        private void Verify(S8ProductionConditionItem item)
        {
            if (null != item)
            {
                var win = M3CordApp.Windows.ConfirmUser;
                if (win.ShowDialog() == true)
                {
                    item.Leader = (null != win.User) ? win.User.FullName : null;
                    S8ProductionConditionItem.Save(item);
                }
                RefreshGrid();
            }
        }

        private void Save()
        {
            if (null != sheet)
            {
                var ret = S8ProductionCondition.Save(sheet);

                if (sheet.DIPPCId.HasValue)
                {
                    if (null != items)
                    {
                        foreach (var item in items)
                        {
                            item.DIPPCId = sheet.DIPPCId.Value;
                            S8ProductionConditionItem.Save(item);
                        }
                    }
                }

                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
            }
        }

        private void Save2()
        {
            if (null != pickup)
            {
                var ret = S8WetPickUp.Save(pickup);

                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            if (null != sheet && null != mc)
            {
                items = null;
                if (sheet.DIPPCId.HasValue)
                {
                    items = S8ProductionConditionItem.Gets(sheet.DIPPCId.Value).Value();
                }
                if (null == items)
                {
                    items = new List<S8ProductionConditionItem>();
                }
            }

            grid.ItemsSource = items;

            RefreshPickup();
        }

        private void RefreshPickup()
        {
            paWetPickup.DataContext = null;
            paElectric.DataContext = null;

            var pickups = S8WetPickUp.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now).Value();
            pickup = (null != pickups) ? pickups.LastOrDefault() : null;
            if (null == pickup)
            {
                pickup = new S8WetPickUp();
                pickup.ProductCode = pcCard.ProductCode;
                pickup.LotNo = pcCard.DIPLotNo;
                pickup.CustomerName = pcCard.CustomerName;
                pickup.DoffingDate = DateTime.Now;
            }

            pickupDoc.Setup(pcCard);

            paWetPickup.DataContext = pickup;
            paElectric.DataContext = pickup;
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            if (null != selecteedMC)
            {
                string mcNo = (selecteedMC.MCCode.EndsWith("1")) ? "1" : "2";
                mc = DIPMC.Gets("S-8", "S-8-" + mcNo).Value().FirstOrDefault();
                if (null != mc)
                {
                    pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                    if (null != pcCard)
                    {
                        CheckStd();

                        var sheets = S8ProductionCondition.Gets(pcCard.DIPPCId.Value).Value();
                        sheet = (null != sheets) ? sheets.LastOrDefault() : null;
                        if (null == sheet)
                        {
                            sheet = new S8ProductionCondition();
                            sheet.DIPPCId = pcCard.DIPPCId.Value;
                            sheet.ProductCode = pcCard.ProductCode;
                            sheet.LotNo = pcCard.DIPLotNo;
                            sheet.RecordDate = DateTime.Now;
                            sheet.CustomerName = pcCard.CustomerName;
                            sheet.CordStructure = pcCard.CordStructure;
                        }
                    }
                }
            }

            paCondition1.DataContext = sheet;
            paCondition2.DataContext = sheet;

            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        #endregion
    }
}
