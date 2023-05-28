#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
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

namespace NLib.Wpf.Pages
{
    /// <summary>
    /// The NPage Control
    /// </summary>
    [TemplatePart(Name = "PART_CaptionBorder", Type = typeof(Border))]
    [TemplatePart(Name = "PART_CaptionText", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_WorkAreaContent", Type = typeof(ContentPresenter))]
    [ContentProperty(nameof(WorkArea))]
    public class NPage : Control
    {
        #region Internal Variables

        protected Border CaptionBorder { get; private set; }
        protected TextBlock CaptionTextBlock { get; private set; }
        protected ContentPresenter WorkAreaContentPresenter { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static NPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPage), new FrameworkPropertyMetadata(typeof(NPage)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPage()
        {

        }

        #endregion

        #region Override Methods

        public override void OnApplyTemplate()
        {
            CaptionBorder = Template.FindName("PART_CaptionBorder", this) as Border;
            CaptionTextBlock = Template.FindName("PART_CaptionText", this) as TextBlock;
            WorkAreaContentPresenter = Template.FindName("PART_WorkAreaContent", this) as ContentPresenter;

            base.OnApplyTemplate();
        }

        #endregion

        #region Public Properties

        #region PageTitle

        /// <summary>
        /// The PageTitleProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register("PageTitle", typeof(string), typeof(NPage));
        /// <summary>
        /// Gets or sets Page Title.
        /// </summary>
        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        #endregion

        #region WorkArea

        /// <summary>
        /// The WorkAreaProperty Dependency property.
        /// </summary>
        public static readonly DependencyProperty WorkAreaProperty =
            DependencyProperty.Register("WorkArea", typeof(object), typeof(NPage));
        /// <summary>
        /// Gets or sets WorkArea Content.
        /// </summary>
        public object WorkArea
        {
            get { return (object)GetValue(WorkAreaProperty); }
            set { SetValue(WorkAreaProperty, value); }
        }

        #endregion

        #endregion
    }
}
