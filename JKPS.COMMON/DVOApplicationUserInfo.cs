using System;
using System.Collections.Generic;
using System.Text;

namespace JKPS.COMMON
{
    public class DVOApplicationUserInfo
    {

        #region Constructor

        public DVOApplicationUserInfo()
        {

        }

        #endregion Constructor

        #region Public Properties

        public static int UserId;

        public static int RoleId;

        public static string LoginId;

        public static string MachineInfo;
        public static string _mcurr = "N";
        public static string _mtax = "N";
        public static string _mtax_desc = "N";
        public static DateTime CurrentDate = DateTime.Now;

        public static string DatabaseName = string.Empty;
        public static string DatabaseDesc = string.Empty;

        //  Added By Rahul Jain on 17/11/2008 for getting LastPwdUpdateDate
        public static DateTime LastPwdUpdDate;
        public static DateTime UserexpiresDate;
        //****************************

        private static string _CurPeriod = string.Empty;
        private static string _CurYear = string.Empty;
        private static string _menustyle = string.Empty;
        public static int LoginLogId = 0;

        //Added By Rahul Jain Using Print opemn order Summary report
        public static string _hcurr = "N";


        private static string _Ministry = string.Empty;
        private static string _MDepartment = string.Empty;

        //public static string DateFormat
        //{
        //    get { return "yyyy/MM/dd"; }
        //}
        public static string DateFormat
        {
            get { return "dd/MM/yyyy"; }
        }

        public static DateTime DateConvertion(DateTime? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString.ToString()))
            {
                dateString = Convert.ToDateTime("01/01/1900", System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            }
            DateTime DateValue = Convert.ToDateTime(dateString, System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            //DateTime DateValue =  DateTime.ParseExact(dateString.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat),"dd/MM/yyyy",System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            return DateValue;
        }
        public static string DateConvertionStr(DateTime? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString.ToString()))
            {
                dateString = Convert.ToDateTime("01/01/1900", System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            }
            DateTime DateValue = Convert.ToDateTime(dateString, System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            return DateValue.ToString(DateFormat, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static DateTime ParseDateConvertion(string dateString)
        {
            DateTime date;
            if (string.IsNullOrWhiteSpace(dateString.ToString()) || dateString.Contains("1900"))
            {
                date = Convert.ToDateTime("01/01/1900", System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            }
            DateTime DateValue = Convert.ToDateTime(dateString, System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            //return DateValue.ToString(DateFormat, System.Globalization.CultureInfo.InvariantCulture);
            //DateTime DateValue = DateTime.ParseExact(dateString, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //DateTime DateValue = Convert.ToDateTime(dateString, System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            return DateValue;
        }
        public static string ParseDateConvertionStr(string dateString)
        {
            // DateTime DateValue = DateTime.ParseExact(dateString, DVOApplicationUserInfo.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime DateValue = Convert.ToDateTime(dateString, System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
            return DateValue.ToShortDateString();
        }

        static string _employeeid = string.Empty;
        public static string EmployeeId
        {
            get { return _employeeid; }
            set { _employeeid = value; }
        }
        public static string Ministry
        {
            get { return _Ministry; }
            set { _Ministry = value; }
        }

        public static string MinisDepartment
        {
            get { return _MDepartment; }
            set { _MDepartment = value; }
        }
        public static string CurPeriod
        {
            get { return _CurPeriod; }
            set { _CurPeriod = value; }
        }
        public static string CurYear
        {
            get { return _CurYear; }
            set { _CurYear = value; }
        }
        public static string MenuStyle
        {
            get { return _menustyle; }
            set { _menustyle = value; }
        }
        #endregion Public Properties

        // #region Stored-Procedures

        //public override string INSERT_SPNAME
        // {
        //     get { return "A"; }
        // }

        //public override string UPDATE_SPNAME
        // {
        //     get { return "B"; }
        // }

        //public override string DELETE_SPNAME
        // {
        //     get { return "C"; }
        // }

        //public override  string FIND_SPNAME
        //{
        //    get { return "E"; }
        //}

        // public override string ALL_SPNAME
        // {
        //     get { return "G"; }
        // }
        //public override string FIND_QUERY(ref Object[] parameters)
        //{
        //    return "";
        //}

        // #endregion Stored-Procedures
    }
}
