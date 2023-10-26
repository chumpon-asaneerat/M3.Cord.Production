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

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for ChemicalRequisitionEditorWindow.xaml
    /// </summary>
    public partial class ChemicalRequisitionEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChemicalRequisitionEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPChemicalReqisition _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Customer
            if (null != cbSolutions.SelectedItem && cbSolutions.SelectedItem is SolutionRecipe)
            {
                var customer = cbSolutions.SelectedItem as SolutionRecipe;
                if (null != _item)
                {
                    _item.SolutionName = customer.SolutionName;
                }
            }

            DialogResult = true;
        }

        #endregion

        #region Privete Methods

        private void LoadComboBoxes()
        {
            // Solution
            cbSolutions.ItemsSource = null;
            var solutions = SolutionRecipe.Gets().Value();
            cbSolutions.ItemsSource = solutions;
        }

        #endregion

        #region Public Methods

        public void Setup(DIPChemicalReqisition item)
        {
            LoadComboBoxes();

            var solutions = cbSolutions.ItemsSource as List<SolutionRecipe>;

            _item = item;
            this.DataContext = _item;

            if (null != _item)
            {
                // Customer
                int idx2 = -1;
                if (null != solutions)
                {
                    idx2 = solutions.FindIndex(customer => { return customer.SolutionName == item.SolutionName; });
                }
                this.InvokeAction(() =>
                {
                    cbSolutions.SelectedIndex = idx2;
                    if (idx2 > -1)
                    {
                        var solution = solutions[idx2];
                    }
                });
            }
        }

        #endregion
    }
}
