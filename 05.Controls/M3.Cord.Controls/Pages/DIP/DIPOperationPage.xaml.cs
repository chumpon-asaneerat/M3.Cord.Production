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
    /// Interaction logic for DIPOperationPage.xaml
    /// </summary>
    public partial class DIPOperationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPOperationPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdMaterialCheckSheet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdS7CreelCheckSheet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDIPCondition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdChemicalRequisitionRecordSheet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdS9CleaningCheckSheet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdDIPProductionTimeTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdS9AppearanceCheckSheet_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
