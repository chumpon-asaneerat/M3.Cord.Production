﻿using M3.Cord.Windows;
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

namespace M3.Cord.Controls.Documents
{
    /// <summary>
    /// Interaction logic for S8WetPickupEntryPage.xaml
    /// </summary>
    public partial class S8WetPickupEntryPage : UserControl
    {
        public S8WetPickupEntryPage()
        {
            InitializeComponent();
        }

        private void cmdAdd1_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void cmdAdd2_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Add()
        {
            S8WetPickUpItemEditWindow win = new S8WetPickUpItemEditWindow();
            win.ShowDialog();
        }
    }
}
