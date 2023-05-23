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

    /// <summary>
    /// The Excel Operation Enum.
    /// </summary>
    public enum ExcelOperations : uint
    {
        None = 0x00000000,
        Import = 0x00000001,
        Export = 0x00000002
    }

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

    /// <summary>
    /// The Print Operation Enum.
    /// </summary>
    public enum PrintOperations : uint
    {
        None = 0x00000000,
        Print = 0x00000001,
        Preview = 0x00000002
    }

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

    /// <summary>
    /// The Search Operation Enum.
    /// </summary>
    public enum SearchOperations : uint
    {
        None = 0x00000000,
        Search = 0x00000001,
        Refresh = 0x00000002
    }
}
