#region Using

using System;
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
            new UIPropertyMetadata(ClipboardOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
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
                if (null == ctrl) return;

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
                            // None
                            ctrl.Visibility = Visibility.Collapsed;
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                    ctrl.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }

    #endregion

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
            typeof(DialogNavigations),
            typeof(DialogButtons),
            new UIPropertyMetadata(DialogNavigations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        public static DialogNavigations GetIconType(DependencyObject obj)
        {
            return (DialogNavigations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, DialogNavigations value)
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
                if (null == ctrl) return;

                string sVal = (null != e.NewValue) ? e.NewValue.ToString() : null;
                DialogNavigations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? DialogNavigations.None :
                        (DialogNavigations)Enum.Parse(typeof(DialogNavigations), sVal);
                }
                catch (Exception)
                {
                    val = DialogNavigations.None;
                }

                Style style = null;
                switch (val)
                {
                    case DialogNavigations.Ok:
                        style = (Style)Application.Current.Resources["fa-ok"];
                        break;
                    case DialogNavigations.Cancel:
                        style = (Style)Application.Current.Resources["fa-cancel"];
                        break;
                    case DialogNavigations.Yes:
                        style = (Style)Application.Current.Resources["fa-yes"];
                        break;
                    case DialogNavigations.No:
                        style = (Style)Application.Current.Resources["fa-no"];
                        break;
                    default:
                        {
                            // None
                            ctrl.Visibility = Visibility.Collapsed;
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                    ctrl.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }

    #endregion

    #region ExcelButtons

    /// <summary>
    /// The ExcelButtons class.
    /// </summary>
    public class ExcelButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(ExcelOperations),
            typeof(ExcelButtons),
            new UIPropertyMetadata(ExcelOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
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
                if (null == ctrl) return;

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
                            // None
                            ctrl.Visibility = Visibility.Collapsed;
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                    ctrl.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }

    #endregion

    #region PageNaviButtons

    /// <summary>
    /// The PageNaviButtons class.
    /// </summary>
    public class PageNaviButtons
    {
        #region IconType

        /// <summary>The IconType variable</summary>
        public static readonly DependencyProperty IconTypeProperty = DependencyProperty.RegisterAttached(
            "IconType",
            typeof(PageNavigations),
            typeof(PageNaviButtons),
            new UIPropertyMetadata(PageNavigations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
        public static PageNavigations GetIconType(DependencyObject obj)
        {
            return (PageNavigations)obj.GetValue(IconTypeProperty);
        }
        /// <summary>
        /// Sets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <param name="value">The new value.</param>
        public static void SetIconType(DependencyObject obj, PageNavigations value)
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
                if (null == ctrl) return;

                string sVal = (null != e.NewValue) ? e.NewValue.ToString() : null;
                PageNavigations val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? PageNavigations.None :
                        (PageNavigations)Enum.Parse(typeof(PageNavigations), sVal);
                }
                catch (Exception)
                {
                    val = PageNavigations.None;
                }

                Style style = null;
                switch (val)
                {
                    case PageNavigations.Home:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;
                    case PageNavigations.Back:
                        style = (Style)Application.Current.Resources["fa-goback"];
                        break;
                    case PageNavigations.Close:
                        style = (Style)Application.Current.Resources["fa-close"];
                        break;
                    default:
                        {
                            // None
                            ctrl.Visibility = Visibility.Collapsed;
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                    ctrl.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }

    #endregion

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
            new UIPropertyMetadata(PrintOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
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
                if (null == ctrl) return;

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
                            // None
                            ctrl.Visibility = Visibility.Collapsed;
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                    ctrl.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }

    #endregion

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
            new UIPropertyMetadata(SearchOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
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
                if (null == ctrl) return;

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
                            // None
                            ctrl.Visibility = Visibility.Collapsed;
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                    ctrl.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }

    #endregion

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
            new UIPropertyMetadata(RecordOperations.None, IconTypePropertyChanged));

        /// <summary>
        /// Gets IconType Value.
        /// </summary>
        /// <param name="obj">The target object.</param>
        /// <returns>Returns current proeprty value.</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBlock))]
        //[AttachedPropertyBrowsableForChildren(IncludeDescendants = false)]
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
                if (null == ctrl) return;

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
                            // None
                            ctrl.Visibility = Visibility.Collapsed;
                        }
                        break;
                }
                // Apply style
                if (null != style)
                {
                    ctrl.Style = style;
                    ctrl.Visibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }

    #endregion
}
