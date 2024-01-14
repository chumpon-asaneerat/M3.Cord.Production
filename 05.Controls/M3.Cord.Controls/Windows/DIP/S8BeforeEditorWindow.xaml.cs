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
using System.Windows.Shapes;

using NLib.Models;
using M3.Cord.Models;
using NLib;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for S8BeforeEditorWindow.xaml
    /// </summary>
    public partial class S8BeforeEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S8BeforeEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private S8BeforeCondition _item = null;

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null != _item)
            {
                var ret = S8BeforeCondition.Save(_item);
                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();
            }
            DialogResult = true;
        }

        #endregion

        #region Public Methods

        public void Setup(S8BeforeCondition item)
        {
            _item = item;
            this.DataContext = _item;
            if (null != _item)
            {

            }
        }

        #endregion
    }
}
