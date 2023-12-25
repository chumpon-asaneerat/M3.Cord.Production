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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
           
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

        #endregion

        #region Combobox Handlers

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cbChemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (null == grid.ItemsSource) 
            //    return;

            //item = grid.SelectedItem as SolutionLotLabel;
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

            this.InvokeAction(() =>
            {
                DisableInputs();
            });
        }

        private void DisableInputs()
        {
            txtQty.IsReadOnly = true;
            txtCompound.IsEnabled = false;

            cbProducts.IsEnabled = false;
            cbChemicals.IsEnabled = false;
        }

        private void EnableInputs()
        {
            txtQty.IsReadOnly = false;

            txtCompound.IsEnabled = true;

            cbProducts.IsEnabled = true;
            cbChemicals.IsEnabled = true;
        }

        private void LoadProducts()
        {
            products = Product.Gets().Value();
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
            grid.ItemsSource = null;
            string lotNo = txtSolutionLotNo.Text;
            grid.ItemsSource = SolutionLotLabel.Gets(lotNo).Value();
        }

        #endregion

        #region Public Methods

        public void Setup(SolutionLotLabel value = null)
        {
            LoadProducts();
            if (null == value)
            {
                ClearInputs();
            }
            else
            {
                item = value;
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
