#region Using

using M3.Cord.Models;
using M3.Cord.Windows;
using NLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLib;

#endregion

namespace M3.Cord.Controls.Documents
{
    /// <summary>
    /// Interaction logic for S8x2WetPickupEntryPage.xaml
    /// </summary>
    public partial class S8x2WetPickupEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public S8x2WetPickupEntryPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPPCCard pcCard;

        #endregion

        #region Button Handlers

        private void cmdEdit1_Click(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button);
            var ctx = (null != btn) ? btn.DataContext : null;
            var item = (null != ctx) ? ctx as S8x2WetPickUpItem : null;
            Edit(item);
        }

        private void cmdEdit2_Click(object sender, RoutedEventArgs e)
        {
            var btn = (sender as Button);
            var ctx = (null != btn) ? btn.DataContext : null;
            var item = (null != ctx) ? ctx as S8x2WetPickUpItem : null;
            Edit(item);
        }

        #endregion

        #region Private Methods

        private void Edit(S8x2WetPickUpItem item)
        {
            if (null == pcCard || null == item)
                return;

            var win = M3CordApp.Windows.S8x2WetPickUpItemEdit;
            win.Setup(item);
            if (win.ShowDialog() == true)
            {
                item.Operator = (null != M3CordApp.Current.User) ?
                            M3CordApp.Current.User.FullName : null;
                item.UpdateDate = DateTime.Now;

                var ret = S8x2WetPickUpItem.Save(item);

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
            MethodBase med = MethodBase.GetCurrentMethod();

            grid.ItemsSource = null;
            if (null != pcCard)
            {
                var items = S8x2WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, pcCard.DoffNo, 1).Value();

                if (null == items || items.Count <= 0)
                {
                    // Auto add if save lot but new doff
                    var last = S8x2WetPickUpItem.GetLast(pcCard.ProductCode, pcCard.DIPLotNo, 1).Value();
                    var inst = new S8x2WetPickUpItem();
                    inst.ProductCode = pcCard.ProductCode;
                    inst.LotNo = pcCard.DIPLotNo;
                    inst.DoffingDate = DateTime.Now;
                    inst.DoffNo = pcCard.DoffNo;
                    inst.FirstAmt = (null != last && last.RestAmt.HasValue && last.RestAmt.Value > decimal.Zero) ?
                        last.RestAmt.Value : new decimal?();
                    inst.TwistNo = 1;
                    inst.RowType = 1;
                    inst.Operator = (null != M3CordApp.Current.User) ?
                                M3CordApp.Current.User.FullName : null;
                    inst.UpdateDate = DateTime.Now;
                    // save
                    var ret = S8x2WetPickUpItem.Save(inst);
                    if (null == ret || !ret.Ok)
                    {
                        string msg = (null == ret) ? "Save failed unknown error!" : "Save failed " + ret.ErrMsg;
                        med.Err(msg);
                    }
                    // reload.
                    items = S8x2WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, pcCard.DoffNo, 1).Value();
                }

                grid.ItemsSource = items;
            }

            grid2.ItemsSource = null;
            if (null != pcCard)
            {
                var items = S8x2WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, pcCard.DoffNo, 2).Value();

                if (null == items || items.Count <= 0)
                {
                    // Auto add if save lot but new doff
                    var last = S8x2WetPickUpItem.GetLast(pcCard.ProductCode, pcCard.DIPLotNo, 2).Value();
                    var inst = new S8x2WetPickUpItem();
                    inst.ProductCode = pcCard.ProductCode;
                    inst.LotNo = pcCard.DIPLotNo;
                    inst.DoffingDate = DateTime.Now;
                    inst.DoffNo = pcCard.DoffNo;
                    inst.FirstAmt = (null != last && last.RestAmt.HasValue && last.RestAmt.Value > decimal.Zero) ?
                        last.RestAmt.Value : new decimal?();
                    inst.TwistNo = 2;
                    inst.RowType = 1;
                    inst.Operator = (null != M3CordApp.Current.User) ?
                                M3CordApp.Current.User.FullName : null;
                    inst.UpdateDate = DateTime.Now;
                    // save
                    var ret = S8x2WetPickUpItem.Save(inst);
                    if (null == ret || !ret.Ok)
                    {
                        string msg = (null == ret) ? "Save failed unknown error!" : "Save failed " + ret.ErrMsg;
                        med.Err(msg);
                    }
                    // reload.
                    items = S8x2WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, pcCard.DoffNo, 2).Value();
                }

                grid2.ItemsSource = items;
            }
        }

        #endregion
    }
}
