#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for FontAwesomeButton.xaml
    /// </summary>
    public partial class FontAwesomeButton : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FontAwesomeButton()
        {
            InitializeComponent();
            //IconType = FontAwesomeIcon.None;
        }

        #endregion

        #region Private Methods

        #region Dispatcher helper method (invoke event)

        private void InvokeAction(Action action)
        {
            try
            {
                if (null == action) return;
                if (null != Application.Current.Dispatcher)
                {
                    Application.Current.Dispatcher.BeginInvoke(action);
                }
                else
                {
                    action();
                }
            }
            catch { }
        }

        #endregion

        #region Button Handlers

        private void cmd_Click(object sender, RoutedEventArgs e)
        {
            if (null != Click)
            {
                InvokeAction(new Action(() =>
                {
                    e.Source = this; // Change source.
                    Click(this, e);
                }));
            }
        }

        #endregion

        #endregion

        #region Public Properties

        #region IconType
        /*
        /// <summary>
        /// The IconTypeProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IconTypeProperty = 
            DependencyProperty.Register("IconType", typeof(FontAwesomeIcon), typeof(FontAwesomeButton),
                new PropertyMetadata(FontAwesomeIcon.None, IconTypePropertyChanged));
        /// <summary>
        /// Gets or sets Font Awesome Icon Type.
        /// </summary>
        public FontAwesomeIcon IconType
        {
            get { return (FontAwesomeIcon)GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }
        */
        private static void IconTypePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            /*
            if (null != obj && obj is TextBlock)
            {
                TextBlock ctrl = obj as TextBlock;
                if (null == ctrl) return;

                string sVal = (null != e.NewValue) ? e.NewValue.ToString() : null;
                FontAwesomeIcon val;
                try
                {
                    val = (string.IsNullOrEmpty(sVal)) ? FontAwesomeIcon.None :
                        (FontAwesomeIcon)Enum.Parse(typeof(FontAwesomeIcon), sVal);
                }
                catch (Exception)
                {
                    val = FontAwesomeIcon.None;
                }

                Style style = null;
                switch (val)
                {
                    case FontAwesomeIcon.Cut:
                        style = (Style)Application.Current.Resources["fa-cut"];
                        break;
                    case FontAwesomeIcon.Copy:
                        style = (Style)Application.Current.Resources["fa-copy"];
                        break;
                    case FontAwesomeIcon.Paste:
                        style = (Style)Application.Current.Resources["fa-paste"];
                        break;
                    case FontAwesomeIcon.Add:
                        style = (Style)Application.Current.Resources["fa-addnew"];
                        break;
                    case FontAwesomeIcon.Edit:
                        style = (Style)Application.Current.Resources["fa-edit"];
                        break;
                    case FontAwesomeIcon.Save:
                        style = (Style)Application.Current.Resources["fa-save"];
                        break;
                    case FontAwesomeIcon.Delete:
                        style = (Style)Application.Current.Resources["fa-remove"];
                        break;
                    case FontAwesomeIcon.Search:
                        style = (Style)Application.Current.Resources["fa-search"];
                        break;
                    case FontAwesomeIcon.Refresh:
                        style = (Style)Application.Current.Resources["fa-refresh"];
                        break;
                    case FontAwesomeIcon.Print:
                        style = (Style)Application.Current.Resources["fa-print"];
                        break;
                    case FontAwesomeIcon.Preview:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;
                    case FontAwesomeIcon.Home:
                        style = (Style)Application.Current.Resources["fa-home"];
                        break;
                    case FontAwesomeIcon.Back:
                        style = (Style)Application.Current.Resources["fa-goback"];
                        break;
                    case FontAwesomeIcon.Close:
                        style = (Style)Application.Current.Resources["fa-close"];
                        break;
                    case FontAwesomeIcon.Import:
                        style = (Style)Application.Current.Resources["fa-import"];
                        break;
                    case FontAwesomeIcon.Export:
                        style = (Style)Application.Current.Resources["fa-export"];
                        break;
                    case FontAwesomeIcon.Ok:
                        style = (Style)Application.Current.Resources["fa-ok"];
                        break;
                    case FontAwesomeIcon.Cancel:
                        style = (Style)Application.Current.Resources["fa-cancel"];
                        break;
                    case FontAwesomeIcon.Yes:
                        style = (Style)Application.Current.Resources["fa-yes"];
                        break;
                    case FontAwesomeIcon.No:
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
            */
        }

        #endregion

        #region Text

        /// <summary>
        /// The TextProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FontAwesomeButton));
        /// <summary>
        /// Gets or sets Inline Button Text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #endregion

        #region Public Events

        /// <summary>
        /// The Click Event.
        /// </summary>
        public event RoutedEventHandler Click;

        #endregion
    }
}
