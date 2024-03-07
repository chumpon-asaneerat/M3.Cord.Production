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
            // Solution
            if (null != cbSolutions.SelectedItem && cbSolutions.SelectedItem is SolutionLotLabel)
            {
                var recipe = cbSolutions.SelectedItem as SolutionLotLabel;
                if (null == recipe)
                {
                    var msgbox = M3CordApp.Windows.MessageBox;
                    msgbox.Setup("กรุณาเลือก Solution");
                    msgbox.ShowDialog();
                    return;
                }
                if (null != _item && null != recipe)
                {
                    _item.SolutionName = recipe.SolutionName;
                }
            }
            DialogResult = true;
        }

        #endregion

        #region TextBox Handler

        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                e.Handled = true;
                LoadComboBoxes();
            }
        }

        private void txtLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            //LoadComboBoxes();
        }

        #endregion

        #region Privete Methods

        private void LoadComboBoxes()
        {
            // Solution
            cbSolutions.ItemsSource = null;

            if (null != _item)
            {
                var solutions = SolutionLotLabel.Gets(_item.DIPLotNo).Value();
                if (null == solutions || solutions.Count <= 0)
                {
                    // load all if not match Lot No.
                    solutions = SolutionLotLabel.Gets().Value();
                }
                cbSolutions.ItemsSource = solutions;

                // Recipes
                int idx2 = -1;
                if (null != solutions && solutions.Count > 0)
                {
                    idx2 = solutions.FindIndex(recipe => { return recipe.SolutionName == _item.SolutionName; });
                    if (idx2 == -1)
                    {
                        idx2 = 0; // Not match so auto pick first one
                    }
                }
                this.InvokeAction(() =>
                {
                    cbSolutions.SelectedIndex = idx2;
                    if (idx2 > -1)
                    {
                        var solution = solutions[idx2];
                    }

                    cbSolutions.FocusControl();
                });
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPChemicalReqisition item)
        {
            cbSolutions.ItemsSource = null;
            _item = item;
            this.DataContext = _item;

            this.InvokeAction(() =>
            {
                txtLotNo.FocusControl();
            });
        }

        #endregion
    }
}
