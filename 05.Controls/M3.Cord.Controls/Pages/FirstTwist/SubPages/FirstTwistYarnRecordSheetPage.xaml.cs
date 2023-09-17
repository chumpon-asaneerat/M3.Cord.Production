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
using NLib.Models;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for FirstTwistYarnRecordSheetPage.xaml
    /// </summary>
    public partial class FirstTwistYarnRecordSheetPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistYarnRecordSheetPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;
        private PCTwist1Operation _operation = null;

        private List<Twist1CheckSheet> checkSheets = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // First Twist
            var page = M3CordApp.Pages.FirstTwistMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            // Save Check Sheet
            SaveCheckSheets();
        }

        #endregion

        #region Private Methods

        private void UpdateMCStatus()
        {
            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;
            // Binding
            paCheckSheet.DataContext = pcCard;

            RefreshGrids();
        }

        private void RefreshCheckSheets()
        {
            lvCheckSheet.ItemsSource = null;
            if (null == pcCard || null == selectedMC) return;
            checkSheets = Twist1CheckSheet.Gets(0).Value();
            if (null == checkSheets || checkSheets.Count <= 0)
            {
                checkSheets = new List<Twist1CheckSheet>();
                for (int i = selectedMC.StartCore; i < selectedMC.EndCore; i++)
                {
                    checkSheets.Add(new Twist1CheckSheet() { Twist1LoadId = 0, SPNo = i });
                }
            }
            else
            {

            }
            lvCheckSheet.ItemsSource = checkSheets;
        }

        private void RefreshGrids()
        {
            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;

            RefreshGrids();
        }

        private void SaveCheckSheets()
        {
            if (null != checkSheets)
            {
                checkSheets.ForEach(checkSheet => { Twist1CheckSheet.Save(checkSheet); });
            }
            RefreshCheckSheets();
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
            UpdateMCStatus();
        }

        #endregion
    }
}
