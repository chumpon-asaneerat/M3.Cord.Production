#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.IO;

using M3.Cord.Models;

#endregion

namespace M3.Cord
{
    public class LobaclDb
    {
        public static List<CordProduct> Products = new List<CordProduct>();

        public static string[] ItemYarns = new string[] 
        {
                "700-108-178E-TTS",
                "470-72-1781-JJ",
                "470-136-178E-APM",
                "470-136-178E-TTS"
        };
    }
}
