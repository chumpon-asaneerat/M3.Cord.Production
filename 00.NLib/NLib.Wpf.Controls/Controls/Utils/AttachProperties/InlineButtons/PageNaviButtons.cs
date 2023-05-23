#region Using

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace NLib.Wpf.Controls.Utils.InlineButtons
{
    #region PageNaviButtons

    /// <summary>
    /// The PageNaviButtons class.
    /// </summary>
    public class PageNaviButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(PageNavigations),
            typeof(PageNaviButtons),
            new PropertyMetadata(PageNavigations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static PageNavigations GetIconType(DependencyObject obj)
        {
            return (PageNavigations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, PageNavigations value)
        {
            obj.SetValue(IconTypeProperty, value);
        }

        #endregion

        #region IconType Changed Handler

        private static void IconTypePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (null != obj && obj is TextBlock)
            {
                TextBlock ctrl = obj as TextBlock;
                string sVal = (null != e.NewValue) ? e.NewValue.ToString() : null;
                PageNavigations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? PageNavigations.None :
                        (PageNavigations)Enum.Parse(typeof(PageNavigations), sVal);
                }
                catch (Exception)
                {
                    val = PageNavigations.None;
                }

                Style style = null;
                switch (val)
                {
                    case PageNavigations.Home:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;
                    case PageNavigations.Back:
                        style = (Style)Application.Current.Resources["fa-goback"];
                        break;
                    case PageNavigations.Close:
                        style = (Style)Application.Current.Resources["fa-close"];
                        break;
                    default:
                        {
                            // FontAwesomeIcon.None
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                }
            }
        }

        #endregion
    }

    #endregion
}
