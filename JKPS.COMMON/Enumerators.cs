using System;
using System.Collections.Generic;
using System.Text;



namespace JKPS.COMMON
{
    public enum ModuleTypeIdEnum
    {
        FOLDER = 1,
        FORM = 2,
        REPORT = 3
    }

    public static class ModuleTypeEnum
    {
        private static string[] arrModuleType = { "FOLDER", "FORM", "REPORT" };

        public static string FOLDER
        {
            get { return arrModuleType[0]; }
        }
        public static string FORM
        {
            get { return arrModuleType[1]; }
        }
        public static string REPORT
        {
            get { return arrModuleType[2]; }
        }
    }

    public static class ModuleTypeImagesEnum
    {
        private static string[] arrModuleType = { "CLOSEFOLDER", "FILE", "BROWSE" };

        public static string FOLDER
        {
            get { return arrModuleType[0]; }
        }
        public static string FORM
        {
            get { return arrModuleType[1]; }
        }
        public static string REPORT
        {
            get { return arrModuleType[2]; }
        }
    }

    public enum BoType
    {
        Account = 1,
        Ledger = 2,
        Employee = 3,
        Vendor = 4,
        Role = 5,
        Module = 6

    }
    public enum MediaType
    {
        TxnReport = 1,
        RetTxnReport = 2,
        Validation = 3,
        Validation_res = 4,
        DisbursementDownload = 5,
    }
    public enum CustomMailPriority
    {
        High = 1,
        Medium = 2,
        Low = 3,
    }
}
