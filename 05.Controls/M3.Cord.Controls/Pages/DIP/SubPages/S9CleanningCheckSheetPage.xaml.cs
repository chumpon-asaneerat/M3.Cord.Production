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
    /// Interaction logic for S9CleanningCheckSheetPage.xaml
    /// </summary>
    public partial class S9CleanningCheckSheetPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S9CleanningCheckSheetPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPPCCard pcCard = null;
        private DIPMC mc = null;
        private S9CleanCheckSheet sheet = null;
        private List<S9CleanCheckSheetItem> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPOperationMenu();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        #endregion

        #region Combobox Handlers

        private void cbS9MC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mc = cbS9MC.SelectedItem as DIPMC;

            RefreshGrid(true);
        }

        #endregion

        #region Private Methods

        private void LoadComcoBox()
        {
            cbS9MC.ItemsSource = DIPMC.Gets("S-9").Value();
            cbS9MC.SelectedIndex = -1;
        }

        private void RefreshGrid(bool bInit)
        {
            grid.ItemsSource = null;

            if (null != sheet && null != mc)
            {
                if (null == items || bInit)
                {
                    // Init all core
                    items = new List<S9CleanCheckSheetItem>();
                    for (int i = mc.StartCore; i <= mc.EndCore; i++)
                    {
                        items.Add(new S9CleanCheckSheetItem() { SPNo = i });
                    }

                    var existItems = S9CleanCheckSheetItem.Gets(sheet.CleanId).Value();
                    if (null != existItems && existItems.Count > 0)
                    {
                        foreach (var existItem in existItems)
                        {
                            int idx = items.FindIndex((item =>
                            {
                                return (existItem.SPNo == item.SPNo);
                            }));
                            if (idx != -1 && null != items[idx])
                            {
                                var item = items[idx];
                                if (null != item)
                                {
                                    item.CleanId = existItem.CleanId;
                                    item.SPNo = existItem.SPNo;

                                    item.CheckSplitter = existItem.CheckSplitter;
                                    item.CheckRollerCeramic = existItem.CheckRollerCeramic;
                                    item.CheckSlidebar = existItem.CheckSlidebar;
                                    item.CheckRollerCambox = existItem.CheckRollerCambox;
                                    item.CheckTopGuide = existItem.CheckTopGuide;
                                    item.CheckBottomGuide = existItem.CheckBottomGuide;
                                    item.CheckRollerFrontS9 = existItem.CheckRollerFrontS9;
                                }
                            }
                        }
                    }
                }
            }

            grid.ItemsSource = items;
        }

        private void Save()
        {
            if (null != sheet)
            {
                if (null != mc)
                {
                    sheet.MCCode = mc.MCCode;
                }

                sheet.UserName = M3CordApp.Current.User.FullName; // set current user
                S9CleanCheckSheet.Save(sheet);

                if (sheet.CleanId.HasValue)
                {
                    foreach (var item in items)
                    {
                        item.CleanId = sheet.CleanId.Value;
                        S9CleanCheckSheetItem.Save(item);
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            LoadComcoBox();

            pcCard = DIPUI.PCCard.Current();
            if (null != pcCard)
            {
                var sheets = S9CleanCheckSheet.Gets(pcCard.DIPPCId.Value).Value();
                sheet = (null != sheets) ? sheets.LastOrDefault() : null;
                if (null == sheet)
                {
                    sheet = new S9CleanCheckSheet();
                    sheet.DIPPCId = pcCard.DIPPCId.Value;
                    sheet.CheckDate = DateTime.Now;
                }
                else
                {
                    cbS9MC.SelectedValue = sheet.MCCode;
                }
            }

            paCondition.DataContext = pcCard;
            paSheetInfo.DataContext = sheet;

            this.InvokeAction(() =>
            {
                RefreshGrid(true);
            });
        }

        #endregion
    }
}
