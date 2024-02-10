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

using NLib.Services;
using M3.Cord.Models;
using NLib.Models;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for AgeingConditionFromDIPViewPage.xaml
    /// </summary>
    public partial class AgeingConditionFromDIPViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AgeingConditionFromDIPViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private S5Condition condition = null;

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
            var page = M3CordApp.Pages.AgeingHistorySearch;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }

        #endregion

        #region Private Methods

        private void Print()
        {
            if (null == condition)
                return;

            var item = S5ConditionPrintModel.Gets(condition.S5ConditionId).Value().FirstOrDefault();
            if (null != item)
            {
                var items = new List<S5ConditionPrintModel>();
                if (!string.IsNullOrEmpty(item.ProductCode1) &&
                    !string.IsNullOrEmpty(item.ProductCode2) &&
                    item.ProductCode1 != item.ProductCode1)
                {
                    // Has both product code but not same
                    // require duplicate and update ProductCode to make 2 records
                    var item1 = item.ShallowCopy();
                    item1.ProductCode = item1.ProductCode1;

                    var item2 = item.ShallowCopy();
                    item2.ProductCode = item1.ProductCode2;

                    items.Add(item1);
                    items.Add(item2);
                }
                else
                {
                    items.Add(item);
                }
                var page = M3CordApp.Pages.S5ReportPreviewView;
                page.Setup(items);
                PageContentManager.Instance.Current = page;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(S5Condition cond)
        {
            this.DataContext = null;
            condition = cond;
            this.DataContext = condition;
        }

        #endregion
    }
}
