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
using M3.Cord.Services.Excels;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for S8ConditionSummaryViewPage.xaml
    /// </summary>
    public partial class S8ConditionSummaryViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S8ConditionSummaryViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private S8ProductionCondition sheet = null;
        private List<S8ProductionConditionItem> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPOperationView;
            page.Setup(pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdReset_Click(object sender, RoutedEventArgs e)
        {
            ResetStd();
        }

        private void cmdDetails_Click(object sender, RoutedEventArgs e)
        {
            /*
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8ProductionConditionItem) ? ctx as S8ProductionConditionItem : null;
            Edit(item);
            */
        }

        private void cmdConfirmCondition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
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

        private void Edit(S8ProductionConditionItem item)
        {
            if (null == item) return;

            var dt = pcCard.StartTime.Value;
            var startDate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);

            var win = M3CordApp.Windows.S8ProductionConditionItemEditor;
            win.Setup(startDate, item);
            if (win.ShowDialog() == true)
            {
                RefreshGrid();
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
        }

        private void Export()
        {
            S8Export.Export(pcCard, sheet, items);
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC, DIPPCCard PCCard)
        {
            if (null != selecteedMC)
            {
                string mcNo = (selecteedMC.MCCode.EndsWith("1")) ? "1" : "2";
                mc = DIPMC.Gets("S-8", "S-8-" + mcNo).Value().FirstOrDefault();
                if (null != mc)
                {
                    pcCard = PCCard;
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
