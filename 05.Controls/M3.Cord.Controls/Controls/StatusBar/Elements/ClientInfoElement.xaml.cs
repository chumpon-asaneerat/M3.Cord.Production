﻿#region Using

using NLib.Utils;
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
using System.Windows.Threading;

#endregion

namespace M3.Cord.Controls.Elements
{
    /// <summary>
    /// Interaction logic for ClientInfoElement.xaml
    /// </summary>
    public partial class ClientInfoElement : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClientInfoElement()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        #endregion

        #region Private Methods

        private void UpdateUI()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                var ipaddr = NetworkUtils.GetLocalIPAddress();
                txtStatus.Text = (null != ipaddr) ? ipaddr.ToString() : "0.0.0.0";
            }));
        }

        #endregion
    }
}
