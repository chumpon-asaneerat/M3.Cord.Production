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
    /// Interaction logic for IndexerBinding2.xaml
    /// </summary>
    public partial class IndexerBinding2 : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexerBinding2()
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

        #region Private Methods

        private void Init()
        {
            
        }

        #endregion
    }

    public class DoubleToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            string strValue = value as string;
            if (strValue != null)
            {
                double result;
                bool converted = Double.TryParse(strValue, out result);
                if (converted)
                {
                    return result;
                }
            }
            return null;
        }
    }

    public enum DistanceType
    {
        Miles,
        Kilometres
    }

    public class DistanceConverter
    {
        public string Convert(double amount, DistanceType distancetype)
        {
            if (distancetype == DistanceType.Miles)
                return (amount * 1.6).ToString("0.##") + " km";

            if (distancetype == DistanceType.Kilometres)
                return (amount * 0.6).ToString("0.##") + " m";

            throw new ArgumentOutOfRangeException("distanceType");
        }
    }
}
