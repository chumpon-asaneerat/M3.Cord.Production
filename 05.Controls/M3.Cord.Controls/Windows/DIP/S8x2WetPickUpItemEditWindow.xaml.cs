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
    /// Interaction logic for S8x2WetPickUpItemEditWindow.xaml
    /// </summary>
    public partial class S8x2WetPickUpItemEditWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S8x2WetPickUpItemEditWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private S8x2WetPickUpItem _item = null;

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

        #endregion

        #region Public Methods

        public void Setup(S8x2WetPickUpItem item)
        {
            _item = item;
            this.DataContext = _item;
            if (null != _item)
            {
                txtDateTime.Text = (_item.DoffingDate.HasValue) ? 
                    _item.DoffingDate.Value.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo) : "";
                txtLotNo.Text = _item.LotNo;
                txtDoffNo.Text = _item.DoffNo.ToString();
            }
            else
            {
                txtDateTime.Text = string.Empty;
                txtLotNo.Text = string.Empty;
                txtDoffNo.Text = string.Empty;
            }
        }

        #endregion
    }
}
