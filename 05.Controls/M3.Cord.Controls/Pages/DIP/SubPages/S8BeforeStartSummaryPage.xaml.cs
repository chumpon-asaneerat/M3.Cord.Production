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

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void cmdDetails_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Edit");
        }

        private void cmdConfirmCondition_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Confirm");
        }

        #endregion

        #region Private Methods

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

        private void Edit()
        {

        }

        private void RefreshGrid()
        {

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

                    }
                }
            }

            paCondition.DataContext = pcCard;

            RefreshGrid();
        }

        #endregion
    }
}
