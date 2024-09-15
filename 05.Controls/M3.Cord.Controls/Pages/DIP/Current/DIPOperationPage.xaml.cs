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
    /// Interaction logic for DIPOperationPage.xaml
    /// </summary>
    public partial class DIPOperationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPOperationPage()
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
            int mcNo = (mc.MCCode.EndsWith("1")) ? 1 : 2;
            if (mcNo == 1)
            {
                // S-8-1
                var page = M3CordApp.Pages.S8BeforeStartSummary;
                page.Setup(mc);
                PageContentManager.Instance.Current = page;
            }
            else
            {
                // S-8-2
                var page = M3CordApp.Pages.S8x2BeforeStartSummary;
                page.Setup(mc);
                PageContentManager.Instance.Current = page;
            }
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
            int mcNo = (mc.MCCode.EndsWith("1")) ? 1 : 2;
            if (mcNo == 1)
            {
                // S-8-1
                var page = M3CordApp.Pages.S8ConditionSummary;
                page.Setup(mc);
                PageContentManager.Instance.Current = page;
            }
            else
            {
                // S-8-2
                var page = M3CordApp.Pages.S8x2ConditionSummary;
                page.Setup(mc);
                PageContentManager.Instance.Current = page;
            }
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

        private void cmdChoosePCCard_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                DIPPCCard.Start(pcCard.DIPPCId.Value);
            }
            if (Refresh())
            {

            }
        }

        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                DIPPCCard.End(pcCard.DIPPCId.Value);
                DIPPCCard.Finish(pcCard.DIPPCId.Value);
            }
            if (Refresh())
            {

            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                //DIPPCCard.Cancel(pcCard.DIPPCId.Value);
            }
            if (Refresh())
            {

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
                    
                }
            }

            paCondition.DataContext = pcCard;

            this.InvokeAction(() =>
            {
                CheckButtons();
            });

            return ret;
        }

        private void CheckButtons()
        {
            if (null != pcCard)
            {
                cmdChoosePCCard.IsEnabled = false;

                if (!pcCard.StartTime.HasValue)
                {
                    // not start
                    cmdStart.IsEnabled = true; // enable start
                    cmdFinish.IsEnabled = false; // disable finish
                    cmdCancel.IsEnabled = true; // Not start so still cancel
                }
                else
                {
                    // already start
                    cmdStart.IsEnabled = false; // disable start
                    cmdCancel.IsEnabled = true; // allow cancel anytime.
                    if (!pcCard.EndTime.HasValue)
                    {
                        cmdFinish.IsEnabled = true; // no end time so enable finished
                        
                    }
                    else
                    {
                        cmdFinish.IsEnabled = false; // has end time so disable finished
                    }
                }
            }
            else
            {
                // No pc card
                cmdChoosePCCard.IsEnabled = true;
                cmdStart.IsEnabled = false;
                cmdFinish.IsEnabled = false;
                cmdCancel.IsEnabled = false;
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
