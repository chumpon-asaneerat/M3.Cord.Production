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
    /// Interaction logic for DIPMaterialCheckSheetPage.xaml
    /// </summary>
    public partial class DIPMaterialCheckSheetPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPMaterialCheckSheetPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private DIPMaterialCheckSheet sheet = null;
        private List<DIPMaterialCheckSheetItem> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPOperationMenu(mc);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ResetTextBoxInputs(true);
            ResetCheckBoxInputs();
        }

        #endregion

        #region TextBox Handlers

        private void txtPallet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddItem();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtPallet.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtSPNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddItem();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtSPNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddItem();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtLotNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtPalletDoffNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddItem();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtPalletDoffNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        private void txtCHNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddItem();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtCHNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void ResetTextBoxInputs(bool clearAll)
        {
            if (clearAll)
            {
                txtPallet.Text = string.Empty;
                txtLotNo.Text = string.Empty;
            }

            txtPalletDoffNo.Text = string.Empty;
            txtCHNo.Text = string.Empty;
            //txtSPNo.Text = (null != mc) ? mc.StartCore.ToString() : string.Empty;
            txtSPNo.Text = string.Empty;
        }

        private void ResetCheckBoxInputs()
        {
            chkCheckYarnNo.IsChecked = new bool?();
            chkCheckYanScrap.IsChecked = new bool?();
            chkCheckYarnBall.IsChecked = new bool?();
            chkCheckCover.IsChecked = new bool?();
            chkCheckSensor.IsChecked = new bool?();
            chkCheckDustFilter.IsChecked = new bool?();
        }

        private void AddItem()
        {
            #region Check empty inputs

            if (string.IsNullOrWhiteSpace(txtPallet.Text))
            {
                txtPallet.FocusControl();
                return;
            }

            // Find lot no by pallet
            string palletNo = txtPallet.Text;

            PalletSetting pallet = PalletSetting.Search(palletNo, PalletStatus.All).Value();
            if (null == pallet)
            {
                txtPallet.FocusControl();
                return;
            }
            string lotNo = (null != pallet) ? pallet.ProductLotNo : string.Empty;
            txtLotNo.Text = lotNo;

            if (string.IsNullOrWhiteSpace(txtSPNo.Text))
            {
                txtSPNo.FocusControl();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPalletDoffNo.Text))
            {
                txtPalletDoffNo.FocusControl();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCHNo.Text))
            {
                txtCHNo.FocusControl();
                return;
            }

            #endregion

            #region Get SPNo/LotNo/PalletDoffNo/CHNo

            int iSP;
            if (!int.TryParse(txtSPNo.Text, out iSP))
            {
                iSP = -1;
            }
            // Check range.
            if (null != mc && 
                (iSP < mc.StartCore || iSP > mc.EndCore))
            {
                // Invalid range
                txtSPNo.SelectAll();
                txtSPNo.FocusControl();
                return;
            }

            int iPalletDoff;
            if (!int.TryParse(txtPalletDoffNo.Text, out iPalletDoff))
            {
                iPalletDoff = 0;
            }

            int iCH;
            if (!int.TryParse(txtCHNo.Text, out iCH))
            {
                iCH = 0;
            }

            #endregion

            #region Add/Update

            int idx = items.FindIndex((match) =>
            {
                return match.SPNo == iSP;
            });

            DIPMaterialCheckSheetItem item;
            if (idx != -1)
            {
                item = items[idx];

                item.LotNo = lotNo;
                item.DoffNo = iPalletDoff;
                item.CHNo = iCH;

                item.CheckYarnNo = (chkCheckYarnNo.IsChecked.HasValue) ? chkCheckYarnNo.IsChecked.Value : new bool?();
                item.CheckYanScrap = (chkCheckYanScrap.IsChecked.HasValue) ? chkCheckYanScrap.IsChecked.Value : new bool?();
                item.CheckYarnBall = (chkCheckYarnBall.IsChecked.HasValue) ? chkCheckYarnBall.IsChecked.Value : new bool?();
                item.CheckCover = (chkCheckCover.IsChecked.HasValue) ? chkCheckCover.IsChecked.Value : new bool?();
                item.CheckSensor = (chkCheckSensor.IsChecked.HasValue) ? chkCheckSensor.IsChecked.Value : new bool?();
                item.CheckDustFilter = (chkCheckDustFilter.IsChecked.HasValue) ? chkCheckDustFilter.IsChecked.Value : new bool?();
            }
            else
            {
                item = new DIPMaterialCheckSheetItem();

                item.SPNo = iSP;
                item.LotNo = lotNo;
                item.DoffNo = iPalletDoff;
                item.CHNo = iCH;

                item.CheckYarnNo = (chkCheckYarnNo.IsChecked.HasValue) ? chkCheckYarnNo.IsChecked.Value : new bool?();
                item.CheckYanScrap = (chkCheckYanScrap.IsChecked.HasValue) ? chkCheckYanScrap.IsChecked.Value : new bool?();
                item.CheckYarnBall = (chkCheckYarnBall.IsChecked.HasValue) ? chkCheckYarnBall.IsChecked.Value : new bool?();
                item.CheckCover = (chkCheckCover.IsChecked.HasValue) ? chkCheckCover.IsChecked.Value : new bool?();
                item.CheckSensor = (chkCheckSensor.IsChecked.HasValue) ? chkCheckSensor.IsChecked.Value : new bool?();
                item.CheckDustFilter = (chkCheckDustFilter.IsChecked.HasValue) ? chkCheckDustFilter.IsChecked.Value : new bool?();

                items.Add(item);
            }

            #endregion

            #region Reset Inputs

            // Increase next SP
            /*
            if (iSP != -1)
            {
                iSP++;
                if (null != mc && iSP > mc.EndCore) iSP = mc.StartCore;
                txtSPNo.Text = iSP.ToString();
            }
            else
            {
                txtSPNo.Text = string.Empty;
            }
            */

            this.InvokeAction(() =>
            {
                ResetTextBoxInputs(false);
                ResetCheckBoxInputs();

                RefreshGrid(false);

                txtSPNo.FocusControl();
            });

            #endregion
        }

        private void RefreshGrid(bool bInit)
        {
            grid.ItemsSource = null;

            if (null != sheet && null != mc)
            {
                if (null == items || bInit)
                {
                    // Init all core
                    items = new List<DIPMaterialCheckSheetItem>();
                    for (int i = mc.StartCore; i <= mc.EndCore; i++)
                    {
                        items.Add(new DIPMaterialCheckSheetItem() { SPNo = i });
                    }

                    var existItems = DIPMaterialCheckSheetItem.Gets(sheet.MaterialCheckId).Value();
                    if (null != existItems && existItems.Count > 0)
                    {
                        foreach (var existItem in existItems)
                        {
                            int idx = items.FindIndex((item =>
                            {
                                return (existItem.SPNo == item.SPNo);
                            }));
                            if (idx != -1 && null != items[idx])
                            {
                                var item = items[idx];
                                if (null != item)
                                {
                                    item.MaterialCheckId = existItem.MaterialCheckId;
                                    item.SPNo = existItem.SPNo;
                                    item.LotNo = existItem.LotNo;
                                    item.DoffNo = existItem.DoffNo;
                                    item.CHNo = existItem.CHNo;

                                    item.CheckYarnNo = existItem.CheckYarnNo;
                                    item.CheckYanScrap = existItem.CheckYanScrap;
                                    item.CheckYarnBall = existItem.CheckYarnBall;
                                    item.CheckCover = existItem.CheckCover;
                                    item.CheckSensor = existItem.CheckSensor;
                                    item.CheckDustFilter = existItem.CheckDustFilter;
                                }
                            }
                        }
                    }
                }
            }

            grid.ItemsSource = items;
        }

        private void Save()
        {
            if (null != sheet)
            {
                if (null != mc)
                {
                    sheet.MCCode = mc.MCCode;
                }

                sheet.UserName = M3CordApp.Current.User.FullName; // set current user
                var ret = DIPMaterialCheckSheet.Save(sheet);

                if (sheet.MaterialCheckId.HasValue)
                {
                    foreach (var item in items)
                    {
                        item.MaterialCheckId = sheet.MaterialCheckId.Value;
                        DIPMaterialCheckSheetItem.Save(item);
                    }
                }

                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            sheet = null;

            paCondition.DataContext = null;
            paSheetInfo.DataContext = null;

            if (null != selecteedMC)
            {
                mc = selecteedMC;
                pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                if (null != pcCard)
                {
                    var sheets = DIPMaterialCheckSheet.Gets(pcCard.DIPPCId.Value).Value();
                    sheet = (null != sheets) ? sheets.LastOrDefault() : null;
                    if (null == sheet)
                    {
                        sheet = new DIPMaterialCheckSheet();
                        sheet.DIPPCId = pcCard.DIPPCId.Value;
                        sheet.CheckDate = DateTime.Now;
                    }
                    // set same as PCCard
                    sheet.MCCode = pcCard.MCCode;
                    sheet.DoffNo = pcCard.DoffNo;
                }
            }

            paCondition.DataContext = pcCard;
            paSheetInfo.DataContext = sheet;

            this.InvokeAction(() =>
            {
                ResetTextBoxInputs(true);
                ResetCheckBoxInputs();

                RefreshGrid(true);

                txtSPNo.FocusControl();
            });
        }

        #endregion
    }
}
