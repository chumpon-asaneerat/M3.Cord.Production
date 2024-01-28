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

        private PCTwist1 pcCard;
        private CordSamplingDetails _item;

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Setup(PCTwist1 PCCard, bool isQA, 
            CordSamplingDetails item = null)
        {
            pcCard = PCCard;
            if (null != pcCard)
            {
                _item = item; // actual code
                if (null == _item)
                {
                    _item = new CordSamplingDetails();
                    _item.SamplingDate = DateTime.Now;
                    _item.LotNo = pcCard.ProductLotNo;
                    _item.CustomerName = pcCard.CustomerName;
                    _item.CordStructure = pcCard.CordStructure;
                    _item.ProductCode = pcCard.ProductCode;
                    _item.MCCode = pcCard.MCCode;
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
                _item.IsQA = isQA;
            }

            this.DataContext = _item;
        }

        public void SetupQA(CordSamplingDetails item = null)
        {
            this.DataContext = null;

            _item = item; // actual code
            if (null != _item)
            {
                _item.IsQA = true;
            }

            this.DataContext = _item;
        }

        public void Save()
        {
            this.DataContext = null;

            if (null != _item)
            {
                if (!_item.SenderTime.HasValue)
                {
                    _item.Sender = (null != M3CordApp.Current.User) ?
                        M3CordApp.Current.User.FullName : null;
                    _item.SenderTime = DateTime.Now;
                }
                else
                {
                    if (!_item.TesterTime.HasValue)
                    {
                        _item.Tester = (null != M3CordApp.Current.User) ?
                            M3CordApp.Current.User.FullName : null;
                        _item.TesterTime = DateTime.Now;
                    }
                }

                var ret = CordSamplingDetails.Save(_item);
                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
            }

            this.DataContext = _item;
        }

        #endregion
    }
}
