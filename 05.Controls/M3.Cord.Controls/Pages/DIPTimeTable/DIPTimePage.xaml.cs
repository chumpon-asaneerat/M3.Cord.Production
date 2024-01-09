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
    /// Interaction logic for DIPTimePage.xaml
    /// </summary>
    public partial class DIPTimePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPTimePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables


        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoSolutionMenu();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void cmdCreate_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            this.InvokeAction(() =>
            {
                ClearInputs();
            });
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region TextBox Handlers

        private void txtProductCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods
        private void ClearInputs()
        {
            txtProductCode.Text = string.Empty;

        }

        #endregion

        #region Public Methods

        public void Setup(DIPTimeTable value = null)
        {
            if (null == value)
            {
                ClearInputs();
            }
            else
            {
                this.InvokeAction(() =>
                {

                });
            }
        }

        #endregion
    }
}
