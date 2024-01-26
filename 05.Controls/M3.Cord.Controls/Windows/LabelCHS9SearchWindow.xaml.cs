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
using NLib.Wpf.Controls;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for LabelCHS9SearchWindow.xaml
    /// </summary>
    public partial class LabelCHS9SearchWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LabelCHS9SearchWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        #endregion

        #region CheckBox Handlers

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckUI();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckUI();
        }

        private void cmdPring_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Text Box Handlers

        private void txtDipLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void CheckUI()
        {
            if (chkManual.IsChecked == true) 
            {
                paSP.IsEnabled = true;
            }
            else paSP.IsEnabled = false;
        }

        private void Search()
        {
            string lotNo = txtDipLotNo.Text;
            if (string.IsNullOrEmpty(lotNo)) 
            {
                this.InvokeAction(() => 
                { 
                    txtDipLotNo.Focus();
                });
                return;
            }

            var ret = LabelCHS9Summary.Search(lotNo);
            var Item = ret.Value();
            if (!ret.Ok || null == Item)
            {
                var msg1 = M3CordApp.Windows.MessageBox;
                msg1.Setup("ไม่พบข้อมูล");
                msg1.ShowDialog();
                return;
            }

            try
            {
                txtItemCode.Text = Item.ProductCode;
                txtTotalGood.Text = (Item.TotalGood.HasValue) ? Item.TotalGood.Value.ToString("n0") : "0";
                txtSPStart.Text = (Item.SPStart.HasValue) ? Item.SPStart.Value.ToString("n0") : "0";
                txtSPEnd.Text = (Item.SPEnd.HasValue) ? Item.SPEnd.Value.ToString("n0") : "0";
            }
            catch (Exception) 
            {
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            CheckUI();
        }

        #endregion

        #region Public Properties

        public LabelCHS9Summary Item { get; set; }

        public bool IsManual
        {
            get { return chkManual.IsChecked == true; }  
        }

        public string DIPLotNo
        {
            get 
            {
                return txtDipLotNo.Text;
            }
        }

        public int? SPStart
        {
            get 
            {
                int ret;
                if (!int.TryParse(txtSPStart.Text, out ret))
                {
                    return new int?();
                }
                return ret;
            }
        }

        public int? SPEnd
        {
            get
            {
                int ret;
                if (!int.TryParse(txtSPEnd.Text, out ret))
                {
                    return new int?();
                }
                return ret;
            }
        }

        #endregion
    }
}
