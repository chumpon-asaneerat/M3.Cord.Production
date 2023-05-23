#region Using

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
                var page = Pages.CordMainMenu;
                PageContentManager.Instance.Current = page;
            }

            #endregion

            #region Main Menu (Cord)

            /// <summary>Gets Cord MainMenu Page.</summary>
            public static Cord.Pages.CordMainMenuPage CordMainMenu
            {
                get { return GetPage<Cord.Pages.CordMainMenuPage>(); }
            }

            #endregion

            #region G4
            /*
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
            */
            #endregion

            #region Warehouse
            /*
            /// <summary>Gets WareHouse Receive Yarn Page.</summary>
            public static Cord.Pages.CordReceiveYarnPage WareHouseReceiveYarn
            {
                get { return GetPage<Cord.Pages.CordReceiveYarnPage>(); }
            }
            /// <summary>Gets WareHouse Planning Page.</summary>
            public static Cord.Pages.CordPlanningPage WarehousePlanning
            {
                get { return GetPage<Cord.Pages.CordPlanningPage>(); }
            }
            */
            #endregion

            #region First Twist
            /*
            /// <summary>Gets First Twist MC Page.</summary>
            public static Cord.Pages.FirstTwistMCPage FirstTwistMC
            {
                get { return GetPage<Cord.Pages.FirstTwistMCPage>(); }
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

            #region G4
            /*
            /// <summary>Gets G4 Receive Yarn Window.</summary>
            public static G4ReceiveYarnWindow G4ReceiveYarn
            {
                get { return GetWindow<G4ReceiveYarnWindow>(); }
            }
            */
            #endregion

            #region CordWarehouse
            /*
            /// <summary>Gets Cord Procuct Planning Window.</summary>
            public static CordProcuctPlanningWindow CordProductPlanning
            {
                get { return GetWindow<CordProcuctPlanningWindow>(); }
            }
            */
            #endregion

            #region First Twist
            /*
            /// <summary>Gets Choose Cord Product Window.</summary>
            public static ChooseCordProductWindow ChooseCordProduct
            {
                get { return GetWindow<ChooseCordProductWindow>(); }
            }

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
