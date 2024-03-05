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
using System.Reflection;

using System.Drawing;
using System.Drawing.Printing;
using System.Printing;

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

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDipLotNo.Text)
                && !string.IsNullOrEmpty(txtSPStart.Text)
                && !string.IsNullOrEmpty(txtSPEnd.Text))
            {
                if (PrintZebra() == true)
                    DialogResult = false;
            }
            else
            {
                var msg1 = M3CordApp.Windows.MessageBox;
                msg1.Setup("กรุณาระบุข้อมูล");
                msg1.ShowDialog();
            }
        }

        private void cmdQRCode_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDipLotNo.Text)
                && !string.IsNullOrEmpty(txtSPStart.Text)
                && !string.IsNullOrEmpty(txtSPEnd.Text))
            {
                if (PrintZebraQRCode() == true)
                    DialogResult = false;
            }
            else
            {
                var msg1 = M3CordApp.Windows.MessageBox;
                msg1.Setup("กรุณาระบุข้อมูล");
                msg1.ShowDialog();
            }
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

        private void txtDoffNo_PreviewKeyDown(object sender, KeyEventArgs e)
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
            string doffNo = txtDoffNo.Text;
            int iDoffNo;
            if (!int.TryParse(doffNo, out iDoffNo))
            {
                this.InvokeAction(() =>
                {
                    txtDoffNo.Focus();
                });
                return;
            }

            var ret = LabelCHS9Summary.Search(lotNo, iDoffNo);
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

        private bool PrintZebra()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                string lotNo = string.Empty;
                string doffNo = txtDoffNo.Text;
                int iDoffNo;
                if (!int.TryParse(doffNo, out iDoffNo))
                {
                    this.InvokeAction(() =>
                    {
                        txtDoffNo.Focus();
                    });
                    return false;
                }

                int? SPStart = null;
                int? SPEnd = null;

                lotNo = txtDipLotNo.Text;

                if (!string.IsNullOrEmpty(txtSPStart.Text))
                    SPStart = int.Parse(txtSPStart.Text);

                if (!string.IsNullOrEmpty(txtSPEnd.Text))
                    SPEnd = int.Parse(txtSPEnd.Text);

                List<LabelCHS9> items = new List<LabelCHS9>();
                var item = Models.LabelCHS9.Gets(lotNo, iDoffNo, SPStart, SPEnd);
                items = item;
                if (null != items)
                {
                    PrintDialog pDialog = new PrintDialog();
                    pDialog.PageRangeSelection = PageRangeSelection.AllPages;
                    pDialog.UserPageRangeEnabled = true;

                    bool? print = pDialog.ShowDialog();
                    if (print == true)
                    {
                        string barcode1 = string.Empty;
                        string barcode2 = string.Empty;
                        string commandLine = string.Empty;

                        foreach (var result in items)
                        {
                            barcode1 = string.Empty;
                            barcode2 = string.Empty;
                            commandLine = string.Empty;

                            if (!string.IsNullOrEmpty(result.BarcodeText1))
                            {
                                barcode1 = "^CFA,15"
                                    + "^FO40,7^GB50,50,3^FS"
                                    + "^CFA,20"
                                    + "^FWR^FO50,87 ,70,60^FD" + result.BarcodeText1 + "^FS"
                                    + "^CFA,15"
                                    + "^FO100,7"
                                    + "^BA,100,N,N,N"
                                    + "^FD" + result.BarcodeText1 + "^FS";
                            }

                            if (!string.IsNullOrEmpty(result.BarcodeText2))
                            {
                                barcode2 = "^CFA,15"
                                    + "^FO290,7^GB50,50,3^FS"
                                    + "^CFA,20"
                                    + "^FWR^FO300,87,70,60^FD" + result.BarcodeText2 + "^FS"
                                    + "^CFA,15"
                                    + "^FO350,7"
                                    + "^BA,100,N,N,N"
                                    + "^FD" + result.BarcodeText2 + "^FS";
                            }

                            if (!string.IsNullOrEmpty(barcode1) || !string.IsNullOrEmpty(barcode2))
                                commandLine = "^XA" + barcode1+ barcode2+ "^XZ";

                            if (!string.IsNullOrEmpty(commandLine))
                                RawPrinterHelper.SendStringToPrinter(pDialog.PrintQueue.Name, commandLine);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                med.Err(ex);
                return false;
            }
        }

        private bool PrintZebraQRCode()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                string lotNo = string.Empty;
                string doffNo = txtDoffNo.Text;
                int iDoffNo;
                if (!int.TryParse(doffNo, out iDoffNo))
                {
                    this.InvokeAction(() =>
                    {
                        txtDoffNo.Focus();
                    });
                    return false;
                }

                int? SPStart = null;
                int? SPEnd = null;

                lotNo = txtDipLotNo.Text;

                if (!string.IsNullOrEmpty(txtSPStart.Text))
                    SPStart = int.Parse(txtSPStart.Text);

                if (!string.IsNullOrEmpty(txtSPEnd.Text))
                    SPEnd = int.Parse(txtSPEnd.Text);

                List<LabelCHS9> items = new List<LabelCHS9>();
                var item = Models.LabelCHS9.Gets(lotNo, iDoffNo, SPStart, SPEnd);
                items = item;
                if (null != items)
                {
                    PrintDialog pDialog = new PrintDialog();
                    pDialog.PageRangeSelection = PageRangeSelection.AllPages;
                    pDialog.UserPageRangeEnabled = true;

                    bool? print = pDialog.ShowDialog();
                    if (print == true)
                    {
                        string barcode1 = string.Empty;
                        string barcode2 = string.Empty;
                        string commandLine = string.Empty;

                        foreach (var result in items)
                        {
                            barcode1 = string.Empty;
                            barcode2 = string.Empty;
                            commandLine = string.Empty;

                            #region Old
                            //if (!string.IsNullOrEmpty(result.BarcodeText1))
                            //{
                            //    barcode1 = "^CFA,15"
                            //        + "^FO15,330^GB50,50,3^FS"
                            //        + "^CF0,35"
                            //        + "^FWR^FO180,245 ,70,60^FD" + result.ProductCode1 + "^FS"
                            //        + "^CF0,35"
                            //        + "^FWR^FO130,245 ,70,60^FD" + result.LotNoDoffNo1 + "^FS"
                            //        + "^CF0,35"
                            //        + "^FWR^FO80,245 ,70,60^FD" + "SP " + result.SPNo1.ToString() + "^FS"
                            //        + "^CFA,15"
                            //        + "^FO15,20"
                            //        + "^BQ,2,8"
                            //        + "^FDQA " + result.BarcodeText1 + "^FS";
                            //}

                            //if (!string.IsNullOrEmpty(result.BarcodeText2))
                            //{
                            //    barcode2 = "^CFA,15"
                            //        + "^FO270,330^GB50,50,3^FS"
                            //        + "^CF0,35"
                            //        + "^FWR^FO435,245 ,70,60^FD" + result.ProductCode2 + "^FS"
                            //        + "^CF0,35"
                            //        + "^FWR^FO385,245 ,70,60^FD" + result.LotNoDoffNo2 + "^FS"
                            //        + "^CF0,35"
                            //        + "^FWR^FO335,245 ,70,60^FD" + "SP " + result.SPNo2.ToString() + "^FS"
                            //        + "^CFA,15"
                            //        + "^FO270,20"
                            //        + "^BQ,2,8"
                            //        + "^FDQA " + result.BarcodeText2 + "^FS";
                            //}
                            #endregion

                            if (!string.IsNullOrEmpty(result.BarcodeText1))
                            {
                                if (result.ProductCode1 == "P1100ZFT")
                                {
                                    barcode1 = "^CFA,15"
                                        + "^FO1,15^GB50,50,3^FS"
                                        + "^CF0,24"
                                        + "^FWR^FO175,15 ,70,60^FDSAP Code:7 179 172 90^FS"
                                        + "^CF0,24"
                                        + "^FWR^FO150,15 ,70,60^FD" + result.ProductCode1 + "^FS"
                                        + "^CF0,24"
                                        + "^FWR^FO125,15 ,70,60^FD" + result.LotNoDoffNo1 + "^FS"
                                        + "^CF0,24"
                                        + "^FWR^FO100,15 ,70,60^FD" + "SP " + result.SPNo1.ToString() + "^FS"
                                        + "^CFA,15"
                                        + "^FO1,100"
                                        + "^BQ,2,5"
                                        + "^FDQA " + result.BarcodeText1 + "^FS";
                                }
                                else
                                {
                                    barcode1 = "^CFA,15"
                                       + "^FO1,15^GB50,50,3^FS"
                                       + "^CF0,24"
                                       + "^FWR^FO150,15 ,70,60^FD" + result.ProductCode1 + "^FS"
                                       + "^CF0,24"
                                       + "^FWR^FO125,15 ,70,60^FD" + result.LotNoDoffNo1 + "^FS"
                                       + "^CF0,24"
                                       + "^FWR^FO100,15 ,70,60^FD" + "SP " + result.SPNo1.ToString() + "^FS"
                                       + "^CFA,15"
                                       + "^FO1,100"
                                       + "^BQ,2,5"
                                       + "^FDQA " + result.BarcodeText1 + "^FS";
                                }

                                #region Old
                                //barcode1 = "^CFA,15"
                                //    + "^FO5,15^GB50,50,3^FS"
                                //    + "^CF0,30"
                                //    + "^FWR^FO180,15 ,70,60^FD" + result.ProductCode1 + "^FS"
                                //    + "^CF0,30"
                                //    + "^FWR^FO145,15 ,70,60^FD" + result.LotNoDoffNo1 + "^FS"
                                //    + "^CF0,30"
                                //    + "^FWR^FO110,15 ,70,60^FD" + "SP " + result.SPNo1.ToString() + "^FS"
                                //    + "^CFA,15"
                                //    + "^FO5,100"
                                //    + "^BQ,2,5"
                                //    + "^FDQA " + result.BarcodeText1 + "^FS";
                                #endregion
                            }

                            if (!string.IsNullOrEmpty(result.BarcodeText2))
                            {
                                if (result.ProductCode2 == "P1100ZFT")
                                {
                                    barcode2 = "^CFA,15"
                                    + "^FO250,15^GB50,50,3^FS"
                                    + "^CF0,24"
                                    + "^FWR^FO425,15 ,70,60^FDSAP Code:7 179 172 90^FS"
                                    + "^CF0,24"
                                    + "^FWR^FO400,15 ,70,60^FD" + result.ProductCode2 + "^FS"
                                    + "^CF0,24"
                                    + "^FWR^FO375,15 ,70,60^FD" + result.LotNoDoffNo2 + "^FS"
                                    + "^CF0,24"
                                    + "^FWR^FO350,15 ,70,60^FD" + "SP " + result.SPNo2.ToString() + "^FS"
                                    + "^CFA,15"
                                    + "^FO250,100"
                                    + "^BQ,2,5"
                                    + "^FDQA " + result.BarcodeText2 + "^FS";
                                }
                                else
                                {
                                    barcode2 = "^CFA,15"
                                    + "^FO250,15^GB50,50,3^FS"
                                    + "^CF0,24"
                                    + "^FWR^FO400,15 ,70,60^FD" + result.ProductCode2 + "^FS"
                                    + "^CF0,24"
                                    + "^FWR^FO375,15 ,70,60^FD" + result.LotNoDoffNo2 + "^FS"
                                    + "^CF0,24"
                                    + "^FWR^FO350,15 ,70,60^FD" + "SP " + result.SPNo2.ToString() + "^FS"
                                    + "^CFA,15"
                                    + "^FO250,100"
                                    + "^BQ,2,5"
                                    + "^FDQA " + result.BarcodeText2 + "^FS";
                                }

                                #region Old

                                //barcode2 = "^CFA,15"
                                //    + "^FO250,15^GB50,50,3^FS"
                                //    + "^CF0,30"
                                //    + "^FWR^FO425,15 ,70,60^FD" + result.ProductCode2 + "^FS"
                                //    + "^CF0,30"
                                //    + "^FWR^FO390,15 ,70,60^FD" + result.LotNoDoffNo2 + "^FS"
                                //    + "^CF0,30"
                                //    + "^FWR^FO355,15 ,70,60^FD" + "SP " + result.SPNo2.ToString() + "^FS"
                                //    + "^CFA,15"
                                //    + "^FO250,100"
                                //    + "^BQ,2,5"
                                //    + "^FDQA " + result.BarcodeText2 + "^FS";
                                #endregion
                            }

                            if (!string.IsNullOrEmpty(barcode1) || !string.IsNullOrEmpty(barcode2))
                                commandLine = "^XA" + barcode1 + barcode2 + "^XZ";

                            if (!string.IsNullOrEmpty(commandLine))
                                RawPrinterHelper.SendStringToPrinter(pDialog.PrintQueue.Name, commandLine);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                med.Err(ex);
                return false;
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

        public int? DoffNo
        {
            get
            {
                int ret;
                if (!int.TryParse(txtDoffNo.Text, out ret))
                {
                    return new int?();
                }
                return ret;
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
