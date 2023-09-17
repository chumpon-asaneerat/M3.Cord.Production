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

                    if (null == _pallet)
                    {
                        var win = M3CordApp.Windows.MessageBox;
                        win.Setup("Pallet not avaliable.");
                        win.ShowDialog();
                    }
                    else
                    {
                        // Check is match ItemYarn
                        if (null != _pcCard && null != _pallet &&
                            _pallet.ItemYarn != _pcCard.ItemYarn)
                        {
                            var win = M3CordApp.Windows.MessageBox;
                            win.Setup("Pallet's Item Yarn does not match with PC card.");
                            win.ShowDialog();
                            _pallet = null;
                        }
                    }
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

        private void EditItem()
        {
            this.DataContext = null;
            this.DataContext = _item;

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
                    var loaditem = ret.Value();
                    if (null != loaditem)
                    {
                        int id = loaditem.Twist1LoadId.Value;
                        foreach (var item in _item.Items) 
                        {
                            item.Twist1LoadId = id;
                            Twist1LoadRecordItem.Save(item);
                        }
                    }
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
            if (null == _mc || null == _item || null == _pallet)
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
            // Add to list
            int id = _item.Twist1LoadId.HasValue ? _item.Twist1LoadId.Value : 0;
            AddRecordItem(id, iSPNo, iDeckNo, _pallet.PalletNo, _pallet.TraceNo, yarnBarcode);

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

        private void AddRecordItem(int loadId, int spNo, int deckNo,
            string palletNo, string traceNo,
            string yarnBarcode)
        {
            if (null != _item && null != _item.Items)
            {
                int idx = _item.Items.FindIndex((item) =>
                {
                    return (item.Twist1LoadId == loadId && item.SPNo == spNo && item.DeckNo == deckNo);
                });
                _item.Items.Add(new Twist1LoadRecordItem() 
                { 
                    SPNo = spNo, DeckNo = deckNo,
                    PalletNo = palletNo, TraceNo = traceNo,
                    YarnBarcode = yarnBarcode 
                });

                this.InvokeAction(() => 
                {
                    RefreshGrid();
                });
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != _item)
            {
                grid.ItemsSource = _item.Items;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc, PCTwist1 pcCard, Twist1LoadRecord record = null)
        {
            RefreshCurrentPallet();

            _mc = mc;
            _pcCard = pcCard;
            _item = record;

            EnableScanOption();

            if (record == null)
            {
                NewItem();
            }
            else
            {
                EditItem();
            }

            RefreshGrid();
        }

        #endregion

        #region Public Properties

        public Twist1LoadRecord Record { get { return _item; } }

        #endregion
    }
}
