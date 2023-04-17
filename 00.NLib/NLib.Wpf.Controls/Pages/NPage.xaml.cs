#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

namespace NLib.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for NPage.xaml
    /// </summary>
    public partial class NPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        #region PageTitle

        /// <summary>
        /// The PageTitleProperty dependency property.
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
        /// The WorkAreaProperty dependency property.
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

        #region Public Events

        #endregion
    }
}
