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
    public class FirstTwistMC : NInpc
    {
        #region Public Properties

        public string MCName { get; set; }
        public int BBCount { get; set; } = 102;
        public int SheetPerBB { get; set; } = 1;

        public string LotNo { get; set; } = "-";

        #endregion

        #region Static Methods

        public static List<FirstTwistMC> Gets()
        {
            List<FirstTwistMC> rets = new List<FirstTwistMC>();

            rets.Add(new FirstTwistMC() { MCName = "S-1", SheetPerBB = 2, LotNo = "21091" });
            rets.Add(new FirstTwistMC() { MCName = "S-4" });

            return rets;
        }

        #endregion
    }
}
