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
using static M3.Cord.Pages.DIPUI;

#endregion

namespace M3.Cord.Pages
{
    public class DIPUI
    {
        public class PCCard
        {
            public static DIPPCCard Current(string mcCode)
            {
                DIPPCCard pcCard = null;
                if (!string.IsNullOrEmpty(mcCode))
                {
                    pcCard = DIPPCCard.Current(mcCode).Value();
                    if (null == pcCard)
                    {
                        pcCard = new DIPPCCard();
                        pcCard.MCCode = mcCode; // set mc
                        var win = M3CordApp.Windows.DIPPCCardEditor;
                        win.Setup(pcCard);
                        if (win.ShowDialog() == true)
                        {
                            DIPPCCard.Save(pcCard);
                        }
                        else
                        {
                            pcCard = null;
                        }
                    }
                }

                return pcCard;
            }
        }
    }
}
