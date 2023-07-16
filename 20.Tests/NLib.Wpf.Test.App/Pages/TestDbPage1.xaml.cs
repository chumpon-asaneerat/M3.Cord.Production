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

using M3.Cord;

namespace NLib.Wpf.Test.App.Pages
{
    /// <summary>
    /// Interaction logic for TestDbPage1.xaml
    /// </summary>
    public partial class TestDbPage1 : UserControl
    {
        #region Constructor

        public TestDbPage1()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DbServer.Instance.Start();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            DbServer.Instance.Shutdown();
        }

        #endregion
    }
}
