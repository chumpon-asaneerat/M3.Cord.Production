#region Using

using M3.Cord.Models;
using M3.Cord.Windows;
using NLib.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
//using M3.Cord.Models;
//using M3.Services;

#endregion

namespace M3.Cord
{
    public class M3CordApp
    {
        /// <summary>
        /// Current.
        /// </summary>
        public static class Current
        {
            /// <summary>
            /// Gets Current User.
            /// </summary>
            public static UserInfo User { get { return SignInManager.Instance.User; } }
        }

        /// <summary>
        /// Pages Static class.
        /// </summary>
        public static class Pages
        {
            #region Static Methods

            private static Dictionary<Type, UserControl> _pages = new Dictionary<Type, UserControl>();

            private static T GetPage<T>()
                where T : UserControl, new()
            {
                Type type = typeof(T);
                if (!_pages.ContainsKey(type)) 
                {
                    lock (typeof(M3CordApp))
                    {
                        T inst = new T();
                        _pages.Add(type, inst);
                    }
                }

                T ret = _pages[type] as T;

                return ret;
            }

            #endregion

            #region Common Method(s)

            /// <summary>
            /// Goto Cord Main Menu.
            /// </summary>
            public static void GotoCordMainMenu()
            {
                var page = Pages.M3CordMainMenu;
                page.Setup();
                PageContentManager.Instance.Current = page;
            }
            /// <summary>
            /// Goto Cord Master Menu.
            /// </summary>
            public static void GotoCordMasterMenu()
            {
                var page = Pages.MasterDataMenu;
                PageContentManager.Instance.Current = page;
            }

            #endregion

            #region Main Menu (Cord)

            /// <summary>Gets M3 Cord MainMenu Page.</summary>
            public static Cord.Pages.M3CordMainMenuPage M3CordMainMenu
            {
                get 
                { 
                    return GetPage<Cord.Pages.M3CordMainMenuPage>(); 
                }
            }

            #endregion

            #region Master Data Menu

            /// <summary>Gets M3 Cord Master Data Menu Page.</summary>
            public static Cord.Pages.MasterDataManuPage MasterDataMenu
            {
                get { return GetPage<Cord.Pages.MasterDataManuPage>(); }
            }

            #endregion

            #region Master Data

            /// <summary>Gets M3 Cord Product Item Code Manage Page.</summary>
            /*
            public static Cord.Pages.ProductItemCodeManagePage ProductItemCodeManage
            {
                get { return GetPage<Cord.Pages.ProductItemCodeManagePage>(); }
            }
            */
            /// <summary>Gets M3 Cord Customer Manage Page.</summary>
            /*
            public static Cord.Pages.CustomerManagePage CustomerManage
            {
                get { return GetPage<Cord.Pages.CustomerManagePage>(); }
            }
            */

            #endregion

            #region Document Menu (Cord) - Temporary

            /// <summary>Gets M3 Cord Document Menu Page.</summary>
            /*
            public static Cord.Pages.M3DocumentMenuPage M3CordDocumentMenu
            {
                get { return GetPage<Cord.Pages.M3DocumentMenuPage>(); }
            }
            */
            #endregion

            #region G4

            /// <summary>Gets G4 Receive Raw Material Page.</summary>
            public static Cord.Pages.G4ReceiveRawMaterialPage G4ReceiveRawMaterial
            {
                get { return GetPage<Cord.Pages.G4ReceiveRawMaterialPage>(); }
            }
            /// <summary>Gets G4 Issue Raw Material Page.</summary>
            public static Cord.Pages.G4IssueRawMaterialPage G4IssueRawMaterial
            {
                get { return GetPage<Cord.Pages.G4IssueRawMaterialPage>(); }
            }
            /// <summary>Gets G4 Stock Page.</summary>
            public static Cord.Pages.G4StockPage G4Stock
            {
                get { return GetPage<Cord.Pages.G4StockPage>(); }
            }
            /// <summary>Gets G4 Go Label Preview Page.</summary>
            public static Cord.Pages.GOLabelPreviewPage GoLabelPreview
            {
                get { return GetPage<Cord.Pages.GOLabelPreviewPage>(); }
            }
            /// <summary>Gets G4 Receive Return Meterial Page.</summary>
            public static Cord.Pages.G4ReceiveReturnMeterialPage G4ReceiveReturnMeterial
            {
                get { return GetPage<Cord.Pages.G4ReceiveReturnMeterialPage>(); }
            }

            #endregion

            #region Warehouse

            /// <summary>Gets WareHouse Receive Yarn Page.</summary>
            public static Cord.Pages.WarehouseReceiveYarnPage WarehouseReceiveYarn
            {
                get { return GetPage<Cord.Pages.WarehouseReceiveYarnPage>(); }
            }

            /// <summary>Gets WareHouse Check Stock Yarn Page.</summary>
            public static Cord.Pages.WarehouseYarnStockPage WarehouseYarnStock
            {
                get { return GetPage<Cord.Pages.WarehouseYarnStockPage>(); }
            }

            #endregion

            #region PCCard

            /// <summary>Gets PC Card Manage Page.</summary>
            public static Cord.Pages.PCCardManagePage PCCardManage
            {
                get { return GetPage<Cord.Pages.PCCardManagePage>(); }
            }

            #endregion

            #region First Twist

            /// <summary>Gets First Twist MC Page.</summary>
            public static Cord.Pages.FirstTwistMCPage FirstTwistMC
            {
                get { return GetPage<Cord.Pages.FirstTwistMCPage>(); }
            }
            /// <summary>Gets FirstTwist Operation Page.</summary>
            public static Cord.Pages.FirstTwistOperationPage FirstTwistOperation
            {
                get { return GetPage<Cord.Pages.FirstTwistOperationPage>(); }
            }

            /// <summary>Gets S1ProductCondition Page.</summary>
            /*
            public static Cord.Pages.S1ProductConditionPage S1ProductCondition
            {
                get { return GetPage<Cord.Pages.S1ProductConditionPage>(); }
            }
            */
            /// <summary>Gets S4x1UpTwisterConditionPage Page.</summary>
            /*
            public static Cord.Pages.S4x1UpTwisterConditionPage S4x1UpTwisterCondition
            {
                get { return GetPage<Cord.Pages.S4x1UpTwisterConditionPage>(); }
            }
            */
            /// <summary>Gets S4x2UpTwisterCondition Page.</summary>
            /*
            public static Cord.Pages.S4x2UpTwisterConditionPage S4x2UpTwisterCondition
            {
                get { return GetPage<Cord.Pages.S4x2UpTwisterConditionPage>(); }
            }
            */

            #endregion
        }

        /// <summary>
        /// Windows Static class.
        /// </summary>
        public static class Windows
        {
            #region Static Methods

            private static T GetWindow<T>()
                where T : Window, new()
            {
                T inst = new T();
                inst.Owner = Application.Current.MainWindow;
                return inst;
            }

            #endregion

            #region MessageBox

            /// <summary>Gets MessageBox Window.</summary>
            public static MessageBoxWindow MessageBox
            {
                get { return GetWindow<Cord.Windows.MessageBoxWindow>(); }
            }

            #endregion

            #region MessageBoxOKCancel

            /// <summary>Gets MessageBoxOkCancel Window.</summary>
            public static MessageBoxOKCancelWindow MessageBoxOKCancel
            {
                get { return GetWindow<Cord.Windows.MessageBoxOKCancelWindow>(); }
            }

            #endregion

            #region SignIn

            /// <summary>Gets SignIn Window.</summary>
            public static SignInWindow SignIn
            {
                get { return GetWindow<SignInWindow>(); }
            }

            #endregion

            #region Import

            /// <summary>Gets Import Condition Std Window.</summary>
            public static ImportConditionStdWindow ImportConditionStd
            {
                get { return GetWindow<ImportConditionStdWindow>(); }
            }

            #endregion

            #region Common Progress Dialog

            /// <summary>Gets Common Progress Dialog Window.</summary>
            public static ProgressWindow ProgressDialog
            {
                get { return GetWindow<ProgressWindow>(); }
            }

            #endregion

            #region Import Error Dialog

            /// <summary>Gets Import Report Dialog Window.</summary>
            public static ImportReportWindow ImportReport
            {
                get { return GetWindow<ImportReportWindow>(); }
            }

            #endregion

            #region G4

            /// <summary>Gets G4 Receive Yarn Window.</summary>
            public static G4ReceiveYarnWindow G4ReceiveYarn
            {
                get { return GetWindow<G4ReceiveYarnWindow>(); }
            }

            #endregion

            #region PC Card

            /// <summary>Gets Cord Procuct Planning Window.</summary>
            public static PCCardEditorWindow PCCardEditor
            {
                get { return GetWindow<PCCardEditorWindow>(); }
            }

            #endregion

            #region First Twist

            /// <summary>Gets Choose PC Card for First Twist Window.</summary>
            public static ChoosePCCardTwist1Window ChoosePCCardTwist1
            {
                get { return GetWindow<ChoosePCCardTwist1Window>(); }
            }

            /// <summary>Gets Twist1 Load Record Editor Window.</summary>
            public static Twist1LoadRecordEditorWindow Twist1LoadRecordEditor
            {
                get { return GetWindow<Twist1LoadRecordEditorWindow>(); }
            }
            /// <summary>Gets S1 Condition Window.</summary>
            public static Twist1S1ConditionWindow S1Condition
            {
                get { return GetWindow<Twist1S1ConditionWindow>(); }
            }

            /// <summary>Gets Start Twist1 Op Window.</summary>
            public static StartTwist1OpWindow StartTwist1Op
            {
                get { return GetWindow<StartTwist1OpWindow>(); }
            }
            /// <summary>Gets End Twist1 Op Window.</summary>
            public static EndTwist1OpWindow EndTwist1Op
            {
                get { return GetWindow<EndTwist1OpWindow>(); }
            }

            /*
            /// <summary>Gets Yarn Loading Window.</summary>
            public static YarnLoadingWindow YarnLoading
            {
                get { return GetWindow<YarnLoadingWindow>(); }
            }

            /// <summary>Gets Yarn Twist Record Sheet Window.</summary>
            public static YarnTwistRecordSheetWindow YarnTwistRecordSheet
            {
                get { return GetWindow<YarnTwistRecordSheetWindow>(); }
            }

            /// <summary>Gets Production Condition Sheet Window.</summary>
            public static ProductionConditionSheetWindow ProductionConditionSheet
            {
                get { return GetWindow<ProductionConditionSheetWindow>(); }
            }
            */
            #endregion
        }
    }
}
