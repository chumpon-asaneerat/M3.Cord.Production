#region Using

using M3.Cord.Models;
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

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for YarnLoadingWindow.xaml
    /// </summary>
    public partial class YarnLoadingWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public YarnLoadingWindow()
        {
            InitializeComponent();
        }

        #endregion

        private List<YarnLoadingItem> yarnLoadingItems = new List<YarnLoadingItem>();

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdScan_Click(object sender, RoutedEventArgs e)
        {
            yarnLoadingItems = YarnLoadingItem.Gets();
            RefreshGrid();
        }

        #endregion

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            grid.ItemsSource = yarnLoadingItems;
        }

        #region Public Methods

        public void Setup()
        {
            RefreshGrid();
        }

        #endregion
    }
}
