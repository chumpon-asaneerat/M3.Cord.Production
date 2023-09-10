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
    /// Interaction logic for FirstTwistOperationPage.xaml
    /// </summary>
    public partial class FirstTwistOperationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistOperationPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;

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
            paRawMat.DataContext = pcCard;

            cmdSelectPCCard.IsEnabled = (null == pcCard);
            cmdLoadYarn.IsEnabled = (null != pcCard);

            RefreshGrids();
        }

        private void RefreshGrids()
        {
            RefreshPCCards();
            RefreshRawMaterials();
            RefreshCheckSheets();
        }

        private void RefreshPCCards()
        {

        }

        private void RefreshRawMaterials()
        {
            lvRawMats.ItemsSource = null;
            if (null == pcCard) return;
            var items = RawMaterialSummary.Gets(pcCard.PCTwist1Id.Value).Value();
            lvRawMats.ItemsSource = items;
        }

        private void RefreshCheckSheets()
        {
            lvCheckSheet.ItemsSource = null;
            if (null == pcCard || null == selectedMC) return;
            var items = Twist1CheckSheet.Gets(0).Value();
            if (null == items || items.Count <= 0)
            {
                items = new List<Twist1CheckSheet>();
                for (int i = selectedMC.StartCore; i < selectedMC.EndCore; i++)
                {
                    items.Add(new Twist1CheckSheet() { Twist1LoadId = 0, SPNo = i });
                }
            }
            else
            {

            }
            lvCheckSheet.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
            if (null !=  selectedMC) 
            {
                page.HeaderText = "1st Twisting - " + selectedMC.MCCode;
            }
            else
            {
                page.HeaderText = "1st Twisting ";
            }
            UpdateMCStatus();
        }

        #endregion
    }
}
