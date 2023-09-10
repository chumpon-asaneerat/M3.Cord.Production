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
using NLib.Data.Design;

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
        private G4IssueYarn _pallet = null; // current pallet

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
                }

                RefreshCurrentPallet();

                e.Handled = true;
            }
        }

        private void txtYarnBarcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AppendYarn();

                e.Handled = true;
            }
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            Save();
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            AppendYarn();
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
            RefreshDoffNo();
        }

        private void chkTest_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshDoffNo();
        }

        #endregion

        #region Radio Button Handlers

        private void rbOption_Checked(object sender, RoutedEventArgs e)
        {
            CheckEnableScanOption();
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

            RefreshCurrentPallet();
        }

        private void RefreshDoffNo()
        {
            if (null == _pcCard || null == _item) return;
            if (chkTest.IsChecked == true)
            {
                _item.TestFlag = true;
                _item.DoffNo = _pcCard.LastTestNo + 1;
            }
            else
            {
                _item.TestFlag = false;
                _item.DoffNo = _pcCard.LastDoffNo + 1;
            }
        }

        private void Save()
        {
            if (null != _item)
            {
                var ret = Twist1LoadRecord.Save(_item);
                if (ret.Ok)
                {

                }
            }
        }

        private void RefreshCurrentPallet()
        {
            paCurrPallet.DataContext = _pallet;
        }

        private void EnableScanOption()
        {
            if (null != _mc)
            {
                txtSPNo.IsReadOnly = false;
                txtDeckNo.IsReadOnly = false;

                txtSPNo.Text = _mc.StartCore.ToString();

                if (_mc.DeckPerCore == 2)
                {
                    paScanOption.Visibility = Visibility.Visible;
                    CheckEnableScanOption();
                }
                else
                {
                    paScanOption.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                paScanOption.Visibility = Visibility.Collapsed;

                txtSPNo.Text = "0";
                txtDeckNo.Text = "0";

                txtSPNo.IsReadOnly = true;
                txtDeckNo.IsReadOnly = true;
            }
        }

        private void CheckEnableScanOption()
        {
            try
            {
                if (null == txtDeckNo)
                    return;
                if (rb1Only.IsChecked == true)
                {
                    txtDeckNo.Text = "1";
                    txtDeckNo.IsReadOnly = true;
                }
                else if (rb2Only.IsChecked == true)
                {
                    txtDeckNo.Text = "2";
                    txtDeckNo.IsReadOnly = true;
                }
                else if (rbToggle.IsChecked == true)
                {
                    txtDeckNo.Text = "1";
                    txtDeckNo.IsReadOnly = false;
                }
                else
                {
                    txtDeckNo.Text = "1";
                    txtDeckNo.IsReadOnly = false;
                }
            }
            catch { }
        }

        private void AppendYarn()
        {
            if (null == _mc)
                return;

            string spNo = txtSPNo.Text.Trim();
            string deckNo = txtDeckNo.Text.Trim();
            string yarnBarcode = txtYarnBarcode.Text.Trim();
            txtYarnBarcode.Text = string.Empty;

            if (string.IsNullOrEmpty(spNo) || string.IsNullOrEmpty(deckNo))
            {
                return;
            }
            int iSPNo, iDeckNo;
            if (!int.TryParse(spNo, out iSPNo))
            {
                return;
            }
            if (!int.TryParse(deckNo, out iDeckNo))
            {
                return;
            }

            if (_mc.DeckPerCore == 2)
            {
                if (rb1Only.IsChecked == true)
                {
                    iDeckNo = 1;
                    iSPNo++;
                    txtSPNo.Text = iSPNo.ToString();
                }
                else if (rb2Only.IsChecked == true)
                {
                    iDeckNo = 2;
                    iSPNo++;
                    txtSPNo.Text = iSPNo.ToString();
                }
                else if (rbToggle.IsChecked == true)
                {
                    if (iDeckNo == 1)
                    {
                        iDeckNo = 2;
                    }
                    else
                    {
                        iDeckNo = 1;
                        iSPNo++;
                    }
                }
                else
                {

                }
            }
            else
            {
                iSPNo++;
            }

            if (iSPNo > _mc.EndCore) iSPNo = _mc.StartCore;

            txtDeckNo.Text = iDeckNo.ToString();
            txtSPNo.Text = iSPNo.ToString();
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
            RefreshCurrentPallet();

            _mc = mc;
            _pcCard = pcCard;

            EnableScanOption();

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
