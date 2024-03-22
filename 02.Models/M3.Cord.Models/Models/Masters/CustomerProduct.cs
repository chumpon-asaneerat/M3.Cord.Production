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
    public class CustomerProduct
    {
        #region Public Properties

        public bool IsSelected { get; set; }
        public string ProductCode { get; set; }
        public string CustomerName { get; set; }
        public int? ServiceLifeMonth { get; set; }
        public string Remark { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<CustomerProduct>> Gets(string ProductCode = null)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CustomerProduct>> rets = new NDbResult<List<CustomerProduct>>();

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

            // Gets customers
            var customers = Customer.Gets().Value();



            string query = string.Empty;
            query += "SELECT * " + Environment.NewLine;
            query += "  FROM CustomerProduct " + Environment.NewLine;
            query += " WHERE ProductCode = @ProductCode " + Environment.NewLine;

            var p = new DynamicParameters();
            p.Add("@ProductCode", ProductCode);

            try
            {
                var items = cnn.Query<CustomerProduct>(query, p,
                    commandType: CommandType.Text);
                var data = (null != items) ? items.ToList() : null;

                var results = new List<CustomerProduct>();
                // load all customers
                if (null != customers)
                {
                    foreach (var customer in customers)
                    {
                        if (!customer.CustomerId.HasValue) 
                            continue;
                        results.Add(new CustomerProduct() 
                        { 
                            CustomerName = customer.CustomerName,
                            IsSelected = false
                        });
                    }
                }
                // set match product
                if (null != data)
                {
                    foreach (var map in data)
                    {
                        int idx = results.FindIndex(cust => 
                        {
                            if (null == cust || string.IsNullOrEmpty(cust.CustomerName) || 
                                null == map || string.IsNullOrEmpty(map.CustomerName))
                            {
                                return false;
                            }
                            else
                            {
                                return cust.CustomerName.ToLower() == map.CustomerName.ToLower();
                            }
                        });
                        if (idx != -1)
                        {
                            results[idx].ProductCode = map.ProductCode;
                            results[idx].ServiceLifeMonth = map.ServiceLifeMonth;
                            results[idx].Remark = map.Remark;
                            results[idx].IsSelected = true;
                        }
                    }
                }

                rets.Success(results);
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
                rets.data = new List<CustomerProduct>();
            }

            return rets;
        }

        public static NDbResult Save(Product product, List<CustomerProduct> values)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == product || null == values)
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
            p.Add("@ProductCode", product.ProductCode);

            try
            {
                cnn.Execute("DELETE FROM CustomerProduct WHERE ProductCode = @ProductCode", p, commandType: CommandType.Text);
                ret.Success();
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            if (!ret.Ok) return ret;

            values.ForEach(v => 
            {
                if (v.IsSelected)
                {
                    try
                    {
                        var p2 = new DynamicParameters();
                        p2.Add("@ProductCode", product.ProductCode);
                        p2.Add("@CustomerName", v.CustomerName);
                        p2.Add("@ServiceLifeMonth", v.ServiceLifeMonth);
                        p2.Add("@Remark", v.Remark);

                        cnn.Execute("INSERT INTO CustomerProduct (ProductCode, CustomerName, ServiceLifeMonth, Remark) VALUES (@ProductCode, @CustomerName, @ServiceLifeMonth, @Remark);", p2, commandType: CommandType.Text);
                        ret.Success();
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                        // Set error number/message
                        ret.ErrNum = 9999;
                        ret.ErrMsg = ex.Message;
                    }
                }
            });

            return ret;
        }

        #endregion
    }
}
