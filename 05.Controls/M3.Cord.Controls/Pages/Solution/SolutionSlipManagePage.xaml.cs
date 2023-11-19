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
    /// Interaction logic for SolutionSlipManagePage.xaml
    /// </summary>
    public partial class SolutionSlipManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SolutionSlipManagePage()
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

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSolutionLotNo.Text))
            {
                return;
            }
            RefreshGrid();
        }

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {
            item = new SolutionLotLabel(); // Create New

            EnableInputs();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                item = null;
                RefreshGrid();
                ClearInputs();
            });
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (null != item)
            {
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
                if (!dtMixDate.SelectedDate.HasValue)
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("Please Enter Mixing Date.");
                    win.ShowDialog();
                    return;
                }
                if (!dtQualifiedDate.SelectedDate.HasValue)
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("Please Enter Qualified Date.");
                    win.ShowDialog();
                    return;
                }
                if (!dtExpireDate.SelectedDate.HasValue)
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("Please Enter Expire Date.");
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
                item.ProductCode = product.ProductCode;
                item.SolutionId = chemical.SolutionId;
                item.SolutionName = chemical.SolutionName;
                item.DipSolutionQty = val;
                item.MixDate = dtMixDate.SelectedDate.Value;
                item.QualifiedDate = dtQualifiedDate.SelectedDate.Value;
                item.ExpireDate = dtExpireDate.SelectedDate.Value;
                item.CreateBy = M3CordApp.Current.User.UserId;
                item.CreateDate = DateTime.Now;

                var ret = SolutionLotLabel.Save(item);
                if (!ret.HasError)
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

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (null == item) return;

            // Show Print Preview
            var page = M3CordApp.Pages.SolitionSlipPreview;
            var items = new List<SolutionLotLabel>();
            items.Add(item);
            page.Setup(items, true); // reprint
            PageContentManager.Instance.Current = page;
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
            LoadChemicals();
        }

        private void cbChemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == grid.ItemsSource) 
                return;

            item = grid.SelectedItem as SolutionLotLabel;
            UpdateUI();
        }

        #endregion

        #region Private Methods

        private void ClearInputs()
        {
            item = null;
            grid.ItemsSource = null;

            txtSolutionLotNo.Text = string.Empty;

            txtQty.Text = string.Empty;

            dtMixDate.SelectedDate = new DateTime?();
            dtQualifiedDate.SelectedDate = new DateTime?();
            dtExpireDate.SelectedDate = new DateTime?();

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

            dtMixDate.IsEnabled = false;
            dtQualifiedDate.IsEnabled = false;
            dtExpireDate.IsEnabled = false;

            cbProducts.IsEnabled = false;
            cbChemicals.IsEnabled = false;

            dtMixDate.SelectedDate = DateTime.Now;
            dtQualifiedDate.SelectedDate = DateTime.Now;
            dtExpireDate.SelectedDate = DateTime.Now;
        }

        private void EnableInputs()
        {
            txtQty.IsReadOnly = false;

            dtMixDate.IsEnabled = true;
            dtQualifiedDate.IsEnabled = true;
            dtExpireDate.IsEnabled = true;

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

                dtMixDate.SelectedDate = item.MixDate.Value;
                dtQualifiedDate.SelectedDate = item.QualifiedDate.Value;
                dtExpireDate.SelectedDate = item.ExpireDate.Value;

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
