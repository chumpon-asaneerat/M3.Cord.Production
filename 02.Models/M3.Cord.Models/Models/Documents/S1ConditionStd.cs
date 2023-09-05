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
using System.Net;
using NLib.Models;

#endregion

namespace M3.Cord.Models
{
    public class S1ConditionStd : ConditionStd
    {
        public static readonly S1ConditionStd[] Defaults = new S1ConditionStd[]
        {
            
        };

        private static S1ConditionStd Create(string productCode,
            string pName, ConditionParamTypes pType, bool sc,
            string sVal = null,
            decimal? dVal = new decimal?(),
            decimal? eVal = new decimal?())
        {
            return new S1ConditionStd()
            { 
                ProcessName = "S-1",
                ProductCode = productCode,
                ParamName = pName,
                ParamType = pType,
                SC = sc,
                StdValueS = sVal,
                StdValueD = dVal,
                StdValueE = eVal
            };
        }

        private static List<S1ConditionStd> Creates(string productCode)
        {
            var results = new List<S1ConditionStd>();

            // Ring Diameter
            results.Add(Create(productCode, "RingDiameter", ConditionParamTypes.String, false, "8 1/2\" R x 14\" L"));
            // TwistChangeGear (I x J x K)
            results.Add(Create(productCode, "TwistChangeGearI", ConditionParamTypes.Number, true, null, 41));
            results.Add(Create(productCode, "TwistChangeGearJ", ConditionParamTypes.Number, true, null, 45));
            results.Add(Create(productCode, "TwistChangeGearK", ConditionParamTypes.Number, true, null, 83));
            // LifterChangeGear (A x B)
            results.Add(Create(productCode, "LifterChangeGearA", ConditionParamTypes.Number, false, null, 48));
            results.Add(Create(productCode, "LifterChangeGearB", ConditionParamTypes.Number, false, null, 56));
            // Outer Diameter
            results.Add(Create(productCode, "OuterDiameter", ConditionParamTypes.Number, false, null, 217));
            // TravellerNo
            results.Add(Create(productCode, "TravellerNo", ConditionParamTypes.String, false, "RK-500 J x 1 Pcs."));

            return results;
        }
    }
}
