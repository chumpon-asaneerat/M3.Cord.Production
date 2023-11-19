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
    /// Interaction logic for SolutionSlipPreviewPage.xaml
    /// </summary>
    public partial class SolutionSlipPreviewPage : UserControl
    {
        #region Construcrtor

        /// <summary>
        /// Construcrtor.
        /// </summary>
        public SolutionSlipPreviewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private bool _reprint = false;
        private List<SolutionLotLabel> _items = null;

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
            GoBack();
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
                if (null != _items)
                {
                    this.rptViewer.Print(ReportDisplayName);
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            cmdPrint.Visibility = Visibility.Visible;

            GoBack();
        }

        private void GoBack()
        {
            // Back to Solution Manage page
            var page = M3CordApp.Pages.SolitionSlipManage;
            var item = (null != _items) ? _items[0] : null;
            page.Setup(item); // restore last item
            PageContentManager.Instance.Current = page;
        }

        #region Report methods

        private string ReportDisplayName
        {
            get { return "SolutionSlip." + DateTime.Now.ToThaiDateTimeString("ddMMyyyyHHmmssfff"); }
        }

        private RdlcReportModel GetReportModel()
        {
            Assembly assembly = this.GetType().Assembly;
            RdlcReportModel inst = new RdlcReportModel();

            // Set Display Name (default file name).
            inst.DisplayName = ReportDisplayName;

            inst.Definition.EmbededReportName = "M3.Cord.Reports.SolutionSlipA6.rdlc";
            inst.Definition.RdlcInstance = RdlcReportUtils.GetEmbededReport(assembly,
                inst.Definition.EmbededReportName);
            // clear reprot datasource.
            inst.DataSources.Clear();

            List<SolutionLotLabel> items = new List<SolutionLotLabel>();
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

        public void Setup(List<SolutionLotLabel> items, bool reprint)
        {
            _reprint = reprint;
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
