#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;

using Dapper;
using Newtonsoft.Json;

#endregion

namespace M3.Cord.Models
{
    public class G4IssueYarn : NInpc
    {
        #region Public Properties

        public int G4IssueYarnPkId { get; set; }
        public string RequestId { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string PalletNo { get; set; }
        public string TraceNo { get; set; }
        public decimal? WeightQty { get; set; }
        public decimal? ConeCH { get; set; }
        public string PalletType { get; set; }
        public string Remark { get; set; }
        public DateTime? EditDate { get; set; }
        public int? EditBy { get; set; } = new int?();
        public bool DeleteFlag { get; set; }

        // FROM G4YARN
        public int G4YarnPkId { get; set; }
        public DateTime? EntryDate { get; set; }
        public string LotNo { get; set; }
        public string ItemYarn { get; set; }
        public string YarnType { get; set; }
        public string Item400 { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int? ReceiveBy { get; set; }
        public bool Verify { get; set; }

        public SolidColorBrush TextColor
        {
            get 
            {
                return (string.IsNullOrEmpty(RequestId)) ? ModelConsts.BlackColor : ModelConsts.RedColor;
            }
            set { }
        }

        public bool IsMark
        {
            get { return !string.IsNullOrEmpty(RequestId); }
            set { }
        }

        #endregion

        #region Public Methods

        public void MarkIssue(string requestId, string issueBy, 
            //string issueTo, 
            DateTime? issueDate)
        {
            RequestId = requestId;
            IssueBy = issueBy;
            //IssueTo = issueTo;
            IssueDate = issueDate;

            Raise(() => this.IsMark);
            Raise(() => this.TextColor);
        }

        public void UnmarkIssue()
        {
            RequestId = null;
            IssueBy = null;
            //IssueTo = null;
            IssueDate = new DateTime?();

            Raise(() => this.IsMark);
            Raise(() => this.TextColor);
        }

        #endregion

        #region Static Methods

        public static List<G4IssueYarn> GetG4IssueYarns(DateTime? receiveDate = new DateTime?())
        {
            var rets = new List<G4IssueYarn>();

            return rets;
        }

        #endregion
    }
}
