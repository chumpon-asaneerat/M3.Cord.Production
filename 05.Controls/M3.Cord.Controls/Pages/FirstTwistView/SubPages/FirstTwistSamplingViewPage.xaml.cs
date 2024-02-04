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
using static M3.Cord.AppConsts.Application;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for FirstTwistSamplingViewPage.xaml
    /// </summary>
    public partial class FirstTwistSamplingViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistSamplingViewPage()
        {
            InitializeComponent();

            s1.DataContext = null;
        }

        #endregion

        #region Internal Variables

        private PCTwist1 pcCard;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // First Twist
            var page = M3CordApp.Pages.FirstTwistHistorySearch;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            if (pcCard != null)
            {
                s1.Save();
            }
        }

        private void Print()
        {
            if (null == pcCard)
                return;
            var items = CordSamplingDetails.Gets(pcCard.MCCode, pcCard.ProductLotNo, pcCard.ProductCode).Value();
            // Show Preview Page
            var page = M3CordApp.Pages.CordSamplingPreview;
            page.Setup(pcCard, items);
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Public Methods

        private void UpdateUI()
        {
            this.DataContext = null;

            s1.DataContext = null;

            if (pcCard != null)
            {
                var items = CordSamplingDetails.Gets(pcCard.MCCode, pcCard.ProductLotNo, pcCard.ProductCode).Value();
                var item = (null != items) ? items.FirstOrDefault() : null;
                s1.Setup(pcCard, false, item);
            }
            this.DataContext = pcCard;
        }

        public void RefreshGrids()
        {
            UpdateUI();
        }

        public void Setup(PCTwist1 selectedPC)
        {
            try
            {
                pcCard = selectedPC;
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
