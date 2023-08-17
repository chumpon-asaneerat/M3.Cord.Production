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
    /// Interaction logic for ChoosePCCardTwist1Window.xaml
    /// </summary>
    public partial class ChoosePCCardTwist1Window : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChoosePCCardTwist1Window()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null != grid.SelectedItem && grid.SelectedItem is PCCard)
            {
                SelectedPCCard = grid.SelectedItem as PCCard;
            }
            DialogResult = true;
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            if (null != grid.SelectedItem && grid.SelectedItem is PCCard)
            {
                SelectedPCCard = grid.SelectedItem as PCCard;
            }
            DialogResult = true;
        }

        #endregion

        #region Privete Methods

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            List<PCCard> items;
            items = PCCard.Gets().Value();
            grid.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            SelectedPCCard = null;
            RefreshGrid();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Selected PCCard.
        /// </summary>
        public PCCard SelectedPCCard { get; private set; }

        #endregion
    }
}
