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

        private List<PCTwist1> pcCards = null;

        private PCTwist1 pcCard = null;
        private PalletSetting _pallet = null;
        private PalletSettingItem _item = null;

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

        private void cmdChoosePCTwist1_Click(object sender, RoutedEventArgs e)
        {
            pcCard = null;
            ShowSelectWindow();
        }

        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            if (null != _pallet && null != _item)
            {
                grid.ItemsSource = null;
                paItem.DataContext = null;

                _pallet.Items.Add(_item);
                _pallet.Calculate();

                // Pre create item
                _item = new PalletSettingItem();
                paItem.DataContext = _item;

                grid.ItemsSource = _pallet.Items;
            }
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;

            var item = btn.DataContext as PalletSettingItem;
            if (null != _pallet && null != item)
            {
                grid.ItemsSource = null;

                var idx = _pallet.Items.FindIndex((src) =>
                {
                    return src.DoffNo == item.DoffNo && src.SPNos == item.SPNos;

                });
                if (idx != -1)
                {
                    _pallet.Items.RemoveAt(idx);
                    _pallet.Calculate();
                }
                
                grid.ItemsSource = _pallet.Items;
            }
        }

        #endregion

        #region TextBox Handlers

        private void txtProductLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetPCCardByLot();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void ClearContext()
        {
            this.DataContext = null;
            paItem.DataContext = null;
            paItem.IsEnabled = false;
            grid.ItemsSource = null;
        }

        private void GetPCCardByLot()
        {
            ClearContext();

            cmdChoosePCTwist1.IsEnabled = false;
            pcCards = PCTwist1.SearchByLotNo(txtProductLotNo.Text).Value();
            if (null != pcCards && pcCards.Count > 0)
            {
                if (pcCards.Count == 1)
                {
                    // has only single lot so auto load
                    pcCard = pcCards[0];
                    GetLotInfo();
                }
                else
                {
                    cmdChoosePCTwist1.IsEnabled = true;
                    ShowSelectWindow();
                }
            }
        }

        private void ShowSelectWindow()
        {
            var win = M3CordApp.Windows.ChoosePCTwist1;
            win.Owner = this;
            win.Setup(pcCards);
            if (win.ShowDialog() == true)
            {
                pcCard = win.Item; // get select item
                GetLotInfo();
            }
        }

        private void GetLotInfo()
        {
            ClearContext();

            if (null != pcCard)
            {
                _pallet = new PalletSetting();
                _pallet.MCCode = pcCard.MCCode;
                // pallet code
                var pCode = PalletCode.GetLastId(pcCard.MCCode).Value();
                string sCode = (null != pCode) ?
                    string.Format("{0:D4}.{1}.{2:D5}", pCode.Year, pCode.MCCode, pCode.LastId + 1) : null;
                _pallet.PalletCode = sCode;
                // get Counter and TwistNo
                //_pallet.Counter;
                //_pallet.TwistNo;

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
                _pallet.UserName = M3CordApp.Current.User.FullName;

                this.DataContext = _pallet;
                grid.ItemsSource = _pallet.Items;

                // Pre create item
                _item = new PalletSettingItem();
                paItem.DataContext = _item;
                paItem.IsEnabled = true;
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            pcCard = null;
            ClearContext();
            this.InvokeAction(() => 
            {
                cmdChoosePCTwist1.IsEnabled = false;
                txtProductLotNo.FocusControl();
            });
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Pallet instnace
        /// </summary>
        public PalletSetting Pallet { get { return _pallet; } }

        #endregion
    }
}
