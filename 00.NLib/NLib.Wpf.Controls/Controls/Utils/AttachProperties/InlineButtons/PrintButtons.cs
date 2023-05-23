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
    #region PrintButtons

    /// <summary>
    /// The PrintButtons class.
    /// </summary>
    public class PrintButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(PrintOperations),
            typeof(PrintButtons),
            new PropertyMetadata(PrintOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static PrintOperations GetIconType(DependencyObject obj)
        {
            return (PrintOperations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, PrintOperations value)
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
                PrintOperations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? PrintOperations.None :
                        (PrintOperations)Enum.Parse(typeof(PrintOperations), sVal);
                }
                catch (Exception)
                {
                    val = PrintOperations.None;
                }

                Style style = null;
                switch (val)
                {
                    case PrintOperations.Print:
                        style = (Style)Application.Current.Resources["fa-print"];
                        break;
                    case PrintOperations.Preview:
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
