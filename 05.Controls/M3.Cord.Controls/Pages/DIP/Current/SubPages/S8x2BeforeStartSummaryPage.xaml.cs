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
    /// Interaction logic for S8x2BeforeStartSummaryPage.xaml
    /// </summary>
    public partial class S8x2BeforeStartSummaryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public S8x2BeforeStartSummaryPage()
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
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8x2BeforeCondition) ? ctx as S8x2BeforeCondition : null;
            Edit(item);
        }

        private void cmdConfirmCondition_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8x2BeforeCondition) ? ctx as S8x2BeforeCondition : null;
            Edit(item);
        }

        private void cmdVerify_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8x2BeforeCondition) ? ctx as S8x2BeforeCondition : null;
            Verify(item);
        }

        #endregion

        #region Private Methods

        private void ResetStd()
        {
            if (null == pcCard)
                return;
            var ret = S8x2BeforeCondition.DeleteStd(pcCard.DIPPCId);
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

            var retV = S8x2BeforeCondition.GetStdV(pcCard.DIPPCId).Value();
            if (null == retV)
            {
                S8x2BeforeCondition.SaveStdV(pcCard.DIPPCId, pcCard.ProductCode, pcCard.DIPLotNo);
            }
            var retB = S8x2BeforeCondition.GetStdB(pcCard.DIPPCId).Value();
            if (null == retB)
            {
                S8x2BeforeCondition.SaveStdB(pcCard.DIPPCId, pcCard.ProductCode, pcCard.DIPLotNo);
            }
            var retCF = S8x2BeforeCondition.GetStdCF(pcCard.DIPPCId).Value();
            if (null == retB)
            {
                S8x2BeforeCondition.SaveStdCF(pcCard.DIPPCId, pcCard.ProductCode, pcCard.DIPLotNo);
            }
        }

        private void Add()
        {
            if (null == pcCard)
                return;
            var win = M3CordApp.Windows.S8x2BeforeEditor;
            var item = S8x2BeforeCondition.Create(pcCard.ProductCode);
            item.DIPPCId = pcCard.DIPPCId;
            item.ProductCode = pcCard.ProductCode;
            item.LotNo = pcCard.DIPLotNo;
            item.RowType = 1;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                RefreshGrid();
            }
        }

        private void Edit(S8x2BeforeCondition item)
        {
            if (null != item)
            {
                var win = M3CordApp.Windows.S8x2BeforeEditor;
                win.Setup(item);
                if (win.ShowDialog() == true)
                {
                    RefreshGrid();
                }
            }
        }

        private void Verify(S8x2BeforeCondition item)
        {
            if (null != item)
            {
                var win = M3CordApp.Windows.ConfirmUser;
                if (win.ShowDialog() == true)
                {
                    item.VerifyBy = (null != win.User) ? win.User.FullName : null;
                    item.VerifyDate = DateTime.Now;
                    S8x2BeforeCondition.Save(item);

                    RefreshGrid();
                }
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != pcCard)
            {
                grid.ItemsSource = S8x2BeforeCondition.Gets(pcCard.DIPPCId).Value();
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            mc = selecteedMC;

            if (null != mc)
            {
                pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                if (null != pcCard)
                {
                    CheckStd();
                }
            }

            paCondition.DataContext = pcCard;

            RefreshGrid();
        }

        #endregion
    }
}
