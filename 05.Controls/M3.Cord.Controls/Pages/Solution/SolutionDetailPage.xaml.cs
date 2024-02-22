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
using System.Reflection;

using NLib.Services;
using M3.Cord.Models;
using NLib.Models;
using NLib;
using M3.Cord.Services.Excels;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for SolutionDetailPage.xaml
    /// </summary>
    public partial class SolutionDetailPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SolutionDetailPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<SolutionLotLabel> chemicals = null;
        private List<SolutionLotDetail> lotDetail = new List<SolutionLotDetail>();

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdHome_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoSolutionMenu();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
            {
                LoadSolutionLotLabel(txtSolutionLotNo.Text);
            }
        }

        private void cmdSaveChemicalLot_Click(object sender, RoutedEventArgs e)
        {
            if (lotDetail.Count > 0)
                Save();
        }

        private void cmdWeight_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            var item = btn.DataContext as SolutionLotDetail;
            if (null == item) return;

            var win = M3CordApp.Windows.SolutionWeight;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                if (win.Item != null)
                {
                    if(win.DialogResult == true)
                    {
                        if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
                        {
                            lotDetail = new List<SolutionLotDetail>();
                            lotDetail = SolutionLotDetail.Gets(txtSolutionLotNo.Text).Value();
                            grid.ItemsSource = lotDetail;
                        }
                    }
                    
                    // do something....
                }
            }
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        #endregion

        #region TextBox Handlers

        private void txtSolutionLotNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrEmpty(txtSolutionLotNo.Text))
                {
                    LoadSolutionLotLabel(txtSolutionLotNo.Text);
                }
                e.Handled = true;
            }
        }

        #endregion

        #region Combobox Handlers

        private void cbChemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var chemicals = cbChemicals.SelectedItem as SolutionLotLabel;
                if (chemicals != null)
                {
                    string solutionlot = chemicals.SolutionLot;
                    int solutionid = chemicals.SolutionId;
                    string recipe = null;

                    txtProductCode.Text = chemicals.ProductCode;
                    txtCompound.Text = chemicals.Compound;
                    dtMixDate.SelectedDate = chemicals.MixDate;

                    if (chemicals.DipSolutionQty != null)
                        txtQty.Text = chemicals.DipSolutionQty.Value.ToString("#,##0.##");

                    lotDetail = new List<SolutionLotDetail>();
                    lotDetail = SolutionLotDetail.Gets(solutionlot, solutionid, recipe).Value();
                    grid.ItemsSource = lotDetail;
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region ListView Handlers

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == grid.ItemsSource)
                return;
        }

        #endregion

        #region Private Methods

        private void ClearInputs()
        {
            grid.ItemsSource = null;
            dtMixDate.SelectedDate = null;
            txtSolutionLotNo.Text = string.Empty;
            txtCompound.Text = string.Empty;
            txtQty.Text = string.Empty;

            txtProductCode.Text = string.Empty;
            cbChemicals.SelectedIndex = -1;

            lotDetail = new List<SolutionLotDetail>();

            Dispatcher.BeginInvoke(new Action(() => { txtSolutionLotNo.Focus(); }));
            txtSolutionLotNo.SelectAll();
            txtSolutionLotNo.Focus();

            this.InvokeAction(() =>
            {
                DisableInputs();
            });
        }

        private void DisableInputs()
        {
            txtCompound.IsReadOnly = true;
            dtMixDate.IsEnabled = false;
            txtQty.IsReadOnly = true;

            txtProductCode.IsEnabled = false;
            //cbChemicals.IsEnabled = true;
        }

        private void LoadSolutionLotLabel(string solutionlot)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {
                var ret = SolutionLotLabel.Gets(solutionlot).Value();

                if (ret != null && ret.Count > 0)
                {
                    //txtProductCode.Text = ret[0].ProductCode;
                    //txtCompound.Text = ret[0].Compound;
                    //dtMixDate.SelectedDate = ret[0].MixDate;

                    //if (ret[0].DipSolutionQty != null)
                    //    txtQty.Text = ret[0].DipSolutionQty.Value.ToString("#,##0.##");

                    if (ret[0].SolutionName != null || ret.Count > 1)
                    {
                        chemicals = ret;
                        cbChemicals.ItemsSource = chemicals;
                        cbChemicals.SelectedItem = ret[0].SolutionName;
                    }

                    //int? solutionid = null;
                    //string recipe = string.Empty;
                    //solutionid = ret[0].SolutionId;

                    //grid.ItemsSource = SolutionLotDetail.Gets(solutionlot, solutionid, recipe).Value();
                }
                else
                {
                    var win = M3CordApp.Windows.MessageBox;
                    win.Setup("ไม่พบ Lot No ที่ระบุ ในระบบ");
                    win.ShowDialog();

                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void Save()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {

                if (lotDetail.Count > 0)
                {
                    bool? chkErr = false;
                    string strErr = string.Empty;

                    SaveSolutionLotDetail d = new SaveSolutionLotDetail();

                    foreach (var itemD in lotDetail)
                    {
                        if (!string.IsNullOrEmpty(itemD.ChemicalLot))
                        {
                            d = new SaveSolutionLotDetail();

                            d.solutionlot = itemD.SolutionLot;
                            d.solutionid = itemD.SolutionID;
                            d.recipe = itemD.Recipe;
                            d.chemicalno = itemD.ChemicalNo;
                            d.chemicallot = itemD.ChemicalLot;

                            //d.recipeorder = itemD.RecipeOrder;
                            //d.mixorder = itemD.MixOrder;
                            //d.weightcal = itemD.WeightCal;
                            //d.weightactual = itemD.WeightActual;
                            //d.weightmc = itemD.WeightMc;
                            //d.weightdate = itemD.WeightDate;
                            //d.weightby = itemD.WeightBy;
                            //d.tweight = itemD.TWeight;
                            //d.gweight = itemD.GWeight;


                            var retD = SaveSolutionLotDetail.Save(d);

                            if (retD.HasError)
                            {
                                strErr = retD.ErrMsg;
                                chkErr = true;
                                //break;
                            }
                        }
                    }

                    if (chkErr == false)
                    {
                        this.InvokeAction(() =>
                        {
                            var win = M3CordApp.Windows.MessageBox;
                            win.Setup("Save Complete");
                            win.ShowDialog();
                            //ClearInputs();
                        });
                    }
                    else
                    {
                        this.InvokeAction(() =>
                        {
                            var win = M3CordApp.Windows.MessageBox;
                            win.Setup(strErr);
                            win.ShowDialog();
                            //ClearInputs();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private bool SaveBeforeExport()
        {
            bool ret = false;
            MethodBase med = MethodBase.GetCurrentMethod();

            try
            {

                if (lotDetail.Count > 0)
                {
                    bool? chkErr = false;
                    string strErr = string.Empty;

                    SaveSolutionLotDetail d = new SaveSolutionLotDetail();

                    foreach (var itemD in lotDetail)
                    {
                        if (!string.IsNullOrEmpty(itemD.ChemicalLot))
                        {
                            d = new SaveSolutionLotDetail();

                            d.solutionlot = itemD.SolutionLot;
                            d.solutionid = itemD.SolutionID;
                            d.recipe = itemD.Recipe;
                            d.chemicalno = itemD.ChemicalNo;
                            d.chemicallot = itemD.ChemicalLot;

                            //d.recipeorder = itemD.RecipeOrder;
                            //d.mixorder = itemD.MixOrder;
                            //d.weightcal = itemD.WeightCal;
                            //d.weightactual = itemD.WeightActual;
                            //d.weightmc = itemD.WeightMc;
                            //d.weightdate = itemD.WeightDate;
                            //d.weightby = itemD.WeightBy;
                            //d.tweight = itemD.TWeight;
                            //d.gweight = itemD.GWeight;


                            var retD = SaveSolutionLotDetail.Save(d);

                            if (retD.HasError)
                            {
                                strErr = retD.ErrMsg;
                                chkErr = true;
                                //break;
                            }
                        }
                    }

                    if (chkErr == false)
                    {
                        ret = true;
                    }
                    else
                    {
                        this.InvokeAction(() =>
                        {
                            var win = M3CordApp.Windows.MessageBox;
                            win.Setup(strErr);
                            win.ShowDialog();
                            //ClearInputs();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return ret;
        }

        private void Export()
        {
            if (string.IsNullOrEmpty(txtSolutionLotNo.Text))
            {
                return;
            }

            if (lotDetail.Count > 0)
            {
                // auto save current data without notify success case
                if (SaveBeforeExport()) 
                {
                    SolutionExports.Export(txtSolutionLotNo.Text);
                }
            }
        }

        #endregion

        #region Public Methods

        public void Setup()
        {
            ClearInputs();
        }

        #endregion
    }
}
