#region Using

using System;

#endregion

namespace NLib.Wpf.Controls
{
    #region PageNavigationEnum and Flags

    /// <summary>
    /// The Page Navigation Enum.
    /// </summary>
    public enum PageNavigationEnum : uint
    {
        None = 0x00000000,
        Home = 0x00000001,
        Back = 0x00000002,
        Close = 0x00000004
    }
    /// <summary>
    /// The Page Navigation Flags.
    /// </summary>
    [Flags]
    public enum PageNavigationFlags : uint
    {
        None = 0x00000000,
        Home = 0x00000001,
        Back = 0x00000002,
        Close = 0x00000004
    }

    #endregion

    #region RecordOperationEnum and Flags

    /// <summary>
    /// The Record Operation Enum.
    /// </summary>
    public enum RecordOperationEnum : uint
    {
        None = 0x00000000,
        Add = 0x00000001,
        Edit = 0x00000002,
        Delete = 0x00000004,
        Save = 0x00000008
    }
    /// <summary>
    /// The Record Operation Flags.
    /// </summary>
    [Flags]
    public enum RecordOperationFlags : uint
    {
        None = 0x00000000,
        Add = 0x00000001,
        Edit = 0x00000002,
        Delete = 0x00000004,
        Save = 0x00000008
    }

    #endregion

    #region ExcelOperationEnum and Flags

    /// <summary>
    /// The Excel Operation Enum.
    /// </summary>
    public enum ExcelOperationEnum : uint
    {
        None = 0x00000000,
        Import = 0x00000001,
        Export = 0x00000002
    }
    /// <summary>
    /// The Excel Operation Flags.
    /// </summary>
    [Flags]
    public enum ExcelOperationFlags : uint
    {
        None = 0x00000000,
        Import = 0x00000001,
        Export = 0x00000002
    }

    #endregion

    #region PrintOperationEnum and Flags

    /// <summary>
    /// The Print Operation Enum.
    /// </summary>
    public enum PrintOperationEnum : uint
    {
        None = 0x00000000,
        Print = 0x00000001,
        Preview = 0x00000002
    }
    /// <summary>
    /// The Print Operation Flags.
    /// </summary>
    [Flags]
    public enum PrintOperationFlags : uint
    {
        None = 0x00000000,
        Print = 0x00000001,
        Preview = 0x00000002
    }

    #endregion

    #region DialogOptionEnum and Flags

    /// <summary>
    /// The Dialog Option Enum.
    /// </summary>
    public enum DialogOptionEnum : uint
    {
        None = 0x00000000,
        Yes = 0x00000001,
        No = 0x00000002,
        Ok = 0x00000004,
        Cancel = 0x00000008
    }
    /// <summary>
    /// The Dialog Option Flags.
    /// </summary>
    [Flags]
    public enum DialogOptionFlags : uint
    {
        None = 0x00000000,
        Yes = 0x00000001,
        No = 0x00000002,
        Ok = 0x00000004,
        Cancel = 0x00000008
    }

    #endregion

    #region SearchOperationEnum and Flags

    /// <summary>
    /// The Search Operation Enum.
    /// </summary>
    public enum SearchOperationEnum : uint
    {
        None = 0x00000000,
        Search = 0x00000001,
        Refresh = 0x00000002
    }
    /// <summary>
    /// The Search Operation Flags.
    /// </summary>
    [Flags]
    public enum SearchOperationFlags : uint
    {
        None = 0x00000000,
        Search = 0x00000001,
        Refresh = 0x00000002
    }

    #endregion

    #region ClipboardOperationEnum and Flags

    /// <summary>
    /// The Clipboard Operation Enum.
    /// </summary>
    public enum ClipboardOperationEnum : uint
    {
        None = 0x00000000,
        Cut = 0x00000001,
        Copy = 0x00000002,
        Paste = 0x00000004
    }
    /// <summary>
    /// The Clipboard Operation Flags.
    /// </summary>
    [Flags]
    public enum ClipboardOperationFlags : uint
    {
        None = 0x00000000,
        Cut = 0x00000001,
        Copy = 0x00000002,
        Paste = 0x00000004
    }

    #endregion

    #region FontAwesomeIcon Enum

    /// <summary>
    /// The FontAwesomeIcon Enum.
    /// </summary>
    public enum FontAwesomeIcon : uint
    {
        None = 0x00000000, // OK
        Home = 0x00000001, // OK
        Back = 0x00000002, // OK
        Close = 0x00000004, // OK
        Add = 0x00000008, // OK
        Edit = 0x00000010, // OK
        Save = 0x00000020, // OK
        Delete = 0x00000040, // OK
        Cut = 0x00000080, // OK
        Copy = 0x00000100, // OK
        Paste = 0x00000200, // OK
        Import = 0x00000400, // OK
        Export = 0x00000800, // OK
        Print = 0x00001000, // OK
        Preview = 0x00002000, // OK
        Search = 0x00004000, // OK
        Refresh = 0x00008000, // OK
        Scan = 0x00010000,
        Yes = 0x00020000, // OK
        No = 0x00040000, // OK
        Ok = 0x00080000, // OK
        Cancel = 0x00100000 // OK
    }

    #endregion

    #region FontAwesomeButtons Flags

    /// <summary>
    /// The FontAwesomeButtons Flags.
    /// </summary>
    [Flags]
    public enum FontAwesomeButtons : uint
    {
        None = 0x00000000,
        Home = 0x00000001,
        Back = 0x00000002,
        Close = 0x00000004,
        Add = 0x00000008,
        Edit = 0x00000010,
        Save = 0x00000020,
        Delete = 0x00000040,
        Cut = 0x00000080,
        Copy = 0x00000100,
        Paste = 0x00000200,
        Import = 0x00000400,
        Export = 0x00000800,
        Print = 0x00001000,
        Preview = 0x00002000,
        Search = 0x00004000,
        Refresh = 0x00008000,
        Scan = 0x00010000,
        Yes = 0x00020000,
        No = 0x00040000,
        Ok = 0x00080000,
        Cancel = 0x00100000
    }

    #endregion
}
