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
    /// Interaction logic for SolutionMenuPage.xaml
    /// </summary>
    public partial class SolutionMenuPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SolutionMenuPage()
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

        private void cmdCreateSolutionLot_Click(object sender, RoutedEventArgs e)
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

            // Pallet Setting
            var page = M3CordApp.Pages.CreateRecipe;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSolutionDetail_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Pallet Setting
            var page = M3CordApp.Pages.SolutionDetail;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdRecordWeight_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Pallet Setting
            var page = M3CordApp.Pages.RecordActualWeight;
            page.Setup();
            PageContentManager.Instance.Current = page;
        }

        private void cmdIssueLabel_Click(object sender, RoutedEventArgs e)
        {
            // Sign In
            var win = M3CordApp.Windows.SignIn;
            if (win.ShowDialog() == false) return;
            // Issue Solution Label.
            var page = M3CordApp.Pages.SolitionSlipManage;
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
