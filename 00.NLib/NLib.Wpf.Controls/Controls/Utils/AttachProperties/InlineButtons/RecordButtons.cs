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
    #region RecordButtons

    /// <summary>
    /// The RecordOperationOptions class.
    /// </summary>
    public class RecordButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(RecordOperations),
            typeof(RecordButtons),
            new PropertyMetadata(RecordOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static RecordOperations GetIconType(DependencyObject obj)
        {
            return (RecordOperations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, RecordOperations value)
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
                RecordOperations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? RecordOperations.None :
                        (RecordOperations)Enum.Parse(typeof(RecordOperations), sVal);
                }
                catch (Exception)
                {
                    val = RecordOperations.None;
                }

                Style style = null;
                switch (val)
                {
                    case RecordOperations.Add:
                        style = (Style)Application.Current.Resources["fa-addnew"];
                        break;
                    case RecordOperations.Edit:
                        style = (Style)Application.Current.Resources["fa-edit"];
                        break;
                    case RecordOperations.Save:
                        style = (Style)Application.Current.Resources["fa-save"];
                        break;
                    case RecordOperations.Delete:
                        style = (Style)Application.Current.Resources["fa-remove"];
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
