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
    /// Interaction logic for AgeingOperationPage.xaml
    /// </summary>
    public partial class AgeingOperationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AgeingOperationPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdAgeingFromRawMaterial_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.AgeingConditionFromRawMaterial;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdAgeingFromTwisting_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.AgeingConditionFromTwisting;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdAgeingFromDIP_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.AgeingConditionFromDIP;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
