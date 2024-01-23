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
    /// Interaction logic for CordSamplingDetailsPage.xaml
    /// </summary>
    public partial class CordSamplingDetailsPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordSamplingDetailsPage()
        {
            InitializeComponent();

            this.DataContext = null;
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;
        private PCTwist1 pcCard;
        private CordSamplingDetails _item;

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc, PCTwist1 PCCard, CordSamplingDetails item = null)
        {
            selectedMC = mc;
            pcCard = PCCard;
            if (null != pcCard)
            {
                _item = item; // actual code
                if (null == _item)
                {
                    _item = new CordSamplingDetails();
                    /*
                    condition = CordSamplingDetails.Create(pcCard.ProductCode);
                    condition.PCTwist1Id = pcCard.PCTwist1Id;
                    condition.UpdateBy = (null != M3CordApp.Current.User) ?
                        M3CordApp.Current.User.FullName : null;
                    */
                }
                else
                {
                    _item = item; // actual code
                    /*
                    _item.UpdateBy = (null != M3CordApp.Current.User) ?
                        M3CordApp.Current.User.FullName : null;
                    */
                }
            }

            this.DataContext = _item;
        }

        public void Save()
        {
            if (null != _item)
            {
                /*
                var ret = CordSamplingDetails.Save(_item);
                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
                */
            }
        }

        #endregion
    }
}
