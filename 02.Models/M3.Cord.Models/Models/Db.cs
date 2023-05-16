#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.IO;

using M3.Cord.Models;

#endregion

namespace M3.Cord
{
    public class LobaclDb
    {
        #region CordProduct

        public static List<CordProduct> Products = new List<CordProduct>();

        public static void LoadCordProducts()
        {
            string fileName = Path.Combine(DbPath, "CordProduct.json");
            var models = NJson.LoadFromFile<List<CordProduct>>(fileName);
            if (null != models)
            {
                Products = models;
            }
        }

        public static void SaveCordProducts()
        {
            string fileName = Path.Combine(DbPath, "CordProduct.json");
            Products.SaveToFile(fileName);
        }

        #endregion

        #region ItemYarns

        public static string[] ItemYarns = new string[] 
        {
                "700-108-178E-TTS",
                "470-72-1781-JJ",
                "470-136-178E-APM",
                "470-136-178E-TTS"
        };

        #endregion

        #region Path

        private static string DbPath
        {
            get 
            {
                var assem = Assembly.GetExecutingAssembly();
                string rootPath = Path.GetDirectoryName(assem.Location);
                var dbPath = Path.Combine(rootPath, "db");
                if (!Directory.Exists(dbPath)) 
                    Directory.CreateDirectory(dbPath);
                return dbPath;
            }
        }

        #endregion

        #region Load/Save

        public static void Load()
        {
            LoadCordProducts();
        }

        public static void Save()
        {
            SaveCordProducts();
        }

        #endregion
    }
}
