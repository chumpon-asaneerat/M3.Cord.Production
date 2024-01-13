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
            M3CordApp.Pages.GotoSolutionMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductCode.Text))
                LoadProductionRecordStd(txtProductCode.Text);
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                ClearInputs();
            });
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        #endregion

        #region TextBox Handlers

        private void txtProductCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrEmpty(txtProductCode.Text))
                    LoadProductionRecordStd(txtProductCode.Text);

                e.Handled = true;
            }
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

            this.DataContext = std;

        }

        private void LoadProductionRecordStd(string productCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var ret = ProductionRecordStd.GetCurrent(productCode).Value();

                if (ret != null)
                {
                    this.DataContext = ret;
                }
                else
                {
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

        #endregion

        #region Public Methods

        public void Setup(ProductionRecordStd value = null)
        {
            if (null == value)
            {
                ClearInputs();
            }
            else
            {
                this.InvokeAction(() =>
                {

                });
            }
        }

        #endregion
    }
}
