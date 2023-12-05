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

namespace M3.Cord.Controls.Documents
{
    /// <summary>
    /// Interaction logic for S5ConditionTwistingEntryPage.xaml
    /// </summary>
    public partial class S5ConditionTwistingEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S5ConditionTwistingEntryPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private S5Condition condition = null;

        #endregion

        #region Public Methods

        public void Setup(S5Condition value)
        {
            this.DataContext = null;

            condition = value;

            this.DataContext = condition;
        }

        #endregion

        #region Public Properties

        #endregion
    }
}
