using NLib.Utils;
using NLib;
using System;
using System.IO;
using System.Reflection;

namespace M3.Cord
{
    internal class ExcelResource
    {
        public const string S1 = "M3.Cord.Resources.Excels.FM4006_Sx1.xlsx";
        public const string S4x1 = "M3.Cord.Resources.Excels.FM4006_Sx4x1.xlsx";
        public const string S4x2 = "M3.Cord.Resources.Excels.FM4006_Sx4x2.xlsx";

        public const string S7YarnSetting = "M3.Cord.Resources.Excels.FM4011Sx7_YarnSetting.xlsx";

        public const string S7TimeTable = "M3.Cord.Resources.Excels.FMCS0602_S7TimeTable.xlsx";

        public const string S8 = "M3.Cord.Resources.Excels.FMCS11_S8.xlsx";
        public const string S8x2 = "M3.Cord.Resources.Excels.FMCS11_S8x2.xlsx";

        public const string S9Appearance = "M3.Cord.Resources.Excels.FM4011Sx9_APP.xlsx";

        public const string DIPConfirmCondition = "M3.Cord.Resources.Excels.DIP_Confirmaion_condition.xlsx";

        public const string SolutionAX09PV3 = "M3.Cord.Resources.Excels.AX_09PV3_8335.xlsx";
    }

    public sealed class ExcelExportUtils
    {
        #region Create Resource

        private static bool CreateResource(string resourceName,
            string FullFileName, bool AutoOverwrite)
        {
            if (File.Exists(FullFileName))
            {
                if (!AutoOverwrite)
                    throw (new Exception("File " + FullFileName + " is Exist"));
                File.Delete(FullFileName);
            }

            using (WindowFormsResourceAccess resAccess = new WindowFormsResourceAccess())
            {
                ResourceStreamOptions option = new ResourceStreamOptions()
                {
                    ResourceName = resourceName,
                    CallerType = typeof(ExcelResource),
                    TargetPath = Path.GetDirectoryName(FullFileName),
                    TargetFileName = Path.GetFileName(FullFileName)
                };

                resAccess.CreateFile(option);
            }

            return File.Exists(FullFileName);
        }

        private static void MoveFile(string sourceFileName, string targetFileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            // move file
            if (File.Exists(sourceFileName))
            {
                try
                {
                    File.Move(sourceFileName, targetFileName);
                }
                catch (Exception ex)
                {
                    ex.Err(med);
                }
            }
        }

        private static void DeleteFile(string fileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            // delete file
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    ex.Err(med);
                }
            }
        }

        private static bool CreateFileFromResource(string embededResourceName,
            string outputFileNameOnly, string FullFileName, bool AutoOverwrite)
        {
            if (string.IsNullOrWhiteSpace(embededResourceName) ||
                string.IsNullOrWhiteSpace(outputFileNameOnly) ||
                string.IsNullOrWhiteSpace(FullFileName))
                return false;

            if (CreateResource(embededResourceName, FullFileName, AutoOverwrite))
            {
                // Rename
                string resourceFileOutput =
                    Path.GetDirectoryName(FullFileName) + outputFileNameOnly;
                // Move resource output file  to target file
                MoveFile(resourceFileOutput, FullFileName);
            }

            return true;
        }

        #endregion

        #region S1, S4x1, S4x2

        /// <summary>
        /// Create S1 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS1File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S1,
                @"\FM4006_Sx1.xlsx", FullFileName, AutoOverwrite);
        }
        /// <summary>
        /// Create S4x1 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS4x1File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S4x1,
                @"\FM4006_Sx1.xlsx", FullFileName, AutoOverwrite);
        }
        /// <summary>
        /// Create S4x2 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS4x2File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S4x2,
                @"\FM4006_Sx2.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region S7 Yarn Setting

        /// <summary>
        /// Create S7 Yarn Setting File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS7YarnSettingFile(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S7YarnSetting,
                @"\FM4011Sx7_YarnSetting.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region S7 Time Table

        /// <summary>
        /// Create S7 Time Table File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS7TimeTableFile(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S7TimeTable,
                @"\FMCS0602_S7TimeTable.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region S8

        /// <summary>
        /// Create S8 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS8File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S8,
                @"\FMCS11_S8.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region S8x2

        /// <summary>
        /// Create S8x2 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS8x2File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S8x2,
                @"\FMCS11_S8x2.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region S9 Appearance

        /// <summary>
        /// Create S9 Apperance File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateS9AppearanceFile(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.S9Appearance,
                @"\FM4011Sx9_APP.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region DIP Confirm Condition

        /// <summary>
        /// Create DIP Confirm Condition File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateDIPConfirmConditionFile(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.DIPConfirmCondition,
                @"\DIP_Confirmaion_condition.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion

        #region Solution AX09PV3

        /// <summary>
        /// Create Solution AX09PV3 File
        /// </summary>
        /// <param name="FullFileName">FileName</param>
        /// <param name="AutoOverwrite">Force Overwrite</param>
        /// <returns>true if file is created</returns>
        public static bool CreateSolutionAX09PV3File(string FullFileName, bool AutoOverwrite)
        {
            return CreateFileFromResource(ExcelResource.SolutionAX09PV3,
                @"\AX_09PV3_8335.xlsx", FullFileName, AutoOverwrite);
        }

        #endregion
    }
}
