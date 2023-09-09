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
    /// Interaction logic for IndexerBinding1.xaml
    /// </summary>
    public partial class IndexerBinding1 : UserControl
    {
        #region Constructor

        public IndexerBinding1()
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

        #region Buttoh Handlers

        private void cmdCheckValue_Click(object sender, RoutedEventArgs e)
        {
            UpdateValues();
        }

        private void cmdDisable_Click(object sender, RoutedEventArgs e)
        {
            SetRadioButtonsEnable(false);
        }

        private void cmdEnable_Click(object sender, RoutedEventArgs e)
        {
            SetRadioButtonsEnable(true);
        }

        #endregion

        #region Private Methods

        private Dictionary<string, object> _data = new Dictionary<string, object>();

        private void Init()
        {
            _data["field1"] = new Field1() { No = 100 };
            _data["field2"] = "Pete Brown";
            _data["field3"] = new DateTime(2010, 03, 08);

            this.DataContext = _data;
        }

        private void UpdateValues()
        {
            if (null != _data)
            {
                string sOut = string.Empty;
                foreach (var key in _data.Keys) 
                {
                    sOut += string.Format("'{0}' = '{1}'. ", key, _data[key]);
                }
                
                txtOutput.Text = sOut;
            }
        }

        private void SetRadioButtonsEnable(bool enabled)
        {
            rbS11.IsEnabled = enabled;
            rbS12.IsEnabled = enabled;
            rbS13.IsEnabled = enabled;
        }

        #endregion
    }

    public class Field1
    {
        public override string ToString()
        {
            return this.No.ToString();
        }

        public int No { get; set; }
    }
}
