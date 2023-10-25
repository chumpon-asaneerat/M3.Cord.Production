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
    /// Interaction logic for DIPOperationPage.xaml
    /// </summary>
    public partial class DIPOperationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPOperationPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPPCCard pcCard = null;

        #endregion

        #region Tab Handlers

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Setup()
        {
            pcCard = DIPPCCard.Get().Value();
            if (null == pcCard)
            {
                pcCard = new DIPPCCard();
                var win = M3CordApp.Windows.DIPPCCardEditor;
                win.Setup(pcCard);
                if (win.ShowDialog() == true)
                {
                    DIPPCCard.Save(pcCard);
                }
            }

            if (null != pcCard)
            {

            }
        }

        #endregion
    }
}
