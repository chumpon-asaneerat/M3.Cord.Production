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
    /// Interaction logic for AdvListView2Page.xaml
    /// </summary>
    public partial class AdvListView2Page : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AdvListView2Page()
        {
            InitializeComponent();
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var doc = MDocument.GetMDocument();
            this.DataContext = doc;
            lv.ItemsSource = doc.Items;
        }
    }

    public class MDocument
    {
        public MDocument()
        {
            Items = new List<MDocumentItem>();
        }

        public List<MDocumentItem> Items { get; set; }


        public static MDocument GetMDocument()
        {
            var doc = new MDocument();
            doc.Items.Add(new MDocumentItem() 
            { 
                Name = "Item 1", Value = "Test 1", PropertyType = PropertyType.String 
            });
            doc.Items.Add(new MDocumentItem() 
            { 
                Name = "Item 2", Value = DateTime.Today, PropertyType = PropertyType.DateTime 
            });
            return doc;
        }
    }

    public enum PropertyType
    {
        String = 1,
        Int = 2,
        Decinal = 3,
        Bool = 4,
        DateTime = 5
    }

    public class MDocumentItem
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}
