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
using System.Windows.Interop;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for G4ReceiveYarnWindow.xaml
    /// </summary>
    public partial class G4ReceiveYarnWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public G4ReceiveYarnWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private G4Yarn _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null != _item)
            {
                if (!_item.ExpiredDate.HasValue)
                {
                    string msg = "Please Enter Expire date.";
                    var win = M3CordApp.Windows.MessageBox;
                    win.Owner = this; // change owner.
                    win.Setup(msg);
                    win.ShowDialog();
                    return;
                }

                if (string.IsNullOrWhiteSpace(_item.DirectionNo))
                {
                    string msg = "Please Enter Directions No.";
                    var win = M3CordApp.Windows.MessageBox;
                    win.Owner = this; // change owner.
                    win.Setup(msg);
                    win.ShowDialog();
                    return;
                }
            }
            DialogResult = true;
        }

        #endregion

        #region Public Methods

        public void Setup(G4Yarn item)
        {
            _item = item;
            this.DataContext = _item;
        }

        #endregion
    }
}
