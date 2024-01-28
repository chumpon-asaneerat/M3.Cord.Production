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
    /// Interaction logic for FirstTwistYarnRecordSheetViewPage.xaml
    /// </summary>
    public partial class FirstTwistYarnRecordSheetViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistYarnRecordSheetViewPage()
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

        private void cmdNewSheet_Click(object sender, RoutedEventArgs e)
        {
            // Show Check Sheet Dialig
            ShowCheckSheetDialog(null);
        }

        private void cmdDetail_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ctx = (null != button) ? button.DataContext : null;
            var item = ctx as Twist1CheckSheet;
            if (null != item)
            {
                var inst = Twist1CheckSheet.Gets(item.PCTwist1Id, item.Twist1CheckId).Value().FirstOrDefault();
                if (null != inst)
                {
                    ShowCheckSheetDialog(inst);
                }
            }
        }

        #endregion

        #region Private Methods

        private void UpdateMCStatus()
        {
            paCheckSheet.DataContext = null;

            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;
            // Binding
            paCheckSheet.DataContext = pcCard;
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
            RefreshGrids();
        }

        private void ShowCheckSheetDialog(Twist1CheckSheet sheet)
        {
            if (null == selectedMC || null == pcCard)
                return;
            var win = M3CordApp.Windows.Twist1CheckSheetEditor;
            // set display mode
            win.Mode = (null != sheet) ? DisplayMode.Edit : DisplayMode.New;
            win.Setup(selectedMC, pcCard, sheet);
            if (win.ShowDialog() == false) return;

            // reload pc card to refresh last doff/test no. 
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;

            RefreshGrids();
        }

        public void RefreshGrids()
        {
            lvCheckSheet.ItemsSource = null;

            if (null != pcCard && pcCard.PCTwist1Id.HasValue)
            {
                var items = Twist1CheckSheet.Gets(pcCard.PCTwist1Id.Value).Value();
                lvCheckSheet.ItemsSource = items;
            }

            UpdateMCStatus();
        }

        #endregion
    }
}
