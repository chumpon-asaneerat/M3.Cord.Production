﻿#region Using

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
    /// The NPreForm Control.
    /// </summary>
    public class NPreForm : Control
    {
        #region Constructor

        static NPreForm()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NPreForm), new FrameworkPropertyMetadata(typeof(NPreForm)));
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public NPreForm()
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

        #endregion
    }
}
