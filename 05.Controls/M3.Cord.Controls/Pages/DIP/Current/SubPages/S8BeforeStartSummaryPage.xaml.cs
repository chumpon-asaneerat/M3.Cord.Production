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
    /// Interaction logic for S8BeforeStartSummaryPage.xaml
    /// </summary>
    public partial class S8BeforeStartSummaryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S8BeforeStartSummaryPage()
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
            var item = (null != ctx && ctx is S8BeforeCondition) ? ctx as S8BeforeCondition : null;
            Edit(item);
        }

        private void cmdConfirmCondition_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8BeforeCondition) ? ctx as S8BeforeCondition : null;
            Edit(item);
        }

        private void cmdVerify_Click(object sender, RoutedEventArgs e)
        {
            var ctx = (sender as Button).DataContext;
            var item = (null != ctx && ctx is S8BeforeCondition) ? ctx as S8BeforeCondition : null;
            Verify(item);
        }

        #endregion

        #region Private Methods

        private void ResetStd()
        {
            if (null == pcCard)
                return;
            var ret = S8BeforeCondition.DeleteStd(pcCard.DIPPCId);
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

            var retV = S8BeforeCondition.GetStdV(pcCard.DIPPCId).Value();
            if (null == retV)
            {
                S8BeforeCondition.SaveStdV(pcCard.DIPPCId, pcCard.ProductCode, pcCard.DIPLotNo);
            }
            var retB = S8BeforeCondition.GetStdB(pcCard.DIPPCId).Value();
            if (null == retB)
            {
                S8BeforeCondition.SaveStdB(pcCard.DIPPCId, pcCard.ProductCode, pcCard.DIPLotNo);
            }
            var retCF = S8BeforeCondition.GetStdCF(pcCard.DIPPCId).Value();
            if (null == retB)
            {
                S8BeforeCondition.SaveStdCF(pcCard.DIPPCId, pcCard.ProductCode, pcCard.DIPLotNo);
            }
        }

        private void Add()
        {
            if (null == pcCard)
                return;
            var win = M3CordApp.Windows.S8BeforeEditor;
            var item = S8BeforeCondition.Create(pcCard.ProductCode);
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

        private void Edit(S8BeforeCondition item)
        {
            if (null != item)
            {
                var win = M3CordApp.Windows.S8BeforeEditor;
                win.Setup(item);
                if (win.ShowDialog() == true)
                {
                    RefreshGrid();
                }
            }
        }

        private void Verify(S8BeforeCondition item)
        {
            if (null != item)
            {
                var win = M3CordApp.Windows.ConfirmUser;
                if (win.ShowDialog() == true)
                {
                    item.VerifyBy = (null != win.User) ? win.User.FullName : null;
                    item.VerifyDate = DateTime.Now;
                    S8BeforeCondition.Save(item);

                    RefreshGrid();
                }
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != pcCard)
            {
                grid.ItemsSource = S8BeforeCondition.Gets(pcCard.DIPPCId).Value();
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            if (null != selecteedMC)
            {
                string mcNo = (selecteedMC.MCCode.EndsWith("1")) ? "1" : "2";
                mc = DIPMC.Gets("S-8", "S-8-" + mcNo).Value().FirstOrDefault();

                // update caption
                string caption = "ตรวจเช็คสภาวะการผลิตก่อนเปิดเครื่อง(S-8 " + mcNo.ToString() + " )";
                page.HeaderText = caption;

                if (null != mc)
                {
                    pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                    if (null != pcCard)
                    {
                        CheckStd();
                    }
                }
            }

            paCondition.DataContext = pcCard;

            RefreshGrid();
        }

        #endregion
    }
}
