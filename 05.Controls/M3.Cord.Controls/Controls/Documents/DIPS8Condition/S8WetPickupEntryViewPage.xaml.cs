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
    /// Interaction logic for S8WetPickupEntryViewPage.xaml
    /// </summary>
    public partial class S8WetPickupEntryViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public S8WetPickupEntryViewPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPPCCard pcCard;

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
                grid.ItemsSource = S8WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, pcCard.DoffNo, 1).Value();
            }

            grid2.ItemsSource = null;
            if (null != pcCard)
            {
                grid2.ItemsSource = S8WetPickUpItem.Gets(pcCard.ProductCode, pcCard.DIPLotNo, DateTime.Now, pcCard.DoffNo, 2).Value();
            }
        }

        #endregion
    }
}
