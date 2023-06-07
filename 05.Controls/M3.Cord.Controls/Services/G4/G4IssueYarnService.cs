#region Using

using System;
using System.Collections.Generic;

using NLib;
using NLib.Models;
using M3.Cord.Models;
using System.Windows.Documents;

#endregion

namespace M3.Cord
{
    /// <summary>
    /// The G4 Issue Yarn Service.
    /// </summary>
    public class G4IssueYarnService : NSingelton<G4IssueYarnService>
    {
        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        protected G4IssueYarnService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~G4IssueYarnService()
        {

        }

        #endregion

        #region Internal Variables

        private string _requestNo = string.Empty;

        private int _totalPallet = 0;
        private decimal _totalWeight = decimal.Zero;
        private decimal _totalCH = decimal.Zero;

        private List<G4IssueYarn> _issueItems = new List<G4IssueYarn>();

        #endregion

        #region Private Methods

        private void CalcTotals()
        {
            _totalPallet = 0;
            _totalWeight = decimal.Zero;
            _totalCH = 0;

            lock (this)
            {
                if (null != _issueItems && _issueItems.Count > 0)
                {
                    _issueItems.ForEach(issue =>
                    {
                        if (issue.IsMark)
                        {
                            // add mark pallet
                            _totalPallet++;
                            // add weight
                            var weight = (issue.WeightQty.HasValue) ? issue.WeightQty.Value : decimal.Zero;
                            _totalWeight += weight;
                            // add cheese count.
                            var cheeseCnt = (issue.ConeCH.HasValue) ? issue.ConeCH.Value : decimal.Zero;
                            _totalCH += cheeseCnt;
                        }
                    });
                }
            }
            // Raise Events
            Raise(() => this.TotalPallet);
            Raise(() => this.TotalWeight);
            Raise(() => this.TotalCH);
        }

        private void ClearUnmark()
        {
            lock (this)
            {
                if (null == _issueItems) _issueItems = new List<G4IssueYarn>();
                var removes = new List<G4IssueYarn>();
                _issueItems.ForEach(issueItem =>
                {
                    if (!issueItem.IsMark) removes.Add(issueItem);
                });

                removes.ForEach(remove =>
                {
                    _issueItems.Remove(remove);
                });
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clear receive list.
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                _issueItems = new List<G4IssueYarn>();
            }
            CalcTotals();
        }

        private void LoadRequest(string requestNo)
        {
            Clear();
            _issueItems = G4IssueYarn.GetG4IssueYarns(requestNo).Value();
        }

        public void LoadYanStocks(string itemYarn)
        {
            ClearUnmark();

            var yarnStocks = G4Yarn.SearchYarnStocks(itemYarn).Value();
            if (null != yarnStocks && yarnStocks.Count > 0)
            {
                yarnStocks.ForEach(yarnStock =>
                {
                    var idx = FindPalletIndex(yarnStock.PalletNo);
                    if (idx != -1) return; // already exists.

                    G4IssueYarn inst = new G4IssueYarn();
                    // clone data.
                    inst.ConeCH = yarnStock.ConeCH;
                    inst.DeleteFlag = false;
                    inst.EntryDate = yarnStock.EntryDate;
                    inst.G4YarnPkId = yarnStock.G4YarnPkId.Value;
                    inst.Item400 = yarnStock.Item400;
                    inst.ItemYarn = yarnStock.ItemYarn;
                    inst.LotNo = yarnStock.LotNo;
                    inst.PalletNo = yarnStock.PalletNo;
                    inst.PalletType = yarnStock.PalletType;
                    inst.ReceiveBy = yarnStock.ReceiveBy;
                    inst.ReceiveDate = yarnStock.ReceiveDate;
                    inst.TraceNo = yarnStock.TraceNo;
                    inst.WeightQty = yarnStock.WeightQty;
                    inst.YarnType = yarnStock.YarnType;
                    // add to list.
                    _issueItems.Add(inst);
                });

                CalcTotals();
            }
        }

        private int FindPalletIndex(string palletNo)
        {
            if (string.IsNullOrEmpty(palletNo))
                return -1;
            if (null == _issueItems)
                return -1;
            int idx = _issueItems.FindIndex(item =>
            {
                bool match = (string.CompareOrdinal(item.PalletNo, palletNo) == 0);
                return match;
            });
            return idx;
        }

        public void MarkIssue(string requestNo, string palletNo)
        {
            if (string.IsNullOrEmpty(requestNo))
                return;
            if (null == _issueItems)
                return;
            int idx = FindPalletIndex(palletNo);
            if (idx != -1)
            {
                _issueItems[idx].MarkIssue(requestNo, null, IssueDate);
            }

            CalcTotals();
        }

        public bool SaveIssueYarns()
        {
            bool bSuccess = false;
            if (null != _issueItems)
            {
                lock (this)
                {
                    // save only mark items
                    var markItems = new List<G4IssueYarn>();

                    _issueItems.ForEach(yarn =>
                    {
                        if (yarn.IsMark && yarn.G4IssueYarnPkId == 0) 
                        { 
                            markItems.Add(yarn); 
                        }
                    });
                    var ret = G4IssueYarn.Save(markItems);

                    bSuccess = (null != ret && ret.Ok);
                }
            }
            Clear(); // clear list.

            return bSuccess;
        }

        #endregion

        #region Public Properties

        public string RequestNo
        {
            get { return _requestNo; }
            set
            {
                if (_requestNo != value)
                {
                    _requestNo = value;
                    LoadRequest(_requestNo);
                }
            }
        }
        /// <summary>
        /// Gets Issue Yarn Items (Raw Meterial) List.
        /// </summary>
        public List<G4IssueYarn> IssueItems
        {
            get { return _issueItems; }
            set { }
        }
        /// <summary>
        /// Gets or sets Issue Date.
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>Gets Total Pallet.</summary>
        public int TotalPallet { get { return _totalPallet; } set { } }
        /// <summary>Gets Total Weight.</summary>
        public decimal TotalWeight { get { return _totalWeight; } set { } }
        /// <summary>Gets Total CH (Cheese).</summary>
        public decimal TotalCH { get { return _totalCH; } set { } }

        #endregion
    }
}
