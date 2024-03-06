#region Using

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

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for ProductionReportMenu.xaml
    /// </summary>
    public partial class ProductionReportMenu : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductionReportMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdMainMenu_Click(object sender, RoutedEventArgs e)
        {
            M3CordApp.Pages.GotoCordMainMenu();
        }

        private void cmdFirstTwistSlip_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            /*
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }
            */

            // Pallet Setting
            var page = M3CordApp.Pages.PalletSettingManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdDIPSlip_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            /*
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }
            */

            // Pallet Setting
            var page = M3CordApp.Pages.DIPPalletSlipManage;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdS9CHLabel_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            /*
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }
            */

            var win2 = M3CordApp.Windows.LabelCHS9Search;
            win2.Setup();
            if (win2.ShowDialog() == true)
            {
                string lotNo = win2.DIPLotNo;
                int? doffNo = win2.DoffNo;
                int? spStart = win2.SPStart;
                int? spEnd = win2.SPEnd;
                var items = Models.LabelCHS9.Gets(lotNo, doffNo, spStart, spEnd);
                var page = M3CordApp.Pages.LabelCHS9PrintPreview;
                page.Setup(items, false);
                PageContentManager.Instance.Current = page;
            }
        }

        private void cmdFirstTwistReports_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }

            // First Twist History
            var page = M3CordApp.Pages.FirstTwistHistorySearch;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdAgeingReports_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }

            // Ageing History
            var page = M3CordApp.Pages.AgeingHistorySearch;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdDIPReports_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;

            if (null == M3CordApp.Current.User)
            {
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ไม่พบข้อมูลผู้ใช้ในระบบ");
                msgbox.ShowDialog();
                return;
            }
            if (M3CordApp.Current.User.RoleId > 10)
            {
                // Role 1 : Admin
                // Role 10 : Supervisor
                var msgbox = M3CordApp.Windows.MessageBox;
                msgbox.Setup("ผู้ใช้ปัจจุบันไม่มีสิทธิเข้าถึงหน้าจอนี้ได้");
                msgbox.ShowDialog();
                return;
            }

            // DIP History
            var page = M3CordApp.Pages.DIPHistorySearch;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Public Methods

        public void Setup()
        {

        }

        #endregion
    }
}
