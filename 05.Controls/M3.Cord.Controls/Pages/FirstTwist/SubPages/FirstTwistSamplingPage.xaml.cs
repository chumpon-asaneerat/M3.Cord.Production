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
    /// Interaction logic for FirstTwistSamplingPage.xaml
    /// </summary>
    public partial class FirstTwistSamplingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistSamplingPage()
        {
            InitializeComponent();

            s1.DataContext = null;
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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
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

        #endregion

        #region Public Methods

        private void UpdateUI()
        {
            this.DataContext = null;

            s1.DataContext = null;

            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;

            if (pcCard != null)
            {
                var items = CordSamplingDetails.Gets(pcCard.MCCode, pcCard.ProductLotNo, pcCard.ProductCode).Value();
                var item = (null != items) ? items.FirstOrDefault() : null;
                s1.Setup(selectedMC, pcCard, item);
            }
            this.DataContext = pcCard;
        }

        public void RefreshGrids()
        {
            UpdateUI();
        }

        public void Setup(FirstTwistMC mc)
        {
            try
            {
                selectedMC = mc;
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
