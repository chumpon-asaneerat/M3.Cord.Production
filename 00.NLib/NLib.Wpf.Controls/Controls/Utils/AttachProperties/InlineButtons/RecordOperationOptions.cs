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
    #region RecordOperationOptions

    /// <summary>
    /// The RecordOperationOptions class.
    /// </summary>
    public class RecordOperationOptions
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(RecordOperationEnum),
            typeof(RecordOperationOptions),
            new PropertyMetadata(RecordOperationEnum.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static RecordOperationEnum GetIconType(DependencyObject obj)
        {
            return (RecordOperationEnum)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, RecordOperationEnum value)
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
                RecordOperationEnum val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? RecordOperationEnum.None :
                        (RecordOperationEnum)Enum.Parse(typeof(RecordOperationEnum), sVal);
                }
                catch (Exception)
                {
                    val = RecordOperationEnum.None;
                }

                Style style = null;
                switch (val)
                {
                    case RecordOperationEnum.Add:
                        style = (Style)Application.Current.Resources["fa-addnew"];
                        break;
                    case RecordOperationEnum.Edit:
                        style = (Style)Application.Current.Resources["fa-edit"];
                        break;
                    case RecordOperationEnum.Save:
                        style = (Style)Application.Current.Resources["fa-save"];
                        break;
                    case RecordOperationEnum.Delete:
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
