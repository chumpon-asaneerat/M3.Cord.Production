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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for DIPTimeTableEditorWindow.xaml
    /// </summary>
    public partial class DIPTimeTableEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPTimeTableEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private DIPTimeTable _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null != _item)
            {
                var hour = cbTimes.SelectedItem as TimeHour;
                if (null != hour && dtDate.SelectedDate.HasValue)
                {
                    DateTime ptime = dtDate.SelectedDate.Value.Date;
                    ptime = ptime.AddHours(hour.Hour);
                    // Set time
                    _item.PeriodTime = ptime;
                    _item.CheckBy = (null != M3CordApp.Current.User) ? M3CordApp.Current.User.FullName : null;
                    _item.CheckDate = DateTime.Now;
                    DIPTimeTable.Save(_item);
                }
            }
            DialogResult = true;
        }

        #endregion

        #region Public Methods

        public void Setup(DIPTimeTable item)
        {
            dtDate.SelectedDate = DateTime.Today;
            var hours = TimeHour.Gets();
            cbTimes.ItemsSource = hours;

            _item = item;
            this.DataContext = _item;
            if (null != _item)
            {
                int hour = DateTime.Now.Hour;
                if (null != hours)
                {
                    int idx = hours.FindIndex(h => h.Hour == hour);
                    if (idx != -1)
                    {
                        this.InvokeAction(() =>
                        {
                            cbTimes.SelectedIndex = idx;
                        });
                    }
                }
            }
        }

        #endregion

        public class TimeHour
        {
            public int Hour { get; set; }
            public string SHour
            {
                get { return string.Format("{0:D2}:00", Hour); }
                set { }
            }

            public static List<TimeHour> Gets()
            {
                var list = new List<TimeHour>();
                for (int i = 0; i < 24; ++i)
                {
                    list.Add(new TimeHour() { Hour = i });
                }
                return list;
            }
        }
    }
}
