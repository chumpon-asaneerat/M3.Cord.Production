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
    #region PrintOperationOptions

    /// <summary>
    /// The PrintOperationOptions class.
    /// </summary>
    public class PrintOperationOptions
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(PrintOperationEnum),
            typeof(PrintOperationOptions),
            new PropertyMetadata(PrintOperationEnum.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static PrintOperationEnum GetIconType(DependencyObject obj)
        {
            return (PrintOperationEnum)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, PrintOperationEnum value)
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
                PrintOperationEnum val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? PrintOperationEnum.None :
                        (PrintOperationEnum)Enum.Parse(typeof(PrintOperationEnum), sVal);
                }
                catch (Exception)
                {
                    val = PrintOperationEnum.None;
                }

                Style style = null;
                switch (val)
                {
                    case PrintOperationEnum.Print:
                        style = (Style)Application.Current.Resources["fa-print"];
                        break;
                    case PrintOperationEnum.Preview:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;
                    default:
                        {
                            // PrintOperationEnum.None
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
