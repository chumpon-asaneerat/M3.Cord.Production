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
    /// Interaction logic for S5StdManagePage.xaml
    /// </summary>
    public partial class S5StdManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public S5StdManagePage()
        {
            InitializeComponent();
        }

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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMasterMenu();
        }

        #endregion

        #region Combobox Handlers

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshGrid();
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            if (null == cond.DataContext) return;
            var std = cond.DataContext as S5ConditionStd;
            if (null == std) return;

            var ret = S5ConditionStd.Save(std);
            if (null != ret && ret.Ok)
                M3CordApp.Windows.SaveSuccess();
            else M3CordApp.Windows.SaveFailed();
        }

        private void RefreshGrid()
        {
            var product = cbProducts.SelectedItem as Product;
            if (product != null)
            {
                string productCode = product.ProductCode;
                var std = S5ConditionStd.Gets(productCode).Value().FirstOrDefault();
                if (null == std)
                {
                    std = new S5ConditionStd();
                    std.ProductCode = productCode;
                }
                cond.DataContext = std;
                cond.IsEnabled = true;
            }
            else
            {
                cond.DataContext = null;
                cond.IsEnabled = false;
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            cbProducts.ItemsSource = Product.Gets().Value();
        }

        #endregion
    }
}
