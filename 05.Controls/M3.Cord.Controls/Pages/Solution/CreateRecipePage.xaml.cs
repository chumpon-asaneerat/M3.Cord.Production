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
    /// Interaction logic for CreateRecipePage.xaml
    /// </summary>
    public partial class CreateRecipePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CreateRecipePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<Product> products = null;
        private List<SolutionRecipe> chemicals = null;
        private SolutionLotLabel item = null;
        private SolutionRecipeDetailSpec itemDetail = null;

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
            if (!string.IsNullOrEmpty(txtSolutionLotNo.Text) && 
                !string.IsNullOrEmpty(txtQty.Text) && cbProducts.SelectedItem != null)
            {
                RefreshGrid();
            }
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
            if (cbProducts.SelectedItem == null)
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("Please Select Item.");
                win.ShowDialog();
                return;
            }
            if (cbChemicals.SelectedItem == null)
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("Please Select Chemical.");
                win.ShowDialog();
                return;
            }

            var product = cbProducts.SelectedItem as Product;
            var chemical = cbChemicals.SelectedItem as SolutionRecipe;

            #endregion

            item.SolutionLot = txtSolutionLotNo.Text;

            if (!string.IsNullOrEmpty(item.SolutionLot))
                solutionLot = item.SolutionLot;

            item.ProductCode = product.ProductCode;
            item.SolutionId = chemical.SolutionId;
            item.SolutionName = chemical.SolutionName;
            item.DipSolutionQty = val;
            item.CreateBy = M3CordApp.Current.User.UserId;
            item.CreateDate = DateTime.Now;

            var ret = SolutionLotLabel.Save(item);

            if (!ret.HasError)
            {
                if (grid.ItemsSource != null)
                {
                    bool? chkErr = false;
                    SaveSolutionLotDetail d = new SaveSolutionLotDetail();
                    var detail = grid.ItemsSource as List<SolutionRecipeDetailSpec>;
                    
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
                            
                            d.weightcal = itemD.CalculationWet;
                            //d.weightactual = itemD.weightactual;
                            //d.weightmc = itemD.weightmc;
                            //d.weightdate = itemD.weightdate;
                            //d.weightby = itemD.weightby;

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
                            RefreshGrid();
                            ClearInputs();
                        });
                    }
                }
                else
                {
                    this.InvokeAction(() =>
                    {
                        item = null;
                        RefreshGrid();
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
                RefreshGrid();
                e.Handled = true;
            }
        }

        private void txtQty_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RefreshGrid();
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadChemicals();
        }

        private void cbChemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbChemicals.SelectedItem != null)
            {
                var chemical = cbChemicals.SelectedItem as SolutionRecipe;
                if (chemical != null)
                    txtCompound.Text = chemical.Compound;
                else
                    txtCompound.Text = string.Empty;
            }
            else
            {
                txtCompound.Text = string.Empty;
            }

            RefreshGrid();
        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == grid.ItemsSource)
                return;

            itemDetail = grid.SelectedItem as SolutionRecipeDetailSpec;
            //UpdateUI();
        }

        #endregion

        #region Private Methods

        private void ClearInputs()
        {
            item = null;
            grid.ItemsSource = null;

            txtSolutionLotNo.Text = string.Empty;
            txtCompound.Text = string.Empty;
            txtQty.Text = string.Empty;

            cbProducts.SelectedIndex = -1;
            cbChemicals.SelectedIndex = -1;

            //this.InvokeAction(() =>
            //{
            //    DisableInputs();
            //});
        }

        private void DisableInputs()
        {
            txtCompound.IsReadOnly = true;
            txtQty.IsReadOnly = true;
            

            cbProducts.IsEnabled = false;
            cbChemicals.IsEnabled = false;
        }

        private void EnableInputs()
        {
            //txtCompound.IsReadOnly = true;
            txtQty.IsReadOnly = false;

            cbProducts.IsEnabled = true;
            cbChemicals.IsEnabled = true;
        }

        private void LoadProducts()
        {
            products = Product.GetDipProducts(null).Value();
            cbProducts.ItemsSource = products;
        }

        private void LoadChemicals()
        {
            var product = cbProducts.SelectedItem as Product;
            var productCode =  (null != product) ? product.ProductCode : null;
            chemicals = SolutionRecipe.Gets(productCode).Value();
            cbChemicals.ItemsSource = chemicals;
        }

        private void SelectProduct(string productCode)
        {
            if (null != products)
            {
                var idx = products.FindIndex((prod) =>
                {
                    return string.CompareOrdinal(prod.ProductCode, productCode) == 0;
                });
                cbProducts.SelectedIndex = idx;
            }
        }

        private void SelectChemical(string solutionName)
        {
            if (null != chemicals)
            {
                var idx = chemicals.FindIndex((chem) => 
                {
                    return string.CompareOrdinal(chem.SolutionName, solutionName) == 0;
                });
                cbChemicals.SelectedIndex = idx;
            }
        }

        private void UpdateUI()
        {
            if (null == item)
            {
                this.InvokeAction(() =>
                {
                    ClearInputs();
                });
            }
            else
            {
                txtSolutionLotNo.Text = item.SolutionLot;

                txtQty.Text = item.DipSolutionQty.ToString();

                this.InvokeAction(() =>
                {
                    DisableInputs();

                    if (null != item)
                    {
                        SelectProduct(item.ProductCode);
                        SelectChemical(item.SolutionName);
                    }
                });
            }
        }

        private void RefreshGrid()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                grid.ItemsSource = null;
                int? solutionid = null;
                string recipe1 = null;
                string recipe2 = null;
                string recipe3 = null;
                string recipe4 = null;
                decimal? qtySpec = null;
                decimal? qty = null;

                var chemical = cbChemicals.SelectedItem as SolutionRecipe;

                if (chemical != null)
                {
                    solutionid = chemical.SolutionId;
                    recipe1 = chemical.Recipe1;
                    recipe2 = chemical.Recipe2;
                    recipe3 = chemical.Recipe3;
                    recipe4 = chemical.Recipe4;
                    qtySpec = chemical.QtySpec;
                }

                if (!string.IsNullOrEmpty(txtQty.Text))
                    qty = decimal.Parse(txtQty.Text);

                grid.ItemsSource = SolutionRecipeDetailSpec.Gets(solutionid, recipe1, recipe2, recipe3, recipe4 , qtySpec, qty).Value();
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region Public Methods

        public void Setup(SolutionLotLabel value = null, SolutionRecipeDetailSpec valueDetail = null)
        {
            LoadProducts();
            if (null == value)
            {
                ClearInputs();
            }
            else
            {
                item = value;

                if(null != valueDetail)
                itemDetail = valueDetail;

                this.InvokeAction(() =>
                {
                    UpdateUI();
                    RefreshGrid();
                });
            }
        }

        #endregion

    }
}
