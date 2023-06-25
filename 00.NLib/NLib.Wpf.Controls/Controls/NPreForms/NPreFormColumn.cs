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
    /// The NPreFormColumn Control.
    /// </summary>
    public class NPreFormColumn : Control
    {
        #region Constructor

        static NPreFormColumn()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPreFormColumn), new FrameworkPropertyMetadata(typeof(NPreFormColumn)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPreFormColumn()
        {
            // init
            //Items = new ObservableCollection<NGroupMenuItem>();
        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region HeaderForeground

        /// <summary>
        /// The HeaderForegroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register(
                nameof(HeaderForeground),
                typeof(Brush),
                typeof(NPreFormColumn),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets HeaderForeground.
        /// </summary>
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }

        #endregion

        #region HeaderBackground

        /// <summary>
        /// The HeaderBackgroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register(
                nameof(HeaderBackground),
                typeof(Brush),
                typeof(NPreFormColumn),
                new FrameworkPropertyMetadata(Brushes.Silver, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets HeaderBackground.
        /// </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        #endregion

        #region HeaderText

        /// <summary>
        /// The HeaderTextProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register(
                nameof(HeaderText),
                typeof(string),
                typeof(NPreFormColumn),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets Header Text.
        /// </summary>
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        #endregion

        #region HeaderTextAlignment

        /// <summary>
        /// The HeaderTextAlignmentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTextAlignmentProperty =
            DependencyProperty.Register(
                nameof(HeaderTextAlignment),
                typeof(TextAlignment),
                typeof(NPreFormColumn),
                new FrameworkPropertyMetadata(TextAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets HeaderTextAlignment.
        /// </summary>
        public TextAlignment HeaderTextAlignment
        {
            get { return (TextAlignment)GetValue(HeaderTextAlignmentProperty); }
            set { SetValue(HeaderTextAlignmentProperty, value); }
        }

        #endregion

        #endregion
    }
}
