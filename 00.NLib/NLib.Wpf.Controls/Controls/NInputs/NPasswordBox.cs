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

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion
    }
}
