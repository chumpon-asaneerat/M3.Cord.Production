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

using NLib.Services;
using M3.Cord.Models;
using NLib.Models;
using NLib;

#endregion

namespace M3.Cord.Pages
{
    /// <summary>
    /// Interaction logic for FirstTwistOperationPage.xaml
    /// </summary>
    public partial class FirstTwistOperationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FirstTwistOperationPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private FirstTwistMC selectedMC;

        #endregion

        #region Public Methods

        public void Setup(FirstTwistMC mc)
        {
            selectedMC = mc;
            if (null !=  selectedMC) 
            {
                page.HeaderText = "1st Twisting - " + selectedMC.MCCode;
            }
            else
            {
                page.HeaderText = "1st Twisting ";
            }

            pagePCCard.Setup(selectedMC);
            pageRawMat.Setup(selectedMC);
            pageCheckSheet.Setup(selectedMC);
            pageMCCondition.Setup(selectedMC);
        }

        #endregion
    }
}
