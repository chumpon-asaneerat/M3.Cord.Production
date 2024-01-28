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

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;
        private PCTwist1Operation _operation = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // First Twist
            var page = M3CordApp.Pages.FirstTwistMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
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
            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;
            // Binding
            paPCCard.DataContext = pcCard;

            // Check current operations
            bool bReachQuota = (null != pcCard && pcCard.TargetQty.HasValue && pcCard.ActualQty.HasValue &&
                pcCard.TargetQty.Value < pcCard.ActualQty.Value);

            var operations = lvPCCards.ItemsSource as List<PCTwist1Operation>;
            if (null != operations && operations.Count > 0)
            {
                int idx = operations.Count - 1;
                _operation = operations[idx];
                if (_operation.EndTime.HasValue)
                {
                    // already end.
                    _operation = null;
                }
            }
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
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
