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

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for FirstTwistPCCardViewPage.xaml
    /// </summary>
    public partial class FirstTwistPCCardViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistPCCardViewPage()
        {
            InitializeComponent();
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

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        #endregion

        #region ListView Handlers

        private void lvPCCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void UpdateMCStatus()
        {
            paPCCard.DataContext = null;
            // Binding
            paPCCard.DataContext = pcCard;
        }

        private void Print()
        {
            if (null == pcCard)
                return;
            var items = PCTwist1Operation.Gets(pcCard.PCTwist1Id.Value).Value();
            // Show Preview Page
            var page = M3CordApp.Pages.FirstTwistPCCardPreview;
            page.Setup(pcCard, items);
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Public Methods

        public void Setup(PCTwist1 selectedPC)
        {
            pcCard = selectedPC;
            RefreshGrids();
        }

        public void RefreshGrids()
        {
            lvPCCards.ItemsSource = null;
            if (null != pcCard)
            {
                var items = PCTwist1Operation.Gets(pcCard.PCTwist1Id.Value).Value();
                lvPCCards.ItemsSource = items;
            }

            UpdateMCStatus();
        }

        #endregion
    }
}
