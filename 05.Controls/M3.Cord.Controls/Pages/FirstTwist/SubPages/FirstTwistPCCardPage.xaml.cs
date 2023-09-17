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
    /// Interaction logic for FirstTwistPCCardPage.xaml
    /// </summary>
    public partial class FirstTwistPCCardPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistPCCardPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;
        private PCTwist1Operation _operation = null;

        private List<Twist1CheckSheet> checkSheets = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // First Twist
            var page = M3CordApp.Pages.FirstTwistMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSelectPCCard_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC)
                return;
            var win = M3CordApp.Windows.ChoosePCCardTwist1;
            win.Setup();
            if (win.ShowDialog() == false) return;
            if (null != win.SelectedPCCard)
            {
                AddNew(win.SelectedPCCard);
            }
        }

        private void cmdStartDoff_Click(object sender, RoutedEventArgs e)
        {
            if (null == selectedMC || null == pcCard)
                return;
            var win = M3CordApp.Windows.StartTwist1Op;
            _operation = new PCTwist1Operation();
            _operation.PCTwist1Id = pcCard.PCTwist1Id;
            _operation.ProductionDate = DateTime.Now;
            _operation.MCCode = selectedMC.MCCode;
            win.Setup(_operation); // New
            if (win.ShowDialog() == false) return;
            RefreshGrids();
        }

        private void cmdEndDoff_Click(object sender, RoutedEventArgs e)
        {
            if (null != lvPCCards.SelectedItem)
            {
                var selected = lvPCCards.SelectedItem as PCTwist1Operation;
                if (null != selected)
                {
                    _operation = selected;
                }
                else
                {
                    _operation = null;
                }

                // already has end time.
                if (null != _operation && _operation.EndTime.HasValue)
                {
                    _operation = null;
                    return;
                }
            }

            if (null == selectedMC || null == pcCard || null == _operation)
                return;

            var win = M3CordApp.Windows.EndTwist1Op;
            _operation.PCTwist1Id = pcCard.PCTwist1Id;
            _operation.MCCode = selectedMC.MCCode;
            win.Setup(_operation); // New
            if (win.ShowDialog() == false) return;
            RefreshGrids();
        }

        #endregion

        #region ListView Handlers

        private void lvPCCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void AddNew(PCCard pccard)
        {
            if (null != selectedMC && null != pccard)
            {
                var ret = PCTwist1.AddNew(selectedMC, pccard);
                if (ret.Ok)
                {

                }
            }
            UpdateMCStatus();
        }

        private void UpdateMCStatus()
        {
            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;
            // Binding
            paPCCard.DataContext = pcCard;

            RefreshGrids();
        }

        private void RefreshGrids()
        {
            lvPCCards.ItemsSource = null;
            if (null == pcCard) return;
            var items = PCTwist1Operation.Gets(pcCard.PCTwist1Id.Value).Value();
            lvPCCards.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
            UpdateMCStatus();
        }

        #endregion
    }
}
