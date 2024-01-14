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

        private List<SolutionLotLabel> chemicals = null;
        private List<SolutionById> dipCondition = null;
        private List<SolutionLotDetail> solutionLotDetail = null;

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

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
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
                    txtProductCode.Text = chemicals.ProductCode;
                    dipCondition = SolutionById.Gets(chemicals.SolutionId).Value();

                    if (dipCondition != null && dipCondition.Count > 0)
                    {
                        cbRecipe.ItemsSource = dipCondition;
                        cbRecipe.SelectedItem = dipCondition[0].recipe;
                    }
                    else
                    {
                        cbRecipe.ItemsSource = null;
                        cbRecipe.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void cbRecipe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var recipe = cbRecipe.SelectedItem as SolutionById;
                if (recipe != null)
                {
                    if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
                        solutionLotDetail = SolutionLotDetail.GetsChemical(txtSolutionLotNo.Text, recipe.SolutionId, recipe.recipe).Value();

                    if (solutionLotDetail != null && solutionLotDetail.Count > 0)
                    {
                        cbChemicalName.ItemsSource = solutionLotDetail;
                        cbChemicalName.SelectedItem = solutionLotDetail[0].ChemicalName;
                    }
                    else
                    {
                        cbChemicalName.ItemsSource = null;
                        cbChemicalName.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void cbChemicalName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var chemical = cbChemicalName.SelectedItem as SolutionLotDetail;

                var weightCal = (null != chemical) ? chemical.WeightCal : null;

                if (weightCal != null)
                    txtWeightCal.Text = weightCal.Value.ToString("#,##0.##");
                else
                    txtWeightCal.Text = string.Empty;
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region Private Methods

        private void ClearInputs()
        {
            txtSolutionLotNo.Text = string.Empty;

            txtProductCode.Text = string.Empty;
            cbChemicals.SelectedIndex = -1;
            cbRecipe.SelectedIndex = -1;
            cbChemicalName.SelectedIndex = -1;
            txtWeightCal.Text = string.Empty;

            txtWeightMc.Text = string.Empty;
            txtWeightActual.Text = string.Empty;

            chemicals = null;
            dipCondition = null;
            solutionLotDetail = null;

            this.InvokeAction(() =>
            {
                DisableInputs();
            });
        }

        private void DisableInputs()
        {
            txtProductCode.IsEnabled = false;
            txtWeightCal.IsEnabled = false;
        }

        private void LoadSolutionLotLabel(string solutionlot)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var ret = SolutionLotLabel.Gets(solutionlot).Value();

                if (ret != null && ret.Count > 0)
                {
                    if (ret[0].SolutionName != null || ret.Count > 1)
                    {
                        chemicals = ret;
                        cbChemicals.ItemsSource = chemicals;
                        cbChemicals.SelectedItem = ret[0].SolutionName;
                    }
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

        private void Save()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                SaveSolutionLotDetail d = new SaveSolutionLotDetail();

                //if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
                //    d.solutionlot = txtSolutionLotNo.Text;

                //var chemicals = cbChemicals.SelectedValue as SolutionLotLabel;
                //if (chemicals != null)
                //{
                //    d.chemicalno = chemicals.SolutionName;
                //    d.solutionid = chemicals.SolutionId;
                //}

                //var recipes = cbRecipe.SelectedValue as SolutionById;
                //if (recipes != null)
                //{
                //    d.recipe = recipes.recipe;
                //}

                var lotDetail = cbChemicalName.SelectedValue as SolutionLotDetail;

                if (lotDetail != null)
                {
                    d.solutionlot = lotDetail.SolutionLot;
                    d.recipe = lotDetail.Recipe;
                    d.chemicalno = lotDetail.ChemicalNo;
                    d.solutionid = lotDetail.SolutionID;
                    d.recipeorder = lotDetail.RecipeOrder;
                    d.mixorder = lotDetail.MixOrder;
                    d.weightcal = lotDetail.WeightCal;
                }

                if (!string.IsNullOrEmpty(txtWeightActual.Text))
                {
                    d.weightactual = decimal.Parse(txtWeightActual.Text);
                }

                if (!string.IsNullOrEmpty(txtWeightMc.Text))
                {
                    d.weightmc = txtWeightMc.Text;
                }

                d.weightdate = DateTime.Now;
                d.weightby = M3CordApp.Current.User.UserId;

                var retD = SaveSolutionLotDetail.Save(d);

                if (!retD.HasError)
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("Save Complete");
                    win.ShowDialog();
                }
                else
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup(retD.ErrMsg);
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
