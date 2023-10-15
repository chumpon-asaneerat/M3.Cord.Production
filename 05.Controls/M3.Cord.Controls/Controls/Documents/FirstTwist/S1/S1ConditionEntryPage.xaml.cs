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
using NLib.Reflection;

#endregion

namespace M3.Cord.Controls.Documents
{
    /// <summary>
    /// Interaction logic for S1ConditionEntry.xaml
    /// </summary>
    public partial class S1ConditionEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S1ConditionEntryPage()
        {
            InitializeComponent();

            this.DataContext = null;
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;
        private S1Condition condition;

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc, PCTwist1 PCCard, S1Condition item = null) 
        {
            selectedMC = mc;
            pcCard = PCCard;
            condition = item;
            if (null != pcCard && null == item)
            {
                if (null != item)
                {
                    condition = S1Condition.Create(pcCard.ProductCode);
                    condition.PCTwist1Id = pcCard.PCTwist1Id;
                    condition.UpdateBy = (null != M3CordApp.Current.User) ?
                        M3CordApp.Current.User.FullName : null;
                }
                else
                {
                    var std = S1ConditionStd.Gets(pcCard.ProductCode).Value().FirstOrDefault();

                    condition = item; // actual code
                    S1Condition.Assign(std, condition); // Temp fixed
                    condition.UpdateBy = (null != M3CordApp.Current.User) ?
                        M3CordApp.Current.User.FullName : null;
                }
            }

            this.DataContext = condition;
        }

        public void Save()
        {
            if (null != condition)
            {
                S1Condition.Save(condition);
            }
        }

        #endregion
    }
}
