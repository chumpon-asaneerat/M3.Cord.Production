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
	public class SolutionById
	{
		#region Public Proeprties

		public string DIPProductCode { get; set; }
		public int? SolutionId { get; set; }
		public string SolutionName { get; set; }
		public string recipe { get; set; }

		#endregion

		#region Static Methods

		public static NDbResult<List<SolutionById>> Gets(int? solutionid)
		{
			MethodBase med = MethodBase.GetCurrentMethod();

			NDbResult<List<SolutionById>> rets = new NDbResult<List<SolutionById>>();

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
			p.Add("@solutionid", solutionid);

			try
			{
				var items = cnn.Query<SolutionById>("ChGetSolutionById", p,
					commandType: CommandType.StoredProcedure);
				var data = (null != items) ? items.ToList() : null;
                //rets.Success(data);

                List<SolutionById> results = new List<SolutionById>();
				SolutionById result = new SolutionById();

                if (null != data && data.Count > 0)
                {
					foreach (var item in data)
					{
						result = new SolutionById();

						result.DIPProductCode = item.DIPProductCode;
						result.SolutionId = item.SolutionId;
						result.SolutionName = item.SolutionName;
						result.recipe = item.recipe;

						if (!string.IsNullOrEmpty(result.recipe))
							results.Add(result);
					}
                }

                if (results != null && results.Count > 0)
                    rets.Success(results);
            }
			catch (Exception ex)
			{
				med.Err(ex);
				// Set error number/message
				rets.ErrNum = 9999;
				rets.ErrMsg = ex.Message;
			}

			return rets;
		}

		#endregion
	}
}
