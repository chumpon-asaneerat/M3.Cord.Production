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
using M3.Cord.Services.Excels;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for DIPConditionViewPage.xaml
    /// </summary>
    public partial class DIPConditionViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPConditionViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;
        private DIPCondition cond = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            var page = M3CordApp.Pages.DIPOperationView;
            page.Setup(pcCard);
            PageContentManager.Instance.Current = page;
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        #endregion

        #region Private Methods

        private void Export()
        {
            DIPConfirmConditionExport.Export(pcCard, cond);
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC, DIPPCCard PCCard)
        {
            paCondition.DataContext = null;
            dip.DataContext = null;

            if (null != selecteedMC)
            {
                mc = selecteedMC;
                pcCard = PCCard;
                if (null != pcCard)
                {
                    var std = DIPConditionStd.Gets(pcCard.ProductCode).Value().FirstOrDefault();
                    cond = DIPCondition.Gets(pcCard.DIPPCId).Value();
                    if (null != cond) 
                    {
                        DIPCondition.Assign(std, cond);
                        cond.DIPPCId = pcCard.DIPPCId;
                        cond.ProductCode = pcCard.ProductCode;

                        cond.UpdateBy = (null != M3CordApp.Current.User) ?
                            M3CordApp.Current.User.FullName : null;
                        cond.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        cond = DIPCondition.Create(pcCard.ProductCode);
                        cond.DIPPCId = pcCard.DIPPCId;
                        cond.ProductCode = pcCard.ProductCode;

                        cond.UpdateBy = (null != M3CordApp.Current.User) ?
                            M3CordApp.Current.User.FullName : null;
                        cond.UpdateDate = DateTime.Now;
                    }
                }
            }

            paCondition.DataContext = pcCard;
            dip.DataContext = cond;
        }

        #endregion
    }
}
