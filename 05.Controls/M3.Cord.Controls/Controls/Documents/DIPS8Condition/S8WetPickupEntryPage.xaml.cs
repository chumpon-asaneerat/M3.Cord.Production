#region Using

using M3.Cord.Models;
using M3.Cord.Windows;
using NLib.Models;
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

#endregion

namespace M3.Cord.Controls.Documents
{
    /// <summary>
    /// Interaction logic for S8WetPickupEntryPage.xaml
    /// </summary>
    public partial class S8WetPickupEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public S8WetPickupEntryPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPPCCard pcCard;

        #endregion

        #region Button Handlers

        private void cmdAdd1_Click(object sender, RoutedEventArgs e)
        {
            Add(1);
        }

        private void cmdAdd2_Click(object sender, RoutedEventArgs e)
        {
            Add(2);
        }

        #endregion

        #region Private Methods

        private void Add(int twistNo)
        {
            if (null == pcCard)
                return;

            var item = new S8WetPickUpItem();
            item.ProductCode = pcCard.ProductCode;
            item.LotNo = pcCard.DIPLotNo;
            item.DoffingDate = DateTime.Now;
            item.DoffNo = pcCard.DoffNo;

            var win = M3CordApp.Windows.S8WetPickUpItemEdit;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                item.TwistNo = twistNo;
                item.RowType = 1;
                item.Operator = (null != M3CordApp.Current.User) ?
                            M3CordApp.Current.User.FullName : null;
                item.UpdateDate = DateTime.Now;

                var ret = S8WetPickUpItem.Save(item);

                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();

                RefreshGrids();
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DIPPCCard pc)
        {
            pcCard = pc;
            RefreshGrids();
        }

        public void RefreshGrids()
        {
            grid.ItemsSource = null;
            if (null != pcCard)
            {
                grid.ItemsSource = S8WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, 1).Value();
            }

            grid2.ItemsSource = null;
            if (null != pcCard)
            {
                grid2.ItemsSource = S8WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, 2).Value();
            }
        }

        #endregion
    }
}
