#region Using

using System;

#endregion

namespace NLib.Wpf.Controls
{
    /// <summary>
    /// The Record Operation Enum.
    /// </summary>
    public enum RecordOperations : uint
    {
        None = 0x00000000,
        Add = 0x00000001,
        Edit = 0x00000002,
        Delete = 0x00000004,
        Save = 0x00000008
    }
}
