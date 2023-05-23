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
    #region SearchButtons

    /// <summary>
    /// The SearchButtons class.
    /// </summary>
    public class SearchButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(SearchOperations),
            typeof(SearchButtons),
            new PropertyMetadata(SearchOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static SearchOperations GetIconType(DependencyObject obj)
        {
            return (SearchOperations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, SearchOperations value)
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
                SearchOperations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? SearchOperations.None :
                        (SearchOperations)Enum.Parse(typeof(SearchOperations), sVal);
                }
                catch (Exception)
                {
                    val = SearchOperations.None;
                }

                Style style = null;
                switch (val)
                {
                    case SearchOperations.Search:
                        style = (Style)Application.Current.Resources["fa-search"];
                        break;
                    case SearchOperations.Refresh:
                        style = (Style)Application.Current.Resources["fa-refresh"];
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
