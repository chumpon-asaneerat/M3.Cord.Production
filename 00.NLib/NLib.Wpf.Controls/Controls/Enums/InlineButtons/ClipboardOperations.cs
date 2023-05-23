#region Using

using System;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The Clipboard Operation Enum.
    /// </summary>
    public enum ClipboardOperations : uint
    {
        None = 0x00000000,
        Cut = 0x00000001,
        Copy = 0x00000002,
        Paste = 0x00000004
    }
}
