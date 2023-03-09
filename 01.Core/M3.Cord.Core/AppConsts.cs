#region Using

using System;

#endregion

namespace M3.Cord
{
    public static class AppConsts
    {
        // common properties
        public static string Version = "1";
        public static string Minor = "0";
        public static string CompanyName = "VS Enterprise Group Co., Ltd.";

        public static class Application
        {
            public static class Cord
            {
                public static class Production
                {
                    public static string ApplicationName = @"M3 Cord Production Application";
                    // common
                    public static string Version = AppConsts.Version;
                    public static string Minor = AppConsts.Minor;
                    public static string Build = "1";
                    public static DateTime LastUpdate = new DateTime(2023, 3, 9, 23, 20, 00);
                }

                public static class QA
                {
                    public static string ApplicationName = @"M3 Cord QA Application";
                    // common
                    public static string Version = AppConsts.Version;
                    public static string Minor = AppConsts.Minor;
                    public static string Build = "1";
                    public static DateTime LastUpdate = new DateTime(2023, 3, 9, 23, 20, 00);
                }
            }
        }
    }
}
