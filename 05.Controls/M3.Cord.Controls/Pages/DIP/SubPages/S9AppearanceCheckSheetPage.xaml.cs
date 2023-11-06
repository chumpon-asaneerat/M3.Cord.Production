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
    /// Interaction logic for S9AppearanceCheckSheetPage.xaml
    /// </summary>
    public partial class S9AppearanceCheckSheetPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S9AppearanceCheckSheetPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private S9AppearanceCheckSheet sheet = null;
        private List<S9AppearanceCheckSheetItem> items = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPMCMenu();
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
                    items = new List<S9AppearanceCheckSheetItem>();
                    for (int i = mc.StartCore; i <= mc.EndCore; i++)
                    {
                        items.Add(new S9AppearanceCheckSheetItem() { SPNo = i });
                    }

                    var existItems = S9AppearanceCheckSheetItem.Gets(sheet.AppearId).Value();
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
                                    item.AppearId = existItem.AppearId;
                                    item.SPNo = existItem.SPNo;

                                    item.CheckGood = existItem.CheckGood;
                                    item.CheckBad = existItem.CheckBad;
                                    item.Check2Color = existItem.Check2Color;
                                    item.CheckKeiba = existItem.CheckKeiba;
                                    item.CheckWeight = existItem.CheckWeight;
                                    item.CheckFrontTwist = existItem.CheckFrontTwist;
                                    item.CheckBackTwist = existItem.CheckBackTwist;
                                    item.CheckSnarl = existItem.CheckSnarl;
                                    item.CheckTube = existItem.CheckTube;
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
                S9AppearanceCheckSheet.Save(sheet);

                if (sheet.AppearId.HasValue)
                {
                    foreach (var item in items)
                    {
                        item.AppearId = sheet.AppearId.Value;
                        S9AppearanceCheckSheetItem.Save(item);
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            LoadComcoBox();

            if (null != selecteedMC)
            {
                mc = selecteedMC;
                pcCard = DIPUI.PCCard.Current(mc.MCCode);
                if (null != pcCard)
                {
                    var sheets = S9AppearanceCheckSheet.Gets(pcCard.DIPPCId.Value).Value();
                    sheet = (null != sheets) ? sheets.LastOrDefault() : null;
                    if (null == sheet)
                    {
                        sheet = new S9AppearanceCheckSheet();
                        sheet.DIPPCId = pcCard.DIPPCId.Value;
                        sheet.CheckDate = DateTime.Now;
                    }
                    else
                    {
                        cbS9MC.SelectedValue = sheet.MCCode;
                    }
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
