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
using NLib.Wpf.Controls;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for ProductItemCodeManagePage.xaml
    /// </summary>
    public partial class ProductItemCodeManagePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductItemCodeManagePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordItemYarn> itemYarns = null;
        private List<Product> _items = null;

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
            M3CordApp.Pages.GotoCordMasterMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = new Product();
            Add(item);
        }


        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as Product;
            if (null == item) return;

            Edit(item);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FontAwesomeButton;
            if (null == btn) return;
            var item = btn.DataContext as Product;
            if (null == item) return;

            Delete(item);
        }

        #endregion

        #region TextBox Handlers

        #endregion

        #region Combobox Handlers

        private void cbItemYanrs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                RefreshGrid();
            });
        }

        #endregion

        #region Private Methods

        private void Add(Product item)
        {
            if (null == item) return;
            var win = M3CordApp.Windows.ProductEditor;
            win.Setup(item);

            if (win.ShowDialog() == true)
            {
                var win2 = M3CordApp.Windows.MessageBox;

                var ret = Product.Save(item);
                if (null != ret && ret.Ok)
                    win2.Setup("บันทึกรายการสำเร็จ");
                else
                    win2.Setup("บันทึกรายการไม่สำเร็จ");

                win2.ShowDialog();

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        private void Edit(Product item)
        {
            if (null == item) return;
            var win = M3CordApp.Windows.ProductEditor;
            win.Setup(item);

            if (win.ShowDialog() == true)
            {
                var win2 = M3CordApp.Windows.MessageBox;

                var ret = Product.Save(item);
                if (null != ret && ret.Ok)
                    win2.Setup("บันทึกรายการสำเร็จ");
                else
                    win2.Setup("บันทึกรายการไม่สำเร็จ");

                win2.ShowDialog();

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        private void Delete(Product item)
        {
            if (null == item) return;

            var win = M3CordApp.Windows.MessageBoxOKCancel;
            win.Setup("ต้องการลบรายการใช่หรือไม่");
            if (win.ShowDialog() == true)
            {
                var win2 = M3CordApp.Windows.MessageBox;

                var ret = Product.Delete(item);
                if (null != ret && ret.Ok)
                    win2.Setup("ลบรายการสำเร็จ");
                else
                    win2.Setup("ลบรายการไม่สำเร็จ");

                win2.ShowDialog();

                this.InvokeAction(() =>
                {
                    RefreshGrid();
                });
            }
        }

        private void RefreshGrid()
        {
            string productCode = txtProductCode.Text;
            productCode = (string.IsNullOrEmpty(productCode)) ? productCode.Trim() : null;

            var cordItemYarn = cbItemYanrs.SelectedItem as CordItemYarn;
            string itemYarn = (null != cordItemYarn) ? cordItemYarn.ItemYarn : null;

            grid.ItemsSource = null;
            _items = Product.Search(productCode, itemYarn).Value();
            grid.ItemsSource = _items;
        }

        private void LoadCombobox()
        {
            cbItemYanrs.ItemsSource = null;

            itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;

            this.InvokeAction(() =>
            {
                if (null != itemYarns && itemYarns.Count > 0) cbItemYanrs.SelectedIndex = 0;
            });
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadCombobox();
            RefreshGrid();
        }

        #endregion
    }
}