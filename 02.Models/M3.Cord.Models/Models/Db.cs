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
using System.Windows.Media;

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

        #region FirstTwistMC

        public static List<FirstTwistMC> Machines = new List<FirstTwistMC>()
        {
            new FirstTwistMC() { MCName = "S-1", SheetPerBB = 2 },
            new FirstTwistMC() { MCName = "S-4" }
        };

        public static void LoadMachines()
        {
            string fileName = Path.Combine(DbPath, "Machines.json");
            var models = NJson.LoadFromFile<List<FirstTwistMC>>(fileName);
            if (null != models)
            {
                Machines = models;
            }
        }

        public static void SaveMachines()
        {
            string fileName = Path.Combine(DbPath, "Machines.json");
            Machines.SaveToFile(fileName);
        }

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
            LoadMachines();
        }

        public static void Save()
        {
            SaveCordProducts();
            SaveMachines();
        }

        #endregion
    }
}
