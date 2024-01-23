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
    /// Interaction logic for S8ConditionSummaryPage.xaml
    /// </summary>
    public partial class S8ConditionSummaryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S8ConditionSummaryPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPMC mc = null;
        private DIPPCCard pcCard = null;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoDIPOperationMenu(mc);
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        #endregion

        #region Private Methods

        private void Save()
        {

        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            if (null != selecteedMC)
            {
                string mcNo = (selecteedMC.MCCode.EndsWith("1")) ? "1" : "2";
                mc = DIPMC.Gets("S-8", "S-8-" + mcNo).Value().FirstOrDefault();
                if (null != mc)
                {
                    pcCard = DIPUI.PCCard.Current(selecteedMC.MCCode);
                    if (null != pcCard)
                    {

                    }
                }
            }

            //paCondition1.DataContext = pcCard;
            //paCondition2.DataContext = pcCard;
        }

        #endregion
    }
}
