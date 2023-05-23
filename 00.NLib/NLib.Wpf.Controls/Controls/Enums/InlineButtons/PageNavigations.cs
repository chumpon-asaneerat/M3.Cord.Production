#region Using

using System;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The Page Navigation Enum.
    /// </summary>
    public enum PageNavigations : uint
    {
        None = 0x00000000,
        Home = 0x00000001,
        Back = 0x00000002,
        Close = 0x00000004
    }
}
