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
using System.Reflection;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for SolutionWeightWindow.xaml
    /// </summary>
    public partial class SolutionWeightWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SolutionWeightWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                SaveSolutionLotDetail d = new SaveSolutionLotDetail();

                d.solutionlot = Item.SolutionLot;
                d.recipe = Item.Recipe;
                d.chemicalno = Item.ChemicalNo;

                d.solutionid = Item.SolutionID;
                d.recipeorder = Item.RecipeOrder;
                d.mixorder = Item.MixOrder;

                d.weightcal = Item.WeightCal;

                if (!string.IsNullOrEmpty(txtWeightActual.Text))
                {
                    d.weightactual = decimal.Parse(txtWeightActual.Text);
                }

                if (!string.IsNullOrEmpty(txtWeightMc.Text))
                {
                    d.weightmc = txtWeightMc.Text;
                }
                else
                {
                    d.weightmc = Item.WeightMc;
                }

                d.weightdate = DateTime.Now;
                d.weightby = M3CordApp.Current.User.UserId;

                var retD = SaveSolutionLotDetail.Save(d);

                if (!retD.HasError)
                {
                    DialogResult = true;
                }
                else
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup(retD.ErrMsg);
                    win.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
           
        }

        #endregion

        #region Privete Methods

        #endregion

        #region Public Methods

        public void Setup(SolutionLotDetail item)
        {
            Item = item;
            this.DataContext = Item;
        }

        #endregion

        #region Public Properties

        public SolutionLotDetail Item { get; set; }

        #endregion
    }
}
