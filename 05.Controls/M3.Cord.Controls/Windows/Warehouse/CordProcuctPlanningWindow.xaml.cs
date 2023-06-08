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
    /// Interaction logic for CordProcuctPlanningWindow.xaml
    /// </summary>
    public partial class CordProcuctPlanningWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordProcuctPlanningWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordProduct _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null != cbItemYanrs.SelectedItem && cbItemYanrs.SelectedItem is CordItemYarn)
            {
                var itemYarn = cbItemYanrs.SelectedItem as CordItemYarn;
                if (null != _item)
                {
                    _item.ItemYarn = itemYarn.ItemYarn;
                }
            }

            DialogResult = true;
        }

        #endregion

        #region TextBox Handlers

        private int tracecnt = 1;

        private void txtPalletNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (null != _item && !string.IsNullOrEmpty(txtPalletNo.Text.Trim()))
                {
                    _item.AddPallet(txtPalletNo.Text,
                        string.Format("SG1J0{0:D3}", tracecnt), 520, 5200);
                    tracecnt++;
                }

                txtPalletNo.Text = string.Empty;
                RefreshGrid();

                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                txtPalletNo.Text = string.Empty;
                e.Handled = true;
            }
        }

        #endregion

        #region Privete Methods

        private void LoadComboBoxes()
        {
            cbItemYanrs.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != _item)
            {
                grid.ItemsSource = _item.Pallets;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(CordProduct item)
        {
            LoadComboBoxes();

            var itemYarns = cbItemYanrs.ItemsSource as List<CordItemYarn>;

            _item = item;
            this.DataContext = _item;

            tracecnt = 1;
            if (null != _item)
            {
                _item.CalcTotals();

                tracecnt = _item.Pallets.Count + 1;
                int idx = -1;
                if (null != itemYarns)
                {
                    idx = itemYarns.FindIndex(yarn => { return yarn.ItemYarn == item.ItemYarn; });
                }
                this.InvokeAction(() => 
                {
                    cbItemYanrs.SelectedIndex = idx;
                });
            }

            RefreshGrid();
        }

        #endregion
    }
}
