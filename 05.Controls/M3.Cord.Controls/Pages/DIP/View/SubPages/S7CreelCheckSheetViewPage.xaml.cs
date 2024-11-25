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
    /// Interaction logic for S7CreelCheckSheetViewPage.xaml
    /// </summary>
    public partial class S7CreelCheckSheetViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S7CreelCheckSheetViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private S7CreelCheckSheet sheet = null;
        private List<S7CreelCheckSheetItem> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPOperationView;
            page.Setup(pcCard);
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Private Methods

        private void RefreshGrid(bool bInit)
        {
            grid.ItemsSource = null;

            if (null != sheet && null != mc)
            {
                if (null == items || bInit)
                {
                    // Init all core
                    items = new List<S7CreelCheckSheetItem>();
                    for (int i = mc.StartCore; i <= mc.EndCore; i++)
                    {
                        items.Add(new S7CreelCheckSheetItem() { SPNo = i });
                    }

                    var existItems = S7CreelCheckSheetItem.Gets(sheet.CreelId).Value();
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
                                    item.CreelId = existItem.CreelId;
                                    item.SPNo = existItem.SPNo;

                                    item.CheckUnstable = existItem.CheckUnstable;
                                    item.CheckNotReachEnd = existItem.CheckNotReachEnd;
                                    item.CheckNotStraight = existItem.CheckNotStraight;
                                    item.CheckHasSound = existItem.CheckHasSound;
                                }
                            }
                        }
                    }
                }
            }

            grid.ItemsSource = items;
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC, DIPPCCard PCCard)
        {
            string mcNo = (selecteedMC.MCCode.EndsWith("1")) ? "1" : "2";
            mc = DIPMC.Gets("S-7", "S-7-" + mcNo).Value().FirstOrDefault();

            pcCard = PCCard;
            if (null != pcCard)
            {
                var sheets = S7CreelCheckSheet.Gets(pcCard.DIPPCId.Value).Value();
                sheet = (null != sheets) ? sheets.LastOrDefault() : null;
                if (null == sheet)
                {
                    sheet = new S7CreelCheckSheet();
                    sheet.DIPPCId = pcCard.DIPPCId.Value;
                    sheet.CheckDate = DateTime.Now;
                }
                // set same as PCCard
                sheet.MCCode = pcCard.MCCode;
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
