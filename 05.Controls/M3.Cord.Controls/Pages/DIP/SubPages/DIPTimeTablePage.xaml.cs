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
    /// Interaction logic for DIPTimeTablePage.xaml
    /// </summary>
    public partial class DIPTimeTablePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPTimeTablePage()
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

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void cmdDetails_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        #endregion

        #region Private Methods

        private void Add()
        {
            if (null == pcCard)
                return;
            var win = M3CordApp.Windows.DIPTimeTableEditor;
            var item = DIPTimeTable.Create(pcCard.ProductCode);
            item.ProductCode = pcCard.ProductCode;
            item.RowType = 1;

            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                RefreshGrid();
            }
        }

        private void Edit()
        {

        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != pcCard)
            {
                if (!dtDate.SelectedDate.HasValue)
                {
                    this.InvokeAction(() =>
                    {
                        dtDate.Focus();
                    });
                    return;
                }
                grid.ItemsSource = DIPTimeTable.Gets(dtDate.SelectedDate.Value.Date).Value();
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPMC selecteedMC)
        {
            if (null != selecteedMC)
            {
                mc = selecteedMC;
                pcCard = DIPUI.PCCard.Current(mc.MCCode);
                if (null != pcCard)
                {
                    /*
                    pcCard.DIPLotNo;
                    pcCard.ProductCode;
                    */
                }
            }
            RefreshGrid();

            paCondition.DataContext = pcCard;
        }

        #endregion
    }
}
