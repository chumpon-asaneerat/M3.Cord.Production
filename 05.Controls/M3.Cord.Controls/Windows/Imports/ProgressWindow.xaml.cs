#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProgressWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void UpdateInfo()
        {
            txtInfo.Text = string.Format("Import Record No: {0:n0} of {1:n0}", progress.Value, progress.Maximum);
        }

        #endregion

        #region Public Methods

        public void Setup(int max, string title = "Importing")
        {
            txtTitle.Text = title;

            progress.Minimum = 0;
            progress.Maximum = max;
            progress.Value = 0;

            UpdateInfo();
        }

        public void Increment()
        {
            Dispatcher.Invoke(() =>
            {
                progress.Value++;
                UpdateInfo();
            }, DispatcherPriority.Background);
        }

        #endregion
    }
}
