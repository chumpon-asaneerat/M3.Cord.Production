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
    /// Interaction logic for ProductionRecordPage.xaml
    /// </summary>
    public partial class ProductionRecordPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductionRecordPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables


        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private ProductionRecord std;
        private List<ProductionRecordDetail> detail;

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
            M3CordApp.Pages.GotoDIPOperationMenu(mc);
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLotNo.Text) && cbProducts.SelectedValue != null)
                LoadProductionRecord();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                //txtLotNo.Text = string.Empty;
                cbProducts.SelectedValue = -1;

                ClearInputs();
            });
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdAddDetail_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLotNo.Text) && cbProducts.SelectedValue != null)
                AddDetail();
        }

        #endregion

        #region Combobox Handlers

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtLotNo.Text) && cbProducts.SelectedValue != null)
            //    LoadProductionRecord();
        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == grid.ItemsSource)
                return;
        }

        #endregion

        #region Private Methods
        private void ClearInputs()
        {
            std = new ProductionRecord();
            detail = new List<ProductionRecordDetail>();
            grid.ItemsSource = null;
            this.DataContext = std;

        }

        private void AddDetail()
        {

            var product = cbProducts.SelectedItem as Product;
            string lotNo = string.Empty;
            string productCode = product.ProductCode;

            if (!string.IsNullOrEmpty(txtLotNo.Text))
                lotNo = txtLotNo.Text;

            if (product != null && !string.IsNullOrEmpty(lotNo))
            {
                ProductionRecordDetail d = new ProductionRecordDetail();
                d.LotNo = lotNo;
                d.ProductCode = productCode;
                d.DoffingDate = DateTime.Now;

                if (detail != null)
                {
                    d.DoffingNo = detail.Count + 1;
                }
                else
                {
                    d.DoffingNo = 1;
                }

                detail.Add(d);

                grid.ItemsSource = null;

                if (detail != null)
                {
                    grid.ItemsSource = detail;
                }
            }
        }

        private void LoadProductionRecord()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var product = cbProducts.SelectedItem as Product;
                if (product != null)
                {
                    string lotNo = string.Empty;
                    string productCode = product.ProductCode;

                    if (!string.IsNullOrEmpty(txtLotNo.Text))
                        lotNo = txtLotNo.Text;

                    var std = ProductionRecord.Get(lotNo, productCode).Value();
                    if (null == std)
                    {
                        std = new ProductionRecord();
                        std.ProductCode = productCode;
                    }

                    var stdDetail = ProductionRecordDetail.Gets(lotNo, productCode).Value();

                    if (stdDetail != null)
                    {
                        detail = new List<ProductionRecordDetail>();
                        detail = stdDetail;
                        grid.ItemsSource = detail;
                    }
                        
                    this.DataContext = std;
                    this.IsEnabled = true;
                }
                else
                {
                    this.DataContext = null;
                    this.IsEnabled = false;

                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("ไม่พบ Product Code ที่ระบุ ในระบบ");
                    win.ShowDialog();

                    ClearInputs();
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
                var std = this.DataContext as ProductionRecord;
                if (null == std) return;
                var retD = ProductionRecord.Save(std);

                if (!retD.HasError)
                {
                    SaveDetail();

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

        private void SaveDetail()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                if (null == detail) return;

                if(detail.Count > 0)
                {
                    foreach (var item in detail)
                    {
                        var retD = ProductionRecordDetail.Save(item);

                        if (retD.HasError)
                        {
                            med.Err(retD.ErrMsg.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            cbProducts.ItemsSource = Product.GetDipProducts(null).Value();

            if (null != selecteedMC)
            {
                mc = selecteedMC;
                pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                if (null != pcCard)
                {
                    txtLotNo.Text = pcCard.DIPLotNo;
                    ClearInputs();
                }
            }
           
        }

        #endregion
    }
}
