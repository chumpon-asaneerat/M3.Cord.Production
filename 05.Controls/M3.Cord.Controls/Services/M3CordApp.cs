﻿#region Using

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

            #region Main Menu

            #region Cord Main Menu

            /// <summary>Gets Cord MainMenu Page.</summary>
            public static Cord.Pages.CordMainMenuPage CordMainMenu
            {
                get { return GetPage<Cord.Pages.CordMainMenuPage>(); }
            }

            /*
            private static M3.Cord.Pages.CordMainMenuPage _CordMainMenu;

            /// <summary>Gets Cord MainMenu Page.</summary>
            public static M3.Cord.Pages.CordMainMenuPage CordMainMenu
            {
                get
                {
                    if (null == _CordMainMenu)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _CordMainMenu = new M3.Cord.Pages.CordMainMenuPage();
                        }
                    }
                    return _CordMainMenu;
                }
            }
            */
            #endregion

            #endregion

            #region G4

            #region Receive Raw Material
            /*
            private static M3.Cord.Pages.G4ReceiveRawMaterialPage _G4ReceiveRawMaterial;

            /// <summary>Gets G4 Receive Raw Material Page.</summary>
            public static M3.Cord.Pages.G4ReceiveRawMaterialPage G4ReceiveRawMaterial
            {
                get
                {
                    if (null == _G4ReceiveRawMaterial)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _G4ReceiveRawMaterial = new M3.Cord.Pages.G4ReceiveRawMaterialPage();
                        }
                    }
                    return _G4ReceiveRawMaterial;
                }
            }
            */
            #endregion

            #region Receive Raw Material
            /*
            private static M3.Cord.Pages.G4IssueRawMaterialPage _G4IssueRawMaterial;

            /// <summary>Gets G4 Issue Raw Material Page.</summary>
            public static M3.Cord.Pages.G4IssueRawMaterialPage G4IssueRawMaterial
            {
                get
                {
                    if (null == _G4IssueRawMaterial)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _G4IssueRawMaterial = new M3.Cord.Pages.G4IssueRawMaterialPage();
                        }
                    }
                    return _G4IssueRawMaterial;
                }
            }
            */
            #endregion

            #region Stock
            /*
            private static M3.Cord.Pages.G4StockPage _G4Stock;

            /// <summary>Gets G4 Stock Page.</summary>
            public static M3.Cord.Pages.G4StockPage G4Stock
            {
                get
                {
                    if (null == _G4Stock)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _G4Stock = new M3.Cord.Pages.G4StockPage();
                        }
                    }
                    return _G4Stock;
                }
            }
            */
            #endregion

            #endregion

            #region Cord Warehouse

            #region Cord Receive Yarn
            /*
            private static M3.Cord.Pages.CordReceiveYarnPage _CordReceiveYarn;

            /// <summary>Gets Cord Receive Yarn.</summary>
            public static M3.Cord.Pages.CordReceiveYarnPage CordReceiveYarn
            {
                get
                {
                    if (null == _CordReceiveYarn)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _CordReceiveYarn = new M3.Cord.Pages.CordReceiveYarnPage();
                        }
                    }
                    return _CordReceiveYarn;
                }
            }
            */
            #endregion

            #region Cord Planning
            /*
            private static M3.Cord.Pages.CordPlanningPage _CordPlanning;

            /// <summary>Gets Cord Planning Page.</summary>
            public static M3.Cord.Pages.CordPlanningPage CordPlanning
            {
                get
                {
                    if (null == _CordPlanning)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _CordPlanning = new M3.Cord.Pages.CordPlanningPage();
                        }
                    }
                    return _CordPlanning;
                }
            }
            */
            #endregion

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
            #region SignIn
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
