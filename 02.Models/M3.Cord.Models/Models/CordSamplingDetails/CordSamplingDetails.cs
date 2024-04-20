#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;
using NLib.Models;

using Dapper;
using Newtonsoft.Json;

#endregion

namespace M3.Cord.Models
{
    public class CordSamplingDetails : NInpc
    {
        #region Public Proeprties

        public int? SamplingId { get; set; }
        public DateTime? SamplingDate { get; set; }

        public bool? TwistYarn { get; set; }
        public bool? CordDipping { get; set; }
        public bool? Rawmaterial { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string CordStructure { get; set; }
        public string LotNo { get; set; }
        public bool? ProductionSituationTwisting { get; set; } = false;
        public bool? ProductionSituationHeatSet { get; set; } = false;
        public bool? ProductionSituationDipCord { get; set; } = false;
        public bool? ProductionSituationJointing { get; set; } = false;
        public string StandardOfTwisting { get; set; }

        public string MCCode { get; set; }

        public bool? Bobbin { get; set; } = false;
        public bool? Cheese { get; set; } = false;
        public bool? TaperBB { get; set; } = false;
        public bool? ContainerOthers { get; set; } = false;
        public string SpindleNo { get; set; }
        public string Quantity { get; set; }
        public bool? Polyester { get; set; } = false;
        public bool? Nylon66 { get; set; } = false;
        public bool? MaterialOthers { get; set; } = false;
        public string YarnType { get; set; }
        public bool? Process { get; set; } = false;
        public bool? Product { get; set; } = false;
        public bool? Special { get; set; } = false;
        public bool? Trial { get; set; } = false;
        public string Shift { get; set; }
        public string ShiftName { get; set; }
        public string Remark { get; set; }

        public string Sender { get; set; }
        public DateTime? SenderTime { get; set; }

        public bool CanSend
        {
            get { return !SenderTime.HasValue; }
            set { }
        }

        public bool? Direct { get; set; } = false;
        public bool? Relax { get; set; } = false;
        public string Relax_k { get; set; }
        public string Relax_n { get; set; }
        public bool? Necessary { get; set; } = false;
        public bool? NotNecessary { get; set; } = false;
        public bool? SpecialMentionJointing { get; set; } = false;
        public bool? SpecialMentionNoJointing { get; set; } = false;

        public decimal? TensileR1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileRSum);
                });
            }
        }
        public decimal? TensileR2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileRSum);
                });
            }
        }
        public decimal? TensileR3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileRSum);
                });
            }
        }
        public decimal? TensileR4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileRSum);
                });
            }
        }
        public decimal? TensileR5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileRSum);
                });
            }
        }
        public decimal? TensileRSum
        {
            get
            {
                int iCnt = 0;
                decimal total = decimal.Zero;
                decimal? result = new decimal?();

                if (TensileR1.HasValue)
                {
                    total += TensileR1.Value;
                    iCnt++;
                }
                if (TensileR2.HasValue)
                {
                    total += TensileR2.Value;
                    iCnt++;
                }
                if (TensileR3.HasValue)
                {
                    total += TensileR3.Value;
                    iCnt++;
                }
                if (TensileR4.HasValue)
                {
                    total += TensileR4.Value;
                    iCnt++;
                }
                if (TensileR5.HasValue)
                {
                    total += TensileR5.Value;
                    iCnt++;
                }

                if (iCnt > 0)
                {
                    result = total / iCnt;
                }

                return result;
            }
            set { }
        }

        public decimal? TensileL1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () => 
                {
                    Raise(() => this.TensileLSum);
                });
            }
        }
        public decimal? TensileL2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileLSum);
                });
            }
        }
        public decimal? TensileL3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileLSum);
                });
            }
        }
        public decimal? TensileL4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileLSum);
                });
            }
        }
        public decimal? TensileL5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.TensileLSum);
                });
            }
        }
        public decimal? TensileLSum 
        {
            get
            {
                int iCnt = 0;
                decimal total = decimal.Zero;
                decimal? result = new decimal?();

                if (TensileL1.HasValue)
                {
                    total += TensileL1.Value;
                    iCnt++;
                }
                if (TensileL2.HasValue)
                {
                    total += TensileL2.Value;
                    iCnt++;
                }
                if (TensileL3.HasValue)
                {
                    total += TensileL3.Value;
                    iCnt++;
                }
                if (TensileL4.HasValue)
                {
                    total += TensileL4.Value;
                    iCnt++;
                }
                if (TensileL5.HasValue)
                {
                    total += TensileL5.Value;
                    iCnt++;
                }

                if (iCnt > 0)
                {
                    result = total / iCnt;
                }

                return result;
            }
            set { }
        }

        public decimal? FirstTwistingR1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingRSum);
                });
            }
        }
        public decimal? FirstTwistingR2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingRSum);
                });
            }
        }
        public decimal? FirstTwistingR3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingRSum);
                });
            }
        }
        public decimal? FirstTwistingR4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingRSum);
                });
            }
        }
        public decimal? FirstTwistingR5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingRSum);
                });
            }
        }
        public decimal? FirstTwistingRSum 
        {
            get
            {
                int iCnt = 0;
                decimal total = decimal.Zero;
                decimal? result = new decimal?();

                if (FirstTwistingR1.HasValue)
                {
                    total += FirstTwistingR1.Value;
                    iCnt++;
                }
                if (FirstTwistingR2.HasValue)
                {
                    total += FirstTwistingR2.Value;
                    iCnt++;
                }
                if (FirstTwistingR3.HasValue)
                {
                    total += FirstTwistingR3.Value;
                    iCnt++;
                }
                if (FirstTwistingR4.HasValue)
                {
                    total += FirstTwistingR4.Value;
                    iCnt++;
                }
                if (FirstTwistingR5.HasValue)
                {
                    total += FirstTwistingR5.Value;
                    iCnt++;
                }

                if (iCnt > 0)
                {
                    result = total / iCnt;
                }

                return result;
            }
            set { }
        }

        public decimal? FirstTwistingL1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingLSum);
                });
            }
        }
        public decimal? FirstTwistingL2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingLSum);
                });
            }
        }
        public decimal? FirstTwistingL3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingLSum);
                });
            }
        }
        public decimal? FirstTwistingL4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingLSum);
                });
            }
        }
        public decimal? FirstTwistingL5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.FirstTwistingLSum);
                });
            }
        }
        public decimal? FirstTwistingLSum
        {
            get
            {
                int iCnt = 0;
                decimal total = decimal.Zero;
                decimal? result = new decimal?();

                if (FirstTwistingL1.HasValue)
                {
                    total += FirstTwistingL1.Value;
                    iCnt++;
                }
                if (FirstTwistingL2.HasValue)
                {
                    total += FirstTwistingL2.Value;
                    iCnt++;
                }
                if (FirstTwistingL3.HasValue)
                {
                    total += FirstTwistingL3.Value;
                    iCnt++;
                }
                if (FirstTwistingL4.HasValue)
                {
                    total += FirstTwistingL4.Value;
                    iCnt++;
                }
                if (FirstTwistingL5.HasValue)
                {
                    total += FirstTwistingL5.Value;
                    iCnt++;
                }

                if (iCnt > 0)
                {
                    result = total / iCnt;
                }

                return result;
            }
            set { }
        }
    
        public decimal? SecondTwistingR1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingRSum);
                });
            }
        }
        public decimal? SecondTwistingR2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingRSum);
                });
            }
        }
        public decimal? SecondTwistingR3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingRSum);
                });
            }
        }
        public decimal? SecondTwistingR4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingRSum);
                });
            }
        }
        public decimal? SecondTwistingR5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingRSum);
                });
            }
        }
        public decimal? SecondTwistingRSum
        {
            get
            {
                int iCnt = 0;
                decimal total = decimal.Zero;
                decimal? result = new decimal?();

                if (SecondTwistingR1.HasValue)
                {
                    total += SecondTwistingR1.Value;
                    iCnt++;
                }
                if (SecondTwistingR2.HasValue)
                {
                    total += SecondTwistingR2.Value;
                    iCnt++;
                }
                if (SecondTwistingR3.HasValue)
                {
                    total += SecondTwistingR3.Value;
                    iCnt++;
                }
                if (SecondTwistingR4.HasValue)
                {
                    total += SecondTwistingR4.Value;
                    iCnt++;
                }
                if (SecondTwistingR5.HasValue)
                {
                    total += SecondTwistingR5.Value;
                    iCnt++;
                }

                if (iCnt > 0)
                {
                    result = total / iCnt;
                }

                return result;
            }
            set { }
        }

        public decimal? SecondTwistingL1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingLSum);
                });
            }
        }
        public decimal? SecondTwistingL2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingLSum);
                });
            }
        }
        public decimal? SecondTwistingL3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingLSum);
                });
            }
        }
        public decimal? SecondTwistingL4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingLSum);
                });
            }
        }
        public decimal? SecondTwistingL5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    Raise(() => this.SecondTwistingLSum);
                });
            }
        }
        public decimal? SecondTwistingLSum
        {
            get
            {
                int iCnt = 0;
                decimal total = decimal.Zero;
                decimal? result = new decimal?();

                if (SecondTwistingL1.HasValue)
                {
                    total += SecondTwistingL1.Value;
                    iCnt++;
                }
                if (SecondTwistingL2.HasValue)
                {
                    total += SecondTwistingL2.Value;
                    iCnt++;
                }
                if (SecondTwistingL3.HasValue)
                {
                    total += SecondTwistingL3.Value;
                    iCnt++;
                }
                if (SecondTwistingL4.HasValue)
                {
                    total += SecondTwistingL4.Value;
                    iCnt++;
                }
                if (SecondTwistingL5.HasValue)
                {
                    total += SecondTwistingL5.Value;
                    iCnt++;
                }

                if (iCnt > 0)
                {
                    result = total / iCnt;
                }

                return result;
            }
            set { }
        }

        public string Remark1 { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string Remark4 { get; set; }
        public string Remark5 { get; set; }
        public string RemarkSum { get; set; }

        public string RemarkOther { get; set; }

        public string Tester { get; set; }
        public DateTime? TesterTime { get; set; }

        public bool IsQA { get; set; }

        public bool CanTest
        {
            get 
            {
                //return IsQA && SenderTime.HasValue && !TesterTime.HasValue; 
                return IsQA;
            }
            set { }
        }

        public bool? TestResult { get; set; }

        public bool TestResultOK 
        {
            get { return (TestResult.HasValue && TestResult.Value == true) ? true : false; }
            set 
            {
                TestResult = true;
                Raise(() => TestResultOK);
                Raise(() => TestResultNG);
                Raise(() => TestResult);
            }
        }
        public bool TestResultNG 
        {
            get { return (TestResult.HasValue && TestResult.Value == false) ? true : false; }
            set
            {
                TestResult = false;
                Raise(() => TestResultOK);
                Raise(() => TestResultNG);
                Raise(() => TestResult);
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<CordSamplingDetails>> Gets(string MCCode, 
            string LotNo, string ProductCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordSamplingDetails>> rets = new NDbResult<List<CordSamplingDetails>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@MCCode", string.IsNullOrWhiteSpace(MCCode) ? null : MCCode);
            p.Add("@LotNo", string.IsNullOrWhiteSpace(LotNo) ? null : LotNo);
            p.Add("@ProductCode", string.IsNullOrWhiteSpace(ProductCode) ? null : ProductCode);

            try
            {
                var items = cnn.Query<CordSamplingDetails>("GetCordSamplingDetails", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<CordSamplingDetails>();
            }

            return rets;
        }

        /// <summary>
        /// GetQAs
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<CordSamplingDetails>> GetQAs(DateTime? date,
            string LotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordSamplingDetails>> rets = new NDbResult<List<CordSamplingDetails>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@Date", date);
            p.Add("@LotNo", string.IsNullOrWhiteSpace(LotNo) ? null : LotNo);

            try
            {
                var items = cnn.Query<CordSamplingDetails>("GetQACordSamplingDetails", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<CordSamplingDetails>();
            }

            return rets;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The CordSamplingDetails item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordSamplingDetails> Save(CordSamplingDetails value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordSamplingDetails> ret = new NDbResult<CordSamplingDetails>();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();

            p.Add("@SamplingDate", value.SamplingDate);
            p.Add("@TwistYarn", value.TwistYarn);
            p.Add("@CordDipping", value.CordDipping);
            p.Add("@Rawmaterial", value.Rawmaterial);
            p.Add("@CustomerName", value.CustomerName);
            p.Add("@ProductCode", value.ProductCode);
            p.Add("@CordStructure", value.CordStructure);
            p.Add("@LotNo", value.LotNo);
            p.Add("@ProductionSituationTwisting", value.ProductionSituationTwisting);
            p.Add("@ProductionSituationHeatSet", value.ProductionSituationHeatSet);
            p.Add("@ProductionSituationDipCord", value.ProductionSituationDipCord);
            p.Add("@ProductionSituationJointing", value.ProductionSituationJointing);
            p.Add("@StandardOfTwisting", value.StandardOfTwisting);

            p.Add("@MCCode", value.MCCode);

            p.Add("@Bobbin", value.Bobbin);
            p.Add("@Cheese", value.Cheese);
            p.Add("@TaperBB", value.TaperBB);
            p.Add("@ContainerOthers", value.ContainerOthers);
            p.Add("@SpindleNo", value.SpindleNo);
            p.Add("@Quantity", value.Quantity);
            p.Add("@Polyester", value.Polyester);
            p.Add("@Nylon66", value.Nylon66);
            p.Add("@MaterialOthers", value.MaterialOthers);
            p.Add("@YarnType", value.YarnType);
            p.Add("@Process", value.Process);
            p.Add("@Product", value.Product);
            p.Add("@Special", value.Special);
            p.Add("@Trial", value.Trial);
            p.Add("@Shift", value.Shift);
            p.Add("@ShiftName", value.ShiftName);
            p.Add("@Remark", value.Remark);

            p.Add("@Sender", value.Sender);
            p.Add("@SenderTime", value.SenderTime);

            p.Add("@Direct", value.Direct);
            p.Add("@Relax", value.Relax);
            p.Add("@Relax_k", value.Relax_k);
            p.Add("@Relax_n", value.Relax_n);
            p.Add("@Necessary", value.Necessary);
            p.Add("@NotNecessary", value.NotNecessary);
            p.Add("@SpecialMentionJointing", value.SpecialMentionJointing);
            p.Add("@SpecialMentionNoJointing", value.SpecialMentionNoJointing);
            p.Add("@TensileR1", value.TensileR1);
            p.Add("@TensileL1", value.TensileL1);
            p.Add("@TensileR2", value.TensileR2);
            p.Add("@TensileL2", value.TensileL2);
            p.Add("@TensileR3", value.TensileR3);
            p.Add("@TensileL3", value.TensileL3);
            p.Add("@TensileR4", value.TensileR4);
            p.Add("@TensileL4", value.TensileL4);
            p.Add("@TensileR5", value.TensileR5);
            p.Add("@TensileL5", value.TensileL5);
            p.Add("@TensileRSum", value.TensileRSum);
            p.Add("@TensileLSum", value.TensileLSum);
            p.Add("@FirstTwistingR1", value.FirstTwistingR1);
            p.Add("@FirstTwistingL1", value.FirstTwistingL1);
            p.Add("@FirstTwistingR2", value.FirstTwistingR2);
            p.Add("@FirstTwistingL2", value.FirstTwistingL2);
            p.Add("@FirstTwistingR3", value.FirstTwistingR3);
            p.Add("@FirstTwistingL3", value.FirstTwistingL3);
            p.Add("@FirstTwistingR4", value.FirstTwistingR4);
            p.Add("@FirstTwistingL4", value.FirstTwistingL4);
            p.Add("@FirstTwistingR5", value.FirstTwistingR5);
            p.Add("@FirstTwistingL5", value.FirstTwistingL5);
            p.Add("@FirstTwistingRSum", value.FirstTwistingRSum);
            p.Add("@FirstTwistingLSum", value.FirstTwistingLSum);
            p.Add("@SecondTwistingR1", value.SecondTwistingR1);
            p.Add("@SecondTwistingL1", value.SecondTwistingL1);
            p.Add("@SecondTwistingR2", value.SecondTwistingR2);
            p.Add("@SecondTwistingL2", value.SecondTwistingL2);
            p.Add("@SecondTwistingR3", value.SecondTwistingR3);
            p.Add("@SecondTwistingL3", value.SecondTwistingL3);
            p.Add("@SecondTwistingR4", value.SecondTwistingR4);
            p.Add("@SecondTwistingL4", value.SecondTwistingL4);
            p.Add("@SecondTwistingR5", value.SecondTwistingR5);
            p.Add("@SecondTwistingL5", value.SecondTwistingL5);
            p.Add("@SecondTwistingRSum", value.SecondTwistingRSum);
            p.Add("@SecondTwistingLSum", value.SecondTwistingLSum);
            p.Add("@Remark1", value.Remark1);
            p.Add("@Remark2", value.Remark2);
            p.Add("@Remark3", value.Remark3);
            p.Add("@Remark4", value.Remark4);
            p.Add("@Remark5", value.Remark5);
            p.Add("@RemarkSum", value.RemarkSum);

            p.Add("@RemarkOther", value.RemarkOther);

            p.Add("@Tester", value.Tester);
            p.Add("@TesterTime", value.TesterTime);
            p.Add("@TestResult", value.TestResult);

            p.Add("@SamplingId", value.SamplingId, DbType.Int32, direction: ParameterDirection.InputOutput);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("SaveCordSamplingDetails", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);

                // get pk
                value.SamplingId = p.Get<dynamic>("@SamplingId");

                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        #endregion
    }
}
