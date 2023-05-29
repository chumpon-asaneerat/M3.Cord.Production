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
    /// NInputControlBase.
    /// </summary>
    public abstract class NInputControlBase : Control
    {
        #region Constructor

        static NInputControlBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NInputGroup), 
                new FrameworkPropertyMetadata(typeof(NInputGroup)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NInputControlBase() : base()
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
            DependencyProperty.Register("CaptionText", typeof(string), typeof(NInputControlBase));
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
            DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(NInputControlBase));
        /// <summary>
        /// Gets or sets Caption Foreground.
        /// </summary>
        public Brush CaptionForeground
        {
            get { return (Brush)GetValue(CaptionForegroundProperty); }
            set { SetValue(CaptionForegroundProperty, value); }
        }

        #endregion

        #region CaptionFontSize

        /// <summary>
        /// The CaptionFontSizeProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty CaptionFontSizeProperty =
            DependencyProperty.Register("CaptionFontSize", typeof(double), typeof(NInputControlBase));
        /// <summary>
        /// Gets or sets Caption Font Size.
        /// </summary>
        public double CaptionFontSize
        {
            get { return (double)GetValue(CaptionFontSizeProperty); }
            set { SetValue(CaptionFontSizeProperty, value); }
        }

        #endregion

        #region CaptionFontWeight

        /// <summary>
        /// The CaptionFontWeightProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty CaptionFontWeightProperty =
            DependencyProperty.Register("CaptionFontWeight", typeof(FontWeight), typeof(NInputControlBase));
        /// <summary>
        /// Gets or sets Caption Font Weight.
        /// </summary>
        public FontWeight CaptionFontWeight
        {
            get { return (FontWeight)GetValue(CaptionFontWeightProperty); }
            set { SetValue(CaptionFontWeightProperty, value); }
        }

        #endregion

        #endregion
    }
}
