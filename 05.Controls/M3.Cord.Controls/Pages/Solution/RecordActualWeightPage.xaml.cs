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
    /// Interaction logic for RecordActualWeightPage.xaml
    /// </summary>
    public partial class RecordActualWeightPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RecordActualWeightPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private SolutionLotLabel item = null;
        private SolutionLotDetail itemDetail = null;

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

        private void cmdWeight_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as SolutionLotLabel;
            if (null == item) return;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            item = new SolutionLotLabel();
            string solutionLot = string.Empty;

            #region Check Input

            if (string.IsNullOrEmpty(txtSolutionLotNo.Text))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("Please Enter Lot No.");
                win.ShowDialog();
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("Please Enter QTY.");
                win.ShowDialog();
                return;
            }
            decimal val;
            if (!decimal.TryParse(txtQty.Text, out val))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("Please QTY must be number only.");
                win.ShowDialog();
                return;
            }

            #endregion

            item.SolutionLot = txtSolutionLotNo.Text;

            if (!string.IsNullOrEmpty(item.SolutionLot))
                solutionLot = item.SolutionLot;

            item.CreateBy = M3CordApp.Current.User.UserId;
            item.CreateDate = DateTime.Now;

            var ret = SolutionLotLabel.Save(item);

            if (!ret.HasError)
            {
                if (grid.ItemsSource != null)
                {
                    bool? chkErr = false;
                    SaveSolutionLotDetail d = new SaveSolutionLotDetail();
                    var detail = grid.ItemsSource as List<SolutionLotDetail>;

                    foreach (var itemD in detail)
                    {
                        if (!string.IsNullOrEmpty(itemD.Recipe))
                        {
                            d = new SaveSolutionLotDetail();

                            //d.solutionlot = itemD.SolutionLot;
                            d.solutionlot = solutionLot;
                            d.recipe = itemD.Recipe;
                            d.chemicalno = itemD.ChemicalNo;

                            d.solutionid = itemD.SolutionID;
                            d.recipeorder = itemD.RecipeOrder;
                            d.mixorder = itemD.MixOrder;

                            d.weightcal = itemD.WeightCal;
                            d.weightactual = itemD.WeightActual;
                            d.weightmc = itemD.WeightMc;
                            d.weightdate = itemD.WeightDate;
                            d.weightby = itemD.WeightBy;

                            var retD = SaveSolutionLotDetail.Save(d);

                            if (retD.HasError)
                            {
                                chkErr = true;
                                //break;
                            }
                        }
                    }

                    if (chkErr == false)
                    {
                        this.InvokeAction(() =>
                        {
                            item = null;
                            ClearInputs();
                        });
                    }
                }
                else
                {
                    this.InvokeAction(() =>
                    {
                        item = null;
                        ClearInputs();
                    });
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

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == grid.ItemsSource)
                return;

            itemDetail = grid.SelectedItem as SolutionLotDetail;
        }

        #endregion

        #region Private Methods

        private void ClearInputs()
        {
            item = null;
            grid.ItemsSource = null;
            dtMixDate.SelectedDate = null;
            txtSolutionLotNo.Text = string.Empty;
            txtCompound.Text = string.Empty;
            txtQty.Text = string.Empty;

            txtProductCode.Text = string.Empty;
            txtSolutionName.Text = string.Empty;

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
            txtSolutionName.IsEnabled = false;
        }


        private void LoadSolutionLotLabel(string solutionlot)
        {
            System.Reflection.MethodBase med = System.Reflection.MethodBase.GetCurrentMethod();

            try
            {
                var ret = SolutionLotLabel.Gets(solutionlot).Value();

                if (ret != null && ret.Count > 0)
                {
                    txtProductCode.Text = ret[0].ProductCode;
                    txtSolutionName.Text = ret[0].SolutionName;
                    txtCompound.Text = ret[0].Compound;
                    dtMixDate.SelectedDate = ret[0].MixDate;

                    if (ret[0].DipSolutionQty != null)
                        txtQty.Text = ret[0].DipSolutionQty.Value.ToString("#,##0.##");

                    grid.ItemsSource = SolutionLotDetail.Gets(solutionlot).Value();
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
