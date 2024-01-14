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
using System.Reflection;

using NLib.Services;
using M3.Cord.Models;
using NLib.Models;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for SolutionDetailPage.xaml
    /// </summary>
    public partial class SolutionDetailPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SolutionDetailPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<SolutionLotLabel> chemicals = null;

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
            M3CordApp.Pages.GotoSolutionMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
            {
                LoadSolutionLotLabel(txtSolutionLotNo.Text);
            }
        }

        private void cmdWeight_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as SolutionLotDetail;
            if (null == item) return;

            var win = M3CordApp.Windows.SolutionWeight;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                if (win.Item != null)
                {
                    if(win.DialogResult == true)
                    {
                        if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
                        {
                            grid.ItemsSource = SolutionLotDetail.Gets(txtSolutionLotNo.Text).Value();
                        }
                    }
                    
                    // do something....
                }
            }
        }

        #endregion

        #region TextBox Handlers

        private void txtSolutionLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
                {
                    LoadSolutionLotLabel(txtSolutionLotNo.Text);
                }
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbChemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var chemicals = cbChemicals.SelectedItem as SolutionLotLabel;
                if (chemicals != null)
                {
                    string solutionlot = chemicals.SolutionLot;
                    int solutionid = chemicals.SolutionId;
                    string recipe = null;

                    txtProductCode.Text = chemicals.ProductCode;
                    txtCompound.Text = chemicals.Compound;
                    dtMixDate.SelectedDate = chemicals.MixDate;

                    if (chemicals.DipSolutionQty != null)
                        txtQty.Text = chemicals.DipSolutionQty.Value.ToString("#,##0.##");

                    grid.ItemsSource = SolutionLotDetail.Gets(solutionlot, solutionid, recipe).Value();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == grid.ItemsSource)
                return;
        }

        #endregion

        #region Private Methods

        private void ClearInputs()
        {
            grid.ItemsSource = null;
            dtMixDate.SelectedDate = null;
            txtSolutionLotNo.Text = string.Empty;
            txtCompound.Text = string.Empty;
            txtQty.Text = string.Empty;

            txtProductCode.Text = string.Empty;
            cbChemicals.SelectedIndex = -1;

            this.InvokeAction(() =>
            {
                DisableInputs();
            });
        }

        private void DisableInputs()
        {
            txtCompound.IsReadOnly = true;
            dtMixDate.IsEnabled = false;
            txtQty.IsReadOnly = true;

            txtProductCode.IsEnabled = false;
            //cbChemicals.IsEnabled = true;
        }


        private void LoadSolutionLotLabel(string solutionlot)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var ret = SolutionLotLabel.Gets(solutionlot).Value();

                if (ret != null && ret.Count > 0)
                {
                    //txtProductCode.Text = ret[0].ProductCode;
                    //txtCompound.Text = ret[0].Compound;
                    //dtMixDate.SelectedDate = ret[0].MixDate;

                    //if (ret[0].DipSolutionQty != null)
                    //    txtQty.Text = ret[0].DipSolutionQty.Value.ToString("#,##0.##");

                    if (ret[0].SolutionName != null || ret.Count > 1)
                    {
                        chemicals = ret;
                        cbChemicals.ItemsSource = chemicals;
                        cbChemicals.SelectedItem = ret[0].SolutionName;
                    }

                    //int? solutionid = null;
                    //string recipe = string.Empty;
                    //solutionid = ret[0].SolutionId;

                    //grid.ItemsSource = SolutionLotDetail.Gets(solutionlot, solutionid, recipe).Value();
                }
                else
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("ไม่พบ Lot No ที่ระบุ ในระบบ");
                    win.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            ClearInputs();
        }

        #endregion
    }
}
