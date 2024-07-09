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
using NLib.Data.Design;
using System.Diagnostics;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for Twist1LoadRecordViewWindow.xaml
    /// </summary>
    public partial class Twist1LoadRecordViewWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Twist1LoadRecordViewWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private PCTwist1 _pcCard;
        private Twist1LoadRecord _item = null;

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        #endregion

        #region Private Methods

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            if (null != _item)
            {
                grid.ItemsSource = _item.Items;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(PCTwist1 pcCard, Twist1LoadRecord record, string palletNo, string traceNo)
        {
            this.DataContext = null;
            _pcCard = pcCard;

            _item = record;
            if (null != _item)
            {
                _item.LoadItems(palletNo, traceNo);
            }

            this.DataContext = _item;
            RefreshGrid();
        }

        #endregion

        #region Public Properties

        public Twist1LoadRecord Record { get { return _item; } }

        #endregion
    }
}
