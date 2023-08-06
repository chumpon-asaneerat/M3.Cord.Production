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
    /// Interaction logic for S1ConditionWindow.xaml
    /// </summary>
    public partial class S1ConditionWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S1ConditionWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC _mc = null;
        //private RawMaterialSheet _rawMat = null;

        private MCBBCondition _cond = null;
        private List<MCBBConditionItem> _condItems = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        #endregion

        #region Private Methods

        private void InitMCBB()
        {
            this.DataContext = null;

            _cond = new MCBBCondition();
            _cond.RecordDate = DateTime.Now;
            _cond.MCCode = (null != _mc) ? _mc.MCCode : null;
            //_cond.ProductLotNo = (null != _rawMat) ? _rawMat.ProductLotNo : null;
            //_cond.ItemCode = (null != _rawMat) ? _rawMat.ItemCode : null;

            this.DataContext = _cond;

            grid.ItemsSource = null;

            if (null == _mc) return;

            _condItems = new List<MCBBConditionItem>();

            for (int i =_mc.StartCore; i < _mc.EndCore; i++)
            {
                var item = new MCBBConditionItem();
                item.BBNo = i;
                _condItems.Add(item);
            }

            grid.ItemsSource = _condItems;
        }


        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc/*, RawMaterialSheet rawMat*/)
        {
            _mc = mc;
            //_rawMat = rawMat;
            InitMCBB();
        }

        #endregion
    }
}
