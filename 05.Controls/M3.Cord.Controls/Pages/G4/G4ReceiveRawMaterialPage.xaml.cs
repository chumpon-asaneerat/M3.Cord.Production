﻿#region Using

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
using M3.Cord.Properties;
using NLib.Models;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for G4ReceiveRawMaterialPage.xaml
    /// </summary>
    public partial class G4ReceiveRawMaterialPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G4ReceiveRawMaterialPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<G4Yarn> receives = null;

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMainMenu();
        }

        #endregion

        #region TextBox Handlers

        private void txtTraceNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter ||
                e.Key == System.Windows.Input.Key.Return)
            {
                ReceivedTraceNo();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                ClearTraceNo();
                e.Handled = true;
            }
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as G4Yarn;
            EditItem(item);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as G4Yarn;
            DeleteItem(item);
        }

        #endregion

        #region Private Methods

        private void ReceivedTraceNo()
        {
            string traceNo = txtTraceNo.Text.Trim();

            int idx = receives.FindIndex(oitem => { return oitem.TraceNo.Trim() == traceNo; });
            if (idx != -1)
            {
                // duplicate.
                txtTraceNo.Text = string.Empty;
                return;
            }

            var item = SerachByTranceNo(traceNo);
            if (null != item)
            {
                var win = M3CordApp.Windows.G4ReceiveYarn;
                win.Setup(item);
                if (win.ShowDialog() == true)
                {
                    receives.Add(item);
                    RefreshGrid();
                }
            }
            txtTraceNo.Text = string.Empty;
        }

        private void ClearTraceNo()
        {
            txtTraceNo.Text = string.Empty;
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            grid.ItemsSource = receives;
        }

        private G4Yarn SerachByTranceNo(string tranceNo)
        {
            var ret = G4Yarn.Get(tranceNo).Value();
            return ret;
        }

        private void EditItem(G4Yarn item)
        {
            if (null == item) return;
            var win = M3CordApp.Windows.G4ReceiveYarn;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                RefreshGrid();
            }
        }

        private void DeleteItem(G4Yarn item)
        {
            if (null == item) return;
            int idx = receives.IndexOf(item);
            if (idx == -1) return;
            receives.RemoveAt(idx);
            RefreshGrid();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup()
        {
            receives = new List<G4Yarn>();
            RefreshGrid();
        }

        #endregion
    }
}
