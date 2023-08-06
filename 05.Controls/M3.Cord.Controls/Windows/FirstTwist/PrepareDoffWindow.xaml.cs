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
        private YarnLoadSheetDoff _item = null;

        #endregion

        #region TextBox Handlers

        private void txtPallet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

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
            if (null != _item && null != Items)
            {
                Items.Add(_item);
            }
            this.InvokeAction(() =>
            {
                RefreshGrid();
                NewItem();
            });
        }

        #endregion

        #region Private Methods

        private void NewItem()
        {
            this.DataContext = null; 

            if (null != _sheet)
            {
                _item = new YarnLoadSheetDoff();
                _item.RecordDate = DateTime.Now;
                _item.YarnLoadSheetId = _sheet.YarnLoadSheetId;
                _item.ProductLotNo = _sheet.ProductLotNo;
                _item.ItemYarn = _sheet.ItemYarn;
                _item.DoffNos = string.Empty;
                _item.Shift = string.Empty;
                this.DataContext = _item;
            }
        }

        private void RefreshGrid()
        {
            grid.ItemsSource = null;
            grid.ItemsSource = Items;
        }

        #endregion

        #region Public Methods

        public void Setup(YarnLoadSheet sheet)
        {
            _sheet = sheet;
            
            this.Items = new List<YarnLoadSheetDoff>();
            NewItem();
            RefreshGrid();
        }

        #endregion

        #region Public Properties

        public List<YarnLoadSheetDoff> Items { get; private set; }

        #endregion
    }
}
