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
        private List<DIPMaterialCheckSheetItem> items = null;

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
            ResetCheckBoxInputs();
        }

        private void Save()
        {
            if (null != sheet)
            {
                var mc = cbS7MC.SelectedItem as DIPMC;
                if (null != mc)
                {
                    sheet.MCCode = mc.MCCode;
                }

                DIPMaterialCheckSheet.Save(sheet);
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

            ResetCheckBoxInputs();

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
        }

        #endregion
    }
}
