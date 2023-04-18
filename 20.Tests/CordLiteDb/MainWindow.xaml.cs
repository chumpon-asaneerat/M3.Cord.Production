#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

using NLib.Services;
using CordLiteDb.Models;

namespace CordLiteDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Closing

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LiteDbSerivce.Instance.Start();

            var db = LiteDbSerivce.Instance.Db;
            var items = db.GetCollection<Item>("Items");
            // Create an index over the Field name (if it doesn't exist)
            items.EnsureIndex(x => x.ItemCode);

            // Insert
            items.Insert(new Item { ItemCode = "N001", ItemName = "Item No 1" });
            items.Insert(new Item { ItemCode = "N002", ItemName = "Item No 2", Active = true });

            // Use LINQ to query documents (filter, sort, transform)
            var results = items.Query()
                    .Where(x => x.ItemCode.StartsWith("N"))
                    .OrderBy(x => x.ItemName)
                    .Select(x => new { x.ItemName, NameUpper = x.ItemName.ToUpper() })
                    .Limit(10)
                    .ToList();
            foreach (var row in results)
                Console.WriteLine("{0}", row.NameUpper);

            // Find one
            var r = items.FindOne(x => x.ItemCode == "N002");
            Console.WriteLine("{0}, Active: {1}", r.ItemName, r.Active);

            // Delete all
            items.DeleteAll();

            // Check No of Rows
            int iCnt = items.Count();
            Console.WriteLine("Item Count: {0}", iCnt);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LiteDbSerivce.Instance.Shutdown();
        }

        #endregion
    }
}
