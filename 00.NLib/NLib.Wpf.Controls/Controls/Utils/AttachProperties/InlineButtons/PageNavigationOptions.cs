#region Using

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace NLib.Wpf.Controls.Utils
{
    #region PageNavigationOptions

    /// <summary>
    /// The PageNavigationOptions class.
    /// </summary>
    public class PageNavigationOptions
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(PageNavigationEnum),
            typeof(PageNavigationOptions),
            new PropertyMetadata(PageNavigationEnum.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static PageNavigationEnum GetIconType(DependencyObject obj)
        {
            return (PageNavigationEnum)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, PageNavigationEnum value)
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
                PageNavigationEnum val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? PageNavigationEnum.None :
                        (PageNavigationEnum)Enum.Parse(typeof(PageNavigationEnum), sVal);
                }
                catch (Exception)
                {
                    val = PageNavigationEnum.None;
                }

                Style style = null;
                switch (val)
                {
                    case PageNavigationEnum.Home:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;
                    case PageNavigationEnum.Back:
                        style = (Style)Application.Current.Resources["fa-goback"];
                        break;
                    case PageNavigationEnum.Close:
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
