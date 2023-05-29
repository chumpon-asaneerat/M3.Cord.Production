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
    /// The NTextBox Control
    /// </summary>
    public class NTextBox : NInputControlBase
    {
        #region Constructor

        static NTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NTextBox), 
                new FrameworkPropertyMetadata(typeof(NTextBox)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NTextBox() : base()
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

        #region Text

        /// <summary>
        /// The TextProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NTextBox));
        /// <summary>
        /// Gets or sets Text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region TextAlignment

        /// <summary>
        /// The TextAlignmentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty TextAlignmentProperty =
            DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(NTextBox));
        /// <summary>
        /// Gets or sets TextAlignment.
        /// </summary>
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        #endregion

        #region TextAlignment

        /// <summary>
        /// The TextAlignmentProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(NTextBox));
        /// <summary>
        /// Gets or sets TextWrapping.
        /// </summary>
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }

        #endregion

        #region AcceptsReturn

        /// <summary>
        /// The AcceptsReturnProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty AcceptsReturnProperty =
            DependencyProperty.Register("AcceptsReturn", typeof(bool), typeof(NTextBox));
        /// <summary>
        /// Gets or sets AcceptsReturn.
        /// </summary>
        public bool AcceptsReturn
        {
            get { return (bool)GetValue(AcceptsReturnProperty); }
            set { SetValue(AcceptsReturnProperty, value); }
        }

        #endregion

        #region AcceptsTab

        /// <summary>
        /// The AcceptsTabProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty AcceptsTabProperty =
            DependencyProperty.Register("AcceptsTab", typeof(bool), typeof(NTextBox));
        /// <summary>
        /// Gets or sets AcceptsTab.
        /// </summary>
        public bool AcceptsTab
        {
            get { return (bool)GetValue(AcceptsTabProperty); }
            set { SetValue(AcceptsTabProperty, value); }
        }

        #endregion

        #endregion
    }
}
