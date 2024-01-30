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
    /// Interaction logic for ChemicalRequisitionRecordViewPage.xaml
    /// </summary>
    public partial class ChemicalRequisitionRecordViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChemicalRequisitionRecordViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private List<DIPChemicalReqisition> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPOperationMenu(mc);
        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                var win = M3CordApp.Windows.ChemicalRequisitionEditor;
                var item = new DIPChemicalReqisition();
                item.ReqDate = DateTime.Now;
                item.ProductCode = pcCard.ProductCode;
                item.S8LotNo = pcCard.DIPLotNo;
                item.DIPPCId = pcCard.DIPPCId.Value;
                item.UserName = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;
                win.Setup(item);
                if (win.ShowDialog() == true)
                {
                    DIPChemicalReqisition.Save(item);
                    RefreshGrid();
                }
            }
        }

        #endregion

        #region Private Methods

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != pcCard && pcCard.DIPPCId.HasValue)
            {
                items = DIPChemicalReqisition.Gets(pcCard.DIPPCId.Value).Value();

                grid.ItemsSource = items;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            if (null != selecteedMC)
            {
                mc = selecteedMC;
                pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                if (null != pcCard)
                {

                }
            }

            paCondition.DataContext = pcCard;

            RefreshGrid();
        }

        #endregion
    }
}
