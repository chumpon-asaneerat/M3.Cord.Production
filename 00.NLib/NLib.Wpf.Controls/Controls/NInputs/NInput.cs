#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The NInput Control
    /// </summary>
    [ContentProperty(nameof(Content))]
    public class NInput : Control
    {
        #region Constructor

        static NInput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NInput), new FrameworkPropertyMetadata(typeof(NInput)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NInput()
        {

        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region CaptionText

        /// <summary>
        /// The CaptionTextProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty CaptionTextProperty =
            DependencyProperty.Register("CaptionText", typeof(string), typeof(NInput));
        /// <summary>
        /// Gets or sets Caption Text.
        /// </summary>
        public string CaptionText
        {
            get { return (string)GetValue(CaptionTextProperty); }
            set { SetValue(CaptionTextProperty, value); }
        }

        #endregion

        #region CaptionForeground

        /// <summary>
        /// The CaptionForegroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty CaptionForegroundProperty =
            DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(NInput));
        /// <summary>
        /// Gets or sets Caption Foreground.
        /// </summary>
        public Brush CaptionForeground
        {
            get { return (Brush)GetValue(CaptionForegroundProperty); }
            set { SetValue(CaptionForegroundProperty, value); }
        }

        #endregion

        #region Content

        /// <summary>
        /// The ContentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(NInput));
        /// <summary>
        /// Gets or sets Items.
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        #endregion

        #endregion
    }
}
