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
    /// Interaction logic for Twist1LoadRecordEditorWindow.xaml
    /// </summary>
    public partial class Twist1LoadRecordEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Twist1LoadRecordEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC _mc;
        private PCTwist1 _pcCard;
        private Twist1LoadRecord _item = null;
        private G4IssueYarn _pallet = null;

        #endregion

        #region TextBox Handlers

        private void txtScanPallet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string palletNo = txtScanPallet.Text;
                txtScanPallet.Text = string.Empty;
                if (!string.IsNullOrEmpty(palletNo))
                {
                    _pallet = G4IssueYarn.SearchWarehousePallet(palletNo).Value();
                    txtPallet.Text = (null != _pallet) ? _pallet.PalletNo : string.Empty;
                    txtTraceNo.Text = (null != _pallet) ? _pallet.TraceNo : string.Empty;
                    txtTotalCH.Text = (null != _pallet && _pallet.ConeCH.HasValue) ? 
                        _pallet.ConeCH.Value.ToString("n0") : "0";
                    txtTotalWg.Text = (null != _pallet && _pallet.WeightQty.HasValue) ? 
                        _pallet.WeightQty.Value.ToString("n2") : "0";
                }
                e.Handled = true;
            }
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (null != _item && null != Items)
            {
                Items.Add(_item);
            }
            this.InvokeAction(() =>
            {
                RefreshGrid();
                NewItem();
            });
            */
        }

        #endregion

        #region CheckBox

        private void chkTest_Checked(object sender, RoutedEventArgs e)
        {
            if (null == _pcCard || null == _item) return;
            _item.TestFlag = true;
            _item.DoffNo = _pcCard.LastDoffNo + 1;
        }

        private void chkTest_Unchecked(object sender, RoutedEventArgs e)
        {
            if (null == _pcCard || null == _item) return;
            _item.TestFlag = false;
            _item.DoffNo = _pcCard.LastTestNo + 1;
        }

        #endregion

        #region Private Methods

        private void NewItem()
        {
            this.DataContext = null;
            if (null != _pcCard)
            {
                _item = new Twist1LoadRecord();
                _item.ProductionDate = DateTime.Now;
                _item.PCTwist1Id = _pcCard.PCTwist1Id;
                _item.ItemYarn = _pcCard.ItemYarn;

                _item.ProductLotNo = _pcCard.ProductLotNo;
                _item.TestFlag = false;
                _item.DoffNo = _pcCard.LastDoffNo + 1;
                _item.ShiftName = string.Empty;

                this.DataContext = _item;
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            //grid.ItemsSource = Items;
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc, PCTwist1 pcCard)
        {
            _mc = mc;
            _pcCard = pcCard;
            /*
            this.Items = new List<YarnLoadSheetDoff>();
            */
            NewItem();
            RefreshGrid();
        }

        #endregion

        #region Public Properties

        public Twist1LoadRecord Record { get { return _item; } }
        //public List<YarnLoadSheetDoff> Items { get; private set; }

        #endregion
    }
}
