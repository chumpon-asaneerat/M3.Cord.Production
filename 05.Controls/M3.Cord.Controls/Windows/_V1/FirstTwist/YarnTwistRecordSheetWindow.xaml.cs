#region Using

using M3.Cord.Models;
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

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for YarnTwistRecordSheetWindow.xaml
    /// </summary>
    public partial class YarnTwistRecordSheetWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public YarnTwistRecordSheetWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC _mc;

        #endregion

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Private Methods

        private void RefreshGrid()
        {
            grid.ItemsSource = null;

            if (null != _mc) 
            {
                var items = new List<YarnTwistConditionRecordItem>();
                int BBCnt = _mc.BBCount;
                for (int i = 0; i < BBCnt; i++) 
                {
                    var inst = new YarnTwistConditionRecordItem();
                    inst.BBNo = i + 1;
                    items.Add(inst);
                }
                grid.ItemsSource = items;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            _mc = mc;
            RefreshGrid();
        }

        #endregion
    }
}
