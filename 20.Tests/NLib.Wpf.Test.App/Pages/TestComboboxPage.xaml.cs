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

namespace NLib.Wpf.Test.App.Pages
{
    /// <summary>
    /// Interaction logic for TestComboboxPage.xaml
    /// </summary>
    public partial class TestComboboxPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TestComboboxPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }

        #endregion

        #region Private Methods

        private void LoadComboBox()
        {
            cbItems.ItemsSource = DataItem.GetItems();
        }

        #endregion
    }

    public class DataItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<DataItem> GetItems()
        {
            var rets = new List<DataItem>();

            rets.Add(new DataItem() { Id = 1, Name = "Apple" });
            rets.Add(new DataItem() { Id = 2, Name = "Orange" });
            rets.Add(new DataItem() { Id = 3, Name = "Applicot" });
            rets.Add(new DataItem() { Id = 4, Name = "Egg" });
            rets.Add(new DataItem() { Id = 5, Name = "Olive" });
            rets.Add(new DataItem() { Id = 6, Name = "Dah?" });
            rets.Add(new DataItem() { Id = 7, Name = "Due" });

            return rets;
        }
    }
}
