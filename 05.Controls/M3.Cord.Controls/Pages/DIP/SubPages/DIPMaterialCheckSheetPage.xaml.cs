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

        private DIPPCCard pcCard = null;
        private DIPMC mc = null;
        private DIPMaterialCheckSheet sheet = null;
        private List<DIPMaterialCheckSheetItem> items = new List<DIPMaterialCheckSheetItem>();

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPOperationMenu();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        #endregion

        #region Combobox Handlers

        private void cbS7MC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mc = cbS7MC.SelectedItem as DIPMC;
        }

        #endregion

        #region TextBox Handlers

        private void txtSPNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddItem();
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
        }

        private void txtCHNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddItem();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void LoadComcoBox()
        {
            cbS7MC.ItemsSource = DIPMC.Gets("S-7").Value();
            cbS7MC.SelectedIndex = -1;
        }

        private void ResetTextBoxInputs()
        {
            txtSPNo.Text = (null != mc) ? mc.StartCore.ToString() : string.Empty;
            txtLotNo.Text = string.Empty;
            txtCHNo.Text = string.Empty;
        }

        private void ResetCheckBoxInputs()
        {
            chkCheckYarnNo.IsChecked = false;
            chkCheckYanScrap.IsChecked = false;
            chkCheckYarnBall.IsChecked = false;
            chkCheckCover.IsChecked = false;
            chkCheckSensor.IsChecked = false;
            chkCheckDustFilter.IsChecked = false;
        }

        private void AddItem()
        {
            #region Check empty inputs

            if (string.IsNullOrWhiteSpace(txtSPNo.Text))
            {
                txtSPNo.FocusControl();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLotNo.Text))
            {
                txtLotNo.FocusControl();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCHNo.Text))
            {
                txtCHNo.FocusControl();
                return;
            }

            #endregion

            #region Get SPNo/LotNo/CHNo

            int iSP;
            if (!int.TryParse(txtSPNo.Text, out iSP))
            {
                iSP = -1;
            }

            int iCH;
            if (!int.TryParse(txtCHNo.Text, out iCH))
            {
                iCH = 0;
            }

            string lotNo = txtLotNo.Text;

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
                item.CHNo = iCH;

                item.CheckYarnNo = chkCheckYarnNo.IsChecked.Value;
                item.CheckYanScrap = chkCheckYanScrap.IsChecked.Value;
                item.CheckYarnBall = chkCheckYarnBall.IsChecked.Value;
                item.CheckCover = chkCheckCover.IsChecked.Value;
                item.CheckSensor = chkCheckSensor.IsChecked.Value;
                item.CheckDustFilter = chkCheckDustFilter.IsChecked.Value;
            }
            else
            {
                item = new DIPMaterialCheckSheetItem();

                item.SPNo = iSP;
                item.LotNo = lotNo;
                item.CHNo = iCH;

                item.CheckYarnNo = chkCheckYarnNo.IsChecked.Value;
                item.CheckYanScrap = chkCheckYanScrap.IsChecked.Value;
                item.CheckYarnBall = chkCheckYarnBall.IsChecked.Value;
                item.CheckCover = chkCheckCover.IsChecked.Value;
                item.CheckSensor = chkCheckSensor.IsChecked.Value;
                item.CheckDustFilter = chkCheckDustFilter.IsChecked.Value;

                items.Add(item);
            }

            RefreshGrid();

            #endregion

            #region Reset Inputs

            ResetTextBoxInputs();
            ResetCheckBoxInputs();

            // Increase next SP
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

            #endregion
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
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
                DIPMaterialCheckSheet.Save(sheet);

                if (sheet.MaterialCheckId.HasValue)
                {
                    foreach (var item in items)
                    {
                        item.MaterialCheckId = sheet.MaterialCheckId.Value;
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            sheet = null;

            paCondition.DataContext = null;
            paSheetInfo.DataContext = null;

            LoadComcoBox();

            pcCard = DIPUI.PCCard.Current();
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
                else
                {
                    cbS7MC.SelectedValue = sheet.MCCode;
                }
            }

            paCondition.DataContext = pcCard;
            paSheetInfo.DataContext = sheet;

            this.InvokeAction(() =>
            {
                ResetTextBoxInputs();
                ResetCheckBoxInputs();
            });
        }

        #endregion
    }
}
