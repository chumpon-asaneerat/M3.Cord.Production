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
using static M3.Cord.Pages.DIPUI;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for DIPOperationViewPage.xaml
    /// </summary>
    public partial class DIPOperationViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPOperationViewPage()
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
            var page = M3CordApp.Pages.DIPHistorySearch;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdMaterialCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPMaterialCheckSheetView;
            page.Setup(mc, pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS7CreelCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S7CreelCheckSheetView;
            page.Setup(mc, pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS8BeforeStart_Click(object sender, RoutedEventArgs e)
        {
            if (null == mc)
            {
                return;
            }
            if (mc.MCCode.EndsWith("1"))
            {
                var page = M3CordApp.Pages.S8BeforeStartSummaryView;
                page.Setup(mc, pcCard);
                PageContentManager.Instance.Current = page;
            }
            else if (mc.MCCode.EndsWith("2"))
            {
                var page = M3CordApp.Pages.S8x2BeforeStartSummaryView;
                page.Setup(mc, pcCard);
                PageContentManager.Instance.Current = page;
            }
        }

        private void cmdChemicalRequisitionRecordSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.ChemicalRequisitionRecordView;
            page.Setup(mc, pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS9CleaningCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S9CleanningCheckSheetView;
            page.Setup(mc, pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdDIPCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPConditionView;
            page.Setup(mc, pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS8ConditionSummary_Click(object sender, RoutedEventArgs e)
        {
            if (null == mc)
            {
                return;
            }
            if (mc.MCCode.EndsWith("1"))
            {
                var page = M3CordApp.Pages.S8ConditionSummaryView;
                page.Setup(mc, pcCard);
                PageContentManager.Instance.Current = page;
            }
            else if (mc.MCCode.EndsWith("2"))
            {
                var page = M3CordApp.Pages.S8x2ConditionSummaryView;
                page.Setup(mc, pcCard);
                PageContentManager.Instance.Current = page;
            }
        }

        private void cmdDIPProductionTimeTable_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPTimeTableView;
            page.Setup(mc, pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS9AppearanceCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S9AppearanceCheckSheetView;
            page.Setup(mc, pcCard);
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Public Methods

        public void Setup(DIPPCCard PCCard)
        {
            mc = null;
            paCondition.DataContext = null;

            pcCard = PCCard;
            if (null != pcCard)
            {
                string mcNo = (pcCard.MCCode.EndsWith("1")) ? "1" : "2";
                mc = DIPMC.Gets("S-7", "S-7-" + mcNo).Value().FirstOrDefault();
            }

            paCondition.DataContext = pcCard;
        }

        #endregion
    }
}
