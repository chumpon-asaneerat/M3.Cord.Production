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
using NLib.Reports.Rdlc;
using System.Reflection;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for S5ReportPreviewPage.xaml
    /// </summary>
    public partial class S5ReportPreviewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S5ReportPreviewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<S5ConditionPrintModel> _items = null;

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
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        #endregion

        private void Print()
        {
            cmdPrint.Visibility = Visibility.Collapsed;

            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                this.rptViewer.Print(ReportDisplayName);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            cmdPrint.Visibility = Visibility.Visible;

            M3CordApp.Pages.GotoCordMainMenu();
        }

        #region Report methods

        private string ReportDisplayName
        {
            get { return "PalletSetting." + DateTime.Now.ToThaiDateTimeString("ddMMyyyyHHmmssfff"); }
        }

        private RdlcReportModel GetReportModel()
        {
            Assembly assembly = this.GetType().Assembly;
            RdlcReportModel inst = new RdlcReportModel();

            // Set Display Name (default file name).
            inst.DisplayName = ReportDisplayName;

            inst.Definition.EmbededReportName = "M3.Cord.Reports.S5Condition.rdlc";
            inst.Definition.RdlcInstance = RdlcReportUtils.GetEmbededReport(assembly,
                inst.Definition.EmbededReportName);
            // clear reprot datasource.
            inst.DataSources.Clear();

            List<S5ConditionPrintModel> items = new List<S5ConditionPrintModel>();
            if (null != _items)
            {
                foreach (var item in _items)
                {
                    items.Add(item); // Add new because is blank.
                }
            }

            // assign new data source
            RdlcReportDataSource mainDS = new RdlcReportDataSource();
            mainDS.Name = "main"; // the datasource name in the rdlc report.
            mainDS.Items = items; // setup data source
            // Add to datasources
            inst.DataSources.Add(mainDS);

            // Add parameters (if required).
            DateTime today = DateTime.Now;
            string printDate = today.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
            inst.Parameters.Add(RdlcReportParameter.Create("PrintDate", printDate));

            return inst;
        }

        #endregion

        #region Public Methods

        public void Setup(List<S5ConditionPrintModel> items)
        {
            _items = items;

            var model = GetReportModel();
            if (null == model ||
                null == model.DataSources || model.DataSources.Count <= 0 ||
                null == model.DataSources[0] || null == model.DataSources[0].Items)
            {
                this.rptViewer.ClearReport();
            }
            else
            {
                MethodBase med = MethodBase.GetCurrentMethod();
                try
                {
                    this.rptViewer.LoadReport(model);
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                }
                finally
                {
                    this.rptViewer.RefreshReport();
                }
            }
        }

        #endregion
    }
}
