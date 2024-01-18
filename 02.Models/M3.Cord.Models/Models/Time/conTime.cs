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
    public class conTime
    {
        #region Public Proeprties

        #endregion

        #region Static Methods
        public static List<string> GetMixHours()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            List<string> rets = new List<string>();

            try
            {
                for (int i = 0; i <= 23; i++)
                {
                    if (i <= 9)
                    {
                        rets.Add("0" + i.ToString());
                    }
                    else
                    {
                        rets.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return rets;
        }
        public static List<string> GetMixMinutes()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            List<string> rets = new List<string>();

            try
            {
                for (int i = 0; i <= 59; i++)
                {
                    if (i <= 9)
                    {
                        rets.Add("0" + i.ToString());
                    }
                    else
                    {
                        rets.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return rets;
        }
        public static List<string> GetQuaHours()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            List<string> rets = new List<string>();

            try
            {
                for (int i = 0; i <= 23; i++)
                {
                    if (i <= 9)
                    {
                        rets.Add("0" + i.ToString());
                    }
                    else
                    {
                        rets.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return rets;
        }
        public static List<string> GetQuaMinutes()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            List<string> rets = new List<string>();

            try
            {
                for (int i = 0; i <= 59; i++)
                {
                    if (i <= 9)
                    {
                        rets.Add("0" + i.ToString());
                    }
                    else
                    {
                        rets.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return rets;
        }
        public static List<string> GetExpHours()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            List<string> rets = new List<string>();

            try
            {
                for (int i = 0; i <= 23; i++)
                {
                    if (i <= 9)
                    {
                        rets.Add("0" + i.ToString());
                    }
                    else
                    {
                        rets.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return rets;
        }
        public static List<string> GetExpMinutes()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            List<string> rets = new List<string>();

            try
            {
                for (int i = 0; i <= 59; i++)
                {
                    if (i <= 9)
                    {
                        rets.Add("0" + i.ToString());
                    }
                    else
                    {
                        rets.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return rets;
        }

        #endregion
    }
}
