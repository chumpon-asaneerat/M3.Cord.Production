﻿#region Using

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
    /// Interaction logic for Twist1S1ConditionWindow.xaml
    /// </summary>
    public partial class Twist1S1ConditionWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Twist1S1ConditionWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
