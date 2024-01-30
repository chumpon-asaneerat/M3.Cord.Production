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
    /// Interaction logic for S4x2ConditionEntryPage.xaml
    /// </summary>
    public partial class S4x2ConditionEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S4x2ConditionEntryPage()
        {
            InitializeComponent();

            this.DataContext = null;
        }

        #endregion

        #region Internal Variables

        private PCTwist1 pcCard;
        private S4x2Condition condition;

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Setup(PCTwist1 PCCard, S4x2Condition item = null)
        {
            pcCard = PCCard;
            if (null != pcCard)
            {
                if (null == item)
                {
                    condition = S4x2Condition.Create(pcCard.ProductCode);
                    condition.PCTwist1Id = pcCard.PCTwist1Id;

                    // Copy from PC
                    condition.MCCode = pcCard.MCCode;
                    condition.ProductLotNo = pcCard.ProductLotNo;
                    condition.CustomerName = pcCard.CustomerName;
                    condition.ProductName = pcCard.ProductName;
                    condition.ItemYarn = pcCard.ItemYarn;
                    condition.CordStructure = pcCard.CordStructure;
                    condition.TreatRoute = pcCard.TreatRoute;
                    condition.TwistSpec = pcCard.TwistSpec;
                    condition.IssueDate = pcCard.IssueDate;
                    condition.IssueBy = pcCard.IssueBy;

                    condition.UpdateBy = (null != M3CordApp.Current.User) ?
                        M3CordApp.Current.User.FullName : null;
                }
                else
                {
                    var std = S4x2ConditionStd.Gets(pcCard.ProductCode).Value().FirstOrDefault();
                    
                    condition = item; // actual code
                    S4x2Condition.Assign(std, condition); // Temp fixed
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
                var ret = S4x2Condition.Save(condition);
                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
            }
        }

        #endregion

        public S4x2Condition Condition { get { return condition; } }
    }
}
