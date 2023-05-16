#region Using

using M3.Cord.Models;
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
    /// Interaction logic for CordProcuctPlanningWindow.xaml
    /// </summary>
    public partial class CordProcuctPlanningWindow : Window
    {
        #region  Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CordProcuctPlanningWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private CordProduct _item = null;

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

        #region Private Methods

        private void LoadComboBoxs()
        {
            cbItemYarns.ItemsSource = Cord.LobaclDb.ItemYarns;
            cbItemYarns.DataContext = _item;
        }

        #endregion

        #region Public Methods

        public void Setup(CordProduct item)
        {
            LoadComboBoxs();

            _item = item;
            this.DataContext = _item;
        }

        #endregion
    }
}
