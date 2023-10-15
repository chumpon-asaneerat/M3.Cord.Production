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
    /// Interaction logic for AgeingManagePage.xaml
    /// </summary>
    public partial class AgeingManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AgeingManagePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private PalletSetting pallet1 = null;
        private PalletSetting pallet2 = null;

        private PCTwist1 pcCard1 = null;
        private PCTwist1 pcCard2 = null;

        private S5ConditionStd std1 = null;
        private S5ConditionStd std2 = null;

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMainMenu();
        }

        #endregion

        #region TextBox Handlers

        private void txtPalletNo1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string palletCode = txtPalletNo1.Text.Trim();
                UpdatePallet1(palletCode);
                e.Handled = true;
            }
        }

        private void txtPalletNo2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string palletCode = txtPalletNo2.Text.Trim();
                UpdatePallet2(palletCode);
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void UpdatePallet1(string palletCode)
        {
            if (string.IsNullOrEmpty(palletCode))
            {
                txtPalletNo1.Text = string.Empty;
                return;
            }

            //pallet1 = PalletSetting.Search(palletCode).Value();
            if (null != pallet1)
            {
                pcCard1 = PCTwist1.Get(pallet1.PCTwist1Id.Value).Value();
                if (null != pcCard1)
                {
                    var stds = S5ConditionStd.Gets(pcCard1.ProductCode).Value();
                    std1 = (null != stds && stds.Count > 0) ? stds.FirstOrDefault() : null;
                    VerifyCondition();
                }
            }
        }

        private void UpdatePallet2(string palletCode)
        {
            if (string.IsNullOrEmpty(palletCode))
            {
                txtPalletNo2.Text = string.Empty;
                return;
            }

            //pallet2 = PalletSetting.Search(palletCode).Value();
            if (null != pallet2)
            {
                pcCard2 = PCTwist1.Get(pallet2.PCTwist1Id.Value).Value();
                if (null != pcCard2)
                {
                    var stds = S5ConditionStd.Gets(pcCard2.ProductCode).Value();
                    std2 = (null != stds && stds.Count > 0) ? stds.FirstOrDefault() : null;
                    VerifyCondition();
                }
            }
        }

        private void VerifyCondition()
        {

        }

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
