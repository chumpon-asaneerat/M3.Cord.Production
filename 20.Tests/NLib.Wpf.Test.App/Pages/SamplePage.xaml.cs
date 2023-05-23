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

#endregion

namespace NLib.Wpf.Test.App.Pages
{
    /// <summary>
    /// Interaction logic for SamplePage.xaml
    /// </summary>
    public partial class SamplePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SamplePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add");
        }

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Back");
        }

        #endregion
    }
}
