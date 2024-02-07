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
using static M3.Cord.Windows.DIPTimeTableEditorWindow;

#endregion

namespace M3.Cord.Windows
{
    /// <summary>
    /// Interaction logic for S8x2ProductionConditionItemEditorWindow.xaml
    /// </summary>
    public partial class S8x2ProductionConditionItemEditorWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public S8x2ProductionConditionItemEditorWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private S8x2ProductionConditionItem _item = null;

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
                _item.Opertor = (null != M3CordApp.Current.User) ?
                        M3CordApp.Current.User.FullName : null;
                var ret = S8x2ProductionConditionItem.Save(_item);

                if (null != ret && ret.Ok)
                    M3CordApp.Windows.SaveSuccess();
                else M3CordApp.Windows.SaveFailed();

                DialogResult = true;
            }
        }

        #endregion

        #region Public Methods

        public void Setup(DateTime StartTime, S8x2ProductionConditionItem item)
        {
            dtDate.SelectedDate = DateTime.Today;

            _item = item;
            this.DataContext = _item;
            if (null != _item)
            {

            }
        }

        #endregion
    }
}
