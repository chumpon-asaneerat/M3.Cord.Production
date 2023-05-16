﻿#region Using

using NLib.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace NLib.Controls
{
    /// <summary>
    /// Interaction logic for ExcelOperationButton.xaml
    /// </summary>
    public partial class ExcelOperationButton : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ExcelOperationButton()
        {
            InitializeComponent();
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

        /// <summary>
        /// The IconTypeProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IconTypeProperty =
            DependencyProperty.Register("IconType", typeof(ExcelOperationEnum), typeof(ExcelOperationButton));
        /// <summary>
        /// Gets or sets Inline Button Icon.
        /// </summary>
        public ExcelOperationEnum IconType
        {
            get { return (ExcelOperationEnum)GetValue(IconTypeProperty); }
            set { SetValue(IconTypeProperty, value); }
        }

        #endregion

        #region Text

        /// <summary>
        /// The TextProperty Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ExcelOperationButton));
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
