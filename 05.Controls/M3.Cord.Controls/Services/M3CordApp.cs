#region Using

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

            #endregion

            #region Warehouse

            /// <summary>Gets WareHouse Receive Yarn Page.</summary>
            public static Cord.Pages.WareHouseReceiveYarnPage WareHouseReceiveYarn
            {
                get { return GetPage<Cord.Pages.WareHouseReceiveYarnPage>(); }
            }
            /// <summary>Gets WareHouse Planning Page.</summary>
            public static Cord.Pages.WarehousePlanningPage CordPlanning
            {
                get { return GetPage<Cord.Pages.WarehousePlanningPage>(); }
            }

            #endregion

            #region First Twist

            #region First Twist MC
            /*
            private static M3.Cord.Pages.FirstTwistMCPage _FirstTwistMC;

            /// <summary>Gets First Twist MC Page.</summary>
            public static M3.Cord.Pages.FirstTwistMCPage FirstTwistMC
            {
                get
                {
                    if (null == _FirstTwistMC)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _FirstTwistMC = new M3.Cord.Pages.FirstTwistMCPage();
                        }
                    }
                    return _FirstTwistMC;
                }
            }
            */
            #endregion

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

            #region SignIn

            /// <summary>Gets SignIn Window.</summary>
            public static Cord.Windows.SignInWindow SignIn
            {
                get { return GetWindow<Cord.Windows.SignInWindow>(); }
            }
            /*
            /// <summary>Gets SignIn Window.</summary>
            public static M3.Cord.Windows.SignInWindow SignIn
            {
                get
                {
                    var ret = new M3.Cord.Windows.SignInWindow();
                    ret.Owner = Application.Current.MainWindow;
                    return ret;
                }
            }
            */
            #endregion

            #region G4

            #region G4 Receive Yarn
            /*
            /// <summary>Gets SignIn Window.</summary>
            public static M3.Cord.Windows.G4ReceiveYarnWindow G4ReceiveYarn
            {
                get
                {
                    var ret = new M3.Cord.Windows.G4ReceiveYarnWindow();
                    ret.Owner = Application.Current.MainWindow;
                    return ret;
                }
            }
            */
            #endregion

            #endregion
        }
    }
}
