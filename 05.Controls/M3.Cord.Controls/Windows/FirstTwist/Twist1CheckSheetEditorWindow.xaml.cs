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

        #region CheckBox

        private void chkTest_Checked(object sender, RoutedEventArgs e)
        {
            RefreshDoffNo();
        }

        private void chkTest_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshDoffNo();
        }

        #endregion

        #region Private Methods

        private void RefreshDoffNo()
        {
            if (null == _pcCard || null == _sheet) return;
            if (chkTest.IsChecked == true)
            {
                _sheet.TestFlag = true;
                _sheet.DoffNo = _pcCard.LastTestNo;
            }
            else
            {
                _sheet.TestFlag = false;
                _sheet.DoffNo = _pcCard.LastDoffNo;
            }
        }

        private void Save()
        {
            if (null != _sheet)
            {
                _sheet = Twist1CheckSheet.Save(_sheet).Value(); // Save check sheet
                if (null == _sheet || _sheet.Twist1CheckId <= 0) return;
                SaveCheckSheets();

                M3CordApp.Windows.SaveSuccess();
            }
        }

        private void SaveCheckSheets()
        {
            if (null != _sheet && null != _items)
            {
                _items.ForEach(item => 
                {
                    item.Twist1CheckId = _sheet.Twist1CheckId; // update key
                    Twist1CheckSheetItem.Save(item); 
                });
            }
            RefreshGrid();
        }

        private void NewItem()
        {
            this.DataContext = null;
            if (null != _pcCard)
            {
                _sheet = new Twist1CheckSheet();
                _sheet.ConditionDate = DateTime.Now;
                _sheet.PCTwist1Id = (_pcCard.PCTwist1Id.HasValue) ? _pcCard.PCTwist1Id.Value : 0;
                _sheet.ItemYarn = _pcCard.ItemYarn;
                _sheet.ProductLotNo = _pcCard.ProductLotNo;
                _sheet.TestFlag = false;
                _sheet.DoffNo = _pcCard.LastDoffNo;
                _sheet.ShiftName = string.Empty;
                _sheet.UserName = (!string.IsNullOrEmpty(M3CordApp.Current.User.FullName)) ? 
                    M3CordApp.Current.User.FullName : M3CordApp.Current.User.UserName;

                this.DataContext = _sheet;
            }
        }

        private void EditItem()
        {
            this.DataContext = null;
            if (null != _sheet)
            {
                _sheet.LoadItems();
            }
            this.DataContext = _sheet;
        }

        private void RefreshGrid()
        {
            if (null != _pcCard && null != _mc)
            {
                _items = new List<Twist1CheckSheetItem>();
                for (int i = _mc.StartCore; i <= _mc.EndCore; i++)
                {
                    _items.Add(new Twist1CheckSheetItem() { Twist1CheckId = 0, SPNo = i });
                }

                if (null != _sheet)
                {
                    var existItems = Twist1CheckSheetItem.Gets(_sheet.Twist1CheckId).Value();
                    if (null != existItems && existItems.Count > 0)
                    {
                        foreach (var existItem in existItems)
                        {
                            int idx = _items.FindIndex((item =>
                            {
                                return (existItem.SPNo == item.SPNo);
                            }));
                            if (idx != -1 && null != _items[idx])
                            {
                                _items[idx].Twist1CheckId = existItem.Twist1CheckId;
                                //_items[idx].SPNo = existItem.SPNo;

                                _items[idx].BBMarkB = existItem.BBMarkB;
                                _items[idx].BBMarkE = existItem.BBMarkE;

                                _items[idx].CrossB = existItem.CrossB;
                                _items[idx].CrossE = existItem.CrossE;

                                _items[idx].RawB = existItem.RawB;
                                _items[idx].RawE = existItem.RawE;

                                _items[idx].FormB = existItem.FormB;
                                _items[idx].FormE = existItem.FormE;

                                _items[idx].KebaB = existItem.KebaB;
                                _items[idx].KebaE = existItem.KebaE;

                                _items[idx].PaperTubeB = existItem.PaperTubeB;
                                _items[idx].PaperTubeE = existItem.PaperTubeE;

                                _items[idx].StainB = existItem.StainB;
                                _items[idx].StainE = existItem.StainE;

                                _items[idx].YarnNoB = existItem.YarnNoB;
                                _items[idx].YarnNoE = existItem.YarnNoE;
                            }
                        }
                    }
                }

                grid.ItemsSource = _items;
            }
        }

        private void CheckEnableSave()
        {
            bool canSave = false;
            if (null != _pcCard)
            {
                if (Mode == DisplayMode.New)
                {
                    canSave = true;
                }
                else if (Mode == DisplayMode.Edit)
                {
                    canSave = true;
                    /*
                    var op = PCTwist1Operation.GetLast(_pcCard.PCTwist1Id.Value).Value();
                    if (null != op)
                    {
                        // has item but not start
                        canSave = !op.StartTime.HasValue;
                    }
                    */
                }
                else
                {
                    canSave = false;
                }
            }
            cmdOk.IsEnabled = canSave;
        }

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc, PCTwist1 pcCard, Twist1CheckSheet sheet = null)
        {
            _mc = mc;
            _pcCard = pcCard;
            _sheet = sheet;

            CheckEnableSave();

            if (_sheet == null)
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

        #region Public Proeprties

        public DisplayMode Mode { get; set; } = DisplayMode.New;

        #endregion
    }
}
