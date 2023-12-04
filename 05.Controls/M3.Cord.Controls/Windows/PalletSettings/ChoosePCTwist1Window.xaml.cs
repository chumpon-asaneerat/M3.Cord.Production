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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for ChoosePCTwist1Window.xaml
    /// </summary>
    public partial class ChoosePCTwist1Window : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChoosePCTwist1Window()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<PCTwist1> pcCards = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            Item = grid.SelectedItem as PCTwist1;
            DialogResult = true;
        }

        #endregion

        #region List View Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmdOk.IsEnabled = (null != grid.SelectedItem);
        }

        #endregion

        #region Public Methods

        public void Setup(List<PCTwist1> items)
        {
            cmdOk.IsEnabled = false;

            grid.ItemsSource = null;
            pcCards = items;
            grid.ItemsSource = pcCards;
        }

        #endregion

        #region Public Properties

        public PCTwist1 Item { get; set; }

        #endregion
    }
}
