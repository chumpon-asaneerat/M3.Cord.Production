#region Using

using M3.Cord.Models;
using NLib.Services;
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

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for CordReceiveYarnPage.xaml
    /// </summary>
    public partial class CordReceiveYarnPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordReceiveYarnPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<CordYarn> sources = CordYarn.GetCordYarns();
        private List<CordYarn> items = null;

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            GotoMainMenu();
        }

        private void cmdPalletSearch_Click(object sender, RoutedEventArgs e)
        {
            // generate sample data
            items = new List<CordYarn>(sources.ToArray());
            RefreshGrid();
        }

        private void cmdYarnCHSearch_Click(object sender, RoutedEventArgs e)
        {
            // generate sample data
            items.ForEach(item =>
            {
                item.MarkReceive(DateTime.Today);
            });
            CalcTotals();
            RefreshGrid();
        }

        private void cmdReceive_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as CordYarn;
            if (null != item)
            {
                item.MarkReceive(DateTime.Today);
                CalcTotals();
            }
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as CordYarn;
            if (null != item)
            {
                item.UnmarkReceive();
                CalcTotals();
            }
        }

        #endregion

        #region Private Methods

        private void GotoMainMenu()
        {
            // Init Main Menu
            var page = M3CordApp.Pages.CordMainMenu;
            PageContentManager.Instance.Current = page;
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            grid.ItemsSource = items;

            CalcTotals();
        }

        private void CalcTotals()
        {
            int totalPallet = 0;
            decimal totalWeight = decimal.Zero;
            decimal totalCH = decimal.Zero;

            if (null != items)
            {
                items.ForEach(item =>
                {
                    if (item.IsMark)
                    {
                        ++totalPallet;
                        totalWeight += (item.WeightQty.HasValue) ? item.WeightQty.Value : decimal.Zero;
                        totalCH += (item.CH.HasValue) ? item.CH.Value : decimal.Zero;
                    }
                });
            }

            txtTotalPallet.Text = totalPallet.ToString("n0");
            txtTotalWeight.Text = totalWeight.ToString("n1");
            txtTotalCH.Text = totalCH.ToString("n0");
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            //LoadComboBoxes();
            RefreshGrid();
        }

        #endregion
    }
}
