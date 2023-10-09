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
using Microsoft.SqlServer.Server;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for FirstTwistConditionPage.xaml
    /// </summary>
    public partial class FirstTwistConditionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistConditionPage()
        {
            InitializeComponent();

            s1.DataContext = null;
            s4x1.DataContext = null;
            s4x2.DataContext = null;
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // First Twist
            var page = M3CordApp.Pages.FirstTwistMC;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void RefreshGrids()
        {

        }

        public void Setup(FirstTwistMC mc)
        {
            s1.DataContext = null;
            s4x1.DataContext = null;
            s4x2.DataContext = null;

            selectedMC = mc;

            // Get PC Card if assigned.
            pcCard = (null != selectedMC) ? PCTwist1.Get(selectedMC.MCCode).Value() : null;

            this.DataContext = pcCard;

            if (pcCard != null) 
            {
                if (pcCard.MCCode == "S-1-1" ||
                    pcCard.MCCode == "S-1-2" ||
                    pcCard.MCCode == "S-1-3")
                {
                    s1.DataContext = new S1Condition();

                    s1.Visibility = Visibility.Visible;
                    s4x1.Visibility = Visibility.Collapsed;
                    s4x2.Visibility = Visibility.Collapsed;
                }
                else if (pcCard.MCCode == "S-4-1")
                {
                    s4x1.DataContext = new S4x1Condition();

                    s1.Visibility = Visibility.Collapsed;
                    s4x1.Visibility = Visibility.Visible;
                    s4x2.Visibility = Visibility.Collapsed;
                }
                else if (pcCard.MCCode == "S-4-2")
                {
                    s4x2.DataContext = new S4x2Condition();

                    s1.Visibility = Visibility.Collapsed;
                    s4x1.Visibility = Visibility.Collapsed;
                    s4x2.Visibility = Visibility.Visible;
                }
                else
                {
                    s1.Visibility = Visibility.Collapsed;
                    s4x1.Visibility = Visibility.Collapsed;
                    s4x2.Visibility = Visibility.Collapsed;
                }
            }
        }

        #endregion
    }
}
