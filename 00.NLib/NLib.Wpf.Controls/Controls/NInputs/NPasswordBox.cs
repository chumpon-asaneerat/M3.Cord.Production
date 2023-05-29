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
    /// The NPasswordBox Control
    /// </summary>
    public class NPasswordBox : NInputControlBase
    {
        #region Constructor

        static NPasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPasswordBox),
                new FrameworkPropertyMetadata(typeof(NPasswordBox)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPasswordBox() : base()
        {

        }

        #endregion

        #region Internal Variables

        PasswordBox ctrl;

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ctrl = null;
            var obj = GetTemplateChild("ctrl");
            if (null != obj && obj is PasswordBox)
            {
                ctrl = (PasswordBox)obj;
            }
        }

        #endregion

        #region Public Properties

        #region InputForeground

        /// <summary>
        /// The InputForegroundProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty InputForegroundProperty =
            DependencyProperty.Register(
                nameof(InputForeground),
                typeof(Brush),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets InputForeground.
        /// </summary>
        public Brush InputForeground
        {
            get { return (Brush)GetValue(InputForegroundProperty); }
            set { SetValue(InputForegroundProperty, value); }
        }

        #endregion

        #region Password

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password 
        {
            get { return (null != ctrl) ? ctrl.Password : null; }
            set 
            { 
                if (null != ctrl)
                {
                    ctrl.Password = value;
                }
            } 
        }

        #endregion

        #region TextAlignment

        /// <summary>
        /// The TextAlignmentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register(
                nameof(TextAlignment),
                typeof(TextAlignment),
                typeof(NPasswordBox),
                new FrameworkPropertyMetadata(TextAlignment.Left, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets TextAlignment.
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        #endregion

        #endregion
    }
}
