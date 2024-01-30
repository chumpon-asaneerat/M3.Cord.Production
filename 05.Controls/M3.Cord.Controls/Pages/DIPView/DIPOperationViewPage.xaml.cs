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
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdMaterialCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPMaterialCheckSheet;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS7CreelCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S7CreelCheckSheet;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS8BeforeStart_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S8BeforeStartSummary;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdChemicalRequisitionRecordSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.ChemicalRequisitionRecord;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS9CleaningCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S9CleanningCheckSheet;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdDIPCondition_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPCondition;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS8ConditionSummary_Click(object sender, RoutedEventArgs e)
        {
            //var page = M3CordApp.Pages.ProductionRecord;
            var page = M3CordApp.Pages.S8ConditionSummary;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdDIPProductionTimeTable_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPTimeTable;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdS9AppearanceCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.S9AppearanceCheckSheet;
            page.Setup(mc);
            PageContentManager.Instance.Current = page;
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                DIPPCCard.Start(pcCard.DIPPCId.Value);
                if (Refresh())
                {

                }
            }
        }

        private void cmdEnd_Click(object sender, RoutedEventArgs e)
        {
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                DIPPCCard.End(pcCard.DIPPCId.Value);
                if (Refresh())
                {

                }
            }
        }

        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                DIPPCCard.Finish(pcCard.DIPPCId.Value);
                if (Refresh())
                {

                }
            }
        }

        #endregion

        private bool Refresh()
        {
            bool ret = false;

            paCondition.DataContext = null;
            if (null != mc)
            {
                pcCard = DIPUI.PCCard.Current(mc.MCCode);
                ret = (null != pcCard);
                if (ret)
                {
                    paCondition.DataContext = pcCard;
                    this.InvokeAction(() =>
                    {
                        CheckButtons();
                    });
                }
            }
            return ret;
        }

        private void CheckButtons()
        {
            if (null != pcCard)
            {
                if (!pcCard.StartTime.HasValue)
                {
                    cmdStart.IsEnabled = true;
                    cmdEnd.IsEnabled = false;
                    cmdFinish.IsEnabled = false;
                }
                else
                {
                    cmdStart.IsEnabled = false;
                    if (!pcCard.EndTime.HasValue)
                    {
                        cmdEnd.IsEnabled = true;
                        cmdFinish.IsEnabled = false;
                    }
                    else
                    {
                        cmdEnd.IsEnabled = false;
                        cmdFinish.IsEnabled = true;
                    }
                }
            }
        }

        #region Public Methods

        public bool Setup(DIPMC selectedMC)
        {
            string mcNo = (selectedMC.MCCode.EndsWith("1")) ? "1" : "2";
            mc = DIPMC.Gets("S-7", "S-7-" + mcNo).Value().FirstOrDefault();
            return Refresh();
        }

        #endregion
    }
}
