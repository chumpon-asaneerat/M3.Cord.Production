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
    /// Interaction logic for Twist1CheckSheetEditorViewWindow.xaml
    /// </summary>
    public partial class Twist1CheckSheetEditorViewWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Twist1CheckSheetEditorViewWindow()
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
            DialogResult = true;
        }

        #endregion

        #region Private Methods

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

        #endregion

        #region Public Methods

        public void Setup(PCTwist1 pcCard, Twist1CheckSheet sheet = null)
        {
            _pcCard = pcCard;
            _mc = (null != _pcCard) ? FirstTwistMC.Get(pcCard.MCCode).Value() : null;
            _sheet = sheet;

            EditItem();
            RefreshGrid();
        }

        #endregion
    }
}
