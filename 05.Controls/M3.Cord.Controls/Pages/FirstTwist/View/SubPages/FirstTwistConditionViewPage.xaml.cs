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
    /// Interaction logic for FirstTwistConditionViewPage.xaml
    /// </summary>
    public partial class FirstTwistConditionViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistConditionViewPage()
        {
            InitializeComponent();

            s1.DataContext = null;
            s4x1.DataContext = null;
            s4x2.DataContext = null;
        }

        #endregion

        #region Internal Variables

        private PCTwist1 pcCard;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // First Twist
            var page = M3CordApp.Pages.FirstTwistHistorySearch;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            //Save();
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            if (pcCard != null)
            {
                if (pcCard.MCCode == "S-1-1" ||
                    pcCard.MCCode == "S-1-2" ||
                    pcCard.MCCode == "S-1-3")
                {
                    s1.Save();
                }
                else if (pcCard.MCCode == "S-4-1")
                {
                    s4x1.Save();
                }
                else if (pcCard.MCCode == "S-4-2")
                {
                    s4x2.Save();
                }
            }
        }

        private void Print()
        {
            if (pcCard != null)
            {
                if (pcCard.MCCode == "S-1-1" ||
                    pcCard.MCCode == "S-1-2" ||
                    pcCard.MCCode == "S-1-3")
                {
                    if (null != s1.Condition)
                    {
                        var page = M3CordApp.Pages.S1ConditionPreview;
                        page.Setup(pcCard, new List<S1Condition>() { s1.Condition });
                        PageContentManager.Instance.Current = page;
                    }
                }
                else if (pcCard.MCCode == "S-4-1")
                {
                    if (null != s4x1.Condition)
                    {
                        var page = M3CordApp.Pages.S4x1ConditionPreview;
                        page.Setup(pcCard, new List<S4x1Condition>() { s4x1.Condition });
                        PageContentManager.Instance.Current = page;
                    }
                }
                else if (pcCard.MCCode == "S-4-2")
                {
                    if (null != s4x2.Condition)
                    {
                        var page = M3CordApp.Pages.S4x2ConditionPreview;
                        page.Setup(pcCard, new List<S4x2Condition>() { s4x2.Condition });
                        PageContentManager.Instance.Current = page;
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        private void UpdateUI()
        {
            this.DataContext = null;

            s1.DataContext = null;
            s4x1.DataContext = null;
            s4x2.DataContext = null;

            if (pcCard != null)
            {
                if (pcCard.MCCode == "S-1-1" ||
                    pcCard.MCCode == "S-1-2" ||
                    pcCard.MCCode == "S-1-3")
                {
                    var conds = S1Condition.Gets(pcCard.PCTwist1Id).Value(); // gets
                    S1Condition cond = null;
                    if (null != conds && conds.Count > 0)
                    {
                        cond = conds[conds.Count - 1]; // used last one
                    }
                    s1.Setup(pcCard, cond);

                    s1.Visibility = Visibility.Visible;
                    s4x1.Visibility = Visibility.Collapsed;
                    s4x2.Visibility = Visibility.Collapsed;
                }
                else if (pcCard.MCCode == "S-4-1")
                {
                    var conds = S4x1Condition.Gets(pcCard.PCTwist1Id).Value(); // gets
                    S4x1Condition cond = null;
                    if (null != conds && conds.Count > 0)
                    {
                        cond = conds[conds.Count - 1]; // used last one
                    }
                    s4x1.Setup(pcCard, cond);

                    s1.Visibility = Visibility.Collapsed;
                    s4x1.Visibility = Visibility.Visible;
                    s4x2.Visibility = Visibility.Collapsed;
                }
                else if (pcCard.MCCode == "S-4-2")
                {
                    var conds = S4x2Condition.Gets(pcCard.PCTwist1Id).Value(); // gets
                    S4x2Condition cond = null;
                    if (null != conds && conds.Count > 0)
                    {
                        cond = conds[conds.Count - 1]; // used last one
                    }
                    s4x2.Setup(pcCard, cond);


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
            else
            {
                s1.Visibility = Visibility.Collapsed;
                s4x1.Visibility = Visibility.Collapsed;
                s4x2.Visibility = Visibility.Collapsed;
            }

            this.DataContext = pcCard;
        }

        public void RefreshGrids()
        {
            UpdateUI();
        }

        public void Setup(PCTwist1 selectedPC)
        {
            try
            {
                pcCard = selectedPC;
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
