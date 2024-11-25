﻿#region Using

using NLib.Services;
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
            AppIdleService.Instance.Tick += Instance_Tick;
            AppIdleService.Instance.ResetLastInputTime(); // Reset time
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            AppIdleService.Instance.Tick -= Instance_Tick;
        }

        #endregion

        private Sample inst = new Sample();

        #region Private Method

        private void Init()
        {
            this.DataContext = inst;
            cbLookup.ItemsSource = Lookup.Gets();
            //inst.CarId = "C02";
            cbLookup.SelectedIndex = 1;
        }

        #endregion

        private void UpdateMessage(string format, params object[] args)
        {
            this.InvokeAction(() => 
            {
                txtMsg3.Text = string.Format(format, args);
            });
        }

        private int timeout = 5;

        private void Instance_Tick(object sender, EventArgs e)
        {
            // Idle Tick
            TimeSpan idleTs = DateTime.Now - AppIdleService.Instance.LastSystemInputTime;
            UpdateMessage("Idle: {0:n0} s", idleTs.TotalSeconds);

            if (AppIdleService.Instance.IsTimeOut(timeout, false))
            {
                UpdateMessage("Timeout. Reset!!");

                AppIdleService.Instance.ResetLastInputTime();
            }
        }

        private void txtName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var iCnt = txtName.Text.Length;
                txtCnt.Text = string.Format("Name chars: {0}", iCnt);
            }
        }

        private void cmdCheck_Click(object sender, RoutedEventArgs e)
        {
            var pwd = txtPwd.Password;
            txtCnt.Text = string.Format("Password: {0}", pwd);
        }

        private void cbLookup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cbLookup.SelectedItem;
            string msg = "Select Brand: ";
            msg += (null != cbLookup.SelectedItem && cbLookup.SelectedItem is Lookup) ? ((Lookup)cbLookup.SelectedItem).Brand : string.Empty;
            txtMsg2.Text = msg;
        }
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

        public string CarId
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

    public class Lookup
    {
        public string CarId { get; set; }
        public string Brand { get; set; }

        public static List<Lookup> Gets()
        {
            return new List<Lookup>() 
            { 
                new Lookup() { CarId = "C01", Brand = "Toyota" },
                new Lookup() { CarId = "C02", Brand = "Honda" },
                new Lookup() { CarId = "C03", Brand = "Yamaha" },
                new Lookup() { CarId = "C04", Brand = "Nisson" },
                new Lookup() { CarId = "C05", Brand = "Suzuki" }
            };
        }
    }
}