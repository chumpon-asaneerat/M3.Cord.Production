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
    #region ClipboardOperationOptions

    /// <summary>
    /// The ClipboardOperationOptions class.
    /// </summary>
    public class ClipboardOperationOptions
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(ClipboardOperationEnum),
            typeof(ClipboardOperationOptions),
            new PropertyMetadata(ClipboardOperationEnum.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static ClipboardOperationEnum GetIconType(DependencyObject obj)
        {
            return (ClipboardOperationEnum)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, ClipboardOperationEnum value)
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
                ClipboardOperationEnum val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? ClipboardOperationEnum.None :
                        (ClipboardOperationEnum)Enum.Parse(typeof(ClipboardOperationEnum), sVal);
                }
                catch (Exception)
                {
                    val = ClipboardOperationEnum.None;
                }

                Style style = null;
                switch (val)
                {
                    case ClipboardOperationEnum.Cut:
                        style = (Style)Application.Current.Resources["fa-cut"];
                        break;
                    case ClipboardOperationEnum.Copy:
                        style = (Style)Application.Current.Resources["fa-copy"];
                        break;
                    case ClipboardOperationEnum.Paste:
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
