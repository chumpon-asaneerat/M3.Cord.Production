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

        #endregion

        #region Private Methods

        private void Save()
        {
            if (null != sheet)
            {
                var ret = S8ProductionCondition.Save(sheet);

                if (sheet.S8ConditionId.HasValue)
                {
                    if (null != items)
                    {
                        foreach (var item in items)
                        {
                            item.S8ConditionId = sheet.S8ConditionId.Value;
                            S8ProductionConditionItem.Save(item);
                        }
                    }
                }

                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
            }
        }

        private void RefreshGrid()
        {
            //grid.ItemsSource = null;

            if (null != sheet && null != mc)
            {
                items = S8ProductionConditionItem.Gets(sheet.S8ConditionId).Value();
                if (null == items)
                {
                    items = new List<S8ProductionConditionItem>();
                }
            }

            //grid.ItemsSource = items;
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
