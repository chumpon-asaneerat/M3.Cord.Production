#region Using

using System;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The Dialog Navigation Enum.
    /// </summary>
    public enum DialogNavigations : uint
    {
        None = 0x00000000,
        Yes = 0x00000001,
        No = 0x00000002,
        Ok = 0x00000004,
        Cancel = 0x00000008
    }
}
