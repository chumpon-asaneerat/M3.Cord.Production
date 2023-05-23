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
    #region ClipboardButtons

    /// <summary>
    /// The ClipboardButtons class.
    /// </summary>
    public class ClipboardButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(ClipboardOperations),
            typeof(ClipboardButtons),
            new PropertyMetadata(ClipboardOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static ClipboardOperations GetIconType(DependencyObject obj)
        {
            return (ClipboardOperations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, ClipboardOperations value)
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
                ClipboardOperations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? ClipboardOperations.None :
                        (ClipboardOperations)Enum.Parse(typeof(ClipboardOperations), sVal);
                }
                catch (Exception)
                {
                    val = ClipboardOperations.None;
                }

                Style style = null;
                switch (val)
                {
                    case ClipboardOperations.Cut:
                        style = (Style)Application.Current.Resources["fa-cut"];
                        break;
                    case ClipboardOperations.Copy:
                        style = (Style)Application.Current.Resources["fa-copy"];
                        break;
                    case ClipboardOperations.Paste:
                        style = (Style)Application.Current.Resources["fa-paste"];
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
