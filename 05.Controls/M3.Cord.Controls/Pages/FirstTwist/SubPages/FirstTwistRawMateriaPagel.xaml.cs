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
    /// Interaction logic for FirstTwistRawMateriaPagel.xaml
    /// </summary>
    public partial class FirstTwistRawMateriaPagel : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistRawMateriaPagel()
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

        private void cmdLoadYarn_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC || null == pcCard)
                return;
            var win = M3CordApp.Windows.Twist1LoadRecordEditor;
            win.Setup(selectedMC, pcCard, null); // New
            if (win.ShowDialog() == false) return;

            // reload pc card to refresh last doff/test no. 
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;
        }

        #endregion

        #region Private Methods

        private void UpdateMCStatus()
        {
            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;
            // Binding
            paRawMat.DataContext = pcCard;
            cmdLoadYarn.IsEnabled = (null != pcCard);

            RefreshGrids();
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
            UpdateMCStatus();
        }

        public void RefreshGrids()
        {
            lvRawMats.ItemsSource = null;
            if (null == pcCard) return;
            var items = RawMaterialSummary.Gets(pcCard.PCTwist1Id.Value).Value();
            lvRawMats.ItemsSource = items;
        }

        #endregion
    }
}
