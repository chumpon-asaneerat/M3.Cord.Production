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
    /// Interaction logic for SampleNInputPage.xaml
    /// </summary>
    public partial class SampleNInputPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SampleNInputPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        #endregion

        private Sample inst = new Sample();

        #region Private Method

        private void Init()
        {
            this.DataContext = inst;
        }

        #endregion
    }

    public class Sample : NLib.NInpc
    {
        public int? Id
        {
            get { return Get<int?>(); }
            set { Set(value); }
        }
        public decimal? Value
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { Raise(() => ValueColor); }); }
        }
        public string Name
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public Brush ValueColor
        {
            get { return (Value.HasValue && Value.Value > 10) ? Brushes.Red : Brushes.Black; }
            set { }
        }

        public DateTime? UpdateDate
        {
            get { return Get<DateTime?>(); }
            set { Set(value); }
        }
    }
}