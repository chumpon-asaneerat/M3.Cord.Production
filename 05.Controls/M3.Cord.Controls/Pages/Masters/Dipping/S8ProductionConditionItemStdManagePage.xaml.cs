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
    /// Interaction logic for S8ProductionConditionItemStdManagePage.xaml
    /// </summary>
    public partial class S8ProductionConditionItemStdManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public S8ProductionConditionItemStdManagePage()
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
            bool std1 = SaveStd1();
            bool std2 = SaveStd2();

            if (std1 && std2)
                M3CordApp.Windows.SaveSuccess();
            else M3CordApp.Windows.SaveFailed();
        }

        private bool SaveStd1()
        {
            if (null == cond.DataContext) 
                return false;
            var std = cond.DataContext as S8ProductionConditionItemStd;
            if (null == std) 
                return false;

            var ret = S8ProductionConditionItemStd.Save(std);
            if (null != ret && ret.Ok)
                return true;
            else return false;
        }

        private bool SaveStd2()
        {
            if (null == cond2.DataContext)
                return false;
            var std = cond2.DataContext as S8x2ProductionConditionItemStd;
            if (null == std)
                return false;

            var ret = S8x2ProductionConditionItemStd.Save(std);
            if (null != ret && ret.Ok)
                return true;
            else return false;
        }

        private void RefreshGrid()
        {
            var product = cbProducts.SelectedItem as Product;
            if (product != null)
            {
                string productCode = product.ProductCode;
                // S-8-1
                {
                    var items = S8ProductionConditionItemStd.Gets(productCode).Value();
                    var std = (null != items) ? items.FirstOrDefault() : null;
                    if (null == std)
                    {
                        std = new S8ProductionConditionItemStd();
                        std.ProductCode = productCode;
                    }
                    cond.DataContext = std;
                    cond.IsEnabled = true;
                }
                // S-8-2
                {
                    var items = S8x2ProductionConditionItemStd.Gets(productCode).Value();
                    var std = (null != items) ? items.FirstOrDefault() : null;
                    if (null == std)
                    {
                        std = new S8x2ProductionConditionItemStd();
                        std.ProductCode = productCode;
                    }
                    cond2.DataContext = std;
                    cond2.IsEnabled = true;
                }
            }
            else
            {
                cond.DataContext = null;
                cond.IsEnabled = false;

                cond2.DataContext = null;
                cond2.IsEnabled = false;
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            cbProducts.ItemsSource = Product.GetDipProducts(null).Value();
        }

        #endregion
    }
}
