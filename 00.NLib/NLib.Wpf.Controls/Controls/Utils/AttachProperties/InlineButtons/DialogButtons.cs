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
    #region DialogButtons

    /// <summary>
    /// The DialogButtons class.
    /// </summary>
    public class DialogButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(DialogOptionEnum),
            typeof(DialogButtons),
            new PropertyMetadata(DialogOptionEnum.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        public static DialogOptionEnum GetIconType(DependencyObject obj)
        {
            return (DialogOptionEnum)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, FontAwesomeIcon value)
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
                DialogOptionEnum val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? DialogOptionEnum.None :
                        (DialogOptionEnum)Enum.Parse(typeof(DialogOptionEnum), sVal);
                }
                catch (Exception)
                {
                    val = DialogOptionEnum.None;
                }

                Style style = null;
                switch (val)
                {
                    case DialogOptionEnum.Ok:
                        style = (Style)Application.Current.Resources["fa-ok"];
                        break;
                    case DialogOptionEnum.Cancel:
                        style = (Style)Application.Current.Resources["fa-cancel"];
                        break;
                    case DialogOptionEnum.Yes:
                        style = (Style)Application.Current.Resources["fa-yes"];
                        break;
                    case DialogOptionEnum.No:
                        style = (Style)Application.Current.Resources["fa-no"];
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
