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
using static M3.Cord.AppConsts.Application;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for CordSamplingQAPage.xaml
    /// </summary>
    public partial class CordSamplingQAPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordSamplingQAPage()
        {
            InitializeComponent();

            s1.DataContext = null;
        }

        #endregion

        #region Internal Variables

        private CordSamplingDetails _item;

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            // QA Manu
            var page = M3CordApp.Pages.QAMenu;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            if (_item != null)
            {
                s1.Save();
            }
        }

        #endregion

        #region Public Methods

        private void UpdateUI()
        {
            s1.DataContext = null;

            if (_item != null)
            {
                s1.SetupQA(_item);
            }
        }

        public void RefreshGrids()
        {
            UpdateUI();
        }

        public void Setup(CordSamplingDetails item)
        {
            try
            {
                _item = item;
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
