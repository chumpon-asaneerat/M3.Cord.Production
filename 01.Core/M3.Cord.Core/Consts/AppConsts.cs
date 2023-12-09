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
                    public static string Build = "930";
                    public static DateTime LastUpdate = new DateTime(2023, 12, 09, 18, 50, 00);
                }

                public static class QA
                {
                    public static string ApplicationName = @"M3 Cord QA Application";
                    // common
                    public static string Version = AppConsts.Version;
                    public static string Minor = AppConsts.Minor;
                    public static string Build = "915";
                    public static DateTime LastUpdate = new DateTime(2023, 12, 06, 06, 00, 00);
                }

                public static class Developer
                {
                    public static string ApplicationName = @"M3 Cord Developer Tools Application";
                    // common
                    public static string Version = AppConsts.Version;
                    public static string Minor = AppConsts.Minor;
                    public static string Build = "915";
                    public static DateTime LastUpdate = new DateTime(2023, 12, 06, 06, 00, 00);
                }
            }
        }
    }
}
