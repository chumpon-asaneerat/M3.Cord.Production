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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for ProductEditorWindow.xaml
    /// </summary>
    public partial class ProductEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordItemYarn> itemYarns = null;
        private Product _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_item.ProductCode))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("กรุณาบันทึก Product Code");
                win.ShowDialog();
                return;
            }

            if (string.IsNullOrEmpty(_item.DIPProductCode))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("กรุณาบันทึก DIP Product Code");
                win.ShowDialog();
                return;
            }

            if (string.IsNullOrEmpty(_item.ProductName))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("กรุณาบันทึก Product Name");
                win.ShowDialog();
                return;
            }

            if (string.IsNullOrEmpty(_item.ItemYarn))
            {
                var win = M3CordApp.Windows.MessageBox;
                win.Setup("กรุณาเลือก Item Yarn");
                win.ShowDialog();
                return;
            }

            DialogResult = true;
        }

        #endregion

        #region Combobox Handlers

        private void cbItemYarns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == _item) return;
            var itemYarn = cbItemYarns.SelectedItem as CordItemYarn;
            if (null == itemYarn) return;
            _item.ItemYarn = itemYarn.ItemYarn;
        }

        #endregion

        #region Private Methods

        private void LoadCombobox()
        {
            cbItemYarns.ItemsSource = null;
            itemYarns = CordItemYarn.Gets().Value();
            cbItemYarns.ItemsSource = itemYarns;
        }

        #endregion

        #region Public Methods

        public void Setup(Product value)
        {
            LoadCombobox();
            _item = value;
            this.DataContext = _item;

            if (null != _item && null != itemYarns) 
            {
                int idx = itemYarns.FindIndex((itemYarn) => itemYarn.ItemYarn == _item.ItemYarn);
                if (idx != -1) 
                {
                    cbItemYarns.SelectedIndex = idx;
                }
            }
        }

        #endregion
    }
}
