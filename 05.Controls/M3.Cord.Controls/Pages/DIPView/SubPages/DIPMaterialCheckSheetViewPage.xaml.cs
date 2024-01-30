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
    /// Interaction logic for DIPMaterialCheckSheetViewPage.xaml
    /// </summary>
    public partial class DIPMaterialCheckSheetViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPMaterialCheckSheetViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private DIPMaterialCheckSheet sheet = null;
        private List<DIPMaterialCheckSheetItem> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPOperationView;
            page.Setup(pcCard);
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Private Methods

        private void RefreshGrid(bool bInit)
        {
            grid.ItemsSource = null;

            if (null != sheet && null != mc)
            {
                if (null == items || bInit)
                {
                    // Init all core
                    items = new List<DIPMaterialCheckSheetItem>();
                    for (int i = mc.StartCore; i <= mc.EndCore; i++)
                    {
                        items.Add(new DIPMaterialCheckSheetItem() { SPNo = i });
                    }

                    var existItems = DIPMaterialCheckSheetItem.Gets(sheet.MaterialCheckId).Value();
                    if (null != existItems && existItems.Count > 0)
                    {
                        foreach (var existItem in existItems)
                        {
                            int idx = items.FindIndex((item =>
                            {
                                return (existItem.SPNo == item.SPNo);
                            }));
                            if (idx != -1 && null != items[idx])
                            {
                                var item = items[idx];
                                if (null != item)
                                {
                                    item.MaterialCheckId = existItem.MaterialCheckId;
                                    item.SPNo = existItem.SPNo;
                                    item.LotNo = existItem.LotNo;
                                    item.DoffNo = existItem.DoffNo;
                                    item.CHNo = existItem.CHNo;

                                    item.CheckYarnNo = existItem.CheckYarnNo;
                                    item.CheckYanScrap = existItem.CheckYanScrap;
                                    item.CheckYarnBall = existItem.CheckYarnBall;
                                    item.CheckCover = existItem.CheckCover;
                                    item.CheckSensor = existItem.CheckSensor;
                                    item.CheckDustFilter = existItem.CheckDustFilter;
                                }
                            }
                        }
                    }
                }
            }

            grid.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC, DIPPCCard PCCard)
        {
            sheet = null;

            paCondition.DataContext = null;
            paSheetInfo.DataContext = null;

            mc = selecteedMC;
            pcCard = PCCard;
            if (null != mc && null != pcCard)
            {
                var sheets = DIPMaterialCheckSheet.Gets(pcCard.DIPPCId.Value).Value();
                sheet = (null != sheets) ? sheets.LastOrDefault() : null;
                if (null == sheet)
                {
                    sheet = new DIPMaterialCheckSheet();
                    sheet.DIPPCId = pcCard.DIPPCId.Value;
                    sheet.CheckDate = DateTime.Now;
                }
                // set same as PCCard
                sheet.MCCode = pcCard.MCCode;
                sheet.DoffNo = pcCard.DoffNo;
            }

            paCondition.DataContext = pcCard;
            paSheetInfo.DataContext = sheet;

            this.InvokeAction(() =>
            {
                RefreshGrid(true);
            });
        }

        #endregion
    }
}
