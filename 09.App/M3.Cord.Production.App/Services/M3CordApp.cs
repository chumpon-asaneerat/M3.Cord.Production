#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
using M3.Models;
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
            #region Main Menu

            private static M3.Cord.Pages.MainMenuPage _MainMenu;

            /// <summary>Gets MainMenu Page.</summary>
            public static M3.Cord.Pages.MainMenuPage MainMenu
            {
                get
                {
                    if (null == _MainMenu)
                    {
                        lock (typeof(M3CordApp))
                        {
                            _MainMenu = new M3.Cord.Pages.MainMenuPage();
                        }
                    }
                    return _MainMenu;
                }
            }

            #endregion
        }
    }
}
