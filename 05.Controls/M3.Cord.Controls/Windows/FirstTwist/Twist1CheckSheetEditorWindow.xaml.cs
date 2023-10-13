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
using NLib.Data.Design;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for Twist1CheckSheetEditorWindow.xaml
    /// </summary>
    public partial class Twist1CheckSheetEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Twist1CheckSheetEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC _mc;
        private PCTwist1 _pcCard;
        private Twist1CheckSheet _sheet = null;
        private List<Twist1CheckSheetItem> _items = null;

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            Save();
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Private Methods

        private void Save()
        {

        }

        private void NewItem()
        {

        }

        private void EditItem()
        {

        }

        private void RefreshGrid()
        {

        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc, PCTwist1 pcCard, Twist1CheckSheet sheet = null)
        {
            _mc = mc;
            _pcCard = pcCard;
            _sheet = sheet;

            if (sheet == null)
            {
                NewItem();
            }
            else
            {
                EditItem();
            }

            RefreshGrid();
        }

        #endregion
    }
}
