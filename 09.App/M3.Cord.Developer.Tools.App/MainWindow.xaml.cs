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

using System.IO;
using System.Reflection;
using M3.Cord.Models;

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
            string rootPath = System.IO.Path.GetDirectoryName(assem.Location);
            string importPath = System.IO.Path.Combine(rootPath, "Imports");
            string fileName = System.IO.Path.Combine(importPath, "itemcode.json");
            var model = NJson.LoadFromFile<JsonModel<CordItemCode>>(fileName);
            if (null != model)
            {
                DbServer.Instance.Start();
                model.Items.ForEach(item => { CordItemCode.Save(item); });
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
