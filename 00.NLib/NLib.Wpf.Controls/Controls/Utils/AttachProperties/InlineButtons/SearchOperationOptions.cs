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
    #region SearchOperationOptions

    /// <summary>
    /// The SearchOperationOptions class.
    /// </summary>
    public class SearchOperationOptions
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(SearchOperationEnum),
            typeof(SearchOperationOptions),
            new PropertyMetadata(SearchOperationEnum.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static SearchOperationEnum GetIconType(DependencyObject obj)
        {
            return (SearchOperationEnum)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, SearchOperationEnum value)
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
                SearchOperationEnum val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? SearchOperationEnum.None :
                        (SearchOperationEnum)Enum.Parse(typeof(SearchOperationEnum), sVal);
                }
                catch (Exception)
                {
                    val = SearchOperationEnum.None;
                }

                Style style = null;
                switch (val)
                {
                    case SearchOperationEnum.Search:
                        style = (Style)Application.Current.Resources["fa-search"];
                        break;
                    case SearchOperationEnum.Refresh:
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
