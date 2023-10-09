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

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for CreatePalletWindow.xaml
    /// </summary>
    public partial class CreatePalletWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CreatePalletWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private PCTwist1 pcCard = null;
        private PalletSetting _pallet = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // test save
            if (null != _pallet)
            {
                PalletSetting.Save(_pallet);
            }
            DialogResult = true;
        }

        #endregion

        #region TextBox Handlers

        private void txtProductLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
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
            pcCard = PCTwist1.SearchByLotNo(txtProductLotNo.Text).Value();
            if (null != pcCard)
            {
                _pallet = new PalletSetting();
                _pallet.MCCode = pcCard.MCCode;
                _pallet.ActualQty = pcCard.ActualQty;
                _pallet.CordStructure = pcCard.CordStructure;
                _pallet.CreateDate = DateTime.Now;
                _pallet.CustomerId = pcCard.CustomerId;
                _pallet.CustomerName = pcCard.CustomerName;
                _pallet.ItemYarn = pcCard.ItemYarn;
                _pallet.PCId = pcCard.PCId;
                _pallet.PCTwist1Id = pcCard.PCTwist1Id;
                _pallet.ProductCode = pcCard.ProductCode;
                _pallet.ProductLotNo = pcCard.ProductLotNo;
                _pallet.ProductName = pcCard.ProductName;
                _pallet.TargetQty = pcCard.TargetQty;
                _pallet.TreatRoute = pcCard.TreatRoute;
                _pallet.TwistSpec = pcCard.TwistSpec;
                _pallet.UserId = M3CordApp.Current.User.UserId;

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

        #region Public Property

        /// <summary>
        /// Gets Pallet instnace
        /// </summary>
        public PalletSetting Pallet { get { return _pallet; } }

        #endregion
    }
}
