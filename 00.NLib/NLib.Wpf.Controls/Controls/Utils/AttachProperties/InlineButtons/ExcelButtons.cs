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
    #region ExcelOperationOptions

    /// <summary>
    /// The ExcelOperationOptions class.
    /// </summary>
    public class ExcelButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(ExcelOperations),
            typeof(ExcelButtons),
            new PropertyMetadata(ExcelOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static ExcelOperations GetIconType(DependencyObject obj)
        {
            return (ExcelOperations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, ExcelOperations value)
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
                ExcelOperations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? ExcelOperations.None :
                        (ExcelOperations)Enum.Parse(typeof(ExcelOperations), sVal);
                }
                catch (Exception)
                {
                    val = ExcelOperations.None;
                }

                Style style = null;
                switch (val)
                {
                    case ExcelOperations.Import:
                        style = (Style)Application.Current.Resources["fa-import"];
                        break;
                    case ExcelOperations.Export:
                        style = (Style)Application.Current.Resources["fa-export"];
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
