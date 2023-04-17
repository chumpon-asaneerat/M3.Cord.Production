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

using NLib.Wpf.Controls;

namespace NLib.Wpf.Test.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        private void NavigatorBar_NavigatorButtonClick(object sender, Controls.NavigatorButtonEventArgs e)
        {
            switch (e.Icon)
            {
                case FontAwesomeIcon.Home:
                    MessageBox.Show("Home click.");
                    break;
                default:
                    MessageBox.Show("Unhandle click.");
                    break;
            }
        }
    }
}
