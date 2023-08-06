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
    /// Interaction logic for PrepareDoffWindow.xaml
    /// </summary>
    public partial class PrepareDoffWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PrepareDoffWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private YarnLoadSheet _sheet = null;
        //private RawMaterialSheetItem _item = null;

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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (null != _item && null != Items)
            {
                Items.Add(_item);
            }
            this.InvokeAction(() =>
            {
                RefreshGrid();
                NewItem();
            });
            */
        }

        #endregion

        #region Private Methods

        private void NewItem()
        {
            this.DataContext = null; 

            /*
            if (null != _sheet)
            {
                _item = new RawMaterialSheetItem();
                _item.ProductionDate = DateTime.Now;
                _item.RawMaterialSheetId = _sheet.RawMaterialSheetId;
                _item.Seq = 0;
                _item.ProductLotNo = _sheet.ProductLotNo;
                _item.ItemYarn = _sheet.ItemYarn;
                _item.PalletNo = string.Empty;
                _item.TraceNo = string.Empty;
                _item.DoffNos = string.Empty;
                _item.InputCH = 0;
                _item.SPNos = string.Empty;

                this.DataContext = _item;
            }
            */
        }

        private void RefreshGrid()
        {
            /*
            grid.ItemsSource = null;
            grid.ItemsSource = Items;
            */
        }

        #endregion

        #region Public Methods

        public void Setup(YarnLoadSheet sheet)
        {
            _sheet = sheet;
            
            //this.Items = new List<RawMaterialSheetItem>();
            NewItem();
            RefreshGrid();
        }

        #endregion

        #region Public Properties

        //public List<RawMaterialSheetItem> Items { get; private set; }

        #endregion
    }
}
