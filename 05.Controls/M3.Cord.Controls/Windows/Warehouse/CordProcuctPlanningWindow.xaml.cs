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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for CordProcuctPlanningWindow.xaml
    /// </summary>
    public partial class CordProcuctPlanningWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordProcuctPlanningWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordProduct _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null != cbItemYanrs.SelectedItem && cbItemYanrs.SelectedItem is CordItemYarn)
            {
                var itemYarn = cbItemYanrs.SelectedItem as CordItemYarn;
                if (null != _item)
                {
                    _item.ItemYarn = itemYarn.ItemYarn;
                }
            }

            DialogResult = true;
        }

        #endregion

        #region Privete Methods

        private void LoadComboBoxes()
        {
            cbItemYanrs.ItemsSource = null;

            var itemYarns = CordItemYarn.Gets().Value();
            cbItemYanrs.ItemsSource = itemYarns;
        }

        #endregion

        #region Public Methods

        public void Setup(CordProduct item)
        {
            LoadComboBoxes();

            var itemYarns = cbItemYanrs.ItemsSource as List<CordItemYarn>;

            _item = item;
            this.DataContext = _item;

            if (null != _item)
            {
                _item.CalcTotals();

                int idx = -1;
                if (null != itemYarns)
                {
                    idx = itemYarns.FindIndex(yarn => { return yarn.ItemYarn == item.ItemYarn; });
                }
                this.InvokeAction(() => 
                {
                    cbItemYanrs.SelectedIndex = idx;
                });
            }
        }

        #endregion
    }
}
