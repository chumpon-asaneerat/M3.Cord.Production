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
    /// The CheckBox Ex Control.
    /// </summary>
    public class CheckBoxEx : CheckBox
    {
        static CheckBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBoxEx), new FrameworkPropertyMetadata(typeof(CheckBoxEx)));
        }
    }
}
