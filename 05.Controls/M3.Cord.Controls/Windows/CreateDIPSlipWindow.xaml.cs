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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;
using NLib.Wpf.Controls;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for CreateDIPSlipWindow.xaml
    /// </summary>
    public partial class CreateDIPSlipWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CreateDIPSlipWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPPCCard pcCard = null;
        private DIPPalletSlip _pallet = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        #endregion

        #region TextBox Handlers

        private void txtDIPLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetLotInfo();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void GetLotInfo()
        {
            this.DataContext = null;

            pcCard = DIPPCCard.SearchByLotNo(txtDIPLotNo.Text).Value();
            if (null != pcCard)
            {
                _pallet = new DIPPalletSlip();
                _pallet.MCCode = pcCard.MCCode;
                // pallet code
                var pCode = PalletCode.GetLastId(pcCard.MCCode).Value();
                string sCode = (null != pCode) ?
                    string.Format("{0:D4}.{1}.{2:D5}", pCode.Year, pCode.MCCode, pCode.LastId + 1) : null;
                _pallet.PalletCode = sCode;

                // get Counter and TwistNo
                //_pallet.Counter;
                //_pallet.TwistNo;

                _pallet.CordStructure = pcCard.CordStructure;
                _pallet.CreateDate = DateTime.Now;
                _pallet.CustomerId = pcCard.CustomerId;
                _pallet.CustomerName = pcCard.CustomerName;
                _pallet.ItemYarn = pcCard.ItemYarn;
                _pallet.DIPPCId = pcCard.DIPPCId;
                _pallet.ProductCode = pcCard.ProductCode;
                _pallet.DIPLotNo = pcCard.DIPLotNo;
                _pallet.ProductName = pcCard.ProductName;
                _pallet.TreatRoute = pcCard.TreatRoute;
                _pallet.TwistSpec = pcCard.TwistSpec;
                _pallet.UserName = M3CordApp.Current.User.FullName;

                _pallet.ActualQty = decimal.Zero;
                _pallet.ActualWeight = decimal.Zero;

                this.DataContext = _pallet;
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            pcCard = null;
            this.DataContext = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Pallet instnace
        /// </summary>
        public DIPPalletSlip Pallet { get { return _pallet; } }

        #endregion
    }
}
