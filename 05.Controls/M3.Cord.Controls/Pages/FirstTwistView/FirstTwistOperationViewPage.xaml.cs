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
    /// Interaction logic for FirstTwistOperationViewPage.xaml
    /// </summary>
    public partial class FirstTwistOperationViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistOperationViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private PCTwist1 _pcCard;

        #endregion

        #region TabControl Handlers

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tabs.SelectedIndex)
            {
                case 0:
                    // PC Card
                    pagePCCard.RefreshGrids();
                    break;
                case 1:
                    // Raw Material
                    pageRawMat.RefreshGrids();
                    break;
                case 2:
                    // Check Sheet
                    pageCheckSheet.RefreshGrids();
                    break;
                case 3:
                    // MC Condition
                    pageMCCondition.RefreshGrids();
                    break;
                case 4:
                    // MC Condition
                    pageSampling.RefreshGrids();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(PCTwist1 pccard)
        {
            _pcCard = pccard;

            if (null != _pcCard)
            {
                page.HeaderText = "1st Twisting - " + _pcCard.MCCode;
            }
            else
            {
                page.HeaderText = "1st Twisting ";
            }

            pagePCCard.Setup(_pcCard);
            pageRawMat.Setup(_pcCard);
            pageCheckSheet.Setup(_pcCard);
            pageMCCondition.Setup(_pcCard);
            pageSampling.Setup(_pcCard);

            tabs.SelectedIndex = 0; // reset index
        }

        #endregion
    }
}
