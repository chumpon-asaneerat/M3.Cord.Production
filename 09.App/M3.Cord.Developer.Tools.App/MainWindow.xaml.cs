#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Reflection;

using NLib;
using M3.Cord.Models;

#endregion

namespace M3.Cord
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

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        #endregion

        private void cmdImportItemCode_Click(object sender, RoutedEventArgs e)
        {
            var assem = Assembly.GetExecutingAssembly();
            string rootPath = Path.GetDirectoryName(assem.Location);
            string importPath = Path.Combine(rootPath, "Imports");
            string fileName = Path.Combine(importPath, "itemcode.json");
            var model = NJson.LoadFromFile<JsonModel<CordItemCode>>(fileName);
            if (null != model)
            {
                DbServer.Instance.Start();
                model.Items.ForEach(item => { CordItemCode.Save(item); });
                DbServer.Instance.Shutdown();
            }
        }

        private void cmdImportG4Yarn_Click(object sender, RoutedEventArgs e)
        {
            var assem = Assembly.GetExecutingAssembly();
            string rootPath = Path.GetDirectoryName(assem.Location);
            string importPath = Path.Combine(rootPath, "Imports");
            string fileName = Path.Combine(importPath, "G3Yarn.json");
            var model = NJson.LoadFromFile<JsonModel<G4Yarn>>(fileName);
            if (null != model)
            {
                DbServer.Instance.Start();
                model.Items.ForEach(item => { G4Yarn.Save(item); });
                DbServer.Instance.Shutdown();
            }
        }
    }

    public class JsonModel<T> 
        where T : new()
    {
        public List<T> Items { get; set; } = new List<T>();
    }
}
